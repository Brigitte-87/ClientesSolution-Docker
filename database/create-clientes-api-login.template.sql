USE [master];
GO

IF DB_ID(N'DBClientes') IS NULL
    THROW 50000, 'DBClientes does not exist.', 1;

IF CAST(SERVERPROPERTY('IsIntegratedSecurityOnly') AS int) = 1
    THROW 50001, 'SQL Server is configured for Windows-only authentication. No server setting was changed.', 1;

IF SUSER_ID(N'clientes_api') IS NOT NULL
    THROW 50002, 'The clientes_api login already exists. Review it manually; this script will not modify it.', 1;

DECLARE @Password nvarchar(128) = N'__REPLACE_MANUALLY_IN_SSMS__';

IF @Password = N'__REPLACE_MANUALLY_IN_SSMS__'
    THROW 50003, 'Replace the password placeholder in the SSMS query window before executing.', 1;

DECLARE @CreateLogin nvarchar(max) =
    N'CREATE LOGIN [clientes_api] WITH PASSWORD = '
    + QUOTENAME(@Password, N'''')
    + N', CHECK_POLICY = ON, CHECK_EXPIRATION = OFF;';
EXEC sys.sp_executesql @CreateLogin;
GO

USE [DBClientes];
GO

IF OBJECT_ID(N'dbo.clientes', N'U') IS NULL
    THROW 50004, 'dbo.clientes does not exist.', 1;

IF USER_ID(N'clientes_api') IS NOT NULL
    THROW 50005, 'The clientes_api database user already exists. Review it manually; this script will not modify it.', 1;

CREATE USER [clientes_api] FOR LOGIN [clientes_api];
GRANT SELECT, INSERT, UPDATE, DELETE ON OBJECT::[dbo].[clientes] TO [clientes_api];
GO
