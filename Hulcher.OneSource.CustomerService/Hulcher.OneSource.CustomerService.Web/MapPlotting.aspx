<%@ Page Title="" Language="C#" MasterPageFile="~/ContentPage.master" AutoEventWireup="true"
    CodeBehind="MapPlotting.aspx.cs" Inherits="Hulcher.OneSource.CustomerService.Web.MapPlotting" %>

<%@ MasterType TypeName="Hulcher.OneSource.CustomerService.Web.ContentPage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/AutoCompleteTextbox.ascx" TagName="AutoCompleteTextbox"
    TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Styles/Forms.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/jquery.multiselect.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/OpenLayers.js" type="text/javascript" defer="defer"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <asp:Label ID="lblTitle" runat="server" Text="Map Plotting" CssClass="fontBold fontBig"></asp:Label>
    <div id="divVisualization" class="Header">
        <asp:Label ID="Label3" runat="server" Text="Filter" CssClass="fontBold"></asp:Label>
    </div>
    <div class="Content">
        <br />
        <div class="space49 floatLeft height35 alignLeft paddingLeft5">
            <asp:Label ID="lblJobFiltersTitle" runat="server" Text="Job Filters" CssClass="fontBold"></asp:Label>
        </div>
        <div class="space49 floatRight height35 alignLeft paddingLeft5">
            <asp:Label ID="lblResourceFiltersTitle" runat="server" Text="Resource Filters" CssClass="fontBold"></asp:Label>
        </div>
        <%--Line--%>
        <div class="space50 floatRight height35">
            <div class="space29 floatLeft alignRight paddingTop5">
                <asp:Label ID="Label4" runat="server" Text="Job #: "></asp:Label>
            </div>
            <div class="space70 floatRight alignLeft">
                <uc1:AutoCompleteTextbox ID="actJobNumber" runat="server" ServiceMethod="GetJobNumberList"
                    GridViewButtonImageUrl="~/Images/money.png" GridViewIdName="ID" DisplayField="PrefixedNumber"
                    TextBoxWidth="200px" AutoPostBack="false" WindowTitle="Map Plotting - Find Job #"
                    AutoCompleteSource="JobNumber" ColumnHeaderList="Job Number" ColumnValueList="PrefixedNumber" ScriptToExecute="getJobNumber"  />
            </div>
        </div>
        <div class="space49 floatLeft height35">
            <div class="space29 floatLeft alignRight paddingTop5">
                <asp:Label ID="lblCustomer" runat="server" Text="Company: "></asp:Label>
            </div>
            <div class="space70 floatRight alignLeft">
                <uc1:AutoCompleteTextbox ID="actCustomer" runat="server" ServiceMethod="GetCustomerList"
                    GridViewButtonImageUrl="~/Images/money.png" GridViewIdName="ID" DisplayField="Name"
                    TextBoxWidth="200px" AutoPostBack="false" WindowTitle="Map Plotting - Find Company"
                    AutoCompleteSource="Customer" ColumnHeaderList="Name,Attn,Company ID" ColumnValueList="Name,Attn,CustomerNumber"
                    ScriptToExecute="getCustomerID" />
            </div>
        </div>
        <div class="space50 floatRight height35">
            <div class="space29 floatLeft alignRight paddingTop5">
                <asp:Label ID="lblEquipmentTypes" runat="server" Text="Eq.Types: "></asp:Label>
            </div>
            <div class="space70 floatRight alignLeft">
                <asp:MultiSelectDropDownList ID="ddlEquipmentTypes" runat="server" SelectionMode="Multiple"
                    CssClass="multiselectdropdown" ClientIDMode="Static">
                </asp:MultiSelectDropDownList>
            </div>
        </div>
        <%--Line--%>
        <div class="space49 floatLeft height35">
            <div class="space29 floatLeft alignRight paddingTop5">
                <asp:Label ID="lblLocation" runat="server" Text="Location: "></asp:Label>
            </div>
            <div class="space70 floatRight alignLeft">
                <uc1:AutoCompleteTextbox ID="actState" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                    TextBoxWidth="200px" GridViewIdName="ID" DisplayField="" AutoPostBack="false"
                    WindowTitle="Map Plotting - Find Location" AutoCompleteSource="State" ColumnHeaderList="Acronym,Name"
                    ColumnValueList="Acronym,Name" ServiceMethod="GetStateList" TextBoxCssClass="input"
                    MinimumPrefixLength="2" ScriptToExecute="GetStateID" />
            </div>
        </div>
        <div class="space50 floatRight height35">
            <div class="space29 floatLeft alignRight paddingTop5">
                <asp:Label ID="lblRegion" runat="server" Text="Region: "></asp:Label>
            </div>
            <div class="space70 floatRight alignLeft">
                <asp:MultiSelectDropDownList ID="ddlRegion" runat="server" SelectionMode="Multiple"
                    CssClass="multiselectdropdown" ClientIDMode="Static">
                </asp:MultiSelectDropDownList>
            </div>
        </div>
        <%--Line--%>
        <div class="space49 floatLeft height35">
            <div class="space29 floatLeft alignRight paddingTop5">
                <asp:Label ID="lblDivisions" runat="server" Text="Divisions: "></asp:Label>
            </div>
            <div class="space70 floatRight alignLeft">
                <asp:MultiSelectDropDownList ID="ddlDivisions" runat="server" SelectionMode="Multiple"
                    CssClass="multiselectdropdown" ClientIDMode="Static">
                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                    <asp:ListItem Text="3" Value="1"></asp:ListItem>
                </asp:MultiSelectDropDownList>
            </div>
        </div>
        <div class="space50 floatRight height35">
            <div class="space29 floatLeft alignRight paddingTop5">
                <asp:Label ID="lblComboNumber" runat="server" Text="Combo #: "></asp:Label>
            </div>
            <div class="space70 floatRight alignLeft">
                <asp:TextBox ID="txtComboNumber" runat="server" Text="" CssClass="input" Width="210px"></asp:TextBox>
            </div>
        </div>
        <%--Line--%>
        <div class="space49 floatLeft height35">
            <div class="space29 floatLeft alignRight paddingTop5">
                <asp:Label ID="lblJobAction" runat="server" Text="Job Action: "></asp:Label>
            </div>
            <div class="space70 floatRight alignLeft">
                <asp:MultiSelectDropDownList ID="ddlJobAction" runat="server" SelectionMode="Multiple"
                    CssClass="multiselectdropdown" ClientIDMode="Static" Width="200px">
                </asp:MultiSelectDropDownList>
            </div>
        </div>
        <div class="space50 floatRight height35">
            <div class="space29 floatLeft alignRight paddingTop5">
                <asp:Label ID="lblUnitNumber" runat="server" Text="Unit #: "></asp:Label>
            </div>
            <div class="space70 floatRight alignLeft">
                <asp:TextBox ID="txtUnitNumber" runat="server" Text="" CssClass="input" Width="210px"></asp:TextBox>
            </div>
        </div>
        <%--Line--%>
        <div class="space49 floatLeft height35">
            <div class="space29 floatLeft alignRight paddingTop5">
                <asp:Label ID="lblJobCategory" runat="server" Text="Job Category: "></asp:Label>
            </div>
            <div class="space70 floatRight alignLeft">
                <asp:MultiSelectDropDownList ID="ddlJobCategory" runat="server" SelectionMode="Multiple"
                    CssClass="multiselectdropdown" ClientIDMode="Static">
                </asp:MultiSelectDropDownList>
            </div>
        </div>
        <div class="space50 floatRight height35">
            <div class="space29 floatLeft alignRight paddingTop5">
                <asp:Label ID="lblEmployeeName" runat="server" Text="Employee Name: "></asp:Label>
            </div>
            <div class="space70 floatRight alignLeft">
                <asp:TextBox ID="txtEmployeeName" runat="server" Text="" CssClass="input" Width="210px"></asp:TextBox>
            </div>
        </div>
        <%--Line--%>
        <div class="space49 floatLeft height35">
            <div class="space29 floatLeft alignRight paddingTop5">
                <asp:Label ID="lblPriceType" runat="server" Text="Price Type: "></asp:Label>
            </div>
            <div class="space70 floatRight alignLeft">
                <asp:MultiSelectDropDownList ID="ddlPriceType" runat="server" SelectionMode="Multiple"
                    CssClass="multiselectdropdown" ClientIDMode="Static">
                </asp:MultiSelectDropDownList>
            </div>
        </div>
        <div class="space50 floatRight height35">
            <div class="space29 floatLeft alignRight paddingTop5">
                <asp:Label ID="lblEmployeeTitle" runat="server" Text="Employee Title: "></asp:Label>
            </div>
            <div class="space70 floatRight alignLeft">
                <asp:TextBox ID="txtEmployeeTitle" runat="server" Text="" CssClass="input" Width="210px"></asp:TextBox>
            </div>
        </div>
        <%--Line--%>
        <div class="space49 floatLeft height35">
            <div class="space29 floatLeft alignRight paddingTop5">
                <asp:Label ID="lblCreationDate" runat="server" Text="Creation Date: "></asp:Label>
            </div>
            <div class="space70 floatRight alignLeft">
                <uc1:DatePicker ID="dpCreationDate" DateTimeFormat="Default" ShowOn="Both" runat="server"
                    IsValidEmpty="true"></uc1:DatePicker>
            </div>
        </div>
        <div class="space50 floatRight height35">
            <div class="space29 floatLeft alignRight paddingTop5">
                <asp:Label ID="Label2" runat="server" Text="" Visible="false"></asp:Label>
            </div>
            <div class="space70 floatRight alignLeft">
                <asp:Label ID="Label1" runat="server" Text="" Visible="false"></asp:Label>
            </div>
        </div>
        <%--Line--%>
        <div class="space95 alignRight paddingBottom5">
            <input id="btnPlotMap" type="button" value="Plot Data" class="btn" onclick="CallMapPlotService();return false;" />
        </div>
    </div>
    <br />
    <div id="div1" class="Header">
        <asp:Label ID="lblMap" Text="Map" runat="server" CssClass="fontBold"></asp:Label>
    </div>
    <div class="Content">
        <div id="map">
        </div>
    </div>
    <script type="text/javascript" src="/Scripts/jquery.multiselect.min.js"></script>
    <script type="text/javascript" src="Scripts/json2.js" language="javascript"></script>
    <script type="text/javascript" language="javascript">

        var PlotObjectsList;

        // Filter Objects
        var jobNumberID;
        var customerID;
        var stateID;
        var divisionList;
        var jobActionList;
        var jobCategoryList;
        var priceTypeList;
        var creationDate;
        var equipmentTypeList;
        var regionList;
        var comboNumber;
        var unitNumber;
        var employeeName;
        var employeeTitle;

        function CallMapPlotService() {
            var request = getMapFilter();
            tempuri.org.IJSONService.GetMapPlottingObjects(request, CallMapPlotServiceCallBack);
        }

        var mapServerPath = '<%=ConfigurationManager.AppSettings["MapServerPath"].ToString() %>';

        function CallMapPlotServiceCallBack(WebServiceResult) {
            PlotObjectsList = WebServiceResult;
            GetSourceDataMapPlot(PlotObjectsList, mapServerPath);
        }

        function getMapFilter() {
            getDivisionList();
            getJobActionList();
            getJobCategoryList();
            getPriceTypeList();
            getCreationDate();
            getEquipmentTypeList();
            getRegionList();
            getComboNumber();
            getUnitNumber();
            getEmployeeName();
            getEmployeeTitle();

            var requestObject = new Hulcher.OneSource.CustomerService.Core.Globals.MapPlotRequestDataObject();

            requestObject.JobNumberID = jobNumberID;
            requestObject.CustomerID = customerID;
            requestObject.StateID = stateID;
            requestObject.DivisionList = divisionList;
            requestObject.JobActionList = jobActionList;
            requestObject.JobCategoryList = jobCategoryList;
            requestObject.PriceTypeList = priceTypeList;
            requestObject.CreationDate = creationDate;
            requestObject.EquipmentTypeList = equipmentTypeList;
            requestObject.RegionList = regionList;
            requestObject.ComboNumber = comboNumber;
            requestObject.UnitNumber = unitNumber;
            requestObject.EmployeeName = employeeName;
            requestObject.EmployeeTitle = employeeTitle;

            return requestObject;
        }

        function getCustomerID(_customerID) {
            if (_customerID != 0)
                customerID = _customerID;
            else
                customerID = null;
        }

        function getJobNumber(_jobNumberId) {
            if (_jobNumberId != 0)
                jobNumberID = _jobNumberId;
            else
                jobNumberID = null;
        }

        function GetStateID(_stateID) {
            if (_stateID != 0)
                stateID = _stateID;
            else
                stateID = null;
        }

        function getDivisionList() {
            divisionList = null;

            var list = '';

            var lstSource = document.getElementById('<%= ddlDivisions.ClientID %>');

            if (lstSource.length > 0) {
                for (var i = 0; i < lstSource.length; i++)
                    if (lstSource[i].selected)
                        list += (list == "") ? lstSource[i].value : ", " + lstSource[i].value;
            }

            if (list != '')
                divisionList = list.split(',');
        }

        function getJobActionList() {
            jobActionList = null;

            var list = '';

            var lstSource = document.getElementById('<%= ddlJobAction.ClientID %>');

            if (lstSource.length > 0) {
                for (var i = 0; i < lstSource.length; i++)
                    if (lstSource[i].selected)
                        list += (list == "") ? lstSource[i].value : ", " + lstSource[i].value;
            }

            if (list != '')
                jobActionList = list.split(',');
        }

        function getJobCategoryList() {
            jobCategoryList = null;

            var list = '';

            var lstSource = document.getElementById('<%= ddlJobCategory.ClientID %>');

            if (lstSource.length > 0) {
                for (var i = 0; i < lstSource.length; i++)
                    if (lstSource[i].selected)
                        list += (list == "") ? lstSource[i].value : ", " + lstSource[i].value;
            }

            if (list != '')
                jobCategoryList = list.split(',');
        }

        function getPriceTypeList() {
            priceTypeList = null;

            var list = '';

            var lstSource = document.getElementById('<%= ddlPriceType.ClientID %>');

            if (lstSource.length > 0) {
                for (var i = 0; i < lstSource.length; i++)
                    if (lstSource[i].selected)
                        list += (list == "") ? lstSource[i].value : ", " + lstSource[i].value;
            }

            if (list != '')
                priceTypeList = list.split(',');
        }

        function getCreationDate() {
            creationDate = null;

            var source = document.getElementById('<%= dpCreationDate.TextBoxClientID %>');

            if (source.value != '') {
                creationDate = new Date(source.value);
            }
        }

        function getEquipmentTypeList() {
            equipmentTypeList = null;

            var list = '';

            var lstSource = document.getElementById('<%= ddlEquipmentTypes.ClientID %>');

            if (lstSource.length > 0) {
                for (var i = 0; i < lstSource.length; i++)
                    if (lstSource[i].selected)
                        list += (list == "") ? lstSource[i].value : ", " + lstSource[i].value;
            }

            if (list != '')
                equipmentTypeList = list.split(',');
        }

        function getRegionList() {
            regionList = null;

            var list = '';

            var lstSource = document.getElementById('<%= ddlRegion.ClientID %>');

            if (lstSource.length > 0) {
                for (var i = 0; i < lstSource.length; i++)
                    if (lstSource[i].selected)
                        list += (list == "") ? lstSource[i].value : ", " + lstSource[i].value;
            }

            if (list != '')
                regionList = list.split(',');
        }

        function getComboNumber() {
            comboNumber = null;

            var source = document.getElementById('<%= txtComboNumber.ClientID %>');
            comboNumber = source.value;
        }

        function getUnitNumber() {
            unitNumber = null;

            var source = document.getElementById('<%= txtUnitNumber.ClientID %>');
            unitNumber = source.value;
        }

        function getEmployeeName() {
            employeeName = null;

            var source = document.getElementById('<%= txtEmployeeName.ClientID %>');
            employeeName = source.value;
        }

        function getEmployeeTitle() {
            employeeTitle = null;

            var source = document.getElementById('<%= txtEmployeeTitle.ClientID %>');
            employeeTitle = source.value;
        }
    </script>
    <script src="Scripts/ol.js" type="text/javascript" language="javascript" defer="defer"></script>
</asp:Content>
