using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BreweryService.Models;
using System.Reflection;

namespace BreweryService.Controllers
{
    [Route("api/[controller]")]
    public class ClientController : Controller
    {

        private readonly IClientRepository _clientRepository;
        private readonly IOrderRepository _orderRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        //POST /api/client
        [HttpPost]
        public IActionResult Create([FromBody] Client item)
        {
            if (item == null)
            {
                item = new Client("new client", "admin11");
                //return BadRequest();
            }

            _clientRepository.Add(item);

            return CreatedAtRoute("GetClient", new { id = item.Id }, item);
        }

        // GET api/client
        [HttpGet]
        public IEnumerable<Client> Get()
        {
            
            return _clientRepository.GetAll();
        }


        //GET /api/client/{id}
        [HttpGet("{id}", Name = "GetClient")]
        public IActionResult GetById(long id)
        {
            var item = _clientRepository.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }


        //GET /api/client/{id}/orders
        [HttpGet("{id}/orders", Name = "GetOrders")]
        public ClientOrders GetOrdersByClient(long id)
        {
            return new ClientOrders(_orderRepository.GetAll().Where(o => o.ClientId == id));


        }
        //GET /api/client/orders
        [HttpGet("orders", Name = "GetAllOrders")]
        public IEnumerable<Order> GetOrders()
        {
            IEnumerable<Order> orders = new List<Order>();
            orders = _orderRepository.GetAll().OrderBy(o => o.Date);
            return orders;
           // return _orderRepository.GetAll();


        }



        // POST api/client
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/client/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/client/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
