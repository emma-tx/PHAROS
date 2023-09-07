-- =============================================
-- Author:		Emma
-- Create date: 04.07.2023
-- Description:	Create a new user for this database server.
-- =============================================

CREATE LOGIN standarduser WITH PASSWORD=N'ThisIsAPassword',
                 DEFAULT_DATABASE=[master], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF

EXEC sp_addsrvrolemember 'standarduser', 'sysadmin'
CREATE USER myusername FOR LOGIN standarduser WITH DEFAULT_SCHEMA=[dbo]