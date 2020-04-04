using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ecare.Models
{
    public class Staff
    {
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public HttpPostedFileBase StaffPhoto { get; set; }

        public int HospitalId { get; set; }
        public int EmployeeCode { get; set; }
        public string StaffSpecialization { get; set; }
        public string StaffDegree { get; set; }
        public string Designation { get; set; }
        public string StaffPhone { get; set; }
        public string StaffEmail { get; set; }
        public string StaffCity { get; set; }
        public string StaffState { get; set; }
        public string StaffCountry { get; set; }
        public bool IsActive { get; set; }
        public string EntryDate { get; set; }

        public string EntryBy { get; set; }
        public string RoleName { get; set; }
        public string StaffAddress { get; set; }


    }
}