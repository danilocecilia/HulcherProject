<%@ Page Language="C#" MasterPageFile="~/ContentPage.master" AutoEventWireup="true"
    CodeBehind="ResourceAllocation.aspx.cs" Inherits="Hulcher.OneSource.CustomerService.Web.ResourceAllocation" %>

<%@ MasterType TypeName="Hulcher.OneSource.CustomerService.Web.ContentPage" %>
<%@ Register TagName="CollapseHolder" TagPrefix="uc" Src="~/UserControls/CollapseHolder.ascx" %>
<%@ Register Src="~/UserControls/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/AutoCompleteTextbox.ascx" TagName="AutoCompleteTextbox"
    TagPrefix="uc1" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
    <link href="../../Styles/ResourceAllocation.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Forms.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="PageContent" ContentPlaceHolderID="Content" runat="server">
    <div id="divReserveList">
        <uc:CollapseHolder ID="chReserveList" runat="server" GridViewCssClass="ScrollableGridView"
            Collapsed="false" Visible="false">
            <Header>
                <asp:Label ID="lblReserveList" runat="server" Text="Reserve List" />
            </Header>
            <Content>
                <div class="gridview">
                    <div class="gridview">
                        <asp:ScrollableGridView ID="gvReserveList" runat="server" CssClass="ScrollableGridView"
                            AutoGenerateColumns="false" OnRowDataBound="gvReserveList_RowDataBound" ShowFooter="false">
                            <Columns>
                                <asp:BoundField HeaderText="Division" DataField="DivisionName" />
                                <asp:BoundField HeaderText="Equipment Type / Employee Name" DataField="Name" />
                                <asp:BoundField HeaderText="Reserved" DataField="NumberOfReserves" />
                                <asp:BoundField HeaderText="Available" DataField="Available" />
                                <asp:BoundField HeaderText="Duration" DataField="Duration" />
                                <asp:TemplateField HeaderText="Start DateTime">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStartDatetime" runat="server" Text='<%# Eval("StartDateTime", "{0:MM/dd/yyyy HH:mm}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:ScrollableGridView>
                    </div>
                </div>
            </Content>
        </uc:CollapseHolder>
    </div>
    <br />
    <div id="divAddResource">
        <uc:CollapseHolder ID="chAddResource" runat="server" GridViewCssClass="ScrollableGridView"
            Collapsed="true">
            <Header>
                <asp:Label ID="lblAddResource" runat="server" Text="Add Resources" />
            </Header>
            <Content>
                <asp:UpdatePanel ID="updAddResource" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="pnlAddResource" runat="server" DefaultButton="btnFindEquipmentAdd">
                            <div class="filter">
                                <div class="control">
                                    <div class="title">
                                        <asp:Label ID="lblFilterEquipmentAdd" runat="server" Text="Filter Listing By: " />
                                    </div>
                                    <div class="checkbox">
                                        <asp:CheckBox ID="chkReserveEquipments" runat="server" Text="Reserved Equipment Types Only"
                                            Checked="true" Visible="false" />
                                    </div>
                                    <div class="combo">
                                        <asp:ComboBox ID="cbFilterEquipmentAdd" runat="server" CssClass="WindowsStyle" AutoCompleteMode="SuggestAppend"
                                            DropDownStyle="DropDown" CaseSensitive="false" RenderMode="Inline">
                                            <asp:ListItem Text="- Select One -" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Division #" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Division State Name" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Division State Acronym" Value="9"></asp:ListItem>
                                            <asp:ListItem Text="Unit #" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Combo #" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="Status" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="Job Location" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="Call type" Value="7"></asp:ListItem>
                                            <asp:ListItem Text="Job #" Value="8"></asp:ListItem>
                                        </asp:ComboBox>
                                    </div>
                                    <div class="textbox">
                                        <asp:TextBox ID="txtFilterValueEquipmentAdd" runat="server" CssClass="input" />
                                        <asp:Button ID="btnFindEquipmentAdd" runat="server" Text="Find" CssClass="btn" OnClick="btnFindEquipmentAdd_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="gridview">
                                <asp:Label ID="lblGridTitleEquipmentAdd" runat="server" Text="Select Equipment" />
                                <asp:HiddenField ID="hfOrderBy" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hfExpandedJobs" runat="server" />
                                <asp:Button ID="btnFakeSort" runat="server" OnClick="btnFakeSort_Click" Style="display: none;" />
                                <asp:Repeater ID="rptEquipmentsAdd" runat="server" OnItemDataBound="rptEquipmentsAdd_ItemDataBound">
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
                                                            <th id="thDivisionName" runat="server" class="header" style="border: 1px, solid, #E6EEEE;"
                                                                onclick="SetOrderBy('1', this);">
                                                                <asp:Label ID="rpt1Header1" CssClass="MarginRight" runat="server" Text="DIV #"></asp:Label>
                                                            </th>
                                                            <th id="thDivisionState" runat="server" class="header" onclick="SetOrderBy('2', this);">
                                                                <asp:Label ID="rpt1Header2" CssClass="MarginRight" runat="server" Text="DIV. State"></asp:Label>
                                                            </th>
                                                            <th id="thComboName" runat="server" class="header" onclick="SetOrderBy('3', this);">
                                                                <asp:Label ID="rpt1Header3" CssClass="MarginRight" runat="server" Text="Combo"></asp:Label>
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
                                                            <th id="thEquipmentStatus" runat="server" class="header" onclick="SetOrderBy('9', this);">
                                                                <asp:Label ID="rpt1Header9" CssClass="MarginRight" runat="server" Text="Oper. Status"></asp:Label>
                                                            </th>
                                                            <th id="thJobNumber" runat="server" class="header" onclick="SetOrderBy('10', this);">
                                                                <asp:Label ID="rpt1Header10" CssClass="MarginRight" runat="server" Text="Job #"></asp:Label>
                                                            </th>
                                                            <th id="thChecked" runat="server" class="header" style="cursor: default">
                                                                <asp:Label ID="rpt1Header11" CssClass="MarginRight" runat="server" Text=""></asp:Label>
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="even" id="trItem" runat="server">
                                            <td style="width: 15px;">
                                                <div class="Expand" id="divExpand" runat="server">
                                                </div>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDivision" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDivisionState" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblComboName" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblUnitNumber" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDescriptor" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblStatus" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblJobLocation" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:HyperLink ID="hlType" runat="server" Text="" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblOperationStatus" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:HyperLink ID="hlJobNumber" runat="server" Text="" />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkEquipmentAdd" runat="server" />
                                                <asp:Label ID="lblEquipmentId" runat="server" Visible="false" Text="" />
                                                <asp:Label ID="lblIsCombo" runat="server" Visible="false" Text="" />
                                                <asp:Label ID="lblComboId" runat="server" Visible="false" Text="" />
                                                <asp:Label ID="lblIsDivConf" runat="server" Visible="false" Text="" />
                                                <asp:Label ID="lblPermitExpired" runat="server" Visible="false" Text="" />
                                                <asp:Label ID="lblIsComboUnit" runat="server" Visible="false" Text="" />
                                                <asp:Label ID="lblIsWhiteLight" runat="server" Visible="false" Text="" />
                                            </td>
                                        </tr>
                                        <asp:Repeater ID="rptEquipmentCombo" runat="server" OnItemDataBound="rptEquipmentCombo_ItemDataBound">
                                            <ItemTemplate>
                                                <tr class="even" id="trComboItem" runat="server">
                                                    <td style="width: 15px;">
                                                        <div class="Expand" id="divExpand" runat="server">
                                                        </div>
                                                    </td>
                                                    <td class="blue">
                                                        <asp:Label ID="lblDivision" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <td class="blue">
                                                        <asp:Label ID="lblDivisionState" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <td class="blue">
                                                        <asp:Label ID="lblComboName" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <td class="blue">
                                                        <asp:Label ID="lblUnitNumber" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <td class="blue">
                                                        <asp:Label ID="lblDescriptor" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <td class="blue">
                                                        <asp:Label ID="lblStatus" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td class="blue">
                                                        <asp:Label ID="lblJobLocation" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <td class="blue">
                                                        <asp:HyperLink ID="hlType" runat="server" Text="" />
                                                    </td>
                                                    <td class="blue">
                                                        <asp:Label ID="lblOperationStatus" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <td class="blue">
                                                        <asp:HyperLink ID="hlJobNumber" runat="server" Text="" />
                                                    </td>
                                                    <td class="blue">
                                                        <asp:CheckBox ID="chkEquipmentCombo" runat="server" />
                                                        <asp:Label ID="lblEquipmentId" runat="server" Visible="false" Text="" />
                                                        <asp:Label ID="lblIsCombo" runat="server" Visible="false" Text="" />
                                                        <asp:Label ID="lblComboId" runat="server" Visible="false" Text="" />
                                                        <asp:Label ID="lblIsDivConf" runat="server" Visible="false" Text="" />
                                                        <asp:Label ID="lblPermitExpired" runat="server" Visible="false" Text="" />
                                                        <asp:Label ID="lblIsComboUnit" runat="server" Visible="false" Text="" />
                                                         <asp:Label ID="lblIsWhiteLight" runat="server" Visible="false" Text="" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr class="odd" id="trItem" runat="server">
                                            <td style="width: 15px;">
                                                <div class="Expand" id="divExpand" runat="server">
                                                </div>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDivision" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDivisionState" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblComboName" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblUnitNumber" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDescriptor" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblStatus" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblJobLocation" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:HyperLink ID="hlType" runat="server" Text="" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblOperationStatus" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:HyperLink ID="hlJobNumber" runat="server" Text="" />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkEquipmentAdd" runat="server" />
                                                <asp:Label ID="lblEquipmentId" runat="server" Visible="false" Text="" />
                                                <asp:Label ID="lblIsCombo" runat="server" Visible="false" Text="" />
                                                <asp:Label ID="lblComboId" runat="server" Visible="false" Text="" />
                                                <asp:Label ID="lblIsDivConf" runat="server" Visible="false" Text="" />
                                                <asp:Label ID="lblPermitExpired" runat="server" Visible="false" Text="" />
                                                <asp:Label ID="lblIsComboUnit" runat="server" Visible="false" Text="" />
                                                 <asp:Label ID="lblIsWhiteLight" runat="server" Visible="false" Text="" />
                                            </td>
                                        </tr>
                                        <asp:Repeater ID="rptEquipmentCombo" runat="server" OnItemDataBound="rptEquipmentCombo_ItemDataBound">
                                            <ItemTemplate>
                                                <tr class="odd" id="trComboItem" runat="server">
                                                    <td style="width: 15px;">
                                                        <div class="Expand" id="divExpand" runat="server">
                                                        </div>
                                                    </td>
                                                    <td class="blue">
                                                        <asp:Label ID="lblDivision" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <td class="blue">
                                                        <asp:Label ID="lblDivisionState" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <td class="blue">
                                                        <asp:Label ID="lblComboName" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <td class="blue">
                                                        <asp:Label ID="lblUnitNumber" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <td class="blue">
                                                        <asp:Label ID="lblDescriptor" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <td class="blue">
                                                        <asp:Label ID="lblStatus" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td class="blue">
                                                        <asp:Label ID="lblJobLocation" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <td class="blue">
                                                        <asp:HyperLink ID="hlType" runat="server" Text="" />
                                                    </td>
                                                    <td class="blue">
                                                        <asp:Label ID="lblOperationStatus" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <td class="blue">
                                                        <asp:HyperLink ID="hlJobNumber" runat="server" Text="" />
                                                    </td>
                                                    <td class="blue">
                                                        <asp:CheckBox ID="chkEquipmentCombo" runat="server" />
                                                        <asp:Label ID="lblEquipmentId" runat="server" Visible="false" Text="" />
                                                        <asp:Label ID="lblIsCombo" runat="server" Visible="false" Text="" />
                                                        <asp:Label ID="lblComboId" runat="server" Visible="false" Text="" />
                                                        <asp:Label ID="lblIsDivConf" runat="server" Visible="false" Text="" />
                                                        <asp:Label ID="lblPermitExpired" runat="server" Visible="false" Text="" />
                                                        <asp:Label ID="lblIsComboUnit" runat="server" Visible="false" Text="" />
                                                         <asp:Label ID="lblIsWhiteLight" runat="server" Visible="false" Text="" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </AlternatingItemTemplate>
                                    <FooterTemplate>
                                        </tbody> </table> </div> </div>
                                    </FooterTemplate>
                                </asp:Repeater>
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
                            </div>
                            <div style="width: 100%; text-align: right;">
                                <asp:Button ID="btnAddShoppingCartEquipmentAdd" runat="server" Text="Add to Resources"
                                    CssClass="btn" OnClick="btnAddShoppingCartEquipmentAdd_Click" CausesValidation="false" />
                            </div>
                            <br />
                            <hr width="95%" />
                            <br />
                            <div class="filter">
                                <asp:Panel ID="pnlFilterEmployee" runat="server" DefaultButton="btnFindEmployeeAdd"
                                    class="control">
                                    <div class="title">
                                        <asp:Label ID="lblFilterEmployeeAdd" runat="server" Text="Filter Listing By: " />
                                    </div>
                                    <div class="combo">
                                        <asp:ComboBox ID="cbFilterEmployeeAdd" runat="server" CssClass="WindowsStyle" AutoCompleteMode="SuggestAppend"
                                            DropDownStyle="DropDown" CaseSensitive="false" RenderMode="Inline">
                                            <asp:ListItem Text="- Select One -" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Division" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Division State Name" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Division State Acronym" Value="7"></asp:ListItem>
                                            <asp:ListItem Text="Status" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Job #" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="Position" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="Employee Name" Value="6"></asp:ListItem>
                                        </asp:ComboBox>
                                    </div>
                                    <div class="textbox">
                                        <asp:TextBox ID="txtFilterValueEmployeeAdd" runat="server" CssClass="input" />
                                        <asp:Button ID="btnFindEmployeeAdd" runat="server" Text="Find" CssClass="btn" OnClick="btnFindEmployeeAdd_Click"
                                            Style="display: inline-block" />
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="gridview">
                                <asp:Label ID="lblGridTitleEmployeeAdd" runat="server" Text="Select Employee(s)"></asp:Label>
                                <asp:ScrollableGridView ID="gvEmployeesAdd" runat="server" CssClass="ScrollableGridView"
                                    AutoGenerateColumns="false" ShowFooter="false" OnRowDataBound="gvEmployeesAdd_RowDataBound">
                                    <Columns>
                                        <asp:BoundField HeaderText="" DataField="ID" Visible="false" />
                                        <asp:BoundField HeaderText="DIV #" DataField="DivisionName" HeaderStyle-Width="40px" />
                                        <asp:BoundField HeaderText="State" DataField="DivisionState" />
                                        <asp:BoundField HeaderText="Employee Name" DataField="EmployeeName" />
                                        <asp:BoundField HeaderText="Position" DataField="Position" />
                                        <asp:TemplateField HeaderText="Profile" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkProfileAdd" runat="server" Text="View Profile" Font-Underline="true"
                                                    CommandName="Profile" CommandArgument='<%# Eval("EmployeeId") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Status" DataField="Assigned" ItemStyle-Font-Bold="true" />
                                        <asp:TemplateField HeaderText="Job #" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lnkJobEmployeeAdd" runat="server" Text='<%# Eval("JobNumber") %>'
                                                    Font-Underline="true" CommandName="JobNumber" CommandArgument='<%# Eval("JobId") %>'></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkEmployeeAdd" runat="server" Enabled='<%# bool.Parse(Eval("SelectAvailable").ToString()) %>' />
                                                <asp:Label ID="lblEmployeeId" runat="server" Visible="false" Text='<%# Eval("EmployeeId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:ScrollableGridView>
                            </div>
                            <div style="width: 100%; text-align: right;">
                                <asp:Button ID="btnAddShoppingCartEmployeeAdd" runat="server" Text="Add to Resources"
                                    CssClass="btn" OnClick="btnAddShoppingCartEmployeeAdd_Click" CausesValidation="false" />
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </Content>
        </uc:CollapseHolder>
    </div>
    <br />
    <div id="divReserveResource">
        <uc:CollapseHolder ID="chReserveResource" runat="server" GridViewCssClass="ScrollableGridView"
            Collapsed="true">
            <Header>
                <asp:Label ID="lblReserveResource" runat="server" Text="Reserve Resources" />
            </Header>
            <Content>
                <asp:UpdatePanel ID="upReserveResource" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="pnlReserveResource" runat="server" DefaultButton="btnFindResourceReserve">
                            <asp:ValidationSummary ID="vsReserveEquipment" runat="server" CssClass="errorbox"
                                ValidationGroup="ReserveEquipment" HeaderText="Please correct the following information" />
                            <asp:Panel ID="pnlPopUp" runat="server" CssClass="updateProgress" Style="width: 200px;">
                                <div align="center" style="margin-top: 13px;">
                                    View the job details for the selected "Assigned" equipment type for:
                                    <br />
                                    <br />
                                    <asp:Repeater ID="rptJobDetails" runat="server" OnItemDataBound="rptJobDetails_ItemDataBound">
                                        <ItemTemplate>
                                            <asp:Label ID="lblJobNumber" runat="server" Visible="false"></asp:Label>
                                            <asp:HyperLink ID="hlJobNumber" runat="server"></asp:HyperLink><br />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <br />
                                    <asp:Button ID="btnClosePopup" runat="server" Text="Close" CssClass="btn" CausesValidation="false" />
                                </div>
                            </asp:Panel>
                            <asp:Button ID="btnPopupExtender" runat="server" Text="open" Style="display: none;" />
                            <asp:ModalPopupExtender ID="mdlPopUpExtender" runat="server" BackgroundCssClass="modalBackground"
                                TargetControlID="btnPopupExtender" PopupControlID="pnlPopUp" OkControlID="btnClosePopup">
                            </asp:ModalPopupExtender>
                            <div>
                                <div class="Row">
                                    <div class="filterText">
                                        <asp:Label ID="lblEquip" runat="server" Text="Equipment Type Required: " />
                                    </div>
                                    <div class="filterCombo">
                                        <asp:ComboBox ID="cbEquipType" runat="server" CssClass="WindowsStyle" AutoCompleteMode="SuggestAppend"
                                            DropDownStyle="DropDown" CaseSensitive="false" RenderMode="Inline" />
                                    </div>
                                </div>
                                <div class="Row">
                                    <div class="filterText">
                                        <asp:Label ID="lblJobLocation" runat="server" Text="Division Location: " />
                                    </div>
                                    <div class="filterCombo">
                                        <uc1:AutoCompleteTextbox ID="actLocation" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                            TextBoxWidth="120px" GridViewIdName="ID" DisplayField="" AutoPostBack="false"
                                            RequiredField="false" WindowTitle="Quick Job - Find State" ErrorMessage="State field is required"
                                            AutoCompleteSource="StateByDivision" ColumnHeaderList="Country,Acronym,Name"
                                            ColumnValueList="CS_Country.Name,Acronym,Name" ServiceMethod="GetStateByDivisionList"
                                            TextBoxCssClass="input" MinimumPrefixLength="2" />
                                    </div>
                                </div>
                                <div class="Row">
                                    <div class="filterText">
                                        <asp:Label ID="lblDesiredDivision" runat="server" Text="Desired Division: " />
                                    </div>
                                    <div class="filterCombo">
                                        <uc1:AutoCompleteTextbox ID="actDivision" runat="server" ServiceMethod="GetDivisionList"
                                            TextBoxWidth="120px" GridViewButtonImageUrl="~/Images/money.png" GridViewIdName="ID"
                                            DisplayField="Division" FilterId="0" ContextKey="0" AutoPostBack="false" RequiredField="false"
                                            WindowTitle="Quick Job - Find Division" ErrorMessage="Primary Division field is required"
                                            AutoCompleteSource="Division" ColumnHeaderList="Name,Description" TextBoxCssClass="input"
                                            ColumnValueList="Name,Description" BehaviorId="Division" />
                                        <asp:Button ID="btnFindResourceReserve" runat="server" CssClass="btn" Text="Find"
                                            OnClick="btnFindResourceReserve_Click" />
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="gridview">
                                <asp:ScrollableGridView ID="gvReserveEquipment" runat="server" CssClass="ScrollableGridView"
                                    AutoGenerateColumns="false" ShowFooter="false" OnRowDataBound="gvReserveEquipment_RowDataBound">
                                    <Columns>
                                        <asp:BoundField HeaderText="" DataField="ID" Visible="false" />
                                        <asp:BoundField HeaderText="DIV #" DataField="Division" HeaderStyle-Width="40px" />
                                        <asp:BoundField HeaderText="State" DataField="State" />
                                        <asp:BoundField HeaderText="Equip. Type" DataField="EquipmentType" />
                                        <asp:BoundField HeaderText="Available" DataField="Available" />
                                        <asp:BoundField HeaderText="Reserved" DataField="Reserve" />
                                        <asp:TemplateField HeaderText="Assigned" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkAssigned" runat="server" Text='<%# Eval("Assigned") %>' CommandArgument='<%# Eval("EquipmentTypeID") %>'
                                                    OnCommand="lnkAssigned_OnCommand" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkEquipmentResereve" runat="server" />
                                                <asp:TextBox ID="txtEquipmentReserveQuantity" runat="server" CssClass="Text" Width="30px"
                                                    Text="1" ValidationGroup="ReserveEquipment" />
                                                <asp:FilteredTextBoxExtender ID="fteEquipmentReserveQuantity" runat="server" TargetControlID="txtEquipmentReserveQuantity"
                                                    FilterType="Numbers" FilterMode="ValidChars" />
                                                <asp:RequiredFieldValidator ID="rfvEquipmentReserveQuantity" runat="server" ControlToValidate="txtEquipmentReserveQuantity"
                                                    Display="Dynamic" ErrorMessage="The Quantity field is required to reserve an equipment"
                                                    SetFocusOnError="true" Text="*" ValidationGroup="ReserveEquipment" />
                                                <asp:Label ID="lblEquipmentTypeId" runat="server" Visible="false" Text='<%# Eval("EquipmentTypeID") %>' />
                                                <asp:Label ID="lblDivisonId" runat="server" Visible="false" Text='<%# Eval("DivisionID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:ScrollableGridView>
                            </div>
                            <div style="width: 100%; text-align: right;">
                                <asp:Button ID="btnAddShoppingCartEquipmentReserve" runat="server" Text="Add to Resources"
                                    CssClass="btn" OnClick="btnAddShoppingCartEquipmentReserve_Click" CausesValidation="true"
                                    ValidationGroup="ReserveEquipment" />
                            </div>
                            <br />
                            <hr width="95%" />
                            <br />
                            <div class="filter">
                                <div class="title">
                                    <asp:Label ID="lblFilterListingBy" runat="server" Text="Filter Listing By:" />
                                </div>
                                <div class="control">
                                    <div class="combo">
                                        <asp:ComboBox ID="cbEmployee" runat="server" CssClass="WindowsStyle" AutoCompleteMode="SuggestAppend"
                                            DropDownStyle="DropDown" CaseSensitive="false" RenderMode="Inline">
                                            <asp:ListItem Text="- Select One -" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Division" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Division State Name" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Division State Acronym" Value="7"></asp:ListItem>
                                            <asp:ListItem Text="Status" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Job #" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="Position" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="Employee Name" Value="6"></asp:ListItem>
                                        </asp:ComboBox>
                                    </div>
                                    <div class="textbox">
                                        <asp:TextBox ID="txtFilterValueEmployeeReserve" runat="server" CssClass="input"></asp:TextBox>
                                        <asp:Button ID="btnFindEmployeeReserve" runat="server" Text="Find" CssClass="btn"
                                            OnClick="btnFindEmployeeReserve_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="gridview">
                                <asp:ScrollableGridView ID="gvReserveEmployee" runat="server" CssClass="ScrollableGridView"
                                    AutoGenerateColumns="false" ShowFooter="false" OnRowDataBound="gvReserveEmployee_RowDataBound">
                                    <Columns>
                                        <asp:BoundField HeaderText="" DataField="ID" Visible="false" />
                                        <asp:BoundField HeaderText="DIV #" DataField="DivisionName" HeaderStyle-Width="40px" />
                                        <asp:BoundField HeaderText="State" DataField="State" />
                                        <asp:BoundField HeaderText="Employee Name" DataField="EmployeeName" />
                                        <asp:BoundField HeaderText="Position" DataField="Position" />
                                        <asp:TemplateField HeaderText="Profile" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkProfileAdd" runat="server" Text="View Profile" Font-Underline="true"
                                                    CommandName="Profile" CommandArgument='<%# Eval("EmployeeId") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Status" DataField="Assigned" ItemStyle-Font-Bold="true" />
                                        <asp:TemplateField HeaderText="Job #" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lnkJobEmployeeReserve" runat="server" Text='<%# Eval("JobNumber") %>'
                                                    Font-Underline="true" CommandName="JobNumber" CommandArgument='<%# Eval("JobId") %>'></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkEmployeeReserve" runat="server" />
                                                <asp:Label ID="lblEmployeeId" runat="server" Visible="false" Text='<%# Eval("EmployeeId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:ScrollableGridView>
                            </div>
                            <div style="width: 100%; text-align: right;">
                                <asp:Button ID="btnAddShoppingCartEmployeeReserve" runat="server" Text="Add to Resources"
                                    ClientIDMode="Static" CssClass="btn" OnClick="btnAddShoppingCartEmployeeReserve_Click"
                                    CausesValidation="true" ValidationGroup="ReserveEmployee" />
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </Content>
        </uc:CollapseHolder>
    </div>
    <div id="divShoppingCart">
        <br />
        <hr width="95%" />
        <br />
        <div id="div1">
            <uc:CollapseHolder ID="chTransferShoppingCart" runat="server" Collapsed="false">
                <Header>
                    <asp:Label ID="lblTransferShoppingCart" runat="server" Text="Assigned Resources" />
                </Header>
                <Content>
                    <asp:UpdatePanel ID="uplTransferShoppingCart" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:ScrollableGridView ID="gvTransferShopCart" runat="server" CssClass="ScrollableGridView"
                                AutoGenerateColumns="false" ShowFooter="false" SkinID="NonSortable" OnRowDataBound="gvTransferShopCart_RowDataBound">
                                <Columns>
                                    <asp:BoundField HeaderText="ID" DataField="ID" Visible="false" />
                                    <asp:BoundField HeaderText="Type" DataField="Type" Visible="false" />
                                    <asp:BoundField HeaderText="Combo # / Unit #" DataField="UnitNumber" Visible="true" />
                                    <asp:BoundField HeaderText="Selected Item" DataField="Name" ItemStyle-Font-Bold="true" />
                                    <asp:TemplateField HeaderText="Duration" ItemStyle-Width="45px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDuration" runat="server" MaxLength="3" CssClass="input" Text='<%# Eval("Duration") %>'
                                                Width="20px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvDuration" runat="server" Text="*" ControlToValidate="txtDuration"
                                                Display="Dynamic" ErrorMessage="The Duration field is required" EnableClientScript="true"
                                                ValidationGroup="ShoppingCart" />
                                            <asp:FilteredTextBoxExtender ID="ftbExtender" runat="server" FilterType="Numbers"
                                                TargetControlID="txtDuration" />
                                            <asp:RangeValidator ID="rvDuration" ControlToValidate="txtDuration" MinimumValue="1"
                                                MaximumValue="90" Type="Integer" EnableClientScript="true" Text="*" Display="Dynamic"
                                                ValidationGroup="ShoppingCart" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Start Date/Time" ItemStyle-Width="160px">
                                        <ItemTemplate>
                                            <uc1:DatePicker InvalidValueMessage="Invalid date format" ValidationGroup="ShoppingCart"
                                                EmptyValueMessage="The Start Date field is required" DateTimeFormat="Default"
                                                ID="dpDatePicker" ShowOn="Both" runat="server" Value='<%# !String.IsNullOrEmpty(Eval("StartDateTime").ToString()) ? ( ( DateTime.Parse(Eval("StartDateTime").ToString()).ToShortDateString() == DateTime.MinValue.ToShortDateString()  )? null : Eval("StartDateTime") ) : null %>'
                                                IsValidEmpty="false" />
                                            <asp:TextBox ID="txtInitialTime" runat="server" CssClass="input" Width="40px" Text='<%# Eval("StartDateTime", "{0:HH:mm}") %>'></asp:TextBox>
                                            <cc1:MaskedEditExtender ID="mskInitialTime" TargetControlID="txtInitialTime" runat="server"
                                                Mask="99:99" MaskType="Time" AcceptAMPM="false" UserTimeFormat="TwentyFourHour"
                                                AutoComplete="true" />
                                            <cc1:MaskedEditValidator ID="rfvTimeValidator" runat="server" ControlExtender="mskInitialTime"
                                                ControlToValidate="txtInitialTime" IsValidEmpty="false" EnableClientScript="true"
                                                Display="Dynamic" InvalidValueBlurredMessage="*" InvalidValueMessage="The Initial Time format is invalid"
                                                EmptyValueMessage="The Initial Time field is required" EmptyValueBlurredText="*"
                                                ValidationGroup="ShoppingCart" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="100px">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblTransferHeader" runat="server" Text="Transfer"></asp:Label>
                                            <div class="TransferAll" onclick="$('.Transfer :checkbox').attr('checked', $(this).find(':checkbox').attr('checked'));">
                                                <asp:CheckBox ID="chkAllTransfer" runat="server" Visible="false" />
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="Transfer" onclick="$('.TransferAll :checkbox').attr('checked', $('.Transfer :not(:checked)').length == 0);">
                                                <asp:CheckBox ID="chkTransfer" runat="server" Visible="false" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:ScrollableGridView>
                            <br />
                            <div style="text-align: right">
                                <asp:Button ID="btnTransfer" runat="server" Text="Transfer Resources" OnClick="btnTransfer_Click"
                                    CssClass="btn" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </Content>
            </uc:CollapseHolder>
        </div>
        <br />
        <div id="divGridShopCart">
            <uc:CollapseHolder ID="chShoppingCart" runat="server" Collapsed="false">
                <Header>
                    <asp:Label ID="lblShoppingCart" runat="server" Text="Resources" />
                </Header>
                <Content>
                    <asp:UpdatePanel ID="upGridShopCart" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:ValidationSummary ID="vsShoppingCart" runat="server" CssClass="errorbox" ValidationGroup="ShoppingCart"
                                HeaderText="Please correct the following information" />
                            <div>
                                <asp:Label ID="lblCallDate" runat="server" Text="Call Date: "></asp:Label>
                                <uc1:DatePicker InvalidValueMessage="Invalid date format" ValidationGroup="ShoppingCart"
                                    EmptyValueMessage="Resources - The Call Date field is required" DateTimeFormat="Default"
                                    ID="dpCallDate" ShowOn="Both" runat="server" IsValidEmpty="false" />
                                <asp:TextBox ID="txtCallTime" runat="server" CssClass="input" Width="40px"></asp:TextBox>
                                <cc1:MaskedEditExtender ID="mskCallTime" TargetControlID="txtCallTime" runat="server"
                                    Mask="99:99" MaskType="Time" AcceptAMPM="false" UserTimeFormat="TwentyFourHour"
                                    AutoComplete="true" />
                                <cc1:MaskedEditValidator ID="rfvCallTimeValidator" runat="server" ControlExtender="mskCallTime"
                                    ControlToValidate="txtCallTime" IsValidEmpty="false" EnableClientScript="true"
                                    Display="Dynamic" InvalidValueBlurredMessage="*" InvalidValueMessage="Resources - The Call Time format is invalid"
                                    EmptyValueMessage="Resources - The Call Time field is required" EmptyValueBlurredText="*"
                                    ValidationGroup="ShoppingCart" />
                                <div>
                                    <br />
                                    <div>
                                        <asp:Label ID="lblSubContractor" runat="server" Text="SubContractor"></asp:Label>
                                        <asp:CheckBox ID="chkSubConstractor" onclick="ShowHideSubcontractor();" runat="server" />
                                    </div>
                                    <br />
                                    <div id="divSubContractor" style="display: none">
                                        <div>
                                            <asp:Label ID="lblSubContractorInfo" runat="server" Text="SubContractor Info:"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:CountableTextBox ID="txtSubContractorInfo" runat="server" MaxChars="1000" MaxCharsWarning="950"
                                                Height="100px" Width="95%" CssClass="input" TextMode="MultiLine" />
                                        </div>
                                        <div>
                                            <asp:Label ID="lblFieldPO" runat="server" Text="Field PO#:"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtFieldPO" CssClass="input" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <asp:Label ID="lblShopCart" runat="server" Text="Selected Items" />
                            <br />
                            <asp:ScrollableGridView ID="gvShopCart" runat="server" CssClass="ScrollableGridView"
                                AutoGenerateColumns="false" ShowFooter="false" SkinID="NonSortable" OnRowDataBound="gvShopCart_RowDataBound">
                                <Columns>
                                    <asp:BoundField HeaderText="ID" DataField="ID" Visible="false" />
                                    <asp:BoundField HeaderText="Type" DataField="Type" Visible="false" />
                                    <asp:BoundField HeaderText="Combo # / Unit #" DataField="UnitNumber" Visible="true" />
                                    <asp:BoundField HeaderText="Selected Item" DataField="Name" ItemStyle-Font-Bold="true" />
                                    <asp:TemplateField HeaderText="Duration" ItemStyle-Width="45px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDuration" runat="server" MaxLength="3" CssClass="input" Text='<%# Eval("Duration") %>'
                                                Width="20px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvDuration" runat="server" Text="*" ControlToValidate="txtDuration"
                                                Display="Dynamic" ErrorMessage="The Duration field is required" EnableClientScript="true"
                                                ValidationGroup="ShoppingCart" />
                                            <asp:FilteredTextBoxExtender ID="ftbExtender" runat="server" FilterType="Numbers"
                                                TargetControlID="txtDuration" />
                                            <asp:RangeValidator ID="rvDuration" ControlToValidate="txtDuration" MinimumValue="1"
                                                MaximumValue="90" Type="Integer" EnableClientScript="true" Text="*" Display="Dynamic"
                                                ValidationGroup="ShoppingCart" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Start Date/Time" ItemStyle-Width="160px">
                                        <ItemTemplate>
                                            <uc1:DatePicker InvalidValueMessage="Invalid date format" ValidationGroup="ShoppingCart"
                                                EmptyValueMessage="The Start Date field is required" DateTimeFormat="Default"
                                                ID="dpDatePicker" ShowOn="Both" runat="server" Value='<%# !String.IsNullOrEmpty(Eval("StartDateTime").ToString()) ? ( ( DateTime.Parse(Eval("StartDateTime").ToString()).ToShortDateString() == DateTime.MinValue.ToShortDateString()  )? null : Eval("StartDateTime") ) : null %>'
                                                IsValidEmpty="false" />
                                            <asp:TextBox ID="txtInitialTime" runat="server" CssClass="input" Width="40px" Text='<%# Eval("StartDateTime", "{0:HH:mm}") %>'></asp:TextBox>
                                            <cc1:MaskedEditExtender ID="mskInitialTime" TargetControlID="txtInitialTime" runat="server"
                                                Mask="99:99" MaskType="Time" AcceptAMPM="false" UserTimeFormat="TwentyFourHour"
                                                AutoComplete="true" />
                                            <cc1:MaskedEditValidator ID="rfvTimeValidator" runat="server" ControlExtender="mskInitialTime"
                                                ControlToValidate="txtInitialTime" IsValidEmpty="false" EnableClientScript="true"
                                                Display="Dynamic" InvalidValueBlurredMessage="*" InvalidValueMessage="The Initial Time format is invalid"
                                                EmptyValueMessage="The Initial Time field is required" EmptyValueBlurredText="*"
                                                ValidationGroup="ShoppingCart" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                        HeaderText="Remove" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkDeselect" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:ScrollableGridView>
                            <br />
                            <div style="text-align: right">
                                <asp:Button ID="btnDeselect" runat="server" CssClass="btn" Text="Remove" OnClick="btnDeselect_Click" />&nbsp;
                            </div>
                            <br />
                            <div style="padding-left: 5px;">
                                <asp:Label ID="lblNotes" runat="server" Text="Notes:" />
                            </div>
                            <div style="text-align: center">
                                <asp:CountableTextBox ID="txtNotes" runat="server" MaxChars="1000" MaxCharsWarning="950"
                                    Height="100px" Width="95%" CssClass="input" TextMode="MultiLine" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </Content>
            </uc:CollapseHolder>
        </div>
    </div>
    <br />
    <div id="divButtons">
        <asp:UpdatePanel ID="upButtons" runat="server">
            <ContentTemplate>
                <div class="Row">
                    <div class="leftButtons">
                        <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn" CausesValidation="false"
                            OnClick="btnReset_Click" />
                    </div>
                    <div class="rightButtons">
                        <asp:Button ID="btnAdd" runat="server" Text="Add to Job" CssClass="btn" ValidationGroup="ShoppingCart"
                            Enabled="false" OnClick="btnAdd_OnClick" />
                        <asp:Button ID="btnCallEntry" runat="server" Text="Call Entry" CssClass="btn" Enabled="false" />
                        <input id="btnCancel" type="button" value="Cancel" class="btn" onclick="window.close();" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript" language="javascript">

        //Instance of ScripManager
        var scriptManager = Sys.WebForms.PageRequestManager.getInstance();

        scriptManager.add_endRequest(function () {
            ShowHideSubcontractor();
        });

        $(document).ready(function () {
            ShowHideSubcontractor();
        });

        function updateParentPage(parentFieldId) {
            if (parentFieldId) {
                if (window.opener != null) {
                    window.opener.document.getElementById(parentFieldId).value = 'go';
                    window.opener.__doPostBack(parentFieldId, '');

                    window.close();
                }
            }
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

        function ShowHideSubcontractor() {
            if ($('#<%= chkSubConstractor.ClientID %>').is(':checked')) {
                $('#divSubContractor').css('display', '');
            }
            else {
                $('#divSubContractor').css('display', 'none');
            }
        }
    </script>
</asp:Content>
