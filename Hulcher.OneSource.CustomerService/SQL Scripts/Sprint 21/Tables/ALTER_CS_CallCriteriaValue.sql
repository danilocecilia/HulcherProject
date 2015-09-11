
ALTER TABLE CS_CallCriteriaValue
ADD IsAnd bit

UPDATE [OneSource].[dbo].[CS_CallCriteriaValue]
   SET [IsAnd] = 0