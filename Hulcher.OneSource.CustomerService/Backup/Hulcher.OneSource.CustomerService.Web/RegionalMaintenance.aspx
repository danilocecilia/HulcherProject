<%@ Page Title="Regional Maintenance" Language="C#" MasterPageFile="~/ContentPage.master"
    AutoEventWireup="true" CodeBehind="RegionalMaintenance.aspx.cs" Inherits="Hulcher.OneSource.CustomerService.Web.RegionalMaintenance" %>

<%@ MasterType TypeName="Hulcher.OneSource.CustomerService.Web.ContentPage" %>
<%@ Register Src="~/UserControls/AutoCompleteTextbox.ascx" TagName="AutoCompleteTextbox"
    TagPrefix="uc1" %>
<%@ Register Assembly="Hulcher.OneSource.CustomerService.Business" Namespace="Hulcher.OneSource.CustomerService.Business.WebControls.ServerControls"
    TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Styles/jquery.multiselect.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Forms.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <asp:UpdatePanel ID="upPage" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblTitle" runat="server" Text="Regional Maintenance" Font-Size="Large"
                Font-Bold="true"></asp:Label>
            <br />
            <asp:Panel ID="pnlVisualization" runat="server">
                <asp:HiddenField ID="hfOrderBy" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfExpandedRegions" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfExpandedDivisions" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfExpandedCombos" runat="server" ClientIDMode="Static" />
                <asp:Button ID="btnFakeSort" runat="server" OnClick="btnFakeSort_Click" Style="display: none;" />
                <div id="divVisualization" class="Header">
                    <asp:Label ID="lblVisualizationTitle" runat="server" Text="Regional Maintenance Viewer"></asp:Label>
                </div>
                <div class="Content">
                    <div id="divFilter" class="filter">
                        <div class="control">
                            <div class="title">
                                <asp:Label ID="lblFilterRegionMaintenance" runat="server" Text="Filter Listing By: " />
                            </div>
                            <div class="combo">
                                <asp:ComboBox ID="ddlFilterRegionMaintenance" runat="server" CssClass="WindowsStyle"
                                    AutoCompleteMode="SuggestAppend" DropDownStyle="DropDown" CaseSensitive="false"
                                    RenderMode="Inline">
                                    <asp:ListItem Text="- Select One -" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Region" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="RVP" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Division" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Employee Name" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="Combo #" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="Equipment #" Value="6"></asp:ListItem>
                                </asp:ComboBox>
                            </div>
                            <div class="textbox">
                                <asp:TextBox ID="txtFilterValueRegionMaintenance" runat="server" CssClass="input" />
                                <asp:Button ID="btnFindRegionMaintenance" runat="server" Text="Find" CssClass="btn"
                                    CausesValidation="false" OnClick="btnFindRegionMaintenance_Click" />
                            </div>
                        </div>
                    </div>
                    <div id="divGrid">
                        <asp:Repeater ID="rptRegion" runat="server" OnItemDataBound="rptRegion_ItemDataBound"
                            OnItemCommand="rptRegion_ItemCommand">
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
                                                    <th id="thRegion" runat="server" class="header" style="width: 20%;" onclick="SetOrderBy('1', this);">
                                                        <asp:Label ID="lblRegionHeader" CssClass="MarginRight" runat="server" Text="Region / RVP"></asp:Label>
                                                    </th>
                                                    <th id="thDivision" runat="server" class="header" style="width: 10%;" onclick="SetOrderBy('2', this);">
                                                        <asp:Label ID="lblDivisionHeader" CssClass="MarginRight" runat="server" Text="Division"></asp:Label>
                                                    </th>
                                                    <th id="thEmployeeEquipment" runat="server" class="header" style="width: 60%;" onclick="SetOrderBy('3', this);">
                                                        <asp:Label ID="lblEmployeeHeader" CssClass="MarginRight" runat="server" Text="Employee / Equipment"></asp:Label>
                                                    </th>
                                                    <th id="thEdit" runat="server" class="header" style="width: 10%;">
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="even">
                                    <td style="width: 15px;">
                                        <div class="Expand" id="divExpand" visible="false" runat="server">
                                        </div>
                                    </td>
                                    <td colspan="3">
                                        <asp:Label ID="lblRegion" runat="server"></asp:Label>
                                        <asp:HiddenField ID="hfRegionID" runat="server" />
                                    </td>
                                    <td>
                                        <%--<asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditRegion" Text="Edit"></asp:LinkButton>--%>
                                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="EditRegion" CommandArgument=""
                                            CssClass="linkButtonStyle" />
                                    </td>
                                </tr>
                                <asp:Repeater ID="rptDivision" runat="server" OnItemDataBound="rptDivision_ItemDataBound">
                                    <ItemTemplate>
                                        <tr id="trDivision" runat="server" class="odd">
                                            <td style="width: 15px;">
                                                <div class="Expand" id="divExpand" visible="false" runat="server">
                                                </div>
                                            </td>
                                            <td>
                                            </td>
                                            <td colspan="3">
                                                <asp:Label ID="lblDivision" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <asp:Repeater ID="rptEmployee" runat="server" OnItemDataBound="rptEmployee_ItemDataBound">
                                            <ItemTemplate>
                                                <tr id="trEmployee" runat="server" class="Child">
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:Label ID="lblEmployee" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:Repeater ID="rptCombo" runat="server" OnItemDataBound="rptCombo_ItemDataBound">
                                            <ItemTemplate>
                                                <tr id="trCombo" runat="server" class="Child">
                                                    <td style="width: 15px;">
                                                        <div class="Expand" id="divExpand" visible="false" runat="server">
                                                        </div>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:Label ID="lblCombo" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <asp:Repeater ID="rptEquipment" runat="server" OnItemDataBound="rptEquipment_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr id="trEquipment" runat="server" class="Child">
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td colspan="2">
                                                                &nbsp;&nbsp;&nbsp;<asp:Label ID="lblEquipment" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody> </table></div></div>
                            </FooterTemplate>
                        </asp:Repeater>
                        <asp:Panel ID="pnlNoRowsRegion" runat="server" Visible="true">
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
                    </div>
                </div>
            </asp:Panel>
            <br />
            <asp:Panel ID="pnlCreation" runat="server" Visible="false">
                <div id="divCreation" class="Header">
                    <asp:Label ID="lblCreationTitle" runat="server" Text="Edit Region"></asp:Label>
                </div>
                <div class="Content">
                    <div>
                        <asp:Label ID="lblRegionName" runat="server" Text="Region: " CssClass="fontBold fontBig"></asp:Label>
                    </div>
                    <br />
                    <div id="editableFields" class="inlineBlock space100">
                        <div class="block floatLeft space39">
                            <div class="floatLeft paddingTop5">
                                <asp:Label ID="lblDivision" runat="server" Text="Division:"></asp:Label>
                            </div>
                            <div class="floatLeft paddingLeft5">
                                <asp:MultiSelectDropDownList ID="ddlDivision" runat="server" SelectionMode="Multiple"
                                    CssClass="multiselectdropdown" OnClientClick="CreateDivisionRow();" ClientIDMode="Static"
                                    DataValueField="id" DataTextField="name" Width="90px">
                                </asp:MultiSelectDropDownList>
                            </div>
                        </div>
                        <div class="block floatLeft paddingLeft5">
                            <div class="floatLeft paddingTop5">
                                <asp:Label ID="lblRegionalRVP" runat="server" Text="Regional RVP:"></asp:Label>
                            </div>
                            <div class="floatLeft paddingLeft5">
                                <asp:ComboBox ID="cbRegionalRVP" runat="server" CssClass="WindowsStyle" AutoCompleteMode="SuggestAppend"
                                    DropDownStyle="DropDown" CaseSensitive="false" DataTextField="FullName" DataValueField="id"
                                    RenderMode="Inline">
                                    <asp:ListItem Text="- Select One -" Value="0" />
                                </asp:ComboBox>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div style="width: 100%">
                        <asp:Panel ID="pnlNoRows" runat="server">
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
                        <asp:Panel ID="pnlGrid" runat="server" Style="display: none;">
                            <div id="tbCriterias_Group" class="ScrollableGridView_Group" style="width: 100%">
                                <div id="tbCriterias_ScrollDiv" class="ScrollableGridView_ScrollDiv" style="max-height: 500px;
                                    min-width: 400px;">
                                    <table id="tbDivisions" class="ScrollableGridView" cellspacing="1">
                                        <thead>
                                            <tr style="position: relative; top: expression(this.offsetParent.scrollTop -1); left: expression(this.offsetParent.style.left);">
                                                <th id="thDivisions" runat="server" class="header">
                                                    <asp:Label ID="lblHeader1" CssClass="MarginRight" runat="server" Text="Selected Divisions"></asp:Label>
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <div id="divButtons" class="inlineBlock alignRight space100">
                    <br />
                    <asp:Button ID="btnSaveContinue" runat="server" Text="Save & Continue" OnClick="btnSaveContinue_Click"
                        CssClass="btn" ValidationGroup="Permitting" />
                    <asp:Button ID="btnSaveClose" runat="server" Text="Save & Close" OnClick="btnSaveClose_Click"
                        CssClass="btn" ValidationGroup="Permitting" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn" CausesValidation="false"
                        OnClientClick="return confirm('All unsaved data will be lost, are you sure you want to cancel?')"
                        OnClick="btnCancel_Click" />
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript" language="javascript" defer="defer">
        // Collapse-Expand functionality
        function CollapseExpandRegion(Button, selector) {
            var line = $(".Division." + selector);
            var button = $("#" + Button);
            var expandedItems = $get('<%=hfExpandedRegions.ClientID %>').value;

            line.toggle();
            button.toggleClass("Expand");
            button.toggleClass("Collapse");

            if (button.attr('class') == 'Expand')
                expandedItems = expandedItems.replace(selector + ';', '');
            else
                expandedItems += selector + ';';
            $get('<%=hfExpandedRegions.ClientID %>').value = expandedItems;

            if (button.attr('class') == 'Expand') {
                var employeeLines = $(".Employee." + selector);
                employeeLines.css("display", "none");

                var comboLines = $(".Combo." + selector);
                comboLines.css("display", "none");

                var equipmentLines = $(".Equipment." + selector);
                equipmentLines.css("display", "none");

                var divisionLinesButton = $(".Division." + selector + " .Collapse");

                divisionLinesButton.toggleClass("Expand");
                divisionLinesButton.toggleClass("Collapse");

                var comboLinesButton = $(".Combo." + selector + " .Collapse");

                comboLinesButton.toggleClass("Expand");
                comboLinesButton.toggleClass("Collapse");
            }
        }

        // Collapse-Expand functionality
        function CollapseExpandDivision(Button, selector) {
            var expandedItems = $get('<%=hfExpandedDivisions.ClientID %>').value;
            var lineEmployee = $(".Employee." + selector);
            lineEmployee.toggle();
            var lineCombo = $(".Combo." + selector);
            lineCombo.toggle();

            var button = $("#" + Button);
            button.toggleClass("Expand");
            button.toggleClass("Collapse");

            if (button.attr('class') == 'Expand')
                expandedItems = expandedItems.replace(selector + ';', '');
            else
                expandedItems += selector + ';';
            $get('<%=hfExpandedDivisions.ClientID %>').value = expandedItems;

            if (button.attr('class') == 'Expand') {
                var equipmentLines = $(".Equipment." + selector);
                equipmentLines.css("display", "none");

                var comboLinesButton = $(".Combo." + selector + " .Collapse");

                comboLinesButton.toggleClass("Expand");
                comboLinesButton.toggleClass("Collapse");
            }
        }

        // Collapse-Expand functionality
        function CollapseExpandCombo(Button, selector) {
            var line = $(".Equipment." + selector);
            line.toggle();
            var expandedItems = $get('<%=hfExpandedCombos.ClientID %>').value;

            var button = $("#" + Button);
            button.toggleClass("Expand");
            button.toggleClass("Collapse");

            if (button.attr('class') == 'Expand')
                expandedItems = expandedItems.replace(selector + ';', '');
            else
                expandedItems += selector + ';';
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




        //Dinamic Bind of the Division Grid
        function CreateDivisionRow() {
            var pnlGrid = document.getElementById('<%= pnlGrid.ClientID %>');
            var pnlNoRows = document.getElementById('<%= pnlNoRows.ClientID %>');

            var lstSource = document.getElementById('<%= ddlDivision.ClientID %>');
            var tbDivisions = document.getElementById("tbDivisions");

            if (tbDivisions != null) {
                var rowCount = tbDivisions.rows.length;
                for (var i = rowCount - 1; i > 0; i--) {
                    tbDivisions.deleteRow(i);
                }

                rowCount = tbDivisions.rows.length;
            }


            if (undefined != lstSource) {
                if (lstSource.length > 0) {
                    var rowCount = tbDivisions.rows.length;
                    var val = "";

                    for (var i = 0; i < lstSource.length; i++)
                        if (lstSource[i].selected)
                            val += (val == "") ? lstSource[i].text : ", " + lstSource[i].text;

                    if (val != "") {

                        var row = tbDivisions.insertRow(rowCount);
                        var cell1 = row.insertCell(0);
                        cell1.innerHTML = val;
                        rowCount++;
                    }
                }

                pnlGrid.style.display = (tbDivisions.rows.length > 1) ? "" : "none";
                pnlNoRows.style.display = (tbDivisions.rows.length > 1) ? "none" : "";
            }
        }

        var scriptManager = Sys.WebForms.PageRequestManager.getInstance();
        scriptManager.add_endRequest(function () {
            CreateDivisionRow();
        });

    </script>
    <script type="text/javascript" src="/Scripts/jquery.multiselect.min.js"></script>
</asp:Content>
