using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LKTManagement.Models.EntityModels.VM
{
    [NotMapped]
    public class TenantFloorVm : TenantInfo
    {
        public Int64 TenantId { get; set; }
        public string TenantName { get; set; }
        public string TenantNote { get; set; }
        public Int64 FloorInfoId { get; set; }
        public Int64 FloorInfoLevel { get; set; }
    }
}
