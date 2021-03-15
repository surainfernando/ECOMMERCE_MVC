using ECOMMERCE_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace ECOMMERCE_MVC.Controllers
{
    public class Item1Controller : Controller
    { //item displa  link
        public readonly SqlConnection _connection;

        public string email="";
        public string name = "";
      
        public string temp { get; set; }
        public Item1Controller(SqlConnection con)
        {
           // dynamic mymodel = new ExpandoObject();
          
            
            _connection = con;
        }
         
        public IActionResult Index2()
        { 
            
            try
            {
                var customer = JsonConvert.DeserializeObject<Models.Customer>(HttpContext.Session.GetString("CustomerSession"));

                return View(customer);
            }
            catch (Exception e)
            {
                Customer customer = null;
                return View(customer);
            }
           

;
        }
        public IActionResult Index()
        {
            try {
                var customer = JsonConvert.DeserializeObject<Models.Customer>(HttpContext.Session.GetString("CustomerSession"));

                List<Item> objList = Item.GetSellersItems(customer.Id, _connection); ;

                return View(objList);

            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Customer1");
            
            }

           
        }


        public IActionResult AddItem()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddItem(Item a)
        {     
            Console.WriteLine("Add Command");
            Debug.WriteLine("http:// My debug string here");
            a.ImageLink = DoesImageExistRemotely(a.ImageLink);

            try
            {
                var customer = JsonConvert.DeserializeObject<Models.Customer>(HttpContext.Session.GetString("CustomerSession"));
                Item.InsertItemDB(a, _connection, customer.Id);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Customer1");

            }
            
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
            a.ImageLink = DoesImageExistRemotely(a.ImageLink);

            try
            {
                var customer = JsonConvert.DeserializeObject<Models.Customer>(HttpContext.Session.GetString("CustomerSession"));
                Debug.WriteLine("http:// Edit  called");
                // Item.EditItemDb(a, _connection);
                int row = Item.EditItemDb(a, _connection);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Customer1");
            }
           
        }
        public IActionResult DeleteItem(int id)
        {

            try
            {
                var customer = JsonConvert.DeserializeObject<Models.Customer>(HttpContext.Session.GetString("CustomerSession"));

                Debug.WriteLine("http:// Edit  called");
                // Item.EditItemDb(a, _connection);
                int row = Item.DeleteItemDb(id, _connection);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Customer1");
            }
           
        }

        public void getCustomerDetails1(int idd)
        {
            // SqlConnection connection = new SqlConnection();
            // connection.ConnectionString= $@"Data Source=LAPTOP-FAS31PRJ;Initial Catalog=EcommerceBook;User Id=LAPTOP-FAS31PRJ\surai;Password=;Integrated Security=true";
            _connection.Open();
            //String email = a.Email;
           
            string query = $"select * from customer where id={idd}";
            Console.WriteLine(query);
            SqlCommand command = new SqlCommand(query, _connection);
            SqlDataReader dataReader = command.ExecuteReader();
            
            
            while (dataReader.Read())
            {
               email = (string)dataReader["Email"];
               name = (string)dataReader["name"];
               
          
            }

            _connection.Close();


        }




    
















        /*public int InsertItemDB(Item a)//
        {
            //int rows=Item.InsertItemDB(a,_connection);//call insert item method in Item Model
            return rows;
           



        }*/

        public static string DoesImageExistRemotely(string uriToImage)
        {
            

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uriToImage);

                request.Method = "HEAD";
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return uriToImage;
                    }
                    else
                    {
                        return "https://www.generationsforpeace.org/wp-content/uploads/2018/03/empty-300x240.jpg";
                    }
                }
            }
            catch (WebException) { return "https://www.generationsforpeace.org/wp-content/uploads/2018/03/empty-300x240.jpg"; }
            catch
            {
                return "https://www.generationsforpeace.org/wp-content/uploads/2018/03/empty-300x240.jpg";
            }
        }



        public string rettemp()
        { return "rocky"; }
    }
}
