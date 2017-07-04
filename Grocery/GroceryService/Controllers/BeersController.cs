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
    public class BeersController : Controller
    {
        private readonly GroceryContext _context;
        private readonly BreweryClient _client;
        private DateTime _beersUpdated;

        public BeersController(GroceryContext context, BreweryClient client)
        {
            _context = context;
            _client = client;
        }

        [Route("")]
        [HttpGet]
        public async Task<IEnumerable<Beer>> GetAll()
        {
            if(_context.Beers.Count() == 0 || _beersUpdated < DateTime.Now.AddDays(-1))
            {
                await Refresh();
            }

            return _context.Beers;
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<Beer> Get(int id)
        {
            return await _context.Beers.FindAsync(id);
        }

        [Route("refresh")]
        [HttpPost]
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

            _beersUpdated = DateTime.Now;
            await _context.SaveChangesAsync();
        }
    }
}
