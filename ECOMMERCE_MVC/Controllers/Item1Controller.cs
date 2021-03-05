﻿using ECOMMERCE_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace ECOMMERCE_MVC.Controllers
{
    public class Item1Controller : Controller
    {
        public readonly SqlConnection _connection;
      
        public string temp { get; set; }
        public Item1Controller(SqlConnection con)
        {
           // dynamic mymodel = new ExpandoObject();
          
            
            _connection = con;
        }
         
        public IActionResult Index()
        {
            ViewBag.Name="Surain";
            ViewBag.FName = rettemp();
            List<Item> objList = Item.GetSellersItems(1,_connection); ;
          
            return View(objList);
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

        public IActionResult EditItem(int id)
        {
            Item item = Item.GetOneSellersItem(id, _connection);
            ViewBag.ItemName = item.Name;
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditItem(Item a)
        {
           // Item.EditItemDb(a, _connection);
            
            return View(a);
        }














        public int InsertItemDB(Item a)//
        {
            int rows=Item.InsertItemDB(a,_connection);//call insert item method in Item Model
            return rows;
           



        }

        



        public string rettemp()
        { return "rocky"; }
    }
}
