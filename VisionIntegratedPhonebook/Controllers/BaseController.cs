using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisionIntegratedPhonebook.Models;
using System.DirectoryServices;
using System.Text;
using System.IO;
using System.Security.Principal;

namespace VisionIntegratedPhonebook.Controllers
{
    public class BaseController : Controller
    {


        public List<Contact> findContacts(LDAPSearchObject search)
        {
            DirectoryEntry entry = new DirectoryEntry(search.root);
            List<Contact> people = new List<Contact>();

            using (DirectorySearcher ds = new DirectorySearcher(entry))
            {
                
                ds.Filter = search.filter();

                foreach (string a in search.LoadAttributes)
                {
                    ds.PropertiesToLoad.Add(a);
                }


                try
                {
                    var results = ds.FindAll();
                    if (results != null && results.Count > 0)
                    {
                        foreach (SearchResult result in results)
                        {
                            Contact p = new Contact();
                            p = processResult(result);
                            people.Add(p);
                        }
                    }
                }
                catch (Exception e)
                {
                    
                }
            }
            return people;
        }

        public Contact findContact(LDAPSearchObject search)
        {
            DirectoryEntry entry = new DirectoryEntry(search.root);
            Contact p = new Contact();

            using (DirectorySearcher ds = new DirectorySearcher(entry))
            {
                ds.Filter = search.filter();

                foreach (string a in search.LoadAttributes)
                {
                    ds.PropertiesToLoad.Add(a);
                }


                var result = ds.FindOne();
                if (result != null)
                {
                    p = processResult(result);
                }
            }
            return p;
        }

        public void saveProperties(FormCollection props)
        {
                System.DirectoryServices.DirectoryEntry user = new System.DirectoryServices.DirectoryEntry("LDAP://" + props["DistinguishedName"]);


                if (string.IsNullOrEmpty(props["GivenName"]))
                {
                    props["GivenName"] = "FirstName";
                }

                if (string.IsNullOrEmpty(props["SurName"]))
                {
                    props["SurName"] = "LastName";
                }

                if (string.IsNullOrEmpty(props["DisplayName"]))
                {
                   props["DisplayName"] = props["GivenName"] + " " + props["SurName"];
                }


                if (!string.IsNullOrEmpty(props["GivenName"]))
                {
                    if (user.Properties.Contains("givenName"))
                    {
                        user.Properties["givenName"][0] = props["GivenName"];
                    }
                    else
                    {
                        user.Properties["givenName"].Add(props["GivenName"]);
                    }
                }
                else
                {
                    user.Properties["givenName"].Clear();
                }

                if (!string.IsNullOrEmpty(props["SurName"]))
                {
                    if (user.Properties.Contains("sn"))
                    {
                        user.Properties["sn"][0] = props["SurName"];
                    }
                    else
                    {
                        user.Properties["sn"].Add(props["SurName"]);
                    }
                }
                else
                {
                    user.Properties["sn"].Clear();
                }

                if (!string.IsNullOrEmpty(props["DisplayName"]))
                {
                    if (user.Properties.Contains("displayName"))
                    {
                        user.Properties["displayName"][0] = props["DisplayName"];
                    }
                    else
                    {
                        user.Properties["displayName"].Add(props["DisplayName"]);
                    }
                }
                else
                {
                    //user.Properties["displayName"].Clear();
                }

                if (!string.IsNullOrEmpty(props["TelephoneNumber"]))
                {
                    if (user.Properties.Contains("telephoneNumber"))
                    {
                        user.Properties["telephoneNumber"][0] = props["TelephoneNumber"];
                    }
                    else
                    {
                        user.Properties["telephoneNumber"].Add(props["TelephoneNumber"]);
                    }
                }
                else
                {
                    user.Properties["telephoneNumber"].Clear();
                }

                if (!string.IsNullOrEmpty(props["Department"]))
                {
                    if (user.Properties.Contains("department"))
                    {
                        user.Properties["department"][0] = props["Department"];
                    }
                    else
                    {
                        user.Properties["department"].Add(props["Department"]);
                    }
                }
                else
                {
                    user.Properties["department"].Clear();
                }

                if (!string.IsNullOrEmpty(props["Office"]))
                {
                    if (user.Properties.Contains("physicalDeliveryOfficeName"))
                    {
                        user.Properties["physicalDeliveryOfficeName"][0] = props["Office"];
                    }
                    else
                    {
                        user.Properties["physicalDeliveryOfficeName"].Add(props["Office"]);
                    }
                }
                else
                {
                    user.Properties["physicalDeliveryOfficeName"].Clear();
                }

                if (!string.IsNullOrEmpty(props["Title"]))
                {
                    if (user.Properties.Contains("title"))
                    {
                        user.Properties["title"][0] = props["Title"];
                    }
                    else
                    {
                        user.Properties["title"].Add(props["Title"]);
                    }
                }
                else
                {
                    user.Properties["title"].Clear();
                }


                // Access resources while impersonating.
                user.CommitChanges();
        }

