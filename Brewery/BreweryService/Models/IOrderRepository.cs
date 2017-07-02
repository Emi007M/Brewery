using System.Collections.Generic;

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