namespace RestSupplyDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_1_cascade_del_migration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SupliersIngredients", "IngredientId", "dbo.Ingredients");
            DropForeignKey("dbo.SupliersIngredients", "SupplierId", "dbo.Suppliers");
            AddForeignKey("dbo.SupliersIngredients", "IngredientId", "dbo.Ingredients", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SupliersIngredients", "SupplierId", "dbo.Suppliers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SupliersIngredients", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.SupliersIngredients", "IngredientId", "dbo.Ingredients");
            AddForeignKey("dbo.SupliersIngredients", "SupplierId", "dbo.Suppliers", "Id");
            AddForeignKey("dbo.SupliersIngredients", "IngredientId", "dbo.Ingredients", "Id");
        }
    }
}
