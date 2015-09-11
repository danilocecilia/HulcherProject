

SET IDENTITY_INSERT [CS_Job] ON

INSERT INTO [OneSource].[dbo].[CS_Job]
           ([ID]
		   ,[Number]
           ,[Internal_Tracking]
           ,[CreatedBy]
           ,[CreationDate]
           ,[ModifiedBy]
           ,[ModificationDate]
           ,[Active]
           ,[CreationID]
           ,[ModificationID]
           ,[BillingStatus]
           ,[IsBilling]
           ,[EmergencyResponse])
     VALUES
           (1
           ,'99999'
           ,''
           ,'System'
           ,GETDATE()
           ,'System'
           ,GETDATE()
           ,1
           ,null
           ,null
           ,3
           ,0
           ,0)
           
           SET IDENTITY_INSERT [CS_Job] OFF
           
GO


INSERT INTO [OneSource].[dbo].[CS_JobDivision]
           ([JobID]
           ,[DivisionID]
           ,[PrimaryDivision]
           ,[CreatedBy]
           ,[CreationDate]
           ,[ModifiedBy]
           ,[ModificationDate]
           ,[Active]
           ,[CreationID]
           ,[ModificationID])
     VALUES
           (1
           ,1
           ,1
           ,'System'
           ,GETDATE()
           ,'System'
           ,GETDATE()
           ,1
           ,null
           ,null)
GO

INSERT INTO [OneSource].[dbo].[CS_JobInfo]
           ([JobID]
           ,[InitialCallDate]
           ,[InitialCallTime]
           ,[InitialCallDatetimeOffset]
           ,[PriceTypeID]
           ,[JobCategoryID]
           ,[InterimBill]
           ,[EmployeeID]
           ,[JobTypeID]
           ,[JobActionID]
           ,[FrequencyID]
           ,[CustomerSpecificInfo]
           ,[CreatedBy]
           ,[CreationDate]
           ,[ModifiedBy]
           ,[ModificationDate]
           ,[Active]
           ,[ProjectManager]
           ,[CreationID]
           ,[ModificationID])
     VALUES
           (1
           ,GETDATE()
           ,GETDATE()
           ,GETDATE()
           ,1
           ,7
           ,0
           ,null
           ,6
           ,4
           ,null
           ,null
           ,'System'
           ,GETDATE()
           ,'System'
           ,GETDATE()
           ,1
           ,0
           ,null
           ,null)
GO


INSERT INTO [OneSource].[dbo].[CS_Job_JobStatus]
           ([JobID]
           ,[JobStatusId]
           ,[JobStartDate]
           ,[JobCloseDate]
           ,[CreationDate]
           ,[ModificationDate]
           ,[CreationID]
           ,[ModificationID]
           ,[CreatedBy]
           ,[ModifiedBy]
           ,[Active])
     VALUES
           (1
           ,1
           ,null
           ,null
           ,GETDATE()
           ,GETDATE()
           ,null
           ,null
           ,'System'
           ,'System'
           ,1)
GO

insert into CS_CustomerInfo (JobId, CustomerId, CreatedBy, CreationDate, ModifiedBy, ModificationDate, Active)
values (1, 1, 'System', GETDATE(), 'System', GETDATE(), 1)
GO