<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LocationInfo.ascx.cs"
    Inherits="Hulcher.OneSource.CustomerService.Web.UserControls.JobRecord.LocationInfo" %>
<%@ Register Src="~/UserControls/AutoCompleteTextbox.ascx" TagName="AutoCompleteTextbox" TagPrefix="uc1" %>
<div class="LocationForm">
    <asp:UpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="title">
                * Country</div>
            <div class="control">
                <asp:ComboBox ID="ddlCountry" DropDownStyle="DropDownList" runat="server" AutoCompleteMode="SuggestAppend"
                    AutoPostBack="true" CssClass="WindowsStyle" RenderMode="Block" OnSelectedIndexChanged="ComboBoxCountry_OnSelectedIndexChanged"
                    CaseSensitive="false">
                </asp:ComboBox>
            </div>
            <div class="title">
                * State:</div>
            <div class="control">
                <uc1:autocompletetextbox id="actState" runat="server" gridviewbuttonimageurl="~/Images/money.png"
                    textboxwidth="120px" gridviewidname="ID" displayfield="" autopostback="false"
                    requiredfield="true" windowtitle="Quick Job - Find State" errormessage="Location Info - State field is required"
                    autocompletesource="State" columnheaderlist="Acronym,Name" columnvaluelist="Acronym,Name"
                    servicemethod="GetStateList" textboxcssclass="input"
                    controlstoupdate="actCity" ontextchanged="actState_TextChanged" scripttoexecute="SetFocusToCity" ContextKey="1"
                    minimumprefixlength="2" />
            <div class="title">
                * City:</div>
            <div class="control">
                <uc1:autocompletetextbox id="actCity" runat="server" gridviewbuttonimageurl="~/Images/money.png"
                    textboxwidth="120px" gridviewidname="ID" displayfield="Name" autopostback="false"
                    requiredfield="true" windowtitle="Quick Job - Find City" errormessage="Location Info - City field is required"
                    autocompletesource="City" columnheaderlist="Name" columnvaluelist="Name" servicemethod="GetCityList"
                    textboxcssclass="input" behaviorid="actCity" controlstoupdate="actZipCode" ontextchanged="actCity_TextChanged"
                    scripttoexecute="CallCityWebService" />
            </div>
            <div class="title">
                * Zip Code:</div>
            <div class="control">
                <uc1:autocompletetextbox id="actZipCode" runat="server" gridviewbuttonimageurl="~/Images/money.png"
                    textboxwidth="120px" gridviewidname="ID" displayfield="" autopostback="false"
                    requiredfield="true" windowtitle="Quick Job - Find Division" errormessage="Location Info - Zip Code field is required"
                    autocompletesource="ZipCode" columnheaderlist="Name" columnvaluelist="Name" servicemethod="GetZipCodeList"
                     textboxcssclass="input" behaviorid="actZipCode" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="title">
        Site Name:</div>
    <div class="control">
        <asp:TextBox ID="txtSiteName" MaxLength="100" runat="server" CssClass="input" Width="170px"></asp:TextBox></div>
    <div class="title">
        Alternate Name:</div>
    <div class="control">
        <asp:TextBox ID="txtAlternateLocation" MaxLength="100" runat="server" CssClass="input"
            Width="170px"></asp:TextBox></div>
</div>
<div class="teste">
    <div class="title">
        Directions:</div>
    <div>
        <asp:CountableTextBox ID="txtDirections" runat="server" TextMode="MultiLine" CssClass="input"
            Height="150px" Width="380" MaxChars="500" MaxCharsWarning="450"></asp:CountableTextBox>
    </div>
</div>

<script>
    function SetFocusToCity(WebServiceResult) {
        document.getElementById('<%=actCity.TextControlClientID%>').focus();
    }

    function CallCityWebService(cityId) {
        if (cityId != 0) {
            tempuri.org.IJSONService.GetZipCodeByCity(cityId, CallCityWebServiceCompleted)
        }
    }

    function CallCityWebServiceCompleted(WebServiceResult) {
        if (null != WebServiceResult) {
            var zipCodeControl = $find('actZipCode');
            zipCodeControl.raiseItemSelected(new Sys.Extended.UI.AutoCompleteItemEventArgs(null, WebServiceResult.Name, WebServiceResult.Id));
        }
    }
</script>
