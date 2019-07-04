using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LKTManagement.Models.EntityModels
{
    
    public class TrackInfo
    {
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsActive { get; set; }
    }
    public class LoginInfo
    {
        public Int64 UserId { get; set; }
        public DateTime CurrentTime { get { return DateTime.UtcNow.AddHours(6); } }
    }
    public class CommonList
    {
        public List<string> BillingParticulars = new List<string> { "Rent", "Common Bill", "WASA Bill", "Electricity Bill", "Emergency Electricity Bill" };
        public List<KeyValuePair<string,int>> list = new List<KeyValuePair<string, int>>() {
            new KeyValuePair<string, int>("January", 1),
            new KeyValuePair<string, int>("February", 2),
            new KeyValuePair<string, int>("March", 3),
            new KeyValuePair<string, int>("April", 4),
            new KeyValuePair<string, int>("May", 5),
            new KeyValuePair<string, int>("June", 6),
            new KeyValuePair<string, int>("July", 7),
            new KeyValuePair<string, int>("Augest", 8),
            new KeyValuePair<string, int>("September", 9),
            new KeyValuePair<string, int>("October", 10),
            new KeyValuePair<string, int>("November", 11),
            new KeyValuePair<string, int>("December", 12)
        };
    }
    
    
}
