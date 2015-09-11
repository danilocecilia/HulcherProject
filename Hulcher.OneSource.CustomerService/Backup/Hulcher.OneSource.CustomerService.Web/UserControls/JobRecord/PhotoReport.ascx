<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PhotoReport.ascx.cs" Inherits="Hulcher.OneSource.CustomerService.Web.UserControls.JobRecord.PhotoReport" %>
<script type="text/javascript" language="javascript" defer="defer">
    function PhotoReportUploadStarted(sender, args) {
        
        var filename = args.get_fileName();
//        var filext = filename.substring(filename.lastIndexOf(".") + 1);
//        if (filext == "png" || filext == "bmp" || filext == "jpg" || filext == "jpeg" || filext == "tif" || filext == "tiff" || filext == "gif") {
            $get("<%= btnAdd.ClientID %>").disabled = true;
            return true;
//        }
//        else {
//            var err = new Error();
//            err.name = 'Error uploading file';
//            err.message = 'Only files with extension .png, .bmp, .jpg, .tiff or .gif are allowed';
//            throw (err);
//            
//            return false;
//        }
    }

    function PhotoReportUploadError(sender, args) {
        var errMsg = args.get_errorMessage();
        if (errMsg.indexOf("Acesso negado.") >= 0 || errMsg.indexOf("Access is denied.") >= 0)
            alert('<%=Hulcher.OneSource.CustomerService.Core.Globals.JobRecord.UploadFileSizeMessage %>');
        else
            alert(args.get_errorMessage());
    }

    function PhotoReportUploadComplete(sender, args) {
        $get("<%= btnAdd.ClientID %>").disabled = false;
        $get("<%=txtAttach.ClientID %>").value = "OK";
    }
</script>
<asp:UpdatePanel ID="updPhotoReport" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:ValidationSummary ID="vsmPhotoReport" runat="server" ValidationGroup="PhotoReport" HeaderText="Please correct the following information" CssClass="errorbox" />
        <div class="PhotoReportForm">
            <div class="Label"><asp:Label ID="lblFileName" runat="server" Text="* File Name" /></div>
            <div class="Form">
                <asp:AsyncFileUpload ID="afuAttach" runat="server" BackColor="White" CompleteBackColor="White" ErrorBackColor="White"  
                    UploaderStyle="Traditional" UploadingBackColor="White" Width="350px" 
                    ThrobberID="imgUploading" onuploadedcomplete="afuAttach_UploadedComplete" 
                    onuploadedfileerror="afuAttach_UploadedFileError" ClientIDMode="AutoID" OnClientUploadStarted="PhotoReportUploadStarted" OnClientUploadComplete="PhotoReportUploadComplete" OnClientUploadError="PhotoReportUploadError" />&nbsp;
                <asp:Image ID="imgUploading" runat="server" ImageUrl="~/Images/uploading.gif" AlternateText="Uploading file..." Width="16" Height="16" />
                <asp:TextBox ID="txtAttach" runat="server" ValidationGroup="PhotoReport" style="display: none;" />
                <asp:RequiredFieldValidator ID="rfvAttach" runat="server" ControlToValidate="txtAttach" Display="Dynamic" ErrorMessage="The File Name field is required" SetFocusOnError="true" ValidationGroup="PhotoReport" Text="*" />
            </div>
            <div class="Label"><asp:Label ID="lblDescription" runat="server" Text="Description" /></div>
            <div class="Form">
                <asp:TextBox ID="txtDescription" runat="server" Width="300px" ValidationGroup="PhotoReport" MaxLength="255" CssClass="input"/>
            </div>
            <div class="Label"></div>
            <div class="Form"><asp:Button ID="btnAdd" runat="server" Text="Add" Width="50px" CssClass="btn" OnClick="btnAdd_Click" ValidationGroup="PhotoReport" CausesValidation="true" OnClientClick=" if(!Page_ClientValidate('PhotoReport')){ this.focus(); } UpdateScrollableGridViewHeader('ScrollableGridView');" /></div>
        </div>
        <div>
            <asp:ScrollableGridView runat="server" ID="grdPhotoReport" OnRowDataBound="grdPhotoReport_RowDataBound" OnRowCommand="grdPhotoReport_RowCommand">
                <Columns>
                    <asp:BoundField HeaderText="File" DataField="FileName" />
                    <asp:BoundField HeaderText="Description" DataField="Description" />
                    <asp:BoundField HeaderText="Added By" DataField="CreatedBy" />
                    <asp:BoundField HeaderText="Date Added" DataField="CreationDate" />
                    <asp:BoundField HeaderText="Time Added" DataField="CreationDate" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:HyperLink ID="hypAttachment" runat="server" Text="View" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkRemove" runat="server" Text="Remove" CommandName="Remove" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to remove this item from list?')" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:ScrollableGridView>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>