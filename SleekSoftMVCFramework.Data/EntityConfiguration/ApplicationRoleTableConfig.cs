using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using SleekSoftMVCFramework.Data.IdentityModel;

namespace SleekSoftMVCFramework.Data.EntityConfiguration
{
    public class ApplicationRoleTableConfig
        : EntityTypeConfiguration<ApplicationRole>
    {
        public ApplicationRoleTableConfig()
        {
           // ToTable("AspNetRoles");
            this.ToTable(tableName: "AspNetRole");
            // Primary Key
            HasKey(r => r.Id);

           // Property(r => r.Id).IsRequired().HasColumnName("Id");
            this.Property(e => e.Id).IsRequired().HasColumnName("AspNetRoleId");

            Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnName("Name")
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(new IndexAttribute("RoleNameIndex")
                    {
                        IsUnique = true
                    }));

            this.Property(m => m.DateCreated).IsRequired();
            Property(u => u.IsDeleted).IsRequired();
            Property(u => u.IsActive).IsRequired();
            // Navigation
            HasMany(r => r.Users)
                .WithRequired()
                .HasForeignKey(ur => ur.RoleId)
                .WillCascadeOnDelete();
        }
    }
}