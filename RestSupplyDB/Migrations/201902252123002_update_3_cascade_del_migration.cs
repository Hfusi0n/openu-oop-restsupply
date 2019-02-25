namespace RestSupplyDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_3_cascade_del_migration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CustomerDetailOrders", "OrderId", "dbo.CustomerOrders");
            AddForeignKey("dbo.CustomerDetailOrders", "OrderId", "dbo.CustomerOrders", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerDetailOrders", "OrderId", "dbo.CustomerOrders");
            AddForeignKey("dbo.CustomerDetailOrders", "OrderId", "dbo.CustomerOrders", "Id");
        }
    }
}
