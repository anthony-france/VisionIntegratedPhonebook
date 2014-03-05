using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using VisionIntegratedPhonebook.Models;

namespace VisionIntegratedPhonebook.Controllers
{
    public class FacilityController : BaseController
    {
        //
        // GET: /Facility/
        public ActionResult Index()
        {
            ViewModelFacilityIndex view = new ViewModelFacilityIndex();

            LDAPSearchObject search = new LDAPSearchObject();
            search.AND.Add("company", "Vision Integrated Properties");
            search.AND.Add("objectClass", "contact");

            view.facilities = findContacts(search);
            return View(view);
        }


        [AllowAnonymous]
        [OutputCache(Duration = 1, VaryByParam = "*")]
        public ActionResult Details(string location, string department)
        {
            LDAPSearchObject search = new LDAPSearchObject();
            search.AND.Add("company", "Vision Integrated Graphics");
            search.AND.Add("physicalDeliveryOfficeName", location +"*");

            if (!string.IsNullOrEmpty(department))
            {
                search.AND.Add("department", department);
            }
            else
            {
                search.AND.Add("department", "*");
            }

            search.AND.Add("objectClass", "user");

            List<Contact> people = findContacts(search);

            search.Clear();
            search.AND.Add("physicalDeliveryOfficeName", location + "*");
            search.AND.Add("objectClass", "contact");

            List<Contact> contacts = findContacts(search);

            ViewModelFacilityDetail view = new ViewModelFacilityDetail();

            view.people = people.OrderBy(o => o.DisplayName).ToList();
            view.contacts = contacts.OrderBy(o => o.DisplayName).ToList();

            search.Clear();

            search.AND.Add("company", "Vision Integrated Properties");
            search.AND.Add("title", "building");
            search.AND.Add("l", location + "*");
            search.AND.Add("objectClass", "contact");

            view.facility = findContact(search);
            return View(view);
        }
	}
}