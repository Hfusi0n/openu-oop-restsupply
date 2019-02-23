namespace RestSupplyDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kitchen_orders_fk_migration : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.SupplierOrders", "KitchenId");
            AddForeignKey("dbo.SupplierOrders", "KitchenId", "dbo.KitchensSet", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SupplierOrders", "KitchenId", "dbo.KitchensSet");
            DropIndex("dbo.SupplierOrders", new[] { "KitchenId" });
        }
    }
}
