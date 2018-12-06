namespace RestSupplyDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove_roles : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RolesSet", "Id", "dbo.AspNetRoles");
            DropIndex("dbo.RolesSet", new[] { "Id" });
            DropTable("dbo.RolesSet");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RolesSet",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.RolesSet", "Id");
            AddForeignKey("dbo.RolesSet", "Id", "dbo.AspNetRoles", "Id");
        }
    }
}
