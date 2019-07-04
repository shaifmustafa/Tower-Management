using LKTManagement.Models.EntityModels;
using LKTManagement.Repository.Base;
using LKTManagement.Repository.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LKTManagement.Repository.Repositories
{    
    public class ApplicationUserRepository : Repository<ApplicationUser>
    {
        public ApplicationUserRepository() : base(new LKTManagementDbContext())
        {
        }
    }
}
