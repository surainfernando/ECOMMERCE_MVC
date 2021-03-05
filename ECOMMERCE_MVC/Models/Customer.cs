using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ECOMMERCE_MVC.Models
{
    public class Customer
    {
        public Customer()
        { 

        }
        [Key]
        public int Id { get; set; }



        [Required]
        public String Email { get; set; }


        [Required]
        public string Password { get; set; }

        [NotMapped] // Does not effect with your database
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public String Name { get; set; }

        [Required]
        public String Address { get; set; }

        public static int InsertCustomer(Customer a, SqlConnection _connection)
        {
            _connection.Open();
           // string query = $"Insert into customer(name) values('y')";

            string query = $"Insert into Customer(name,email,password,address) values('{a.Name}','{a.Email}','{a.Password}','{a.Address}')";
            SqlCommand command = new SqlCommand(query, _connection);
            int rows = command.ExecuteNonQuery();
            _connection.Close();
            return rows;



        }

        public static int LoginDB(Customer a, SqlConnection _connection)
        {
            // SqlConnection connection = new SqlConnection();
            // connection.ConnectionString= $@"Data Source=LAPTOP-FAS31PRJ;Initial Catalog=EcommerceBook;User Id=LAPTOP-FAS31PRJ\surai;Password=;Integrated Security=true";
            _connection.Open();
            //String email = a.Email;
            String password = a.Password;
            string query = $"select * from customer where email='{a.Email}'";
            Console.WriteLine(query);
            SqlCommand command = new SqlCommand(query, _connection);
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
            else
            {
                if (dbPassword == password)
                {
                    return 1;

                }
                else
                {
                    return 2;

                }
            }


        }
    }
}
