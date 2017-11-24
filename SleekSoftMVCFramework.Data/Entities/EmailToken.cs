
using SleekSoftMVCFramework.Data.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleekSoftMVCFramework.Data.Entities
{
   public class EmailToken : Entity<long>
    {
        public string EmailCode { get; set; }
        public string Token { get; set; }
        public string PreviewText { get; set; }

       
    }
}
