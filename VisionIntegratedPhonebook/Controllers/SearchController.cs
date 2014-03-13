using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisionIntegratedPhonebook.Models;


namespace VisionIntegratedPhonebook.Controllers
{
    public class SearchController : BaseController
    {
        [AllowAnonymous]
        [OutputCache(Duration = 300, VaryByParam = "*")]
        [HttpPost]
        public ActionResult Index(string q)
        {
            ViewBag.PageClass = "details";
            ViewModelSearchResults view = new ViewModelSearchResults();

            LDAPSearchObject search = new LDAPSearchObject();
            search.AND.Add("company", "Vision Integrated Graphics");
            search.AND.Add("department", "*");
            search.AND.Add("objectClass", "user");
            search.AND.Add("DisplayName", "*" + q + "*");

            view.people = findContacts(search);

            return View(view);
        }

        [AllowAnonymous]
        [OutputCache(Duration = 300, VaryByParam = "*")]
        public ActionResult Index(string q, int? placeholder)
        {
            ViewBag.PageClass = "details";
            ViewModelSearchResults view = new ViewModelSearchResults();

            LDAPSearchObject search = new LDAPSearchObject();
            search.AND.Add("company", "Vision Integrated Graphics");
            search.AND.Add("department", "*");
            search.AND.Add("objectClass", "user");
            search.AND.Add("DisplayName", "*" + q + "*");

            view.people = findContacts(search);

            return PartialView(view);
        }
	}
}