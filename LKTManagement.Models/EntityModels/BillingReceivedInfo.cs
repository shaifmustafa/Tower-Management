using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LKTManagement.Models.EntityModels
{
    public class BillingReceivedInfo : TrackInfo
    {
        public Int64 Id { get; set; }

        public Int64 TenantInfoId { get; set; }

        public TenantInfo TenantInfo { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public string Purpose { get; set; }

        public string Note { get; set; }
    }
}
