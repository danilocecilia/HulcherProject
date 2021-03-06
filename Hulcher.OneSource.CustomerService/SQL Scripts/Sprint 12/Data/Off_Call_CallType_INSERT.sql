SET IDENTITY_INSERT CS_CALLTYPE ON

GO

INSERT INTO [OneSource].[dbo].[CS_CallType]
	(
		Id
		,Description
		,Xml
		,CallCriteria
		,IsAutomaticProcess
		,DpiStatus
		,CreatedBy
		,CreationDate
		,CreationID
		,ModifiedBy
		,ModificationDate
		,ModificationID
		,Active
	)
VALUES
	(
		42
		,'Off Call'
		,NULL
		,0
		,1
		,NULL
		,'System'
		,GETDATE()
		,325237
		,'System'
		,GETDATE()
		,325237
		,1
	)
GO

SET IDENTITY_INSERT CS_CALLTYPE OFF

GO

INSERT INTO [OneSource].[dbo].[CS_PrimaryCallType_CallType]
	(
		CallTypeID
		,PrimaryCallTypeID
	)
SELECT
	(SELECT TOP 1 ID FROM [OneSource].[dbo].[CS_CallType] WHERE [Description] = 'Off Call')
	, (SELECT TOP 1 ID FROM [OneSource].[dbo].[CS_PrimaryCallType] WHERE [Type] = 'Non-Job Update Notification')
	
GO
  
  
