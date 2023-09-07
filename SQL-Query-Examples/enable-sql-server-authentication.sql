-- =============================================
-- Author:		Emma
-- Create date: 04.07.2023
-- Description:	Enable SQL Server Authentication for this database.
-- =============================================

EXEC xp_instance_regwrite N'HKEY_LOCAL_MACHINE', N'Software\Microsoft\MSSQLServer\MSSQLServer', N'LoginMode', REG_DWORD, 2