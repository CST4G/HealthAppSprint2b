using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace healthApp.Models
{
    public class Schedule
    {
        public int ID { get; set; }
        public int taskID { get; set; }
        [Display(Name = "Patient ID")]
        public string PatientID { get; set; }
        [Display(Name = "Room Number")]
        public string RoomNo { get; set; }
        public string Task { get; set; }
        public DateTime tDate { get; set; }

        [Display(Name = "Duration")]
        //[Range(1, int.MaxValue, ErrorMessage = "Duration must be greater than {1} units of time")]
        public int duration { get; set; }
        [Display(Name = "Actual Duration")]
        //[Range(1, int.MaxValue, ErrorMessage = "Actual Duration must be greater than {1} units of time")]
        public int actual { get; set; }
        public string comments { get; set; }
        
        [Display(Name = "Completion")]
        public bool isComplete { get; set; }
        
    }

}