-- =============================================
-- Author:		Emma
-- Create date: 04.07.2023
-- Description:	Copy a table to a temporary table.
-- =============================================

SELECT *
INTO [UserAccountsDB].[dbo].[tblTempDepartments]
FROM [UserAccountsDB].[dbo].[tblDepartments]