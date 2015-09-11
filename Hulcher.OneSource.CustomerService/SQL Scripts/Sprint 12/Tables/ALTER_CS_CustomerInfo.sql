USE [OneSource]
GO

ALTER TABLE CS_CustomerInfo DROP IsCustomer

ALTER TABLE CS_CustomerInfo DROP CONSTRAINT FK_CS_CustomerInfo_CS_Contact_CalledInBy

ALTER TABLE CS_CustomerInfo DROP CalledInByContactId