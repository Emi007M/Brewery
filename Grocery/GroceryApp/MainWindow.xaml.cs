using GroceryModels.Models;
using System.Collections.Generic;
using System.Windows;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace GroceryApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ServiceClient _client;
        
        public List<Beer> Beers { get; set; }
        public List<Sale> Sales { get; set; }
        public List<SalesInfo> SalesInfos { get; set; }
        public Dictionary<Beer, int> Orders { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            _client = new ServiceClient();
            Orders = new Dictionary<Beer, int>();
            OrderPartList.ItemsSource = Orders;

            GetData();
        }

        private async Task GetData()
        {
            BeersList.IsEnabled = false;
            SalesList.IsEnabled = false;
            SellButton.IsEnabled = false;

            Beers = (await _client.GetBeers()).ToList();
            Sales = (await _client.GetSales()).ToList();

            foreach (var sale in Sales)
            {
                sale.Beer = Beers.Find(b => b.Id == sale.BeerId);
            }

            BeersList.ItemsSource = Beers;
            BeerOrderList.ItemsSource = Beers;
            BeerStatsList.ItemsSource = Beers;
            SalesList.ItemsSource = Sales;

            BeersList.IsEnabled = true;
            SalesList.IsEnabled = true;
            SellButton.IsEnabled = true;
        }

        private async void SellButton_Click(object sender, RoutedEventArgs e)
        {
            if (BeersList.SelectedItem == null) return;

            var beer = BeersList.SelectedItem as Beer;

            int amount;
            if (!int.TryParse(SaleAmountBox.Text, out amount)) return;

            var sale = new Sale()
            {
                Beer = beer,
                Amount = amount,
                Timestamp = DateTime.Now
            };
            Sales.Add(sale);
            beer.InStore -= sale.Amount;
            SalesList.Items.Refresh();

            try
            {
                await _client.SendSale(sale);
            }
            catch (Exception ex)
            {
                Sales.Remove(sale);
                beer.InStore += sale.Amount;
                SalesList.Items.Refresh();
            }

            BeersList.Items.Refresh();
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (BeerOrderList.SelectedItem == null) return;

            var beer = BeerOrderList.SelectedItem as Beer;

            if (!int.TryParse(OrderAmountBox.Text, out int amount)) return;

            if (Orders.ContainsKey(beer))
            {
                Orders[beer] += amount;
            } else
            {
                Orders.Add(beer, amount);
            }

            OrderPartList.Items.Refresh();
        }

        private async void CommitOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            CommitOrdersButton.IsEnabled = false;
            var orders = new Dictionary<int, int>();
            foreach(var kvp in Orders)
            {
                orders.Add((int)kvp.Key.Id, kvp.Value);
            }

            try
            {
                var result = await _client.SendOrders(orders);

                foreach (var kvp in result)
                {
                    if (kvp.Value > 0)
                    {
                        var local = Beers.Find(b => b.Id == kvp.Key);
                        local.InStore += (int)kvp.Value;
                        local.Amount -= (int)kvp.Value;
                    }
                }

                BeersList.Items.Refresh();
                BeerOrderList.Items.Refresh();
            } catch
            {

            }

            Orders.Clear();

            CommitOrdersButton.IsEnabled = true;
        }

        private async void BeerStatsList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var beer = BeerStatsList.SelectedItem as Beer;
            try
            {
                var info = SalesInfos.First(i => i.BeerName == beer.Type);

                StatsBeerName.Text = info.BeerName;
                StatsAvSale.Text = info.AvSale.ToString();
                StatsInStock.Text = info.InStock.ToString();
                StatsSoldMonth.Text = info.SoldMonth.ToString();
                StatsSoldTotal.Text = info.SoldTotal.ToString();
                StatsSoldWeek.Text = info.SoldWeek.ToString();
                StatsSoldYesterday.Text = info.SoldYesterday.ToString();
            } catch { }


        }

        private async void TabItem_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            await GetData();

            SalesList.ItemsSource = Sales;
            SalesList.Items.Refresh();

            try
            {
                SalesInfos = (await SalesInfo.CreateSalesInfo(Beers, Sales)).ToList();
            } catch { }
        }

        private async void SendStats_Click(object sender, RoutedEventArgs e)
        {
            SendStats.IsEnabled = false;
            try
            {
                await _client.SendSalesInfo(SalesInfos);
            }
            catch { }

            SendStats.IsEnabled = true;

        }
    }
}