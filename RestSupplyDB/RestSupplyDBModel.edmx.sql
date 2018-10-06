
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/06/2018 18:45:23
-- Generated from EDMX file: C:\workspace\rest_supply_project\RestSupplyWPFUI\RestSupplyDB\RestSupplyDBModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [dbrestdev];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_OrderHistoryOrderIngredients]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderIngredientsSet] DROP CONSTRAINT [FK_OrderHistoryOrderIngredients];
GO
IF OBJECT_ID(N'[dbo].[FK_IngredientsOrderIngredients]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderIngredientsSet] DROP CONSTRAINT [FK_IngredientsOrderIngredients];
GO
IF OBJECT_ID(N'[dbo].[FK_SuppliersOrderIngredients]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderIngredientsSet] DROP CONSTRAINT [FK_SuppliersOrderIngredients];
GO
IF OBJECT_ID(N'[dbo].[FK_IngredientsMenuIngredients]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MenuIngredientsSet] DROP CONSTRAINT [FK_IngredientsMenuIngredients];
GO
IF OBJECT_ID(N'[dbo].[FK_MenuItemsMenuIngredients]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MenuIngredientsSet] DROP CONSTRAINT [FK_MenuItemsMenuIngredients];
GO
IF OBJECT_ID(N'[dbo].[FK_SuppliersIngredients]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IngredientsSet] DROP CONSTRAINT [FK_SuppliersIngredients];
GO
IF OBJECT_ID(N'[dbo].[FK_IngredientsKitchenIngredients]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[KitchenIngredientsSet] DROP CONSTRAINT [FK_IngredientsKitchenIngredients];
GO
IF OBJECT_ID(N'[dbo].[FK_KitchensKitchenIngredients]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[KitchenIngredientsSet] DROP CONSTRAINT [FK_KitchensKitchenIngredients];
GO
IF OBJECT_ID(N'[dbo].[FK_UserType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UsersSet] DROP CONSTRAINT [FK_UserType];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[IngredientsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IngredientsSet];
GO
IF OBJECT_ID(N'[dbo].[UsersSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UsersSet];
GO
IF OBJECT_ID(N'[dbo].[SuppliersSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SuppliersSet];
GO
IF OBJECT_ID(N'[dbo].[MenuItemsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MenuItemsSet];
GO
IF OBJECT_ID(N'[dbo].[OrdersSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrdersSet];
GO
IF OBJECT_ID(N'[dbo].[OrderIngredientsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderIngredientsSet];
GO
IF OBJECT_ID(N'[dbo].[MenuIngredientsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MenuIngredientsSet];
GO
IF OBJECT_ID(N'[dbo].[KitchensSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[KitchensSet];
GO
IF OBJECT_ID(N'[dbo].[KitchenIngredientsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[KitchenIngredientsSet];
GO
IF OBJECT_ID(N'[dbo].[UserTypesSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserTypesSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'IngredientsSet'
CREATE TABLE [dbo].[IngredientsSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Unit] nvarchar(max)  NOT NULL,
    [SupplierId] int  NOT NULL,
    [PricePerUnit] float  NOT NULL
);
GO

-- Creating table 'UsersSet'
CREATE TABLE [dbo].[UsersSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [UserTypesId] int  NULL
);
GO

-- Creating table 'SuppliersSet'
CREATE TABLE [dbo].[SuppliersSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'MenuItemsSet'
CREATE TABLE [dbo].[MenuItemsSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'OrdersSet'
CREATE TABLE [dbo].[OrdersSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] nvarchar(max)  NOT NULL,
    [Time] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'OrderIngredientsSet'
CREATE TABLE [dbo].[OrderIngredientsSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OrderId] int  NULL,
    [IngredientId] int  NULL,
    [IngredientPrice] float  NOT NULL,
    [SupplierId] int  NULL
);
GO

-- Creating table 'MenuIngredientsSet'
CREATE TABLE [dbo].[MenuIngredientsSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Valid] bit  NOT NULL,
    [IngredientName] nvarchar(max)  NOT NULL,
    [IngredientId] int  NULL,
    [MenuItemId] int  NULL,
    [Quantity] float  NOT NULL
);
GO

-- Creating table 'KitchensSet'
CREATE TABLE [dbo].[KitchensSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'KitchenIngredientsSet'
CREATE TABLE [dbo].[KitchenIngredientsSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Valid] bit  NOT NULL,
    [KitchenId] int  NOT NULL,
    [IngredientId] int  NOT NULL,
    [MinimalQuantity] float  NOT NULL,
    [CurrentQuantity] float  NOT NULL
);
GO

-- Creating table 'UserTypesSet'
CREATE TABLE [dbo].[UserTypesSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TypeName] nvarchar(max)  NOT NULL,
    [AccessLevel] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'IngredientsSet'
ALTER TABLE [dbo].[IngredientsSet]
ADD CONSTRAINT [PK_IngredientsSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UsersSet'
ALTER TABLE [dbo].[UsersSet]
ADD CONSTRAINT [PK_UsersSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SuppliersSet'
ALTER TABLE [dbo].[SuppliersSet]
ADD CONSTRAINT [PK_SuppliersSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MenuItemsSet'
ALTER TABLE [dbo].[MenuItemsSet]
ADD CONSTRAINT [PK_MenuItemsSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrdersSet'
ALTER TABLE [dbo].[OrdersSet]
ADD CONSTRAINT [PK_OrdersSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderIngredientsSet'
ALTER TABLE [dbo].[OrderIngredientsSet]
ADD CONSTRAINT [PK_OrderIngredientsSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MenuIngredientsSet'
ALTER TABLE [dbo].[MenuIngredientsSet]
ADD CONSTRAINT [PK_MenuIngredientsSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'KitchensSet'
ALTER TABLE [dbo].[KitchensSet]
ADD CONSTRAINT [PK_KitchensSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'KitchenIngredientsSet'
ALTER TABLE [dbo].[KitchenIngredientsSet]
ADD CONSTRAINT [PK_KitchenIngredientsSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserTypesSet'
ALTER TABLE [dbo].[UserTypesSet]
ADD CONSTRAINT [PK_UserTypesSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [OrderId] in table 'OrderIngredientsSet'
ALTER TABLE [dbo].[OrderIngredientsSet]
ADD CONSTRAINT [FK_OrderHistoryOrderIngredients]
    FOREIGN KEY ([OrderId])
    REFERENCES [dbo].[OrdersSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderHistoryOrderIngredients'
CREATE INDEX [IX_FK_OrderHistoryOrderIngredients]
ON [dbo].[OrderIngredientsSet]
    ([OrderId]);
GO

-- Creating foreign key on [IngredientId] in table 'OrderIngredientsSet'
ALTER TABLE [dbo].[OrderIngredientsSet]
ADD CONSTRAINT [FK_IngredientsOrderIngredients]
    FOREIGN KEY ([IngredientId])
    REFERENCES [dbo].[IngredientsSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_IngredientsOrderIngredients'
CREATE INDEX [IX_FK_IngredientsOrderIngredients]
ON [dbo].[OrderIngredientsSet]
    ([IngredientId]);
GO

-- Creating foreign key on [SupplierId] in table 'OrderIngredientsSet'
ALTER TABLE [dbo].[OrderIngredientsSet]
ADD CONSTRAINT [FK_SuppliersOrderIngredients]
    FOREIGN KEY ([SupplierId])
    REFERENCES [dbo].[SuppliersSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SuppliersOrderIngredients'
CREATE INDEX [IX_FK_SuppliersOrderIngredients]
ON [dbo].[OrderIngredientsSet]
    ([SupplierId]);
GO

-- Creating foreign key on [IngredientId] in table 'MenuIngredientsSet'
ALTER TABLE [dbo].[MenuIngredientsSet]
ADD CONSTRAINT [FK_IngredientsMenuIngredients]
    FOREIGN KEY ([IngredientId])
    REFERENCES [dbo].[IngredientsSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_IngredientsMenuIngredients'
CREATE INDEX [IX_FK_IngredientsMenuIngredients]
ON [dbo].[MenuIngredientsSet]
    ([IngredientId]);
GO

-- Creating foreign key on [MenuItemId] in table 'MenuIngredientsSet'
ALTER TABLE [dbo].[MenuIngredientsSet]
ADD CONSTRAINT [FK_MenuItemsMenuIngredients]
    FOREIGN KEY ([MenuItemId])
    REFERENCES [dbo].[MenuItemsSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MenuItemsMenuIngredients'
CREATE INDEX [IX_FK_MenuItemsMenuIngredients]
ON [dbo].[MenuIngredientsSet]
    ([MenuItemId]);
GO

-- Creating foreign key on [SupplierId] in table 'IngredientsSet'
ALTER TABLE [dbo].[IngredientsSet]
ADD CONSTRAINT [FK_SuppliersIngredients]
    FOREIGN KEY ([SupplierId])
    REFERENCES [dbo].[SuppliersSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SuppliersIngredients'
CREATE INDEX [IX_FK_SuppliersIngredients]
ON [dbo].[IngredientsSet]
    ([SupplierId]);
GO

-- Creating foreign key on [IngredientId] in table 'KitchenIngredientsSet'
ALTER TABLE [dbo].[KitchenIngredientsSet]
ADD CONSTRAINT [FK_IngredientsKitchenIngredients]
    FOREIGN KEY ([IngredientId])
    REFERENCES [dbo].[IngredientsSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_IngredientsKitchenIngredients'
CREATE INDEX [IX_FK_IngredientsKitchenIngredients]
ON [dbo].[KitchenIngredientsSet]
    ([IngredientId]);
GO

-- Creating foreign key on [KitchenId] in table 'KitchenIngredientsSet'
ALTER TABLE [dbo].[KitchenIngredientsSet]
ADD CONSTRAINT [FK_KitchensKitchenIngredients]
    FOREIGN KEY ([KitchenId])
    REFERENCES [dbo].[KitchensSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_KitchensKitchenIngredients'
CREATE INDEX [IX_FK_KitchensKitchenIngredients]
ON [dbo].[KitchenIngredientsSet]
    ([KitchenId]);
GO

-- Creating foreign key on [UserTypesId] in table 'UsersSet'
ALTER TABLE [dbo].[UsersSet]
ADD CONSTRAINT [FK_UserType]
    FOREIGN KEY ([UserTypesId])
    REFERENCES [dbo].[UserTypesSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserType'
CREATE INDEX [IX_FK_UserType]
ON [dbo].[UsersSet]
    ([UserTypesId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------