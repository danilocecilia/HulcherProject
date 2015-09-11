<%@ Page Language="C#" MasterPageFile="~/ContentPage.master" AutoEventWireup="true" CodeBehind="ContractDetail.aspx.cs" Inherits="Hulcher.OneSource.CustomerService.Web.ContractDetail" %>
<%@ MasterType TypeName="Hulcher.OneSource.CustomerService.Web.ContentPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" type="text/css" href="/Styles/ContractDetail.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="Header" id="trHeader" runat="server" style="padding: 5px;">
        <asp:Label ID="lblHeader" runat="server" Text="Contract Details" />
    </div>
    <asp:Panel ID="trContent" runat="server" CssClass="Content" style="padding: 5px;">
        <div class="ContractDetail">
            <div class="Label"><asp:Label ID="lblCustomerLabel" runat="server" Text="Company:" /></div>
            <div class="Form"><asp:Label ID="lblCustomer" runat="server" Text=" " /></div>
        </div><br />
        <div class="ContractDetail">
            <div class="Label"><asp:Label ID="lblNumberLabel" runat="server" Text="Contract Number:" /></div>
            <div class="Form"><asp:Label ID="lblNumber" runat="server" Text=" " /></div>
        </div><br />
        <div class="ContractDetail">
            <div class="Label"><asp:Label ID="lblDescriptionLabel" runat="server" Text="Description:" /></div>
            <div class="Form"><asp:Label ID="lblDescription" runat="server" Text=" " /></div>
        </div><br />
        <div class="ContractDetail">
            <div class="Label"><asp:Label ID="lblAdditionalDetailsLabel" runat="server" Text="Additional Details:" /></div>
            <div class="Form"><asp:Label ID="lblAdditionalDetails" runat="server" Text=" " /></div>
        </div><br />
        <div class="ContractDetail">
            <div class="Label"><asp:Label ID="lblStartDateLabel" runat="server" Text="Start Date:" /></div>
            <div class="Form"><asp:Label ID="lblStartDate" runat="server" Text=" " /></div>
        </div><br />
        <div class="ContractDetail">
            <div class="Label"><asp:Label ID="lblEndDateLabel" runat="server" Text="End Date:" /></div>
            <div class="Form"><asp:Label ID="lblEndDate" runat="server" Text=" " /></div>
        </div><br />
    </asp:Panel>
</asp:Content>
