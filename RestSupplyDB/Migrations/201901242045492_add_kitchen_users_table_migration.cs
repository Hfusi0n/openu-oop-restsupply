namespace RestSupplyDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_kitchen_users_table_migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KitchenUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        KitchenId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.KitchensSet", t => t.KitchenId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.KitchenId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KitchenUsers", "KitchenId", "dbo.KitchensSet");
            DropForeignKey("dbo.KitchenUsers", "UserId", "dbo.Users");
            DropIndex("dbo.KitchenUsers", new[] { "KitchenId" });
            DropIndex("dbo.KitchenUsers", new[] { "UserId" });
            DropTable("dbo.KitchenUsers");
        }
    }
}
