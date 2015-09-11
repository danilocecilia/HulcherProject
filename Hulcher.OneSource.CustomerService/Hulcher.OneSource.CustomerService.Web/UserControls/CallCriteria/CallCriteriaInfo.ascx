<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CallCriteriaInfo.ascx.cs"
    Inherits="Hulcher.OneSource.CustomerService.Web.UserControls.CallCriteria.CallCriteriaInfo" %>
<%@ Register Src="~/UserControls/AutoCompleteTextbox.ascx" TagName="AutoCompleteTextbox"
    TagPrefix="uc1" %>
<%@ Register Assembly="Hulcher.OneSource.CustomerService.Business" Namespace="Hulcher.OneSource.CustomerService.Business.WebControls.ServerControls"
    TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagName="CollapseHolder" TagPrefix="uc" Src="~/UserControls/CollapseHolder.ascx" %>
<link href="../../Styles/jquery.multiselect.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/CallCriteria.css" rel="stylesheet" type="text/css" />
<asp:UpdatePanel ID="updCallCriteriaInfo" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <uc:CollapseHolder ID="chCCJobAtributes" runat="server" GridViewCssClass="ScrollableGridView">
            <Header>
                <asp:Label ID="lblJobAttributes" runat="server" Text="Call Criteria - Job Attributes/Call Log" />
            </Header>
            <Content>
                <div class="width100 marginBottom5">
                    <asp:Label runat="server" ID="lblCallCriteriaHeader" Text="Select your top level criteria condition:"></asp:Label>
                </div>
                <asp:Panel ID="pnlGroupCriteria" runat="server">
                    <div class="width100 marginBottom5">
                        <div class="radioHeader">
                            <asp:RadioButton ID="rbDivision" runat="server" GroupName="radioHeader" Text="Division Level" />
                        </div>
                        <div class="autocompleteHeader" id="divisionAC">
                            <uc1:AutoCompleteTextbox ID="actDivisionLevel" runat="server" ServiceMethod="GetDivisionList"
                                Enabled="false" GridViewButtonImageUrl="~/Images/money.png" GridViewIdName="ID"
                                DisplayField="Division" RequiredField="false" ErrorMessage="Call Criteria - The Division Level field is required"
                                WindowTitle="Call Criteria - Find Division" AutoCompleteSource="Division" ColumnHeaderList="Name,Description"
                                ColumnValueList="Name,Description" HasSearchButton="false" AutoPostBack="true"
                                OnTextChanged="txtDivisionCustomerWideLevel_TextChanged" ValidationGroup="CallCriteria" />
                        </div>
                    </div>
                    <div class="width100 marginBottom5">
                        <div class="radioHeader">
                            <asp:RadioButton ID="rbCustomer" runat="server" GroupName="radioHeader" Text="Company Level" />
                        </div>
                        <div class="autocompleteHeader" id="customerAC">
                            <uc1:AutoCompleteTextbox ID="actCustomerLevel" runat="server" ServiceMethod="GetCustomerList"
                                Enabled="false" GridViewButtonImageUrl="~/Images/money.png" GridViewIdName="ID"
                                DisplayField="Name" RequiredField="false" ErrorMessage="Call Criteria - The Company Level field is required"
                                WindowTitle="Call Criteria - Find Company" AutoCompleteSource="Customer" ColumnHeaderList="Name,Attn,Company ID"
                                ColumnValueList="Name,Attn,CustomerNumber" HasSearchButton="false" AutoPostBack="true"
                                OnTextChanged="txtDivisionCustomerWideLevel_TextChanged" ValidationGroup="CallCriteria" />
                        </div>
                    </div>
                    <div class="width100 marginBottom5">
                        <div class="radioHeader">
                            <asp:RadioButton ID="rbWide" runat="server" GroupName="radioHeader" Text="System Wide Level" />
                        </div>
                        <div class="autocompleteHeader" id="swlAC">
                            <asp:TextBox ID="txtSystemWideLevel" runat="server" CssClass="input" AutoPostBack="true"
                                OnTextChanged="txtDivisionCustomerWideLevel_TextChanged"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSystemWide" runat="server" EnableClientScript="true"
                                Text="*" Display="Dynamic" ErrorMessage="Call Criteria - The System Wide Level field is required"
                                ControlToValidate="txtSystemWideLevel" ValidationGroup="CallCriteria"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </asp:Panel>
                <br />
                <asp:Panel ID="pnlCallCriteria" runat="server" Visible="false">
                    <div class="width100 inlineBlock">
                        <uc:CollapseHolder ID="panelJobAttributes" runat="server" GridViewCssClass="ScrollableGridView">
                            <Header>
                                Job Attributes</Header>
                            <Content>
                                <div class="width100 inlineBlock">
                                    <asp:CheckBox ClientIDMode="Static" runat="server" ID="chkAllJobs" onclick="toggleJobAttr();CreateGrid();"
                                        Text="Select All Jobs" />
                                </div>
                                <br />
                                <div class="width100 inlineBlock">
                                    <div class="subTitles">
                                        <%--<div class="column20SubTitles rightLineTitle">
                                Contract Info Attributes</div>--%>
                                        <div class="column20SubTitles rightLineTitle">
                                            Job Info Attributes</div>
                                        <div class="column20SubTitles rightLineTitle">
                                            Job Location Attributes</div>
                                        <div class="column20SubTitles rightLineTitle">
                                            Job Description Attributes</div>
                                        <div class="column18SubTitles">
                                            Resource Attributes</div>
                                    </div>
                                </div>
                                <div class="width100 inlineBlock">
                                    <div class="fields">
                                        <div class="column20Fields rightLine" id="JobFields">
                                            <asp:Panel ID="pnlCustomer" runat="server" Visible="false">
                                                <div class="callCriteriaField">
                                                    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" CssClass="radioAndOr"
                                                        RepeatLayout="Flow" ID="rblCustomer" Enabled="false">
                                                        <asp:ListItem Text="And" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Or" Value="0" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <br />
                                                    <asp:CheckBox ClientIDMode="Static" runat="server" CssClass="jobAttr" ID="chkCustomer"
                                                        onclick="toogleField(this.id, 'customerFields');CreateGrid();" Text="Company:" />
                                                    <div class="customerFields">
                                                        <uc1:AutoCompleteTextbox ID="actCustomer" runat="server" ServiceMethod="GetCustomerList"
                                                            GridViewButtonImageUrl="~/Images/money.png" GridViewIdName="ID" DisplayField="Name"
                                                            RequiredField="false" ValidationGroup="CallEntry" ErrorMessage="The Company field is required"
                                                            WindowTitle="Call Entry - Find Company" AutoCompleteSource="Customer" ColumnHeaderList="Name,Attn,Company ID"
                                                            ColumnValueList="Name,Attn,CustomerNumber" ScriptToExecute="AddCustomer" HasSearchButton="false" />
                                                        <div class="autoCompleteSelectedList">
                                                            <asp:ListBox ID="lstSelectedCustomers" runat="server" Style="width: 100%;"></asp:ListBox>
                                                        </div>
                                                        <div class="autoCompleteRemove">
                                                            <asp:Button ID="btnRemove" runat="server" Text="Remove" CssClass="btn" CausesValidation="false"
                                                                OnClientClick="RemoveItem(SELECTED_CUSTOMERS,VALUES_CUSTOMER);CreateGrid(CUSTOMER);return false;" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlDivision" runat="server" Visible="false">
                                                <div class="callCriteriaField">
                                                    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" CssClass="radioAndOr"
                                                        RepeatLayout="Flow" ID="rblDivision" Enabled="false">
                                                        <asp:ListItem Text="And" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Or" Value="0" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <br />
                                                    <asp:CheckBox ID="chkDivision" runat="server" CssClass="jobAttr" ClientIDMode="Static"
                                                        onclick="toogleField(this.id, 'ddlDivision');CreateGrid();" Text="Division:" />
                                                    <asp:MultiSelectDropDownList ID="ddlDivision" runat="server" SelectionMode="Multiple"
                                                        CssClass="multiselectdropdown" ClientIDMode="Static" OnClientClick="CreateGrid();">
                                                    </asp:MultiSelectDropDownList>
                                                </div>
                                            </asp:Panel>
                                            <div class="callCriteriaField">
                                                <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" CssClass="radioAndOr"
                                                    RepeatLayout="Flow" ID="rblJobStatus" Enabled="false">
                                                    <asp:ListItem Text="And" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Or" Value="0" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                                <br />
                                                <asp:CheckBox ClientIDMode="Static" runat="server" CssClass="jobAttr" ID="chkJobStatus"
                                                    Text="Job Status:" onclick="toogleField(this.id, 'ddlJobStatus');CreateGrid();" />
                                                <asp:MultiSelectDropDownList ID="ddlJobStatus" runat="server" SelectionMode="Multiple"
                                                    CssClass="multiselectdropdown" ClientIDMode="Static" OnClientClick="CreateGrid();">
                                                </asp:MultiSelectDropDownList>
                                            </div>
                                            <div class="callCriteriaField">
                                                <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" CssClass="radioAndOr"
                                                    RepeatLayout="Flow" ID="rblPriceType" Enabled="false">
                                                    <asp:ListItem Text="And" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Or" Value="0" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                                <br />
                                                <asp:CheckBox ClientIDMode="Static" runat="server" CssClass="jobAttr" ID="chkPriceType"
                                                    Text="Price Type:" onclick="toogleField(this.id, 'ddlPriceType');CreateGrid();" />
                                                <asp:MultiSelectDropDownList ID="ddlPriceType" runat="server" SelectionMode="Multiple"
                                                    CssClass="multiselectdropdown" ClientIDMode="Static" OnClientClick="CreateGrid();">
                                                </asp:MultiSelectDropDownList>
                                            </div>
                                            <div class="callCriteriaField">
                                                <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" CssClass="radioAndOr"
                                                    RepeatLayout="Flow" ID="rblJobCategory" Enabled="false">
                                                    <asp:ListItem Text="And" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Or" Value="0" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                                <br />
                                                <asp:CheckBox ClientIDMode="Static" runat="server" CssClass="jobAttr" ID="chkJobCategory"
                                                    Text="Job Category:" onclick="toogleField(this.id, 'ddlJobCategory');CreateGrid();" />
                                                <asp:MultiSelectDropDownList ID="ddlJobCategory" runat="server" SelectionMode="Multiple"
                                                    CssClass="multiselectdropdown" ClientIDMode="Static" OnClientClick="CreateGrid();">
                                                </asp:MultiSelectDropDownList>
                                            </div>
                                            <div class="callCriteriaField">
                                                <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" CssClass="radioAndOr"
                                                    RepeatLayout="Flow" ID="rblJobType" Enabled="false">
                                                    <asp:ListItem Text="And" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Or" Value="0" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                                <br />
                                                <asp:CheckBox ClientIDMode="Static" runat="server" CssClass="jobAttr" ID="chkJobType"
                                                    Text="Job Type:" onclick="toogleField(this.id, 'ddlJobType');CreateGrid();" />
                                                <asp:MultiSelectDropDownList ID="ddlJobType" runat="server" SelectionMode="Multiple"
                                                    CssClass="multiselectdropdown" ClientIDMode="Static" OnClientClick="CreateGrid();">
                                                </asp:MultiSelectDropDownList>
                                            </div>
                                            <div class="callCriteriaField">
                                                <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" CssClass="radioAndOr"
                                                    RepeatLayout="Flow" ID="rblJobAction" Enabled="false">
                                                    <asp:ListItem Text="And" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Or" Value="0" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                                <br />
                                                <asp:CheckBox ClientIDMode="Static" runat="server" CssClass="jobAttr" ID="chkJobAction"
                                                    Text="Job Action:" onclick="toogleField(this.id, 'ddlJobAction');CreateGrid();" />
                                                <asp:MultiSelectDropDownList ID="ddlJobAction" runat="server" SelectionMode="Multiple"
                                                    CssClass="multiselectdropdown" ClientIDMode="Static" OnClientClick="CreateGrid();">
                                                </asp:MultiSelectDropDownList>
                                            </div>
                                            <div class="callCriteriaField">
                                                <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" CssClass="radioAndOr"
                                                    RepeatLayout="Flow" ID="rblInterimBilling" Enabled="false">
                                                    <asp:ListItem Text="And" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Or" Value="0" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                                <br />
                                                <asp:CheckBox ClientIDMode="Static" runat="server" CssClass="jobAttr" ID="chkInterimbilling"
                                                    Text="Interim billing:" onclick="toogleField(this.id, 'ddlInterimbilling');CreateGrid();" />
                                                <asp:MultiSelectDropDownList ID="ddlInterimbilling" runat="server" SelectionMode="Multiple"
                                                    CssClass="multiselectdropdown" ClientIDMode="Static" OnClientClick="CreateGrid();">
                                                </asp:MultiSelectDropDownList>
                                            </div>
                                            <div class="callCriteriaField">
                                                <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" CssClass="radioAndOr"
                                                    RepeatLayout="Flow" ID="rblGeneralLog" Enabled="false">
                                                    <asp:ListItem Text="And" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Or" Value="0" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                                <br />
                                                <asp:CheckBox ClientIDMode="Static" runat="server" CssClass="jobAttr" ID="chkGeneralLog"
                                                    Text="General Log" onclick="CreateGrid(); toogleField(this.id, '')" />
                                            </div>
                                        </div>
                                        <div class="column20Fields rightLine">
                                            <div class="callCriteriaField">
                                                <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" CssClass="radioAndOr"
                                                    RepeatLayout="Flow" ID="rblCountry" Enabled="false">
                                                    <asp:ListItem Text="And" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Or" Value="0" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                                <br />
                                                <asp:CheckBox ClientIDMode="Static" runat="server" CssClass="jobAttr" ID="chkCountry"
                                                    Text="Country:" onclick="toogleField(this.id, 'ddlCountry');CreateGrid();" />
                                                <asp:MultiSelectDropDownList ID="ddlCountry" runat="server" SelectionMode="Multiple"
                                                    CssClass="multiselectdropdown" ClientIDMode="Static" AutoPostBack="true" OnClientClick="CreateGrid();">
                                                </asp:MultiSelectDropDownList>
                                            </div>
                                            <div class="callCriteriaField">
                                                <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" CssClass="radioAndOr"
                                                    RepeatLayout="Flow" ID="rblState" Enabled="false">
                                                    <asp:ListItem Text="And" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Or" Value="0" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                                <br />
                                                <asp:CheckBox ClientIDMode="Static" runat="server" ID="chkState" CssClass="jobAttr"
                                                    Text="State:" onclick="toogleField(this.id, 'stateFields');CreateGrid();" />
                                                <div class="stateFields">
                                                    <uc1:AutoCompleteTextbox ID="acState" runat="server" ServiceMethod="GetStateList"
                                                        GridViewButtonImageUrl="~/Images/money.png" GridViewIdName="ID" DisplayField="Name"
                                                        RequiredField="false" WindowTitle="State Field" MinimumPrefixLength="2" AutoCompleteSource="State"
                                                        ColumnHeaderList="Name" ColumnValueList="Name" ScriptToExecute="AddState" />
                                                    <div class="autoCompleteSelectedList">
                                                        <asp:ListBox ID="lstBoxSelectedState" runat="server" Style="width: 100%;"></asp:ListBox>
                                                    </div>
                                                    <div class="autoCompleteRemove">
                                                        <asp:Button ID="btnRemoveState" runat="server" Text="Remove" CssClass="btn" CausesValidation="false"
                                                            OnClientClick="RemoveItem(SELECTED_STATE,VALUES_STATE);return false;" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="callCriteriaField">
                                                <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" CssClass="radioAndOr"
                                                    RepeatLayout="Flow" ID="rblCity" Enabled="false">
                                                    <asp:ListItem Text="And" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Or" Value="0" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                                <br />
                                                <asp:CheckBox ClientIDMode="Static" runat="server" CssClass="jobAttr" ID="chkCity"
                                                    Text="City:" onclick="toogleField(this.id, 'cityFields');CreateGrid();" />
                                                <div class="cityFields">
                                                    <uc1:AutoCompleteTextbox ID="acCity" runat="server" ServiceMethod="GetCityList" GridViewButtonImageUrl="~/Images/money.png"
                                                        GridViewIdName="ID" DisplayField="Name" RequiredField="false" WindowTitle="State Field"
                                                        AutoCompleteSource="City" ColumnHeaderList="Name" ColumnValueList="Name" ScriptToExecute="AddCity"
                                                        HasSearchButton="false" />
                                                    <div class="autoCompleteSelectedList">
                                                        <asp:ListBox ID="lstBoxSelectedCity" runat="server" Style="width: 100%;"></asp:ListBox>
                                                    </div>
                                                    <div class="autoCompleteRemove">
                                                        <asp:Button ID="btnRemoveCity" runat="server" Text="Remove" CssClass="btn" CausesValidation="false"
                                                            OnClientClick="RemoveItem(SELECTED_CITY,VALUES_CITY);return false;" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="column20Fields rightLine">
                                            <div class="callCriteriaField">
                                                <div class="inlineBlock width100">
                                                    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" CssClass="radioAndOr"
                                                        RepeatLayout="Flow" ID="rblCarCount" Enabled="false">
                                                        <asp:ListItem Text="And" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Or" Value="0" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div style="display: inline-block; float: left; margin-right: 2px;">
                                                    <asp:CheckBox ClientIDMode="Static" runat="server" CssClass="jobAttr" ID="chkCarCount"
                                                        Text="Car Count:" onclick="toogleField(this.id, 'txtCarCount');CreateGrid();" />
                                                </div>
                                                <div style="display: inline-block; float: left; padding-top: 2px; padding-bottom: 2px;">
                                                    <asp:DropDownList ID="ddlCarCount" runat="server" CssClass="Dropdownlist" Width="95px"
                                                        disabled="disabled" onchange="CreateGrid();">
                                                        <asp:ListItem Text="Equal To" Value="" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Greater Than" Value=">"></asp:ListItem>
                                                        <asp:ListItem Text="Less Than" Value="<"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div style="display: inline-block; float: left; padding-left: 3px; padding-top: 2px;
                                                    padding-bottom: 2px;">
                                                    <asp:TextBox ID="txtCarCount" runat="server" Width="40px" CssClass="input" onchange="CreateGrid();"
                                                        disabled="disabled"></asp:TextBox>
                                                </div>
                                                <asp:FilteredTextBoxExtender ID="fteCarCount" runat="server" TargetControlID="txtCarCount"
                                                    FilterType="Custom, Numbers" />
                                            </div>
                                            <div class="callCriteriaField" style="vertical-align: bottom">
                                                <br />
                                                <br />
                                                <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" CssClass="radioAndOr"
                                                    RepeatLayout="Flow" ID="rblCommodities" Enabled="false">
                                                    <asp:ListItem Text="And" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Or" Value="0" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                                <br />
                                                <asp:CheckBox ClientIDMode="Static" runat="server" CssClass="jobAttr" ID="chkCommodities"
                                                    Text="Commodities:" onclick="toogleField(this.id, 'ddlCommodities');CreateGrid();" />
                                                <asp:MultiSelectDropDownList ID="ddlCommodities" runat="server" SelectionMode="Multiple"
                                                    CssClass="multiselectdropdown" ClientIDMode="Static" OnClientClick="CreateGrid();">
                                                    <asp:ListItem Text="Lading" Value="1"></asp:ListItem>
                                                </asp:MultiSelectDropDownList>
                                            </div>
                                            <div class="callCriteriaField">
                                                <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" CssClass="radioAndOr"
                                                    RepeatLayout="Flow" ID="rblChemicals" Enabled="false">
                                                    <asp:ListItem Text="And" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Or" Value="0" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                                <br />
                                                <asp:CheckBox ClientIDMode="Static" runat="server" CssClass="jobAttr" ID="chkChemicals"
                                                    Text="Chemicals:" onclick="toogleField(this.id, 'ddlChemicals');CreateGrid();" />
                                                <asp:MultiSelectDropDownList ID="ddlChemicals" runat="server" SelectionMode="Multiple"
                                                    CssClass="multiselectdropdown" ClientIDMode="Static" OnClientClick="CreateGrid();">
                                                    <asp:ListItem Text="UN#" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="STTCC Info" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Hazmat" Value="3"></asp:ListItem>
                                                </asp:MultiSelectDropDownList>
                                            </div>
                                        </div>
                                        <div class="column18Fields">
                                            <div class="callCriteriaField">
                                                <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" CssClass="radioAndOr"
                                                    RepeatLayout="Flow" ID="rblHeavyEquipment" Enabled="false">
                                                    <asp:ListItem Text="And" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Or" Value="0" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                                <br />
                                                <asp:CheckBox ClientIDMode="Static" runat="server" CssClass="jobAttr" ID="chkHeavyEquipment"
                                                    Text="Heavy Equipment - Flag:" onclick="toogleField(this.id, '');CreateGrid();" />
                                            </div>
                                            <div class="callCriteriaField">
                                                <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" CssClass="radioAndOr"
                                                    RepeatLayout="Flow" ID="rblNonHeavyEquipment" Enabled="false">
                                                    <asp:ListItem Text="And" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Or" Value="0" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                                <br />
                                                <asp:CheckBox ClientIDMode="Static" runat="server" CssClass="jobAttr" ID="chkNonHeavyEquipment"
                                                    Text="Non-Heavy Equipment - Flag:" onclick="toogleField(this.id, '');CreateGrid();" />
                                            </div>
                                            <div class="callCriteriaField">
                                                <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" CssClass="radioAndOr"
                                                    RepeatLayout="Flow" ID="rblAllEquipment" Enabled="false">
                                                    <asp:ListItem Text="And" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Or" Value="0" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                                <br />
                                                <asp:CheckBox ClientIDMode="Static" runat="server" CssClass="jobAttr" ID="chkAllEquipment"
                                                    Text="All Equipment:" onclick="toogleField(this.id, '');CreateGrid();" />
                                            </div>
                                            <div class="callCriteriaField">
                                                <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" CssClass="radioAndOr"
                                                    RepeatLayout="Flow" ID="rblWhiteLight" Enabled="false">
                                                    <asp:ListItem Text="And" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Or" Value="0" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                                <br />
                                                <asp:CheckBox ClientIDMode="Static" runat="server" CssClass="jobAttr" ID="chkWhiteLight"
                                                    Text="White Light:" onclick="toogleField(this.id, '');CreateGrid();" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="width100 inlineBlock">
                                    <asp:Panel ID="pnlNoRows" runat="server">
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
                                    <asp:Panel ID="pnlGrid" runat="server" Style="display: none;">
                                        <div id="tbCriterias_Group" class="ScrollableGridView_Group" style="width: 100%">
                                            <div id="tbCriterias_HeaderDiv" class="ScrollableGridView_HeaderDiv" style="min-width: 400px;">
                                            </div>
                                            <div id="tbCriterias_ScrollDiv" class="ScrollableGridView_ScrollDiv" style="max-height: 500px;
                                                min-width: 400px;">
                                                <table id="tbCriterias" class="ScrollableGridView" cellspacing="1">
                                                    <thead>
                                                        <tr style="position: relative; top: expression(this.offsetParent.scrollTop -1); left: expression(this.offsetParent.style.left);">
                                                            <th id="thCriteria" runat="server" class="header">
                                                                <asp:Label ID="rpt1Header1" CssClass="MarginRight" runat="server" Text="Criteria"></asp:Label>
                                                            </th>
                                                            <th id="thSelected" runat="server" class="header">
                                                                <asp:Label ID="rpt1Header2" CssClass="MarginRight" runat="server" Text="Selected"></asp:Label>
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
                            </Content>
                        </uc:CollapseHolder>
                    </div>
                    <br />
                    <uc:CollapseHolder ID="chCCJobCallLogConditions" runat="server" GridViewCssClass="ScrollableGridView">
                        <Header>
                            <asp:Label ID="lblJobCallLogConditions" runat="server" Text="Job Call Log Conditions" />
                        </Header>
                        <Content>
                            <div class="width100 inlineBlock">
                                <asp:CheckBox ClientIDMode="Static" runat="server" ID="chkAllCallTypes" onclick="toggleAllCallType();"
                                    Text="Select All Call Types" />
                            </div>
                            <div class="width100 inlineBlock">
                                <div class="wrapper" id="Wrapper">
                                    <asp:Repeater ID="rptPrimaryCallTypes" runat="server" OnItemDataBound="rptPrimaryCallTypes_ItemDataBound">
                                        <ItemTemplate>
                                            <div class="column20CallTypes rightLineCallType callType" id="divColumn" runat="server">
                                                <div class="callTypesSubTitles">
                                                    <asp:Label ID="lblPrimaryCallType" runat="server" />
                                                </div>
                                                <div id="dvSelectAll" runat="server">
                                                    <asp:CheckBox ID="chkAddAll" runat="server" />
                                                </div>
                                                <div class="callTypeList">
                                                    <asp:CheckBoxList ID="chkCallTypes" runat="server" />
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </Content>
                    </uc:CollapseHolder>
                    <br />
                    <uc:CollapseHolder ID="CollapseHolder1" runat="server" GridViewCssClass="ScrollableGridView">
                        <Header>
                            In person advisement notes</Header>
                        <Content>
                            <div style="vertical-align: top; margin-bottom: 10px;">
                                <asp:Label ID="lblNote" runat="server" Text="Notes:" Width="4%" CssClass="inline"
                                    Style="vertical-align: top; left: auto;"></asp:Label>
                                <asp:CountableTextBox ID="txtNotes" runat="server" TextMode="MultiLine" Width="90%"
                                    Height="150px" CssClass="input" MaxChars="255" MaxCharsWarning="255" MaxLength="255"></asp:CountableTextBox>
                            </div>
                        </Content>
                    </uc:CollapseHolder>
                    <br />
                    <br />
                    <div style="text-align: right">
                        <asp:Button runat="server" ID="btnAddCallCriteria" Text="Save" CssClass="btn" OnClick="btnAddCallCriteria_Click"
                            CausesValidation="true" ValidationGroup="CallCriteria" />
                        <asp:Button runat="server" ID="btnCancel" CssClass="btn" Text="Cancel" OnClick="btnCancel_Click" />
                    </div>
                </asp:Panel>
            </Content>
        </uc:CollapseHolder>
        <br />
        <uc:CollapseHolder ID="chCCListing" runat="server" GridViewCssClass="ScrollableGridView">
            <Header>
                <asp:Label ID="lblCallCriteriaListing" runat="server" Text="Call Criteria Listing" />
            </Header>
            <Content>
                <div id="divGrid">
                    <asp:HiddenField ID="hfExpandedCallCriterias" runat="server" />
                    <asp:HiddenField ID="hfExpandedJobAttributes" runat="server" />
                    <asp:HiddenField ID="hfExpandedJobCallLogConditions" runat="server" />
                    <asp:Panel ID="pnlRowsCallCriteriaListing" runat="server">
                        <asp:Repeater ID="rptCallCriteriaListing" runat="server" OnItemDataBound="rptCallCriteriaListing_ItemDataBound"
                            OnItemCommand="rptCallCriteriaListing_ItemCommand">
                            <HeaderTemplate>
                                <div id="tbRepeaters_Group" class="ScrollableGridView_Group" style="width: 100%">
                                    <div id="tbRepeaters_HeaderDiv" class="ScrollableGridView_HeaderDiv" style="min-width: 400px;">
                                    </div>
                                    <div id="tbRepeaters_ScrollDiv" class="ScrollableGridView_ScrollDiv" style="max-height: 450px;
                                        min-width: 400px;">
                                        <table id="tbRepeaters" class="ScrollableGridView" cellspacing="1">
                                            <thead>
                                                <tr style="position: relative; top: expression(this.offsetParent.scrollTop -1); left: expression(this.offsetParent.style.left);">
                                                    <th id="thExpandCollapse" runat="server" class="header" style="border: 1px solid #E6EEEE;
                                                        width: 15%;">
                                                        <asp:Label ID="lblCriteriaHeader" runat="server" CssClass="MarginRight" Text="Criteria" />
                                                    </th>
                                                    <th id="thRegion" runat="server" class="header" colspan="2">
                                                        <asp:Label ID="lblSelectedHeader" CssClass="MarginRight" runat="server" Text="Selected" />
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="even">
                                    <td style="text-align: center; width: 15%;">
                                        <div class="Expand" id="divExpand" runat="server">
                                        </div>
                                    </td>
                                    <td style="width: 70%">
                                        <asp:Label ID="lblSelectedCriteria" runat="server"></asp:Label>
                                        <asp:HiddenField ID="hfCriteriaID" runat="server" />
                                    </td>
                                    <td style="text-align: center; width: 15%;">
                                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="EditCriteria" CausesValidation="false" 
                                                CssClass="linkButtonStyle" />&nbsp;
                                        <asp:Button ID="btnRemove" runat="server" Text="Remove" CommandName="RemoveCriteria"
                                            CausesValidation="false" CssClass="linkButtonStyle" OnClientClick="return confirm('Are you sure you want to remove the selected Call Criteria?');" />
                                    </td>
                                </tr>
                                <tr id="trJobAttributesHeader" runat="server" style="display: none;">
                                    <td style="text-align: center; width: 15%;">
                                        <div class="Expand" id="divExpandJobAttributes" visible="false" runat="server">
                                        </div>
                                    </td>
                                    <td colspan="2" style="width: 85%">
                                        <asp:Label ID="lblJobAttributesHeader" runat="server" Text="Call Criteria - Job Attributes" />
                                    </td>
                                </tr>
                                <asp:Repeater ID="rptJobAttributes" runat="server" OnItemDataBound="rptJobAttributes_ItemDataBound">
                                    <ItemTemplate>
                                        <tr id="trJobAttributes" runat="server" style="display: none;">
                                            <td style="width: 15%">
                                                <asp:Label ID="lblCriteria" runat="server" />
                                            </td>
                                            <td colspan="2" style="width: 85%">
                                                <asp:Label ID="lblSelected" runat="server" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr id="trJobCallLogConditionsHeader" runat="server" style="display: none;">
                                    <td style="text-align: center; width: 15%;">
                                        <div class="Expand" id="divExpandJobCallLogConditions" visible="false" runat="server">
                                        </div>
                                    </td>
                                    <td colspan="2" style="width: 85%">
                                        <asp:Label ID="lblJobCallLogConditions" runat="server" Text="Call Criteria - Job Call Log Conditions" />
                                    </td>
                                </tr>
                                <asp:Repeater ID="rptJobCallLogConditions" runat="server" OnItemDataBound="rptJobCallLogConditions_ItemDataBound">
                                    <ItemTemplate>
                                        <tr id="trJobCallLogConditions" runat="server" style="display: none;">
                                            <td style="width: 15%">
                                                <asp:Label ID="lblCriteria" runat="server" />
                                            </td>
                                            <td colspan="2" style="width: 85%">
                                                <asp:Label ID="lblSelected" runat="server" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr id="trAdvisement" runat="server" style="display: none;">
                                    <td style="width: 15%">
                                        <asp:Label ID="lblNoteTitle" runat="server" Text="Note" />
                                    </td>
                                    <td colspan="2" style="width: 85%">
                                        <asp:Label ID="lblNote" runat="server" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody> </table> </div> </div>
                            </FooterTemplate>
                        </asp:Repeater>
                    </asp:Panel>
                    <asp:Panel ID="pnlNoRowsCallCriteriaListing" runat="server" Visible="false">
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
            </Content>
        </uc:CollapseHolder>
        <asp:HiddenField ID="hfCustomerValues" runat="server" />
        <asp:HiddenField ID="hfStateValues" runat="server" />
        <asp:HiddenField ID="hfCityValues" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
