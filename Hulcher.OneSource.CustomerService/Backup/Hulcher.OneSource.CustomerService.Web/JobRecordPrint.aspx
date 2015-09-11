<%@ Page Language="C#" MasterPageFile="~/ContentPage.master" AutoEventWireup="true"
    CodeBehind="JobRecordPrint.aspx.cs" Inherits="Hulcher.OneSource.CustomerService.Web.JobRecordPrint" %>

<%@ MasterType TypeName="Hulcher.OneSource.CustomerService.Web.ContentPage" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
    <link rel="icon" type="image/png" href="images/money.png" />
    <link rel="Shortcut Icon" type="image/png" href="images/money.png" />
    <link href="../../Styles/JobRecord.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/SorterBasePrint.css" media="print" rel="stylesheet" type="text/css" />
    <link href="../../Styles/SorterBasePrint.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/JobRecord.css" media="print" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Default.css" media="print" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            window.resizeTo(870, 600);
            self.print();
            self.close();
        });
    </script>
    <div style="min-width: 800px;">
        <div style="display: inline-block; float: left; text-align: right; padding-bottom: 10px;
            height: 20px;">
            <asp:Label ID="lblTitle" runat="server" Font-Size="Large" Font-Bold="true" />
        </div>
        <div style="display: inline-block; float: right; text-align: right; padding-bottom: 10px;
            height: 20px;">
            <div style="display: inline-block; float: left; text-align: right; padding-top: 5px;">
                <asp:Label ID="lblEmergencyResponse" runat="server" Text="Emergency: " />
            </div>
            <div>
                <asp:RadioButtonList ID="rblEmergencyResponse" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="No" Value="0" Selected="True" />
                    <asp:ListItem Text="Yes" Value="1" />
                </asp:RadioButtonList>
            </div>
        </div>
        <br />
        <br />
        <%--Customer Info--%>
        <div class="Header">
            <asp:Label ID="lblCustomerInfoTitle" runat="server" Text="Contact Info (Who)"></asp:Label>
        </div>
        <div class="Content" style="display: inline-block">
            <div class="CustomerInfoSource" style="float: left;">
                <fieldset class="CustomerInfoSource groupBox">
                    <legend>
                        <asp:Label ID="lblGroupSource" runat="server" Text="Source"></asp:Label></legend>
                    <div class="CustomerInfoSource title">
                        <asp:Label ID="lblCustomerContact" runat="server" Text="* Primary Contact:"></asp:Label>
                    </div>
                    <div class="CustomerInfoSource control">
                        <asp:Label ID="lblCustomerContactDescription" runat="server"></asp:Label>
                    </div>
                    <div class="CustomerInfoSource title">
                        <asp:Label ID="lblPOC" runat="server" Text="Hulcher Contact:"></asp:Label>
                    </div>
                    <div class="CustomerInfoSource control">
                        <asp:Label ID="lblPOCDescription" runat="server"></asp:Label>
                    </div>
                    <div class="CustomerInfoSource title">
                        <asp:Label ID="lblDivision" runat="server" Text="Division:"></asp:Label>
                    </div>
                    <div class="CustomerInfoSource control">
                        <asp:Label ID="lblCIDivisionDescription" runat="server"></asp:Label>
                    </div>
                </fieldset>
            </div>
            <div class="CustomerInfo">
                <fieldset class="CustomerInfo groupBox">
                    <legend>
                        <asp:Label ID="lblGroupCustomer" runat="server" Text="Company Info"></asp:Label></legend>
                    <div class="title">
                        <asp:Label ID="lblCustomer" runat="server" Text="* Company:"></asp:Label></div>
                    <div class="control">
                        <asp:Label ID="lblCustomerDescription" runat="server"></asp:Label>
                    </div>
                    <div class="title">
                        <asp:Label ID="lblEic" runat="server" Text="On-Site:"></asp:Label>
                    </div>
                    <div class="control">
                        <asp:Label ID="lblEICDescription" runat="server"></asp:Label>
                    </div>
                    <div class="title">
                        <asp:Label ID="lblAdditionalContact" runat="server" Text="Secondary Contact:"></asp:Label>
                    </div>
                    <div class="control">
                        <asp:Label ID="lblAdditionalContactDescription" runat="server"></asp:Label>
                    </div>
                    <div class="title">
                        <asp:Label ID="lblBillToContact" runat="server" Text="Bill To:"></asp:Label>
                    </div>
                    <div class="control">
                        <asp:Label ID="lblBillToContactDescription" runat="server"></asp:Label>
                    </div>
                </fieldset>
            </div>
        </div>
        <br />
        <%--Job Info--%>
        <div class="Header">
            <asp:Label ID="lblJobInfoTitle" runat="server" Text="Job Info (What & When)"></asp:Label>
        </div>
        <div class="Content" style="display: inline-block">
            <div style="float: left; width: 490px; display: inline-block;">
                <table>
                    <tr>
                        <td style="text-align: right">
                            *Initial Call Date:
                        </td>
                        <td>
                            <asp:Label ID="lblCallDateDescription" runat="server"></asp:Label>
                        </td>
                        <td style="text-align: right">
                            *Job Action:
                        </td>
                        <td>
                            <asp:Label ID="lblJobActionDescription" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            *Initial Call Time:
                        </td>
                        <td>
                            <asp:Label ID="lblInitialCallTimeDescription" runat="server"></asp:Label>
                        </td>
                        <td style="text-align: right">
                            *Job Category:
                        </td>
                        <td>
                            <asp:Label ID="lblJobCategoryDescription" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            *Job Status:
                        </td>
                        <td>
                            <asp:Label ID="lblJobStatusDescription" runat="server"></asp:Label>
                        </td>
                        <td style="text-align: right">
                            *Job Type:
                        </td>
                        <td>
                            <asp:Label ID="lblJobTypeDescription" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            *Price Type:
                        </td>
                        <td>
                            <asp:Label ID="lblPriceTypeDescription" runat="server"></asp:Label>
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
                            <asp:Label ID="lblJobStartDateDescription" runat="server"></asp:Label>
                        </td>
                        <td style="text-align: right">
                            Job Close Date:
                        </td>
                        <td>
                            <asp:Label ID="lblJobCloseDateDescription" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="text-align: right">
                            Project Manager:
                        </td>
                        <td>
                            <asp:Label ID="lblProjectManagerDescription" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div style="width: 380px;">
                                <asp:GridView ID="gdvDivision" runat="server" CssClass="ScrollableGridView" AutoGenerateColumns="false"
                                    ShowFooter="false">
                                    <Columns>
                                        <asp:CompositeBoundField HeaderText="ID" DataField="CS_Division.ID" Visible="false"
                                            ItemStyle-Width="0px">
                                        </asp:CompositeBoundField>
                                        <asp:CompositeBoundField HeaderText="Name" DataField="CS_Division.Name" ItemStyle-Width="120px">
                                        </asp:CompositeBoundField>
                                        <asp:TemplateField HeaderText="Primary Division">
                                            <ItemStyle Width="120px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:RadioButton ID="radPrimaryDivision" runat="server" GroupName="PrimaryDivision"
                                                    Checked='<%# Eval("PrimaryDivision") %>' />
                                                <asp:HiddenField ID="hfDivisionId" runat="server" Value='<%# Eval("CS_Division.ID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="float: right; width: 300px; display: inline-block;">
                <div id="divSpecialPricing" runat="server" style="display: none;">
                    <div class="Header">
                        <asp:Label ID="lblSpecialPricingInfoTitle" runat="server" Text="Special Pricing Info"></asp:Label>
                    </div>
                    <div class="Content">
                        <div class="PermitForm">
                            <div class="Form">
                                <asp:RadioButton ID="rbNoSpecialPricing" runat="server" Text="No Special Pricing"
                                    GroupName="SpecialPricing" />
                            </div>
                            <div class="Form">
                                <asp:RadioButton ID="rbOverallSpecialPricing" runat="server" Text="Overall Job Discount"
                                    GroupName="SpecialPricing" />&nbsp;&nbsp;
                                <asp:Label ID="lblOverallSpecialPricing" runat="server"></asp:Label>
                                <asp:Label ID="lblPercentOVP" runat="server" Text="%"></asp:Label>
                            </div>
                            <div>
                                <asp:RadioButton ID="rbLumpSum" runat="server" Text="Total Project Lump Sum (For Day)"
                                    GroupName="SpecialPricing" />
                            </div>
                            <div class="Form" style="text-align: right">
                                <asp:Label ID="lblLumpSum" runat="server"></asp:Label>
                            </div>
                            <div class="Form">
                                <asp:RadioButton ID="rbManualCalculation" runat="server" Text="Manual Special Pricing Calculation"
                                    GroupName="SpecialPricing" />
                            </div>
                            <div>
                                <asp:Label ID="lblSpecialPricingNotesLabel" runat="server" Text="Special Pricing Notes:"></asp:Label></div>
                            <div>
                                <asp:CountableTextBox ID="txtSpecialPricingNotes" runat="server" TextMode="MultiLine"
                                    Width="250px" Height="100px" CssClass="input" MaxChars="255" MaxCharsWarning="200"></asp:CountableTextBox>
                            </div>
                            <div class="Label">
                                <asp:Label ID="Label9" runat="server" Text="Approving RVP:"></asp:Label></div>
                            <div class="Form">
                                <asp:Label ID="lblApprovingRVPDescription" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <br />
                </div>
                <div id="divPresetInfo" runat="server" style="display: none;">
                    <div class="Header">
                        <asp:Label ID="lblPresetInfoTitle" runat="server" Text="Preset Info"></asp:Label>
                    </div>
                    <div class="Content">
                        <table>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblPresetInstructionsTitle" Text="Preset Instructions:" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblPresetInstructionsDescription" runat="server"></asp:Label>
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
                                    <asp:Label ID="lblPresetDateDescription" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblPresetTimeTitle" runat="server" Text="Preset Time:"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblPresetTimeDescription" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                </div>
                <div id="divLostJobInfo" runat="server" style="display: none;">
                    <div class="Header">
                        <asp:Label ID="lblLostJobInfoTitle" runat="server" Text="Lost Job Info"></asp:Label>
                    </div>
                    <div class="Content">
                        <table>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblLostJobNotesTitle" runat="server" Text="Lost Job Notes:"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblLostJobNotesDescription" runat="server"></asp:Label>
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
                                    <asp:Label ID="lblLostJobReasonDescription" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblCompetitor" runat="server" Text="Competitor:"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblCompetitorDescription" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblPocFollowUp" runat="server" Text="Customer Contact:"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblPocFollowUpDescription" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblHsirep" runat="server" Text="HSI Rep"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblHsirepDescription" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                </div>
                <asp:Panel ID="pnlCustomerSpecifcInfo" runat="server" Visible="false">
                    <div class="Header">
                        <asp:Label ID="lblCustomerSpecificInfoTitle" runat="server" Text="Company Specific Info"></asp:Label>
                    </div>
                    <div class="Content">
                        <asp:PlaceHolder ID="plhCustomerSpecificInfo" runat="server" />
                    </div>
                </asp:Panel>
                <br />
                <asp:CheckBox ID="ckbInterimBill" runat="server" Text="Interim Bill" />
                <br />
                <table>
                    <tr>
                        <td style="text-align: right">
                            Requested By:
                        </td>
                        <td>
                            <asp:Label ID="lblRequestedByDescription" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            Frequency:
                        </td>
                        <td>
                            <asp:Label ID="lblFrequencyDescription" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <br />
        <div class="Header" id="ContractHeader" runat="server" style="display: none;">
            <asp:Label ID="lblGroupContract" runat="server" Text="Company Contract Table"></asp:Label>
        </div>
        <div class="Content" id="ContractContent" runat="server" style="display: none; margin-bottom: 20px;">
            <asp:GridView ID="gdvCustomerContract" runat="server" Style="display: inline-block"
                AutoGenerateColumns="false" ShowFooter="false">
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
            </asp:GridView>
        </div>
        <%--Location Info--%>
        <div class="Header">
            <asp:Label ID="lblLocationInfo" runat="server" Text="Location Info (Where)"></asp:Label>
        </div>
        <div class="Content">
            <div style="display: inline-block">
                <div class="LocationForm">
                    <div class="title">
                        <asp:Label ID="Label2" runat="server" Text="Country:"></asp:Label>
                    </div>
                    <div class="control">
                        <asp:Label ID="lblCountryDescription" runat="server"></asp:Label>
                    </div>
                    <div class="title">
                        <asp:Label ID="Label3" runat="server" Text="* State:"></asp:Label>
                    </div>
                    <div class="control">
                        <asp:Label ID="lblStateDescription" runat="server"></asp:Label>
                    </div>
                    <div class="title">
                        <asp:Label ID="Label4" runat="server" Text="* City:"></asp:Label>
                    </div>
                    <div class="control">
                        <asp:Label ID="lblCityDescription" runat="server"></asp:Label>
                    </div>
                    <div class="title">
                        <asp:Label ID="Label5" runat="server" Text="* Zip Code:"></asp:Label>
                    </div>
                    <div class="control">
                        <asp:Label ID="lblZipCodeDescription" runat="server"></asp:Label>
                    </div>
                    <div class="title">
                        <asp:Label ID="Label6" runat="server" Text="Site Name:"></asp:Label>
                    </div>
                    <div class="control">
                        <asp:Label ID="lblSiteNameDescription" runat="server"></asp:Label>
                    </div>
                    <div class="title">
                        <asp:Label ID="Label7" runat="server" Text="Alternate Name:"></asp:Label>
                    </div>
                    <div class="control">
                        <asp:Label ID="lblAlternateLocationDescription" runat="server"></asp:Label>
                    </div>
                    <div class="title">
                        <asp:Label ID="Label8" runat="server" Text="Directions:"></asp:Label>
                    </div>
                    <div class="control">
                        <asp:Label ID="lblDirectionsDescription" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <%--Job Description--%>
        <div class="Header">
            <asp:Label ID="lblJobDescription" runat="server" Text="Job Description (What)"></asp:Label>
        </div>
        <div class="Content" style="display: inline-block">
            <div>
                <div class="JobDescriptionDerailment">
                    <fieldset class="groupBox">
                        <legend>
                            <asp:Label ID="lblRerailment" runat="server" Text="Derailment"></asp:Label>
                        </legend>
                        <div class="title" style="margin-top: 20px">
                            <asp:Label ID="lblNumberEngines" runat="server" Text="# of Engines:"></asp:Label>
                        </div>
                        <div class="control" style="margin-top: 20px">
                            <asp:Label ID="lblNumberEnginesDescription" runat="server"></asp:Label>
                        </div>
                        <div class="title">
                            <asp:Label ID="lblNumberLoads" runat="server" Text="# of Loads:"></asp:Label>
                        </div>
                        <div class="control">
                            <asp:Label ID="lblNumberLoadsDescription" runat="server"></asp:Label>
                        </div>
                        <div class="title">
                            <asp:Label ID="lblNumberEmpties" runat="server" Text="# of Empties:"></asp:Label>
                        </div>
                        <div class="control">
                            <asp:Label ID="lblNumberEmptiesDescription" runat="server"></asp:Label>
                        </div>
                    </fieldset>
                </div>
                <div class="JobDescriptionCommodities">
                    <fieldset class="groupBox">
                        <legend>
                            <asp:Label ID="lblCommodities" runat="server" Text="Commodities"></asp:Label>
                        </legend>
                        <div class="title" style="margin-top: 20px">
                            <asp:Label ID="lblLading" runat="server" Text="Lading:"></asp:Label>
                        </div>
                        <div class="control" style="margin-top: 20px">
                            <asp:Label ID="lblLadingDescription" runat="server"></asp:Label>
                        </div>
                    </fieldset>
                </div>
                <div class="JobDescriptionChemical" style="float: right;">
                    <fieldset class="groupBox">
                        <legend>
                            <asp:Label ID="lblChemical" runat="server" Text="Chemical"></asp:Label>
                        </legend>
                        <div class="title" style="margin-top: 20px">
                            <asp:Label ID="lblUnNumber" runat="server" Text="UN#:"></asp:Label>
                        </div>
                        <div class="control" style="margin-top: 20px">
                            <asp:Label ID="lblUnNumberDescription" runat="server"></asp:Label>
                        </div>
                        <div class="title">
                            <asp:Label ID="lblStccInfo" runat="server" Text="STCC Info:"></asp:Label>
                        </div>
                        <div class="control">
                            <asp:Label ID="lblStccInfoDescription" runat="server"></asp:Label>
                        </div>
                        <div class="title">
                            <asp:Label ID="lblHazmat" runat="server" Text="Hazmat:"></asp:Label>
                        </div>
                        <div class="control">
                            <asp:Label ID="lblHazmatDescription" runat="server"></asp:Label>
                        </div>
                    </fieldset>
                </div>
            </div>
            <div class="title" style="margin-top: 20px; width: 100%;">
                <asp:Label ID="lblScopeofWork" runat="server" Text="Scope of Work:"></asp:Label>
            </div>
            <div style="margin-top: 18px;">
                <asp:GridView ID="sgvScopeOfWork" runat="server" OnRowDataBound="sgvScopeOfWork_RowDataBound"
                    AutoGenerateColumns="false" Width="100%">
                    <Columns>
                        <asp:BoundField HeaderText="Scope of Work" DataField="ScopeOfWork"
                            ItemStyle-Width="250px" />
                        <asp:BoundField HeaderText="Created By" DataField="CreatedBy" />
                        <asp:BoundField HeaderText="Create Date" />
                        <asp:BoundField HeaderText="Create Time" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <br />
        <%--Equipment Requested--%>
        <div class="Header">
            <asp:Label ID="lblEquipmentRequest" runat="server" Text="Equipment Requested" />
        </div>
        <div class="Content" style="display: inline-block">
            <asp:GridView runat="server" ID="grdEquipmentRequest" Width="50%"
                AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField HeaderText="Equipment Requested" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblType" runat="server" Text='<%# Eval("CS_LocalEquipmentType.Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="QTY" DataField="Quantity" ItemStyle-HorizontalAlign="Right" />
                </Columns>
            </asp:GridView>
        </div>
        <br />
        <%--Permit Info--%>
        <div class="Header">
            <asp:Label ID="lblPermitInfoTitle" runat="server" Text="Permit Info" />
        </div>
        <div class="Content" style="display: inline-block">
            <asp:GridView runat="server" ID="grdPermitInfo" OnRowDataBound="grdPermitInfo_RowDataBound"
                AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField HeaderText="Permit Type">
                        <ItemTemplate>
                            <asp:Label ID="lblType" runat="server" Text='<%# Eval("CS_JobPermitType.Description") %>'></asp:Label>
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
                </Columns>
            </asp:GridView>
        </div>
        <br />
        <%--Photo Report--%>
        <div class="Header">
            <asp:Label ID="lblPhotoReportTitle" runat="server" Text="Photo Report" />
        </div>
        <div class="Content" style="display: inline-block">
            <asp:GridView runat="server" ID="grdPhotoReport" OnRowDataBound="grdPhotoReport_RowDataBound"
                AutoGenerateColumns="false">
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
                </Columns>
            </asp:GridView>
        </div>
        <br />
        <%--Job Call Log--%>
        <div class="Header">
            <asp:Label ID="lblCallLogTitle" runat="server" Text="Job Call Log"></asp:Label>
        </div>
        <div class="Content" style="display: inline-block">
            <asp:GridView ID="sgvCallLog" runat="server" CssClass="ScrollableGridView" AutoGenerateColumns="false"
                ShowFooter="false">
                <Columns>
                    <asp:BoundField HeaderText="" DataField="ID" Visible="false" />
                    <asp:BoundField HeaderText="Call Date" DataField="CallDate" />
                    <asp:BoundField HeaderText="Call Time" DataField="CallDate" />
                    <asp:TemplateField HeaderText="Call Type">
                        <ItemTemplate>
                            <asp:Label ID="lblType" runat="server" Text='<%# Eval("CS_CallType.Description") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Modified By" DataField="ModifiedBy" />
                    <asp:TemplateField HeaderText="Details">
                        <ItemTemplate>
                            <div>
                                <asp:Label ID="lblTool" runat="server" Text='<%# Eval("Note") %>' />
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
