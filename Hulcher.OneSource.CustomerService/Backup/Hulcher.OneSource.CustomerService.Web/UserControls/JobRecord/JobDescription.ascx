<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JobDescription.ascx.cs"
    Inherits="Hulcher.OneSource.CustomerService.Web.UserControls.JobRecord.JobDescription" %>
<div>
    <asp:ValidationSummary ID="vsmPermitInfo" CssClass="errorbox" runat="server" ValidationGroup="ScopeOfWork"
        HeaderText="Please correct the following information" />
    <div class="JobDescriptionDerailment">
        <fieldset class="groupBox">
            <legend>
                <asp:Label ID="lblRerailment" runat="server" Text="Derailment"></asp:Label>
            </legend>
            <div class="title" style="margin-top: 20px">
                <asp:Label ID="lblNumberEngines" runat="server" Text="# of Engines:"></asp:Label>
            </div>
            <div class="control" style="margin-top: 20px">
                <asp:TextBox ID="txtNumberEngines" runat="server" CssClass="input" Width="100px"
                    MaxLength="6"></asp:TextBox>
                <asp:FilteredTextBoxExtender ID="fteNumberEngines" TargetControlID="txtNumberEngines"
                    runat="server" FilterType="Numbers">
                </asp:FilteredTextBoxExtender>
            </div>
            <div class="title">
                <asp:Label ID="lblNumberLoads" runat="server" Text="# of Loads:"></asp:Label>
            </div>
            <div class="control">
                <asp:TextBox ID="txtNumberLoads" runat="server" CssClass="input" Width="100px" MaxLength="6"></asp:TextBox>
                <asp:FilteredTextBoxExtender ID="fteNumberLoads" TargetControlID="txtNumberLoads"
                    FilterType="Numbers" runat="server">
                </asp:FilteredTextBoxExtender>
            </div>
            <div class="title">
                <asp:Label ID="lblNumberEmpties" runat="server" Text="# of Empties:"></asp:Label>
            </div>
            <div class="control">
                <asp:TextBox ID="txtNumberEmpties" runat="server" CssClass="input" Width="100px"
                    MaxLength="6"></asp:TextBox>
                <asp:FilteredTextBoxExtender ID="fteNumberEmpties" TargetControlID="txtNumberEmpties"
                    FilterType="Numbers" runat="server">
                </asp:FilteredTextBoxExtender>
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
                <asp:TextBox ID="txtLading" runat="server" CssClass="input" Width="200px" TextMode="MultiLine"
                    Height="100px"></asp:TextBox>
            </div>
        </fieldset>
    </div>
    <div class="JobDescriptionChemical">
        <fieldset class="groupBox">
            <legend>
                <asp:Label ID="lblChemical" runat="server" Text="Chemical"></asp:Label>
            </legend>
            <div class="title" style="margin-top: 20px">
                <asp:Label ID="lblUnNumber" runat="server" Text="UN#:"></asp:Label>
            </div>
            <div class="control" style="margin-top: 20px">
                <asp:TextBox ID="txtUnNumber" runat="server" CssClass="input" Width="100px" MaxLength="20"></asp:TextBox>
                <asp:FilteredTextBoxExtender ID="fteUnNumber" TargetControlID="txtUnNumber" FilterType="Custom"
                    runat="server" FilterMode="InvalidChars" InvalidChars="a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,x,y,z,w,ç,Ç,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,X,Y,Z,W">
                </asp:FilteredTextBoxExtender>
            </div>
            <div class="title">
                <asp:Label ID="lblStccInfo" runat="server" Text="STCC Info:"></asp:Label>
            </div>
            <div class="control">
                <asp:TextBox ID="txtStccInfo" runat="server" CssClass="input" Width="100px" MaxLength="20"></asp:TextBox>
                <asp:FilteredTextBoxExtender ID="fteStccInfo" TargetControlID="txtStccInfo" FilterType="Custom"
                    runat="server" FilterMode="InvalidChars" InvalidChars="a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,x,y,z,w,ç,Ç,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,X,Y,Z,W">
                </asp:FilteredTextBoxExtender>
            </div>
            <div class="title">
                <asp:Label ID="lblHazmat" runat="server" Text="Hazmat:"></asp:Label>
            </div>
            <div class="control">
                <asp:TextBox ID="txtHazmat" runat="server" CssClass="input" Width="100px" MaxLength="100"></asp:TextBox>
            </div>
        </fieldset>
    </div>
</div>
<br />
<asp:UpdatePanel ID="upScopeOfWork" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnAdd" />
    </Triggers>
    <ContentTemplate>
        <div>
            <asp:Label ID="lblScopeOfWork" runat="server" Text="*Scope Of Work:"></asp:Label>&nbsp&nbsp
            <asp:RequiredFieldValidator ID="rfvScope" runat="server" ErrorMessage="The Scope of Work field is required"
                EnableClientScript="true" ValidationGroup="ScopeOfWork" ControlToValidate="txtScope"
                Display="Dynamic" Text="*"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="rfvScopeValue" runat="server" ErrorMessage="Job Description - The Scope of Work field needs to be added on the grid."
                EnableClientScript="true" ValidationGroup="JobRecord" ControlToValidate="txtScopeOfWork" Enabled="false"
                Display="Dynamic" Text="*"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="rfvGridValidation" runat="server" ErrorMessage="Job Description - The Scope of Work Field is required"
                EnableClientScript="true" ControlToValidate="txtGridValidation" Display="Dynamic"
                Text="*"></asp:RequiredFieldValidator><br />
            <div style="display: inline-block; float: left">
                <asp:CountableTextBox ID="txtScope" runat="server" TextMode="MultiLine" Width="650px"
                    Height="100px" CssClass="input"
                    MaxChars="255" MaxCharsWarning="200"></asp:CountableTextBox>
            </div>
            <div style="display: inline-block; margin: 0px 12px;">
                <br />
                <br />
                &nbsp&nbsp&nbsp<asp:CheckBox ID="chkScopeChange" runat="server" Text="Scope Change" />
                <br />
                <br />
                <br />
                <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn" OnClick="btnAdd_Click"
                    ValidationGroup="ScopeOfWork" OnClientClick="UpdateScrollableGridViewHeader('ScrollableGridView');" />
                <asp:TextBox ID="txtScopeOfWork" runat="server" Style="display: none"></asp:TextBox>
            </div>
        </div>
        <br />
        <div style="width: 100%">
            <asp:Label ID="lblScopeOfWorkChronology" runat="server" Text="Scope Of Work Chronology:"></asp:Label><br />
            <asp:ScrollableGridView ID="sgvScopeOfWork" runat="server" OnRowDataBound="sgvScopeOfWork_RowDataBound"
                SkinID="NonSortable">
                <Columns>
                    <asp:BoundField HeaderText="Scope of Work" DataField="ScopeOfWork"
                        ItemStyle-Width="250px" />
                    <asp:BoundField HeaderText="Created By" DataField="CreatedBy" />
                    <asp:BoundField HeaderText="Create Date" />
                    <asp:BoundField HeaderText="Create Time" />
                </Columns>
            </asp:ScrollableGridView>
            <asp:TextBox ID="txtGridValidation" runat="server" Text="" Style="display: none" />
            <br />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
