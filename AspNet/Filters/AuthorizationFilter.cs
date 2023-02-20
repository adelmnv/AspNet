using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNet.Filters
{
    public class AuthorizationFilter : AuthorizeAttribute
    {
        private string[] allowedRoles = new string[] { "admin,doctor,patient" };
        public AuthorizationFilter()
        {

        }

        protected virtual bool AuthorizeCore(HttpContextBase httpContext)
        {

            if (String.IsNullOrEmpty(base.Roles))
            {
                allowedRoles = base.Roles.Split(new char[] { ',' });
                for (int i = 0; i < allowedRoles.Length; i++)
                {
                    allowedRoles[i] = allowedRoles[i].Trim();
                }
            }

            return httpContext.Request.IsAuthenticated && Role(httpContext);
        }

        private bool Role(HttpContextBase httpContext)
        {
            if (allowedRoles.Length > 0)
            {
                for (int i = 0; i < allowedRoles.Length; i++)
                {
                    if (httpContext.User.IsInRole(allowedRoles[i]))
                    { return true; }
                }
                return false;
            }
            return false;
        }
    }
}