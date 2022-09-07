CREATE TABLE [dbo].[files] (
    [id]       INT           IDENTITY (1, 1) NOT NULL,
    [userId]   INT           NULL,
    [filename] VARCHAR (MAX) NULL,
    [location] VARCHAR (MAX) NULL
);

