﻿using BreweryService.Models;
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
    public class ClientList : ViewComponent
    {

        
        public ClientList()
        {
            
        }

        public async Task<IViewComponentResult> InvokeAsync(int i)
        {
          //  if (i == 1) await InvokeProduction();
            var items = await GetItemsAsync();
            return View("Default", items);
        }
       
        public static async Task<List<Beer>> GetItemsAsync()
        {

            var apiRequestUri = new Uri("http://localhost:65320/api/beer");
            using (var client = new HttpClient())
            {
                var stringResponse = await client.GetStringAsync(apiRequestUri);
                dynamic beers = JsonConvert.DeserializeObject<List<Beer>>(stringResponse);
                return beers;
            }
        
        }

        //public static async Task<float> GetBalanceAsync()
        //{
        //    //var items = await GetItemsAsync();
        //    OverallMoney m = await GetOverallMoneyAsync();
        //    return m.Balance;
        //}

        //public static async Task<OverallMoney> GetOverallMoneyAsync()
        //{

        //    var apiRequestUri = new Uri("http://localhost:65320/api/beer/overall");
        //    using (var client = new HttpClient())
        //    {
        //        var stringResponse = await client.GetStringAsync(apiRequestUri);
        //        dynamic beers = JsonConvert.DeserializeObject<OverallMoney>(stringResponse);
        //        return beers;
        //    }

        //}

        //public static async task<overallmoney> invokeproduction()
        //{

        //    var apirequesturi = new uri("http://localhost:65320/api/beer/produce");
        //    using (var client = new httpclient())
        //    {
        //        var stringresponse = await client.getstringasync(apirequesturi);
        //        dynamic beers = jsonconvert.deserializeobject<overallmoney>(stringresponse);
        //        return beers;
        //    }

        //}
    }
}
