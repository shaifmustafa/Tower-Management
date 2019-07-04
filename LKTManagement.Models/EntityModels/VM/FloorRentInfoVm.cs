using LKTManagement.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LKTManagement.Models.VM
{
    [NotMapped]
    public class FloorRentInfoVm : FloorRentInfo
    {
           public string TenantName { get; set; }
           public string EffectiveDateText { get { return EffectiveDate.ToString("yyyy-MM-dd"); } }
           public string ExpiryDateText { get { return ExpiryDate.ToString("yyyy-MM-dd"); } }
    }
}