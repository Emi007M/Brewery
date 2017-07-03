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
    public class SalesController : Controller
    {
        private readonly GroceryContext _context;

        public SalesController(GroceryContext context)
        {
            _context = context;
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
            if(sale.Beer.InStore < sale.Amount)
            {
                return BadRequest();
            }

            sale.Beer.InStore -= sale.Amount;
            await _context.Sales.AddAsync(sale);
            return Ok();
        }
    }
}
