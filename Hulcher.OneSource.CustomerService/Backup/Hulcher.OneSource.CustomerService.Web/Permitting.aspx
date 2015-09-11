<%@ Page Title="Permitting" Language="C#" MasterPageFile="~/ContentPage.master" AutoEventWireup="true"
    CodeBehind="Permitting.aspx.cs" Inherits="Hulcher.OneSource.CustomerService.Web.Permitting" %>

<%@ MasterType TypeName="Hulcher.OneSource.CustomerService.Web.ContentPage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/AutoCompleteTextbox.ascx" TagName="AutoCompleteTextbox"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Styles/Forms.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <asp:UpdatePanel ID="upPage" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
        <ContentTemplate>
            <asp:Panel ID="pnlVisualization" runat="server" DefaultButton="btnCreate">
                <div id="divVisualization" class="Header">
                    <asp:Label ID="lblVisualizationTitle" runat="server" Text="Combo Viewer"></asp:Label>
                </div>
                <div class="Content">
                    <asp:Button ID="btnCreate" runat="server" Text="New Combo" CssClass="btn" OnClick="btnCreate_Click" />
                    <asp:HiddenField ID="hfOrderBy" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hfExpandedCombos" runat="server" />
                    <asp:HiddenField ID="hfEquipmentComboId" runat="server" />
                    <asp:HiddenField ID="hfExpandedButtons" runat="server" />
                    <asp:Repeater ID="rptCombo" runat="server" OnItemDataBound="rptCombo_ItemDataBound"
                        OnItemCommand="rptCombo_OnItemCommand">
                        <HeaderTemplate>
                            <div id="tbRepeaters_Group" class="ScrollableGridView_Group" style="width: 100%">
                                <div id="tbRepeaters_HeaderDiv" class="ScrollableGridView_HeaderDiv" style="min-width: 300;">
                                </div>
                                <div id="tbRepeaters_ScrollDiv" class="ScrollableGridView_ScrollDiv" style="max-height: 450px;
                                    min-width: 300px;">
                                    <table id="tbRepeaters" class="ScrollableGridView" cellspacing="1">
                                        <thead>
                                            <tr style="position: relative; top: expression(this.offsetParent.scrollTop -1); left: expression(this.offsetParent.style.left);">
                                                <th id="thExpandCollapse" runat="server" class="header" style="border: 1px solid #E6EEEE;">
                                                    &nbsp;
                                                </th>
                                                <th id="thComboUnit" runat="server" class="header" style="border: 1px, solid, #E6EEEE;"
                                                    onclick="SetOrderBy('1', this);">
                                                    <asp:Label ID="rpt1Header1" CssClass="MarginRight" runat="server" Text="Combo/Unit #"></asp:Label>
                                                </th>
                                                <th id="thIsPrimary" runat="server" class="header">
                                                    <asp:Label ID="rpt1Header3" CssClass="MarginRight" runat="server" Text="Primary"></asp:Label>
                                                </th>
                                                <th id="thCreateDate" runat="server" class="header" onclick="SetOrderBy('2', this);">
                                                    <asp:Label ID="rpt1Header4" CssClass="MarginRight" runat="server" Text="Create Date"></asp:Label>
                                                </th>
                                                <th id="thDivisionName" runat="server" class="header" onclick="SetOrderBy('3', this);">
                                                    <asp:Label ID="rpt1Header5" CssClass="MarginRight" runat="server" Text="Div #"></asp:Label>
                                                </th>
                                                <th id="thDivisionState" runat="server" class="header" onclick="SetOrderBy('4', this);">
                                                    <asp:Label ID="rpt1Header6" CssClass="MarginRight" runat="server" Text="Div State"></asp:Label>
                                                </th>
                                                <th id="thEquipmentTypeDescriptor" runat="server" class="header" onclick="SetOrderBy('5', this);">
                                                    <asp:Label ID="rpt1Header7" CssClass="MarginRight" runat="server" Text="Eq. Type/Descriptor"></asp:Label>
                                                </th>
                                                <th id="thJobNumber" runat="server" class="header" onclick="SetOrderBy('6', this);">
                                                    <asp:Label ID="rpt1Header8" CssClass="MarginRight" runat="server" Text="Job #"></asp:Label>
                                                </th>
                                                <th id="thLinks" runat="server" class="header">
                                                    <asp:Label ID="rpt1Header9" CssClass="MarginRight" runat="server" Text=""></asp:Label>
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
                                    <asp:Label ID="lblComboName" CssClass="MarginRight" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblUnitNumber" CssClass="MarginRight" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblCreateDate" CssClass="MarginRight" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblDivName" CssClass="MarginRight" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblDivState" CssClass="MarginRight" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblTypeDescriptor" CssClass="MarginRight" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    <asp:HyperLink ID="hlJobNumber" runat="server" Text="" />
                                </td>
                                <td>
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="Combo" CommandArgument=""
                                        CssClass="linkButtonStyle" /> | 
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="DeleteCombo"
                                        CommandArgument="" CssClass="linkButtonStyle" />
                                </td>
                            </tr>
                            <asp:Repeater ID="rptEquipments" runat="server" OnItemDataBound="rptEquipments_ItemDataBound">
                                <ItemTemplate>
                                    <tr class="even" id="trEquipment" runat="server">
                                        <td style="width: 15px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="lblUnitNumber" CssClass="MarginRight" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPrimary" CssClass="MarginRight" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDivName" CssClass="MarginRight" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDivState" CssClass="MarginRight" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTypeDescriptor" CssClass="MarginRight" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="hlJobNumber" runat="server" Text="" />
                                        </td>
                                        <td>
                                            &nbsp
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
            </asp:Panel>
            <br />
            <asp:ValidationSummary ID="vsPermitting" runat="server" CssClass="errorbox" ValidationGroup="Permitting"
                HeaderText="Please correct the following information" />
            <asp:Panel ID="pnlCreation" runat="server" Visible="false">
                <div id="divCreation" class="Header">
                    <asp:Label ID="lblCreationTitle" runat="server" Text="Create/Edit Combo"></asp:Label>
                </div>
                <div class="Content">
                    <asp:Panel ID="pnlFilter" runat="server" DefaultButton="btnFindEquipment">
                        <div class="inlineBlock floatRight alignRight">
                            <div class="floatLeft paddingTop5 paddingRight5">
                                <asp:Label ID="lblFilterEmployeeAdd" runat="server" Text="Filter Listing By: "></asp:Label>
                            </div>
                            <div class="floatLeft paddingRight5">
                                <asp:ComboBox ID="cbFilterEquipment" runat="server" CssClass="WindowsStyle" AutoCompleteMode="SuggestAppend"
                                    DropDownStyle="DropDown" CaseSensitive="false" RenderMode="Inline">
                                    <asp:ListItem Text="- Select One -" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Division" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Division State" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Unit Number" Value="3"></asp:ListItem>
                                </asp:ComboBox>
                            </div>
                            <div class="floatLeft">
                                <asp:TextBox ID="txtFilterEquipment" runat="server" CssClass="input">
                                </asp:TextBox>
                                <asp:Button ID="btnFindEquipment" runat="server" Text="Find" CssClass="btn" OnClick="btnFindEquipment_Click" />
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlEquipmentAdd" runat="server" DefaultButton="btnAdd">
                        <div class="gridview">
                            <asp:Label ID="lblGridTitleCombo" runat="server" Text="Create Combo"></asp:Label>
                            <asp:ScrollableGridView ID="gvEquipments" runat="server" CssClass="ScrollableGridView"
                                AutoGenerateColumns="false" ShowFooter="false" OnRowDataBound="gvEquipments_RowDataBound"
                                SaveScrollPosition="true">
                                <Columns>
                                    <asp:BoundField HeaderText="DIV#" DataField="DivisionName" />
                                    <asp:BoundField HeaderText="Div. State" DataField="DivisionState" />
                                    <asp:BoundField HeaderText="Unit #" DataField="UnitNumber" />
                                    <asp:BoundField HeaderText="Descriptor" DataField="Descriptor" />
                                    <asp:BoundField HeaderText="Status" DataField="EquipmentStatus" />
                                    <asp:BoundField HeaderText="Oper. Status" DataField="Status" ItemStyle-Font-Bold="true" />
                                    <asp:BoundField HeaderText="Job Location" DataField="JobLocation" />
                                    <asp:TemplateField HeaderText="Job #" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlJobNumber" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Combo" DataField="ComboName" />
                                    <asp:BoundField HeaderText="Prim." />
                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkEquipmentAdd" runat="server" />
                                            <asp:Label ID="lblEquipmentId" runat="server" Visible="false" Text='<%# Eval("EquipmentId") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:ScrollableGridView>
                        </div>
                        <div style="width: 100%; text-align: right;">
                            <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn" OnClick="btnAdd_Click"
                                CausesValidation="false" />
                        </div>
                    </asp:Panel>
                    <br />
                    <asp:Panel ID="pnlShoppingCart" runat="server" DefaultButton="btnRemove">
                        <div id="shoppingCart" class="block">
                            <div class="space100">
                                <asp:Label ID="lblShoppTitle" runat="server" Text="Unit(s) Selected:"></asp:Label>
                            </div>
                            <div class="floatLeft space60 block">
                                <div class="inlineBlock">
                                    <asp:ScrollableGridView ID="gvShoppingCart" runat="server" CssClass="ScrollableGridView"
                                        AutoGenerateColumns="false" ShowFooter="false" OnRowDataBound="gvShoppingCart_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkEquipmentAdd" runat="server" />
                                                    <asp:Label ID="lblEquipmentId" runat="server" Visible="false" Text='<%# Eval("EquipmentId") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Primary Unit" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:RadioButton ID="rbPrimaryUnit" runat="server" Checked='<%#Eval("IsPrimary") %>'
                                                        GroupName="PrimaryUnit" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Div #" DataField="DivisionNumber" />
                                            <asp:BoundField HeaderText="Unit #" DataField="UnitNumber" />
                                            <asp:BoundField HeaderText="Descriptor" DataField="Descriptor" />
                                        </Columns>
                                    </asp:ScrollableGridView>
                                </div>
                                <div class="alignRight">
                                    <asp:TextBox ID="txtHasEquipments" runat="server" ValidationGroup="Permitting" Text=""
                                        Style="display: none;" />
                                    <asp:RequiredFieldValidator ID="rfvHasEquipments" runat="server" ControlToValidate="txtHasEquipments"
                                        Display="Dynamic" ErrorMessage="At least one equipment must be selected before saving"
                                        SetFocusOnError="true" Text="*" ValidationGroup="Permitting" />
                                    <asp:TextBox ID="txtHasPrimarySelected" runat="server" ValidationGroup="Permitting"
                                        Text="" Style="display: none;" />
                                    <asp:RequiredFieldValidator ID="rfvHasPrimarySelected" runat="server" ControlToValidate="txtHasPrimarySelected"
                                        Display="Dynamic" ErrorMessage="One equipment must be selected as primary before saving"
                                        SetFocusOnError="true" Text="*" ValidationGroup="Permitting" />
                                    <asp:Button ID="btnRemove" runat="server" Text="Remove" CssClass="btn" OnClick="btnRemove_Click"
                                        CausesValidation="false" />
                                </div>
                            </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlComboInfo" runat="server" DefaultButton="btnSaveContinue">
                        <div class="floatRight paddingBottom5 space39">
                            <div class="floatLeft alignRight paddingTop5 space39">
                                <asp:Label ID="lblComboName" Text="* Combo Name:" runat="server"></asp:Label>&nbsp;
                            </div>
                            <div class="floatright alignleft space60">
                                <asp:TextBox ID="txtComboName" runat="server" CssClass="input" ValidationGroup="Permitting"
                                    MaxLength="250"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvComboName" runat="server" ControlToValidate="txtComboName"
                                    Display="Dynamic" ErrorMessage="The Combo Name field is required" SetFocusOnError="true"
                                    Text="*" ValidationGroup="Permitting" />
                            </div>
                            <div class="floatLeft alignRight paddingTop5 space39">
                                <asp:Label ID="lblComboType" Text="Combo Type:" runat="server"></asp:Label>&nbsp;
                            </div>
                            <div class="floatright alignleft space60">
                                <asp:TextBox ID="txtComboType" runat="server" CssClass="input" ValidationGroup="Permitting"
                                    MaxLength="250"></asp:TextBox>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div id="divButtons" class="inlineBlock alignRight space100">
                    <br />
                    <asp:Button ID="btnSaveContinue" runat="server" Text="Save & Continue" OnClick="btnSaveContinue_Click"
                        CssClass="btn" ValidationGroup="Permitting" />
                    <asp:Button ID="btnSaveClose" runat="server" Text="Save & Close" OnClick="btnSaveClose_Click"
                        CssClass="btn" ValidationGroup="Permitting" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn" CausesValidation="false"
                        OnClick="btnCancel_Click" />
                </div>
                </div>
            </asp:Panel>
            <br />
            <asp:Panel ID="pnlLog" runat="server" Visible="true">
                <div id="divLog" class="Header">
                    <asp:Label ID="lblComboHistory" runat="server" Text="Combo Log History"></asp:Label>
                </div>
                <div class="Content">
                   
                        <div id="divDetails" runat="server">
                            <table border="0" cellspacing="0" cellpadding="2" width="100%">
                                <asp:Repeater ID="rptComboHistory" runat="server" OnItemDataBound="rptComboHistory_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td style="vertical-align: top; font-size: 14px; text-decoration: bold underline;">
                                                <asp:Label ID="lblName" runat="server" Text="" />
                                            </td>
                                            <td style="vertical-align: top;">
                                                <div style="display: inline-block;">
                                                    <div style="text-align: right; width: 15%; display: inline-block; float: left; padding-right: 5px;">
                                                        <asp:Label ID="lblUnits" runat="server" Text="Units: " />
                                                    </div>
                                                    <div style="text-align: left; width: 83%; display: inline-block;">
                                                        <asp:Label ID="lblUnitsText" runat="server" Text="" />
                                                    </div>
                                                </div>
                                                <div style="display: inline-block;">
                                                    <div style="text-align: right; width: 15%; display: inline-block; float: left; padding-right: 5px;">
                                                        <asp:Label ID="lblPrimary" runat="server" Text="Primary: " />
                                                    </div>
                                                    <div style="text-align: left; width: 83%; display: inline-block;">
                                                        <asp:Label ID="lblPrimaryText" runat="server" Text="" />
                                                    </div>
                                                </div>
                                                <div style="display: inline-block;">
                                                    <div style="text-align: right; width: 15%; display: inline-block; float: left; padding-right: 5px;">
                                                        <asp:Label ID="lblDivision" runat="server" Text="Division: " />
                                                    </div>
                                                    <div style="text-align: left; width: 83%; display: inline-block;">
                                                        <asp:Label ID="lblDivisionText" runat="server" Text="" />
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr>
                                            <td style="vertical-align: top; font-size: 14px; text-decoration: bold underline;">
                                                <asp:Label ID="lblName" runat="server" Text="" />
                                            </td>
                                            <td style="vertical-align: top;">
                                                <div style="display: inline-block;">
                                                    <div style="text-align: right; width: 15%; display: inline-block; float: left; padding-right: 5px;">
                                                        <asp:Label ID="lblUnits" runat="server" Text="Units: " />
                                                    </div>
                                                    <div style="text-align: left; width: 83%; display: inline-block;">
                                                        <asp:Label ID="lblUnitsText" runat="server" Text="" />
                                                    </div>
                                                </div>
                                                <div style="display: inline-block;">
                                                    <div style="text-align: right; width: 15%; display: inline-block; float: left; padding-right: 5px;">
                                                        <asp:Label ID="lblPrimary" runat="server" Text="Primary: " />
                                                    </div>
                                                    <div style="text-align: left; width: 83%; display: inline-block;">
                                                        <asp:Label ID="lblPrimaryText" runat="server" Text="" />
                                                    </div>
                                                </div>
                                                <div style="display: inline-block;">
                                                    <div style="text-align: right; width: 15%; display: inline-block; float: left; padding-right: 5px;">
                                                        <asp:Label ID="lblDivision" runat="server" Text="Division: " />
                                                    </div>
                                                    <div style="text-align: left; width: 83%; display: inline-block;">
                                                        <asp:Label ID="lblDivisionText" runat="server" Text="" />
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                    <SeparatorTemplate>
                                        <tr>
                                            <td colspan="2">
                                                <hr />
                                            </td>
                                        </tr>
                                    </SeparatorTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
                  
            </asp:Panel>
            <asp:Button ID="btnFakeSort" runat="server" OnClick="btnFakeSort_Click" Style="display: none;" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript" language="javascript" defer="defer">

        //Instance of ScripManager
        var scriptManager = Sys.WebForms.PageRequestManager.getInstance();
        scriptManager.add_endRequest(RestoreCollapsedItems);

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

        // Collapse-Expand functionality
        function CollapseExpand(Button, selector) {
            var line = $("." + selector);
            var button = $("#" + Button);
            var expandedItems = $get('<%=hfExpandedCombos.ClientID %>').value;
            var expandedButton = $get('<%=hfExpandedButtons.ClientID %>').value;

            line.toggle();
            button.toggleClass("Expand");
            button.toggleClass("Collapse");

            if (button.attr('class') == 'Expand') {
                expandedItems = expandedItems.replace(';' + selector, '');
                expandedButton = expandedButton.replace(';' + Button, '');
            }
            else {
                expandedItems += ";" + selector;
                expandedButton += ";" + Button;
            }

            $get('<%=hfExpandedCombos.ClientID %>').value = expandedItems;
            $get('<%=hfExpandedButtons.ClientID %>').value = expandedButton;
        }

        function RestoreCollapsedItems() {
            var expandedItems = $get('<%=hfExpandedCombos.ClientID %>').value.split(';');
            var expandedButton = $get('<%=hfExpandedButtons.ClientID %>').value.split(';');
            var line;
            var button;

            for (var i = 0; i < expandedItems.length; i++) {
                if (expandedItems[i] != '') {
                    line = $("." + expandedItems[i]);

                    line.toggle();
                }
            }

            for (var j = 0; j < expandedButton.length; j++) {

                if (expandedButton[j] != '') {
                    button = $("#" + expandedButton[j]);

                    button.toggleClass("Expand");
                    button.toggleClass("Collapse");
                }
            }
        }

        function SetUniqueRadioButton(nameregex, current) {
            re = new RegExp(nameregex);
            for (i = 0; i < document.forms[0].elements.length; i++) {
                elm = document.forms[0].elements[i];
                if (elm.type == 'radio') {
                    if (re.test(elm.name)) {
                        elm.checked = false;
                    }
                }
            }
            current.checked = true;
        }
    </script>
</asp:Content>
