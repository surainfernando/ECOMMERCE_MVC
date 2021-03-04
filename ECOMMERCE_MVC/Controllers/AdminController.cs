using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

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
        public IActionResult Create()
        {
            FirstInsert();

            return View();
        }
        public void FirstInsert()
        {
           // SqlConnection connection = new SqlConnection();
           // connection.ConnectionString= $@"Data Source=LAPTOP-FAS31PRJ;Initial Catalog=EcommerceBook;User Id=LAPTOP-FAS31PRJ\surai;Password=;Integrated Security=true";
            _connection.Open();
            string query = "insert into a(id,name) values(8,'77777uior')";
            SqlCommand command = new SqlCommand(query,_connection);
            command.ExecuteNonQuery();
        
        }

    }
}
