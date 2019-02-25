namespace RestSupplyDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_7_cascade_del_migration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SupplierOrders", "KitchenId", "dbo.KitchensSet");
            AddForeignKey("dbo.SupplierOrders", "KitchenId", "dbo.KitchensSet", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SupplierOrders", "KitchenId", "dbo.KitchensSet");
            AddForeignKey("dbo.SupplierOrders", "KitchenId", "dbo.KitchensSet", "Id");
        }
    }
}
