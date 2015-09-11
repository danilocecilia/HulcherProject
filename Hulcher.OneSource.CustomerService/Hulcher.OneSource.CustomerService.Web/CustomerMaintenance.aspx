<%@ Page Title="Company Profile Maintenance" Language="C#" MasterPageFile="~/ContentPage.master"
    AutoEventWireup="true" CodeBehind="CustomerMaintenance.aspx.cs" Inherits="Hulcher.OneSource.CustomerService.Web.CustomerMaintenance" %>

<%@ MasterType TypeName="Hulcher.OneSource.CustomerService.Web.ContentPage" %>
<%@ Register Src="~/UserControls/CallCriteria/CallCriteriaInfo.ascx" TagName="CallCriteria"
    TagPrefix="uc" %>
<%@ Register Src="~/UserControls/AutoCompleteTextbox.ascx" TagName="AutoCompleteTextbox"
    TagPrefix="uc1" %>
<%@ Register TagName="CollapseHolder" TagPrefix="uc" Src="~/UserControls/CollapseHolder.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Styles/Forms.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <script src="Scripts/jquery.maskedinput-1.3.min.js" type="text/javascript"></script>
    <div style="padding-bottom: 10px; height: 20px;">
        <asp:Label ID="lblPageTitle" runat="server" Font-Size="Large" Font-Bold="true" Text="Company Profile Maintenance" />
    </div>
    <asp:UpdatePanel ID="updGeneral" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:Panel ID="pnlFullAccess" runat="server" Visible="true">
                <div class="Page">
                    <asp:Panel ID="pnlSelection" runat="server">
                        <div class="Header">
                            <asp:Label ID="lblHeader" runat="server" Text="Company Listing" />
                        </div>
                        <div class="Content">
                            <div class="inlineBlock space100">
                                <asp:Panel ID="pnlFilterExced" runat="server" CssClass="warningbox" Visible="false">
                                    <ul>
                                        <li>The query result count has exceded the limit, please try a new filter.</li>
                                    </ul>
                                </asp:Panel>
                            </div>
                            <asp:Panel ID="pnlFilter" runat="server" DefaultButton="btnFind" CssClass="filter">
                                <div class="control">
                                    <div class="title">
                                        <asp:Label ID="lblFilter" runat="server" Text="Filter Listing By: " />
                                    </div>
                                    <div class="combo">
                                        <asp:ComboBox ID="ddlFilter" runat="server" CssClass="WindowsStyle" AutoCompleteMode="SuggestAppend"
                                            DropDownStyle="DropDown" CaseSensitive="false" RenderMode="Inline">
                                            <asp:ListItem Text="- Select One -" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Company" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Contact Name" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Location" Value="3"></asp:ListItem>
                                        </asp:ComboBox>
                                    </div>
                                    <div class="textbox">
                                        <asp:TextBox ID="txtFilterValue" runat="server" CssClass="input" />
                                        <asp:Button ID="btnFind" runat="server" Text="Find" CssClass="btn" CausesValidation="false"
                                            OnClick="btnFind_Click" />
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="gridview">
                                <asp:HiddenField ID="hfOrderBy" runat="server" ClientIDMode="Static" />
                                <asp:Button ID="btnFakeSort" runat="server" OnClick="btnFakeSort_Click" Style="display: none;" />
                                <asp:Panel ID="pnlSelectionCustomer" runat="server">
                                    <asp:Repeater ID="rptCustomer" runat="server" OnItemDataBound="rptCustomer_ItemDataBound"
                                        OnItemCommand="rptCustomer_ItemCommand">
                                        <HeaderTemplate>
                                            <div id="tbRepeaters_Group" class="ScrollableGridView_Group" style="width: 100%">
                                                <div id="tbRepeaters_HeaderDiv" class="ScrollableGridView_HeaderDiv" style="min-width: 400px;">
                                                </div>
                                                <div id="tbRepeaters_ScrollDiv" class="ScrollableGridView_ScrollDiv" style="max-height: 220px;
                                                    min-width: 400px;">
                                                    <table id="tbRepeaters" class="ScrollableGridView" cellspacing="1">
                                                        <thead>
                                                            <tr style="position: relative; top: expression(this.offsetParent.scrollTop -1); left: expression(this.offsetParent.style.left);">
                                                                <th id="thExpandCollapse" runat="server" class="header" style="border: 1px solid #E6EEEE;
                                                                    display: none;">
                                                                    &nbsp;
                                                                </th>
                                                                <th id="thCustomer" runat="server" class="header" style="border: 1px, solid, #E6EEEE;"
                                                                    onclick="SetOrderBy('1', this);">
                                                                    <asp:Label ID="rpt1Header1" CssClass="MarginRight" runat="server" Text="Company"></asp:Label>
                                                                </th>
                                                                <th id="thLocation" runat="server" class="header" onclick="SetOrderBy('4', this);">
                                                                    <asp:Label ID="rpt1Header4" CssClass="MarginRight" runat="server" Text="Location"></asp:Label>
                                                                </th>
                                                                <th id="thEdit" runat="server" class="header">
                                                                    <asp:Label ID="rpt1Header5" CssClass="MarginRight" runat="server" Text=""></asp:Label>
                                                                </th>
                                                                <th id="thAddNew" runat="server" class="header">
                                                                    <asp:Label ID="rpt1Header6" CssClass="MarginRight" runat="server" Text=""></asp:Label>
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class="even" id="trItem" runat="server">
                                                <td style="width: 15px; display: none;">
                                                    <div class="Expand" id="divExpand" runat="server" visible="false">
                                                    </div>
                                                </td>
                                                <td colspan="2">
                                                    <asp:Label ID="lblCustomer" runat="server" Text=""></asp:Label>
                                                    <asp:HiddenField ID="hfCustomerID" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="EditCustomer" CommandArgument=""
                                                        CssClass="btn linkButtonStyle" CausesValidation="false" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnAddNewContact" runat="server" Text="Add New Contact" CommandName="AddNewContact"
                                                        CommandArgument="" CssClass="btn linkButtonStyle" CausesValidation="false" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr class="odd" id="trItem" runat="server">
                                                <td style="width: 15px; display: none;">
                                                    <div class="Expand" id="divExpand" runat="server" visible="false">
                                                    </div>
                                                </td>
                                                <td colspan="2">
                                                    <asp:Label ID="lblCustomer" runat="server" Text=""></asp:Label>
                                                    <asp:HiddenField ID="hfCustomerID" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="EditCustomer" CommandArgument=""
                                                        CssClass="btn linkButtonStyle" CausesValidation="false" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnAddNewContact" runat="server" Text="Add New Contact" CommandName="AddNewContact"
                                                        CausesValidation="false" CommandArgument="" CssClass="btn linkButtonStyle" />
                                                </td>
                                            </tr>
                                        </AlternatingItemTemplate>
                                        <FooterTemplate>
                                            </tbody> </table> </div> </div>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </asp:Panel>
                                <asp:Panel ID="pnlSelectionContact" runat="server">
                                    <asp:Repeater ID="rptContact" runat="server" OnItemDataBound="rptContact_ItemDataBound"
                                        OnItemCommand="rptContact_ItemCommand">
                                        <HeaderTemplate>
                                            <div id="tbRepeaters_Group" class="ScrollableGridView_Group" style="width: 100%">
                                                <div id="tbRepeaters_HeaderDiv" class="ScrollableGridView_HeaderDiv" style="min-width: 400px;">
                                                </div>
                                                <div id="tbRepeaters_ScrollDiv" class="ScrollableGridView_ScrollDiv" style="max-height: 220px;
                                                    min-width: 400px;">
                                                    <table id="tbRepeaters" class="ScrollableGridView" cellspacing="1">
                                                        <thead>
                                                            <tr style="position: relative; top: expression(this.offsetParent.scrollTop -1); left: expression(this.offsetParent.style.left);">
                                                                <th id="thCustomer" runat="server" class="header" style="border: 1px, solid, #E6EEEE;"
                                                                    onclick="SetOrderBy('1', this);">
                                                                    <asp:Label ID="rpt1Header1" CssClass="MarginRight" runat="server" Text="Company"></asp:Label>
                                                                </th>
                                                                <th id="thContact" runat="server" class="header" onclick="SetOrderBy('2', this);">
                                                                    <asp:Label ID="rpt1Header2" CssClass="MarginRight" runat="server" Text="Contact"></asp:Label>
                                                                </th>
                                                                <th id="thType" runat="server" class="header" onclick="SetOrderBy('3', this);">
                                                                    <asp:Label ID="rpt1Header3" CssClass="MarginRight" runat="server" Text="Type"></asp:Label>
                                                                </th>
                                                                <th id="thLocation" runat="server" class="header" onclick="SetOrderBy('4', this);">
                                                                    <asp:Label ID="rpt1Header4" CssClass="MarginRight" runat="server" Text="Location"></asp:Label>
                                                                </th>
                                                                <th id="thEdit" runat="server" class="header">
                                                                    <asp:Label ID="rpt1Header5" CssClass="MarginRight" runat="server" Text=""></asp:Label>
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class="even" id="tr2" runat="server">
                                                <td>
                                                    <asp:Label ID="lblCustomerName" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblContact" runat="server" Text=""></asp:Label>
                                                    <asp:HiddenField ID="hfContactID" runat="server" />
                                                    <asp:HiddenField ID="hfCustomerID" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblType" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblLocation" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="EditContact" CommandArgument=""
                                                        CssClass="btn linkButtonStyle" CausesValidation="false" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr class="odd" id="tr2" runat="server">
                                                <td>
                                                    <asp:Label ID="lblCustomerName" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblContact" runat="server" Text=""></asp:Label>
                                                    <asp:HiddenField ID="hfContactID" runat="server" />
                                                    <asp:HiddenField ID="hfCustomerID" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblType" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblLocation" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="EditContact" CommandArgument=""
                                                        CssClass="btn linkButtonStyle" CausesValidation="false" />
                                                </td>
                                            </tr>
                                        </AlternatingItemTemplate>
                                        <FooterTemplate>
                                            </tbody> </table> </div> </div>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </asp:Panel>
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
                        <div style="text-align: right; padding-top: 5px;">
                            <asp:Button ID="btnNewCustomerRequest" runat="server" Text="New Company Request"
                                CssClass="btn" CausesValidation="false" OnClick="btnNewCustomerRequest_Click" />
                            <asp:Button ID="btnNewContactRequest" runat="server" Text="New Contact Request" CssClass="btn"
                                CausesValidation="false" OnClick="btnNewContactRequest_Click" />
                            <asp:Button ID="btnViewCustomerRequests" runat="server" Text="View Company / Contact Requests"
                                CssClass="btn" CausesValidation="false" OnClientClick="javascript:window.open('/CustomerRequest.aspx', '', 'width=1200, height=600, scrollbars=1, resizable=yes'); return false;" />
                        </div>
                        <br />
                    </asp:Panel>
                    <div id="divControl" runat="server" style="display: none">
                        <div id="divControlCustomer" runat="server">
                            <asp:Panel ID="pnlCustomerFields" runat="server" Style="display: none;">
                                <uc:CollapseHolder ID="chCustomerInfo" runat="server" GridViewCssClass="ScrollableGridView">
                                    <Header>
                                        <asp:Label ID="lblCustomerInfoTitle" runat="server" Text="Company Info"></asp:Label>
                                    </Header>
                                    <Content>
                                        <asp:ValidationSummary ID="vsCustomerInformation" runat="server" CssClass="errorbox"
                                            ValidationGroup="CustomerInformation" HeaderText="Please correct the following information" />
                                        <div class="inlineBlock space100">
                                            <asp:Panel ID="pnlCustomerWarning" runat="server" CssClass="warningbox" Visible="false">
                                                <ul>
                                                    <li>Since there are more than one information change requests pending, the information
                                                        below may diverge from the last added information.</li>
                                                </ul>
                                            </asp:Panel>
                                        </div>
                                        <div class="inlineBlock space100">
                                            <div class="space49 floatLeft block">
                                                <div class="space29 floatLeft alignRight">
                                                    <asp:Label ID="lblCustomerName" runat="server" Text="Name:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtCustomerName" runat="server" MaxLength="255" CssClass="input"
                                                        Width="200px" />
                                                    <asp:RequiredFieldValidator ID="rfvCustomerName" ErrorMessage="Company Name field is required"
                                                        Display="Dynamic" ControlToValidate="txtCustomerName" runat="server" ValidationGroup="CustomerInformation"
                                                        Text="*" />
                                                </div>
                                            </div>
                                            <div class="space50 floatRight block">
                                                <div class="space29 floatLeft alignRight">
                                                    <asp:Label ID="lblCustomerAttn" runat="server" Text="Attn:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtCustomerAttn" runat="server" MaxLength="255" CssClass="input"
                                                        Width="200px" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="inlineBlock space100">
                                            <div class="space49 floatLeft block">
                                                <div class="space29 floatLeft alignRight">
                                                    <asp:Label ID="lblCustomerAddress" runat="server" Text="Address:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtCustomerAddress" runat="server" MaxLength="255" CssClass="input"
                                                        Width="200px" />
                                                </div>
                                            </div>
                                            <div class="space50 floatRight block">
                                                <div class="space29 floatLeft alignRight">
                                                    <asp:Label ID="lblCustomerAddress2" runat="server" Text="Complement:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtCustomerAddress2" runat="server" MaxLength="255" CssClass="input"
                                                        Width="200px" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="inlineBlock space100">
                                            <div class="space49 floatLeft block">
                                                <div class="space29 floatLeft alignRight">
                                                    <asp:Label ID="lblCustomerStateProvinceCode" runat="server" Text="State Province Code:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtCustomerStateProvinceCode" runat="server" MaxLength="2" CssClass="input"
                                                        Width="20px" />
                                                </div>
                                            </div>
                                            <div class="space50 floatRight block">
                                                <div class="space29 floatLeft alignRight">
                                                    <asp:Label ID="lblCustomerCity" runat="server" Text="City:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtCustomerCity" runat="server" MaxLength="255" CssClass="input"
                                                        Width="100px" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="inlineBlock space100">
                                            <div class="space49 floatLeft block">
                                                <div class="space29 floatLeft alignRight">
                                                    <asp:Label ID="lblCustomerCountryCode" runat="server" Text="Country Code:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:ComboBox ID="ddlCountryCode" runat="server" CssClass="WindowsStyle" AutoCompleteMode="SuggestAppend"
                                                        DropDownStyle="DropDown" CaseSensitive="false" RenderMode="Inline" Width="80px">
                                                        <asp:ListItem Selected="True" Value="US" Text="US"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="CA" Text="CN"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="MX" Text="MX"></asp:ListItem>
                                                    </asp:ComboBox>
                                                </div>
                                            </div>
                                            <div class="space50 floatRight block">
                                                <div class="space29 floatLeft alignRight">
                                                    <asp:Label ID="lblCustomerPostalCode" runat="server" Text="Postal Code:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtCustomerPostalCode" runat="server" MaxLength="10" CssClass="input"
                                                        Width="100px" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="inlineBlock space100">
                                            <div class="space49 floatLeft block">
                                                <div class="space29 floatLeft alignRight">
                                                    <asp:Label ID="lblWorkPhone" runat="server" Text="Work Phone:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtCustomerHomePhoneNumber" runat="server" MaxLength="30" CssClass="input"
                                                        Width="100px" />
                                                </div>
                                            </div>
                                            <div class="space50 floatRight block">
                                                <div class="space29 floatLeft alignRight">
                                                    <asp:Label ID="lblCustomerFaxPhone" runat="server" Text="Fax Phone:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtCustomerFaxPhone" runat="server" MaxLength="30" CssClass="input"
                                                        Width="100px" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="inlineBlock space100">
                                            <div class="space49 floatLeft block">
                                                <div class="space29 floatLeft alignRight">
                                                    <asp:Label ID="lblCustomerEmail" runat="server" Text="Email:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtCustomerEmail" runat="server" MaxLength="255" CssClass="input"
                                                        Width="200px" />
                                                </div>
                                            </div>
                                            <div class="space50 floatRight block">
                                                <div class="space29 floatLeft alignRight">
                                                    <asp:Label ID="lblCustomerWebPage" runat="server" Text="Web page address:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtCustomerWebPage" runat="server" MaxLength="255" CssClass="input"
                                                        Width="200px" />
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="inlineBlock space100">
                                            <div class="space49 floatLeft block">
                                                <div class="space29 floatLeft alignRight">
                                                    <asp:Label ID="lblCustomerIMAddress" runat="server" Text="IM Address:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtCustomerIMAddress" runat="server" MaxLength="255" CssClass="input"
                                                        Width="200px" />
                                                </div>
                                            </div>
                                            <div class="space50 floatRight block">
                                                <div class="space29 floatLeft alignRight">
                                                    <asp:Label ID="lblCustomerBillingName" runat="server" Text="Billing Name:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtCustomerBillingName" runat="server" MaxLength="255" CssClass="input"
                                                        Width="200px" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="inlineBlock space100">
                                            <div class="space49 floatLeft block">
                                                <div class="space29 floatLeft alignRight">
                                                    <asp:Label ID="lblCustomerBillingAddress" runat="server" Text="Billing Address:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtCustomerBillingAddress" runat="server" MaxLength="255" CssClass="input"
                                                        Width="200px" />
                                                </div>
                                            </div>
                                            <div class="space50 floatRight block">
                                                <div class="space29 floatLeft alignRight">
                                                    <asp:Label ID="lblCustomerBillingAddress2" runat="server" Text="Address 2:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtCustomerBillingAddress2" runat="server" MaxLength="255" CssClass="input"
                                                        Width="200px" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="inlineBlock space100">
                                            <div class="space49 floatLeft block">
                                                <div class="space29 floatLeft alignRight">
                                                    <asp:Label ID="lblCustomerBillingAttn" runat="server" Text="Attn:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtCustomerBillingAttn" runat="server" MaxLength="255" CssClass="input"
                                                        Width="200px" />
                                                </div>
                                            </div>
                                            <div class="space50 floatRight block">
                                                <div class="space29 floatLeft alignRight">
                                                    <asp:Label ID="lblCustomerBillingStateProvinceCode" runat="server" Text="State Province Code:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtCustomerBillingStateProvinceCode" runat="server" MaxLength="2"
                                                        CssClass="input" Width="20px" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="inlineBlock space100">
                                            <div class="space49 floatLeft block">
                                                <div class="space29 floatLeft alignRight">
                                                    <asp:Label ID="lblCustomerBillingCity" runat="server" Text="City:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtCustomerBillingCity" runat="server" MaxLength="255" CssClass="input"
                                                        Width="100px" />
                                                </div>
                                            </div>
                                            <div class="space50 floatRight block">
                                                <div class="space29 floatLeft alignRight">
                                                    <asp:Label ID="lblCustomerBillingPostalCode" runat="server" Text="Postal Code:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtCustomerBillingPostalCode" runat="server" MaxLength="10" CssClass="input"
                                                        Width="100px" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="inlineBlock space100">
                                            <div class="space49 floatLeft block">
                                                <div class="space29 floatLeft alignRight">
                                                    <asp:Label ID="lblBillingCountryCode" runat="server" Text="Country Code:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:ComboBox ID="ddlCustomerBillingCountryCode" runat="server" CssClass="WindowsStyle"
                                                        AutoCompleteMode="SuggestAppend" DropDownStyle="DropDown" CaseSensitive="false"
                                                        RenderMode="Inline" Width="80px">
                                                        <asp:ListItem Selected="True" Value="US" Text="US"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="CA" Text="CN"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="MX" Text="MX"></asp:ListItem>
                                                    </asp:ComboBox>
                                                </div>
                                            </div>
                                            <div class="space50 floatRight block">
                                                <div class="space29 floatLeft alignRight">
                                                    <asp:Label ID="Label1" runat="server" Text="Thru Project:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="TextBox1" runat="server" MaxLength="6" CssClass="input" Width="200px" />
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtCustomerThruProjectt"
                                                        FilterMode="ValidChars" FilterType="Numbers" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="inlineBlock space100">
                                            <div class="space49 floatLeft block">
                                                <div class="space29 floatLeft alignRight">
                                                    <asp:Label ID="lblBillingCustomerHomePhone" runat="server" Text="Work Phone:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtBillingCustomerHomePhoneNumber" runat="server" MaxLength="30"
                                                        CssClass="input" Width="100px" />
                                                </div>
                                            </div>
                                            <div class="space50 floatRight block">
                                                <div class="space29 floatLeft alignRight">
                                                    <asp:Label ID="lblBillingCustomerFaxPhone" runat="server" Text="Fax Phone:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtBillingCustomerFaxPhone" runat="server" MaxLength="30" CssClass="input"
                                                        Width="100px" />
                                                </div>
                                            </div>
                                            <div class="inlineBlock space100">
                                                <div class="space49 floatLeft block">
                                                    <div class="space29 floatLeft alignRight">
                                                        <asp:Label ID="lblCustomerBillingSalutation" runat="server" Text="Billing Salutation:"></asp:Label>
                                                    </div>
                                                    <div class="space70 floatRight alignLeft">
                                                        <asp:TextBox ID="txtCustomerBillingSalutation" runat="server" MaxLength="255" CssClass="input"
                                                            Width="200px" />
                                                    </div>
                                                </div>
                                                <div class="space50 floatRight block">
                                                    <div class="space29 floatLeft alignRight">
                                                        <asp:Label ID="lblCustomerThruProject" runat="server" Text="Thru Project:"></asp:Label>
                                                    </div>
                                                    <div class="space70 floatRight alignLeft">
                                                        <asp:TextBox ID="txtCustomerThruProjectt" runat="server" MaxLength="6" CssClass="input"
                                                            Width="200px" />
                                                        <asp:FilteredTextBoxExtender ID="fteCustomerThruProject" runat="server" TargetControlID="txtCustomerThruProjectt"
                                                            FilterMode="ValidChars" FilterType="Numbers" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="inlineBlock space100">
                                            <div class="space49 floatLeft block">
                                                <div class="space29 floatLeft alignRight">
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:CheckBox ID="chkCustomerCreditCheck" runat="server" Text="Credit Check Completed" />
                                                </div>
                                            </div>
                                            <div class="space50 floatRight alignLeft">
                                            </div>
                                        </div>
                                        <asp:Panel ID="pnlIsCollection" runat="server" Visible="false">
                                            <hr />
                                            <div class="inlineBlock space100">
                                                <div class="space49 floatLeft block">
                                                    <div class="space70 floatRight alignLeft">
                                                        <asp:CheckBox ID="chkIsCollection" runat="server" Text="Is Collection" />
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <br />
                                    </Content>
                                </uc:CollapseHolder>
                                <br />
                                <uc:CollapseHolder ID="chAlertNotification" runat="server" GridViewCssClass="ScrollableGridView">
                                    <Header>
                                        <asp:Label ID="lblAlertNotificationTitle" runat="server" Text="Alert Notification"></asp:Label>
                                    </Header>
                                    <Content>
                                        <asp:Label ID="lblCustomerNote" runat="server" Text="Company Note:"></asp:Label>
                                        <asp:CountableTextBox ID="txtNotes" runat="server" MaxChars="1000" MaxCharsWarning="950"
                                            Height="100px" Width="95%" CssClass="input" TextMode="MultiLine" />
                                        <asp:CheckBox ID="chkOperatorAlert" runat="server" Text="Operator Alert" />
                                    </Content>
                                </uc:CollapseHolder>
                                <br />
                                <uc:CollapseHolder ID="chCustomerSpecificInfo" runat="server" GridViewCssClass="ScrollableGridView">
                                    <Header>
                                        <asp:Label ID="lblCustomerSpecificInfoTitle" runat="server" Text="Company Specific Info"></asp:Label>
                                    </Header>
                                    <Content>
                                        <asp:CheckBoxList ID="cblCustomerSpecificInfoType" runat="server" DataTextField="Description"
                                            DataValueField="ID" RepeatLayout="Table" RepeatDirection="Horizontal" RepeatColumns="3">
                                        </asp:CheckBoxList>
                                    </Content>
                                </uc:CollapseHolder>
                                <br />
                            </asp:Panel>
                        </div>
                        <div id="divControlContact" runat="server">
                            <asp:Panel ID="pnlContactFields" runat="server" Style="display: none;">
                                <uc:CollapseHolder ID="MainContactInfo" runat="server" GridViewCssClass="ScrollableGridView">
                                    <Header>
                                        <asp:Label ID="lblContactInfoTitle" runat="server" Text="Contact Info"></asp:Label></Header>
                                    <Content>
                                        <asp:ValidationSummary ID="vsContactInformation" runat="server" CssClass="errorbox"
                                            ValidationGroup="ContactInformation" HeaderText="Please correct the following information" />
                                        <div class="inlineBlock space100">
                                            <div class="space49 floatLeft">
                                                <div class="space29 floatLeft alignRight paddingTop2">
                                                    <asp:Label ID="lblSelectCustomer" runat="server" Text="Select Company: "></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <div id="divSelectCustomer" runat="server" style="display: inline;">
                                                        <uc1:AutoCompleteTextbox ID="actCustomerSelection" runat="server" ServiceMethod="GetCustomerList"
                                                            GridViewButtonImageUrl="~/Images/money.png" GridViewIdName="ID" DisplayField="Name"
                                                            TextBoxWidth="300px" AutoPostBack="false" ErrorMessage="Company field is required"
                                                            WindowTitle="Company Profile Maintenance - Find Company" AutoCompleteSource="Customer"
                                                            ColumnHeaderList="Name,Attn,Company ID" ColumnValueList="Name,Attn,CustomerNumber"
                                                            ValidationGroup="ContactInformation" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="space50 floatLeft">
                                                <div class="space29 floatLeft alignLeft paddingTop2">
                                                    <asp:Button ID="btnNewCompany" runat="server" Text="Add new Company" CssClass="btn"
                                                        CausesValidation="false" OnClientClick="javascript:window.open('/CustomerMaintenance.aspx?ViewType=CUSTOMER&NewCustomer=true&RefField=actCustomerSelection', '', 'width=1200, height=600, scrollbars=1, resizable=yes'); return false;" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="inlineBlock space100" runat="server" id="ContactTypeDiv">
                                            <div class="space49 floatLeft">
                                                <div class="space29 floatLeft alignRight paddingTop2">
                                                    <asp:Label ID="lblContactType" runat="server" Text="Contact Type:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:ComboBox ID="ddlContactType" runat="server" CssClass="WindowsStyle" AutoCompleteMode="SuggestAppend"
                                                        DropDownStyle="DropDown" CaseSensitive="false" RenderMode="Inline" Width="100px"
                                                        ValidationGroup="ContactInformation">
                                                        <asp:ListItem Text="Primary Contact" Value="False"></asp:ListItem>
                                                        <asp:ListItem Text="Bill-To Contact" Value="True"></asp:ListItem>
                                                    </asp:ComboBox>
                                                </div>
                                            </div>
                                            <div class="space50 floatRight">
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="inlineBlock space100">
                                            <div class="space49 floatLeft">
                                                <div class="space29 floatLeft alignRight paddingTop2">
                                                    <asp:Label ID="lblContactName" runat="server" Text="First Name:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtContactName" runat="server" MaxLength="255" CssClass="input"
                                                        Width="200px" ValidationGroup="ContactInformation" />
                                                    <asp:RequiredFieldValidator ID="rfvContactName" ErrorMessage="Contact First Name field is required"
                                                        Display="Dynamic" Text="*" ControlToValidate="txtContactName" runat="server"
                                                        ValidationGroup="ContactInformation" />
                                                </div>
                                            </div>
                                            <div class="space50 floatRight">
                                                <div class="space29 floatLeft alignRight paddingTop2">
                                                    <asp:Label ID="lblContactLastName" runat="server" Text="Last Name:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtContactLastName" runat="server" MaxLength="255" CssClass="input"
                                                        Width="200px" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="inlineBlock space100">
                                            <div class="space49 floatLeft">
                                                <div class="space29 floatLeft alignRight paddingTop2">
                                                    <asp:Label ID="lblContactCountryCode" runat="server" Text="Country Code:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:ComboBox ID="ddlContactCountryCode" runat="server" CssClass="WindowsStyle" AutoCompleteMode="SuggestAppend"
                                                        DropDownStyle="DropDown" CaseSensitive="false" RenderMode="Inline" Width="80px">
                                                        <asp:ListItem Selected="True" Value="US" Text="US"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="CA" Text="CN"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="MX" Text="MX"></asp:ListItem>
                                                    </asp:ComboBox>
                                                </div>
                                            </div>
                                            <div class="space50 floatRight">
                                                <asp:TextBox ID="txtContactHomePhoneNumber" runat="server" MaxLength="30" CssClass="input"
                                                    Style="display: none" Width="100px" />
                                            </div>
                                        </div>
                                        <hr />
                                        <div id="errorDiv" class="errorbox" style="display: none;">
                                            <asp:Label ID="lblNumberError" runat="server" Text="Invalid Type or Number, please correct the information"></asp:Label>
                                        </div>
                                        <div class="inlineBlock space100">
                                            <asp:ValidationSummary ID="vsAdditionalPhone" runat="server" CssClass="errorbox"
                                                HeaderText="Please correct the following information" ValidationGroup="Add" />
                                            <br />
                                            <div class="space49 floatLeft">
                                                <div class="space29 floatLeft alignRight paddingTop2">
                                                    <asp:Label ID="lblAdditionalContactType" runat="server" Text="Phone Type:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:ComboBox ID="ddlAdditionalContactType" runat="server" CssClass="WindowsStyle"
                                                        AutoCompleteMode="SuggestAppend" DropDownStyle="DropDown" CaseSensitive="false"
                                                        RenderMode="Inline" Width="100px" DataValueField="ID" DataTextField="Name">
                                                    </asp:ComboBox>
                                                </div>
                                                <div class="space29 floatLeft alignRight paddingTop2">
                                                    <asp:Label ID="lblAdditionalContactNumber" runat="server" Text="Phone:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <div class="floatLeft alignLeft">
                                                        <asp:TextBox ID="txtAdditionalContactNumber" runat="server" MaxLength="50" CssClass="input"
                                                            Width="100px" />
                                                        <div id="divExtension" style="display: none">
                                                            Ext:
                                                            <asp:TextBox ID="txtExtension" runat="server" MaxLength="5" CssClass="input" Width="41"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="paddingTop2 alignRight" style="width: 35px;">
                                                        <asp:Button ID="btnAdditionalContactAdd" runat="server" Text="Add" CssClass="btn"
                                                            OnClientClick=" AddPhone(); return false;" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="space50 floatRight">
                                                <asp:Panel ID="pnlNoRowsPhone" runat="server">
                                                    <div class="ScrollableGridView_Group" style="width: 100%">
                                                        <div class="ScrollableGridView_HeaderDiv" style="min-width: 400px;">
                                                        </div>
                                                        <div class="ScrollableGridView_ScrollDiv" style="max-height: 220px; min-width: 400px;">
                                                            <table id="Table1" class="ScrollableGridView" cellspacing="1">
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
                                                <asp:Panel ID="pnlGrid" runat="server">
                                                    <div id="tbPhoneNumbers_Group" class="ScrollableGridView_Group" style="width: 100%">
                                                        <div id="tbPhoneNumbers_HeaderDiv" class="ScrollableGridView_HeaderDiv" style="min-width: 150px;">
                                                        </div>
                                                        <div id="tbPhoneNumbers_ScrollDiv" class="ScrollableGridView_ScrollDiv" style="max-height: 100px;
                                                            min-width: 400px;">
                                                            <table id="tbPhoneNumbers" class="ScrollableGridView" cellspacing="1">
                                                                <thead>
                                                                    <tr style="position: relative; top: expression(this.offsetParent.scrollTop -1); left: expression(this.offsetParent.style.left);">
                                                                        <th id="thID" runat="server" class="header" visible="false">
                                                                            <asp:Label ID="rpt1Header1" CssClass="MarginRight" runat="server" Text="ID" Visible="false"></asp:Label>
                                                                        </th>
                                                                        <th id="thArea" runat="server" class="header">
                                                                            <asp:Label ID="rpt1Header2" CssClass="MarginRight" runat="server" Text="Area"></asp:Label>
                                                                        </th>
                                                                        <th id="thNumber" runat="server" class="header">
                                                                            <asp:Label ID="rpt1Header3" CssClass="MarginRight" runat="server" Text="Number"></asp:Label>
                                                                        </th>
                                                                        <th id="thRemove" runat="server" class="header">
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                        <div class="inlineBlock scpace100">
                                            <div class="space50 floatRight">
                                            </div>
                                        </div>
                                    </Content>
                                </uc:CollapseHolder>
                                <br />
                                <uc:CollapseHolder ID="chContactInfo" runat="server" GridViewCssClass="ScrollableGridView">
                                    <Header>
                                        <asp:Label ID="lblMoreContactInfo" runat="server" Text="More Contact Info"></asp:Label>
                                    </Header>
                                    <Content>
                                        <div class="inlineBlock space100">
                                            <asp:Panel ID="pnlWarning" runat="server" CssClass="warningbox" Visible="false">
                                                <ul>
                                                    <li>Since there are more than one change information requests pending, the information
                                                        below may diverge from the last added information.</li>
                                                </ul>
                                            </asp:Panel>
                                        </div>
                                        <div class="inlineBlock space100">
                                            <div class="space49 floatLeft">
                                                <div class="space29 floatLeft alignRight paddingTop2">
                                                    <asp:Label ID="lblContactAlias" runat="server" Text="Subsidiary:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtContactAlias" runat="server" MaxLength="255" CssClass="input"
                                                        Width="200px" />
                                                </div>
                                            </div>
                                            <div class="space50 floatRight">
                                                <div class="space29 floatLeft alignRight paddingTop2">
                                                    <asp:Label ID="lblContactContactNumber" runat="server" Text="Contract Number:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtContactContactNumber" runat="server" MaxLength="255" CssClass="input"
                                                        Width="200px" Enabled="false" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="inlineBlock space100">
                                            <div class="space49 floatLeft">
                                                <div class="space29 floatLeft alignRight paddingTop2">
                                                    <asp:Label ID="lblContactAttn" runat="server" Text="Attn:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtContactAttn" runat="server" MaxLength="255" CssClass="input"
                                                        Width="200px" />
                                                </div>
                                            </div>
                                            <div class="space50 floatRight">
                                                <div class="space29 floatLeft alignRight paddingTop2">
                                                    <asp:Label ID="lblContactAddress" runat="server" Text="Address:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtContactAddress" runat="server" MaxLength="255" CssClass="input"
                                                        Width="200px" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="inlineBlock space100">
                                            <div class="space49 floatLeft">
                                                <div class="space29 floatLeft alignRight paddingTop2">
                                                    <asp:Label ID="lblContactAddress2" runat="server" Text="Address 2:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtContactAddress2" runat="server" MaxLength="255" CssClass="input"
                                                        Width="200px" />
                                                </div>
                                            </div>
                                            <div class="space50 floatRight">
                                                <div class="space29 floatLeft alignRight paddingTop2">
                                                    <asp:Label ID="lblContactStateProvinceCode" runat="server" Text="State Province Code:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtContactStateProvinceCode" runat="server" MaxLength="2" CssClass="input"
                                                        Width="20px" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="inlineBlock space100">
                                            <div class="space49 floatLeft">
                                                <div class="space29 floatLeft alignRight paddingTop2">
                                                    <asp:Label ID="lblContactCity" runat="server" Text="City:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtContactCity" runat="server" MaxLength="255" CssClass="input"
                                                        Width="100px" />
                                                </div>
                                            </div>
                                            <div class="space50 floatRight">
                                                <div class="space29 floatLeft alignRight paddingTop2">
                                                    <asp:Label ID="lblContactPostalCode" runat="server" Text="Postal Code:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtContactPostalCode" runat="server" MaxLength="10" CssClass="input"
                                                        Width="100px" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="inlineBlock space100">
                                            <div class="space49 floatLeft">
                                                <div class="space29 floatLeft alignRight paddingTop2">
                                                    <asp:Label ID="lblContactEmail" runat="server" Text="Email:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtContactEmail" runat="server" MaxLength="255" CssClass="input"
                                                        Width="200px" />
                                                </div>
                                            </div>
                                            <div class="space50 floatRight">
                                                <asp:TextBox ID="txtContactFaxPhone" runat="server" MaxLength="30" CssClass="input"
                                                    Style="display: none" Width="100px" />
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="inlineBlock space100">
                                            <div class="space49 floatLeft">
                                                <div class="space29 floatLeft alignRight paddingTop2">
                                                    <asp:Label ID="lblContactWebPage" runat="server" Text="Web page address:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtContactWebPage" runat="server" MaxLength="255" CssClass="input"
                                                        Width="200px" />
                                                </div>
                                            </div>
                                            <div class="space50 floatRight">
                                                <div class="space29 floatLeft alignRight paddingTop2">
                                                    <asp:Label ID="lblContactIMAddress" runat="server" Text="IM Address:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtContactIMAddress" runat="server" MaxLength="255" CssClass="input"
                                                        Width="200px" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="inlineBlock space100">
                                            <div class="space49 floatLeft">
                                                <div class="space29 floatLeft alignRight paddingTop2">
                                                    <asp:Label ID="lblContactBillingName" runat="server" Text="Billing Name:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtContactBillingName" runat="server" MaxLength="255" CssClass="input"
                                                        Width="200px" />
                                                </div>
                                            </div>
                                            <div class="space50 floatRight">
                                                <div class="space29 floatLeft alignRight paddingTop2">
                                                    <asp:Label ID="lblContactBillingAddress" runat="server" Text="Billing Address:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtContactBillingAddress" runat="server" MaxLength="255" CssClass="input"
                                                        Width="200px" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="inlineBlock space100">
                                            <div class="space49 floatLeft">
                                                <div class="space29 floatLeft alignRight paddingTop2">
                                                    <asp:Label ID="lblContactBillingAddress2" runat="server" Text="Billing Address 2:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtContactBillingAddress2" runat="server" MaxLength="255" CssClass="input"
                                                        Width="200px" />
                                                </div>
                                            </div>
                                            <div class="space50 floatRight">
                                                <div class="space29 floatLeft alignRight paddingTop2">
                                                    <asp:Label ID="lblContactBillingAttn" runat="server" Text="Billing Attn:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtContactBillingAttn" runat="server" MaxLength="255" CssClass="input"
                                                        Width="200px" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="inlineBlock space100">
                                            <div class="space49 floatLeft">
                                                <div class="space29 floatLeft alignRight paddingTop2">
                                                    <asp:Label ID="lblContactBillingStateProvinceCode" runat="server" Text="State Province Code:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtContactBillingStateProvinceCode" runat="server" MaxLength="2"
                                                        CssClass="input" Width="20px" />
                                                </div>
                                            </div>
                                            <div class="space50 floatRight">
                                                <div class="space29 floatLeft alignRight paddingTop2">
                                                    <asp:Label ID="lblContactBillingCity" runat="server" Text="City:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtContactBillingCity" runat="server" MaxLength="255" CssClass="input"
                                                        Width="100px" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="inlineBlock space100">
                                            <div class="space49 floatLeft">
                                                <div class="space29 floatLeft alignRight paddingTop2">
                                                    <asp:Label ID="lblContactBillingSalutation" runat="server" Text="Billing Salutation:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtContactBillingSalutation" runat="server" MaxLength="255" CssClass="input"
                                                        Width="200px" />
                                                </div>
                                            </div>
                                            <div class="space50 floatRight">
                                                <div class="space29 floatLeft alignRight paddingTop2">
                                                    <asp:Label ID="lblContactBillingPostalCode" runat="server" Text="Postal Code:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtContactBillingPostalCode" runat="server" MaxLength="10" CssClass="input"
                                                        Width="100px" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="inlineBlock space100">
                                            <div class="space49 floatLeft">
                                                <div class="space29 floatLeft alignRight paddingTop2">
                                                    <asp:Label ID="lblContactBillingCountryCode" runat="server" Text="Country Code:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:ComboBox ID="ddlContactBillingCountryCode" runat="server" CssClass="WindowsStyle"
                                                        AutoCompleteMode="SuggestAppend" DropDownStyle="DropDown" CaseSensitive="false"
                                                        RenderMode="Inline" Width="80px">
                                                        <asp:ListItem Selected="True" Value="US" Text="US"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="CA" Text="CN"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="MX" Text="MX"></asp:ListItem>
                                                    </asp:ComboBox>
                                                </div>
                                            </div>
                                            <div class="space50 floatRight">
                                                <div class="space29 floatLeft alignRight paddingTop2">
                                                    <asp:Label ID="lblBillingContactFaxPhone" runat="server" Text="Fax Phone:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtBillingContactFaxPhone" runat="server" MaxLength="30" CssClass="input"
                                                        Width="100px" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="inlineBlock space100">
                                            <div class="space49 floatLeft">
                                                <div class="space29 floatLeft alignRight paddingTop2">
                                                    <asp:Label ID="lblBillingContactHomePhone" runat="server" Text="Work Phone:"></asp:Label>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <asp:TextBox ID="txtBillingContactHomePhoneNumber" runat="server" MaxLength="30"
                                                        CssClass="input" Width="100px" />
                                                </div>
                                            </div>
                                            <div class="space50 floatRight">
                                            </div>
                                        </div>
                                        <div class="inlineBlock space100">
                                            <div class="space50 floatRight">
                                                <div class="space29 floatLeft alignRight paddingTop2">
                                                    <%--<asp:Label ID="lblContactThruProject" runat="server" Text="Thru Project:"></asp:Label>--%>
                                                </div>
                                                <div class="space70 floatRight alignLeft">
                                                    <%--<asp:TextBox ID="txtContactThruProjectt" runat="server" MaxLength="255" CssClass="input"
                                                        Width="200px" />--%>
                                                </div>
                                            </div>
                                        </div>
                                    </Content>
                                </uc:CollapseHolder>
                                <br />
                                <uc:CallCriteria ID="uscCallCriteria" runat="server" EnableViewState="true" />
                            </asp:Panel>
                        </div>
                        <div style="text-align: right; padding-top: 5px;">
                            <asp:TextBox ID="fakeTxtPhone" runat="server" Text="" Style="display: none;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="fakeValidator" ErrorMessage="At least one of the Phone Numbers is required"
                                ControlToValidate="fakeTxtPhone" runat="server" Display="None" Enabled="false" />
                            <asp:TextBox ID="fakeTxtWPhone" runat="server" Text="" Style="display: none;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="fakeWorkPhoneValidator" ErrorMessage="The Work Phone format is invalid"
                                ControlToValidate="fakeTxtWPhone" runat="server" Display="None" Enabled="false" />
                            <asp:TextBox ID="fakeTxtFPhone" runat="server" Text="" Style="display: none;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="fakeFaxPhoneValidator" ErrorMessage="The Fax Phone format is invalid"
                                ControlToValidate="fakeTxtFPhone" runat="server" Display="None" Enabled="false" />
                            <asp:TextBox ID="fakeTxtBWPhone" runat="server" Text="" Style="display: none;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="fakeBWorkPhoneValidator" ErrorMessage="The Billing Work Phone format is invalid"
                                ControlToValidate="fakeTxtBWPhone" runat="server" Display="None" Enabled="false" />
                            <asp:TextBox ID="fakeTxtBFPhone" runat="server" Text="" Style="display: none;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="fakeBFaxPhoneValidator" ErrorMessage="The Billing Fax Phone format is invalid"
                                ControlToValidate="fakeTxtBFPhone" runat="server" Display="None" Enabled="false" />
                            <asp:TextBox ID="fakeTxtAdditionalPhone" runat="server" Text="" Style="display: none;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="fakeAdditionalPhoneValidator" ErrorMessage="The Additional Phone format is invalid"
                                ControlToValidate="fakeTxtAdditionalPhone" runat="server" Display="None" Enabled="false"
                                ValidationGroup="Add" />
                            <asp:TextBox ID="fakeTxtPhoneList" runat="server" Text="" Style="display: none;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="fakePhoneListValidator" ErrorMessage="There are Additional Phones with invalid formats"
                                ControlToValidate="fakeTxtPhoneList" runat="server" Display="None" Enabled="false" />
                            <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save & Close" OnClick="btnSaveAndClose_Click" />
                            <asp:Button ID="btnSaveAndContinue" runat="server" CssClass="btn" Text="Save & Continue"
                                OnClick="btnSaveContinue_Click" />
                            <asp:HiddenField ID="hfPhoneNumbers" runat="server" EnableViewState="true" ViewStateMode="Enabled" />
                            <asp:HiddenField ID="hidBtnSaveContinue" runat="server" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn" CausesValidation="false"
                                Visible="false" OnClientClick="return confirm('All unsaved data will be lost, are you sure you want to cancel?')"
                                OnClick="btnCancel_Click" />
                            <input id="btnClose" type="button" value="Close" class="btn" onclick="window.close();" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlNoAccess" runat="server" Visible="false">
                <div class="Page">
                    <div class="Header">
                        <asp:Label ID="lblTitleNoAccess" runat="server" Text="Company Details" />
                    </div>
                    <div class="Content">
                        The current user does not have access to this functionality!
                    </div>
                </div>
            </asp:Panel>
            <asp:HiddenField ID="hfExpandedItems" runat="server" />
            <asp:HiddenField ID="hidIsEditCompany" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript" language="javascript">
        function CheckIfAlreadyExistCustomer(btn) {

            var hidBtnSaveContinue = document.getElementById('<%= hidBtnSaveContinue.ClientID %>');
            hidBtnSaveContinue.value = btn;

            var customerName = $('#ContentPlaceHolder1_Content_chCustomerInfo_txtCustomerName').val();
            tempuri.org.IJSONService.CheckExistingCustomer(customerName, CheckExistingCustomerCompleted);

        }

        function CheckExistingCustomerCompleted(result) {

            var hidBtnSaveContinue = document.getElementById('<%= hidBtnSaveContinue.ClientID %>');

            var ExistingCustomer = document.getElementById('<%= hidIsEditCompany.ClientID %>');

            if (ExistingCustomer.value != 'true') {
                if (result.AlreadyExistsCompany) {
                    if (confirm("Already exists on the database this Company Name, would you like to proceed with the creation?"))
                        __doPostBack(hidBtnSaveContinue.value, '');
                }
                else {
                    __doPostBack(hidBtnSaveContinue.value, '');
                }
            }
            else { __doPostBack(hidBtnSaveContinue.value, ''); }
        }

        Sys.UI.DomEvent.addHandler(window, "load", function () {

            AddOnChangeEventPhoneNumber('<%= ddlCustomerBillingCountryCode.ClientID %>', '<%= txtBillingCustomerFaxPhone.ClientID%>');
            AddOnChangeEventPhoneNumber('<%= ddlCountryCode.ClientID %>', '<%= txtCustomerHomePhoneNumber.ClientID%>');
            AddOnChangeEventPhoneNumber('<%= ddlCountryCode.ClientID %>', '<%= txtCustomerFaxPhone.ClientID %>');
            AddOnChangeEventPhoneNumber('<%= ddlCustomerBillingCountryCode.ClientID %>', '<%= txtBillingCustomerHomePhoneNumber.ClientID %>');

            AddOnChangeEventPhoneNumber('<%= ddlContactBillingCountryCode.ClientID %>', '<%= txtBillingContactHomePhoneNumber.ClientID %>');
            AddOnChangeEventPhoneNumber('<%= ddlContactBillingCountryCode.ClientID %>', '<%= txtBillingContactFaxPhone.ClientID %>');
            AddOnChangeEventPhoneNumber('<%= ddlContactCountryCode.ClientID %>', '<%= txtAdditionalContactNumber.ClientID %>');

            AddOnChangeEventTypePhoneNumber('<%= ddlContactCountryCode.ClientID %>', '<%= ddlAdditionalContactType.ClientID %>', '<%= txtAdditionalContactNumber.ClientID %>');


            LoadInputMaskPhoneNumbers('<%= ddlCustomerBillingCountryCode.ClientID %>', '<%= txtBillingCustomerFaxPhone.ClientID%>');
            LoadInputMaskPhoneNumbers('<%= ddlCountryCode.ClientID %>', '<%= txtCustomerHomePhoneNumber.ClientID%>');
            LoadInputMaskPhoneNumbers('<%= ddlCountryCode.ClientID %>', '<%= txtCustomerFaxPhone.ClientID %>');
            LoadInputMaskPhoneNumbers('<%= ddlCustomerBillingCountryCode.ClientID %>', '<%= txtBillingCustomerHomePhoneNumber.ClientID %>');
            LoadInputMaskPhoneNumbers('<%= ddlContactBillingCountryCode.ClientID %>', '<%= txtBillingContactHomePhoneNumber.ClientID %>');
            LoadInputMaskPhoneNumbers('<%= ddlContactBillingCountryCode.ClientID %>', '<%= txtBillingContactFaxPhone.ClientID %>');

            LoadInputMaskPhoneNumbers('<%= ddlContactCountryCode.ClientID %>', '<%= txtAdditionalContactNumber.ClientID %>');

        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {

            AddOnChangeEventPhoneNumber('<%= ddlCustomerBillingCountryCode.ClientID %>', '<%= txtBillingCustomerFaxPhone.ClientID%>');
            AddOnChangeEventPhoneNumber('<%= ddlCountryCode.ClientID %>', '<%= txtCustomerHomePhoneNumber.ClientID%>');
            AddOnChangeEventPhoneNumber('<%= ddlCountryCode.ClientID %>', '<%= txtCustomerFaxPhone.ClientID %>');
            AddOnChangeEventPhoneNumber('<%= ddlCustomerBillingCountryCode.ClientID %>', '<%= txtBillingCustomerHomePhoneNumber.ClientID %>');

            AddOnChangeEventPhoneNumber('<%= ddlContactBillingCountryCode.ClientID %>', '<%= txtBillingContactHomePhoneNumber.ClientID %>');
            AddOnChangeEventPhoneNumber('<%= ddlContactBillingCountryCode.ClientID %>', '<%= txtBillingContactFaxPhone.ClientID %>');
            AddOnChangeEventPhoneNumber('<%= ddlContactCountryCode.ClientID %>', '<%= txtAdditionalContactNumber.ClientID %>');

            AddOnChangeEventTypePhoneNumber('<%= ddlContactCountryCode.ClientID %>', '<%= ddlAdditionalContactType.ClientID %>', '<%= txtAdditionalContactNumber.ClientID %>');


            LoadInputMaskPhoneNumbers('<%= ddlCustomerBillingCountryCode.ClientID %>', '<%= txtBillingCustomerFaxPhone.ClientID%>');
            LoadInputMaskPhoneNumbers('<%= ddlContactBillingCountryCode.ClientID %>', '<%= txtBillingContactHomePhoneNumber.ClientID %>');
            LoadInputMaskPhoneNumbers('<%= ddlContactBillingCountryCode.ClientID %>', '<%= txtBillingContactFaxPhone.ClientID %>');
            LoadInputMaskPhoneNumbers('<%= ddlCountryCode.ClientID %>', '<%= txtCustomerHomePhoneNumber.ClientID%>');
            LoadInputMaskPhoneNumbers('<%= ddlCountryCode.ClientID %>', '<%= txtCustomerFaxPhone.ClientID %>');
            LoadInputMaskPhoneNumbers('<%= ddlCustomerBillingCountryCode.ClientID %>', '<%= txtBillingCustomerHomePhoneNumber.ClientID %>');

            LoadInputMaskPhoneNumbers('<%= ddlContactCountryCode.ClientID %>', '<%= txtAdditionalContactNumber.ClientID %>');
        });


        function AddOnChangeEventTypePhoneNumber(countryCode, typePhone, phoneTextField) {
            $find(typePhone).add_propertyChanged(function (sender, e) {
                if (e.get_propertyName() == 'selectedIndex') {

                    var selectedIndexTphone = $find(typePhone).get_selectedIndex();
                    var selectedIndexCcode = $find(countryCode).get_selectedIndex();

                    var cCode = $get(countryCode).getElementsByTagName('LI')[selectedIndexCcode].outerText;
                    var tPhone = $get(typePhone).getElementsByTagName('LI')[selectedIndexTphone].outerText;

                    if (tPhone.trim() == 'Work')
                        var extension = $('#divExtension').css('display', 'block');
                    else
                        var extension = $('#divExtension').css('display', 'none');

                    if (tPhone.trim() == 'Home' || tPhone.trim() == 'Cell' || tPhone.trim() == 'Fax' || tPhone.trim() == 'Work') {
                        if (cCode == 'CA' || cCode == 'US') {
                            $('#' + phoneTextField).mask("(999) 999-9999");
                        }
                        else {
                            $('#' + phoneTextField).mask("(999) 9999-9999");
                        }
                    }
                    else {
                        $('#' + phoneTextField).unbind();
                    }
                }
            });
        }

        function AddOnChangeEventPhoneNumber(countryCode, phoneTextField) {
            $find(countryCode).add_propertyChanged(function (sender, e) {
                if (e.get_propertyName() == 'selectedIndex') {
                    var selectedIndex = $find(countryCode).get_selectedIndex();
                    var cCode = $get(countryCode).getElementsByTagName('LI')[selectedIndex].outerText;

                    var phoneNumber = $('#' + phoneTextField).val();

                    if (cCode == 'CA' || cCode == 'US') {
                        $('#' + phoneTextField).mask("(999) 999-9999");
                    }
                    else {
                        $('#' + phoneTextField).mask("(999) 9999-9999");
                    }

                    if (phoneNumber != '')
                        $('#' + phoneTextField).val(phoneNumber);
                }
            })
        }

        function LoadInputMaskPhoneNumbers(countryCode, phoneTextField) {
            var phoneNumber = $('#' + phoneTextField).val();

            var selectedIndex = $find(countryCode).get_selectedIndex();
            var cCode = $get(countryCode).getElementsByTagName('LI')[selectedIndex].outerText;

            var type = $find('<%= ddlAdditionalContactType.ClientID %>');

            if (type._textBoxControl.value.trim() == 'Work')
                var extension = $('#divExtension').css('display', 'block');
            else
                var extension = $('#divExtension').css('display', 'none');


            if (cCode == 'CA' || cCode == 'US') {
                $('#' + phoneTextField).mask("(999) 999-9999");
            }
            else {
                $('#' + phoneTextField).mask("(999) 9999-9999");
            }

            if (phoneNumber != '')
                $('#' + phoneTextField).val(phoneNumber);
        }

        function CheckPhoneValues(source) {

            if (source == 'Customer') {

                var homePhoneNumber = document.getElementById('<%= txtCustomerHomePhoneNumber.ClientID %>');
                var faxPhoneNumber = document.getElementById('<%= txtCustomerFaxPhone.ClientID %>');

                var bhomePhoneNumber = document.getElementById('<%= txtBillingCustomerHomePhoneNumber.ClientID %>');
                var bfaxPhoneNumber = document.getElementById('<%= txtBillingCustomerFaxPhone.ClientID %>');

                //Check if any of the phone values are fully filled
                if ((homePhoneNumber.value == "") && (faxPhoneNumber.value == "") && (bhomePhoneNumber.value == "") && (bfaxPhoneNumber.value == "")) {
                    ValidatorEnable($get('<%= fakeValidator.ClientID %>'), true);
                }
                else {
                    ValidatorEnable($get('<%= fakeValidator.ClientID %>'), false);
                }

                ApplyMaskValidation('<%= fakeWorkPhoneValidator.ClientID %>', homePhoneNumber);
                ApplyMaskValidation('<%= fakeFaxPhoneValidator.ClientID %>', faxPhoneNumber);
                ApplyMaskValidation('<%= fakeBWorkPhoneValidator.ClientID %>', bhomePhoneNumber);
                ApplyMaskValidation('<%= fakeBFaxPhoneValidator.ClientID %>', bfaxPhoneNumber);
            }
            else if (source == 'Contact') {

                var homePhoneNumber = document.getElementById('<%= txtContactHomePhoneNumber.ClientID %>');

                var faxPhoneNumber = document.getElementById('<%= txtContactFaxPhone.ClientID %>');

                var bhomePhoneNumber = document.getElementById('<%= txtBillingContactHomePhoneNumber.ClientID %>');

                var bfaxPhoneNumber = document.getElementById('<%= txtBillingContactFaxPhone.ClientID %>');

                //Check if any of the phone values are fully filled
                if ((homePhoneNumber.value == "") && (faxPhoneNumber.value == "") &&
                    (bhomePhoneNumber.value == "") && (bfaxPhoneNumber.value == "")) {
                    ValidatorEnable($get('<%= fakeValidator.ClientID %>'), true);
                }
                else {
                    ValidatorEnable($get('<%= fakeValidator.ClientID %>'), false);
                }

                //Check if format is ok
                ApplyMaskValidation('<%= fakeWorkPhoneValidator.ClientID %>', homePhoneNumber);
                ApplyMaskValidation('<%= fakeBWorkPhoneValidator.ClientID %>', bhomePhoneNumber);
                ApplyMaskValidation('<%= fakeBFaxPhoneValidator.ClientID %>', bfaxPhoneNumber);

                //Check if phonelis has any wrong format
                var countryCode = '<%= ddlContactCountryCode.ClientID %>';
                var selectedIndex = $find(countryCode).get_selectedIndex();
                var cCode = $get(countryCode).getElementsByTagName('LI')[selectedIndex].outerText;
                var clearString = "";

                var phoneList = hidden.value.split('|');

                for (var i = 0; i < phoneList.length; i++) {
                    var values = phoneList[i].split(';');
                    clearString = replaceAll(values[2], "(", "");
                    clearString = replaceAll(clearString, ")", "");
                    clearString = replaceAll(clearString, "-", "");
                    clearString = replaceAll(clearString, " ", "");

                    if ((cCode == "US" || cCode == "CN") && (clearString.length < 10 && clearString.length > 0))
                        ValidatorEnable($get('<%= fakePhoneListValidator.ClientID %>'), true);
                    else if (cCode == "MX" && (clearString.length < 11 && clearString.length > 0))
                        ValidatorEnable($get('<%= fakePhoneListValidator.ClientID %>'), true);
                    else
                        ValidatorEnable($get('<%= fakePhoneListValidator.ClientID %>'), false);
                }
            }
        }

        function ApplyMaskValidation(validator, textBox) {

            var countryCode = '<%= ddlContactCountryCode.ClientID %>';
            var selectedIndex = $find(countryCode).get_selectedIndex();
            var cCode = $get(countryCode).getElementsByTagName('LI')[selectedIndex].outerText;
            var clearString = replaceAll(textBox.value, "(", "");
            clearString = replaceAll(clearString, ")", "");
            clearString = replaceAll(clearString, "-", "");
            clearString = replaceAll(clearString, " ", "");

            if ((cCode == "US" || cCode == "CN") && (clearString.length < 10 && clearString.length > 0))
                ValidatorEnable($get(validator), true);
            else if (cCode == "MX" && (clearString.length < 11 && clearString.length > 0))
                ValidatorEnable($get(validator), true);
            else
                ValidatorEnable($get(validator), false);
        }

        function ApplyAPMaskValidation() {
            var validator = '<%= fakeAdditionalPhoneValidator.ClientID %>';
            var textBox = document.getElementById('<%= txtAdditionalContactNumber.ClientID %>');
            var countryCode = '<%= ddlContactCountryCode.ClientID %>';
            var selectedIndex = $find(countryCode).get_selectedIndex();
            var cCode = $get(countryCode).getElementsByTagName('LI')[selectedIndex].outerText;
            var clearString = replaceAll(textBox.value, "(", "");
            clearString = replaceAll(clearString, ")", "");
            clearString = replaceAll(clearString, "-", "");
            clearString = replaceAll(clearString, " ", "");

            if ((cCode == "US" || cCode == "CN") && (clearString.length < 10 && clearString.length > 0))
                ValidatorEnable($get(validator), true);
            else if (cCode == "MX" && (clearString.length < 11 && clearString.length > 0))
                ValidatorEnable($get(validator), true);
            else
                ValidatorEnable($get(validator), false);
        }

        function replaceAll(string, token, newtoken) {
            if (undefined == string)
                return '';

            while (string.indexOf(token) != -1) {
                string = string.replace(token, newtoken);
            }
            return string;
        }

        var hidden = document.getElementById('<%= hfPhoneNumbers.ClientID %>');
        var tbPhoneNumbers = document.getElementById("tbPhoneNumbers");

        function AddPhone() {
            ApplyAPMaskValidation();

            if (Page_ClientValidate('Add')) {
                var type = $find('<%= ddlAdditionalContactType.ClientID %>');
                var number = document.getElementById('<%= txtAdditionalContactNumber.ClientID %>');
                var errorDiv = document.getElementById('errorDiv');
                var errorLabel = document.getElementById('<%= lblNumberError.ClientID %>');
                var showError = false;
                var extension = document.getElementById('<%= txtExtension.ClientID %>');
                var type = $find('<%= ddlAdditionalContactType.ClientID %>');


                if (null != hidden && null != type && null != number) {

                    if ("" != number.value && type._hiddenFieldControl.value >= 0) {
                        var numberToInsert = '0;' + type._textBoxControl.value + ';' + number.value;

                        if (! ~hidden.value.indexOf(numberToInsert)) {
                            if ("" != hidden.value) {
                                switch (type._textBoxControl.value) {
                                    case 'Work':
                                        if (hidden.value.indexOf('Work') == -1) {
                                            if (extension.value != '')
                                                numberToInsert = numberToInsert + ' ext ' + extension.value;
                                            
                                                hidden.value += '|' + numberToInsert;

                                                document.getElementById('<%=txtContactHomePhoneNumber.ClientID %>').value = number.value;

                                        }
                                        break;
                                    case 'Home':
                                        if (hidden.value.indexOf('Home') == -1)
                                            hidden.value += '|' + numberToInsert;
                                        break;
                                    case 'Cell':
                                        if (hidden.value.indexOf('Cell') == -1)
                                            hidden.value += '|' + numberToInsert;
                                        break;
                                    case 'Fax':
                                        if (hidden.value.indexOf('Fax') == -1) {
                                            hidden.value += '|' + numberToInsert;

                                            document.getElementById('<%=txtContactFaxPhone.ClientID %>').value = number.value;
                                        }
                                        break;
                                    default:
                                }
                            }
                            else {

                                if (type._textBoxControl.value == 'Work') {
                                    if (extension.value != '')
                                        hidden.value = numberToInsert + ' ext ' + extension.value.trim();
                                    else
                                        hidden.value = numberToInsert;

                                    document.getElementById('<%=txtContactHomePhoneNumber.ClientID %>').value = number.value;
                                }
                                else if (type._textBoxControl.value == 'Fax') {
                                    hidden.value = numberToInsert;
                                    document.getElementById('<%=txtContactFaxPhone.ClientID %>').value = number.value;
                                }
                                else
                                    hidden.value = numberToInsert;
                            }

                            CreatePhoneGrid();
                            number.value = "";
                            extension.value = '';
                        }
                        else {
                            errorLabel.innerHTML = 'The specified Phone Number already exists in the list.';
                            showError = true;
                        }
                    }
                    else {
                        errorLabel.innerHTML = 'The specified Phone Type/Number is incorrect, please verify the information.';
                        showError = true;
                    }

                    if (showError)
                        errorDiv.style.display = 'block';
                }
            } else {
                CreatePhoneGrid();
                this.focus();
            }
        }

        function RemovePhone(index) {
            var replace = '';
            var row = tbPhoneNumbers.rows[index];

            var id = row.cells[0].innerHTML;
            var type = row.cells[1].innerHTML;
            var phone = row.cells[2].innerHTML;

            //try to replace if on first/middle position of string
            replace = id + ';' + type + ';' + phone + '|';
            hidden.value = hidden.value.replace(replace, "");

            //try to replace if on last position of string
            replace = '|' + id + ';' + type + ';' + phone;
            hidden.value = hidden.value.replace(replace, "");

            //try to replace if only element on string
            replace = id + ';' + type + ';' + phone;
            hidden.value = hidden.value.replace(replace, "");

            if (type == 'Work')
                document.getElementById('<%=txtContactHomePhoneNumber.ClientID %>').value = '';
            else if (type == 'Fax')
                document.getElementById('<%=txtContactFaxPhone.ClientID %>').value = '';



            CreatePhoneGrid();
        }


        function SetAutocompleteField(Name, ID, Field) {
            var actField = $find(Field);
            actField.raiseItemSelected(new Sys.Extended.UI.AutoCompleteItemEventArgs(null, Name, ID));
        }

        //Dinamic Bind of the PhoneNumber Grid
        function CreatePhoneGrid() {
            try {

                var pnlGrid = document.getElementById('<%= pnlGrid.ClientID %>');
                var pnlNoRowsPhone = document.getElementById('<%= pnlNoRowsPhone.ClientID %>');
                //Clear Table
                rowCount = tbPhoneNumbers.rows.length;

                for (var i = rowCount - 1; i > 0; i--) {
                    tbPhoneNumbers.deleteRow(i);
                }

                if ("" != hidden.value) {
                    var phoneList = hidden.value.split('|');

                    //Insert New Values
                    for (var i = 0; i < phoneList.length; i++) {
                        var values = phoneList[i].split(';');
                        var rowIndex = parseInt(i + 1);

                        var row = tbPhoneNumbers.insertRow(rowIndex);

                        var cell1 = row.insertCell(0);
                        cell1.innerHTML = values[0];
                        cell1.style.display = 'none';

                        var cell2 = row.insertCell(1);
                        cell2.innerHTML = values[1];

                        var cell3 = row.insertCell(2);
                        cell3.innerHTML = values[2];

                        var cell4 = row.insertCell(3);
                        var button = document.createElement("input");
                        button.id = rowIndex;
                        button.type = "button";
                        button.setAttribute("className", "linkButtonStyle");
                        button.value = "Remove";
                        button.onclick = function () { RemovePhone(this.id); };
                        cell4.appendChild(button);
                    }
                }

                pnlGrid.style.display = (tbPhoneNumbers.rows.length > 1) ? "" : "none";
                pnlNoRowsPhone.style.display = (tbPhoneNumbers.rows.length > 1) ? "none" : "";

            } catch (e) {

            }
        }

        var scriptManager = Sys.WebForms.PageRequestManager.getInstance();
        scriptManager.add_endRequest(function () {
            hidden = document.getElementById('<%= hfPhoneNumbers.ClientID %>');
            tbPhoneNumbers = document.getElementById("tbPhoneNumbers");
            CreatePhoneGrid();
        });

        $(document).ready(function () { disableAll(); CreatePhoneGrid(); });

        function disableAll() {
            $('.usercontrolDiv').attr('disabled', true);
        }

        function enableAll(holder) {
            if (holder != "0")
                $('.usercontrolDiv').attr('disabled', false);
        }

        // Collapse-Expand functionality
        function CollapseExpand(Button, selector) {
            var line = $("." + selector);
            var button = $("#" + Button);
            var expandedItems = $get('<%=hfExpandedItems.ClientID %>').value;

            line.toggle();
            button.toggleClass("Expand");
            button.toggleClass("Collapse");

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
