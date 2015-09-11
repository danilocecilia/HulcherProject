<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerInfo.ascx.cs"
    Inherits="Hulcher.OneSource.CustomerService.Web.UserControls.JobRecord.CustomerInfo" %>
<%@ Register Src="~/UserControls/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/AutoCompleteTextbox.ascx" TagName="AutoCompleteTextbox"
    TagPrefix="uc1" %>
<asp:UpdatePanel ID="uplCustomerInfo" UpdateMode="Conditional" runat="server">
    <ContentTemplate>
        <div class="CustomerInfoSource" style="float: left;">
            <fieldset class="CustomerInfoSource groupBox">
                <legend>
                    <asp:Label ID="lblGroupSource" runat="server" Text="Source"></asp:Label></legend>
                <div class="CustomerInfoSource title">
                    <asp:Label ID="lblCustomerContact" runat="server" Text="* Primary Contact:"></asp:Label>
                </div>
                <div class="CustomerInfoSource control">
                    <uc1:AutoCompleteTextbox ID="actCustomerContact" runat="server" ServiceMethod="GetCustomerServiceContactList"
                        GridViewButtonImageUrl="~/Images/money.png" GridViewIdName="ID" DisplayField="FullContactInformation"
                        RequiredField="true" ErrorMessage="Contact Info - The Primary Contact field is required"
                        WindowTitle="Job Record - Find Contact" ContextKey="0" AutoCompleteSource="CustomerServiceContact"
                        ColumnHeaderList="Name" ColumnValueList="FullContactInformation" ScriptToExecute="CallCustomerWebService" TextBoxWidth="250px" />
                    <asp:HiddenField ID="hidContext" runat="server"  />
                </div>
                <div class="CustomerInfoSource title">
                </div>
                <div class="CustomerInfoSource control">
                    <asp:HyperLink ID="hlAddNewContact" runat="server" NavigateUrl="javascript: var newWindow = window.open('/CustomerMaintenance.aspx?ViewType=CONTACT&NewContact=true&RefField=actCustomerContact&BillToContact=False','','width=1200, height=600, scrollbars=1, resizable=yes');"
                        Text="Add New Contact"></asp:HyperLink>
                    <%--<asp:LinkButton ID="lnkAddNewContact" Text="Add New Contact" runat="server"  OnClientClick="ignoreDirty();" CausesValidation="false" OnClick="lnkAddNewContact_Click"></asp:LinkButton>--%>
                </div>
                <div class="CustomerInfoSource title">
                    <asp:Label ID="lblPOC" runat="server" Text="Hulcher Contact:"></asp:Label>
                </div>
                <div class="CustomerInfoSource control">
                    <uc1:AutoCompleteTextbox ID="actPOC" runat="server" ServiceMethod="GetEmployeeListWithDivisionName"
                        WindowTitle="Job Record - Find Employee" GridViewButtonImageUrl="~/Images/money.png"
                        ErrorMessage="Contact Info - The Hulcher Contact field is required" GridViewIdName="ID"
                        DisplayField="CompleteName" ContextKey="0" AutoPostBack="true" RequiredField="false"
                        ValidationGroup="JobRecord" AutoCompleteSource="EmployeeWithDivision" ColumnHeaderList="Division - Name"
                        ColumnValueList="DivisionAndFullName" OnTextChanged="actPOC_TextChanged" ScriptToExecute="FocusNext" TextBoxWidth="250px" />
                </div>
                <div class="CustomerInfoSource title">
                    <asp:Label ID="lblDivision" runat="server" Text="Division:"></asp:Label>
                </div>
                <div class="CustomerInfoSource control">
                    <asp:ComboBox ID="ddlDivision" runat="server" AutoCompleteMode="SuggestAppend" RenderMode="Block"
                        CaseSensitive="false" DropDownStyle="DropDownList" AutoPostBack="true" CssClass="WindowsStyle"
                        OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" Width="250px">
                    </asp:ComboBox>
                </div>
            </fieldset>
        </div>
        <div class="CustomerInfo">
            <fieldset class="CustomerInfo groupBox">
                <legend>
                    <asp:Label ID="lblGroupCustomer" runat="server" Text="Company Info"></asp:Label></legend>
                <div class="title">
                    <asp:Label ID="lblCustomer" runat="server" Text="* Company:"></asp:Label></div>
                <div class="control">
                    <uc1:AutoCompleteTextbox ID="actCustomer" runat="server" ServiceMethod="GetCustomerList"
                        GridViewButtonImageUrl="~/Images/money.png" GridViewIdName="ID" DisplayField="Name"
                        OnTextChanged="actCustomer_TextChanged" 
                        RequiredField="true" WindowTitle="Job Record - Find Company" ValidationGroup="JobRecord"
                        ErrorMessage="Contact Info - The Company field is required" AutoCompleteSource="Customer"
                        ColumnHeaderList="Name,Attn,Company ID" ColumnValueList="Name,Attn,CustomerNumber" AutoPostBack="true"
                        ScriptToExecute="UpdateContactHyperlink" TextBoxWidth="250px" />
                    <asp:HiddenField ID="hidCollections" runat="server" Value="0" />
                    <asp:HiddenField ID="hidAlert" runat="server" Value="0" />
                </div>
                <div class="title">
                </div>
                <div class="control">
                    <asp:HyperLink ID="hlAddNewCustomer" runat="server" NavigateUrl="javascript: var newWindow = window.open('/CustomerMaintenance.aspx?ViewType=CUSTOMER&NewCustomer=true&RefField=actCustomer', '', 'width=1200, height=600, scrollbars=1, resizable=yes');"
                        Text="Add New Company"></asp:HyperLink>
                    <%--<asp:LinkButton ID="lnkNewCustomer" runat="server" Text="Add a new customer" OnClientClick="ignoreDirty();"></asp:LinkButton>--%>
                </div>
                <div class="title">
                    <asp:Label ID="lblEic" runat="server" Text="On-Site:"></asp:Label>
                </div>
                <div class="control">
                    <uc1:AutoCompleteTextbox ID="actEIC" runat="server" ServiceMethod="GetCustomerServiceContactList"
                        GridViewButtonImageUrl="~/Images/money.png" GridViewIdName="ID" DisplayField="FullContactInformation"
                        WindowTitle="Job Record - Find Contact (Secondary Contact)" ContextKey="0" AutoCompleteSource="CustomerServiceContact"
                        ColumnHeaderList="Name" ColumnValueList="FullContactInformation" ScriptToExecute="CallCustomerWebService" TextBoxWidth="250px" />
                </div>
                <div class="title">
                </div>
                <div class="control">
                    <asp:HyperLink ID="hlAddNewEIContact" Text="Add New On-Site Contact" runat="server" NavigateUrl="javascript: var newWindow = window.open('/CustomerMaintenance.aspx?ViewType=CONTACT&NewContact=true&RefField=actEIC&BillToContact=False', '', 'width=1200, height=600, scrollbars=1, resizable=yes');"></asp:HyperLink>
                </div>
                <div class="title">
                    <asp:Label ID="lblAdditionalContact" runat="server" Text="Secondary Contact:"></asp:Label>
                </div>
                <div class="control">
                    <uc1:AutoCompleteTextbox ID="actAdditionalContact" runat="server" ServiceMethod="GetCustomerServiceContactList"
                        GridViewButtonImageUrl="~/Images/money.png" GridViewIdName="ID" DisplayField="FullContactInformation"
                        WindowTitle="Job Record - Find Contact (Additional)" ContextKey="0" AutoCompleteSource="CustomerServiceContact"
                        ColumnHeaderList="Name" ColumnValueList="FullContactInformation" TextBoxWidth="250px" />
                </div>
                <div class="title">
                </div>
                <div class="control">
                    <asp:HyperLink ID="hlAddictionalContact" Text="Add New Secondary Contact" runat="server" NavigateUrl="javascript: var newWindow = window.open('/CustomerMaintenance.aspx?ViewType=CONTACT&NewContact=true&RefField=actAdditionalContact&BillToContact=False', '', 'width=1200, height=600, scrollbars=1, resizable=yes');"></asp:HyperLink>
                </div>
                <div class="title">
                    <asp:Label ID="lblBillToContact" runat="server" Text="Bill To:"></asp:Label>
                </div>
                <div class="control">
                    <uc1:AutoCompleteTextbox ID="actBillToContact" runat="server" ServiceMethod="GetDynamicsContactList"
                        GridViewButtonImageUrl="~/Images/money.png" GridViewIdName="ID" DisplayField="FullContactInformation"
                        ContextKey="0" AutoPostBack="false" AutoCompleteSource="DynamicsContact" ColumnHeaderList="Name"
                        ColumnValueList="FullContactInformation" WindowTitle="Job Record - Find Contact (Bill To)" ScriptToExecute="CallCustomerWebService" TextBoxWidth="250px" />
                </div>
                <div class="title">
                </div>
                <div class="control">
                    <asp:HyperLink ID="hlBillToContact" Text="Add New Bill-To Contact" runat="server" NavigateUrl="javascript: var newWindow = window.open('/CustomerMaintenance.aspx?ViewType=CONTACT&NewContact=True&RefField=actBillToContact&BillToContact=True','','width=1200,height=600, scrollbars=1, resizable=yes');"></asp:HyperLink>
                </div>
            </fieldset>
        </div>
        <script language="javascript" type="text/javascript" defer="defer">
            $(document).ready(function () { ValidateFields(); UpdateContactHyperlinkReady(document.getElementById('<%= actCustomer.HiddenFieldValueClientID %>').value) });

            function ValidateFields() {
                var primaryContact = document.getElementById('<%= actCustomerContact.HiddenFieldValueClientID %>');
                
                if ("0" != primaryContact.value) {
                    ValidatorEnable($get('<%= actCustomerContact.RequiredFieldClientId %>'), false);
                }
                else {
                    ValidatorEnable($get('<%= actCustomerContact.RequiredFieldClientId %>'), true);
                }

                /*var primaryContact = document.getElementById('<%= actCustomerContact.TextControlClientID %>');
                var hulcherContact = document.getElementById('<%= actPOC.TextControlClientID %>');

                if ("" != hulcherContact.value) {
                    ValidatorEnable($get('<%= actCustomerContact.RequiredFieldClientId %>'), false);
                }
                else if ("" != primaryContact.value) {
                    ValidatorEnable($get('<%= actPOC.RequiredFieldClientId %>'), false);
                }
                else {
                    ValidatorEnable($get('<%= actCustomerContact.RequiredFieldClientId %>'), true);
                    ValidatorEnable($get('<%= actPOC.RequiredFieldClientId %>'), true);
                }*/
            }

            function UpdateContactHyperlinkReady(customerId) {
                var actCustomer = document.getElementById('<%= actCustomer.HiddenFieldValueClientID %>');
                var hlAddNewContact = document.getElementById('<%= hlAddNewContact.ClientID %>');
                var hlAddNewEIContact = document.getElementById('<%= hlAddNewEIContact.ClientID %>');
                //var hlAddictionalContact = document.getElementById('<%= hlAddictionalContact.ClientID %>');
                var hlBillToContact = document.getElementById('<%= hlBillToContact.ClientID %>');

                if (customerId != 0) {
                    hlAddNewContact.href = "javascript: var newWindow = window.open('/CustomerMaintenance.aspx?ViewType=CONTACT&CustomerID=" + actCustomer.value + "&NewContact=True&RefField=actCustomerContact&BillToContact=False','','width=1200,height=600, scrollbars=1, resizable=yes');";
                    hlAddNewEIContact.href = "javascript: var newWindow = window.open('/CustomerMaintenance.aspx?ViewType=CONTACT&CustomerID=" + actCustomer.value + "&NewContact=True&RefField=actEIC&BillToContact=False','','width=1200,height=600, scrollbars=1, resizable=yes');";
                    //hlAddictionalContact.href = "javascript: var newWindow = window.open('/CustomerMaintenance.aspx?ViewType=CONTACT&CustomerID=" + actCustomer.value + "&NewContact=True&RefField=actAdditionalContact&BillToContact=False','','width=1200,height=600, scrollbars=1, resizable=yes');";
                    hlBillToContact.href = "javascript: var newWindow = window.open('/CustomerMaintenance.aspx?ViewType=CONTACT&CustomerID=" + actCustomer.value + "&NewContact=True&RefField=actBillToContact&BillToContact=True','','width=1200,height=600, scrollbars=1, resizable=yes');";
                }
                else {
                    hlAddNewContact.href = "javascript: var newWindow = window.open('/CustomerMaintenance.aspx?ViewType=CONTACT&NewContact=True&RefField=actCustomerContact&BillToContact=False','','width=1200,height=600, scrollbars=1, resizable=yes');";
                    hlAddNewEIContact.href = "javascript: var newWindow = window.open('/CustomerMaintenance.aspx?ViewType=CONTACT&NewContact=True&RefField=actEIC&BillToContact=False','','width=1200,height=600, scrollbars=1, resizable=yes');";
                    //hlAddictionalContact.href = "javascript: var newWindow = window.open('/CustomerMaintenance.aspx?ViewType=CONTACT&NewContact=True&RefField=actAdditionalContact&BillToContact=False','','width=1200,height=600, scrollbars=1, resizable=yes');";
                    hlBillToContact.href = "javascript: var newWindow = window.open('/CustomerMaintenance.aspx?ViewType=CONTACT&NewContact=True&RefField=actBillToContact&BillToContact=True','','width=1200,height=600, scrollbars=1, resizable=yes');";
                }
            }

            function FocusNext(customerId) {
                // $('#ContentPlaceHolder1_Content_chCustomerInfo_uscCustomer_actCustomer_actCustomer_text').focus();
            }

            function UpdateContactHyperlink(customerId) {
                UpdateContactHyperlinkReady(customerId);
                CustomerIsCollection(customerId);
                CustomerHasOperatorAlert(customerId);

                //$('#ContentPlaceHolder1_Content_chJobInfo_uscJobInfo_dpDatePicker_txtDatePicker').focus();
            }

            function CustomerHasOperatorAlert(customerId) {

                if (customerId != 0) {
                    tempuri.org.IJSONService.CustomerHasOperatorAlert(customerId, CustomerHasOperatorAlertCompleted);
                }
            }

            function CustomerHasOperatorAlertCompleted(result) {
                var operatorAlert = document.getElementById('<%= hidAlert.ClientID %>');
                var selectedValue = document.getElementById('<%= actCustomer.HiddenFieldValueClientID %>').value

                if (operatorAlert != null && operatorAlert != null && result.AlertMessage != '' && selectedValue != operatorAlert.value) {
                    operatorAlert.value = selectedValue;
                    alert(result.AlertMessage);
                }
                else if (operatorAlert != null && (result == null || result.AlertMessage == '')) {
                    operatorAlert.value = selectedValue;
                }
            }

            function CustomerIsCollection(customerId) {
                    
                if (customerId != 0) {
                    tempuri.org.IJSONService.IsCustomerCollection(customerId, CustomerIsCollectionCompleted);
                }
            }

            function CustomerIsCollectionCompleted(result) {
                var collections = document.getElementById('<%= hidCollections.ClientID %>');
                var selectedValue = document.getElementById('<%= actCustomer.HiddenFieldValueClientID %>').value

                if (collections != null && result != null && result.Collection && selectedValue != collections.value) {
                    collections.value = selectedValue;
                    alert("Company account is in collections");
                }
                else if (collections != null && (result == null || !result.Collection)) {
                    collections.value = selectedValue;
                }
            }

            function SetAutocompleteField(Name, ID, Field) {
                var actField = $find(Field);
                actField.raiseItemSelected(new Sys.Extended.UI.AutoCompleteItemEventArgs(null, Name, ID));
            }

            function CallCustomerWebService(contactId) {
                if (contactId != 0) {
                    if (document.getElementById('<%=actCustomer.HiddenFieldValueClientID %>').value == '0')
                        tempuri.org.IJSONService.GetCustomer(contactId, CallCustomerWebServiceCompleted);
                }
            }

            function CallCustomerWebServiceCompleted(WebServiceResult) {
                var customerControl = $find('actCustomer');
                var hidContext = document.getElementById('<%= hidContext.ClientID %>');

                if (hidContext.value != WebServiceResult.Id) {
                    hidContext.value = WebServiceResult.Id;
                    customerControl.raiseItemSelected(new Sys.Extended.UI.AutoCompleteItemEventArgs(null, WebServiceResult.Name, WebServiceResult.Id));
                }
            }

        </script>
    </ContentTemplate>
</asp:UpdatePanel>
