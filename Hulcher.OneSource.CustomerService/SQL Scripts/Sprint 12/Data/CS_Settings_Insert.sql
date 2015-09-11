SET IDENTITY_INSERT CS_Settings ON

GO

insert into cs_settings (ID, Description, Type, Value)
select 18, 'Transportation Team', 2, 'permits@uat.com;bburns@uat.com'

insert into cs_settings (ID, Description, Type, Value)
select 19, 'AddressChangeNotification', 2, 'cburton@uat.com'

insert into cs_settings (ID, Description, Type, Value)
select 21, 'ContactChangeNotification', 2, 'cburton@uat.com'

SET IDENTITY_INSERT CS_Settings OFF

GO