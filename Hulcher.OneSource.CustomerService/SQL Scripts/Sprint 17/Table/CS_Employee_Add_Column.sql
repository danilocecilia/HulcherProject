ALTER TABLE dbo.CS_Employee
ADD IsKeyPerson bit not null;

update CS_Employee
set IsKeyPerson = 0
