-- =============================================
-- Author:		Emma
-- Create date: 04.07.2023
-- Description:	Add a column to a table.
-- =============================================

BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.tblUserAccounts ADD
	IsActive bit NULL
GO
ALTER TABLE dbo.tblUserAccounts SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
