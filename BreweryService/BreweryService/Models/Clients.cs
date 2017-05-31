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
                c.Discounts.Per100 = GlobalDiscounts.Per100;
                c.Discounts.Per250 = GlobalDiscounts.Per250;
                c.Discounts.Per500 = GlobalDiscounts.Per500;
                c.Discounts.Per1000= GlobalDiscounts.Per1000;
            }
        }

        public List<Client> ClientsList { get; set; }

        public Discounts GlobalDiscounts { get; set; } 
        //TODO when setting then recalculate for all clients (client can't have smaller discounts than global
    }
}
