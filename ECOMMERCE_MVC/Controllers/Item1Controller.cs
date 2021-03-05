using ECOMMERCE_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECOMMERCE_MVC.Controllers
{
    public class Item1Controller : Controller
    {
        public readonly SqlConnection _connection;
        public Item1Controller(SqlConnection con)
        {
            _connection = con;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddItem()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddItem(Item a)
        {
            InsertItemDB(a);
            return RedirectToAction("Index");
        }


        public int InsertItemDB(Item a)//
        {
            int rows=Item.InsertItemDB(a,_connection);//call insert item method in Item Model
            return rows;
           



        }
    }
}
