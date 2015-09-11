<%@ Page Title="Resource Transfer" Language="C#" MasterPageFile="~/ContentPage.master"
    AutoEventWireup="true" CodeBehind="ResourceTransfer.aspx.cs" Inherits="Hulcher.OneSource.CustomerService.Web.ResourceTransfer" %>

<%@ MasterType TypeName="Hulcher.OneSource.CustomerService.Web.ContentPage" %>
<%@ Register Src="~/UserControls/AutoCompleteTextbox.ascx" TagName="AutoCompleteTextbox"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div style="padding-bottom: 10px; height: 20px;">
        <asp:Label ID="lblPageTitle" runat="server" Font-Size="Large" Font-Bold="true" Text="Resource Transfer" />
    </div>
    <asp:ValidationSummary ID="vsResourceTransfer" runat="server" CssClass="errorbox"
        ValidationGroup="ResourceTransfer" HeaderText="Please correct the following information" />
    <asp:UpdatePanel ID="upMain" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="Page">
                <div class="Header">
                    <asp:Label ID="lblTitle" runat="server" Text="Resource Selection" />
                </div>
                <div class="Content">
                    <div>
                        <asp:Label ID="lblDestinationJob" runat="server" Text="Select the destination Job: "></asp:Label>
                        <uc1:AutoCompleteTextbox ID="actJobNumber" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                            TextBoxWidth="120px" GridViewIdName="ID" DisplayField="" AutoPostBack="false"
                            RequiredField="true" WindowTitle="Resource Transfer - Find Job Number" ErrorMessage="Job Number field is required"
                            AutoCompleteSource="JobNumberWithGeneral" ColumnHeaderList="Number,Company,Location" ColumnValueList="PrefixedNumber,CS_CustomerInfo.CS_Customer.Name,CS_LocationInfo.FullLocation"
                            ServiceMethod="GetJobNumberListWithGeneral" ValidationGroup="ResourceTransfer" TextBoxCssClass="input" ScriptToExecute="VerifyIfJobIsActive" />
                        <asp:HiddenField ID="hidHasEquipments" runat="server" />
                    </div>
                    <br />
                    <div>
                        <asp:Label ID="lblResourcesGrid" runat="server" Text="Select the related Call logs to transfer:"></asp:Label>
                        <asp:Repeater ID="rptResources" runat="server" OnItemDataBound="rptResources_ItemDataBound">
                            <HeaderTemplate>
                                <div id="tbRepeaters_Group" class="ScrollableGridView_Group" style="width: 100%">
                                    <div id="tbRepeaters_HeaderDiv" class="ScrollableGridView_HeaderDiv" style="min-width: 400px;">
                                    </div>
                                    <div id="tbRepeaters_ScrollDiv" class="ScrollableGridView_ScrollDiv" style="max-height: 220px;
                                        min-width: 400px;">
                                        <table id="tbRepeaters" class="ScrollableGridView" cellspacing="1">
                                            <thead>
                                                <tr>
                                                    <th id="thResourceType" runat="server" class="header" style="border: 1px solid #E6EEEE;
                                                        cursor: default;">
                                                        <asp:Label ID="lblHeaderType" runat="server" CssClass="MarginRight" Text="Resource Type" />
                                                    </th>
                                                    <th id="thResourceName" runat="server" class="header" style="border: 1px solid #E6EEEE;
                                                        cursor: default;">
                                                        <asp:Label ID="lblHeaderName" runat="server" CssClass="MarginRight" Text="Name" />
                                                    </th>
                                                    <th id="thCallType" runat="server" class="header" style="border: 1px solid #E6EEEE;
                                                        cursor: default;">
                                                        <asp:Label ID="lblHeaderCallType" runat="server" CssClass="MarginRight" Text="Call Type" />
                                                    </th>
                                                    <th id="thCalledInBy" runat="server" class="header" style="border: 1px solid #E6EEEE;
                                                        cursor: default;">
                                                        <asp:Label ID="lblHeaderCalledInBy" runat="server" CssClass="MarginRight" Text="Called In By" />
                                                    </th>
                                                    <th id="thCallDate" runat="server" class="header" style="border: 1px solid #E6EEEE;
                                                        cursor: default;">
                                                        <asp:Label ID="lblHeaderCallDate" runat="server" CssClass="MarginRight" Text="Call Date" />
                                                    </th>
                                                    <th id="thCallTime" runat="server" class="header" style="border: 1px solid #E6EEEE;
                                                        cursor: default;">
                                                        <asp:Label ID="lblHeaderCallTime" runat="server" CssClass="MarginRight" Text="Call Time" />
                                                    </th>
                                                    <th id="thModifiedBy" runat="server" class="header" style="border: 1px solid #E6EEEE;
                                                        cursor: default;">
                                                        <asp:Label ID="lblHeaderModifiedBy" runat="server" CssClass="MarginRight" Text="Modified By" />
                                                    </th>
                                                    <th id="thNote" runat="server" class="header" style="border: 1px solid #E6EEEE; cursor: default;">
                                                        <asp:Label ID="lblHeadeNote" runat="server" CssClass="MarginRight" Text="Details" />
                                                    </th>
                                                    <th id="thCheckCallLog" runat="server" class="header" style="border: 1px solid #E6EEEE;
                                                        cursor: default;">
                                                        &nbsp;
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="even">
                                    <td style="vertical-align: middle;">
                                        <asp:HiddenField ID="hfResourceId" runat="server" />
                                        <asp:Label ID="lblResourceType" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td style="vertical-align: middle;">
                                        <asp:Label ID="lblResourceName" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td colspan="7" style="text-align: right;">
                                        Transfered from: &nbsp;
                                        <asp:RadioButtonList ID="rblTransferType" runat="server" RepeatDirection="Horizontal"
                                            RepeatLayout="Flow" ValidationGroup="ResourceTransfer">
                                            <asp:ListItem Text="Existing Job Location" Value="1" Selected="True" />
                                            <asp:ListItem Text="Specific Location" Value="2" />
                                            <asp:ListItem Text="Division Location" Value="3" />
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="rfvTransferType" runat="server" ControlToValidate="rblTransferType"
                                            Display="Dynamic" ErrorMessage="You must select the transfer type for each resource"
                                            ValidationGroup="ResourceTransfer" Text="*" />
                                    </td>
                                </tr>
                                <asp:Repeater ID="rptCallLogs" runat="server" OnItemDataBound="rptCallLogs_ItemDataBound">
                                    <ItemTemplate>
                                        <tr id="trCallEntry" runat="server" class="Child">
                                            <td>
                                                <asp:HiddenField id="hidCallId" runat="server"/>
                                                <asp:HiddenField id="hidCallLastModification" runat="server"/>
                                            </td>
                                            <td>
                                                &nbsp;
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
                                                <div style="max-height: 100px; overflow: hidden;">
                                                    <asp:Label ID="lblNotes" runat="server" Text=""></asp:Label>
                                                </div>
                                                <asp:Panel ID="pnlToolTip" runat="server" Style="background-color: #FFFFFF; border: 1px solid #000;
                                                    display: none; width: 400px; position: fixed; max-height: 300px; overflow-y: auto;
                                                    overflow-x: hidden;" CssClass="tooltip">
                                                    <asp:Label ID="lblTool" runat="server" />
                                                </asp:Panel>
                                            </td>
                                            <td class="links" style="text-align: center;">
                                                <asp:CheckBox ID="chkTransfer" runat="server" />
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
                    <br />
                    <div id="divButtons" style="text-align: right; width: 100%;">
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn" CausesValidation="false"
                            OnClientClick="window.close(); return false;" UseSubmitBehavior="false" />
                        <asp:Button ID="btnTransfer" runat="server" Text="Transfer" OnClick="btnTransfer_Click"
                            CssClass="btn" ValidationGroup="ResourceTransfer" />
                    </div>
                    <br />
                </div>
            </div>

            <script language="javascript" type="text/javascript" language="javascript" defer="defer">

                // ToolTip for the Details of a Call Log
                var panelId;
                var panelTop;
                var panelLeft;

                window.onresize = function () { panelTop = 0; panelLeft = 0; }

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

                function VerifyIfJobIsActive(jobId) {
                    if (jobId != 0) {
                        tempuri.org.IJSONService.GetJobStatus(jobId, GetJobStatusCompleted);
                    }
                }

                function GetJobStatusCompleted(WebServiceResult) {
                    var jobStatusId = WebServiceResult.Id;
                    var activeJobStatusId = <%=(int)Hulcher.OneSource.CustomerService.Core.Globals.JobRecord.JobStatus.Active %>;
                    var presetPurchaseJobStatusId = <%=(int)Hulcher.OneSource.CustomerService.Core.Globals.JobRecord.JobStatus.PresetPurchase %>;
                    if (jobStatusId != activeJobStatusId &&
                        jobStatusId != presetPurchaseJobStatusId) {
                        
                        if ($('#<%=hidHasEquipments.ClientID %>').val() == "True") {
                            alert('You cannot transfer Equipments for non-active jobs.');

                            var actJobNumber = $find("actJobNumber");
                            actJobNumber.raiseItemSelected(new Sys.Extended.UI.AutoCompleteItemEventArgs(null, "", "0"));
                        }
                    }
                    
                }

                function EnableEquipments(enable) {
                    alert(enable);
                }

            </script>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
