using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreweryService.Models
{
    public class Clients
    {
        public Clients()
        {
            //generating temporary db of clients
            ClientsList = new List<Client>();

            ClientsList.Add(new Client());
            ClientsList.Add(new Client());
            ClientsList.Add(new Client());

            //setting same discounts to all
            GlobalDiscounts = new Discounts();
            foreach(Client c in ClientsList)
            {
                c.Discounts.per100 = GlobalDiscounts.per100;
                c.Discounts.per250 = GlobalDiscounts.per250;
                c.Discounts.per500 = GlobalDiscounts.per500;
                c.Discounts.per1000= GlobalDiscounts.per1000;
            }
        }

        public List<Client> ClientsList { get; set; }

        public Discounts GlobalDiscounts { get; set; } 
        //TODO when setting then recalculate for all clients (client can't have smaller discounts than global
    }
}
