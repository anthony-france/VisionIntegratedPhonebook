using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisionIntegratedPhonebook.Models
{
    public class ViewModelHomeIndex
    {
        public IEnumerable<Contact> people { get; set; }
        public IEnumerable<Contact> facilities { get; set; }
    }
}