USE OneSource
GO

ALTER TABLE CS_SpecialPricing ALTER COLUMN ApprovingRVPEmployeeID INT NULL
GO
ALTER TABLE CS_DpiSpecialPricing DROP CONSTRAINT FK_CS_DPISpecialPricing_CS_EmployeeApprovingRVP
GO
ALTER TABLE CS_DpiSpecialPricing DROP COLUMN ApprovingRVPEmployee
GO