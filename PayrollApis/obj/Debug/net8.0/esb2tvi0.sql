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

