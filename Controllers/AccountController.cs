using ST10120867_FarmCentral.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ST10120867_FarmCentral.Controllers
{
    //AccountController handels login and logout
    public class AccountController : Controller
    {
        //get login view
        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.Message = "";

            return View();
        }

        //post request connects to SQL server database and trys to log user,employee,admin in
        [HttpPost]
        public ActionResult Login(Login login, Hashing hash)
        {
            //create connection
            SqlConnection con = new SqlConnection("Data Source = ISMAIL-PC; Initial Catalog = 'FarmCentral'; Integrated Security = True" );
            string query = "SELECT [ID],[Name],[Username],[PasswordHash],[Role] FROM [dbo].[Admin] where [Username]= @Username and [PasswordHash]= @PasswordHash";
            //query to admin table
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                cmd.Parameters.AddWithValue("@Username", login.userName);
                //cmd.Parameters.AddWithValue("@PasswordHash", login.password);
                cmd.Parameters.AddWithValue("@PasswordHash", hash.HashPassword(login.password));
                //call hash model to encrypt password
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    //pass userID,Name and role into sessions if there is a user in the admin table
                    Session["userID"] = (int)reader["ID"];
                    Session["username"] = login.userName.ToString();
                    Session["admin"] = reader["Role"].ToString();
                    return RedirectToAction("ViewProducts", "Employee");
                }
                else
                {//if user is not an admin try to login to employees table
                    reader.Close();
                    string query2 = "SELECT [ID],[Name],[Username],[PasswordHash],[Role] FROM [dbo].[Employee] where [Username]= @Username and [PasswordHash]= @PasswordHash";
                    SqlCommand cmd2 = new SqlCommand(query2, con);
                    cmd2.Parameters.AddWithValue("@Username", login.userName);
                    cmd2.Parameters.AddWithValue("@PasswordHash", hash.HashPassword(login.password));
                    SqlDataReader reader2 = cmd2.ExecuteReader();

                    if (reader2.Read())
                    {//pass userID,Name and role into sessions if there is a user in the employees table
                        Session["userID"] = (int)reader2["ID"];
                        Session["username"] = login.userName.ToString();
                        Session["employee"] = reader2["Role"].ToString();
                        return RedirectToAction("ViewProducts", "Employee");
                    }
                    else
                    {//if user is not an employees try to login to users table
                        reader2.Close();
                        string query3 = "SELECT [ID],[Name],[Username],[PasswordHash],[Role] FROM [dbo].[Users] where [Username]= @Username and [PasswordHash]= @PasswordHash";
                        SqlCommand cmd3 = new SqlCommand(query3, con);
                        cmd3.Parameters.AddWithValue("@Username", login.userName);
                        cmd3.Parameters.AddWithValue("@PasswordHash", hash.HashPassword(login.password));
                        SqlDataReader reader3 = cmd3.ExecuteReader();

                        if (reader3.Read())
                        {//pass userID,Name and role into sessions if there is a user in the users table
                            Session["userID"] = (int)reader3["ID"];
                            Session["username"] = login.userName.ToString();
                            Session["user"] = reader3["Role"].ToString();
                            return RedirectToAction("AddProduct", "Products");
                        }
                        else
                        {
                            reader3.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;

            }

            con.Close();
            return View();
        }
        /* 15 How to do Authentication, Authorisation in ASP.NET MVC
         * - mahesh panhale
         * https://www.youtube.com/watch?v=7m6pY8Bpxj4&t=491s 
         * Aug 18, 2016
         */

        [HttpGet]
        public ActionResult Logout()
        {
            //end all sessions and redirect to Home page on logout
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }       
}
