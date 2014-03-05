using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace VisionIntegratedPhonebook.Models
{
    public class Person
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

        [DisplayFormat(DataFormatString = "{0:###-###-####}", ApplyFormatInEditMode = true)]
        [Display(Name = "Phone")]
        public string TelephoneNumber { get; set; }


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

        [Display(Name = "UPN")]
        public string UserPrincipalName { get; set; }

        [Display(Name = "Manager")]
        public Person Manager { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Image")]
        public byte[] Thumbnail { get; set; }

        [Display(Name = "Direct Reports")]
        public List<Person> DirectReports { get; set; }
    }
}