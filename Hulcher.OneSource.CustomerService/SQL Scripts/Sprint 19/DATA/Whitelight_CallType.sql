insert into cs_calltype
select 45,'White Light', null, 0, 1, null, 'System', GETDATE(), 325237, 'System', GETDATE(), 325237, 1, 0 ,1

INSERT INTO CS_PrimaryCallType_CallType
SELECT 45, 5

INSERT INTO CS_PrimaryCallType_CallType
SELECT 45, 10

INSERT CS_CallCriteriaType VALUES (20, 'White Light', 'system', 'system', 0, 0, GETDATE(), GETDATE(), 1)
