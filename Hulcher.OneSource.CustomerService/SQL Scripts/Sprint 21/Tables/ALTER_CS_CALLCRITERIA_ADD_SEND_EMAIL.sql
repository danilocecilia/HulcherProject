ALTER TABLE CS_CallType
ADD SendEmail bit NOT NULL DEFAULT 1

UPDATE CS_CallType set SendEmail=0 where ID = 1