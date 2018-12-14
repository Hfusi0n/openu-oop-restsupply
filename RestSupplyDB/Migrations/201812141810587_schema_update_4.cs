namespace RestSupplyDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class schema_update_4 : DbMigration
    {
        public override void Up()
        {

            DropForeignKey("dbo.IngredientsSet", "SupplierId", "dbo.SuppliersSet");
            DropForeignKey("dbo.MenuIngredientsSet", "IngredientId", "dbo.IngredientsSet");
            DropIndex("dbo.IngredientsSet", new[] { "SupplierId" });

            RenameTable(name: "dbo.IngredientListOrdersSet", newName: "IngredientListOrders");
            RenameTable(name: "dbo.IngredientOrdersSet", newName: "IngredientOrders");
            RenameTable(name: "dbo.CustomerDetailOrdersSet", newName: "CustomerDetailOrders");
            RenameTable(name: "dbo.MenuIngredientsSet", newName: "MenuIngredients");
            RenameTable(name: "dbo.CustomerOrdersSet", newName: "CustomerOrders");
            RenameTable(name: "dbo.IngredientsSet", newName: "Ingredients");
            RenameTable(name: "dbo.SuppliersSet", newName: "Suppliers");
            
            CreateTable(
                "dbo.SuppliersIngredients",
                c => new
                    {
                        SupplierRefId = c.Int(nullable: false),
                        IngredientRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SupplierRefId, t.IngredientRefId })
                .ForeignKey("dbo.Suppliers", t => t.SupplierRefId, cascadeDelete: true)
                .ForeignKey("dbo.Ingredients", t => t.IngredientRefId, cascadeDelete: true)
                .Index(t => t.SupplierRefId)
                .Index(t => t.IngredientRefId);
            
            AddColumn("dbo.IngredientListOrders", "MoneyAmount", c => c.Double(nullable: false));
            AddColumn("dbo.KitchensSet", "Address", c => c.String());
            AddColumn("dbo.MenuIngredients", "IsDeleted", c => c.Boolean(nullable: false));
            AddForeignKey("dbo.MenuIngredients", "IngredientId", "dbo.Ingredients", "Id");
            DropColumn("dbo.IngredientListOrders", "IngredientPrice");
            DropColumn("dbo.MenuIngredients", "Valid");
            DropColumn("dbo.MenuIngredients", "IngredientName");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SuppliersSet",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IngredientsSet",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Unit = c.String(nullable: false),
                        SupplierId = c.Int(nullable: false),
                        PricePerUnit = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.MenuIngredients", "IngredientName", c => c.String(nullable: false));
            AddColumn("dbo.MenuIngredients", "Valid", c => c.Boolean(nullable: false));
            AddColumn("dbo.IngredientListOrders", "IngredientPrice", c => c.Double(nullable: false));
            DropForeignKey("dbo.MenuIngredients", "IngredientId", "dbo.Ingredients");
            DropForeignKey("dbo.SuppliersIngredients", "IngredientRefId", "dbo.Ingredients");
            DropForeignKey("dbo.SuppliersIngredients", "SupplierRefId", "dbo.Suppliers");
            DropIndex("dbo.SuppliersIngredients", new[] { "IngredientRefId" });
            DropIndex("dbo.SuppliersIngredients", new[] { "SupplierRefId" });
            DropColumn("dbo.MenuIngredients", "IsDeleted");
            DropColumn("dbo.KitchensSet", "Address");
            DropColumn("dbo.IngredientListOrders", "MoneyAmount");
            DropTable("dbo.SuppliersIngredients");
            DropTable("dbo.Suppliers");
            DropTable("dbo.Ingredients");
            CreateIndex("dbo.IngredientsSet", "SupplierId");
            AddForeignKey("dbo.MenuIngredientsSet", "IngredientId", "dbo.IngredientsSet", "Id");
            AddForeignKey("dbo.IngredientsSet", "SupplierId", "dbo.SuppliersSet", "Id");
            RenameTable(name: "dbo.CustomerOrders", newName: "CustomerOrdersSet");
            RenameTable(name: "dbo.MenuIngredients", newName: "MenuIngredientsSet");
            RenameTable(name: "dbo.CustomerDetailOrders", newName: "CustomerDetailOrdersSet");
            RenameTable(name: "dbo.IngredientOrders", newName: "IngredientOrdersSet");
            RenameTable(name: "dbo.IngredientListOrders", newName: "IngredientListOrdersSet");
        }
    }
}
