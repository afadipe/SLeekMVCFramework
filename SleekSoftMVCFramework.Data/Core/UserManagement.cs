using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleekSoftMVCFramework.Data.Core
{

    public class UserRoleInfo
    {
        public Int64 RoleId { get;set;}
        public string Name { get; set; }
    }
    public class UserPremissionAndRole
    {
        public string PermissionName { get; set; }

        public string PermissionCode { get; set; }

        public string RoleName { get; set; }
    }
}
