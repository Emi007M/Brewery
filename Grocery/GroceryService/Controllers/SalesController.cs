using GroceryService.DAL;
using GroceryModels.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryService.Controllers
{
    [Route("api/[controller]")]
    public class SalesController : Controller
    {
        private readonly GroceryContext _context;
        private readonly BreweryClient _client;

        public SalesController(GroceryContext context, BreweryClient client)
        {
            _context = context;
            _client = client;
        }

        [Route("")]
        [HttpGet]
        public async Task<IEnumerable<Sale>> GetAll()
        {
            return _context.Sales;
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Sale sale)
        {
            sale.Beer = await _context.Beers.FindAsync(sale.Beer.Id);
            sale.BeerId = sale.Beer.Id;
            if(sale.Beer.InStore < sale.Amount)
            {
                return BadRequest();
            }

            sale.Beer.InStore -= sale.Amount;
            await _context.Sales.AddAsync(sale);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [Route("info")]
        [HttpPost]
        public async Task<dynamic> PostInfo([FromBody] IEnumerable<SalesInfo> salesinfo)
        {
            return await _client.SendStats(salesinfo);
        }
    }
}
