namespace RestSupplyDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_6_cascade_del_migration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MenuIngredients", "MenuItemId", "dbo.MenuItemsSet");
            AddForeignKey("dbo.MenuIngredients", "MenuItemId", "dbo.MenuItemsSet", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MenuIngredients", "MenuItemId", "dbo.MenuItemsSet");
            AddForeignKey("dbo.MenuIngredients", "MenuItemId", "dbo.MenuItemsSet", "Id");
        }
    }
}
