-- TẠO DATABASE PAWNSHOP VÀ TẤT CẢ TABLES
USE master;
GO

-- Tạo database nếu chưa tồn tại
IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'PawnShop')
BEGIN
    CREATE DATABASE PawnShop;
END
GO

USE PawnShop;
GO

-- TẠO TABLES
CREATE TABLE [CapitalInformation] (
    [Id] int NOT NULL IDENTITY,
    [TotalCapital] decimal(18,2) NOT NULL,
    [TotalIncome] decimal(18,2) NOT NULL,
    [TotalExpenditure] decimal(18,2) NOT NULL,
    [TotalProfit] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_CapitalInformation] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [ShopInformation] (
    [Id] int NOT NULL IDENTITY,
    [ShopName] nvarchar(100) NOT NULL,
    [ShopAddress] nvarchar(200) NOT NULL,
    [Telephone] nvarchar(max) NOT NULL,
    [EmailAddress] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_ShopInformation] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [ShopItem] (
    [ShopItemId] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Description] nvarchar(500) NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [DateAdded] datetime2 NOT NULL,
    [IsExpired] bit NOT NULL,
    CONSTRAINT [PK_ShopItem] PRIMARY KEY ([ShopItemId])
);
GO

CREATE TABLE [User] (
    [UserID] int NOT NULL IDENTITY,
    [UserName] nvarchar(50) NOT NULL,
    [UserRealName] nvarchar(100) NOT NULL,
    [Telephone] nvarchar(max) NOT NULL,
    [Gender] nvarchar(max) NOT NULL,
    [EmailAddress] nvarchar(max) NOT NULL,
    [Dob] datetime2 NULL,
    [Address] nvarchar(200) NOT NULL,
    [UserRole] int NOT NULL,
    [CID] nvarchar(20) NOT NULL,
    [Password] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([UserID])
);
GO

