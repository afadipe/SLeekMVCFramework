using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SleekSoftMVCFramework.ViewModel
{
    public class ActivitlogSearchInfo
    {
        [Display(Name = "Controller")]
        public string SelectedController { get; set; }
      
        public IEnumerable<System.Web.Mvc.SelectListItem> Contollers { get; set; }

        public DateTime SelectedStartDate { get; set; }

        public DateTime SelectedEndDate { get; set; }

        [Display(Name = "Controller")]
        public string SelectedUser { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> Users { get; set; }
    }
  
}
