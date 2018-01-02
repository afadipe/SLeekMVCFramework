namespace SleekSoftMVCFramework.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_framework_default_tbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivityLog",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(),
                        ModuleName = c.String(nullable: false),
                        ModuleAction = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Record = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRole",
                c => new
                    {
                        AspNetRoleId = c.Long(nullable: false, identity: true),
                        DateCreated = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.AspNetRoleId)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRole",
                c => new
                    {
                        AspNetUserId = c.Long(nullable: false),
                        AspNetRoleId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.AspNetUserId, t.AspNetRoleId })
                .ForeignKey("dbo.AspNetRole", t => t.AspNetRoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.AspNetUserId, cascadeDelete: true)
                .Index(t => t.AspNetUserId)
                .Index(t => t.AspNetRoleId);
            
            CreateTable(
                "dbo.Application",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationName = c.String(nullable: false),
                        Description = c.String(),
                        TermsAndConditions = c.String(),
                        HasAdminUserConfigured = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaim",
                c => new
                    {
                        AspNetUserClaimId = c.Int(nullable: false),
                        AspNetUserId = c.Long(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => new { t.AspNetUserClaimId, t.AspNetUserId })
                .ForeignKey("dbo.AspNetUsers", t => t.AspNetUserId, cascadeDelete: true)
                .Index(t => t.AspNetUserId);
            
            CreateTable(
                "dbo.AspNetUserLogin",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        AspNetUserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.AspNetUserId })
                .ForeignKey("dbo.AspNetUsers", t => t.AspNetUserId, cascadeDelete: true)
                .Index(t => t.AspNetUserId);
            
            CreateTable(
                "dbo.PasswordHistory",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AspNetUserId = c.Long(nullable: false),
                        HashPassword = c.String(nullable: false),
                        PasswordSalt = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        UpdatedBy = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        AspNetUserId = c.Long(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        MiddleName = c.String(),
                        LastName = c.String(nullable: false),
                        DOB = c.DateTime(),
                        MobileNumber = c.String(),
                        Address = c.String(),
                        IsFirstLogin = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreatedBy = c.Long(),
                        DeletedBy = c.Long(),
                        UpdatedBy = c.Long(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Email = c.String(nullable: false, maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.AspNetUserId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.EmailAttachments",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        EmailLogID = c.Long(nullable: false),
                        FilePath = c.String(nullable: false),
                        FileTitle = c.String(maxLength: 50),
                        DateCreated = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmailLogs", t => t.EmailLogID, cascadeDelete: true)
                .Index(t => t.EmailLogID);
            
            CreateTable(
                "dbo.EmailLogs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Sender = c.String(nullable: false, maxLength: 1000),
                        Receiver = c.String(nullable: false, maxLength: 1000),
                        CC = c.String(maxLength: 1000),
                        BCC = c.String(),
                        Subject = c.String(nullable: false),
                        MessageBody = c.String(nullable: false),
                        HasAttachment = c.Boolean(nullable: false),
                        Retires = c.Int(nullable: false),
                        IsSent = c.Boolean(nullable: false),
                        DateSent = c.DateTime(),
                        DateToSend = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmailTemplates",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                        Body = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmailTokens",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        EmailCode = c.String(),
                        Token = c.String(),
                        PreviewText = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Permission",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Code = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        UpdatedBy = c.Long(),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PortalVersion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FrameworkName = c.String(nullable: false),
                        FrameworkVersion = c.String(nullable: false),
                        FrameworkDescription = c.String(nullable: false),
                        TargetServer = c.String(nullable: false),
                        DefaultDatabaseEngine = c.String(nullable: false),
                        PackagesUsed = c.String(nullable: false),
                        DevelopedBy = c.String(nullable: false),
                        UX = c.String(nullable: false),
                        IOC = c.String(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RolePermission",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PermissionId = c.Long(nullable: false),
                        RoleId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        UpdatedBy = c.Long(),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);




            CreateStoredProcedure("dbo.spFetchUserPermissionAndRole",
           p => new
           {
               UserId = p.Int()
           },
           body:

           @"select p.Name PermissionName,p.Code PermissionCode,r.Name RoleName
                    ,rp.PermissionId,rp.RoleId
                    from [AspNetUsers] e  
                    inner join [AspNetUserRole] ur on e.AspNetUserId=ur.AspNetUserId
                    inner join [AspNetRole] r on ur.AspNetRoleId=r.AspNetRoleId
                    inner join [RolePermission] rp on ur.AspNetRoleId=rp.RoleId
                    inner join [Permission] p on rp.PermissionId=p.Id
                    where (e.AspNetUserId =@UserId)"
       );


            CreateStoredProcedure("dbo.spPasswordHistorySelect",
              p => new
              {
                  UserId = p.Int(),
                  numberOfRecentPasswordsToKeep = p.Int()
              },
              body:
              @"SELECT TOP(@numberOfRecentPasswordsToKeep)
                    HashPassword,
                    PasswordSalt
                FROM
                    dbo.PasswordHistory
                WHERE
                    AspNetUserId = @UserId
                ORDER BY
                    DateCreated DESC"
          );



            CreateStoredProcedure("dbo.spPasswordHistoryDeleteNonRecentPasswords",
               p => new
               {
                   UserId = p.Int(),
                   numberOfRecentPasswordsToKeep = p.Int()
               },
               body:

               @"DECLARE @minimumDate DATETIME
               SELECT @minimumDate = MIN(DateCreated) FROM  dbo.PasswordHistory
               WHERE
                AspNetUserId = @userId
                AND DateCreated IN
                (
                    SELECT
                        TOP(@numberOfRecentPasswordsToKeep) DateCreated
                    FROM
                        dbo.PasswordHistory
                    WHERE
                        AspNetUserId = @UserId
                    ORDER BY
                        DateCreated DESC
                )
             IF(@minimumDate IS NOT NULL)
            BEGIN
                DELETE FROM dbo.PasswordHistory
                WHERE AspNetUserId = @UserId AND DateCreated < @minimumDate
            END"
           );


            CreateStoredProcedure("dbo.spDeletePermissionByRoleID",
                p => new
                {
                    RoleId = p.Int()
                },
                body:
                @"delete from RolePermission where RoleId= @RoleId"
            );


            CreateStoredProcedure("dbo.SpGetActivityLog",
              p => new
              {
                  UserId = p.Int(0),
                  ControllerName = p.String(null),
                  StartDate = p.DateTime(null),
                  EndDate = p.DateTime(null)
              },
              body:
              @"select  isnull(e.FirstName + ' ' + e.MiddleName + ' ' + e.LastName,'N/A') FullName,
	              a.Id,
                  a.[UserID]
                  ,a.[Description]
                  ,a.[DateCreated]
                  ,a.[ModuleName]
                  ,a.[ModuleAction]
                  ,isnull(a.[Record],'N/A') Record
              FROM [dbo].[ActivityLog] a
              left outer join [dbo].[AspNetUsers] e on  a.[UserID]=e.AspNetUserId
              where (@UserId=0 or e.AspNetUserId=@UserId)
              and (@ControllerName='' or a.ModuleName=@ControllerName)
              and  
                (a.DateCreated >= @StartDate OR @StartDate is null)
                AND
                (a.DateCreated  <= @EndDate OR  @EndDate is null)
              order by a.Id desc");


            CreateStoredProcedure("dbo.SpGetAllActivityLog",
              body:
              @"select  isnull(e.FirstName + ' ' + e.MiddleName + ' ' + e.LastName,'N/A') FullName,
	            a.Id,
                a.[UserID]
                ,a.[Description]
                ,a.[DateCreated]
                ,a.[ModuleName]
                ,a.[ModuleAction]
                ,isnull(a.[Record],'N/A') Record
            FROM [dbo].[ActivityLog] a
            left outer join [dbo].[AspNetUsers] e on  a.[UserID]=e.AspNetUserId
            order by a.Id desc");

            CreateStoredProcedure("dbo.SpGetUserRole",
       p => new
       {
           UserId = p.Int(0)
       },
       body:
       @"select  
	              a.AspNetRoleId RoleId
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
             @"Delete from AspNetUserRole  where AspNetRoleId=@UserId");

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmailAttachments", "EmailLogID", "dbo.EmailLogs");
            DropForeignKey("dbo.AspNetUserRole", "AspNetUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogin", "AspNetUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaim", "AspNetUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRole", "AspNetRoleId", "dbo.AspNetRole");
            DropIndex("dbo.EmailAttachments", new[] { "EmailLogID" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserLogin", new[] { "AspNetUserId" });
            DropIndex("dbo.AspNetUserClaim", new[] { "AspNetUserId" });
            DropIndex("dbo.AspNetUserRole", new[] { "AspNetRoleId" });
            DropIndex("dbo.AspNetUserRole", new[] { "AspNetUserId" });
            DropIndex("dbo.AspNetRole", "RoleNameIndex");
            DropTable("dbo.RolePermission");
            DropTable("dbo.PortalVersion");
            DropTable("dbo.Permission");
            DropTable("dbo.EmailTokens");
            DropTable("dbo.EmailTemplates");
            DropTable("dbo.EmailLogs");
            DropTable("dbo.EmailAttachments");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.PasswordHistory");
            DropTable("dbo.AspNetUserLogin");
            DropTable("dbo.AspNetUserClaim");
            DropTable("dbo.Application");
            DropTable("dbo.AspNetUserRole");
            DropTable("dbo.AspNetRole");
            DropTable("dbo.ActivityLog");



            DropStoredProcedure("dbo.spFetchUserPermissionAndRole");
            DropStoredProcedure("dbo.spPasswordHistorySelect");
            DropStoredProcedure("dbo.spPasswordHistoryDeleteNonRecentPasswords");
            DropStoredProcedure("dbo.spDeletePermissionByRoleID");

            DropStoredProcedure("dbo.SpGetActivityLog");
            DropStoredProcedure("dbo.SpGetAllActivityLog");
            DropStoredProcedure("dbo.SpGetUserRole");
            DropStoredProcedure("dbo.SpDeleteUserRoleByUserId");
        }
    }
}
