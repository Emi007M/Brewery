using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreweryService.Models
{
    public class Clients
    {
        public List<Client> ClientsList { get; set; }

        public Discounts GlobalDiscounts { get; set; } 
        //TODO when setting then recalculate for all clients (client can't have smaller discounts than global
    }
}
