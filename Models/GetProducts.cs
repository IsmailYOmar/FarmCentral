using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ST10120867_FarmCentral.Models
{
    //get and set for GetProducts model , used to get Product data from database
    public class GetProducts
    {
        public int ID { get; set; }

        public string ProductName { get; set; }

        public string ProductType { get; set; }

        public int ProductQuantity { get; set; }
        public string ProductPrice { get; set; }
        public string UserName { get; set; }

        public DateTime ProductDate { get; set; }
    }
}