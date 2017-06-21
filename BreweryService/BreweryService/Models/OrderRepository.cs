using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreweryService.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BeerContext _context;
        public OrderRepository(BeerContext context)
        {
            _context = context;

            //setting same discounts to all
           // Client.GlobalDiscounts = new Discounts();


            if (_context.Orders.Count() == 0) //for now initial db of beers
            {
              

            }

           

        }

        public bool Add(Order item)
        {
            
            //validate, check whether beer id exists and amount is sufficient
            if( _context.Beers.Find(item.BeerId).Buy(item.Amount, item.Discount))
            {
                _context.Orders.Add(item);
                _context.SaveChanges();
                return true;
            }
            return false;
            
        }

        public long AddOrder(long beerId, long clientId, int amount)
        {
            
            float price = _context.Beers.Find(beerId).Price;
            int discount = _context.Clients.Find(clientId).Discounts.GetDiscount(amount);

            Order o = new Order(beerId, clientId, amount, price, discount);
            //if success, return order id
            if (Add(o))
                return o.Id;
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
