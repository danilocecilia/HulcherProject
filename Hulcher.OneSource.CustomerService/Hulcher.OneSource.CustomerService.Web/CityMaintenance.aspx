<%@ Page Title="City Maintenance" Language="C#" MasterPageFile="~/ContentPage.master" AutoEventWireup="true"
    CodeBehind="CityMaintenance.aspx.cs" Inherits="Hulcher.OneSource.CustomerService.Web.CityMaintenance" %>

<%@ MasterType TypeName="Hulcher.OneSource.CustomerService.Web.ContentPage" %>
<%@ Register Src="~/UserControls/AutoCompleteTextbox.ascx" TagName="AutoCompleteTextbox"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Styles/Forms.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <asp:UpdatePanel ID="upPage" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label ID="lblTitle" runat="server" Text="City Maintenance" Font-Size="Large"
                Font-Bold="true"></asp:Label>
            <br />
            <br />
            <asp:Panel ID="pnlVisualization" runat="server" DefaultButton="btnFind">
                <div id="divVisualization" class="Header">
                    <asp:Label ID="lblVisualizationTitle" runat="server" Text="City List"></asp:Label>
                </div>
                <div class="Content">
                    <div id="divSearchFilters">
                        <div class="inlineBlock floatRight alignRight">
                            <div class="floatLeft paddingTop5 paddingRight5">
                                <asp:Label ID="lblFilter" runat="server" Text="Filter Listing By State:"></asp:Label>
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
                        <asp:ScrollableGridView runat="server" ID="gvCityList" AllowSorting="true" AutoGenerateColumns="false"
                            CssClass="ScrollableGridView" SaveScrollPosition="true" MaxHeight="400px" EnableViewState="true"
                            OnRowDataBound="gvCityList_RowDataBound" OnRowCommand="gvCityList_RowCommand">
                            <Columns>
                                <asp:BoundField HeaderText="City" DataField="ExtendedName" />
                                <asp:TemplateField HeaderText="State">
                                    <ItemTemplate>
                                        <asp:Label ID="lblState" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCountry" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CausesValidation="false"></asp:LinkButton>
                                        <asp:LinkButton ID="lnkRemove" runat="server" Text="Remove" CausesValidation="false" ></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:ScrollableGridView>
                    </div>
                </div>
                <div class="inlineBlock alignRight" style="width: 100%">
                    <br />
                    <asp:Button ID="btnNewCity" runat="server" CssClass="btn" Text="Add City" OnClick="btnNewCity_Click" />
                </div>
            </asp:Panel>
            <br />
            <asp:Panel ID="pnlCreation" runat="server" Visible="false">
                <asp:ValidationSummary ID="vsCity" runat="server" CssClass="errorbox" HeaderText="Please correct the following information" ValidationGroup="City" />
                <div class="Header">
                    <asp:Label ID="lblHeaderCreation" runat="server" Text="City Details"></asp:Label>
                </div>
                <div class="Content">
                    <div class="space100 paddingBottom5">
                        <div class="floatLeft space50 inline">
                            <div class="floatLeft alignRight width140 paddingRight5 inline">
                                <asp:Label ID="lblStateID" Text="State:" runat="server"></asp:Label>
                            </div>
                            <div class="inlineBlock">
                                <uc1:AutoCompleteTextbox ID="actState" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                    TextBoxWidth="120px" GridViewIdName="ID" DisplayField="" AutoPostBack="false" ValidationGroup="City"
                                    RequiredField="true" WindowTitle="City Maintenance - Find State" ErrorMessage="City Maintenance - State field is required"
                                    AutoCompleteSource="State" ColumnHeaderList="Acronym,Name" ColumnValueList="Acronym,Name"
                                    ServiceMethod="GetStateList" TextBoxCssClass="input" MinimumPrefixLength="2" />
                            </div>
                        </div>
                    </div>
                    <div class="space100 paddingBottom5">
                        <div class="floatLeft space50 inline">
                            <div class="floatLeft alignRight width140 paddingRight5 inline">
                                <asp:Label ID="lblName" runat="server" Text="Name:"></asp:Label>
                            </div>
                            <div class="inlineBlock">
                                <asp:TextBox ID="txtName" runat="server" CssClass="input" ValidationGroup="City"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName" Display="Dynamic" ErrorMessage="City Maintenance - Name field is required" Text="*" ValidationGroup="City" />
                            </div>
                            <asp:HiddenField ID="hidCreatedBy" runat="server" />
                            <asp:HiddenField ID="hidCreatedDate" runat="server" />
                        </div>
                    </div>
                    <hr />
                    <br />
                    <asp:ValidationSummary ID="vsZipCode" runat="server" CssClass="errorbox" HeaderText="Please correct the following information" ValidationGroup="ZipCode" />
                    <div class="space100 paddingBottom5">
                        <div class="floatLeft space50 inline">
                            <div class="floatLeft alignRight width140 paddingRight5 paddingBottom5 inline">
                                <asp:Label ID="lblZipCode" runat="server" Text="Zip Code:"></asp:Label>
                            </div>
                            <div class="inlineBlock paddingBottom5">
                                <asp:TextBox ID="txtZipCode" runat="server" CssClass="input" ValidationGroup="ZipCode"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvZipCode" runat="server" ControlToValidate="txtZipCode" ErrorMessage="City Maintenance - The Zip Code field is required" Text="*" ValidationGroup="ZipCode" />
                            </div>
                            <div class="floatLeft alignRight width140 paddingRight5 paddingBottom5 inline">
                                <asp:Label ID="lblLat" runat="server" Text="Latitude:"></asp:Label>
                            </div>
                            <div class="inlineBlock paddingBottom5">
                                <asp:TextBox ID="txtLat" runat="server" CssClass="input" onkeydown="return formatLonLat();"></asp:TextBox>
                            </div>
                            <div class="floatLeft alignRight width140 paddingRight5 paddingBottom5 inline">
                                <asp:Label ID="lblLon" runat="server" Text="Longitude:"></asp:Label>
                            </div>
                            <div class="inlineBlock paddingBottom5">
                                <asp:TextBox ID="txtLon" runat="server" CssClass="input" onkeydown="return formatLonLat();"></asp:TextBox>
                            </div>
                            <div class="floatLeft alignRight width140 paddingRight5 paddingBottom5 inline">
                                &nbsp;
                            </div>
                            <div class="inlineBlock paddingBottom5">
                                
                                <asp:Button ID="btnAddZipCode" runat="server" CssClass="btn" Text="Add Zip Code" OnClientClick="Page_ClientValidate('ZipCode');Add();return false;" ValidationGroup="ZipCode" UseSubmitBehavior="false" />
                                <asp:TextBox ID="txtHasZipCode" runat="server" CssClass="input" ValidationGroup="City" Style="display: none"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvHasZipCode" runat="server" ControlToValidate="txtHasZipCode" Display="Dynamic" ErrorMessage="City Maintenance - At least one Zip Code must be added before saving" Text="*" ValidationGroup="City" />
                            </div>
                        </div>
                        <div class="space49 inlineBlock">
                            <div id="tbZipCode_Group" class="ScrollableGridView_Group" style="width: 100%">
                                <div id="tbZipCode_HeaderDiv" class="ScrollableGridView_HeaderDiv" style="min-width: 150px;">
                                </div>
                                <div id="tbZipCode_ScrollDiv" class="ScrollableGridView_ScrollDiv" style="max-height: 300px;
                                    min-width: 200px;">
                                    <table id="tbZipCode" class="ScrollableGridView" cellspacing="1">
                                        <thead>
                                            <tr style="position: relative; top: expression(this.offsetParent.scrollTop -1); left: expression(this.offsetParent.style.left);">
                                                <th style="display: none;">
                                                </th>
                                                <th>
                                                    <asp:Label ID="header1" CssClass="MarginRight" runat="server" Text="Zip Code"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="header2" CssClass="MarginRight" runat="server" Text="Lat"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="header3" CssClass="MarginRight" runat="server" Text="Lon"></asp:Label>
                                                </th>
                                                <th id="thRemove" runat="server" class="header">
                                                </th>
                                                <th style="display: none;">
                                                </th>
                                                <th style="display: none;">
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="div1" class="inlineBlock alignRight space100">
                    <br />
                    <asp:Button ID="btnSaveCity" runat="server" Text="Save" CssClass="btn" OnClick="btnSaveCity_Click" ValidationGroup="City" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn" OnClientClick="return confirm('All unsaved data will be lost, are you sure you want to cancel?')"
                        OnClick="btnCancel_Click" />
                </div>
            </asp:Panel>
            <asp:HiddenField ID="hidZipcode" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <script src="Scripts/formatFunctions.js" type="text/javascript"></script>
    <script type="text/javascript">
        function Add() {
            var hidden = document.getElementById('<%= hidZipcode.ClientID %>');
            var tbZipCode = document.getElementById('tbZipCode');

            var name = document.getElementById('<%= txtZipCode.ClientID %>');
            var lat = document.getElementById('<%= txtLat.ClientID %>');
            var lon = document.getElementById('<%= txtLon.ClientID %>');

            if (null != hidden && null != lat && null != lon) {
                if (name.value != '') {
                    var zipCodeString = '|' + name.value + ';' + lat.value + ';' + lon.value + ';0;;';
                    var zipCodeStringSearch = '|' + name.value + ';' + lat.value + ';' + lon.value;

                    if (hidden.value.indexOf(zipCodeStringSearch) < 0)
                        hidden.value += zipCodeString;
                }
            }

            CreateTable();

            name.value = "";
            lat.value = "";
            lon.value = "";
        }

        function CreateTable() {
            var hidden = document.getElementById('<%= hidZipcode.ClientID %>');
            var tbZipCode = document.getElementById('tbZipCode');
            var hasZipCode = document.getElementById('<%= txtHasZipCode.ClientID %>');

            if (tbZipCode != null) {
                //Clear Table
                var rowCount = tbZipCode.rows.length;
                for (var i = rowCount - 1; i > 0; i--) {
                    tbZipCode.deleteRow(i);
                }

                if ('' != hidden.value) {
                    if (hasZipCode != null)
                        hasZipCode.value = '1';

                    var equipmentList = hidden.value.substring(1).split('|');

                    //Insert New Values
                    for (var i = 0; i < equipmentList.length; i++) {
                        var rowIndex = parseInt(i + 1);
                        var values = equipmentList[i].split(';');

                        var row = tbZipCode.insertRow(rowIndex);

                        // ZipCode Id
                        var cell1 = row.insertCell(-1);
                        cell1.innerHTML = values[3];
                        cell1.style.display = 'none';

                        // Zip code name
                        var cell2 = row.insertCell(-1);
                        cell2.innerHTML = values[0];

                        // latitude TextBox
                        var cell3 = row.insertCell(-1);
                        var textbox = document.createElement('input');
                        textbox.id = 'lat' + rowIndex;
                        textbox.type = 'text';
                        textbox.className = 'input ' + values[1];
                        textbox.style.width = '75px';
                        textbox.value = values[1];
                        textbox.onblur = function () { UpdateLatLon(this.id.replace('lat', '')); };
                        textbox.onkeydown = function (event) {
                            event = event || window.event;
                            var charCode = (event.which) ? event.which : event.keyCode
                            if (charCode == 46 || charCode == 37 || charCode == 39)
                                return true;
                            if (event.srcElement.value.length >= 4 && charCode > 31)
                                return false;
                            if (charCode > 31 && !((charCode > 47 && charCode < 58) || (charCode > 95 && charCode < 106)))
                                return false;
                            if (charCode > 31 && !((charCode > 47 && charCode < 58) || (charCode > 95 && charCode < 106)) && (charCode != 190 || (charCode == 190 && event.srcElement.value.indexOf('.') != -1)))
                                return false;

                            return true;
                        };
                        cell3.appendChild(textbox);

                        // Longitude TextBox
                        var cell4 = row.insertCell(-1);
                        var textbox = document.createElement('input');
                        textbox.id = 'lon' + rowIndex;
                        textbox.type = 'text';
                        textbox.className = 'input ' + values[2];
                        textbox.style.width = '75px';
                        textbox.value = values[2];
                        textbox.onblur = function () { UpdateLatLon(this.id.replace('lon', '')); };
                        textbox.onkeydown = function (event) {
                            event = event || window.event;
                            var charCode = (event.which) ? event.which : event.keyCode
                            if (charCode == 46 || charCode == 37 || charCode == 39)
                                return true;
                            if (event.srcElement.value.length >= 4 && charCode > 31)
                                return false;
                            if (charCode > 31 && !((charCode > 47 && charCode < 58) || (charCode > 95 && charCode < 106)))
                                return false;
                            if (charCode > 31 && !((charCode > 47 && charCode < 58) || (charCode > 95 && charCode < 106)) && (charCode != 190 || (charCode == 190 && event.srcElement.value.indexOf('.') != -1)))
                                return false;

                            return true;
                        };
                        cell4.appendChild(textbox);

                        // Remove Button
                        var cell5 = row.insertCell(-1);
                        var button = document.createElement('input');
                        button.id = 'btnRemove' + rowIndex;
                        button.type = 'button';
                        button.setAttribute('className', 'linkButtonStyle');
                        button.value = 'Remove';
                        button.onclick = function () { RemoveEquipment(this.id.replace('btnRemove', '')); };
                        cell5.appendChild(button);

                        // Created By
                        var cell6 = row.insertCell(-1);
                        cell6.innerHTML = values[4];
                        cell6.style.display = 'none';

                        // Creation Date
                        var cell7 = row.insertCell(-1);
                        cell7.innerHTML = values[5];
                        cell7.style.display = 'none';
                    }
                }

                if (tbZipCode.rows.length <= 1) {
                    var row = tbZipCode.insertRow(1);

                    var cell1 = row.insertCell(0);
                    cell1.innerHTML = 'The list is Empty';
                    cell1.style.textAlign = 'center';
                    cell1.colSpan = '7';

                    if (hasZipCode != null)
                        hasZipCode.value = '';

                    tbZipCode.rows[0].style.display = 'none';
                }
                else
                    tbZipCode.rows[0].style.display = '';
            }
        }

        function UpdateLatLon(index) {
            var hidden = document.getElementById('<%= hidZipcode.ClientID %>');
            var tbZipCode = document.getElementById('tbZipCode');

            var textboxLat = document.getElementById('lat' + index);
            var textboxLon = document.getElementById('lon' + index);

            var oldLat = textboxLat.className.replace('input ', '');
            var oldLon = textboxLon.className.replace('input ', '');

            var lat = textboxLat.value;
            var lon = textboxLon.value;

            var replaceFrom = '';
            var replaceTo = '';
            var row = tbZipCode.rows[index];

            var id = row.cells[0].innerHTML;
            var name = row.cells[1].innerHTML;
            var createdby = row.cells[5].innerHTML;
            var creationdate = row.cells[6].innerHTML;

            replaceFrom = '|' + name + ';' + oldLat + ';' + oldLon + ';' + id + ';' + createdby + ';' + creationdate;
            replaceTo = '|' + name + ';' + lat + ';' + lon + ';' + id + ';' + createdby + ';' + creationdate;
            hidden.value = hidden.value.replace(replaceFrom, replaceTo);

            textboxLat.className = 'input ' + lat;
            textboxLat.className = 'input ' + lon;
        }

        function RemoveEquipment(index) {
            var hidden = document.getElementById('<%= hidZipcode.ClientID %>');
            var tbZipCode = document.getElementById('tbZipCode');

            var replace = '';
            var row = tbZipCode.rows[index];

            var id = row.cells[0].innerHTML;
            var name = row.cells[1].innerHTML;
            var lat = document.getElementById('lat' + index).value;
            var lon = document.getElementById('lon' + index).value;
            var createdby = row.cells[5].innerHTML;
            var creationdate = row.cells[6].innerHTML;

            replace = '|' + name + ';' + lat + ';' + lon + ';' + id + ';' + createdby + ';' + creationdate;
            hidden.value = hidden.value.replace(replace, '');

            CreateTable();
        }

        var scriptManager = Sys.WebForms.PageRequestManager.getInstance();
        scriptManager.add_endRequest(function () {
            CreateTable();
        });
        $(document).ready(function () {
            CreateTable();
        });
    </script>
</asp:Content>
