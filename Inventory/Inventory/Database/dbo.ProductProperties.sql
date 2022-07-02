CREATE TABLE [dbo].[ProductProperties] (
    [ProductPropertyId] INT            NOT NULL,
    [Name]              NVARCHAR (255) NOT NULL,
    [Value]             NVARCHAR (255) NOT NULL,
	[ProductId]			INT NOT NULL,
    PRIMARY KEY CLUSTERED ([ProductPropertyId] ASC),

	CONSTRAINT FK_ProdcutProperties_Products FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products]([ProductId])
);

