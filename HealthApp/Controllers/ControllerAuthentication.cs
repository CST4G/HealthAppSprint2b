using System;
using System.Web.Mvc;
namespace healthApp.Controllers
{
    //controllers should extend this class. It contains methods for authentication methods for the users
    public class ControllerAuthentication : Controller
    {
        
        
        public bool hasAdminAccess()
        {
            return Request.IsAuthenticated && (User.Identity.Name.ToString() == "sysadmin");
        }

        public bool hasUserAccess()
        {
            //note: name in this case is actually the role of the user not the name
            return Request.IsAuthenticated && (hasAdminAccess() || (User.Identity.Name.ToString() == "user"));
        }
    }
}
