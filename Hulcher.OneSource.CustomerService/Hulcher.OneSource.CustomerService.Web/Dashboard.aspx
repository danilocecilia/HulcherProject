<%@ Page Title="" Language="C#" MasterPageFile="~/DefaultPage.master" AutoEventWireup="true"
    CodeBehind="Dashboard.aspx.cs" Inherits="Hulcher.OneSource.CustomerService.Web.Dashboard" %>

<%@ MasterType TypeName="Hulcher.OneSource.CustomerService.Web.DefaultPage" %>
<%@ Register Src="UserControls/DatePicker.ascx" TagName="DatePicker" TagPrefix="asp" %>
<%@ Register Assembly="Hulcher.OneSource.CustomerService.Business" Namespace="Hulcher.OneSource.CustomerService.Business.WebControls.ServerControls"
    TagPrefix="asp" %>
<%@ Register Src="~/UserControls/AutoCompleteTextbox.ascx" TagName="AutoCompleteTextbox"
    TagPrefix="uc1" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
    <link href="../../Styles/DashBoard.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Forms.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ContentPlaceHolderID="Content" ID="Content1" ContentPanelID="Content"
    runat="server">
    <div id="divTotal">
        <asp:UpdatePanel ID="upViewSummary" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="tmrUpdate" EventName="Tick" />
                <asp:AsyncPostBackTrigger ControlID="rbSummary" EventName="SelectedIndexChanged" />
                <asp:PostBackTrigger ControlID="btnExport" />
            </Triggers>
            <ContentTemplate>
                <asp:Timer ID="tmrUpdate" runat="server" OnTick="tmrUpdate_Tick" />
                <asp:HiddenField ID="hfExpandedJobs" runat="server" />
                <asp:HiddenField ID="hfReadCallLogs" runat="server" />
                <asp:HiddenField ID="hfDivisionCount" runat="server" />
                <asp:HiddenField ID="hfJobCount" runat="server" />
                <asp:HiddenField ID="hfCallLogCount" runat="server" />
                <asp:HiddenField ID="hfOrderBy" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfSelectedCallLog" runat="server" />
                <asp:HiddenField ID="hfJobId" runat="server" />
                <asp:HiddenField ID="hfCallId" runat="server" />
                <asp:HiddenField ID="hfToggleFilterStatus" runat="server" />
                <div id="divMain">
                    <div style="width: 100%;">
                        <div id="divSummary">
                            <div id="divSummaryChoice" class="divSummaryChoice">
                                <asp:Label ID="lblSummary" Text="Select Your Dashboard View Option:" runat="server"
                                    Style="font-size: 12pt; margin-right: 10px;"></asp:Label>
                                <asp:RadioButtonList ID="rbSummary" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" OnSelectedIndexChanged="rbSummary_SelectedIndexChanged" class="controls">
                                    <asp:ListItem Text="Job Call Log View" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Job Summary View" Value="2"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div id="divContentCounter" class="divContentCounter">
                            <div id="divCounter" class="divCounter">
                            </div>
                            <div class="toggleFilters toggleUp">
                                &nbsp;</div>
                        </div>
                    </div>
                    <div id="divAllFilters" style="text-align: center;">
                        <div id="divFilter" class="divFilter">
                            <%--<asp:Label ID="lblFilterViewBy" runat="server" Text="Filter View By: " class="labels"
                                Style="font-weight: bold;"></asp:Label>--%>
                            <asp:Panel ID="pnlJobSummaryView" runat="server" Visible="false" DefaultButton="btnFilterJobSummary">
                                <asp:ValidationSummary ID="vsJobSummary" runat="server" CssClass="errorbox" ValidationGroup="JobSummary"
                                    HeaderText="Please correct the following information" />
                                <div id="divUpper" style="display: inline-block;">
                                    <div style="float: left; margin-right: 5px;">
                                        <div class="controls">
                                            <asp:Label ID="lblJobStatusJobSummary" runat="server" Text="Job Status: " class="labels"></asp:Label>
                                            <uc1:AutoCompleteTextbox ID="actJobStatusJobSummary" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                                TextBoxWidth="120px" GridViewIdName="ID" MinimumPrefixLength="1" DisplayField=""
                                                AutoPostBack="false" RequiredField="false" WindowTitle="Dashboard - Find Job Status"
                                                ErrorMessage="Job Status field is required" AutoCompleteSource="JobStatus" ColumnHeaderList="Description"
                                                ColumnValueList="Description" ServiceMethod="GetJobStatusList" ValidationGroup="JobSummary"
                                                TextBoxCssClass="input" TextControlOnClientClickScript="setTimeout(function () { this.focus(); focusIn = true; }, 100);"
                                                TextControlOnFocusScript=" focusIn = true; " TextControlOnBlurScript=" focusIn = false; "
                                                ControlsToUpdate="actJobNumberJobSumary" />
                                        </div>
                                    </div>
                                    <div style="float: left; margin-right: 5px;">
                                        <div class="controls">
                                            <asp:Label ID="lblJobNumberJobSummary" runat="server" Text="Job #: " class="labels"></asp:Label>
                                            <uc1:AutoCompleteTextbox ID="actJobNumberJobSumary" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                                TextBoxWidth="120px" GridViewIdName="ID" DisplayField="" AutoPostBack="false"
                                                RequiredField="false" WindowTitle="Dashboard - Find Job Number" ErrorMessage="Job Number field is required"
                                                AutoCompleteSource="JobNumberByStatus" ColumnHeaderList="Number,Company,Location"
                                                ColumnValueList="PrefixedNumber,CS_CustomerInfo.CS_Customer.Name,CS_LocationInfo.FullLocation"
                                                ServiceMethod="GetJobNumberList" ValidationGroup="JobSummary" TextBoxCssClass="input"
                                                TextControlOnClientClickScript="setTimeout(function () { this.focus(); focusIn = true; }, 100);"
                                                TextControlOnFocusScript=" focusIn = true; " TextControlOnBlurScript=" focusIn = false; "
                                                ContextKey="1" />
                                        </div>
                                    </div>
                                    <div style="float: left; margin-right: 5px;">
                                        <div class="controls">
                                            <asp:Label ID="lblDivisionJobSummary" runat="server" Text="Division: " class="labels"></asp:Label>
                                            <uc1:AutoCompleteTextbox ID="actDivisionJobSummary" runat="server" ServiceMethod="GetDivisionList"
                                                TextBoxWidth="120px" GridViewButtonImageUrl="~/Images/money.png" GridViewIdName="ID"
                                                DisplayField="Division" FilterId="0" ContextKey="0" AutoPostBack="false" RequiredField="false"
                                                WindowTitle="Dashboard - Find Division" ErrorMessage="Primary Division field is required"
                                                AutoCompleteSource="Division" ColumnHeaderList="Name" TextBoxCssClass="input"
                                                ColumnValueList="ExtendedDivisionName" ValidationGroup="JobSummary" TextControlOnClientClickScript="setTimeout(function () { this.focus(); focusIn = true; }, 100);"
                                                TextControlOnFocusScript=" focusIn = true; " TextControlOnBlurScript=" focusIn = false; " />
                                        </div>
                                    </div>
                                    <div style="float: left">
                                        <div class="controls">
                                            <asp:Label ID="lblCustomerJobSummary" runat="server" Text="Company: " class="labels"></asp:Label>
                                            <uc1:AutoCompleteTextbox ID="actCustomerJobSummary" runat="server" ServiceMethod="GetCustomerList"
                                                GridViewButtonImageUrl="~/Images/money.png" GridViewIdName="ID" DisplayField="Name"
                                                TextBoxWidth="120px" AutoPostBack="false" RequiredField="false" ValidationGroup="JobSummary"
                                                ErrorMessage="The Company field is required" WindowTitle="Dashboard - Find Company"
                                                AutoCompleteSource="Customer" ColumnHeaderList="Name,Attn,Company ID" ColumnValueList="Name,Attn,CustomerNumber"
                                                TextControlOnClientClickScript="setTimeout(function () { this.focus(); focusIn = true; }, 100);"
                                                TextControlOnFocusScript=" focusIn = true; " TextControlOnBlurScript=" focusIn = false; " />
                                        </div>
                                    </div>
                                    <div style="float: left; margin-right: 5px;">
                                        <div class="controls">
                                            <asp:Label ID="lblJobPerson" runat="server" Text="Person: " class="labels"></asp:Label>
                                            <asp:TextBox ID="txtJobPerson" runat="server" Text="" CssClass="input" Width="120px"
                                                onfocus=" focusIn = true; " onblur=" focusIn = false; "></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div id="divBottom" style="display: block; margin-top: 10px;">
                                    <div style="float: left; margin-right: 22px;">
                                        <asp:Label ID="lblDateTypeJobSummary" runat="server" Text="Date Type: " class="labels"></asp:Label>
                                        <asp:DropDownList ID="cbbDateTypeJobSummary" runat="server" class="controls" Width="128px">
                                            <asp:ListItem Text="Initial Call Date" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Preset Date" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Job Start Date" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Modification Date" Value="4" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left; margin-right: 82px;">
                                        <asp:Label ID="lblBeginDateJobSummary" runat="server" Text="Begin: "></asp:Label>
                                        <asp:DatePicker ID="dpBeginDateJobSummary" InvalidValueMessage="Invalid date format"
                                            ValidationGroup="JobSummary" EmptyValueMessage="The Start Date field is required"
                                            DateTimeFormat="Default" ShowOn="Both" runat="server" IsValidEmpty="false" TextControlOnClientClickScript="setTimeout(function () { this.focus(); focusIn = true; }, 100);"
                                            TextControlOnFocusScript=" focusIn = true; " TextControlOnBlurScript=" focusIn = false; " />
                                    </div>
                                    <div style="float: left; margin-right: 63px;">
                                        <asp:Label ID="lblEndDateJobSummary" runat="server" Text="End: "></asp:Label>
                                        <asp:DatePicker ID="dpEndDateJobSummary" InvalidValueMessage="Invalid date format"
                                            ValidationGroup="JobSummary" EmptyValueMessage="The Start Date field is required"
                                            DateTimeFormat="Default" ShowOn="Both" runat="server" IsValidEmpty="false" TextControlOnClientClickScript="setTimeout(function () { this.focus(); focusIn = true; }, 100);"
                                            TextControlOnFocusScript=" focusIn = true; " TextControlOnBlurScript=" focusIn = false; " />
                                    </div>
                                    <div style="float: right; margin-right: 10px;">
                                        <div class="controls">
                                            <asp:Button ID="btnFilterJobSummary" runat="server" Text="Filter" class="btn" OnClick="btnFilterJobSummary_OnClick"
                                                ValidationGroup="JobSummary" />
                                            <asp:Button ID="btnResetFieldsJobSummary" runat="server" Text="Reset" class="btn"
                                                OnClick="btnResetFieldsJobSummary_OnClick" ValidationGroup="JobSummary" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlCallLogView" runat="server" Visible="false" DefaultButton="btnFilterCallLog">
                                <asp:ValidationSummary ID="vsJobCallLog" runat="server" CssClass="errorbox" ValidationGroup="JobCallLog"
                                    HeaderText="Please correct the following information" />
                                <div id="divUpperCallLog" style="display: inline-block; margin-top: 10px; width: 100%">
                                    <div style="display: block; float: left; width: 25%;">
                                        <div style="float: left; text-align: right; width: 30%; padding-top: 2px;">
                                            <asp:Label ID="lblJobStatusFilterCallLog" runat="server" Text="Job Status: " class="labels"></asp:Label>
                                        </div>
                                        <div style="float: right; text-align: left; width: 69%;">
                                            <uc1:AutoCompleteTextbox ID="actJobStatusCallLog" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                                TextBoxWidth="120px" GridViewIdName="ID" MinimumPrefixLength="1" DisplayField="Description"
                                                AutoPostBack="false" RequiredField="false" WindowTitle="Dashboard - Find Job Status"
                                                ErrorMessage="Job Status field is required" AutoCompleteSource="JobStatus" ColumnHeaderList="Description"
                                                ColumnValueList="Description" ServiceMethod="GetJobStatusList" ValidationGroup="JobSummary"
                                                TextBoxCssClass="input" TextControlOnClientClickScript="setTimeout(function () { this.focus(); focusIn = true; }, 100);"
                                                TextControlOnFocusScript=" focusIn = true; " TextControlOnBlurScript=" focusIn = false; " />
                                        </div>
                                    </div>
                                    <div style="display: block; float: left; width: 25%;">
                                        <div style="float: left; text-align: right; width: 30%; padding-top: 2px;">
                                            <asp:Label ID="lblCallTypeFilterCallLog" runat="server" Text="Call Type: " class="labels"></asp:Label>
                                        </div>
                                        <div style="float: right; text-align: left; width: 69%;">
                                            <uc1:AutoCompleteTextbox ID="actCallTypeCallLog" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                                TextBoxWidth="120px" GridViewIdName="ID" DisplayField="Description" AutoPostBack="false"
                                                RequiredField="false" WindowTitle="Dashboard - Find Call Type" ErrorMessage="Call Type field is required"
                                                AutoCompleteSource="CallType" ColumnHeaderList="Description" ColumnValueList="Description"
                                                ServiceMethod="GetCallTypeList" ValidationGroup="JobSummary" TextBoxCssClass="input"
                                                TextControlOnClientClickScript="setTimeout(function () { this.focus(); focusIn = true; }, 100);"
                                                TextControlOnFocusScript=" focusIn = true; " TextControlOnBlurScript=" focusIn = false; " />
                                        </div>
                                    </div>
                                    <div style="display: block; float: left; width: 25%;">
                                        <div style="float: left; text-align: right; width: 30%; padding-top: 2px;">
                                            <asp:Label ID="lblModifiedByFilterCallLog" runat="server" Text="Modified By: " class="labels"></asp:Label>
                                        </div>
                                        <div style="float: right; text-align: left; width: 69%;">
                                            <%--<uc1:AutoCompleteTextbox ID="actModifiedByFilterCallLog" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                            TextBoxWidth="120px" GridViewIdName="ID" DisplayField="Description" AutoPostBack="false"
                                            RequiredField="false" WindowTitle="Dashboard - Find Modified By" ErrorMessage="Modified By field is required"
                                            AutoCompleteSource="CallType" ColumnHeaderList="Description" ColumnValueList="Description"
                                            ServiceMethod="GetCallTypeList" ValidationGroup="JobSummary" TextBoxCssClass="input" />--%>
                                            <asp:TextBox ID="txtPlaceHolderModifiedBy" runat="server" Width="120px" onclick="setTimeout(function () { this.focus(); focusIn = true; }, 100);"
                                                onfocus=" focusIn = true; " onblur=" focusIn = false; "></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="display: block; float: left; width: 24%;">
                                        <div style="float: left; text-align: right; width: 30%; padding-top: 2px;">
                                            <asp:Label ID="lblDivisionCallLogView" runat="server" Text="Division: " class="labels"></asp:Label>
                                        </div>
                                        <div style="float: right; text-align: left; width: 69%;">
                                            <uc1:AutoCompleteTextbox ID="actDivisionCallLogView" runat="server" ServiceMethod="GetDivisionList"
                                                TextBoxWidth="120px" GridViewButtonImageUrl="~/Images/money.png" GridViewIdName="ID"
                                                DisplayField="Division" FilterId="0" ContextKey="0" AutoPostBack="false" RequiredField="false"
                                                WindowTitle="Dashboard - Find Division" ErrorMessage="Primary Division field is required"
                                                AutoCompleteSource="Division" ColumnHeaderList="Name,Description" TextBoxCssClass="input"
                                                ColumnValueList="Name,Description" ValidationGroup="" TextControlOnClientClickScript="setTimeout(function () { this.focus(); focusIn = true; }, 100);"
                                                TextControlOnFocusScript=" focusIn = true; " TextControlOnBlurScript=" focusIn = false; " />
                                        </div>
                                    </div>
                                </div>
                                <div id="divBottomCallLog" style="display: inline-block; margin-top: 10px; width: 100%">
                                    <div style="display: block; float: left; width: 25%;">
                                        <div style="float: left; text-align: right; width: 30%; padding-top: 2px;">
                                            <asp:Label ID="lblCallLogPerson" runat="server" Text="Person: "></asp:Label>
                                        </div>
                                        <div style="float: right; text-align: left; width: 69%;">
                                            <asp:TextBox ID="txtCallLogPerson" runat="server" Text="" CssClass="input" Width="120px"
                                                onfocus=" focusIn = true; " onblur=" focusIn = false; "></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="display: block; float: left; width: 25%;">
                                        <div style="float: left; text-align: right; width: 30%; padding-top: 2px;">
                                            <asp:Label ID="lblStartDateCallEntry" runat="server" Text="Start: "></asp:Label>
                                        </div>
                                        <div style="float: right; text-align: left; width: 69%;">
                                            <asp:DatePicker IsValidEmpty="false" InvalidValueMessage="Begin - Invalid date format"
                                                ValidationGroup="JobCallLog" EmptyValueMessage="The Start Date field is required"
                                                DateTimeFormat="Default" ID="dpStartDateCallEntry" ShowOn="Both" runat="server"
                                                TextControlOnClientClickScript="setTimeout(function () { this.focus(); focusIn = true; }, 100);"
                                                TextControlOnFocusScript=" focusIn = true; " TextControlOnBlurScript=" focusIn = false; ">
                                            </asp:DatePicker>
                                        </div>
                                    </div>
                                    <div style="display: block; float: left; width: 25%;">
                                        <div style="float: left; text-align: right; width: 30%; padding-top: 2px;">
                                            <asp:Label ID="lblEndDateCallEntry" runat="server" Text="End: "></asp:Label>
                                        </div>
                                        <div style="float: right; text-align: left; width: 69%;">
                                            <asp:DatePicker IsValidEmpty="false" InvalidValueMessage="End - Invalid date format"
                                                ValidationGroup="JobCallLog" EmptyValueMessage="The Start Date field is required"
                                                DateTimeFormat="Default" ID="dpEndDateCallEntry" ShowOn="Both" runat="server"
                                                TextControlOnClientClickScript="setTimeout(function () { this.focus(); focusIn = true; }, 100);"
                                                TextControlOnFocusScript=" focusIn = true; " TextControlOnBlurScript=" focusIn = false; ">
                                            </asp:DatePicker>
                                        </div>
                                    </div>
                                    <div style="display: block; float: left; width: 24%;">
                                        <div style="float: left; text-align: right; width: 30%; padding-top: 2px;">
                                        </div>
                                        <div style="float: right; text-align: left; width: 69%;">
                                        </div>
                                    </div>
                                </div>
                                <div style="display: inline-block; text-align: right; width: 100%;">
                                    <div style="float: right; display: block;">
                                        <div style="float: left; margin-right: 10px;">
                                            <div class="controls">
                                                <asp:CheckBox ID="chkGeneralLog" runat="server" Text="General Log" />
                                                <asp:CheckBox ID="chkShiftTransferLog" runat="server" Text="Shift Transfer Log" />
                                            </div>
                                        </div>
                                        <div style="float: left; margin-right: 10px;">
                                            <asp:Label ID="lblFilter" runat="server" Text="Filter By: "></asp:Label>
                                            <asp:DropDownList ID="ddlFilter" runat="server" onchange="SetView(this);">
                                                <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                                <asp:ListItem Text="Unread" Value="Unread"></asp:ListItem>
                                                <asp:ListItem Text="Read" Value="Read"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Label ID="lblUnreadCounterTitle" Text="Unread" CssClass="UnreadCounter" runat="server"></asp:Label>
                                            <asp:Label ID="lblUnreadCounter" CssClass="UnreadCounter" runat="server"></asp:Label>
                                        </div>
                                        <div style="float: left; margin-right: 10px;">
                                            <div class="controls">
                                                <asp:Button ID="btnFilterCallLog" runat="server" Text="Filter" CssClass="btn" OnClick="btnFilterCallLog_Click"
                                                    ValidationGroup="JobCallLog" />
                                                <asp:Button ID="btnResetFieldsCallLog" CssClass="btn" runat="server" Text="Reset"
                                                    OnClick="btnResetFieldsCallLog_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                    <div id="divGrids">
                        <asp:Panel ID="pnlGridJobSummary" runat="server" Visible="false">
                            <div id="divJobSum" runat="server">
                                <asp:Repeater ID="rptJobSummary" runat="server" OnItemDataBound="rptJobSummary_ItemDataBound">
                                    <HeaderTemplate>
                                        <div id="tbRepeaters_Group" class="ScrollableGridView_Group" style="width: 100%">
                                            <div id="tbRepeaters_HeaderDiv" class="ScrollableGridView_HeaderDiv" style="min-width: 400px;">
                                            </div>
                                            <div id="tbRepeaters_ScrollDiv" class="ScrollableGridView_ScrollDiv" style="max-height: 220px;
                                                min-width: 400px;">
                                                <table id="tbRepeaters" class="ScrollableGridView" cellspacing="1">
                                                    <thead>
                                                        <tr style="position: relative; top: expression(this.offsetParent.scrollTop -1); left: expression(this.offsetParent.style.left);">
                                                            <th id="thExpandCollapse" runat="server" class="header" style="border: 1px solid #E6EEEE;">
                                                                &nbsp;
                                                            </th>
                                                            <th id="thDivision" runat="server" class="header" style="border: 1px solid #E6EEEE;
                                                                margin: 0 20px 0 0" onclick="SetOrderBy('1', this);">
                                                                <asp:Label ID="lblHeaderDivision" runat="server" CssClass="MarginRight" Text="Division" />
                                                            </th>
                                                            <th id="thJobNumber" runat="server" class="header" style="border: 1px solid #E6EEEE;"
                                                                onclick="SetOrderBy('2', this);">
                                                                <asp:Label ID="lblHeaderJobNumber" runat="server" CssClass="MarginRight" Text="Job Number" />
                                                            </th>
                                                            <th id="thCustomerResource" runat="server" class="header" style="border: 1px solid #E6EEEE;"
                                                                onclick="SetOrderBy('3', this);">
                                                                <asp:Label ID="lblHeaderCustomerResources" runat="server" CssClass="MarginRight"
                                                                    Text="Company / Resources" />
                                                            </th>
                                                            <th id="thJobStatus" runat="server" class="header" style="border: 1px solid #E6EEEE;"
                                                                onclick="SetOrderBy('4', this);">
                                                                <asp:Label ID="lblHeaderStatus" runat="server" CssClass="MarginRight" Text="Status" />
                                                            </th>
                                                            <th id="thLocation" runat="server" class="header" style="border: 1px solid #E6EEEE;"
                                                                onclick="SetOrderBy('5', this);">
                                                                <asp:Label ID="lblHeaderLocation" runat="server" CssClass="MarginRight" Text="Location" />
                                                            </th>
                                                            <th id="thProjectManager" runat="server" class="header" style="border: 1px solid #E6EEEE;"
                                                                onclick="SetOrderBy('6', this);">
                                                                <asp:Label ID="lblHeaderProjectManager" runat="server" CssClass="MarginRight" Text="Project Manager" />
                                                            </th>
                                                            <th id="thModifiedBy" runat="server" class="header" style="border: 1px solid #E6EEEE;"
                                                                onclick="SetOrderBy('7', this);">
                                                                <asp:Label ID="lblHeaderModifiedBy" runat="server" CssClass="MarginRight" Text="Modified By" />
                                                            </th>
                                                            <th id="thLastModification" runat="server" class="header" style="border: 1px solid #E6EEEE;"
                                                                onclick="SetOrderBy('8', this);">
                                                                <asp:Label ID="lblHeaderLastModification" runat="server" CssClass="MarginRight" Text="Last Modification" />
                                                            </th>
                                                            <th id="thInitialCallDate" runat="server" class="header" style="border: 1px solid #E6EEEE;"
                                                                onclick="SetOrderBy('9', this);">
                                                                <asp:Label ID="lblHeaderInitialCallDate" runat="server" CssClass="MarginRight" Text="Initial Call Date" />
                                                            </th>
                                                            <th id="thPresetDate" runat="server" class="header" style="border: 1px solid #E6EEEE;"
                                                                onclick="SetOrderBy('10', this);">
                                                                <asp:Label ID="lblHeaderPreset" runat="server" CssClass="MarginRight" Text="Preset" />
                                                            </th>
                                                            <th id="thLastCallType" runat="server" class="header" style="border: 1px solid #E6EEEE;"
                                                                onclick="SetOrderBy('11', this);">
                                                                <asp:Label ID="lblHeaderLastCallType" runat="server" CssClass="MarginRight" Text="Last Call Type" />
                                                            </th>
                                                            <th id="thLastCallDate" runat="server" class="header" style="border: 1px solid #E6EEEE;"
                                                                onclick="SetOrderBy('12', this);">
                                                                <asp:Label ID="lblHeaderLastCallDateTime" runat="server" CssClass="MarginRight" Text="Last Call Date Time" />
                                                            </th>
                                                            <th id="thOpenJob" runat="server" class="header" style="border: 1px solid #E6EEEE;">
                                                                &nbsp;
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="even" id="trJob" runat="server">
                                            <td style="width: 15px;">
                                                <div class="Expand" id="divExpand" runat="server">
                                                </div>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDivision" runat="server"></asp:Label>
                                                <asp:HiddenField ID="hfIdJob" runat="server" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblJobNumber" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCustomerResource" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblLocation" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblProjectManager" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblModifiedBy" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblLastModification" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCallDate" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblPreset" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:HyperLink ID="hlLastCallEntry" runat="server"></asp:HyperLink>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblLastCallDate" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 40px;">
                                                <asp:HyperLink ID="hlOpenJob" runat="server" Text="Open"></asp:HyperLink>
                                            </td>
                                        </tr>
                                        <asp:Repeater ID="rptJobSummaryResources" runat="server" OnItemDataBound="rptJobSummaryResources_ItemDataBound">
                                            <ItemTemplate>
                                                <tr id="trResource" runat="server">
                                                    <td style="width: 15px;">
                                                        <div class="Expand" id="divExpand" runat="server">
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblDivision" runat="server"></asp:Label>
                                                        <asp:HiddenField ID="hfIdJob" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblJobNumber" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCustomerResource" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblLocation" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblProjectManager" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblModifiedBy" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblLastModification" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCallDate" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblPreset" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:HyperLink ID="hlLastCallEntry" runat="server"></asp:HyperLink>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblLastCallDate" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="width: 40px;">
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr class="odd" id="trJob" runat="server">
                                            <td style="width: 15px;">
                                                <div class="Expand" id="divExpand" runat="server">
                                                </div>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDivision" runat="server"></asp:Label>
                                                <asp:HiddenField ID="hfIdJob" runat="server" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblJobNumber" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCustomerResource" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblLocation" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblProjectManager" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblModifiedBy" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblLastModification" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCallDate" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblPreset" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:HyperLink ID="hlLastCallEntry" runat="server"></asp:HyperLink>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblLastCallDate" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 40px;">
                                                <asp:HyperLink ID="hlOpenJob" runat="server" Text="Open"></asp:HyperLink>
                                            </td>
                                        </tr>
                                        <asp:Repeater ID="rptJobSummaryResources" runat="server" OnItemDataBound="rptJobSummaryResources_ItemDataBound">
                                            <ItemTemplate>
                                                <tr id="trResource" runat="server">
                                                    <td style="width: 15px;">
                                                        <div class="Expand" id="divExpand" runat="server">
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblDivision" runat="server"></asp:Label>
                                                        <asp:HiddenField ID="hfIdJob" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblJobNumber" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCustomerResource" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblLocation" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblProjectManager" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblModifiedBy" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblLastModification" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCallDate" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblPreset" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:HyperLink ID="hlLastCallEntry" runat="server"></asp:HyperLink>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblLastCallDate" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="width: 40px;">
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </AlternatingItemTemplate>
                                    <FooterTemplate>
                                        </tbody> </table></div></div>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                            <asp:Panel ID="pnlNoRows" runat="server" Visible="false">
                                <div class="ScrollableGridView_Group" style="width: 100%">
                                    <div class="ScrollableGridView_HeaderDiv" style="min-width: 400px;">
                                    </div>
                                    <div class="ScrollableGridView_ScrollDiv" style="max-height: 220px; min-width: 400px;">
                                        <table id="tbRepeaters" class="ScrollableGridView" cellspacing="1">
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
                        </asp:Panel>
                        <asp:Panel ID="pnlGridCallLogSummary" runat="server" Visible="false">
                            <asp:Repeater ID="rptCallLogSummaryDivision" runat="server" OnItemDataBound="rptCallLogSummaryDivision_ItemDataBound">
                                <HeaderTemplate>
                                    <div id="tbRepeaters_Group" class="ScrollableGridView_Group" style="width: 100%">
                                        <div id="tbRepeaters_HeaderDiv" class="ScrollableGridView_HeaderDiv" style="min-width: 400px;">
                                        </div>
                                        <div id="tbRepeaters_ScrollDiv" class="ScrollableGridView_ScrollDiv" style="max-height: 220px;
                                            min-width: 400px;">
                                            <table id="tbRepeaters" class="ScrollableGridView" cellspacing="1">
                                                <thead>
                                                    <tr style="position: relative; top: expression(this.offsetParent.scrollTop -1); left: expression(this.offsetParent.style.left);">
                                                        <th id="thDivision" runat="server" class="header" style="border: 1px, solid, #E6EEEE;"
                                                            onclick="SetOrderBy('1', this);">
                                                            <asp:Label ID="rpt1Header1" CssClass="MarginRight" runat="server" Text="Div"></asp:Label>
                                                        </th>
                                                        <th id="thJobNumber" runat="server" class="header" onclick="SetOrderBy('2', this);">
                                                            <asp:Label ID="rpt1Header2" CssClass="MarginRight" runat="server" Text="Job#"></asp:Label>
                                                        </th>
                                                        <th id="thCustomer" runat="server" class="header" onclick="SetOrderBy('3', this);">
                                                            <asp:Label ID="rpt1Header3" CssClass="MarginRight" runat="server" Text="Company"></asp:Label>
                                                        </th>
                                                        <th id="thCallType" runat="server" class="header" onclick="SetOrderBy('4', this);">
                                                            <asp:Label ID="rpt1Header4" CssClass="MarginRight" runat="server" Text="Call Type"></asp:Label>
                                                        </th>
                                                        <th id="thCalledInBy" runat="server" class="header" onclick="SetOrderBy('5',this);">
                                                            <asp:Label ID="rpt1Header5" CssClass="MarginRight" runat="server" Text="Called In By"></asp:Label>
                                                        </th>
                                                        <th id="thCallDate" runat="server" class="header" onclick="SetOrderBy('6',this);">
                                                            <asp:Label ID="rpt1Header6" CssClass="MarginRight" runat="server" Text="Call Date"></asp:Label>
                                                        </th>
                                                        <th id="thCallTime" runat="server" class="header" onclick="SetOrderBy('7',this);">
                                                            <asp:Label ID="rpt1Header7" CssClass="MarginRight" runat="server" Text="Call Time"></asp:Label>
                                                        </th>
                                                        <th id="thModifiedBy" runat="server" class="header" onclick="SetOrderBy('8', this);">
                                                            <asp:Label ID="rpt1Header8" CssClass="MarginRight" runat="server" Text="Modified By"></asp:Label>
                                                        </th>
                                                        <th id="thDetails" runat="server" class="header" onclick="SetOrderBy('9', this);">
                                                            <asp:Label ID="rpt1Header9" CssClass="MarginRight" runat="server" Text="Details"></asp:Label>
                                                        </th>
                                                        <th id="thLinks" runat="server" class="header" style="cursor: default">
                                                            <asp:Label ID="rpt1Header10" CssClass="MarginRight" runat="server" Text=""></asp:Label>
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="evenDivision" id="trDivision" runat="server">
                                        <td colspan="10" runat="server" id="divisionColumn">
                                            <asp:Label Font-Bold="true" ID="lblDivision" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="rptCallLogSummaryJob" runat="server" OnItemDataBound="rptCallLogSummaryJob_ItemDataBound">
                                        <ItemTemplate>
                                            <tr class="evenCustomer jobCallLogTable Job" id="trJob" runat="server">
                                                <td id="colCustomer" runat="server">
                                                    &nbsp
                                                </td>
                                                <td colspan="9" id="colJobNumberCustomer" runat="server">
                                                    <asp:Label Font-Bold="true" ID="lblJobNumberCustomer" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <asp:Repeater ID="rptCallLogSummaryCallEntry" runat="server" OnItemDataBound="rptCallLogSummaryCallEntry_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr id="trCallEntry" runat="server">
                                                        <td runat="server" id="colDivision">
                                                        </td>
                                                        <td id="colJob" runat="server">
                                                            &nbsp
                                                        </td>
                                                        <td id="colCust" runat="server">
                                                            &nbsp
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCallType" runat="server" Text=""></asp:Label>
                                                            <input type="hidden" id="hidCallId" runat="server" name="hidCallId" />
                                                            <input type="hidden" id="hidCallLastModification" runat="server" name="hidCallLastModification" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCalledInBy" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCallDate" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCallTime" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblModifiedBy" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td id="pnlCell" runat="server">
                                                            <div style="max-height: 75px; overflow: hidden;">
                                                                <asp:Label ID="lblDetails" runat="server" Text=""></asp:Label>
                                                            </div>
                                                            <asp:Panel ID="pnlToolTip" runat="server" Style="background-color: #FFFFFF; border: 1px solid #000;
                                                                display: none; width: 400px; position: fixed; max-height: 300px; overflow-y: auto;
                                                                overflow-x: hidden;" CssClass="tooltip">
                                                                <asp:Label ID="lblTool" runat="server" />
                                                            </asp:Panel>
                                                        </td>
                                                        <td class="links">
                                                            <asp:HyperLink ID="hlUpdate" runat="server" Text="Update"></asp:HyperLink><br />
                                                            <asp:HyperLink ID="hlEmail" runat="server" Text="Email">
                                                                <asp:Panel ID="pnlToolTipEmail" runat="server" Style="background-color: #FFFFFF;
                                                                    border: 1px solid #000; display: none; width: 400px; position: fixed; max-height: 300px;
                                                                    overflow-y: auto; overflow-x: hidden;" CssClass="tooltip">
                                                                    <asp:Label ID="lblToolTipEmail" runat="server" />
                                                                </asp:Panel>
                                                            </asp:HyperLink><br />
                                                            <asp:HyperLink ID="hlMove" runat="server" Text="Move" Enabled="false"></asp:HyperLink><br />
                                                            <asp:Button ID="btnDelete" runat="server" CssClass="linkButtonStyle" Text="Delete"
                                                                OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure you want to delete this Call Entry?')">
                                                            </asp:Button><br />
                                                            <asp:CheckBox ID="chkEmail" CssClass="checkEmail" runat="server" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr id="trCallEntry" runat="server" class="odd">
                                                        <td runat="server" id="colDivision">
                                                            <input type="hidden" id="hidCallId" runat="server" name="hidCallId" />
                                                            <input type="hidden" id="hidCallLastModification" runat="server" name="hidCallLastModification" />
                                                        </td>
                                                        <td id="colJob" runat="server">
                                                            &nbsp
                                                        </td>
                                                        <td id="colCust" runat="server">
                                                            &nbsp
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCallType" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCalledInBy" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCallDate" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCallTime" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblModifiedBy" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td id="pnlCell" runat="server">
                                                            <div style="max-height: 75px; overflow: hidden;">
                                                                <asp:Label ID="lblDetails" runat="server" Text=""></asp:Label>
                                                            </div>
                                                            <asp:Panel ID="pnlToolTip" CssClass="tooltip" runat="server" Style="background-color: #FFFFFF;
                                                                border: 1px solid #000; display: none; width: 400px; position: fixed; max-height: 300px;
                                                                overflow-y: auto; overflow-x: hidden;">
                                                                <asp:Label ID="lblTool" runat="server" />
                                                            </asp:Panel>
                                                        </td>
                                                        <td class="links">
                                                            <asp:HyperLink ID="hlUpdate" runat="server" Text="Update"></asp:HyperLink><br />
                                                            <asp:HyperLink ID="hlEmail" runat="server" Text="Email">
                                                                <asp:Panel ID="pnlToolTipEmail" runat="server" Style="background-color: #FFFFFF;
                                                                    border: 1px solid #000; display: none; width: 400px; position: fixed; max-height: 300px;
                                                                    overflow-y: auto; overflow-x: hidden;" CssClass="tooltip">
                                                                    <asp:Label ID="lblToolTipEmail" runat="server" />
                                                                </asp:Panel>
                                                            </asp:HyperLink><br />
                                                            <asp:HyperLink ID="hlMove" runat="server" Text="Move" Enabled="false"></asp:HyperLink><br />
                                                            <asp:Button ID="btnDelete" CssClass="linkButtonStyle" runat="server" Text="Delete"
                                                                OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure you want to delete this Call Entry?')">
                                                            </asp:Button><br />
                                                            <asp:CheckBox ID="chkEmail" CssClass="checkEmail" runat="server" />
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:Repeater>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr class="oddDivision" id="trDivision" runat="server">
                                        <td colspan="10">
                                            <asp:Label Font-Bold="true" ID="lblDivision" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="rptCallLogSummaryJob" runat="server" OnItemDataBound="rptCallLogSummaryJob_ItemDataBound">
                                        <ItemTemplate>
                                            <tr class="oddCustomer" id="trJob" runat="server">
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td colspan="9">
                                                    <asp:Label Font-Bold="true" ID="lblJobNumberCustomer" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <asp:Repeater ID="rptCallLogSummaryCallEntry" runat="server" OnItemDataBound="rptCallLogSummaryCallEntry_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr id="trCallEntry" runat="server" class="odd">
                                                        <td runat="server" id="colDivision">
                                                            <input type="hidden" id="hidCallId" runat="server" name="hidCallId" />
                                                            <input type="hidden" id="hidCallLastModification" runat="server" name="hidCallLastModification" />
                                                        </td>
                                                        <td id="colJob" runat="server">
                                                            &nbsp
                                                        </td>
                                                        <td id="colCust" runat="server">
                                                            &nbsp
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCallType" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCalledInBy" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCallDate" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCallTime" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblModifiedBy" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td id="pnlCell" runat="server">
                                                            <div style="max-height: 75px; overflow: hidden;">
                                                                <asp:Label ID="lblDetails" runat="server" Text=""></asp:Label>
                                                            </div>
                                                            <asp:Panel ID="pnlToolTip" CssClass="tooltip" runat="server" Style="background-color: #FFFFFF;
                                                                border: 1px solid #000; display: none; width: 400px; position: fixed; max-height: 300px;
                                                                overflow-y: auto; overflow-x: hidden;">
                                                                <asp:Label ID="lblTool" runat="server" />
                                                            </asp:Panel>
                                                        </td>
                                                        <td class="links">
                                                            <asp:HyperLink ID="hlUpdate" runat="server" Text="Update"></asp:HyperLink><br />
                                                            <asp:HyperLink ID="hlEmail" runat="server" Text="Email">
                                                                <asp:Panel ID="pnlToolTipEmail" runat="server" Style="background-color: #FFFFFF;
                                                                    border: 1px solid #000; display: none; width: 400px; position: fixed; max-height: 300px;
                                                                    overflow-y: auto; overflow-x: hidden;" CssClass="tooltip">
                                                                    <asp:Label ID="lblToolTipEmail" runat="server" />
                                                                </asp:Panel>
                                                            </asp:HyperLink><br />
                                                            <asp:HyperLink ID="hlMove" runat="server" Text="Move" Enabled="false"></asp:HyperLink><br />
                                                            <asp:Button ID="btnDelete" CssClass="linkButtonStyle" runat="server" Text="Delete"
                                                                OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure you want to delete this Call Entry?')">
                                                            </asp:Button><br />
                                                            <asp:CheckBox ID="chkEmail" CssClass="checkEmail" runat="server" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr id="trCallEntry" runat="server">
                                                        <td runat="server" id="colDivision">
                                                        </td>
                                                        <td id="colJob" runat="server">
                                                            &nbsp
                                                        </td>
                                                        <td id="colCust" runat="server">
                                                            &nbsp
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCallType" runat="server" Text=""></asp:Label>
                                                            <input type="hidden" id="hidCallId" runat="server" name="hidCallId" />
                                                            <input type="hidden" id="hidCallLastModification" runat="server" name="hidCallLastModification" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCalledInBy" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCallDate" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCallTime" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblModifiedBy" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td id="pnlCell" runat="server">
                                                            <div style="max-height: 75px; overflow: hidden;">
                                                                <asp:Label ID="lblDetails" runat="server" Text=""></asp:Label>
                                                            </div>
                                                            <asp:Panel ID="pnlToolTip" runat="server" Style="background-color: #FFFFFF; border: 1px solid #000;
                                                                display: none; width: 400px; position: fixed; max-height: 300px; overflow-y: auto;
                                                                overflow-x: hidden;" CssClass="tooltip">
                                                                <asp:Label ID="lblTool" runat="server" />
                                                            </asp:Panel>
                                                        </td>
                                                        <td class="links">
                                                            <asp:HyperLink ID="hlUpdate" runat="server" Text="Update"></asp:HyperLink><br />
                                                            <asp:HyperLink ID="hlEmail" runat="server" Text="Email">
                                                                <asp:Panel ID="pnlToolTipEmail" runat="server" Style="background-color: #FFFFFF;
                                                                    border: 1px solid #000; display: none; width: 400px; position: fixed; max-height: 300px;
                                                                    overflow-y: auto; overflow-x: hidden;" CssClass="tooltip">
                                                                    <asp:Label ID="lblToolTipEmail" runat="server" />
                                                                </asp:Panel>
                                                            </asp:HyperLink><br />
                                                            <asp:HyperLink ID="hlMove" runat="server" Text="Move" Enabled="false"></asp:HyperLink><br />
                                                            <asp:Button ID="btnDelete" runat="server" CssClass="linkButtonStyle" Text="Delete"
                                                                OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure you want to delete this Call Entry?')">
                                                            </asp:Button><br />
                                                            <asp:CheckBox ID="chkEmail" CssClass="checkEmail" runat="server" />
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:Repeater>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </AlternatingItemTemplate>
                                <FooterTemplate>
                                    </tbody> </table></div></div>
                                </FooterTemplate>
                            </asp:Repeater>
                        </asp:Panel>
                    </div>
                    <div id="divButtonsCallLog" runat="server" visible="false" style="float: left; display: inline;">
                        <asp:Button ID="btnSkipToNext" runat="server" CssClass="btn" Text=">" OnClientClick="SkipToNext();return false;"
                            ToolTip="Skip to Next Unread Call Log" />&nbsp;&nbsp;
                        <asp:Button ID="btnSkipToNextOnJob" runat="server" CssClass="btn" Text=">>" OnClientClick="SkipToNextOnJob();return false;"
                            ToolTip="Skip to Next Unread Call Log of the Next Job" />&nbsp;&nbsp;
                        <asp:Button ID="btnSkipToNextOnDivision" runat="server" CssClass="btn" Text=">>>"
                            OnClientClick="SkipToNextOnDivision();return false;" ToolTip="Skip to Next Unread Call Log of the Next Division" />&nbsp;&nbsp;
                        <asp:Button ID="btnSkipToPrevOnJob" runat="server" CssClass="btn" Text="<<" OnClientClick="SkipToPrevOnJob();return false;"
                            ToolTip="Skip back to First Unread Call Log of the Previous Job" />
                        <asp:Button ID="btnSkipToNextJob" runat="server" CssClass="btn" Text="Next Job" OnClientClick="SkipToNextJob();return false;"
                            ToolTip="Skip to Next Job" />
                        <asp:Button ID="btnSkipToPrevJob" runat="server" CssClass="btn" Text="Prev Job" OnClientClick="SkipToPrevJob();return false;"
                            ToolTip="Skip to Previous Job" />
                    </div>
                    <div id="divButtons" class="buttons" runat="server">
                        <asp:Button ID="btnPrint" runat="server" CssClass="btn" Text="Print Job Worksheet"
                            CausesValidation="false" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Button ID="btnExport" runat="server" CssClass="btn" Text="Export Job Worksheet"
                            CausesValidation="false" OnClick="btnExport_Click" />&nbsp;&nbsp;
                        <asp:Button ID="btnEmailManual" runat="server" CssClass="btn" Text="Send Manual Email"
                            Enabled="false" CausesValidation="false" OnClick="btnEmailManual_Click" />
                    </div>
                    <div id="divRightClick" class="divRightClick" runat="server" clientidmode="Static">
                        <div class="divRightClickItem" onclick="RightClickAddCallEntry();">
                            <a id="lnKAddCallEntry">Apply Call Entry</a></div>
                        <div class="divRightClickItem" onclick="RightClickUpdJobRecord();">
                            <a id="lnKUpdJobRecord">Update Job Record</a></div>
                        <div class="divRightClickItem" onclick="RightClickAddResource();">
                            <a id="lnkAddResource">Add Resource</a></div>
                        <div class="divRightClickItem" onclick="RightClickPrintJobRecord();">
                            <a id="lnkPrintJobRecord">Print Job Record</a></div>
                        <div class="divRightClickItem" onclick="RighClickJobCloning();">
                            <a id="lnkJobCloning">Create Job Cloning</a></div>
                        <div class="divRightClickItem" onclick="RightClickFirstAlert();">
                            <a id="lnkFirstAlert">Add First Alert</a></div>
                        <div class="divRightClickItem" onmousedown="Copy();" onmouseup="KeepSelection();">
                            <a id="lnkCopy">Copy</a></div>
                    </div>
                    <asp:Button ID="btnFakeSort" runat="server" OnClick="btnFakeSort_Click" Style="display: none;" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript" language="javascript" defer="defer">
                
                //Instance of ScripManager
                var scriptManager = Sys.WebForms.PageRequestManager.getInstance();
                scriptManager.add_beginRequest(function () { 
                    clearTimeout(timeoutId); 
                    BeginRequestGridPosition();
                });

                scriptManager.add_endRequest(function() {
                    RestoreToggleFilters();
                    SetGridHeight();
                    EndRequestGridPosition();
                    EndRequestHandlerSetView();
                    EndRequestHandlerSelectRow();
                    $('#<%= pnlGridCallLogSummary.ClientID %>').keydown(function(event) { KeyCheck(); });
                    startCounter();
                });

                function LoadPage() {
                    setTimeout(startCounter, 1000);
                }

                $(document).ready(function () {
                    ConfigureToggleFilters();
                    SetGridHeight(); 
                    SetView(document.getElementById(<%= "'" + ddlFilter.ClientID + "'" %>));
                    SetUnreadCounter();
                    LoadPage();
                    $('#<%= pnlGridCallLogSummary.ClientID %>').keydown(function(event) { KeyCheck(); });
                });

                var timeoutHeightId;

                function SetGridHeight() {
                    var heightGrid = parseInt($("#tbRepeaters_ScrollDiv").css('max-height').replace('px', ''));
                    var heightVal = (ContentPanel.offsetHeight - (QuickSearch.offsetHeight + divTotal.offsetHeight + 20)) + heightGrid;

                    if (heightVal > 220)
                        $("#tbRepeaters_ScrollDiv").css('max-height', heightVal);
                    else
                        $("#tbRepeaters_ScrollDiv").css('max-height', 220);

                    if (timeoutHeightId)
                        clearTimeout(timeoutHeightId);
                }

                function ConfigureToggleFilters() {
                    $('.toggleFilters').click(function() {
                        $('.divFilter').toggle();
                        $('.toggleFilters').toggleClass('toggleUp');
                        $('.toggleFilters').toggleClass('toggleDown');
                        $('#<%=hfToggleFilterStatus.ClientID %>').val($('.divFilter').css('display'));
                        SetGridHeight();
                    });

                    $(window).resize(function () { timeoutHeightId = setTimeout(SetGridHeight, 500); });
                }

                function RestoreToggleFilters() {
                    $('.divFilter').css('display', $('#<%=hfToggleFilterStatus.ClientID %>').val());
                    if ($('.divFilter').css('display') == 'none') {
                        $('.toggleFilters').toggleClass('toggleUp');
                        $('.toggleFilters').toggleClass('toggleDown');
                    }

                    ConfigureToggleFilters();
                }

                // Timer Control
                var timerStarted = true;
                var initiateTimer = false;
                var initialCounterValue = 0;
                var timerTime;
                var timerUpdateTime;
                var timeoutId;
                var focusIn = false;
                var timerStoped = false;
                              
                function startCounter() {
                    clearTimeout(timeoutId);
                    var timerControl = $find('<%=tmrUpdate.ClientID %>');
                    if (timerControl) {
                        timerStarted = true;
                        initiateTimer = true;
                        timerControl._stopTimer();
                        initialCounterValue = timerControl.get_interval() / 1000;
                        if ($get('divCounter').innerText == undefined || $get('divCounter').innerText == '')
                            $get('divCounter').innerText = "Loading...";
                        timeoutId = setTimeout(updateCounter, 1000);
                        SetUnreadCounter();
                    }
                }

                function updateCounter() {
                    var timerControl = $find('<%=tmrUpdate.ClientID %>');

                    if (!focusIn)
                    {
                        if (timerStoped)
                        {
                            var d = new Date();
                            timerUpdateTime = ((d.getHours() * 3600) + d.getMinutes() * 60) + d.getSeconds() + secondsToUpdate;
                            timerControl._startTimer();
                            timerStoped = false;
                        }

                        if (initiateTimer)
                        {
                            var timerControl = $find('<%=tmrUpdate.ClientID %>');
                            var d = new Date();
                            timerControl._startTimer();
                            timerTime = ((d.getHours() * 3600) + d.getMinutes() * 60) + d.getSeconds();
                            timerUpdateTime = ((d.getHours() * 3600) + d.getMinutes() * 60) + d.getSeconds() + initialCounterValue;
                            initiateTimer = false;
                        }
                        $get('divCounter').innerText = "Update in " + getSecondsToUpdate();
                    }
                    else
                    {
                        if (!timerStoped)
                        {
                            timerControl._stopTimer();
                            timerStoped = true;
                        }
                    }
                    timeoutId = setTimeout(updateCounter, 1000);
                }

                var secondsToUpdate;

                function getSecondsToUpdate()
                {
                    var d = new Date();
                    timerTime = ((d.getHours() * 3600) + d.getMinutes() * 60) + d.getSeconds();
                    var ret = Math.round(timerUpdateTime - timerTime);
                    secondsToUpdate = ret;

                    if (ret <= 0)
                        return 1;
                    else
                        return ret;
                }

                // Collapse-Expand functionality
                function CollapseExpand(Button, selector) {
                    var line = $("." + selector);
                    var button = $("#" + Button);
                    var expandedItems = $get('<%=hfExpandedJobs.ClientID %>').value;

                    line.toggle();
                    button.toggleClass("Expand");
                    button.toggleClass("Collapse");

                    if (button.attr('class') == 'Expand')
                        expandedItems = expandedItems.replace(';' + selector, '');
                    else
                        expandedItems += ";" + selector;
                    $get('<%=hfExpandedJobs.ClientID %>').value = expandedItems;
                }

                // Right click functionality
                var jobId;
                var tempX = 0;
                var tempY = 0;
                function getMouseXY(e) {
                    tempX = event.clientX + document.body.scrollLeft;
                    tempY = event.clientY + document.body.scrollTop;
                }
                var selRange;
                function Copy() { 
                    var txt = '';

                    if (window.getSelection)
                    {
                        txt = window.getSelection();
                    }
	                else if (document.selection) {
                        var selectedText = document.selection;
                         if (selectedText.type == 'Text') {
		                    selRange = selectedText.createRange();
                            txt = selRange.text;
	                    }
                    }
                    
                    window.clipboardData.setData("Text", txt);
                }
                function KeepSelection() { 
                    if (selRange) {
                        selRange.select(); 
                        selRange = undefined; 
                    }
                }
                function showDiv() {
                    document.getElementById('divRightClick').style.display = 'block';
                    document.getElementById('divRightClick').style.top = tempY + document.documentElement.scrollTop + 'px';
                    document.getElementById('divRightClick').style.left = tempX + 'px';
                    document.getElementById('divRightClick').focus();
                    focusIn = true;
                }
                function hideDiv() {
                    if (document.getElementById('divRightClick'))
                        document.getElementById('divRightClick').style.display = 'none';
                    focusIn = false;
                }
                function RightClickFirstAlert() {
                    var newWindow = window.open('/FirstAlert.aspx?JobID=' + jobId, '', 'width=950, height=600, scrollbars=1, resizable=yes');
                }
                function RightClickAddResource() {
                    var newWindow = window.open('/ResourceAllocation.aspx?JobId=' + jobId, '', 'width=800, height=600, scrollbars=1, resizable=yes');
                }

                function RightClickUpdJobRecord() {
                    var newWindow = window.open('/JobRecord.aspx?JobId=' + jobId, '', 'width=1040, height=600, scrollbars=1, resizable=yes');
                }

                function RightClickAddCallEntry() {
                    var newWindow = window.open('/CallEntry.aspx?JobId=' + jobId, '', 'width=800, height=600, scrollbars=1, resizable=yes');
                }

                function RightClickPrintJobRecord() {
                    var newWindow = window.open('/JobRecordPrint.aspx?JobId=' + jobId, '', 'width=870, height=600, scrollbars=1, resizable=yes');
                }

                function RighClickJobCloning() {
                    if (confirm('Are you sure you want to clone this job? The data will be copied over into a new record.'))
                        var newWindow = window.open('/JobRecord.aspx?CloningId=' + jobId,'', 'width=1040, height=600, scrollbars=1, resizable=yes');
                }
