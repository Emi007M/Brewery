using System.Collections.Generic;

namespace BreweryService.Models
{
    public interface IBeerRepository
    {
        void Add(Beer item);

        IEnumerable<Beer> GetAll();

        Beer Find(long key);

        void Remove(long key);

        void Update(Beer item);

        OverallMoney GetOverallMoney();

        void ProduceAll();
    }
}