using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace VisionIntegratedPhonebook.Models
{
    public class Contact
    {
        [Required(ErrorMessage = "Please enter a First Name.")]
        [Display(Name="First Name")]
        public string GivenName { get; set; }

        [Required(ErrorMessage = "Please enter a Last Name.")]
        [Display(Name = "Last Name")]
        public string SurName { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a Display Name.")]
        [Display(Name = "Name")]
        public string DisplayName { get; set; }

        [Display(Name = "Phone")]
        public string TelephoneNumber { get; set; }

        [Display(Name = "Fax")]
        public string FaxNumber { get; set; }

        [Display(Name = "Mobile")]
        public string MobileNumber { get; set; }

        [Display(Name="E-Mail")]
        public string EMail { get; set; }

        [ReadOnly(true)]
        [Editable(false)]
        [Display(Name = "DN")]
        public string DistinguishedName { get; set; }

        [Display(Name = "Department")]
        public string Department { get; set; }

        [Display(Name = "Office")]
        public string Office { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "UPN")]
        public string UserPrincipalName { get; set; }

        [Display(Name = "Image")]
        public byte[] Thumbnail { get; set; }

        [Display(Name = "Manager")]
        public Contact Manager { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "Zip")]
        public string PostalCode { get; set; }

        [Display(Name = "Url")]
        public string Url { get; set; }
                
        [Display(Name = "Direct Reports")]
        public List<Contact> DirectReports { get; set; }


    }
}