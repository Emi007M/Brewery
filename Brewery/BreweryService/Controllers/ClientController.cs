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



        //POST /api/client/{id}/info
        [HttpPost("{id}/info")]
        public IActionResult UpdateInfo(long id, [FromBody] List<ClientInfoFromShop> item)
        {

            if (item == null)
            {
                return Json(new { success = 0 });
            }

            _clientRepository.Find(id).Info = item;

            return Json(new { success = 1 });
        }

        //POST /api/client/{id}/order
        // dictionary: { "beerId": id, "amount": amount, "key": password }
        [HttpPost("{id}/order")]
        public IActionResult UpdateInfo(long id, [FromBody] Dictionary<string,string> item)
        {
            //validate password
            if (_clientRepository.IsKeyClientValid(id, (string)item["key"]))
            {
                //check if beer id exists and amount of bottles is sufficient
                long order_placed = 
                    _orderRepository.AddOrder(int.Parse(item["beerId"]), id, int.Parse(item["amount"]));
                if (!order_placed.Equals(-1))
                    //return CreatedAtRoute("GetOrder", id = order_placed);
                    return Json( GetOrder(order_placed) );
                else
                    return Json( new{ success = 0} );
            }

            else
                return null;
        }
    }
}
