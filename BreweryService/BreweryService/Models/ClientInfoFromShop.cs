using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreweryService.Models
{
    public class ClientInfoFromShop
    {
        public ClientInfoFromShop()
        {
            BeerName        = "beer";
            InStock         = 0;
            AvSale          = 0;
            SoldYesterday   = 0;
            SoldWeek        = 0;
            SoldMonth       = 0;
            SoldTotal       = 0;
        }

        public ClientInfoFromShop(string _beerName, int _inStock, float _avSale, int _soldYesterday, int _soldWeek, int _soldMonth, int _soldTotal)
        {
            BeerName        = _beerName;
            InStock         = _inStock;
            AvSale          = _avSale;
            SoldYesterday   = _soldYesterday;
            SoldWeek        = _soldWeek;
            SoldMonth       = _soldMonth;
            SoldTotal       = _soldTotal;
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }


        public string BeerName { get; set; }
        public int InStock { get; set; }
        /// <summary>
        /// average daily sold bottles
        /// </summary>
        public float AvSale { get; set; }

        public int SoldYesterday { get; set; }
        public int SoldWeek { get; set; }
        public int SoldMonth { get; set; }
        public int SoldTotal { get; set; }

        public static implicit operator List<object>(ClientInfoFromShop v)
        {
            throw new NotImplementedException();
        }
    }
}