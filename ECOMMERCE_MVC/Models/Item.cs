using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Net;
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
        public string ImageLink { get;
            set; }

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
            // this method will be used for multiple purposes. If normal id is passed, a particualr sellers items will be returnes
            // but if -99 is passed as id , it means it's for diaply purposes only in home page, so all itrms will be returned.
            List<Item> ItemList = new List<Item>();
            _connection.Open();
            string query = $"select * from item where sellerid ={id}";
            if (id == -99)
            {
                query = $"select * from item where isavailable=1";

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

        public static List<Item> GetitemsForHome(int id, SqlConnection _connection, string option)
        {
            // this method will be used for multiple purposes. If normal id is passed, a particualr sellers items will be returnes
            // but if -99 is passed as id , it means it's for diaply purposes only in home page, so all itrms will be returned.
            List<Item> ItemList = new List<Item>();
            Debug.WriteLine("------------------------------------------------");
            Debug.WriteLine(option);
            _connection.Open();
            string query = $"select * from item where  isavailable=1 and sellerid!={id} and categorytext='{option}'";

            if (option.Equals("All"))
            {
                query = $"select * from item where isavailable=1  and sellerid!={id}";
            }

            if (id == -99)
            {
                if (option.Equals("All"))
                {
                    query = $"select * from item where isavailable=1 ";
                }
                else { query = $"select * from item where isavailable=1 and categorytext='{option}'"; }
               

            }
            Debug.WriteLine(query);
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


        public static List<Item> GetitemsForHomeSearch(int id, SqlConnection _connection, string searchtext) 
        {
          /* This is the search function for home,ir is sellerid, searchtext is text enterd in the text
           * Important : 
           box*/
            
            List<Item> ItemList = new List<Item>();
            Debug.WriteLine("------------------------------------------------");
         //   Debug.WriteLine(option);
            _connection.Open();
            
            string query = $"select * from item  where isavailable=1 and sellerid!={id} and (description like '%{searchtext}%' or name like '%{searchtext}%'  or categorytext like '%{searchtext}%' )";
            
            if (id == -99)
            {
                 query = query = $"select * from item  where isavailable=1 and (description like '%{searchtext}%' or name like '%{searchtext}%'  or categorytext like '%{searchtext}%' )";


            }
     
            Debug.WriteLine(query);
            SqlCommand command = new SqlCommand(query, _connection);

            try
            {
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
            catch (Exception e)
            {

                _connection.Close();
                return ItemList;
            }
           
            



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
            
           
            string query = $"Delete from item  where itemid={a}";
            //string query = $"update item set name='{a.Name}' where itemid={a.ItemId}";

           // Debug.WriteLine("Delete2 2");
           // Debug.WriteLine(query);

            SqlCommand command = new SqlCommand(query, _connection);
            int rows = command.ExecuteNonQuery();
            _connection.Close();
            return rows;



        }
        public static int AddToCart(int a, SqlConnection _connection)
        {
            _connection.Open();


            string query = $"Update  item   set isavailable=2    where itemid={a}";
            //string query = $"update item set name='{a.Name}' where itemid={a.ItemId}";

            Debug.WriteLine("Delete2 2");
            Debug.WriteLine(query);

            SqlCommand command = new SqlCommand(query, _connection);
            int rows = command.ExecuteNonQuery();
            _connection.Close();
            return rows;



        }

        public static string DoesImageExistRemotely(string uriToImage)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uriToImage);

            request.Method = "HEAD";

            try
            {
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
    }//
}
