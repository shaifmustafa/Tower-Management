using LKTManagement.BLL.Managers;
using LKTManagement.Models.EntityModels;
using LKTManagement.Models.EntityModels.VM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LKTManagement.Controllers
{
    public class CompanyInfoController : Controller
    {

        CompanyInfoManager companyInfoManager = new CompanyInfoManager();

        // GET: CompanyInfo
        public ActionResult Index()
        {
            if (Session["Id"] != null)
            {
                return View();
            }

            else
            {
                return RedirectToAction("Login", "AccountInfo");
            }
        }

        //public ActionResult LogoView(CompanyUser company)
        //{
        //    return PartialView(company);
        //}

        [HttpPost]
        public ActionResult Save(CompanyUserVm model)
        {
            CompanyUser company = new CompanyUser();

            if (model.ImageFile != null)
            {
                string imageName = System.IO.Path.GetFileName(model.ImageFile.FileName);
                string physicalPath = Server.MapPath("~/Images/Upload/" + imageName);
                model.ImageFile.SaveAs(physicalPath);

                model.ProfilePicture = imageName;
                company.ProfilePicture = model.ProfilePicture;
                Session["logo"] = company.ProfilePicture;
            }

            company.Id = model.Id;
            company.Name = model.Name;
            company.Email = model.Email;
            company.Address = model.Address;
            company.Phone = model.Phone;

            if (companyInfoManager.SaveOrUpdate(company))
            {
                return RedirectToAction("Index", "CompanyInfo");
            }

            else
            {
                return RedirectToAction("Index", "CompanyInfo");
            }
        }
    }
}