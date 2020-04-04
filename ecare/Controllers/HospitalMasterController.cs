using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using ecare.Models;

namespace ecare.Controllers
{
    public class HospitalMasterController : Controller
    {
        // GET: HospitalMaster
        private static readonly string cs = WebConfigurationManager.ConnectionStrings["dbECare"].ConnectionString;

        public ActionResult Index()
        {
            return View(GetHospitals());
        }

        public ActionResult Create()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Create(Hospital objHospital)
        {
            SqlConnection con = new SqlConnection(cs);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("pInsertHospitalMaster", con)
                {
                    CommandType = CommandType.StoredProcedure
                };


                cmd.Parameters.AddWithValue("@HospitalName", objHospital.HospitalName);
                cmd.Parameters.AddWithValue("@HospitalAddress", objHospital.HospitalAddress);
                cmd.Parameters.AddWithValue("@HospitalCity", objHospital.HospitalCity);
                cmd.Parameters.AddWithValue("@HospitalState", objHospital.HospitalState);
                cmd.Parameters.AddWithValue("@HospitalCountry", objHospital.HospitalCountry);
                cmd.Parameters.AddWithValue("@HospitalPhone", objHospital.HospitalPhone);
                cmd.Parameters.AddWithValue("@HospitalEmail", objHospital.HospitalEmail);

                if (objHospital.HospitalLogo != null && objHospital.HospitalLogo.ContentLength > 0)
                {

                    string FileName = Path.GetFileName(objHospital.HospitalLogo.FileName);
                    FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName;
                    string UploadPath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["HospitalLogoPath"].ToString()), FileName);
                    objHospital.HospitalLogo.SaveAs(UploadPath);
                    cmd.Parameters.AddWithValue("@HospitalLogo", (DateTime.Now.ToString("yyyyMMdd") + "-" + objHospital.HospitalLogo.FileName));
                }
                cmd.Parameters.AddWithValue("@IsActive", objHospital.IsActive);
                cmd.Parameters.AddWithValue("@EntryDateTime", DateTime.Now);
                cmd.Parameters.AddWithValue("@EntryBy", objHospital.EntryBy);

                int i = cmd.ExecuteNonQuery();
                if (i >= 1)
                    ViewBag.SuccessMessage = "success";
                else
                    ViewBag.ErrorMessage = "Failed";


            }
            catch (SqlException ex)
            { }
            finally
            {
                con.Close();
            }
            ModelState.Clear();
            return View();
        }


    
     
        public ActionResult Delete(int id)
        {

            try
            {

                if (DeleteHospital(id))
                {
                    ViewBag.SuccessMessage = "Success";
                 
                }
                else
                {
                    ViewBag.ErrorMessage = "error";
                   
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
           
        }

        public ActionResult Edit(int id)
        {
            return View(GetHospitals().Find(model=>model.HospitalId==id));
        }
        [HttpPost]
        public ActionResult Edit(int id,Hospital objHospital)

        {
            SqlConnection con = new SqlConnection(cs);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("pUPdateHospitalMaster", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@HospitalId", objHospital.HospitalId);
                cmd.Parameters.AddWithValue("@HospitalName", objHospital.HospitalName);
                cmd.Parameters.AddWithValue("@HospitalAddress", objHospital.HospitalAddress);
                cmd.Parameters.AddWithValue("@HospitalCity", objHospital.HospitalCity);
                cmd.Parameters.AddWithValue("@HospitalState", objHospital.HospitalState);
                cmd.Parameters.AddWithValue("@HospitalCountry", objHospital.HospitalCountry);
                cmd.Parameters.AddWithValue("@HospitalPhone", objHospital.HospitalPhone);
                cmd.Parameters.AddWithValue("@HospitalEmail", objHospital.HospitalEmail);

            
                cmd.Parameters.AddWithValue("@IsActive", objHospital.IsActive);
                cmd.Parameters.AddWithValue("@EntryDateTime", DateTime.Now);
                cmd.Parameters.AddWithValue("@EntryBy", objHospital.EntryBy);

                int i = cmd.ExecuteNonQuery();
                if (i >= 1)
                    ViewBag.SuccessMessage = "success";
                else
                    ViewBag.ErrorMessage = "Failed";
            }
            catch (SqlException ex)
            { }
            finally
            {
                con.Close();
            }
            ModelState.Clear();


            return RedirectToAction("Index");
        }


        [HttpGet]
        public static List<Hospital> GetHospitals()
        {
            List<Hospital> HospitalList = new List<Hospital>();
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("pHospitalMasterDetails", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                HospitalList.Add(
                    new Hospital
                    {
                        HospitalId = Convert.ToInt32(dr["HospitalId"]),
                        HospitalName = Convert.ToString(dr["HospitalName"]),
                        HospitalAddress = Convert.ToString(dr["HospitalAddress"]),
                        HospitalCity = Convert.ToString(dr["HospitalCity"]),
                        HospitalState = Convert.ToString(dr["HospitalState"]),
                        HospitalCountry = Convert.ToString(dr["HospitalCountry"]),
                        HospitalPhone = Convert.ToString(dr["HospitalPhone"]),
                        HospitalEmail = Convert.ToString(dr["HospitalEmail"]),
                        IsActive = Convert.ToBoolean(dr["IsActive"]),
                        EntryDateTime = Convert.ToString(dr["EntryDateTime"]),
                        EntryBy = Convert.ToString(dr["EntryBy"])
                    });
            }
            return HospitalList;
        }
        public static bool DeleteHospital(int id)
        {
            SqlConnection con = new SqlConnection(cs);

            SqlCommand cmd = new SqlCommand("pDeleteHospitalMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@HospitalId", id);
            try
            {
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();

                if (i >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
            }
            finally
            {
                con.Close();
            }
            return false;
        }


    }
}