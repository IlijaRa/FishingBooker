CREATE TABLE [dbo].[RegUser] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (50)  NOT NULL,
    [Surname]      NVARCHAR (50)  NOT NULL,
    [PhoneNumber]  NVARCHAR (50)  NULL,
    [EmailAddress] NVARCHAR (100) NOT NULL,
	[Password]	   NVARCHAR (300) NOT NULL,
    [Address]      NVARCHAR (50)  NULL,
    [City]         NVARCHAR (50)  NULL,
    [Country]      NVARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

