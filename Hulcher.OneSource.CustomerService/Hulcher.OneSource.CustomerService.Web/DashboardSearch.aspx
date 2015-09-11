<%@ Page Title="Dashboard - Search Results" Language="C#" MasterPageFile="~/ContentPage.master" AutoEventWireup="true"
    CodeBehind="DashboardSearch.aspx.cs" Inherits="Hulcher.OneSource.CustomerService.Web.DashboardSearch" %>

<%@ MasterType TypeName="Hulcher.OneSource.CustomerService.Web.ContentPage" %>
<%@ Register Src="UserControls/DatePicker.ascx" TagName="DatePicker" TagPrefix="asp" %>
<%@ Register Assembly="Hulcher.OneSource.CustomerService.Business" Namespace="Hulcher.OneSource.CustomerService.Business.WebControls.ServerControls"
    TagPrefix="asp" %>
<%@ Register Src="~/UserControls/AutoCompleteTextbox.ascx" TagName="AutoCompleteTextbox"
    TagPrefix="uc1" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
    <link href="../../Styles/Forms.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/DashBoard.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ContentPlaceHolderID="Content" ID="Content1" ContentPanelID="Content"
    runat="server">
    <div class="Header">
        Dashboard - Search Results
    </div>
    <div class="Content">
        <asp:UpdatePanel ID="upViewSummary" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExport" />
            </Triggers>
            <ContentTemplate>
                <asp:HiddenField ID="hfExpandedJobs" runat="server" />
                <asp:HiddenField ID="hfOrderBy" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfJobId" runat="server" />
                <asp:HiddenField ID="hfCallId" runat="server" />
                <br />
                <div id="divMain">
                    <div id="divFilter" class="divFilter">
                        <asp:Label ID="lblFilterViewBy" runat="server" Text="Selected Filters: " class="labels"
                            Style="font-weight: bold;"></asp:Label>
                            <br />                                   
                        <asp:Panel ID="pnlJobSummaryView" runat="server">
                            <asp:Label ID="lblFilterValues" runat="server"></asp:Label>
                        </asp:Panel>
                    </div>
                    <div id="divGrids">
                        <asp:Panel ID="pnlGridJobSummary" runat="server">
                            <asp:Repeater ID="rptJobSummary" runat="server" OnItemDataBound="rptJobSummary_ItemDataBound">
                                <HeaderTemplate>
                                    <div id="tbRepeaters_Group" class="ScrollableGridView_Group" style="width: 100%">
                                    <div id="tbRepeaters_HeaderDiv" class="ScrollableGridView_HeaderDiv" style="min-width: 400px;">
                                    </div>
                                    <div id="tbRepeaters_ScrollDiv" class="ScrollableGridView_ScrollDiv" style="max-height: 400px;
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
                        </asp:Panel>
                    </div>
                    <br />
                    <br />
                    <div id="buttons" class="buttons">
                        <asp:Button ID="btnPrint" runat="server" CssClass="btn" Text="Print Job Worksheet"
                            CausesValidation="false" OnClick="btnPrint_Click" />&nbsp;&nbsp;
                        <asp:Button ID="btnExport" runat="server" CssClass="btn" Text="Export Job Worksheet"
                            CausesValidation="false" OnClick="btnExport_Click" />
                    </div>
                    <div id="divRightClick" class="divRightClick" runat="server" clientidmode="Static">
                        <div class="divRightClickItem">
                            <a id="lnkAddResource" onclick="RightClickAddResource();">Add Resource</a></div>
                        <div class="divRightClickItem">
                            <a id="lnKUpdJobRecord" onclick="RightClickUpdJobRecord();">Update Job Record</a></div>
                        <div class="divRightClickItem">
                            <a id="lnKAddCallEntry" onclick="RightClickAddCallEntry();">Apply Call Entry</a></div>
                        <div class="divRightClickItem">
                            <a id="lnkPrintJobRecord" onclick="RightClickPrintJobRecord();">Print Job Record</a></div>
                        <div class="divRightClickItem">
                            <a id="lnkJobCloning" onclick="RighClickJobCloning();">Create Job Cloning</a></div>
                    </div>
                    <asp:Button ID="btnFakeSort" runat="server" OnClick="btnFakeSort_Click" Style="display: none;" />
                </div>
                <script type="text/javascript">

                    //Instance of ScripManager
                    var scriptManager = Sys.WebForms.PageRequestManager.getInstance();
                    scriptManager.add_beginRequest(BeginRequestGridPosition);
                    scriptManager.add_endRequest(EndRequestGridPosition);

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
                    function showDiv() {
                        document.getElementById('divRightClick').style.display = 'block';
                        document.getElementById('divRightClick').style.top = tempY + document.documentElement.scrollTop + 'px';
                        document.getElementById('divRightClick').style.left = tempX + 'px';
                        document.getElementById('divRightClick').focus();
                    }
                    function hideDiv() {
                        if (document.getElementById('divRightClick'))
                            document.getElementById('divRightClick').style.display = 'none';
                    }
                    function RightClickAddResource() {
                        var newWindow = window.open('/ResourceAllocation.aspx?JobId=' + jobId, '', 'width=800, height=600, scrollbars=1, resizable=yes');
                    }

                    function RightClickUpdJobRecord() {
                        var newWindow = window.open('/JobRecord.aspx?JobId=' + jobId, '', 'width=870, height=600, scrollbars=1, resizable=yes');
                    }

                    function RightClickAddCallEntry() {
                        var newWindow = window.open('/CallEntry.aspx?JobId=' + jobId, '', 'width=800, height=600, scrollbars=1, resizable=yes');
                    }

                    function RightClickPrintJobRecord() {
                        var newWindow = window.open('/JobRecordPrint.aspx?JobId=' + jobId, '', 'width=870, height=600, scrollbars=1, resizable=yes');
                    }

                    function RighClickJobCloning() {
                        if (confirm('Are you sure you want to clone this job? The data will be copied over into a new record.'))
                            var newWindow = window.open('/JobRecord.aspx?CloningId=' + jobId, '', 'width=870, height=600, scrollbars=1, resizable=yes');
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

                    // Select line functionality / Skip funcionality
                    var CurrentId;
                    var CurrentJobIndex = 0;
                    var CurrentDivisionIndex = 0;
                    var windowHasFocus = true;
                    if (/*@cc_on!@*/false) { // check for Internet Explorer
                        document.onfocusout = function () { windowHasFocus = false; };
                        document.onfocusin = function () { windowHasFocus = true; };
                    } else {
                        window.onblur = function () { windowHasFocus = false; };
                        window.onfocus = function () { windowHasFocus = true; };
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


                    function onChecked(checkbox, jobid, callid) {
                        var JobID = document.getElementById('<%= hfJobId.ClientID %>');
                        if (JobID.value == "") {
                            JobID.value = jobid;
                            document.getElementById('<%= hfCallId.ClientID %>').value += callid + ';';
                        }
                        else {
                            if (JobID.value == jobid) {
                                if (checkbox.checked) {
                                    document.getElementById('<%= hfCallId.ClientID %>').value += callid + ';';
                                }
                                else {
                                    var callLogId = document.getElementById('<%= hfCallId.ClientID %>').value;

                                    var lstCallLogId = callLogId.split(';');

                                    var callogIds = '';

                                    for (var i = 0; i < lstCallLogId.length; i++) {
                                        if (lstCallLogId[i] != '') {
                                            if (callid != lstCallLogId[i]) {
                                                callogIds += lstCallLogId[i] + ';';
                                            }
                                        }
                                    }

                                    if (callogIds != '') {
                                        document.getElementById('<%= hfCallId.ClientID %>').value = callogIds;
                                    }
                                    else {
                                        JobID.value = '';
                                        document.getElementById('<%= hfCallId.ClientID %>').value = '';
                                    }
                                }
                            }
                            else {
                                alert("You can't select call logs for different job.");
                                checkbox.checked = false;
                            }
                        }
                    }
                </script>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
