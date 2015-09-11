<%@ Page Language="C#" MasterPageFile="~/ContentPage.master" AutoEventWireup="true"
    CodeBehind="CallEntry.aspx.cs" Inherits="Hulcher.OneSource.CustomerService.Web.CallEntry" %>

<%@ MasterType TypeName="Hulcher.OneSource.CustomerService.Web.ContentPage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/AutoCompleteTextbox.ascx" TagName="AutoCompleteTextbox"
    TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/JobRecord/EquipmentRequested.ascx" TagName="EquipmentRequested"
    TagPrefix="eq" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/CallEntry.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/jquery.multiselect.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <script src="Scripts/DirtyFormValidator.js" type="text/javascript"></script>
    <asp:Panel ID="pnlForDefaultButton" runat="server" DefaultButton="btnSaveContinue">
        <asp:UpdatePanel ID="upCallEntry" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Button ID="btnRefreshHistory" runat="server" OnClick="btnRefreshHistory_Click"
                    CausesValidation="false" Style="display: none;" />
                <asp:HiddenField ID="hfIsHistoryUpdate" runat="server" />
                <asp:HiddenField ID="hfRefreshUniqueID" runat="server" />
                <asp:HiddenField ID="hfGeneralLog" runat="server" />
                <asp:HiddenField ID="hfSave" runat="server" Value="" />
                <asp:HiddenField ID="hfEmail" runat="server" />
                <asp:HiddenField ID="hfValidatorEmployeeStatus" runat="server" />
                <asp:HiddenField ID="hfValidatorExternalStatus" runat="server" />
                <asp:HiddenField ID="hfValidatiorCustomerStatus" runat="server" />
                <asp:HiddenField ID="hfJobId" runat="server" />
                <asp:HiddenField ID="hfCallTypeId" runat="server" />
                <asp:HiddenField ID="hfCallCriteria" runat="server" />
                <asp:HiddenField ID="hfDynamicXml" runat="server" />
                <asp:HiddenField ID="hfSelectedCallTypeId" runat="server" />
                <asp:ValidationSummary ID="vsCallEntry" runat="server" CssClass="errorbox" ValidationGroup="CallEntry"
                    HeaderText="Please correct the following information" />
                <asp:PlaceHolder ID="phJobFilter" runat="server">
                    <div class="Header">
                        <asp:Label ID="lblJobSearchHeader" runat="server" Text="Select a Job" />
                    </div>
                    <div class="Content">
                        <div>
                            <asp:RadioButton ID="rbGeneralLog" runat="server" AutoPostBack="true" Text="General Log (Non job Related) - 999999"
                                OnCheckedChanged="rbGeneralLog_CheckedChanged" GroupName="jobFilter" Checked="false" />
                        </div>
                        <asp:PlaceHolder ID="phRadioJobRecord" runat="server">
                            <div>
                                <asp:RadioButton ID="rbJobRecord" runat="server" AutoPostBack="true" Text="Job Record"
                                    GroupName="jobFilter" OnCheckedChanged="rbJobRecord_CheckedChanged" Checked="false" />
                            </div>
                        </asp:PlaceHolder>
                        <asp:Panel ID="phFindJob" runat="server" DefaultButton="btnFind" Visible="false">
                            <div class="filter">
                                <div class="control" style="text-align: right;">
                                    <asp:Label ID="lblFind" runat="server" Text="Find:"></asp:Label>&nbsp;
                                    <asp:DropDownList ID="ddlFilter" runat="server" CssClass="Dropdownlist">
                                        <asp:ListItem Text="- Select One -" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Job Number" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Division" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Company" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Location" Value="4"></asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;
                                    <asp:TextBox ID="txtName" runat="server" CssClass="input" />&nbsp;
                                    <asp:Button ID="btnFind" runat="server" Text="Find" CausesValidation="false" OnClick="btnFind_Click"
                                        CssClass="btn" />
                                </div>
                            </div>
                            <div class="gridview">
                                <asp:ScrollableGridView ID="gvJobFilter" runat="server" CssClass="ScrollableGridView"
                                    AutoGenerateColumns="false" ShowFooter="false" OnRowDataBound="gvJobFilter_RowDataBound">
                                    <Columns>
                                        <asp:BoundField HeaderText="" DataField="ID" Visible="false" />
                                        <asp:BoundField HeaderText="Div#" DataField="" />
                                        <asp:BoundField HeaderText="Job#" DataField="" />
                                        <asp:BoundField HeaderText="Status" DataField="" />
                                        <asp:BoundField HeaderText="Company" DataField="" />
                                        <asp:BoundField HeaderText="Location" DataField="" />
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Panel ID="pnlJobFilter" runat="server">
                                                    <asp:Button ID="btnSelect" runat="server" Text="Select" CssClass="btn" CommandName="SelectButton"
                                                        CommandArgument='<%# Eval("ID") %>' OnCommand="btnSelect_Command" CausesValidation="false" />
                                                </asp:Panel>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:ScrollableGridView>
                            </div>
                            <hr width="100%" />
                        </asp:Panel>
                    </div>
                    <br />
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="phJobInformation" runat="server" Visible="false">
                    <div style="width: 100%; margin-bottom: 10px;">
                        <div style="width: 39%; float: left;">
                            <div class="Header">
                                <asp:Label ID="lblJobInformationHeader" runat="server" Text="Job Info" />
                            </div>
                            <div class="Content" style="height: 150px;">
                                <div id="divGeneral" runat="server" class="divUpperFields">
                                    <div class="label">
                                        <asp:Label ID="lblJob" Text="Job #:" runat="server"></asp:Label></div>
                                    <div class="text">
                                        <asp:Label ID="lblJobNumber" Text="99999" runat="server"></asp:Label></div>
                                    <div class="label">
                                        <asp:Label ID="lblCustomer" runat="server" Text="Company Name:"></asp:Label></div>
                                    <div class="text">
                                        <asp:Label ID="lblCustomerName" Text="General Log" runat="server"></asp:Label></div>
                                </div>
                                <div id="divSpecific" runat="server" class="divUpperFields">
                                    <div class="label">
                                        <asp:Label ID="lblDivision" runat="server" Text="Division:"></asp:Label></div>
                                    <div class="text">
                                        <asp:Label ID="lblDivisionNumber" runat="server" Text="084"></asp:Label></div>
                                    <div class="label">
                                        <asp:Label ID="lblCity" runat="server" Text="City:"></asp:Label></div>
                                    <div class="text">
                                        <asp:Label ID="lblCityName" runat="server" Text="Fort Worth"></asp:Label></div>
                                    <div class="label">
                                        <asp:Label ID="lblState" runat="server" Text="State:"></asp:Label></div>
                                    <div class="text">
                                        <asp:Label ID="lblStateName" runat="server" Text="TX"></asp:Label></div>
                                </div>
                            </div>
                        </div>
                        <div style="float: right; width: 60%;">
                            <div class="Header">
                                <asp:Label ID="lblCallerInfo" runat="server" Text="Caller Info" />
                            </div>
                            <div class="Content" style="height: 150px;">
                                <asp:Panel ID="pnlCalledInBy" runat="server">
                                    <div class="filter">
                                        <div class="title LongText">
                                            <asp:Label ID="lblCalledByExternal" runat="server" Text="* Called in by External:"></asp:Label>
                                        </div>
                                        <div class="control">
                                            <asp:TextBox ID="txtCalledInExternal" runat="server" CssClass="input" onblur="ChangeCalledInValidationExternal(this);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCalledInExternal" runat="server" ControlToValidate="txtCalledInExternal"
                                                Display="Dynamic" ErrorMessage="Called in By External is required" Text="*" ValidationGroup="CallEntry" />
                                        </div>
                                        <div class="title LongText">
                                            <asp:Label ID="lblCalledByEmployee" runat="server" Text="* Called in by Employee:"></asp:Label>
                                        </div>
                                        <div class="control">
                                            <uc1:AutoCompleteTextbox ID="actCalledInEmployee" runat="server" ServiceMethod="GetEmployeeList"
                                                GridViewButtonImageUrl="~/Images/money.png" GridViewIdName="ID" DisplayField="Name"
                                                FilterId="0" ContextKey="0" OnTextChanged="actCalledInEmployee_TextChanged" AutoPostBack="false"
                                                RequiredField="false" WindowTitle="Call Entry - Find Employee" ErrorMessage="Called In By Employee field is required"
                                                AutoCompleteSource="Employee" ColumnHeaderList="Division - Name" TextBoxCssClass="input"
                                                ColumnValueList="DivisionAndFullName" ValidationGroup="CallEntry" ScriptToExecute="ChangeCalledInValidationEmployee" />
                                            <asp:Button ID="btnFakeEmployeeChange" runat="server" OnClick="btnFakeEmployeeChange_Click"
                                                Style="display: none;" />
                                        </div>
                                        <div class="title LongText">
                                            <asp:Label ID="lblCalledByCustomer" runat="server" Text="* Called in by Company:"></asp:Label>
                                        </div>
                                        <div class="control">
                                            <uc1:AutoCompleteTextbox ID="actCalledInCustomer" runat="server" ServiceMethod="GetCustomerServiceContactList"
                                                GridViewButtonImageUrl="~/Images/money.png" GridViewIdName="ID" DisplayField="FullContactInformation"
                                                OnTextChanged="actCalledInCustomer_TextChanged" AutoPostBack="false" RequiredField="false"
                                                WindowTitle="Call Entry - Find Contact" ErrorMessage="Called In By Company field is required"
                                                AutoCompleteSource="CustomerServiceContact" ColumnHeaderList="Name" TextBoxCssClass="input"
                                                ColumnValueList="FullContactInformation" ValidationGroup="CallEntry" ScriptToExecute="ChangeCalledInValidationCustomer" />
                                            <asp:Button ID="btnFakeCustomerChange" runat="server" OnClick="btnFakeCustomerChange_Click"
                                                Style="display: none;" />
                                        </div>
                                        <div class="title LongText">
                                        </div>
                                        <div class="control">
                                            <asp:HyperLink ID="hypAddNewCustomerContact" Width="100px" runat="server" CssClass="btn"
                                                Text="Add New Contact" NavigateUrl="javascript: var newWindow = window.open('/CustomerMaintenance.aspx?ViewType=CONTACT&NewContact=true&RefField=actCalledInCustomer', '', 'width=1200, height=600, scrollbars=1, resizable=yes');" />
                                        </div>
                                        <br />
                                        <asp:Panel ID="pnlSelectCustomer" runat="server" Visible="false">
                                            <div class="title LongText">
                                                <asp:Label ID="lblContactsAutoComplete" runat="server" Text="Select a Company:"></asp:Label>
                                            </div>
                                            <div class="control">
                                                <uc1:AutoCompleteTextbox ID="actCustomer" runat="server" ServiceMethod="GetCustomerList"
                                                    GridViewButtonImageUrl="~/Images/money.png" GridViewIdName="ID" DisplayField="Name"
                                                    OnTextChanged="actCustomer_TextChanged" AutoPostBack="false" RequiredField="false"
                                                    WindowTitle="Call Entry - Find Company" ErrorMessage="The Company field is required"
                                                    AutoCompleteSource="Customer" ColumnHeaderList="Name,Attn,Company ID" TextBoxCssClass="input"
                                                    ColumnValueList="Name,Attn,CustomerNumber" />
                                            </div>
                                        </asp:Panel>
                                        <div class="title LongText">
                                            <asp:CheckBox ID="chkUserCall" runat="server" Text="Call generated by user" OnCheckedChanged="chkUserCall_CheckedChanged"
                                                AutoPostBack="false" onclick="ChangeCalledInValidationUser(this)"></asp:CheckBox>
                                        </div>
                                        <div class="control">
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <div style="width: 100%;">
                        <div style="width: 39%; float: left;">
                            <div class="Header">
                                <asp:Label ID="lblCallInfo" runat="server" Text="Call Info" />
                            </div>
                            <div class="Content" style="height: 50px;">
                                <asp:Panel ID="pnlCallEntryInfo" runat="server">
                                    <div>
                                        <div class="filter">
                                            <div class="title LongText">
                                                <asp:Label ID="lblCallDate" runat="server" Text="* Call Date:"></asp:Label>
                                            </div>
                                            <div class="control">
                                                <uc1:DatePicker InvalidValueMessage="The Call Date format is invalid" ValidationGroup="CallEntry"
                                                    EmptyValueMessage="The Call Date field is required" DateTimeFormat="Default"
                                                    ID="dpCallDate" ShowOn="Both" runat="server" Value="01/01/2011" IsValidEmpty="false">
                                                </uc1:DatePicker>
                                            </div>
                                        </div>
                                        <div class="filter">
                                            <div class="title LongText">
                                                <asp:Label ID="lblCallTime" runat="server" Text="* Call Time:"></asp:Label>
                                            </div>
                                            <div class="control">
                                                <asp:TextBox ID="txtCallTime" runat="server" CssClass="input" Width="40px" Text="11:00"></asp:TextBox>
                                                <cc1:MaskedEditExtender ID="mskInitialTime" TargetControlID="txtCallTime" runat="server"
                                                    Mask="99:99" MaskType="Time" AcceptAMPM="false" UserTimeFormat="TwentyFourHour"
                                                    AutoComplete="true">
                                                </cc1:MaskedEditExtender>
                                                <cc1:MaskedEditValidator ID="rfvTimeValidator" runat="server" ControlExtender="mskInitialTime"
                                                    ControlToValidate="txtCallTime" IsValidEmpty="false" EnableClientScript="true"
                                                    Display="Dynamic" InvalidValueBlurredMessage="*" InvalidValueMessage="The Call Time format is invalid"
                                                    EmptyValueMessage="The Call Time field is required" EmptyValueBlurredText="*"
                                                    ValidationGroup="CallEntry">
                                                </cc1:MaskedEditValidator>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                        <div style="width: 60%; float: right;">
                            <div class="Header">
                                <asp:Label ID="lblCallTypeInfo" runat="server" Text="Call Type Info" />
                            </div>
                            <div class="Content" style="height: 50px;">
                                <asp:Panel ID="phCallType" runat="server">
                                    <div class="filter">
                                        <div class="title LongText">
                                            <asp:Label ID="lblCallType" runat="server" Text="* Select Call Type:"></asp:Label>
                                        </div>
                                        <div class="control">
                                            <asp:DropDownList ID="ddlCallType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCallType_SelectedIndexChanged"
                                                ValidationGroup="CallEntry" />
                                            <asp:RequiredFieldValidator ID="rfvCallType" runat="server" ControlToValidate="ddlCallType"
                                                Display="Dynamic" ErrorMessage="The Call Type field is required" Text="*" SetFocusOnError="true"
                                                ValidationGroup="CallEntry" InitialValue="0" />
                                        </div>
                                    </div>
                                    <%--<div class="filter" style="display:none">
                                        <div class="title LongText">
                                            <asp:Label ID="lblPrimaryCallType" runat="server" Text="* Select Primary Call Type:"></asp:Label>
                                        </div>
                                        <div class="control">
                                            <asp:DropDownList ID="cbPrimaryCallType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbPrimaryCallType_SelectedIndexChanged"
                                                ValidationGroup="CallEntry" />
                                            <asp:RequiredFieldValidator ID="rfvPrimaryCallType" runat="server" ControlToValidate="cbPrimaryCallType"
                                                Display="Dynamic" ErrorMessage="The Primary Call Type field is required" Text="*"
                                                SetFocusOnError="true" ValidationGroup="CallEntry" />
                                        </div>
                                    </div>--%>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </asp:PlaceHolder>
                <br />
                <asp:Panel ID="pnlFields" runat="server" Visible="false">
                    <div class="Header">
                        <asp:Label ID="lblCallDetails" runat="server" Text="Call Details" />
                    </div>
                    <div class="Content">
                        <asp:Panel ID="pnlInitialAdvise" runat="server" Visible="false">
                            <asp:Label ID="lblInitialAdviseTitle" runat="server" Font-Size="16px" Text="Select Person(s) To Advise or Track:"></asp:Label>
                            <div class="gridview">
                                <asp:ScrollableGridView ID="gvInitialAdvise" runat="server" CssClass="ScrollableGridView"
                                    SaveScrollPosition="true" AutoGenerateColumns="false" ShowFooter="false" UpdateMode="Conditional"
                                    OnRowDataBound="gvInitialAdvise_RowDataBound" NonSorteableCells="4" MinWidth="600">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Div # / Company">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDivCust" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Person">
                                            <ItemTemplate>
                                                <asp:Label ID="lblName" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Adivise Note">
                                            <ItemTemplate>
                                                <div style="max-height: 150px; overflow-x: hidden; overflow-y: auto;">
                                                    <asp:Label ID="lblAdiviseNote" runat="server"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contact Info">
                                            <ItemTemplate>
                                                <asp:Label ID="lblContactInfo" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Notes/Comments">
                                            <ItemTemplate>
                                                <asp:CountableTextBox ID="txtNotes" runat="server" TextMode="MultiLine" Width="150px"
                                                    Height="150px" CssClass="input" MaxChars="255" MaxCharsWarning="255" MaxLength="255"></asp:CountableTextBox>
                                            </ItemTemplate>
                                            <ItemStyle Width="150px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAllPerson" runat="server" ClientIDMode="Static" onclick="selectAllPersonInitialAdvise(this);" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hfPersonSelect" runat="server" />
                                                <asp:HiddenField ID="hfPersonType" runat="server" />
                                                <asp:CheckBoxList ID="cbMethodOfContact" runat="server" CssClass="selectAllInitialAdvise">
                                                    <asp:ListItem Text="In Person" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Voicemail" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Email" Value="3" Selected="false" Enabled="true"></asp:ListItem>
                                                </asp:CheckBoxList>
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:ScrollableGridView>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="phPersonsAdvise" runat="server" Visible="false">
                            <asp:Panel ID="phAddPerson" runat="server">
                                <div style="display: inline-block;">
                                    <div style="text-align: left; float: left;">
                                        <asp:Label ID="lblAdviceOrTrack" runat="server" Font-Size="16px" Text="Select Person(s):"></asp:Label>
                                    </div>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblSearchBy" runat="server" Text="Search By:"></asp:Label>
                                        <asp:DropDownList ID="ddlFilterContacts" runat="server">
                                            <asp:ListItem Text=" - Select One -" Value="0" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Company Contact" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Hulcher Contact" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtFilteredEmployeeCustomerName" CssClass="input" runat="server"></asp:TextBox>
                                        <asp:Button ID="btnFilterPesonsAdvise" runat="server" CssClass="btn" Text="Find"
                                            OnClick="btnFilterPesonsAdvise_OnClick" />
                                    </div>
                                </div>
                                <div class="gridview">
                                    <asp:ScrollableGridView ID="gvPersonalAdvise" runat="server" CssClass="ScrollableGridView"
                                        SaveScrollPosition="true" AutoGenerateColumns="false" ShowFooter="false" UpdateMode="Conditional"
                                        OnRowDataBound="gvPersonalAdvise_RowDataBound" NonSorteableCells="3" MinWidth="600">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Div # / Company">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDivNum" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="30px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Person">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPerson" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="200px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Adivise Note">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAdivise" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="200px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contact Info">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblContactInfo" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="300px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAllPerson" runat="server" ClientIDMode="Static" onclick="selectAllPerson(this);" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkPersonSelect" runat="server" CssClass="selectAllPerson" name="chkPerson"
                                                        onclick="deselectMainPerson(this);" />
                                                    <asp:HiddenField ID="hfPersonSelect" runat="server" />
                                                    <asp:HiddenField ID="hfPersonType" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle Width="25px" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:ScrollableGridView>
                                </div>
                                <div style="text-align: right;">
                                    <asp:Button ID="btnAddPersons" CausesValidation="false" CssClass="btn" runat="server"
                                        OnClick="btnAddPersons_Click" Text="Add" />
                                    <asp:Button ID="btnResetPersons" CausesValidation="false" CssClass="btn" runat="server"
                                        OnClick="btnResetPersons_Click" Text="Reset" />
                                </div>
                            </asp:Panel>
                            <asp:Label ID="lblPersonSelected" runat="server" Font-Size="16px" Text="Person(s) Selected:"></asp:Label>
                            <div>
                                <asp:ScrollableGridView ID="gvPersonsShopingCart" runat="server" CssClass="ScrollableGridView"
                                    SaveScrollPosition="true" AutoGenerateColumns="false" ShowFooter="false" UpdateMode="Conditional"
                                    OnRowDataBound="gvPersonsShopingCart_RowDataBound" MinWidth="150" ViewStateMode="Enabled">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkPersonSelect" runat="server" />
                                                <asp:HiddenField ID="hfPersonSelect" runat="server" />
                                                <asp:HiddenField ID="hfPersonType" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="25px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Person">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPerson" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAllPerson" runat="server" ClientIDMode="Static" onclick="selectAllPersonInitialAdvise(this);" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBoxList ID="cbMethodOfContact" runat="server" CssClass="selectAllInitialAdvise">
                                                    <asp:ListItem Text="In Person" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Voicemail" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Email" Value="3" Selected="false" Enabled="true"></asp:ListItem>
                                                </asp:CheckBoxList>
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:ScrollableGridView>
                            </div>
                            <div style="text-align: right;">
                                <asp:Button ID="btnRemovePersonsShopingCart" CausesValidation="false" CssClass="btn"
                                    runat="server" OnClick="btnRemovePersonsShopingCart_Click" Text="Remove" />
                            </div>
                            <hr width="100%" />
                        </asp:Panel>
                        <asp:Panel ID="phResourceAssigned" runat="server" Visible="false">
                            <div class="titleGridFilter">
                                <asp:Label ID="lblResourceAssigned" runat="server" Font-Size="16px" Text="Select Resource(s) Assigned to Job:"></asp:Label>
                            </div>
                            <div class="gridFilter">
                                <asp:Label ID="lblFilterResource" runat="server" Text="Filter Listing by:"></asp:Label>
                                <asp:DropDownList ID="ddlFilterResource" runat="server" Width="150px" DataValueField="Value"
                                    DataTextField="Text">
                                    <asp:ListItem Text="- Select One -" Value="" Selected="True" />
                                </asp:DropDownList>
                                <asp:TextBox ID="txtFilterResource" runat="server" Width="150px" CssClass="input" />
                                <asp:Button ID="btnFilterResource" runat="server" Text="Find" CssClass="btn" CausesValidation="false"
                                    OnClick="btnFilterResource_Click" />
                            </div>
                            <div class="control">
                                <asp:ScrollableGridView ID="gvResource" runat="server" CssClass="ScrollableGridView"
                                    AutoGenerateColumns="false" ShowFooter="false" SkinID="NonSortable" OnRowDataBound="gvResource_RowDataBound"
                                    EmptyDataText="The List is Empty" MinWidth="600">
                                    <Columns>
                                        <asp:TemplateField HeaderText="CallTypeID" Visible="false">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hfCallTypeID" Value='<%# Eval("CallTypeID") %>' runat="server">
                                                </asp:HiddenField>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="JobID" Visible="false">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hfJobID" Value='<%# Eval("JobID") %>' runat="server"></asp:HiddenField>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Div #">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDivNum" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit # / Combo #">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUnitNumber" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="65px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Resource">
                                            <ItemTemplate>
                                                <asp:Label ID="lblResource" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Call Entry">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hlLastCallEntry" runat="server" Text='<%# Eval("CallTypeDescription") %>'></asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle Width="70px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Call Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCallDate" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="70px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Call Time">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCallTime" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Modified By">
                                            <ItemTemplate>
                                                <asp:Label ID="lblModified" runat="server" Text='<%# Eval("ModifiedBy") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="80px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAllResource" runat="server" ClientIDMode="Static" onclick="selectAllResource();"
                                                    Visible="true" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkResourceSelect" runat="server" CssClass="selectAllResource"
                                                    onclick="deselectMainResource(this);" />
                                                <asp:HiddenField ID="hfResourceSelect" runat="server" Value='<%# Eval("ResourceID") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="25px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:ScrollableGridView>
                                <asp:TextBox ID="txtValidateResources" runat="server" Style="display: none"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvValidateResources" runat="server" ControlToValidate="txtValidateResources"
                                    ErrorMessage="Resources - Select at least one resource" Text="*" ValidationGroup="CallEntry"
                                    Enabled="false"></asp:RequiredFieldValidator>
                                <div style="text-align: right;">
                                    <asp:Button ID="btnAddResource" runat="server" CssClass="btn" Text="Add Resources"
                                        CausesValidation="false" UseSubmitBehavior="false" />
                                    <asp:Button ID="btnResetResource" runat="server" CssClass="btn" Text="Reset" CausesValidation="false"
                                        OnClick="btnResetResource_Click" />
                                </div>
                                <hr width="100%" />
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlEquipmentType" runat="server" Visible="false">
                            <eq:EquipmentRequested ID="uscEqRequested" runat="server" />
                        </asp:Panel>
                        <asp:Panel ID="pnlResourceReadOnly" runat="server" Visible="false">
                            <div class="control">
                                <asp:ScrollableGridView ID="gvResourceReadOnly" runat="server" CssClass="ScrollableGridView"
                                    AutoGenerateColumns="false" ShowFooter="false" SkinID="NonSortable" OnRowDataBound="gvResourceReadOnly_RowDataBound"
                                    EmptyDataText="The List is Empty" MinWidth="600">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Div #">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDivNum" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit # / Combo #">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUnitNumber" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="65px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Resource">
                                            <ItemTemplate>
                                                <asp:Label ID="lblResource" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:ScrollableGridView>
                                <hr width="100%" />
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlDynamic" runat="server" Visible="false">
                            <asp:PlaceHolder ID="phDynamic" runat="server"></asp:PlaceHolder>
                            <div class="checkbox" style="width: 100%; text-align: center;">
                                <asp:CheckBox ID="ckbShiftTransferLog" runat="server" Text="Copy to Shift Transfer Log" />
                                <asp:CheckBox ID="ckbCopyGeneralLog" runat="server" onClick="CheckedChanged();" Text="Copy to General Log" />
                            </div>
                            <hr width="100%" />
                        </asp:Panel>
                    </div>
                </asp:Panel>
                <div class="bottom">
                    <div class="buttons">
                        <asp:TextBox ID="txtParentUpdate" runat="server" Style="visibility: hidden" OnTextChanged="btnParentUpdate_Click" />
                        <asp:Button ID="btnSaveContinue" runat="server" CssClass="btn" Text="Save & Continue"
                            OnClientClick="doBeforeUnload();" OnClick="btnSaveContinue_Click" ValidationGroup="CallEntry" />
                        <asp:Button ID="btnSaveClose" runat="server" CssClass="btn" Text="Save & Close" OnClick="btnSaveAndClose_OnClick"
                            ValidationGroup="CallEntry" />
                        <asp:Button ID="btnClose" runat="server" CssClass="btn" Text="Cancel" OnClientClick="CheckIsHistoryUpdate();window.close();return false;"
                            UseSubmitBehavior="false" />
                    </div>
                </div>
                <asp:Panel ID="pnlCallLog" runat="server" Visible="false">
                    <br />
                    <div class="Header">
                        <asp:Label ID="lblHeader" runat="server" Text="Call Log History"></asp:Label>
                    </div>
                    <div class="Content">
                        <div style="max-height: 200px; overflow-y: auto; overflow-x: hidden; text-align: left;">
                            <asp:Repeater ID="rptCallLogHistory" runat="server" OnItemDataBound="rptCallLogHistory_ItemDataBound">
                                <ItemTemplate>
                                    <div class="historyTitle">
                                        <asp:Label ID="lblCallTitle" runat="server" Font-Bold="true" Font-Underline="true" />&nbsp;
                                        <asp:LinkButton ID="hlUpdate" runat="server" Text="Update" CausesValidation="false"></asp:LinkButton>
                                    </div>
                                    &nbsp;&nbsp;<asp:Label ID="lblNote" runat="server"></asp:Label>
                                    <asp:Repeater ID="rptResources" runat="server" Visible="false">
                                        <HeaderTemplate>
                                            <div style='width: 100%; display: inline-block;'>
                                                <div style='text-align: right; width: 30%; height: 100%; display: inline-block; float: left;'>
                                                    <b>Person(s) Advised:</b>
                                                </div>
                                                <div style='text-align: left; width: 68%; height: 100%; display: inline-block; float: left;'>
                                                    <table border="0" cellspacing="0" cellpadding="2" width="100%">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="vertical-align: top; width: 30%;">
                                                    <asp:Label ID="lblName" runat="server" />
                                                </td>
                                                <td style="vertical-align: top; width: 30%;">
                                                    <asp:Label ID="lblDetails" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; width: 30%;" colspan="1">
                                                    <asp:Label ID="lblAdviseNote" runat="server" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table> </div> </div>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </ItemTemplate>
                                <SeparatorTemplate>
                                    <hr />
                                </SeparatorTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlCallLogInitialAdvise" runat="server" Visible="false">
                    <br />
                    <div class="Header">
                        <asp:Label ID="lblHeader2" runat="server" Text="Call Log History - Initial Advise"></asp:Label>
                    </div>
                    <div class="Content">
                        <div class="control" style="max-height: 200px; overflow-y: auto; overflow-x: hidden;
                            text-align: left;">
                            <asp:Repeater ID="rptCallLogHistoryInitialAdvise" runat="server" OnItemDataBound="rptCallLogHistoryInitialAdvise_ItemDataBound">
                                <ItemTemplate>
                                    <div id="divDetails" runat="server">
                                        <table border="0" cellspacing="0" cellpadding="2" width="100%">
                                            <asp:Repeater ID="rptResources" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="vertical-align: top; width: 20%;">
                                                            <asp:Label ID="lblName" runat="server" />
                                                        </td>
                                                        <td style="vertical-align: top; width: 20%;">
                                                            <asp:Label ID="lblDetails" runat="server" />
                                                        </td>
                                                        <td style="vertical-align: top; width: 60%;">
                                                            <asp:Label ID="lblAdviseNote" runat="server" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                        <hr />
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </asp:Panel>
                <script type="text/javascript" src="/Scripts/jquery.multiselect.min.js"></script>
                <script type="text/javascript" language="javascript">
                    var isCleaningEmployee = false;
                    var isCleaningCustomer = false;
                    var isCleaningExternal = false;
                    var isEndRequestCustomer = false;
                    var isEndRequestEmployee = false;
                    function ChangeCalledInValidationEmployee(WebServiceResult) {
                        var checkbox = document.getElementById('<%= chkUserCall.ClientID %>');

                        if (WebServiceResult != "0") {
                            checkbox.checked = false;
                            ChangeCalledInValidators(true, false, false);
                        }
                        else if (!isCleaningEmployee)
                            ChangeCalledInValidators(true, true, true);

                        isCleaningEmployee = false;

                        if (!isEndRequestEmployee) {
                            if (document.getElementById('<%=ddlCallType.ClientID %>').value == '6')
                                __doPostBack('<%= btnFakeEmployeeChange.UniqueID %>', '');
                        }
                        else
                            isEndRequestEmployee = false;
                    }

                    function ChangeCalledInValidationCustomer(WebServiceResult) {
                        var checkbox = document.getElementById('<%= chkUserCall.ClientID %>');

                        if (WebServiceResult != "0") {
                            checkbox.checked = false;
                            ChangeCalledInValidators(false, true, false);
                        }
                        else if (!isCleaningCustomer)
                            ChangeCalledInValidators(true, true, true);

                        isCleaningCustomer = false;

                        if (WebServiceResult != "0" && document.getElementById("<%= hfGeneralLog.ClientID %>").value == "GeneralLog")
                            CallCustomerWebService(WebServiceResult);

                        if (!isEndRequestCustomer) {
                            if (document.getElementById('<%=ddlCallType.ClientID %>').value == '6')
                                __doPostBack('<%= btnFakeCustomerChange.UniqueID %>', '');
                        }
                        else
                            isEndRequestCustomer = false;
                    }

                    function ChangeCalledInValidationExternal(control) {
                        var checkbox = document.getElementById('<%= chkUserCall.ClientID %>');

                        if (control.value != "") {
                            checkbox.checked = false;
                            ChangeCalledInValidators(false, false, true);
                        }
                        else if (!isCleaningExternal)
                            ChangeCalledInValidators(true, true, true);
                    }

                    function ChangeCalledInValidationUser(checkbox) {
                        if (checkbox.checked)
                            ChangeCalledInValidators(false, false, false);
                        else
                            ChangeCalledInValidators(true, true, true);
                    }

                    function ChangeCalledInValidators(enableEmployee, enableCustomer, enableExternal) {
                        ValidatorEnable($get('<%= actCalledInEmployee.RequiredFieldClientId %>'), enableEmployee);
                        ValidatorEnable($get('<%= actCalledInCustomer.RequiredFieldClientId %>'), enableCustomer);
                        ValidatorEnable($get('<%= rfvCalledInExternal.ClientID %>'), enableExternal);

                        if (!enableEmployee) {
                            isCleaningEmployee = true;
                            $find('actCalledInEmployee').raiseItemSelected(new Sys.Extended.UI.AutoCompleteItemEventArgs(null, "", "0"));
                        }
                        else
                            isEndRequestEmployee = false;

                        if (!enableCustomer) {
                            isCleaningCustomer = true;
                            $find('actCalledInCustomer').raiseItemSelected(new Sys.Extended.UI.AutoCompleteItemEventArgs(null, "", "0"));
                        }
                        else
                            isEndRequestCustomer = false;
                        if (!enableExternal) {
                            isCleaningExternal = true;
                            document.getElementById('<%= txtCalledInExternal.ClientID %>').value = "";
                        }

                        $('#<%=hfValidatorEmployeeStatus.ClientID %>').val(enableEmployee);
                        $('#<%=hfValidatiorCustomerStatus.ClientID %>').val(enableCustomer);
                        $('#<%=hfValidatorExternalStatus.ClientID %>').val(enableExternal);
                    }

                    var scriptManager = Sys.WebForms.PageRequestManager.getInstance();
                    scriptManager.add_endRequest(function () {
                        isEndRequestEmployee = true;
                        isEndRequestCustomer = true;
                        if ($get('<%= actCalledInEmployee.RequiredFieldClientId %>') &&
                            $get('<%= actCalledInCustomer.RequiredFieldClientId %>') &&
                            $get('<%= rfvCalledInExternal.ClientID %>')) {
                            var enableEmployee = true;
                            var enableCustomer = true;
                            var enableExternal = true;
                            if ($get('<%= actCalledInEmployee.RequiredFieldClientId %>')) {
                                if ($('#<%=hfValidatorEmployeeStatus.ClientID %>').val() == "false")
                                    enableEmployee = false;
                            }
                            if ($get('<%= actCalledInCustomer.RequiredFieldClientId %>')) {
                                if ($('#<%=hfValidatiorCustomerStatus.ClientID %>').val() == "false")
                                    enableCustomer = false;
                            }
                            if ($get('<%= rfvCalledInExternal.ClientID %>')) {
                                if ($('#<%=hfValidatorExternalStatus.ClientID %>').val() == "false")
                                    enableExternal = false;
                            }

                            ChangeCalledInValidators(enableEmployee, enableCustomer, enableExternal);
                        }
                    });

                    function CheckedChanged() {
                        if (document.getElementById("<%=ckbCopyGeneralLog.ClientID %>").checked)
                            document.getElementById("<%=hfSave.ClientID %>").value = "1";
                        else
                            document.getElementById("<%=hfSave.ClientID %>").value = "0";
                    }


                    if (window.body)
                    // IE
                        window.body.onbeforeunload = CheckIsHistoryUpdate;
                    else
                    // FX
                        window.onbeforeunload = CheckIsHistoryUpdate;

                    function CheckIsHistoryUpdate() {
                        if (document.getElementById("<%=hfIsHistoryUpdate.ClientID %>").value == 'True') {
                            if (window.opener != null)
                                window.opener.__doPostBack(document.getElementById("<%=hfRefreshUniqueID.ClientID %>").value, '');
                        }
                    }

                    function CheckEmail() {
                        if (document.getElementById("<%=hfIsHistoryUpdate.ClientID %>").value == 'False') {
                            if (document.getElementById("<%=hfEmail.ClientID %>").value != "") {
                                if (confirm("Do you want to send the email to the matching users of the Call Criteria?"))
                                    var emailWindow = window.open('/Email.aspx?CallLogListID=' + document.getElementById("<%=hfEmail.ClientID %>").value, '', 'width=800, height=600, scrollbars=1, resizable=yes');
                                document.getElementById("<%=hfEmail.ClientID %>").value = "";
                            }
                        }
                        else if (document.getElementById("<%=hfIsHistoryUpdate.ClientID %>").value == 'True') {
                            if (window.opener != null)
                                window.opener.__doPostBack(document.getElementById("<%=hfRefreshUniqueID.ClientID %>").value, '');
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

                    }

                    function SetAutocompleteField(Name, ID, Field) {
                        var actField = $find(Field);
                        actField.raiseItemSelected(new Sys.Extended.UI.AutoCompleteItemEventArgs(null, Name, ID));
                    }

                    function selectAllResource() {
                        var chkBoxPerson = document.getElementById("chkAllResource");
                        var grid = document.getElementById('<%= gvResource.ClientID %>');

                        for (i = 1; i < grid.rows.length; i++) {
                            grid.rows[i].cells[7].getElementsByTagName("INPUT")[0].checked = chkBoxPerson.checked;
                        }

                        if (chkBoxPerson.checked)
                            document.getElementById('<%= txtValidateResources.ClientID %>').value = "1";
                        else
                            document.getElementById('<%= txtValidateResources.ClientID %>').value = '';
                    }

                    function validateResources() {
                        var resources = $(".selectAllResource :checked");

                        if (resources.length == 0)
                            document.getElementById('<%= txtValidateResources.ClientID %>').value = '';
                        else
                            document.getElementById('<%= txtValidateResources.ClientID %>').value = "1";
                    }

                    function selectAllPerson(chkBoxPerson) {
                        var grid = document.getElementById('<%= gvPersonalAdvise.ClientID %>');

                        for (i = 1; i < grid.rows.length; i++) {
                            grid.rows[i].cells[3].getElementsByTagName("INPUT")[0].checked = chkBoxPerson.checked;
                        }
                    }

                    function selectAllPersonInitialAdvise(chkBoxPerson) {
                        $('.selectAllInitialAdvise input:checkbox').each(function () {
                            if (!this.disabled)
                                this.checked = chkBoxPerson.checked;
                        });
                    }

                    function deselectMainResource(object) {
                        var chkBoxResource = document.getElementById("chkAllResource");

                        if (!$(object).checked) {
                            chkBoxResource.checked = false;
                        }
                    }

                    function deselectMainPerson(object) {
                        var chkBoxPerson = document.getElementById("chkAllPerson");

                        if (!$(object).checked) {
                            chkBoxPerson.checked = false;
                        }
                    }


                </script>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