//                function OpenDashboardPrintPage(){
//                    var newWindow = window.open('/DashboardPrint.aspx?
//                }

                document.onmousemove = getMouseXY;
                document.onclick = hideDiv;

                // ToolTip for the Details of a Call Log
                var panelId;
                var panelTop;
                var panelLeft;
                function ShowToolTip(panelControl, labelDes, labelTool) {
                    var scnWid, scnHei;
                    if (document.documentElement && document.documentElement.clientHeight) {
                        scnWid = document.documentElement.offsetWidth;
                        scnHei = document.documentElement.offsetHeight;
                    }
                    else if (document.body) {
                        scnWid = document.body.offsetWidth;
                        scnHei = document.body.offsetHeight;
                    }
                    labelTool.innerHTML = labelDes.innerHTML;
                    if (panelControl.style.display == 'none') {
                        if (panelId != panelControl.id) {
                            panelId = panelControl.id;
                            panelTop = 0;
                            panelLeft = 0;
                        }
                        panelControl.style.display = 'block';
                        var yPos = window.event.clientY + panelControl.offsetHeight;
                        if (yPos > scnHei) {
                            var difference = yPos - scnHei;
                            panelControl.style.top = window.event.clientY + document.documentElement.scrollTop - difference;
                            panelTop = panelControl.style.top;
                        }
                        else {
                            panelControl.style.top = window.event.clientY + document.documentElement.scrollTop;
                            panelTop = panelControl.style.top;
                        }
                        var xPos = window.event.clientX + panelControl.offsetWidth + 25;
                        if (xPos > scnWid) {
                            if (panelLeft == 0) {
                                var difference = xPos - scnWid;
                                panelControl.style.left = window.event.clientX - difference;
                                panelLeft = panelControl.style.left;
                            }
                            else {
                                panelControl.style.left = panelLeft;
                            }
                        }
                        else {
                            if (panelLeft == 0) {
                                panelControl.style.left = window.event.clientX;
                                panelLeft = panelControl.style.left;
                            }
                            else {
                                panelControl.style.left = panelLeft; 
                            }
                        }
                    }
                }

                // Keep Scroll Position for the Repeater
                var control, yPos;
                function BeginRequestGridPosition(sender, args) {
                    control = $get('tbRepeaters_ScrollDiv');
                    if (control)
                        yPos = control.scrollTop;
                }

                function EndRequestGridPosition(sender, args) {
                    control = $get('tbRepeaters_ScrollDiv');
                    if (control)
                        control.scrollTop = yPos;
                }
                

                // Filter for Read/Unread items
                function SetView(obj) {
                    if (obj != null) {
                        if (obj.value == 'All') {
                            SetViewFilter('Unread', true);
                            SetViewFilter('Read', true);
                        }
                        else if (obj.value == 'Unread') {
                            SetViewFilter('Unread', true);
                            SetViewFilter('Read', false);
                        }
                        else if (obj.value == 'Read') {
                            SetViewFilter('Unread', false);
                            SetViewFilter('Read', true);
                        }
                    }
                }

                function SetViewFilter(param, display) {
                    var selection = $('.' + param);
                    selection.each(function () {
                        if (display)
                            $(this)[0].style.display = '';
                        else
                            $(this)[0].style.display = 'none';
                    });
                }
                function EndRequestHandlerSetView(sender, args) {
                    var obj = document.getElementById('<%=ddlFilter.ClientID %>');
                    if (obj)
                        SetView(obj);
                }
                

                // Select line functionality / Skip funcionality
                var CurrentId;
                var CurrentJobIndex = 0;
                var CurrentDivisionIndex = 0;
                var numberOfDivisions = document.getElementById('<%=hfDivisionCount.ClientID %>').value;
                var numberOfJobs = document.getElementById('<%=hfJobCount.ClientID %>').value;
                var numberOfCallLogs = document.getElementById('<%=hfCallLogCount.ClientID %>').value;
                var windowHasFocus = true;

                if (/*@cc_on!@*/false) { // check for Internet Explorer
	                document.onfocusout = function() { windowHasFocus = false; };
                    document.onfocusin =  function() { windowHasFocus = true; };
                } else {
	                window.onblur = function() { windowHasFocus = false; };
                    window.onfocus =  function() { windowHasFocus = true; };
                }

                function SkipToNext() {
                    var selection = $(".Unread.Job" + CurrentJobIndex);
                    while (selection.length == 0 && CurrentJobIndex <= numberOfJobs) {
                        CurrentJobIndex++;
                        selection = $(".Unread.Job" + CurrentJobIndex);
                    }
                    if (selection.length > 0) {
                        if (CurrentId)
                            deselectLastRow();
                        CurrentId = selection.first().attr("id");
                        selectCurrentRow();
                    }
                }

                function SkipToNextJob() {
                    if ((CurrentJobIndex + 1) <= numberOfJobs) {
                        CurrentJobIndex++;   
                        var selection = $(".Job" + CurrentJobIndex);
                        while (selection.length == 0 && CurrentJobIndex <= numberOfJobs) {
                            CurrentJobIndex++;
                            selection = $(".Job" + CurrentJobIndex);
                        }
                        if (selection.length > 0) {
                            if (CurrentId)
                                deselectLastRow();
                            CurrentId = selection.first().attr("id");
                            selectCurrentRow();
                        }
                    }     
                }

                function SkipToPrevJob() {
                    if ((CurrentJobIndex - 1) >= 1) {
                        CurrentJobIndex--;
                        var selection = $(".Job" + CurrentJobIndex);
                        while (selection.length == 0 && CurrentJobIndex >= 0) {
                            CurrentJobIndex--;
                            selection = $(".Job" + CurrentJobIndex);
                        }
                        if (selection.length > 0) {
                            if (CurrentId)
                                deselectLastRow();
                            CurrentId = selection.first().attr("id");
                            selectCurrentRow();
                        }             
                    }
                }
                function SkipToNextOnJob() {
                    CurrentJobIndex++;
                    SkipToNext();
                }

                function SkipToNextOnDivision() {
                    CurrentDivisionIndex++;
                    var selection = $(".Unread.Division" + CurrentDivisionIndex);
                    while (selection.length == 0 && CurrentDivisionIndex <= numberOfDivisions) {
                        CurrentDivisionIndex++;
                        selection = $(".Unread.Division" + CurrentDivisionIndex);
                    }
                    if (selection.length > 0) {
                        if (CurrentId)
                            deselectLastRow();
                        CurrentId = selection.first().attr("id");
                        selectCurrentRow();
                    }
                }

                function SkipToPrevOnJob() {
                    CurrentJobIndex--;
                    var selection = $(".Unread.Job" + CurrentJobIndex);
                    while (selection.length == 0 && CurrentJobIndex >= 0) {
                        CurrentJobIndex--;
                        selection = $(".Unread.Job" + CurrentJobIndex);
                    }
                    if (selection.length > 0) {
                        if (CurrentId)
                            deselectLastRow();
                        CurrentId = selection.first().attr("id");
                        selectCurrentRow();
                    }
                }

                function deselectLastRow() {
                    var row = document.getElementById(CurrentId);
                    if (row) {
                        var cells = row.getElementsByTagName('td');
                        if (cells)
                            cells[0].setAttribute('id', '');
                    }
                    var currentRow = $('#' + CurrentId);
                    if (currentRow.hasClass('focus')) {
                        currentRow.removeClass('focus');
                    }
                }

                function selectCurrentRow() {
                    // Set focus to the firs TD of the selected row
                    var hiddenField = document.getElementById('<%= hfSelectedCallLog.ClientID %>');
                    var row = document.getElementById(CurrentId);
                    if (row) {
                        var cells = row.getElementsByTagName('td');
                        if (cells) {
                            cells[0].setAttribute('id', 'focus');
                            if (windowHasFocus) {
                                var focusCell = document.getElementById('focus');
                                if (focusCell)
                                    focusCell.focus();
                            }
                        }
                    }
                    // Change the Css Class of the selected row
                    var currentRow = $('#' + CurrentId);
                    if (!currentRow.hasClass('focus'))
                        currentRow.addClass('focus');                    
                    // Moves the selected row to te top of the overflow div, if necessary
                    if (row) {
                        var t = document.getElementById('tbRepeaters');
                        if (row.offsetTop >= t.rows[0].offsetTop && row.offsetTop <= t.rows[0].offsetTop + t.rows[0].scrollHeight)
                            tbRepeaters_ScrollDiv.scrollTop = t.rows[0].offsetTop + (row.offsetTop - t.rows[0].offsetTop) - t.rows[0].scrollHeight;
                    }
                    // Update Hidden fields and Css Class for changing the item from Unread to Read
                    if (currentRow.hasClass('Unread')) {
                        currentRow.removeClass('Unread');
                        currentRow.addClass('Read');
                        var hiddenFields = row.getElementsByTagName("input");
                        if (hiddenFields) { 
                            document.getElementById('<%=hfReadCallLogs.ClientID %>').value += ";" + hiddenFields[0].value + ',' + hiddenFields[1].value;
                            }
                    }
                    // Finding the Current Job Index based on row Css Class
                    if ($('#' + CurrentId).first().attr("class")) {
                        CurrentJobIndex = $('#' + CurrentId).first().attr("class").substring($('#' + CurrentId).first().attr("class").indexOf("Job") + 3);
                        if (CurrentJobIndex.indexOf(" ") > 0)
                            CurrentJobIndex = CurrentJobIndex.substring(0, CurrentJobIndex.indexOf(" "));
                    }
                    // Finding the Current Division Index based on row Css Class
                    if ($('#' + CurrentId).first().attr("class")) {
                        CurrentDivisionIndex = $('#' + CurrentId).first().attr("class").substring($('#' + CurrentId).first().attr("class").indexOf("Division") + 8);
                        if (CurrentDivisionIndex.indexOf(" ") > 0)
                            CurrentDivisionIndex = CurrentDivisionIndex.substring(0, CurrentDivisionIndex.indexOf(" "));
                    }

                    hiddenField.value = CurrentId;
                    SetUnreadCounter();
                }

                function rowClick(id) {
                    var t = document.getElementById('tbRepeaters');
                    if (CurrentId)
                        deselectLastRow();
                    CurrentId = id;
                    selectCurrentRow();                    
                }

                function KeyCheck() {
                    var t = document.getElementById('tbRepeaters');
                    var KeyID = event.keyCode;
                    var RowIndex;
                    if (KeyID == 38 || KeyID == 40) {
                        if (null == CurrentId || CurrentId == "")
                            CurrentId = $('.CallLogCount0').attr('id');
                        else {
                            RowIndex = $('#' + CurrentId).attr("class").substring($('#' + CurrentId).attr("class").indexOf("CallLogCount") + 12);
                            if (RowIndex.indexOf(" ") > 0)
                                RowIndex = RowIndex.substring(0, RowIndex.indexOf(" "));
                            RowIndex = RowIndex * 1;
                            if (KeyID == 38) {
                                if (RowIndex - 1 < 0)
                                    return false;
                                else
                                    deselectLastRow();
                                CurrentId = $('.CallLogCount' + (RowIndex - 1)).attr('id');
                            }
                            else {
                                if (RowIndex + 1 >= numberOfCallLogs)
                                    return false;
                                else
                                    deselectLastRow(RowIndex % 2 == 0);
                                CurrentId = $('.CallLogCount' + (RowIndex + 1)).attr('id');
                            }
                        }
                        selectCurrentRow();
                        return false;
                    }
                    SetUnreadCounter();
                }

                function EndRequestHandlerSelectRow(sender, args) {
                    CurrentId = document.getElementById('<%=hfSelectedCallLog.ClientID%>').value;
                    numberOfDivisions = document.getElementById('<%=hfDivisionCount.ClientID %>').value;
                    numberOfJobs = document.getElementById('<%=hfJobCount.ClientID %>').value;
                    numberOfCallLogs = document.getElementById('<%=hfCallLogCount.ClientID %>').value;
                    if (CurrentId != '')
                        selectCurrentRow();
                }

                // Sorting Functionality
                    function SetOrderBy(value, control) {
                        var hfOrderBy = document.getElementById("hfOrderBy");
                        var oldOrderBy = hfOrderBy.value.split(' ');
                        if (oldOrderBy[0] == value) {
                            if (oldOrderBy[1] == '1')
                                hfOrderBy.value = value + ' 2';
                            else
                                hfOrderBy.value = value + ' 1';
                        }
                        else
                            hfOrderBy.value = value + ' 1'
                        __doPostBack('<%= btnFakeSort.UniqueID %>', '');
                    }

                function SetUnreadCounter(){
                    var unreadLabel = document.getElementById('<%=lblUnreadCounter.ClientID %>');
                    var callLogCount = $(".Unread").length;   
                    if(unreadLabel != null){                               
                        unreadLabel.innerText='(' + callLogCount + ')';
                    }
                }

                function getMousePosition() {
                    xPosClick = window.event.clientX;
                    yPosClick = window.event.clientY;
                    return true;
                }

                 var panelObj, panelLabelObj, yPosClick, xPosClick;

                function CallEmailWebService(callLogId, panel, panelLabel) {
                    panelObj = panel;
                    panelLabelObj = panelLabel;

                    if (panelLabel.innerHTML != '')
                        return;

                    if (callLogId != '0') {
                        tempuri.org.IJSONService.GetEmailData(callLogId, CallEmailWebServiceCompleted);
                    }
                }

                function CallEmailWebServiceCompleted(WebServiceResult) {
                    var panelControl = panelObj;
                    var labelTool = panelLabelObj;
                    var scnWid, scnHei;
                    if (document.documentElement && document.documentElement.clientHeight) {
                        scnWid = document.documentElement.offsetWidth;
                        scnHei = document.documentElement.offsetHeight;
                    }
                    else if (document.body) {
                        scnWid = document.body.offsetWidth;
                        scnHei = document.body.offsetHeight;
                    }

                    labelTool.innerHTML = WebServiceResult.InnerHTML;

                    if (panelControl.style.display == 'none') {
                        if (panelId != panelControl.id) {
                            panelId = panelControl.id;
                            panelTop = 0;
                            panelLeft = 0;
                        }
                        panelControl.style.display = 'block';
                        yPosClick -= 2;
                        var yPos = yPosClick + panelControl.offsetHeight;
                        if (yPos > scnHei) {
                            var difference = yPos - scnHei;
                            panelControl.style.top = yPosClick + document.documentElement.scrollTop - difference;
                            panelTop = panelControl.style.top;
                        }
                        else {
                            panelControl.style.top = yPosClick + document.documentElement.scrollTop;
                            panelTop = panelControl.style.top;
                        }
                        var xPos = xPosClick + panelControl.offsetWidth + 25;
                        if (xPos > scnWid) {
                            if (panelLeft == 0) {
                                var difference = xPos - scnWid;
                                panelControl.style.left = xPosClick - difference;
                                panelLeft = panelControl.style.left;
                            }
                            else {
                                panelControl.style.left = panelLeft;
                            }
                        }
                        else {
                            if (panelLeft == 0) {
                                panelControl.style.left = xPosClick;
                                panelLeft = panelControl.style.left;
                            }
                            else {
                                panelControl.style.left = panelLeft; 
                            }
                        }
                    }
                }
                
                function onChecked(checkbox, jobid, callid)
                {
                    var JobID = document.getElementById('<%= hfJobId.ClientID %>');
                    if(JobID.value == "")
                    {
                        JobID.value = jobid;
                        document.getElementById('<%= hfCallId.ClientID %>').value += callid + ';';
                        document.getElementById('<%= btnEmailManual.ClientID %>').disabled = false;
                    }
                    else
                    {
                        if(JobID.value == jobid)
                        {
                            if(checkbox.checked)
                            {
                                document.getElementById('<%= hfCallId.ClientID %>').value += callid + ';';
                                document.getElementById('<%= btnEmailManual.ClientID %>').disabled = false;
                            }
                            else
                            {
                                var callLogId = document.getElementById('<%= hfCallId.ClientID %>').value;
                            
                                var lstCallLogId = callLogId.split(';');

                                var callogIds = '';

                                for (var i = 0; i < lstCallLogId.length; i++) 
                                {
                                    if(lstCallLogId[i] != '')
                                    {
                                        if(callid != lstCallLogId[i])
                                        {
                                            callogIds += lstCallLogId[i] + ';';
                                        }
                                    }
                                }

                                if(callogIds != '')
                                {
                                    document.getElementById('<%= hfCallId.ClientID %>').value = callogIds;
                                    document.getElementById('<%= btnEmailManual.ClientID %>').disabled = false;
                                }
                                else
                                {
                                    JobID.value = '';
                                    document.getElementById('<%= hfCallId.ClientID %>').value = '';
                                    document.getElementById('<%= btnEmailManual.ClientID %>').disabled = true;
                                }
                             }
                         }
                         else
                         {
                            alert("You can't select call logs for different job.");
                            checkbox.checked = false;
                         }
                    }
                }
    </script>
    </div>
</asp:Content>
