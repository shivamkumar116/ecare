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

        string cs = WebConfigurationManager.ConnectionStrings["dbECare"].ConnectionString;

        public ActionResult Index()
        {


            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login objLogin)
        {
            SqlConnection con = new SqlConnection(cs);
            try
            {

                con.Open();
                SqlCommand cmd = new SqlCommand("pValidateUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Email", objLogin.Email);
                cmd.Parameters.AddWithValue("@Password", objLogin.Password);
                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Session["Email"] = dt.Rows[0]["Email"].ToString();

                    RedirectToAction("Index");
                }

                else
                {

                    RedirectToAction("Login");

                }
            }
            catch (SqlException ex)
            {
            }
            finally
            {
                con.Close();
            }

            return View(objLogin);
        }
    }
}
