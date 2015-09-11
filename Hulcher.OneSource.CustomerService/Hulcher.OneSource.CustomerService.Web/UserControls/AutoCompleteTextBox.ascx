<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AutoCompleteTextBox.ascx.cs"
    Inherits="Hulcher.OneSource.CustomerService.Web.UserControls.AutoCompleteTextBox" %>
<div id="dvBody" runat="server" style="display: inline">
    <asp:TextBox runat="server" ID="textControl" CssClass="input"></asp:TextBox>&nbsp;
    <asp:ImageButton ID="imgButton" runat="server" ImageUrl="~/Images/Search.png" OnClick="imgButton_Click"
        CausesValidation="false" AlternateText="Search" />
    <asp:AutoCompleteExtender runat="server" ID="aceCustomer" ServicePath="~/WebService/AutoCompleteWebService.svc"
        CompletionSetCount="10" FirstRowSelected="true" CompletionListCssClass="autocomplete_list">
    </asp:AutoCompleteExtender>
    <asp:TextBox ID="hfValue" runat="server" Style="display: none" Text="0" />
    <asp:RequiredFieldValidator ID="rfvSelectedValue" runat="server" InitialValue="0"
        Display="Dynamic" Text="*"></asp:RequiredFieldValidator>
    <asp:Button ID="btnFake" runat="server" Style="display: none" />
    <asp:HiddenField ID="hfFilter" runat="server" Value="0" />
    <asp:HiddenField ID="hfTitle" runat="server" Value="-" />
    <asp:HiddenField ID="hfItemSelected" runat="server" />
    <asp:HiddenField ID="hfLastValue" runat="server" />
    <asp:HiddenField ID="hfLastText" runat="server" />
    <asp:HiddenField ID="hfBlur" runat="server" />
    <script type="text/javascript">
        //Instance of ScripManager
        var scriptManager = Sys.WebForms.PageRequestManager.getInstance();

        $(document).ready(
            function () {


                // Tooltip only Text
                $('#<%= textControl.ClientID %>').hover(function () {
                    
                    // Hover over code
                    var title = $(this).val();

                    var pValue = $('#<%= hfValue.ClientID %>').val();

                    if (pValue == '0')
                        title = ' - ';

                    $(this).data('tipText', title).removeAttr('title');
                    $('<p class="tooltip"></p>')
                .text(title)
                .appendTo('body')
                .fadeIn('slow');
                }, function () {
                    // Hover out code
                    //$(this).val($(this).data('tipText'));
                    $('.tooltip').remove();
                }).mousemove(function (e) {
                    var mousex = e.pageX + 20; //Get X coordinates
                    var mousey = e.pageY + 10; //Get Y coordinates
                    $('.tooltip')
                .css({ top: mousey, left: mousex })

                });



                var divBody = document.getElementById('<%= dvBody.ClientID %>')

                if (null != divBody) {
                    divBody.EnableAutocomplete =
                    function (enabled) {
                        var autoComplete = $find('<%= aceCustomer.BehaviorID %>');
                        var textCtrl = document.getElementById('<%= textControl.ClientID %>');
                        var validator = $get('<%= rfvSelectedValue.ClientID %>')
                        var hidValue = document.getElementById('<%=this.HiddenFieldValueClientID %>');

                        if (null != autoComplete && null != textCtrl && null != validator && null != hidValue) {
                            textCtrl.disabled = !enabled;
                            ValidatorEnable(validator, enabled);

                            if (enabled == false) {
                                if (hidValue.value != 0)
                                    autoComplete.raiseItemSelected(new Sys.Extended.UI.AutoCompleteItemEventArgs(null, '', '0'));
                                else if (textCtrl.value != '')
                                    textCtrl.value = '';
                            }
                        }
                    }
                }

            }
        );

        var scriptManager = Sys.WebForms.PageRequestManager.getInstance();

        scriptManager.add_endRequest(
            function () {

                // Tooltip only Text
                $('#<%= textControl.ClientID %>').hover(function () {
                    
                    // Hover over code
                    var title = $(this).val();

                    var pValue = $('#<%= hfValue.ClientID %>').val();

                    if (pValue == '0')
                        title = ' - ';

                    $(this).data('tipText', title).removeAttr('title');
                    $('<p class="tooltip"></p>')
                .text(title)
                .appendTo('body')
                .fadeIn('slow');
                }, function () {
                    // Hover out code
                    //$(this).val($(this).data('tipText'));
                    $('.tooltip').remove();
                }).mousemove(function (e) {
                    var mousex = e.pageX + 20; //Get X coordinates
                    var mousey = e.pageY + 10; //Get Y coordinates
                    $('.tooltip')
                .css({ top: mousey, left: mousex })

                });


                var divBody = document.getElementById('<%= dvBody.ClientID %>')

                if (null != divBody) {
                    divBody.EnableAutocomplete =
                    function (enabled) {
                        var autoComplete = $find('<%= aceCustomer.BehaviorID %>');
                        var textCtrl = document.getElementById('<%= textControl.ClientID %>');
                        var validator = $get('<%= rfvSelectedValue.ClientID %>')
                        var hidValue = document.getElementById('<%=this.HiddenFieldValueClientID %>');

                        if (null != autoComplete && null != textCtrl && null != validator && null != hidValue) {
                            textCtrl.disabled = !enabled;
                            ValidatorEnable(validator, enabled);

                            if (enabled == false) {
                                if (hidValue.value != 0)
                                    autoComplete.raiseItemSelected(new Sys.Extended.UI.AutoCompleteItemEventArgs(null, '', '0'));
                                else if (textCtrl.value != '')
                                    textCtrl.value = '';
                            }
                        }
                    }
                }

            }
        );

    </script>
</div>
