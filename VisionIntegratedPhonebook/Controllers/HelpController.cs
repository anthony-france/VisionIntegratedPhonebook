using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VisionIntegratedPhonebook.Controllers
{
    public class HelpController : Controller
    {
        //
        // GET: /Help/
        public ActionResult Index()
        {
            ViewBag.PageClass = "help";
            return View();
        }
	}
}