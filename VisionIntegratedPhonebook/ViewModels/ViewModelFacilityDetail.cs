using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisionIntegratedPhonebook.Models;

namespace VisionIntegratedPhonebook.Models
{
    public class ViewModelFacilityDetail
    {
        public Contact facility { get; set; }
        public IEnumerable<Contact> people { get; set; }
        public IEnumerable<Contact> contacts { get; set; }
    }
}
