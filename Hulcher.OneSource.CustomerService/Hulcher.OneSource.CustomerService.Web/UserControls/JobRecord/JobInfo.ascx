<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JobInfo.ascx.cs" Inherits="Hulcher.OneSource.CustomerService.Web.UserControls.JobRecord.JobInfo" %>
<%@ Register Src="~/UserControls/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<%@ Register TagName="CollapseHolder" TagPrefix="uc" Src="~/UserControls/CollapseHolder.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/AutoCompleteTextbox.ascx" TagName="AutoCompleteTextbox"
    TagPrefix="uc1" %>
<script src="../../Scripts/jquery.jqEasyCharCounter.js" type="text/javascript" defer="defer"></script>
<script src="../../Scripts/jqueryMaxLenght.js" type="text/javascript" defer="defer"></script>
<script src="../../Scripts/jobInfo.js" type="text/javascript" defer="defer"></script>
<script type="text/javascript" language="javascript" defer="defer">
    Sys.UI.DomEvent.addHandler(window, "load", function () {
        setTimeout("AddOnChangeEventPriceType('<%=ddlPriceType.ClientID %>')", 10);
        setTimeout("AddOnChangeEventJobStatus('<%=ddlJobStatus.ClientID %>')", 10);
    });
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
        setTimeout("AddOnChangeEventPriceType('<%=ddlPriceType.ClientID %>')", 10);
        setTimeout("AddOnChangeEventJobStatus('<%=ddlJobStatus.ClientID %>')", 10);
        setTimeout("AddoOnChangeInterimBill('<%=ckbInterimBill.ClientID %>')", 10);

        var selectedIndex = $find('<%=ddlPriceType.ClientID %>').get_selectedIndex();
        var priceType = $get('<%=ddlPriceType.ClientID %>').getElementsByTagName('LI')[selectedIndex].value;
        var specialRateValue = <%= Convert.ToInt32(Hulcher.OneSource.CustomerService.Core.Globals.JobRecord.PriceType.SpecialRate) %>;

        if (priceType == specialRateValue) {
            changeValidations();
        }

        selectedIndex = $find('<%=ddlJobStatus.ClientID %>').get_selectedIndex();
        var jobStatus = $get('<%=ddlJobStatus.ClientID %>').getElementsByTagName('LI')[selectedIndex].value;
        var lostJobValue = <%= Convert.ToInt32(Hulcher.OneSource.CustomerService.Core.Globals.JobRecord.JobStatus.Lost) %>;

        if (jobStatus == lostJobValue) {
            ValidatorEnable($get('<%=rfvLostJobReason.ClientID %>'), true);
        }
        else {
            ValidatorEnable($get('<%=rfvLostJobReason.ClientID %>'), false);
        }
    });

//    function PropagateDateUp() {
//            var selectedValue = document.getElementById('< %= dpDatePicker.TextBoxClientID %>').value;
//            var date = document.getElementById('< %= hidCallDateEssentialTextBoxClientID.Value %>');
//            var toValue = date.value;

//            if (date != null && selectedValue != toValue)
//                date.value = selectedValue;
//        }

//    function PropagateTimeUp() {
//            var selectedValue = document.getElementById('< %= txtInitialCallTime.ClientID %>').value;
//            var time = document.getElementById('< %= hidCallTimeEssentialTextBoxClientID.Value %>');
//            var toValue = time.value;

