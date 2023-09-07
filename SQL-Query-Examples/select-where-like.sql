-- =============================================
-- Author:		Emma
-- Create date: 04.07.2023
-- Description:	Search for records where the Id is like a search string.
-- =============================================

SELECT TOP 1000 [Id]
      ,[Description]
      ,[Title]
      ,[Department]
      ,[Notes]
  FROM [UserAccountsDB].[dbo].[Usernames]
	WHERE Id LIKE '%username%'