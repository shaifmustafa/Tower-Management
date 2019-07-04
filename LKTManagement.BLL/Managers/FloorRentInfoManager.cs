using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LKTManagement.BLL.Base;
using LKTManagement.Models.EntityModels;
using LKTManagement.Repository.Base;
using LKTManagement.Repository.Repositories;

namespace LKTManagement.BLL.Managers
{
    public class FloorRentInfoManager : Manager<FloorRentInfo>
    {
        LoginInfo Log = new LoginInfo();
        TenantInfoManager tenantInfoManager = new TenantInfoManager();        
        TenantInfo tenantInfo = new TenantInfo();
        public FloorRentInfoManager() : base(new FloorRentInfoRepository())
        {
        }

        public override bool SaveOrUpdate(FloorRentInfo entity)
        {
            var exist = Repository.GetById(entity.Id);
            var tenantList = tenantInfoManager.GetById(entity.TenantInfoId);

            entity.Id = 0;
            entity.CreatedOn = new LoginInfo().CurrentTime;
            entity.CreatedBy = Log.UserId.ToString();
            entity.IsActive = true;
            entity.IsCurrent = true;


            var TotalRent = (entity.RentSpaceSFT * entity.SftRate);
            var TotalAIT = (TotalRent * entity.LessAITPercent) / 100;
            var CurrentDues = TotalRent - TotalAIT - entity.AdvancePay;
            var CommonDues = (entity.RentSpaceSFT * entity.CommonBillRateSFT);

            var CurrentDuesAIT = (CurrentDues * entity.RentAIT) / 100;
            var CommonDuesAIT = (CommonDues * entity.CommonAIT) / 100;

            if (exist != null)
            {

                if (tenantList != null)
                {
                    tenantList.Id = entity.TenantInfoId;
                    tenantList.LessAITPercent = entity.LessAITPercent;
                    tenantList.AdvancePay = entity.AdvancePay;
                    tenantList.RentAIT = CurrentDuesAIT;
                    tenantList.CommonAIT = CommonDuesAIT;
                    tenantInfoManager.Update(tenantList);
                }

                exist.FloorLevel = entity.FloorLevel;
                exist.LessAITPercent = entity.LessAITPercent;
                exist.RentSpaceSFT = entity.RentSpaceSFT;
                exist.SftRate = entity.SftRate;
                exist.TenantInfoId = entity.TenantInfoId;
                exist.AdvanceAdjustment = entity.AdvanceAdjustment;
                exist.AdvancePay = entity.AdvancePay;
                exist.CommonBillRateSFT = entity.CommonBillRateSFT;
                exist.EffectiveDate = entity.EffectiveDate;
                exist.ExpiryDate = entity.ExpiryDate;
                exist.RentAIT = entity.RentAIT;
                exist.CommonAIT = entity.CommonAIT;

                //exist.IsCurrent = true;
                exist.UpdatedBy = Log.UserId.ToString();
                exist.UpdatedOn = System.DateTime.Now;
                Repository.Update(exist);
            }
            else
            {
                if (tenantList != null)
                {
                    tenantList.Id = entity.TenantInfoId;
                    tenantList.LessAITPercent = entity.LessAITPercent;
                    tenantList.AdvancePay = entity.AdvancePay;
                    tenantList.RentAIT = CurrentDuesAIT;
                    tenantList.CommonAIT = CommonDuesAIT;
                    tenantInfoManager.SaveOrUpdate(tenantList);
                }

                var repo = new BillingInfoPerMonthRepository();
                var MonthNo = entity.EffectiveDate.Month;
                var YearNo = entity.EffectiveDate.Year;
                var tanentInfo = new TenantInfoRepository().GetById(entity.TenantInfoId);
                

                var existMonthActive = new BillingInfoPerMonthRepository().GetAll().FirstOrDefault(x=>x.Month==MonthNo.ToString()
                && x.Year == YearNo.ToString() && x.TenantInfoId==entity.TenantInfoId && x.IsActive == true);

                var existMonthInactive = new BillingInfoPerMonthRepository().GetAll().FirstOrDefault(x => x.Month == MonthNo.ToString()
                && x.Year == YearNo.ToString() && x.TenantInfoId == entity.TenantInfoId && x.IsActive == false);

                if (existMonthActive != null)
                    existMonthInactive = null;

                if(existMonthActive == null || existMonthInactive != null)
                {
                    var newRow = new BillingInfoPerMonth
                    {
                        Month = MonthNo.ToString(),
                        Year = YearNo.ToString(),
                        TenantInfoId = entity.TenantInfoId,
                        OpeningBalanceRent = 0,
                        OpeningBalanceCommon = 0,
                        OpeningBalanceElectricity = 0,
                        OpeningBalanceWasa = 0,
                        OpeningBalanceEmElectricity = 0,
                        CurrentDuesRent = CurrentDues,
                        CurrentDuesCommon = CommonDues,
                        CurrentDuesElectricity = 0,
                        CurrentDuesEmElectricity = 0,
                        CurrentDuesWasa = 0,
                        CreatedOn = new LoginInfo().CurrentTime,
                        CreatedBy = Log.UserId.ToString(),
                        IsActive = true
                };

                    repo.SaveOrUpdate(newRow);
                    repo.Done();
                }
                else
                {
                    existMonthActive.CurrentDuesRent += CurrentDues;
                    existMonthActive.CurrentDuesCommon += CommonDues;
                    existMonthActive.IsActive = true;
                    repo.SaveOrUpdate(existMonthActive);
                    repo.Done();
                }
                
                
                Repository.SaveOrUpdate(entity);
                Repository.Done();
            }

                       
            return true;
        }

