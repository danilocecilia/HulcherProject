<%@ Page Title="Phone Number Listing" Language="C#" MasterPageFile="~/ContentPage.master"
    AutoEventWireup="true" CodeBehind="PhoneListing.aspx.cs" Inherits="Hulcher.OneSource.CustomerService.Web.PhoneListing" %>

<%@ Register Src="~/UserControls/AutoCompleteTextbox.ascx" TagName="AutoCompleteTextbox"
    TagPrefix="uc1" %>
<%@ MasterType TypeName="Hulcher.OneSource.CustomerService.Web.ContentPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Styles/Forms.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <asp:UpdatePanel ID="upPage" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label ID="lblTitle" runat="server" Text="Phone Number Listing" Font-Size="Large"
                Font-Bold="true"></asp:Label>
            <br />
            <br />
            <asp:ValidationSummary ID="vsFilter" runat="server" CssClass="errorbox" HeaderText="Please correct the following information" ValidationGroup="Filter" />
            <asp:ValidationSummary ID="vsDivisionFields" runat="server" ValidationGroup="NewDivisionData" CssClass="errorbox" HeaderText="Please correct the following information" />
            <div class="Header">
                <asp:Label ID="lblFilterTitle" runat="server" Text="Phone Number Listing" />
            </div>
            <div class="Content">
                <asp:Panel ID="pnlFilter" runat="server" DefaultButton="btnFind">
                    <div class="floatLeft space29 inline">
                        <asp:Button ID="btnAddDivision" runat="server" Text="Add Division/Phone Number" CssClass="btn"
                            Style="display: none" OnClick="btnAddDivision_Click" />
                    </div>
                    <div class="filter">
                        <div class="title">
                            <asp:Label ID="lblViewBy" runat="server" Text="View By: " />
                        </div>
                        <div class="control">
                            <div class="combo">
                                <asp:ComboBox ID="cboViewBy" runat="server" CssClass="WindowsStyle" AutoCompleteMode="SuggestAppend"
                                    RenderMode="Inline" DropDownStyle="DropDown" CaseSensitive="false" OnSelectedIndexChanged="cboViewBy_SelectedIndexChanged" ValidationGroup="Filter">
                                    <asp:ListItem Value="0" Text="- Select One -" Selected="True" />
                                    <asp:ListItem Value="1" Text="Division" />
                                    <asp:ListItem Value="2" Text="Company" />
                                    <asp:ListItem Value="3" Text="Employee" />
                                </asp:ComboBox>
                            </div>
                            <div class="textbox">
                                <asp:RequiredFieldValidator ID="rfvViewBy" runat="server" ControlToValidate="cboViewBy"
                                    Display="Dynamic" ErrorMessage="The View By field is required" Text="*" ValidationGroup="Filter" />
                                <div class="title">
                                    <asp:Label ID="lblSearchFor" runat="server" Text="Search For: " />
                                </div>
                                <asp:TextBox ID="txtSearchFor" runat="server" CssClass="input" ValidationGroup="Filter" />
                                <asp:RequiredFieldValidator ID="rfvSearchFor" runat="server" ControlToValidate="txtSearchFor"
                                    Display="Dynamic" ErrorMessage="The Search For field is required" Text="*" ValidationGroup="Filter" />
                                <asp:Button ID="btnFind" runat="server" Text="Find" CssClass="btn" OnClick="btnFind_Click"
                                    ValidationGroup="Filter" />
                            </div>
                        </div>
                    </div>
                    <br />
                    <asp:ScrollableGridView ID="gvCustomer" runat="server" Visible="false" CssClass="ScrollableGridView"
                        AutoGenerateColumns="false" ShowFooter="false" OnRowCommand="gvCustomer_RowCommand"
                        OnRowDataBound="gvCustomer_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Company Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomerName" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Company #">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomerNumber" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Contact Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblContactName" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Phone Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblPhoneType" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Phone Number">
                                <ItemTemplate>
                                    <asp:Label ID="lblPhoneNumber" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Company Notes">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomerNotes" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:ScrollableGridView>
                    <asp:ScrollableGridView ID="gvEmployee" runat="server" CssClass="ScrollableGridView"
                        Visible="false" AutoGenerateColumns="false" ShowFooter="false" OnRowDataBound="gvEmployee_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmployeeName" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Phone Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblPhoneType" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Phone Number">
                                <ItemTemplate>
                                    <asp:Label ID="lblPhoneNumber" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:ScrollableGridView>
                    <asp:ScrollableGridView ID="gvDivision" runat="server" CssClass="ScrollableGridView"
                        Visible="false" AutoGenerateColumns="false" ShowFooter="false" OnRowDataBound="gvDivision_RowDataBound" OnRowCommand="gvDivision_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Division Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblDivision" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Address">
                                <ItemTemplate>
                                    <asp:Label ID="lblAddress" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Phone Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblPhoneType" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Phone Number">
                                <ItemTemplate>
                                    <asp:Label ID="lblPhoneNumber" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnEditDivision" runat="server" Text="Edit" CommandName="EditDivision"
                                        CommandArgument="" CssClass="btn linkButtonStyle" CausesValidation="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:ScrollableGridView>
                    <br />
                </asp:Panel>
                <br />
                <asp:Panel ID="pnlDivisionPhoneNumber" Visible="false" runat="server">
                    <div id="errorDiv" class="errorbox" style="display: none;">
                        <asp:Label ID="lblNumberError" runat="server" Text="Invalid Type or Number, please correct the information"></asp:Label>
                    </div>
                    <div class="floatLeft space50 inline">
                        <div class="floatLeft alignRight width140 paddingRight5 paddingBottom5 inline">
                            <asp:Label ID="lblDivisionName" runat="server" Text="Division Name:"></asp:Label>
                        </div>
                        <div class="inlineBlock paddingBottom5">
                            <asp:TextBox ID="txtDivisionName" runat="server" CssClass="input" ValidationGroup="NewDivisionData"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDivisionName" runat="server" Display="Dynamic" ErrorMessage="The Division Name field is required." Text="*" ControlToValidate="txtDivisionName" ValidationGroup="NewDivisionData"></asp:RequiredFieldValidator>
                        </div>
                        <div class="floatLeft alignRight width140 paddingRight5 paddingBottom5 inline">
                            <asp:Label ID="lblAddress" runat="server" Text="Address:"></asp:Label>
                        </div>
                        <div class="inlineBlock paddingBottom5">
                            <asp:TextBox ID="txtAddress" runat="server" CssClass="input"></asp:TextBox>
                        </div>
                        <div class="floatLeft alignRight width140 paddingRight5 paddingBottom5 inline">
                            State:</div>
                        <div class="inlineBlock paddingBottom5">
                            <uc1:AutoCompleteTextbox ID="actState" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                TextBoxWidth="120px" GridViewIdName="ID" DisplayField="" AutoPostBack="false"
                                RequiredField="false" WindowTitle="Quick Job - Find State" ErrorMessage="Location Info - State field is required"
                                AutoCompleteSource="State" ColumnHeaderList="Acronym,Name" ColumnValueList="Acronym,Name"
                                ServiceMethod="GetStateList" TextBoxCssClass="input" ControlsToUpdate="actCity"
                                ScriptToExecute="SetFocusToCity" MinimumPrefixLength="2" />
                        </div>
                        <div class="floatLeft alignRight width140 paddingRight5 paddingBottom5 inline">
                            City:</div>
                        <div class="inlineBlock paddingBottom5">
                            <uc1:AutoCompleteTextbox ID="actCity" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                TextBoxWidth="120px" GridViewIdName="ID" DisplayField="Name" AutoPostBack="false"
                                RequiredField="false" WindowTitle="Quick Job - Find City" ErrorMessage="Location Info - City field is required"
                                AutoCompleteSource="City" ColumnHeaderList="Name" ColumnValueList="Name" ServiceMethod="GetCityList"
                                TextBoxCssClass="input" BehaviorId="actCity" ControlsToUpdate="actZipCode" ScriptToExecute="CallCityWebService" />
                        </div>
                        <div class="floatLeft alignRight width140 paddingRight5 paddingBottom5 inline">
                            Zip Code:</div>
                        <div class="inlineBlock paddingBottom5">
                            <uc1:AutoCompleteTextbox ID="actZipCode" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                TextBoxWidth="120px" GridViewIdName="ID" DisplayField="" AutoPostBack="false"
                                RequiredField="false" WindowTitle="Quick Job - Find Division" ErrorMessage="Location Info - Zip Code field is required"
                                AutoCompleteSource="ZipCode" ColumnHeaderList="Name" ColumnValueList="Name" ServiceMethod="GetZipCodeList"
                                TextBoxCssClass="input" BehaviorId="actZipCode" />
                        </div>
                        <div class="floatLeft alignRight width140 paddingRight5 paddingBottom5 inline">
                            <asp:Label ID="lblPhoneType" runat="server" Text="Phone Type:"></asp:Label>
                        </div>
                        <div class="inlineBlock paddingBottom5">
                            <asp:ComboBox ID="ddlDivisionPhoneType" runat="server" CssClass="WindowsStyle" AutoCompleteMode="SuggestAppend"
                                DropDownStyle="DropDown" CaseSensitive="false" RenderMode="Inline" Width="100px"
                                DataValueField="ID" DataTextField="Name">
                            </asp:ComboBox>
                        </div>
                        <div class="floatLeft alignRight width140 paddingRight5 paddingBottom5 inline">
                            <asp:Label ID="lblDivisionPhoneNumber" runat="server" Text="Phone:"></asp:Label>
                        </div>
                        <div class="inlineBlock paddingBottom5">
                            <asp:TextBox ID="txtDivisionPhoneArea" runat="server" MaxLength="3" CssClass="input" Width="25px" />
                            <asp:TextBox ID="txtDivisionPhoneNumber" runat="server" MaxLength="7" CssClass="input" Width="100px" />
                            <asp:Button ID="btnDivisionPhoneAdd" runat="server" Text="Add" CssClass="btn" OnClientClick="AddPhone(); return false;" />
                        </div>
                        <div class="floatLeft alignRight width140 paddingRight5 paddingBottom5 inline">
                        </div>
                    </div>
                    <asp:MaskedEditExtender ID="mskDivisionPhoneArea" runat="server" MaskType="Number" Mask="999" TargetControlID="txtDivisionPhoneArea">
                    </asp:MaskedEditExtender>
                    <asp:MaskedEditExtender ID="mskDivisionPhoneNumber" runat="server" MaskType="Number" Mask="999-9999" TargetControlID="txtDivisionPhoneNumber">
                    </asp:MaskedEditExtender>
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
                    <asp:Panel ID="pnlPhoneTable" runat="server">
                        <div id="tbPhoneNumbers_Group" class="ScrollableGridView_Group" style="width: 100%">
                            <div id="tbPhoneNumbers_HeaderDiv" class="ScrollableGridView_HeaderDiv" style="min-width: 150px;">
                            </div>
                            <div id="tbPhoneNumbers_ScrollDiv" class="ScrollableGridView_ScrollDiv" style="max-height: 100px;
                                min-width: 400px;">
                                <table id="tbPhoneNumbers" class="ScrollableGridView" cellspacing="1">
                                    <thead>
                                        <tr style="position: relative; top: expression(this.offsetParent.scrollTop -1); left: expression(this.offsetParent.style.left);">
                                            <th id="thID" runat="server" class="header" visible="false">
                                                <asp:Label ID="header1" CssClass="MarginRight" runat="server" Text="ID" Visible="false"></asp:Label>
                                            </th>
                                            <th id="thType" runat="server" class="header">
                                                <asp:Label ID="header2" CssClass="MarginRight" runat="server" Text="Phone Type"></asp:Label>
                                            </th>
                                            <th id="thNumber" runat="server" class="header">
                                                <asp:Label ID="header3" CssClass="MarginRight" runat="server" Text="Number"></asp:Label>
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
                        <asp:TextBox ID="hfPhoneNumbers" runat="server" style="display:none"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfPhoneNumber" runat="server" ErrorMessage="The Phone Number field is required" Text="*" ControlToValidate="hfPhoneNumbers" ValidationGroup="NewDivisionData"></asp:RequiredFieldValidator>
                    </asp:Panel>
                </asp:Panel>
            </div>
            <asp:Panel ID="pnlButtons" runat="server" Visible="false">
                <div id="div1" class="inlineBlock alignRight space100">
                    <br />
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn" OnClick="btnSave_Click" ValidationGroup="NewDivisionData" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn" OnClientClick="return confirm('Are you sure you want to delete the Division Location and Phones?')" OnClick="btnDelete_Click" Enabled="false" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn" OnClientClick="return confirm('All unsaved data will be lost, are you sure you want to cancel?')" OnClick="btnCancel_Click" />
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
        function SetFocusToCity(WebServiceResult) {
            document.getElementById('<%=actCity.TextControlClientID%>').focus();
        }

        function CallCityWebService(cityId) {
            if (cityId != 0) {
                tempuri.org.IJSONService.GetZipCodeByCity(cityId, CallCityWebServiceCompleted)
            }
        }

        function CallCityWebServiceCompleted(WebServiceResult) {
            var zipCodeControl = $find('actZipCode');
            zipCodeControl.raiseItemSelected(new Sys.Extended.UI.AutoCompleteItemEventArgs(null, WebServiceResult.Name, WebServiceResult.Id));
        }
    </script>
    <script type="text/javascript" language="javascript">

        Sys.UI.DomEvent.addHandler(window, "load", function () {
            setTimeout("AddOnChangeEventViewBy('<%= cboViewBy.ClientID %>')", 10);
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            setTimeout("AddOnChangeEventViewBy('<%= cboViewBy.ClientID %>')", 10);
        });


        function AddOnChangeEventViewBy(id) {
            if (null != $find(id)) {
                $find(id).add_propertyChanged(function (sender, e) {
                    if (e.get_propertyName() == 'selectedIndex') {
                        var divSpecialPricing = $get('<%= btnAddDivision.ClientID %>');
                        var selectedIndex = sender.get_selectedIndex();
                        var newValue = $get(id).getElementsByTagName('li')[selectedIndex].firstChild.data;

                        if (newValue == "Division") {

                            divSpecialPricing.style.display = 'block';
                        }
                        else {
                            divSpecialPricing.style.display = 'none';
                        }
                    }

                })
            }
        }

