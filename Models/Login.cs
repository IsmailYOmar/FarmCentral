using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;

namespace ST10120867_FarmCentral.Models
{
    //get and set for Login model with validation, used in form to Login new users,employees and the admin

    public class Login
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string fullName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string userName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        public string role { get; set; }

    }
}