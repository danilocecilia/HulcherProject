<%@ Page Title="" Language="C#" MasterPageFile="~/ContentPage.master" AutoEventWireup="true"
    CodeBehind="ProjectCalendarPrint.aspx.cs" Inherits="Hulcher.OneSource.CustomerService.Web.ProjectCalendarPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Styles/Forms.css" rel="stylesheet" type="text/css" />
    <link href="Styles/ProjectCalendar.css" rel="stylesheet" type="text/css" />
    <style type="text/css" media="print">
        .landScape
        {
            width: 100%;
            height: 100%;
            margin: 0% 0% 0% 0%;
            filter: progid:DXImageTransform.Microsoft.BasicImage(Rotation=3);
        }
        .noPrint
        {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div id="mainTitle" class="Header">
        <asp:Label ID="lblTitle" Text="Project Calendar Print" runat="server"></asp:Label>
    </div>
    <br />
    <div id="FieldsFilter">
        <div>
            <asp:Label ID="lblDivisionFilterTitle" runat="server" Text="Division:"></asp:Label>
            <asp:Label ID="lblDivisionFilter" runat="server"></asp:Label>
        </div>
        <div>
            <asp:Label ID="lblEquipmentTypeFilterTitle" runat="server" Text="Equipment Type:"></asp:Label>
            <asp:Label ID="lblEquipmentTypeFilter" runat="server"></asp:Label>
        </div>
        <div>
            <asp:Label ID="lblEquipmentFilterTitle" runat="server" Text="Equipment:"></asp:Label>
            <asp:Label ID="lblEquipmentFilter" runat="server"></asp:Label>
        </div>
        <div>
            <asp:Label ID="lblEmployeeFilterTitle" runat="server" Text="Employee:"></asp:Label>
            <asp:Label ID="lblEmployeeFilter" runat="server"></asp:Label>
        </div>
        <div>
            <asp:Label ID="lblCustomerFilterTitle" runat="server" Text="Company:"></asp:Label>
            <asp:Label ID="lblCustomerFilter" runat="server"></asp:Label>
        </div>
        <div>
            <asp:Label ID="lblJobFilterTitle" runat="server" Text="Job:"></asp:Label>
            <asp:Label ID="lblJobFilter" runat="server"></asp:Label>
        </div>
        <div>
            <asp:Label ID="lblJobActionFilterTitle" runat="server" Text="Job Action:"></asp:Label>
            <asp:Label ID="lblJobActionFilter" runat="server"></asp:Label>
        </div>
        <div>
            <asp:Label ID="lblStartDateValueTitle" runat="server" Text="Start Date:"></asp:Label>
            <asp:Label ID="lblStartDateValue" runat="server"></asp:Label>
        </div>
        <div>
            <asp:Label ID="lblEndDateValueTitle" runat="server" Text="End Date:"></asp:Label>
            <asp:Label ID="lblEndDateValue" runat="server"></asp:Label>
        </div>
    </div>
    <br />
    <br />
    <div id="ProjectCalendarTable" runat="server">
        <div>
            <div>
            </div>
            <div id="tbProjectCalendar" class="ProjectCalendar_ScrollDiv2">
                <div id="divTeste" runat="server">
                </div>
                <%--<asp:Literal ID="litCalendar" runat="server"></asp:Literal>--%>
            </div>
        </div>
    </div>
    <script>
        //Instance of ScripManager
        var scriptManager = Sys.WebForms.PageRequestManager.getInstance();

        scriptManager.add_endRequest(function () {

            SetGridHeight();
        });

        $(document).ready(function () {
            $("body").addClass("landScape");

            SetGridHeight();

            $(window).resize(function () { timeoutHeightId = setTimeout(SetGridHeight, 500); });

            alert('Please verify that your print settings have a Landscape orientation and minimum margins.');

            self.print();
        });


        function SetGridHeight() {
            var heightGrid = parseInt($("#tbProjectCalendar").css('max-height').replace('px', ''));
            var heightVal = (document.body.offsetHeight - (mainTitle.offsetHeight + FieldsFilter.offsetHeight + 100));

            if (heightVal > 220)
                $("#tbProjectCalendar").css('max-height', heightVal);
            else
                $("#tbProjectCalendar").css('max-height', 220);
        }
                      
    </script>
</asp:Content>
