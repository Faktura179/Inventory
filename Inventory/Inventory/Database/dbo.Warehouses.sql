CREATE TABLE [dbo].[Warehouses] (
    [WarehouseId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (255) NOT NULL,
    [City]        NVARCHAR (255) NOT NULL,
    PRIMARY KEY CLUSTERED ([WarehouseId] ASC)
);

