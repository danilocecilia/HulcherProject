INSERT INTO [OneSource].[dbo].[CS_CallType]
           ([ID]
           ,[Description]
           ,[Xml]
           ,[CallCriteria]
           ,[IsAutomaticProcess]
           ,[DpiStatus]
           ,[CreatedBy]
           ,[CreationDate]
           ,[CreationID]
           ,[ModifiedBy]
           ,[ModificationDate]
           ,[ModificationID]
           ,[Active]
           ,[ShowEmployee]
           ,[ShowEquipment])
     VALUES
           (46
           ,'CSX Update'
           ,'<?xml version="1.0" encoding="utf-8"?>  <DynamicFieldsAggregator xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">    <Controls>      <DynamicControls xsi:type="DynamicCountableTextBoxXml">        <Name>txtNote</Name>        <IsRequired>false</IsRequired>        <MaxChars>255</MaxChars>        <MaxCharsWarning>250</MaxCharsWarning>        <TextMode>MultiLine</TextMode>        <Width>300</Width>        <Height>150</Height>        <Label>          <Text>Note:</Text>          <Css>dynamicLabel</Css>        </Label>        <Css>input</Css>      </DynamicControls>    </Controls>  </DynamicFieldsAggregator>'
           ,0
           ,0
           ,NULL
           ,'System'
           ,'2012-02-08'
           ,325237	
           ,'System'
           ,'2012-02-08'
           ,325237
           ,1
           ,1
           ,1)
GO