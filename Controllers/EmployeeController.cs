using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ST10120867_FarmCentral.Models;

namespace ST10120867_FarmCentral.Controllers
{
    public class EmployeeController : Controller
    {
        //Load all product in database to view
        public ActionResult ViewProducts()
        {
                //list object to add data into
                List<GetProducts> getProducts = new List<GetProducts>();
                //create connection
                SqlConnection con = new SqlConnection("Data Source = ISMAIL-PC; Initial Catalog = 'FarmCentral'; Integrated Security = True");

                SqlCommand cmd = new SqlCommand("Select * From [dbo].[Products] INNER JOIN [Users] ON[Products].[UserID] = [Users].[ID] ", con);
                //query to select all users
                con.Open();
                SqlDataReader idr = cmd.ExecuteReader();

                if (idr.HasRows)

                {

                    while (idr.Read())

                    {

                        getProducts.Add(new GetProducts

                        {
                            //pass data into list
                            ID = Convert.ToInt32(idr["ID"]),

                            ProductName = Convert.ToString(idr["ProductName"]),

                            ProductType = Convert.ToString(idr["ProductType"]),

                            ProductQuantity = Convert.ToInt32(idr["ProductQuantity"]),

                            UserName = Convert.ToString(idr["UserName"]),

                            ProductPrice = Convert.ToString(idr["ProductPrice"]),

                            ProductDate = Convert.ToDateTime(idr["ProductDate"]),

                        });

                    }

                }
                //send list to view
            return View(getProducts);
        }

        //get AddEmployee view
        public ActionResult AddEmployee()
        {
            ViewBag.Message = "";

            return View();
        }

        //post request connects to SQL server database and trys to add new employee
        [HttpPost]
        public ActionResult AddEmployee(Employees employees, Hashing hash)
        {
            //create connection
            SqlConnection con = new SqlConnection("Data Source = ISMAIL-PC; Initial Catalog = 'FarmCentral'; Integrated Security = True");
            string query = "INSERT INTO [dbo].[Employee](Name,Username,PasswordHash,role) VALUES (@FullName,@Username,@PasswordHash,@Role)";
            //query to insert into employees table
            SqlCommand cmd = new SqlCommand(query, con);
                //add Parameters
                cmd.Parameters.AddWithValue("@FullName", employees.fullName);
                cmd.Parameters.AddWithValue("@Username", employees.userName);
                cmd.Parameters.AddWithValue("@PasswordHash", hash.HashPassword(employees.password));
                cmd.Parameters.AddWithValue("@Role", "Employee");

                con.Open();
                cmd.ExecuteNonQuery();
            
            con.Close();

            return View();
        }

    }
}
