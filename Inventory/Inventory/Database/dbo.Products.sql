CREATE TABLE [dbo].[Products] (
    [ProductId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (255) NOT NULL,
    [Sku]       NVARCHAR (20)  NOT NULL,
    [Price]     MONEY          NOT NULL,
    [ProductPropertyId] INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([ProductId] ASC),
    CONSTRAINT FK_Products_ProductProperties FOREIGN KEY ([ProductPropertyId]) REFERENCES [dbo].[ProductProperties]([ProductPropertyId]),
);