        public Contact processResult(SearchResult result)
        {
            Contact p = new Contact();

            if (result.Properties.Contains("name"))
            {
                p.Name = result.Properties["name"][0].ToString();
            }

            if (result.Properties.Contains("displayname"))
            {
                p.DisplayName = result.Properties["displayname"][0].ToString();
            }

            if (result.Properties.Contains("wWWHomePage"))
            {
                p.Url = result.Properties["wWWHomePage"][0].ToString();
            }

            if (result.Properties.Contains("sn"))
            {
                p.SurName = result.Properties["sn"][0].ToString();
            }

            if (result.Properties.Contains("givenname"))
            {
                p.GivenName = result.Properties["givenname"][0].ToString();
            }

            if (result.Properties.Contains("description"))
            {
                p.Description = result.Properties["description"][0].ToString();
            }

            if (result.Properties.Contains("thumbnailPhoto"))
            {
                p.Thumbnail = (byte[])result.Properties["thumbnailPhoto"][0];
            }

            if (result.Properties.Contains("department"))
            {
                p.Department = result.Properties["department"][0].ToString();
            }

            if (result.Properties.Contains("physicalDeliveryOfficeName"))
            {
                p.Office = result.Properties["physicalDeliveryOfficeName"][0].ToString();
            }

            if (result.Properties.Contains("mobile"))
            {
                p.MobileNumber = result.Properties["mobile"][0].ToString();
            }

            if (result.Properties.Contains("title"))
            {
                p.Title = result.Properties["title"][0].ToString();
            }

            if (result.Properties.Contains("mail"))
            {
                p.EMail = result.Properties["mail"][0].ToString().ToLower();
            }

            if (result.Properties.Contains("telephoneNumber"))
            {
                p.TelephoneNumber = result.Properties["telephoneNumber"][0].ToString();
            }

            if (result.Properties.Contains("facsimileTelephoneNumber"))
            {
                p.FaxNumber = result.Properties["facsimileTelephoneNumber"][0].ToString();
            }

             if (result.Properties.Contains("userPrincipalName"))
            {
                p.UserPrincipalName = result.Properties["userPrincipalName"][0].ToString();
            }

             if (result.Properties.Contains("DistinguishedName"))
            {
                p.DistinguishedName = result.Properties["DistinguishedName"][0].ToString();
            }
            
            if (result.Properties.Contains("manager"))
            {
                LDAPSearchObject search = new LDAPSearchObject();
                search.AND.Add("distinguishedName", result.Properties["manager"][0].ToString());
                p.Manager = findContact(search);
            }

            if (result.Properties.Contains("st"))
            {
                p.State = result.Properties["st"][0].ToString();
            }

            if (result.Properties.Contains("streetAddress"))
            {
                p.Address = result.Properties["streetAddress"][0].ToString();
            }

            if (result.Properties.Contains("postalCode"))
            {
                p.PostalCode = result.Properties["postalCode"][0].ToString();
            }

            if (result.Properties.Contains("l"))
            {
                p.City = result.Properties["l"][0].ToString();
            }

            return p;
        }
	}

    
}