using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ECOMMERCE_MVC.Models;

namespace ECOMMERCE_MVC.Controllers
{
    public class AdminController : Controller
    {
        public readonly SqlConnection _connection;

        public AdminController(SqlConnection con)
        {
            _connection = con;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Admin a)
        {
           int s=FirstInsert(a);
            if (s == 1) { return RedirectToAction("ViewProducts"); }
            else if (s == 0)
            {
                ModelState.AddModelError("Email", "The Email is incorrect");
                return View();
            }
            else {
                ModelState.AddModelError("Password", "The  Password is incorrect");
                return View();

            }

            
        }

        public IActionResult ViewProducts()
        {
            return View();
        }

        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCategory(Category a)
        {
            InsertCategory(a);
            
            return RedirectToAction("ViewProducts");
        }


        public int InsertCategory(Category a)
        {
            _connection.Open();
            string query = $"Insert into Category(categorytext) values('{a.CategoryText}')";
            SqlCommand command = new SqlCommand(query, _connection);
            int rows= command.ExecuteNonQuery();
            return rows;

        }

        public int FirstInsert(Admin a)
        {
           // SqlConnection connection = new SqlConnection();
           // connection.ConnectionString= $@"Data Source=LAPTOP-FAS31PRJ;Initial Catalog=EcommerceBook;User Id=LAPTOP-FAS31PRJ\surai;Password=;Integrated Security=true";
            _connection.Open();
            //String email = a.Email;
            String password = a.Password;
            string query = $"select * from adminprofile where email='{a.Email}'";
            Console.WriteLine(query);
            SqlCommand command = new SqlCommand(query,_connection);
            SqlDataReader dataReader = command.ExecuteReader();
                 string dbEmail = "";
                string dbPassword = ""; 
            while (dataReader.Read())
            {
                dbEmail = (string)dataReader["Email"];
                dbPassword = (string)dataReader["Password"];
            }

            if (dbEmail == "")
            {
                return 0;
            }
            else {
                if (dbPassword == password)
                {
                    return 1;

                }
                else {
                    return 2;
                
                }
            }


        }

    }
}
