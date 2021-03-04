using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECOMMERCE_MVC.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }


        
        [Required]
        public String Email { get; set; }


        [Required]
        public String Password { get; set; }
    }
}
