using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LKTManagement.BLL.Managers;
using LKTManagement.Models.EntityModels;
using System.Linq.Dynamic;
using LKTManagement.Models.EntityModels.VM;
using LKTManagement.Models.VM;

namespace LKTManagement.Controllers
{
    public class TenantInfoController : Controller
    {
        // GET: /TenantInfo/
        TenantInfoManager _tenantInfoManagerger = new TenantInfoManager();
        FloorRentInfoManager floorInfoManager = new FloorRentInfoManager();
        BillingReceivedInfoInput billingReceivedInfo = new BillingReceivedInfoInput();
        BillingInfoPerMonth billingInfoPerMonth = new BillingInfoPerMonth();

        FloorRentInfo floorRent = new FloorRentInfo();
        public ActionResult Index()
        {
            if(Session["Id"] != null)
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
            //jQuery DataTables Param
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
            var sName = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault().ToLower();
          

            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt16(start) : 0;
            var query = _tenantInfoManagerger.GetAll().Where(s => s.IsActive && s.IsCurrent == true);
            var floorRentInfo = floorInfoManager.GetAll().Where(s => s.IsActive && s.IsCurrent);

            

            var toList = query.ToList();
            var total = query.Count();
            //SEARCHING(Loading Table Data)...
            query = query.Where(q => (q.Name.ToLower().Contains(sName) || string.IsNullOrEmpty(sName)));
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
            var res = _tenantInfoManagerger.GetById(id);
            return Json(new { Data = res, status = res==null? false:true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult TenantInfoes(Int64 id)
        {
            var res = _tenantInfoManagerger.GetById(id);

            var tenantInfo = _tenantInfoManagerger.GetAll().Where(x=>x.IsActive);
            var floorInfo = floorInfoManager.GetAll().Where(x=>x.IsActive && x.IsCurrent);

            var query = (from f in floorInfo
                         where f.TenantInfoId.Equals(id)
                         join t in tenantInfo on f.TenantInfoId equals t.Id
                         select new TenantInfoVm
                         {
                             Id = t.Id,
                             FloorInfoId = f.Id,
                             Name = t.Name,
                             CommonBillRateSFT = f.CommonBillRateSFT,
                             LessAITPercent = f.LessAITPercent,
                             AdvanceAdjustment = f.AdvanceAdjustment,
                             Note = t.Note,
                             AdvancePay = f.AdvancePay,
                             FloorLevel = f.FloorLevel,
                             RentSpaceSFT = f.RentSpaceSFT,
                             SftRate = f.SftRate
                         });


            //var flr = floorInfoManager.GetById(query);            

            var total = query.Count();

            var result = query.ToList();

            Console.WriteLine(result);

            var historyList = _tenantInfoManagerger.TenantHistory(id).ToList();

            var check = historyList.Count();

            return Json(new { Data = result, status = result == null ? false : true }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult TenantDetailsTable(Int64 id)
        {
            //jQuery DataTables Param
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

            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt16(start) : 0;


            var res = _tenantInfoManagerger.GetById(id);

            var tenantInfo = _tenantInfoManagerger.GetAll();
            var floorInfo = floorInfoManager.GetAll().ToList();

            var query = (from f in floorInfo
                         where f.TenantInfoId.Equals(id)
                         where f.IsActive.Equals(true)
                         join t in tenantInfo on f.TenantInfoId equals t.Id
                         where t.IsActive.Equals(true)
                         select new TenantInfoVm
                         {
                             TenantId = t.Id,
                             FloorInfoId = f.Id,
                             Name = t.Name,
                             CommonBillRateSFT = f.CommonBillRateSFT,
                             LessAITPercent = f.LessAITPercent,
                             Note = t.Note,
                             AdvancePay = f.AdvancePay,
                             FloorLevel = f.FloorLevel,
                             RentSpaceSFT = f.RentSpaceSFT,
                             SftRate = f.SftRate,
                             EffectiveDate = f.EffectiveDate,
                             ExpiryDate = f.ExpiryDate,
                             RentAIT = f.RentAIT,
                             CommonAIT = f.CommonAIT
                         });


            var total = query.Count();

            var result = query.ToList();

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

            return Json(new { draw, recordsFiltered = filtered, recordsTotal = total, data = result },
                    JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Save(TenantInfo model)
        {
            if (!ModelState.IsValid) return Json(new { info = "Failed", status = false }, JsonRequestBehavior.AllowGet);
            if (_tenantInfoManagerger.SaveOrUpdate(model))
                return Json(new { info = "Saved", status = true }, JsonRequestBehavior.AllowGet);
            return Json(new { info = "Not Saved", status = false }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(Int64 Id)
        {
            if (_tenantInfoManagerger.Delete(Id))
            {
                return Json(new { info = "Done!", status = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { info = "Failed!!", status = false }, JsonRequestBehavior.AllowGet);
            }
        }        


        [HttpPost]

        public JsonResult ShowInactive()
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
            var sName = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault().ToLower();


            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt16(start) : 0;
            var query = _tenantInfoManagerger.GetAll().Where(s => s.IsCurrent == false);
            var total = query.Count();

            //SEARCHING(Loading Table Data)...
            query = query.Where(q => (q.Name.ToLower().Contains(sName) || string.IsNullOrEmpty(sName)));

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



        [HttpGet]
        public JsonResult TenantList()
        {
            return Json(new
            {
                Data = new TenantInfoManager().GetAll().Where(w=>w.IsActive).Select(x => new {
                    x.Id,
                    x.Name
                }).ToList(),
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

       
    }
}
