using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LKTManagement.BLL.Base;
using LKTManagement.Models.EntityModels;
using LKTManagement.Repository.Base;
using LKTManagement.Repository.Repositories;
using LKTManagement.Models.VM;

namespace LKTManagement.BLL.Managers
{
    public class BillingReceivedInfoManager : Manager<BillingReceivedInfo>
    {
        LoginInfo Log = new LoginInfo();
        
        public BillingReceivedInfoManager() : base(new BillingReceivedInfoRepository())
        {
        }
        public bool Saved(BillingReceivedInfoInput model)
        {
            decimal rentBill = 0;
            decimal commonBill = 0;
            decimal electricityBill = 0;
            decimal emElectricityBill = 0;
            decimal wasaBill = 0;

            BillingInfoPerMonth billingInfo = new BillingInfoPerMonth();
            BillingInfoPerMonthManager billingInfoManager = new BillingInfoPerMonthManager();

            for(var i=0;i<model.Amount.Length;i++)
            {
                if (model.Amount[i] < 1) continue;
                var entity = new BillingReceivedInfo()
                {
                    TenantInfoId = model.TenantInfoId,
                    Date = model.Date,
                    CreatedOn = new LoginInfo().CurrentTime,
                    CreatedBy = Log.UserId.ToString(),
                    IsActive = true,
                    Amount = model.Amount[i],
                    Note = model.Note[i],
                    Purpose = model.Purpose[i]
                };

                if (i == 0)
                    rentBill = model.Amount[i];
                else if (i == 1)
                    commonBill = model.Amount[i];
                else if (i == 2)
                    electricityBill = model.Amount[i];
                else if (i == 3)
                    emElectricityBill = model.Amount[i];
                else
                    wasaBill = model.Amount[i];

                Repository.SaveOrUpdate(entity); 
            }

            billingInfo.Month = Convert.ToString(model.Date.Month);
            billingInfo.Year = Convert.ToString(model.Date.Year);
            billingInfo.TenantInfoId = model.TenantInfoId;

            var dateYearExist = billingInfoManager.GetAll().Where(x => x.Month == billingInfo.Month && x.Year == billingInfo.Year && x.TenantInfoId == billingInfo.TenantInfoId && x.IsActive).LastOrDefault();
            var exist = billingInfoManager.GetAll().Where(x => x.TenantInfoId == billingInfo.TenantInfoId && x.IsActive).LastOrDefault();

            if (dateYearExist == null)
            {
                billingInfo.RentBill = rentBill;
                billingInfo.CommonBill = commonBill;
                billingInfo.ElectricityBill = electricityBill;
                billingInfo.EmElectricityBill = emElectricityBill;
                billingInfo.WasaBill = wasaBill;
            }
            else
            {
                billingInfo.Id = dateYearExist.Id;
                billingInfo.RentBill = dateYearExist.RentBill + rentBill;
                billingInfo.CommonBill = dateYearExist.CommonBill + commonBill;
                billingInfo.ElectricityBill = dateYearExist.ElectricityBill + electricityBill;
                billingInfo.EmElectricityBill = dateYearExist.EmElectricityBill + emElectricityBill;
                billingInfo.WasaBill = dateYearExist.WasaBill + wasaBill;

                billingInfo.CurrentDuesRent = dateYearExist.CurrentDuesRent;
                billingInfo.CurrentDuesCommon = dateYearExist.CurrentDuesCommon;
                billingInfo.CurrentDuesElectricity = dateYearExist.CurrentDuesElectricity;
                billingInfo.CurrentDuesEmElectricity = dateYearExist.CurrentDuesEmElectricity;
                billingInfo.CurrentDuesWasa = dateYearExist.CurrentDuesWasa;

                billingInfo.OpeningBalanceRent = dateYearExist.OpeningBalanceRent;
                billingInfo.OpeningBalanceCommon = dateYearExist.OpeningBalanceCommon;
                billingInfo.OpeningBalanceElectricity = dateYearExist.OpeningBalanceElectricity;
                billingInfo.OpeningBalanceEmElectricity = dateYearExist.OpeningBalanceEmElectricity;
                billingInfo.OpeningBalanceWasa = dateYearExist.OpeningBalanceWasa;

                billingInfo.RemarksRent = dateYearExist.RemarksRent;
                billingInfo.RemarksCommon = dateYearExist.RemarksCommon;
                billingInfo.RemarksElectricity = dateYearExist.RemarksElectricity;
                billingInfo.RemarksEmElectricity = dateYearExist.RemarksEmElectricity;
                billingInfo.RemarksWasa = dateYearExist.RemarksWasa;

                billingInfo.CreatedBy = dateYearExist.CreatedBy;
                billingInfo.CreatedOn = dateYearExist.CreatedOn;
                billingInfo.UpdatedBy = Log.UserId.ToString();
                billingInfo.UpdatedOn = System.DateTime.Now;
                billingInfo.IsActive = true;

                exist = null;

                billingInfoManager.SaveOrUpdate(billingInfo);
                
            }
            


            if (exist != null)
            {
                billingInfo.CurrentDuesRent = exist.CurrentDuesRent;
                billingInfo.CurrentDuesCommon = exist.CurrentDuesCommon;
                billingInfo.CurrentDuesElectricity = exist.CurrentDuesElectricity;
                billingInfo.CurrentDuesEmElectricity = exist.CurrentDuesEmElectricity;
                billingInfo.CurrentDuesWasa = exist.CurrentDuesWasa;

                billingInfo.OpeningBalanceRent = exist.CurrentDuesRent + exist.OpeningBalanceRent;
                billingInfo.OpeningBalanceCommon = exist.CurrentDuesCommon + exist.OpeningBalanceCommon;
                billingInfo.OpeningBalanceElectricity = exist.CurrentDuesElectricity + exist.OpeningBalanceElectricity;
                billingInfo.OpeningBalanceEmElectricity = exist.CurrentDuesEmElectricity + exist.OpeningBalanceEmElectricity;
                billingInfo.OpeningBalanceWasa = exist.CurrentDuesWasa + exist.OpeningBalanceWasa;

                billingInfo.RemarksRent = exist.RemarksRent;
                billingInfo.RemarksCommon = exist.RemarksCommon;
                billingInfo.RemarksElectricity = exist.RemarksElectricity;
                billingInfo.RemarksEmElectricity = exist.RemarksEmElectricity;
                billingInfo.RemarksWasa = exist.RemarksWasa;

                billingInfo.CreatedBy = exist.CreatedBy;
                billingInfo.CreatedOn = exist.CreatedOn;
                billingInfo.UpdatedBy = Log.UserId.ToString();
                billingInfo.UpdatedOn = System.DateTime.Now;
                billingInfo.IsActive = true;

                billingInfoManager.SaveOrUpdate(billingInfo);
            }


            return Repository.Done();
        }


