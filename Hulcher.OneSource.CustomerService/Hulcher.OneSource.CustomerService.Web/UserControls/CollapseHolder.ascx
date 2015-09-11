<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollapseHolder.ascx.cs"
    Inherits="Hulcher.OneSource.CustomerService.Web.UserControls.CollapseHolder" %>

<div id="dvBody" runat="server">
    <div class="Header" id="trHeader" runat="server">
        <table cellpadding="2" cellspacing="0">
            <tr>
                <td>
                    <asp:Image runat="server" ID="ImageHeader" />
                    <input type="hidden" id="hfExpand" runat="server" class="ImagemExpand" value="/Images/CollapseHolder/Expand.gif" />
                    <input type="hidden" id="hfCollapse" runat="server" class="ImagemCollapse" value="/Images/CollapseHolder/Collapse.gif" />
                    <input type="hidden" id="hfLastState" runat="server" class="LastState" />
                </td>
                <td>
                    <asp:PlaceHolder ID="HeaderHolder" runat="server"></asp:PlaceHolder>
                </td>
            </tr>
        </table>
    </div>
    <asp:Panel ID="trContent" runat="server" CssClass="Content" >
        <asp:PlaceHolder ID="ContentHolder" runat="server"></asp:PlaceHolder>
    </asp:Panel>
    <asp:CollapsiblePanelExtender ID="clpExtender" Collapsed="true" runat="server" EnableViewState="true"
        ImageControlID="ImageHeader" SuppressPostBack="true" ViewStateMode="Enabled"
        TargetControlID="trContent" CollapseControlID="trHeader" ExpandControlID="trHeader"
        ExpandDirection="Vertical">
    </asp:CollapsiblePanelExtender>
    <asp:HiddenField ID="hfCollapsed" runat="server" Value="0" />
</div>