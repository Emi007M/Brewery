using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BreweryService.Models;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json;
using BreweryWebApp.ViewComponents;
using System.Reflection;
using System.Text;

namespace BreweryWebApp.Controllers
{
    public class HomeController : Controller
    {
        public object JsonRequestBehavior { get; private set; }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Money()
        {
            //ViewData["Message"] = "Your application description page.";
            //ViewData["Beers"] = "xyz";
            return View();
        }

        public IActionResult Clients()
        {
            //ViewData["Message"] = "Your contact page.";
            return View();
        }

        public IActionResult Orders()
        {
            //ViewData["Message"] = "Your sth page.";
             return View();

        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult InvokeProduction()
        {

            return ViewComponent("BeerList", new { i = 1 });

        }
        public IActionResult AddNewBeer()
        {

            return ViewComponent("BeerList", new { i = 2 });

        }

        public IActionResult GetOverallBalance()
        {

            return ViewComponent("BeerOverall", new { i = 1 });

        }

        [HttpPost]
        public async Task<IActionResult> UpdateBeer(string name, string value, string pk)
        {

            long beerId = int.Parse(pk);
            //get beer of id
            //Beer b = 
            Beer b;
            var apiRequestUri = new Uri("http://localhost:65320/api/beer/"+pk);

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(apiRequestUri);
                var responseString = await response.Content.ReadAsStringAsync();
                b = JsonConvert.DeserializeObject<Beer>(responseString);
             
            }


            // Change the field value using reflection
            Type myType = typeof(Beer);
            FieldInfo fields = myType.GetField(name);           
            fields.SetValue(b, value);

            // Serialize our concrete class into a JSON String
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(b));
            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            apiRequestUri = new Uri("http://localhost:65320/api/beer/"+pk);

            using (var client = new HttpClient())
            {
                var response = await client.PutAsync(apiRequestUri, httpContent);
                var responseString = await response.Content.ReadAsStringAsync();
                dynamic resp = JsonConvert.DeserializeObject<Beer>(responseString);
                return Json(new { });
            }
            
        }

    }
}
