using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SleekSoftMVCFramework.Utilities.ValidationHelper;
using SleekSoftMVCFramework.Data.IdentityModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SleekSoftMVCFramework.ViewModel
{
    public class UserViewModel
    {
        public Int64 Id { get; set; }

        [Required(ErrorMessage = "* Required")]
        [DisplayName("First Name")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "* Required")]
        [DisplayName("Last Name")]
        [StringLength(50)]
        public string LastName { get; set; }

        [DisplayName("Middle Name")]
        [StringLength(50)]
        public string MiddleName { get; set; }


        [Required(ErrorMessage = "* Required")]
        [DisplayName("User Name")]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "* Required")]
        [DisplayName("Mobile Number")]
        [StringLength(50)]
        public string MobileNumber { get; set; }

        [DisplayName("Phone Number")]
        [StringLength(50)]
        public string PhoneNumber { get; set; }

        [DisplayName("Date of Birth")]
        public DateTime? DOB { get; set; }

        [DisplayName("Home Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "* Required")]
        [DisplayName("Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Email is not valid.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Role(s)")]
        public IEnumerable<string> SelectedRole { get; set; }
        public IEnumerable<long> SelectedRoleId { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> Roles { get; set; }

        [NotMapped]
        public virtual ICollection<ApplicationUserRole> UserRoles
        {
            get;set;
        }

        public static ApplicationUser ModeltoEntity(UserViewModel model)
        {
            return model == null
               ? null
               : new ApplicationUser
               {
                   FirstName = model.FirstName,
                   LastName = model.LastName,
                   MiddleName = model.MiddleName,
                   UserName = model.UserName,
                   MobileNumber = model.MobileNumber,
                   PhoneNumber = model.PhoneNumber,
                   Email = model.Email,
                   DOB = model.DOB.HasValue ? model.DOB : null,
                   Address = model.Address,
                   EmailConfirmed = true,
                   PhoneNumberConfirmed = true,
                   TwoFactorEnabled = false,
                   LockoutEnabled = false,
                   AccessFailedCount = 0,
                   DateCreated = DateTime.Now,
                   IsFirstLogin = true
               };
        }
        public static UserViewModel EntityToModels(ApplicationUser dbmodel)
        {
            return dbmodel == null
                ? null
                : new UserViewModel
                {
                    Id = dbmodel.Id,
                    FirstName = dbmodel.FirstName,
                    LastName = dbmodel.LastName,
                    MiddleName = dbmodel.MiddleName,
                    UserName = dbmodel.UserName,
                    MobileNumber = dbmodel.MobileNumber,
                    PhoneNumber = dbmodel.PhoneNumber,
                    Email = dbmodel.Email,
                    DOB=dbmodel.DOB.HasValue ? dbmodel.DOB.Value :DateTime.Now.Date,
                    Address=dbmodel.Address,
                    UserRoles=dbmodel.Roles
                };
        }
    }
}