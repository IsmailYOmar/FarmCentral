using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ST10120867_FarmCentral.Models;

namespace ST10120867_FarmCentral.Controllers
{
    public class UserController : Controller
    {

        public ActionResult AddUser()
        {

            return View();

        }
        //post request connects to SQL server database and trys to add new users
        [HttpPost]
        public ActionResult AddUser(Users user, Hashing hash)
        {
            //create connection
            SqlConnection con = new SqlConnection("Data Source = ISMAIL-PC; Initial Catalog = 'FarmCentral'; Integrated Security = True");
            string query = "INSERT INTO [dbo].[Users](Name,Username,PasswordHash,role) VALUES (@FullName,@Username,@PasswordHash,@Role)";
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                //add Parameters
                cmd.Parameters.AddWithValue("@FullName", user.fullName);
                cmd.Parameters.AddWithValue("@Username", user.userName);
                cmd.Parameters.AddWithValue("@PasswordHash", hash.HashPassword(user.password));
                cmd.Parameters.AddWithValue("@Role", "User");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch
            {
                ViewBag.Message = "ERROR!!!!: Failed to add user.";
            }
            ViewBag.Message = "User added.";
            return View();
        }




    }

}
