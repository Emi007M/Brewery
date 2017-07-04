using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryModels.Models
{
    public class Sale
    {
        public long Id { get; set; }
        public int BeerId { get; set; }
        public Beer Beer { get; set; }
        public int Amount { get; set;}
        public DateTime Timestamp { get; set; }
    }
}
