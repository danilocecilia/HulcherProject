<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatePicker.ascx.cs"
    Inherits="Hulcher.OneSource.CustomerService.Web.UserControls.DatePicker" %>
<script type="text/javascript">
    function setCentury(ctrl) {
        debugger;

    }
    //txtDatePicker.Attributes.Add("onblur", "javascript:setCentury(this);");
</script>
<asp:TextBox Width="70px" ID="txtDatePicker" runat="server" CssClass="input"></asp:TextBox>
<asp:MaskedEditExtender ID="MaskedEditExtender1" ClearMaskOnLostFocus="true" TargetControlID="txtDatePicker"
    runat="server">
</asp:MaskedEditExtender>
<asp:MaskedEditValidator ID="MaskedEditValidator1" Display="Dynamic" ControlExtender="MaskedEditExtender1"
    EnableClientScript="true" ControlToValidate="txtDatePicker" runat="server" Text="*"
    EmptyValueBlurredText="*"></asp:MaskedEditValidator>
<asp:CompareValidator ID="cvDatePicker" runat="server" Visible="false" ValueToCompare="01/01/1900"
    Display="Dynamic" Type="Date" ControlToValidate="txtDatePicker" EnableClientScript="true"
    Text="*"></asp:CompareValidator>