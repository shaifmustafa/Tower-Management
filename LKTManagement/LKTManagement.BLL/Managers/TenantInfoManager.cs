using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LKTManagement.Models.EntityModels;
using LKTManagement.BLL.Base;
using LKTManagement.Repository.Base;
using LKTManagement.Repository.Repositories;

namespace LKTManagement.BLL.Managers
{
    public class TenantInfoManager : Manager<TenantInfo>
    {
        public TenantInfoManager() : base(new TenantInfoRepository())
        {
        }

        //public override bool Save(TenantInfo entity)
        //{
            
        //}
    }
}
