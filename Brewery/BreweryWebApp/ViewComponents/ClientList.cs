using BreweryService.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static BreweryService.Models.BeerRepository;

namespace BreweryWebApp.ViewComponents
{
    public class ClientList : ViewComponent
    {

        
        public ClientList()
        {
            
        }

        public async Task<IViewComponentResult> InvokeAsync(int i)
        {
          //  if (i == 1) await InvokeProduction();
            var client = await GetClientAsync(i);

       
            ViewData["orders"] = await GetOrdersAsync(i);

            return View("Default", client);
        }

        public static async Task<Client> GetClientAsync(int i)
        {

            var apiRequestUri = new Uri("http://localhost:65320/api/client/" + i);
            using (var client = new HttpClient())
            {
                var stringResponse = await client.GetStringAsync(apiRequestUri);
                dynamic beers = JsonConvert.DeserializeObject<Client>(stringResponse);
                return beers;
            }

        }

        public static async Task<List<Client>> GetClientsAsync()
        {

            var apiRequestUri = new Uri("http://localhost:65320/api/client/");
            using (var client = new HttpClient())
            {
                var stringResponse = await client.GetStringAsync(apiRequestUri);
                dynamic beers = JsonConvert.DeserializeObject<List<Client>>(stringResponse);
                return beers;
            }

        }

        public static async Task<ClientOrders> GetOrdersAsync(int i)
        {

            var apiRequestUri = new Uri("http://localhost:65320/api/client/" + i + "/orders");
            using (var client = new HttpClient())
            {
                var stringResponse = await client.GetStringAsync(apiRequestUri);
                dynamic beers = JsonConvert.DeserializeObject<ClientOrders>(stringResponse);
                if (beers.orders == null) beers.orders = new List<Order>();
                return beers;
            }

        }

        //public static async Task<float> GetBalanceAsync()
        //{
        //    //var items = await GetItemsAsync();
        //    OverallMoney m = await GetOverallMoneyAsync();
        //    return m.Balance;
        //}

        //public static async Task<OverallMoney> GetOverallMoneyAsync()
        //{

        //    var apiRequestUri = new Uri("http://localhost:65320/api/beer/overall");
        //    using (var client = new HttpClient())
        //    {
        //        var stringResponse = await client.GetStringAsync(apiRequestUri);
        //        dynamic beers = JsonConvert.DeserializeObject<OverallMoney>(stringResponse);
        //        return beers;
        //    }

        //}

        //public static async task<overallmoney> invokeproduction()
        //{

        //    var apirequesturi = new uri("http://localhost:65320/api/beer/produce");
        //    using (var client = new httpclient())
        //    {
        //        var stringresponse = await client.getstringasync(apirequesturi);
        //        dynamic beers = jsonconvert.deserializeobject<overallmoney>(stringresponse);
        //        return beers;
        //    }

        //}
    }
}
