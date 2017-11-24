namespace SleekSoftMVCFramework.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_appRole_tbl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetRole", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetRole", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetRole", "IsDeleted", c => c.Boolean(nullable: false));

            CreateStoredProcedure("dbo.SpGetUserRole",
             p => new
             {
                 UserId = p.Int(0)
             },
             body:
             @"select  
	              a.AspNetRoleId RoleId,
                  ,e.[Name] 
              FROM [dbo].[AspNetUserRole] a
              inner join [dbo].[AspNetRole] e on  a.[AspNetRoleId]=e.AspNetRoleId
              where (@UserId=0 or a.AspNetUserId=@UserId)
              ");


            CreateStoredProcedure("dbo.SpDeleteUserRoleByUserId",
             p => new
             {
                 UserId = p.Int(0)
             },
             body:
             @"Delete from AspNetUserRole ur where ur.AspNetRoleId=@UserId");

        }

        public override void Down()
        {
            DropColumn("dbo.AspNetRole", "IsDeleted");
            DropColumn("dbo.AspNetRole", "IsActive");
            DropColumn("dbo.AspNetRole", "DateCreated");
            DropStoredProcedure("dbo.SpGetUserRole");
            DropStoredProcedure("dbo.SpDeleteUserRoleByUserId");
        }
    }
}
