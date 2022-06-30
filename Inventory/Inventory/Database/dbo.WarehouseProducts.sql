CREATE TABLE [dbo].[WarehouseProducts] (
    [ProductId]   INT NOT NULL,
    [WarehouseId] INT NOT NULL,
    [Quantity]    INT NOT NULL,
    PRIMARY KEY CLUSTERED ([ProductId] ASC, [WarehouseId] ASC)
);

