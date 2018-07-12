
namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class role : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Customers", newName: "Roles");
            CreateTable(
                "dbo.RoleViewModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Roles", "IsEmployee", c => c.Boolean(nullable: false));
            AddColumn("dbo.Roles", "IsAdmin", c => c.Boolean());
            AddColumn("dbo.Roles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.AspNetRoles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Roles", "ZipCode", c => c.Int());
            DropTable("dbo.Employees");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Roles", "ZipCode", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetRoles", "Discriminator");
            DropColumn("dbo.Roles", "Discriminator");
            DropColumn("dbo.Roles", "IsAdmin");
            DropColumn("dbo.Roles", "IsEmployee");
            DropTable("dbo.RoleViewModels");
            RenameTable(name: "dbo.Roles", newName: "Customers");
        }
    }
}
