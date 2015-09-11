
CREATE TABLE #CallLogTemp(
ID int,
CallLogDate datetime)

insert into #CallLogTemp
SELECT  cLog.ID,
		res.StartDateTime as Dt
FROM
	CS_CallLog cLog
	LEFT OUTER JOIN CS_Resource res on cLog.JobID = res.JobID
WHERE cLog.CallTypeID = 27

insert into #CallLogTemp
SELECT ID,
	CAST(CAST([Xml] AS XML).value('(/DynamicFieldsAggregator/Controls/DynamicControls[Name="dtpDate"]/Text)[1]','varchar(5000)') AS datetime)	
	+
	CAST(CAST([Xml] AS XML).value('(/DynamicFieldsAggregator/Controls/DynamicControls[Name="txtTime"]/Text)[1]','varchar(5000)') AS datetime)	
FROM
	CS_CallLog
where ISDATE(CAST([Xml] AS XML).value('(/DynamicFieldsAggregator/Controls/DynamicControls[Name="dtpDate"]/Text)[1]','varchar(5000)')) = 1


update r
set r.ActionDate = t.CallLogDate
from CS_CallLogResource r INNER JOIN #CallLogTemp t ON t.ID = r.CallLogID
where ActionDate is null

drop table #CallLogTemp