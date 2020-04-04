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
    public class DoctorMasterController : Controller
    {
        // GET: DoctorMaster
        private static readonly string cs = WebConfigurationManager.ConnectionStrings["dbECare"].ConnectionString;

        public ActionResult Index()
        {
            return View(GetDoctors());
        }

        public ActionResult Create()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Create(Doctor objDoctor)
        {
            SqlConnection con = new SqlConnection(cs);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("pInsertDoctorMaster", con)
                {
                    CommandType = CommandType.StoredProcedure
                };


                cmd.Parameters.AddWithValue("@EmployeeCode", objDoctor.EmployeeCode);

                if (objDoctor.DoctorPhoto != null && objDoctor.DoctorPhoto.ContentLength > 0)
                {

                    string FileName = Path.GetFileName(objDoctor.DoctorPhoto.FileName);
                    FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName;
                    string UploadPath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["HospitalLogoPath"].ToString()), FileName);
                    objDoctor.DoctorPhoto.SaveAs(UploadPath);
                    cmd.Parameters.AddWithValue("@DoctorPhoto", (DateTime.Now.ToString("yyyyMMdd") + "-" + objDoctor.DoctorPhoto.FileName));
                }
                cmd.Parameters.AddWithValue("@DoctorName", objDoctor.DoctorName);
                cmd.Parameters.AddWithValue("@HospitalId", objDoctor.HospitalId);
                cmd.Parameters.AddWithValue("@DoctorSpecialization", objDoctor.DoctorSpecialization);
                cmd.Parameters.AddWithValue("@DoctorDegree", objDoctor.DoctorDegree);
                cmd.Parameters.AddWithValue("@DoctorPhone", objDoctor.DoctorPhone);
                cmd.Parameters.AddWithValue("@DoctorEmail", objDoctor.DoctorEmail);
                cmd.Parameters.AddWithValue("@DoctorCity", objDoctor.DoctorCity);
                cmd.Parameters.AddWithValue("@DoctorState", objDoctor.DoctorState);
                cmd.Parameters.AddWithValue("@DoctorCountry", objDoctor.DoctorCountry);
                cmd.Parameters.AddWithValue("@IsActive", objDoctor.IsActive);
                cmd.Parameters.AddWithValue("@EntryDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@EntryBy", objDoctor.EntryBy);
                cmd.Parameters.AddWithValue("@DoctorAddress", objDoctor.DoctorAddress);

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

                if (DeleteDoctor(id))
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
            return View(GetDoctors().Find(model => model.DoctorId == id));
        }
        [HttpPost]
        public ActionResult Edit(int id, Doctor objDoctor)

        {
            SqlConnection con = new SqlConnection(cs);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("pUpdateDoctorMaster", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@DoctorId", objDoctor.DoctorId);
                cmd.Parameters.AddWithValue("@EmployeeCode", objDoctor.EmployeeCode);
                cmd.Parameters.AddWithValue("@DoctorName", objDoctor.DoctorName);
                cmd.Parameters.AddWithValue("@HospitalId", objDoctor.HospitalId);
                cmd.Parameters.AddWithValue("@DoctorSpecialization", objDoctor.DoctorSpecialization);
                cmd.Parameters.AddWithValue("@DoctorDegree", objDoctor.DoctorDegree);
                cmd.Parameters.AddWithValue("@DoctorPhone", objDoctor.DoctorPhone);
                cmd.Parameters.AddWithValue("@DoctorEmail", objDoctor.DoctorEmail);
                cmd.Parameters.AddWithValue("@DoctorCity", objDoctor.DoctorCity);
                cmd.Parameters.AddWithValue("@DoctorState", objDoctor.DoctorState);
                cmd.Parameters.AddWithValue("@DoctorCountry", objDoctor.DoctorCountry);
                cmd.Parameters.AddWithValue("@IsActive", objDoctor.IsActive);
             
                cmd.Parameters.AddWithValue("@EntryBy", objDoctor.EntryBy);
                cmd.Parameters.AddWithValue("@DoctorAddress", objDoctor.DoctorAddress);
              

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
        public static List<Doctor> GetDoctors()
        {
            List<Doctor> DoctorList = new List<Doctor>();
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("pDoctorMasterDetails", con)
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
                DoctorList.Add(
                    new Doctor
                    {
                        DoctorId = Convert.ToInt32(dr["DoctorId"]),
                        EmployeeCode = Convert.ToInt32 (dr["EmployeeCode"]),
                        DoctorName = Convert.ToString(dr["DoctorName"]),

                        HospitalId = Convert.ToInt32(dr["HospitalId"]),
                        DoctorSpecialization = Convert.ToString(dr["DoctorSpecialization"]),
                        DoctorDegree = Convert.ToString (dr["DoctorDegree"]),
                        DoctorPhone = Convert.ToString(dr["DoctorPhone"]),
                        DoctorEmail = Convert.ToString(dr["DoctorEmail"]),

                        DoctorCity = Convert.ToString(dr["DoctorCity"]),
                        DoctorState = Convert.ToString(dr["DoctorState"]),
                        DoctorCountry = Convert.ToString(dr["DoctorCountry"]),
                        IsActive = Convert.ToBoolean (dr["IsActive"]),                     
                        EntryBy = Convert.ToString(dr["EntryBy"]),
                        DoctorAddress = Convert.ToString(dr["DoctorAddress"]),

                    });
            }
            return DoctorList;
        }
        public static bool DeleteDoctor(int id)
        {
            SqlConnection con = new SqlConnection(cs);

            SqlCommand cmd = new SqlCommand("pDeleteDoctorMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@DoctorId", id);
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