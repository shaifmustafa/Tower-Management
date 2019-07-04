using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LKTManagement.BLL.Managers;
using LKTManagement.Models.EntityModels;

namespace LKTManagement.Controllers
{
    public class FloorRentInfoController : Controller
    {
        FloorRentInfoManager _floorRentInfoManager = new FloorRentInfoManager();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetList()
        {
            return Json(new { info = _floorRentInfoManager.GetAll().ToList(), status = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]

        public JsonResult GetDetails(Int64 id)
        {
            return Json(new { info = _floorRentInfoManager.GetById(id), status = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]

        public JsonResult Save(FloorRentInfo model)
        {
            if (!ModelState.IsValid) return Json(new { info = "Failed", status = false }, JsonRequestBehavior.AllowGet);

            if (_floorRentInfoManager.SaveOrUpdate(model))
                return Json(new { info = "Saved", status = true }, JsonRequestBehavior.AllowGet);
            return Json(new { info = "Not Saved", status = false }, JsonRequestBehavior.AllowGet);

        }
	}
}