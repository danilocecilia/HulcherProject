<%@ Page Language="C#" MasterPageFile="~/ContentPage.master" AutoEventWireup="true"
    CodeBehind="JobRecord.aspx.cs" Inherits="Hulcher.OneSource.CustomerService.Web.JobRecord" %>

<%@ MasterType TypeName="Hulcher.OneSource.CustomerService.Web.ContentPage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/AutoCompleteTextbox.ascx" TagName="AutoCompleteTextbox"
    TagPrefix="uc1" %>
<%@ Register TagName="LocationInfo" TagPrefix="uc" Src="~/UserControls/JobRecord/LocationInfo.ascx" %>
<%@ Register TagName="JobCallLog" TagPrefix="uc" Src="~/UserControls/JobRecord/JobCallLog.ascx" %>
<%@ Register TagName="PermitInfo" TagPrefix="uc" Src="~/UserControls/JobRecord/PermitInfo.ascx" %>
<%@ Register TagName="PhotoReport" TagPrefix="uc" Src="~/UserControls/JobRecord/PhotoReport.ascx" %>
<%@ Register TagName="CollapseHolder" TagPrefix="uc" Src="~/UserControls/CollapseHolder.ascx" %>
<%@ Register TagName="CustomerInfo" TagPrefix="uc" Src="~/UserControls/JobRecord/CustomerInfo.ascx" %>
<%@ Register TagName="JobDescription" TagPrefix="uc" Src="~/UserControls/JobRecord/JobDescription.ascx" %>
<%@ Register TagName="JobInfo" TagPrefix="uc" Src="~/UserControls/JobRecord/JobInfo.ascx" %>
<%@ Register TagName="EquipmentRequested" TagPrefix="uc" Src="~/UserControls/JobRecord/EquipmentRequested.ascx" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
    <link rel="icon" type="image/png" href="images/money.png" />
    <link rel="Shortcut Icon" type="image/png" href="images/money.png" />
    <link href="../../Styles/JobRecord.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/jquery.multiselect.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Forms.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/EssentialJobData.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <script src="Scripts/DirtyFormValidator.js" type="text/javascript" defer="defer"></script>
    <script type="text/javascript" language="javascript" defer="defer">
        $(document).ready(function () {
            try {
                window.resizeTo(1040, 600);
            }
            catch (err) {
            }
        });

        $(window).bind("beforeunload", function (evt) {
            var message = 'If you close the page without saving, the Initial Advise will not be generated, Do want to proced?';
            hfValue = document.getElementById('<%= hfCreateInitialAdvise.ClientID %>');

            if (typeof evt == 'undefined') {
                evt = window.event;
            }
            if (evt) {
                evt.returnValue = message;
            }

            if (hfValue.value.toUpperCase() == "TRUE")
                return message;

            doBeforeUnload();
        });

        function RedirectEmailPage(jobID) {
            var initialEmail = document.getElementById("hidSaveInitialEmail");

            if (initialEmail.value == "true") {
                var emailWindow = window.open('/Email.aspx?JobID=' + jobID, '', 'width=800, height=600, scrollbars=1, resizable=yes');
                initialEmail.value = "false"
            }

        }

        function SaveInitialEmail() {
            var initialEmail = document.getElementById("hidSaveInitialEmail");

            if (confirm("Do you want to send the initial advise email?")) {
                initialEmail.value = "true";
            }

            else {
                initialEmail.value = "false";
            }
        }


        /* Global Variables */
        var tabMap;
        var prefixes = { 'customerInfo': 'ContentPlaceHolder1_Content_chCustomerInfo_uscCustomer_',
            'jobInfo': 'ContentPlaceHolder1_Content_chJobInfo_uscJobInfo_',
            'locationInfo': 'ContentPlaceHolder1_Content_chLocationInfo_uscLocation_',
            'jobDescription': 'ContentPlaceHolder1_Content_chJobDescription_uscJobDescription_'
        };
        var controls = [
                    prefixes['customerInfo'] + 'actCustomerContact_actCustomerContact_text',
                    prefixes['customerInfo'] + 'actPOC_actPOC_text',
                    prefixes['customerInfo'] + 'actCustomer_actCustomer_text',
                    prefixes['jobInfo'] + 'dpDatePicker_txtDatePicker',
                    prefixes['jobInfo'] + 'txtInitialCallTime',
                    prefixes['jobInfo'] + 'ddlJobStatus_TextBox',
                    prefixes['jobInfo'] + 'ddlPriceType_TextBox',
                    prefixes['jobInfo'] + 'ddlDivision_TextBox',
                    prefixes['jobInfo'] + 'btnAddDivision',
                    prefixes['jobInfo'] + 'ddlJobAction_TextBox',
                    prefixes['locationInfo'] + 'ddlCountry_TextBox',
                    prefixes['locationInfo'] + 'actState_actState_text',
                    prefixes['locationInfo'] + 'actCity_actCity_text',
                    prefixes['locationInfo'] + 'actZipCode_actZipCode_text',
                    prefixes['jobDescription'] + 'txtScope',
                    prefixes['jobDescription'] + 'btnAdd',
                    'ContentPlaceHolder1_Content_btnSave'
            ];

        $(document).ready(function () {
            /* Initialize global tabMap variable */
            // tabMap = new TabIndexNavigation(controls);
            // tabMap.createMap();
        });

        //Instance of ScripManager
        var scriptManager = Sys.WebForms.PageRequestManager.getInstance();

        scriptManager.add_endRequest(function () {
            // tabMap.createMap();
        });

        //  Creates the automatic navigation throught the required fields
        //  author: Guilherme Rey
        //  date: 30/01/2012
        //  Params:
        //      @controls: Ids list to map through
        var TabIndexNavigation = function (controls) {
            this.controls = controls;
            this.toMap = new Array();

            this.createMap = function () {
                $('#' + this.controls[this.controls.length - 1]).blur(function () {
                    $('#' + tabMap.controls[0]).focus();
                });

                var divisionId = '#ContentPlaceHolder1_Content_chCustomerInfo_uscCustomer_ddlDivision_TextBox';

                this.toMap = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16];

                // If the primary contact is filled in, jump over hulcher contact
                if ($('#' + this.controls[0]).val() != '') {
                    this.toMap = [0, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16];
                }
                else if ($('#' + this.controls[1]).val() != '') {
                    // If customer info division info was filled, jump over job info division
                    if (!isNaN(parseInt($(divisionId).val()))) {
                        this.toMap = [0, 1, 2, 3, 4, 5, 6, 9, 10, 11, 12, 13, 14, 15, 16];
                    }
                }

                // Creates the tabindex navigation
                var index = 0;
                for (var i = 0; i < this.toMap.length; i++) {
                    $('#' + this.controls[this.toMap[i]]).attr('tabindex', ++index);
                }
            };
        };

    </script>
    <asp:Panel ID="pnlForDefaltButton" runat="server">
        <asp:HiddenField ID="hidSaved" runat="server" Value="" ClientIDMode="Static" />
        <asp:HiddenField ID="hidSaveInitialEmail" runat="server" Value="false" ClientIDMode="Static" />
        <asp:HiddenField ID="hfEmail" runat="server" />
        <div style="min-width: 800px;">
            <asp:ValidationSummary ID="vsJobRecord" runat="server" CssClass="errorbox" ValidationGroup="JobRecord"
                HeaderText="Please correct the following information" />
            <asp:UpdatePanel ID="upTitle" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnSaveAndContinue" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnSaveClose" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div style="display: inline-block; float: left; text-align: right; padding-bottom: 10px;
                        height: 20px;">
                        <asp:Label ID="lblTitle" runat="server" Font-Size="Large" Font-Bold="true" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div style="display: inline-block; float: right; text-align: right; padding-bottom: 10px;
                height: 20px;">
                <div style="display: inline-block; float: left; text-align: right; padding-top: 5px;">
                    <asp:Label ID="lblEmergencyResponse" runat="server" Text="Emergency: " />
                </div>
                <div>
                    <asp:RadioButtonList ID="rblEmergencyResponse" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="No" Value="False" Selected="True" />
                        <asp:ListItem Text="Yes" Value="True" />
                    </asp:RadioButtonList>
                </div>
            </div>
            <br />
            <br />
            <uc:CollapseHolder ID="chCustomerInfo" runat="server" GridViewCssClass="ScrollableGridView"
                Collapsed="false">
                <Header>
                    <asp:Label ID="lblCustomerInfoTitle" runat="server" Text="Contact Info (Who)"></asp:Label>
                </Header>
                <Content>
                    <uc:CustomerInfo ID="uscCustomer" EnableViewState="true" runat="server" ValidationGroup="JobRecord" />
                </Content>
            </uc:CollapseHolder>
            <br />
            <uc:CollapseHolder ID="chJobInfo" runat="server" GridViewCssClass="ScrollableGridView"
                Collapsed="false">
                <Header>
                    <asp:Label ID="lblJobInfoTitle" runat="server" Text="Job Info (What & When)"></asp:Label>
                </Header>
                <Content>
                    <uc:JobInfo ID="uscJobInfo" ValidationGroup="JobRecord" EnableViewState="true" runat="server"
                        OnJobTypeChanged="uscJobInfo_JobTypeChanged" />
                </Content>
            </uc:CollapseHolder>
            <br />
            <uc:CollapseHolder ID="chLocationInfo" runat="server" GridViewCssClass="ScrollableGridView"
                Collapsed="false">
                <Header>
                    <asp:Label ID="lblLocationInfo" runat="server" Text="Location Info (Where)"></asp:Label>
                </Header>
                <Content>
                    <uc:LocationInfo ID="uscLocation" runat="server" ValidationGroup="JobRecord" />
                </Content>
            </uc:CollapseHolder>
            <br />
            <uc:CollapseHolder ID="chJobDescription" runat="server" GridViewCssClass="ScrollableGridView"
                Collapsed="false">
                <Header>
                    <asp:Label ID="lblJobDescription" runat="server" Text="Job Description (What)"></asp:Label>
                </Header>
                <Content>
                    <uc:JobDescription ID="uscJobDescription" runat="server" ValidationGroup="JobRecord" />
                </Content>
            </uc:CollapseHolder>
            <br />
            <asp:UpdatePanel ID="updEquipmentRequest" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnSaveAndContinue" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnSaveClose" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <uc:CollapseHolder ID="chEquipmentRequested" runat="server" GridViewCssClass="ScrollableGridView"
                        Collapsed="false">
                        <Header>
                            <asp:Label ID="lblEquipmentRequested" runat="server" Text="Equipment Requested"></asp:Label>
                        </Header>
                        <Content>
                            <uc:EquipmentRequested ID="uscEquipmentRequested" runat="server" ValidationGroup="JobRecord" />
                        </Content>
                    </uc:CollapseHolder>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <uc:CollapseHolder ID="chPermitInfo" runat="server" GridViewCssClass="ScrollableGridView">
                <Header>
                    <asp:Label ID="lblPermitInfoTitle" runat="server" Text="Permit Info" />
                </Header>
                <Content>
                    <uc:PermitInfo ID="uscPermitInfo" runat="server" />
                </Content>
            </uc:CollapseHolder>
            <br />
            <uc:CollapseHolder ID="chPhotoReport" runat="server" GridViewCssClass="ScrollableGridView">
                <Header>
                    <asp:Label ID="lblPhotoReportTitle" runat="server" Text="Photos and Documents" />
                </Header>
                <Content>
                    <uc:PhotoReport ID="uscPhotoReport" runat="server" />
                </Content>
            </uc:CollapseHolder>
            <br />
            <uc:CollapseHolder ID="chJobCallLog" runat="server" GridViewCssClass="ScrollableGridView">
                <Header>
                    <asp:Label ID="lblCallLogTitle" runat="server" Text="Job Call Log"></asp:Label>
                </Header>
                <Content>
                    <uc:JobCallLog ID="uscJobCallLog" runat="server" />
                </Content>
            </uc:CollapseHolder>
        </div>
    </asp:Panel>
    <div style="width: 100%; padding-top: 10px; text-align: right;">
        <asp:UpdatePanel ID="upButtons" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Button ID="btnJobCloning" runat="server" Text="Job Cloning" CssClass="btn" CausesValidation="false"
                    Enabled="false" />&nbsp;
                <asp:Button ID="btnSave" runat="server" Text="Save & Notify" CssClass="btn" OnClick="btnSave_Click"
                    ValidationGroup="JobRecord" OnClientClick="ignoreDirty();UpdateScrollableGridViewHeader('ScrollableGridView');"
                    CausesValidation="true" />&nbsp;
                <asp:Button ID="btnSaveAndContinue" runat="server" Text="Save & Continue" CssClass="btn"
                    OnClick="btnSaveContinue_Click" Visible="false" ValidationGroup="JobRecord" OnClientClick="ignoreDirty();UpdateScrollableGridViewHeader('ScrollableGridView');"
                    CausesValidation="true" />
                <asp:Button ID="btnSaveClose" runat="server" Text="Save & Close" CssClass="btn" OnClick="btnSaveClose_Click"
                    Visible="false" ValidationGroup="JobRecord" OnClientClick="ignoreDirty();UpdateScrollableGridViewHeader('ScrollableGridView');"
                    CausesValidation="true" />
                <input id="btnCancel" type="button" value="Cancel" class="btn" onclick="window.close();" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:UpdatePanel ID="upButtonsLower" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSaveAndContinue" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSaveClose" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <div style="display: inline-block; float: left; padding-top: 5px;">
                <asp:Button ID="btnSearchBidRecord" runat="server" CssClass="btn" Text="Search Bid Record"
                    CausesValidation="false" Enabled="false" />
            </div>
            <div style="float: right; text-align: right; padding-top: 5px;">
                <input type="button" id="btnResourceAllocation" runat="server" class="btn" value="Resource Allocation"
                    causesvalidation="false" disabled="disabled" />&nbsp;
                <input type="button" id="btnCallEntry" runat="server" class="btn" value="Call Entry"
                    causesvalidation="false" disabled="disabled" />
            </div>
            <asp:HiddenField ID="hfCreateInitialAdvise" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript" src="/Scripts/jquery.multiselect.min.js"></script>
    <script type="text/javascript" language="javascript">

        
    </script>
</asp:Content>
