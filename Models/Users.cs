using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ST10120867_FarmCentral.Models
{
    //get and set for Users model with validation, used in form to create new Users

    public class Users
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

        [Required(ErrorMessage = "Confirm Password is required")]

        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "Passwords do not match.")]
        public string confirmPassword { get; set; }
    }
    }