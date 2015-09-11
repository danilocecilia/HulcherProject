<%@ Page Title="Key Persons Location" Language="C#" MasterPageFile="~/ContentPage.master" AutoEventWireup="true"
    CodeBehind="ManagersLocation.aspx.cs" Inherits="Hulcher.OneSource.CustomerService.Web.ManagersLocation" %>

<%@ MasterType TypeName="Hulcher.OneSource.CustomerService.Web.ContentPage" %>
<%@ Register Src="~/UserControls/AutoCompleteTextbox.ascx" TagName="AutoCompleteTextbox"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Styles/Forms.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <asp:UpdatePanel ID="upPage" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="mainTitle">
                <asp:Label ID="lblTitle" runat="server" Text="Key Persons Location" Font-Size="Large" Font-Bold="true"></asp:Label>
                <br />
                <br />
                <div id="divVisualization" class="Header">
                    <asp:Label ID="lblVisualizationTitle" runat="server" Text="Filter"></asp:Label>
                </div>
                <div class="Content AlignCenter">
                    <div class="paddingBottom5 paddingTop5">
                        <div class="inline paddingRight10">
                            <asp:Label ID="lblName" runat="server" Text="Name: "></asp:Label>
                            <asp:TextBox ID="txtName" runat="server" CssClass="input"></asp:TextBox>
                        </div>
                        <div class="inline paddingRight5">
                            <asp:Label ID="lblCallType" runat="server" Text="Call Type: "></asp:Label>
                            <uc1:AutoCompleteTextbox ID="actCallType" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                TextBoxWidth="120px" GridViewIdName="ID" DisplayField="Description" AutoPostBack="false"
                                RequiredField="false" WindowTitle="Key Person Location - Find Call Type" ErrorMessage="Call Type field is required"
                                AutoCompleteSource="CallType" ColumnHeaderList="Description" ColumnValueList="Description"
                                ServiceMethod="GetCallTypeList" TextBoxCssClass="input" />
                        </div>
                        <div class="inline paddingRight5">
                            <asp:Label ID="lblJobNumber" runat="server" Text="Job #: "></asp:Label>
                            <uc1:AutoCompleteTextbox ID="actJobNumber" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                                TextBoxWidth="120px" GridViewIdName="ID" DisplayField="" AutoPostBack="false"
                                RequiredField="false" WindowTitle="Key Person Location - Find Job Number" ErrorMessage="Job Number field is required"
                                AutoCompleteSource="JobNumberWithGeneral" ColumnHeaderList="Number,Company,Location"
                                ColumnValueList="PrefixedNumber,CS_CustomerInfo.CS_Customer.Name,CS_LocationInfo.FullLocation"
                                ServiceMethod="GetJobNumberListWithGeneral" TextBoxCssClass="input" />
                        </div>
                        <div class="inline paddingRight5">
                            <asp:Button ID="btnFilter" runat="server" Text="Filter" CssClass="btn" OnClick="btnFilter_Click" />
                        </div>
                    </div>
                </div>
                <br />
                <div class="Header">
                    <asp:Label ID="lblListingTitle" runat="server" Text="Listing"></asp:Label>
                </div>
            </div>
            <div class="Content">
                <div id="divGrid" class="gridview">
                    <asp:ScrollableGridView ID="sgvManagers" runat="server" CssClass="ScrollableGridView" MaxHeight="220px"
                        OnRowDataBound="sgvManagers_RowDataBound" AutoGenerateColumns="false" ShowFooter="false">
                        <Columns>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlEmployeeName" runat="server"></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Last Call Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblLastCallTypeDate" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Last Call Type">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlLastCallType" runat="server" NavigateUrl="javascript: alert('This Call Entry is automatic generated and can not be updated.');"></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Hotel Details">
                                <ItemTemplate>
                                    <div>
                                        <div style="max-height: 100px; overflow: hidden;">
                                            <asp:Label ID="lblHotelDetails" runat="server"></asp:Label>
                                        </div>
                                        <asp:Panel ID="pnlToolTip" runat="server" Style="background-color: #FFFFFF; border: 1px solid #000;
                                            display: none; width: 400px; position: fixed; max-height: 300px; overflow-y: auto;
                                            overflow-x: hidden;">
                                            <asp:Label ID="lblTool" runat="server" />
                                        </asp:Panel>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Job #">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlJobNumber" runat="server"></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:ScrollableGridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script language="javascript" language="javascript">
        var panelId;
        var panelTop;
        var panelLeft;

        window.onresize = function () { panelTop = 0; panelLeft = 0; }

        //Instance of ScripManager
        var scriptManager = Sys.WebForms.PageRequestManager.getInstance();

        scriptManager.add_endRequest(function () {
            SetGridHeight();
        });

        $(document).ready(function () {
            SetGridHeight();
            $(window).resize(function () { timeoutHeightId = setTimeout(SetGridHeight, 500); });
        });

        function SetGridHeight() {
            var heightGrid = parseInt($('#sgvManagers_ScrollDiv').css('max-height').replace('px', ''));
            var heightVal = (document.body.offsetHeight - (mainTitle.offsetHeight + 20)) + heightGrid;

            if (heightVal > 220)
                $('#sgvManagers_ScrollDiv').css('max-height', heightVal);
            else
                $('#sgvManagers_ScrollDiv').css('max-height', 220);
        }

        function ShowToolTip(clientId) {

            var scnWid, scnHei;
            if (document.documentElement && document.documentElement.clientHeight) {

                scnWid = document.documentElement.offsetWidth;
                scnHei = document.documentElement.offsetHeight;
            }
            else if (document.body) {
                scnWid = document.body.offsetWidth;
                scnHei = document.body.offsetHeight;

            }
            var panelControl = document.getElementById(clientId);
            if (panelControl.style.display == 'none') {
                if (panelId != panelControl.id) {
                    panelId = panelControl.id;
                    panelTop = 0;
                    panelLeft = 0;
                }

                panelControl.style.display = 'block';
                var yPos = window.event.clientY + panelControl.offsetHeight;
                if (yPos > scnHei) {

                    if (panelTop == 0) {
                        var difference = yPos - scnHei;
                        panelControl.style.top = window.event.clientY - difference;

                        panelTop = panelControl.style.top;
                    }
                    else {
                        panelControl.style.top = panelTop;
                    }
                }
                else {
                    if (panelTop == 0) {
                        panelControl.style.top = window.event.clientY;

                        panelTop = panelControl.style.top;
                    }
                    else {
                        panelControl.style.top = panelTop;
                    }
                }
                var xPos = window.event.clientX + panelControl.offsetWidth;
                if (xPos > scnWid) {
                    if (panelLeft == 0) {
                        var difference = xPos - scnWid;
                        panelControl.style.left = window.event.clientX - difference - 37;

                        panelLeft = panelControl.style.left;
                    }
                    else {
                        panelControl.style.left = panelLeft;
                    }
                }
                else {
                    if (panelLeft == 0) {
                        panelControl.style.left = window.event.clientX;

                        panelLeft = panelControl.style.left;
                    }
                    else {
                        panelControl.style.left = panelLeft;
                    }
                }
            };
        }        
    </script>

</asp:Content>
