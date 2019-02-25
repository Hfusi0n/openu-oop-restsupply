namespace RestSupplyDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_4_cascade_del_migration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SupplierOrderDetails", "OrderId", "dbo.SupplierOrders");
            AddForeignKey("dbo.SupplierOrderDetails", "OrderId", "dbo.SupplierOrders", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SupplierOrderDetails", "OrderId", "dbo.SupplierOrders");
            AddForeignKey("dbo.SupplierOrderDetails", "OrderId", "dbo.SupplierOrders", "Id");
        }
    }
}
