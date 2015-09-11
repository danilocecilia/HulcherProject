<%@ Page Title="Route Maintenance" Language="C#" MasterPageFile="~/ContentPage.master"
    AutoEventWireup="true" CodeBehind="RouteMaintenance.aspx.cs" Inherits="Hulcher.OneSource.CustomerService.Web.RouteMaintenance" %>

<%@ MasterType TypeName="Hulcher.OneSource.CustomerService.Web.ContentPage" %>
<%@ Register Src="~/UserControls/AutoCompleteTextbox.ascx" TagName="AutoCompleteTextbox"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/jquery.multiselect.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Forms.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <asp:UpdatePanel ID="upPage" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnCreate"/>
        </Triggers>
        <ContentTemplate>
            <asp:Label ID="lblTitle" runat="server" Text="Route Maintenance" Font-Size="Large"
                Font-Bold="true"></asp:Label>
            <br />
            <br />
            <asp:Panel ID="pnlNoAccess" runat="server" Visible="false">
                <div>
                    <div>
                        <asp:Label ID="lblTitleNoAccess" runat="server" Text="Route Maintenance Details" />
                    </div>
                    <div>
                        The current user does not have access to this functionality!
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlVisualization" runat="server" DefaultButton="btnCreate">
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" CssClass="errorbox"
                    HeaderText="Please correct the following information" ValidationGroup="RouteFilter" />
                <div id="divVisualization" class="Header">
                    <asp:Label ID="lblVisualizationTitle" runat="server" Text="Route Viewer"></asp:Label>
                </div>
                <div class="Content">
                    <asp:Button ID="btnCreate" runat="server" Text="New Route" CssClass="btn" OnClick="btnCreate_Click"
                        CausesValidation="false" />
                    <asp:Panel ID="pnlFilter" runat="server" DefaultButton="btnFindLocation">
                        <div class="inlineBlock floatRight alignRight">
                            <div>
                                <asp:Label ID="Label7" runat="server" Text="State: "></asp:Label>
                                <uc1:AutoCompleteTextbox ID="actFilterState" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                    TextBoxWidth="120px" GridViewIdName="ID" DisplayField="Name" AutoPostBack="false"
                                    RequiredField="false" WindowTitle="Route Maintenance - Find State" ErrorMessage="Route Maintenance - State field is required"
                                    AutoCompleteSource="State" ColumnHeaderList="Acronym,Name" ColumnValueList="Acronym,Name"
                                    ServiceMethod="GetStateList" TextBoxCssClass="input" ControlsToUpdate="actFilterCity"
                                    OnTextChanged="actFilterState_TextChanged" MinimumPrefixLength="2" ValidationGroup="RouteFilter" />
                                <asp:Label ID="Label83" runat="server" Text="City: "></asp:Label>
                                <uc1:AutoCompleteTextbox ID="actFilterCity" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                    TextBoxWidth="120px" GridViewIdName="ID" DisplayField="Name" AutoPostBack="false"
                                    RequiredField="false" WindowTitle="Route Maintenance - Find City" ErrorMessage="RouteMaintenance - City field is required"
                                    AutoCompleteSource="City" ColumnHeaderList="Name" ColumnValueList="Name" ServiceMethod="GetCityList"
                                    TextBoxCssClass="input" BehaviorId="actFilterCity" ValidationGroup="RouteFilter"
                                    ControlsToUpdate="actZipCode" ScriptToExecute="CallCityWebService" />
                                <asp:Label ID="lblZipCode" runat="server" Text="ZipCode: "></asp:Label>
                                <uc1:AutoCompleteTextbox ID="actFilterZipCode" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                    TextBoxWidth="120px" GridViewIdName="ID" DisplayField="" AutoPostBack="false"
                                    RequiredField="false" WindowTitle="Find ZipCode" AutoCompleteSource="ZipCode"
                                    ColumnHeaderList="Name" ColumnValueList="Name" ServiceMethod="GetZipCodeList"
                                    TextBoxCssClass="input" BehaviorId="actZipCode" />
                                <asp:Button ID="btnFindLocation" runat="server" Text="Find" CssClass="btn" OnClick="btnFindLocation_Click"
                                    ValidationGroup="RouteFilter" />
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:ScrollableGridView ID="gvRoute" runat="server" CssClass="ScrollableGridView"
                        AutoGenerateColumns="false" ShowFooter="false" OnRowCommand="gvRoute_RowCommand"
                        OnRowDataBound="gvRoute_RowDataBound" DataKeyNames="ID">
                        <Columns>
                            <asp:TemplateField HeaderText="Location">
                                <ItemTemplate>
                                    <asp:Label ID="lblLocation" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Division">
                                <ItemTemplate>
                                    <asp:Label ID="lblDivision" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Miles" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblMiles" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Hours" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblHours" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fuel" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblFuel" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="City Permit Office">
                                <ItemTemplate>
                                    <asp:Label ID="lblCityPermitOffice" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="County Permit Office">
                                <ItemTemplate>
                                    <asp:Label ID="lblCountyPermitOffice" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CausesValidation="false"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkRemove" runat="server" Text="Remove" CausesValidation="false"
                                        OnClientClick="return confirm('Are you sure you want to delete this route?')"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:ScrollableGridView>
                </div>
            </asp:Panel>
            <br />
            <asp:Panel ID="pnlCreation" runat="server" Visible="true">
                <asp:ValidationSummary ID="vsRoute" runat="server" CssClass="errorbox" HeaderText="Please correct the following information"
                    ValidationGroup="Route" />
                <div id="divCreation" class="Header">
                    <asp:Label ID="lblCreationTitle" runat="server" Text="Create/Edit Route"></asp:Label>
                </div>
                <div class="Content">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="errorbox"
                        HeaderText="Please correct the following information" ValidationGroup="Add" />
                    <div class="space100 paddingBottom5">
                        <div class="paddingBottom5 paddingTop5">
                            <div class="floatLeft paddingRight20">
                                <asp:Label ID="lblState" runat="server" Text="State: "></asp:Label>
                                <uc1:AutoCompleteTextbox ID="actState" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                    TextBoxWidth="120px" GridViewIdName="ID" DisplayField="" AutoPostBack="false"
                                    RequiredField="true" WindowTitle="Route Maintenance - Find State" ErrorMessage="Route Maintenance - State field is required"
                                    AutoCompleteSource="State" ColumnHeaderList="Acronym,Name" ColumnValueList="Acronym,Name"
                                    ServiceMethod="GetStateList" TextBoxCssClass="input" ControlsToUpdate="actCity"
                                    OnTextChanged="actState_TextChanged" MinimumPrefixLength="2" />
                            </div>
                            <div class="floatLeft paddingRight20">
                                <asp:Label ID="lblCity" runat="server" Text="City: "></asp:Label>
                                <uc1:AutoCompleteTextbox ID="actCity" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                    TextBoxWidth="120px" GridViewIdName="ID" DisplayField="Name" AutoPostBack="false"
                                    RequiredField="true" WindowTitle="Route Maintenance - Find City" ErrorMessage="Route Maintenance - City field is required"
                                    AutoCompleteSource="City" ColumnHeaderList="Name" ColumnValueList="Name" ServiceMethod="GetCityList"
                                    TextBoxCssClass="input" BehaviorId="actCity" ValidationGroup="Add" ControlsToUpdate="actZipCode" />
                            </div>
                            <div class="floatLeft paddingRight20">
                                <asp:Label ID="lblZipCodeAdd" runat="server" Text="ZipCode: "></asp:Label>
                                <uc1:AutoCompleteTextbox ID="actZipCode" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                    TextBoxWidth="120px" GridViewIdName="ID" DisplayField="" AutoPostBack="false"
                                    RequiredField="false" WindowTitle="Find ZipCode" AutoCompleteSource="ZipCode"
                                    ColumnHeaderList="Name" ColumnValueList="Name" ServiceMethod="GetZipCodeList"
                                    TextBoxCssClass="input" BehaviorId="actZipCode"  />
                            </div>
                            <div class="paddingRight5 floatLeft paddingTop5">
                                <asp:Label ID="Label8" runat="server" Text="Division: "></asp:Label>
                            </div>
                            <div class="paddingRight10 floatLeft">
                                <asp:MultiSelectDropDownList ID="ddlDivision" runat="server" IsRequired="true" ValidationGroup="Add"
                                    SelectionMode="Multiple" OnClientClick="ValidateDivisions();" CssClass="multiselectdropdown"
                                    ClientIDMode="Static">
                                </asp:MultiSelectDropDownList>
                                <asp:TextBox ID="txtHasDivision" runat="server" CssClass="input" Style="display: none"></asp:TextBox>
                            </div>
                            <div class="paddingRight5 floatLeft  paddingTop5">
                                <asp:RequiredFieldValidator ID="rfvHasDivision" runat="server" ControlToValidate="txtHasDivision"
                                    Display="Dynamic" ErrorMessage="Route Maintenance - You must choose at least one division before click on add buton."
                                    Text="*" ValidationGroup="Add" />
                            </div>
                            <div class="paddingRight5 floatRight paddingTop5">
                                <asp:Button ID="btnAdd" runat="server" CssClass="btn" Text="Add" ValidationGroup="Add"
                                    OnClientClick="if(Page_ClientValidate('Add')){Add();}return false;" />
                            </div>
                        </div>
                        <div class="space100 inlineBlock">
                            <div id="tbRoute_Group" class="ScrollableGridView_Group" style="width: 100%">
                                <div id="tbRoute_HeaderDiv" class="ScrollableGridView_HeaderDiv" style="min-width: 150px;">
                                </div>
                                <div id="tbRoute_ScrollDiv" class="ScrollableGridView_ScrollDiv" style="max-height: 300px; min-width: 200px;">
                                    <table id="tbRoute" class="ScrollableGridView" cellspacing="1">
                                        <thead>
                                            <tr style="position: relative; top: expression(this.offsetParent.scrollTop -1); left: expression(this.offsetParent.style.left);">
                                                <th style="display: none;">
                                                </th>
                                                <th>
                                                    <asp:Label ID="header1" CssClass="MarginRight" runat="server" Text="City/State/ZipCode"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="header2" CssClass="MarginRight" runat="server" Text="Division"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="Label1" CssClass="MarginRight" runat="server" Text="Miles"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="Label2" CssClass="MarginRight" runat="server" Text="Hours"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="Label3" CssClass="MarginRight" runat="server" Text="Fuel"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="Label5" CssClass="MarginRight" runat="server" Text="City Permit Office"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="Label6" CssClass="MarginRight" runat="server" Text="County Permit Office"></asp:Label>
                                                </th>
                                                <th>
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <div>
                                        <asp:RequiredFieldValidator ID="rfHiddenTextBox" ValidationGroup="Route" runat="server"
                                            Text="*" ControlToValidate="hidDivisions" Display="Dynamic" ErrorMessage="You cannot save before add data on the table."></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="inlineBlock alignRight space100">
                    <br />
                    <asp:Button ID="btnSaveContinue" runat="server" Text="Save & Continue" OnClick="btnSaveContinue_Click"
                        CausesValidation="true" CssClass="btn" ValidationGroup="Route" />
                    <asp:Button ID="btnSaveClose" runat="server" Text="Save & Close" OnClick="btnSaveClose_Click"
                        CssClass="btn" CausesValidation="true" ValidationGroup="Route" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn" CausesValidation="false"
                        OnClick="btnCancel_Click" />
                </div>
                <asp:TextBox ID="hidDivisions" runat="server" Style="display: none" ValidationGroup="Route" />
            </asp:Panel>
            <script src="/Scripts/formatFunctions.js" type="text/javascript"></script>
            <script type="text/javascript" src="/Scripts/jquery.multiselect.min.js"></script>
            <script language="javascript" type="text/javascript">

                function CallCityWebService(cityId) {
                    if (cityId != 0) {
                        tempuri.org.IJSONService.GetZipCodeByCity(cityId, CallCityWebServiceCompleted)
                    }
                }

                function CallCityWebServiceCompleted(WebServiceResult) {
                    if (null != WebServiceResult) {
                        var zipCodeControl = $find('actZipCode');
                        zipCodeControl.raiseItemSelected(new Sys.Extended.UI.AutoCompleteItemEventArgs(null, WebServiceResult.Name, WebServiceResult.Id));
                    }
                }

                function SetFocusToCity(WebServiceResult) {
                    document.getElementById('<%=actCity.TextControlClientID%>').focus();
                }

                function SetFocusToFilterCity(WebServiceResult) {
                    document.getElementById('<%=actFilterCity.TextControlClientID%>').focus();
                }


                function Add() {
                    var hidden = document.getElementById('<%= hidDivisions.ClientID %>');
                    var city = document.getElementById('<%= actCity.HiddenFieldValueClientID %>');
                    var cityName = document.getElementById('<%=actCity.TextControlClientID %>');
                    var divisions = document.getElementById('<%= ddlDivision.ClientID %>');
                    var state = document.getElementById('<%= actState.HiddenFieldValueClientID %>');
                    var stateName = document.getElementById('<%= actState.TextControlClientID %>');
                    var tbRoute = document.getElementById('tbRoute');
                    var zipCode = document.getElementById('<%=  actZipCode.HiddenFieldValueClientID %>');
                    var zipCodeName = document.getElementById('<%=  actZipCode.TextControlClientID %>');

                    if (null != hidden && null != divisions) {
                        var count = divisions.options.length;
                        for (var i = 0; i < count; i++) {
                            if (divisions.options[i].selected) {
                                var divisionString = '|' + divisions.options[i].value + ';' + divisions.options[i].text + ';' + city.value + ';' + cityName.value + ' - ' + zipCodeName.value + ';0;;;;' + zipCode.value + ';;;;';
                                var divisionStringSearch = '|' + divisions.options[i].value + ';' + divisions.options[i].text + ';' + city.value + ';' + cityName.value;

                                if (hidden.value.indexOf(divisionStringSearch) < 0) {
                                    hidden.value += divisionString;
                                }
                            }
                        }

                        CreateTable();
                    }

                    city.value = "0";
                    cityName.value = "";
                    state.value = "0";
                    stateName.value = "";
                    zipCodeName.value = "";
                    zipCode.value = "0";

                    $("#<%= ddlDivision.ClientID %>").multiselect("uncheckAll");

                }

                function getCursorPosition(ctrl) {
                    var CaretPos = 0; // IE Support
                    if (document.selection) {
                        ctrl.focus();
                        var Sel = document.selection.createRange();
                        Sel.moveStart('character', -ctrl.value.length);
                        CaretPos = Sel.text.length;
                    }
                    // Firefox support
                    else if (ctrl.selectionStart || ctrl.selectionStart == '0')
                        CaretPos = ctrl.selectionStart;
                    return (CaretPos);
                }
                function setCursorPosition(ctrl, pos) {
                    if (ctrl.setSelectionRange) {
                        ctrl.focus();
                        ctrl.setSelectionRange(pos, pos);
                    }
                    else if (ctrl.createTextRange) {
                        var range = ctrl.createTextRange();
                        range.collapse(true);
                        range.moveEnd('character', pos);
                        range.moveStart('character', pos);
                        range.select();
                    }
                }

                function CreateTable() {
                    var hidDivisions = document.getElementById('<%= hidDivisions.ClientID %>');
                    var tbRoute = document.getElementById('tbRoute');
                    var hasDivision = document.getElementById('<%= txtHasDivision.ClientID %>');

                    if (tbRoute != null) {
                        //Clear Table
                        var rowCount = tbRoute.rows.length;

                        for (var i = rowCount - 1; i > 0; i--) {
                            tbRoute.deleteRow(i);
                        }

                        if ('' != hidDivisions.value) {
                            if (hasDivision != null)
                                hasDivision.value = '1';

                            var divisionList = hidDivisions.value.substring(1).split('|');

                            //Insert New Values
                            for (var i = 0; i < divisionList.length; i++) {
                                var rowIndex = parseInt(i + 1);
                                var values = divisionList[i].split(';');

                                var row = tbRoute.insertRow(rowIndex);

                                //City ID
                                var cell1 = row.insertCell(-1);
                                cell1.innerHTML = values[2];
                                cell1.style.display = 'none';

                                //City Name
                                var cell2 = row.insertCell(-1);
                                cell2.innerHTML = values[3];

                                //Division Id
                                var cell3 = row.insertCell(-1);
                                cell3.innerHTML = values[0];
                                cell3.style.display = 'none';

                                //Division Name
                                var cell4 = row.insertCell(-1);
                                cell4.innerHTML = values[1];

                                // Id Route
                                var cell5 = row.insertCell(-1);
                                cell5.innerHTML = values[4];
                                cell5.style.display = 'none';

                                //Miles TextBox
                                var cell6 = row.insertCell(-1);
                                var textbox = document.createElement('input');
                                textbox.id = 'mile' + rowIndex;
                                textbox.type = 'text';
                                textbox.className = 'input ' + values[5];
                                textbox.style.width = '35px';
                                textbox.value = values[5];
                                textbox.onblur = function () { UpdateFields(this, 'miles', this.value, this.className.replace('input ', ''), this.id.replace('mile', '')); };
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
                                cell6.appendChild(textbox);

                                //Hours TextBox
                                var cell7 = row.insertCell(-1);
                                var textbox = document.createElement('input');
                                textbox.id = 'hour' + rowIndex;
                                textbox.type = 'text';
                                textbox.className = 'input ' + values[6];
                                textbox.style.width = '35px';
                                textbox.value = values[6];
                                textbox.onblur = function () { formatDecimalOO(this.value, this); UpdateFields(this, 'hours', this.value, this.className.replace('input ', ''), this.id.replace('hour', '')); };
                                textbox.onkeyup = function (event) {
                                    var cursorPos = getCursorPosition(this);
                                    event = event || window.event;
                                    var charCode = (event.which) ? event.which : event.keyCode

                                    if (this.value.indexOf('.') != -1) {
                                        var nums = this.value.split('.');
                                        if (nums[0].length == 1)
                                            this.value = "0" + this.value;
                                    }

                                    var text = this.value.replace('.', '');

                                    if (charCode == 37 || charCode == 39)
                                        return true;

                                    if (text.length == 2 && (charCode == 46 || charCode == 8))
                                        return true;

                                    if (charCode == 190 && text.length == 1) {
                                        text = "0" + text + '.';
                                        cursorPos++;
                                    }
                                    else if (text.length == 2) {
                                        text = text + '.';
                                        cursorPos++;
                                    }
                                    else if (text.length > 4)
                                        text = text.substring(0, 2) + '.' + text.substring(2, 4);
                                    else if (text.length > 2)
                                        text = text.substring(0, 2) + '.' + text.substring(2, text.length);

                                    this.value = text;
                                    setCursorPosition(this, cursorPos);
                                };

                                textbox.onkeydown = function (event) {
                                    event = event || window.event;
                                    var charCode = (event.which) ? event.which : event.keyCode
                                    if (charCode == 46 || charCode == 37 || charCode == 39)
                                        return true;
                                    if (event.srcElement.value.length >= 5 && charCode > 31)
                                        return false;
                                    if (charCode > 31 && !((charCode > 47 && charCode < 58) || (charCode > 95 && charCode < 106)) && (charCode != 190 || (charCode == 190 && event.srcElement.value.indexOf('.') != -1)))
                                        return false;

                                    return true;
                                };
                                cell7.appendChild(textbox);

                                //Fuel TextBox
                                var cell8 = row.insertCell(-1);
                                var textbox = document.createElement('input');
                                textbox.id = 'fuel' + rowIndex;
                                textbox.type = 'text';
                                textbox.className = 'input ' + values[7];
                                textbox.style.width = '35px';
                                textbox.value = values[7];
                                textbox.onblur = function () { UpdateFields(this, 'fuel', this.value, this.className.replace('input ', ''), this.id.replace('fuel', '')); };
                                textbox.onkeydown = function (event) {
                                    event = event || window.event;
                                    var charCode = (event.which) ? event.which : event.keyCode
                                    if (charCode == 46 || charCode == 37 || charCode == 39)
                                        return true;
                                    if (event.srcElement.value.length >= 4 && charCode > 31)
                                        return false;
                                    if (charCode > 31 && !((charCode > 47 && charCode < 58) || (charCode > 95 && charCode < 106)))
                                        return false;

                                    return true;
                                };
                                cell8.appendChild(textbox);

                                //ZipCode ID
                                var cell9 = row.insertCell(-1);
                                cell9.innerHTML = values[8];
                                cell9.style.display = 'none';

                                //CityPermitOffice TextBox
                                var cell10 = row.insertCell(-1);
                                var textbox = document.createElement('textarea');
                                textbox.id = 'citypo' + rowIndex;
                                textbox.className = 'input ' + values[9];
                                textbox.style.width = '100px';
                                textbox.style.height = '90px';
                                textbox.value = values[9];
                                textbox.onblur = function () { UpdateFields(this, 'citypo', this.value, this.className.replace('input ', ''), this.id.replace('citypo', '')); };
                                textbox.onkeydown = function (event) {
                                    event = event || window.event;
                                    var charCode = (event.which) ? event.which : event.keyCode
                                    if ((event.srcElement.value.length >= 300 && charCode > 31) || (charCode == 226 && event.shiftKey) || charCode == 191)
                                        return false;
                                    return true;
                                };
                                cell10.appendChild(textbox);

                                //CountyPermitOffice TextBox
                                var cell11 = row.insertCell(-1);
                                var textbox = document.createElement('textarea');
                                textbox.id = 'countypo' + rowIndex;
                                textbox.style.width = '100px';
                                textbox.style.height = '90px';
                                textbox.className = 'input ' + values[10];

                                textbox.value = values[10];
                                textbox.onblur = function () { UpdateFields(this, 'countypo', this.value, this.className.replace('input ', ''), this.id.replace('countypo', '')); };
                                textbox.onkeydown = function (event) {
                                    event = event || window.event;
                                    var charCode = (event.which) ? event.which : event.keyCode
                                    if ((event.srcElement.value.length >= 300 && charCode > 31) || (charCode == 226 && event.shiftKey) || charCode == 191)
                                        return false;
                                    return true;
                                };
                                cell11.appendChild(textbox);

                                // Created By
                                var cell12 = row.insertCell(-1);
                                cell12.innerHTML = values[11];
                                cell12.style.display = 'none';

                                // Creation Date
                                var cell13 = row.insertCell(-1);
                                cell13.innerHTML = values[12];
                                cell13.style.display = 'none';

                                // Remove Button

                                var cell14 = row.insertCell(-1);
                                if (values[4] == '0') {
                                    var button = document.createElement('input');
                                    button.id = 'btnRemove' + rowIndex;
                                    button.type = 'button';
                                    button.setAttribute('className', 'linkButtonStyle');
                                    button.value = 'Remove';
                                    button.onclick = function () { RemoveEquipment(this.id.replace('btnRemove', '')); };
                                    cell14.appendChild(button);
                                }
                            }
                        }

                        if (tbRoute.rows.length <= 1) {
                            var row = tbRoute.insertRow(1);

                            var cell1 = row.insertCell(0);
                            cell1.innerHTML = 'The list is Empty';
                            cell1.style.textAlign = 'center';
                            cell1.colSpan = '8';

                            if (hasDivision != null)
                                hasDivision.value = '';

                            tbRoute.rows[0].style.display = 'none';
                        }
                        else
                            tbRoute.rows[0].style.display = '';
                    }
                }


                function UpdateFields(textbox, fieldName, newvalue, oldvalue, index) {
                    var hidden = document.getElementById('<%= hidDivisions.ClientID%>');
                    var tbEquipments = document.getElementById('tbRoute');

                    var replaceFrom = '';
                    var replaceTo = '';
                    var row = tbRoute.rows[index];

                    var idCity = row.cells[0].innerHTML;
                    var nameCity = row.cells[1].innerHTML;
                    var idZipCode = row.cells[8].innerHTML;

                    var idDivision = row.cells[2].innerHTML;
                    var nameDivision = row.cells[3].innerHTML;
                    var idRoute = row.cells[4].innerHTML;
                    var miles = document.getElementById('mile' + index).value;
                    var hours = document.getElementById('hour' + index).value;
                    var fuel = document.getElementById('fuel' + index).value;
                    var cityPermitOffice = document.getElementById('citypo' + index).value;
                    var countyPermitOffice = document.getElementById('countypo' + index).value;
                    var creationdate = row.cells[11].innerHTML;
                    var createdBy = row.cells[12].innerHTML;

                    switch (fieldName) {
                        case 'miles':
                            replaceFrom = '|' + idDivision + ';' + nameDivision + ';' + idCity + ';' + nameCity + ';' + idRoute + ';' + oldvalue + ';' + hours + ';' + fuel + ';' + idZipCode + ';' + cityPermitOffice + ';' + countyPermitOffice + ';' + creationdate + ';' + createdBy ;
                            replaceTo = '|' + idDivision + ';' + nameDivision + ';' + idCity + ';' + nameCity + ';' + idRoute + ';' + newvalue + ';' + hours + ';' + fuel + ';' + idZipCode + ';' + cityPermitOffice + ';' + countyPermitOffice + ';' + creationdate + ';' + createdBy;

                            textbox.className = 'input ' + fieldName;
                            break;
                        case 'hours':
                            replaceFrom = '|' + idDivision + ';' + nameDivision + ';' + idCity + ';' + nameCity + ';' + idRoute + ';' + miles + ';' + oldvalue + ';' + fuel + ';' + idZipCode + ';' + cityPermitOffice + ';' + countyPermitOffice + ';' + creationdate + ';' + createdBy;
                            replaceTo = '|' + idDivision + ';' + nameDivision + ';' + idCity + ';' + nameCity + ';' + idRoute + ';' + miles + ';' + newvalue + ';' + fuel + ';' + idZipCode + ';' + cityPermitOffice + ';' + countyPermitOffice + ';' + creationdate + ';' + createdBy;

                            textbox.className = 'input ' + fieldName;
                            break;
                        case 'fuel':
                            replaceFrom = '|' + idDivision + ';' + nameDivision + ';' + idCity + ';' + nameCity + ';' + idRoute + ';' + miles + ';' + hours + ';' + oldvalue + ';' + idZipCode + ';' + cityPermitOffice + ';' + countyPermitOffice + ';' + creationdate + ';' + createdBy;
                            replaceTo = '|' + idDivision + ';' + nameDivision + ';' + idCity + ';' + nameCity + ';' + idRoute + ';' + miles + ';' + hours + ';' + newvalue + ';' + idZipCode + ';' + cityPermitOffice + ';' + countyPermitOffice + ';' + creationdate + ';' + createdBy;

                            textbox.className = 'input ' + fieldName;
                            break;
                        case 'citypo':
                            replaceFrom = '|' + idDivision + ';' + nameDivision + ';' + idCity + ';' + nameCity + ';' + idRoute + ';' + miles + ';' + hours + ';' + fuel + ';' + idZipCode + ';' + oldvalue + ';' + countyPermitOffice + ';' + creationdate + ';' + createdBy;
                            replaceTo = '|' + idDivision + ';' + nameDivision + ';' + idCity + ';' + nameCity + ';' + idRoute + ';' + miles + ';' + hours + ';' + fuel + ';' + idZipCode + ';' + newvalue + ';' + countyPermitOffice + ';' + creationdate + ';' + createdBy;

                            textbox.className = 'input ' + fieldName;
                            break;
                        case 'countypo':
                            replaceFrom = '|' + idDivision + ';' + nameDivision + ';' + idCity + ';' + nameCity + ';' + idRoute + ';' + miles + ';' + hours + ';' + fuel + ';' + idZipCode + ';' + cityPermitOffice + ';' + oldvalue + ';' + creationdate + ';' + createdBy;
                            replaceTo = '|' + idDivision + ';' + nameDivision + ';' + idCity + ';' + nameCity + ';' + idRoute + ';' + miles + ';' + hours + ';' + fuel + ';' + idZipCode + ';' + cityPermitOffice + ';' + newvalue + ';' + creationdate + ';' + createdBy;

                            textbox.className = 'input ' + fieldName;
                            break;
                        default:

                    }

                    hidden.value = hidden.value.replace(replaceFrom, replaceTo);

                }

                function RemoveEquipment(index) {
                    var hidden = document.getElementById('<%= hidDivisions.ClientID %>');
                    var tbRoute = document.getElementById('tbRoute');

                    var replace = '';
                    var row = tbRoute.rows[index];

                    var idCity = row.cells[0].innerHTML;
                    var nameCity = row.cells[1].innerHTML;
                    var idZipCode = row.cells[8].innerHTML;
                    var idDivision = row.cells[2].innerHTML;
                    var nameDivision = row.cells[3].innerHTML;
                    var idRoute = row.cells[4].innerHTML;
                    var miles = document.getElementById('mile' + index).value;
                    var hours = document.getElementById('hour' + index).value;
                    var fuel = document.getElementById('fuel' + index).value;
                   
                    var cityPermitOffice = document.getElementById('citypo' + index).value;
                    var countyPermitOffice = document.getElementById('countypo' + index).value;
                    var creationdate = row.cells[11].innerHTML;
                    var createdBy = row.cells[12].innerHTML;

                    replace = '|' + idDivision + ';' + nameDivision + ';' + idCity + ';' + nameCity + ';' + idRoute + ';' + miles + ';' + hours + ';' + fuel + ';' + idZipCode + ';' + cityPermitOffice + ';' + countyPermitOffice + ';' + creationdate + ';' + createdBy;

                    hidden.value = hidden.value.replace(replace, '');

                    CreateTable();
                }

                function ValidateDivisions() {
                    var textbox = document.getElementById('<%= txtHasDivision.ClientID %>');

                    var values = $('#<%= ddlDivision.ClientID %>').val();

                    if (values != null) {
                        textbox.value = values.join();
                    }
                    else
                        textbox.value = '';
                }



                var scriptManager = Sys.WebForms.PageRequestManager.getInstance();
                scriptManager.add_endRequest(function () {
                    CreateTable();
                    $(".multiselectdropdown").each(function (index) {
                        if (!document.getElementById('<%= btnAdd.ClientID %>').disabled)
                            $("#" + $(this).attr('id')).multiselect("enable");
                        else
                            $("#" + $(this).attr('id')).multiselect("disable");
                    });
                });
                $(document).ready(function () {
                    CreateTable();
                });
             
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
