namespace RestSupplyDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixed_many_to_many_supplier_ingredient_migration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SuppliersIngredients", "SupplierRefId", "dbo.Suppliers");
            DropForeignKey("dbo.SuppliersIngredients", "IngredientRefId", "dbo.Ingredients");
            DropIndex("dbo.SuppliersIngredients", new[] { "SupplierRefId" });
            DropIndex("dbo.SuppliersIngredients", new[] { "IngredientRefId" });
            CreateTable(
                "dbo.SupliersIngredients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SupplierId = c.Int(nullable: false),
                        IngredientId = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId)
                .ForeignKey("dbo.Ingredients", t => t.IngredientId)
                .Index(t => t.SupplierId)
                .Index(t => t.IngredientId);
            
            DropTable("dbo.SuppliersIngredients");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SuppliersIngredients",
                c => new
                    {
                        SupplierRefId = c.Int(nullable: false),
                        IngredientRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SupplierRefId, t.IngredientRefId });
            
            DropForeignKey("dbo.SupliersIngredients", "IngredientId", "dbo.Ingredients");
            DropForeignKey("dbo.SupliersIngredients", "SupplierId", "dbo.Suppliers");
            DropIndex("dbo.SupliersIngredients", new[] { "IngredientId" });
            DropIndex("dbo.SupliersIngredients", new[] { "SupplierId" });
            DropTable("dbo.SupliersIngredients");
            CreateIndex("dbo.SuppliersIngredients", "IngredientRefId");
            CreateIndex("dbo.SuppliersIngredients", "SupplierRefId");
            AddForeignKey("dbo.SuppliersIngredients", "IngredientRefId", "dbo.Ingredients", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SuppliersIngredients", "SupplierRefId", "dbo.Suppliers", "Id", cascadeDelete: true);
        }
    }
}
