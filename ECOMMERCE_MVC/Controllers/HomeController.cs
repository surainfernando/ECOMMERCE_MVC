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

        public HomeController(ILogger<HomeController> logger, SqlConnection con)
        {
            _logger = logger;
            _connection = con;
        }


        public IActionResult Index()
        {  
            try
            {
                var customer = JsonConvert.DeserializeObject<Models.Customer>(HttpContext.Session.GetString("CustomerSession"));
                // Debug.WriteLine("http:// Edit  called");
                List<Item> objList = Item.GetitemsForHome(customer.Id, _connection); ;
                return View(objList);
            }
            catch (Exception e)
            {
                List<Item> objList = Item.GetitemsForHome(-99, _connection); ;
                return View(objList);
            }
           

            
        }

        public IActionResult Privacy()
        {
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
