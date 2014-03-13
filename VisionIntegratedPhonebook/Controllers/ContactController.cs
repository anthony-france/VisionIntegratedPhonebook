using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using VisionIntegratedPhonebook.Models;

namespace VisionIntegratedPhonebook.Controllers
{
    public class ContactController : BaseController
    {
        [AllowAnonymous]
        [OutputCache(Duration = 300, VaryByParam = "*")]
        public ActionResult Image(string dn)
        {
            Contact p = new Contact();  
            MemoryStream imageStream = new MemoryStream();

            Response.Clear();
            Response.Expires = 0;
            Response.BufferOutput = false;

            try
            {
                LDAPSearchObject search = new LDAPSearchObject();
                search.AND.Add("objectClass", "user");
                search.AND.Add("DistinguishedName", dn);
                search.LoadAttributes.Add("thumbnailPhoto");

                p = findContact(search);

                imageStream.Write(p.Thumbnail, 0, p.Thumbnail.Length);
                Response.AddHeader("Content-Length", p.Thumbnail.Length.ToString());
            } 
            catch
            {
                using (FileStream fileStream = System.IO.File.OpenRead(Server.MapPath(@"\Content\Images\nophoto.png")))
                {
                    imageStream.SetLength(fileStream.Length);
                    fileStream.Read(imageStream.GetBuffer(), 0, (int)fileStream.Length);
                }
            }

            imageStream.Position = 0;
            return new FileStreamResult(imageStream, "image/jpeg"); 
        }
        //
        // GET: /Person/
        [AllowAnonymous]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Person/Details/5
        [AllowAnonymous]
        [OutputCache(Duration = 300, VaryByParam = "*")]
        public ActionResult Details(string dn)
        {
            if (String.IsNullOrEmpty(dn))
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.PageClass = "details";
            ViewModelContactDetail view = new ViewModelContactDetail();

            LDAPSearchObject search = new LDAPSearchObject();
            search.AND.Add("DistinguishedName", dn);

            view.me = findContact(search);
            view.Manager = view.me.Manager;

            search.Clear();
            search.AND.Add("company", "Vision Integrated Graphics");
            search.AND.Add("manager", view.me.DistinguishedName);
            search.AND.Add("objectClass", "user");

            view.DirectReports = findContacts(search).OrderBy(c=>c.Department).ThenBy(c=>c.Office).ThenBy(c=>c.DisplayName);

            return View(view);
        }

        //
        // GET: /Person/Details/5
        [AllowAnonymous]
        [OutputCache(Duration = 300, VaryByParam = "*")]
        public ActionResult vCard(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return RedirectToAction("Index", "Home");
            }

            Response.Clear();
            Response.Expires = 0;
            Response.BufferOutput = false;

            ViewModelContactvCard view = new ViewModelContactvCard();

            LDAPSearchObject search = new LDAPSearchObject();
            search.AND.Add("company", "Vision Integrated Graphics");
            search.AND.Add("DistinguishedName", name);
            search.AND.Add("objectClass", "user");

            view.me = findContact(search);

            string output = @"BEGIN:VCARD
FN:{0}
TITLE:{1}
ORG:{2};{3};{4}
TEL;Work;VOICE;MESG;PREF:{5}
TEL;Cell:{6}
EMAIL;Internet:{7}
TZ:-0600
REV:20090401T065518
VERSION:2.1
END:VCARD";

            output = string.Format(
                output, 
                view.me.DisplayName, 
                view.me.Title, 
                view.me.Department, 
                view.me.Department, 
                view.me.Office, 
                view.me.TelephoneNumber, 
                view.me.MobileNumber, 
                view.me.EMail
                );
            MemoryStream s = new MemoryStream();
            StreamWriter w = new StreamWriter(s);

            byte[] bytes = new byte[output.Length * sizeof(char)];
            System.Buffer.BlockCopy(output.ToCharArray(), 0, bytes, 0, bytes.Length);

            s.Write(bytes,0,bytes.Count());
            s.Position = 0; 

            return new FileStreamResult(s, "text/vcard");
        }

        //
        // GET: /Person/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Person/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Person/Edit/5
        public ActionResult Edit(string name)
        {
            ViewBag.PageClass = "person-edit";
            if (String.IsNullOrEmpty(name))
            {
                return RedirectToAction("Index", "Home");
            }

            LDAPSearchObject search = new LDAPSearchObject();
            search.AND.Add("userPrincipalName", name);

            Contact p = findContact(search);
            return View(p);
        }

        //
        // POST: /Person/Edit/5
        [HttpPost]
        public ActionResult Edit(string name, FormCollection collection)
        {
            IIdentity WinId = System.Web.HttpContext.Current.User.Identity;
            WindowsIdentity wi = (WindowsIdentity)WinId;

            WindowsImpersonationContext wic = wi.Impersonate();
            try
            {
                saveProperties(collection);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                wic.Undo();
            }
            
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Person/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Person/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
