namespace RestSupplyDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class unified_date_and_time_customerorders_migration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CustomerOrders", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.CustomerOrders", "Time");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CustomerOrders", "Time", c => c.String(nullable: false));
            AlterColumn("dbo.CustomerOrders", "Date", c => c.String(nullable: false));
        }
    }
}
