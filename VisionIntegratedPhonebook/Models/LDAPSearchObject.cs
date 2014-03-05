using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisionIntegratedPhonebook.Models
{
    public class LDAPSearchObject 
    {
        public LDAPSearchObject()
        {
            NOT.Add("userAccountControl:1.2.840.113556.1.4.803:", "2");
        }
            
        public string root = "LDAP://OU=Vision Integrated Graphics,DC=awardgraphics,DC=com";

        public Dictionary<string, string> AND = new Dictionary<string, string>();
        public Dictionary<string, string> NOT = new Dictionary<string, string>();

        public List<string> LoadAttributes = new List<string>(new string[] {
                "displayName",
                "name",
                "title",
                "department",
                "telephoneNumber",
                "facsimileTelephoneNumber",
                "description",
                "mobile", 
                "mail", 
                "userPrincipalName", 
                "physicalDeliveryOfficeName",
                "sn",
                "givenName",
                "city", 
                "st",
                "postalCode",
                "streetAddress",
                "l",
                "wWWHomePage",
                "manager",
                "DistinguishedName"
        });

        public string filter()
        {
   
            string filter = "(&";

            foreach (KeyValuePair<string, string> kv in AND)
            {
                filter += string.Format("({0}={1})", kv.Key, kv.Value);
            }

            if (NOT.Count > 0)
            {
                filter += "(!";
                foreach (KeyValuePair<string, string> kv in NOT)
                {
                    filter += string.Format("({0}={1})", kv.Key, kv.Value);
                }
                filter += "))";
            }
            else
            {
                filter += ")";
            }


            return filter;            
        }

        internal void Clear()
        {
            AND.Clear();
            NOT.Clear();
            NOT.Add("userAccountControl:1.2.840.113556.1.4.803:", "2");
        }
    }
}