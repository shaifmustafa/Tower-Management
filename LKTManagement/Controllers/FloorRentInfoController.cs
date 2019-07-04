using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LKTManagement.BLL.Managers;
using LKTManagement.BLL.Managers;
using LKTManagement.Models.EntityModels;
using System.Linq.Dynamic;
using LKTManagement.Models.VM;
using System.Data.Entity.Core.Objects;

namespace LKTManagement.Controllers
{
    public class FloorRentInfoController : Controller
    {
        FloorRentInfoManager _floorRentInfoManager = new FloorRentInfoManager();
        TenantInfoManager _tenantInfoManager = new TenantInfoManager();
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

        [HttpPost]
        public JsonResult GetList()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            //Find paging info
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            //Find order columns info
            var sortColumnIndex = Request.Form.GetValues("order[0][column]").FirstOrDefault();
            var sortColumnName = Request.Form.GetValues("columns[" + sortColumnIndex + "][data]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            //find search columns info
            var search = Request.Form.GetValues("search[value]").FirstOrDefault().ToLower();
            var sFloorLevel = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault().ToLower();
            var sTenantInfoId = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault().ToLower();
            var sEffectiveDate = Request.Form.GetValues("columns[2][search][value]").FirstOrDefault();
            var sExpiryDate = Request.Form.GetValues("columns[3][search][value]").FirstOrDefault().ToLower();            

            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt16(start) : 0;
            var floorRentInfo = _floorRentInfoManager.GetAll().Where(s => s.IsActive && s.IsCurrent);  
            var tenantInfo = _tenantInfoManager.GetAll().Where(x => x.IsActive && x.IsCurrent == true);
            var query =(from f in floorRentInfo
                       join t in tenantInfo on f.TenantInfoId equals t.Id                       
                       select new FloorRentInfoVm{
                            Id = f.Id,
                            TenantInfoId = f.TenantInfoId,
                            FloorLevel =f.FloorLevel,
                            TenantName =t.Name,
                            EffectiveDate =f.EffectiveDate,
                            ExpiryDate =f.ExpiryDate,
                            RentSpaceSFT =f.RentSpaceSFT,
                            SftRate =f.SftRate,
                            LessAITPercent = f.LessAITPercent,
                            RentAIT = f.RentAIT,
                            CommonAIT = f.CommonAIT,
                            CommonBillRateSFT = f.CommonBillRateSFT,
                            AdvancePay = f.AdvancePay,
                            AdvanceAdjustment = f.AdvanceAdjustment
                           });
            var total = query.Count();

            //SEARCHING...
            //query = query.Where(q => q.FloorLevel == sFloorLevel || string.IsNullOrEmpty(sFloorLevel));
            query = query.Where(q => q.TenantInfoId.ToString() == sTenantInfoId || string.IsNullOrEmpty(sTenantInfoId));
            
            if (!string.IsNullOrEmpty(sEffectiveDate))
            {
                var sdEffectiveDate = DateTime.Parse(sEffectiveDate);
                query = query.Where(q => q.EffectiveDate == sdEffectiveDate);
            }
            if (!string.IsNullOrEmpty(sExpiryDate))
            {
                var sdExpiryDate = DateTime.Parse(sExpiryDate);
                query = query.Where(q => q.ExpiryDate == sdExpiryDate);
            }
                       

            //SORTING...  (For sorting we need to add a reference System.Linq.Dynamic)
            if (!(string.IsNullOrEmpty(sortColumnName) && string.IsNullOrEmpty(sortColumnDir)))
            {
                query = query.OrderBy(sortColumnName + " " + sortColumnDir);
            }
            var filtered = query.Count();
            if (pageSize != -1)
            {
                query = query.Skip(skip).Take(pageSize);
            }

            return Json(new { draw, recordsFiltered = filtered, recordsTotal = total, data = query.ToList() },
                     JsonRequestBehavior.AllowGet);
        }

        [HttpPost]

        public JsonResult GetDetails(Int64 id)
        {
            var res = _floorRentInfoManager.GetById(id);
            var resText = new FloorRentInfoVm
            {
                Id=res.Id,
                TenantInfoId = res.TenantInfoId,
                EffectiveDate = res.EffectiveDate,
                ExpiryDate = res.ExpiryDate,
                FloorLevel = res.FloorLevel,
                RentSpaceSFT = res.RentSpaceSFT,
                SftRate = res.SftRate,
                LessAITPercent = res.LessAITPercent,
                CommonBillRateSFT = res.CommonBillRateSFT,
                AdvancePay = res.AdvancePay,
                AdvanceAdjustment = res.AdvanceAdjustment,
                RentAIT = res.RentAIT,
                CommonAIT = res.CommonAIT
            };
            return Json(new { Data = resText, status = res == null ? false : true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]

        public JsonResult Save(FloorRentInfo model)
        {
            if (!ModelState.IsValid) return Json(new { info = "Failed", status = false }, JsonRequestBehavior.AllowGet);

            if (_floorRentInfoManager.SaveOrUpdate(model))
                return Json(new { info = "Saved", status = true }, JsonRequestBehavior.AllowGet);
            return Json(new { info = "Not Saved", status = false }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult Delete(Int64 Id)
        {
            if (_floorRentInfoManager.Delete(Id))
            {
                return Json(new { info = "Deleted", status = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { info = "Not Deleted", status = false }, JsonRequestBehavior.AllowGet);
            }

        }
	}
}