<%@ Page Title="Equipment Maintenance" Language="C#" MasterPageFile="~/ContentPage.master"
    AutoEventWireup="true" CodeBehind="EquipmentMaintenance.aspx.cs" Inherits="Hulcher.OneSource.CustomerService.Web.EquipmentMaintenance" %>

<%@ MasterType TypeName="Hulcher.OneSource.CustomerService.Web.ContentPage" %>
<%@ Register Src="~/UserControls/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/AutoCompleteTextbox.ascx" TagName="AutoCompleteTextbox"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Styles/Forms.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <asp:UpdatePanel ID="upPage" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label ID="lblTitle" runat="server" Text="Equipment Maintenance" Font-Size="Large"
                Font-Bold="true"></asp:Label>
            <br />
            <asp:Panel ID="pnlNoAccess" runat="server" Visible="false">
                <div>
                    <div>
                        <asp:Label ID="lblTitleNoAccess" runat="server" Text="Equipment Maintenance Details" />
                    </div>
                    <div>
                        The current user does not have access to this functionality!
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlVisualization" runat="server">
                <div id="divVisualization" class="Header">
                    <asp:Label ID="lblVisualizationTitle" runat="server" Text="Equipment Maintenance List"></asp:Label>
                </div>
                <div class="Content">
                    <div id="divSearchFilters">
                        <div class="inlineBlock floatRight alignRight">
                            <div class="floatLeft paddingTop5 paddingRight5">
                                <asp:Label ID="lblFilter" runat="server" Text="Filter Listing By:"></asp:Label>
                            </div>
                            <div class="floatLeft paddingRight5">
                                <asp:ComboBox ID="cbFilter" runat="server" CssClass="WindowsStyle" AutoCompleteMode="SuggestAppend"
                                    DropDownStyle="DropDown" CaseSensitive="false" RenderMode="Inline">
                                    <asp:ListItem Text="Division #" Value="1" />
                                    <asp:ListItem Text="Division State" Value="2" />
                                    <asp:ListItem Text="Combo #" Value="3" />
                                    <asp:ListItem Text="Unit #" Value="4" />
                                    <asp:ListItem Text="Status" Value="5" />
                                    <asp:ListItem Text="Job Location" Value="6" />
                                    <asp:ListItem Text="Call type" Value="7" />
                                    <asp:ListItem Text="Job #" Value="8" />
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
                    <div id="divSearchGrid">
                        <asp:ScrollableGridView runat="server" ID="gvEquipmentList" AllowSorting="true" AutoGenerateColumns="false"
                            CssClass="ScrollableGridView" SaveScrollPosition="true" MaxHeight="400px" EnableViewState="true"
                            OnRowDataBound="gvEquipmentList_RowDataBound" OnRowCommand="gvEquipmentList_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="" Visible="false">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hfEquipmentId" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Div #" />
                                <asp:BoundField HeaderText="Div Sate" />
                                <asp:BoundField HeaderText="Combo #" />
                                <asp:BoundField HeaderText="Unit #" />
                                <asp:BoundField HeaderText="Descriptor" />
                                <asp:BoundField HeaderText="Status" />
                                <asp:BoundField HeaderText="Job Location" />
                                <asp:TemplateField HeaderText="Last Call Entry">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlLastCallEntry" runat="server" Text='' NavigateUrl="javascript: alert('This Call Entry is automatic generated and can not be updated.');" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Job #">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlJobNumber" runat="server" Text='' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Oper. Status" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CausesValidation="false"
                                            CommandName="EditEquipment"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:ScrollableGridView>
                    </div>
                </div>
                <div class="inlineBlock alignRight" style="width: 100%">
                    <br />
                    <asp:Button ID="btnEquipmentDisplay" runat="server" CssClass="btn" Text="Equipment Display"
                        OnClick="btnEquipmentDisplay_Click" />
                </div>
            </asp:Panel>
            <br />
            <asp:HiddenField ID="hfOrderBy" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hfExpandedEquipmentType" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hfExpandedDivision" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hfExpandedEquipment" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hfSelectedH" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hfSelectedD" runat="server" ClientIDMode="Static" />
            <asp:Button ID="btnFakeSort" runat="server" OnClick="btnFakeSort_Click" Style="display: none;" />
            <asp:Panel ID="pnlManagementEquipment" Visible="false" runat="server">
                <div class="Header">
                    <asp:Label ID="lblTitleManagementEquipment" runat="server" Text="Management Equipment"></asp:Label>
                </div>
                <div class="Content">
                    <div id="divSearchMgmEq">
                        <div class="inlineBlock floatRight alignRight">
                            <div class="floatLeft paddingTop5 paddingRight5">
                                <asp:Label ID="lblFilterEquipManagement" runat="server" Text="Filter Listing By:"></asp:Label>
                            </div>
                            <div class="floatLeft paddingRight5">
                                <asp:ComboBox ID="cbFilterEquipmentDisplay" runat="server" CssClass="WindowsStyle"
                                    AutoCompleteMode="SuggestAppend" DropDownStyle="DropDown" CaseSensitive="false"
                                    RenderMode="Inline">
                                    <asp:ListItem Text="- Select One -" Value="0" />
                                    <asp:ListItem Text="Equipment Type" Value="9" />
                                    <asp:ListItem Text="Division #" Value="1" />
                                    <asp:ListItem Text="Equipment" Value="4" />
                                </asp:ComboBox>
                            </div>
                            <div class="floatLeft paddingRight5">
                                <asp:TextBox ID="txtFilterValueMgmEq" CssClass="input" runat="server" />
                            </div>
                            <div class="floatLeft paddingRight5 paddingTop2">
                                <asp:Button ID="btnFindMgmEq" runat="server" Text="Find" CssClass="btn" CausesValidation="false"
                                    OnClick="btnFindMgmEq_Click" />
                            </div>
                        </div>
                    </div>
                    <div id="divSearchMgmEqRepeater">
                        <asp:Repeater ID="rptEquipmentType" runat="server" OnItemDataBound="rptEquipmentType_ItemDataBound">
                            <HeaderTemplate>
                                <div id="tbRepeaters_Group" class="ScrollableGridView_Group" style="width: 100%">
                                    <div id="tbRepeaters_HeaderDiv" class="ScrollableGridView_HeaderDiv" style="min-width: 400px;">
                                    </div>
                                    <div id="tbRepeaters_ScrollDiv" class="ScrollableGridView_ScrollDiv" style="max-height: 400px;
                                        min-width: 400px;">
                                        <table id="tbRepeaters" class="ScrollableGridView" cellspacing="1">
                                            <thead>
                                                <tr style="position: relative; top: expression(this.offsetParent.scrollTop -1); left: expression(this.offsetParent.style.left);">
                                                    <th id="thHeavyEquipment" runat="server" class="header" style="border: 1px solid #E6EEEE;">
                                                        <asp:Label ID="lblHeavyEquipment" runat="server" CssClass="MarginRight" Text="H" />
                                                    </th>
                                                    <th id="thResourceAllocation" runat="server" class="header" style="border: 1px solid #E6EEEE;">
                                                        <asp:Label ID="lblResourceAllocation" runat="server" CssClass="MarginRight" Text="D" />
                                                    </th>
                                                    <th id="thExpandCollapse" runat="server" class="header" style="border: 1px solid #E6EEEE;">
                                                        &nbsp;
                                                    </th>
                                                    <th id="thUnitType" runat="server" class="header" style="border: 1px solid #E6EEEE;
                                                        margin: 0 20px 0 0" onclick="SetOrderBy('1', this);">
                                                        <asp:Label ID="lblHeaderUnitType" runat="server" CssClass="MarginRight" Text="Unit Type" />
                                                    </th>
                                                    <th id="thDivision" runat="server" class="header" style="border: 1px solid #E6EEEE;"
                                                        onclick="SetOrderBy('2', this);">
                                                        <asp:Label ID="lblHeaderDivision" runat="server" CssClass="MarginRight" Text="Division" />
                                                    </th>
                                                    <th id="thUnitNumber" runat="server" class="header" style="border: 1px solid #E6EEEE;"
                                                        onclick="SetOrderBy('3', this);">
                                                        <asp:Label ID="lblHeaderUnitNumber" runat="server" CssClass="MarginRight" Text="Unit Number" />
                                                    </th>
                                                    <th id="thUnitDescription" runat="server" class="header" style="border: 1px solid #E6EEEE;"
                                                        onclick="SetOrderBy('4', this);">
                                                        <asp:Label ID="lblHeaderUnitDescription" runat="server" CssClass="MarginRight" Text="Unit Description" />
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="even" id="trItem" runat="server">
                                    <td style="width: 15px;">
                                        <asp:CheckBox ID="chkEquipmentTypeHeavyEq" runat="server" CssClass="H" />
                                    </td>
                                    <td style="width: 15px;">
                                        <asp:CheckBox ID="chkEquipmentTypeResAllocation" runat="server" CssClass="D" />
                                    </td>
                                    <td style="width: 15px;">
                                        <div class="Expand" id="divExpand" visible="false" runat="server">
                                        </div>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblUnitType" runat="server"></asp:Label>
                                        <asp:HiddenField ID="hfEquipmentTypeID" runat="server" />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <asp:Repeater ID="rptDivision" runat="server" OnItemDataBound="rptDivision_ItemDataBound">
                                    <ItemTemplate>
                                        <tr id="trDivision" runat="server" class="odd">
                                            <td style="width: 15px;">
                                                <asp:CheckBox ID="chkDivisionHeavyEquipment" runat="server" CssClass="H" />
                                            </td>
                                            <td style="width: 15px;">
                                                <asp:CheckBox ID="chkResourceAllocation" runat="server" CssClass="D" />
                                            </td>
                                            <td style="width: 15px;">
                                                <div class="Expand" id="divExpand" visible="false" runat="server">
                                                </div>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDivision" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <asp:Repeater ID="rptEquipments" runat="server" OnItemDataBound="rptEquipments_ItemDataBound">
                                            <ItemTemplate>
                                                <tr id="trEquipment" runat="server" class="Child">
                                                    <td style="width: 15px;">
                                                        <asp:CheckBox ID="chkEquipmentHeavyEquipment" runat="server" CssClass="H" />
                                                        <asp:HiddenField runat="server" ID="hfEquipmentId" />
                                                    </td>
                                                    <td style="width: 15px;">
                                                        <asp:CheckBox ID="chkResourceAllocation" runat="server" CssClass="D" />
                                                    </td>
                                                    <td style="width: 15px;">
                                                        <div class="Expand" id="divExpand" visible="false" runat="server">
                                                        </div>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblUnitNumber" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblUnitDescription" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
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
                    </div>
                </div>
                <br />
                <div id="div1" class="inlineBlock alignRight space100">
                    <asp:Button ID="btnSaveEquipmentDisplay" runat="server" Text="Save" CssClass="btn"
                        CausesValidation="true" ValidationGroup="EquipmentDisplay" OnClick="btnSaveEquipmentDisplay_Click" />
                    <asp:Button ID="btnCancelEquipmentDisplay" runat="server" Text="Cancel" CssClass="btn"
                        CausesValidation="false" OnClientClick="return confirm('All unsaved data will be lost, are you sure you want to cancel?')"
                        OnClick="btnCancel_Click" />
                </div>
            </asp:Panel>
            <br />
            <asp:Panel ID="pnlCreation" runat="server" Visible="true">
                <div class="Header">
                    <asp:Label ID="lblHeaderCreation" runat="server" Text="Equipment Details"></asp:Label>
                </div>
                <div class="Content">
                    <div class="space100 paddingBottom5">
                        <div class="floatLeft space50 inline">
                            <div class="floatLeft alignRight width140 paddingRight5 inline">
                                <asp:Label ID="lblEquipmentName" Text="Name:" runat="server"></asp:Label>
                            </div>
                            <div class="inlineBlock">
                                <asp:Label ID="lblName" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="space49 inlineBlock">
                            <div class="floatLeft alignRight width140 paddingRight5 inline">
                                <asp:Label ID="lblEquipmentDescription" runat="server" Text="Description:"></asp:Label>
                            </div>
                            <div class="inlineBlock">
                                <asp:Label ID="lblDescription" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="space100 paddingBottom5">
                        <div class="floatLeft space50 inline">
                            <div class="floatLeft alignRight width140 paddingRight5 inline">
                                <asp:Label ID="lblEquipmentType" runat="server" Text="Equipment Type:"></asp:Label>
                            </div>
                            <div class="inlineBlock">
                                <asp:Label ID="lblType" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="space49 inlineBlock">
                            <div class="floatLeft alignRight width140 paddingRight5 inline">
                                <asp:Label ID="lblEquipmentLicensePlate" runat="server" Text="License Plate:"></asp:Label>
                            </div>
                            <div class="inlineBlock">
                                <asp:Label ID="lblLicensePlate" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="space100 paddingBottom5">
                        <div class="floatLeft space50 inline">
                            <div class="floatLeft alignRight width140 paddingRight5 inline">
                                <asp:Label ID="lblEquipmentSerialNumber" Text="Serial Number:" runat="server"></asp:Label>
                            </div>
                            <div class="inlineBlock">
                                <asp:Label ID="lblSerialNumber" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="space49 inlineBlock">
                            <div class="floatLeft alignRight width140 paddingRight5 inline">
                                <asp:Label ID="lblEquipmentYear" runat="server" Text="Year:"></asp:Label>
                            </div>
                            <div class="inlineBlock">
                                <asp:Label ID="lblYear" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="space100 paddingBottom5">
                        <div class="floatLeft space50 inline">
                            <div class="floatLeft alignRight width140 paddingRight5 inline">
                                <asp:Label ID="lblEquipmentBodyType" Text="Body Type:" runat="server"></asp:Label>
                            </div>
                            <div class="inlineBlock">
                                <asp:Label ID="lblBodyType" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="space49 inlineBlock">
                            <div class="floatLeft alignRight width140 paddingRight5 inline">
                                <asp:Label ID="lblEquipmentMake" runat="server" Text="Make:"></asp:Label>
                            </div>
                            <div class="inlineBlock">
                                <asp:Label ID="lblMake" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="space100 paddingBottom5">
                        <div class="floatLeft space50 inline">
                            <div class="floatLeft alignRight width140 paddingRight5 inline">
                                <asp:Label ID="lblEquipmentModel" Text="Model:" runat="server"></asp:Label>
                            </div>
                            <div class="inlineBlock">
                                <asp:Label ID="lblModel" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="space49 inlineBlock">
                            <div class="floatLeft alignRight width140 paddingRight5 inline">
                                <asp:Label ID="lblEquipmentFunctionTitle" Text="Equipment Function:" runat="server"></asp:Label>
                            </div>
                            <div class="inlineBlock">
                                <asp:Label ID="lblEquipmentFunction" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="space100 paddingBottom5">
                        <div class="floatLeft space50 inline">
                            <div class="floatLeft alignRight width140 paddingRight5 inline">
                                <asp:Label ID="lblEquipmentAssignedTo" Text="Assigned To:" runat="server"></asp:Label>
                            </div>
                            <div class="inlineBlock">
                                <asp:Label ID="lblAssignedTo" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="space49 inlineBlock">
                            <div class="floatLeft alignRight width140 paddingRight5 inline">
                                <asp:Label ID="lblEquipmentRegisteredState" Text="Registered State:" runat="server"></asp:Label>
                            </div>
                            <div class="inlineBlock">
                                <asp:Label ID="lblRegisteredState" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="space100 paddingBottom5">
                        <div class="floatLeft space50 inline">
                            <div class="floatLeft alignRight width140 paddingRight5 inline">
                                <asp:Label ID="lblEquipmentAttachPanelBoss" Text="Attach. Panel Boss:" runat="server"></asp:Label>
                            </div>
                            <div class="inlineBlock">
                                <asp:Label ID="lblAttachPanelBoss" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="space49 inlineBlock">
                            <div class="floatLeft alignRight width140 paddingRight5 inline">
                                <asp:Label ID="lblEquipmentAttachPileDriver" Text="Attach. Pile Driver:" runat="server"></asp:Label>
                            </div>
                            <div class="inlineBlock">
                                <asp:Label ID="lblAttachPileDriver" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="space100 paddingBottom5">
                        <div class="floatLeft space50 inline">
                            <div class="floatLeft alignRight width140 paddingRight5 inline">
                                <asp:Label ID="lblEquipmentAttachSlipSheet" Text="Attach. Slip Sheet:" runat="server"></asp:Label>
                            </div>
                            <div class="inlineBlock">
                                <asp:Label ID="lblAttachSlipSheet" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="space49 inlineBlock">
                            <div class="floatLeft alignRight width140 paddingRight5 inline">
                                <asp:Label ID="lblEquipmentAttachTieClamp" Text="Attach. Tie Clamp:" runat="server"></asp:Label>
                            </div>
                            <div class="inlineBlock">
                                <asp:Label ID="lblAttachTieClamp" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="space100 paddingBottom5">
                        <div class="floatLeft space50 inline">
                            <div class="floatLeft alignRight width140 paddingRight5 inline">
                                <asp:Label ID="lblEquipmentAttachTieInserter" Text="Attach. Tie Inserter:" runat="server"></asp:Label>
                            </div>
                            <div class="inlineBlock">
                                <asp:Label ID="lblAttachTieInserter" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="space49 inlineBlock">
                            <div class="floatLeft alignRight width140 paddingRight5 inline">
                                <asp:Label ID="lblEquipmentTieTamper" Text="Attach. Tie Tamper:" runat="server"></asp:Label>
                            </div>
                            <div class="inlineBlock">
                                <asp:Label ID="lblAttachTieTamper" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="space100 paddingBottom5">
                        <div class="floatLeft space50 inline">
                            <div class="floatLeft alignRight width140 paddingRight5 inline">
                                <asp:Label ID="lblEquipmentAttachUnderCutter" Text="Attach. Under Cutter:" runat="server"></asp:Label>
                            </div>
                            <div class="inlineBlock">
                                <asp:Label ID="lblAttachUnderCutter" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="space80 paddingBottom5">
                        <div class="floatLeft alignRight width140 paddingRight5 inline">
                            <asp:Label ID="lblEquipmentNotes" Text="Notes:" runat="server"></asp:Label>
                        </div>
                        <div class="inlineBlock">
                            <asp:Label ID="lblNotes" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
                <br />
                <asp:ValidationSummary ID="vsEquipmentMaintenance" runat="server" CssClass="errorbox"
                    ValidationGroup="EquipmentMaintenance" HeaderText="Please correct the following information" />
                <div class="Header">
                    <asp:Label ID="lblUpdateHeader" runat="server" Text="Update Equipment"></asp:Label>
                </div>
                <div class="Content">
                    <div class="space100 paddingBottom5">
                        <div class="floatLeft space50 inline">
                            <div class="floatLeft alignRight width140 paddingRight5 paddingTop2 inline">
                                <asp:Label ID="lblSeasonal" Text="Seasonal:" runat="server"></asp:Label>
                            </div>
                            <div class="inlineBlock">
                                <asp:CheckBox ID="chkSeasonal" runat="server" />
                            </div>
                        </div>
                        <div class="floatRight space49 inline">
                            <div class="floatLeft alignRight width140 paddingRight5 paddingTop2 inline">
                                <asp:Label ID="lblWhiteLight" runat="server" Text="White Light:"></asp:Label>
                            </div>
                            <div class="inlineBlock">
                                <asp:CheckBox ID="chkWhiteLight" runat="server" />
                            </div>
                        </div>
                        <div class="floatLeft space50 inline">
                            <div class="floatLeft alignRight width140 paddingRight5 paddingTop2 inline">
                                <asp:Label ID="lblHeavyEquipment" Text="Heavy Equipment:" runat="server"></asp:Label>
                            </div>
                            <div class="inlineBlock">
                                <asp:CheckBox ID="chkHeavyEquipment" runat="server" />
                            </div>
                        </div>
                        <div class="floatRight space49 inline">
                            <div class="floatLeft alignRight width140 paddingRight5 paddingTop2 inline">
                                <asp:Label ID="lblWhiteLightNotes" runat="server" Text="Notes:"></asp:Label>
                            </div>
                            <div class="inlineBlock">
                                <asp:TextBox ID="txtWhiteLightNotes" runat="server" MaxLength="500" Width="250px" CssClass="input" />
                            </div>
                        </div>
                        <div class="floatLeft space50 inline">
                            <div class="floatLeft alignRight width140 paddingRight5 paddingTop2 inline">
                                <asp:Label ID="lblDisplayInResourceAllocation" Text="Display In Resource Allocation:"
                                    runat="server"></asp:Label>
                            </div>
                            <div class="inlineBlock">
                                <asp:CheckBox ID="chkDisplayInResourceAllocation" runat="server" />
                            </div>
                        </div>
                        <div class="floatLeft space50 inline" id="divReplicateCombo" runat="server">
                            <div class="floatLeft alignRight width140 paddingRight5 paddingTop2 inline">
                                <asp:Label ID="lblReplicateToCombo" Text="Replicate Changes to Combo:"
                                    runat="server"></asp:Label>
                            </div>
                            <div class="inlineBlock">
                                <asp:CheckBox ID="chkReplicateCombo" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="space100 paddingBottom5">
                        <div class="floatLeft space50 inline">
                            <div class="floatLeft alignRight width140 paddingRight5 paddingTop2 inline">
                                <asp:Label ID="lblEquipmentStatus" runat="server" Text="Status:"></asp:Label>
                            </div>
                            <div class="inlineBlock">
                                <asp:ComboBox ID="ddlEquipmentStatus" runat="server" CssClass="WindowsStyle" AutoCompleteMode="SuggestAppend"
                                    AutoPostBack="true" DropDownStyle="DropDown" CaseSensitive="false" RenderMode="Inline"
                                    OnSelectedIndexChanged="ddlEquipmentStatus_SelectedIndexChanged">
                                    <asp:ListItem Text="Up" Value="Up"></asp:ListItem>
                                    <asp:ListItem Text="Down" Value="Down"></asp:ListItem>
                                </asp:ComboBox>
                            </div>
                        </div>
                        <div class="floatRight space49 inline">
                            <asp:Panel ID="pnlStatusDown" runat="server">
                                <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                    <asp:Label ID="lblDurationEquipmentMaintenanceDateTime" runat="server" Text="Start Date:"></asp:Label>
                                </div>
                                <div class="inlineBlock">
                                    <uc1:DatePicker ID="dpEquipmentStartDate" InvalidValueMessage="Update Equipment - The Status Down Start date format is invalid"
                                        DateTimeFormat="Default" ShowOn="Both" runat="server" EmptyValueMessage="Update Equipment - The Status Down Start date field is required" />
                                    <asp:TextBox ID="txtEquipmentTime" runat="server" CssClass="input" Width="40px"></asp:TextBox>
                                    <uc1:MaskedEditExtender ID="mskInitialTime" TargetControlID="txtEquipmentTime" runat="server"
                                        Mask="99:99" MaskType="Time" AcceptAMPM="false" UserTimeFormat="TwentyFourHour"
                                        AutoComplete="true">
                                    </uc1:MaskedEditExtender>
                                    <uc1:MaskedEditValidator ID="rfvTimeValidator" runat="server" ControlExtender="mskInitialTime"
                                        ControlToValidate="txtEquipmentTime" EnableClientScript="true" Display="Dynamic"
                                        InvalidValueBlurredMessage="*" InvalidValueMessage="Update Equipment - The Status Down Start time format is invalid"
                                        EmptyValueBlurredText="*" EmptyValueMessage="Update Equipment - The Status Down Start time field is required">
                                    </uc1:MaskedEditValidator>
                                </div>
                                <div class="floatLeft alignRight width140 paddingRight5 paddingTop2 inline">
                                    <asp:Label ID="lblEquipmentDownDuration" Text="Duration:" runat="server"></asp:Label>
                                </div>
                                <div class="inlineBlock">
                                    <asp:TextBox ID="txtDuration" MaxLength="4" CssClass="input" runat="server"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender TargetControlID="txtDuration" ID="ftDuration" FilterType="Custom, Numbers"
                                        runat="server">
                                    </asp:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="rfvDurationEqDown" runat="server" Text="*" Display="Dynamic"
                                        ErrorMessage="Equipment Down - The Duration Field is required." ValidationGroup="EquipmentMaintenance"
                                        ControlToValidate="txtDuration"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ControlToValidate="txtDuration" ID="cpvEDCoverageDuration"
                                        runat="server" Text="*" ErrorMessage="Equipment Dow - The Duration Field does not allow 0."
                                        Operator="GreaterThan" ValueToCompare="0" ValidationGroup="EquipmentMaintenance"></asp:CompareValidator>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlStatusUp" runat="server">
                                <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                    <asp:Label ID="lblEquipmentEndDate" runat="server" Text="End Date:"></asp:Label>
                                </div>
                                <div class="inlineBlock">
                                    <uc1:DatePicker ID="dpEquipmentEndDate" InvalidValueMessage="Update Equipment - The Status Up End date format is invalid"
                                        DateTimeFormat="Default" ShowOn="Both" runat="server" EmptyValueMessage="Update Equipment - The Status Up End date field is required" />
                                    <asp:TextBox ID="txtEquipmentStatusUpTime" runat="server" CssClass="input" Width="40px"></asp:TextBox>
                                    <uc1:MaskedEditExtender ID="mskInitialEquipmentUpTime" TargetControlID="txtEquipmentStatusUpTime"
                                        runat="server" Mask="99:99" MaskType="Time" AcceptAMPM="false" UserTimeFormat="TwentyFourHour"
                                        AutoComplete="true">
                                    </uc1:MaskedEditExtender>
                                    <uc1:MaskedEditValidator ID="rfvEqquipmentUpTimeValidator" runat="server" ControlExtender="mskInitialEquipmentUpTime"
                                        ControlToValidate="txtEquipmentStatusUpTime" EnableClientScript="true" Display="Dynamic"
                                        InvalidValueBlurredMessage="*" InvalidValueMessage="Update Equipment - The Status Up End time format is invalid"
                                        EmptyValueBlurredText="*" EmptyValueMessage="Update Equipment - The Status Up End time field is required">
                                    </uc1:MaskedEditValidator>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="floatLeft space50 inline">
                            <asp:Label ID="lblStatusHistoryTitle" Text="Status History" runat="server"></asp:Label>
                            <asp:ScrollableGridView runat="server" ID="gvStatusHistory" AllowSorting="true" AutoGenerateColumns="false"
                                CssClass="ScrollableGridView" SaveScrollPosition="true" MaxHeight="200px" EnableViewState="true"
                                MinWidth="90%" Width="90%">
                                <Columns>
                                    <asp:BoundField HeaderText="Start" DataField="DownHistoryStartDate" DataFormatString="{0:MM/dd/yyyy HH:mm}" />
                                    <asp:BoundField HeaderText="End" DataField="DownHistoryEndDate" DataFormatString="{0:MM/dd/yyyy HH:mm}" />
                                    <asp:BoundField HeaderText="Duration" DataField="Duration" />
                                </Columns>
                            </asp:ScrollableGridView>
                        </div>
                        <div class="space49 inlineBlock">
                            <asp:Label ID="lblWHiteLightHistoryTitle" Text="White Light History" runat="server"></asp:Label>
                            <asp:ScrollableGridView runat="server" ID="gvWhiteLightHistory" AllowSorting="true"
                                AutoGenerateColumns="false" CssClass="ScrollableGridView" SaveScrollPosition="true"
                                MaxHeight="200px" EnableViewState="true" MinWidth="90%" Width="90%">
                                <Columns>
                                    <asp:BoundField HeaderText="Start" DataField="WhiteLightStartDate" DataFormatString="{0:MM/dd/yyyy HH:mm}" />
                                    <asp:BoundField HeaderText="End" DataField="WhiteLightEndDate" DataFormatString="{0:MM/dd/yyyy HH:mm}" />
                                    <asp:BoundField HeaderText="Notes" DataField="Notes" />
                                </Columns>
                            </asp:ScrollableGridView>
                        </div>
                    </div>
                </div>
                <br />
                <div class="Header">
                    <asp:Label ID="lblECoverage" runat="server" Text="Equipment Coverage"></asp:Label>
                </div>
                <div class="Content">
                    <div class="space100 paddingBottom5">
                        <div class="floatLeft inline space49">
                            <asp:HiddenField ID="hfIsCoverage" runat="server" />
                            <asp:Label ID="lblMsg" Visible="false" runat="server" Text="You cannot mark this equipment for coverage because it´s already assigned to a job."></asp:Label>
                            <asp:Panel ID="pnlCoverage" runat="server">
                                <div class="floatLeft space100 inline">
                                    <div class="floatLeft alignRight width140 paddingRight5 inline">
                                        <asp:Label ID="lblCoverage" runat="server" Text="Coverage"></asp:Label>
                                    </div>
                                    <div class="inlineBlock alignLeft">
                                        <asp:CheckBox ID="chkCoverage" runat="server" onclick="ShowHideEquipmentCoverageFields();" />
                                    </div>
                                </div>
                                <div class="floatLeft space100 inline">
                                    <div class="floatLeft alignRight width140 paddingRight5 inline">
                                        <asp:Label ID="lblActualDivision" runat="server" Text="Home Division:"></asp:Label>
                                    </div>
                                    <div class="inlineBlock alignLeft">
                                        <asp:Label ID="lblDivisionName" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <asp:Panel ID="pnlEquipmentCoverageStart" runat="server" Style="display: none">
                                    <div class="floatLeft space100 inline">
                                        <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                            <asp:Label ID="lblCoverageDivision" runat="server" Text="Coverage Division:"></asp:Label>
                                        </div>
                                        <div class="inlineBlock">
                                            <uc1:AutoCompleteTextbox ID="actDivision" runat="server" ServiceMethod="GetDivisionList"
                                                ValidationGroup="EquipmentMaintenance" TextBoxWidth="120px" GridViewButtonImageUrl="~/Images/money.png"
                                                GridViewIdName="ID" DisplayField="Division" FilterId="0" ContextKey="0" AutoPostBack="false"
                                                RequiredField="true" WindowTitle="Find Division" ErrorMessage="Equipment Coverage - Coverage Division field is required"
                                                AutoCompleteSource="Division" ColumnHeaderList="Name,Description" TextBoxCssClass="input"
                                                ColumnValueList="Name,Description" />
                                        </div>
                                    </div>
                                    <div class="floatLeft space100 inline">
                                        <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                            <asp:Label ID="lblCoverageDateTimeFrom" runat="server" Text="Start Date and Time:"></asp:Label>
                                        </div>
                                        <div class="inlineBlock">
                                            <uc1:DatePicker ID="dpEquipmentCoverageFrom" ValidationGroup="EquipmentMaintenance"
                                                InvalidValueMessage="Equipment Coverage - The Start Date format is invalid" DateTimeFormat="Default"
                                                IsValidEmpty="false" ShowOn="Both" runat="server" EmptyValueMessage="Equipment Coverage - The Start date field is required" />
                                            <asp:TextBox ID="txtCoverageTimeFrom" runat="server" CssClass="input" Width="40px"
                                                ValidationGroup="EquipmentMaintenance"></asp:TextBox>
                                            <uc1:MaskedEditExtender ID="mskCoverageTimeFrom" TargetControlID="txtCoverageTimeFrom"
                                                runat="server" Mask="99:99" MaskType="Time" AcceptAMPM="false" UserTimeFormat="TwentyFourHour"
                                                AutoComplete="true">
                                            </uc1:MaskedEditExtender>
                                            <uc1:MaskedEditValidator ID="rfvCoverageTimeValidatorFrom" runat="server" IsValidEmpty="false"
                                                ValidationGroup="EquipmentMaintenance" ControlExtender="mskCoverageTimeFrom"
                                                ControlToValidate="txtCoverageTimeFrom" ErrorMessage="Equipment Coverage - The Start time field is required"
                                                Display="Dynamic" InvalidValueBlurredMessage="*" InvalidValueMessage="Equipment Coverage - The Start time format is invalid"
                                                EmptyValueBlurredText="*" EmptyValueMessage="Equipment Coverage - The Start time field is required">
                                            </uc1:MaskedEditValidator>
                                        </div>
                                    </div>
                                    <div class="floatleft space100 inline">
                                        <div class="floatLeft alignRight width140 paddingRight5 paddingTop2 inline">
                                            <asp:Label ID="Label1" Text="Duration:" runat="server"></asp:Label>
                                        </div>
                                        <div class="inlineBlock">
                                            <asp:TextBox ID="txtCoverageDuration" CssClass="input" runat="server" MaxLength="4"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCoverageDuration" runat="server" Text="*" Display="Dynamic"
                                                ValidationGroup="EquipmentMaintenance" ErrorMessage="Equipment Coverage - The Duration Field is required."
                                                ControlToValidate="txtCoverageDuration"></asp:RequiredFieldValidator>
                                            <asp:FilteredTextBoxExtender TargetControlID="txtCoverageDuration" ID="fteCoverageDuration"
                                                FilterType="Custom, Numbers" runat="server">
                                            </asp:FilteredTextBoxExtender>
                                            <asp:CompareValidator ControlToValidate="txtCoverageDuration" ID="cpvCoverageDuration"
                                                runat="server" Text="*" ErrorMessage="Equipment Coverage - The Duration Field does not allow 0."
                                                Operator="GreaterThan" ValueToCompare="0" ValidationGroup="EquipmentMaintenance"></asp:CompareValidator>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlEquipmentCoverageEnd" runat="server" Style="display: none">
                                    <div class="floatLeft space100 inline">
                                        <div class="floatLeft alignRight width140 paddingRight5 paddingTop5 inline">
                                            <asp:Label ID="lblCoverageDateTimeTO" runat="server" Text="End Date and Time:"></asp:Label>
                                        </div>
                                        <div class="inlineBlock">
                                            <uc1:DatePicker ID="dpEquipmentCoverageTO" ValidationGroup="EquipmentMaintenance"
                                                InvalidValueMessage="Equipment Coverage - The End date format is invalid" DateTimeFormat="Default"
                                                IsValidEmpty="false" ShowOn="Both" runat="server" EmptyValueMessage="Equipment Coverage - The End date field is required" />
                                            <asp:TextBox ID="txtCoverageTimeTO" runat="server" CssClass="input" Width="40px"
                                                ValidationGroup="EquipmentMaintenance"></asp:TextBox>
                                            <uc1:MaskedEditExtender ID="mskCoverageTimeTO" TargetControlID="txtCoverageTimeTO"
                                                runat="server" Mask="99:99" MaskType="Time" AcceptAMPM="false" UserTimeFormat="TwentyFourHour"
                                                AutoComplete="true">
                                            </uc1:MaskedEditExtender>
                                            <uc1:MaskedEditValidator ID="rfvCoverageTimeValidatorTO" IsValidEmpty="false" runat="server"
                                                ControlExtender="mskCoverageTimeTO" ControlToValidate="txtCoverageTimeTO" EnableClientScript="true"
                                                Display="Dynamic" InvalidValueBlurredMessage="*" InvalidValueMessage="Equipment Coverage - The End time format is invalid"
                                                ValidationGroup="EquipmentMaintenance" EmptyValueBlurredText="*" EmptyValueMessage="Equipment Coverage - The End time field is required">
                                            </uc1:MaskedEditValidator>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </asp:Panel>
                        </div>
                        <div class="inlineBlock">
                            <asp:Label ID="lblCoverHistoryTitle" Text="Coverage History" runat="server"></asp:Label>
                            <asp:ScrollableGridView runat="server" ID="gvCoverHistory" AllowSorting="true" AutoGenerateColumns="false"
                                CssClass="ScrollableGridView" SaveScrollPosition="true" MaxHeight="200px" EnableViewState="true"
                                MinWidth="90%" Width="90%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Division">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDivision" Text='<%# Eval("CS_Division.Name") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Start" DataField="CoverageStartDate" DataFormatString="{0:MM/dd/yyyy HH:mm}" />
                                    <asp:BoundField HeaderText="End" DataField="CoverageEndDate" DataFormatString="{0:MM/dd/yyyy HH:mm}" />
                                    <asp:BoundField HeaderText="Duration" DataField="Duration" />
                                </Columns>
                            </asp:ScrollableGridView>
                        </div>
                    </div>
                </div>
                <br />
                <div id="divButtons" class="inlineBlock alignRight space100">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn" CausesValidation="true"
                        ValidationGroup="EquipmentMaintenance" OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn" CausesValidation="false"
                        OnClientClick="return confirm('All unsaved data will be lost, are you sure you want to cancel?')"
                        OnClick="btnCancel_Click" />
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript" language="javascript" defer="defer">
        function OpenCallEntry(jobId, callLogId) {
            var newWindow = window.open('/CallEntry.aspx?JobId=' + jobId + '&CallEntryID=' + callLogId, '', 'width=800, height=600, scrollbars=1, resizable=yes');
        }
        function OpenJobRecord(jobId) {
            var newWindow = window.open('/JobRecord.aspx?JobId=' + jobId, '', 'width=870, height=600, scrollbars=1, resizable=yes');
        }

        function ShowHideEquipmentCoverageFields() {

            if ($('#<%= chkCoverage.ClientID %>').length > 0) {
                if ($('#<%= chkCoverage.ClientID %>').attr('checked')) {
                    if ($('#<%= pnlEquipmentCoverageStart.ClientID %>').length > 0)
                        $('#<%= pnlEquipmentCoverageStart.ClientID %>').attr("style", "display:inline");
                    if ($get('<%=rfvCoverageTimeValidatorFrom.ClientID %>'))
                        ValidatorEnable($get('<%=rfvCoverageTimeValidatorFrom.ClientID %>'), true);
                    if ($get('<%=actDivision.RequiredFieldClientId %>'))
                        ValidatorEnable($get('<%=actDivision.RequiredFieldClientId %>'), true);
                    if ($get('<%=rfvCoverageDuration.ClientID %>'))
                        ValidatorEnable($get('<%=rfvCoverageDuration.ClientID %>'), true);
                    if ($get('<%=cpvCoverageDuration.ClientID %>'))
                        ValidatorEnable($get('<%=cpvCoverageDuration.ClientID %>'), true);
                    if ($('#<%= pnlEquipmentCoverageEnd.ClientID %>').length > 0)
                        $('#<%= pnlEquipmentCoverageEnd.ClientID %>').attr("style", "display:none");
                    //Validators coverage
                    if ($('#ContentPlaceHolder1_Content_pnlEquipmentCoverageEnd').css('display') != 'none') {
                        if ($get('<% = dpEquipmentCoverageTO.MaskedEditValidatorID %>')) {
                            ValidatorEnable($get('<%=dpEquipmentCoverageTO.MaskedEditValidatorID %>'), true);
                        }
                        if ($get('<%=rfvCoverageTimeValidatorTO.ClientID %>')) {
                            ValidatorEnable($get('<%=rfvCoverageTimeValidatorTO.ClientID %>'), true);
                        }
                    }
                    else {
                        if ($get('<% = dpEquipmentCoverageTO.MaskedEditValidatorID %>')) {
                            ValidatorEnable($get('<%=dpEquipmentCoverageTO.MaskedEditValidatorID %>'), false);
                        }
                        if ($get('<%=rfvCoverageTimeValidatorTO.ClientID %>')) {
                            ValidatorEnable($get('<%=rfvCoverageTimeValidatorTO.ClientID %>'), false);
                        }
                    }

                    if ($get('<% = dpEquipmentCoverageFrom.TextBoxClientID %>').style("display") != 'none') {
                        ValidatorEnable($get('<%=dpEquipmentCoverageFrom.MaskedEditValidatorID %>'), true);
                    }
                }
                else {
                    if ($('#<%= hfIsCoverage.ClientID %>').length > 0) {
                        if ($('#<%= pnlEquipmentCoverageEnd.ClientID %>').length > 0) {
                            if ($('#<%= hfIsCoverage.ClientID %>').attr('value') == '1')
                                $('#<%= pnlEquipmentCoverageEnd.ClientID %>').attr("style", "display:inline");
                            else
                                $('#<%= pnlEquipmentCoverageEnd.ClientID %>').attr("style", "display:none");
                        }
                        if ($('#<%= pnlEquipmentCoverageStart.ClientID %>').length > 0) {
                            if ($('#<%= hfIsCoverage.ClientID %>').attr('value') == '1')
                                $('#<%= pnlEquipmentCoverageStart.ClientID %>').attr("style", "display:inline");
                            else
                                $('#<%= pnlEquipmentCoverageStart.ClientID %>').attr("style", "display:none");
                        }
                        if ($get('<%=rfvCoverageDuration.ClientID %>')) {
                            if ($('#<%= hfIsCoverage.ClientID %>').attr('value') == '1')
                                ValidatorEnable($get('<%=rfvCoverageDuration.ClientID %>'), true);
                            else
                                ValidatorEnable($get('<%=rfvCoverageDuration.ClientID %>'), false);
                        }
                        if ($get('<%=cpvCoverageDuration.ClientID %>')) {
                            if ($('#<%= hfIsCoverage.ClientID %>').attr('value') == '1')
                                ValidatorEnable($get('<%=cpvCoverageDuration.ClientID %>'), true);
                            else
                                ValidatorEnable($get('<%=cpvCoverageDuration.ClientID %>'), false);
                        }
                        if ($get('<%=actDivision.RequiredFieldClientId %>')) {
                            if ($('#<%= hfIsCoverage.ClientID %>').attr('value') == '1')
                                ValidatorEnable($get('<%=actDivision.RequiredFieldClientId %>'), true);
                            else
                                ValidatorEnable($get('<%=actDivision.RequiredFieldClientId %>'), false);
                        }
                        if ($get('<% = dpEquipmentCoverageFrom.MaskedEditValidatorID %>')) {
                            if ($('#<%= hfIsCoverage.ClientID %>').attr('value') == '1')
                                ValidatorEnable($get('<%=dpEquipmentCoverageFrom.MaskedEditValidatorID %>'), true);
                            else
                                ValidatorEnable($get('<%=dpEquipmentCoverageFrom.MaskedEditValidatorID %>'), false);
                        }
                        if ($get('<%=rfvCoverageTimeValidatorFrom.ClientID %>')) {
                            if ($('#<%= hfIsCoverage.ClientID %>').attr('value') == '1')
                                ValidatorEnable($get('<%=rfvCoverageTimeValidatorFrom.ClientID %>'), true);
                            else
                                ValidatorEnable($get('<%=rfvCoverageTimeValidatorFrom.ClientID %>'), false);
                        }
                        if ($get('<% = dpEquipmentCoverageTO.MaskedEditValidatorID %>')) {
                            if ($('#<%= hfIsCoverage.ClientID %>').attr('value') == '1')
                                ValidatorEnable($get('<%=dpEquipmentCoverageTO.MaskedEditValidatorID %>'), true);
                            else
                                ValidatorEnable($get('<%=dpEquipmentCoverageTO.MaskedEditValidatorID %>'), false);
                        }
                        if ($get('<%=rfvCoverageTimeValidatorTO.ClientID %>')) {
                            if ($('#<%= hfIsCoverage.ClientID %>').attr('value') == '1')
                                ValidatorEnable($get('<%=rfvCoverageTimeValidatorTO.ClientID %>'), true);
                            else
                                ValidatorEnable($get('<%=rfvCoverageTimeValidatorTO.ClientID %>'), false);
                        }
                    }
                }
            }
        }




        function ValidateJobAssigment() {
            if (document.getElementById('<%= chkCoverage.ClientID %>').checked)
                return confirm('This employee is currently assigned to a job. Do you wish to proceed with the Coverage operation?');
            else
                return true;
        }

        var scriptManager = Sys.WebForms.PageRequestManager.getInstance();
        scriptManager.add_endRequest(function () {
            ShowHideEquipmentCoverageFields();
        });

        // Collapse-Expand functionality
        function CollapseExpandEquipmentType(Button, selector) {
            var line = $(".Division." + selector);
            var button = $("#" + Button);
            var expandedItems = $get('<%=hfExpandedEquipmentType.ClientID %>').value;

            line.toggle();
            button.toggleClass("Expand");
            button.toggleClass("Collapse");

            if (button.attr('class') == 'Expand')
                expandedItems = expandedItems.replace(selector + ';', '');
            else
                expandedItems += selector + ';';
            $get('<%=hfExpandedEquipmentType.ClientID %>').value = expandedItems;

            if (button.attr('class') == 'Expand') {
                var divisionLines = $(".Division." + selector);
                divisionLines.css("display", "none");

                var equipmentLines = $(".Equipment." + selector);
                equipmentLines.css("display", "none");

                var divisionLinesButton = $(".Division." + selector + " .Collapse");

                divisionLinesButton.toggleClass("Expand");
                divisionLinesButton.toggleClass("Collapse");
            }
        }

        // Collapse-Expand functionality
        function CollapseExpandDivision(Button, selectorEqType, selectorDiv) {
            var expandedItems = $get('<%=hfExpandedDivision.ClientID %>').value;
            var lineEquipment = $(".Equipment." + selectorEqType + "." + selectorDiv);
            lineEquipment.toggle();

            var button = $("#" + Button);
            button.toggleClass("Expand");
            button.toggleClass("Collapse");

            if (button.attr('class') == 'Expand')
                expandedItems = expandedItems.replace(selectorEqType + selectorDiv + ';', '');
            else
                expandedItems += selectorEqType + selectorDiv + ';';

            $get('<%=hfExpandedDivision.ClientID %>').value = expandedItems;
        }

        function CheckAllH(obj, eqTypeId) {
            if (!obj.checked)
                $(".EqType" + eqTypeId + " .H input:checkbox,.EqTypeMain" + eqTypeId + " .H input:checkbox").attr('checked', obj.checked);
            else
                $(".EqType" + eqTypeId + " input:checkbox,.EqTypeMain" + eqTypeId + " input:checkbox").attr('checked', obj.checked);
        }

        function CheckAllD(obj, eqTypeId) {
            $(".EqType" + eqTypeId + " .D input:checkbox,.EqTypeMain" + eqTypeId + " .D input:checkbox").attr('checked', obj.checked);
        }

        function CheckAllDivisionH(obj, eqTypeId, divisionId) {
            if (!obj.checked) {
                $(".Equipment.EqType" + eqTypeId + ".Div" + divisionId + " .H input:checkbox").attr("checked", obj.checked);
            }
            else {
                $(".Equipment.EqType" + eqTypeId + ".Div" + divisionId + " input:checkbox, .Division.EqType" + eqTypeId + ".DivMain" + divisionId + " input:checkbox").attr("checked", obj.checked);
            }

            $(".EqTypeMain" + eqTypeId + " input:checkbox").attr('checked', $(".EqType" + eqTypeId + " input:checkbox:input:not(:checked)").length == 0);
        }

        function CheckAllDivisionD(obj, eqTypeId, divisionId) {
            if (!obj.checked) {
                $(".Equipment.EqType" + eqTypeId + ".Div" + divisionId + " .D input:checkbox").attr("checked", obj.checked);
            }
            else {
                $(".Equipment.EqType" + eqTypeId + ".Div" + divisionId + " .D input:checkbox").attr("checked", obj.checked);
            }

            $(".EqTypeMain" + eqTypeId + " .D input:checkbox").attr('checked', $(".EqType" + eqTypeId + " .D input:not(:checked)").length == 0);
        }

        function CheckDivisionByEquipmentH(obj, eqTypeId, divisionId, equipmentRow, equipmentId) {

            if (!obj.checked) {
                $(".Equipment.EqType" + eqTypeId + ".Div" + divisionId + "Eq" + equipmentId + " .H input:checkbox").attr("checked", obj.checked);
                $(".Division.EqType" + eqTypeId + ".DivMain" + divisionId + " .H input:checkbox").attr("checked", obj.checked);
            }
            else {
                $("#" + equipmentRow + " input:checkbox").attr("checked", obj.checked);
                $(".Division.EqType" + eqTypeId + ".DivMain" + divisionId + " input:checkbox").attr("checked", $(".Equipment.EqType" + eqTypeId + ".Div" + divisionId + " input:checkbox:not(:checked)").length == 0);

            }


            $(".EqTypeMain" + eqTypeId + " .H input:checkbox").attr('checked', $(".EqType" + eqTypeId + " .H input:not(:checked)").length == 0);
            $(".EqTypeMain" + eqTypeId + " .D input:checkbox").attr('checked', $(".EqType" + eqTypeId + " .D input:not(:checked)").length == 0);
        }

        function CheckDivisionByEquipmentD(obj, eqTypeId, divisionId, equipmentRow) {
            $("#" + equipmentRow + " .D input:checkbox").attr("checked", obj.checked);

            $(".Division.EqType" + eqTypeId + ".DivMain" + divisionId + " .D input:checkbox").attr("checked", $(".Equipment.EqType" + eqTypeId + ".Div" + divisionId + " input:checkbox:input:not(:checked)").length == 0);

            $(".EqTypeMain" + eqTypeId + " .D input:checkbox").attr('checked', $(".EqType" + eqTypeId + " .D input:not(:checked)").length == 0);
        }


        function GetEquipmentValuesH() {
            var selectedItems = "";
            $(".ScrollableGridView tr:has(.H :checked)").each(function () {
                if (this.id != "") {
                    if ($("#" + this.id + " .H input:checkbox").attr('checked')) {
                        if (this.className.indexOf("Division") != -1)
                            selectedItems += this.className.substring(this.className.indexOf("Division ") + 9) + ";";
                    }
                }
            });

            document.getElementById('<%= hfSelectedH.ClientID %>').value = selectedItems;

            GetEquipmentValuesD();
        }

        function GetEquipmentValuesD() {
            var selectedItems = "";
            $(".ScrollableGridView tr:has(.D :checked)").each(function () {
                if (this.id != "") {
                    if ($("#" + this.id + " .D input:checkbox").attr('checked')) {
                        if (this.className.indexOf("Division") != -1)
                            selectedItems += this.className.substring(this.className.indexOf("Division ") + 9) + ";";
                    }
                }
            });

            document.getElementById('<%= hfSelectedD.ClientID %>').value = selectedItems;
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
