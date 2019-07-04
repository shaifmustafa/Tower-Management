using LKTManagement.BLL.Managers;
using LKTManagement.Models.EntityModels;
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

        ApplicationUserManager _applicationUserManager = new ApplicationUserManager();
        [HttpGet]
        public ActionResult Index(Int64? id)
        {
            var applicationUserList = _applicationUserManager.GetById((Int64)id);
            return View(applicationUserList);
        }     

        [HttpGet]
        public ActionResult Login()
        {
            Session.Clear();
            return View();
        }

        [HttpPost]
        public ActionResult Login(ApplicationUser applicationUser)
        {   
            try
            {              
                var usr = _applicationUserManager.GetAll().Where(u => u.UserName == applicationUser.UserName && u.Password == applicationUser.Password).FirstOrDefault();
                if(usr != null)
                {
                    Session["Id"] = usr.Id.ToString();
                    Session["UserName"] = usr.UserName.ToString();
                    return RedirectToAction("Index", "TenantInfo");
                }
                else
                {
                    ViewBag.Msg = "Username or Password is wrong!";
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());

            }

            return View(applicationUser);
        }

        private void EnsureLoggedOut()
        {            
            if (Request.IsAuthenticated)
                Logout();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            try
            {                
                HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);

                Session.Clear();
                System.Web.HttpContext.Current.Session.RemoveAll();                
                return View();
            }
            catch
            {
                throw;
            }
        }        

        [HttpPost]
        public JsonResult Save(ApplicationUser model)
        {
            //model.Id = id;
            if (!ModelState.IsValid) return Json(new { info = "Failed", status = false }, JsonRequestBehavior.AllowGet);
            if (_applicationUserManager.SaveOrUpdate(model))
                return Json(new { info = "Saved", status = true }, JsonRequestBehavior.AllowGet);
            return Json(new { info = "Not Saved", status = false }, JsonRequestBehavior.AllowGet);
        }
    }
}