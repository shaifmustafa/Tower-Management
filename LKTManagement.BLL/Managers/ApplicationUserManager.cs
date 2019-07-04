using LKTManagement.BLL.Base;
using LKTManagement.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LKTManagement.Repository.Base;
using LKTManagement.Repository.Repositories;

namespace LKTManagement.BLL.Managers
{
    public class ApplicationUserManager : Manager<ApplicationUser>
    {
        LoginInfo Log = new LoginInfo();
        public ApplicationUserManager() : base(new ApplicationUserRepository())
        {

        }

        public override bool SaveOrUpdate(ApplicationUser entity)
        {
            
           var exist = Repository.GetById(entity.Id);
           entity.CreatedOn = exist.CreatedOn;
           //entity.IsActive = exist.IsActive;
           //entity.UpdatedBy = Log.UserId.ToString();
           //entity.UpdatedOn = new LoginInfo().CurrentTime;
           //entity.Name = exist.Name;
           //entity.UserName = exist.UserName;
           //entity.PhoneNumber = exist.PhoneNumber;
           //entity.Password = exist.Password;

            Repository.SaveOrUpdate(entity);
           return Repository.Done();
        }
    }
}
