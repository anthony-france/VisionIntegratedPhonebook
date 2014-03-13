using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisionIntegratedPhonebook.Models
{
    public class ViewModelSearchResults
    {
        public IEnumerable<Contact> people { get; set; }
    }
}