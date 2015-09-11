update CS_CallType
set [Description] = 'Advise'
where ID=1

update CS_CallType
set Active=0
where id=34

update CS_CallType
set Xml = '<?xml version="1.0" encoding="utf-8"?>  <DynamicFieldsAggregator xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">    <Controls>      <DynamicControls xsi:type="DynamicCountableTextBoxXml">        <Name>txtNote</Name>        <IsRequired>false</IsRequired>        <MaxChars>255</MaxChars>        <MaxCharsWarning>250</MaxCharsWarning>        <TextMode>MultiLine</TextMode>        <Width>300</Width>        <Height>150</Height>        <Label>          <Text>Note:</Text>          <Css>dynamicLabel</Css>        </Label>        <Css>input</Css>      </DynamicControls>    </Controls>  </DynamicFieldsAggregator>'
where ID=1