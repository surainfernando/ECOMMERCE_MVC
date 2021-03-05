using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECOMMERCE_MVC.Models
{
    public class Item
    { public int ItemId { get; set; }
        [Required]
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
        public float Price { get; set; }

        public int SellerId { get; set; }
        public int CurrentOwner { get; set; }




        public static int InsertItemDB(Item a,SqlConnection _connection)
        {
            _connection.Open();
            float price = a.Price;
            float positiveprice = price > 0 ? price : -price;//convert price to positive
            string query = $"Insert into Item(name,description,categorytext,imagelink,isavailable,price,sellerid,currentowner) values('{a.Name}','{a.Description}','{a.CategoryText}','{a.ImageLink}',1,{positiveprice},1,1)";
            SqlCommand command = new SqlCommand(query, _connection);
            int rows = command.ExecuteNonQuery();
            _connection.Close();
            return rows;



        }
    }
}
