using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LKTManagement.Models.EntityModels;
using LKTManagement.Repository.Base;
using LKTManagement.Repository.DatabaseContext;

namespace LKTManagement.Repository.Repositories
{
    public class BillingReceivedInfoRepository : Repository<BillingReceivedInfo>
    {
        public BillingReceivedInfoRepository() : base(new LKTManagementDbContext())
        {
        }
    }
}
