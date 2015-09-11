<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PermitInfo.ascx.cs" Inherits="Hulcher.OneSource.CustomerService.Web.UserControls.JobRecord.PermitInfo" %>
<%@ Register Assembly="Hulcher.OneSource.CustomerService.Business" Namespace="Hulcher.OneSource.CustomerService.Business.WebControls.ServerControls" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<script type="text/javascript" language="javascript" defer="defer">
    function PermitInfoUploadError(sender, args) {
        var errMsg = args.get_errorMessage();
        if (errMsg.indexOf("Acesso negado.") >= 0 || errMsg.indexOf("Access is denied.") >= 0)
            alert('<%=Hulcher.OneSource.CustomerService.Core.Globals.JobRecord.UploadFileSizeMessage %>');
        else
            alert(args.get_errorMessage());
    }
</script>
<asp:UpdatePanel ID="updPermitInfo" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:ValidationSummary ID="vsmPermitInfo" CssClass="errorbox" runat="server" ValidationGroup="PermitInfo" HeaderText="Please correct the following information" />
        <div class="PermitForm">
            <div class="Label"><asp:Label ID="lblPermitType" runat="server" Text="* Permit Type" /></div>
            <div class="Form">
                <asp:DropDownList ID="cbPermitType" runat="server" Width="157px" ValidationGroup="PermitInfo"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvPermitType" runat="server" ControlToValidate="cbPermitType" Display="Dynamic" ErrorMessage="The Permit Type field is required" ValidationGroup="PermitInfo" Text="*"/>
            </div>
            <div class="Label"><asp:Label ID="lblPermitNumber" runat="server" Text="* Permit Number" /></div>
            <div class="Form">
                <asp:TextBox ID="txtPermitNumber" runat="server" Width="150px" ValidationGroup="PermitInfo" MaxLength="50"  CssClass="input"/>
                <asp:RequiredFieldValidator ID="rfvPermitNumber" runat="server" ControlToValidate="txtPermitNumber" Display="Dynamic" ErrorMessage="The Permit Number field is required" ValidationGroup="PermitInfo" Text="*" />
            </div>
            <div class="Label"><asp:Label ID="lblPermitLocation" runat="server" Text="* Permit Location" /></div>
            <div class="Form">
                <asp:TextBox ID="txtPermitLocation" runat="server" Width="150px" ValidationGroup="PermitInfo" MaxLength="50" CssClass="input"/>
                <asp:RequiredFieldValidator ID="rfvPermitLocation" runat="server" ControlToValidate="txtPermitLocation" Display="Dynamic" ErrorMessage="The Permit Location field is required" ValidationGroup="PermitInfo" Text="*" />
            </div>
            <div class="Label"><asp:Label ID="lblAgencyOperator" runat="server" Text="Agency/Operator" /></div>
            <div class="Form">
                <asp:TextBox ID="txtAgencyOperator" runat="server" Width="150px" MaxLength="50" CssClass="input"/>
            </div>
            <div class="Label"><asp:Label ID="lblAgentOperator" runat="server" Text="Agent/Operator Name" /></div>
            <div class="Form">
                <asp:TextBox ID="txtAgentOperator" runat="server" Width="150px" MaxLength="50" CssClass="input"/>
            </div>
            <div class="Label"><asp:Label ID="lblPermitDate" runat="server" Text="Permit Date" /></div>
            <div class="Form">
                <uc1:DatePicker InvalidValueMessage="The Permit Date format is invalid" DateTimeFormat="Default" ID="dpPermitDate" 
                    ShowOn="Both" runat="server" IsValidEmpty="true">
                </uc1:DatePicker>
            </div>
            <div class="Label"><asp:Label ID="lblAttachFile" runat="server" Text="Attach File" /></div>
            <div class="Form">
                <asp:AsyncFileUpload ID="afuAttach" runat="server" BackColor="White" CompleteBackColor="White" ErrorBackColor="White"  
                    UploaderStyle="Traditional" UploadingBackColor="White" Width="350px" 
                    ThrobberID="imgUploading" onuploadedcomplete="afuAttach_UploadedComplete" 
                    onuploadedfileerror="afuAttach_UploadedFileError" ClientIDMode="AutoID" OnClientUploadError="PermitInfoUploadError" />&nbsp;
                <asp:Image ID="imgUploading" runat="server" ImageUrl="~/Images/uploading.gif" AlternateText="Uploading file..." Width="16" Height="16"/>
            </div>
            <div class="Label"></div>
            <div class="Form"><asp:Button ID="btnAdd" runat="server" Text="Add" Width="50px" CssClass="btn" onclick="btnAdd_Click" ValidationGroup="PermitInfo" OnClientClick="UpdateScrollableGridViewHeader('ScrollableGridView')" /></div>
        </div>
        <div>
            <cc1:ScrollableGridView runat="server" ID="grdPermitInfo" OnRowDataBound="grdPermitInfo_RowDataBound" OnRowCommand="grdPermitInfo_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Permit Type">
                        <ItemTemplate>
                            <asp:Label ID="lblType" runat="server" Text=<%# Eval("CS_JobPermitType.Description") %> ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Permit Number" DataField="Number" />
                    <asp:BoundField HeaderText="Permit Location" DataField="Location" />
                    <asp:BoundField HeaderText="Agency/Operator" DataField="AgencyOperator" />
                    <asp:BoundField HeaderText="Agent/Operator Name" DataField="AgentOperatorName" />
                    <asp:BoundField HeaderText="Permit Date" DataField="PermitDate" DataFormatString="{0:MM/dd/yyyy}" />
                    <asp:BoundField HeaderText="Created Date" DataField="CreationDate" DataFormatString="{0:MM/dd/yyyy}" />
                    <asp:BoundField HeaderText="Creating User" DataField="CreatedBy" />
                    <asp:TemplateField HeaderText="Attachments" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:HyperLink ID="hypAttachment" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkRemove" runat="server" Text="Remove" CommandName="Remove" CausesValidation="false" OnClientClick="ignoreDirty(); return confirm('Are you sure you want to remove this item from list?')" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </cc1:ScrollableGridView>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>