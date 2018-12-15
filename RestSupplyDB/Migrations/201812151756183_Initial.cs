namespace RestSupplyDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerDetailOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MenuItemId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MenuItemsSet", t => t.MenuItemId)
                .Index(t => t.MenuItemId);
            
            CreateTable(
                "dbo.MenuItemsSet",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MenuIngredients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        IngredientId = c.Int(),
                        MenuItemId = c.Int(),
                        Quantity = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ingredients", t => t.IngredientId)
                .ForeignKey("dbo.MenuItemsSet", t => t.MenuItemId)
                .Index(t => t.IngredientId)
                .Index(t => t.MenuItemId);
            
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Unit = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IngredientListOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        IngredientId = c.Int(nullable: false),
                        MoneyAmount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IngredientOrders", t => t.OrderId)
                .ForeignKey("dbo.Ingredients", t => t.IngredientId)
                .Index(t => t.OrderId)
                .Index(t => t.IngredientId);
            
            CreateTable(
                "dbo.IngredientOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.String(nullable: false),
                        Time = c.String(nullable: false),
                        SupplierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.KitchenIngredientsSet",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Valid = c.Boolean(nullable: false),
                        KitchenId = c.Int(nullable: false),
                        IngredientId = c.Int(nullable: false),
                        MinimalQuantity = c.Double(nullable: false),
                        CurrentQuantity = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.KitchensSet", t => t.KitchenId)
                .ForeignKey("dbo.Ingredients", t => t.IngredientId)
                .Index(t => t.KitchenId)
                .Index(t => t.IngredientId);
            
            CreateTable(
                "dbo.KitchensSet",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomerOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.String(nullable: false),
                        Time = c.String(nullable: false),
                        KitchenId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.KitchensSet", t => t.KitchenId, cascadeDelete: true)
                .Index(t => t.KitchenId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.MenuIngredients", "MenuItemId", "dbo.MenuItemsSet");
            DropForeignKey("dbo.MenuIngredients", "IngredientId", "dbo.Ingredients");
            DropForeignKey("dbo.KitchenIngredientsSet", "IngredientId", "dbo.Ingredients");
            DropForeignKey("dbo.KitchenIngredientsSet", "KitchenId", "dbo.KitchensSet");
            DropForeignKey("dbo.CustomerOrders", "KitchenId", "dbo.KitchensSet");
            DropForeignKey("dbo.IngredientListOrders", "IngredientId", "dbo.Ingredients");
            DropForeignKey("dbo.SuppliersIngredients", "IngredientRefId", "dbo.Ingredients");
            DropForeignKey("dbo.SuppliersIngredients", "SupplierRefId", "dbo.Suppliers");
            DropForeignKey("dbo.IngredientOrders", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.IngredientListOrders", "OrderId", "dbo.IngredientOrders");
            DropForeignKey("dbo.CustomerDetailOrders", "MenuItemId", "dbo.MenuItemsSet");
            DropIndex("dbo.SuppliersIngredients", new[] { "IngredientRefId" });
            DropIndex("dbo.SuppliersIngredients", new[] { "SupplierRefId" });
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.CustomerOrders", new[] { "KitchenId" });
            DropIndex("dbo.KitchenIngredientsSet", new[] { "IngredientId" });
            DropIndex("dbo.KitchenIngredientsSet", new[] { "KitchenId" });
            DropIndex("dbo.IngredientOrders", new[] { "SupplierId" });
            DropIndex("dbo.IngredientListOrders", new[] { "IngredientId" });
            DropIndex("dbo.IngredientListOrders", new[] { "OrderId" });
            DropIndex("dbo.MenuIngredients", new[] { "MenuItemId" });
            DropIndex("dbo.MenuIngredients", new[] { "IngredientId" });
            DropIndex("dbo.CustomerDetailOrders", new[] { "MenuItemId" });
            DropTable("dbo.SuppliersIngredients");
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Roles");
            DropTable("dbo.CustomerOrders");
            DropTable("dbo.KitchensSet");
            DropTable("dbo.KitchenIngredientsSet");
            DropTable("dbo.Suppliers");
            DropTable("dbo.IngredientOrders");
            DropTable("dbo.IngredientListOrders");
            DropTable("dbo.Ingredients");
            DropTable("dbo.MenuIngredients");
            DropTable("dbo.MenuItemsSet");
            DropTable("dbo.CustomerDetailOrders");
        }
    }
}
