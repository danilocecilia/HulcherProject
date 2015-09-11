<%@ Page Language="C#" MasterPageFile="~/DefaultPage.master" AutoEventWireup="true"
    CodeBehind="SamplePage.aspx.cs" Inherits="Hulcher.OneSource.CustomerService.Web.SamplePage" %>

<%@ Register Assembly="Hulcher.OneSource.CustomerService.Business" Namespace="Hulcher.OneSource.CustomerService.Business.WebControls.ServerControls"
    TagPrefix="cc1" %>
<%@ MasterType TypeName="Hulcher.OneSource.CustomerService.Web.DefaultPage" %>
<%@ Register Src="UserControls/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<%@ Register Src="UserControls/CollapseHolder.ascx" TagName="CollapseHolder" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <asp:UpdatePanel ID="updGeneral" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <uc2:CollapseHolder ID="clhGrid" runat="server" InitialState="Expanded" >
                <Header>
                    Job Listing
                </Header>
                <Content>
                    <asp:Button ID="btnRefresh" runat="server" CausesValidation="false" Text="Refresh"
                        CssClass="btn" OnClick="btnRefresh_Click" />
                    <asp:UpdatePanel ID="uppDefault" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <cc1:ScrollableGridView ID="grdJobs" runat="server" OnRowDataBound="grdJobs_RowDataBound"
                                OnRowCommand="grdJobs_RowCommand" MaxHeight="330">
                                <Columns>
                                    <asp:BoundField HeaderText="Job Number" />
                                    <asp:BoundField HeaderText="Company " />
                                    <asp:BoundField HeaderText="Modified By" DataField="ModifiedBy" />
                                    <asp:BoundField HeaderText="Last Modification" DataField="ModificationDate" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hypOpen" runat="server" Text="Open" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CommandName="DeleteItem"
                                                CommandArgument='<%# Bind("Id") %>' OnClientClick="return confirm('Are you sure you want to delete this item?');" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </cc1:ScrollableGridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </Content>
            </uc2:CollapseHolder>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
