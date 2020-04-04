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
    public class ReceptionMasterController : Controller
    {
        // GET: ReceptionMaster
        private static readonly string cs = WebConfigurationManager.ConnectionStrings["dbECare"].ConnectionString;

        public ActionResult Index()
        {
            return View(GetReceptions());
        }

        public ActionResult Create()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Create(Reception objReception)
        {
            SqlConnection con = new SqlConnection(cs);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("pInsertReceptionMaster", con)
                {
                    CommandType = CommandType.StoredProcedure
                };


                cmd.Parameters.AddWithValue("@Name", objReception.Name);
                cmd.Parameters.AddWithValue("@Phone", objReception.Phone);
                cmd.Parameters.AddWithValue("@AppointmentType", objReception.AppointmentType);
                cmd.Parameters.AddWithValue("@Gender", objReception.Gender);
                cmd.Parameters.AddWithValue("@Age", objReception.Age);
                cmd.Parameters.AddWithValue("@Problem", objReception.Problem);
                cmd.Parameters.AddWithValue("@Address", objReception.Address);
                cmd.Parameters.AddWithValue("@Payment", objReception.Payment);
                if (objReception.Slot == null) {
                    cmd.Parameters.AddWithValue("@Slot",DateTime.Parse("00:00:00"));
                }
                else
                    cmd.Parameters.AddWithValue("@Slot", objReception.Slot);
                cmd.Parameters.AddWithValue("@EntryBy", objReception.EntryBy);
                cmd.Parameters.AddWithValue("@EntryDateTime", DateTime.Now);

         
   



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

                if (DeleteReception(id))
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
            return View(GetReceptions().Find(model => model.AppointmentId == id));
        }
        [HttpPost]
        public ActionResult Edit(int id, Reception objReception)

        {
            SqlConnection con = new SqlConnection(cs);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("pUpdateReceptionMaster", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@AppointmentId", objReception.AppointmentId);
                cmd.Parameters.AddWithValue("@Name", objReception.Name);
                cmd.Parameters.AddWithValue("@Phone", objReception.Phone);
                cmd.Parameters.AddWithValue("@AppointmentType", objReception.AppointmentType);
                cmd.Parameters.AddWithValue("@Gender", objReception.Gender);
                cmd.Parameters.AddWithValue("@Age", objReception.Age);
                cmd.Parameters.AddWithValue("@Problem", objReception.Problem);
                cmd.Parameters.AddWithValue("@Address", objReception.Address);
                cmd.Parameters.AddWithValue("@Payment", objReception.Payment);
                cmd.Parameters.AddWithValue("@Slot", objReception.Slot);
                cmd.Parameters.AddWithValue("@EntryBy", objReception.EntryBy);
                cmd.Parameters.AddWithValue("@EntryDateTime", DateTime.Now);

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
        public static List<Reception> GetReceptions()
        {
            List<Reception> ReceptionList = new List<Reception>();
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("pDetailsReceptionMaster", con)
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
                ReceptionList.Add(
                    new Reception
                    {
                        AppointmentId = Convert.ToInt32(dr["AppointmentId"]),
                        Name = Convert.ToString(dr["Name"]),
                        Phone = Convert.ToString(dr["Phone"]),
                        AppointmentType = Convert.ToString(dr["AppointmentType"]),
                        Gender = Convert.ToString(dr["Gender"]),
                        Age = Convert.ToInt32(dr["Age"]),

                        Problem = Convert.ToString(dr["Problem"]),
                        Address = Convert.ToString(dr["Address"]),
                        Payment = Convert.ToInt32(dr["Payment"]),
                        Slot = Convert.ToString(dr["Slot"]),
                        EntryDateTime = Convert.ToString(dr["EntryDateTime"]),
                        EntryBy = Convert.ToString(dr["EntryBy"])
                    });
            }
            return ReceptionList;
        }
        public static bool DeleteReception(int id)
        {
            SqlConnection con = new SqlConnection(cs);

            SqlCommand cmd = new SqlCommand("pDeleteAppointmentMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@AppointmentId", id);
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