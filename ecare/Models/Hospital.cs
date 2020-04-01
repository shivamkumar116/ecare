using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ecare.Models
{
    public class Hospital
    {
        public int HospitalId { get; set; }
        public string HospitalName { get; set; }
        public string HospitalAddress { get; set; }
        public string HospitalCity { get; set; }
        public string HospitalState { get; set; }
        public string HospitalCountry { get; set; }
        public string HospitalPhone { get; set; }
        public string HospitalEmail { get; set; }
        public HttpPostedFileBase HospitalLogo { get; set; }
        public bool IsActive { get; set; }
        public string EntryDateTime { get; set; }
        public string EntryBy { get; set; }

    }
}