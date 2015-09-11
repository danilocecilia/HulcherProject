<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Email.aspx.cs" MasterPageFile="~/ContentPage.master"
    Inherits="Hulcher.OneSource.CustomerService.Web.Email" %>

<%@ MasterType TypeName="Hulcher.OneSource.CustomerService.Web.ContentPage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="HTMLEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/Email.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="PageContent" ContentPlaceHolderID="Content" runat="server">
    <asp:UpdatePanel ID="updGeneral" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="mainTitle" class="Header">
                <asp:Label ID="lblTitle" Text="Send Email" runat="server"></asp:Label>
            </div>
            <div class="Content">
                <asp:ValidationSummary ID="vsEmail" runat="server" CssClass="errorbox" ValidationGroup="Email"
                    HeaderText="Please correct the following information" />
                <asp:TextBox ID="txtHasEmail" runat="server" ValidationGroup="Email" Style="display: none;" />
                <asp:RequiredFieldValidator ID="rfvHasEmail" runat="server" ControlToValidate="txtHasEmail"
                    Display="None" ErrorMessage="At least one receipt is necessary to send the email"
                    Text="" SetFocusOnError="false" ValidationGroup="Email" />
                <div class="Row">
                    <div class="labels2">
                        <asp:Label ID="lblTo" runat="server" Text="To:"></asp:Label>
                    </div>
                    <div class="controls maxHeight">
                        <asp:CheckBoxList ID="cblTo" runat="server" RepeatDirection="Horizontal" />
                        <asp:Label ID="lblNoReceipts" runat="server" Text="No Matched Call Criteria users"
                            Visible="false" />
                    </div>
                </div>
                <div class="Row">
                    <div class="labels2">
                        <asp:Label ID="lblCc" runat="server" Text="Cc:"></asp:Label>
                    </div>
                    <div class="controls">
                        <asp:TextBox ID="txtCc" runat="server" CssClass="input" Width="100%" onblur="ValidateEmails()"></asp:TextBox>
                    </div>
                </div>
                <div class="Row">
                    <div class="labels2">
                        <asp:Label ID="lblSubject" runat="server" Text="Subject:"></asp:Label>
                        <asp:RequiredFieldValidator ID="rfvSubject" runat="server" ControlToValidate="txtSubject"
                            Display="Dynamic" ErrorMessage="Subject Field is Required" Text="*" SetFocusOnError="true"
                            ValidationGroup="Email" />
                    </div>
                    <div class="controls">
                        <asp:TextBox ID="txtSubject" runat="server" CssClass="input" Width="100%" ValidationGroup="Email"></asp:TextBox>
                    </div>
                </div>
                <div class="Row">
                    <div class="labels2">
                        <asp:Label ID="lblEditor" runat="server" Text="Additional Notes:"></asp:Label>
                    </div>
                    <div class="controls">
                        <HTMLEditor:Editor ID="editorHtmlTextArea" runat="server" Height="200px" Width="100%"
                            IgnoreTab="true" AutoFocus="true" />
                    </div>
                </div>
                <div class="Row">
                    <br />
                    <asp:Literal ID="litTable" runat="server"></asp:Literal>
                </div>
                <div style="text-align: right;">
                    <br />
                    <asp:Button ID="btnSend" Text="Send Email" CssClass="btn" OnClick="btnSend_Click"
                        runat="server" ValidationGroup="Email" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Cancel" CausesValidation="false"
                        OnClientClick="window.close(); return false;" UseSubmitBehavior="false" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function ValidateEmails() {
            var existReceipt = false;
            $(".checkBoxTo input").each(function () {
                if (this.checked)
                    existReceipt = true;
            });

            if (!existReceipt)
                if ($("#<%= txtCc.ClientID %>").attr("value") != "")
                    existReceipt = true;

        if (existReceipt)
            $("#<%= txtHasEmail.ClientID %>").attr("value", "1");
        else
            $("#<%= txtHasEmail.ClientID %>").attr("value", "");
    }

    $(document).ready(function () {
        ValidateEmails();
    });
    </script>
</asp:Content>
