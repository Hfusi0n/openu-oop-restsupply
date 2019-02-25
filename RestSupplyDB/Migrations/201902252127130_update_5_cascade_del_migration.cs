namespace RestSupplyDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_5_cascade_del_migration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CustomerDetailOrders", "MenuItemId", "dbo.MenuItemsSet");
            AddForeignKey("dbo.CustomerDetailOrders", "MenuItemId", "dbo.MenuItemsSet", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerDetailOrders", "MenuItemId", "dbo.MenuItemsSet");
            AddForeignKey("dbo.CustomerDetailOrders", "MenuItemId", "dbo.MenuItemsSet", "Id");
        }
    }
}
