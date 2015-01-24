namespace FriendsDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        CommentId = c.String(nullable: false, maxLength: 50),
                        Id = c.Int(nullable: false, identity: true),
                        TypeId = c.String(nullable: false, maxLength: 50),
                        CommentMessage = c.String(nullable: false, maxLength: 50),
                        Type = c.String(nullable: false, maxLength: 50),
                        UserId = c.String(nullable: false, maxLength: 50),
                        CommentTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.UserCredential", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserCredential",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 50),
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 500),
                        LastSeen = c.DateTime(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        IsActive = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.EventInvited",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EventId = c.String(nullable: false, maxLength: 50),
                        UserId = c.String(nullable: false, maxLength: 50),
                        Attending = c.Int(),
                        Time = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .ForeignKey("dbo.UserCredential", t => t.UserId, cascadeDelete: true)
                .Index(t => t.EventId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.String(nullable: false, maxLength: 50),
                        Id = c.Int(nullable: false, identity: true),
                        EventCode = c.String(nullable: false, maxLength: 10),
                        EventType = c.Int(nullable: false),
                        Place = c.String(nullable: false, maxLength: 500),
                        Purpose = c.String(nullable: false, maxLength: 500),
                        EventTime = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(),
                        CreatedBy = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.EventId)
                .ForeignKey("dbo.UserCredential", t => t.CreatedBy, cascadeDelete: true)
                .Index(t => t.CreatedBy);
            
            CreateTable(
                "dbo.Image",
                c => new
                    {
                        ImageId = c.String(nullable: false, maxLength: 50),
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(nullable: false, maxLength: 50),
                        UserId = c.String(nullable: false, maxLength: 50),
                        Text = c.String(maxLength: 50),
                        Description = c.String(maxLength: 500),
                        UploadTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ImageId)
                .ForeignKey("dbo.UserCredential", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Like",
                c => new
                    {
                        LikeId = c.String(nullable: false, maxLength: 50),
                        Id = c.Int(nullable: false, identity: true),
                        TypeId = c.String(nullable: false, maxLength: 50),
                        Type = c.String(nullable: false, maxLength: 50),
                        UserId = c.String(nullable: false, maxLength: 50),
                        LikeType = c.Int(nullable: false),
                        Time = c.DateTime(),
                    })
                .PrimaryKey(t => t.LikeId)
                .ForeignKey("dbo.UserCredential", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        Pid = c.String(nullable: false, maxLength: 50),
                        Id = c.Int(nullable: false, identity: true),
                        PostMessage = c.String(nullable: false, maxLength: 500),
                        Author = c.String(nullable: false, maxLength: 50),
                        Recipient = c.String(maxLength: 50),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Pid)
                .ForeignKey("dbo.UserCredential", t => t.Author, cascadeDelete: true)
                .Index(t => t.Author);
            
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 50),
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        DOB = c.DateTime(nullable: false),
                        Gender = c.String(nullable: false, maxLength: 1, fixedLength: true),
                        About = c.String(maxLength: 2500),
                        StatusId = c.Int(),
                        LocationId = c.Int(),
                        Status = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.UserCredential", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 50),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.UserCredential", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false),
                        RoleName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.EventType",
                c => new
                    {
                        EventTypeId = c.Int(nullable: false),
                        EventType = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.EventTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comment", "UserId", "dbo.UserCredential");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.UserCredential");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserProfile", "UserId", "dbo.UserCredential");
            DropForeignKey("dbo.Post", "Author", "dbo.UserCredential");
            DropForeignKey("dbo.Like", "UserId", "dbo.UserCredential");
            DropForeignKey("dbo.Image", "UserId", "dbo.UserCredential");
            DropForeignKey("dbo.EventInvited", "UserId", "dbo.UserCredential");
            DropForeignKey("dbo.EventInvited", "EventId", "dbo.Events");
            DropForeignKey("dbo.Events", "CreatedBy", "dbo.UserCredential");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.UserProfile", new[] { "UserId" });
            DropIndex("dbo.Post", new[] { "Author" });
            DropIndex("dbo.Like", new[] { "UserId" });
            DropIndex("dbo.Image", new[] { "UserId" });
            DropIndex("dbo.Events", new[] { "CreatedBy" });
            DropIndex("dbo.EventInvited", new[] { "UserId" });
            DropIndex("dbo.EventInvited", new[] { "EventId" });
            DropIndex("dbo.Comment", new[] { "UserId" });
            DropTable("dbo.EventType");
            DropTable("dbo.Roles");
            DropTable("dbo.UserRoles");
            DropTable("dbo.UserProfile");
            DropTable("dbo.Post");
            DropTable("dbo.Like");
            DropTable("dbo.Image");
            DropTable("dbo.Events");
            DropTable("dbo.EventInvited");
            DropTable("dbo.UserCredential");
            DropTable("dbo.Comment");
        }
    }
}
