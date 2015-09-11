<%@ Page Title="" Language="C#" MasterPageFile="~/ContentPage.master" AutoEventWireup="true"
    CodeBehind="AutoCompletePopUp.aspx.cs" Inherits="Hulcher.OneSource.CustomerService.Web.AutoCompletePopUp" %>
<%@ MasterType TypeName="Hulcher.OneSource.CustomerService.Web.ContentPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function updateParentPage(controlId, controlName, controlHF, valueId, valueName, parentFieldId) {
            var objId = window.opener.document.getElementById(controlId);
            var objName = window.opener.document.getElementById(controlName);
            var objName = window.opener.$find(controlName);
            var objHF = window.opener.document.getElementById(controlHF);

            objId.value = valueId;
            objName.value = valueName;
            objName.raiseItemSelected(new Sys.Extended.UI.AutoCompleteItemEventArgs(null, valueName, valueId));
            objHF.value = valueName;

            if (parentFieldId)
                window.opener.__doPostBack(parentFieldId, '');

            
            window.close();
        }

        var TRange = null

        function findString() {
            var strFound;

            var str = document.getElementById('<%= txtFind.ClientID %>').value;
            var grid = document.getElementById('<%= gvList.ClientID %>');
            var scrollbefore;

            if (TRange != null) {
                TRange.collapse(false)
                strFound = TRange.findText(str)
                if (strFound) {
                    scrollbefore = grid.offsetParent.scrollTop;
                    TRange.select()

                    if (grid.offsetParent.scrollTop < scrollbefore)
                        grid.offsetParent.scrollTop = grid.offsetParent.scrollTop - 24

                }
            }
            if (TRange == null || strFound == 0) {
                TRange = self.document.body.createTextRange()
                strFound = TRange.findText(str)
                if (strFound) {
                    scrollbefore = grid.offsetParent.scrollTop;
                    TRange.select()

                    if (grid.offsetParent.scrollTop < scrollbefore)
                        grid.offsetParent.scrollTop = grid.offsetParent.scrollTop - 24
                }
            }

            if (!strFound)
                alert("'" + str + "' not found!")

            return false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <asp:Panel DefaultButton="btnFind" ID="pnlMain" runat="server">
        <asp:TextBox ID="txtFind" runat="server" CssClass="input"></asp:TextBox>&nbsp;&nbsp;<asp:Button ID="btnFind"
            Text="Find" runat="server" CssClass="btn" OnClientClick="return findString();" /><br />
        <asp:ScrollableGridView ID="gvList" runat="server" EnableViewState="false" MaxHeight="500">
        </asp:ScrollableGridView>
    </asp:Panel>

    <asp:AutoCompleteExtender runat="server" ID="aceCustomer" ServicePath="~/WebService/AutoCompleteWebService.svc" ServiceMethod="GetEmployeeList" CompletionSetCount="10" MinimumPrefixLength="3" FirstRowSelected="true" TargetControlID="txt" >
    </asp:AutoCompleteExtender>
    <asp:TextBox ID="txt" runat="server" style="display: none;" />
</asp:Content>
