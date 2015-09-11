<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JobCallLog.ascx.cs"
    Inherits="Hulcher.OneSource.CustomerService.Web.UserControls.JobRecord.JobCallLog" %>
<script language="javascript" type="text/javascript">


    var panelId;
    var panelTop;
    var panelLeft;

    window.onresize = function () { panelTop = 0; panelLeft = 0; }

    function ShowToolTip(clientId) {
       
        var scnWid, scnHei;
        if (document.documentElement && document.documentElement.clientHeight) {

            scnWid = document.documentElement.offsetWidth;
            scnHei = document.documentElement.offsetHeight;
        }
        else if (document.body) {
            scnWid = document.body.offsetWidth;
            scnHei = document.body.offsetHeight;

        }
        var panelControl = document.getElementById(clientId);
        if (panelControl.style.display == 'none') {
            if (panelId != panelControl.id) {
                panelId = panelControl.id;
                panelTop = 0;
                panelLeft = 0;
            }

            panelControl.style.display = 'block';
            var yPos = window.event.clientY + panelControl.offsetHeight;
            if (yPos > scnHei) {

                if (panelTop == 0) {
                    var difference = yPos - scnHei;
                    panelControl.style.top = window.event.clientY - difference;

                    panelTop = panelControl.style.top;
                }
                else {
                    panelControl.style.top = panelTop;
                }
            }
            else {
                if (panelTop == 0) {
                    panelControl.style.top = window.event.clientY;

                    panelTop = panelControl.style.top;
                }
                else {
                    panelControl.style.top = panelTop;
                }
            }
            var xPos = window.event.clientX + panelControl.offsetWidth;
            if (xPos > scnWid) {
                if (panelLeft == 0) {
                    var difference = xPos - scnWid;
                    panelControl.style.left = window.event.clientX - difference - 37;

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
        };
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

        if (window.getSelection) {
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
</script>
<div class="JobCallLog">
    <asp:ValidationSummary ID="vsJobCallLog" runat="server" CssClass="errorbox" ValidationGroup="callLogFilter"
        HeaderText="Please correct the following information" EnableClientScript="true" />
    <div class="filter">
        <div class="title">
            <asp:Label ID="lblFilter" runat="server" Text="Filter Listing By:" /></div>
        <div class="control">
            <asp:UpdatePanel ID="upFiltro" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="combo">
                        <asp:ComboBox ID="cbbFilter" runat="server" CssClass="WindowsStyle" AutoCompleteMode="SuggestAppend"
                            DropDownStyle="DropDown" AutoPostBack="true" CaseSensitive="false" RenderMode="Inline"
                            OnSelectedIndexChanged="cbbFilter_SelectedIndexChanged">
                            <asp:ListItem Text="- Select One -" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Call Date" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Call Time" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Call Type" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Modified By" Value="4"></asp:ListItem>
                        </asp:ComboBox>
                    </div>
                    <div class="textbox">
                        <asp:TextBox ID="txtFilterValue" runat="server" CssClass="input"></asp:TextBox>
                        <asp:Button ID="btnFind" runat="server" Text="Find" OnClick="btnFind_Click" CausesValidation="true"
                            CssClass="btn" ValidationGroup="callLogFilter" />
                        <asp:MaskedEditExtender ID="meeFilterValueDate" runat="server" MaskType="Date" Mask="99/99/9999"
                            Enabled="false" TargetControlID="txtFilterValue">
                        </asp:MaskedEditExtender>
                        <asp:MaskedEditValidator ID="mevFilterValueDate" runat="server" ControlExtender="meeFilterValueDate"
                            ControlToValidate="txtFilterValue" ValidationGroup="callLogFilter" SetFocusOnError="true"
                            IsValidEmpty="true" EnableClientScript="true" Display="None" InvalidValueMessage="Job Call Log - The Filter Value format (Date) is invalid"></asp:MaskedEditValidator>
                        <asp:MaskedEditExtender ID="meeFilterValueTime" runat="server" MaskType="Time" Mask="99:99"
                            Enabled="false" TargetControlID="txtFilterValue">
                        </asp:MaskedEditExtender>
                        <asp:MaskedEditValidator ID="mevFilterValueTime" runat="server" ControlExtender="meeFilterValueTime"
                            ControlToValidate="txtFilterValue" ValidationGroup="callLogFilter" SetFocusOnError="true"
                            IsValidEmpty="true" EnableClientScript="true" Display="None" InvalidValueMessage="Job Call Log - The Filter Value format (Time) is invalid"></asp:MaskedEditValidator>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="gridview">
        <asp:UpdatePanel ID="upGridView" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnFind" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <asp:ScrollableGridView ID="sgvCallLog" runat="server" CssClass="ScrollableGridView"
                    AutoGenerateColumns="false" ShowFooter="false" OnRowDataBound="sgvCallLog_RowDataBound">
                    <Columns>
                        <asp:BoundField HeaderText="" DataField="ID" Visible="false" />
                        <asp:BoundField HeaderText="Call Date" DataField="CallDate" />
                        <asp:BoundField HeaderText="Call Time" DataField="CallDate" />
                        <asp:TemplateField HeaderText="Call Type">
                            <ItemTemplate>
                                <asp:Label ID="lblType" runat="server" Text='<%# Eval("CS_CallType.Description") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Modified By" DataField="ModifiedBy" />
                        <asp:TemplateField HeaderText="Details">
                            <ItemTemplate>
                                <div>
                                    <div style="max-height: 100px; overflow: hidden;">
                                        <asp:Label ID="lblDetails" runat="server"></asp:Label>
                                    </div>
                                    <asp:Panel ID="pnlToolTip" runat="server" Style="background-color: #FFFFFF; border: 1px solid #000;
                                        display: none; width: 400px; position: fixed; max-height: 300px; overflow-y: auto;
                                        overflow-x: hidden;">
                                        <asp:Label ID="lblTool" runat="server" />
                                    </asp:Panel>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink ID="hlUpdate" runat="server" Text="Update"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:ScrollableGridView>
                <asp:TextBox ID="txtParentUpdate" runat="server" OnTextChanged="Update" Style="display: none"></asp:TextBox>
            </ContentTemplate>
        </asp:UpdatePanel>
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
            <a id="lnkJobCloning" >Create Job Cloning</a></div>
        <div class="divRightClickItem" onclick="RightClickFirstAlert();">
            <a id="lnkFirstAlert">Add First Alert</a></div>
        <div class="divRightClickItem" onmousedown="Copy();" onmouseup="KeepSelection();">
            <a id="lnkCopy">Copy</a></div>
    </div>
</div>
