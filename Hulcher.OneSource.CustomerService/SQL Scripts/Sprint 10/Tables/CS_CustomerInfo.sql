Use OneSource
GO

ALTER TABLE CS_CustomerInfo ADD CalledInByContactId INT NULL
GO

ALTER TABLE CS_CustomerInfo ADD CONSTRAINT FK_CS_CustomerInfo_CS_Contact_CalledInBy FOREIGN KEY (CalledInByContactId) REFERENCES CS_Contact(ID)
GO