<%@ Page Title="SubContractor Maintenance" Language="C#" MasterPageFile="~/ContentPage.master" AutoEventWireup="true"
    CodeBehind="SubContractorMaintenance.aspx.cs" Inherits="Hulcher.OneSource.CustomerService.Web.SubContractorMaintenance" %>

<%@ MasterType TypeName="Hulcher.OneSource.CustomerService.Web.ContentPage" %>
<%@ Register Assembly="Hulcher.OneSource.CustomerService.Business" Namespace="Hulcher.OneSource.CustomerService.Business.WebControls.ServerControls"
    TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Styles/Forms.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <asp:UpdatePanel ID="upPage" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
        <ContentTemplate>
            <asp:Label ID="lblTitle" runat="server" Text="SubContractor Maintenance" Font-Size="Large" Font-Bold="true"></asp:Label>
            <br />
            <br />
            <asp:Panel ID="pnlNoAccess" runat="server" Visible="false">
                <div>
                    <div>
                        <asp:Label ID="lblTitleNoAccess" runat="server" Text="Equipment Maintenance Details" />
                    </div>
                    <div>
                        The current user does not have access to this functionality!
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlVisualization" runat="server">
                <div id="divVisualization" class="Header">
                    <asp:Label ID="lblVisualizationTitle" runat="server" Text="Subcontractor Viewer"></asp:Label>
                </div>
                <div class="Content">
                 <asp:Button ID="btnCreate" runat="server" Text="New Subcontractor" CssClass="btn" OnClick="btnCreate_Click" />
                    <asp:ScrollableGridView ID="gvSubContractor" runat="server" CssClass="ScrollableGridView"
                        AutoGenerateColumns="false" ShowFooter="false" OnRowCommand="gvSubContractor_RowCommand" OnRowDataBound="gvSubContractor_RowDataBound">
                        <Columns>
                            <asp:BoundField HeaderText="" DataField="ID" Visible="false" />
                            <asp:BoundField HeaderText="Name" DataField="Name" />
                            <asp:CompositeBoundField HeaderText="Created By" DataField="CreatedBy" />
                            <asp:BoundField HeaderText="Creation Date" DataField="CreationDate" DataFormatString="{0:MM/dd/yyyy HH:mm}" />
                            <asp:CompositeBoundField HeaderText="Modified By" DataField="ModifiedBy" />
                            <asp:BoundField HeaderText="Modification Date" DataField="ModificationDate" DataFormatString="{0:MM/dd/yyyy HH:mm}" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="EditSubcontractor" CommandArgument='<%# Eval("ID") %>' CausesValidation="false"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkRemove" runat="server" Text="Remove" CommandName="RemoveSubcontractor"
                                        CommandArgument='<%# Eval("ID") %>' CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete this subcontractor?')"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:ScrollableGridView>
                </div>
            </asp:Panel>
            <br />
            <asp:ValidationSummary ID="vsSubContractors" runat="server" CssClass="errorbox" HeaderText="Please correct the following information" ValidationGroup="SubContractor" />
            <asp:Panel ID="pnlCreation" runat="server" Visible="true">
                <div id="divCreation" class="Header">
                    <asp:Label ID="lblCreationTitle" runat="server" Text="Edit Subcontractor"></asp:Label>
                </div>
                <div class="Content">
                    <div>
                        <asp:Label ID="lblSubcontractorName" runat="server" Text="Subcontractor: "></asp:Label>
                    </div>
                    <div>
                        <asp:TextBox ID="txtSubcontractorName" runat="server" CssClass="input" MaxLength="50" ValidationGroup="SubContractor"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvSubContractorName" Display="Dynamic" Text="*" ErrorMessage="The SubContractor field is required." runat="server" ValidationGroup="SubContractor" ControlToValidate="txtSubcontractorName"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="inlineBlock alignRight space100">
                    <br />
                    <asp:Button ID="btnSaveContinue" runat="server" Text="Save & Continue" OnClick="btnSaveContinue_Click" CausesValidation="true" CssClass="btn" ValidationGroup="SubContractor" />
                    <asp:Button ID="btnSaveClose" runat="server" Text="Save & Close" OnClick="btnSaveClose_Click" CssClass="btn" CausesValidation="true" ValidationGroup="SubContractor"/>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn" CausesValidation="false" OnClick="btnCancel_Click" />
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
