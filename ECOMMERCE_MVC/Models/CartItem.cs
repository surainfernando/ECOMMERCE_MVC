using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECOMMERCE_MVC.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }

        public int OwnerId { get; set; }

        public int ItemId { get; set; }

        public static int AddCartItem(int OwnerId,int ItemId,SqlConnection _connection)
        {

            _connection.Open();
            // string query = $"Insert into customer(name) values('y')";

            string query = $"Insert into cart(ownerid,itemid) values({OwnerId},{ItemId})";
            SqlCommand command = new SqlCommand(query, _connection);
            int rows = command.ExecuteNonQuery();

            _connection.Close();
            return rows;

        }
    }
}
