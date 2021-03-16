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
using System.Net;

namespace ECOMMERCE_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public readonly SqlConnection _connection;
        enum selectoptions { 
        All,
        Books,
        Computer,
        Phone,
        Electronics,
        Kitchenware

        }

        public HomeController(ILogger<HomeController> logger, SqlConnection con)
        {
            _logger = logger;
            _connection = con;
        }


        public IActionResult Index(String? option)
        {    if (TempData["list"] != null)
            {
                Debug.WriteLine("9999999999999999999999999999");
                List<Item> objList = JsonConvert.DeserializeObject<List<Item>>((string)TempData["list"]);
                return View(objList);
            }

            else {
                string optiontext;
                if (option == null)
                {
                    ViewBag.option = $"0";
                    optiontext = "All";
                }
                else
                {
                    int index = 0;
                    foreach (selectoptions val in Enum.GetValues(typeof(selectoptions)))
                    {
                        string xx = Convert.ToString(val);
                        Console.WriteLine(val);

                        if (String.Equals(option, xx))
                        {


                            break;
                        }
                        index++;
                    }
                    ViewBag.option = $"{index}";
                    optiontext = option;
                }

                try
                {
                    var customer = JsonConvert.DeserializeObject<Models.Customer>(HttpContext.Session.GetString("CustomerSession"));
                    ViewBag.IsLogged = "true";
                    // Debug.WriteLine("http:// Edit  called");
                    List<Item> objList = Item.GetitemsForHome(customer.Id, _connection, optiontext); ;
                    return View(objList);
                }
                catch (Exception e)
                {
                    List<Item> objList = Item.GetitemsForHome(-99, _connection, optiontext); ;
                    ViewBag.IsLogged = "false";
                    return View(objList);
                }




            }


        }
        public IActionResult Search(String? searchtext1)
        {

            
            string searchtext;
            if (searchtext1 == null)
            {
                ViewBag.option = $"0";
                searchtext = "All";
            }
            else
            {
                searchtext = searchtext1;
            }

            try
            {
                var customer = JsonConvert.DeserializeObject<Models.Customer>(HttpContext.Session.GetString("CustomerSession"));
                // Debug.WriteLine("http:// Edit  called");
                List<Item> objList = Item.GetitemsForHomeSearch(customer.Id, _connection, searchtext); ;
                TempData["list"] = JsonConvert.SerializeObject(objList);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                List<Item> objList = Item.GetitemsForHomeSearch(-99, _connection, searchtext);
                Debug.WriteLine("88888888888888888888888888888888888888888888");
                TempData["list"] = JsonConvert.SerializeObject(objList);
                Debug.WriteLine("length="+objList.Count);

                return RedirectToAction("Index","Home");
            }



        }

        public IActionResult Privacy(String id)
        {
            ViewBag.name = id;
            ViewBag.name1 = "fffff";
            return View();
        }
        public IActionResult ItemView(int id)
        {
            Item item = Item.GetOneSellersItem(id, _connection);
            item.ImageLink = DoesImageExistRemotely(item.ImageLink);
            //ViewBag.ItemName = item.Name;
            return View(item);
            
          
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
    }
}
