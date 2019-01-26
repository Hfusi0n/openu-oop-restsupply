namespace RestSupplyDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class unified_duplicate_time_date_migration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SupplierOrders", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.SupplierOrders", "Time");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SupplierOrders", "Time", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SupplierOrders", "Date", c => c.String(nullable: false));
        }
    }
}
