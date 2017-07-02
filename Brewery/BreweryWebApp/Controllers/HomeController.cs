using BreweryService.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
            var apiRequestUri = new Uri("http://localhost:65320/api/beer/" + pk);

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(apiRequestUri);
                var responseString = await response.Content.ReadAsStringAsync();
                b = JsonConvert.DeserializeObject<Beer>(responseString);
            }

            switch (name)
            {
                case "Cost":
                    {
                        b.Cost = Single.Parse(value);
                    }
                    break;

                case "Price":
                    {
                        b.Price = Single.Parse(value);
                    }
                    break;

                case "ProductionDaily":
                    {
                        b.ProductionDaily = int.Parse(value);
                    }
                    break;

                case "Type":
                    {
                        b.Type = value;
                    }
                    break;
            }

            // Serialize our concrete class into a JSON String
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(b));
            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            apiRequestUri = new Uri("http://localhost:65320/api/beer/" + pk);

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