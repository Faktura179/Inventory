CREATE TABLE [dbo].[WarehouseProducts] (
    [ProductId]   INT NOT NULL,
    [WarehouseId] INT NOT NULL,
    [Quantity]    INT NOT NULL,
    PRIMARY KEY CLUSTERED ([ProductId] ASC, [WarehouseId] ASC),
	
	CONSTRAINT FK_WarehouseProducts_Products FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products]([ProductId]),
	CONSTRAINT FK_WarehouseProducts_Warehouses FOREIGN KEY ([WarehouseId]) REFERENCES [dbo].[Warehouses]([WarehouseId])
);

