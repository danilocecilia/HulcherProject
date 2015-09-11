<%@ Page Title="Company / Contact Request" Language="C#" MasterPageFile="~/ContentPage.master"
    AutoEventWireup="true" CodeBehind="CustomerRequest.aspx.cs" Inherits="Hulcher.OneSource.CustomerService.Web.CustomerRequest" %>

<%@ MasterType TypeName="Hulcher.OneSource.CustomerService.Web.ContentPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/Forms.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <asp:UpdatePanel ID="upPage" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hfExpandedItems" runat="server" />
            <asp:HiddenField ID="hfOrderBy" runat="server" ClientIDMode="Static" />
            <asp:Button ID="btnFakeSort" runat="server" OnClick="btnFakeSort_Click" Style="display: none;" />
            <div style="padding-bottom: 10px; height: 20px;">
                <asp:Label ID="lblTitle" runat="server" Text="Company / Contact Request" Font-Size="Large"
                    Font-Bold="true"></asp:Label>
            </div>
            <asp:Panel ID="pnlVisualization" runat="server" DefaultButton="btnFind">
                <div id="divVisualization" class="Header">
                    <asp:Label ID="lblVisualizationTitle" runat="server" Text="Company / Contact Request"></asp:Label>
                </div>
                <div class="Content">
                    <div id="divFilter">
                        <div class="inlineBlock floatRight alignRight">
                            <div class="floatLeft paddingTop5 paddingRight5">
                                <asp:Label ID="lblFilter" runat="server" Text="Filter Listing By: " />
                            </div>
                            <div class="floatLeft paddingRight5">
                                <asp:ComboBox ID="ddlFilterType" runat="server" CssClass="WindowsStyle" AutoCompleteMode="SuggestAppend"
                                    DropDownStyle="DropDown" CaseSensitive="false" RenderMode="Inline">
                                    <asp:ListItem Text="- Select One -" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Company Name" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Contact Name" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Status" Value="3"></asp:ListItem>
                                </asp:ComboBox>
                            </div>
                            <div class="floatLeft paddingRight5">
                                <asp:TextBox ID="txtFilterValue" runat="server" CssClass="input" />
                            </div>
                            <div class="floatLeft paddingRight5 paddingTop2">
                                <asp:Button ID="btnFind" runat="server" Text="Find" CssClass="btn" CausesValidation="false"
                                    OnClick="btnFind_Click" />
                            </div>
                        </div>
                    </div>
                    <div id="divGrid">
                        <asp:Repeater ID="rptRequest" runat="server" OnItemDataBound="rptRequest_ItemDataBound"
                            OnItemCommand="rptRequest_ItemCommand">
                            <HeaderTemplate>
                                <div id="tbRepeaters_Group" class="ScrollableGridView_Group" style="width: 100%">
                                    <div id="tbRepeaters_HeaderDiv" class="ScrollableGridView_HeaderDiv" style="min-width: 400px;">
                                    </div>
                                    <div id="tbRepeaters_ScrollDiv" class="ScrollableGridView_ScrollDiv" style="max-height: 450px;
                                        min-width: 400px;">
                                        <table id="tbRepeaters" class="ScrollableGridView" cellspacing="1">
                                            <thead>
                                                <tr style="position: relative; top: expression(this.offsetParent.scrollTop -1); left: expression(this.offsetParent.style.left);">
                                                    <th id="thExpandCollapse" runat="server" class="header" style="border: 1px solid #E6EEEE;">
                                                        &nbsp;
                                                    </th>
                                                    <th id="thDate" runat="server" class="header" style="width: 20%;" onclick="SetOrderBy('1', this);">
                                                        <asp:Label ID="lblDate" CssClass="MarginRight" runat="server" Text="Date"></asp:Label>
                                                    </th>
                                                    <th id="thRequestedBy" runat="server" class="header" style="width: 20%;" onclick="SetOrderBy('2', this);">
                                                        <asp:Label ID="lblRequestedBy" CssClass="MarginRight" runat="server" Text="Requested By"></asp:Label>
                                                    </th>
                                                    <th id="thType" runat="server" class="header" style="width: 10%;" onclick="SetOrderBy('3', this);">
                                                        <asp:Label ID="lblType" CssClass="MarginRight" runat="server" Text="Type"></asp:Label>
                                                    </th>
                                                    <th id="thCustomerContactName" runat="server" class="header" style="width: 30%;"
                                                        onclick="SetOrderBy('4', this);">
                                                        <asp:Label ID="lblCustomerContactName" CssClass="MarginRight" runat="server" Text="Company / Contact Name"></asp:Label>
                                                    </th>
                                                    <th id="thStatus" runat="server" class="header" style="width: 10%;" onclick="SetOrderBy('5', this);">
                                                        <asp:Label ID="lblStatus" CssClass="MarginRight" runat="server" Text="Status"></asp:Label>
                                                    </th>
                                                    <th id="thRemove" runat="server" class="header" style="width: 10%;">
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="even">
                                    <td style="width: 15px;">
                                        <div class="Expand" id="divExpand" runat="server">
                                        </div>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDate" runat="server"></asp:Label>
                                        <asp:HiddenField ID="hfRequestID" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblRequestedBy" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblType" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCustomerContactName" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="DeleteRequest"
                                            CommandArgument="" CssClass="linkButtonStyle" />
                                        <asp:Button ID="btnResend" runat="server" Text="Resend" CommandName="ResendRequest"
                                            CommandArgument="" CssClass="linkButtonStyle" />
                                    </td>
                                </tr>
                                <tr id="trNotes" runat="server" class="odd" style="display: none;">
                                    <td>
                                    </td>
                                    <td colspan="5">
                                        <asp:Label ID="lblNotes" runat="server" />
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody></table></div></div>
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
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript" language="javascript" defer="defer">
        // Collapse-Expand functionality
        function CollapseExpand(Button, selector) {

            var line = $("." + selector);
            line.toggle();

            var button = $("#" + Button);
            button.toggleClass("Expand");
            button.toggleClass("Collapse");

            var expandedItems = $get('<%=hfExpandedItems.ClientID %>').value;
            if (button.attr('class') == 'Expand')
                expandedItems = expandedItems.replace(selector + ';', '');
            else
                expandedItems += selector + ';';
            $get('<%=hfExpandedItems.ClientID %>').value = expandedItems;

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
    </script>
</asp:Content>
