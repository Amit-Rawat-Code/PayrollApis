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

CREATE TABLE [Departments] (
    [DepartmentId] int NOT NULL IDENTITY,
    [DepartmentName] nvarchar(100) NOT NULL,
    [Description] nvarchar(250) NOT NULL,
    [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
    [CreatedOn] datetime2 NOT NULL DEFAULT (GETDATE()),
    [CreatedBy] int NOT NULL,
    [UpdatedOn] datetime2 NULL,
    [UpdatedBy] int NULL,
    CONSTRAINT [PK_Departments] PRIMARY KEY ([DepartmentId])
);
GO

CREATE TABLE [Designations] (
    [DesignationId] int NOT NULL IDENTITY,
    [DepartmentId] int NOT NULL,
    [DesignationName] nvarchar(50) NULL,
    [Description] nvarchar(250) NULL,
    [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
    [CreatedOn] datetime2 NOT NULL DEFAULT (GETDATE()),
    [CreatedBy] int NOT NULL,
    [UpdatedOn] datetime2 NULL,
    [UpdatedBy] int NULL,
    CONSTRAINT [PK_Designations] PRIMARY KEY ([DesignationId])
);
GO

CREATE TABLE [Employees] (
    [EmployeeId] int NOT NULL IDENTITY,
    [EmpCode] nvarchar(20) NOT NULL,
    [EmployeeName] nvarchar(max) NOT NULL,
    [EmployeeAge] int NOT NULL,
    [DOB] datetime2 NOT NULL,
    [Gender] nvarchar(max) NOT NULL,
    [Mobile] nvarchar(20) NOT NULL,
    [IsManager] bit NOT NULL,
    [Address] nvarchar(300) NOT NULL,
    [ImagePath] nvarchar(max) NULL,
    [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
    [CreatedOn] datetime2 NOT NULL DEFAULT (GETDATE()),
    [CreatedBy] int NOT NULL,
    [UpdatedOn] datetime2 NULL,
    [UpdatedBy] int NULL,
    CONSTRAINT [PK_Employees] PRIMARY KEY ([EmployeeId])
);
GO

CREATE UNIQUE INDEX [IX_Employees_EmpCode] ON [Employees] ([EmpCode]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260318112137_InitialCode', N'8.0.24');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Employees]') AND [c].[name] = N'EmployeeAge');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Employees] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Employees] DROP COLUMN [EmployeeAge];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Employees]') AND [c].[name] = N'CreatedBy');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Employees] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Employees] ALTER COLUMN [CreatedBy] int NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Designations]') AND [c].[name] = N'CreatedBy');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Designations] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Designations] ALTER COLUMN [CreatedBy] int NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Departments]') AND [c].[name] = N'CreatedBy');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Departments] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Departments] ALTER COLUMN [CreatedBy] int NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260318161206_RemoveColumnEmployeeAge', N'8.0.24');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Employees]') AND [c].[name] = N'DOB');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Employees] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Employees] DROP COLUMN [DOB];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260318170054_Checking', N'8.0.24');
GO

COMMIT;
GO

