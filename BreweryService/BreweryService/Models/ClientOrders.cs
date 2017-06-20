using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreweryService.Models
{
    public class ClientOrders
    {
        public IEnumerable<Order> orders;

        public ClientOrders()
        {
            orders = new List<Order>();
        }
        public ClientOrders(IEnumerable<Order> _orders)
        {
            orders = _orders;
        }

        public IEnumerable<int> GetTypes()
        {
            HashSet<int> types = new HashSet<int>();
            foreach(Order o in orders)
            {
                types.Add((int)o.BeerId);
            }

            return types;
        }


        public int getOrdersAmount(int type)
        {
            return orders.Count(o => o.BeerId == type);
        }

        public int getSumOfBottles(int type)
        {
            return orders.Where(o => o.BeerId == type).Sum(o => o.Amount);
        }

        public float getAvOrderSize(int type)
        {
            return getSumOfBottles(type) / getOrdersAmount(type);
        }

        public float getAvIncomeBottle(int type)
        {
            return getIncome(type) / getSumOfBottles(type);
        }

        public float getIncome(int type)
        {
            return orders.Where(o => o.BeerId == type).Sum(o => o.TotalPrice);
        }



    }
}
