namespace RestSupplyDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
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
            
            CreateTable(
                "dbo.MenuItemsSet",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MenuIngredientsSet",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Valid = c.Boolean(nullable: false),
                        IngredientName = c.String(nullable: false),
                        IngredientId = c.Int(),
                        MenuItemId = c.Int(),
                        Quantity = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IngredientsSet", t => t.IngredientId)
                .ForeignKey("dbo.MenuItemsSet", t => t.MenuItemId)
                .Index(t => t.IngredientId)
                .Index(t => t.MenuItemId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SuppliersSet", t => t.SupplierId)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "dbo.IngredientListOrdersSet",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        IngredientId = c.Int(nullable: false),
                        IngredientPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IngredientOrdersSet", t => t.OrderId)
                .ForeignKey("dbo.IngredientsSet", t => t.IngredientId)
                .Index(t => t.OrderId)
                .Index(t => t.IngredientId);
            
            CreateTable(
                "dbo.IngredientOrdersSet",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.String(nullable: false),
                        Time = c.String(nullable: false),
                        SupplierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SuppliersSet", t => t.SupplierId)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "dbo.SuppliersSet",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
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
                .ForeignKey("dbo.IngredientsSet", t => t.IngredientId)
                .Index(t => t.KitchenId)
                .Index(t => t.IngredientId);
            
            CreateTable(
                "dbo.KitchensSet",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomerOrdersSet",
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
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
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
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.RolesSet",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetRoles", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RolesSet", "Id", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.MenuIngredientsSet", "MenuItemId", "dbo.MenuItemsSet");
            DropForeignKey("dbo.MenuIngredientsSet", "IngredientId", "dbo.IngredientsSet");
            DropForeignKey("dbo.KitchenIngredientsSet", "IngredientId", "dbo.IngredientsSet");
            DropForeignKey("dbo.KitchenIngredientsSet", "KitchenId", "dbo.KitchensSet");
            DropForeignKey("dbo.CustomerOrdersSet", "KitchenId", "dbo.KitchensSet");
            DropForeignKey("dbo.IngredientListOrdersSet", "IngredientId", "dbo.IngredientsSet");
            DropForeignKey("dbo.IngredientsSet", "SupplierId", "dbo.SuppliersSet");
            DropForeignKey("dbo.IngredientOrdersSet", "SupplierId", "dbo.SuppliersSet");
            DropForeignKey("dbo.IngredientListOrdersSet", "OrderId", "dbo.IngredientOrdersSet");
            DropForeignKey("dbo.CustomerDetailOrdersSet", "MenuItemId", "dbo.MenuItemsSet");
            DropIndex("dbo.RolesSet", new[] { "Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.CustomerOrdersSet", new[] { "KitchenId" });
            DropIndex("dbo.KitchenIngredientsSet", new[] { "IngredientId" });
            DropIndex("dbo.KitchenIngredientsSet", new[] { "KitchenId" });
            DropIndex("dbo.IngredientOrdersSet", new[] { "SupplierId" });
            DropIndex("dbo.IngredientListOrdersSet", new[] { "IngredientId" });
            DropIndex("dbo.IngredientListOrdersSet", new[] { "OrderId" });
            DropIndex("dbo.IngredientsSet", new[] { "SupplierId" });
            DropIndex("dbo.MenuIngredientsSet", new[] { "MenuItemId" });
            DropIndex("dbo.MenuIngredientsSet", new[] { "IngredientId" });
            DropIndex("dbo.CustomerDetailOrdersSet", new[] { "MenuItemId" });
            DropTable("dbo.RolesSet");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.CustomerOrdersSet");
            DropTable("dbo.KitchensSet");
            DropTable("dbo.KitchenIngredientsSet");
            DropTable("dbo.SuppliersSet");
            DropTable("dbo.IngredientOrdersSet");
            DropTable("dbo.IngredientListOrdersSet");
            DropTable("dbo.IngredientsSet");
            DropTable("dbo.MenuIngredientsSet");
            DropTable("dbo.MenuItemsSet");
            DropTable("dbo.CustomerDetailOrdersSet");
        }
    }
}
