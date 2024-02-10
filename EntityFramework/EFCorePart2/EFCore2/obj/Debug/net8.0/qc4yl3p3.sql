IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Blogs] (
    [BlogId] int NOT NULL IDENTITY,
    [Url] nvarchar(max) NOT NULL,
    [AddedOn] datetime2 NOT NULL,
    CONSTRAINT [PK_Blogs] PRIMARY KEY ([BlogId])
);
GO

CREATE TABLE [Nationalities] (
    [NationalityId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_Nationalities] PRIMARY KEY ([NationalityId])
);
GO

CREATE TABLE [Posts] (
    [PostId] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [Content] nvarchar(max) NOT NULL,
    [BlogId] int NOT NULL,
    CONSTRAINT [PK_Posts] PRIMARY KEY ([PostId]),
    CONSTRAINT [FK_Posts_Blogs_BlogId] FOREIGN KEY ([BlogId]) REFERENCES [Blogs] ([BlogId]) ON DELETE CASCADE
);
GO

CREATE TABLE [Authors] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [NationalityId] int NOT NULL,
    [NationalityId1] int NULL,
    CONSTRAINT [PK_Authors] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Authors_Nationalities_NationalityId] FOREIGN KEY ([NationalityId]) REFERENCES [Nationalities] ([NationalityId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Authors_Nationalities_NationalityId1] FOREIGN KEY ([NationalityId1]) REFERENCES [Nationalities] ([NationalityId])
);
GO

CREATE TABLE [Books] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Price] decimal(18,2) NOT NULL,
    [AuthorId] int NOT NULL,
    CONSTRAINT [PK_Books] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Books_Authors_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [Authors] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Authors_NationalityId] ON [Authors] ([NationalityId]);
GO

CREATE UNIQUE INDEX [IX_Authors_NationalityId1] ON [Authors] ([NationalityId1]) WHERE [NationalityId1] IS NOT NULL;
GO

CREATE INDEX [IX_Books_AuthorId] ON [Books] ([AuthorId]);
GO

CREATE INDEX [IX_Posts_BlogId] ON [Posts] ([BlogId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240210215811_AddBooksAuthorsNationalitiesTables', N'8.0.1');
GO

COMMIT;
GO