//            if (time != null && selectedValue != toValue)
//                time.value = selectedValue;
//        }

    function AddOnChangeEventPriceType(id) {
        $find(id).add_propertyChanged(function (sender, e) {
            if (e.get_propertyName() == 'selectedIndex') {
                var divSpecialPricing = $get('<%= divSpecialPricing.ClientID %>');
                var selectedIndex = sender.get_selectedIndex();
                var newValue = $get(id).getElementsByTagName('LI')[selectedIndex].value;
                var newText = $get(id).getElementsByTagName('LI')[selectedIndex].innerText;
                var specialRateValue = <%= Convert.ToInt32(Hulcher.OneSource.CustomerService.Core.Globals.JobRecord.PriceType.SpecialRate) %>;
                var BidRateValue = <%= Convert.ToInt32(Hulcher.OneSource.CustomerService.Core.Globals.JobRecord.PriceType.BidRate) %>;
                if (newValue == specialRateValue || newValue == BidRateValue)
                {
                    divSpecialPricing.style.display = 'inline';

                    changeValidations();
                }
                else
                {
                    divSpecialPricing.style.display = 'none';
                    DisableSpecialPricingValidation(true, true);
                }

                $get('<%= hidSpecialPricingDisplay.ClientID %>').value = divSpecialPricing.style.display;
            }
        })
    }


       function AddOnChangeEventJobStatus(id) {
        $find(id).add_propertyChanged(function (sender, e) { 
            if (e.get_propertyName() == 'selectedIndex') {
                var divPresetInfo = $get('<%= divPresetInfo.ClientID %>');
                var divLostJobInfo = $get('<%= divLostJobInfo.ClientID %>');

                var selectedIndex = sender.get_selectedIndex();
                var newValue = $get(id).getElementsByTagName('LI')[selectedIndex].value;
                var newText = $get(id).getElementsByTagName('LI')[selectedIndex].innerText;
                var activeValue = <%= Convert.ToInt32(Hulcher.OneSource.CustomerService.Core.Globals.JobRecord.JobStatus.Active) %>;
                var closedValue = <%= Convert.ToInt32(Hulcher.OneSource.CustomerService.Core.Globals.JobRecord.JobStatus.Closed) %>;
                var presetValue = <%= Convert.ToInt32(Hulcher.OneSource.CustomerService.Core.Globals.JobRecord.JobStatus.Preset) %>;
                var presetPurchaseValue = <%= Convert.ToInt32(Hulcher.OneSource.CustomerService.Core.Globals.JobRecord.JobStatus.PresetPurchase) %>;
                var lostJobValue = <%= Convert.ToInt32(Hulcher.OneSource.CustomerService.Core.Globals.JobRecord.JobStatus.Lost) %>;

                var txtJobStartDate = $get('<%= txtJobStartDate.ClientID %>');
                var txtJobCloseDate = $get('<%= txtJobCloseDate.ClientID %>');

                if (newValue == presetValue || newValue == presetPurchaseValue) {
                    divPresetInfo.style.display = 'inline';
                    divLostJobInfo.style.display = 'none';
                    var presetDate = $get('<%=dpPresetDate.CompareValidatorClientID %>');
                    var lostReasonDate = $get('<%=rfvLostJobReason.ClientID %>');

                    if (presetDate != null)
                        ValidatorEnable(presetDate, true);
                    if (lostReasonDate != null)
                        ValidatorEnable(lostReasonDate, false);
                }
                else if (newValue == lostJobValue) {
                    divPresetInfo.style.display = 'none';
                    divLostJobInfo.style.display = 'inline';

                    var presetDate = $get('<%=dpPresetDate.CompareValidatorClientID %>');
                    var lostReasonDate = $get('<%=rfvLostJobReason.ClientID %>');

                    if (presetDate != null)
                        ValidatorEnable(presetDate, false);
                    if (lostReasonDate != null)
                        ValidatorEnable(lostReasonDate, true);
                }
                else {
                    divPresetInfo.style.display = 'none';
                    divLostJobInfo.style.display = 'none';

                    var presetDate = $get('<%=dpPresetDate.CompareValidatorClientID %>');
                    var lostReasonDate = $get('<%=rfvLostJobReason.ClientID %>');

                    if (presetDate != null)
                        ValidatorEnable(presetDate, false);
                    if (lostReasonDate != null)
                        ValidatorEnable(lostReasonDate, false);
                }

                if (newValue == activeValue) {
                    txtJobStartDate.value = '<%= DateTime.Now.ToString("MM/dd/yyyy") %>';
                    txtJobCloseDate.value = '';
                }
                else if (newValue == closedValue) {
                    txtJobStartDate.value = '';
                    txtJobCloseDate.value = '<%= DateTime.Now.ToString("MM/dd/yyyy") %>';
                }
                else {
                    txtJobStartDate.value = '';
                    txtJobCloseDate.value = '';
                }

                $get('<%= hidPresetInfoDisplay.ClientID %>').value = divPresetInfo.style.display;
                $get('<%= hidLostJobInfoDisplay.ClientID %>').value = divLostJobInfo.style.display;
            }
        })
    }

    function AddoOnChangeInterimBill(id) {
        $('#' + id).change(function() {
            var valueChecked = $('#' + id).attr('checked');
            
            ValidatorEnable($get('<%=actRequestedBy.RequiredFieldClientId %>'), valueChecked);
            ValidatorEnable($get('<%=rfvFrequency.ClientID %>'), valueChecked);

            document.getElementById('<%=actRequestedBy.TextControlClientID %>').disabled = !valueChecked;
            document.getElementById('<%=actRequestedBy.ImageButtonClientID %>').disabled = !valueChecked;
            $find('<%=ddlFrequency.ClientID %>').get_textBoxControl().disabled = !valueChecked;
            $find('<%=ddlFrequency.ClientID %>').get_buttonControl().disabled = !valueChecked;

            if (!valueChecked)
            {
                $find('actRequestedBy').raiseItemSelected(new Sys.Extended.UI.AutoCompleteItemEventArgs(null, "", "0"));

                $find('<%=ddlFrequency.ClientID %>').set_selectedIndex(0);
                $find('<%=ddlFrequency.ClientID %>').set_hiddenFieldControl('0');
                $find('<%=ddlFrequency.ClientID %>').get_textBoxControl().value = '- Select One -';
            }
        });
    }

    function DisableSpecialPricingValidation(eraseOverrall, eraseLumpSum)
    {
        ValidatorEnable($get('<%=rfvOverallSpecialPricing.ClientID %>'), false);
        ValidatorEnable($get('<%=rfvLumpSumValue.ClientID %>'), false);
        ValidatorEnable($get('<%=rfvLumpSumDuration.ClientID %>'), false);
        ValidatorEnable($get('<%=rfvApprovingRVP.ClientID %>'), false);

        var overallDiscountControl = document.getElementById("<%= txtOverallSpecialPricing.ClientID %>");

        if (eraseOverrall)
            overallDiscountControl.value = "";

        overallDiscountControl.disabled = true;
        
        var lumpSumValue = document.getElementById("<%= txtLumpSumValue.ClientID %>");
        var lumpSumDuration = document.getElementById("<%= txtLumpSumDuration.ClientID %>");
        var bidNumber = document.getElementById("<%= txtBidNumber.ClientID %>");

        lumpSumValue.disabled = true;
        
        if (eraseLumpSum) {
            lumpSumValue.value = "";
            lumpSumDuration.value = "";
        }

        lumpSumDuration.disabled = true;
        
        if (eraseLumpSum && eraseOverrall)
            bidNumber.value = "";

        bidNumber.disabled = true;

        var approvingRVP = document.getElementById("<%= ddlApprovingRVP.ClientID %>");

        approvingRVP.disabled = true;
    }

    function EnableSpecialPricingOverallDiscount()
    {  
    debugger;
        DisableSpecialPricingValidation(false, true);

        var bidNumber = document.getElementById("<%= txtBidNumber.ClientID %>");
        bidNumber.disabled = false;

        var overallDiscountControl = document.getElementById("<%= txtOverallSpecialPricing.ClientID %>");

        ValidatorEnable($get('<%=rfvOverallSpecialPricing.ClientID %>'), true);
        overallDiscountControl.disabled = false;

        var approvingRVP = document.getElementById("<%= ddlApprovingRVP.ClientID %>");

        approvingRVP.disabled = false;

        ValidatorEnable($get('<%=rfvApprovingRVP.ClientID %>'), true);
    }

    function EnableSpecialPricingLumpSum()
    {
        DisableSpecialPricingValidation(true, false);

        var lumpSumValue = document.getElementById("<%= txtLumpSumValue.ClientID %>");
        var lumpSumDuration = document.getElementById("<%= txtLumpSumDuration.ClientID %>");

        ValidatorEnable($get('<%=rfvLumpSumValue.ClientID %>'), true);
        ValidatorEnable($get('<%=rfvLumpSumDuration.ClientID %>'), true);

        lumpSumValue.disabled = false;
        lumpSumDuration.disabled = false;

        var approvingRVP = document.getElementById("<%= ddlApprovingRVP.ClientID %>");

        approvingRVP.disabled = false;

        ValidatorEnable($get('<%=rfvApprovingRVP.ClientID %>'), true);
    }

    function EnableApprovingRVP()
    {
        DisableSpecialPricingValidation(true, true);
        
        var approvingRVP = document.getElementById("<%= ddlApprovingRVP.ClientID %>");

        approvingRVP.disabled = false;

        ValidatorEnable($get('<%=rfvApprovingRVP.ClientID %>'), true);
    }

    function changeValidations()
    {
        var noSpecial = document.getElementById("<%= rbNoSpecialPricing.ClientID %>");
        var overallDiscount = document.getElementById("<%= rbOverallSpecialPricing.ClientID %>");
        var lumpSum = document.getElementById("<%= rbLumpSum.ClientID %>");

        if (noSpecial.checked)
            DisableSpecialPricingValidation(true, true);
        else if (overallDiscount.checked)
            EnableSpecialPricingOverallDiscount();
        else if (lumpSum.checked)
            EnableSpecialPricingLumpSum();
        else
            EnableApprovingRVP();
    }

