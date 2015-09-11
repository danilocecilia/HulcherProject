<%@ Page Title="Automated DPI" Language="C#" MasterPageFile="~/ContentPage.master"
    AutoEventWireup="true" CodeBehind="AutomatedDPI.aspx.cs" Inherits="Hulcher.OneSource.CustomerService.Web.AutomatedDPI" %>

<%@ MasterType TypeName="Hulcher.OneSource.CustomerService.Web.ContentPage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Styles/Forms.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div id="divTotal">
        <asp:ValidationSummary ID="vsSummary" runat="server" ValidationGroup="DPI" />
        <div>
            <asp:Label ID="lblTitle" runat="server" Text="Automated DPI" Font-Size="Large" Font-Bold="true"></asp:Label>
        </div>
        <br />
        <div id="Header" class="Header">
            <asp:Label ID="lblDashboardTitle" runat="server" Text="Dashboard"></asp:Label>
        </div>
        <asp:UpdatePanel ID="upDPIDashboard" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnFilter" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <div id="Content" class="Content">
                    <div id="Top" class="space95 paddingBottom5 inlineBlock">
                        <div class="floatRight alignRight">
                            <asp:Label ID="lblGenerationDateTitle" runat="server" Text="Data Generated on:" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblGenerationDateValue" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <br />
                    <div id="JobTypeFilter" class="space100 paddingBottom5 inlineBlock">
                        <div class="space100 inlineBlock">
                            <div class="space49 alignRight floatLeft">
                                <asp:Label ID="lblProcessingType" runat="server" Text="Processing Type:"></asp:Label>
                            </div>
                            <div class="space50 aligntLeft floatRight">
                                <asp:ComboBox ID="cbbProcessingType" runat="server" CssClass="WindowsStyle" AutoCompleteMode="SuggestAppend"
                                    DropDownStyle="DropDown" CaseSensitive="false" RenderMode="Inline" Width="100px">
                                    <asp:ListItem Selected="True" Text="New Jobs" Value="1"></asp:ListItem>
                                    <asp:ListItem Selected="False" Text="Continuing Jobs" Value="2"></asp:ListItem>
                                    <asp:ListItem Selected="False" Text="Reprocess Jobs" Value="3"></asp:ListItem>
                                </asp:ComboBox>
                            </div>
                        </div>
                        <div class="space100 inlineBlock">
                            <div class="space49 alignRight floatLeft inline">
                                <asp:Label ID="lblReprocessDate" runat="server" Text="Date To Reprocess:"></asp:Label>
                            </div>
                            <div id="divMainFilter" class="space50 aligntLeft floatRight inlineBlock">
                                <div id="divDatePicker" style="display: none" class="inline">
                                    <uc1:DatePicker ID="dpReprocessDate" runat="server" InvalidValueMessage="Invalid date format"
                                        DateTimeFormat="Default" ShowOn="Both" IsValidEmpty="false" EmptyValueMessage="The Date to Reprocess field is required."
                                        ValidationGroup="DPI" />
                                </div>
                                <div id="divNewJobFilter" class="inline">
                                    <asp:RadioButtonList ID="rbNewJobDataFilter" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Selected="true" Text="Day Before" Value="DayBefore"></asp:ListItem>
                                        <asp:ListItem Text="Current Date" Value="CurrentDate"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <asp:Button ID="btnFilter" runat="server" Text="Filter" OnClick="btnFilter_Click"
                                    OnClientClick="return ValidateDateProcessJob();" ValidationGroup="DPI" class="btn" />
                            </div>
                        </div>
                    </div>
                    <div id="Grid">
                        <div id="showGrid" runat="server">
                            <asp:Repeater ID="rptDPIDashboard" runat="server" OnItemDataBound="rptDPIDashboard_ItemDataBound">
                                <HeaderTemplate>
                                    <div id="tbRepeaters_Group" class="ScrollableGridView_Group" style="width: 100%">
                                        <div id="tbRepeaters_HeaderDiv" class="ScrollableGridView_HeaderDiv" style="min-width: 400px;">
                                        </div>
                                        <div id="tbRepeaters_ScrollDiv" class="ScrollableGridView_ScrollDiv" style="max-height: 220px;
                                            min-width: 400px;">
                                            <table id="tbRepeaters" class="ScrollableGridView" cellspacing="1">
                                                <thead>
                                                    <tr>
                                                        <th id="thExpandCollapse" runat="server" class="header AlignCenter" style="border: 1px solid #E6EEEE;
                                                            height: 40px" rowspan="2">
                                                            &nbsp;
                                                        </th>
                                                        <th id="thJobNumber" runat="server" class="header AlignCenter" style="border: 1px solid #E6EEEE;
                                                            height: 40px" rowspan="2">
                                                            <asp:Label ID="lblHeaderJobNumber" runat="server" CssClass="AlignCenter" Text="Job Number" />
                                                        </th>
                                                        <th id="thDivision" runat="server" class="header AlignCenter" style="border: 1px solid #E6EEEE;
                                                            margin: 0 20px 0 0; height: 40px" rowspan="2">
                                                            <asp:Label ID="lblHeaderDivision" runat="server" CssClass="AlignCenter" Text="Division" />
                                                        </th>
                                                        <th id="thCustomerResource" runat="server" class="header AlignCenter" style="border: 1px solid #E6EEEE;
                                                            height: 40px" rowspan="2">
                                                            <asp:Label ID="lblHeaderCustomerResources" runat="server" CssClass="AlignCenter"
                                                                Text="Company / Resources" />
                                                        </th>
                                                        <th id="thLocationDescription" runat="server" class="header AlignCenter" style="border: 1px solid #E6EEEE;
                                                            height: 40px" rowspan="2">
                                                            <asp:Label ID="lblHeaderLocationDescription" runat="server" CssClass="AlignCenter"
                                                                Text="Location / Description" />
                                                        </th>
                                                        <th id="thJobAction" runat="server" class="header AlignCenter" style="border: 1px solid #E6EEEE;
                                                            height: 40px" rowspan="2">
                                                            <asp:Label ID="lblHeaderJobAction" runat="server" CssClass="AlignCenter" Text="Job Action" />
                                                        </th>
                                                        <th id="thCarCount" runat="server" class="header AlignCenter" style="border: 1px solid #E6EEEE;
                                                            height: 20px" colspan="3">
                                                            <asp:Label ID="lblHeaderCarCount" runat="server" CssClass="AlignCenter" Text="Car Count" />
                                                        </th>
                                                        <th id="thProcessDPI" runat="server" class="header AlignCenter" style="border: 1px solid #E6EEEE;
                                                            height: 40px" colspan="2" rowspan="2">
                                                            <asp:Label ID="lblHeaderProcessDPI" runat="server" CssClass="AlignCenter" Text="Process DPI" />
                                                        </th>
                                                        <th id="thCalculationStatus" runat="server" class="header AlignCenter" style="border: 1px solid #E6EEEE;
                                                            height: 40px" rowspan="2">
                                                            <asp:Label ID="lblHeaderCalculationStatus" runat="server" CssClass="AlignCenter"
                                                                Text="Status" />
                                                        </th>
                                                        <th id="thRevenue" runat="server" class="header AlignCenter" style="border: 1px solid #E6EEEE;
                                                            height: 40px" rowspan="2">
                                                            <asp:Label ID="lblHeaderRevenue" runat="server" CssClass="AlignCenter" Text="Revenue" />
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <th style="height: 20px" class="AlignCenter">
                                                            <asp:Label ID="lblEngines" runat="server" Font-Size="Smaller" Text="Engines" />
                                                        </th>
                                                        <th style="height: 20px" class="AlignCenter">
                                                            <asp:Label ID="lblEmpties" runat="server" Font-Size="Smaller" CssClass="AlignCenter"
                                                                Text="Empties" />
                                                        </th>
                                                        <th style="height: 20px" class="AlignCenter">
                                                            <asp:Label ID="lblLoads" runat="server" Font-Size="Smaller" CssClass="AlignCenter"
                                                                Text="Loads" />
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="even" id="trJob" runat="server">
                                        <td style="width: 15px;">
                                            <div class="Expand" id="divExpand" visible="false" runat="server">
                                            </div>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblJobNumber" runat="server"></asp:Label>
                                            <asp:HiddenField ID="hfIdJob" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDivision" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCustomerResource" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblLocationDescription" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblJobAction" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCarCountEngines" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCarCountEmpties" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCarCountLoads" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkProcess" runat="server" OnClientClick="return false;" Text="Process"></asp:LinkButton>
                                        </td>
                                        <td style="width: 100px;">
                                            <asp:Label ID="lblDPIStatus" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCalculationStatus" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblRevenue" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="rptResources" runat="server" OnItemDataBound="rptResources_ItemDataBound">
                                        <ItemTemplate>
                                            <tr id="trResource" runat="server">
                                                <td style="width: 15px;">
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDivision" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCustomerResource" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblLocationDescription" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCalculationStatus" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblRevenue" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody> </table></div></div>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                        <asp:Panel ID="pnlNoRows" runat="server" Visible="true">
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
                    </div>
                    <br />
                    <div id="Footer">
                        <div class="space95 alignRight">
                            <div>
                                <asp:Label ID="lblRunningTotalTitle" runat="server" Text="Running Total:" Font-Bold="true"></asp:Label>
                                <asp:Label ID="lblRunningTotalValue" runat="server" Text="$ 0" CssClass="Revenue"></asp:Label>
                            </div>
                        </div>
                        <div class="space95 alignRight">
                            <div>
                                <asp:Label ID="lblTotalJobsTitle" runat="server" Text="Total of Jobs:" Font-Bold="true"></asp:Label>
                                <asp:Label ID="lblTotalJobsValue" runat="server" Text="0"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div id="Buttons" class="space100">
                        <div id="Left" class="space49 floatLeft alignLeft">
                            <%--Should not be displayed right now--%>
                            <%--<asp:Button ID="btnDPIReports" runat="server" Text="DPI Reports" class="btn"/>
                <asp:Button ID="btnHistoricalDPIData" runat="server" Text="View Historical DPI Job Data" class="btn"/>--%>
                        </div>
                        <div id="Right" class="space50 floatLeft alignRight">
                            <asp:Button ID="btnGenerateReport" runat="server" Text="Generate DPI Report" class="btn"
                                OnClick="btnGenerateReport_Click" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script language="javascript" type="text/javascript" defer="defer">
        //Instance of ScripManager
        var scriptManager = Sys.WebForms.PageRequestManager.getInstance();

        scriptManager.add_beginRequest(function () {
            BeginRequestGridPosition();
        });

        scriptManager.add_endRequest(function () {
            EndRequestGridPosition();

            initializePage();

            var comboControl = $find('<%= cbbProcessingType.ClientID %>');
            if (comboControl)
                ProcessingTypeFilter(comboControl);
        });

        Sys.Application.add_load(initializePage);

        function initializePage() {
            var comboControl = $find('<%= cbbProcessingType.ClientID %>');
            if (comboControl) {
                comboControl.add_propertyChanged(function (sender, e) {
                    if (e.get_propertyName() == 'selectedIndex') {
                        ProcessingTypeFilter(comboControl);
                    }
                });
            }
        }

        function ProcessingTypeFilter(comboControl) {
            if (comboControl != null) {
                var value = comboControl.get_textBoxControl().value;

                var divDatePicker = $get('divDatePicker');
                var divNewJobFilter = $get('divNewJobFilter');

                if (value == "New Jobs") {
                    divNewJobFilter.style.display = "inline";
                    divDatePicker.style.display = "none";
                }
                else if (value == "Continuing Jobs") {
                    divNewJobFilter.style.display = "none";
                    divDatePicker.style.display = "none";
                }
                else if (value == "Reprocess Jobs") {

                    divNewJobFilter.style.display = "none";
                    divDatePicker.style.display = "inline";
                }
            }
        }

        function ValidateDateProcessJob() {
            var dpReprocessDate = $get('<%=dpReprocessDate.TextBoxClientID %>');

            var date = new Date();
            var currentDate = date.format('MM/dd/yyyy');

            if (dpReprocessDate.value > currentDate) {
                alert("You can not select a date grather than current date");
                return false;
            }
        }

        $(document).ready(function () { SetGridHeight() });
        $(window).resize(function () { timeoutHeightId = setTimeout(SetGridHeight, 500); });

        // Keep Scroll Position for the Repeater
        var control, yPos;
        function BeginRequestGridPosition(sender, args) {
            control = $get('tbRepeaters_ScrollDiv');
            if (control) {
                yPos = control.scrollTop;
            }
        }

        function EndRequestGridPosition(sender, args) {
            control = $get('tbRepeaters_ScrollDiv');
            if (control) {
                control.scrollTop = yPos;
                SetGridHeight();
                FormatValues();
            }
        }

        var timeoutHeightId;

        function SetGridHeight() {
            var screenHeight = document.body.clientHeight - 20;

            var div = document.getElementById("divTotal");
            var totalHeight = div.offsetHeight;

            var heightGrid = parseInt($("#tbRepeaters_ScrollDiv").css('max-height').replace('px', ''));

            var heightVal = (screenHeight - totalHeight) + heightGrid;

            if (heightVal > 220)
                $("#tbRepeaters_ScrollDiv").css('max-height', heightVal);
            else
                $("#tbRepeaters_ScrollDiv").css('max-height', 220);

            if (timeoutHeightId)
                clearTimeout(timeoutHeightId);
        }

        // Collapse-Expand functionality
        function CollapseExpand(Button, selector) {
            var line = $("." + selector);
            var button = $("#" + Button);

            line.toggle();
            button.toggleClass("Expand");
            button.toggleClass("Collapse");
        }

        function FormatValues() {
            var list = $('.Revenue');

            for (var i = 0; i < list.length; i++) {
                current = list[i].innerHTML.replace('$', '').replace(',', '') * 1;
                list[i].innerHTML = formatCurrency(current);
            }
        }

    </script>
    <script src="Scripts/formatFunctions.js" language="javascript"></script>
</asp:Content>
