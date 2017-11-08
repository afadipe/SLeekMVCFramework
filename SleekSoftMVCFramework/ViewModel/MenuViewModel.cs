using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SleekSoftMVCFramework.ViewModel
{
    public class MenuContract
    {
        [Required(ErrorMessage = "* Required")]
        [DisplayName("Tab Title")]
        public string Title { get; set; }

        [ScaffoldColumn(false)]
        public int PortalTabId { get; set; }


        public string ContentUrl { get; set; }
        public string LeftPaneUrl { get; set; }
        public string RightPaneUrl { get; set; }
        public string IconUrl { get; set; }
        public string MainUrl { get; set; }

        public string Description { get; set; }
        public int TabParentId { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> TabParentList { get; set; }

        public int TabType { get; set; }

        public string TabTypeName
        {
            get
            {
                if (TabType < 1)
                {
                    return "None";
                }
                var type = string.Empty;
                    //TabTypeManager.GetList().Find(m => m.Id == TabType);
                return type == null ? "None" : "";
            }
        }

        public string TabParent
        {
            get
            {
                if (TabParentId < 1)
                {
                    return "ROOT";
                }
                string msg;
                var type = string.Empty;
                //PortalTabManager.LoadTab(TabParentId, out msg);
                return type == null ? "None" : string.Empty;
            }
        }

        public int TabOrder { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> TabOrderList { get; set; }

        public string ModuleTitle { get; set; }
        public string[] Roles { get; set; }

        public string RoleItems
        {
            get
            {
                if (Roles == null)
                {
                    return "";
                }
                return !Roles.Any() ? "" : string.Join(",", Roles);
            }
        }
        public bool IsCurrentlyActive { get; set; }

        public string ContentActionName { get; set; }
        public string ContentControllerName { get; set; }
        public string ContentAreaName { get; set; }

        public string LeftPanelActionName { get; set; }
        public string LeftPanelControllerName { get; set; }
        public string LeftPanelAreaName { get; set; }

        public string RightPanelActionName { get; set; }
        public string RightPanelControllerName { get; set; }
        public string RightPanelAreaName { get; set; }
    }

    public class RouteContract
    {
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string AreaName { get; set; }
    }
}