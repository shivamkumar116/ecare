using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ecare.Models
{
    public class AppointmentMasterDbHandle
    {



        private SqlConnection con;

        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["Roleconn"].ToString();
            con = new SqlConnection(constring);
        }

        // **************** ADD NEW Appointment *********************
        public bool AddAppointment(Reception smodel)
        {
            connection();
            SqlCommand cmd = new SqlCommand("pInsertAppointmentMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Name", smodel.Name);
            cmd.Parameters.AddWithValue("@Phone", smodel.Phone);
            cmd.Parameters.AddWithValue("@AppointmentType", smodel.AppointmentType);
            cmd.Parameters.AddWithValue("@Gender", smodel.Gender);
            cmd.Parameters.AddWithValue("@Age", smodel.Age);
            cmd.Parameters.AddWithValue("@Problem", smodel.Problem);
            cmd.Parameters.AddWithValue("@Address", smodel.Address);
            cmd.Parameters.AddWithValue("@Payment", smodel.Payment);
            cmd.Parameters.AddWithValue("@EntryDateTime", DateTime.Now);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        // ********** VIEW Appointment DETAILS ********************
        public List<Reception> GetAppointment()
        {
            connection();
            List<Reception> Appointmentlist = new List<Reception>();

            SqlCommand cmd = new SqlCommand("pDetailsAppointmentMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                Appointmentlist.Add(
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
                        EntryDateTime = Convert.ToString(dr["EntryDateTime"])
                    });
            }
            return Appointmentlist;
        }

        // ***************** UPDATE Appointment DETAILS *********************
        public bool UpdateDetails(Reception smodel)
        {
            connection();
            SqlCommand cmd = new SqlCommand("pUpdateVitalMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@AppointmentId", smodel.AppointmentId);
            cmd.Parameters.AddWithValue("@Name", smodel.Name);
            cmd.Parameters.AddWithValue("@Phone", smodel.Phone);
            cmd.Parameters.AddWithValue("@AppointmentType", smodel.AppointmentType);
            cmd.Parameters.AddWithValue("@Gender", smodel.Gender);
            cmd.Parameters.AddWithValue("@Age", smodel.Age);
            cmd.Parameters.AddWithValue("@Problem", smodel.Problem);
            cmd.Parameters.AddWithValue("@Address", smodel.Address);
            cmd.Parameters.AddWithValue("@Payment", smodel.Payment);
            cmd.Parameters.AddWithValue("@EntryDateTime", Convert.ToDateTime(smodel.EntryDateTime));

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        // ********************** DELETE Appointment DETAILS *******************
        public bool DeleteAppointment(int id)
        {
            connection();
            SqlCommand cmd = new SqlCommand("pDeleteAppointmentMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@AppointmentId", id);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
    }
}
