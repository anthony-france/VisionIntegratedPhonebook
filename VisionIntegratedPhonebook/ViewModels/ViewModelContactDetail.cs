using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VisionIntegratedPhonebook.Models;

namespace VisionIntegratedPhonebook.Models
{
    public class ViewModelContactDetail
    {
        public Contact me { get; set; }
        public IEnumerable<Contact> DirectReports { get; set; }
        public IEnumerable<Contact> Managers { get; set; }
    }
}