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

        public static int UpdateProfile(Customer a, SqlConnection _connection)
        {
            _connection.Open();
            // string query = $"Insert into customer(name) values('y')";

            string query = $"update Customer set name='{a.Name}',email='{a.Email}',address='{a.Address}',password='{a.Password}' where id={a.Id}";
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
            int id = 0;
            while (dataReader.Read())
            {
                dbEmail = (string)dataReader["Email"];
                dbPassword = (string)dataReader["Password"];
                id=(int)dataReader["id"];
            }
            _connection.Close();

            if (dbEmail == "")
            {
                return 0;
            }
            else
            {
                if (dbPassword == password)
                {
                    return id;

                }
                else
                {
                    return 2;

                }
            }


        }
        public static  Customer getCustomerDetails(int idd, SqlConnection _connection)
        {
            // SqlConnection connection = new SqlConnection();
            // connection.ConnectionString= $@"Data Source=LAPTOP-FAS31PRJ;Initial Catalog=EcommerceBook;User Id=LAPTOP-FAS31PRJ\surai;Password=;Integrated Security=true";
            _connection.Open();
            //String email = a.Email;

            string query = $"select * from customer where id={idd}";
            Console.WriteLine(query);
            SqlCommand command = new SqlCommand(query, _connection);
            SqlDataReader dataReader = command.ExecuteReader();
            Customer customer = null;

            while (dataReader.Read())
            {
                customer = new Customer()
                {
                    Name = (String)dataReader["name"],
                    Address = (String)dataReader["address"],
                    Id = (int)dataReader["id"],
                    Password = (String)dataReader["password"],
                   Email = (String)dataReader["email"],
                   ConfirmPassword = (String)dataReader["password"],
                };
         
                


            }


            _connection.Close();

            return customer;
        }

        public static Customer getCustomerDetailsByEmail(string email, SqlConnection _connection)
        {
            // SqlConnection connection = new SqlConnection();
            // connection.ConnectionString= $@"Data Source=LAPTOP-FAS31PRJ;Initial Catalog=EcommerceBook;User Id=LAPTOP-FAS31PRJ\surai;Password=;Integrated Security=true";
            _connection.Open();
            //String email = a.Email;

            string query = $"select * from customer where email='{email}'";
            Console.WriteLine(query);
            SqlCommand command = new SqlCommand(query, _connection);
            SqlDataReader dataReader = command.ExecuteReader();
            Customer customer = null;

            while (dataReader.Read())
            {
                customer = new Customer()
                {
                    Name = (String)dataReader["name"],
                    Address = (String)dataReader["address"],
                    Id = (int)dataReader["id"],
                    Password = (String)dataReader["password"],
                    Email = (String)dataReader["email"],
                    ConfirmPassword = (String)dataReader["password"],
                };




            }


            _connection.Close();

            return customer;
        }
    }
}
