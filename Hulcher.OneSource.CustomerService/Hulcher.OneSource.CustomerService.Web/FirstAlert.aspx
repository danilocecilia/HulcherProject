<%@ Page Title="First Alert" Language="C#" MasterPageFile="~/ContentPage.master"
    AutoEventWireup="true" CodeBehind="FirstAlert.aspx.cs" Inherits="Hulcher.OneSource.CustomerService.Web.FirstAlert" %>

<%@ MasterType TypeName="Hulcher.OneSource.CustomerService.Web.ContentPage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/AutoCompleteTextbox.ascx" TagName="AutoCompleteTextbox"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Styles/Forms.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/jquery.multiselect.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/formatFunctions.js" language="javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <asp:HiddenField ID="hfVehiclesAdded" runat="server" />
    <asp:HiddenField ID="hfLastVehicleTempID" runat="server" Value="0" />
    <div id="Title">
        <asp:UpdatePanel ID="upTitle" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:Label ID="lblTitle" runat="server" Text="First Alert" Font-Size="Large" Font-Bold="true"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <br />
    <div id="Page">
        <asp:Panel ID="pnlNoAccess" runat="server" Visible="false">
            <div class="Page">
                <div class="Header">
                    <asp:Label ID="lblTitleNoAccess" runat="server" Text="First Alert Details" />
                </div>
                <div class="Content">
                    The current user does not have access to this functionality!
                </div>
            </div>
        </asp:Panel>
        <asp:UpdatePanel ID="updVisualization" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="pnlVisualization" runat="server" DefaultButton="btnCreate">
                    <div id="divVisualization" class="Header">
                        <asp:Label ID="lblVisualizationTitle" runat="server" Text="First Alert List"></asp:Label>
                    </div>
                    <div class="Content">
                        <div id="divSearchFilters">
                            <div class="inline floatLeft">
                                <asp:Button ID="btnCreate" runat="server" Text="New First Alert" CssClass="btn" CausesValidation="false"
                                    OnClick="btnCreate_Click" />
                            </div>
                            <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearchAlert">
                                <div class="inlineBlock floatRight alignRight">
                                    <div class="floatLeft paddingTop5 paddingRight5">
                                        <asp:Label ID="lblFilterSearchAlert" runat="server" Text="Filter Listing By: " />
                                    </div>
                                    <div class="floatLeft paddingRight5">
                                        <asp:ComboBox ID="cbFilterSearchAlert" runat="server" CssClass="WindowsStyle" AutoCompleteMode="SuggestAppend"
                                            DropDownStyle="DropDown" CaseSensitive="false" RenderMode="Inline">
                                            <asp:ListItem Text="- Select One -" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Alert#" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Type" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Date" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Time" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="Job#" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="Division" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="Company" Value="7"></asp:ListItem>
                                            <asp:ListItem Text="Location" Value="8"></asp:ListItem>
                                        </asp:ComboBox>
                                    </div>
                                    <div class="floatLeft paddingRight5">
                                        <asp:TextBox ID="txtFilterSearchAlert" runat="server" CssClass="input" />
                                        <asp:TextBox ID="txtFilterDateAndTime" Style="display: none" runat="server" CssClass="input"></asp:TextBox>
                                        <asp:MaskedEditExtender ID="meeFilterValueDate" runat="server" MaskType="Date" Mask="99/99/9999"
                                            TargetControlID="txtFilterDateAndTime">
                                        </asp:MaskedEditExtender>
                                    </div>
                                    <div class="floatLeft paddingRight5 paddingTop2">
                                        <asp:Button ID="btnSearchAlert" runat="server" Text="Find" CssClass="btn" OnClick="btnSearchAlert_Click" />
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div id="divSearchGrid">
                            <asp:ScrollableGridView runat="server" ID="gvAlertViewer" AllowSorting="true" AutoGenerateColumns="false"
                                CssClass="ScrollableGridView" SaveScrollPosition="true" MaxHeight="400px" EnableViewState="true"
                                OnRowDataBound="gvAlertViewer_RowDataBound" OnRowCommand="gvAlertViewer_RowCommand"
                                DataKeyNames="ID">
                                <Columns>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:HiddenField runat="server" ID="hfAlertId" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Alert#" />
                                    <asp:BoundField HeaderText="Type" />
                                    <asp:BoundField HeaderText="Date/Time" />
                                    <asp:BoundField HeaderText="Job#" />
                                    <asp:BoundField HeaderText="Division" />
                                    <asp:BoundField HeaderText="Company" />
                                    <asp:BoundField HeaderText="Location" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="FirstAlertEdit"
                                                CausesValidation="false"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPrint" runat="server" Text="Print" CommandName="Print" CausesValidation="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:ScrollableGridView>
                        </div>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="updCreation" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="pnlCreation" runat="server" Visible="false">
                    <asp:ValidationSummary ID="vsFirstAlert" runat="server" CssClass="errorbox" ValidationGroup="FirstAlert"
                        HeaderText="Please correct the following information" />
                    <%--<asp:ValidationSummary ID="vsFirstAlertVehicles" runat="server" CssClass="errorbox"
                    ValidationGroup="FirstAlertVehicles" HeaderText="Please correct the following information" />
                <asp:ValidationSummary ID="vsFirstAlertPeople" runat="server" CssClass="errorbox"
                    ValidationGroup="FirstAlertPeople" HeaderText="Please correct the following information" />--%>
                    <asp:Panel ID="pnlHeader" runat="server">
                        <div class="Header">
                            <asp:Label ID="lblAlertHeaderTitle" runat="server" Text="First Alert Header"></asp:Label>
                        </div>
                        <div class="Content">
                            <asp:UpdatePanel ID="updHeader" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="space100 paddingBottom5">
                                        <div class="floatLeft space50 inline">
                                            <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                <asp:Label ID="lblAlertHeaderJobNumber" runat="server" Text="Job Number:"></asp:Label>
                                            </div>
                                            <div class="inline">
                                                <uc1:AutoCompleteTextbox ID="actAlertHeaderJobNumber" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                                    TextBoxWidth="200px" GridViewIdName="ID" DisplayField="" AutoPostBack="true"
                                                    OnTextChanged="actAlertHeaderJobNumber_TextChanged" WindowTitle="First Alert - Find Job Number"
                                                    AutoCompleteSource="JobNumber" ColumnHeaderList="Number,Company,Location" ColumnValueList="PrefixedNumber,CS_CustomerInfo.CS_Customer.Name,CS_LocationInfo.FullLocation"
                                                    ServiceMethod="GetJobNumberList" TextBoxCssClass="input" />
                                            </div>
                                        </div>
                                        <div class="space49 inlineBlock">
                                            <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                <asp:RequiredFieldValidator ID="rfvFirstAlertType" ValidationGroup="FirstAlert" ControlToValidate="txtFirstAlertType"
                                                    runat="server" ErrorMessage="The First Alert Type multiselect field is required. "
                                                    Text="*"></asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtFirstAlertType" runat="server" Style="display: none"></asp:TextBox>
                                                <asp:Label ID="lblFirstAlertType" runat="server" Text="First Alert Type:"></asp:Label>
                                            </div>
                                            <div class="inline">
                                                <asp:MultiSelectDropDownList ID="ddlFirstAlertType" runat="server" SelectionMode="Multiple"
                                                    CssClass="multiselectdropdown" ClientIDMode="Static" OnClientClick="ValidateFirstAlertType();">
                                                </asp:MultiSelectDropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="space100 paddingBottom5">
                                        <div class="floatLeft space50 inline">
                                            <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                            </div>
                                            <div class="inline">
                                                <asp:CheckBox ID="chkGeneralLog" runat="server" Text="General Log" AutoPostBack="true"
                                                    OnCheckedChanged="chkGeneralLog_CheckedChanged" /><!--onclick="GeneralLogSelected();" -->
                                                <asp:TextBox ID="txtValidateJob" runat="server" CssClass="input" ValidationGroup="FirstAlert"
                                                    Style="display: none" />
                                                <asp:RequiredFieldValidator ID="rfvValidateJob" runat="server" ControlToValidate="txtValidateJob"
                                                    Display="Dynamic" ErrorMessage="Mark the General Log checkbox, or select a Job Record prior to save the First Alert."
                                                    Text="*" ValidationGroup="FirstAlert" />
                                            </div>
                                        </div>
                                        <div class="space49 inlineBlock">
                                            <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                <asp:Label ID="lblAlertHeaderCustomer" runat="server" Text="Company:"></asp:Label>
                                            </div>
                                            <div class="inline">
                                                <uc1:AutoCompleteTextbox ID="actAlertHeaderCustomer" runat="server" ServiceMethod="GetCustomerList"
                                                    GridViewButtonImageUrl="~/Images/money.png" GridViewIdName="ID" DisplayField="Name"
                                                    TextBoxWidth="200px" AutoPostBack="false" WindowTitle="First Alert - Find Company"
                                                    AutoCompleteSource="Customer" ColumnHeaderList="Name,Attn,Company ID" ColumnValueList="Name,Attn,CustomerNumber" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="space100 paddingBottom5">
                                        <div class="floatLeft space50 inline">
                                            <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                <asp:RequiredFieldValidator ID="rfvDivision" ValidationGroup="FirstAlert" ControlToValidate="txtDivision"
                                                    runat="server" ErrorMessage="The Division multiselect field is required. " Text="*"></asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtDivision" runat="server" Style="display: none"></asp:TextBox>
                                                <asp:Label ID="lblAlertHeaderDivision" runat="server" Text="Division(s):"></asp:Label>
                                            </div>
                                            <div class="inline">
                                                <asp:MultiSelectDropDownList ID="ddlDivision" runat="server" SelectionMode="Multiple"
                                                    ValidationGroup="FirstAlert" CssClass="multiselectdropdown" ClientIDMode="Static"
                                                    OnClientClick="ValidateDivisions();">
                                                </asp:MultiSelectDropDownList>
                                            </div>
                                        </div>
                                        <div class="space49 inlineBlock">
                                            <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                <asp:Label ID="lblAlertHeaderEIC" runat="server" Text="On-Site:"></asp:Label>
                                            </div>
                                            <div class="inline">
                                                <uc1:AutoCompleteTextbox ID="actAlertHeaderEIC" runat="server" ServiceMethod="GetCustomerServiceContactList"
                                                    TextBoxWidth="200px" GridViewButtonImageUrl="~/Images/money.png" GridViewIdName="ID"
                                                    DisplayField="FullContactInformation" AutoPostBack="true" AutoCompleteSource="CustomerServiceContact"
                                                    ColumnHeaderList="Name" ColumnValueList="FullContactInformation" TextBoxCssClass="input" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="space100 paddingBottom5">
                                        <div class="floatLeft space50 inline">
                                            <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                <asp:Label ID="lblAlertHeaderDateTime" runat="server" Text="Date and Time:"></asp:Label>
                                            </div>
                                            <div class="inline">
                                                <uc1:DatePicker ID="dpAlertHeaderDate" InvalidValueMessage="Invalid date format"
                                                    DateTimeFormat="Default" ShowOn="Both" runat="server" IsValidEmpty="false" EmptyValueMessage="The Date field is required"
                                                    ValidationGroup="FirstAlert" />
                                                <asp:TextBox ID="txtAlertHeaderTime" runat="server" CssClass="input" Width="40px"></asp:TextBox>
                                                <uc1:MaskedEditExtender ID="mskInitialTime" TargetControlID="txtAlertHeaderTime"
                                                    runat="server" Mask="99:99" MaskType="Time" AcceptAMPM="false" UserTimeFormat="TwentyFourHour"
                                                    AutoComplete="true">
                                                </uc1:MaskedEditExtender>
                                                <uc1:MaskedEditValidator ID="rfvTimeValidator" runat="server" ControlExtender="mskInitialTime"
                                                    ControlToValidate="txtAlertHeaderTime" IsValidEmpty="false" EnableClientScript="true"
                                                    Display="Dynamic" InvalidValueBlurredMessage="*" InvalidValueMessage="The time format is invalid">
                                                </uc1:MaskedEditValidator>
                                            </div>
                                        </div>
                                        <div class="space49 inlineBlock">
                                            <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                <asp:Label ID="lblAlertHeaderCountry" runat="server" Text="Country:"></asp:Label>
                                            </div>
                                            <div class="inline">
                                                <asp:ComboBox ID="cbAlertHeaderCountry" runat="server" CssClass="WindowsStyle" AutoCompleteMode="SuggestAppend"
                                                    DropDownStyle="DropDown" CaseSensitive="false" RenderMode="Inline" Width="200px">
                                                    <asp:ListItem Text="USA" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Canada" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Mexico" Value="3"></asp:ListItem>
                                                </asp:ComboBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="space100 paddingBottom5">
                                        <div class="floatLeft space50 inline">
                                            <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                <asp:Label ID="lblAlertHeaderReportedBy" runat="server" Text="Reported By:"></asp:Label>
                                            </div>
                                            <div class="inline">
                                                <uc1:AutoCompleteTextbox ID="actAlertHeaderReportedBy" runat="server" ServiceMethod="GetEmployeeList"
                                                    TextBoxWidth="200px" GridViewButtonImageUrl="~/Images/money.png" GridViewIdName="ID"
                                                    DisplayField="Name" AutoPostBack="false" AutoCompleteSource="Employee" ColumnHeaderList="Division - Name"
                                                    ColumnValueList="DivisionAndFullName" TextBoxCssClass="input" />
                                            </div>
                                        </div>
                                        <div class="space49 inlineBlock">
                                            <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                <asp:Label ID="lblAlertHeaderState" runat="server" Text="State:"></asp:Label>
                                            </div>
                                            <div class="inline">
                                                <uc1:AutoCompleteTextbox ID="actAlertHeaderState" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                                    TextBoxWidth="200px" GridViewIdName="ID" DisplayField="" AutoPostBack="false"
                                                    ControlsToUpdate="actAlertHeaderCity" WindowTitle="First Alert - Find State"
                                                    AutoCompleteSource="State" ColumnHeaderList="Acronym,Name" ColumnValueList="Acronym,Name"
                                                    ServiceMethod="GetStateList" TextBoxCssClass="input" MinimumPrefixLength="2"
                                                    ContextKey="1" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="space100 paddingBottom5">
                                        <div class="floatLeft space50 inline">
                                            <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                <asp:Label ID="lblAlertHeaderCompletedBy" runat="server" Text="Completed By:"></asp:Label>
                                            </div>
                                            <div class="inline">
                                                <uc1:AutoCompleteTextbox ID="actAlertHeaderCompletedBy" runat="server" ServiceMethod="GetEmployeeList"
                                                    TextBoxWidth="200px" GridViewButtonImageUrl="~/Images/money.png" GridViewIdName="ID"
                                                    DisplayField="Name" AutoPostBack="false" AutoCompleteSource="Employee" ColumnHeaderList="Division - Name"
                                                    ColumnValueList="DivisionAndFullName" TextBoxCssClass="input" />
                                            </div>
                                        </div>
                                        <div class="space49 inlineBlock">
                                            <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                <asp:Label ID="lblAlertHeaderCity" runat="server" Text="City:"></asp:Label>
                                            </div>
                                            <div class="inline">
                                                <uc1:AutoCompleteTextbox ID="actAlertHeaderCity" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                                    TextBoxWidth="200px" GridViewIdName="ID" DisplayField="Name" AutoPostBack="false"
                                                    WindowTitle="First Alert - Find City" AutoCompleteSource="City" ColumnHeaderList="Name"
                                                    ColumnValueList="Name" ServiceMethod="GetCityList" TextBoxCssClass="input" BehaviorId="actAlertHeaderCity"
                                                    ScriptToExecute="FillStateAndCountryFieldsHeader" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="space100 paddingBottom5">
                                        <div>
                                            <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                <asp:Label ID="lblAlertHeaderDetails" runat="server" Text="Details:"></asp:Label>
                                            </div>
                                            <div class="inline">
                                                <asp:CountableTextBox ID="txtAlertHeaderDetails" runat="server" Text="" Width="500px"
                                                    MaxChars="1000" MaxCharsWarning="900" TextMode="MultiLine" CssClass="input" Height="40px"></asp:CountableTextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="space100 paddingBottom5">
                                        <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                        </div>
                                        <div class="inline">
                                            <asp:CheckBox ID="chkAlertHeaderPoliceReport" onclick="BindPoliceReport();"
                                                runat="server" Text="Police Report" />
                                        </div>
                                    </div>
                                    <div id="divPoliceReport" class="space100 paddingBottom5" style="display: none;">
                                        <div class="floatLeft space50 inline">
                                            <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                <asp:Label ID="lblAlertHeaderPoliceAgency" runat="server" Text="Police Agency:"></asp:Label>
                                            </div>
                                            <div class="inline">
                                                <asp:TextBox ID="txtAlertHeaderPoliceAgency" runat="server" Width="200px" CssClass="input" />
                                            </div>
                                        </div>
                                        <div class="space49 inlineBlock">
                                            <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                <asp:Label ID="lblAlertHeaderPoliceReport" runat="server" Text="Report Number:"></asp:Label>
                                            </div>
                                            <div class="inline">
                                                <asp:TextBox ID="txtAlertHeaderPoliceReport" runat="server" Width="200px" CssClass="input" />
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </asp:Panel>
                    <br />
                    <asp:UpdatePanel ID="updVehicles" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnVehicleListAdd" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnVehicleFormAdd" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnVehicleFormClose" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="gvVehiclesList" EventName="RowCommand" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Panel ID="divVehiclesList" runat="server">
                                <div class="Header">
                                    <asp:Label ID="lblVehicleListTitle" runat="server" Text="Vehicles Involved"></asp:Label>
                                </div>
                                <div class="Content">
                                    <asp:UpdatePanel ID="upVehiclesList" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnVehicleFormAdd" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="gvVehiclesList" EventName="RowCommand" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:ScrollableGridView runat="server" ID="gvVehiclesList" AllowSorting="true" AutoGenerateColumns="false"
                                                CssClass="ScrollableGridView" SaveScrollPosition="true" EnableViewState="true"
                                                OnRowCommand="gvVehiclesList_RowCommand" OnRowDataBound="gvVehiclesList_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:HiddenField runat="server" ID="hfVehicleId" Value='<%# Eval("ID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Unit #" />
                                                    <asp:BoundField HeaderText="Make" />
                                                    <asp:BoundField HeaderText="Model" />
                                                    <asp:BoundField HeaderText="Year" />
                                                    <asp:BoundField HeaderText="Damage" />
                                                    <asp:BoundField HeaderText="Hulcher Vehicle" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="VehicleEdit"
                                                                CommandArgument='<%# Container.DataItemIndex %>' CausesValidation="false"></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkDelete" runat="server" Text="Remove" CommandName="VehicleDelete"
                                                                CommandArgument='<%# Container.DataItemIndex %>' CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete this First Alert Vehicle?')"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:ScrollableGridView>
                                            <div class="inlineBlock alignRight space100">
                                                <asp:Button ID="btnVehicleListAdd" runat="server" Text="Add Vehicle" CssClass="btn"
                                                    OnClick="btnVehicleListAdd_Click" CausesValidation="false" />
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlVehiclesForm" runat="server" Visible="false">
                                <br />
                                <div class="Header">
                                    <asp:Label ID="lblVehiclesFormTitle" runat="server" Text="Add Vehicle"></asp:Label>
                                </div>
                                <div class="Content">
                                    <div id="divVehiclesFormHeader">
                                        <div class="space49 floatLeft">
                                            <div class="floatLeft alignRight paddingRight5 paddingTop5 inline">
                                                <asp:Label ID="lblVehiclesFormTypeTitle" runat="server" Text="Hulcher Vehicle:"></asp:Label>
                                            </div>
                                            <div class="inline">
                                                <asp:RadioButtonList ID="rbVehiclesFormType" runat="server" RepeatDirection="Horizontal"
                                                    RepeatLayout="Flow" OnSelectedIndexChanged="rbVehiclesFormType_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <asp:ListItem Selected="True" Text="Yes" Value="true"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="false"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                        <div class="floatRight alignRight space50">
                                            <asp:CheckBox ID="chkFilterVehiclesByJob" CssClass="paddingRight40" runat="server"
                                                Text="Only Resources from Job" />
                                        </div>
                                    </div>
                                    <asp:UpdatePanel ID="updVehicleFormType" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="rbVehiclesFormType" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:Panel ID="pnlHulcherVehicles" runat="server">
                                                <div id="divHulcherVehicleSearch" class="space100">
                                                    <div class="inlineBlock floatRight alignRight">
                                                        <div class="floatLeft paddingTop5 paddingRight5">
                                                            <asp:Label ID="lblHulcherVehicleSearch" runat="server" Text="Filter Listing By: " />
                                                        </div>
                                                        <div class="floatLeft paddingRight5">
                                                            <asp:ComboBox ID="cbHulcherVehicleSearch" runat="server" CssClass="WindowsStyle"
                                                                AutoCompleteMode="SuggestAppend" DropDownStyle="DropDown" CaseSensitive="false"
                                                                RenderMode="Inline">
                                                                <asp:ListItem Text="- Select One -" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Unit#" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Division" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="Make" Value="3"></asp:ListItem>
                                                            </asp:ComboBox>
                                                        </div>
                                                        <div class="floatLeft">
                                                            <asp:TextBox ID="txtHulcherVehicleSearch" runat="server" CssClass="input" />
                                                            <asp:Button ID="btnHulcherVehicleSearch" runat="server" Text="Find" CssClass="btn"
                                                                CausesValidation="false" OnClick="btnHulcherVehicleSearch_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="divHulcherVehicle">
                                                    <asp:UpdatePanel ID="updFilter" runat="server" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnHulcherVehicleSearch" EventName="Click" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:ScrollableGridView runat="server" ID="gvFilteredEquipments" AllowSorting="true"
                                                                AutoGenerateColumns="false" CssClass="ScrollableGridView" SaveScrollPosition="true"
                                                                EnableViewState="true" OnRowDataBound="gvFilteredEquipments_RowDataBound">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="">
                                                                        <ItemTemplate>
                                                                            <asp:HiddenField runat="server" ID="hfVehicleId" Value="" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Unit#">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblUnitNumber" runat="server" Text='<%# Eval("UnitNumber")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField HeaderText="Division" DataField="DivisionName" />
                                                                    <asp:TemplateField HeaderText="Make">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMake" runat="server" Text='<%# Eval("Make")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Damage">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtDamage" runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Est. Cost">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtEstCost" runat="server" onblur="formatCurrency(this.value, this);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                                                            <asp:HiddenField ID="hfEquipmentID" runat="server" Value='<%# Eval("EquipmentID")%>' />
                                                                            <asp:HiddenField ID="hfModel" runat="server" Value='<%# Eval("Model")%>' />
                                                                            <asp:HiddenField ID="hfYear" runat="server" Value='<%# Eval("Year")%>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:ScrollableGridView>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlOtherVehicles" runat="server" Visible="false">
                                                <div class="space100 paddingBottom5">
                                                    <div class="floatLeft space50 inline">
                                                        <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                            <asp:Label ID="lblOtherVehicleMake" runat="server" Text="Make:"></asp:Label>
                                                        </div>
                                                        <div class="inline">
                                                            <asp:TextBox ID="txtOtherVehicleMake" runat="server" Width="200px" CssClass="input"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="space49 inlineBlock">
                                                        <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                            <asp:Label ID="lblOtherVehicleDamage" runat="server" Text="Damage:"></asp:Label>
                                                        </div>
                                                        <div class="inline">
                                                            <asp:TextBox ID="txtOtherVehicleDamage" runat="server" Width="200px" CssClass="input"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="space100 paddingBottom5">
                                                    <div class="floatLeft space50 inline">
                                                        <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                            <asp:Label ID="lblOtherVehicleModel" runat="server" Text="Model:"></asp:Label>
                                                        </div>
                                                        <div class="inline">
                                                            <asp:TextBox ID="txtOtherVehicleModel" runat="server" Width="200px" CssClass="input"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="space49 inlineBlock">
                                                        <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                            <asp:Label ID="lblOtherVehicleEstCost" runat="server" Text="Estimated Cost:"></asp:Label>
                                                        </div>
                                                        <div class="inline">
                                                            <asp:TextBox ID="txtOtherVehicleEstCost" runat="server" Width="200px" CssClass="input"
                                                                onblur="formatCurrency(this.value, this);"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="space100 paddingBottom5">
                                                    <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                        <asp:Label ID="lblOtherVehicleYear" runat="server" Text="Year:"></asp:Label>
                                                    </div>
                                                    <div class="inline">
                                                        <asp:TextBox ID="txtOtherVehicleYear" runat="server" Width="200px" CssClass="input"></asp:TextBox>
                                                        <asp:MaskedEditExtender ID="meeYear" runat="server" Mask="9999" InputDirection="RightToLeft"
                                                            TargetControlID="txtOtherVehicleYear" MaskType="Number">
                                                        </asp:MaskedEditExtender>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <div class="inlineBlock alignRight space100">
                                                <asp:Button ID="btnVehicleFormAdd" runat="server" Text="Save" CssClass="btn" OnClick="btnVehicleFormAdd_Click"
                                                    CausesValidation="false" />
                                                <asp:Button ID="btnVehicleFormClose" runat="server" Text="Cancel" CssClass="btn"
                                                    OnClick="btnVehicleFormClose_Click" CausesValidation="false" />
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="updPeople" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnPersonListAdd" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnPersonFormAdd" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnPersonFormClose" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="gvPeopleList" EventName="RowCommand" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Panel ID="pnlPeopleList" runat="server">
                                <br />
                                <div class="Header">
                                    <asp:Label ID="lblPeopleInvolvedHeader" runat="server" Text="People Involved"></asp:Label>
                                </div>
                                <div class="Content">
                                    <asp:UpdatePanel ID="updPeopleList" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:ScrollableGridView ID="gvPeopleList" runat="server" AllowSorting="true" AutoGenerateColumns="false"
                                                CssClass="ScrollableGridView" SaveScrollPosition="true" EnableViewState="true"
                                                OnRowCommand="gvPeopleList_RowCommand" OnRowDataBound="gvPeopleList_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Last Name" />
                                                    <asp:BoundField HeaderText="First Name" />
                                                    <asp:BoundField HeaderText="Hulcher Employee" />
                                                    <asp:BoundField HeaderText="Location" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="PersonEdit"
                                                                CommandArgument='<%# Container.DataItemIndex %>' CausesValidation="false"></asp:LinkButton>&nbsp;
                                                            <asp:LinkButton ID="lnkRemove" runat="server" Text="Remove" CommandName="RemovePerson"
                                                                CommandArgument='<%# Container.DataItemIndex %>' CausesValidation="false"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:ScrollableGridView>
                                            <div class="inlineBlock alignRight space100">
                                                <asp:Button ID="btnPersonListAdd" runat="server" Text="Add Person" CssClass="btn"
                                                    CausesValidation="false" OnClick="btnPersonListAdd_Click" />
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlPeopleForm" runat="server" Visible="false">
                                <br />
                                <div class="Header">
                                    <asp:Label ID="lblPeopleHeader" runat="server" Text="Add Person Involved"></asp:Label>
                                </div>
                                <div class="Content">
                                    <div class="space100">
                                        <div class="floatLeft space50">
                                            <div class="floatLeft alignRight paddingRight5 paddingTop5 inline">
                                                <asp:Label ID="lblIsHulcherEmployee" runat="server" Text="Hulcher Employee:" />
                                            </div>
                                            <div class="inline">
                                                <asp:RadioButtonList ID="rblIsHulcherEmployee" runat="server" RepeatDirection="Horizontal"
                                                    RepeatLayout="Flow" AutoPostBack="true" OnSelectedIndexChanged="rblIsHulcherEmployee_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="true" Text="Yes" />
                                                    <asp:ListItem Value="false" Text="No" />
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                        <div class="space49 floatRight alignRight">
                                            <asp:CheckBox ID="chkFilterPeopleFromJob" runat="server" CssClass="paddingRight40"
                                                Text="Only Resources from Job" />
                                        </div>
                                    </div>
                                    <asp:Panel ID="pnlHulcherPeople" runat="server">
                                        <asp:UpdatePanel ID="updHulcherPeople" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="inlineBlock floatRight alignRight">
                                                    <div class="floatLeft paddingTop5 paddingRight5">
                                                        <asp:Label ID="lblHulcherPeopleSearch" runat="server" Text="Filter Listing By: " />
                                                    </div>
                                                    <div class="floatLeft paddingRight5">
                                                        <asp:ComboBox ID="cbHulcherPeopleSearch" runat="server" CssClass="WindowsStyle" AutoCompleteMode="SuggestAppend"
                                                            DropDownStyle="DropDown" CaseSensitive="false" RenderMode="Inline">
                                                            <asp:ListItem Text="- Select One -" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Division" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Last Name" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="First Name" Value="3"></asp:ListItem>
                                                            <asp:ListItem Text="Location" Value="4"></asp:ListItem>
                                                        </asp:ComboBox>
                                                    </div>
                                                    <div class="floatLeft">
                                                        <asp:TextBox ID="txtHulcherPeopleSearch" runat="server" CssClass="input" />
                                                        <asp:Button ID="btnHulcherPeopleSearch" runat="server" Text="Find" CssClass="btn"
                                                            CausesValidation="false" OnClick="btnHulcherPeopleSearch_Click" />
                                                    </div>
                                                </div>
                                                <asp:ScrollableGridView ID="gvEmployeeList" runat="server" AllowSorting="true" AutoGenerateColumns="false"
                                                    CssClass="ScrollableGridView" SaveScrollPosition="true" EnableViewState="true"
                                                    OnRowDataBound="gvEmployeeList_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Division" />
                                                        <asp:BoundField HeaderText="Last Name" />
                                                        <asp:BoundField HeaderText="First Name" />
                                                        <asp:BoundField HeaderText="Location" />
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:RadioButton ID="rbSelect" runat="server" GroupName="Test" />
                                                                <asp:MutuallyExclusiveCheckBoxExtender ID="mecSelect" runat="server" TargetControlID="rbSelect"
                                                                    Key="Select">
                                                                </asp:MutuallyExclusiveCheckBoxExtender>
                                                                <asp:HiddenField ID="hfID" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:ScrollableGridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlOtherPeople" runat="server" Visible="false">
                                        <div class="space100 paddingBottom5">
                                            <div class="floatLeft space50 inline">
                                                <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                    <asp:Label ID="lblPersonLastName" runat="server" Text="Last Name:" />
                                                </div>
                                                <div class="inline">
                                                    <asp:TextBox ID="txtPersonLastName" runat="server" MaxLength="100" CssClass="input"
                                                        Width="200px" />
                                                </div>
                                            </div>
                                            <div class="space49 inlineBlock">
                                                <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                    <asp:Label ID="lblPersonFirstName" runat="server" Text="First Name:" />
                                                </div>
                                                <div class="inline">
                                                    <asp:TextBox ID="txtPersonFirstName" runat="server" MaxLength="100" CssClass="input"
                                                        Width="200px" />
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <div id="divPersonDetails">
                                        <div class="space100 paddingBottom5">
                                            <div class="floatLeft space50 inline">
                                                <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                    <asp:Label ID="lblVehicle" runat="server" Text="Vehicle:" />
                                                </div>
                                                <div class="inline">
                                                    <asp:ComboBox ID="cbVehicle" runat="server" CssClass="WindowsStyle" AutoCompleteMode="SuggestAppend"
                                                        DropDownStyle="DropDown" CaseSensitive="false" RenderMode="Inline" Width="200px" />
                                                </div>
                                            </div>
                                            <div class="space49 inlineBlock">
                                                <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                    <asp:Label ID="lblPersonVehiclePosition" runat="server" Text="Vehicle Position:" />
                                                </div>
                                                <div class="inline">
                                                    <asp:ComboBox ID="cbPersonVehiclePosition" runat="server" CssClass="WindowsStyle"
                                                        AutoCompleteMode="SuggestAppend" DropDownStyle="DropDown" CaseSensitive="false"
                                                        RenderMode="Inline" Width="200px">
                                                        <asp:ListItem Selected="True" Value="" Text="Not In Vehicle" />
                                                        <asp:ListItem Value="1" Text="Owner" />
                                                        <asp:ListItem Value="2" Text="Driver" />
                                                        <asp:ListItem Value="3" Text="Passenger" />
                                                    </asp:ComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="space100 paddingBottom5">
                                            <div class="floatLeft space50 inline">
                                                <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                    <asp:Label ID="lblPersonCountry" runat="server" Text="Country:" />
                                                </div>
                                                <div class="inline">
                                                    <asp:ComboBox ID="cbPersonCountry" runat="server" CssClass="WindowsStyle" AutoCompleteMode="SuggestAppend"
                                                        DropDownStyle="DropDown" CaseSensitive="false" RenderMode="Inline" Width="200px">
                                                        <asp:ListItem Selected="True" Text="USA" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Canada" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Mexico" Value="3"></asp:ListItem>
                                                    </asp:ComboBox>
                                                </div>
                                            </div>
                                            <div class="space49 inlineBlock">
                                                <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                    <asp:Label ID="lblPersonInjuryNature" runat="server" Text="Injury Nature:" />
                                                </div>
                                                <div class="inline">
                                                    <asp:TextBox ID="txtPersonInjuryNature" runat="server" MaxLength="100" CssClass="input"
                                                        Width="200px" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="space100 paddingBottom5">
                                            <div class="floatLeft space50 inline">
                                                <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                    <asp:Label ID="lblPersonState" runat="server" Text="State:" />
                                                </div>
                                                <div class="inline">
                                                    <uc1:AutoCompleteTextbox ID="actPersonState" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                                        TextBoxWidth="200px" GridViewIdName="ID" DisplayField="" AutoPostBack="false"
                                                        WindowTitle="First Alert - Find State" AutoCompleteSource="State" ColumnHeaderList="Acronym,Name"
                                                        ColumnValueList="Acronym,Name" ServiceMethod="GetStateList" TextBoxCssClass="input"
                                                        ControlsToUpdate="actPersonCity" ContextKey="1" MinimumPrefixLength="2" RequiredField="false" />
                                                </div>
                                            </div>
                                            <div class="space49 inlineBlock">
                                                <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                    <asp:Label ID="lblPersonInjuryBodyPart" runat="server" Text="Injury Body Part:" />
                                                </div>
                                                <div class="inline">
                                                    <asp:TextBox ID="txtPersonInjuryBodyPart" runat="server" MaxLength="100" CssClass="input"
                                                        Width="200px" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="space100 paddingBottom5">
                                            <div class="floatLeft space50 inline">
                                                <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                    <asp:Label ID="lblPersonCity" runat="server" Text="City:" />
                                                </div>
                                                <div class="inline">
                                                    <uc1:AutoCompleteTextbox ID="actPersonCity" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                                        TextBoxWidth="200px" GridViewIdName="ID" DisplayField="Name" AutoPostBack="false"
                                                        WindowTitle="First Alert - Find City" AutoCompleteSource="City" ColumnHeaderList="Name"
                                                        ColumnValueList="Name" ServiceMethod="GetCityList" TextBoxCssClass="input" BehaviorId="actPersonCity"
                                                        ScriptToExecute="FillStateAndCountryFieldsPerson" RequiredField="false" ControlsToUpdate="actPersonZipCode" />
                                                </div>
                                            </div>
                                            <div class="space49 inlineBlock">
                                                <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                    <asp:Label ID="lblPersonMedicalSeverity" runat="server" Text="Medical Severity:" />
                                                </div>
                                                <div class="inline">
                                                    <asp:ComboBox ID="cbPersonMedicalSeverity" runat="server" CssClass="WindowsStyle"
                                                        AutoCompleteMode="SuggestAppend" DropDownStyle="DropDown" CaseSensitive="false"
                                                        RenderMode="Inline" Width="200px">
                                                        <asp:ListItem Selected="True" Text="- Select One -" Value=""></asp:ListItem>
                                                        <asp:ListItem Text="Medical Treatment" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="First Aid" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Fatality" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="N/A" Value="4"></asp:ListItem>
                                                    </asp:ComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="space100 paddingBottom5">
                                            <div class="floatLeft space50 inline">
                                                <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                    <asp:Label ID="lblPersonZipCode" runat="server" Text="Zip Code:" />
                                                </div>
                                                <div class="inline">
                                                    <uc1:AutoCompleteTextbox ID="actPersonZipCode" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                                        TextBoxWidth="200px" GridViewIdName="ID" DisplayField="" AutoPostBack="false"
                                                        WindowTitle="First Alert - Find Zip Code" AutoCompleteSource="ZipCode" ColumnHeaderList="Name"
                                                        ColumnValueList="Name" ServiceMethod="GetZipCodeList" TextBoxCssClass="input"
                                                        BehaviorId="actPersonZipCode" RequiredField="false" />
                                                </div>
                                            </div>
                                            <div class="space49 inlineBlock">
                                                <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                    <asp:Label ID="lblPersonInsuranceCompany" runat="server" Text="Insurance Company:" />
                                                </div>
                                                <div class="inline">
                                                    <asp:TextBox ID="txtPersonInsuranceCompany" runat="server" MaxLength="50" CssClass="input"
                                                        Width="200px" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="space100 paddingBottom5">
                                            <div class="floatLeft space50 inline">
                                                <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                    <asp:Label ID="lblPersonAddress" runat="server" Text="Details:" />
                                                </div>
                                                <div class="inline">
                                                    <asp:TextBox ID="txtPersonAddress" runat="server" MaxLength="100" CssClass="input"
                                                        Width="200px" />
                                                </div>
                                            </div>
                                            <div class="space49 inlineBlock">
                                                <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                    <asp:Label ID="lblPersonPolicyNumber" runat="server" Text="Policy Number:" />
                                                </div>
                                                <div class="inline">
                                                    <asp:TextBox ID="txtPersonPolicyNumber" runat="server" MaxLength="50" CssClass="input"
                                                        Width="200px" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="space100 paddingBottom5">
                                            <div class="floatLeft space50 inline">
                                                <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                    <asp:Label ID="lblPersonDrugScreenRequired" runat="server" Text="Drug Screen:" />
                                                </div>
                                                <div class="inline">
                                                    <asp:CheckBox ID="chkPersonDrugScreen" runat="server" />
                                                </div>
                                            </div>
                                            <div class="space49 inlineBlock">
                                                <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                    <asp:Label ID="lblPersonDetails" runat="server" Text="Details:" />
                                                </div>
                                                <div class="inline">
                                                    <asp:CountableTextBox ID="txtPersonDetails" runat="server" MaxLength="1000" CssClass="input"
                                                        Height="40px" Width="200px" TextMode="MultiLine" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="divPersonDoctor">
                                        <hr width="90%" />
                                        <div class="space100 paddingBottom5">
                                            <div class="floatLeft space50 inline">
                                                <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                    <asp:Label ID="lblPersonDoctorsName" runat="server" Text="Doctor's Name:" />
                                                </div>
                                                <div class="inline">
                                                    <asp:TextBox ID="txtPersonDoctorsName" runat="server" MaxLength="100" CssClass="input"
                                                        Width="200px" />
                                                </div>
                                            </div>
                                            <div class="space49 inlineBlock">
                                                <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                    <asp:Label ID="lblPersonDoctorsCity" runat="server" Text="Doctor's City:" />
                                                </div>
                                                <div class="inline">
                                                    <uc1:AutoCompleteTextbox ID="actPersonDoctorsCity" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                                        TextBoxWidth="200px" GridViewIdName="ID" DisplayField="Name" AutoPostBack="false"
                                                        WindowTitle="First Alert - Find City" AutoCompleteSource="City" ColumnHeaderList="Name"
                                                        ColumnValueList="Name" ServiceMethod="GetCityList" TextBoxCssClass="input" BehaviorId="actPersonDoctorsCity"
                                                        ScriptToExecute="FillStateAndCountryFieldsPersonDoctor" ControlsToUpdate="actPersonDoctorsZipCode"
                                                        RequiredField="false" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="space100 paddingBottom5">
                                            <div class="floatLeft space50 inline">
                                                <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                    <asp:Label ID="lblPersonDoctorsCountry" runat="server" Text="Doctor's Country:" />
                                                </div>
                                                <div class="inline">
                                                    <asp:ComboBox ID="cbPersonDoctorsCountry" runat="server" CssClass="WindowsStyle"
                                                        AutoCompleteMode="SuggestAppend" DropDownStyle="DropDown" CaseSensitive="false"
                                                        RenderMode="Inline" Width="200px">
                                                        <asp:ListItem Selected="True" Text="USA" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Canada" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Mexico" Value="3"></asp:ListItem>
                                                    </asp:ComboBox>
                                                </div>
                                            </div>
                                            <div class="space49 inlineBlock">
                                                <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                    <asp:Label ID="lblPersonDoctorsPhoneNumber" runat="server" Text="Doctor's Number:" />
                                                </div>
                                                <div class="inline">
                                                    <asp:TextBox ID="txtPersonDoctorsPhoneNumber" runat="server" MaxLength="20" CssClass="input"
                                                        Width="200px" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="space100 paddingBottom5">
                                            <div class="floatLeft space50 inline">
                                                <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                    <asp:Label ID="lblPersonDoctorsState" runat="server" Text="Doctor's State:" />
                                                </div>
                                                <div class="inline">
                                                    <uc1:AutoCompleteTextbox ID="actPersonDoctorsState" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                                        TextBoxWidth="200px" GridViewIdName="ID" DisplayField="" AutoPostBack="false"
                                                        WindowTitle="First Alert - Find State" AutoCompleteSource="State" ColumnHeaderList="Acronym,Name"
                                                        ColumnValueList="Acronym,Name" ServiceMethod="GetStateList" TextBoxCssClass="input"
                                                        MinimumPrefixLength="2" ControlsToUpdate="actPersonDoctorsCity" ContextKey="1"
                                                        RequiredField="false" />
                                                </div>
                                            </div>
                                            <div>
                                                <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                    <asp:Label ID="lblPersonDoctorsZipCode" runat="server" Text="Doctor's Zip Code:" />
                                                </div>
                                                <div class="inline">
                                                    <uc1:AutoCompleteTextbox ID="actPersonDoctorsZipCode" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                                        TextBoxWidth="200px" GridViewIdName="ID" DisplayField="" AutoPostBack="false"
                                                        RequiredField="false" WindowTitle="First Alert - Find Zip Code" AutoCompleteSource="ZipCode"
                                                        ColumnHeaderList="Name" ColumnValueList="Name" ServiceMethod="GetZipCodeList"
                                                        TextBoxCssClass="input" BehaviorId="actPersonDoctorsZipCode" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="divPersonHospital">
                                        <hr width="90%" />
                                        <div class="space100 paddingBottom5">
                                            <div class="floatLeft space50 inline">
                                                <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                    <asp:Label ID="lblPersonHospitalName" runat="server" Text="Hospital Name:" />
                                                </div>
                                                <div class="inline">
                                                    <asp:TextBox ID="txtPersonHospitalName" runat="server" MaxLength="100" CssClass="input"
                                                        Width="200px" />
                                                </div>
                                            </div>
                                            <div class="space49 inlineBlock">
                                                <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                    <asp:Label ID="lblPersonHospitalCity" runat="server" Text="Hospital City:" />
                                                </div>
                                                <div class="inline">
                                                    <uc1:AutoCompleteTextbox ID="actPersonHospitalCity" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                                        TextBoxWidth="200px" GridViewIdName="ID" DisplayField="Name" AutoPostBack="false"
                                                        WindowTitle="First Alert - Find City" AutoCompleteSource="City" ColumnHeaderList="Name"
                                                        ColumnValueList="Name" ServiceMethod="GetCityList" TextBoxCssClass="input" ScriptToExecute="FillStateAndCountryFieldsPersonHospital"
                                                        ControlsToUpdate="actPersonHospitalZipCode" RequiredField="false" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="space100 paddingBottom5">
                                            <div class="floatLeft space50 inline">
                                                <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                    <asp:Label ID="lblPersonHospitalCountry" runat="server" Text="Hospital Country:" />
                                                </div>
                                                <div class="inline">
                                                    <asp:ComboBox ID="cbPersonHospitalCountry" runat="server" CssClass="WindowsStyle"
                                                        AutoCompleteMode="SuggestAppend" DropDownStyle="DropDown" CaseSensitive="false"
                                                        RenderMode="Inline" Width="200px">
                                                        <asp:ListItem Text="USA" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Canada" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Mexico" Value="3"></asp:ListItem>
                                                    </asp:ComboBox>
                                                </div>
                                            </div>
                                            <div class="space49 inlineBlock">
                                                <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                    <asp:Label ID="lblPersonHospitalPhoneNumber" runat="server" Text="Hospital Number:" />
                                                </div>
                                                <div class="inline">
                                                    <asp:TextBox ID="txtPersonHospitalPhoneNumber" runat="server" MaxLength="20" CssClass="input"
                                                        Width="200px" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="space100 paddingBottom5">
                                            <div class="floatLeft space50 inline">
                                                <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                    <asp:Label ID="lblPersonHospitalState" runat="server" Text="Hospital State:" />
                                                </div>
                                                <div class="inline">
                                                    <uc1:AutoCompleteTextbox ID="actPersonHospitalState" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                                        TextBoxWidth="200px" GridViewIdName="ID" DisplayField="" AutoPostBack="false"
                                                        WindowTitle="First Alert - Find State" AutoCompleteSource="State" ColumnHeaderList="Acronym,Name"
                                                        ColumnValueList="Acronym,Name" ServiceMethod="GetStateList" TextBoxCssClass="input"
                                                        MinimumPrefixLength="2" ControlsToUpdate="actPersonHospitalCity" ContextKey="1"
                                                        RequiredField="false" />
                                                </div>
                                            </div>
                                            <div class="space49 inlineBlock">
                                                <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                    <asp:Label ID="lblPersonHospitalZipCode" runat="server" Text="Hospital Zip Code:" />
                                                </div>
                                                <div class="inline">
                                                    <uc1:AutoCompleteTextbox ID="actPersonHospitalZipCode" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                                        TextBoxWidth="200px" GridViewIdName="ID" DisplayField="" AutoPostBack="false"
                                                        RequiredField="false" WindowTitle="First Alert - Find Zip Code" AutoCompleteSource="ZipCode"
                                                        ColumnHeaderList="Name" ColumnValueList="Name" ServiceMethod="GetZipCodeList"
                                                        TextBoxCssClass="input" BehaviorId="actPersonHospitalZipCode" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="divPersonDriversLicense" style="display: none;">
                                        <hr width="90%" />
                                        <div class="space100 paddingBottom5">
                                            <div class="floatLeft space50 inline">
                                                <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                    <asp:Label ID="lblPersonDriversLicenseNumber" runat="server" Text="Driver's License Number:" />
                                                </div>
                                                <div class="inline">
                                                    <asp:TextBox ID="txtPersonDriversLicenseNumber" runat="server" MaxLength="15" CssClass="input"
                                                        Width="200px" />
                                                </div>
                                            </div>
                                            <div class="space49 inlineBlock">
                                                <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                </div>
                                                <div class="inline">
                                                    <asp:CheckBox ID="chkPersonSameAddressAsAbove" runat="server" Text="Same Address as Above:" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="space100 paddingBottom5">
                                            <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                <asp:Label ID="lblPersonDriversLicenseAddress" runat="server" Text="Driver's License Address:" />
                                            </div>
                                            <div class="inline">
                                                <asp:TextBox ID="txtPersonDriversLicenseAddress" runat="server" MaxLength="100" CssClass="input"
                                                    Width="200px" />
                                            </div>
                                        </div>
                                        <div class="space100 paddingBottom5">
                                            <div class="floatLeft space50 inline">
                                                <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                    <asp:Label ID="lblPersonDriversLicenseCountry" runat="server" Text="Driver's License Country:" />
                                                </div>
                                                <div class="inline">
                                                    <asp:ComboBox ID="cbPersonDriversLicenseCountry" runat="server" CssClass="WindowsStyle"
                                                        AutoCompleteMode="SuggestAppend" DropDownStyle="DropDown" CaseSensitive="false"
                                                        RenderMode="Inline" Width="200px">
                                                        <asp:ListItem Selected="True" Text="USA" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Canada" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Mexico" Value="3"></asp:ListItem>
                                                    </asp:ComboBox>
                                                </div>
                                            </div>
                                            <div class="space49 inlineBlock">
                                                <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                    <asp:Label ID="lblPersonDriversLicenseCity" runat="server" Text="Driver's License City:" />
                                                </div>
                                                <div class="inline">
                                                    <uc1:AutoCompleteTextbox ID="actPersonDriversLicenseCity" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                                        TextBoxWidth="200px" GridViewIdName="ID" DisplayField="Name" AutoPostBack="false"
                                                        RequiredField="false" WindowTitle="First Alert - Find City" AutoCompleteSource="City"
                                                        ColumnHeaderList="Name" ColumnValueList="Name" ServiceMethod="GetCityList" TextBoxCssClass="input"
                                                        BehaviorId="actPersonDriversLicenseCity" ScriptToExecute="FillStateAndCountryFieldsPersonDriversLicense"
                                                        ControlsToUpdate="actPersonDriversLicenseZipCode" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="space100 paddingBottom5">
                                            <div class="floatLeft space50 inline">
                                                <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                    <asp:Label ID="lblPersonDriversLicenseState" runat="server" Text="Driver's License State:" />
                                                </div>
                                                <div class="inline">
                                                    <uc1:AutoCompleteTextbox ID="actPersonDriversLicenseState" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                                        TextBoxWidth="200px" GridViewIdName="ID" DisplayField="" AutoPostBack="false"
                                                        RequiredField="false" WindowTitle="First Alert - Find State" AutoCompleteSource="State"
                                                        ColumnHeaderList="Acronym,Name" ColumnValueList="Acronym,Name" ServiceMethod="GetStateList"
                                                        TextBoxCssClass="input" MinimumPrefixLength="2" ControlsToUpdate="actPersonDriversLicenseCity"
                                                        ContextKey="1" />
                                                </div>
                                            </div>
                                            <div class="space49 inlineBlock">
                                                <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                    <asp:Label ID="lblPersonDriversLicenseZipCode" runat="server" Text="Driver's License Zip Code:" />
                                                </div>
                                                <div class="inline">
                                                    <uc1:AutoCompleteTextbox ID="actPersonDriversLicenseZipCode" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                                        TextBoxWidth="200px" GridViewIdName="ID" DisplayField="" AutoPostBack="false"
                                                        RequiredField="false" WindowTitle="First Alert - Find Zip Code" AutoCompleteSource="ZipCode"
                                                        ColumnHeaderList="Name" ColumnValueList="Name" ServiceMethod="GetZipCodeList"
                                                        TextBoxCssClass="input" BehaviorId="actPersonDriversLicenseZipCode" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="inlineBlock alignRight space100">
                                        <asp:Button ID="btnPersonFormAdd" runat="server" Text="Save" CssClass="btn" OnClick="btnPersonFormAdd_Click"
                                            CausesValidation="false" />
                                        <asp:Button ID="btnPersonFormClose" runat="server" Text="Cancel" CssClass="btn" CausesValidation="false"
                                            OnClick="btnPersonFormClose_Click" />
                                    </div>
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:Panel ID="pnlContactPersonal" runat="server" Visible="false">
                        <br />
                        <asp:UpdatePanel ID="updContactPersonal" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>
                                <div class="Header">
                                    <asp:Label ID="lblContactPersonalTitle" runat="server" Text="Contact Personal" />
                                </div>
                                <div class="Content">
                                    <asp:ScrollableGridView ID="gvContactPersonal" runat="server" OnRowDataBound="gvContactPersonal_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblName" runat="server" />
                                                    <asp:HiddenField ID="hidID" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Email Advised" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkEmailAdvised" runat="server" Enabled="false" />
                                                    <asp:Label ID="lblEmailAdvisedDate" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="VMX Advised" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkVMXAdvised" runat="server" />
                                                    <asp:Label ID="lblVMXAdvisedDate" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="In Person Advised" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkInPersonAdvised" runat="server" />
                                                    <asp:Label ID="lblInPersonAdvisedDate" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:ScrollableGridView>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                    <br />
                    <div id="divButtons" class="inlineBlock alignRight space100">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn" OnClick="btnSave_Click"
                            ValidationGroup="FirstAlert" />
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn" CausesValidation="false"
                            Enabled="false" OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure you want to delete this First Alert?')" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn" CausesValidation="false"
                            OnClientClick="return confirm('All unsaved data will be lost, are you sure you want to cancel?')"
                            OnClick="btnCancel_Click" />
                    </div>
                </asp:Panel>
                <asp:Button ID="btnFakeSort" runat="server" Style="display: none;" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript" src="/Scripts/jquery.multiselect.min.js"></script>
    <script type="text/javascript" language="javascript" defer="defer">

        //Instance of ScripManager
        var scriptManager = Sys.WebForms.PageRequestManager.getInstance();

        scriptManager.add_endRequest(function () {
            initializePage();

            var comboControl = $find('<%= cbFilterSearchAlert.ClientID %>');
            if (comboControl)
                ValidateDateAndTime(comboControl);

            BindPoliceReport();
            BindClickInRadioButtons();
            BindCountryFilters();
        });

        Sys.Application.add_load(initializePage);

        $(document).ready(function () {
            BindPoliceReport();
            BindClickInRadioButtons();
        });

        function initializePage() {
            var comboControl = $find('<%= cbFilterSearchAlert.ClientID %>');
            if (comboControl) {
                comboControl.add_propertyChanged(function (sender, e) {
                    if (e.get_propertyName() == 'selectedIndex') {
                        ValidateDateAndTime(comboControl);
                    }
                });
            }
            BindCountryFilters();
        }

        function ValidateDateAndTime(comboControl) {
            if (comboControl != null) {
                var newValue = comboControl.get_textBoxControl().value;

                var maskedEditExtender = $find('<%= meeFilterValueDate.BehaviorID %>');

                var txtFilterDateAndTime = document.getElementById('<%= txtFilterDateAndTime.ClientID %>');
                var txtFilterSearchAlert = document.getElementById('<%= txtFilterSearchAlert.ClientID %>');

                if (newValue == "Date") {
                    txtFilterSearchAlert.style.display = "none";
                    txtFilterDateAndTime.style.display = "block";
                    txtFilterDateAndTime.value = "";
                    maskedEditExtender.enabled = true;
                    maskedEditExtender.set_Mask("99/99/9999");
                    maskedEditExtender._convertMask();
                    maskedEditExtender.set_MaskType(Sys.Extended.UI.MaskedEditType.Date);
                }
                else if (newValue == "Time") {
                    txtFilterSearchAlert.style.display = "none";
                    txtFilterDateAndTime.style.display = "block";
                    txtFilterDateAndTime.value = "";
                    maskedEditExtender.enabled = true;
                    maskedEditExtender.set_Mask("99:99");
                    maskedEditExtender._convertMask();
                    maskedEditExtender.set_MaskType(Sys.Extended.UI.MaskedEditType.Time);
                }
                else {
                    txtFilterSearchAlert.style.display = "block";
                    txtFilterDateAndTime.style.display = "none";
                    maskedEditExtender.set_Mask("");
                    maskedEditExtender._convertMask();
                    maskedEditExtender.set_MaskType(Sys.Extended.UI.MaskedEditType.None);
                }
            }
        }

        //        function GeneralLogSelected() {
        //            if ($('#<%=chkGeneralLog.ClientID %>').attr('checked')) {
        //                $('#<%=actAlertHeaderJobNumber.TextControlClientID %>').attr('disabled', true);
        //                $('#<%=actAlertHeaderJobNumber.ImageButtonClientID %>').attr('disabled', true);
        //                $('#<%=actAlertHeaderCustomer.TextControlClientID %>').attr('disabled', true);
        //                $('#<%=actAlertHeaderCustomer.ImageButtonClientID%>').attr('disabled', true);
        //                $('#<%=txtValidateJob.ClientID%>').attr('value', '1');
        //            }
        //            else {
        //                $('#<%=actAlertHeaderJobNumber.TextControlClientID %>').attr('disabled', false);
        //                $('#<%=actAlertHeaderJobNumber.ImageButtonClientID %>').attr('disabled', false);
        //                $('#<%=actAlertHeaderCustomer.TextControlClientID %>').attr('disabled', false);
        //                $('#<%=actAlertHeaderCustomer.ImageButtonClientID%>').attr('disabled', false);
        //                $('#<%=txtValidateJob.ClientID%>').attr('value', '');
        //            }
        //        }

        function BindPoliceReport() {
            if ($('#<%= chkAlertHeaderPoliceReport.ClientID %>').length > 0) {
                if ($('#<%= chkAlertHeaderPoliceReport.ClientID %>').is(':checked')) {
                    $('#divPoliceReport').css('display', '');
                }
                else {
                    $('#divPoliceReport').css('display', 'none');
                    $('#<%= txtAlertHeaderPoliceAgency.ClientID %>').val('');
                    $('#<%= txtAlertHeaderPoliceReport.ClientID %>').val('');
                    
                }
            }
        }

        function BindClickInRadioButtons() {
            //            if ($('#<%=rbVehiclesFormType.ClientID %>').length > 0) {
            //                $('#<%=rbVehiclesFormType.ClientID %> input').click(function () {
            //                    if (this.value == 'true') {
            //                        divHulcherVehicles.style.display = '';
            //                        divOtherVehicles.style.display = 'none';
            //                    }
            //                    else {
            //                        divHulcherVehicles.style.display = 'none';
            //                        divOtherVehicles.style.display = '';
            //                    }
            //                });
            //            }

            //            if ($('#<%=rblIsHulcherEmployee.ClientID %>').length > 0) {
            //                $('#<%=rblIsHulcherEmployee.ClientID %> input').click(function () {
            //                    if (this.value == 'true') {
            //                        pnlHulcherPeople.style.display = '';
            //                        pnlOtherPeople.style.display = 'none';
            //                    }
            //                    else {
            //                        pnlHulcherPeople.style.display = 'none';
            //                        pnlOtherPeople.style.display = '';
            //                    }
            //                });
            //            }

            if ($('#<%=cbPersonVehiclePosition.ClientID %>').length > 0) {
                $find('<%= cbPersonVehiclePosition.ClientID %>').add_propertyChanged(function (sender, e) {
                    if (e.get_propertyName() == 'selectedIndex') {
                        var newValue = sender.get_textBoxControl().value;

                        if (newValue == 'Driver')
                            divPersonDriversLicense.style.display = '';
                        else
                            divPersonDriversLicense.style.display = 'none';
                    }
                });
            }
        }

        function BindCountryFilters() {
            if ($('#<%=cbAlertHeaderCountry.ClientID %>').length > 0) {
                $find('<%=cbAlertHeaderCountry.ClientID %>').add_propertyChanged(function (sender, e) {
                    if (e.get_propertyName() == 'selectedIndex') {
                        ChangeStateContextKey(
                            $find('actAlertHeaderState'),
                            $find('actAlertHeaderCity'),
                            parseInt($get('<%= cbAlertHeaderCountry.ClientID%>_HiddenField').value) + 1);
                    }
                });
            }
            if ($('#<%= cbPersonCountry.ClientID %>').length > 0) {
                $find('<%= cbPersonCountry.ClientID %>').add_propertyChanged(function (sender, e) {
                    if (e.get_propertyName() == 'selectedIndex') {
                        ChangeStateContextKey(
                            $find('actPersonState'),
                            $find('actPersonCity'),
                            parseInt($get('<%=  cbPersonCountry.ClientID%>_HiddenField').value) + 1);
                    }
                });
            }
            if ($('#<%= cbPersonDoctorsCountry.ClientID %>').length > 0) {
                $find('<%= cbPersonDoctorsCountry.ClientID %>').add_propertyChanged(function (sender, e) {
                    if (e.get_propertyName() == 'selectedIndex') {
                        ChangeStateContextKey(
                            $find('actPersonDoctorsState'),
                            $find('actPersonDoctorsCity'),
                            parseInt($get('<%=  cbPersonDoctorsCountry.ClientID%>_HiddenField').value) + 1);
                    }
                });
            }
            if ($('#<%= cbPersonHospitalCountry.ClientID %>').length > 0) {
                $find('<%= cbPersonHospitalCountry.ClientID %>').add_propertyChanged(function (sender, e) {
                    if (e.get_propertyName() == 'selectedIndex') {
                        ChangeStateContextKey(
                            $find('actPersonHospitalState'),
                            $find('actPersonHospitalCity'),
                            parseInt($get('<%=  cbPersonHospitalCountry.ClientID%>_HiddenField').value) + 1);
                    }
                });
            }
            if ($('#<%= cbPersonDriversLicenseCountry.ClientID %>').length > 0) {
                $find('<%= cbPersonDriversLicenseCountry.ClientID %>').add_propertyChanged(function (sender, e) {
                    if (e.get_propertyName() == 'selectedIndex') {
                        ChangeStateContextKey(
                            $find('actPersonDriversLicenseState'),
                            $find('actPersonDriversLicenseCity'),
                            parseInt($get('<%=  cbPersonDriversLicenseCountry.ClientID%>_HiddenField').value) + 1);
                    }
                });
            }
        }

        function ChangeStateContextKey(controlState, controlCity, newValue) {
            if (controlState.get_contextKey() != '0' && controlState.get_contextKey() != newValue) {
                controlState.raiseItemSelected(new Sys.Extended.UI.AutoCompleteItemEventArgs(null, '', '0'));
                controlCity.raiseItemSelected(new Sys.Extended.UI.AutoCompleteItemEventArgs(null, '', '0'));
            }
            controlState.set_contextKey(newValue);
        }

        function FillStateAndCountryFieldsHeader(cityId) {
            if (cityId != 0) {
                tempuri.org.IJSONService.GetStateAndCountryByCity(cityId, FillStateAndCountryFieldsHeaderCompleted);
            }
        }

        function FillStateAndCountryFieldsPerson(cityId) {
            if (cityId != 0) {
                tempuri.org.IJSONService.GetStateAndCountryByCity(cityId, FillStateAndCountryFieldsPersonCompleted);
            }
        }

        function FillStateAndCountryFieldsPersonDoctor(cityId) {
            if (cityId != 0) {
                tempuri.org.IJSONService.GetStateAndCountryByCity(cityId, FillStateAndCountryFieldsPersonDoctorCompleted);
            }
        }

        function FillStateAndCountryFieldsPersonHospital(cityId) {
            if (cityId != 0) {
                tempuri.org.IJSONService.GetStateAndCountryByCity(cityId, FillStateAndCountryFieldsPersonHospitalCompleted);
            }
        }


        function FillStateAndCountryFieldsPersonDriversLicense(cityId) {
            if (cityId != 0) {
                tempuri.org.IJSONService.GetStateAndCountryByCity(cityId, FillStateAndCountryFieldsPersonDriversLicenseCompleted);
            }
        }

        function FillStateAndCountryFieldsHeaderCompleted(WebServiceResult) {
            FillStateAndCountry($find('actAlertHeaderState'), $find('<%= cbAlertHeaderCountry.ClientID %>'), WebServiceResult);
        }

        function FillStateAndCountryFieldsPersonCompleted(WebServiceResult) {
            FillStateAndCountry($find('actPersonState'), $find('<%= cbPersonCountry.ClientID %>'), WebServiceResult);
        }

        function FillStateAndCountryFieldsPersonDoctorCompleted(WebServiceResult) {
            FillStateAndCountry($find('actPersonDoctorsState'), $find('<%= cbPersonDoctorsCountry.ClientID %>'), WebServiceResult);
        }

        function FillStateAndCountryFieldsPersonHospitalCompleted(WebServiceResult) {
            FillStateAndCountry($find('actPersonHospitalState'), $find('<%= cbPersonHospitalCountry.ClientID %>'), WebServiceResult);
        }

        function FillStateAndCountryFieldsPersonDriversLicenseCompleted(WebServiceResult) {
            FillStateAndCountry($find('actPersonDriversLicenseState'), $find('<%= cbPersonDriversLicenseCountry.ClientID %>'), WebServiceResult);
        }

        function FillStateAndCountry(stateControl, countryControl, WebServiceResult) {
            stateControl.raiseItemSelected(new Sys.Extended.UI.AutoCompleteItemEventArgs(null, WebServiceResult.StateName, WebServiceResult.StateId));
            countryControl.set_selectedIndex(WebServiceResult.CountryId - 1);
            countryControl._textBoxControl.value = countryControl._optionListItems[WebServiceResult.CountryId - 1].text;

            document.getElementById('<%= btnPersonFormAdd.ClientID %>').focus();
        }

        function SetVehiclesValidation(obj) {
            if (obj.checked)
                obj.className = "checkedVehicle";
            else
                obj.className = "";

            var hfVehiclesAdded = document.getElementById('<%= hfVehiclesAdded.ClientID %>');

            if ($(".checkedVehicle").length > 0)
                hfVehiclesAdded.value = "hasVehicles";
            else
                hfVehiclesAdded.value = "";
        }


        function ValidateFirstAlertType() {
            var textbox = document.getElementById('<%= txtFirstAlertType.ClientID %>');

            var values = $('#<%= ddlFirstAlertType.ClientID %>').val();

            if (values != null) {
                textbox.value = values.join();
            }
            else
                textbox.value = '';

        }

        function ValidateDivisions() {
            var textbox = document.getElementById('<%= txtDivision.ClientID %>');

            var values = $('#<%= ddlDivision.ClientID %>').val();

            if (values != null) {
                textbox.value = values.join();
            }
            else
                textbox.value = '';
        }
     
    </script>
</asp:Content>
