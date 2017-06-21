using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BreweryService.Models.BeerRepository;

namespace BreweryService.Models
{
    public interface IOrderRepository
    {
        bool Add(Order item);
        IEnumerable<Order> GetAll();
        Order Find(long key);
        void Remove(long key);
        void Update(Order item);

        long AddOrder(long beerId, long clientId, int amount);

     

    }
}
