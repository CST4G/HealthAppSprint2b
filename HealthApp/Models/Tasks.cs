using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace healthApp.Models
{
    public class Tasks
    {
        public int ID { get; set; }

        //datestamp of the creation of the event, need not be set explicitly
        public DateTime created { get; set; }

        [Display(Name = "Patient ID")]
        [Required]
        public string PatientID { get; set; }

        [Display(Name = "Room Number")]
        public string RoomNo { get; set; }

        public string Task { get; set; } // task description

        [Display(Name = "Duration")]
        [Range(1, int.MaxValue, ErrorMessage = "Duration must be greater than {1} units of time")]
        public int duration { get; set; } // e.g. e hrs

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)] //to show a calendar to pick a date
        public DateTime dtStart { get; set; }

        [Display(Name = "Until")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //[DateGreaterThan("dtStart", "Cannot be earlier than Start Date")]
        public DateTime? dtEnd { get; set; }

        [Required]
        [Display(Name = "Frequency")]
        [ValidFrequency(ErrorMessage = "is not a valid frequency")]
        public string freq { get; set; } //daily, monthly...
        public int? interval { get; set; } // every 2nd/3rd month/day
        public int? count { get; set; }  //how many times, instead of end date
        public string byDay { get; set; }  //day of weeek for weekly freq
        public int? byMonthDay { get; set; } //day of month for monthly freq

        public int dtStartWD { get; set; } // week day of the start date, needed for weekly calculations

        //ctor to set default values
        public Tasks()
        {
            //set the the created time to current time
            created = DateTime.Now;
            //default repeat interval of 1 
            //ie. every day, every week, every month
            interval = 1;
            //start date day of week
            dtStartWD = (int)dtStart.DayOfWeek;
        }
    }

    public class TaskDBContext : DbContext
    {
        public TaskDBContext() : base("DefaultConnection") {}
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Schedule> Sched { get; set; }
    }
}