<script type="text/javascript" src="/Scripts/jquery.multiselect.min.js"></script>
<script type="text/javascript">

    var scriptManager = Sys.WebForms.PageRequestManager.getInstance();
    scriptManager.add_endRequest(function () {
        SetFields();
    });

    function SetFields() {

        $(".multiselectdropdown").multiselect();

        $(".multiselectdropdown").each(function (index) {
            if (document.getElementById($(this).attr('id').replace("ddl", "chk")).checked)
                $("#" + $(this).attr('id')).multiselect("enable");
            else
                $("#" + $(this).attr('id')).multiselect("disable");
        });

        if (document.getElementById('<%= hfCustomerValues.ClientID %>').value == "")
            $(".customerFields *").attr('disabled', !$('#chkCustomer').checked);

        if (document.getElementById('<%= hfStateValues.ClientID %>').value == "")
            $(".stateFields *").attr('disabled', !$('#chkState').checked);

        if (document.getElementById('<%= hfCityValues.ClientID %>').value == "")
            $(".cityFields *").attr('disabled', !$('#chkCity').checked);
    }

    function toggleJobAttr() {
        if (document.getElementById('<%= chkAllJobs.ClientID %>').checked) {
            $('.jobAttr > input:checkbox').attr('checked', true);

            $('.multiselectdropdown').multiselect("enable");
            $(".customerFields *").attr('disabled', false);
            $(".stateFields *").attr('disabled', false);
            $(".cityFields *").attr('disabled', false);

            $(".radioAndOr").attr("disabled", false);

            $(".radioAndOr").each(function (index) {
                $(this).find("input").attr("disabled", false);
            });

            var cntrl = document.getElementById('<%= txtCarCount.ClientID %>');
            var ddl = document.getElementById('<%= ddlCarCount.ClientID %>');
            cntrl.disabled = false;
            ddl.disabled = false;
        }
        else {
            $('.jobAttr > input:checkbox').attr('checked', false);

            $('.multiselectdropdown').multiselect("uncheckAll");
            $('.multiselectdropdown').multiselect("disable");
            $(".customerFields *").attr('disabled', true);
            $(".stateFields *").attr('disabled', true);
            $(".cityFields *").attr('disabled', true);

            $(".radioAndOr").attr("disabled", true);

            $(".radioAndOr").each(function (index) {
                $(this).find("input").attr("disabled", true);
                $(this).find("input")[0].checked = false;
                $(this).find("input")[1].checked = true;
            });

            var cntrl = document.getElementById('<%= txtCarCount.ClientID %>');
            var ddl = document.getElementById('<%= ddlCarCount.ClientID %>');
            cntrl.disabled = true;
            ddl.disabled = true;
            ddl[0].selected = true;
            ddl[1].selected = false;
            ddl[2].selected = false;
            cntrl.value = "";
        }
    }

    function toggleAllCallType() {
        if (document.getElementById('<%= chkAllCallTypes.ClientID %>').checked) {
            $('.callType input:checkbox').attr('checked', true);
        }
        else {
            $('.callType input:checkbox').attr('checked', false);
        }
    }

    function toggleSingleCallType(selector, ClientId) {
        if (document.getElementById(ClientId).checked) {
            $('.callType.' + selector + ' table input:checkbox').attr('checked', true);
        }
        else {
            $('.callType.' + selector + ' table input:checkbox').attr('checked', false);
        }
    }

    $(document).ready(function () {
        SetFields();
    });

    function toogleField(checkBoxName, controlName) {

        var checkBoxClicked = $('#' + checkBoxName);
        var isChecked = checkBoxClicked.attr('checked');

        if (controlName != "") {
            if ("txtCarCount" == controlName) {
                var cntrl = document.getElementById('<%= txtCarCount.ClientID %>');
                var ddl = document.getElementById('<%= ddlCarCount.ClientID %>');
                if (isChecked) {
                    cntrl.disabled = false;
                    ddl.disabled = false;
                }
                else {
                    cntrl.disabled = true;
                    ddl.disabled = true;
                    ddl[0].selected = true;
                    ddl[1].selected = false;
                    ddl[2].selected = false;
                    cntrl.value = "";
                }
            }
            else if ("customerFields" == controlName || "stateFields" == controlName || "cityFields" == controlName) {
                $("." + controlName + " *").attr('disabled', !isChecked);

                if (!isChecked) {
                    if ("customerFields" == controlName) {
                        $('#<%=lstSelectedCustomers.ClientID%>').children().remove();
                        document.getElementById('<%= hfCustomerValues.ClientID %>').value = "";
                    }
                    else if ("stateFields" == controlName) {
                        $('#<%=lstBoxSelectedState.ClientID%>').children().remove();
                        document.getElementById('<%= hfStateValues.ClientID %>').value = "";
                    }
                    else if ("cityFields" == controlName) {
                        $('#<%=lstBoxSelectedCity.ClientID%>').children().remove();
                        document.getElementById('<%= hfCityValues.ClientID %>').value = "";
                    }
                }
            }
            else {
                if (isChecked) {
                    $("#" + controlName).multiselect("enable");
                }
                else {
                    $("#" + controlName).multiselect("uncheckAll");
                    $("#" + controlName).multiselect("disable");
                }
            }
        }

        try {

            var element = null;

            if (controlName === "txtCarCount")
                element = checkBoxClicked.parent().parent().parent();
            else
                element = checkBoxClicked.parent().parent();

            element.find(".radioAndOr").attr("disabled", !isChecked);
            element.find(".radioAndOr").find("input").attr("disabled", !isChecked);

            if (!isChecked) {
                element.find(".radioAndOr").find("input")[0].checked = false;
                element.find(".radioAndOr").find("input")[1].checked = true;
            }
        }
        catch (err) {
        }
    }

    function toogleFieldOld(isChecked, controlName) {
        if ("txtCarCount" == controlName) {
            var cntrl = document.getElementById('<%= txtCarCount.ClientID %>');
            var ddl = document.getElementById('<%= ddlCarCount.ClientID %>');
            if (isChecked) {
                cntrl.disabled = false;
                ddl.disabled = false;
            }
            else {
                cntrl.disabled = true;
                ddl.disabled = true;
                ddl[0].selected = true;
                ddl[1].selected = false;
                ddl[2].selected = false;
                cntrl.value = "";
            }
        }
        else if ("customerFields" == controlName || "stateFields" == controlName || "cityFields" == controlName) {
            $("." + controlName + " *").attr('disabled', !isChecked);

            if (!isChecked) {
                if ("customerFields" == controlName) {
                    $('#<%=lstSelectedCustomers.ClientID%>').children().remove();
                    document.getElementById('<%= hfCustomerValues.ClientID %>').value = "";
                }
                else if ("stateFields" == controlName) {
                    $('#<%=lstBoxSelectedState.ClientID%>').children().remove();
                    document.getElementById('<%= hfStateValues.ClientID %>').value = "";
                }
                else if ("cityFields" == controlName) {
                    $('#<%=lstBoxSelectedCity.ClientID%>').children().remove();
                    document.getElementById('<%= hfCityValues.ClientID %>').value = "";
                }
            }
        }
        else {
            if (isChecked)
                $("#" + controlName).multiselect("enable");
            else {
                $("#" + controlName).multiselect("uncheckAll");
                $("#" + controlName).multiselect("disable");
            }
        }
    }

    function AddCustomer(customerId) {
        if (customerId != 0) {
            var lstSelectedCustomers = document.getElementById('<%= lstSelectedCustomers.ClientID %>');
            var txtSelectedCustomers = document.getElementById('<%= actCustomer.TextControlClientID %>');
            var hfValue = document.getElementById('<%= hfCustomerValues.ClientID %>');

            for (var i = 0; i < lstSelectedCustomers.length; i++)
                if (lstSelectedCustomers[i].text == txtSelectedCustomers.value) {
                    alert("The selected company have already been selected.");
                    txtSelectedCustomers.value = "";
                    return;
                }

            lstSelectedCustomers.add(new Option(txtSelectedCustomers.value, customerId));
            txtSelectedCustomers.value = "";

            hfValue.value += ';' + customerId;

            CreateGrid();
        }
    }

    function AddState(stateId) {
        if (stateId != 0) {
            var lstSelectedStates = document.getElementById('<%= lstBoxSelectedState.ClientID %>');
            var txtSelectedStates = document.getElementById('<%= acState.TextControlClientID %>');
            var hfValue = document.getElementById('<%= hfStateValues.ClientID %>');

            for (var i = 0; i < lstSelectedStates.length; i++)
                if (lstSelectedStates[i].text == txtSelectedStates.value) {
                    alert("The selected State have already been selected.");
                    txtSelectedStates.value = "";
                    return;
                }

            lstSelectedStates.add(new Option(txtSelectedStates.value, stateId));
            txtSelectedStates.value = "";

            hfValue.value += ';' + stateId;

            CreateGrid();
        }
    }

    function AddCity(cityId) {
        if (cityId != 0) {
            var lstSelectedCities = document.getElementById('<%= lstBoxSelectedCity.ClientID %>');
            var txtSelectedCities = document.getElementById('<%= acCity.TextControlClientID %>');
            var hfValue = document.getElementById('<%= hfCityValues.ClientID %>');

            for (var i = 0; i < lstSelectedCities.length; i++)
                if (lstSelectedCities[i].text == txtSelectedCities.value) {
                    alert("The selected City have already been selected.");
                    txtSelectedCities.value = "";
                    return;
                }

            lstSelectedCities.add(new Option(txtSelectedCities.value, cityId));
            txtSelectedCities.value = "";

            hfValue.value += ';' + cityId;

            CreateGrid();
        }
    }

    var SELECTED_CUSTOMERS = '<%= lstSelectedCustomers.ClientID %>';
    var VALUES_CUSTOMER = '<%= hfCustomerValues.ClientID %>';
    var SELECTED_STATE = '<%= lstBoxSelectedState.ClientID %>';
    var VALUES_STATE = '<%= hfStateValues.ClientID %>';
    var SELECTED_CITY = '<%= lstBoxSelectedCity.ClientID %>';
    var VALUES_CITY = '<%= hfCityValues.ClientID %>';


    function RemoveItem(SourceId, hfValueId) {
        $('#' + SourceId + ' option:selected').each(function () {
            var hfValue = document.getElementById(hfValueId);

            $(this).remove();

            hfValue.value = hfValue.value.replace(';' + $(this).attr('value'), '');
        });
    }

    var CUSTOMER = "Company";
    var DIVISION = "Division";
    var JOBSTATUS = "Job Status";
    var PRICETYPE = "Price Type";
    var JOBCATEGORY = "Job Category";
    var JOBTYPE = "Job Type";
    var JOBACTION = "Job Action";
    var JOBINTERIMBILLING = "Job Interim Billing";
    var GENERALLOG = "General Log";
    var COUNTRY = "Country";
    var STATE = "State";
    var CITY = "City";
    var CARCOUNT = "Car Count";
    var COMMODITIES = "Commodities";
    var CHEMICALS = "Chemicals";
    var HEAVYEQUIPMENT = "Heavy Equipment - Flag";
    var NONHEAVYEQUIPMENT = "Non-Heavy Equipment - Flag";
    var ALLEQUIPMENT = "All Equipment - Flag";
    var WHITELIGHT = "White Light - Flag";
    var tbCriterias = document.getElementById("tbCriterias");
    var rowCount;

    function CreateListRow(SourceID, Type) {
        var lstSource = document.getElementById(SourceID);

        if (lstSource === null)
            return;

        if (lstSource.length > 0) {
            var val = "";

            for (var i = 0; i < lstSource.length; i++)
                val += (val == "") ? lstSource[i].text : ", " + lstSource[i].text;

            CreateRow(Type, val);
        }
    }

    function CreateDropDownRow(SourceID, Type) {
        var lstSource = document.getElementById(SourceID);

        if (lstSource === null)
            return;

        if (lstSource.length > 0) {
            var val = "";

            for (var i = 0; i < lstSource.length; i++)
                if (lstSource[i].selected)
                    val += (val == "") ? lstSource[i].text : ", " + lstSource[i].text;

            CreateRow(Type, val);
        }
    }

    function CreateCheckRow(SourceID, Type) {
        var checkSource = document.getElementById(SourceID);


        if (checkSource.checked)
            CreateRow(Type, "True");
    }

    function CreateTextRow(ddlID, SourceID, Type) {
        var ddlCarCount = document.getElementById(ddlID);
        var txtCarCount = document.getElementById(SourceID);

        if (txtCarCount.value.replace(/^\s+|\s+$/g, "") != "")
            for (var i = 0; i < ddlCarCount.length; i++) {
                if (ddlCarCount[i].selected)
                    CreateRow(Type, ddlCarCount[i].text + " " + txtCarCount.value);
            }
    }

    function CreateRow(Type, val) {
        if (val != "") {
            var row = tbCriterias.insertRow(rowCount);
            var cell1 = row.insertCell(0);
            cell1.innerHTML = Type;
            var cell2 = row.insertCell(1);
            cell2.innerHTML = val;
            rowCount++;
        }
    }

    function CreateGrid() {
        var pnlGrid = document.getElementById('<%= pnlGrid.ClientID %>');
        var pnlNoRows = document.getElementById('<%= pnlNoRows.ClientID %>');
        tbCriterias = document.getElementById("tbCriterias");

        if (tbCriterias != null) {
            rowCount = tbCriterias.rows.length;

            for (var i = rowCount - 1; i > 0; i--) {
                tbCriterias.deleteRow(i);
            }

            rowCount = tbCriterias.rows.length;

            CreateListRow('<%= lstSelectedCustomers.ClientID %>', CUSTOMER);
            CreateDropDownRow('<%= ddlDivision.ClientID %>', DIVISION);
            CreateDropDownRow('<%= ddlJobStatus.ClientID %>', JOBSTATUS);
            CreateDropDownRow('<%= ddlPriceType.ClientID %>', PRICETYPE);
            CreateDropDownRow('<%= ddlJobCategory.ClientID %>', JOBCATEGORY);
            CreateDropDownRow('<%= ddlJobType.ClientID %>', JOBTYPE);
            CreateDropDownRow('<%= ddlJobAction.ClientID %>', JOBACTION);
            CreateDropDownRow('<%= ddlInterimbilling.ClientID %>', JOBINTERIMBILLING);
            CreateCheckRow('<%= chkGeneralLog.ClientID %>', GENERALLOG);
            CreateDropDownRow('<%= ddlCountry.ClientID %>', COUNTRY);
            CreateListRow('<%= lstBoxSelectedState.ClientID %>', STATE);
            CreateListRow('<%= lstBoxSelectedCity.ClientID %>', CITY);
            CreateTextRow('<%= ddlCarCount.ClientID %>', '<%= txtCarCount.ClientID %>', CARCOUNT);
            CreateDropDownRow('<%= ddlCommodities.ClientID %>', COMMODITIES);
            CreateDropDownRow('<%= ddlChemicals.ClientID %>', CHEMICALS);
            CreateCheckRow('<%= chkHeavyEquipment.ClientID %>', HEAVYEQUIPMENT);
            CreateCheckRow('<%= chkNonHeavyEquipment.ClientID %>', NONHEAVYEQUIPMENT);
            CreateCheckRow('<%= chkAllEquipment.ClientID %>', ALLEQUIPMENT);
            CreateCheckRow('<%= chkWhiteLight.ClientID %>', WHITELIGHT);

            pnlGrid.style.display = (tbCriterias.rows.length > 1) ? "" : "none";
            pnlNoRows.style.display = (tbCriterias.rows.length > 1) ? "none" : "";
        }
    }

    function toggleAll(isChecked, fieldName) {
        $('#' + fieldName)[0].EnableAutocomplete(isChecked);
    }

    function CollapseExpandCallCriteria(Button, selector) {
        var line1 = $(".Attributes." + selector);
        var line2 = $(".CallLogs." + selector);
        var line3 = $(".Advisement." + selector);
        var button = $("#" + Button);
        var expandedItems = $get('<%=hfExpandedCallCriterias.ClientID %>').value;

        line1.toggle();
        line2.toggle();
        line3.toggle();
        button.toggleClass("Expand");
        button.toggleClass("Collapse");

        if (button.attr('class') == 'Expand')
            expandedItems = expandedItems.replace(selector + ';', '');
        else
            expandedItems += selector + ';';
        $get('<%=hfExpandedCallCriterias.ClientID %>').value = expandedItems;

        if (button.attr('class') == 'Expand') {
            var attributesLines = $(".AttributesItems." + selector);
            attributesLines.css("display", "none");

            var conditionsLines = $(".CallLogsItems." + selector);
            conditionsLines.css("display", "none");

            var attributesLinesButton = $(".Attributes." + selector + " .Collapse");

            attributesLinesButton.toggleClass("Expand");
            attributesLinesButton.toggleClass("Collapse");

            var conditionsLinesButton = $(".CallLogs." + selector + " .Collapse");

            conditionsLinesButton.toggleClass("Expand");
            conditionsLinesButton.toggleClass("Collapse");
        }
    }

    function CollapseExpandJobAttributes(Button, selector) {
        var line = $(".AttributesItems." + selector);
        var button = $("#" + Button);
        var expandedItems = $get('<%=hfExpandedJobAttributes.ClientID %>').value;

        line.toggle();
        button.toggleClass("Expand");
        button.toggleClass("Collapse");

        if (button.attr('class') == 'Expand')
            expandedItems = expandedItems.replace(selector + ';', '');
        else
            expandedItems += selector + ';';
        $get('<%=hfExpandedJobAttributes.ClientID %>').value = expandedItems;
    }

    function CollapseExpandCallLogConditions(Button, selector) {
        var line = $(".CallLogsItems." + selector);
        var button = $("#" + Button);
        var expandedItems = $get('<%=hfExpandedJobCallLogConditions.ClientID %>').value;

        line.toggle();
        button.toggleClass("Expand");
        button.toggleClass("Collapse");

        if (button.attr('class') == 'Expand')
            expandedItems = expandedItems.replace(selector + ';', '');
        else
            expandedItems += selector + ';';
        $get('<%=hfExpandedJobCallLogConditions.ClientID %>').value = expandedItems;
    }

</script>
