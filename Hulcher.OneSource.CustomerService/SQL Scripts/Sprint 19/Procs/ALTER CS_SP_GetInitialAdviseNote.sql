ALTER PROCEDURE [CS_SP_GetInitialAdviseNote] (
	@isEmployee		BIT,
	@personID		INT
)
AS
BEGIN

	DECLARE @type			VARCHAR(255),
			@value			VARCHAR(255),
			@lastType		VARCHAR(255),
			@description	VARCHAR(255),
			@adviseNote		VARCHAR(5000)
			
	SET @adviseNote = ''
			
	DECLARE TEMP_CURSOR CURSOR FOR
	SELECT
		b.[Description],
		a.[Value]
	FROM
		CS_CallCriteriaValue a (NOLOCK)
			INNER JOIN CS_CallCriteriaType b (NOLOCK) ON a.CallCriteriaTypeID = b.ID
			INNER JOIN CS_CallCriteria c (NOLOCK) ON a.CallCriteriaID = c.ID
	WHERE
		a.Active = 1 AND
		c.Active = 1 AND
		c.EmployeeID = CASE WHEN @isEmployee = 1 THEN @personID ELSE NULL END OR
		c.ContactID = CASE WHEN @isEmployee = 0 THEN @personID ELSE NULL END
	ORDER BY
		b.[Description]
		
	OPEN TEMP_CURSOR
	FETCH NEXT
	 FROM TEMP_CURSOR
	 INTO @type,
		  @value

	WHILE @@FETCH_STATUS = 0
	BEGIN

		IF @type = 'All Equipment' 
			SET @description = ''
		ELSE IF @type = 'Car Count'
			SET @description = @value
		ELSE IF @type = 'Chemicals'
			SET @description = 
				CASE @value
					WHEN 1 THEN 'UN#'
					WHEN 2 THEN 'STTCC Info'
					WHEN 3 THEN 'Hazmat'
				END
		ELSE IF @type = 'Commodities'
			SET @description = 'Lading'
		ELSE IF @type = 'General Log'
			SET @description = ''
		ELSE IF @type = 'Heavy Equipment'
			SET @description = ''
		ELSE IF @type = 'Non-Heavy-Equipment'
			SET @description = ''
		ELSE IF @type = 'Call Type'
			SELECT @description = [Description] FROM CS_CallType (NOLOCK) WHERE ID = CONVERT(INT, @value)
		ELSE IF @type = 'City'
			SELECT @description = CASE [CSRecord] WHEN 1 THEN '* ' + [Name] ELSE [Name] END FROM CS_City (NOLOCK) WHERE ID = CONVERT(INT, @value)
		ELSE IF @type = 'Country'
			SELECT @description = [Name] FROM CS_Country (NOLOCK) WHERE ID = CONVERT(INT, @value)
		ELSE IF @type = 'Customer'
			SELECT @description = [Name] FROM CS_Customer (NOLOCK) WHERE ID = CONVERT(INT, @value)
		ELSE IF @type = 'Division'
			SELECT @description = [Name] FROM CS_Division (NOLOCK) WHERE ID = CONVERT(INT, @value)
		ELSE IF @type = 'Interim Billing'
			SELECT @description = [Description] FROM CS_Frequency (NOLOCK) WHERE ID = CONVERT(INT, @value)
		ELSE IF @type = 'Job Action'
			SELECT @description = [Description] FROM CS_JobAction (NOLOCK) WHERE ID = CONVERT(INT, @value)
		ELSE IF @type = 'Job Category'
			SELECT @description = [Description] FROM CS_JobCategory (NOLOCK) WHERE ID = CONVERT(INT, @value)
		ELSE IF @type = 'Job Status'
			SELECT @description = [Description] FROM CS_JobStatus (NOLOCK) WHERE ID = CONVERT(INT, @value)
		ELSE IF @type = 'Job Type'
			SELECT @description = [Description] FROM CS_JobType (NOLOCK) WHERE ID = CONVERT(INT, @value)
		ELSE IF @type = 'Price Type'
			SELECT @description = [Description] FROM CS_PriceType (NOLOCK) WHERE ID = CONVERT(INT, @value)
		ELSE IF @type = 'State'
			SELECT @description = [Acronym] FROM CS_State (NOLOCK) WHERE ID = CONVERT(INT, @value)
		
		IF @description <> ''
		BEGIN
			SET @description = LTRIM(RTRIM(@description))
			
			IF @lastType = @type
			BEGIN
				SET @adviseNote = @adviseNote + ', ' + @description
			END
			ELSE
			BEGIN
				IF @adviseNote <> ''
					SET @adviseNote = @adviseNote + '; ' + @type + ': ' + @description
				ELSE
					SET @adviseNote = @type + ': ' + @description
			END
		END	
		
		SET @lastType = @type
		
		FETCH NEXT
		 FROM TEMP_CURSOR
		 INTO @type,
			  @value
	END

	CLOSE TEMP_CURSOR
	DEALLOCATE TEMP_CURSOR

	SELECT @adviseNote

END


