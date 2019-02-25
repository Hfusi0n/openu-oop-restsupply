namespace RestSupplyDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_10_cascade_del_migration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.KitchenIngredientsSet", "IngredientId", "dbo.Ingredients");
            DropForeignKey("dbo.SupplierOrderDetails", "IngredientId", "dbo.Ingredients");
            DropForeignKey("dbo.MenuIngredients", "IngredientId", "dbo.Ingredients");
            DropForeignKey("dbo.KitchenUsers", "UserId", "dbo.Users");
            AddForeignKey("dbo.KitchenIngredientsSet", "IngredientId", "dbo.Ingredients", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SupplierOrderDetails", "IngredientId", "dbo.Ingredients", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MenuIngredients", "IngredientId", "dbo.Ingredients", "Id", cascadeDelete: true);
            AddForeignKey("dbo.KitchenUsers", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KitchenUsers", "UserId", "dbo.Users");
            DropForeignKey("dbo.MenuIngredients", "IngredientId", "dbo.Ingredients");
            DropForeignKey("dbo.SupplierOrderDetails", "IngredientId", "dbo.Ingredients");
            DropForeignKey("dbo.KitchenIngredientsSet", "IngredientId", "dbo.Ingredients");
            AddForeignKey("dbo.KitchenUsers", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.MenuIngredients", "IngredientId", "dbo.Ingredients", "Id");
            AddForeignKey("dbo.SupplierOrderDetails", "IngredientId", "dbo.Ingredients", "Id");
            AddForeignKey("dbo.KitchenIngredientsSet", "IngredientId", "dbo.Ingredients", "Id");
        }
    }
}
