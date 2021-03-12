﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECOMMERCE_MVC.Models;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using ECOMMERCE_MVC.Models;
using System.Diagnostics;

namespace ECOMMERCE_MVC.Controllers
{
    public class Customer1Controller : Controller
    {
        public readonly SqlConnection _connection;

        public Customer1Controller(SqlConnection con)
        {
            _connection = con;
        }
        public IActionResult Index() ///login page
        {

            try {
                var customer = JsonConvert.DeserializeObject<Models.Customer>(HttpContext.Session.GetString("CustomerSession"));
                return RedirectToAction("Profile", "Customer1");
            }
            catch (Exception e)
            {
                return View();
            }
          
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Item1");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(ECOMMERCE_MVC.Models.Customer a)
        {
            int s= ECOMMERCE_MVC.Models.Customer.LoginDB(a, _connection);

           
               
           
             if (s == 0)
            {
                ModelState.AddModelError("Email", "The Email is incorrect");
                return View();
            }
            else if (s == 2)
            {
                ModelState.AddModelError("Password", "The  Password is incorrect");
                return View();

            }
            else {
                TempData["id"] = s;
                Models.Customer customer = Models.Customer.getCustomerDetails(s, _connection);
                HttpContext.Session.SetString("CustomerSession", JsonConvert.SerializeObject(customer));



                return RedirectToAction("Profile", "Customer1");
            }

        }



        public IActionResult Register()
        {
            try
            {
                var customer = JsonConvert.DeserializeObject<Models.Customer>(HttpContext.Session.GetString("CustomerSession"));
                return RedirectToAction("Profile", "Customer1");
            }
            catch (Exception e)
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Models.Customer a)
        {
            int d = ECOMMERCE_MVC.Models.Customer.InsertCustomer(a,_connection);
            Debug.Write("Integer Message Is"+d);
            if (d == 1)
            {
                Models.Customer customer = Models.Customer.getCustomerDetailsByEmail(a.Email, _connection);
                HttpContext.Session.SetString("CustomerSession", JsonConvert.SerializeObject(customer));
                return RedirectToAction("Index", "Item1", new { id = 47 });
            }
            else {
                return RedirectToAction("Index", "Home");
            }

            
        }
        public IActionResult Profile() {
            try { var customer = JsonConvert.DeserializeObject<Models.Customer>(HttpContext.Session.GetString("CustomerSession"));
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Item1");
                
            }
            
        
        
        }

       









    }
}