        public bool Delete(Int64 Id)
        {
            BillingReceivedInfoManager billingReceivedManager = new BillingReceivedInfoManager();
            BillingInfoPerMonthManager billingInfoManager = new BillingInfoPerMonthManager();

            var exist = Repository.GetById(Id);

            var multipleFloorExist = Repository.GetAll().Where(x => x.TenantInfoId == exist.TenantInfoId && x.IsActive && x.IsCurrent).ToList();

            exist.UpdatedBy = Log.UserId.ToString();
            exist.UpdatedOn = new LoginInfo().CurrentTime;
            exist.IsActive = false;
            exist.IsCurrent = false;
            Repository.SaveOrUpdate(exist);


            if(multipleFloorExist.Count == 1)
            {
                var billingReceivedExist = billingReceivedManager.GetAll().Where(x => x.TenantInfoId == exist.TenantInfoId && x.IsActive).ToList();
                var billingInfoExist = billingInfoManager.GetAll().Where(x => x.TenantInfoId == exist.TenantInfoId && x.IsActive).ToList();


                foreach (var billingReceive in billingReceivedExist)
                {
                    var existBillingReceive = billingReceivedManager.GetById(billingReceive.Id);

                    billingReceive.IsActive = false;
                    billingReceive.UpdatedBy = Log.UserId.ToString();
                    billingReceive.UpdatedOn = new LoginInfo().CurrentTime;


                    billingReceivedManager.SaveOrUpdateDelete(existBillingReceive);
                }


                foreach (var billingInfo in billingInfoExist)
                {
                    var existBillingInfo = billingInfoManager.GetById(billingInfo.Id);

                    billingInfo.IsActive = false;
                    billingInfo.UpdatedBy = Log.UserId.ToString();
                    billingInfo.UpdatedOn = new LoginInfo().CurrentTime;


                    billingInfoManager.SaveOrUpdateDelete(existBillingInfo);
                }
            }

            else
            {
                var TotalRent = (exist.RentSpaceSFT * exist.SftRate);
                var TotalAIT = (TotalRent * exist.LessAITPercent) / 100;
                var CurrentDues = TotalRent - TotalAIT - exist.AdvancePay;
                var CommonDues = (exist.RentSpaceSFT * exist.CommonBillRateSFT);


                var billingInfoExist = billingInfoManager.GetAll().Where(x => x.TenantInfoId == exist.TenantInfoId && x.IsActive).FirstOrDefault();

                var existBillingInfo = billingInfoManager.GetById(billingInfoExist.Id);

                existBillingInfo.IsActive = true;
                existBillingInfo.UpdatedBy = Log.UserId.ToString();
                existBillingInfo.UpdatedOn = new LoginInfo().CurrentTime;

                existBillingInfo.CurrentDuesRent = existBillingInfo.CurrentDuesRent - CurrentDues;
                existBillingInfo.CurrentDuesRent = Math.Abs(existBillingInfo.CurrentDuesRent);

                existBillingInfo.CurrentDuesCommon = existBillingInfo.CurrentDuesCommon - CommonDues;
                existBillingInfo.CurrentDuesCommon = Math.Abs(existBillingInfo.CurrentDuesCommon);

                billingInfoManager.SaveOrUpdateDelete(existBillingInfo);
            }


            return Repository.Done();
        }
    }
}
