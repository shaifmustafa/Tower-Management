using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LKTManagement.Models.EntityModels;
using LKTManagement.BLL.Base;
using LKTManagement.Repository.Base;
using LKTManagement.Repository.Repositories;
using LKTManagement.Repository.DatabaseContext;
using System.Data.Entity;

namespace LKTManagement.BLL.Managers
{
    


    public class TenantInfoManager : Manager<TenantInfo>
    {

        protected LKTManagementDbContext dbContext =new  LKTManagementDbContext();
        LoginInfo Log = new LoginInfo();
        public TenantInfoManager() : base(new TenantInfoRepository())
        {
        }
        public override bool SaveOrUpdate(TenantInfo entity)
        {
            var exist = Repository.GetById(entity.Id);

            if (exist == null)
            {
                entity.CreatedOn = new LoginInfo().CurrentTime;
                entity.CreatedBy = Log.UserId.ToString();
                entity.IsActive = true;
                entity.IsCurrent = true;
            }
            else
            {
                entity.CreatedBy = exist.CreatedBy;
                entity.CreatedOn = exist.CreatedOn;
                entity.IsActive = exist.IsActive;
                entity.IsCurrent = exist.IsCurrent;
                entity.UpdatedBy = Log.UserId.ToString();
                entity.UpdatedOn = new LoginInfo().CurrentTime;
            }
            Repository.SaveOrUpdate(entity);
            return Repository.Done();
        }


        public bool UncheckTenant(Int64 Id)
        {            
            return Repository.Done();
        }

        public bool Delete(Int64 Id)
        {
            FloorRentInfoManager floorInfoManager = new FloorRentInfoManager();
            BillingReceivedInfoManager billingReceivedManager = new BillingReceivedInfoManager();
            BillingInfoPerMonthManager billingInfoManager = new BillingInfoPerMonthManager();
            
            var exist = Repository.GetById(Id);            

            exist.UpdatedBy = Log.UserId.ToString();
            exist.UpdatedOn = new LoginInfo().CurrentTime;
            exist.IsActive = false;
            exist.IsCurrent = false;
            Repository.SaveOrUpdate(exist);


            var floorRentExist = floorInfoManager.GetAll().Where(x => x.TenantInfoId == Id && x.IsActive).ToList();
            var billingReceivedExist = billingReceivedManager.GetAll().Where(x => x.TenantInfoId == Id && x.IsActive).ToList();
            var billingInfoExist = billingInfoManager.GetAll().Where(x => x.TenantInfoId == Id && x.IsActive).ToList();

            foreach (var floors in floorRentExist)
            {
                var existFloor = floorInfoManager.GetById(floors.Id);
                floors.IsActive = false;
                floors.IsCurrent = false;
                floors.UpdatedBy = Log.UserId.ToString();
                floors.UpdatedOn = new LoginInfo().CurrentTime;
                

                floorInfoManager.SaveOrUpdateDelete(existFloor);
            }

            foreach(var billingReceive in billingReceivedExist)
            {
                var existBillingReceive = billingReceivedManager.GetById(billingReceive.Id);

                billingReceive.IsActive = false;
                billingReceive.UpdatedBy = Log.UserId.ToString();
                billingReceive.UpdatedOn = new LoginInfo().CurrentTime;


                billingReceivedManager.SaveOrUpdateDelete(existBillingReceive);
            }


            foreach(var billingInfo in billingInfoExist)
            {
                var existBillingInfo = billingInfoManager.GetById(billingInfo.Id);

                billingInfo.IsActive = false;
                billingInfo.UpdatedBy = Log.UserId.ToString();
                billingInfo.UpdatedOn = new LoginInfo().CurrentTime;


                billingInfoManager.SaveOrUpdateDelete(existBillingInfo);
            }


            return Repository.Done();
        }

        public List<FloorRentInfo> TenantHistory(long Id)
        {
            var historyList = dbContext.FloorRentInfos.Where(x => x.TenantInfoId == Id).Include(x => x.TenantInfo).ToList();
            return historyList;
        }
    }
}
