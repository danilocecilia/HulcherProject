<%@ Page Title="Report Viewer" Language="C#" MasterPageFile="~/ReportMasterPage.Master"
    AutoEventWireup="true" CodeBehind="ShiftTurnOverReport.aspx.cs" Inherits="Hulcher.OneSource.CustomerService.Web.ShiftTurnOverReport" %>

<%@ MasterType TypeName="Hulcher.OneSource.CustomerService.Web.ReportMasterPage" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Styles/ReportView.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jobInfo.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="PageContent" ContentPlaceHolderID="Content" runat="server">
    <div style="width: 100%">
        <asp:HiddenField ID="hfViewType" runat="server" Value="2" />
        <asp:ValidationSummary ID="vsStatus" runat="server" CssClass="errorbox" HeaderText="Please correct the following information" />
        <div class="mainTitle">
            <asp:Label ID="lblMainTitle" runat="server" Font-Size="Large" Font-Bold="true" Text="Shift Transfer Report" />
        </div>
        <div class="filter">
            <div class="control">
                <div class="viewtype">
                    <asp:RadioButtonList ID="rblReportType" runat="server" RepeatDirection="Horizontal"
                        RepeatLayout="Flow" CssClass="RepType" onclick="javascript:ChangeFilter();" Visible="false">
                        <asp:ListItem Text="Quick Reference View" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Job View" Value="2" Selected="True"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="title filteritem">
                    <asp:Label ID="lblFilter" runat="server" Text="Job Status:"></asp:Label>
                </div>
                <div class="combo filteritem">
                    <asp:ComboBox ID="ddlJobStatus" runat="server" CssClass="WindowsStyle" AutoCompleteMode="SuggestAppend"
                        DropDownStyle="DropDownList" CaseSensitive="false" RenderMode="Inline">
                    </asp:ComboBox>
                    <asp:RequiredFieldValidator ID="rfvJobStatus" runat="server" EnableClientScript="true"
                        Text="*" Display="Dynamic" ErrorMessage="Shift Transfer Log - The Job Status field is required"
                        ControlToValidate="ddlJobStatus" InitialValue="- Select One - "></asp:RequiredFieldValidator>
                </div>
                <div class="button">
                    <asp:Button ID="btnGenerate" runat="server" Text="Run Report" CssClass="btn generate"
                        OnClick="btnGenerate_Click" CausesValidation="true" />
                </div>
            </div>
        </div>
    </div>
    <hr />
    <script type="text/javascript" language="javascript" defer="defer">

        //var scriptManager = Sys.WebForms.PageRequestManager.getInstance();
        //scriptManager.add_endRequest(EndRequestHandler);

        function EndRequestHandler() {
            var inputs = $(".RepType input");
            var obj = document.getElementById('<%= hfViewType.ClientID %>');

            if (obj.value == "1") {
                inputs[0].checked = true;
                ChangeFilter();
            }

            if (obj.value == "2") {
                inputs[1].checked = true;
                ChangeFilter();
            }
        }

        function ChangeFilter() {
            var inputs = $(".RepType input");
            var obj = document.getElementById('<%= hfViewType.ClientID %>');

            if (inputs[0].checked) {
                $(".filteritem").each(function () { this.style.display = "none"; });
                obj.value = inputs[0].value;
                ValidatorEnable($get('<%=rfvJobStatus.ClientID %>'), false);
            }
            else {
                $(".filteritem").each(function () { this.style.display = "inline-block"; });
                obj.value = inputs[1].value;
                ValidatorEnable($get('<%=rfvJobStatus.ClientID %>'), true);
            }

            $(".generate")[0].disabled = false;
        }

    </script>
</asp:Content>
