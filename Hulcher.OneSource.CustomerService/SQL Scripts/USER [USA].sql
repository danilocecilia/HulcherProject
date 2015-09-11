USE [usa]
GO
CREATE USER [OneSourceUser] FOR LOGIN [OneSourceUser]
GO
USE [usa]
GO
EXEC sp_addrolemember N'db_datareader', N'OneSourceUser'
GO
use [usa]
GO
GRANT SELECT ON [dbo].[OneSource_Customers] TO [OneSourceUser]
GO
use [usa]
GO
GRANT SELECT ON [dbo].[OneSource_Contract] TO [OneSourceUser]
GO
use [usa]
GO
GRANT SELECT ON [dbo].[OneSource_Contact] TO [OneSourceUser]
GO
