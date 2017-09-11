using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreweryService
{
    public static class TestCfg
    {
        public const int Beers = 5000;
        public const int Clients = 200;
        public const int Orders = Beers * Clients;
        public const int MinOrder = 100;
        public const int MaxOrder = 500;
    }
}
