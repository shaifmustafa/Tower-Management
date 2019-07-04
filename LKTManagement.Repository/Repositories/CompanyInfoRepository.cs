using LKTManagement.Models.EntityModels;
using LKTManagement.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using LKTManagement.Repository.DatabaseContext;

namespace LKTManagement.Repository.Repositories
{
    public class CompanyInfoRepository : Repository<CompanyUser>
    {
        public CompanyInfoRepository() : base(new LKTManagementDbContext())
        {

        }
    }
}
