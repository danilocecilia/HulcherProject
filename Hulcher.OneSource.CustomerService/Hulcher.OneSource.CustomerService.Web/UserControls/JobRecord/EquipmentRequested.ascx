<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EquipmentRequested.ascx.cs"
    Inherits="Hulcher.OneSource.CustomerService.Web.UserControls.JobRecord.EquipmentRequested" %>
<%@ Register Src="~/UserControls/AutoCompleteTextbox.ascx" TagName="AutoCompleteTextbox"
    TagPrefix="uc1" %>
<asp:HiddenField ID="hidEquipmentRequested" runat="server" />
<asp:ValidationSummary ID="vsEquipmentRequest" runat="server" CssClass="errorbox"
                        HeaderText="Please correct the following information" ValidationGroup="Add" />
<asp:Panel ID="pnlDisplay" runat="server" DefaultButton="btnAdd">
    <div class="inlineBlock space100">
        <div class="floatLeft space20 inlineBlock">
            <div class="floatLeft space100 alignLeft paddingBottom5">
                <asp:Label ID="lblEquipmentType" runat="server" Text="Equipment Type:"></asp:Label>
            </div>
            <div class="floatLeft space100 alignLeft paddingBottom5">
                <uc1:AutoCompleteTextbox ID="actEquipmentType" runat="server" GridViewButtonImageUrl="~/Images/money.png"
                    GridViewIdName="ID" MinimumPrefixLength="3" DisplayField="Name" AutoPostBack="false"
                    RequiredField="true" ErrorMessage="Equipment Request - The Equipment Type field is required." ValidationGroup="Add" WindowTitle="Job Record - Find Equipment Type" AutoCompleteSource="LocalEquipmentType"
                    ColumnHeaderList="EquipmentType" ColumnValueList="Name" ServiceMethod="GetLocalEquipmentTypeList"
                    TextBoxCssClass="input"  />
            </div>
        </div>
        <div class="floatLeft space20 inlineBlock" id="divSpecify" style="display: none;">
            <div class="floatLeft space100 alignLeft paddingBottom5">
                <asp:Label ID="lblSpecify" runat="server" Text="Specify Equipment Type:"></asp:Label>
            </div>
            <div class="floatLeft space100 alignLeft paddingBottom5">
                <asp:TextBox ID="txtSpecifyEquipmentType" runat="server" />
                <asp:RequiredFieldValidator ID="rfvSpecifiedEquip" runat="server" ControlToValidate="txtSpecifyEquipmentType"
                    Display="Dynamic" ErrorMessage="Equipment Request - The Specify Equipment Type field is required."
                    Text="*" ValidationGroup="Add" Enabled="false" />
            </div>
        </div>
        <div class="floatLeft space20 inlineBlock">
            <div class="floatLeft space100 alignLeft paddingBottom5">
                <asp:Label ID="lblQTY" runat="server" Text="QTY:"></asp:Label>
            </div>
            <div class="floatLeft space100 alignLeft paddingBottom5">
                <asp:TextBox ID="txtQuatity" runat="server" MaxLength="4" CssClass="input" />
                <asp:RequiredFieldValidator ID="rfvQTY" runat="server" ControlToValidate="txtQuatity"
                    Display="Dynamic" ErrorMessage="Equipment Request - The QTY field is required."
                    Text="*" ValidationGroup="Add"  />
            </div>
        </div>
        <div class="floatLeft space20 inlineBlock" style="margin-top: 22px;">
            <div class="floatLeft space100 alignLeft">
                <asp:Button ID="btnAdd" runat="server" CssClass="btn" Text="Add" OnClientClick="if(Page_ClientValidate('Add')){ AddSelected(); } else { CreateTable(); this.focus(); } return false; " ValidationGroup="Add" />
            </div>
        </div>
    </div>
    <div class="inlineBlock space100">
        <div class="space60 floatLeft">
            <div id="tbEquipments_Group" class="ScrollableGridView_Group" style="width: 100%">
                <div id="tbEquipments_HeaderDiv" class="ScrollableGridView_HeaderDiv" style="min-width: 150px;">
                </div>
                <div id="tbEquipments_ScrollDiv" class="ScrollableGridView_ScrollDiv" style="max-height: 300px;
                    min-width: 400px;">
                    <table id="tbEquipments" class="ScrollableGridView" cellspacing="1">
                        <thead>
                            <tr style="position: relative; top: expression(this.offsetParent.scrollTop -1); left: expression(this.offsetParent.style.left);">
                                <th style="display: none;">
                                </th>
                                <th>
                                    <asp:Label ID="header1" CssClass="MarginRight" runat="server" Text="Equipment Requested"></asp:Label>
                                </th>
                                <th>
                                    <asp:Label ID="header2" CssClass="MarginRight" runat="server" Text="QTY"></asp:Label>
                                </th>
                                <th id="thRemove" runat="server" class="header">
                                </th>
                                <th style="display: none;">
                                </th>
                                <th style="display: none;">
                                </th>
                                <th style="display: none;">
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <asp:TextBox ID="txtValidateQuantity" runat="server" Style="display: none;" />
    <asp:RequiredFieldValidator ID="rfvValidateQuantity" runat="server" ControlToValidate="txtValidateQuantity"
        ErrorMessage="Please add all quantity values for the Equipment Requested list" />
</asp:Panel>
