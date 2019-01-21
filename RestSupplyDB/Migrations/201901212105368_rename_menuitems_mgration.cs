namespace RestSupplyDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rename_menuitems_mgration : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.MenuIngredients", new[] { "IngredientId" });
            DropIndex("dbo.MenuIngredients", new[] { "MenuItemId" });
            AlterColumn("dbo.MenuIngredients", "IngredientId", c => c.Int(nullable: false));
            AlterColumn("dbo.MenuIngredients", "MenuItemId", c => c.Int(nullable: false));
            CreateIndex("dbo.MenuIngredients", "IngredientId");
            CreateIndex("dbo.MenuIngredients", "MenuItemId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.MenuIngredients", new[] { "MenuItemId" });
            DropIndex("dbo.MenuIngredients", new[] { "IngredientId" });
            AlterColumn("dbo.MenuIngredients", "MenuItemId", c => c.Int());
            AlterColumn("dbo.MenuIngredients", "IngredientId", c => c.Int());
            CreateIndex("dbo.MenuIngredients", "MenuItemId");
            CreateIndex("dbo.MenuIngredients", "IngredientId");
        }
    }
}
