using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using VisionIntegratedPhonebook.Models;

namespace VisionIntegratedPhonebook.Controllers
{
    public class HomeController : BaseController
    {
        [AllowAnonymous]
        [OutputCache(Duration = 1, VaryByParam = "*")]
        public ActionResult Index()
        {
            ViewModelHomeIndex view = new ViewModelHomeIndex();

            LDAPSearchObject search = new LDAPSearchObject();
            search.AND.Add("company", "Vision Integrated Graphics");
            search.AND.Add("department", "*");
            search.AND.Add("objectClass", "user");
            

            List<Contact> people = findContacts(search);
            view.people = people.OrderBy(o => o.DisplayName).ToList();

            search.Clear();
            search.AND.Add("company", "Vision Integrated Properties");
            search.AND.Add("objectClass", "contact");

            view.facilities =  findContacts(search);

            

            ViewBag.PageClass = "index";
            return View(view);
        }

    }
}