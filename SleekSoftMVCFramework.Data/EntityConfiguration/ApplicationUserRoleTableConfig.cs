using System.Data.Entity.ModelConfiguration;
using SleekSoftMVCFramework.Data.IdentityModel;

namespace SleekSoftMVCFramework.Data.EntityConfiguration
{
    public class ApplicationUserRoleTableConfig
        : EntityTypeConfiguration<ApplicationUserRole>
    {
        public ApplicationUserRoleTableConfig()
        {
            this.ToTable(tableName: "AspNetUserRole");
            //ToTable("AspNetUserRoles");
            // Primary Key
            HasKey(t => new {t.UserId, t.RoleId});

            this.Property(e => e.UserId).IsRequired().HasColumnName("AspNetUserId");
            this.Property(e => e.RoleId).IsRequired().HasColumnName("AspNetRoleId");

            //Property(t => t.UserId).IsRequired().HasColumnName("UserId");
            //Property(t => t.RoleId).IsRequired().HasColumnName("RoleId");

            
            
        }
    }
}