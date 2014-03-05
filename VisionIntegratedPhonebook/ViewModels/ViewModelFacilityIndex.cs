using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VisionIntegratedPhonebook.Models;

namespace VisionIntegratedPhonebook.Models
{
    public class ViewModelFacilityIndex
    {
        public IEnumerable<Contact> facilities { get; set; }
    }
}