        private bool BillingInfoPerMonth(BillingReceivedInfoInput model)
        {           
            return Repository.Done();
        }


        public bool Updating(BillingReceivedInfo entity)
        {
            var exist = Repository.GetById(entity.Id);

            BillingInfoPerMonth billingInfo = new BillingInfoPerMonth();
            BillingInfoPerMonthManager billingInfoManager = new BillingInfoPerMonthManager();

            billingInfo.Month = Convert.ToString(entity.Date.Month);
            billingInfo.Year = Convert.ToString(entity.Date.Year);
            billingInfo.TenantInfoId = entity.TenantInfoId;

            var dateYearExist = billingInfoManager.GetAll().Where(x => x.Month == billingInfo.Month && x.Year == billingInfo.Year && x.TenantInfoId == billingInfo.TenantInfoId && x.IsActive).LastOrDefault();

            if (exist!= null)
            {
                entity.CreatedBy = exist.CreatedBy;
                entity.CreatedOn = exist.CreatedOn;
                entity.IsActive = exist.IsActive;
                entity.UpdatedBy = Log.UserId.ToString();
                entity.UpdatedOn = new LoginInfo().CurrentTime;

                Repository.SaveOrUpdate(entity);

                if (entity.Purpose == "Rent")
                {
                    billingInfo.RentBill = entity.Amount + dateYearExist.RentBill;
                    billingInfo.CommonBill = dateYearExist.CommonBill;
                    billingInfo.ElectricityBill = dateYearExist.ElectricityBill;
                    billingInfo.EmElectricityBill = dateYearExist.EmElectricityBill;
                    billingInfo.WasaBill = dateYearExist.WasaBill;
                }
                else if (entity.Purpose == "Common Bill")
                {
                    billingInfo.CommonBill = entity.Amount + dateYearExist.CommonBill;
                    billingInfo.RentBill = dateYearExist.RentBill;
                    billingInfo.ElectricityBill = dateYearExist.ElectricityBill;
                    billingInfo.EmElectricityBill = dateYearExist.EmElectricityBill;
                    billingInfo.WasaBill = dateYearExist.WasaBill;
                }
                else if (entity.Purpose == "Electricity Bill")
                {
                    billingInfo.ElectricityBill = entity.Amount + dateYearExist.ElectricityBill;
                    billingInfo.RentBill = dateYearExist.RentBill;
                    billingInfo.CommonBill = dateYearExist.CommonBill;
                    billingInfo.EmElectricityBill = dateYearExist.EmElectricityBill;
                    billingInfo.WasaBill = dateYearExist.WasaBill;
                }
                else if (entity.Purpose == "Emergency Electricity Bill")
                {
                    billingInfo.EmElectricityBill = entity.Amount + dateYearExist.EmElectricityBill;
                    billingInfo.RentBill = dateYearExist.RentBill;
                    billingInfo.CommonBill = dateYearExist.CommonBill;
                    billingInfo.ElectricityBill = dateYearExist.ElectricityBill;
                    billingInfo.WasaBill = dateYearExist.WasaBill;
                }
                else
                {
                    billingInfo.WasaBill = entity.Amount + dateYearExist.WasaBill;
                    billingInfo.RentBill = dateYearExist.RentBill;
                    billingInfo.CommonBill = dateYearExist.CommonBill;
                    billingInfo.ElectricityBill = dateYearExist.ElectricityBill;
                    billingInfo.EmElectricityBill = dateYearExist.EmElectricityBill;
                }
                billingInfo.Id = dateYearExist.Id;
                billingInfo.CurrentDuesRent = dateYearExist.CurrentDuesRent;
                billingInfo.CurrentDuesCommon = dateYearExist.CurrentDuesCommon;
                billingInfo.CurrentDuesElectricity = dateYearExist.CurrentDuesElectricity;
                billingInfo.CurrentDuesEmElectricity = dateYearExist.CurrentDuesEmElectricity;
                billingInfo.CurrentDuesWasa = dateYearExist.CurrentDuesWasa;

                billingInfo.OpeningBalanceRent = dateYearExist.OpeningBalanceRent;
                billingInfo.OpeningBalanceCommon = dateYearExist.OpeningBalanceCommon;
                billingInfo.OpeningBalanceElectricity = dateYearExist.OpeningBalanceElectricity;
                billingInfo.OpeningBalanceEmElectricity = dateYearExist.OpeningBalanceEmElectricity;
                billingInfo.OpeningBalanceWasa = dateYearExist.OpeningBalanceWasa;

                billingInfo.RemarksRent = dateYearExist.RemarksRent;
                billingInfo.RemarksCommon = dateYearExist.RemarksCommon;
                billingInfo.RemarksElectricity = dateYearExist.RemarksElectricity;
                billingInfo.RemarksEmElectricity = dateYearExist.RemarksEmElectricity;
                billingInfo.RemarksWasa = dateYearExist.RemarksWasa;

                billingInfo.CreatedBy = dateYearExist.CreatedBy;
                billingInfo.CreatedOn = dateYearExist.CreatedOn;
                billingInfo.UpdatedBy = Log.UserId.ToString();
                billingInfo.UpdatedOn = System.DateTime.Now;
                billingInfo.IsActive = true;


                billingInfoManager.SaveOrUpdate(billingInfo);

            }            
            return Repository.Done();
        }
    }
}
