CREATE TABLE [dbo].[ProductProperties] (
    [ProductPropertyId] INT            NOT NULL IDENTITY(1,1),
    [Name]              NVARCHAR (255) NOT NULL,
    [Value]             NVARCHAR (255) NOT NULL,
    PRIMARY KEY CLUSTERED ([ProductPropertyId] ASC),
);

