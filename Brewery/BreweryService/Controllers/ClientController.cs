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

        public ClientController(IClientRepository clientRepository, IOrderRepository orderRepository)
        {
            _clientRepository = clientRepository;
            _orderRepository = orderRepository;
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
            IEnumerable<Order> orders = new List<Order>(_orderRepository.GetAll().Where(o => o.ClientId == id));
            return new ClientOrders(orders);


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

        //GET /api/client/orders/{id}
        [HttpGet("orders/{id}", Name = "GetOrder")]
        public Order GetOrder(long id)
        {
            return _orderRepository.Find(id);
        }

        //POST /api/client/discounts
        [HttpPost("discounts", Name = "GetDiscounts")]
        public IActionResult GetDiscounts([FromBody] AuthorizedObject<dynamic> auth)
        {
            var client = _clientRepository.FindByName(auth.ClientName);
            //validate password
            if (client != null && _clientRepository.IsKeyClientValid(client.Id, auth.ClientKey))
            {
                return Json(client.Discounts);
            }

            return null;
        }

        //POST /api/client/info
        [HttpPost("info")]
        public IActionResult UpdateInfo(long id, [FromBody] AuthorizedObject<List<ClientInfoFromShop>> item)
        {

            if (item == null)
            {
                return Json(new { success = 0 });
            }

            _clientRepository.Find(id).Info = item.Object;

            return Json(new { success = 1 });
        }

        //POST /api/client/orders
        [HttpPost("orders")]
        public IActionResult PlaceOrders([FromBody] AuthorizedObject<Dictionary<int, int>> item)
        {
            var client = _clientRepository.FindByName(item.ClientName);
            //validate password
            if (client != null && _clientRepository.IsKeyClientValid(client.Id, item.ClientKey))
            {
                var results = new Dictionary<int, long>();
                foreach (var o in item.Object)
                {
                    long order_placed = _orderRepository.AddOrder(o.Key, client.Id, o.Value);
                    results.Add(o.Key, order_placed);
                }

                return Json(results);
            }
            return BadRequest();
        }
    }
}
