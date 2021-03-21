using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using ECOMMERCE_MVC.Models;
using System.Diagnostics;

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
            try
            {
                var customer = JsonConvert.DeserializeObject<Models.Customer>(HttpContext.Session.GetString("CustomerSession"));
                ViewBag.IsLogged = "true";
                ViewBag.Name = customer.Name;
                List<Item> objList = CartItem.GetCartItems(customer.Id, _connection);
                if ((objList != null) && (!objList.Any()))
                {


                    ViewBag.Total = 0;

                }
                else {
                    double total = 0;
                    foreach (var item in objList)
                    {
                        // Console.WriteLine("Amount is {0} and type is {1}", money.amount, money.type);
                       total = item.Price + total;
                    
                    }
                    
                    ViewBag.Total = total;



                }


                    return View(objList);

            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Customer1");

            }
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

        public IActionResult DeleteCartItem(int id)
        {
           

            try
            {
                var customer = JsonConvert.DeserializeObject<Models.Customer>(HttpContext.Session.GetString("CustomerSession"));
                // Debug.WriteLine("http:// Edit  called");
                CartItem.DeleteCartItem(_connection, id);
                Item.ItemRemovedFromCart(id, _connection);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Customer1");
            }
        }

        public IActionResult CheckOut()
        {
            try
            {
                var customer = JsonConvert.DeserializeObject<Models.Customer>(HttpContext.Session.GetString("CustomerSession"));
                ViewBag.IsLogged = "true";
                ViewBag.Name = customer.Name;
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Customer1");
            }
           
        
        
        }
        public IActionResult PaymentSuccess()
        {
            try
            {
                var customer = JsonConvert.DeserializeObject<Models.Customer>(HttpContext.Session.GetString("CustomerSession"));
                ViewBag.IsLogged = "true";
                ViewBag.Name = customer.Name;
                List<int> ItemIdList= CartItem.GetCartItemIdList(customer.Id, _connection);

              //  Debug.WriteLine(ItemIdList.Count);
                Debug.WriteLine("iiiiiiiiiiiiiiiiiiii");//int[] x=ItemIdList.GetRange(0,4).ToArray();
                for (int i = 0; i < 5; i++)
                {
                    Debug.WriteLine(i);


                }
                Debug.WriteLine("xxooooooooooooooooooooooooooooooooooooooooo");
               
                int[] ItemIdListArray = ItemIdList.GetRange(0, ItemIdList.Count).ToArray();
                string arraylist = string.Join(",", ItemIdListArray);
                Debug.WriteLine(arraylist);
                Item.ItemSold(arraylist,_connection);
                CartItem.DeleteMultipleCartItem(customer.Id,_connection);
                ViewBag.IsLogged = "true";
                return View();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return RedirectToAction("Index", "Customer1");
            }


        }
    }


}