</script>
<asp:HiddenField ID="hidSpecialPricingDisplay" runat="server" />
<asp:HiddenField ID="hidPresetInfoDisplay" runat="server" />
<asp:HiddenField ID="hidLostJobInfoDisplay" runat="server" />
<asp:UpdatePanel ID="updPanel" UpdateMode="Conditional" runat="server">
    <ContentTemplate>
        <asp:HiddenField ID="hfCustomerSpecificInfo" runat="server" />
        <asp:TextBox ID="txtParentUpdate" runat="server" Style="visibility: hidden" OnTextChanged="LoadJobInfo" />
        <div style="display: inline-block">
            <div style="float: left;">
                <table>
                    <tr>
                        <td style="text-align: right">
                            *Initial Call Date:
                        </td>
                        <td>
                            <uc1:DatePicker IsValidEmpty="false" EmptyValueMessage="Job Info - The Initial Call Date field is required"
                                InvalidValueMessage="Job Info - Invalid date format" DateTimeFormat="Default"
                                ID="dpDatePicker" ShowOn="Both" runat="server" ValidationGroup="JobRecord"></uc1:DatePicker>
                        </td>
                        <td style="text-align: right">
                            *Job Action:
                        </td>
                        <td>
                            <uc1:AutoCompleteTextbox ID="actJobAction" runat="server" ServiceMethod="GetJobActionList"
                                GridViewButtonImageUrl="~/Images/money.png" GridViewIdName="ID" DisplayField="Description"
                                RequiredField="true" ErrorMessage="Job Info - The Job Action field is required"
                                WindowTitle="Job Record - Find Job Action" ContextKey="0" AutoCompleteSource="JobAction"
                                ColumnHeaderList="Name" ColumnValueList="Description" AutoPostBack="true" OnTextChanged="actJobAction_TextChanged" TextBoxWidth="120px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            *Initial Call Time:
                        </td>
                        <td>
                            <asp:TextBox ID="txtInitialCallTime" Width="80px" runat="server" CssClass="input"></asp:TextBox>
                            <cc1:MaskedEditExtender ID="mskInitialCallTime" TargetControlID="txtInitialCallTime"
                                runat="server" Mask="99:99" MaskType="Time" AcceptAMPM="false" UserTimeFormat="TwentyFourHour"
                                AutoComplete="true">
                            </cc1:MaskedEditExtender>
                            <cc1:MaskedEditValidator ID="rfvInitialCallTimeValidator" runat="server" ControlExtender="mskInitialCallTime"
                                ControlToValidate="txtInitialCallTime" IsValidEmpty="false" EnableClientScript="true"
                                Display="Dynamic" InvalidValueBlurredMessage="*" InvalidValueMessage="Job Info - The Initial Call Time format is invalid"
                                EmptyValueMessage="Job Info - The Initial Call Time field is required" EmptyValueBlurredText="*"></cc1:MaskedEditValidator>
                        </td>
                        <td style="text-align: right">
                            *Job Category:
                        </td>
                        <td>
                            <asp:ComboBox ID="ddlJobCategory" Width="120px" runat="server" AutoCompleteMode="SuggestAppend"
                                RenderMode="Inline" CaseSensitive="false" DropDownStyle="DropDownList" AutoPostBack="false"
                                CssClass="WindowsStyle" Enabled="false">
                            </asp:ComboBox>
                            <asp:RequiredFieldValidator ID="rfvJobCategory" runat="server" EnableClientScript="true"
                                Text="*" Display="Dynamic" ErrorMessage="Job Info - The Job Category field is required"
                                ControlToValidate="ddlJobCategory" InitialValue="- Select One - "></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            *Job Status:
                        </td>
                        <td>
                            <asp:ComboBox ID="ddlJobStatus" runat="server" AutoCompleteMode="SuggestAppend" RenderMode="Inline"
                                CaseSensitive="false" Width="120px" DropDownStyle="DropDownList" CssClass="WindowsStyle">
                            </asp:ComboBox>
                            <asp:RequiredFieldValidator ID="rfvJobStatus" runat="server" EnableClientScript="true"
                                Text="*" Display="Dynamic" ErrorMessage="Job Info - The Job Status field is required"
                                ControlToValidate="ddlJobStatus" InitialValue="- Select One - "></asp:RequiredFieldValidator>
                        </td>
                        <td style="text-align: right">
                            *Job Type:
                        </td>
                        <td>
                            <asp:ComboBox ID="ddlJobType" Width="120px" runat="server" AutoCompleteMode="SuggestAppend"
                                RenderMode="Inline" CaseSensitive="false" DropDownStyle="DropDownList" CssClass="WindowsStyle"
                                Enabled="false" />
                            <asp:RequiredFieldValidator ID="rfvJobType" runat="server" EnableClientScript="true"
                                Text="*" Display="Dynamic" ErrorMessage="Job Info - The Job Type field is required"
                                ControlToValidate="ddlJobType" InitialValue="- Select One - " />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            *Price Type:
                        </td>
                        <td>
                            <asp:ComboBox ID="ddlPriceType" runat="server" Width="120px" AutoCompleteMode="SuggestAppend"
                                RenderMode="Inline" CaseSensitive="false" DropDownStyle="DropDownList" CssClass="WindowsStyle">
                            </asp:ComboBox>
                            <asp:RequiredFieldValidator ID="rfvPriceType" runat="server" EnableClientScript="true"
                                Text="*" Display="Dynamic" ErrorMessage="Job Info - The Price Type field is required"
                                ControlToValidate="ddlPriceType" InitialValue="- Select One -"></asp:RequiredFieldValidator>
                        </td>
                        <td style="text-align: right">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            Job Start Date:
                        </td>
                        <td>
                            <asp:TextBox Width="80px" ID="txtJobStartDate" runat="server" BackColor="LightGray"
                                CssClass="input" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="text-align: right">
                            Job Close Date:
                        </td>
                        <td>
                            <asp:TextBox ID="txtJobCloseDate" Width="80px" runat="server" BackColor="LightGray"
                                CssClass="input" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            *Division:
                        </td>
                        <td>
                            <table border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <asp:ComboBox Width="80px" ID="ddlDivision" runat="server" AutoCompleteMode="SuggestAppend"
                                            RenderMode="Inline" CaseSensitive="false" DropDownStyle="DropDownList" AutoPostBack="false"
                                            CssClass="WindowsStyle">
                                        </asp:ComboBox>
                                    </td>
                                    <td style="padding-left: 2px;">
                                        <asp:Button ID="btnAddDivision" runat="server" Text="Add" Width="30px" CssClass="btn"
                                            OnClick="btnAddDivision_Click" CausesValidation="false" ValidationGroup="JobRecord" />
                                        <asp:TextBox ID="txtGridValidation" runat="server" Text="" Style="display: none" />
                                        <asp:RequiredFieldValidator ID="rfvGridValidation" runat="server" ErrorMessage="Job Info - The Division Field is required"
                                            EnableClientScript="true" ControlToValidate="txtGridValidation" Display="Dynamic"
                                            Text="*"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtGridValidation2" runat="server" Text="" Style="display: none" />
                                        <asp:RequiredFieldValidator ID="rfvGridValidation2" runat="server" ErrorMessage="Job Info - The Primary Division Field is required"
                                            EnableClientScript="true" ControlToValidate="txtGridValidation2" Display="Dynamic"
                                            Text="*"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="text-align: right">
                            Project Manager:
                        </td>
                        <td>
                             <uc1:AutoCompleteTextbox ID="acProjectManager" Width="120px" runat="server" ServiceMethod="GetProjectManagerList"
                                WindowTitle="Job Info - Project Manager" GridViewButtonImageUrl="~/Images/money.png"
                                GridViewIdName="ID" DisplayField="DivisionAndFullName" ContextKey="0" AutoPostBack="false" 
                                AutoCompleteSource="ProjectManager" ColumnHeaderList="Division - Name"
                                ColumnValueList="DivisionAndFullName" RequiredField="false" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div style="width: 380px;">
                                <asp:ScrollableGridView ID="gdvDivision" runat="server" CssClass="ScrollableGridView"
                                    AutoGenerateColumns="false" ShowFooter="false" OnRowCommand="gdvDivision_RowCommand"
                                    OnRowDataBound="gdvDivision_RowDataBound" MinWidth="360">
                                    <Columns>
                                        <asp:CompositeBoundField HeaderText="ID" DataField="CS_Division.ID" Visible="false"
                                            ItemStyle-Width="0px">
                                        </asp:CompositeBoundField>
                                        <asp:CompositeBoundField HeaderText="Name" DataField="CS_Division.ExtendedDivisionName" ItemStyle-Width="120px">
                                        </asp:CompositeBoundField>
                                        <asp:TemplateField HeaderText="Primary Division">
                                            <ItemStyle Width="120px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:RadioButton ID="radPrimaryDivision" runat="server" GroupName="PrimaryDivision" />
                                                <asp:HiddenField ID="hfDivisionId" runat="server" Value='<%# Eval("CS_Division.ID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="120px">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkRemove" runat="server" Text="Remove" CommandName="Remove"
                                                    CausesValidation="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:ScrollableGridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="float: right; width: 300px;">
                <div id="divSpecialPricing" runat="server" style="display: none">
                    <uc:CollapseHolder ID="chSpecialPricingInfo" runat="server" Collapsed="false">
                        <Header>
                            <asp:Label ID="lblSpecialPricingInfoTitle" runat="server" Text="Special Pricing Info"></asp:Label>
                        </Header>
                        <Content>
                            <div class="PermitForm">
                                <div class="Form">
                                    <asp:Label ID="lblBidNumber" runat="server" Text="Bid Number"></asp:Label>
                                    <asp:TextBox ID="txtBidNumber" runat="server" MaxLength="20" CssClass="input"></asp:TextBox>
                                </div>
                                <div class="Form">
                                    <asp:RadioButton ID="rbNoSpecialPricing" runat="server" Text="No Special Pricing"
                                        Checked="true" GroupName="SpecialPricing" onclick="DisableSpecialPricingValidation(true);" />
                                </div>
                                <div class="Form">
                                    <asp:RadioButton ID="rbOverallSpecialPricing" runat="server" Text="Overall Job Discount"
                                        Checked="false" GroupName="SpecialPricing" onclick="EnableSpecialPricingOverallDiscount()" />&nbsp;&nbsp;
                                    <asp:TextBox ID="txtOverallSpecialPricing" runat="server" Text="" Width="70px" CssClass="input"
                                        onblur="formatDecimal(this.value,this);"></asp:TextBox>
                                    <asp:Label ID="lblPercentOVP" runat="server" Text="%"></asp:Label>
                                    <asp:FilteredTextBoxExtender ID="fteOverallSpecialPricing" runat="server" FilterType="Custom"
                                        ValidChars="1234567890.," FilterMode="ValidChars" TargetControlID="txtOverallSpecialPricing">
                                    </asp:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="rfvOverallSpecialPricing" EnableClientScript="true"
                                        Enabled="false" ControlToValidate="txtOverallSpecialPricing" runat="server" ErrorMessage="Job Info - Special Pricing Info - The Overall Job Discount is required"
                                        Text="*" ValidationGroup="JobRecord"></asp:RequiredFieldValidator>
                                </div>
                                <div>
                                    <asp:RadioButton ID="rbLumpSum" runat="server" Text="Total Project Lump Sum (For Day)"
                                        Checked="false" GroupName="SpecialPricing" onclick="EnableSpecialPricingLumpSum()" />
                                </div>
                                <div class="Form" style="text-align: right">
                                    <asp:TextBox ID="txtLumpSumValue" runat="server" Text="" Width="70px" CssClass="input"
                                        onblur="formatCurrency(this.value,this);"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="fteLumpSumValue" runat="server" FilterType="Custom"
                                        ValidChars="1234567890.,$" FilterMode="ValidChars" TargetControlID="txtLumpSumValue">
                                    </asp:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="rfvLumpSumValue" EnableClientScript="true" Enabled="false"
                                        ControlToValidate="txtLumpSumValue" runat="server" ErrorMessage="Job Info - Special Pricing Info - The Lump Sum Value is required"
                                        Text="*" ValidationGroup="JobRecord"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtLumpSumDuration" runat="server" Text="" Width="40px" CssClass="input"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="fteLumpSumDuration" runat="server" FilterType="Custom"
                                        ValidChars="1234567890" FilterMode="ValidChars" TargetControlID="txtLumpSumDuration">
                                    </asp:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="rfvLumpSumDuration" EnableClientScript="true" Enabled="false"
                                        ControlToValidate="txtLumpSumDuration" runat="server" ErrorMessage="Job Info - Special Pricing Info - The Lump Sum Duration is required"
                                        Text="*" ValidationGroup="JobRecord"></asp:RequiredFieldValidator>
                                    <asp:Label ID="lblDuration" runat="server" Text="Duration"></asp:Label>
                                </div>
                                <div class="Form">
                                    <asp:RadioButton ID="rbManualCalculation" runat="server" Text="Manual Special Pricing Calculation"
                                        Checked="false" GroupName="SpecialPricing" onclick="EnableApprovingRVP()" />
                                </div>
                                <div>
                                    <asp:Label ID="lblSpecialPricingNotesLabel" runat="server" Text="Special Pricing Notes:"></asp:Label></div>
                                <div>
                                    <asp:CountableTextBox ID="txtSpecialPricingNotes" runat="server" TextMode="MultiLine"
                                        Width="250px" Height="100px" CssClass="input" MaxChars="255" MaxCharsWarning="200"></asp:CountableTextBox>
                                </div>
                                <div class="Label">
                                    <asp:Label ID="lblApprovingRVP" runat="server" Text="Approving RVP:"></asp:Label></div>
                                <div class="Form">
                                    <asp:DropDownList ID="ddlApprovingRVP" runat="server" Enabled="false">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvApprovingRVP" EnableClientScript="true" Enabled="false"
                                        ControlToValidate="ddlApprovingRVP" InitialValue="0" runat="server" ErrorMessage="Job Info - Special Pricing Info - The Approving RVP is required"
                                        Text="*" ValidationGroup="JobRecord"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </Content>
                    </uc:CollapseHolder>
                    <br />
                </div>
                <div id="divPresetInfo" runat="server" style="display: none;">
                    <uc:CollapseHolder ID="chPresetInfo" runat="server" Collapsed="false">
                        <Header>
                            <asp:Label ID="lblPresetInfoTitle" runat="server" Text="Preset Info"></asp:Label>
                        </Header>
                        <Content>
                            <table>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblPresetInstructionsTitle" Text="Preset Instructions:" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CountableTextBox ID="txtPresetInstructions" runat="server" TextMode="MultiLine"
                                            CssClass="input" Height="50px" Width="180px" MaxChars="100" MaxCharsWarning="70"></asp:CountableTextBox>
                                        <asp:RequiredFieldValidator ID="rfvPresetInstructions" runat="server" Text="*" Display="Dynamic"
                                            ErrorMessage="Job Info -Preset Info - The Preset Instructions field is required"
                                            EnableClientScript="true" ControlToValidate="txtPresetInstructions">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblPresetDateTitle" runat="server" Text="Preset Date:"></asp:Label>
                                    </td>
                                    <td>
                                        <uc1:DatePicker InvalidValueMessage="Job Info - Preset Info - The Preset Date is invalid"
                                            IsValidEmpty="true" DateTimeFormat="Default" ID="dpPresetDate" ShowOn="Both"
                                            runat="server" ValidationGroup="JobRecord" ValidationCompareOperator="GreaterThanEqual"
                                            EnableCompareValidator="true" TextCompareValidator="Job Info - Preset Info - The Preset Date must be Greater than or equal current date">
                                        </uc1:DatePicker>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblPresetTimeTitle" runat="server" Text="Preset Time:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPresetTime" runat="server" Width="50"></asp:TextBox>
                                        <cc1:MaskedEditExtender ID="mskPresetTime" TargetControlID="txtPresetTime" MessageValidatorTip="true"
                                            UserTimeFormat="TwentyFourHour" runat="server" Mask="99:99" MaskType="Time" AcceptAMPM="false">
                                        </cc1:MaskedEditExtender>
                                        <cc1:MaskedEditValidator Enabled="false" ID="mskPresetTimeValidator" runat="server"
                                            ControlExtender="mskPresetTime" ControlToValidate="txtPresetTime" IsValidEmpty="true"
                                            Display="Dynamic" EnableClientScript="true" InvalidValueMessage="Job Info - Preset Info - The Preset Time is invalid"
                                            InvalidValueBlurredMessage="*">
                                        </cc1:MaskedEditValidator>
                                    </td>
                                </tr>
                            </table>
                        </Content>
                    </uc:CollapseHolder>
                    <br />
                </div>
                <div id="divLostJobInfo" runat="server" style="display: none;">
                    <uc:CollapseHolder ID="chLostJobInfo" runat="server" Collapsed="false">
                        <Header>
                            <asp:Label ID="lblLostJobInfoTitle" runat="server" Text="Lost Job Info"></asp:Label>
                        </Header>
                        <Content>
                            <table>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblLostJobNotesTitle" runat="server" Text="Lost Job Notes:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CountableTextBox ID="txtLostJobNotes" runat="server" TextMode="MultiLine" CssClass="input"
                                            Height="50px" Width="180" MaxChars="100" MaxCharsWarning="70"></asp:CountableTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblLostJobReasonTitle" runat="server" Text="*Reason:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:ComboBox ID="ddlLostJobReason" runat="server" AutoCompleteMode="SuggestAppend"
                                            RenderMode="Inline" CaseSensitive="false" Width="80px" DropDownStyle="DropDownList"
                                            AutoPostBack="false" CssClass="WindowsStyle">
                                        </asp:ComboBox>
                                        <asp:RequiredFieldValidator ID="rfvLostJobReason" Enabled="false" runat="server"
                                            EnableClientScript="true" Text="*" Display="Dynamic" ErrorMessage="Job Info - Lost Job Info - The Reason field is required"
                                            ControlToValidate="ddlLostJobReason" InitialValue="- Select One - "></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblCompetitor" runat="server" Text="Competitor:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlCompetitor" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblPocFollowUp" runat="server" Text="Customer Contact:"></asp:Label>
                                    </td>
                                    <td>
                                        <uc1:AutoCompleteTextbox ID="actPocFollowUp" runat="server" ServiceMethod="GetEmployeeListWithDivisionName"
                                            WindowTitle="Job Record - Find Customer Contact" GridViewButtonImageUrl="~/Images/money.png"
                                            GridViewIdName="ID" DisplayField="FullName" ContextKey="0" AutoPostBack="false"
                                            AutoCompleteSource="EmployeeWithDivision" ColumnHeaderList="Division - Name"
                                            ColumnValueList="DivisionAndFullName" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblHsirep" runat="server" Text="HSI Rep:"></asp:Label>
                                    </td>
                                    <td>
                                        <uc1:AutoCompleteTextbox ID="actHsiRep" runat="server" ServiceMethod="GetEmployeeList"
                                            WindowTitle="Job Record - Find HSI Rep" GridViewButtonImageUrl="~/Images/money.png"
                                            GridViewIdName="ID" DisplayField="FullName" ContextKey="0" AutoPostBack="false"
                                            AutoCompleteSource="Employee" ColumnHeaderList="Division - Name" ColumnValueList="DivisionAndFullName" />
                                    </td>
                                </tr>
                            </table>
                        </Content>
                    </uc:CollapseHolder>
                    <br />
                </div>
                <uc:CollapseHolder ID="chCustomerSpecificInfo" runat="server">
                    <Header>
                        <asp:Label ID="lblCustomerSpecificInfoTitle" runat="server" Text="Company Specific Info"></asp:Label>
                    </Header>
                    <Content>
                        <div style="display: block;">
                            <asp:PlaceHolder ID="plhCustomerSpecificInfo" runat="server" />
                        </div>
                    </Content>
                </uc:CollapseHolder>
                <br />
                <asp:CheckBox ID="ckbInterimBill" runat="server" Text="Interim Bill" AutoPostBack="true"
                    OnCheckedChanged="ckbInterimBill_CheckedChanged" />
                <br />
                <table>
                    <tr>
                        <td style="text-align: right">
                            Requested By:
                        </td>
                        <td>
                            <uc1:AutoCompleteTextbox ID="actRequestedBy" runat="server" ServiceMethod="GetEmployeeListWithDivisionName"
                                WindowTitle="Job Record - Find Requested By" GridViewButtonImageUrl="~/Images/money.png"
                                GridViewIdName="ID" DisplayField="CompleteName" ContextKey="0" AutoPostBack="false"
                                Enabled="false" AutoCompleteSource="EmployeeWithDivision" ColumnHeaderList="Division - Name"
                                ColumnValueList="DivisionAndFullName" RequiredField="false" ErrorMessage="Job Info - The Requested by field is required if Interim Bill field is selected" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            Frequency:
                        </td>
                        <td>
                            <asp:ComboBox ID="ddlFrequency" runat="server" AutoCompleteMode="SuggestAppend" RenderMode="Inline"
                                CaseSensitive="false" Enabled="false" Width="80px" DropDownStyle="DropDownList"
                                AutoPostBack="false" CssClass="WindowsStyle">
                            </asp:ComboBox>
                            <asp:RequiredFieldValidator ID="rfvFrequency" Enabled="false" runat="server" EnableClientScript="true"
                                Text="*" Display="Dynamic" ErrorMessage="Job Info - The Frequency field is required if Interim Bill field is selected"
                                ControlToValidate="ddlFrequency" InitialValue="- Select One - "></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="float: left; width: 99%; display: none;" id="divContract" runat="server">
                <fieldset class="JobInfo groupBox" style="width: 100%">
                    <legend>
                        <asp:Label ID="lblGroupContract" runat="server" Text="Company Contract Table"></asp:Label></legend>
                    <asp:ScrollableGridView ID="gdvCustomerContract" runat="server" CssClass="ScrollableGridView"
                        AutoGenerateColumns="false" ShowFooter="false" Width="100%" OnRowDataBound="gdvCustomerContract_RowDataBound">
                        <Columns>
                            <asp:BoundField HeaderText="Contract" DataField="ContractNumber"></asp:BoundField>
                            <asp:BoundField HeaderText="Contract Description" DataField="Description"></asp:BoundField>
                            <asp:BoundField HeaderText="Additional Details" DataField="AdditionalDetails"></asp:BoundField>
                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="false">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hypView" runat="server" Text="View" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:ScrollableGridView>
                </fieldset>
            </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script type="text/javascript" src="../../Scripts/formatFunctions.js" defer="defer">
</script>
