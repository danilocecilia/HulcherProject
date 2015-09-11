<%@ Page Title="Process DPI" Language="C#" MasterPageFile="~/ContentPage.master"
    AutoEventWireup="true" CodeBehind="ProcessDPI.aspx.cs" Inherits="Hulcher.OneSource.CustomerService.Web.ProcessDPI" %>

<%@ MasterType TypeName="Hulcher.OneSource.CustomerService.Web.ContentPage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="uc1" %>
<%@ Register TagName="CollapseHolder" TagPrefix="uc" Src="~/UserControls/CollapseHolder.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Styles/Forms.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/ProcessDPI.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div style="min-width: 1050px;">
        <div>
            <asp:Label ID="lblTitle" runat="server" Text="Process DPI" Font-Size="Large" Font-Bold="true"></asp:Label>
        </div>
        <br />
        <asp:ValidationSummary ID="vsDPI" runat="server" ValidationGroup="DPI" CssClass="errorbox"
            HeaderText="Please correct the following information" />
        <div id="divJobInfoHeader">
            <uc:CollapseHolder ID="chJobInfoHeader" runat="server" GridViewCssClass="ScrollableGridView"
                Collapsed="false" Visible="true">
                <Header>
                    <asp:Label ID="lblHeaderTitle" runat="server" Text="Job Information"></asp:Label>
                </Header>
                <Content>
                    <div id="divJobInfoContent">
                        <div class="divJobInfoLeftColumn">
                            <div class="row">
                                <div class="label">
                                    <asp:Label ID="lblJobNumberTitle" runat="server" Text="Job #:"></asp:Label>
                                </div>
                                <div class="value">
                                    <asp:Label ID="lblJobNumber" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="label">
                                    <asp:Label ID="lblPrimaryDivisionTitle" runat="server" Text="Primary Div. #:"></asp:Label>
                                </div>
                                <div class="value">
                                    <asp:Label ID="lblPrimaryDivision" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="label">
                                    <asp:Label ID="lblCustomerTitle" runat="server" Text="Company:"></asp:Label>
                                </div>
                                <div class="value">
                                    <asp:Label ID="lblCustomer" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="label">
                                    <asp:Label ID="lblLocationTitle" runat="server" Text="Location:"></asp:Label>
                                </div>
                                <div class="value">
                                    <asp:Label ID="lblLocation" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="divJobInfoMiddleColumn">
                            <div class="row">
                                <div class="label">
                                    <asp:Label ID="lblJobActionTitle" runat="server" Text="Job Action:"></asp:Label>
                                </div>
                                <div class="value">
                                    <asp:Label ID="lblJobAction" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="label">
                                    <asp:Label ID="lblJobCategoryTitle" runat="server" Text="Job Category:"></asp:Label>
                                </div>
                                <div class="value">
                                    <asp:Label ID="lblJobCategory" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="label">
                                    <asp:Label ID="lblJobTypeTitle" runat="server" Text="Job Type:"></asp:Label>
                                </div>
                                <div class="value">
                                    <asp:Label ID="lblJobType" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="divJobInfoRightColumn">
                            <div class="row">
                                <div class="label">
                                    <asp:Label ID="lblNumberEnginesTitle" runat="server" Text="# of Engines:"></asp:Label>
                                </div>
                                <div class="value">
                                    <asp:Label ID="lblNumberEngines" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="label">
                                    <asp:Label ID="lblNumberLoadsTitle" runat="server" Text="# of Loads:"></asp:Label>
                                </div>
                                <div class="value">
                                    <asp:Label ID="lblNumberLoads" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="label">
                                    <asp:Label ID="lblNumberEmptiesTitle" runat="server" Text="# of Empties:"></asp:Label>
                                </div>
                                <div class="value">
                                    <asp:Label ID="lblNumberEmpties" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <br style="clear: left;" />
                    </div>
                </Content>
            </uc:CollapseHolder>
        </div>
        <br />
        <div id="divPublishedRateHeader">
            <uc:CollapseHolder ID="chPublishedRate" runat="server" Collapsed="false" Visible="true">
                <Header>
                    <asp:Label ID="lblPublishedRateTitle" runat="server" Text="Blended Rates"></asp:Label>
                </Header>
                <Content>
                    <asp:UpdatePanel ID="uplPublishedRate" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnApprove" />
                            <asp:AsyncPostBackTrigger ControlID="btnSaveDraft" />
                        </Triggers>
                        <ContentTemplate>
                            <div id="divPublishedRateContent">
                                <br />
                                <asp:Label ID="lblDisclaimer" runat="server" Text=""></asp:Label>
                                <div id="divControls" runat="server">
                                    <asp:Repeater ID="rptPublishedRate" runat="server" OnItemDataBound="rptPublishedRate_ItemDataBound">
                                        <HeaderTemplate>
                                            <div id="tbRepeaters_Group" class="ScrollableGridView_Group" style="width: 100%">
                                                <div id="tbRepeaters_HeaderDiv" class="ScrollableGridView_HeaderDiv" style="min-width: 400px;">
                                                </div>
                                                <div id="tbRepeaters_ScrollDiv" class="ScrollableGridView_ScrollDiv" style="max-height: 220px;
                                                    min-width: 400px;">
                                                    <table id="tbRepeaters" class="ScrollableGridView" cellspacing="1">
                                                        <thead>
                                                            <tr>
                                                                <th id="thDivision" runat="server" class="header AlignCenter" style="border: 1px, solid, #E6EEEE;">
                                                                    <asp:Label ID="lblDivisionHeader" runat="server" Text="Division"></asp:Label>
                                                                </th>
                                                                <th id="thResourceID" runat="server" class="header AlignCenter">
                                                                    <asp:Label ID="lblResourceIDHeader" runat="server" Text="Resource ID"></asp:Label>
                                                                </th>
                                                                <th id="thResourceName" runat="server" class="header AlignCenter">
                                                                    <asp:Label ID="lblResourceNameHeader" runat="server" Text="Resource Name"></asp:Label>
                                                                </th>
                                                                <th id="thHours" runat="server" class="header AlignCenter" colspan="5">
                                                                    <asp:Label ID="lblHoursHeader" runat="server" Text="Hours"></asp:Label>
                                                                </th>
                                                                <th id="thRate" runat="server" class="header AlignCenter" colspan="3">
                                                                    <asp:Label ID="lblRateHeader" runat="server" Text="Rate"></asp:Label>
                                                                </th>
                                                                <th id="thEstimatedRate" runat="server" class="header AlignCenter">
                                                                    <asp:Label ID="lblEstimatedRateHeader" runat="server" Text="Est. Rate"></asp:Label>
                                                                </th>
                                                                <th id="thDiscount" runat="server" class="header AlignCenter">
                                                                    <asp:Label ID="lblDiscountHeader" runat="server" Text="Discount"></asp:Label>
                                                                </th>
                                                                <th id="thRevisedRate" runat="server" class="header AlignCenter">
                                                                    <asp:Label ID="lblRevisedRateHeader" runat="server" Text="Revised Est. Rate"></asp:Label>
                                                                </th>
                                                                <th id="thPermit" runat="server" class="header AlignCenter" colspan="2">
                                                                    <asp:Label ID="lblPermitHeader" runat="server" Text="Permit"></asp:Label>
                                                                </th>
                                                                <th id="thMeals" runat="server" class="header AlignCenter" colspan="2">
                                                                    <asp:Label ID="lblMealsHeader" runat="server" Text="Meals"></asp:Label>
                                                                </th>
                                                                <th id="thHotel" runat="server" class="header AlignCenter" colspan="2">
                                                                    <asp:Label ID="lblHotelHeader" runat="server" Text="Hotel"></asp:Label>
                                                                </th>
                                                                <th id="thRevenue" runat="server" class="header AlignCenter">
                                                                    <asp:Label ID="lblRevenueHeader" runat="server" Text="Revenue Total"></asp:Label>
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class="odd">
                                                <td colspan="21">
                                                    <asp:Label ID="lblDivision" runat="server" Text=""></asp:Label>
                                                    <asp:HiddenField ID="hfDivisionID" runat="server" />
                                                </td>
                                            </tr>
                                            <asp:Repeater ID="rptPublishedRateResources" runat="server" OnItemDataBound="rptPublishedRateResources_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="vertical-align: middle;">
                                                            <asp:Label ID="lblDivision" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle;">
                                                            <asp:HiddenField ID="hfResourceID" runat="server" />
                                                            <asp:Label ID="lblResourceID" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle;">
                                                            <asp:Label ID="lblResourceName" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle;">
                                                            <asp:HiddenField ID="hfCalculatedNumberHours" runat="server" />
                                                            <asp:TextBox ID="txtNumberHours" runat="server" Text="" Width="40px" CssClass="input"></asp:TextBox>
                                                        </td>
                                                        <td style="vertical-align: middle;">
                                                            <asp:Label ID="lblStatusHours" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle;">
                                                            <asp:CheckBox ID="chkContinuing" runat="server" Text="Con." TextAlign="Left" />
                                                            <asp:HiddenField ID="hfContinuingHours" Value="15" runat="server" />
                                                        </td>
                                                        <td style="width: 6px; vertical-align: middle;">
                                                            <asp:Label ID="lblHoursModified" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle;">
                                                            <asp:LinkButton ID="lnkResetHours" runat="server" Text="Reset"></asp:LinkButton>
                                                        </td>
                                                        <td style="vertical-align: middle;">
                                                            <asp:HiddenField ID="hfCalculatedRate" runat="server" />
                                                            <asp:TextBox ID="txtRate" runat="server" Text="" Width="70px" CssClass="input"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 6px; vertical-align: middle;">
                                                            <asp:Label ID="lblRateModified" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle;">
                                                            <asp:LinkButton ID="lnkRateReset" runat="server" Text="Reset"></asp:LinkButton>
                                                        </td>
                                                        <td style="vertical-align: middle;">
                                                            <asp:Label ID="lblEstimatedRate" runat="server" CssClass="estimatedRate"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle;">
                                                            <asp:Label ID="lblDiscount" runat="server" Text="" CssClass="estimatedRateDiscount"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle;">
                                                            <asp:Label ID="lblRevisedEstRate" runat="server" Text="" CssClass="estimatedRevisedRate"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle;">
                                                            <asp:TextBox ID="txtPermitNumber" runat="server" Text="" Width="30px" CssClass="input"></asp:TextBox>
                                                        </td>
                                                        <td style="vertical-align: middle;">
                                                            <asp:HiddenField ID="hfPermitRate" Value="100" runat="server" />
                                                            <asp:Label ID="lblPermitRate" runat="server" Text="" CssClass="permitsTotal"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle;">
                                                            <asp:DropDownList ID="drpMeals" runat="server">
                                                                <asp:ListItem Value="" Text="" />
                                                                <asp:ListItem Value="1" Text="1" />
                                                                <asp:ListItem Value="2" Text="2" />
                                                                <asp:ListItem Value="3" Text="3" />
                                                                <asp:ListItem Value="4" Text="4" />
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="vertical-align: middle;">
                                                            <asp:HiddenField ID="hfMealsRate" Value="20" runat="server" />
                                                            <asp:Label ID="lblMealsRate" runat="server" Text="" CssClass="mealsTotal"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle;">
                                                            <asp:CheckBox ID="chkHotel" runat="server" CssClass="chkHotel" />
                                                        </td>
                                                        <td style="vertical-align: middle;">
                                                            <asp:HiddenField ID="hfHotelRate" runat="server" />
                                                            <asp:TextBox ID="txtHotelRate" runat="server" Width="70px" CssClass="input hotelsTotal"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right; vertical-align: middle;">
                                                            <asp:Label ID="lblRevenue" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <tr class="odd">
                                                <td colspan="21" align="right">
                                                    <asp:Label ID="lblDivisionRevenue" runat="server" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody> </table></div></div>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                    <div class="divTotalBox">
                                        <div class="divTotal">
                                            <div class="inline floatLeft alignRight fontBold paddingRight5 space50">
                                                <asp:Label ID="lblPreviousTotalTitle" runat="server" Text="Previous Total:" /></div>
                                            <div class="inlineBlock">
                                                <asp:Label ID="lblPreviousTotal" runat="server" CssClass="previousTotal" /></div>
                                            <div class="inline floatLeft alignRight fontBold paddingRight5 space50">
                                                <asp:Label ID="lblNewRevenueTitle" runat="server" Text="New Revenue:" /></div>
                                            <div class="inlineBlock">
                                                <asp:Label ID="lblNewRevenue" runat="server" CssClass="totalRevenue" /></div>
                                            <div class="inline floatLeft alignRight fontBold paddingRight5 space50">
                                                <asp:Label ID="lblCurrentTotalTitle" runat="server" Text="Current Total:" /></div>
                                            <div class="inlineBlock">
                                                <asp:Label ID="lblCurrentTotal" runat="server" CssClass="currentTotal" /></div>
                                        </div>
                                        <div class="divRevenueInfo">
                                            <asp:Label ID="lblTotalRevenueTitle" runat="server" Text="Total Job Revenue:" Font-Bold="true"></asp:Label>
                                            <asp:Label ID="lblTotalRevenue" runat="server" CssClass="totalRevenue"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="EstRateGrandTotalColumn">
                                        <asp:Label ID="lblRateGrandTotal" Text="Est. Rate Grand Total:" runat="server" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtRateGrandTotalBlendedRates" CssClass="input" Width="80px" runat="server"
                                            onblur="Javascript:return formatCurrency(this.value,this);" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <asp:Panel ID="pnlNoRows" runat="server" Visible="true">
                                    <div class="ScrollableGridView_Group" style="width: 100%">
                                        <div class="ScrollableGridView_HeaderDiv" style="min-width: 400px;">
                                        </div>
                                        <div class="ScrollableGridView_ScrollDiv" style="max-height: 220px; min-width: 400px;">
                                            <table id="tbRepeaters" class="ScrollableGridView" cellspacing="1">
                                                <tbody>
                                                    <tr>
                                                        <td class="even" align="center">
                                                            The List Is Empty
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </Content>
            </uc:CollapseHolder>
        </div>
        <br />
        <asp:Panel ID="pnlSpecialPricing" runat="server">
            <uc:CollapseHolder ID="chSpecialPricing" runat="server" GridViewCssClass="ScrollableGridView"
                Collapsed="false" Visible="true">
                <Header>
                    <asp:Label ID="lblSpecialPricingTitle" runat="server" Text="Special Pricing DPI"></asp:Label>
                </Header>
                <Content>
                    <asp:UpdatePanel ID="uplSpecialPricing" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnApprove" />
                            <asp:AsyncPostBackTrigger ControlID="btnSaveDraft" />
                        </Triggers>
                        <ContentTemplate>
                            <div id="divSpecialPricingContent">
                                <asp:HiddenField ID="hfDiscount" runat="server" />
                                <asp:HiddenField ID="hfLastChecked" runat="server" />
                                <div class="row">
                                    <div class="labelSpecialPricing">
                                        <asp:RadioButton ID="rbNoSpecialPricing" runat="server" Text="No Special Pricing"
                                            Checked="true" GroupName="SpecialPricing" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="labelSpecialPricing">
                                        <asp:RadioButton ID="rbOverallSpecialPricing" runat="server" Text="Overall Job Discount"
                                            Checked="false" GroupName="SpecialPricing" />
                                    </div>
                                    <div class="value">
                                        <asp:TextBox ID="txtOverallDiscount" runat="server" Text="" Width="70px" CssClass="input"
                                            Enabled="false"></asp:TextBox>
                                        <asp:Label ID="lblPercentOVP" runat="server" Text="%"></asp:Label>
                                        <asp:RequiredFieldValidator ID="rfvOverallDiscount" runat="server" ControlToValidate="txtOverallDiscount"
                                            Display="Dynamic" ErrorMessage="The Overall Discount field is required" ValidationGroup="DPI"
                                            Enabled="false" Text="*" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="labelSpecialPricing">
                                        <asp:RadioButton ID="rbLumpSum" runat="server" Text="Total Project Lump Sum (For Day)"
                                            Checked="false" GroupName="SpecialPricing" />
                                    </div>
                                    <div class="value">
                                        <asp:TextBox ID="txtLumpSumValue" runat="server" Text="" Width="70px" CssClass="input"
                                            Enabled="false"></asp:TextBox>
                                        <asp:TextBox ID="txtLumpSumDuration" runat="server" Text="" Width="40px" CssClass="input"
                                            Enabled="false"></asp:TextBox>
                                        <asp:Label ID="lblDurationTitle" runat="server" Text="Duration"></asp:Label>
                                        &nbsp;
                                        <asp:TextBox ID="txtLumpSumValuePerDay" runat="server" Text="" Width="70px" CssClass="input"
                                            Enabled="false"></asp:TextBox>
                                        <asp:Label ID="lblValuePerDurationTitle" runat="server" Text="Per Duration"></asp:Label>
                                        <asp:RequiredFieldValidator ID="rfvLumpSumValue" runat="server" ControlToValidate="txtLumpSumValue"
                                            Display="Dynamic" ErrorMessage="The Lump Sum Value field is required" ValidationGroup="DPI"
                                            Enabled="false" Text="*" />
                                        <asp:RequiredFieldValidator ID="rfvLumpSumDuration" runat="server" ControlToValidate="txtLumpSumDuration"
                                            Display="Dynamic" ErrorMessage="The Lump Sum Duration field is required" ValidationGroup="DPI"
                                            Enabled="false" Text="*" />
                                        <asp:RequiredFieldValidator ID="rfvLumpSumValuePerDay" runat="server" ControlToValidate="txtLumpSumValuePerDay"
                                            Display="Dynamic" ErrorMessage="The Lump Sum Value Per Duration field is required"
                                            ValidationGroup="DPI" Enabled="false" Text="*" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="labelSpecialPricing">
                                        <asp:RadioButton ID="rbManualCalculation" runat="server" Text="Manual Special Pricing Calculation"
                                            Checked="false" GroupName="SpecialPricing" />
                                    </div>
                                </div>
                                <asp:Panel ID="pnlManualSpecialPricing" runat="server" Style="display: none;">
                                    <br />
                                    <input type="button" value="Add Row" onclick="addRow('dataTable')" class="btn" id="btnAddRow"
                                        runat="server" />
                                    <input type="button" value="Delete Row" onclick="deleteRow('dataTable')" class="btn"
                                        id="btnDeleteRow" runat="server" />
                                    <br />
                                    <div class="row">
                                        <%--<asp:ScrollableGridView ID="grdManualSpecialPricing" runat="server" Width="700px">
                                            <Columns>
                                                <asp:BoundField HeaderText="Description" />
                                                <asp:BoundField HeaderText="QTY/HRs" />
                                                <asp:BoundField HeaderText="Rate" />
                                                <asp:BoundField HeaderText="Extended Rate" />
                                            </Columns>                                        
                                        </asp:ScrollableGridView>--%>
                                        <asp:HiddenField ID="hidManualSpecialPricing" runat="server" />
                                        <div class="ScrollableGridView_Group" style="width: 100%">
                                            <div class="ScrollableGridView_HeaderDiv" style="min-width: 400px;">
                                            </div>
                                            <div class="ScrollableGridView_ScrollDiv" style="max-height: 220px; min-width: 400px;">
                                                <table id="dataTable" class="ScrollableGridView" cellspacing="1">
                                                    <thead>
                                                        <tr>
                                                            <th class="header">
                                                                Select
                                                            </th>
                                                            <th class="header">
                                                                Description
                                                            </th>
                                                            <th class="header">
                                                                QTY/HRs
                                                            </th>
                                                            <th class="header">
                                                                Rate
                                                            </th>
                                                            <th class="header">
                                                                Extended Rate
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <%--<td>
                                                            <input type="checkbox" name="chk" />
                                                        </td>
                                                        <td>
                                                          <input type="text" />                                                  
                                                        </td>
                                                        <td>
                                                            <input type="text" value="0" />
                                                        </td>
                                                        <td>
                                                          <input type="text" value="0" />                                                  
                                                        </td>
                                                        <td>
                                                          <span>0</span>                                              
                                                        </td>--%>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="text-align: center">
                                        <asp:Label ID="lblExtendedRateGrandTotal" runat="server" Text="Est. Rate Grand Total:"
                                            Font-Bold="true" Style="padding-top: 5px;"></asp:Label>
                                        <asp:TextBox ID="txtExtendedRateGrandTotalSpecialPricing" runat="server" Text="0"
                                            Width="70px" CssClass="input" onblur="Javascript:return formatCurrency(this.value,this);"
                                            ReadOnly="true"></asp:TextBox>
                                        <asp:TextBox ID="txtExtendedRateGrandTotalPercent" runat="server" Text="" Width="40px"
                                            onblur="Javascript:return formatDecimal(this.value);" CssClass="input" ReadOnly="true"></asp:TextBox>
                                        <asp:Label ID="lblPercentERGT" runat="server" Text="%"></asp:Label>
                                        <asp:Label ID="lblTaxTypeDescription" runat="server" Text=""></asp:Label>
                                    </div>
                                </asp:Panel>
                                <div class="row">
                                    <asp:Label ID="lblSpecialPricingNotesTitle" runat="server" Text="Special Pricing Notes:"
                                        Font-Bold="true"></asp:Label>
                                </div>
                                <div class="row">
                                    <div style="margin-left: 10px">
                                        <asp:CountableTextBox ID="txtSpecialPricingNotes" runat="server" Width="500px" Height="70px"
                                            MaxChars="250" MaxCharsWarning="220" CssClass="input" />
                                    </div>
                                </div>
                                <br style="clear: left;" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    </div>
                </Content>
            </uc:CollapseHolder>
        </asp:Panel>
        <br />
        <div class="divButtonsFooter">
            <asp:Button ID="btnSaveDraft" runat="server" Text="Save As Draft" CssClass="btn"
                OnClick="btnSaveDraft_Click" ValidationGroup="DPI" />
            <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="btn" OnClick="btnApprove_Click"
                ValidationGroup="DPI" />
        </div>
    </div>
    <script type="text/javascript" language="javascript">

        function CalculateHoursEstimatedRate(hoursCount, hoursRate, estimatedRate) {
            var numHoursCount = document.getElementById(hoursCount).value.toString().replace(/\$|\,/g, '');
            var numHoursRate = document.getElementById(hoursRate).value.toString().replace(/\$|\,/g, '');
            var controlEstimatedRate = document.getElementById(estimatedRate);

            var value = numHoursCount * numHoursRate;

            controlEstimatedRate.innerHTML = formatCurrency(value);
        }

        function CalculateHoursEstimatedRateTotal(estimatedRateTotal) {
            var estimatedRateArray = $('.estimatedRate');
            var controlEstimatedRateTotal = document.getElementById(estimatedRateTotal);
            var total = parseFloat("0");

            estimatedRateArray.each
            (
                function () {
                    total += parseFloat(((this.innerHTML.replace(/\$|\,/g, '') != "") ? this.innerHTML.replace(/\$|\,/g, '') : "0"));
                }
            );

            controlEstimatedRateTotal.value = formatCurrency(total);
        }

        function NoDiscount(radioType, lastCheckedControl, hfDiscount) {
            var controlDiscount = document.getElementById(hfDiscount);
            var lastChecked = document.getElementById(lastCheckedControl);

            if (lastChecked.value != radioType.id)
                if (controlDiscount != null) {
                    controlDiscount.value = 0;
                    ApplyDiscount(hfDiscount);
                    CalculateResourceRevenueTotal(hfDiscount);
                    lastChecked.value = radioType.id;
                }
    }

    function CalculateEstimatedRateOverallDiscount(overallDiscount, hfDiscount) {
        var controlOverallDiscount = document.getElementById(overallDiscount);
        var controlDiscount = document.getElementById(hfDiscount);

        if (controlOverallDiscount != null) {
            var discountPercentage = parseFloat("0");
            discountPercentage = parseFloat(((controlOverallDiscount.value.replace(/\$|\,/g, '') != "") ? controlOverallDiscount.value.replace(/\$|\,/g, '') : "0"));
            controlDiscount.value = discountPercentage;
            ApplyDiscount(hfDiscount);
        }
    }

    function ApplyDiscount(hfDiscount) {
        var controlDiscount = document.getElementById(hfDiscount);
        var estimatedRateArray = $('.estimatedRate');
        var estimatedRateDiscountArray = $('.estimatedRateDiscount');
        var estimatedRevisedRateArray = $('.estimatedRevisedRate');
        var discountPercentage = parseFloat("0");
        var discount = parseFloat("0");
        var estimatedRate = parseFloat("0");
        var revisedRate = parseFloat("0");

        discountPercentage = controlDiscount.value;

        estimatedRateArray.each
                (
                    function (index) {

                        estimatedRate = parseFloat(((this.innerHTML.replace(/\$|\,/g, '') != "") ? this.innerHTML.replace(/\$|\,/g, '') : "0"));
                        discount = estimatedRate * (discountPercentage / 100);
                        revisedRate = estimatedRate - discount;

                        if (discountPercentage > 0)
                            estimatedRateDiscountArray[index].innerHTML = formatPercent(discountPercentage) + " D";
                        else if (discountPercentage < 0)
                            estimatedRateDiscountArray[index].innerHTML = formatPercent(discountPercentage * -1) + " O";
                        else
                            estimatedRateDiscountArray[index].innerHTML = formatPercent(discountPercentage);

                        estimatedRevisedRateArray[index].innerHTML = formatCurrency(revisedRate);
                    }
                );
    }

    function CalculateResourceOverallDiscount(overallDiscount, estimatedRate, estimatedRateDiscount, estimatedRevisedRate, hfDiscount) {
        var controlOverallDiscount = document.getElementById(overallDiscount);

        if (controlOverallDiscount != null) {
            var controlDiscount = document.getElementById(hfDiscount);
            var estimatedRateCtrl = document.getElementById(estimatedRate);
            var estimatedRateDiscountCtrl = document.getElementById(estimatedRateDiscount);
            var estimatedRevisedRateCtrl = document.getElementById(estimatedRevisedRate);
            var discountPercentage = parseFloat("0");
            var discount = parseFloat("0");
            var estimatedRate = parseFloat("0");

            if (isNaN(parseFloat(controlDiscount.value)))
                controlDiscount.value = parseFloat(((controlOverallDiscount.value.replace(/\$|\,/g, '') != "") ? controlOverallDiscount.value.replace(/\$|\,/g, '') : "0"));

            discountPercentage = parseFloat(controlDiscount.value);
            estimatedRate = parseFloat(((estimatedRateCtrl.innerHTML.replace(/\$|\,/g, '') != "") ? estimatedRateCtrl.innerHTML.replace(/\$|\,/g, '') : "0"));
            discount = estimatedRate * (discountPercentage / 100);
            revisedRate = estimatedRate - discount;
            estimatedRateDiscountCtrl.innerHTML = formatPercent(discountPercentage);
            estimatedRevisedRateCtrl.innerHTML = formatCurrency(revisedRate);
        }
    }

    function CalculateResourceRevenueTotal(hfDiscount) {

        var controlDiscount = document.getElementById(hfDiscount);
        var estimatedRateArray = $('.estimatedRate');
        var estimatedRateDiscountArray = $('.estimatedRateDiscount');
        var permitsTotalArray = $('.permitsTotal');
        var mealsTotalArray = $('.mealsTotal');
        var hotelsTotalArray = $('.hotelsTotal');
        var chkhotelsArray = $('.chkHotel :input');
        var revenueTotalArray = $('.Revenue');
        var discountPercentage = parseFloat("0");
        var discount = parseFloat("0");
        var estimatedRate = parseFloat("0");
        var revenue = parseFloat("0");
        var permits = parseFloat("0");
        var meals = parseFloat("0");
        var hotels = parseFloat("0");

        if (controlDiscount) {
            discount = parseFloat(controlDiscount.value);

            if (!isNaN(discount)) {

                estimatedRateArray.each
                    (
                        function (index) {
                            estimatedRate = parseFloat(((this.innerHTML.replace(/\$|\,/g, '') != "") ? this.innerHTML.replace(/\$|\,/g, '') : "0"));
                            revisedRate = estimatedRate - (estimatedRate * (discount / 100));
                            permits = parseFloat(((permitsTotalArray[index].innerHTML.toString().replace(/\$|\,/g, '') != "") ? permitsTotalArray[index].innerHTML.toString().replace(/\$|\,/g, '') : "0"));
                            meals = parseFloat(((mealsTotalArray[index].innerHTML.toString().replace(/\$|\,/g, '') != "") ? mealsTotalArray[index].innerHTML.toString().replace(/\$|\,/g, '') : "0"));
                            hotels = parseFloat(((hotelsTotalArray[index].value.toString().replace(/\$|\,/g, '') != "") ? hotelsTotalArray[index].value.toString().replace(/\$|\,/g, '') : "0"));

                            revenue = revisedRate + permits + meals;

                            if (chkhotelsArray[index].checked)
                                revenue += hotels

                            revenueTotalArray[index].innerHTML = formatCurrency(revenue);
                        }
                    );

                CalculateDivisionRevenueTotal();
                CalculateTotalRevenue();
            }
        }
    }

    function CalculateResourceLumpSumTotal(lumpSumPerDay, hfDiscount, totalBlendedRates) {
        var controlLumpSumPerDay = document.getElementById(lumpSumPerDay);
        var controlDiscount = document.getElementById(hfDiscount);
        var controlTotalBlendedRates = document.getElementById(totalBlendedRates);

        if (controlLumpSumPerDay != null) {
            var discountPercentage = parseFloat("0");
            var discountValue = parseFloat("0");
            var total = parseFloat("0");

            discountValue = parseFloat(((controlLumpSumPerDay.value.replace(/\$|\,/g, '') != "") ? controlLumpSumPerDay.value.replace(/\$|\,/g, '') : "0"));

            total = parseFloat(((controlTotalBlendedRates.value.replace(/\$|\,/g, '') != "") ? controlTotalBlendedRates.value.replace(/\$|\,/g, '') : "0"));

            if (total == 0)
                discountPercentage = 0
            else
                discountPercentage = 100 - ((discountValue * 100) / total);

            controlDiscount.value = discountPercentage;
            ApplyDiscount(hfDiscount)
        }
    }

    function CalculateResourceLumpSumTotal2(total) {
        if (total == 0)
            discountPercentage = 0
        else
            discountPercentage = (discountValue * 100) / total;

        if (discountPercentage > 100)
            discountPercentage = (discountPercentage - 100) * -1;

        controlDiscount.value = discountPercentage;
        ApplyDiscount(hfDiscount)
    }


    function CalculateResourceLumpSumDiscount(lumpSumDiscount, lumpSumDuration, lumpSumPerDay, hfDiscount, totalBlendedRates) {
        CalculateLumpSumPerDuration(lumpSumDiscount, lumpSumDuration, lumpSumPerDay);
        var controlDiscount = document.getElementById(hfDiscount);

        if (controlDiscount != null) {
            CalculateResourceLumpSumTotal(lumpSumPerDay, hfDiscount, totalBlendedRates);
        }
    }

    function CalculateDivisionRevenueTotal() {
        var divisionRevenueArray = $('.DivisionRevenue');
        var array;

        divisionRevenueArray.each
                (
                    function (index) {
                        array = this.className.split(' ');
                        if (array.length > 0)
                            CalculateDivisionRevenue(array[0])
                    }
                );
    }

    function SetContinuingHours(continuingFlag, continuingHoursValue, hoursCount, calculatedHoursValue, statusLabel) {
        var controlContinuingFlag = document.getElementById(continuingFlag);
        var numContinuingHours = document.getElementById(continuingHoursValue).value.toString().replace(/\$|\,/g, '');
        var controlHoursCount = document.getElementById(hoursCount);
        var numCalculatedHours = document.getElementById(calculatedHoursValue).value.toString().replace(/\$|\,/g, '');
        var controlStatusLabel = document.getElementById(statusLabel);

        if (controlContinuingFlag.checked) {
            controlHoursCount.value = formatDecimal(numContinuingHours);
            controlStatusLabel.style.color = 'black';
        }
        else {
            controlHoursCount.value = formatDecimal(numCalculatedHours);
            if (controlStatusLabel.innerHTML == 'INSF')
                controlStatusLabel.style.color = 'red';
            else
                controlStatusLabel.style.color = 'black';
        }
    }

    function CalculatePermitRate(permitCount, permitRate, permitTotal) {
        var numPermitCount = document.getElementById(permitCount).value.toString().replace(/\$|\,/g, '');
        var numPermitRate = document.getElementById(permitRate).value.toString().replace(/\$|\,/g, '');
        var controlPermitTotal = document.getElementById(permitTotal);

        var value = numPermitCount * numPermitRate;

        controlPermitTotal.innerHTML = formatCurrency(value);
    }

    function CalculateMealsRate(mealsCount, mealsRate, mealsTotal) {
        var numMealsCount = document.getElementById(mealsCount).value.toString().replace(/\$|\,/g, '');
        var numMealsRate = document.getElementById(mealsRate).value.toString().replace(/\$|\,/g, '');
        var controlMealsTotal = document.getElementById(mealsTotal);

        var value = numMealsCount * numMealsRate;

        controlMealsTotal.innerHTML = formatCurrency(value);
    }

    function SetHotelRate(hotelCheck, hotelRate, hotelRateValue) {
        var controlHotel = document.getElementById(hotelCheck);
        var controlHotelRate = document.getElementById(hotelRate);
        var numHotelRate = document.getElementById(hotelRateValue).value.toString().replace(/\$|\,/g, '');

        if (controlHotel.checked)
            controlHotelRate.value = formatCurrency(numHotelRate);
        else
            controlHotelRate.value = "";
    }

    function CalculateResourceRevenue(estimatedRate, discountRate, permitRate, mealsRate, hotelCheck, hotelRate, resourceRevenue, divisionSelector, hfDiscount) {
        var numEstimatedRate = document.getElementById(estimatedRate).innerHTML.toString().replace(/\$|\,/g, '');
        var numDiscountRate = parseFloat('0');
        var numPermitRate = document.getElementById(permitRate).innerHTML.toString().replace(/\$|\,/g, '');
        var numMealsRate = document.getElementById(mealsRate).innerHTML.toString().replace(/\$|\,/g, '');
        var numHotelRate = document.getElementById(hotelRate).value.toString().replace(/\$|\,/g, '');
        var controlResourceRevenue = document.getElementById(resourceRevenue);
        var controlHotel = document.getElementById(hotelCheck);
        var numDiscountPercent = document.getElementById(hfDiscount)

        if (numDiscountPercent && !isNaN(numDiscountPercent.value))
            numDiscountRate = numEstimatedRate * (numDiscountPercent.value / 100);

        var totalRevenue = (numEstimatedRate * 1) + (numPermitRate * 1) + (numMealsRate * 1) - (numDiscountRate * 1)

        if (controlHotel.checked)
            totalRevenue = totalRevenue + (numHotelRate * 1);

        controlResourceRevenue.innerHTML = formatCurrency(totalRevenue);

        CalculateDivisionRevenue(divisionSelector);
        CalculateTotalRevenue();
    }

    function CalculateDivisionRevenue(divisionSelector) {
        var list = $("." + divisionSelector + ".Revenue");
        var total = 0;

        for (var i = 0; i < list.length; i++) {
            total = total + list[i].innerHTML.replace('$', '').replace(',', '') * 1;
        }

        $("." + divisionSelector + ".DivisionRevenue")[0].innerHTML = formatCurrency(total);
    }

    function CalculateTotalRevenue() {
        var list = $(".Revenue");
        var total = 0;

        for (var i = 0; i < list.length; i++) {
            total = total + list[i].innerHTML.replace('$', '').replace(',', '') * 1;
        }

        $(".totalRevenue")[0].innerHTML = formatCurrency(total);
        $(".totalRevenue")[1].innerHTML = formatCurrency(total);

        var previousTotal = $(".previousTotal")[0].innerHTML.replace('$', '').replace(',', '') * 1;
        var currentTotal = previousTotal + total;
        $(".currentTotal")[0].innerHTML = formatCurrency(currentTotal);
    }

    function VerifyHourModification(hourCount, defaultHourCount, modified) {
        var numHourCount = document.getElementById(hourCount).value.toString().replace(/\$|\,/g, '');
        var numDefaultHourCount = document.getElementById(defaultHourCount).value.toString().replace(/\$|\,/g, '');
        var controlModified = document.getElementById(modified);

        if ((numHourCount * 1) != (numDefaultHourCount * 1))
            controlModified.innerHTML = "M";
        else
            controlModified.innerHTML = "";
    }

    function VerifyRateModification(rateCount, defaultRateCount, modified) {
        var numRateCount = document.getElementById(rateCount).value.toString().replace(/\$|\,/g, '');
        var numDefaultRateCount = document.getElementById(defaultRateCount).value.toString().replace(/\$|\,/g, '');
        var controlModified = document.getElementById(modified);

        if ((numRateCount * 1) != (numDefaultRateCount * 1))
            controlModified.innerHTML = "M";
        else
            controlModified.innerHTML = "";
    }

    function addRow(tableID) {

        var table = document.getElementById(tableID);

        var rowCount = table.rows.length;
        var row = table.insertRow(rowCount);

        var cell1 = row.insertCell(0);
        var element1 = document.createElement("input");
        element1.type = "checkbox";
        cell1.appendChild(element1);

        var cell2 = row.insertCell(1);
        var element2 = document.createElement("input");
        element2.type = "text";
        element2.setAttribute("className", "input");
        element2.maxLength = 100;
        element2.onblur = function () { createValuesFromManualTable('dataTable'); }
        cell2.appendChild(element2);

        var cell3 = row.insertCell(2);
        var element3 = document.createElement("input");
        element3.type = "text";
        element3.setAttribute("className", "input");
        element3.value = formatDecimal(0, element3);
        element3.onblur = function () { calculateEstRate(element3, element4, element5); CalculateResourceRevenueTotal("<%=  hfDiscount.ClientID %>"); };
        cell3.appendChild(element3);

        var cell4 = row.insertCell(3);
        var element4 = document.createElement("input");
        element4.type = "text";
        element4.setAttribute("className", "input");
        element4.value = formatCurrency(0, element4);
        element4.onblur = function () { calculateEstRate(element3, element4, element5); CalculateResourceRevenueTotal("<%=  hfDiscount.ClientID %>"); };
        cell4.appendChild(element4);

        var cell5 = row.insertCell(4);
        var element5 = document.createElement("span");
        element5.innerHTML = "$0.00";
        cell5.appendChild(element5);

        var cell6 = row.insertCell(5);
        var element6 = document.createElement("input");
        element6.type = "hidden";
        element6.setAttribute("value", "");
        cell6.appendChild(element6);
        cell6.style.display = "none";

        var cell7 = row.insertCell(6);
        var element7 = document.createElement("input");
        element7.type = "hidden";
        element7.setAttribute("value", "");
        cell7.appendChild(element7);
        cell7.style.display = "none";

        var cell8 = row.insertCell(7);
        var element8 = document.createElement("input");
        element8.type = "hidden";
        element8.setAttribute("value", "");
        cell8.appendChild(element8);
        cell8.style.display = "none";
    }

    function calculateEstRate(el1, el2, el3) {
        var v1 = parseFloat(el1.value);
        var v2 = parseFloat(el2.value.toString().replace(/\$|\,/g, ''));
        var v3 = v1 * v2;
        el3.innerHTML = formatCurrency(round(v3.toString(), 2), el3);
        calculateGrandEstRateTotal('dataTable');
        addToHidden();
        el1.value = formatDecimal(parseFloat(el1.value.toString()), el1).toString();
        el2.value = formatCurrency(parseFloat(el2.value.toString().replace(/\$|\,/g, '')), el2).toString();

    }

    function CalculateManualSpecialPricingResources() {
        var rbManualSpecialPricing = document.getElementById("<%= rbManualCalculation.ClientID %>");
        if (rbManualSpecialPricing.checked) {
            var txtGrandRateSpecialPricing = document.getElementById("<%= txtExtendedRateGrandTotalSpecialPricing.ClientID %>");
            var txtRateGrandTotalBlendedRates = document.getElementById("<%= txtRateGrandTotalBlendedRates.ClientID %>");
            var specialPricingValue = parseFloat(txtGrandRateSpecialPricing.value.replace("$", "").replace(",", ""));
            var total = parseFloat(txtRateGrandTotalBlendedRates.value.replace("$", "").replace(",", ""));
            calculateManualSpecialPricingPercentage(specialPricingValue, total);
        }
    }

    function calculateManualSpecialPricingPercentage(man, grandTotalBlendedRates) {
        //var grandTotal = document.getElementById('<%= txtRateGrandTotalBlendedRates.ClientID %>');
        //var grandTotalNumber = grandTotal.value.replace(/\$/g, '');
        //var value = parseFloat(grandTotalNumber.replace(",", ""));

        if (grandTotalBlendedRates != 0 && !isNaN(grandTotalBlendedRates)) {
            if (man != 0) {
                var result = 100 * ((man - grandTotalBlendedRates) / grandTotalBlendedRates);
            }
            else {
                result = 0;
            }
            var hidden = document.getElementById('<%= hfDiscount.ClientID %>');
            if (hidden != null) {
                hidden.setAttribute("value", result * -1);
                ApplyDiscount("<%=  hfDiscount.ClientID %>");
            }
            var estGrandRateManual = document.getElementById('<%= txtExtendedRateGrandTotalPercent.ClientID %>');
            estGrandRateManual.setAttribute("value", formatDecimal(Math.abs(result)));
            var taxTypeDescription = document.getElementById('<%= lblTaxTypeDescription.ClientID %>');
            if (result == 0)
                taxTypeDescription.innerHTML = "";
            else if (result > 0)
                taxTypeDescription.innerHTML = "(Overcharge)";
            else
                taxTypeDescription.innerHTML = "(Discount)";
        }
    }

    function deleteRow(tableID) {
        try {
            var table = document.getElementById(tableID);
            var rowCount = table.rows.length;

            for (var i = 0; i < rowCount; i++) {
                var row = table.rows[i];
                var chkbox = row.cells[0].childNodes[0];
                if (null != chkbox && true == chkbox.checked) {
                    table.deleteRow(i);
                    rowCount--;
                    i--;
                }
                calculateGrandEstRateTotal('dataTable');
                addToHidden();
                CalculateResourceRevenueTotal("<%=  hfDiscount.ClientID %>");
            }
        } catch (e) {
            alert(e);
        }
    }

    function round(n, dec) {
        n = parseFloat(n);
        if (!isNaN(n)) {
            if (!dec) var dec = 0;
            var factor = Math.pow(10, dec);
            return Math.floor(n * factor + ((n * factor * 10) % 10 >= 5 ? 1 : 0)) / factor;
        } else {
            return n;
        }
    }

    function calculateGrandEstRateTotal(id) {
        var table = document.getElementById(id);
        var sum = 0;
        var rowCount = table.rows.length;

        for (var i = 1; i < rowCount; i++) {
            var row = table.rows[i];
            if (row.cells.length > 0) {
                var label = row.cells[4].childNodes[0];
                var value = round(parseFloat(label.innerHTML.replace("$", "").replace(",", "")), 2);
                sum = sum + value;
            }
        }
        var result = document.getElementById('<%= txtExtendedRateGrandTotalSpecialPricing.ClientID %>');
        result.setAttribute("value", formatCurrency(round(sum.toString(), 2), this).toString());
        var grandTotal = document.getElementById('<%= txtRateGrandTotalBlendedRates.ClientID %>');
        var grandTotalNumber = grandTotal.value.replace(/\$/g, '');
        var value = parseFloat(grandTotalNumber.replace(",", ""));
        calculateManualSpecialPricingPercentage(round(sum, 2), value);
    }

    function createValuesFromManualTable(id) {
        var table = document.getElementById(id);
        var rowCount = table.rows.length;
        var values = "";

        for (var i = 1; i < rowCount; i++) {
            var row = table.rows[i];
            if (row.cells.length > 0) {
                var description = row.cells[1].childNodes[0];
                values += description.value.toString();

                var qtyHrs = row.cells[2].childNodes[0];
                values += "," + qtyHrs.value.toString();

                var rate = row.cells[3].childNodes[0];
                values += "," + rate.value.toString().replace("$", "").replace(",", "");

                var id = row.cells[5].childNodes[0];
                values += "," + id.value.toString();

                var creationDate = row.cells[6].childNodes[0];
                values += "," + creationDate.value.toString();

                var createdBy = row.cells[7].childNodes[0];
                values += "," + createdBy.value.toString() + "|";
            }
        }
        return values;
    }

    function addToHidden() {
        var hidden = document.getElementById('<%= hidManualSpecialPricing.ClientID %>');
        if (hidden != null) {
            var result = createValuesFromManualTable('dataTable');
            hidden.setAttribute("value", result);
        }
    }

    function CalculateLumpSumPerDuration(lumpSumValueControl, lumpSumDurationControl, lumpSumValuePerDayControl) {
        var txtLumpSumValue = document.getElementById(lumpSumValueControl);
        var txtLumpSumDuration = document.getElementById(lumpSumDurationControl);
        var txtLumpSumValuePerDay = document.getElementById(lumpSumValuePerDayControl);

        if (txtLumpSumValue.value && txtLumpSumDuration.value) {
            var duration = parseFloat(txtLumpSumDuration.value)

            if (!isNaN(duration) && duration > 0)
                txtLumpSumValuePerDay.value = formatCurrency(parseFloat(txtLumpSumValue.value.replace(/\$|\,/g, '')) / parseFloat(txtLumpSumDuration.value));
            else
                txtLumpSumValuePerDay.value = formatCurrency(parseFloat(txtLumpSumValue.value.replace(/\$|\,/g, '')));
        }
    }

    function EnableSpecialPricingType(noSpecialPricingControl, overallDiscountControl, lumpSumControl, manualSpecialPricingControl,
                                            discountControl, lumpSumValueControl, lumpSumDurationControl, lumpSumValuePerDurationControl,
                                            reqDiscountControl, reqLumpSumValueControl, reqLumpSumDurationControl, reqLumpSumValuePerDurationControl,
                                            pnlManualSpecialPricingControl) {
        var rbNoSpecialPricing = document.getElementById(noSpecialPricingControl);
        var rbOverallJobDiscount = document.getElementById(overallDiscountControl);
        var rbLumpSum = document.getElementById(lumpSumControl);
        var rbManualSpecialPricing = document.getElementById(manualSpecialPricingControl);

        var txtOverallJobDiscount = document.getElementById(discountControl);
        var txtLumpSumValue = document.getElementById(lumpSumValueControl);
        var txtLumpSumDuration = document.getElementById(lumpSumDurationControl);
        var txtLumpSumValuePerDuration = document.getElementById(lumpSumValuePerDurationControl);

        var pnlManualSpecialPricing = document.getElementById(pnlManualSpecialPricingControl);

        txtOverallJobDiscount.disabled = true;
        txtLumpSumValue.disabled = true;
        txtLumpSumDuration.disabled = true;
        txtLumpSumValuePerDuration.disabled = true;

        ValidatorEnable($get(reqDiscountControl), false);
        ValidatorEnable($get(reqLumpSumValueControl), false);
        ValidatorEnable($get(reqLumpSumDurationControl), false);
        ValidatorEnable($get(reqLumpSumValuePerDurationControl), false);

        pnlManualSpecialPricing.style.display = 'none';

        if (rbOverallJobDiscount.checked) {
            txtOverallJobDiscount.disabled = false;
            ValidatorEnable($get(reqDiscountControl), true);

            txtLumpSumValue.value = '';
            txtLumpSumDuration.value = '';
            txtLumpSumValuePerDuration.value = '';
        }
        else if (rbLumpSum.checked) {
            txtLumpSumValue.disabled = false;
            txtLumpSumDuration.disabled = false;
            txtLumpSumValuePerDuration.disabled = false;

            ValidatorEnable($get(reqLumpSumValueControl), true);
            ValidatorEnable($get(reqLumpSumDurationControl), true);
            ValidatorEnable($get(reqLumpSumValuePerDurationControl), true);

            txtOverallJobDiscount.value = '';
        }
        else {
            txtOverallJobDiscount.value = '';
            txtLumpSumValue.value = '';
            txtLumpSumDuration.value = '';
            txtLumpSumValuePerDuration.value = '';

            if (rbManualSpecialPricing.checked) {
                pnlManualSpecialPricing.style.display = '';
            }
        }
    }

    function UpdateDashboard(parentFieldId) {
        if (parentFieldId) {
            window.opener.__doPostBack(parentFieldId, '');
            window.close();
        }
    }

    function loadManualSpecialPricingTable() {
        var hidden = document.getElementById('<%= hidManualSpecialPricing.ClientID %>');
        if (hidden != null) {
            var array = hidden.value.split('|');
            for (var i = 0; i < array.length; i++) {
                var row = array[i].toString();
                createloadControls(row, 'dataTable');
            }
        }
    }

    function createloadControls(rowValue, tableID) {
        var table = document.getElementById(tableID);

        var rowCount = table.rows.length;
        var row = table.insertRow(rowCount);

        var cell1 = row.insertCell(0);
        var element1 = document.createElement("input");
        element1.type = "checkbox";
        cell1.appendChild(element1);

        var description = "";
        var hours = "";
        var rate = "";
        var id = "";
        var creationDate = "";
        var createdBy = "";

        var array = rowValue.split(',');
        description = array[0];
        hours = array[1];
        rate = array[2];
        id = array[3];
        creationDate = array[4];
        createdBy = array[5];

        var cell2 = row.insertCell(1);
        var element2 = document.createElement("input");
        element2.type = "text";
        element2.setAttribute("className", "input");
        element2.maxLength = 100;
        element2.onblur = function () { createValuesFromManualTable('dataTable'); }
        element2.setAttribute("value", description);
        cell2.appendChild(element2);

        var cell3 = row.insertCell(2);
        var element3 = document.createElement("input");
        element3.type = "text";
        element3.setAttribute("className", "input");
        element3.value = formatDecimal(0, element3);
        element3.onblur = function () { calculateEstRate(element3, element4, element5); CalculateResourceRevenueTotal("<%=  hfDiscount.ClientID %>"); };
        element3.setAttribute("value", formatDecimal(parseFloat(hours), element3));
        cell3.appendChild(element3);

        var cell4 = row.insertCell(3);
        var element4 = document.createElement("input");
        element4.type = "text";
        element4.setAttribute("className", "input");
        element4.value = formatCurrency(0, element4);
        element4.onblur = function () { calculateEstRate(element3, element4, element5); CalculateResourceRevenueTotal("<%=  hfDiscount.ClientID %>"); };
        element4.setAttribute("value", formatCurrency(parseFloat(rate), element4));
        cell4.appendChild(element4);

        var cell5 = row.insertCell(4);
        var element5 = document.createElement("span");
        element5.innerHTML = "$0.00";
        cell5.appendChild(element5);

        var cell6 = row.insertCell(5);
        var element6 = document.createElement("input");
        element6.type = "hidden";
        element6.setAttribute("value", id.toString());
        cell6.appendChild(element6);

        var cell7 = row.insertCell(6);
        var element7 = document.createElement("input");
        element7.type = "hidden";
        element7.setAttribute("value", creationDate.toString());
        cell7.appendChild(element7);

        var cell8 = row.insertCell(7);
        var element8 = document.createElement("input");
        element8.type = "hidden";
        element8.setAttribute("value", createdBy.toString());
        cell8.appendChild(element8);

        cell8.style.display = "none";
        cell7.style.display = "none";
        cell6.style.display = "none";

        calculateEstRate(element3, element4, element5);
        CalculateResourceRevenueTotal("<%=  hfDiscount.ClientID %>");
    }
    </script>
    <script type="text/javascript" src="Scripts/formatFunctions.js"></script>
</asp:Content>
