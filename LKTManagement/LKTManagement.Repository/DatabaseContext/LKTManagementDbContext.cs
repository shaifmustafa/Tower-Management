using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LKTManagement.Models.EntityModels;

namespace LKTManagement.Repository.DatabaseContext
{
    public class LKTManagementDbContext : DbContext
    {
        public DbSet<TenantInfo> TenantInfos { get; set; }
        public DbSet<BillingInfoPerMonth> BillingInfoPerMonths { get; set; }
        public DbSet<BillingReceivedInfo> BillingReceivedInfos { get; set; }
        public DbSet<FloorRentInfo> FloorRentInfos { get; set; } 
    }
}
