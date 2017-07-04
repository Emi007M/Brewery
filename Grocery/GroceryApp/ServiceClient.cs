using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using System.Configuration;
using GroceryModels.Models;

namespace GroceryApp
{
    class ServiceClient
    {
        private readonly string _apiBase;

        public ServiceClient()
        {
            _apiBase = ConfigurationManager.AppSettings["apiUrl"];

        }

        public async Task<IEnumerable<Beer>> GetBeers()
        {
            return await _apiBase.AppendPathSegment("beers").GetJsonAsync<IEnumerable<Beer>>();
        }

        public async Task<Beer> GetBeer(int id)
        {
            return await _apiBase.AppendPathSegment($"beers/{id}").GetJsonAsync<Beer>();
        }

        public async Task RefreshBeers(int id)
        {
            await _apiBase.AppendPathSegment("beers/refresh").PostJsonAsync(null);
        }

        public async Task<int> GetDiscounts(int count)
        {
            return await _apiBase.AppendPathSegment($"orders/discount/{count}").GetJsonAsync<int>();
        }

        public async Task<Dictionary<int, long>> SendOrders(Dictionary<int, int> orders)
        {
            return await _apiBase.AppendPathSegment("orders").PostJsonAsync(orders).ReceiveJson<Dictionary<int, long>>();
        }

        public async Task<IEnumerable<Sale>> GetSales()
        {
            return await _apiBase.AppendPathSegment("sales").GetJsonAsync<IEnumerable<Sale>>();
        }

        public async Task<dynamic> SendSale(Sale sale)
        {
            return await _apiBase.AppendPathSegment("sales").PostJsonAsync(sale).ReceiveJson();
        }

        public async Task<dynamic> SendSalesInfo(IEnumerable<SalesInfo> salesinfo)
        {
            return await _apiBase.AppendPathSegment("sales/info").PostJsonAsync(salesinfo).ReceiveJson();
        }
    }
}
