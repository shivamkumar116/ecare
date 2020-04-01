using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ecare.Models;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;

namespace ecare.Controllers
{
    public class HomeController : Controller
    {


        private readonly string cs = WebConfigurationManager.ConnectionStrings["dbECare"].ConnectionString;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Credentials objUser)
        {
            if (ModelState.IsValid)
            {
                SqlConnection con = new SqlConnection(cs);
                try
                {

                    con.Open();
                    SqlCommand cmd = new SqlCommand("pValidateUser", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.AddWithValue("@Email", objUser.Email);
                    cmd.Parameters.AddWithValue("@Password", objUser.Password);
                    DataTable dt = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        Session["Email"] = dt.Rows[0]["Email"].ToString();
                        Session["Name"] = dt.Rows[0]["Name"].ToString();
                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {

                        return RedirectToAction("Login", "Home");
                    }
                }
                catch (SqlException ex)
                {


                }
                finally
                {
                    con.Close();
                    ModelState.Clear();
                }
            }

            return View();
        }




        public ActionResult Register()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Register(User objUser)
        {

            if (ModelState.IsValid)
            {
                SqlConnection con = new SqlConnection(cs);
                try
                {

                    con.Open();
                    SqlCommand cmd = new SqlCommand("pInsertRegisterMaster", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@Name", objUser.Name);
                    cmd.Parameters.AddWithValue("@Email", objUser.Email);
                    cmd.Parameters.AddWithValue("@Password", objUser.Password);
                    cmd.ExecuteNonQuery();
                    ViewBag.Message = objUser.Name + " is Registered Successfully";
                    ModelState.Clear();
                    return RedirectToAction("Register", "Home");

                }

                catch (SqlException ex)
                {


                }
                finally
                {
                    con.Close();
                }
            }

            return View();

        }


        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPassword objForgotPassword)
        {
            if (ModelState.IsValid)
            {
                SqlConnection con = new SqlConnection(cs);
                try
                {

                    con.Open();
                    SqlCommand cmd = new SqlCommand("Select Password from RegisterMaster where Email=@email", con);
                    cmd.Parameters.AddWithValue("@email", objForgotPassword.Email);
                    DataTable dt = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        String password = dt.Rows[0]["Password"].ToString();
                        ViewBag.SuccessMessage = password;
                        ModelState.Clear();
                          //Code to send the email

                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Null";
                        ModelState.Clear();
                    }


                  
                }
                catch (Exception e) { }
                finally { con.Close(); }
            }
       
            return View();
        }


    }
}
