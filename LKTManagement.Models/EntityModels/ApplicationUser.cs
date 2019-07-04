using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LKTManagement.Models.EntityModels
{
   public class ApplicationUser : TrackInfo
    {
        public Int64 Id { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        [MaxLength(13)]
        public  string PhoneNumber { get; set; }

        [StringLength(32)]
        public string Password { get; set; }
    }
}
