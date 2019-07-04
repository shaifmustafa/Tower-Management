﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LKTManagement.BLL.Managers;
using LKTManagement.Models.EntityModels;

namespace LKTManagement.Controllers
{
    public class BillingInfoPerMonthController : Controller
    {
        BillingInfoPerMonthManager _billingInfoPerMonthManager = new BillingInfoPerMonthManager();
        public ActionResult Index()
        {
            return View();
        }
        

        [HttpGet]
        public JsonResult GetList()
        {
            return Json(new { info = _billingInfoPerMonthManager.GetAll().ToList(), status = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]

        public JsonResult GetDetails(Int64 id)
        {
            return Json(new { info = _billingInfoPerMonthManager.GetById(id), status = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]

        public JsonResult Save(BillingInfoPerMonth model)
        {
            if (!ModelState.IsValid) return Json(new { info = "Failed", status = false }, JsonRequestBehavior.AllowGet);

            if (_floorRentInfoManager.SaveOrUpdate(model))
                return Json(new { info = "Saved", status = true }, JsonRequestBehavior.AllowGet);
            return Json(new { info = "Not Saved", status = false }, JsonRequestBehavior.AllowGet);

        }
	}
}