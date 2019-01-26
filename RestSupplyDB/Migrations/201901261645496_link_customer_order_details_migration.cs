namespace RestSupplyDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class link_customer_order_details_migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerDetailOrders", "OrderId", c => c.Int(nullable: false));
            CreateIndex("dbo.CustomerDetailOrders", "OrderId");
            AddForeignKey("dbo.CustomerDetailOrders", "OrderId", "dbo.CustomerOrders", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerDetailOrders", "OrderId", "dbo.CustomerOrders");
            DropIndex("dbo.CustomerDetailOrders", new[] { "OrderId" });
            DropColumn("dbo.CustomerDetailOrders", "OrderId");
        }
    }
}
