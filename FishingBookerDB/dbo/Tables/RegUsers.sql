CREATE TABLE [dbo].[RegUsers] (
    [Id]                   NVARCHAR (128) NOT NULL,
    [Name]                 NVARCHAR (MAX) NULL,
    [Surname]              NVARCHAR (MAX) NULL,
    [PhoneNumber]          NVARCHAR (MAX) NULL,
    [EmailAddress]         NVARCHAR (256) NULL,
    [Password]             NVARCHAR (MAX) NULL,
    [Type]                 NVARCHAR (MAX) NULL,
    [Address]              NVARCHAR (MAX) NULL,
    [City]                 NVARCHAR (MAX) NULL,
    [Country]              NVARCHAR (MAX) NULL,
    [Description]          NVARCHAR (MAX) NULL,
    [Status]               NVARCHAR (MAX) NULL,
    [EmailConfirmed]       BIT            NOT NULL,
    [SecurityStamp]        NVARCHAR (MAX) NULL,
    [PhoneNumberConfirmed] BIT            NOT NULL,
    [TwoFactorEnabled]     BIT            NOT NULL,
    [LockoutEndDateUtc]    DATETIME       NULL,
    [LockoutEnabled]       BIT            NOT NULL,
    [AccessFailedCount]    INT            NOT NULL,
    [UserName]             NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_dbo.RegUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex]
    ON [dbo].[RegUsers]([UserName] ASC);

