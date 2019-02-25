namespace RestSupplyDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_2_cascade_del_migration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SupplierOrders", "SupplierId", "dbo.Suppliers");
            AddForeignKey("dbo.SupplierOrders", "SupplierId", "dbo.Suppliers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SupplierOrders", "SupplierId", "dbo.Suppliers");
            AddForeignKey("dbo.SupplierOrders", "SupplierId", "dbo.Suppliers", "Id");
        }
    }
}
