namespace RestSupplyDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version_upgrade_3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CustomerOrdersSet", "MenuItemId", "dbo.MenuItemsSet");
            DropForeignKey("dbo.CustomerOrdersSet", "KitchenId", "dbo.KitchensSet");
            DropForeignKey("dbo.IngredientListOrdersSet", "IngredientId", "dbo.IngredientsSet");
            DropForeignKey("dbo.IngredientListOrdersSet", "OrderId", "dbo.IngredientOrdersSet");
            DropForeignKey("dbo.IngredientOrdersSet", "SupplierId", "dbo.SuppliersSet");
            DropIndex("dbo.CustomerOrdersSet", new[] { "MenuItemId" });
            CreateTable(
                "dbo.CustomerDetailOrdersSet",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MenuItemId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MenuItemsSet", t => t.MenuItemId)
                .Index(t => t.MenuItemId);
            
            AddForeignKey("dbo.CustomerOrdersSet", "KitchenId", "dbo.KitchensSet", "Id", cascadeDelete: true);
            AddForeignKey("dbo.IngredientListOrdersSet", "IngredientId", "dbo.IngredientsSet", "Id");
            AddForeignKey("dbo.IngredientListOrdersSet", "OrderId", "dbo.IngredientOrdersSet", "Id");
            AddForeignKey("dbo.IngredientOrdersSet", "SupplierId", "dbo.SuppliersSet", "Id");
            DropColumn("dbo.CustomerOrdersSet", "MenuItemId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CustomerOrdersSet", "MenuItemId", c => c.Int(nullable: false));
            DropForeignKey("dbo.IngredientOrdersSet", "SupplierId", "dbo.SuppliersSet");
            DropForeignKey("dbo.IngredientListOrdersSet", "OrderId", "dbo.IngredientOrdersSet");
            DropForeignKey("dbo.IngredientListOrdersSet", "IngredientId", "dbo.IngredientsSet");
            DropForeignKey("dbo.CustomerOrdersSet", "KitchenId", "dbo.KitchensSet");
            DropForeignKey("dbo.CustomerDetailOrdersSet", "MenuItemId", "dbo.MenuItemsSet");
            DropIndex("dbo.CustomerDetailOrdersSet", new[] { "MenuItemId" });
            DropTable("dbo.CustomerDetailOrdersSet");
            CreateIndex("dbo.CustomerOrdersSet", "MenuItemId");
            AddForeignKey("dbo.IngredientOrdersSet", "SupplierId", "dbo.SuppliersSet", "Id", cascadeDelete: true);
            AddForeignKey("dbo.IngredientListOrdersSet", "OrderId", "dbo.IngredientOrdersSet", "Id", cascadeDelete: true);
            AddForeignKey("dbo.IngredientListOrdersSet", "IngredientId", "dbo.IngredientsSet", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CustomerOrdersSet", "KitchenId", "dbo.KitchensSet", "Id");
            AddForeignKey("dbo.CustomerOrdersSet", "MenuItemId", "dbo.MenuItemsSet", "Id", cascadeDelete: true);
        }
    }
}
