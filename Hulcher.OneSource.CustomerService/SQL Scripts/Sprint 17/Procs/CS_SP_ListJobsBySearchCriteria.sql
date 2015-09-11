ALTER PROCEDURE [dbo].[CS_SP_ListJobsBySearchCriteria] (
	@CustomerInfoType		varchar(200), 
	@JobInfoType			varchar(200), 
	@LocationInfoType		varchar(200),  
	@JobDescriptionType		varchar(200), 
	@EquipmentType			int, 
	@ResourceType			varchar(200),  
	@CustomerInfoValue		varchar(200), 
	@JobInfoValue			varchar(200), 
	@LocationInfoValue		varchar(200), 
	@JobDescriptionValue	varchar(200),  
	@EquipmentValue			varchar(200), 
	@ResourceValue			varchar(200), 
	@StartDate				datetime, 
	@EndDate				datetime
)
AS
BEGIN

	SELECT
		j.ID AS [JobId]
	FROM   
		CS_Job j (NOLOCK)  
			INNER JOIN CS_CustomerInfo ci (NOLOCK) ON j.ID = ci.JobId  
				INNER JOIN CS_Customer cust (NOLOCK) ON ci.CustomerId = cust.id  
				LEFT JOIN CS_Contact primContact (NOLOCK) on ci.InitialCustomerContactId = primContact.ID  
				 LEFT JOIN CS_Contact eicContact (NOLOCK) ON ci.EicContactId = eicContact.ID  
				 LEFT JOIN CS_Contact addContact (NOLOCK) ON ci.AdditionalContactId = addContact.ID  
				 LEFT JOIN CS_Contact billToContact (NOLOCK) ON ci.BillToContactId = billToContact.ID  
				 LEFT JOIN CS_Division divisionContact (NOLOCK) ON ci.DivisionId = divisionContact.ID  
				 LEFT JOIN CS_Employee employeContact (NOLOCK) ON ci.PocEmployeeId = employeContact.ID  
			INNER JOIN CS_JobInfo jobInfo (NOLOCK) ON j.ID = jobInfo.JobID  
				INNER JOIN CS_Job_JobStatus j_jobStatus (NOLOCK) ON j.ID = j_jobStatus.JobID AND j_jobStatus.Active = 1  
					INNER JOIN CS_JobStatus jobStatus (NOLOCK) ON j_jobStatus.JobStatusId = jobStatus.ID
				INNER JOIN CS_PriceType priceType (NOLOCK) ON jobInfo.PriceTypeID = priceType.ID  
				INNER JOIN CS_JobCategory jobCategory (NOLOCK) ON jobInfo.JobCategoryID = jobCategory.ID  
				INNER JOIN CS_JobType jobType (NOLOCK) ON jobInfo.JobTypeID = jobType.ID  
				INNER JOIN CS_JobAction jobAction (NOLOCK) ON jobInfo.JobActionID = jobAction.ID
				LEFT JOIN CS_Employee managerContact (NOLOCK) ON jobInfo.ProjectManager = managerContact.ID      
				LEFT JOIN CS_Frequency freq (NOLOCK) ON jobInfo.FrequencyID = freq.ID  
			INNER JOIN CS_JobDivision j_Divisions (NOLOCK) on j.ID = j_Divisions.JobID AND j_Divisions.Active = 1  
				INNER JOIN CS_Division jobDivision (NOLOCK) ON j_Divisions.DivisionID = jobDivision.ID  
			INNER JOIN CS_LocationInfo locInfo (NOLOCK) ON j.ID = locInfo.JobID  
				LEFT JOIN CS_Country country (NOLOCK) ON locInfo.CountryID = country.ID  
				LEFT JOIN CS_State [state] (NOLOCK) ON locInfo.StateID = [state].ID  
				LEFT JOIN CS_City city (NOLOCK) ON locInfo.CityID = city.ID  
				LEFT JOIN CS_ZipCode zip (NOLOCK) ON locInfo.ZipCodeId = zip.ID  
			INNER JOIN CS_JobDescription jobDesc (NOLOCK) ON j.ID = jobDesc.JobId  
			LEFT JOIN CS_PresetInfo preset (NOLOCK) ON j.ID = preset.JobId AND preset.Active = 1  
			LEFT JOIN CS_LostJobInfo lostJob (NOLOCK) ON j.ID = lostJob.JobId AND lostJob.Active = 1  
				LEFT JOIN CS_LostJobReason lostReason (NOLOCK) ON lostJob.ReasonID = lostReason.ID  
				LEFT JOIN CS_Competitor competitor (NOLOCK) ON lostJob.CompetitorID = competitor.ID  
				LEFT JOIN CS_Employee lostPOC (NOLOCK) ON lostJob.EmployeeID = lostPOC.ID  
			LEFT JOIN CS_Resource [resource] (NOLOCK) ON j.ID = [resource].JobID AND resource.Active = 1
				LEFT JOIN CS_Equipment equipment(NOLOCK) ON [resource].EquipmentID = equipment.ID  
					LEFT JOIN CS_CallLogResource clResource (NOLOCK) ON equipment.ID = clResource.EquipmentID AND clResource.Active = 1  
						LEFT JOIN CS_CallLog callLog (NOLOCK) ON clResource.CallLogID = callLog.ID  
							LEFT JOIN CS_CallType callType (NOLOCK) ON callLog.CallTypeID = callType.ID  
				LEFT JOIN CS_Employee employeeResource (NOLOCK) ON [resource].EmployeeID = employeeResource.ID  
	WHERE
		 j.Active = 1  
	 AND (@CustomerInfoType = 'none'  
	  OR ((@CustomerInfoType = 'Customer' OR @CustomerInfoType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@CustomerInfoValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, cust.Name) AS int)) > 0))  
	  OR ((@CustomerInfoType = 'InitialCustomerContact' OR @CustomerInfoType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@CustomerInfoValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, (isnull(primContact.LastName,'') + ', ' + isnull(primContact.Name, '') + ' "' + ISNULL(primContact.Alias, '') + '"')) AS int)) > 0))  
	  OR ((@CustomerInfoType = 'EIC' OR @CustomerInfoType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@CustomerInfoValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, (ISNULL(eicContact.LastName, '') + ', ' + isnull(eicContact.Name,'') + ' "' + ISNULL(eicContact.Alias, '') + '"')) AS int)) > 0))  
	  OR ((@CustomerInfoType = 'Secondary' OR @CustomerInfoType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@CustomerInfoValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, (ISNULL(addContact.LastName, '') + ', ' + isnull(addContact.Name,'') + ' "' + ISNULL(addContact.Alias, '') + '"')) AS int)) > 0))  
	  OR ((@CustomerInfoType = 'BillTo' OR @CustomerInfoType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@CustomerInfoValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, (ISNULL(billToContact.LastName, '') + ', ' + isnull(billToContact.Name,'') + ' "' + ISNULL(billToContact.Alias, '') + '"')) AS int)) > 0))  
	  OR ((@CustomerInfoType = 'Division' OR @CustomerInfoType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@CustomerInfoValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, divisionContact.Name) AS int)) > 0))  
	  OR ((@CustomerInfoType = 'POC' OR @CustomerInfoType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@CustomerInfoValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, (ISNULL(employeContact.Name, '') + ', ' + isnull(employeContact.FirstName,''))) AS int)) > 0))  
	  OR ((@CustomerInfoType = 'ProjectManager' OR @CustomerInfoType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@CustomerInfoValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, (ISNULL(managerContact.Name, '') + ', ' + isnull(managerContact.FirstName,''))) AS int)) > 0)))  	 
	 AND (@JobInfoType = 'none'  
	  OR ((@JobInfoType = 'InitialCallDate' OR @JobInfoType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@JobInfoValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, convert(varchar,jobInfo.InitialCallDate, 101)) AS int)) > 0))  
	  OR ((@JobInfoType = 'InitialCallTime' OR @JobInfoType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@JobInfoValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, convert(varchar,jobInfo.InitialCallTime, 108)) AS int)) > 0))  
	  OR ((@JobInfoType = 'JobStatus' OR @JobInfoType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@JobInfoValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, jobStatus.Description) AS int)) > 0))  
	  OR ((@JobInfoType = 'Division' OR @JobInfoType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@JobInfoValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, jobDivision.Name) AS int)) > 0))  
	  OR ((@JobInfoType = 'PriceType' OR @JobInfoType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@JobInfoValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, priceType.Acronym + ' - ' + priceType.Description) AS int)) > 0))  
	  OR ((@JobInfoType = 'JobCategory' OR @JobInfoType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@JobInfoValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, jobCategory.Description) AS int)) > 0))  
	  OR ((@JobInfoType = 'JobType' OR @JobInfoType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@JobInfoValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, jobType.Description) AS int)) > 0))  
	  OR ((@JobInfoType = 'JobAction' OR @JobInfoType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@JobInfoValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, jobAction.Description) AS int)) > 0))  
	  OR ((@JobInfoType = 'JobStartDate' OR @JobInfoType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@JobInfoValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, convert(varchar,j_jobStatus.JobStartDate, 101)) AS int)) > 0))  
	  OR ((@JobInfoType = 'JobEndDate' OR @JobInfoType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@JobInfoValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, convert(varchar,j_jobStatus.JobCloseDate, 101)) AS int)) > 0))  
	  OR ((@JobInfoType = 'PresetDate' OR @JobInfoType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@JobInfoValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, convert(varchar,preset.Date, 101)) AS int)) > 0))  
	  OR ((@JobInfoType = 'PresetTime' OR @JobInfoType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@JobInfoValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, convert(varchar,preset.Time, 108)) AS Int)) > 0))  
	  OR ((@JobInfoType = 'LostJobReason' OR @JobInfoType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@JobInfoValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, lostReason.Description) AS Int)) > 0))  
	  OR ((@JobInfoType = 'LostCompetitor' OR @JobInfoType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@JobInfoValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, competitor.Description) AS Int)) > 0))  
	  OR ((@JobInfoType = 'LostPOC' OR @JobInfoType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@JobInfoValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, (ISNULL(lostPOC.Name, '') + ', ' + isnull(lostPOC.FirstName,''))) AS Int)) > 0))  
	  OR ((@JobInfoType = 'Frequency' OR @JobInfoType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@JobInfoValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, freq.Description) AS Int)) > 0)))  
	 AND (@LocationInfoType = 'none'  
	  OR ((@LocationInfoType = 'Country' OR @LocationInfoType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@LocationInfoValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, country.Name) AS Int)) > 0))  
	  OR ((@LocationInfoType = 'State' OR @LocationInfoType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@LocationInfoValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, (state.Acronym + ' ' + state.Name)) AS Int)) > 0))  
	  OR ((@LocationInfoType = 'City' OR @LocationInfoType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@LocationInfoValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, city.Name) AS Int)) > 0))  
	  OR ((@LocationInfoType = 'ZipCode' OR @LocationInfoType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@LocationInfoValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, zip.Name) AS Int)) > 0))  
	  OR ((@LocationInfoType = 'SiteName' OR @LocationInfoType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@LocationInfoValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, locInfo.SiteName) AS Int)) > 0))  
	  OR ((@LocationInfoType = 'Alternate' OR @LocationInfoType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@LocationInfoValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, locInfo.AlternateName) AS Int)) > 0)))  
	 AND (@JobDescriptionType = 'none'  
	  OR ((@JobDescriptionType = 'Engines' OR @JobDescriptionType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@JobDescriptionValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, jobDesc.NumberEngines) AS Int)) > 0))  
	  OR ((@JobDescriptionType = 'Loads' OR @JobDescriptionType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@JobDescriptionValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, jobDesc.NumberLoads) AS Int)) > 0))  
	  OR ((@JobDescriptionType = 'Empties' OR @JobDescriptionType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@JobDescriptionValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, jobDesc.NumberEmpties) AS Int)) > 0))  
	  OR ((@JobDescriptionType = 'Lading' OR @JobDescriptionType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@JobDescriptionValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, jobDesc.Lading) AS Int)) > 0))  
	  OR ((@JobDescriptionType = 'UN' OR @JobDescriptionType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@JobDescriptionValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, jobDesc.UnNumber) AS Int)) > 0))  
	  OR ((@JobDescriptionType = 'STCC' OR @JobDescriptionType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@JobDescriptionValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, jobDesc.STCCInfo) AS Int)) > 0))  
	  OR ((@JobDescriptionType = 'Hazmat' OR @JobDescriptionType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@JobDescriptionValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, jobDesc.Hazmat) AS Int)) > 0)))  
	 AND (@EquipmentType = 0
	  OR (equipment.EquipmentTypeID = @EquipmentType AND EXISTS(SELECT 1 FROM Split2Varchar(@EquipmentValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, equipment.Name) AS Int)) > 0)))  
	 AND (@ResourceType = 'none'  
	  OR ((@ResourceType = 'Status' OR @ResourceType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@ResourceValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, equipment.Status) AS Int)) > 0))   
	  OR ((@ResourceType = 'CallType' OR @ResourceType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@ResourceValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, callType.Description) AS Int)) > 0))   
	  OR ((@ResourceType = 'Employee' OR @ResourceType = 'ALL') AND EXISTS(SELECT 1 FROM Split2Varchar(@ResourceValue, ',') AS [union] WHERE (CAST(CHARINDEX([union].Data, (ISNULL(employeeResource.Name, '') + ', ' + isnull(employeeResource.FirstName,''))) AS Int)) > 0)))  
	 AND (j.ModificationDate between @StartDate AND @EndDate)  
	GROUP BY  
		j.ID

END

