/*
FirstCSharp.DataBase 的部署脚本

此代码由工具生成。
如果重新生成此代码，则对此文件的更改可能导致
不正确的行为并将丢失。
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "FirstCSharp.DataBase"
:setvar DefaultFilePrefix "FirstCSharp.DataBase"
:setvar DefaultDataPath "C:\Users\Administrator\AppData\Local\Microsoft\VisualStudio\SSDT\FirstCSharp"
:setvar DefaultLogPath "C:\Users\Administrator\AppData\Local\Microsoft\VisualStudio\SSDT\FirstCSharp"

GO
:on error exit
GO
/*
请检测 SQLCMD 模式，如果不支持 SQLCMD 模式，请禁用脚本执行。
要在启用 SQLCMD 模式后重新启用脚本，请执行:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'要成功执行此脚本，必须启用 SQLCMD 模式。';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                CURSOR_DEFAULT LOCAL 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET PAGE_VERIFY NONE,
                DISABLE_BROKER 
            WITH ROLLBACK IMMEDIATE;
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE (CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 367)) 
            WITH ROLLBACK IMMEDIATE;
    END


GO
PRINT N'正在创建 用户定义的表类型 [dbo].[type_member]...';


GO
CREATE TYPE [dbo].[type_member] AS TABLE (
    [f_name]     VARCHAR (25) NOT NULL,
    [f_price]    DECIMAL (18) NULL,
    [f_descript] VARCHAR (25) NULL);


GO
PRINT N'正在创建 表 [dbo].[t_member]...';


GO
CREATE TABLE [dbo].[t_member] (
    [f_id]       INT          IDENTITY (1, 1) NOT NULL,
    [f_name]     VARCHAR (25) NOT NULL,
    [f_price]    DECIMAL (18) NULL,
    [f_descript] VARCHAR (25) NULL,
    CONSTRAINT [PK_member] PRIMARY KEY CLUSTERED ([f_id] ASC)
);


GO
PRINT N'正在创建 过程 [dbo].[pro_memberAdd]...';


GO


CREATE PROCEDURE [dbo].[pro_memberAdd]
	@userName VARCHAR(25),
	@price DECIMAL,
	@descript VARCHAR(25)
AS
	INSERT INTO t_member (f_name, f_price, f_descript)
	OUTPUT inserted.*
	VALUES(@userName, @price, @descript)
RETURN 0
GO
PRINT N'正在创建 过程 [dbo].[pro_memberAddBatch]...';


GO


CREATE PROCEDURE [dbo].[pro_memberAddBatch]
	@type_member type_member READONLY
AS
	INSERT INTO t_member (f_name, f_price, f_descript)
	OUTPUT inserted.*
	SELECT f_name, f_price, f_descript FROM @type_member
RETURN 0
GO
PRINT N'正在创建 过程 [dbo].[pro_memberDelete]...';


GO
CREATE PROCEDURE [dbo].[pro_memberDelete]
	@id INT
AS
	DELETE FROM t_member WITH(ROWLOCK)
	OUTPUT deleted.*
	WHERE f_id = @id
RETURN 0
GO
PRINT N'正在创建 过程 [dbo].[pro_memberQuery]...';


GO
CREATE PROCEDURE [dbo].[pro_memberQuery]
AS
	SELECT f_id, f_name, f_price, f_descript  FROM t_member WITH(NOLOCK) 
RETURN 0
GO
PRINT N'正在创建 过程 [dbo].[pro_memberUpdate]...';


GO
CREATE PROCEDURE [dbo].[pro_memberUpdate]
	@id INT,--傳入值
	@userName VARCHAR(25),
	@price DECIMAL,
	@descript VARCHAR(25)
AS
	UPDATE t_member WITH(ROWLOCK)
	SET 
	f_name = @userName, 
	f_price = @price, 
	f_descript = @descript 
	OUTPUT inserted.*
	WHERE f_id = @id
RETURN 0
GO
PRINT N'正在创建 权限 权限...';


GO
GRANT EXECUTE
    ON OBJECT::[dbo].[pro_memberAdd] TO PUBLIC
    AS [dbo];


GO
PRINT N'正在创建 权限 权限...';


GO
GRANT EXECUTE
    ON OBJECT::[dbo].[pro_memberAddBatch] TO PUBLIC
    AS [dbo];


GO
PRINT N'正在创建 权限 权限...';


GO
GRANT EXECUTE
    ON OBJECT::[dbo].[pro_memberDelete] TO PUBLIC
    AS [dbo];


GO
PRINT N'正在创建 权限 权限...';


GO
GRANT EXECUTE
    ON OBJECT::[dbo].[pro_memberQuery] TO PUBLIC
    AS [dbo];


GO
PRINT N'正在创建 权限 权限...';


GO
GRANT EXECUTE
    ON OBJECT::[dbo].[pro_memberUpdate] TO PUBLIC
    AS [dbo];


GO
PRINT N'正在创建 权限 权限...';


GO
GRANT EXECUTE
    ON TYPE::[dbo].[type_member] TO PUBLIC;


GO
PRINT N'更新完成。';


GO
