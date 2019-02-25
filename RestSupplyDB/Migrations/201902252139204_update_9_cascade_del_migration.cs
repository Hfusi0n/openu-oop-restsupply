namespace RestSupplyDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_9_cascade_del_migration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.KitchenIngredientsSet", "KitchenId", "dbo.KitchensSet");
            AddForeignKey("dbo.KitchenIngredientsSet", "KitchenId", "dbo.KitchensSet", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KitchenIngredientsSet", "KitchenId", "dbo.KitchensSet");
            AddForeignKey("dbo.KitchenIngredientsSet", "KitchenId", "dbo.KitchensSet", "Id");
        }
    }
}
