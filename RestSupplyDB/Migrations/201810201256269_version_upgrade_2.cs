namespace RestSupplyDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version_upgrade_2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.IngredientListOrdersSet", "IngredientId", "dbo.IngredientsSet");
            DropForeignKey("dbo.IngredientListOrdersSet", "OrderId", "dbo.IngredientOrdersSet");
            DropIndex("dbo.IngredientListOrdersSet", new[] { "OrderId" });
            DropIndex("dbo.IngredientListOrdersSet", new[] { "IngredientId" });
            CreateTable(
                "dbo.CustomerOrdersSet",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.String(nullable: false),
                        Time = c.String(nullable: false),
                        MenuItemId = c.Int(nullable: false),
                        KitchenId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.KitchensSet", t => t.KitchenId)
                .ForeignKey("dbo.MenuItemsSet", t => t.MenuItemId, cascadeDelete: true)
                .Index(t => t.MenuItemId)
                .Index(t => t.KitchenId);
            
            AlterColumn("dbo.IngredientListOrdersSet", "OrderId", c => c.Int(nullable: false));
            AlterColumn("dbo.IngredientListOrdersSet", "IngredientId", c => c.Int(nullable: false));
            CreateIndex("dbo.IngredientListOrdersSet", "OrderId");
            CreateIndex("dbo.IngredientListOrdersSet", "IngredientId");
            AddForeignKey("dbo.IngredientListOrdersSet", "IngredientId", "dbo.IngredientsSet", "Id", cascadeDelete: true);
            AddForeignKey("dbo.IngredientListOrdersSet", "OrderId", "dbo.IngredientOrdersSet", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IngredientListOrdersSet", "OrderId", "dbo.IngredientOrdersSet");
            DropForeignKey("dbo.IngredientListOrdersSet", "IngredientId", "dbo.IngredientsSet");
            DropForeignKey("dbo.CustomerOrdersSet", "MenuItemId", "dbo.MenuItemsSet");
            DropForeignKey("dbo.CustomerOrdersSet", "KitchenId", "dbo.KitchensSet");
            DropIndex("dbo.IngredientListOrdersSet", new[] { "IngredientId" });
            DropIndex("dbo.IngredientListOrdersSet", new[] { "OrderId" });
            DropIndex("dbo.CustomerOrdersSet", new[] { "KitchenId" });
            DropIndex("dbo.CustomerOrdersSet", new[] { "MenuItemId" });
            AlterColumn("dbo.IngredientListOrdersSet", "IngredientId", c => c.Int());
            AlterColumn("dbo.IngredientListOrdersSet", "OrderId", c => c.Int());
            DropTable("dbo.CustomerOrdersSet");
            CreateIndex("dbo.IngredientListOrdersSet", "IngredientId");
            CreateIndex("dbo.IngredientListOrdersSet", "OrderId");
            AddForeignKey("dbo.IngredientListOrdersSet", "OrderId", "dbo.IngredientOrdersSet", "Id");
            AddForeignKey("dbo.IngredientListOrdersSet", "IngredientId", "dbo.IngredientsSet", "Id");
        }
    }
}
