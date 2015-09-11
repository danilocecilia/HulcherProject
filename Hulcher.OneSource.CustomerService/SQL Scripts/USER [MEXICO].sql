USE [mexico]
GO
CREATE USER [OneSourceUser] FOR LOGIN [OneSourceUser]
GO
USE [mexico]
GO
EXEC sp_addrolemember N'db_datareader', N'OneSourceUser'
GO
use [mexico]
GO
GRANT SELECT ON [dbo].[OneSource_Customers] TO [OneSourceUser]
GO
use [mexico]
GO
GRANT SELECT ON [dbo].[OneSource_Contract] TO [OneSourceUser]
GO
use [mexico]
GO
GRANT SELECT ON [dbo].[OneSource_Contact] TO [OneSourceUser]
GO
