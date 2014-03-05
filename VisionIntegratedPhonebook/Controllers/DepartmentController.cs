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
    public class DepartmentController : BaseController
    {
        //
        // GET: /Facility/
        public ActionResult Index()
        {
            ViewModelDepartmentIndex view = new ViewModelDepartmentIndex();
            LDAPSearchObject search = new LDAPSearchObject();
            search.AND.Add("company", "Vision Integrated Graphics");
            search.AND.Add("department", "*");

            List<Contact> people = findContacts(search);
            IEnumerable<string> departments = people.Select(x => x.Department).Distinct();

            Dictionary<string, int> viewObj = new Dictionary<string, int>();
            foreach (var department in departments)
            {
                 viewObj.Add(department, people.Count(x => x.Department == department));
            }

            view.departments = viewObj;

            return View(view);
        }


        [AllowAnonymous]
        [OutputCache(Duration = 1, VaryByParam = "*")]
        public ActionResult Details(string department, string facility)
        {
            if (string.IsNullOrEmpty(department))
            {
                return RedirectToAction("Index");
            }

            ViewModelDepartmentDetails view = new ViewModelDepartmentDetails();

            LDAPSearchObject search = new LDAPSearchObject();
            search.AND.Add("company", "Vision Integrated Graphics");
            search.AND.Add("department", department + "*");

            if (!string.IsNullOrEmpty(facility))
            {
                search.AND.Add("physicalDeliveryOfficeName", facility);
            }
            else
            {
                search.AND.Add("physicalDeliveryOfficeName", "*");
            }

            search.AND.Add("objectClass", "user");

            List<Contact> people = findContacts(search);
            people = people.OrderBy(o => o.DisplayName).ToList();


            view.Department = department;
            view.people = people;

            return View(view);
        }
	}
}