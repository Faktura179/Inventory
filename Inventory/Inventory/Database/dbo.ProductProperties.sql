CREATE TABLE [dbo].[ProductProperties] (
    [ProductPropertyId] INT            NOT NULL,
    [Name]              NVARCHAR (255) NOT NULL,
    [Value]             NVARCHAR (255) NOT NULL,
    PRIMARY KEY CLUSTERED ([ProductPropertyId] ASC),
);

