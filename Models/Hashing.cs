using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ST10120867_FarmCentral.Models
{
    //Model that encrypts all password accross the site
    public class Hashing
    {
        public string HashPassword(string password)
        {
            // method to hash user password 
            SHA1CryptoServiceProvider sHA1 = new SHA1CryptoServiceProvider();
            byte[] password_bytes = Encoding.ASCII.GetBytes(password);
            byte[] encrypted_bytes = sHA1.ComputeHash(password_bytes);

            return Convert.ToBase64String(encrypted_bytes);
        }
    }
}