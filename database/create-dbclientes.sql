IF DB_ID(N'DBClientes') IS NULL
BEGIN
    CREATE DATABASE [DBClientes];
END;
GO

USE [DBClientes];
GO

IF OBJECT_ID(N'dbo.clientes', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[clientes]
    (
        [Id_cliente] INT IDENTITY(1,1) NOT NULL,
        [CUI] NVARCHAR(MAX) NOT NULL,
        [NIT] NVARCHAR(MAX) NOT NULL,
        [Nombres] NVARCHAR(MAX) NOT NULL,
        [Apellidos] NVARCHAR(MAX) NOT NULL,
        [Direccion] NVARCHAR(MAX) NOT NULL,
        [Telefono] NVARCHAR(MAX) NOT NULL,
        [Fecha_Nacimiento] DATETIME2 NOT NULL,
        CONSTRAINT [PK_clientes] PRIMARY KEY ([Id_cliente])
    );
END;
GO
