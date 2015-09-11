<%@ Page Title="" Language="C#" MasterPageFile="~/ReportMasterPage.Master" AutoEventWireup="true"
    CodeBehind="FirstAlertReport.aspx.cs" Inherits="Hulcher.OneSource.CustomerService.Web.FirstAlertReport" %>

<%@ MasterType TypeName="Hulcher.OneSource.CustomerService.Web.ReportMasterPage" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Styles/ReportView.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="PageContent" ContentPlaceHolderID="Content" runat="server">
    <div style="width: 100%">
        <div class="mainTitle">
            <asp:Label ID="lblMainTitle" runat="server" Font-Size="Large" Font-Bold="true" Text="First Alert Report" />
        </div>
    </div>
    <hr />
</asp:Content>
