<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DashboardPrint.aspx.cs"
    Inherits="Hulcher.OneSource.CustomerService.Web.DashboardPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dashboard - Print Version</title>
    <link href="../../Styles/sorter.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/DashBoard.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Forms.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.6.2.min.js" type="text/javascript"></script>
    <style type="text/css">
        .Label
        {
            font-size: 12px;
            font-weight: bold;
            color: Black;
        }
        .landScape
        {
            width: 100%;
            height: 100%;
            margin: 0% 0% 0% 0%;
            filter: progid:DXImageTransform.Microsoft.BasicImage(Rotation=3);
        }
        .noPrint
        {
            display: none;
        }
    </style>
    <script type="text/javascript">

        $(document).ready(function () {

            alert('Please verify that your print settings have a Landscape orientation and minimum margins.');

            self.print();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="viewPoint0" runat="server" Visible="false">
        <asp:Label ID="lblFiltersTitleCallLog" runat="server" Text="- Filter Values -" CssClass="Label"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblJobStatusCallLog" runat="server" Text="Job Status: " CssClass="Label"></asp:Label><br />
        <asp:Label ID="lblCallTypeCallLog" runat="server" Text="Call Type: " CssClass="Label"></asp:Label><br />
        <asp:Label ID="lblModifiedByCallLog" runat="server" Text="Modified By: " CssClass="Label"></asp:Label><br />
        <asp:Label ID="lblDivisionCallLog" runat="server" Text="Division: " CssClass="Label"></asp:Label><br />
        <asp:Label ID="lblPersonCallLog" runat="server" Text="Person: " CssClass="Label"></asp:Label><br />
        <asp:Label ID="lblStartDateCallLog" runat="server" Text="Start Date: " CssClass="Label"></asp:Label><br />
        <asp:Label ID="lblEndDateCallLog" runat="server" Text="End Date: " CssClass="Label"></asp:Label><br />
        <br />
        <asp:Repeater ID="rptCallLogSummaryDivision" runat="server" OnItemDataBound="rptCallLogSummaryDivision_ItemDataBound"
            EnableViewState="false">
            <HeaderTemplate>
                <table id="tbRepeaters" cellspacing="0" rules="all" border="1" style="border-collapse: collapse;">
                    <tr>
                        <th id="thDiv" runat="server" scope="col">
                            <asp:Label ID="rpt1Header1" CssClass="MarginRight" runat="server" Text="Div"></asp:Label>
                        </th>
                        <th id="thJobNumber" runat="server" scope="col">
                            <asp:Label ID="rpt1Header2" CssClass="MarginRight" runat="server" Text="Job#"></asp:Label>
                        </th>
                        <th id="thCustomer" runat="server" scope="col">
                            <asp:Label ID="rpt1Header3" CssClass="MarginRight" runat="server" Text="Company"></asp:Label>
                        </th>
                        <th id="thCallType" runat="server" scope="col">
                            <asp:Label ID="rpt1Header4" CssClass="MarginRight" runat="server" Text="Call Type"></asp:Label>
                        </th>
                        <th id="thCalledInBy" runat="server" scope="col">
                            <asp:Label ID="rpt1Header5" CssClass="MarginRight" runat="server" Text="Called In By"></asp:Label>
                        </th>
                        <th id="thCallDate" runat="server" scope="col">
                            <asp:Label ID="rpt1Header6" CssClass="MarginRight" runat="server" Text="Call Date"></asp:Label>
                        </th>
                        <th id="thCallTime" runat="server" scope="col">
                            <asp:Label ID="rpt1Header7" CssClass="MarginRight" runat="server" Text="Call Time"></asp:Label>
                        </th>
                        <th id="thModifiedBy" runat="server" scope="col">
                            <asp:Label ID="rpt1Header8" CssClass="MarginRight" runat="server" Text="Modified By"></asp:Label>
                        </th>
                        <th id="thDetails" runat="server" scope="col">
                            <asp:Label ID="rpt1Header9" CssClass="MarginRight" runat="server" Text="Details"></asp:Label>
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td colspan="10">
                        <asp:Label ID="lblDivision" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <asp:Repeater ID="rptCallLogSummaryJob" runat="server" OnItemDataBound="rptCallLogSummaryJob_ItemDataBound"
                    EnableViewState="false">
                    <ItemTemplate>
                        <tr class="jobCallLogTable Job">
                            <td>
                                &nbsp
                            </td>
                            <td colspan="9">
                                <asp:Label ID="lblJobNumberCustomer" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <asp:Repeater ID="rptCallLogSummaryCallEntry" runat="server" OnItemDataBound="rptCallLogSummaryCallEntry_ItemDataBound"
                            EnableViewState="false">
                            <ItemTemplate>
                                <tr id="trCallEntry" runat="server">
                                    <td>
                                        <input type="hidden" id="hidCallId" runat="server" name="hidCallId" />
                                        <input type="hidden" id="hidCallLastModification" runat="server" name="hidCallLastModification" />
                                    </td>
                                    <td>
                                        &nbsp
                                    </td>
                                    <td>
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
                                        <asp:Label ID="lblDetails" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ItemTemplate>
                </asp:Repeater>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </asp:Panel>
    <asp:Panel ID="viewPoint1" runat="server" Visible="false">
        <asp:Label ID="lblFiltersTitle" runat="server" Text="- Filter Values -" CssClass="Label"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblJobStatus" runat="server" Text="Job Status: " CssClass="Label"></asp:Label><br />
        <asp:Label ID="lblJobNumber" runat="server" Text="Job Number: " CssClass="Label"></asp:Label><br />
        <asp:Label ID="lblDivision" runat="server" Text="Division: " CssClass="Label"></asp:Label><br />
        <asp:Label ID="lblCustomer" runat="server" Text="Company: " CssClass="Label"></asp:Label><br />
        <asp:Label ID="lblPerson" runat="server" Text="Person: " CssClass="Label"></asp:Label><br />
        <asp:Label ID="lblDateType" runat="server" Text="Date Type: " CssClass="Label"></asp:Label><br />
        <asp:Label ID="lblStartDate" runat="server" Text="Start Date: " CssClass="Label"></asp:Label><br />
        <asp:Label ID="lblEndDate" runat="server" Text="End Date: " CssClass="Label"></asp:Label><br />
        <br />
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
                                    <th id="thDivision" runat="server" class="header" style="border: 1px solid #E6EEEE;
                                        margin: 0 20px 0 0">
                                        <asp:Label ID="lblHeaderDivision" runat="server" CssClass="MarginRight" Text="Division" />
                                    </th>
                                    <th id="thJobNumber" runat="server" class="header" style="border: 1px solid #E6EEEE;">
                                        <asp:Label ID="lblHeaderJobNumber" runat="server" CssClass="MarginRight" Text="Job Number" />
                                    </th>
                                    <th id="thCustomerResource" runat="server" class="header" style="border: 1px solid #E6EEEE;">
                                        <asp:Label ID="lblHeaderCustomerResources" runat="server" CssClass="MarginRight"
                                            Text="Company / Resources" />
                                    </th>
                                    <th id="thJobStatus" runat="server" class="header" style="border: 1px solid #E6EEEE;">
                                        <asp:Label ID="lblHeaderStatus" runat="server" CssClass="MarginRight" Text="Status" />
                                    </th>
                                    <th id="thLocation" runat="server" class="header" style="border: 1px solid #E6EEEE;">
                                        <asp:Label ID="lblHeaderLocation" runat="server" CssClass="MarginRight" Text="Location" />
                                    </th>
                                    <th id="thProjectManager" runat="server" class="header" style="border: 1px solid #E6EEEE;">
                                        <asp:Label ID="lblHeaderProjectManager" runat="server" CssClass="MarginRight" Text="Project Manager" />
                                    </th>
                                    <th id="thModifiedBy" runat="server" class="header" style="border: 1px solid #E6EEEE;">
                                        <asp:Label ID="lblHeaderModifiedBy" runat="server" CssClass="MarginRight" Text="Modified By" />
                                    </th>
                                    <th id="thLastModification" runat="server" class="header" style="border: 1px solid #E6EEEE;">
                                        <asp:Label ID="lblHeaderLastModification" runat="server" CssClass="MarginRight" Text="Last Modification" />
                                    </th>
                                    <th id="thInitialCallDate" runat="server" class="header" style="border: 1px solid #E6EEEE;">
                                        <asp:Label ID="lblHeaderInitialCallDate" runat="server" CssClass="MarginRight" Text="Initial Call Date" />
                                    </th>
                                    <th id="thPresetDate" runat="server" class="header" style="border: 1px solid #E6EEEE;">
                                        <asp:Label ID="lblHeaderPreset" runat="server" CssClass="MarginRight" Text="Preset" />
                                    </th>
                                    <th id="thLastCallType" runat="server" class="header" style="border: 1px solid #E6EEEE;">
                                        <asp:Label ID="lblHeaderLastCallType" runat="server" CssClass="MarginRight" Text="Last Call Type" />
                                    </th>
                                    <th id="thLastCallDate" runat="server" class="header" style="border: 1px solid #E6EEEE;">
                                        <asp:Label ID="lblHeaderLastCallDateTime" runat="server" CssClass="MarginRight" Text="Last Call Date Time" />
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="even" id="trJob" runat="server">
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
                        <asp:Label ID="lblLastCallEntry" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblLastCallDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <asp:Repeater ID="rptJobSummaryResources" runat="server" OnItemDataBound="rptJobSummaryResources_ItemDataBound">
                    <ItemTemplate>
                        <tr id="trResource" runat="server">
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
                                <asp:Label ID="lblLastCallEntry" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblLastCallDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </ItemTemplate>
            <FooterTemplate>
                </tbody> </table></div></div>
            </FooterTemplate>
        </asp:Repeater>
    </asp:Panel>
    <asp:Panel ID="viewPoint2" runat="server" Visible="false">
        <asp:Label ID="lblFiltersTitleSearch" runat="server" Text="- Filter Values -" CssClass="Label"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblSearchContactInfo" runat="server" Text="Contact Info: " CssClass="Label"></asp:Label><br />
        <asp:Label ID="lblSearchJobInfo" runat="server" Text="Job Info: " CssClass="Label"></asp:Label><br />
        <asp:Label ID="lblSearchLocationInfo" runat="server" Text="Location Info: " CssClass="Label"></asp:Label><br />
        <asp:Label ID="lblSearchJobDescription" runat="server" Text="Job Description: " CssClass="Label"></asp:Label><br />
        <asp:Label ID="lblSearchEquipmentType" runat="server" Text="Equipment Type: " CssClass="Label"></asp:Label><br />
        <asp:Label ID="lblSearchResource" runat="server" Text="Resource: " CssClass="Label"></asp:Label><br />
        <asp:Label ID="lblSearchDateRange" runat="server" Text="Date Range: " CssClass="Label"></asp:Label><br />
        <br />
        <asp:Repeater ID="rptJobSummarySearch" runat="server" OnItemDataBound="rptJobSummarySearch_ItemDataBound">
            <HeaderTemplate>
                <div id="tbRepeaters_Group" class="ScrollableGridView_Group" style="width: 100%">
                    <div id="tbRepeaters_HeaderDiv" class="ScrollableGridView_HeaderDiv" style="min-width: 400px;">
                    </div>
                    <div id="tbRepeaters_ScrollDiv" class="ScrollableGridView_ScrollDiv" style="max-height: 400px;
                        min-width: 400px;">
                        <table id="tbRepeaters" class="ScrollableGridView" cellspacing="1">
                            <thead>
                                <tr style="position: relative; top: expression(this.offsetParent.scrollTop -1); left: expression(this.offsetParent.style.left);">
                                    <th id="thDivision" runat="server" class="header" style="border: 1px solid #E6EEEE;
                                        margin: 0 20px 0 0">
                                        <asp:Label ID="lblHeaderDivision" runat="server" CssClass="MarginRight" Text="Division" />
                                    </th>
                                    <th id="thJobNumber" runat="server" class="header" style="border: 1px solid #E6EEEE;">
                                        <asp:Label ID="lblHeaderJobNumber" runat="server" CssClass="MarginRight" Text="Job Number" />
                                    </th>
                                    <th id="thCustomerResource" runat="server" class="header" style="border: 1px solid #E6EEEE;">
                                        <asp:Label ID="lblHeaderCustomerResources" runat="server" CssClass="MarginRight"
                                            Text="Company / Resources" />
                                    </th>
                                    <th id="thJobStatus" runat="server" class="header" style="border: 1px solid #E6EEEE;">
                                        <asp:Label ID="lblHeaderStatus" runat="server" CssClass="MarginRight" Text="Status" />
                                    </th>
                                    <th id="thLocation" runat="server" class="header" style="border: 1px solid #E6EEEE;">
                                        <asp:Label ID="lblHeaderLocation" runat="server" CssClass="MarginRight" Text="Location" />
                                    </th>
                                    <th id="thProjectManager" runat="server" class="header" style="border: 1px solid #E6EEEE;">
                                        <asp:Label ID="lblHeaderProjectManager" runat="server" CssClass="MarginRight" Text="Project Manager" />
                                    </th>
                                    <th id="thModifiedBy" runat="server" class="header" style="border: 1px solid #E6EEEE;">
                                        <asp:Label ID="lblHeaderModifiedBy" runat="server" CssClass="MarginRight" Text="Modified By" />
                                    </th>
                                    <th id="thLastModification" runat="server" class="header" style="border: 1px solid #E6EEEE;">
                                        <asp:Label ID="lblHeaderLastModification" runat="server" CssClass="MarginRight" Text="Last Modification" />
                                    </th>
                                    <th id="thInitialCallDate" runat="server" class="header" style="border: 1px solid #E6EEEE;">
                                        <asp:Label ID="lblHeaderInitialCallDate" runat="server" CssClass="MarginRight" Text="Initial Call Date" />
                                    </th>
                                    <th id="thPresetDate" runat="server" class="header" style="border: 1px solid #E6EEEE;">
                                        <asp:Label ID="lblHeaderPreset" runat="server" CssClass="MarginRight" Text="Preset" />
                                    </th>
                                    <th id="thLastCallType" runat="server" class="header" style="border: 1px solid #E6EEEE;">
                                        <asp:Label ID="lblHeaderLastCallType" runat="server" CssClass="MarginRight" Text="Last Call Type" />
                                    </th>
                                    <th id="thLastCallDate" runat="server" class="header" style="border: 1px solid #E6EEEE;">
                                        <asp:Label ID="lblHeaderLastCallDateTime" runat="server" CssClass="MarginRight" Text="Last Call Date Time" />
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="even" id="trJob" runat="server">
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
                </tr>
                <asp:Repeater ID="rptJobSummarySearchResources" runat="server" OnItemDataBound="rptJobSummarySearchResources_ItemDataBound">
                    <ItemTemplate>
                        <tr id="trResource" runat="server">
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
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </ItemTemplate>
            <FooterTemplate>
                </tbody> </table></div></div>
            </FooterTemplate>
        </asp:Repeater>
    </asp:Panel>
    </form>
</body>
</html>
