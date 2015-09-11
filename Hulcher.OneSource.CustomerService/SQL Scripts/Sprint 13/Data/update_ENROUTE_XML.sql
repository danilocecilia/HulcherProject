update CS_CallType set Xml = '<?xml version="1.0" encoding="utf-8"?>
<DynamicFieldsAggregator xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Controls>
    <DynamicControls xsi:type="DynamicCountableTextBoxXml">
      <Name>txtNote</Name>
      <IsRequired>false</IsRequired>
      <MaxChars>255</MaxChars>
      <MaxCharsWarning>250</MaxCharsWarning>
      <TextMode>MultiLine</TextMode>
      <Width>300</Width>
      <Height>150</Height>
      <Label>
        <Text>Note:</Text>
        <Css>dynamicLabel</Css>
      </Label>
      <Css>input</Css>
    </DynamicControls>
    <DynamicControls xsi:type="DynamicDatePickerXml">
      <Name>dtpDate</Name>
      <IsValidEmpty>false</IsValidEmpty>
      <EmptyValueMessage>The Enroute Date field is required</EmptyValueMessage>
      <InvalidValueMessage>Invalid Enroute Date format</InvalidValueMessage>
      <DateTimeFormat>Default</DateTimeFormat>
      <ShowOn>Both</ShowOn>
      <ValidationGroup>CallEntry</ValidationGroup>
      <Label>
        <Text>Enroute Date:</Text>
        <Css>dynamicLabel</Css>
      </Label>
      <Css>input</Css>
    </DynamicControls>
    <DynamicControls xsi:type="DynamicTimeXml">
      <Name>txtTime</Name>
      <IsValidEmpty>false</IsValidEmpty>
      <ValidationGroup>CallEntry</ValidationGroup>
      <EmptyValueMessage>The field Enroute Time is required.</EmptyValueMessage>
      <InvalidValueMessage>The field Enroute Time is invalid.</InvalidValueMessage>
      <MaskedType>Time</MaskedType>
      <Mask>99:99</Mask>
      <Label>
        <Text>Enroute Time:</Text>
        <Css>dynamicLabel</Css>
      </Label>
      <Css>input</Css>
    </DynamicControls>
    <DynamicControls xsi:type="DynamicTextBoxXml">
      <Name>txtDestinationFrom</Name>
      <IsRequired>true</IsRequired>
      <ValidationGroup>CallEntry</ValidationGroup>
      <ErrorMessage>The field Destination From is required.</ErrorMessage>
      <Label>
        <Text>Destination From:</Text>
        <Css>dynamicLabel</Css>
      </Label>
      <Css>input</Css>
    </DynamicControls>
    <DynamicControls xsi:type="DynamicTextBoxXml">
      <Name>txtDestinationTo</Name>
      <IsRequired>true</IsRequired>
      <ValidationGroup>CallEntry</ValidationGroup>
      <ErrorMessage>The field Destination To is required.</ErrorMessage>
      <Label>
        <Text>Destination To:</Text>
        <Css>dynamicLabel</Css>
      </Label>
      <Css>input</Css>
    </DynamicControls>
    <DynamicControls xsi:type="DynamicDropDownListXml">
      <Name>drpModeOfTransportation</Name>
      <IsRequired>false</IsRequired>
      <Label>
        <Text>Mode of Transportation:</Text>
        <Css>dynamicLabel</Css>
      </Label>
      <Css/>
    </DynamicControls>
    <DynamicControls xsi:type="DynamicDatePickerXml">
      <Name>dtpDateOfArrival</Name>
      <Visible>false</Visible>
      <IsValidEmpty>false</IsValidEmpty>
      <EmptyValueMessage>The Expected Date of Arriaval field is required</EmptyValueMessage>
      <InvalidValueMessage>Invalid Expected Date of Arriaval format</InvalidValueMessage>
      <DateTimeFormat>Default</DateTimeFormat>
      <ShowOn>Both</ShowOn>
      <ValidationGroup>CallEntry</ValidationGroup>
      <Label>
        <Text>* Expected Date Of Arrival:</Text>
        <Css>dynamicLabel</Css>
      </Label>
      <Css>input</Css>
    </DynamicControls>
    <DynamicControls xsi:type="DynamicTimeXml">
      <Name>txtTimeOfArrival</Name>
      <Visible>false</Visible>
      <IsValidEmpty>true</IsValidEmpty>
      <ValidationGroup>CallEntry</ValidationGroup>
      <EmptyValueMessage>The Expected Time Of Arrival field is required.</EmptyValueMessage>
      <InvalidValueMessage>The Expected Time Of Arrival field is invalid.</InvalidValueMessage>
      <MaskedType>Time</MaskedType>
      <Mask>99:99</Mask>
      <Label>
        <Text>Expected Time Of Arrival:</Text>
        <Css>dynamicLabel</Css>
      </Label>
      <Css>input</Css>
    </DynamicControls>
  </Controls>
  <Extenders>
    <Extenders xsi:type="ListExtenderXml">
      <TargetControlName>drpModeOfTransportation</TargetControlName>
      <Type>Custom</Type>
      <Items xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
        <ListExtenderItem>
          <Name>- Select One -</Name>
          <Value></Value>
          <Selected>true</Selected>
        </ListExtenderItem>
        <ListExtenderItem>
          <Name>Air (Non Hulcher Plane) Commercial</Name>
          <Value>1</Value>
          <Selected>false</Selected>
        </ListExtenderItem>
        <ListExtenderItem>
          <Name>Air (Non Hulcher Plane) Charter</Name>
          <Value>2</Value>
          <Selected>false</Selected>
        </ListExtenderItem>
        <ListExtenderItem>
          <Name>Ground</Name>
          <Value>3</Value>
          <Selected>false</Selected>
        </ListExtenderItem>
      </Items>
    </Extenders>
    <Extenders xsi:type="VisibleExtenderXml">
      <CallerControlName>drpModeOfTransportation</CallerControlName>
      <TargetControlName>dtpDateOfArrival</TargetControlName>
      <TargetValue>1</TargetValue>
      <Visible>true</Visible>
    </Extenders>
    <Extenders xsi:type="VisibleExtenderXml">
      <CallerControlName>drpModeOfTransportation</CallerControlName>
      <TargetControlName>dtpDateOfArrival</TargetControlName>
      <TargetValue>2</TargetValue>
      <Visible>true</Visible>
    </Extenders>
    <Extenders xsi:type="VisibleExtenderXml">
      <CallerControlName>drpModeOfTransportation</CallerControlName>
      <TargetControlName>dtpDateOfArrival</TargetControlName>
      <TargetValue>3</TargetValue>
      <Visible>false</Visible>
    </Extenders>
    <Extenders xsi:type="VisibleExtenderXml">
      <CallerControlName>drpModeOfTransportation</CallerControlName>
      <TargetControlName>dtpDateOfArrival</TargetControlName>
      <TargetValue>0</TargetValue>
      <Visible>false</Visible>
    </Extenders>
    <Extenders xsi:type="VisibleExtenderXml">
      <CallerControlName>drpModeOfTransportation</CallerControlName>
      <TargetControlName>txtTimeOfArrival</TargetControlName>
      <TargetValue>1</TargetValue>
      <Visible>true</Visible>
    </Extenders>
    <Extenders xsi:type="VisibleExtenderXml">
      <CallerControlName>drpModeOfTransportation</CallerControlName>
      <TargetControlName>txtTimeOfArrival</TargetControlName>
      <TargetValue>2</TargetValue>
      <Visible>true</Visible>
    </Extenders>
    <Extenders xsi:type="VisibleExtenderXml">
      <CallerControlName>drpModeOfTransportation</CallerControlName>
      <TargetControlName>txtTimeOfArrival</TargetControlName>
      <TargetValue>3</TargetValue>
      <Visible>false</Visible>
    </Extenders>
    <Extenders xsi:type="VisibleExtenderXml">
      <CallerControlName>drpModeOfTransportation</CallerControlName>
      <TargetControlName>txtTimeOfArrival</TargetControlName>
      <TargetValue>0</TargetValue>
      <Visible>false</Visible>
    </Extenders>
    <Extenders xsi:type="ValidationExtenderXml">
      <TargetControlName>txtDestinationTo</TargetControlName>
      <CallerControlName>txtDestinationFrom</CallerControlName>
    </Extenders>
  </Extenders>
</DynamicFieldsAggregator>' where ID = 5