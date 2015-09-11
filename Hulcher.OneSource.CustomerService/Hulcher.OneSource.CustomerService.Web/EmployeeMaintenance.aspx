<%@ Page Title="Employee Profile Maintenance" Language="C#" MasterPageFile="~/ContentPage.master"
    AutoEventWireup="true" CodeBehind="EmployeeMaintenance.aspx.cs" Inherits="Hulcher.OneSource.CustomerService.Web.EmployeeMaintenance" %>

<%@ MasterType TypeName="Hulcher.OneSource.CustomerService.Web.ContentPage" %>
<%@ Register Src="~/UserControls/CallCriteria/CallCriteriaInfo.ascx" TagName="CallCriteria"
    TagPrefix="uc" %>
<%@ Register Src="~/UserControls/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<%@ Register TagName="CollapseHolder" TagPrefix="uc" Src="~/UserControls/CollapseHolder.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/AutoCompleteTextbox.ascx" TagName="AutoCompleteTextbox"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Styles/Forms.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div style="padding-bottom: 10px; height: 20px;">
        <asp:Label ID="lblPageTitle" runat="server" Font-Size="Large" Font-Bold="true" Text="Employee Profile Maintenance" />
    </div> 
    <asp:ValidationSummary ID="vsEmployee" runat="server" CssClass="errorbox" HeaderText="Please correct the following information" />
    <asp:UpdatePanel ID="upMain" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:Panel ID="pnlFullAccess" runat="server" Visible="true">
                <div class="Page">
                    <div class="Header">
                        <asp:Label ID="lblTitle" runat="server" Text="Employee Details" />
                    </div>
                    <div class="Content">
                        <asp:Label ID="lblEmployee" runat="server" Text="Select Employee: "></asp:Label>
                        <uc1:AutoCompleteTextbox ID="actEmployee" runat="server" ServiceMethod="GetEmployeeList"
                            TextBoxWidth="250px" GridViewButtonImageUrl="~/Images/money.png" GridViewIdName="ID"
                            DisplayField="Name" AutoPostBack="true" RequiredField="true" WindowTitle="Employee Maintenance - Find Employee"
                            AutoCompleteSource="Employee" ColumnHeaderList="Division - Name" ColumnValueList="DivisionAndFullName"
                            TextBoxCssClass="input" OnTextChanged="EmployeeContact_OnTextChanged" ScriptToExecute="enableAll" />
                    </div>
                    <br />
                    <div id="divControl" runat="server" class="usercontrolDiv">
                        <uc:CollapseHolder ID="chEmployeeInfo" runat="server" GridViewCssClass="ScrollableGridView">
                            <Header>
                                <asp:Label ID="lblEmployeeInfoTitle" runat="server" Text="Employee Information"></asp:Label>
                            </Header>
                            <Content>
                                <div class="floatLeft space50 inline paddingBottom5">
                                    <div class="floatLeft space100 inline paddingBottom5">
                                        <div class="floatLeft alignRight width140 paddingRight5 inline">
                                            <asp:Label ID="lblEmployeeID" runat="server" Text="Employee ID #:"></asp:Label>
                                        </div>
                                        <div class="inlineBlock">
                                            <asp:Label ID="lblEmployeeIDDesc" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                    <div class="floatLeft space100 inline paddingBottom5">
                                        <div class="floatLeft alignRight width140 paddingRight5 inline">
                                            <asp:Label ID="lblEmployeeName" runat="server" Text="Employee Name:"></asp:Label>
                                        </div>
                                        <div class="inlineBlock">
                                            <asp:Label ID="lblEmployeeNameDesc" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                    <div class="floatLeft space100 inline paddingBottom5">
                                        <div class="floatLeft alignRight width140 paddingRight5 inline">
                                            <asp:Label ID="lblEmployeeHireDate" runat="server" Text="Hire Date:"></asp:Label>
                                        </div>
                                        <div class="inlineBlock">
                                            <asp:Label ID="lblEmployeeHireDateDesc" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                    <div class="floatLeft space100 inline paddingBottom5">
                                        <div class="floatLeft alignRight width140 paddingRight5 inline">
                                            <asp:Label ID="lblEmployeePosition" runat="server" Text="Position:"></asp:Label>
                                        </div>
                                        <div class="inlineBlock">
                                            <asp:Label ID="lblEmployeePositionDesc" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                    <div class="floatLeft space100 inline paddingBottom5">
                                        <div class="floatLeft alignRight width140 paddingRight5 inline">
                                            <asp:Label ID="lblEmployeeDivision" runat="server" Text="Division:"></asp:Label>
                                        </div>
                                        <div class="inlineBlock">
                                            <asp:Label ID="lblEmployeeDivisionDesc" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                    <div class="floatLeft space100 inline paddingBottom5">
                                        <div class="floatLeft alignRight width140 paddingRight5 inline">
                                            <asp:Label ID="lblEmployeePassport" runat="server" Text="Passport:"></asp:Label>
                                        </div>
                                        <div class="inlineBlock">
                                            <asp:Label ID="lblEmployeePassportDesc" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="space49 inlineBlock">
                                    <asp:Label ID="lblEmployeeICEContact" runat="server" Text="ICE Contacts:"></asp:Label>
                                    <div class="inlineBlock">
                                        <asp:ScrollableGridView runat="server" ID="gvICEContacts" AllowSorting="true" AutoGenerateColumns="false"
                                            CssClass="ScrollableGridView" SaveScrollPosition="true" MaxHeight="200px" EnableViewState="true"
                                            MinWidth="90%" Width="90%">
                                            <Columns>
                                                <asp:BoundField HeaderText="Contact Name" DataField="FullName" />
                                                <asp:BoundField HeaderText="Home Phone" DataField="HomePhoneExtension" />
                                                <asp:BoundField HeaderText="Mobile Phone" DataField="MobilePhoneExtension" />
                                            </Columns>
                                        </asp:ScrollableGridView>
                                    </div>
                                </div>
                                <hr />
                                <div class="space100 paddingBottom5">
                                    <div class="floatLeft space50 inline">
                                        <div class="floatLeft alignRight width140 paddingRight5 inline">
                                            <asp:Label ID="lblAddress" runat="server" Text="Address:"></asp:Label>
                                        </div>
                                        <div class="inlineBlock">
                                            <asp:TextBox ID="txtAddress" runat="server" MaxLength="255" CssClass="input" Width="200px" />
                                        </div>
                                    </div>
                                    <div class="space49 inlineBlock">
                                        <div class="floatLeft alignRight width140 paddingRight5 inline">
                                            <asp:Label ID="lblEmployeeDayPhone" runat="server" Text="Day Phone:"></asp:Label>
                                        </div>
                                        <div class="inlineBlock">
                                            <asp:TextBox ID="txtEmployeeDayCode" runat="server" MaxLength="20" CssClass="input"
                                                Width="25px" />
                                            <asp:TextBox ID="txtEmployeeDayPhone" runat="server" MaxLength="20" CssClass="input"
                                                Width="100px" />
                                            <asp:MaskedEditExtender ID="mskEmployeeDayCode" runat="server" MaskType="Number"
                                                Mask="999" TargetControlID="txtEmployeeDayCode">
                                            </asp:MaskedEditExtender>
                                            <asp:MaskedEditExtender ID="mskEmployeeDayPhone" runat="server" MaskType="Number"
                                                Mask="999-9999" TargetControlID="txtEmployeeDayPhone">
                                            </asp:MaskedEditExtender>
                                        </div>
                                    </div>
                                </div>
                                <div class="space100 paddingBottom5">
                                    <div class="floatLeft space50 inline">
                                        <div class="floatLeft alignRight width140 paddingRight5 inline">
                                            <asp:Label ID="lblPostalCode" runat="server" Text="Postal Code:"></asp:Label>
                                        </div>
                                        <div class="inlineBlock">
                                            <asp:TextBox ID="txtPostalCode" runat="server" MaxLength="12" CssClass="input" Width="100px" />
                                        </div>
                                    </div>
                                    <div class="space49 inlineBlock">
                                        <div class="floatLeft alignRight width140 paddingRight5 inline">
                                            <asp:Label ID="lblEmployeeHomePhone" runat="server" Text="Home Phone:"></asp:Label>
                                        </div>
                                        <div class="inlineBlock">
                                            <asp:TextBox ID="txtEmployeeHomePhoneArea" runat="server" MaxLength="20" CssClass="input"
                                                Width="25px" />
                                            <asp:TextBox ID="txtEmployeeHomePhoneNumber" runat="server" MaxLength="20" CssClass="input"
                                                Width="100px" />
                                            <asp:MaskedEditExtender ID="mskEmployeeHomePhoneArea" runat="server" MaskType="Number"
                                                Mask="999" TargetControlID="txtEmployeeHomePhoneArea">
                                            </asp:MaskedEditExtender>
                                            <asp:MaskedEditExtender ID="mskEmployeeHomePhoneNumber" runat="server" MaskType="Number"
                                                Mask="999-9999" TargetControlID="txtEmployeeHomePhoneNumber">
                                            </asp:MaskedEditExtender>
                                        </div>
                                    </div>
                                </div>
                                <div class="space100 paddingBottom5">
                                    <div class="floatLeft space50 inline">
                                        <div class="floatLeft alignRight width140 paddingRight5 inline">
                                            <asp:Label ID="lblAddress2" runat="server" Text="Complement:"></asp:Label>
                                        </div>
                                        <div class="inlineBlock">
                                            <asp:TextBox ID="txtAddress2" runat="server" MaxLength="255" CssClass="input" Width="200px" />
                                        </div>
                                    </div>
                                    <div class="space49 inlineBlock">
                                        <div class="floatLeft alignRight width140 paddingRight5 inline">
                                            <asp:Label ID="lblEmployeeFaxPhone" runat="server" Text="Fax Phone:"></asp:Label>
                                        </div>
                                        <div class="inlineBlock">
                                            <asp:TextBox ID="txtEmployeeFaxCode" runat="server" MaxLength="20" CssClass="input"
                                                Width="25px" />
                                            <asp:TextBox ID="txtEmployeeFaxPhone" runat="server" MaxLength="20" CssClass="input"
                                                Width="100px" />
                                            <asp:MaskedEditExtender ID="mskEmployeeFaxCode" runat="server" MaskType="Number"
                                                Mask="999" TargetControlID="txtEmployeeFaxCode">
                                            </asp:MaskedEditExtender>
                                            <asp:MaskedEditExtender ID="mskEmployeeFaxPhone" runat="server" MaskType="Number"
                                                Mask="999-9999" TargetControlID="txtEmployeeFaxPhone">
                                            </asp:MaskedEditExtender>
                                        </div>
                                    </div>
                                    <div class="floatLeft space50 inline">
                                        <div class="floatLeft alignRight width140 paddingRight5 inline">
                                            <asp:Label ID="lblCity" runat="server" Text="City:"></asp:Label>
                                        </div>
                                        <div class="inlineBlock">
                                            <asp:TextBox ID="txtCity" runat="server" MaxLength="30" CssClass="input" Width="100px" />
                                        </div>
                                    </div>
                                    <div class="space49 inlineBlock">
                                        <div class="floatLeft alignRight width140 paddingRight5 inline">
                                            <asp:Label ID="lblEmployeeCellPhone" runat="server" Text="Cell Phone:"></asp:Label>
                                        </div>
                                        <div class="inlineBlock">
                                            <asp:TextBox ID="txtEmployeeCellPhoneArea" runat="server" MaxLength="20" CssClass="input"
                                                Width="25px" />
                                            <asp:TextBox ID="txtEmployeeCellPhoneNumber" runat="server" MaxLength="20" CssClass="input"
                                                Width="100px" />
                                            <asp:MaskedEditExtender ID="mskEmployeeCellPhoneArea" runat="server" MaskType="Number"
                                                Mask="999" TargetControlID="txtEmployeeCellPhoneArea">
                                            </asp:MaskedEditExtender>
                                            <asp:MaskedEditExtender ID="mskmployeeCellPhoneNumber" runat="server" MaskType="Number"
                                                Mask="999-9999" TargetControlID="txtEmployeeCellPhoneNumber">
                                            </asp:MaskedEditExtender>
                                        </div>
                                    </div>
                                </div>
                                <div class="space100 paddingBottom5">
                                    <div class="floatLeft space50 inline">
                                        <div class="floatLeft alignRight width140 paddingRight5 inline">
                                            <asp:Label ID="lblStateProvinceCode" runat="server" Text="State Province Code:"></asp:Label>
                                        </div>
                                        <div class="inlineBlock">
                                            <asp:TextBox ID="txtStateProvinceCode" runat="server" MaxLength="15" CssClass="input"
                                                Width="25px" />
                                        </div>
                                    </div>
                                    <div class="space49 inlineBlock">
                                        <div class="floatLeft alignRight width140 paddingRight5 inline">
                                            <asp:Label ID="lblOtherPhone" runat="server" Text="Other Phone:"></asp:Label>
                                        </div>
                                        <div class="inlineBlock">
                                            <asp:TextBox ID="txtEmployeeOtherPhoneArea" runat="server" MaxLength="20" CssClass="input"
                                                Width="25px" />
                                            <asp:TextBox ID="txtEmployeeOtherPhoneNumber" runat="server" MaxLength="20" CssClass="input"
                                                Width="100px" />
                                            <asp:MaskedEditExtender ID="mskEmployeeOtherPhoneArea" runat="server" MaskType="Number"
                                                Mask="999" TargetControlID="txtEmployeeOtherPhoneArea">
                                            </asp:MaskedEditExtender>
                                            <asp:MaskedEditExtender ID="mskEmployeeOtherPhoneNumber" runat="server" MaskType="Number"
                                                Mask="999-9999" TargetControlID="txtEmployeeOtherPhoneNumber">
                                            </asp:MaskedEditExtender>
                                        </div>
                                    </div>
                                </div>
                                <div class="space100 paddingBottom5">
                                    <div class="floatLeft space50 inline">
                                        <div class="floatLeft alignRight width140 paddingRight5 inline">
                                            <asp:Label ID="lblCountryCode" runat="server" Text="Country Code:"></asp:Label>
                                        </div>
                                        <div class="inlineBlock">
                                            <asp:TextBox ID="txtCountryCode" runat="server" MaxLength="15" CssClass="input" Width="20px" />
                                        </div>
                                    </div>
                                    <div class="space49 inlineBlock">
                                        <div class="floatLeft alignRight width140 paddingRight5 inline">
                                            <asp:Label ID="lblDentonPersonel" runat="server" Text="Denton Personel:"></asp:Label>
                                        </div>
                                        <div class="inlineBlock">
                                            <asp:CheckBox ID="chkDentonPersonel" runat="server" ClientIDMode="Static" />
                                        </div>
                                        <div class="floatLeft alignRight width140 paddingRight5 inline">
                                            <asp:Label ID="lblKeyPerson" runat="server" Text="Key Person:"></asp:Label>
                                        </div>
                                        <div class="inlineBlock">
                                            <asp:CheckBox ID="chkKeyPerson" runat="server" ClientIDMode="Static" />
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <hr />
                                <div id="errorDiv" class="errorbox" style="display: none;">
                                    <asp:Label ID="lblNumberError" runat="server" Text="Invalid Type or Number, please correct the information"></asp:Label>
                                </div>
                                <div class="inlineBlock space100">
                                    <div class="floatLeft space49 alignLeft">
                                        <br />
                                        <br />
                                        <div class="space49 paddingBottom5 block">
                                            <div class="floatLeft width140 alignRight paddingRight5">
                                                <asp:Label ID="lblAdditionalContactType" runat="server" Text="Type:"></asp:Label>
                                            </div>
                                            <div class="floatLeft alignLeft">
                                                <asp:ComboBox ID="ddlAdditionalContactType" runat="server" CssClass="WindowsStyle"
                                                    AutoCompleteMode="SuggestAppend" DropDownStyle="DropDown" CaseSensitive="false"
                                                    RenderMode="Inline" Width="100px" DataValueField="ID" DataTextField="Name">
                                                </asp:ComboBox>
                                            </div>
                                        </div>
                                        <div>
                                            <div class="floatLeft width140 alignRight paddingRight5">
                                                <asp:Label ID="lblAdditionalContactNumber" runat="server" Text="Phone:"></asp:Label>
                                            </div>
                                            <div class="floatLeft alignLeft">
                                                <asp:TextBox ID="txtAdditionalContactArea" runat="server" MaxLength="3" CssClass="input"
                                                    Width="25px" />
                                                <asp:MaskedEditExtender ID="mskAdditionalContactArea" runat="server" MaskType="Number"
                                                    Mask="999" TargetControlID="txtAdditionalContactArea">
                                                </asp:MaskedEditExtender>
                                                <asp:TextBox ID="txtAdditionalContactNumber" runat="server" MaxLength="7" CssClass="input"
                                                    Width="100px" />
                                                <asp:MaskedEditExtender ID="mskAdditionalContactNumber" runat="server" MaskType="Number"
                                                    Mask="999-9999" TargetControlID="txtAdditionalContactNumber">
                                                </asp:MaskedEditExtender>
                                            </div>
                                            <div class="paddingTop2 alignRight" style="width: 35px;">
                                                <asp:Button ID="btnAdditionalContactAdd" runat="server" Text="Add" CssClass="btn"
                                                    OnClientClick="AddPhone(); return false;" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="space50 floatRight">
                                        <asp:HiddenField ID="hfPhoneNumbers" runat="server" EnableViewState="true" ViewStateMode="Enabled" />
                                        <asp:Panel ID="pnlNoRowsPhone" runat="server">
                                            <div class="ScrollableGridView_Group" style="width: 100%">
                                                <div class="ScrollableGridView_HeaderDiv" style="min-width: 400px;">
                                                </div>
                                                <div class="ScrollableGridView_ScrollDiv" style="max-height: 220px; min-width: 400px;">
                                                    <table id="Table1" class="ScrollableGridView" cellspacing="1">
                                                        <tbody>
                                                            <tr>
                                                                <td class="even" align="center">
                                                                    The List Is Empty
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlPhoneTable" runat="server">
                                            <div id="tbPhoneNumbers_Group" class="ScrollableGridView_Group" style="width: 100%">
                                                <div id="tbPhoneNumbers_HeaderDiv" class="ScrollableGridView_HeaderDiv" style="min-width: 150px;">
                                                </div>
                                                <div id="tbPhoneNumbers_ScrollDiv" class="ScrollableGridView_ScrollDiv" style="max-height: 100px;
                                                    min-width: 400px;">
                                                    <table id="tbPhoneNumbers" class="ScrollableGridView" cellspacing="1">
                                                        <thead>
                                                            <tr style="position: relative; top: expression(this.offsetParent.scrollTop -1); left: expression(this.offsetParent.style.left);">
                                                                <th id="thID" runat="server" class="header" visible="false">
                                                                    <asp:Label ID="header1" CssClass="MarginRight" runat="server" Text="ID" Visible="false"></asp:Label>
                                                                </th>
                                                                <th id="thArea" runat="server" class="header">
                                                                    <asp:Label ID="header2" CssClass="MarginRight" runat="server" Text="Phone Type"></asp:Label>
                                                                </th>
                                                                <th id="thNumber" runat="server" class="header">
                                                                    <asp:Label ID="header3" CssClass="MarginRight" runat="server" Text="Number"></asp:Label>
                                                                </th>
                                                                <th id="thRemove" runat="server" class="header">
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </Content>
                        </uc:CollapseHolder>
                        <br />
                        <uc:CallCriteria ID="uscCallCriteria" runat="server" EnableViewState="true" />
                        <br />
                        <uc:CollapseHolder ID="chOffCall" runat="server" GridViewCssClass="ScrollableGridView">
                            <Header>
                                <asp:Label ID="lblOffCallTitle" runat="server" Text="Off Call"></asp:Label>
                            </Header>
                            <Content>
                                <div class="space100 paddingBottom5">
                                    <div class="floatLeft inline space50">
                                        <asp:Label ID="lblOffCallMsg" Style="color: Red" Visible="false" runat="server" Text="You cannot mark this employee for Off Call, It's necessary to end the resource lifecycle before saving an Off Call."></asp:Label>
                                        <asp:Panel ID="pnlOffCall" runat="server">
                                            <div class="floatLeft space100 inline">
                                                <div class="floatLeft alignRight width140 paddingRight5 inline">
                                                    <asp:Label ID="lblOffCall" runat="server" Text="Off Call:"></asp:Label>
                                                </div>
                                                <div class="inlineBlock alignLeft">
                                                    <asp:CheckBox ID="chkOffCall" runat="server" onclick="ShowHideEmployeeOffCallFields();" />
                                                </div>
                                            </div>
                                            <asp:Panel ID="pnlOffCallSettings" runat="server" Style="display: none">
                                                <div class="space100 paddingBottom5">
                                                    <div class="floatLeft alignRight width140 paddingRight5 inline">
                                                        <asp:Label ID="lblOffCallFrom" runat="server" Text="* Off Call From:"></asp:Label>
                                                    </div>
                                                    <div class="inlineBlock">
                                                        <uc1:DatePicker ID="dpOffCallFrom" runat="server" InvalidValueMessage="Invalid date format"
                                                            DateTimeFormat="Default" ShowOn="Both" IsValidEmpty="false" EmptyValueMessage="Employee Off Call - The Start Date field is required." />
                                                    </div>
                                                </div>
                                                <div class="space100 paddingBottom5">
                                                    <div class="floatLeft alignRight width140 paddingRight5 inline">
                                                        <asp:Label ID="lblOffCallTo" runat="server" Text="* Off Call To:"></asp:Label>
                                                    </div>
                                                    <div class="inlineBlock">
                                                        <uc1:DatePicker ID="dpOffCallTo" runat="server" InvalidValueMessage="Invalid date format"
                                                            DateTimeFormat="Default" ShowOn="Both" IsValidEmpty="false" EmptyValueMessage="Employee Off Call - The End Date field is required." />
                                                    </div>
                                                </div>
                                                <div class="space100 paddingBottom5">
                                                    <div class="floatLeft alignRight width140 paddingRight5 inline">
                                                        <asp:Label ID="lblReturnTime" runat="server" Text="* Return Time:"></asp:Label>
                                                    </div>
                                                    <div class="inlineBlock">
                                                        <asp:TextBox ID="txtReturnTime" runat="server" CssClass="input" Width="40px" Text=""></asp:TextBox>
                                                        <cc1:MaskedEditExtender ID="mskInitialTime" TargetControlID="txtReturnTime" runat="server"
                                                            Mask="99:99" MaskType="Time" AcceptAMPM="false" UserTimeFormat="TwentyFourHour"
                                                            AutoComplete="true">
                                                        </cc1:MaskedEditExtender>
                                                        <cc1:MaskedEditValidator ID="rfvReturnTime" runat="server" ControlExtender="mskInitialTime"
                                                            ControlToValidate="txtReturnTime" EnableClientScript="true" Display="Dynamic"
                                                            IsValidEmpty="false" InvalidValueBlurredMessage="*" InvalidValueMessage="Employee Off Call - The Return Time format is invalid"
                                                            EmptyValueBlurredText="*" EmptyValueMessage="Employee Off Call - The Return Time field is required">
                                                        </cc1:MaskedEditValidator>
                                                    </div>
                                                </div>
                                                <div class="space100 paddingBottom5">
                                                    <div class="floatLeft alignRight width140 paddingRight5 inline">
                                                        <asp:Label ID="lblProxy" runat="server" Text="* Proxy:"></asp:Label>
                                                    </div>
                                                    <div class="inlineBlock">
                                                        <uc1:AutoCompleteTextbox ID="actProxyEmployee" runat="server" ServiceMethod="GetEmployeeList"
                                                            GridViewButtonImageUrl="~/Images/money.png" GridViewIdName="ID" DisplayField="Name"
                                                            AutoPostBack="false" RequiredField="true" WindowTitle="Off Call - Proxy Employee"
                                                            AutoCompleteSource="Employee" ColumnHeaderList="Division - Name" ColumnValueList="DivisionAndFullName"
                                                            TextBoxCssClass="input" ScriptToExecute="enableAll" ErrorMessage="Employee Off Call - The Proxy Employee field is required" />
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </asp:Panel>
                                    </div>
                                    <div class="inlineBlock">
                                        <asp:Label ID="lblOffCallHistory" Text="Off Call History" runat="server"></asp:Label>
                                        <asp:ScrollableGridView runat="server" ID="gvOffCallHistory" AllowSorting="true"
                                            AutoGenerateColumns="false" CssClass="ScrollableGridView" SaveScrollPosition="true"
                                            MaxHeight="200px" EnableViewState="true" MinWidth="90%" Width="90%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Proxy Employee">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProxyEmployee" Text='<%# Eval("CS_Employee_Proxy.FullName") %>'
                                                            runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Start Date" DataField="OffCallStartDate" DataFormatString="{0:MM/dd/yyyy}" />
                                                <asp:BoundField HeaderText="End Date" DataField="OffCallEndDate" DataFormatString="{0:MM/dd/yyyy}" />
                                                <asp:BoundField HeaderText="Return Time" DataField="OffCallReturnTime" />
                                            </Columns>
                                        </asp:ScrollableGridView>
                                    </div>
                                </div>
                            </Content>
                        </uc:CollapseHolder>
                        <br />
                        <uc:CollapseHolder ID="chCoverage" runat="server" GridViewCssClass="ScrollableGridView">
                            <Header>
                                <asp:Label ID="lblCoverageTitle" runat="server" Text="Employee Coverage"></asp:Label>
                            </Header>
                            <Content>
                                <div class="space100 paddingBottom5">
                                    <div class="floatLeft inline space49">
                                        <asp:HiddenField ID="hfIsCoverage" runat="server" />
                                        <asp:Label ID="lblMsg" Visible="false" runat="server" Text="You cannot mark this employee for coverage because it´s already assigned to a job."></asp:Label>
                                        <asp:Panel ID="pnlCoverage" runat="server">
                                            <div class="floatLeft space100 inline">
                                                <div class="floatLeft alignRight width140 paddingRight5 inline">
                                                    <asp:Label ID="lblCoverage" runat="server" Text="Coverage:"></asp:Label>
                                                </div>
                                                <div class="inlineBlock alignLeft">
                                                    <asp:CheckBox ID="chkCoverage" runat="server" onclick="ShowHideEmployeeCoverageFields();" />
                                                </div>
                                            </div>
                                            <div class="floatLeft space100 inline">
                                                <div class="floatLeft alignRight width140 paddingRight5 inline">
                                                    <asp:Label ID="lblActualDivision" runat="server" Text="Home Division:"></asp:Label>
                                                </div>
                                                <div class="inlineBlock alignLeft">
                                                    <asp:Label ID="lblDivisionName" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <asp:Panel ID="pnlEmployeeCoverageStart" runat="server" Style="display: none">
                                                <div class="floatLeft space100 inline">
                                                    <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                        <asp:Label ID="lblCoverageDivision" runat="server" Text="* Coverage Division:"></asp:Label>
                                                    </div>
                                                    <div class="inlineBlock">
                                                        <uc1:AutoCompleteTextbox ID="actDivision" runat="server" ServiceMethod="GetDivisionList"
                                                            TextBoxWidth="120px" GridViewButtonImageUrl="~/Images/money.png" GridViewIdName="ID"
                                                            DisplayField="Division" FilterId="0" ContextKey="0" AutoPostBack="false" RequiredField="true"
                                                            WindowTitle="Find Division" ErrorMessage="Employee Coverage - Coverage Division field is required"
                                                            AutoCompleteSource="Division" ColumnHeaderList="Name,Description" TextBoxCssClass="input"
                                                            ColumnValueList="Name,Description" />
                                                    </div>
                                                </div>
                                                <div class="floatLeft space100 inline">
                                                    <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                        <asp:Label ID="lblCoverageDateTimeFrom" runat="server" Text="* Start Date and Time:"></asp:Label>
                                                    </div>
                                                    <div class="inlineBlock">
                                                        <uc1:DatePicker ID="dpEmployeeCoverageFrom" InvalidValueMessage="Employee Coverage - The Start Date format is invalid"
                                                            DateTimeFormat="Default" IsValidEmpty="false" ShowOn="Both" runat="server" EmptyValueMessage="Employee Coverage - The Start date field is required" />
                                                        <asp:TextBox ID="txtCoverageTimeFrom" runat="server" CssClass="input" Width="40px"></asp:TextBox>
                                                        <cc1:MaskedEditExtender ID="mskCoverageTimeFrom" TargetControlID="txtCoverageTimeFrom"
                                                            runat="server" Mask="99:99" MaskType="Time" AcceptAMPM="false" UserTimeFormat="TwentyFourHour"
                                                            AutoComplete="true">
                                                        </cc1:MaskedEditExtender>
                                                        <cc1:MaskedEditValidator ID="rfvCoverageTimeValidatorFrom" runat="server" ControlExtender="mskCoverageTimeFrom"
                                                            ControlToValidate="txtCoverageTimeFrom" EnableClientScript="true" Display="Dynamic"
                                                            IsValidEmpty="false" InvalidValueBlurredMessage="*" InvalidValueMessage="Employee Coverage - The Start time format is invalid"
                                                            EmptyValueBlurredText="*" EmptyValueMessage="Employee Coverage - The Start time field is required"
                                                            Text="*">
                                                        </cc1:MaskedEditValidator>
                                                    </div>
                                                </div>
                                                <div class="floatleft space100 inline">
                                                    <div class="floatLeft alignRight width140 paddingRight5 paddingTop2 inline">
                                                        <asp:Label ID="lblCoverageDuration" Text="* Duration:" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="inlineBlock">
                                                        <asp:TextBox ID="txtCoverageDuration" CssClass="input" runat="server" MaxLength="4"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender TargetControlID="txtCoverageDuration" ID="fteCoverageDuration"
                                                            FilterType="Numbers" runat="server">
                                                        </asp:FilteredTextBoxExtender>
                                                        <asp:RequiredFieldValidator ID="rfvCoverageDuration" runat="server" Text="*" ErrorMessage="Employee Coverage - The Duration Field is required."
                                                            ControlToValidate="txtCoverageDuration"></asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ControlToValidate="txtCoverageDuration" ID="cpvCoverageDuration"
                                                            runat="server" Text="*" ErrorMessage="Employee Coverage - The Duration Field does not allow 0."
                                                            Operator="GreaterThan" ValueToCompare="0"></asp:CompareValidator>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlEmployeeCoverageEnd" runat="server" Style="display: none">
                                                <div class="floatLeft space100 inline">
                                                    <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                                        <asp:Label ID="lblCoverageDateTimeTO" runat="server" Text="* End Date and Time:"></asp:Label>
                                                    </div>
                                                    <div class="inlineBlock">
                                                        <uc1:DatePicker ID="dpEmployeeCoverageTO" InvalidValueMessage="Employee Coverage - The End date format is invalid"
                                                            DateTimeFormat="Default" ShowOn="Both" runat="server" EmptyValueMessage="Employee Coverage - The End date field is required" />
                                                        <asp:TextBox ID="txtCoverageTimeTO" runat="server" CssClass="input" Width="40px"></asp:TextBox>
                                                        <cc1:MaskedEditExtender ID="mskCoverageTimeTO" TargetControlID="txtCoverageTimeTO"
                                                            runat="server" Mask="99:99" MaskType="Time" AcceptAMPM="false" UserTimeFormat="TwentyFourHour"
                                                            AutoComplete="true">
                                                        </cc1:MaskedEditExtender>
                                                        <cc1:MaskedEditValidator ID="rfvCoverageTimeValidatorTO" runat="server" ControlExtender="mskCoverageTimeTO"
                                                            ControlToValidate="txtCoverageTimeTO" EnableClientScript="true" Display="Dynamic"
                                                            IsValidEmpty="false" InvalidValueBlurredMessage="*" InvalidValueMessage="Employee Coverage - The End time format is invalid"
                                                            EmptyValueBlurredText="*" EmptyValueMessage="Employee Coverage - The End time field is required"
                                                            Text="*">
                                                        </cc1:MaskedEditValidator>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </asp:Panel>
                                    </div>
                                    <div class="inlineBlock">
                                        <asp:Label ID="lblCoverHistoryTitle" Text="Coverage History" runat="server"></asp:Label>
                                        <asp:ScrollableGridView runat="server" ID="gvCoverHistory" AllowSorting="true" AutoGenerateColumns="false"
                                            CssClass="ScrollableGridView" SaveScrollPosition="true" MaxHeight="200px" EnableViewState="true"
                                            MinWidth="90%" Width="90%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Division">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDivision" Text='<%# Eval("CS_Division.Name") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Start" DataField="CoverageStartDate" DataFormatString="{0:MM/dd/yyyy HH:mm}" />
                                                <asp:BoundField HeaderText="End" DataField="CoverageEndDate" DataFormatString="{0:MM/dd/yyyy HH:mm}" />
                                                <asp:BoundField HeaderText="Duration" DataField="Duration" />
                                            </Columns>
                                        </asp:ScrollableGridView>
                                    </div>
                                </div>
                            </Content>
                        </uc:CollapseHolder>
                        <br />
                        <uc:CollapseHolder ID="chDrivingRecordInformation" runat="server">
                            <Header>
                                <asp:Label ID="lblDrivingRecordInformationTitle" runat="server" Text="Driving Record Information"></asp:Label>
                            </Header>
                            <Content>
                                <div class="space100 paddingBottom5">
                                    <div class="floatLeft space50 inline paddingBottom5">
                                        <div class="floatLeft alignRight width140 paddingRight5 inline">
                                            <asp:Label ID="lblDriversLicenseNumber" runat="server" Text="Driver's License #:"></asp:Label>
                                        </div>
                                        <div class="inlineBlock">
                                            <asp:Label ID="lblDriversLicenseNumberDesc" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                    <div class="floatLeft space49 inline paddingBottom5">
                                        <div class="floatLeft alignRight width140 paddingRight5 inline">
                                            <asp:Label ID="lblDriversLicenseClass" runat="server" Text="Driver's License Class:"></asp:Label>
                                        </div>
                                        <div class="inlineBlock">
                                            <asp:Label ID="lblDriversLicenseClassDesc" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="space100 paddingBottom5">
                                    <div class="floatLeft space50 inline paddingBottom5">
                                        <div class="floatLeft alignRight width140 paddingRight5 inline">
                                            <asp:Label ID="lblDriversLicenseState" runat="server" Text="Driver's License State:"></asp:Label>
                                        </div>
                                        <div class="inlineBlock">
                                            <asp:Label ID="lblDriversLicenseStateDesc" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                    <div class="floatLeft space49 inline paddingBottom5">
                                        <div class="floatLeft alignRight width140 paddingRight5 inline">
                                            <asp:Label ID="lblDriversLicenseExpireDate" runat="server" Text="Expire Date:"></asp:Label>
                                        </div>
                                        <div class="inlineBlock">
                                            <asp:Label ID="lblDriversLicenseExpireDateDesc" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </Content>
                        </uc:CollapseHolder>
                        <br />
                        <uc:CollapseHolder ID="chSafetyInformation" runat="server">
                            <Header>
                                <asp:Label ID="lblSafetyInformationTitle" runat="server" Text="Safety Information"></asp:Label>
                            </Header>
                            <Content>
                            </Content>
                        </uc:CollapseHolder>
                        <br />
                        <uc:CollapseHolder ID="chCertificationInformation" runat="server">
                            <Header>
                                <asp:Label ID="lblCertificationInformationTitle" runat="server" Text="Certification Information"></asp:Label>
                            </Header>
                            <Content>
                            </Content>
                        </uc:CollapseHolder>
                        <br />
                        <div>
                            <div style="text-align: right; padding-top: 5px;">
                                <asp:Button ID="btnSaveClose" runat="server" CssClass="btn" Text="Save & Close" OnClick="btnSaveClose_Click" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Cancel" OnClientClick="window.close();return false;"
                                    UseSubmitBehavior="false" />
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlNoAccess" runat="server" Visible="false">
                <div class="Page">
                    <div class="Header">
                        <asp:Label ID="lblTitleNoAccess" runat="server" Text="Employee Details" />
                    </div>
                    <div class="Content">
                        The current user does not have access to this functionality!
                    </div>
                </div>
            </asp:Panel>
            <script type="text/javascript" language="javascript">

                $(document).ready(function () { if (document.getElementById('<%= actEmployee.TextControlClientID %>').value == '') disableAll(); });

                function disableAll() {
                    $('.usercontrolDiv').attr('disabled', true);
                }

                function enableAll(holder) {
                    if (holder != "0")
                        $('.usercontrolDiv').attr('disabled', false);
                }

                function ShowHideEmployeeCoverageFields() {
                    if ($('#<%= chkCoverage.ClientID %>').length > 0) {
                        if ($('#<%= chkCoverage.ClientID %>').attr('checked')) {
                            if ($('#<%= pnlEmployeeCoverageStart.ClientID %>').length > 0)
                                $('#<%= pnlEmployeeCoverageStart.ClientID %>').attr("style", "display:inline");
                            if ($get('<%=rfvCoverageTimeValidatorFrom.ClientID %>'))
                                ValidatorEnable($get('<%=rfvCoverageTimeValidatorFrom.ClientID %>'), true);
                            if ($get('<%=actDivision.RequiredFieldClientId %>'))
                                ValidatorEnable($get('<%=actDivision.RequiredFieldClientId %>'), true);
                            if ($get('<%=rfvCoverageDuration.ClientID %>'))
                                ValidatorEnable($get('<%=rfvCoverageDuration.ClientID %>'), true);
                            if ($get('<%=cpvCoverageDuration.ClientID %>'))
                                ValidatorEnable($get('<%=cpvCoverageDuration.ClientID %>'), true);
                            if ($('#<%= pnlEmployeeCoverageEnd.ClientID %>').length > 0)
                                $('#<%= pnlEmployeeCoverageEnd.ClientID %>').attr("style", "display:none");

                            //Validators coverage
                            if ($('#<% = pnlEmployeeCoverageEnd.ClientID %>').css('display') != 'none') {
                                if ($get('<% = dpEmployeeCoverageTO.TextBoxClientID %>')) {
                                    ValidatorEnable($get('<%=dpEmployeeCoverageTO.MaskedEditValidatorID %>'), true);
                                }
                                if ($get('<%=rfvCoverageTimeValidatorTO.ClientID %>')) {
                                    ValidatorEnable($get('<%=rfvCoverageTimeValidatorTO.ClientID %>'), true);
                                }
                            }
                            else {
                                if ($get('<% = dpEmployeeCoverageTO.TextBoxClientID %>')) {
                                    ValidatorEnable($get('<%=dpEmployeeCoverageTO.MaskedEditValidatorID %>'), false);
                                } if ($get('<%=rfvCoverageTimeValidatorTO.ClientID %>')) {
                                    ValidatorEnable($get('<%=rfvCoverageTimeValidatorTO.ClientID %>'), false);
                                }
                            }

                            if ($get('<% = dpEmployeeCoverageFrom.TextBoxClientID %>').style("display") != 'none') {
                                ValidatorEnable($get('<%=dpEmployeeCoverageFrom.MaskedEditValidatorID %>'), true);
                            }
                        }
                        else {
                            if ($('#<%= hfIsCoverage.ClientID %>').length > 0) {
                                if ($('#<%= pnlEmployeeCoverageEnd.ClientID %>').length > 0) {
                                    if ($('#<%= hfIsCoverage.ClientID %>').attr('value') == '1')
                                        $('#<%= pnlEmployeeCoverageEnd.ClientID %>').attr("style", "display:inline");
                                    else
                                        $('#<%= pnlEmployeeCoverageEnd.ClientID %>').attr("style", "display:none");
                                }
                                if ($('#<%= pnlEmployeeCoverageStart.ClientID %>').length > 0) {
                                    if ($('#<%= hfIsCoverage.ClientID %>').attr('value') == '1')
                                        $('#<%= pnlEmployeeCoverageStart.ClientID %>').attr("style", "display:inline");
                                    else
                                        $('#<%= pnlEmployeeCoverageStart.ClientID %>').attr("style", "display:none");
                                }
                                if ($get('<%=rfvCoverageDuration.ClientID %>')) {
                                    if ($('#<%= hfIsCoverage.ClientID %>').attr('value') == '1') {
                                        ValidatorEnable($get('<%=rfvCoverageDuration.ClientID %>'), true);
                                    }
                                    else {
                                        ValidatorEnable($get('<%=rfvCoverageDuration.ClientID %>'), false);
                                    }
                                }
                                if ($get('<%=cpvCoverageDuration.ClientID %>')) {
                                    if ($('#<%= hfIsCoverage.ClientID %>').attr('value') == '1') {
                                        ValidatorEnable($get('<%=cpvCoverageDuration.ClientID %>'), true);
                                    }
                                    else {
                                        ValidatorEnable($get('<%=cpvCoverageDuration.ClientID %>'), false);
                                    }
                                }
                                if ($get('<%=actDivision.RequiredFieldClientId %>')) {
                                    if ($('#<%= hfIsCoverage.ClientID %>').attr('value') == '1')
                                        ValidatorEnable($get('<%=actDivision.RequiredFieldClientId %>'), true);
                                    else
                                        ValidatorEnable($get('<%=actDivision.RequiredFieldClientId %>'), false);
                                }
                                if ($get('<% = dpEmployeeCoverageFrom.MaskedEditValidatorID %>')) {
                                    if ($('#<%= hfIsCoverage.ClientID %>').attr('value') == '1')
                                        ValidatorEnable($get('<%=dpEmployeeCoverageFrom.MaskedEditValidatorID %>'), true);
                                    else
                                        ValidatorEnable($get('<%=dpEmployeeCoverageFrom.MaskedEditValidatorID %>'), false);
                                }

                                if ($get('<%=rfvCoverageTimeValidatorFrom.ClientID %>')) {
                                    if ($('#<%= hfIsCoverage.ClientID %>').attr('value') == '1')
                                        ValidatorEnable($get('<%=rfvCoverageTimeValidatorFrom.ClientID %>'), true);
                                    else
                                        ValidatorEnable($get('<%=rfvCoverageTimeValidatorFrom.ClientID %>'), false);
                                }
                                if ($get('<% = dpEmployeeCoverageTO.MaskedEditValidatorID %>')) {
                                    if ($('#<%= hfIsCoverage.ClientID %>').attr('value') == '1')
                                        ValidatorEnable($get('<%=dpEmployeeCoverageTO.MaskedEditValidatorID %>'), true);
                                    else
                                        ValidatorEnable($get('<%=dpEmployeeCoverageTO.MaskedEditValidatorID %>'), false);
                                }

                                if ($get('<%=rfvCoverageTimeValidatorTO.ClientID %>')) {
                                    if ($('#<%= hfIsCoverage.ClientID %>').attr('value') == '1')
                                        ValidatorEnable($get('<%=rfvCoverageTimeValidatorTO.ClientID %>'), true);
                                    else
                                        ValidatorEnable($get('<%=rfvCoverageTimeValidatorTO.ClientID %>'), false);
                                }
                            }
                        }
                    }
                }

                function ShowHideEmployeeOffCallFields() {
                    if ($('#<%= chkOffCall.ClientID %>').length > 0) {
                        if ($('#<%= chkOffCall.ClientID %>').attr('checked')) {
                            if ($('#<%= pnlOffCallSettings.ClientID %>').length > 0)
                                $('#<%= pnlOffCallSettings.ClientID %>').attr("style", "display:inline");
                            if ($get('<%=rfvReturnTime.ClientID %>'))
                                ValidatorEnable($get('<%=rfvReturnTime.ClientID %>'), true);

                            if ($get('<% = dpOffCallFrom.TextBoxClientID %>'))
                                ValidatorEnable($get('<%=dpOffCallFrom.MaskedEditValidatorID %>'), true);
                            if ($get('<% = dpOffCallTo.TextBoxClientID %>'))
                                ValidatorEnable($get('<%=dpOffCallTo.MaskedEditValidatorID %>'), true);

                            if ($get('<%=actProxyEmployee.RequiredFieldClientId %>'))
                                ValidatorEnable($get('<%=actProxyEmployee.RequiredFieldClientId %>'), true);
                        }
                        else {
                            if ($('#<%= pnlOffCallSettings.ClientID %>').length > 0)
                                $('#<%= pnlOffCallSettings.ClientID %>').attr("style", "display:none");
                            if ($get('<%=rfvReturnTime.ClientID %>'))
                                ValidatorEnable($get('<%=rfvReturnTime.ClientID %>'), false);

                            if ($get('<% = dpOffCallFrom.TextBoxClientID %>'))
                                ValidatorEnable($get('<%=dpOffCallFrom.MaskedEditValidatorID %>'), false);
                            if ($get('<% = dpOffCallTo.TextBoxClientID %>'))
                                ValidatorEnable($get('<%=dpOffCallTo.MaskedEditValidatorID %>'), false);

                            if ($get('<%=actProxyEmployee.RequiredFieldClientId %>'))
                                ValidatorEnable($get('<%=actProxyEmployee.RequiredFieldClientId %>'), false);
                        }
                    }
                }

                function ValidateJobAssigment() {
                    if (document.getElementById('<%= chkCoverage.ClientID %>').checked)
                        return confirm('This equipment is currently assigned to a job. Do you wish to proceed with the Coverage operation?');
                    else
                        return true;
                }

                function AddPhone() {
                    var hidden = document.getElementById('<%= hfPhoneNumbers.ClientID %>');
                    var tbPhoneNumbers = document.getElementById("tbPhoneNumbers");

                    var type = $find('<%= ddlAdditionalContactType.ClientID %>');
                    var area = document.getElementById('<%= txtAdditionalContactArea.ClientID %>');
                    var number = document.getElementById('<%= txtAdditionalContactNumber.ClientID %>');
                    var errorDiv = document.getElementById('errorDiv');
                    var errorLabel = document.getElementById('<%= lblNumberError.ClientID %>');
                    var showError = false;

                    if (null != hidden && null != type && null != area && null != number) {
                        if ("" != area.value && "" != number.value && type._hiddenFieldControl.value >= 0) {
                            var numberToInsert = '0;' + type._textBoxControl.value + ';' + area.value + '-' + number.value.substr(0, 3) + '-' + number.value.substr(3, 4)

                            if (hidden.value.indexOf(numberToInsert) == -1) {
                                if ("" != hidden.value)
                                    hidden.value += '|' + numberToInsert;
                                else
                                    hidden.value = numberToInsert;
                                
                                CreatePhoneTable();
                            }
                            else {
                                errorLabel.innerHTML = 'The specified Phone Number already exists in the list.';
                                showError = true;
                            }
                        }
                        else {
                            errorLabel.innerHTML = 'The specified Phone Type/Number is incorrect, please verify the information.';
                            showError = true;
                        }

                        if (showError)
                            errorDiv.style.display = 'block';
                    }
                }

                function RemovePhone(index) {
                    var hidden = document.getElementById('<%= hfPhoneNumbers.ClientID %>');
                    var tbPhoneNumbers = document.getElementById("tbPhoneNumbers");

                    var replace = '';
                    var row = tbPhoneNumbers.rows[index];

                    var id = row.cells[0].innerHTML;
                    var type = row.cells[1].innerHTML;
                    var phone = row.cells[2].innerHTML;

                    //try to replace if on first/middle position of string
                    replace = id + ';' + type + ';' + phone + '|';
                    hidden.value = hidden.value.replace(replace, "");

                    //try to replace if on last position of string
                    replace = '|' + id + ';' + type + ';' + phone;
                    hidden.value = hidden.value.replace(replace, "");

                    //try to replace if only element on string
                    replace = id + ';' + type + ';' + phone;
                    hidden.value = hidden.value.replace(replace, "");
                    
                    CreatePhoneTable();
                }

                //Dinamic Bind of the PhoneNumber Grid
                function CreatePhoneTable() {
                    var hidden = document.getElementById('<%= hfPhoneNumbers.ClientID %>');
                    var tbPhoneNumbers = document.getElementById("tbPhoneNumbers");
                    var pnlGrid = document.getElementById('<%= pnlPhoneTable.ClientID %>');
                    var pnlNoRowsPhone = document.getElementById('<%= pnlNoRowsPhone.ClientID %>');

                    //Clear Table
                    rowCount = tbPhoneNumbers.rows.length;
                    for (var i = rowCount - 1; i > 0; i--) {
                        tbPhoneNumbers.deleteRow(i);
                    }

                    if ("" != hidden.value) {
                        var phoneList = hidden.value.split('|');

                        //Insert New Values
                        for (var i = 0; i < phoneList.length; i++) {
                            var values = phoneList[i].split(';');
                            var rowIndex = parseInt(i + 1);

                            var row = tbPhoneNumbers.insertRow(rowIndex);

                            var cell1 = row.insertCell(0);
                            cell1.innerHTML = values[0];
                            cell1.style.display = 'none';

                            var cell2 = row.insertCell(1);
                            cell2.innerHTML = values[1];

                            var cell3 = row.insertCell(2);
                            cell3.innerHTML = values[2];

                            var cell4 = row.insertCell(3);
                            var button = document.createElement("input");
                            button.id = rowIndex;
                            button.type = "button";
                            button.setAttribute("className", "linkButtonStyle");
                            button.value = "Remove";
                            button.onclick = function () { RemovePhone(this.id); };
                            cell4.appendChild(button);
                        }
                    }

                    pnlGrid.style.display = (tbPhoneNumbers.rows.length > 1) ? "" : "none";
                    pnlNoRowsPhone.style.display = (tbPhoneNumbers.rows.length > 1) ? "none" : "";
                }

                var scriptManager = Sys.WebForms.PageRequestManager.getInstance();
                scriptManager.add_endRequest(function () {
                    ShowHideEmployeeCoverageFields();
                    ShowHideEmployeeOffCallFields(); 
                    CreatePhoneTable();
                });

                $(document).ready(function () {
                    ShowHideEmployeeCoverageFields();
                    ShowHideEmployeeOffCallFields();
                });
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
