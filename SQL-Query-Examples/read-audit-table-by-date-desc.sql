-- =============================================
-- Author:		Emma
-- Create date: 04.07.2023
-- Description:	Read a table, with results ordered by date descending.
-- =============================================

SELECT TOP (1000) [Username]
      ,[Description]
      ,[Department]
      ,[EmailAddress]
      ,[JobTitle]
      ,[Status]
      ,[NewUEmail]
      ,[PrimaryDomain]
      ,[DisplayName]
  FROM [UserAccountsDB].[dbo].[Usernames]
  ORDER BY Date DESC
