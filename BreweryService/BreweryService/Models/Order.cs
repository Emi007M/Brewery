using System;

namespace BreweryService.Models
{
    public class Order
    {
        public DateTime Date { get; set; }
        public long BeerId { get; set; }
        public long ClientId { get; set; }
        public int Amount { get; set; }
        /// <summary>
        /// Price per single bottle
        /// </summary>
        public float Price { get; set; }
        /// <summary>
        /// Given in %
        /// </summary>
        public int Discount { get; set; }
        /// <summary>
        /// price*amount*(100-discount)/100f
        /// </summary>
        public float TotalPrice { get; set; }

    }
}