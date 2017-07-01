using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreweryService.Models
{
    public class Order
    {
        public Order() { }
        public Order(long beerId, long clientId, int amount, float price, int discount)
        {
            Date = System.DateTime.Now;
            BeerId = beerId;
            ClientId = clientId;
            Amount = amount;
            Price = price;
            Discount = discount;
            TotalPrice = Amount * Price * (float)(100 - Discount)/100;

        }
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

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

    }
}