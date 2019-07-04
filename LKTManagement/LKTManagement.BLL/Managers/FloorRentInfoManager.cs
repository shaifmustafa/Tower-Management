using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LKTManagement.BLL.Base;
using LKTManagement.Models.EntityModels;
using LKTManagement.Repository.Base;
using LKTManagement.Repository.Repositories;

namespace LKTManagement.BLL.Managers
{
    public class FloorRentInfoManager : Manager<FloorRentInfo>
    {
        public FloorRentInfoManager() : base(new FloorRentInfoRepository())
        {
        }
    }
}
