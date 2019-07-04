using LKTManagement.Models.EntityModels.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace LKTManagement.Controllers
{
    public class AccountInfoController : Controller
    {
        // GET: AccountInfo
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login(string returnURL)
        {
            var userinfo = new LoginVM();

            try
            {
                // We do not want to use any existing identity information
                EnsureLoggedOut();

                // Store the originating URL so we can attach it to a form field
                userinfo.ReturnURL = returnURL;

                return View(userinfo);
            }
            catch
            {
                throw;
            }
        }

        private void EnsureLoggedOut()
        {
            // If the request is (still) marked as authenticated we send the user to the logout action
            if (Request.IsAuthenticated)
                Logout();
        }
        //POST: Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            try
            {
                // First we clean the authentication ticket like always
                //required NameSpace: using System.Web.Security;
                //FormsAuthentication.SignOut();

                // Second we clear the principal to ensure the user does not retain any authentication
                //required NameSpace: using System.Security.Principal;
                HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);

                Session.Clear();
                System.Web.HttpContext.Current.Session.RemoveAll();

                // Last we redirect to a controller/action that requires authentication to ensure a redirect takes place
                // this clears the Request.IsAuthenticated flag since this triggers a new request
                return View();
            }
            catch
            {
                throw;
            }
        }
    }
}