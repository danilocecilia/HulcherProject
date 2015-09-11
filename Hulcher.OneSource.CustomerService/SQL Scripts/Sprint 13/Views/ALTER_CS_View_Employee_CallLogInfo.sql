USE [OneSource]
GO

/****** Object:  View [dbo].[CS_View_Employee_CallLogInfo]    Script Date: 09/16/2011 13:35:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [dbo].[CS_View_Employee_CallLogInfo]
AS
      
	SELECT
		 e.ID					AS [EmployeeID]
		,e.Name					AS [EmployeeName]      
		,e.FirstName			AS [EmployeeFirstName]
		,e.Nickname				AS [EmployeeNickname]
		,e.DivisionID			AS [DivisionID]
		,CASE --COVERAGE
			WHEN cd.ID IS NOT NULL THEN 'C ' + cd.Name + '/' + d.Name  
			ELSE d.Name  
		 END					AS [DivisionName]
		,'Safety 10+ cars'		AS [Advise Note]
		,'000'					AS [VMX]
		,CAST(
			CASE 
				WHEN (e.Active = 1 AND (d.Active = 1 OR d.Active IS NULL)) THEN 1 
				ELSE 0 
			END AS BIT
		 )						AS [Active]      
	FROM               
		CS_Employee AS e (NOLOCK)
			LEFT OUTER JOIN CS_Division AS d (NOLOCK) ON e.DivisionID = d.ID 
			LEFT OUTER JOIN CS_EmployeeCoverage c (NOLOCK) ON e.ID = c.EmployeeID 
				LEFT OUTER JOIN CS_Division cd (NOLOCK) ON c.DivisionID = cd.ID
	 
GO