using GroceryService.DAL;
using GroceryService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryService.Controllers
{
    [Route("api/[controller]")]
    public class BeersController : Controller
    {
        private readonly GroceryContext _context;
        private readonly BreweryClient _client;

        public BeersController(GroceryContext context, BreweryClient client)
        {
            _context = context;
            _client = client;
        }

        [Route("")]
        [HttpGet]
        public async Task<IEnumerable<Beer>> GetAll()
        {
            return _context.Beers;
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<Beer> Get(int id)
        {
            return await _context.Beers.FindAsync(id);
        }

        [Route("refresh")]
        [HttpGet]
        public async Task Refresh()
        {
            var beers = await _client.GetBeers();

            foreach (var beer in beers)
            {
                var local = await _context.Beers.FindAsync(beer.Id);
                if(local == null)
                {
                    await _context.Beers.AddAsync(beer);
                } else
                {
                    local.Amount = beer.Amount;
                    local.Price = beer.Price;
                    _context.Beers.Update(local);
                }
            }
        }
    }
}
