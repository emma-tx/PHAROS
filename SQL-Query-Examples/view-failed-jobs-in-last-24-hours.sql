USE [UsersDatabase]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		
-- Create date: 07.09.2023
-- Description:	Check for failed account creation jobs and number of attempts within the past 24 hours.
-- =============================================

ALTER PROCEDURE [dbo].[spGetFailedAccountCreationAttempts] 

AS
BEGIN

Declare @inputdate datetime2= getdate()
DECLARE @PreviousDay AS DATETIME = DATEADD(DAY,-1,@inputdate)
 
select distinct username, Attempt, TimeStamp from tblCreationJobs where 
attempt > 1
and status = 0
and TimeStamp > @PreviousDay
and jobtype = 'create'

END
