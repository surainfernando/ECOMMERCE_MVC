using ECOMMERCE_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace ECOMMERCE_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public readonly SqlConnection _connection;

        public HomeController(ILogger<HomeController> logger, SqlConnection con)
        {
            _logger = logger;
            _connection = con;
        }


        public IActionResult Index()
        {
            List<Item> objList = Item.GetSellersItems(-99, _connection); ;

            return View(objList);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult ItemView(int id)
        {
            Item item = Item.GetOneSellersItem(id, _connection);
            ViewBag.ItemName = item.Name;
            return View(item);
            
          
        }

        public IActionResult AddToCart(int id)
        {

            try
            {
                var customer = JsonConvert.DeserializeObject<Models.Customer>(HttpContext.Session.GetString("CustomerSession"));
                // Debug.WriteLine("http:// Edit  called");
                CartItem.AddCartItem(customer.Id,id, _connection);
                Item.AddToCart(id, _connection);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Customer1");
            }
        }

        public IActionResult TempSession()
        {
            return View();

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
