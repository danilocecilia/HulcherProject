ALTER TABLE CS_Customer
ADD AlertNotification varchar(max) null

go

ALTER TABLE CS_Customer
ADD OperatorAlert bit null

go

ALTER TABLE CS_Customer
ADD CreditCheck bit not null DEFAULT 0

go

