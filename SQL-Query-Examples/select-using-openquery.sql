-- =============================================
-- Author:		Emma
-- Create date: 04.07.2023
-- Description:	Search for records in a MySQL database from SSMS, using OPENQUERY.
-- =============================================

SELECT * FROM OPENQUERY(UserAccountsMySQLDB,'SELECT * FROM UserAccounts.users') WHERE name LIKE '%smith%';