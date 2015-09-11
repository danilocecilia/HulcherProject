USE [ff6110]
GO
CREATE USER [OneSourceUser] FOR LOGIN [OneSourceUser]
GO
USE [ff6110]
GO
Exec sp_defaultdb @loginame='OneSourceUser', @defdb='ff6110'
GO
USE [ff6110]
GO
EXEC sp_addrolemember N'db_datareader', N'OneSourceUser'
GO
use [ff6110]
GO
GRANT SELECT ON [dbo].[OneSource_Employees] TO [OneSourceUser]
GO
use [ff6110]
GO
GRANT SELECT ON [dbo].[OneSource_EmergencyContacts] TO [OneSourceUser]
GO
use [ff6110]
GO
GRANT SELECT ON [dbo].[OneSource_EmergencyContacts] TO [OneSourceUser]
GO