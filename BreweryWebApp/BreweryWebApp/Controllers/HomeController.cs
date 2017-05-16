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
    }
}
