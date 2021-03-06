USE [OneSource]
GO

/****** Object:  View [dbo].[CS_View_EmployeeInfo]    Script Date: 09/16/2011 13:35:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [dbo].[CS_View_EmployeeInfo]    
AS            
        
	SELECT DISTINCT
		 e.ID									AS [EmployeeId]
		,CASE
			WHEN e.Nickname IS NULL THEN e.Name + ', ' + e.FirstName
			ELSE e.Name + ', ' + e.FirstName + ' "' + e.Nickname + '"'
		 END									AS [EmployeeName]
		,e.BusinessCardTitle					AS [Position]
		,e.Active								AS [Active]
		,d.ID									AS [DivisionId]
		,CASE
			WHEN cd.ID IS NULL THEN d.Name
			ELSE 'C ' + cd.Name + '/' + d.Name
		 END									AS [DivisionName]
		,s.ID									AS [StateId]
		,s.Name									AS [State]
		,r.JobID								AS [JobId]
		,CASE
			WHEN r.JobID IS NOT NULL THEN 'Assigned'
			WHEN offCall.EmployeeID IS NOT NULL THEN 'Unavailable'
			ELSE 'Available'
		 END									AS [Assigned]
		,CASE
			WHEN j.Number = '' THEN j.Internal_Tracking
			WHEN j.Number IS NULL THEN j.Internal_Tracking
			ELSE j.Number
		 END									AS [JobNumber]
		,CASE
			WHEN j.Number = '' THEN ptype.Acronym + jtype.Description + j.Internal_Tracking
			WHEN j.Number IS NULL THEN ptype.Acronym + jtype.Description + j.Internal_Tracking
			ELSE ptype.Acronym + jtype.Description + j.Number
		 END									AS [PrefixedNumber]
		,ptype.Acronym							AS [PriceTypeAcronym]
		,jtype.[Description]					AS [JobTypeDescription]
	FROM
		CS_Employee e (NOLOCK)
			INNER JOIN CS_Division d (NOLOCK) ON e.DivisionID = d.ID
				LEFT OUTER JOIN CS_State s (NOLOCK) ON d.StateId = s.ID AND d.CountryId = s.CountryID
			LEFT OUTER JOIN CS_Resource r (NOLOCK) ON e.ID = r.EmployeeID AND r.Active = 1
				LEFT OUTER JOIN CS_Job j (NOLOCK) ON r.JobID = j.ID
					LEFT OUTER JOIN CS_JobInfo ji (NOLOCK) ON j.ID = ji.JobID
						LEFT OUTER JOIN CS_PriceType ptype ON ptype.ID = ji.PriceTypeID
						LEFT OUTER JOIN CS_JobType jtype ON jtype.ID = ji.JobTypeID
			LEFT OUTER JOIN CS_EmployeeOffCallHistory offCall (NOLOCK) ON offCall.EmployeeID = e.ID and offCall.Active = 1
			LEFT OUTER JOIN CS_EmployeeCoverage ec (NOLOCK) ON e.ID = ec.EmployeeID and ec.Active = 1
				LEFT OUTER JOIN CS_Division cd (NOLOCK) ON ec.DivisionID = cd.ID        
	WHERE
		e.Active = 1

GO


