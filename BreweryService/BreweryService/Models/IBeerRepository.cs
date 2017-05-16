using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BreweryService.Models.BeerRepository;

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
