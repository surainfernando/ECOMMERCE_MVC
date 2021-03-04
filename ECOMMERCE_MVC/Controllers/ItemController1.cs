using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECOMMERCE_MVC.Controllers
{
    public class ItemController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddItem()
        {
            return View();
        }
    }
}