//        function ValidadePhoneList() {
//            var hidden = document.getElementById('<%= hfPhoneNumbers.ClientID %>');
//            var errorDiv = document.getElementById('errorDiv');
//            var errorLabel = document.getElementById('<%= lblNumberError.ClientID %>');

//            if (hidden.value == '') {
//                errorDiv.style.display = 'block';
//                errorLabel.innerHTML = 'The Phone field is a required field.';
//            }
//            else {
//                errorDiv.style.display = 'none';
//            }
//        }

        function AddPhone() {
            var hidden = document.getElementById('<%= hfPhoneNumbers.ClientID %>');
            var tbPhoneNumbers = document.getElementById("tbPhoneNumbers");

            var type = $find('<%= ddlDivisionPhoneType.ClientID %>');
            var area = document.getElementById('<%= txtDivisionPhoneArea.ClientID %>');
            var number = document.getElementById('<%= txtDivisionPhoneNumber.ClientID %>');
            var errorDiv = document.getElementById('errorDiv');
            var errorLabel = document.getElementById('<%= lblNumberError.ClientID %>');
            var showError = false;

            if (null != hidden && null != type && null != area && null != number) {
                if ("" != area.value && "" != number.value && type._hiddenFieldControl.value >= 0) {
                    var numberToInsert = '0;' + type._textBoxControl.value + ';' + area.value + '-' + number.value.substr(0, 3) + '-' + number.value.substr(3, 4)

                    if (hidden.value.indexOf(numberToInsert) == -1) {
                        if ("" != hidden.value)
                            hidden.value += '|' + numberToInsert;
                        else
                            hidden.value = numberToInsert;

                        CreatePhoneTable();
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
        }

        function RemovePhone(index) {
            var hidden = document.getElementById('<%= hfPhoneNumbers.ClientID %>');
            var tbPhoneNumbers = document.getElementById("tbPhoneNumbers");

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

            CreatePhoneTable();
        }

        //Dinamic Bind of the PhoneNumber Grid
        function CreatePhoneTable() {
            var hidden = document.getElementById('<%= hfPhoneNumbers.ClientID %>');
            if (hidden) {
                var tbPhoneNumbers = document.getElementById("tbPhoneNumbers");
                var pnlGrid = document.getElementById('<%= pnlPhoneTable.ClientID %>');
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
            }
        }

        var scriptManager = Sys.WebForms.PageRequestManager.getInstance();
        scriptManager.add_endRequest(function () {
            CreatePhoneTable();
        });

       

    </script>
</asp:Content>
