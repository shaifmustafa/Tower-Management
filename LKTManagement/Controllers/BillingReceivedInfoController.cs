using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LKTManagement.BLL.Managers;
using LKTManagement.Models.EntityModels;
using LKTManagement.Models.VM;
using System.Linq.Dynamic;

namespace LKTManagement.Controllers
{
    public class BillingReceivedInfoController : Controller
    {
        BillingReceivedInfoManager _billingReceivedInfoManager = new BillingReceivedInfoManager();
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
            var sTenantInfoId = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault().ToLower();
            var sDateFrom = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault().ToLower();
            var sDateTo = Request.Form.GetValues("columns[2][search][value]").FirstOrDefault().ToLower();

            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt16(start) : 0;
            var billingInfo = _billingReceivedInfoManager.GetAll().Where(x=>x.IsActive);
            var tenantInfo = _tenantInfoManager.GetAll();
            var query = (from b in billingInfo
                         join t in tenantInfo on b.TenantInfoId equals t.Id
                         orderby b.Date
                         select new BillingReceivedInfoList
                         {
                             Id = b.Id,
                             TenantInfoId = b.TenantInfoId,
                             TenantName = t.Name,                             
                             Purpose = b.Purpose,
                             Date = b.Date,
                             Note=b.Note,
                             Amount=b.Amount
                         });
            var total = query.Count();
            var desQuery = query.OrderByDescending(x => x.Date).ToList();

            //List<BillingReceivedInfoList> desQuery = null;            

            //SEARCHING...
            query = query.Where(q => q.TenantInfoId.ToString() == sTenantInfoId || string.IsNullOrEmpty(sTenantInfoId));

            if (!string.IsNullOrEmpty(sDateFrom))
            {
                query = query.Where(q => q.Date >= DateTime.Parse(sDateFrom));
            }
            if (!string.IsNullOrEmpty(sDateTo))
            {
                query = query.Where(q => q.Date <= DateTime.Parse(sDateTo));
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
            var res = _billingReceivedInfoManager.GetById(id);

            var resText = new BillingReceivedInfoList
            {
                Id = res.Id,
                TenantInfoId = res.TenantInfoId,                
                Purpose = res.Purpose,
                Date = res.Date,                
                Note = res.Note,
                Amount = res.Amount
            };

            return Json(new { Data = resText, status = res == null ? false : true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]

        public JsonResult Save(BillingReceivedInfoInput model)
        {
            //if (!ModelState.IsValid) return Json(new { info = "Failed", status = false }, JsonRequestBehavior.AllowGet);

            if (_billingReceivedInfoManager.Saved(model))
            {
                return Json(new { info = "Saved", status = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { info = "Please Set Value of Date and Amonut", status = false }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult Update(BillingReceivedInfo model)
        {
            //if (!ModelState.IsValid) return Json(new { info = "Failed", status = false }, JsonRequestBehavior.AllowGet);

            if (_billingReceivedInfoManager.Updating(model))
                return Json(new { info = "Saved", status = true }, JsonRequestBehavior.AllowGet);
            return Json(new { info = "Please Set Value of Date and Amonut", status = false }, JsonRequestBehavior.AllowGet);
        }

    }
}