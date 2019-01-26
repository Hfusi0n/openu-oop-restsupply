namespace RestSupplyDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class string_to_datetime_migration : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.IngredientListOrders", newName: "SupplierOrderDetails");
            RenameTable(name: "dbo.IngredientOrders", newName: "SupplierOrders");
            AddColumn("dbo.SupplierOrderDetails", "Amount", c => c.Double(nullable: false));
            AlterColumn("dbo.SupplierOrders", "Time", c => c.DateTime(nullable: false));
            DropColumn("dbo.SupplierOrderDetails", "MoneyAmount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SupplierOrderDetails", "MoneyAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.SupplierOrders", "Time", c => c.String(nullable: false));
            DropColumn("dbo.SupplierOrderDetails", "Amount");
            RenameTable(name: "dbo.SupplierOrders", newName: "IngredientOrders");
            RenameTable(name: "dbo.SupplierOrderDetails", newName: "IngredientListOrders");
        }
    }
}
