using GroceryService.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using GroceryService.Models;

namespace GroceryService.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly GroceryContext _context;
        private readonly BreweryClient _client;

        public OrdersController(GroceryContext context, BreweryClient client)
        {
            _context = context;
            _client = client;
        }

        [Route("")]
        [HttpPost]
        public async Task<Dictionary<int, long>> Post([FromBody] Dictionary<int, int> orders)
        {
            var response = await _client.SendOrders(orders);

            foreach (var item in response)
            {
                if (item.Value > 0)
                {
                    var beer = _context.Beers.Find(item.Key);
                    beer.InStore += item.Value;
                }
            }

            return response;
        }

        [Route("discount/{count}")]
        [HttpGet]
        public async Task<int> GetDiscounts(int count)
        {
            var discounts = await _context.Discounts.ToAsyncEnumerable().OrderByDescending(d => d.Timestamp).FirstOrDefault();

            if(discounts == null || discounts.Timestamp < DateTime.Now.AddDays(-1))
            {
                discounts = await _client.GetDiscounts();
                discounts.Timestamp = DateTime.Now;

                await _context.Discounts.AddAsync(discounts);
            }

            return discounts.GetDiscount(count);
        }
    }
}
