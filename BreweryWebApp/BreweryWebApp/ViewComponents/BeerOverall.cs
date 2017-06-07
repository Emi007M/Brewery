using BreweryService.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static BreweryService.Models.BeerRepository;

namespace BreweryWebApp.ViewComponents
{
    public class BeerOverall : ViewComponent
    {

        
        public BeerOverall()
        {
            
        }

        public async Task<IViewComponentResult> InvokeAsync(int i)
        {
            //  if (i == 1) await InvokeProduction();
            var items = await GetOverallMoneyAsync();
            //ViewData["balancex"] = await GetBalanceAsync();
            ViewData["balancex"] = (float)items.Balance; //nie działa, zwraca zero

            return View("Default");
        }
       
     

        public static async Task<float> GetBalanceAsync()
        {
            //var items = await GetItemsAsync();
            OverallMoney m = await GetOverallMoneyAsync();
            return m.Balance;
            // var apiRequestUri = new Uri("http://localhost:65320/api/beer/overall");
            //using (var client = new HttpClient())
            //{
            //    var stringResponse = await client.GetStringAsync(apiRequestUri);
            //    dynamic beers = JsonConvert.DeserializeObject<OverallMoney>(stringResponse);
            //    return beers;
            //}
        }

        public static async Task<OverallMoney> GetOverallMoneyAsync()
        {

            var apiRequestUri = new Uri("http://localhost:65320/api/beer/overall");
            using (var client = new HttpClient())
            {
                var stringResponse = await client.GetStringAsync(apiRequestUri);
                OverallMoney beers = JsonConvert.DeserializeObject<OverallMoney>(stringResponse);
                return beers;
            }

        }

        //public static async Task<OverallMoney> InvokeProduction()
        //{

        //    var apiRequestUri = new Uri("http://localhost:65320/api/beer/produce");
        //    using (var client = new HttpClient())
        //    {
        //        var stringResponse = await client.GetStringAsync(apiRequestUri);
        //        dynamic beers = JsonConvert.DeserializeObject<OverallMoney>(stringResponse);
        //        return beers;
        //    }

        //}
    }
}
