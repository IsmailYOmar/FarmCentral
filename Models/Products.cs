using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace ST10120867_FarmCentral.Models
{
    //get and set for Products model with validation, used in form to create new Products

    public class Products
    {


        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product Name is required")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Product Type is required")]
        public string ProductType { get; set; }

        [Required(ErrorMessage = "Product Quantity is required")]
        public int ProductQuantity { get; set; }

        [Required(ErrorMessage = "Product Price  is required")]
        public double ProductPrice { get; set; }
        public string stProductPrice { get; set; }
        public string UserID { get; set; }
        public DateTime ProductDate { get; set; }
}
}


