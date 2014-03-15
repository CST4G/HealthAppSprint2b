﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace healthApp.Models
{
    public class Client
    {

        //Autogenerated ID for each client
        public int ClientID { get; set; }

        //CLients first name, limited to length 12
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "First Name")]
        [StringLength(12, ErrorMessage = "Name limit is 12 characters")]
        public string ClientFirstName { get; set; }

        //Clients last name limited to length 12
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Last Name")]
        [StringLength(12, ErrorMessage = "Name limit is 12 characters")]
        public string ClientLastName { get; set; }

        //Marital status: Single or Married
        [Display(Name = "Marital Status")]
        public string ClientMarital { get; set; }

        //Date of birth: dd/mm/yyyy
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime ClientDOB { get; set; }

        //Clients health number: ranging from 0 to 999999999
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Health Number")]
        [Range(0, 999999999, ErrorMessage = "Must be between 0 and 999999999")]
        public int ClientHealthNum { get; set; }

        //Gender: Male or Female
        [Display(Name = "Gender")]
        public string ClientGender { get; set; }

        //Clients bed number: ranging from 1 to 1000
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Room Number")]
        [Range(1, 1000, ErrorMessage = "Must be between 1 and 1000")]
        public int RoomNumber { get; set; }

        //Name of clients family doctor
        [Display(Name = "Family Doctor")]
        public string ClientFamilyDoc { get; set; }

        [Display(Name = "Picture")]
        public byte[] ClientPicture { get; set; }

        //CLients Type
        [Display(Name = "Client Type")]
        public string ClientType { get; set; }

        // Suite number
        [Display(Name = "Suite Number")]
        public string ClientSuiteNumber { get; set; }
        
        // Phone num of the suite
        [Display(Name = "Suite Phone Num")]
        public string ClientSuitePhone { get; set; }

        // Personal Agents
        [Display(Name = "Personal Agent")]
        public string ClientPersonalAgent { get; set; }

        // Safety Concern
        [Display(Name = "Safety Concern")]
        public string ClientSafetyConcern { get; set; }

        // Risk Level
        [Display(Name = "Risk Level")]
        public string ClientRiskLevel { get; set; }

        // Indoor Mobility
        [Display(Name = "Indoor Mobility")]
        public string ClientIndoorMobility { get; set; }

        // Outdoor Mobility
        [Display(Name = "Outdoor Mobility")]
        public string ClientOutdoorMobility { get; set; }


        // Preferred Language
        [Display(Name = "Preferred Language")]
        public string ClientPreferredLanguage { get; set; }

        // Interpreter Required
        [Display(Name = "Interpreter Required")]
        public string ClientInterpreterRequired { get; set; }

        // Hearing
        [Display(Name = "Hearing")]
        public string ClientHearing { get; set; }

        // Does Understand
        [Display(Name = "Does Understand")]
        public string ClientDoesUnderstand { get; set; }

        // Vision
        [Display(Name = "Vision")]
        public string ClientVision { get; set; }

        // Current Medication
        [Display(Name = "Current Medication")]
        public string ClientCurrentMedication { get; set; }

        // Food
        [Display(Name = "Food")]
        public string ClientFood { get; set; }

        // Family Doctor
        [Display(Name = "Family Doctor")]
        public string ClientFamilyDoctor { get; set; }


        // Medication
        [Display(Name = "Medication")]
        public string ClientMedication { get; set; }

        // Pharmacist
        [Display(Name = "Pharmacist")]
        public string ClientPharmacist { get; set; }

        // Special Diet
        [Display(Name = "Special Diet")]
        public string ClientSpecialDiet { get; set; }

        // Other 
        [Display(Name = "Other")]
        public string ClientOther { get; set; }


        // Client Diagnosis 
        [Display(Name = "Client Diagnosis" )]
        public string ClientDiagnosis { get; set; }


        public Client()
        {
            /*Image img = new Bitmap("../Images/PhotoPlaceHolder.jpg");
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
            ClientPicture = (byte[])converter.ConvertTo(img, typeof(byte[])); */
        }

    }



    public class ClientDBContext : DbContext
    {
        public ClientDBContext() : base("DefaultConnection") { }
        public DbSet<Client> Client { get; set; }
    }
}