CREATE TABLE [Item] (
    [ItemId] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Description] nvarchar(500) NOT NULL,
    [Value] decimal(18,2) NOT NULL,
    [Status] nvarchar(50) NOT NULL,
    [ExpirationDate] datetime2 NOT NULL,
    [Interest] decimal(18,2) NOT NULL,
    [IsApproved] bit NOT NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_Item] PRIMARY KEY ([ItemId]),
    CONSTRAINT [FK_Item_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Bills] (
    [BillId] int NOT NULL IDENTITY,
    [ShopItemId] int NULL,
    [UserId] int NOT NULL,
    [DateBuy] datetime2 NOT NULL,
    [PawnContractId] int NULL,
    CONSTRAINT [PK_Bills] PRIMARY KEY ([BillId]),
    CONSTRAINT [FK_Bills_ShopItem_ShopItemId] FOREIGN KEY ([ShopItemId]) REFERENCES [ShopItem] ([ShopItemId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Bills_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [PawnContracts] (
    [ContractId] int NOT NULL IDENTITY,
    [ItemId] int NOT NULL,
    [UserId] int NOT NULL,
    [LoanAmount] decimal(18,2) NOT NULL,
    [ContractDate] datetime2 NOT NULL,
    [ExpirationDate] datetime2 NOT NULL,
    CONSTRAINT [PK_PawnContracts] PRIMARY KEY ([ContractId]),
    CONSTRAINT [FK_PawnContracts_Item_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Item] ([ItemId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PawnContracts_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
);
GO

-- TẠO INDEXES
CREATE INDEX [IX_Bills_ShopItemId] ON [Bills] ([ShopItemId]);
CREATE INDEX [IX_Bills_UserId] ON [Bills] ([UserId]);
CREATE INDEX [IX_Item_UserId] ON [Item] ([UserId]);
CREATE INDEX [IX_PawnContracts_ItemId] ON [PawnContracts] ([ItemId]);
CREATE INDEX [IX_PawnContracts_UserId] ON [PawnContracts] ([UserId]);
GO

-- INSERT SEED DATA
INSERT INTO [CapitalInformation] ([TotalCapital], [TotalExpenditure], [TotalIncome], [TotalProfit]) 
VALUES (10000.00, 10000.00, 0, 0);
GO

INSERT INTO [ShopInformation] ([EmailAddress], [ShopAddress], [ShopName], [Telephone]) 
VALUES ('fpt@edu.vn', 'FPT University', 'FPT Pawn Shop', '1234-5555');
GO

INSERT INTO [ShopItem] ([DateAdded], [Description], [IsExpired], [Name], [Price]) VALUES 
('2025-06-09T00:00:00.0000000', 'High performance laptop for gaming.', 1, 'Gaming Laptop', 750.00),
('2025-06-14T00:00:00.0000000', 'Latest smartphone with advanced features.', 1, 'Latest Model Smartphone', 300.00),
('2025-05-30T00:00:00.0000000', 'Professional electric guitar.', 1, 'Electric Guitar', 450.00),
('2025-06-24T00:00:00.0000000', 'High resolution digital camera.', 1, 'Digital Camera', 600.00),
('2025-06-19T00:00:00.0000000', 'Luxury brand handbag.', 1, 'Designer Handbag', 850.00);
GO

INSERT INTO [User] ([Address], [CID], [Dob], [EmailAddress], [Gender], [Password], [Telephone], [UserName], [UserRealName], [UserRole]) VALUES 
('123 Main St', 'C123456789', '1990-05-20T00:00:00.0000000', 'john.doe@example.com', 'Male', 'Password123', '123-456-7890', 'john_doe', 'John Doe', 1),
('456 Oak St', 'C987654321', '1988-08-15T00:00:00.0000000', 'jane.smith@example.com', 'Female', 'Password456', '098-765-4321', 'jane_smith', 'Jane Smith', 2),
('789 Pine St', 'C555123456', '1985-10-05T00:00:00.0000000', 'michael.brown@example.com', 'Male', 'Password789', '555-123-4567', 'michael_brown', 'Michael Brown', 1),
('101 Maple St', 'C444987654', '1992-02-28T00:00:00.0000000', 'emily.jones@example.com', 'Male', 'Password101', '444-987-6543', 'emily_jones', 'Emily Jones', 2),
('202 Oakwood Ave', 'C333222111', '1978-12-12T00:00:00.0000000', 'robert.smith@example.com', 'Female', 'Password202', '333-222-1111', 'robert_smith', 'Robert Smith', 1);
GO

INSERT INTO [Item] ([Description], [ExpirationDate], [Interest], [IsApproved], [Name], [Status], [UserId], [Value]) VALUES 
('14K Gold Ring', '2025-07-29T00:00:00.0000000', 0.05, 1, 'Gold Ring', 'Pending', 1, 250.00),
('Luxury Watch', '2025-08-29T00:00:00.0000000', 0.10, 1, 'Luxury Watch', 'Active', 2, 500.00),
('24K Diamond Necklace', '2025-09-29T00:00:00.0000000', 0.07, 1, 'Diamond Necklace', 'Pending', 3, 1200.00),
('Sterling Silver Bracelet', '2025-07-29T00:00:00.0000000', 0.04, 0, 'Silver Bracelet', 'Active', 4, 150.00),
('Porcelain Antique Vase', '2025-10-29T00:00:00.0000000', 0.08, 1, 'Antique Vase', 'Pending', 5, 750.00);
GO

INSERT INTO [Bills] ([DateBuy], [ShopItemId], [UserId]) VALUES 
('2025-06-19T00:00:00.0000000', 1, 1),
('2025-06-24T00:00:00.0000000', 2, 2),
('2025-06-09T00:00:00.0000000', 3, 3),
('2025-06-27T00:00:00.0000000', 4, 4),
('2025-06-22T00:00:00.0000000', 5, 5);
GO

INSERT INTO [PawnContracts] ([ContractDate], [ExpirationDate], [ItemId], [LoanAmount], [UserId]) VALUES 
('2025-05-29T00:00:00.0000000', '2025-07-29T00:00:00.0000000', 1, 200.00, 1),
('2025-04-29T00:00:00.0000000', '2025-08-29T00:00:00.0000000', 2, 400.00, 2),
('2025-03-29T00:00:00.0000000', '2025-07-29T00:00:00.0000000', 3, 900.00, 3),
('2025-05-29T00:00:00.0000000', '2025-07-29T00:00:00.0000000', 4, 120.00, 4),
('2025-02-28T00:00:00.0000000', '2025-07-29T00:00:00.0000000', 5, 600.00, 5);
GO

PRINT 'Database PawnShop đã được tạo thành công với tất cả tables và seed data!';
GO 