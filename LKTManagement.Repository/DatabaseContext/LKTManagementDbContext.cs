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
        public LKTManagementDbContext() : base("ServerDB")
        {
            this.Configuration.ProxyCreationEnabled = false;
        }

        public static LKTManagementDbContext Create()
        {
            return new LKTManagementDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<LKTManagementDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }
        //tables

        public DbSet<CompanyUser> CompanyUsers { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<TenantInfo> TenantInfos { get; set; }

        public DbSet<BillingInfoPerMonth> BillingInfoPerMonths { get; set; }

        public DbSet<BillingReceivedInfo> BillingReceivedInfos { get; set; }

        public DbSet<FloorRentInfo> FloorRentInfos { get; set; }

       
    }
}
