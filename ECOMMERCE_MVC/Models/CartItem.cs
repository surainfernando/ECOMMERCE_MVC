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
        public string DateModiFied { get; set; }

        public static int AddCartItem(int OwnerId,int ItemId,SqlConnection _connection)
        {

            _connection.Open();
            // string query = $"Insert into customer(name) values('y')";
            DateTime today = DateTime.Today;
            string query = $"Insert into cart(ownerid,itemid,date_modified) values({OwnerId},{ItemId},'{today.ToString("MMMM dd, yyyy")}')";
            SqlCommand command = new SqlCommand(query, _connection);
            int rows = command.ExecuteNonQuery();

            _connection.Close();
            return rows;

        }

        public static int DeleteCartItem(SqlConnection _connection, int id)
        {
            string sql = $"Delete from cart where itemid={id}";
            _connection.Open();
            SqlCommand command = new SqlCommand(sql,_connection);
            int x = command.ExecuteNonQuery();
            _connection.Close();
            return x;
        
        }
        public static int DeleteMultipleCartItem(int id,SqlConnection _connection)
        {
            string sql = $"Delete from cart where ownerid={id}";
            _connection.Open();
            SqlCommand command = new SqlCommand(sql, _connection);
            int x = command.ExecuteNonQuery();
            _connection.Close();
            return x;

        }


        public static List<Item> GetCartItems(int customerid, SqlConnection _connection)
        {
            // this method will be used for multiple purposes. If normal id is passed, a particualr sellers items will be returnes
            // but if -99 is passed as id , it means it's for diaply purposes only in home page, so all itrms will be returned.
            List<Item> ItemList = new List<Item>();
            _connection.Open();
            string query = $"select  cart.itemid as itemid,item.name as name,item.description as description,item.categorytext as categorytext,item.imagelink as imagelink, item.isavailable as isavailable, item.price as price, item.sellerid as sellerid, item.currentowner as currentowner from cart inner join item on item.itemid = cart.itemid where cart.ownerid = {customerid}";
            SqlCommand command = new SqlCommand(query, _connection);
            SqlDataReader datareader = command.ExecuteReader();
            while (datareader.Read())
            {
                Item item = new Item()
                {
                    ItemId = (int)datareader["itemid"],
                    Name = (string)datareader["name"],
                    Description = (string)datareader["description"],
                    CategoryText = (string)datareader["categorytext"],
                    ImageLink = (string)datareader["imagelink"],
                    IsAvailable = (int)datareader["isavailable"],
                    SellerId = (int)datareader["sellerid"],
                    CurrentOwner = (int)datareader["currentowner"],
                    Price = Convert.ToDouble(datareader["Price"])

                };
                ItemList.Add(item);

            }
            datareader.Close();
            _connection.Close();
            return ItemList;



        }

        public static List<int> GetCartItemIdList(int customerid, SqlConnection _connection)
        {
            // this method will be used for multiple purposes. If normal id is passed, a particualr sellers items will be returnes
            // but if -99 is passed as id , it means it's for diaply purposes only in home page, so all itrms will be returned.
            List<int> ItemIdList = new List<int>();
            _connection.Open();
            string query = $"select  cart.itemid as itemid from cart where cart.ownerid ={customerid}";
            SqlCommand command = new SqlCommand(query, _connection);
            SqlDataReader datareader = command.ExecuteReader();
            while (datareader.Read())
            {
                
                ItemIdList.Add((int)datareader["itemid"]);

            }
            datareader.Close();
            _connection.Close();
            return ItemIdList;



        }


    }
}
