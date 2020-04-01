using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace ecare.Models
{
    public class Doctor
    {
        [Display(Name = "DoctorId")]
        public string RoleName { get; set; }
        public int DoctorId { get; set; }
        public int EmployeeCode { get; set; }
        public string DoctorName { get; set; }
        public int HospitalId { get; set; }
        public string DoctorSpecialization { get; set; }
        public string DoctorDegree { get; set; }
        public string DoctorPhone { get; set; }
        public string DoctorEmail { get; set; }
        public string DoctorCity { get; set; }
        public string DoctorState { get; set; }

        public string DoctorCountry { get; set; }
        public int IsActive { get; set; }
        public string EntryDate { get; set; }
        public string EntryBy { get; set; }

        public int RoleId { get; set; }
        public string DoctorAddress { get; set; }




    }
}