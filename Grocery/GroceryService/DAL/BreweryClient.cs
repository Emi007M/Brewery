using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using GroceryService.Models;

namespace GroceryService.DAL
{
    public class BreweryClient
    {
        private readonly RestConfig _config;

        public BreweryClient(IOptions<RestConfig> options)
        {
            _config = options.Value;
        }

        public async Task<Dictionary<int, long>> SendOrders(Dictionary<int, int> orders)
        {
            return await _config.URI.AppendPathSegment("client/orders").PostJsonAsync(new AuthorizedObject<Dictionary<int, int>>
            {
                ClientKey = _config.ClientKey,
                ClientName = _config.ClientName,
                Object = orders
            }).ReceiveJson<Dictionary<int, long>>();
        }

        public async Task<dynamic> SendStats(IEnumerable<SalesInfo> salesinfo)
        {
            return await _config.URI.AppendPathSegment("client/orders").PostJsonAsync(new AuthorizedObject<IEnumerable<SalesInfo>>
            {
                ClientKey = _config.ClientKey,
                ClientName = _config.ClientName,
                Object = salesinfo
            }).ReceiveJson();
        }

        public async Task<Discounts> GetDiscounts()
        {
            return await _config.URI.AppendPathSegment("client/discounts").PostJsonAsync(new AuthorizedObject<dynamic>
            {
                ClientKey = _config.ClientKey,
                ClientName = _config.ClientName,
                Object = null
            }).ReceiveJson<Discounts>();
        }

        public async Task<IEnumerable<Beer>> GetBeers()
        {
            return await _config.URI.AppendPathSegment("beer").GetJsonAsync<IEnumerable<Beer>>();
        }
    }
}
