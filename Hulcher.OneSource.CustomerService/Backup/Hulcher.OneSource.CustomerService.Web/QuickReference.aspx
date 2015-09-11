<%@ Page Title="Quick Reference" Language="C#" MasterPageFile="~/ContentPage.master"
    AutoEventWireup="true" CodeBehind="QuickReference.aspx.cs" Inherits="Hulcher.OneSource.CustomerService.Web.QuickReference" %>

<%@ MasterType TypeName="Hulcher.OneSource.CustomerService.Web.ContentPage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Styles/Forms.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/QuickReferencePrint.css" media="print" rel="stylesheet"
        type="text/css" />
</asp:Content>
<asp:Content ID="PageContent" ContentPlaceHolderID="Content" runat="server">
    <div>
        <asp:Label ID="lblMainTitle" runat="server" Font-Size="Large" Font-Bold="true" Text="Quick Reference" />
    </div>
    <br />
    <div class="Header">
        <asp:Label ID="lblVisualization" runat="server" Text="Visualization"></asp:Label>
    </div>
    <div class="Content">
        <div class="filter">
            <div class="title">
                <asp:Label ID="lblFilter" runat="server" Text="Filter Listing By:"></asp:Label>
            </div>
            <div class="control">
                <div class="combo">
                    <asp:ComboBox ID="cbFilter" runat="server" CssClass="WindowsStyle" AutoCompleteMode="SuggestAppend"
                        DropDownStyle="DropDown" CaseSensitive="false" RenderMode="Inline">
                        <asp:ListItem Text="- Select One -" Value="0" />
                        <asp:ListItem Text="Division #" Value="1" />
                        <asp:ListItem Text="Division State" Value="2" />
                        <asp:ListItem Text="Combo #" Value="4" />
                        <asp:ListItem Text="Unit #" Value="3" />
                        <asp:ListItem Text="Status" Value="5" />
                        <asp:ListItem Text="Job Location" Value="6" />
                        <asp:ListItem Text="Call type" Value="7" />
                        <asp:ListItem Text="Job #" Value="8" />
                    </asp:ComboBox>
                </div>
                <div class="textbox">
                    <asp:TextBox ID="txtFilterValue" runat="server" CssClass="input" />
                    <asp:Button ID="btnFind" runat="server" Text="Find" CssClass="btn" OnClick="btnFind_Click" />
                    <input type="button" value="Print" class="btn" onclick="document.getElementById('tbRepeaters').cellSpacing='0'; window.print(); document.getElementById('tbRepeaters').cellSpacing='1';" />
                </div>
            </div>
        </div>
        <div id="divGrid" class="gridview">
            <asp:UpdatePanel ID="upGridView" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnFind" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <asp:HiddenField ID="hfOrderBy" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hfExpandedCombos" runat="server" />
                    <asp:Repeater ID="rptCombo" runat="server" OnItemDataBound="rptCombo_ItemDataBound">
                        <HeaderTemplate>
                            <div id="tbRepeaters_Group" class="ScrollableGridView_Group" style="width: 100%">
                                <div id="tbRepeaters_HeaderDiv" class="ScrollableGridView_HeaderDiv" style="min-width: 600px;">
                                </div>
                                <div id="tbRepeaters_ScrollDiv" class="ScrollableGridView_ScrollDiv" style="max-height: 450px;
                                    min-width: 600px;">
                                    <table id="tbRepeaters" class="ScrollableGridView" cellspacing="1">
                                        <thead>
                                            <tr style="position: relative; top: expression(this.offsetParent.scrollTop -1); left: expression(this.offsetParent.style.left);">
                                                <th id="thExpandCollapse" runat="server" class="header">
                                                    &nbsp;
                                                </th>
                                                <th id="thDivisionName" runat="server" class="header"
                                                    onclick="SetOrderBy('1', this);">
                                                    <asp:Label ID="rpt1Header1" CssClass="MarginRight" runat="server" Text="Div #"></asp:Label>
                                                </th>
                                                <th id="thDivisionState" runat="server" class="header" onclick="SetOrderBy('2', this);">
                                                    <asp:Label ID="rpt1Header2" CssClass="MarginRight" runat="server" Text="Div State"></asp:Label>
                                                </th>
                                                <th id="thComboName" runat="server" class="header" onclick="SetOrderBy('3', this);">
                                                    <asp:Label ID="rpt1Header3" CssClass="MarginRight" runat="server" Text="Combo #"></asp:Label>
                                                </th>
                                                <th id="thUnitNumber" runat="server" class="header" onclick="SetOrderBy('4', this);">
                                                    <asp:Label ID="rpt1Header4" CssClass="MarginRight" runat="server" Text="Unit #"></asp:Label>
                                                </th>
                                                <th id="thDescriptor" runat="server" class="header" onclick="SetOrderBy('5',this);">
                                                    <asp:Label ID="rpt1Header5" CssClass="MarginRight" runat="server" Text="Descriptor"></asp:Label>
                                                </th>
                                                <th id="thStatus" runat="server" class="header" onclick="SetOrderBy('6',this);">
                                                    <asp:Label ID="rpt1Header6" CssClass="MarginRight" runat="server" Text="Status"></asp:Label>
                                                </th>
                                                <th id="thJobLocation" runat="server" class="header" onclick="SetOrderBy('7',this);">
                                                    <asp:Label ID="rpt1Header7" CssClass="MarginRight" runat="server" Text="Job Location"></asp:Label>
                                                </th>
                                                <th id="thType" runat="server" class="header" onclick="SetOrderBy('8', this);">
                                                    <asp:Label ID="rpt1Header8" CssClass="MarginRight" runat="server" Text="Type"></asp:Label>
                                                </th>
                                                <th id="thOperationStatus" runat="server" class="header" onclick="SetOrderBy('9', this);">
                                                    <asp:Label ID="rpt1Header9" CssClass="MarginRight" runat="server" Text="Oper. Status"></asp:Label>
                                                </th>
                                                <th id="thJobNumber" runat="server" class="header" onclick="SetOrderBy('10', this);">
                                                    <asp:Label ID="rpt1Header10" CssClass="MarginRight" runat="server" Text="Job #"></asp:Label>
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="even">
                                <td style="width: 15px;">
                                    <div class="Expand" id="divExpand" runat="server">
                                    </div>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblDivisionName" CssClass="MarginRight" runat="server" Text="Div #"></asp:Label>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblDivisionState" CssClass="MarginRight" runat="server" Text="Div State"></asp:Label>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblComboName" CssClass="MarginRight" runat="server" Text="Combo #"></asp:Label>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblUnitNumber" CssClass="MarginRight" runat="server" Text="Unit #"></asp:Label>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblDescriptor" CssClass="MarginRight" runat="server" Text="Descriptor"></asp:Label>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblStatus" CssClass="MarginRight" runat="server" Text="Status"></asp:Label>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblJobLocation" CssClass="MarginRight" runat="server" Text="Job Location"></asp:Label>&nbsp;
                                </td>
                                <td>
                                    <asp:HyperLink ID="hlLastCallEntry" runat="server" Text='<%# Eval("Type") %>' NavigateUrl="javascript: alert('This Call Entry is automatic generated and can not be updated.');" />&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblOperationStatus" CssClass="MarginRight" runat="server" Text="Oper. Status"></asp:Label>&nbsp;
                                </td>
                                <td>
                                    <asp:LinkButton ID="lbJob" runat="server" Text='<%# Eval("JobNumber") %>'></asp:LinkButton>&nbsp;
                                </td>
                            </tr>
                            <asp:Repeater ID="rptEquipments" runat="server" OnItemDataBound="rptEquipments_ItemDataBound">
                                <ItemTemplate>
                                    <tr class="even" id="trEquipment" runat="server">
                                        <td style="width: 15px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDivisionName" CssClass="MarginRight" runat="server" Text="Div #"></asp:Label>&nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDivisionState" CssClass="MarginRight" runat="server" Text="Div State"></asp:Label>&nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="lblUnitNumber" CssClass="MarginRight" runat="server" Text="Unit #"></asp:Label>&nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDescriptor" CssClass="MarginRight" runat="server" Text="Descriptor"></asp:Label>&nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="lblStatus" CssClass="MarginRight" runat="server" Text="Status"></asp:Label>&nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="lblJobLocation" CssClass="MarginRight" runat="server" Text="Job Location"></asp:Label>&nbsp;
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="hlLastCallEntry" runat="server" Text='<%# Eval("Type") %>' NavigateUrl="javascript: alert('This Call Entry is automatic generated and can not be updated.');" />&nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="lblOperationStatus" CssClass="MarginRight" runat="server" Text="Oper. Status"></asp:Label>&nbsp;
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lbJob" runat="server" Text='<%# Eval("JobNumber") %>'></asp:LinkButton>&nbsp;
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody> </table></div></div>
                        </FooterTemplate>
                    </asp:Repeater>
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
                    <asp:Button ID="btnFakeSort" runat="server" OnClick="btnFakeSort_Click" Style="display: none;" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <script type="text/javascript" language="javascript" defer="defer">

        // Collapse-Expand functionality
        function CollapseExpand(Button, selector) {
            var line = $("." + selector);
            var button = $("#" + Button);
            var expandedItems = $get('<%=hfExpandedCombos.ClientID %>').value;

            line.toggle();
            button.toggleClass("Expand");
            button.toggleClass("Collapse");

            if (button.attr('class') == 'Expand')
                expandedItems = expandedItems.replace(';' + selector, '');
            else
                expandedItems += ";" + selector;
            $get('<%=hfExpandedCombos.ClientID %>').value = expandedItems;
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

        function OpenCallEntry(jobId, callLogId) {
            var newWindow = window.open('/CallEntry.aspx?JobId=' + jobId + '&CallEntryID=' + callLogId, '', 'width=800, height=600, scrollbars=1, resizable=yes');
        }

    </script>
</asp:Content>
