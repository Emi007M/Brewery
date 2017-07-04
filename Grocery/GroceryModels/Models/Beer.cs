using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryModels.Models
{
    public class Beer
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public float Price { get; set; }

        public int Amount { get; set; }

        public int InStore { get; set; }
    }
}
