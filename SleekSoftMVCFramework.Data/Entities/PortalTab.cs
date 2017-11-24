using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SleekSoftMVCFramework.Data.EntityBase;

namespace SleekSoftMVCFramework.Data.Entities
{
    public class PortalTab: Entity<int>
    {
        public string ContentUrl { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public string LeftPaneUrl { get; set; }
        public string ModuleTitle { get; set; }
       // public int PortalTabId { get; set; }
        public string RightPaneUrl { get; set; }
        public string Roles { get; set; }
        public int TabOrder { get; set; }
        public int TabParentId { get; set; }
        public int TabType { get; set; }
        public string Title { get; set; }
    }
}
