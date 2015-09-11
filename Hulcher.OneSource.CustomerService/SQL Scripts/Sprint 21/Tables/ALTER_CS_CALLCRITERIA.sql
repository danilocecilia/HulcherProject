ALTER TABLE CS_CallCriteria
ADD DivisionID int 

ALTER TABLE CS_CallCriteria
ADD CONSTRAINT [FK_DivisionID]
FOREIGN KEY ([DivisionID])
REFERENCES CS_Division([ID])

ALTER TABLE CS_CallCriteria
ADD CustomerID int 

ALTER TABLE CS_CallCriteria
ADD CONSTRAINT [FK_CustomerID]
FOREIGN KEY ([CustomerID])
REFERENCES CS_Customer([ID])

ALTER TABLE CS_CallCriteria
ADD SystemWideLevel varchar(50) 

ALTER TABLE CS_CallCriteria
ADD Notes varchar(255) 


