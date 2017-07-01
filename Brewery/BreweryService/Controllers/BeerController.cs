using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BreweryService.Models;
using System.Reflection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BreweryService.Controllers
{
    [Route("api/[controller]")]
    public class BeerController : Controller
    {

        private readonly IBeerRepository _beerRepository;

        public BeerController(IBeerRepository beerRepository)
        {
            _beerRepository = beerRepository;
        }

        //GET /api/beer
        [HttpGet]
        public IEnumerable<Beer> GetAll()
        {
            return _beerRepository.GetAll();
        }

        //GET /api/beer/{id}
        [HttpGet("{id}", Name = "GetBeer")]
        public IActionResult GetById(long id)
        {
            var item = _beerRepository.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        //POST /api/beer
        [HttpPost]
        public IActionResult Create([FromBody] Beer item)
        {
            if (item == null)
            {
                item = new Beer();
                //return BadRequest();
            }

            _beerRepository.Add(item);

            return CreatedAtRoute("GetBeer", new { id = item.Id }, item);
        }

        //PUT /api/beer/{id}, the whole obj, not only changes
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Beer item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var beer = _beerRepository.Find(id);
            if (beer == null)
            {
                return NotFound();
            }

            //TODO update only mentioned things
            //beer.IsComplete = item.IsComplete;
            //beer.Name = item.Name;
            foreach (var prop in item.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                //if(prop.GetValue(item, null) != null)
                prop.SetValue(beer,  prop.GetValue(item, null) );
            }

            _beerRepository.Update(beer);
            return new NoContentResult();
        }

        //DELETE /api/beer/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _beerRepository.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _beerRepository.Remove(id);
            return new NoContentResult();
        }


        //overall income/outcome statistics

        //GET /api/beer/overall/
        [HttpGet("overall")]
        public OverallMoney GetOverallMoney()
        {
            return _beerRepository.GetOverallMoney();

        }

        //GET /api/beer/produce/
        [HttpGet("produce")]
        public OverallMoney ProduceAll()
        {
            _beerRepository.ProduceAll();
            return _beerRepository.GetOverallMoney();

        }
    }
}
