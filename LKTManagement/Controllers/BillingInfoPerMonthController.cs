using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LKTManagement.BLL.Managers;
using LKTManagement.Models.EntityModels;
using LKTManagement.Models.VM;
using LKTManagement.Repository.Base;
using LKTManagement.BLL.Base;

namespace LKTManagement.Controllers
{
    public class BillingInfoPerMonthController : Controller
    {
        BillingInfoPerMonthManager _billingInfoPerMonthManager = new BillingInfoPerMonthManager();
        TenantInfoManager _tenantInfoManager = new TenantInfoManager();
        FloorRentInfoManager _floorRentInfoManager = new FloorRentInfoManager();
        BillingReceivedInfoManager _BillingReceivedInfoManager = new BillingReceivedInfoManager();

        public ActionResult Index(DateTime? SDateFrom, DateTime? SDateTo)
        {
            if (Session["Id"] != null)
            {
                if (SDateFrom == null && SDateTo == null)
                {
                    //SDateFrom = DateTime.Parse("2017-01-01");
                    //SDateTo = DateTime.Parse("2019-12-31");

                    DateTime now = DateTime.Now;
                    SDateFrom = new DateTime(now.Year, now.Month, 1);
                    SDateTo = SDateFrom.Value.AddMonths(1).AddDays(-1);

                    String sDateFromstring = String.Format("{0:yyyy-MM-dd}", SDateFrom);

                    String sDateTostring = String.Format("{0:yyyy-MM-dd}", SDateTo);

                    ViewBag.SDateFrom = sDateFromstring;
                    ViewBag.SDateTo = sDateTostring;
                }

                else
                {                    
                    String sDateFromstring = String.Format("{0:yyyy-MM-dd}", SDateFrom);

                    String sDateTostring = String.Format("{0:yyyy-MM-dd}", SDateTo);

                    ViewBag.SDateFrom = sDateFromstring;
                    ViewBag.SDateTo = sDateTostring;
                }

                var TIDs = TempData["TIDs"] as IEnumerable<Int64>;
                DateTime ReportFrom = SDateFrom.HasValue ? SDateFrom.Value:new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).Date;
                DateTime ReportTo =SDateTo.HasValue?SDateTo.Value: DateTime.Now.Date;
                int SMonth = ReportFrom.Month;
                int TMonth = ReportTo.Month;
                int SYear = ReportFrom.Year;
                int TYear = ReportTo.Year;

                int CurrentMonth = DateTime.Now.Month;
                int CurrentYear = DateTime.Now.Year;

                var tenants = _tenantInfoManager.GetAll().Where(x => x.IsActive).ToList();
                

                var Tin = TIDs==null?new  TenantInfoManager().GetAll(): new TenantInfoManager().GetAll().Where(x=>TIDs.Contains(x.Id)).ToList();

                IEnumerable<TenantInfo> tins;

                if(TIDs == null)
                {
                    tins = new TenantInfoManager().GetAll();
                }
                else
                {
                    tins = new TenantInfoManager().GetAll().Where(x => TIDs.Contains(x.Id)).ToList();
                }

                var Fri = new FloorRentInfoManager().GetAll().Where(x=>x.IsActive == true).ToList();
                var Bim = new BillingInfoPerMonthManager().GetAll().Where(x => x.IsActive && (Convert.ToInt64(x.Year) * 12 + Convert.ToInt64(x.Month)) >= SYear * 12 + SMonth && (Convert.ToInt64(x.Year) * 12 + Convert.ToInt64(x.Month)) <= TYear * 12 + TMonth).ToList();
                var Bri = new BillingReceivedInfoManager().GetAll().Where(x => x.Date >= ReportFrom && x.Date <= ReportTo && x.IsActive).ToList();

                var query = (from tin in tins
                             where tin.IsActive.Equals(true)
                             join fri in Fri on tin.Id equals fri.TenantInfoId into fris
                             join bim in Bim on tin.Id equals bim.TenantInfoId
                             join bri in Bri on tin.Id equals bri.TenantInfoId into bris
                             select new MonthlyBillingReceivedInfoReportVm
                             {
                                 TenantId = tin.Id,
                                 TenantName = tin.Name,
                                 MonthlyBillingId = bim.Id,
                                 Month = Convert.ToInt32(bim.Month),
                                 Year = Convert.ToInt32(bim.Year),
                                 RentBill = bim.RentBill,
                                 CommonBill = bim.CommonBill,
                                 ElectricityBill = bim.ElectricityBill,
                                 EmElectricityBill = bim.EmElectricityBill,
                                 WasaBill = bim.WasaBill,


                                 FloorBasics = fris.Select(x => new FloorBasic
                                 {
                                     FloorLevel = x.FloorLevel,
                                     FloorSpace = x.RentSpaceSFT,
                                     RentPerSft = x.SftRate,
                                     EffectiveDate = x.EffectiveDate,
                                     ExpiryDate = x.ExpiryDate,
                                     CommonBillRateSFT = x.CommonBillRateSFT,
                                     AdvanceAdjustment = x.AdvanceAdjustment

                                 }).ToList(),

                                 ReceivedBillsPerMonth = bris.Select(x => new BillingReceivedPerMonth
                                 {
                                     Date = x.Date,
                                     Amount = x.Amount,
                                     Purpose = x.Purpose
                                 }).ToList(),

                                 LessAITPercent = tin.LessAITPercent,
                                 AdvanceAdjustment = tin.AdvancePay,
                                 RentAIT = tin.RentAIT,
                                 CommonAIT = tin.CommonAIT,

                                 OpeningBalanceRent = bim.OpeningBalanceRent,
                                 CurrentDuesRent = bim.CurrentDuesRent,                                 
                                 RemarksRent = bim.RemarksRent,

                                 OpeningBalanceCommon = bim.OpeningBalanceCommon,
                                 CurrentDuesCommon = bim.CurrentDuesCommon,
                                 RemarksCommon = bim.RemarksCommon,

                                 OpeningBalanceElectricity = bim.OpeningBalanceElectricity,
                                 CurrentDuesElectricity = bim.CurrentDuesElectricity,
                                 RemarksElectricity = bim.RemarksElectricity,

                                 OpeningBalanceWasa = bim.OpeningBalanceWasa,
                                 CurrentDuesWasa = bim.CurrentDuesWasa,
                                 RemarksWasa = bim.RemarksWasa,

                                 OpeningBalanceEmElectricity = bim.OpeningBalanceEmElectricity,
                                 CurrentDuesEmElectricity = bim.CurrentDuesEmElectricity,
                                 RemarksEmElectricity = bim.RemarksEmElectricity,
                             }).OrderByDescending(x => x.Month);



                var result = query.ToList();
                return View(result);
            }
            else
            {
                return RedirectToAction("Login", "AccountInfo");
            }            
        }

        [HttpPost]
        public ActionResult Index(Int64[] STenantInfoId, DateTime? SDateFrom, DateTime? SDateTo)
        {

            if (STenantInfoId[0] == 0)
            {
                STenantInfoId = null;
            }
            TempData["TIDs"] = STenantInfoId;
            return RedirectToAction("Index", new { SDateFrom, SDateTo });
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
        public ActionResult Save(Int64 UpdatedId, decimal[] Amount, string[] Note)
        {
            if (_billingInfoPerMonthManager.Saved(UpdatedId, Amount, Note))
                return RedirectToAction("Index", new { msg = "Saved" });
            return RedirectToAction("Index", new { msg = "Not Saved" });
        }
    }
}