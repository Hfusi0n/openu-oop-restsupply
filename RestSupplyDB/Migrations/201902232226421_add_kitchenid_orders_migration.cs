namespace RestSupplyDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_kitchenid_orders_migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SupplierOrders", "KitchenId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SupplierOrders", "KitchenId");
        }
    }
}
