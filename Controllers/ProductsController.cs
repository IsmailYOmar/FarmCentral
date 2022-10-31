using ST10120867_FarmCentral.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ST10120867_FarmCentral.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products WHERE UserID is current  UserID
        public ActionResult ProductsGrid()
        {   
            //list object to add data into
            List<GetProducts> getProducts = new List<GetProducts>();
            //create connection
            SqlConnection con = new SqlConnection("Data Source = ISMAIL-PC; Initial Catalog = 'FarmCentral'; Integrated Security = True");

            SqlCommand cmd = new SqlCommand("Select * From [dbo].[Products] INNER JOIN [Users] ON[Products].[UserID] = [Users].[ID] WHERE UserID = @UserID", con);

            con.Open();
            int userID = (int)System.Web.HttpContext.Current.Session["userID"];
            cmd.Parameters.AddWithValue("@UserID", userID);
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

        //AddProduct view
        public ActionResult AddProduct()
        {

            return View();

        }
        //post request connects to SQL server database and trys to add new product
        [HttpPost]
        public ActionResult AddProduct(Products products)
        {
            //create connection
            SqlConnection con = new SqlConnection("Data Source = ISMAIL-PC; Initial Catalog = 'FarmCentral'; Integrated Security = True");
            string query;
            if (products.ProductType != null)
            {
                //ProductType can be null, change query based on this
                query = "INSERT INTO [dbo].[Products](ProductName,ProductType,ProductQuantity,ProductPrice,UserID,ProductDate) VALUES (@ProductName,@ProductType,@ProductQuantity,@ProductPrice,@UserID,@ProductDate)";
            }
            else
            {
                query = "INSERT INTO [dbo].[Products](ProductName,ProductQuantity,ProductPrice,UserID,ProductDate) VALUES (@ProductName,@ProductQuantity,@ProductPrice,@UserID,@ProductDate)";

            }
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {  
                //add Parameters
                var dateAndTime = DateTime.Now;
                var date = dateAndTime.Date;
                int userID = (int)System.Web.HttpContext.Current.Session["userID"];
                cmd.Parameters.AddWithValue("@ProductName", products.ProductName);
                cmd.Parameters.AddWithValue("@ProductQuantity", products.ProductQuantity);
                cmd.Parameters.AddWithValue("@ProductPrice", products.ProductPrice);
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@ProductDate", date);
                if (products.ProductType != null)
                {
                    cmd.Parameters.AddWithValue("@ProductType", products.ProductType);
                }
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch
            {
                ViewBag.Message = "ERROR!!!!: Failed to add product.";
            }
            ViewBag.Message = "Product added.";
            return View();
        }
    }
}