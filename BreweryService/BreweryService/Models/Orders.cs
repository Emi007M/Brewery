using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreweryService.Models
{
    public class Orders
    {
        public BeerContext beerContext { get; set; }

        public Orders(BeerContext bc)
        {
            beerContext = bc;
            OrdersList = new List<Order>();
        }

        public List<Order> OrdersList { get; set; }

        public void AddOrder(long beerId, long clientId, int amount)
        {
            float price = beerContext.Beers.Find(beerId).Price;
            int discount = beerContext.Clients.ClientsList.Find(c=>c.Id==clientId).Discounts.GetDiscount(amount);

            OrdersList.Add(new Order(beerId, clientId, amount, price, discount));
        }
    }
}
