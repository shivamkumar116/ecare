using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ecare.Models
{
    public class Reception
    {
        [Display(Name = "AppointmentId")]
        public int AppointmentId { get; set; }
        [Required]
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        [Required]
        public string AppointmentType { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Problem { get; set; }
        [Required]
        public int Payment { get; set; }

        public string Slot{ get; set; }

        [Required]
        public string EntryBy { get; set; }
        public string EntryDateTime{ get; set; }
      
  

    }
}