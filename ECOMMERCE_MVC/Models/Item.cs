using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ECOMMERCE_MVC.Models
{
    public class Item
    {
        public Item()
        { 
        }
        
        public int ItemId { get; set; }
        [Required]
        [Display(Name = "Product Name")]
        public string Name { get; set; }
        [Required]
        [ScaffoldColumn(true)]
        [StringLength(100, ErrorMessage = "The cannnot exceed 100 characters")]
        public string Description { get; set; }

        [Required]
        [Display(Name="Category")]
        public string CategoryText { get; set; }
        [Required]
        [Display(Name = "Image Link")]
        public string ImageLink { get; set; }

        public int IsAvailable { get; set; }
        [Required]
        [Display(Name = "Selling Price")]
        public double Price { get; set; }

        public int SellerId { get; set; }
        public int CurrentOwner { get; set; }




        public static int InsertItemDB(Item a,SqlConnection _connection,int currentId)
        {
            _connection.Open();
            double price = a.Price;
            double positiveprice = price > 0 ? price : -price;//convert price to positive
            string query = $"Insert into Item(name,description,categorytext,imagelink,isavailable,price,sellerid,currentowner) values('{a.Name}','{a.Description}','{a.CategoryText}','{a.ImageLink}',1,{positiveprice},{currentId},{currentId})";
            SqlCommand command = new SqlCommand(query, _connection);
            int rows = command.ExecuteNonQuery();
            _connection.Close();
            return rows;



        }
       
        public static List<Item> GetSellersItems(int id, SqlConnection _connection)
        {
            List<Item> ItemList = new List<Item>();
            _connection.Open();
            string query = $"select * from item where sellerid ={id}";
            if (id == -99)
            {
                query = $"select * from item";

            }
           SqlCommand command = new SqlCommand(query, _connection);
           SqlDataReader datareader=command.ExecuteReader();
            while (datareader.Read())
            {
                Item item = new Item() {
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

        public static Item GetOneSellersItem(int id, SqlConnection _connection)
        {
            Item retitem = null;
            _connection.Open();
            string query = $"select * from item where itemid={id}";
            SqlCommand command = new SqlCommand(query, _connection);
            SqlDataReader datareader = command.ExecuteReader();
            while (datareader.Read())
            {
                retitem = new Item()
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
                

            }
            datareader.Close();
            _connection.Close();
            return retitem;



        }
        public static int EditItemDb(Item a, SqlConnection _connection)
        {
            _connection.Open();
            double price = a.Price;
            double positiveprice = price > 0 ? price : -price;//convert price to positive
               string query = $"update item set price={positiveprice} ,name='{a.Name}',description='{a.Description}',categorytext='{a.CategoryText}',imagelink='{a.ImageLink}'  where itemid={a.ItemId}";
            //string query = $"update item set name='{a.Name}' where itemid={a.ItemId}";

            Debug.WriteLine("Edit 2");
            Debug.WriteLine(query);

            SqlCommand command = new SqlCommand(query, _connection);
            int rows = command.ExecuteNonQuery();
            _connection.Close();
            return rows;



        }
        public static int DeleteItemDb(int a, SqlConnection _connection)
        {
            _connection.Open();
            
           
            string query = $"Delete from item   where itemid={a}";
            //string query = $"update item set name='{a.Name}' where itemid={a.ItemId}";

            Debug.WriteLine("Delete2 2");
            Debug.WriteLine(query);

            SqlCommand command = new SqlCommand(query, _connection);
            int rows = command.ExecuteNonQuery();
            _connection.Close();
            return rows;



        }
    }
}
