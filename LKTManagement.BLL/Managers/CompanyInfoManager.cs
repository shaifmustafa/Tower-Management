using LKTManagement.BLL.Base;
using LKTManagement.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LKTManagement.Repository.Base;
using LKTManagement.Repository.Repositories;
using System.Web;
using LKTManagement.Models.EntityModels.VM;

namespace LKTManagement.BLL.Managers
{
    public class CompanyInfoManager : Manager<CompanyUser>
    {

        LoginInfo log = new LoginInfo();
        public CompanyInfoManager() : base(new CompanyInfoRepository())
        {

        }

        public override bool SaveOrUpdate(CompanyUser entity)
        {

            var exist = Repository.GetById(entity.Id);                        
            
            if(exist != null)
            {
                entity.Name = exist.Name;
                entity.Address = exist.Address;
                entity.Email = exist.Email;
                entity.Phone = exist.Phone;
                entity.ProfilePicture = exist.ProfilePicture;
                entity.CreatedOn = exist.CreatedOn;
                entity.IsActive = exist.IsActive;
                entity.UpdatedBy = log.UserId.ToString();
                entity.UpdatedOn = new LoginInfo().CurrentTime;
            }

            else
            {
                entity.CreatedOn = System.DateTime.Now;
                entity.IsActive = true;
                entity.UpdatedBy = log.UserId.ToString();
                entity.UpdatedOn = new LoginInfo().CurrentTime;
            }

            Repository.SaveOrUpdate(entity);
            return Repository.Done();
        }
    }
}
