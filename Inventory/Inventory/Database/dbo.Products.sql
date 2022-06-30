CREATE TABLE [dbo].[Products] (
    [ProductId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (255) NOT NULL,
    [Sku]       NVARCHAR (20)  NOT NULL,
    [Price]     MONEY          NOT NULL,
    PRIMARY KEY CLUSTERED ([ProductId] ASC)
);

