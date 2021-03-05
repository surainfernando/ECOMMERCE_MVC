using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECOMMERCE_MVC.Models;
using Microsoft.Data.SqlClient;

namespace ECOMMERCE_MVC.Controllers
{
    public class Customer1Controller : Controller
    {
        public readonly SqlConnection _connection;

        public Customer1Controller(SqlConnection con)
        {
            _connection = con;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(ECOMMERCE_MVC.Models.Customer a)
        {
            int s= ECOMMERCE_MVC.Models.Customer.LoginDB(a, _connection);
            
            if (s == 1) { return RedirectToAction("Index","Item1"); }
            else if (s == 0)
            {
                ModelState.AddModelError("Email", "The Email is incorrect");
                return View();
            }
            else
            {
                ModelState.AddModelError("Password", "The  Password is incorrect");
                return View();

            }

        }



        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Models.Customer a)
        {
            int d = ECOMMERCE_MVC.Models.Customer.InsertCustomer(a,_connection);
            return RedirectToAction("Index","Item1");
        }




    }
}
