<%@ Page Title="" Language="C#" MasterPageFile="~/ContentPage.master" AutoEventWireup="true"
    CodeBehind="ProjectCalendar.aspx.cs" Inherits="Hulcher.OneSource.CustomerService.Web.ProjectCalendar" %>

<%@ MasterType TypeName="Hulcher.OneSource.CustomerService.Web.ContentPage" %>
<%@ Register Src="UserControls/DatePicker.ascx" TagName="DatePicker" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/AutoCompleteTextbox.ascx" TagName="AutoCompleteTextbox"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Styles/Forms.css" rel="stylesheet" type="text/css" />
    <link href="Styles/ProjectCalendar.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <asp:Panel ID="pnlForDefaultButton" runat="server" DefaultButton="">
        <asp:UpdatePanel ID="upContent" runat="server">
            <ContentTemplate>
                <asp:ValidationSummary ID="vsProjectCalendar" runat="server" CssClass="errorbox"
                    ValidationGroup="ProjectCalendar" HeaderText="Please correct the following information" />
                <div id="mainTitle" class="Header">
                    <asp:Label ID="lblTitle" Text="Project Calendar" runat="server"></asp:Label>
                </div>
                <div id="Content" class="Content">
                    <div class="toggleFilters toggleUp">
                        &nbsp;
                    </div>
                    <asp:HiddenField ID="hfToggleFilterStatus" runat="server" />
                    <div class="inlineBlock space100" />
                    <div id="divFilter" class="divFilter">
                        <div class="inlineBlock space100 paddingBottom5">
                            <div class="space39 floatLeft block">
                                <div class="space29 floatLeft alignRight">
                                    <asp:Label ID="lblEquipmentType" runat="server" Text="Equipment Type:"></asp:Label>
                                </div>
                                <div class="space70 floatRight alignLeft">
                                    <uc1:AutoCompleteTextbox ID="actEquipmentType" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                        TextBoxWidth="120px" GridViewIdName="ID" MinimumPrefixLength="1" DisplayField="CompleteName"
                                        AutoPostBack="false" RequiredField="false" WindowTitle="Project Calendar - Equipment Type"
                                        AutoCompleteSource="EquipmentType" ColumnHeaderList="EquipmentType" ColumnValueList="CompleteName"
                                        ServiceMethod="GetEquipmentTypeList" ControlsToUpdate="actUnitNumber" ValidationGroup="ProjectCalendar"
                                        TextBoxCssClass="input" TextControlOnClientClickScript="setTimeout(function () { this.focus(); focusIn = true; }, 100);"
                                        TextControlOnFocusScript=" focusIn = true; " TextControlOnBlurScript=" focusIn = false; " />
                                </div>
                            </div>
                            <div class="space30 floatLeft">
                                <div class="space29 floatLeft alignRight paddingTop2">
                                    <asp:Label ID="lblUnitNumber" runat="server" Text="Unit#:"></asp:Label>
                                </div>
                                <div class="space70 floatRight alignLeft">
                                    <uc1:AutoCompleteTextbox ID="actUnitNumber" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                        TextBoxWidth="120px" GridViewIdName="ID" MinimumPrefixLength="1" DisplayField="Descriprion"
                                        AutoPostBack="false" RequiredField="false" WindowTitle="Project Calendar - Unit#"
                                        AutoCompleteSource="Equipment" ColumnHeaderList="Unit,Description" ColumnValueList="Name,Description"
                                        ServiceMethod="GetEquipmentList" OnTextChanged="actUnitNumber_TextChanged" ValidationGroup="ProjectCalendar"
                                        TextBoxCssClass="input" TextControlOnClientClickScript="setTimeout(function () { this.focus(); focusIn = true; }, 100);"
                                        TextControlOnFocusScript=" focusIn = true; " TextControlOnBlurScript=" focusIn = false; "
                                        ScriptToExecute="SetEquipmentType" />
                                </div>
                            </div>
                            <div class="space30 floatLeft block">
                                <div class="space29 floatLeft alignRight paddingTop2">
                                    <asp:Label ID="lblStart" runat="server" Text="Start:"></asp:Label>
                                </div>
                                <div class="floatRight alignLeft space70">
                                    <asp:DatePicker ID="dpBeginDate" InvalidValueMessage="Invalid date format" ValidationGroup="ProjectCalendar"
                                        EmptyValueMessage="The Start Date field is required" DateTimeFormat="Default"
                                        ShowOn="Both" runat="server" IsValidEmpty="false" TextControlOnClientClickScript="setTimeout(function () { this.focus(); focusIn = true; }, 100);"
                                        TextControlOnFocusScript=" focusIn = true; " TextControlOnBlurScript=" focusIn = false; " />
                                </div>
                            </div>
                        </div>
                        <div class="inlineBlock space100 paddingBottom5">
                            <div class="space39 floatLeft block">
                                <div class="space29 floatLeft alignRight">
                                    <asp:Label ID="lblEmployee" runat="server" Text="Employee Name:"></asp:Label>
                                </div>
                                <div class="space70 floatRight alignLeft">
                                    <uc1:AutoCompleteTextbox ID="actEmployeeName" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                        TextBoxWidth="120px" GridViewIdName="ID" MinimumPrefixLength="1" DisplayField=""
                                        AutoPostBack="false" RequiredField="false" WindowTitle="Project Calendar - Employee Name"
                                        AutoCompleteSource="Employee" ColumnHeaderList="Name,FirstName" ColumnValueList="Name,FirstName"
                                        ServiceMethod="GetEmployeeList" ValidationGroup="ProjectCalendar" TextBoxCssClass="input"
                                        TextControlOnClientClickScript="setTimeout(function () { this.focus(); focusIn = true; }, 100);"
                                        TextControlOnFocusScript=" focusIn = true; " TextControlOnBlurScript=" focusIn = false; " />
                                </div>
                            </div>
                            <div class="space30 floatLeft">
                                <div class="space29 floatLeft alignRight paddingTop2">
                                    <asp:Label ID="lblJobNumber" runat="server" Text="Job Number:"></asp:Label>
                                </div>
                                <div class="space70 floatRight alignLeft">
                                    <uc1:AutoCompleteTextbox ID="actJobNumber" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                        TextBoxWidth="120px" GridViewIdName="ID" DisplayField="" AutoPostBack="false"
                                        RequiredField="false" WindowTitle="Project Calendar - Job Number" AutoCompleteSource="JobNumberByStatus"
                                        ColumnHeaderList="Number,Company,Location" ColumnValueList="PrefixedNumber,CS_CustomerInfo.CS_Customer.Name,CS_LocationInfo.FullLocation"
                                        ServiceMethod="GetJobNumberList" ValidationGroup="ProjectCalendar" TextBoxCssClass="input"
                                        TextControlOnClientClickScript="setTimeout(function () { this.focus(); focusIn = true; }, 100);"
                                        TextControlOnFocusScript=" focusIn = true; " TextControlOnBlurScript=" focusIn = false; "
                                        ContextKey="1" />
                                </div>
                            </div>
                            <div class="space30 floatLeft block">
                                <div class="floatLeft alignRight paddingTop2 space29">
                                    <asp:Label ID="lblEndDate" runat="server" Text="End: "></asp:Label>
                                </div>
                                <div class="floatRight alignLeft space70">
                                    <asp:DatePicker ID="dpEndDate" InvalidValueMessage="Invalid date format" ValidationGroup="ProjectCalendar"
                                        EmptyValueMessage="The Start Date field is required" DateTimeFormat="Default"
                                        ShowOn="Both" runat="server" IsValidEmpty="false" TextControlOnClientClickScript="setTimeout(function () { this.focus(); focusIn = true; }, 100);"
                                        TextControlOnFocusScript=" focusIn = true; " TextControlOnBlurScript=" focusIn = false; " />
                                </div>
                            </div>
                        </div>
                        <div class="inlineBlock space100 paddingBottom5">
                            <div class="space39 floatLeft block">
                                <div class="space29 floatLeft alignRight">
                                    <asp:Label ID="lblCustomer" runat="server" Text="Company:"></asp:Label>
                                </div>
                                <div class="space70 floatRight alignLeft">
                                    <uc1:AutoCompleteTextbox ID="actCustomer" runat="server" ServiceMethod="GetCustomerList"
                                        GridViewButtonImageUrl="~/Images/money.png" GridViewIdName="ID" DisplayField="Name"
                                        TextBoxWidth="120px" AutoPostBack="false" RequiredField="false" ValidationGroup="ProjectCalendar"
                                        WindowTitle="Project Calendar - Company" AutoCompleteSource="Customer" ColumnHeaderList="Name,Attn,Company ID"
                                        ColumnValueList="Name,Attn,CustomerNumber" TextControlOnClientClickScript="setTimeout(function () { this.focus(); focusIn = true; }, 100);"
                                        TextControlOnFocusScript=" focusIn = true; " TextControlOnBlurScript=" focusIn = false; " />
                                </div>
                            </div>
                            <div class="space30 floatLeft">
                                <div class="space29 floatLeft alignRight paddingTop2">
                                    <asp:Label ID="lblDivision" runat="server" Text="Division: " class="labels"></asp:Label>
                                </div>
                                <div class="space70 floatRight alignLeft">
                                    <uc1:AutoCompleteTextbox ID="actDivision" runat="server" ServiceMethod="GetDivisionList"
                                        TextBoxWidth="120px" GridViewButtonImageUrl="~/Images/money.png" GridViewIdName="ID"
                                        DisplayField="Division" FilterId="0" ContextKey="0" AutoPostBack="false" RequiredField="false"
                                        WindowTitle="Project Calendar - Division" AutoCompleteSource="Division" ColumnHeaderList="Name,Description"
                                        TextBoxCssClass="input" ColumnValueList="Name,Description" ValidationGroup="ProjectCalendar"
                                        TextControlOnClientClickScript="setTimeout(function () { this.focus(); focusIn = true; }, 100);"
                                        TextControlOnFocusScript=" focusIn = true; " TextControlOnBlurScript=" focusIn = false; " />
                                </div>
                            </div>
                            <div class="space30 floatLeft">
                                <div class="space29 floatLeft alignRight paddingTop2">
                                    <asp:Label ID="lblJobAction" runat="server" Text="Job Action: " class="labels"></asp:Label>
                                </div>
                                <div class="space70 floatRight alignLeft">
                                    <uc1:AutoCompleteTextbox ID="actJobAction" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                        TextBoxWidth="120px" GridViewIdName="ID" DisplayField="Description" AutoPostBack="false"
                                        RequiredField="false" WindowTitle="Quick Job - Find Job Action" AutoCompleteSource="JobAction"
                                        ColumnHeaderList="Description" ColumnValueList="Description" ServiceMethod="GetJobActionList"
                                        ValidationGroup="ProjectCalendar" TextBoxCssClass="input" />
                                </div>
                            </div>
                        </div>
                        <div class="inlineBlock alignRight space100 paddingBottom5">
                            <div class="space30">
                                <div class="space70 floatRight alignLeft paddingRight5">
                                    <asp:Button ID="btnFind" runat="server" CssClass="btn" OnClick="btnFind_Click" Text="Find"
                                        ValidationGroup="ProjectCalendar" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="ProjectCalendarTable" runat="server" style="display: none;">
                        <div class="ProjectCalendar_Group" style="width: 100%">
                            <div class="ProjectCalendar_HeaderDiv" style="min-width: 400px;">
                            </div>
                            <div id="tbProjectCalendar" class="ProjectCalendar_ScrollDiv" style="max-height: 220px; min-width: 400px;">
                                <asp:Literal ID="litCalendar" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                    <asp:Panel ID="pnlNoRows" runat="server" Visible="true">
                        <div class="ProjectCalendar_Group" style="width: 100%">
                            <div class="ProjectCalendar_HeaderDiv" style="min-width: 400px;">
                            </div>
                            <div id="tbProjectCalendar" class="ScrollableGridView_ScrollDiv" style="max-height: 220px;
                                min-width: 400px;">
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
                </div>
                <div class="AlignCenter">
                    <asp:Button CssClass="btn" ID="btnPrint" runat="server" Text="Print Calendar" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <script language="javascript" type="text/javascript">
        function SetEquipmentType(equipmentId) {
            if (equipmentId != 0) {
                if (document.getElementById('<%=actEquipmentType.HiddenFieldValueClientID %>').value == '0')
                    tempuri.org.IJSONService.GetEquipmentType(equipmentId, CallEquipmentWebServiceCompleted);
            }
        }

        function CallEquipmentWebServiceCompleted(result) {
            var equipmentType = $find('actEquipmentType');

            if (null != result)
                equipmentType.raiseItemSelected(new Sys.Extended.UI.AutoCompleteItemEventArgs(null, result.Name, result.Id));
        }

        //Instance of ScripManager
        var scriptManager = Sys.WebForms.PageRequestManager.getInstance();

        scriptManager.add_endRequest(function () {
            ConfigureToggleFilters();
            SetGridHeight();
        });

        $(document).ready(function () {
            ConfigureToggleFilters();
        });

        function SetGridHeight() {
            var heightGrid = parseInt($("#tbProjectCalendar").css('max-height').replace('px', ''));
            var heightVal = (document.body.offsetHeight - (mainTitle.offsetHeight + Content.offsetHeight + 20)) + heightGrid;

            if (heightVal > 220)
                $("#tbProjectCalendar").css('max-height', heightVal);
            else
                $("#tbProjectCalendar").css('max-height', 220);
        }

        function ConfigureToggleFilters() {
            $('.toggleFilters').click(function () {
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

        var tempX = 0;
        var tempY = 0;
        function getMouseXY(e) {
            tempX = event.clientX + document.body.scrollLeft;
            tempY = event.clientY + document.body.scrollTop;
        }

        document.onmousemove = getMouseXY;

        var panelId;
        var panelTop;
        var panelLeft;
        function ShowToolTip(panelControl) {

            var scnWid, scnHei;
            if (document.documentElement && document.documentElement.clientHeight) {
                scnWid = document.documentElement.offsetWidth;
                scnHei = document.documentElement.offsetHeight;
            }
            else if (document.body) {
                scnWid = document.body.offsetWidth;
                scnHei = document.body.offsetHeight;
            }

            if (panelControl.style.display == 'none') {
                if (panelId != panelControl.id) {
                    panelId = panelControl.id;
                    panelTop = 0;
                    panelLeft = 0;
                }
                panelControl.style.display = 'inline';
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
    </script>
</asp:Content>
