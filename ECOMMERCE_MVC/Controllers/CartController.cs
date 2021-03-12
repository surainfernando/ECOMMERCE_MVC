using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using ECOMMERCE_MVC.Models;

namespace ECOMMERCE_MVC.Controllers
{
    public class CartController : Controller
    {
        public readonly SqlConnection _connection;
        public CartController( SqlConnection con)
        {
           
            _connection = con;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddToCart(int id)
        {

            try
            {
                var customer = JsonConvert.DeserializeObject<Models.Customer>(HttpContext.Session.GetString("CustomerSession"));
                // Debug.WriteLine("http:// Edit  called");
                CartItem.AddCartItem(customer.Id, id, _connection);
                Item.AddToCart(id, _connection);
                return RedirectToAction("Index","Home");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Customer1");
            }
        }

    }
}
