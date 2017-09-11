using System;
using System.Collections.Generic;
using System.Linq;

namespace BreweryService.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BeerContext _context;

        public OrderRepository(BeerContext context)
        {
            _context = context;
            var rand = new Random();

            //setting same discounts to all
            // Client.GlobalDiscounts = new Discounts();

            if (_context.Orders.Count() == 0) //for now initial db of beers
            {
                Console.WriteLine("Orders");
                for (int i = 0; i < TestCfg.Orders; i++)
                {
                    var beerId = rand.Next(0, TestCfg.Beers - 1);
                    var clientId = rand.Next(0, TestCfg.Clients - 1);
                    var amount = rand.Next(TestCfg.MinOrder, TestCfg.MaxOrder);

                    Beer b = _context.Beers.Find(beerId);
                    if (b == null) continue;

                    float price = b.Price;
                    int discount = _context.Clients.Find(clientId).Discounts.GetDiscount(amount);

                    Order o = new Order(beerId, clientId, amount, price, discount);
                    _context.Orders.Add(o);

                    Console.WriteLine($"{i}/{TestCfg.Orders}");
                }
                _context.SaveChanges();
            }
        }

        public bool Add(Order item)
        {
            Beer b = _context.Beers.Find(item.BeerId);
            //validate, check whether beer id exists and amount is sufficient
            if (b != null && b.Buy(item.Amount, item.Discount))
            {
                _context.Orders.Add(item);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public long AddOrder(long beerId, long clientId, int amount)
        {
            Beer b = _context.Beers.Find(beerId);
            if (b == null) return -1;

            float price = b.Price;
            int discount = _context.Clients.Find(clientId).Discounts.GetDiscount(amount);

            Order o = new Order(beerId, clientId, amount, price, discount);
            //if success, return order id
            if (Add(o))
                return o.Amount;
            return -1;
        }

        public Order Find(long key)
        {
            return _context.Orders.FirstOrDefault(t => t.Id == key);
        }

        public IEnumerable<Order> GetAll()
        {
            return _context.Orders.ToList();
        }

        public void Remove(long key)
        {
            var entity = _context.Orders.First(t => t.Id == key);
            _context.Orders.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Order item)
        {
            _context.Orders.Update(item);
            _context.SaveChanges();
        }

        //public OverallMoney GetOverallMoney()
        //{
        //    return new OverallMoney(_context);

        //}

        //public void ProduceAll()
        //{
        //    _context.ProduceAll();
        //    _context.SaveChanges();
        //}
    }
}