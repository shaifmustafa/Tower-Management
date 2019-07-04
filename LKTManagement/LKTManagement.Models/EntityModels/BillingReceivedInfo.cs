using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LKTManagement.Models.EntityModels
{
    public class BillingReceivedInfo : TrackInfo
    {
        public int Id { get; set; }
        public int TenantInfoId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Purpose { get; set; }
        public string Note { get; set; }
    }
}
