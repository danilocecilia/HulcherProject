<%@ Page Title="" Language="C#" MasterPageFile="~/ContentPage.master" AutoEventWireup="true"
    CodeBehind="EssentialJobData.aspx.cs" Inherits="Hulcher.OneSource.CustomerService.Web.EssentialJobData" %>

<%@ MasterType TypeName="Hulcher.OneSource.CustomerService.Web.ContentPage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/AutoCompleteTextbox.ascx" TagName="AutoCompleteTextbox" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Styles/EssentialJobData.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="PageContent" ContentPlaceHolderID="Content" runat="server">
    <div style="min-width: 1000px;" >
    <asp:Panel ID="pnlForDefaultButton" runat="server" DefaultButton="btnSaveContinue">
        <asp:UpdatePanel ID="upContent" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSaveContinue" />
            </Triggers>
            <ContentTemplate>
                <div id="summary">
                    <asp:ValidationSummary ID="vsQuickJob" runat="server" CssClass="errorbox" ValidationGroup="QuickJob"
                        HeaderText="Please correct the following information" />
                </div>
                <div id="mainTitle" class="Header">
                    <asp:Label ID="lblTitle" Text="Essential Job Data" runat="server"></asp:Label>
                </div>
                <div class="Content">
                    <div class="Row">
                        <div class="Right" style="margin-right: 2%;">
                            <asp:Label ID="lblEmergency" Text="Emergency: " runat="server"></asp:Label>
                           <asp:RadioButtonList ID="rblEmergencyResponse" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Text="No" Value="False" Selected="True" />
                                <asp:ListItem Text="Yes" Value="True" />
                           </asp:RadioButtonList>
                        </div>
                    </div>
                    <br />
                    <div id="firstRow" class="Row">
                        <div>
                            <div class="labels2">
                                <asp:Label ID="lblPrimaryContact" runat="server" Text="* Primary Contact" CssClass="Label"></asp:Label>
                            </div>
                            <div class="controls">
                                <uc1:AutoCompleteTextbox ID="actContact" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                    GridViewIdName="ID" DisplayField="Name" AutoPostBack="false" RequiredField="true"
                                    ValidationGroup="QuickJob" ErrorMessage="The Primary Contact field is required" WindowTitle="Quick Job - Find Contact"
                                    AutoCompleteSource="CustomerServiceContact" ColumnHeaderList="Name" TextBoxCssClass="input"
                                    ColumnValueList="FullContactInformation" ServiceMethod="GetCustomerServiceContactList"
                                    ScriptToExecute="CallCustomerWebService" TextBoxWidth="120px" /> 
                                    <asp:HyperLink ID="lnkAddNewContact" runat="server" NavigateUrl="javascript: var newWindow = window.open('/CustomerMaintenance.aspx?ViewType=CONTACT&NewContact=True&RefField=actContact','','width=1200,height=600, scrollbars=1, resizable=yes');" Text="Add New Contact" ></asp:HyperLink>
                            </div>
                        </div>
                        <div>
                            <div class="labels">
                                <asp:Label ID="lblCustomer" runat="server" Text="* Company" CssClass="Label"></asp:Label>
                            </div>
                            <div class="controls">
                                <uc1:AutoCompleteTextbox ID="actCustomer" runat="server" ServiceMethod="GetCustomerList"
                                    GridViewButtonImageUrl="~/Images/money.png" GridViewIdName="ID" DisplayField="Name"
                                    TextBoxWidth="120px" AutoPostBack="false" RequiredField="true" ValidationGroup="QuickJob"
                                    ErrorMessage="The Company field is required" WindowTitle="Quick Job - Find Company"
                                    AutoCompleteSource="Customer" ColumnHeaderList="Name,Attn,Company ID" ColumnValueList="Name,Attn,CustomerNumber"
                                    ControlsToUpdate="actContact" OnTextChanged="actCustomer_TextChanged"  ScriptToExecute="UpdateContactHyperlink"/>
                                    <asp:HyperLink ID="hlAddNewCustomer" runat="server" NavigateUrl="javascript: var newWindow = window.open('/CustomerMaintenance.aspx?ViewType=CUSTOMER&NewCustomer=true&RefField=actCustomer','','width=1200, height=600, scrollbars=1, resizable=yes');" Text="Add New Company"></asp:HyperLink>
                            </div>
                        </div>
                        <div>
                            <div class="labels2">
                                <asp:Label ID="lblHulcherContact" runat="server" Text="* Hulcher Contact" CssClass="Label"></asp:Label>
                            </div>
                            <div class="controls">
                                <uc1:AutoCompleteTextbox ID="actHulcherContact" runat="server" ServiceMethod="GetEmployeeList"
                                    TextBoxWidth="120px" GridViewButtonImageUrl="~/Images/money.png" GridViewIdName="ID"
                                    DisplayField="Name" FilterId="0" ContextKey="0" AutoPostBack="false" RequiredField="true"
                                    WindowTitle="Quick Job - Find Employee" ErrorMessage="Hulcher Contact field is required"
                                    AutoCompleteSource="Employee" ColumnHeaderList="Division - Name" TextBoxCssClass="input"
                                    ColumnValueList="DivisionAndFullName" ValidationGroup="QuickJob" ScriptToExecute="CallDivisionWebService" />

                                    <asp:HiddenField ID="hidCollections" runat="server" Value="0"/>
                            </div>
                        </div>
                        <div>
                            <div class="labels2">
                                <asp:Label ID="lblPrimaryDivision" runat="server" Text="* Primary Division"></asp:Label>
                            </div>
                            <div class="controls">
                                <uc1:AutoCompleteTextbox ID="actDivision" runat="server" ServiceMethod="GetDivisionList" TextBoxWidth="120px"
                                    GridViewButtonImageUrl="~/Images/money.png" GridViewIdName="ID" DisplayField="Division" 
                                    FilterId="0" ContextKey="0" AutoPostBack="false" RequiredField="true" WindowTitle="Quick Job - Find Division" 
                                    ErrorMessage="Primary Division field is required" AutoCompleteSource="Division" 
                                    ColumnHeaderList="Name,Description" TextBoxCssClass="input" ColumnValueList="Name,Description"
                                    ValidationGroup="QuickJob" BehaviorId="Division" />
                            </div>
                        </div>
                    </div>
                    <br>
                    </br>
                    <div id="secondRow" class="Row">
                        <div>
                            <div class="labels">
                                <asp:Label ID="lblJobStatus" runat="server" Text="* Job Status"></asp:Label>
                            </div>
                            <div class="controls">
                                <uc1:AutoCompleteTextbox ID="actJobStatus" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                    TextBoxWidth="120px" GridViewIdName="ID" DisplayField="" AutoPostBack="false"
                                    RequiredField="true" WindowTitle="Quick Job - Find Job Status" ErrorMessage="Job Status field is required"
                                    AutoCompleteSource="JobStatusJobRecord" ColumnHeaderList="Description" ColumnValueList="Description"
                                    ServiceMethod="GetJobStatusListForJobRecord" ValidationGroup="QuickJob" TextBoxCssClass="input" MinimumPrefixLength="1" />
                            </div>
                        </div>
                        <div>
                            <div class="labels">
                                <asp:Label ID="lblPriceType" runat="server" Text="* Price Type"></asp:Label>
                            </div>
                            <div class="controls">
                                <uc1:AutoCompleteTextbox ID="actPriceType" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                    TextBoxWidth="120px" GridViewIdName="ID" DisplayField="" AutoPostBack="false"
                                    RequiredField="true" WindowTitle="Quick Job - Find Price Type" ErrorMessage="Price Type field is required"
                                    AutoCompleteSource="PriceType" ColumnHeaderList="Description" ColumnValueList="Description"
                                    ServiceMethod="GetPriceTypeList" ValidationGroup="QuickJob" TextBoxCssClass="input" MinimumPrefixLength="1" />
                            </div>
                        </div>
                        <div>
                            <div class="labels">
                                <asp:Label ID="lblJobAction" runat="server" Text="* Job Action"></asp:Label>
                            </div>
                            <div class="controls">
                                <uc1:AutoCompleteTextbox ID="actJobAction" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                    TextBoxWidth="120px" GridViewIdName="ID" DisplayField="Description" AutoPostBack="false"
                                    RequiredField="true" WindowTitle="Quick Job - Find Job Action" ErrorMessage="Job Action field is required"
                                    AutoCompleteSource="JobAction" ColumnHeaderList="Description" ColumnValueList="Description"
                                    ServiceMethod="GetJobActionList" ValidationGroup="QuickJob" TextBoxCssClass="input" />
                            </div>
                        </div>
                    </div>
                    <br>
                    </br>
                    <div id="thirdRow" class="Row">
                        <div>
                            <div class="labels">
                                <asp:Label ID="lblState" runat="server" Text="* State"></asp:Label>
                            </div>
                            <div class="controls">
                                <uc1:AutoCompleteTextbox ID="actState" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                    TextBoxWidth="120px" GridViewIdName="ID" DisplayField="" AutoPostBack="false"
                                    RequiredField="true" WindowTitle="Quick Job - Find State" ErrorMessage="State field is required"
                                    AutoCompleteSource="State" ColumnHeaderList="Acronym,Name" ColumnValueList="Acronym,Name" ServiceMethod="GetStateList"
                                    ValidationGroup="QuickJob" TextBoxCssClass="input" ControlsToUpdate="actCity"
                                    OnTextChanged="actState_TextChanged" ScriptToExecute="SetFocusToCity" MinimumPrefixLength="2" />
                            </div>
                        </div>
                        <div>
                            <div class="labels">
                                <asp:Label ID="lblCity" runat="server" Text="* City"></asp:Label>
                            </div>
                            <div class="controls">
                                <uc1:AutoCompleteTextbox ID="actCity" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                    TextBoxWidth="120px" GridViewIdName="ID" DisplayField="Name" AutoPostBack="false"
                                    RequiredField="true" WindowTitle="Quick Job - Find City" ErrorMessage="City field is required"
                                    AutoCompleteSource="City" ColumnHeaderList="Name" ColumnValueList="Name" ServiceMethod="GetCityList"
                                    ValidationGroup="QuickJob" TextBoxCssClass="input" BehaviorId="actCity" ControlsToUpdate="actZipCode"
                                    OnTextChanged="actCity_TextChanged" ScriptToExecute="CallCityWebService" />
                            </div>
                        </div>
                        <div>
                            <div class="labels">
                                <asp:Label ID="lvlZipCode" runat="server" Text="* Zip Code"></asp:Label>
                            </div>
                            <div class="controls">
                                <uc1:AutoCompleteTextbox ID="actZipCode" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                    TextBoxWidth="120px" GridViewIdName="ID" DisplayField="" AutoPostBack="false"
                                    RequiredField="true" WindowTitle="Quick Job - Find Division" ErrorMessage="Zip Code field is required"
                                    AutoCompleteSource="ZipCode" ColumnHeaderList="Name" ColumnValueList="Name" ServiceMethod="GetZipCodeList"
                                    ValidationGroup="QuickJob" TextBoxCssClass="input" BehaviorId="actZipCode" />
                            </div>
                        </div>
                    </div>
                    <br />
                    <div id="fourthRow" class="Row">
                        <div>
                            <div class="labels2">
                                <asp:Label ID="lblCallDate" Text="* Initial<br />Call Date" runat="server"></asp:Label>
                            </div>
                            <div class="controls">
                                <uc1:DatePicker ID="dpCallDate" InvalidValueMessage="The Call Date format is invalid"
                                    ValidationGroup="QuickJob" EmptyValueMessage="The Call Date field is required"
                                    DateTimeFormat="Default" ShowOn="Both" runat="server" Value="01/01/2011" IsValidEmpty="false">
                                </uc1:DatePicker>
                            </div>
                        </div>
                        <div>
                            <div class="labels2">
                                <asp:Label ID="lblCallTime" runat="server" Text="* Initial<br />Call Time"></asp:Label>
                            </div>
                            <div class="controls">
                                <asp:TextBox ID="txtCallTime" runat="server" CssClass="input" Width="40px" Text="01:01"></asp:TextBox>
                                <uc1:MaskedEditExtender ID="mskInitialTime" TargetControlID="txtCallTime" runat="server"
                                    Mask="99:99" MaskType="Time" AcceptAMPM="false" UserTimeFormat="TwentyFourHour"
                                    AutoComplete="true">
                                </uc1:MaskedEditExtender>
                                <uc1:MaskedEditValidator ID="rfvTimeValidator" runat="server" ControlExtender="mskInitialTime"
                                    ControlToValidate="txtCallTime" IsValidEmpty="false" EnableClientScript="true"
                                    Display="Dynamic" InvalidValueBlurredMessage="*" InvalidValueMessage="The Call Time format is invalid"
                                    EmptyValueMessage="The Call Time field is required" EmptyValueBlurredText="*"
                                    ValidationGroup="QuickJob">
                                </uc1:MaskedEditValidator>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div id="fifthRow">
                        <div>
                            <div class="scopeLabel">
                                <asp:Label ID="lblScopeEquipment" runat="server" Text="* Scope Of Work/Requested Equipment:"></asp:Label>
                                <asp:RequiredFieldValidator ID="rfvScopeEquipment" runat="server" ErrorMessage="The Scope of Work/Requested Equipment field is required"
                                    EnableClientScript="true" ControlToValidate="txtScopeEquipment" ValidationGroup="QuickJob"
                                    Display="Dynamic" Text="*"></asp:RequiredFieldValidator>
                            </div>
                            <div style="text-align: center; width: 100%;">
                                <asp:CountableTextBox ID="txtScopeEquipment" runat="server" TextMode="MultiLine"
                                    Width="98%" Height="100px" CssClass="input" MaxChars="255" MaxCharsWarning="200"></asp:CountableTextBox>
                            </div>
                        </div>
                        <br />
                        <div id="buttons" class="Row">
                            <div class="Right">
                                <asp:Button ID="btnSaveContinue" Text="Save & Continue" runat="server" ValidationGroup="QuickJob"
                                    OnClick="SaveAndContinue_OnClick" CssClass="btn" OnClientClick="ValidateFields(); DisableButtons();" />
                                <input id="btnCancel" type="button" value="Cancel" class="btn" onclick="window.close();" />
                            </div>
                        </div>
                        <div id="savingMessage" style="display: none;">
                            <p align="right"><b>Saving Job Record...</b></p>
                        </div>
                    </div>
                </div>
                <script type="text/javascript" language="javascript" defer="defer">

                    $(document).ready(function () { ValidateFields() });

               

                    function ValidateFields() {
                        var primaryContact = document.getElementById('<%= actContact.TextControlClientID %>');
                        var hulcherContact = document.getElementById('<%= actHulcherContact.TextControlClientID %>');

                        if ("" != hulcherContact.value) {
                            ValidatorEnable($get('<%= actContact.RequiredFieldClientId %>'), false);
                        }
                        else if ("" != primaryContact.value) {
                            ValidatorEnable($get('<%= actHulcherContact.RequiredFieldClientId %>'), false);
                        }
                        else {
                            ValidatorEnable($get('<%= actContact.RequiredFieldClientId %>'), true);
                            ValidatorEnable($get('<%= actHulcherContact.RequiredFieldClientId %>'), true);
                        }
                    }

                    function CallDivisionWebService(employeeId) {
                        if (employeeId != '0') {
                            tempuri.org.IJSONService.GetDivision(employeeId, CallDivisionWebServiceCompleted);
                        }
                    }

                    function CallDivisionWebServiceCompleted(WebServiceResult) {
                        var divisionControl = $find('Division');
                        var divisionName = WebServiceResult.Name.replace(/[^0-9]/, '');

                        try {
                            divisionName = parseInt(divisionName);
                        }
                        catch (err) {
                            divisionName = 0;
                        }

                        if (divisionName < 97) {
                            divisionControl.raiseItemSelected(new Sys.Extended.UI.AutoCompleteItemEventArgs(null, WebServiceResult.Name, WebServiceResult.Id));
                            document.getElementById('<%= actJobStatus.TextControlClientID %>').focus();
                        }
                        else {
                            document.getElementById('<%=actDivision.TextControlClientID %>').focus();
                        }
                    }

                    function CallCustomerWebService(contactId) {
                        if (contactId != 0) {
                            if (document.getElementById('<%=actCustomer.HiddenFieldValueClientID %>').value == '0')
                                tempuri.org.IJSONService.GetCustomer(contactId, CallCustomerWebServiceCompleted);
                        }
                    }

                    function CallCustomerWebServiceCompleted(WebServiceResult) {
                        var customerControl = $find('actCustomer');
                        customerControl.raiseItemSelected(new Sys.Extended.UI.AutoCompleteItemEventArgs(null, WebServiceResult.Name, WebServiceResult.Id));
                        document.getElementById('<%=actHulcherContact.TextControlClientID %>').focus();
                        UpdateContactHyperlink(WebServiceResult.Id);
                    }

                    function UpdateContactHyperlink(customerId) {
                        var actCustomer = document.getElementById('<%= actCustomer.HiddenFieldValueClientID %>');
                        var hlAddNewContact = document.getElementById('<%= lnkAddNewContact.ClientID %>');

                        if (customerId != 0) {
                            hlAddNewContact.href = "javascript: var newWindow = window.open('/CustomerMaintenance.aspx?ViewType=CONTACT&CustomerID=" + customerId + "&NewContact=True&RefField=actCustomerContact','','width=1200,height=600, scrollbars=1, resizable=yes');";
                            
                        }
                        else {
                            hlAddNewContact.href = "javascript: var newWindow = window.open('/CustomerMaintenance.aspx?ViewType=CONTACT&NewContact=True&RefField=actCustomerContact','','width=1200,height=600, scrollbars=1, resizable=yes');";
                        }

                        setTimeout(function () { CustomerIsCollection(customerId); }, 100);
                    }

                    function CustomerIsCollection(customerId) {
                        
                        if (customerId != 0) {
                            tempuri.org.IJSONService.IsCustomerCollection(customerId, CustomerIsCollectionCompleted);
                        }
                    }

                    function CustomerIsCollectionCompleted(result) {
                        var collections = document.getElementById('<%= hidCollections.ClientID %>');
                        var selectedValue = document.getElementById('<%=actCustomer.HiddenFieldValueClientID %>').value

                        if (result != null && result.Collection && selectedValue != collections.value) {

                            alert("Company account is in collections");
                            collections.value = selectedValue;
                        }
                        else if (result == null || !result.Collection) {
                            collections.value = selectedValue;
                        }
                    }

                    function SetFocusToCity(WebServiceResult) {
                        document.getElementById('<%=actCity.TextControlClientID%>').focus();
                    }

                    function CallCityWebService(cityId) {
                        if (cityId != 0) {
                            tempuri.org.IJSONService.GetZipCodeByCity(cityId, CallCityWebServiceCompleted)
                        }
                    }

                    function CallCityWebServiceCompleted(WebServiceResult) {
                        var zipCodeControl = $find('actZipCode');
                        zipCodeControl.raiseItemSelected(new Sys.Extended.UI.AutoCompleteItemEventArgs(null, WebServiceResult.Name, WebServiceResult.Id));
                        document.getElementById('<%=txtScopeEquipment.ClientID%>').focus();
                    }


                    function DisableButtons() {
                        Page_ClientValidate("QuickJob");
                        if (Page_IsValid) {
                            setTimeout(DisableSaveContinue, 200);
                        }
                    }

                    function DisableSaveContinue() {
                        document.getElementById('<%= btnSaveContinue.ClientID %>').disabled = true;
                        document.getElementById('savingMessage').style.display = 'inline';
                    }

                    function SetAutocompleteField(Name, ID, Field) {
                        var actField = $find(Field);
                        actField.raiseItemSelected(new Sys.Extended.UI.AutoCompleteItemEventArgs(null, Name, ID));
                    }

                </script>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    </div>
</asp:Content>
