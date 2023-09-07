-- =============================================
-- Author:		Emma
-- Create date: 04.07.2023
-- Description:	Insert a record into a MySQL database table from SSMS, using OPENQUERY.
-- =============================================


insert openquery(UserNamesMySQLDB,
'select name, email, username, created_at from UserNamesDB.Usernames ')
Select 'Emma Scarbrough', 'emma.scarbrough@mynetwork.net', 'scarbrough', '2023-05-05 09:01:36'