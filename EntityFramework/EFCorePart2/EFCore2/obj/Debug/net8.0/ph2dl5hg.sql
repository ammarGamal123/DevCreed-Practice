BEGIN TRANSACTION;
GO

ALTER TABLE [Blogs] ADD [AddedOn] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240208110648_AddAddedOnColumn', N'8.0.1');
GO

COMMIT;
GO

