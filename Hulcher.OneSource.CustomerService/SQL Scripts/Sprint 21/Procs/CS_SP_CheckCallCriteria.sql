CREATE PROCEDURE [dbo].[CS_SP_CheckCallCriteria] (  
 @CallLogId int,
 @CallTypeId int,
 @JobId int
)  
AS  
BEGIN  
  
 SET NOCOUNT ON;  
  
 DECLARE  
   @CustomerId  int,  
   @JobStatus  int,  
   @PriceType  int,  
   @JobCategory int,  
   @JobType  int,  
   @JobAction  int,  
   @InterimBill bit,  
   @GeneralLog  bit,  
   @Country  int,  
   @State   int,  
   @City   int,  
   @CarCount  int,  
   @Commodities bit,  
   @Chemicals  bit,  
   @Heavy   bit,  
   @NonHeavy  bit,  
   @AllEq   bit,  
   @WhiteLight  bit
   
 DECLARE @JobDivision table (Id int)  
 
 IF @CallLogId IS NOT NULL AND @CallLogId > 0
 BEGIN
	 SELECT @JobId = JobId,  
	   @CallTypeId = CallTypeID  
	   FROM CS_CallLog (NOLOCK)  
	  WHERE ID = @CallLogId  
 END
    
 SELECT @CustomerId = CustomerId  
   FROM CS_CustomerInfo (NOLOCK)  
  WHERE JobId = @Jobid  
    
 INSERT INTO @JobDivision  
  SELECT DivisionId  
    FROM CS_JobDivision (NOLOCK)  
   WHERE JobID = @JobId AND Active = 1  
    
 DECLARE @CallCriteriaResult table (  
   CallCriteriaID  int,  
   CallCriteriaTypeID int,  
   Value    varchar(5),  
   IsAnd    bit)  
      
 IF @CustomerId IS NOT NULL AND @JobId IS NOT NULL  
 BEGIN  
      
     -- INSERTING CALL CRITERIA DATA INTO THE TEMPORARY TABLE  
  INSERT INTO @CallCriteriaResult  
  SELECT 
		a.CallCriteriaID,  
		a.CallCriteriaTypeID,  
		a.Value,  
		a.IsAnd  
	FROM 
		CS_CallCriteriaValue a (NOLOCK)  
		INNER JOIN CS_CallCriteria b (NOLOCK) on a.CallCriteriaID = b.ID  
   WHERE 
		a.Active = 1  
		AND b.Active = 1  
		AND 
		(  
			(b.CustomerID IS NULL AND  LTRIM(RTRIM(ISNULL(b.SystemWideLevel, ''))) = '' AND b.DivisionID IS NOT NULL AND b.DivisionID IN (SELECT Id FROM @JobDivision))  
			OR (b.DivisionID IS NULL AND b.CustomerID IS NOT NULL AND b.CustomerID = @CustomerId)  
			OR (b.CustomerID IS NULL and b.DivisionID IS NULL AND LTRIM(RTRIM(ISNULL(b.SystemWideLevel, ''))) <> '')  
		)  
       
  -- GETTING JOB INFORMATION  
  SELECT @JobStatus = JobStatusID  
    FROM CS_Job_JobStatus (NOLOCK)  
   WHERE JobID = @JobId AND Active = 1  
     
  SELECT @PriceType = PriceTypeID,  
    @JobCategory = JobCategoryID,  
    @JobType = JobTypeID,  
    @JobAction = JobActionID,  
    @InterimBill = InterimBill  
    FROM CS_JobInfo (NOLOCK)  
   WHERE JobID = @JobId AND Active = 1  
     
  IF @JobId = 1  
   SET @GeneralLog = 1  
  ELSE  
   SET @GeneralLog = 0  
    
  SELECT @Country = CountryID,  
    @State = StateID,  
    @City = CityID  
    FROM CS_LocationInfo (NOLOCK)  
   WHERE JobID = @JobId AND Active = 1  
     
  SELECT @CarCount = ISNULL(NumberEmpties, 0) + ISNULL(NumberEngines, 0) + ISNULL(NumberLoads, 0),  
    @Commodities = CASE WHEN Lading IS NULL THEN 0 ELSE 1 END,  
    @Chemicals = CASE WHEN (UnNumber IS NOT NULL OR STCCInfo IS NOT NULL OR Hazmat IS NOT NULL) THEN 1 ELSE 0 END  
    FROM CS_JobDescription (NOLOCK)  
   WHERE JobID = @JobId AND Active = 1  
     
  SELECT @Heavy = CASE WHEN COUNT(*) = 0 THEN 0 ELSE 1 END  
    FROM CS_CallLogResource a (NOLOCK)  
    INNER JOIN CS_Equipment b (NOLOCK) ON a.EquipmentID = b.ID  
   WHERE a.CallLogID = @CallLogId AND a.Active = 1 AND b.HeavyEquipment = 1  
     
  SELECT @NonHeavy = CASE WHEN COUNT(*) = 0 THEN 0 ELSE 1 END  
    FROM CS_CallLogResource a (NOLOCK)  
    INNER JOIN CS_Equipment b (NOLOCK) ON a.EquipmentID = b.ID  
   WHERE a.CallLogID = @CallLogId AND a.Active = 1 AND b.HeavyEquipment = 0  
     
  SELECT @AllEq = CASE WHEN COUNT(*) = 0 THEN 0 ELSE 1 END  
    FROM CS_CallLogResource a (NOLOCK)  
    INNER JOIN CS_Equipment b (NOLOCK) ON a.EquipmentID = b.ID  
   WHERE a.CallLogID = @CallLogId AND a.Active = 1  
     
  SELECT @WhiteLight = CASE WHEN COUNT(*) = 0 THEN 0 ELSE 1 END  
    FROM CS_CallLogResource a (NOLOCK)  
    INNER JOIN CS_Equipment b (NOLOCK) ON a.EquipmentID = b.ID  
     INNER JOIN CS_EquipmentWhiteLight c (NOLOCK) ON c.EquipmentID = b.ID  
   WHERE a.CallLogID = @CallLogId AND a.Active = 1 and c.Active = 1  
     
  -- REMOVING UNMATCHED CALL CRITERIA TYPES (ONLY CALLTYPEID, CUSTOMER, DIVISION AND 'AND' CASES)  
  DELETE   
    FROM @CallCriteriaResult  
  WHERE
  CallCriteriaId IN 
  (
	  SELECT 
		CallCriteriaId 
	  FROM 
	    @CallCriteriaResult
	  WHERE 
	    CallCriteriaTypeID = 19  
	    AND Value <> Convert(varchar, @CallTypeId)
	    AND CallCriteriaID NOT IN (SELECT CallCriteriaID FROM @CallCriteriaResult WHERE Value = Convert(varchar, @CallTypeId))
  )
  
  DELETE  
    FROM @CallCriteriaResult  
  WHERE
  CallCriteriaId IN 
  (
	  SELECT 
		CallCriteriaId 
	  FROM 
	    @CallCriteriaResult
	  WHERE CallCriteriaTypeID = 1  
		 AND IsAnd = 1
		 AND Value <> Convert(varchar, @CustomerId)
		 AND CallCriteriaID NOT IN (SELECT CallCriteriaID FROM @CallCriteriaResult WHERE Value = Convert(varchar, @CustomerId))
   )  
   
  DELETE  
    FROM @CallCriteriaResult  
  WHERE
  CallCriteriaId IN 
  (
	  SELECT 
		CallCriteriaId 
	  FROM 
	    @CallCriteriaResult
      WHERE CallCriteriaTypeID = 2 
	     AND IsAnd = 1 
         AND Value NOT IN ( SELECT CONVERT(varchar, Id) FROM @JobDivision )
         AND CallCriteriaID NOT IN (SELECT CallCriteriaID FROM @CallCriteriaResult WHERE Value IN (  SELECT CONVERT(varchar, Id) FROM @JobDivision  ))
   )
   
   DELETE   
    FROM @CallCriteriaResult  
   WHERE
   CallCriteriaId IN 
   (
	  SELECT 
		CallCriteriaId 
	  FROM 
	    @CallCriteriaResult
      WHERE CallCriteriaTypeID = 3  
         AND IsAnd = 1  
         AND Value <> Convert(varchar, @JobStatus)  
         AND CallCriteriaID NOT IN (SELECT CallCriteriaID FROM @CallCriteriaResult WHERE Value = Convert(varchar, @JobStatus))
   )
  
  
  DELETE   
    FROM @CallCriteriaResult  
  WHERE
  CallCriteriaId IN 
   (
	  SELECT 
		CallCriteriaId 
	  FROM 
	    @CallCriteriaResult
      WHERE CallCriteriaTypeID = 4  
        AND IsAnd = 1  
        AND Value <> Convert(varchar, @PriceType)  
        AND CallCriteriaID NOT IN (SELECT CallCriteriaID FROM @CallCriteriaResult WHERE Value = Convert(varchar, @PriceType))
    )
    
    
  DELETE   
    FROM @CallCriteriaResult
  WHERE
   CallCriteriaId IN 
   (
	  SELECT 
		CallCriteriaId 
	  FROM 
	    @CallCriteriaResult  
      WHERE CallCriteriaTypeID = 5  
        AND IsAnd = 1  
        AND Value <> Convert(varchar, @JobCategory)  
        AND CallCriteriaID NOT IN (SELECT CallCriteriaID FROM @CallCriteriaResult WHERE Value = Convert(varchar, @JobCategory))
    )
    
  DELETE   
    FROM @CallCriteriaResult  
  WHERE
   CallCriteriaId IN 
   (
	  SELECT 
		CallCriteriaId 
	  FROM 
	    @CallCriteriaResult  
      WHERE CallCriteriaTypeID = 6  
        AND IsAnd = 1  
        AND Value <> Convert(varchar, @JobType)  
        AND CallCriteriaID NOT IN (SELECT CallCriteriaID FROM @CallCriteriaResult WHERE Value = Convert(varchar, @JobType))
    )
    
  DELETE   
    FROM @CallCriteriaResult  
  WHERE
   CallCriteriaId IN 
   (
	  SELECT 
		CallCriteriaId 
	  FROM 
	    @CallCriteriaResult  
      WHERE CallCriteriaTypeID = 7  
        AND IsAnd = 1  
        AND Value <> Convert(varchar, @JobAction)  
        AND CallCriteriaID NOT IN (SELECT CallCriteriaID FROM @CallCriteriaResult WHERE Value = Convert(varchar, @JobAction))
    )
    
  DELETE   
    FROM @CallCriteriaResult  
  WHERE
   CallCriteriaId IN 
   (
	  SELECT 
		CallCriteriaId 
	  FROM 
	    @CallCriteriaResult  
      WHERE CallCriteriaTypeID = 8  
        AND IsAnd = 1  
        AND Value <> Convert(varchar, @InterimBill)  
        AND CallCriteriaID NOT IN (SELECT CallCriteriaID FROM @CallCriteriaResult WHERE Value = Convert(varchar, @InterimBill))
    )
       
  DELETE   
    FROM @CallCriteriaResult
  WHERE
    CallCriteriaId IN 
    (
	  SELECT 
		CallCriteriaId 
	  FROM 
	    @CallCriteriaResult  
      WHERE CallCriteriaTypeID = 9  
        AND IsAnd = 1  
        AND Value <> Convert(varchar, @GeneralLog)  
        AND CallCriteriaID NOT IN (SELECT CallCriteriaID FROM @CallCriteriaResult WHERE Value = Convert(varchar, @GeneralLog))
     )
     
  DELETE   
    FROM @CallCriteriaResult  
  WHERE
    CallCriteriaId IN 
    (
	  SELECT 
		CallCriteriaId 
	  FROM 
	    @CallCriteriaResult  
      WHERE CallCriteriaTypeID = 10  
        AND IsAnd = 1  
        AND Value <> Convert(varchar, @Country)  
        AND CallCriteriaID NOT IN (SELECT CallCriteriaID FROM @CallCriteriaResult WHERE Value = Convert(varchar, @Country))
     )
     
  DELETE   
    FROM @CallCriteriaResult  
  WHERE
    CallCriteriaId IN 
    (
	  SELECT 
		CallCriteriaId 
	  FROM 
	    @CallCriteriaResult  
      WHERE CallCriteriaTypeID = 11  
        AND IsAnd = 1  
        AND Value <> Convert(varchar, @State)  
        AND CallCriteriaID NOT IN (SELECT CallCriteriaID FROM @CallCriteriaResult WHERE Value = Convert(varchar, @State))
    )
    
  DELETE   
    FROM @CallCriteriaResult  
  WHERE
    CallCriteriaId IN 
    (
	  SELECT 
		CallCriteriaId 
	  FROM 
	    @CallCriteriaResult  
      WHERE CallCriteriaTypeID = 12  
        AND IsAnd = 1  
        AND Value <> Convert(varchar, @City)  
        AND CallCriteriaID NOT IN (SELECT CallCriteriaID FROM @CallCriteriaResult WHERE Value = Convert(varchar, @City))
    )
    
  DELETE   
    FROM @CallCriteriaResult 
  WHERE
    CallCriteriaId IN 
    (
	  SELECT 
		CallCriteriaId 
	  FROM 
	    @CallCriteriaResult   
      WHERE CallCriteriaTypeID = 13  
        AND IsAnd = 1  
        AND 
		(  
		(SUBSTRING(Value, 1, 1) = '>' AND CONVERT(int, SUBSTRING(Value, 2, LEN(Value) - 1)) <= @CarCount) OR  
		(SUBSTRING(Value, 1, 1) = '<' AND CONVERT(int, SUBSTRING(Value, 2, LEN(Value) - 1)) >= @CarCount) OR  
		(SUBSTRING(Value, 1, 1) <> '<' AND SUBSTRING(Value, 1, 1) <> '>' AND CONVERT(int, Value) <> @CarCount)  
		)  
		AND CallCriteriaID NOT IN (SELECT CallCriteriaID FROM @CallCriteriaResult WHERE Value = Convert(varchar, @CarCount))
    )
    
  DELETE   
    FROM @CallCriteriaResult 
  WHERE
    CallCriteriaId IN 
    (
	  SELECT 
		CallCriteriaId 
	  FROM 
	    @CallCriteriaResult    
      WHERE CallCriteriaTypeID = 14  
        AND IsAnd = 1  
        AND Value <> Convert(varchar, @Commodities)  
        AND CallCriteriaID NOT IN (SELECT CallCriteriaID FROM @CallCriteriaResult WHERE Value = Convert(varchar, @Commodities))
     )
     
  DELETE   
    FROM @CallCriteriaResult  
  WHERE
    CallCriteriaId IN 
    (
	  SELECT 
		CallCriteriaId 
	  FROM 
	    @CallCriteriaResult   
      WHERE CallCriteriaTypeID = 15  
        AND IsAnd = 1  
        AND Value <> Convert(varchar, @Chemicals)  
        AND CallCriteriaID NOT IN (SELECT CallCriteriaID FROM @CallCriteriaResult WHERE Value = Convert(varchar, @Chemicals))
     )
       
  DELETE   
    FROM @CallCriteriaResult
  WHERE
    CallCriteriaId IN 
    (
	  SELECT 
		CallCriteriaId 
	  FROM 
	    @CallCriteriaResult    
      WHERE CallCriteriaTypeID = 16  
        AND IsAnd = 1  
        AND Value <> Convert(varchar, @Heavy) 
        AND CallCriteriaID NOT IN (SELECT CallCriteriaID FROM @CallCriteriaResult WHERE Value = Convert(varchar, @Heavy)) 
     )
       
  DELETE   
    FROM @CallCriteriaResult  
  WHERE
    CallCriteriaId IN 
    (
	  SELECT 
		CallCriteriaId 
	  FROM 
	    @CallCriteriaResult    
      WHERE CallCriteriaTypeID = 17  
        AND IsAnd = 1  
        AND Value <> Convert(varchar, @NonHeavy) 
        AND CallCriteriaID NOT IN (SELECT CallCriteriaID FROM @CallCriteriaResult WHERE Value = Convert(varchar, @NonHeavy)) 
    )
       
  DELETE   
    FROM @CallCriteriaResult
  WHERE
    CallCriteriaId IN 
    (
	  SELECT 
		CallCriteriaId 
	  FROM 
	 @CallCriteriaResult      
      WHERE CallCriteriaTypeID = 18  
        AND IsAnd = 1  
        AND Value <> Convert(varchar, @AllEq)  
        AND CallCriteriaID NOT IN (SELECT CallCriteriaID FROM @CallCriteriaResult WHERE Value = Convert(varchar, @AllEq)) 
    )
    
  DELETE   
    FROM @CallCriteriaResult  
  WHERE
    CallCriteriaId IN 
    (
	  SELECT 
		CallCriteriaId 
	  FROM 
	    @CallCriteriaResult      
      WHERE CallCriteriaTypeID = 20  
        AND IsAnd = 1  
        AND Value <> Convert(varchar, @WhiteLight)  
        AND CallCriteriaID NOT IN (SELECT CallCriteriaID FROM @CallCriteriaResult WHERE Value = Convert(varchar, @WhiteLight)) 
     )
    
 END  
  
 -- FILTERING 'OR' CASES  
 SELECT DISTINCT  
  CallCriteriaID  
 FROM  
  @CallCriteriaResult  
 WHERE  
  (CallCriteriaTypeID = 1 AND IsAnd = 0 AND Value = CONVERT(varchar, @CustomerId))  
  OR (CallCriteriaTypeID = 2 AND IsAnd = 0 AND Value IN (SELECT CONVERT(varchar, Id) FROM @JobDivision))  
  OR (CallCriteriaTypeID = 3 AND IsAnd = 0 AND Value = CONVERT(varchar, @JobStatus))
  OR (CallCriteriaTypeID = 4 AND IsAnd = 0 AND Value = CONVERT(varchar, @PriceType))  
  OR (CallCriteriaTypeID = 5 AND IsAnd = 0 AND Value = CONVERT(varchar, @JobCategory))  
  OR (CallCriteriaTypeID = 6 AND IsAnd = 0 AND Value = CONVERT(varchar, @JobType))  
  OR (CallCriteriaTypeID = 7 AND IsAnd = 0 AND Value = CONVERT(varchar, @JobAction))  
  OR (CallCriteriaTypeID = 8 AND IsAnd = 0 AND Value = CONVERT(varchar, @InterimBill))  
  OR (CallCriteriaTypeID = 9 AND IsAnd = 0 AND Value = CONVERT(varchar, @GeneralLog))  
  OR (CallCriteriaTypeID = 10 AND IsAnd = 0 AND Value = CONVERT(varchar, @Country))  
  OR (CallCriteriaTypeID = 11 AND IsAnd = 0 AND Value = CONVERT(varchar, @State))  
  OR (CallCriteriaTypeID = 12 AND IsAnd = 0 AND Value = CONVERT(varchar, @City))  
  OR (CallCriteriaTypeID = 13 AND IsAnd = 0 AND (  
   VALUE = CONVERT(VARCHAR, @CarCount) OR  
   VALUE = '<' + CONVERT(VARCHAR, @CarCount) OR  
   VALUE = '>' + CONVERT(VARCHAR, @CarCount)  
  ))  
  OR (CallCriteriaTypeID = 14 AND IsAnd = 0 AND Value = CONVERT(varchar, @Commodities))  
  OR (CallCriteriaTypeID = 15 AND IsAnd = 0 AND Value = CONVERT(varchar, @Chemicals))  
  OR (CallCriteriaTypeID = 16 AND IsAnd = 0 AND Value = CONVERT(varchar, @Heavy))  
  OR (CallCriteriaTypeID = 17 AND IsAnd = 0 AND Value = CONVERT(varchar, @NonHeavy))  
  OR (CallCriteriaTypeID = 18 AND IsAnd = 0 AND Value = CONVERT(varchar, @AllEq))  
  OR (CallCriteriaTypeID = 20 AND IsAnd = 0 AND Value = CONVERT(varchar, @WhiteLight))  
 UNION
 SELECT DISTINCT  
  CallCriteriaID  
 FROM  
  @CallCriteriaResult  
 WHERE IsAnd = 1
    
END


