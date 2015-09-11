using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core;
using System.Text;

namespace Hulcher.OneSource.CustomerService.Web
{
    public partial class ProcessDPI : System.Web.UI.Page, IProcessDPIView
    {
        #region [ Attributes ]
        private ProcessDPIPresenter _presenter;
        #endregion

        #region [ Overrides ]
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            _presenter = new ProcessDPIPresenter(this);
        }
        #endregion

        #region [ Events ]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _presenter.LoadPage();
                SetSpecialPricingTypeScript();
            }
            else if (ViewSpecialPricing)
                SetSpecialPricingValidations();
        }

        protected void rptPublishedRate_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DivisionRowDataItem = (KeyValuePair<int, string>)e.Item.DataItem;
                DivisionRowItem = e.Item;
                _presenter.SetDivisionRowInfo();
            }
            //else if (e.Item.ItemType == ListItemType.Footer)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "CalculationsFooter", GenerateCalculateResourceRevenueTotalScript(), true);
            //}

        }

        protected void rptPublishedRateResources_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ResourceRowDataItem = (CS_DPIResource)e.Item.DataItem;
                ResourceRowItem = e.Item;
                _presenter.SetResourceRowInfo();
            }
        }

        protected void btnSaveDraft_Click(object sender, EventArgs e)
        {
            if (IsValid)
                _presenter.SaveDraft();
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            if (IsValid)
                _presenter.Approve();
        }

        #endregion

        #region [ Methods ]

        public void DisplayMessage(string message, bool closeWindow)
        {
            Master.DisplayMessage(message, closeWindow);
        }

        public void UpdateDashboard()
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "UpdateDashboard", string.Format("UpdateDashboard('{0}');", ParentFieldId), true);
        }

        public void SetResourceRevenueCalculation()
        {
            TextBox txtNumberHours = ResourceRowItem.FindControl("txtNumberHours") as TextBox;
            TextBox txtRate = ResourceRowItem.FindControl("txtRate") as TextBox;
            TextBox txtPermitNumber = ResourceRowItem.FindControl("txtPermitNumber") as TextBox;
            DropDownList drpMeals = ResourceRowItem.FindControl("drpMeals") as DropDownList;
            TextBox txtHotelRate = ResourceRowItem.FindControl("txtHotelRate") as TextBox;
            CheckBox chkHotel = ResourceRowItem.FindControl("chkHotel") as CheckBox;
            CheckBox chkContinuing = ResourceRowItem.FindControl("chkContinuing") as CheckBox;

            txtNumberHours.Attributes["onblur"] = GenerateHoursScript() + GenerateHoursTotalScript() + GenerateFormatDecimalStringScript() + GenerateResourceRevenueScript() + GenerateHoursModifiedScript() + GenerateDiscountScript();
            txtRate.Attributes["onblur"] = GenerateHoursScript() + GenerateHoursTotalScript() + GenerateFormatCurrencyStringScript() + GenerateResourceRevenueScript() + GenerateHoursRateModifiedScript() + GenerateDiscountScript();
            txtPermitNumber.Attributes["onblur"] = GeneratePermitScript() + GenerateResourceRevenueScript();
            drpMeals.Attributes["onchange"] = GenerateMealsScript() + GenerateResourceRevenueScript();
            txtHotelRate.Attributes["onblur"] = GenerateFormatCurrencyStringScript() + GenerateResourceRevenueScript();
            chkHotel.Attributes["onclick"] = GenerateHotelRateScript() + GenerateResourceRevenueScript();
            chkContinuing.Attributes["onclick"] = GenerateContinuingHoursScript() + GenerateHoursScript() + GenerateHoursTotalScript() + GenerateResourceRevenueScript() + GenerateHoursModifiedScript() + GenerateDiscountScript();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Calculations" + ResourceRowDataItem.ID.ToString(), GenerateHoursScript() + GenerateHoursTotalScript() + GeneratePermitScript() + GenerateMealsScript() + GenerateResourceRevenueScript() + GenerateDiscountScript(), true);
        }

        private string GenerateResourceRevenueScript()
        {
            Label lblEstimatedRate = ResourceRowItem.FindControl("lblEstimatedRate") as Label;
            Label lblDiscount = ResourceRowItem.FindControl("lblDiscount") as Label;
            Label lblPermitRate = ResourceRowItem.FindControl("lblPermitRate") as Label;
            Label lblMealsRate = ResourceRowItem.FindControl("lblMealsRate") as Label;
            CheckBox chkHotel = ResourceRowItem.FindControl("chkHotel") as CheckBox;
            TextBox txtHotelRate = ResourceRowItem.FindControl("txtHotelRate") as TextBox;
            Label lblRevenue = ResourceRowItem.FindControl("lblRevenue") as Label;
            HiddenField hfDivisionID = DivisionRowItem.FindControl("hfDivisionID") as HiddenField;

            return "CalculateResourceRevenue('" + lblEstimatedRate.ClientID + "','" + lblDiscount.ClientID + "','" + lblPermitRate.ClientID + "','" + lblMealsRate.ClientID + "','" + chkHotel.ClientID + "','" + txtHotelRate.ClientID + "','" + lblRevenue.ClientID + "','" + hfDivisionID.Value + "','" + hfDiscount.ClientID + "');";
        }

        private string GenerateHoursScript()
        {
            TextBox txtNumberHours = ResourceRowItem.FindControl("txtNumberHours") as TextBox;
            TextBox txtRate = ResourceRowItem.FindControl("txtRate") as TextBox;
            Label lblEstimatedRate = ResourceRowItem.FindControl("lblEstimatedRate") as Label;

            return "CalculateHoursEstimatedRate('" + txtNumberHours.ClientID + "','" + txtRate.ClientID + "','" + lblEstimatedRate.ClientID + "');";
        }

        private string GenerateHoursTotalScript()
        {
            return "CalculateHoursEstimatedRateTotal('" + txtRateGrandTotalBlendedRates.ClientID + "');";
        }

        private string GenerateCalculateResourceRevenueTotalScript()
        {
            return "CalculateResourceRevenueTotal('" + hfDiscount.ClientID + "');";
        }

        private string GenerateEnableSpecialPricingTypeStringScript()
        {
            return string.Format("EnableSpecialPricingType('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}');",
                rbNoSpecialPricing.ClientID, rbOverallSpecialPricing.ClientID, rbLumpSum.ClientID, rbManualCalculation.ClientID,
                txtOverallDiscount.ClientID, txtLumpSumValue.ClientID, txtLumpSumDuration.ClientID, txtLumpSumValuePerDay.ClientID,
                rfvOverallDiscount.ClientID, rfvLumpSumValue.ClientID, rfvLumpSumDuration.ClientID, rfvLumpSumValuePerDay.ClientID,
                pnlManualSpecialPricing.ClientID);
        }

        private string GenerateNoDiscountScript()
        {
            return string.Format("NoDiscount(this, '{0}', '{1}')", hfLastChecked.ClientID, hfDiscount.ClientID);
        }

        private string GenerateOverallDiscountScript()
        {
            Label lblEstimatedRate = ResourceRowItem.FindControl("lblEstimatedRate") as Label;
            Label lblDiscount = ResourceRowItem.FindControl("lblDiscount") as Label;
            Label lblRevisedEstRate = ResourceRowItem.FindControl("lblRevisedEstRate") as Label;

            return " if ($('#" + rbOverallSpecialPricing.ClientID + "')[0].checked) { CalculateResourceOverallDiscount('" + txtOverallDiscount.ClientID + "','" + lblEstimatedRate.ClientID + "','" + lblDiscount.ClientID + "','" + lblRevisedEstRate.ClientID + "','" + hfDiscount.ClientID + "'); } ";
        }

        private string GenerateManualSpecialPricingScript()
        {

            return " if ($('#" + rbManualCalculation.ClientID + "')[0].checked) { CalculateManualSpecialPricingResources(); } ";
        }


        private string GenerateAllOverallDiscountScript()
        {
            return "CalculateEstimatedRateOverallDiscount('" + txtOverallDiscount.ClientID + "','" + hfDiscount.ClientID + "');";
        }

        private string GenerateLumpSumDiscountScript()
        {
            Label lblEstimatedRate = ResourceRowItem.FindControl("lblEstimatedRate") as Label;
            Label lblDiscount = ResourceRowItem.FindControl("lblDiscount") as Label;
            Label lblRevisedEstRate = ResourceRowItem.FindControl("lblRevisedEstRate") as Label;

            return " if ($('#" + rbLumpSum.ClientID + "')[0].checked) { CalculateResourceLumpSumDiscount('" + txtLumpSumValue.ClientID + "', '" + txtLumpSumDuration.ClientID + "', '" + txtLumpSumValuePerDay.ClientID + "', '" + hfDiscount.ClientID + "', '" + txtRateGrandTotalBlendedRates.ClientID + "'); } ";
        }

        private string GenerateAllLumpSumDiscountScript()
        {
            return "CalculateResourceLumpSumTotal('" + txtLumpSumValuePerDay.ClientID + "','" + hfDiscount.ClientID + "','" + txtRateGrandTotalBlendedRates.ClientID + "');";
        }

        private string GenerateLumpSumPerDurationScript()
        {
            return string.Format("CalculateLumpSumPerDuration('{0}', '{1}', '{2}');",
                txtLumpSumValue.ClientID, txtLumpSumDuration.ClientID, txtLumpSumValuePerDay.ClientID);
        }

        private string GenerateContinuingHoursScript()
        {
            CheckBox chkContinuing = ResourceRowItem.FindControl("chkContinuing") as CheckBox;
            HiddenField hfContinuingHours = ResourceRowItem.FindControl("hfContinuingHours") as HiddenField;
            TextBox txtNumberHours = ResourceRowItem.FindControl("txtNumberHours") as TextBox;
            HiddenField hfCalculatedNumberHours = ResourceRowItem.FindControl("hfCalculatedNumberHours") as HiddenField;
            Label lblStatusHours = ResourceRowItem.FindControl("lblStatusHours") as Label;

            return "SetContinuingHours('" + chkContinuing.ClientID + "','" + hfContinuingHours.ClientID + "','" + txtNumberHours.ClientID + "','" + hfCalculatedNumberHours.ClientID + "','" + lblStatusHours.ClientID + "');";
        }

        private string GenerateHoursModifiedScript()
        {
            TextBox txtNumberHours = ResourceRowItem.FindControl("txtNumberHours") as TextBox;
            HiddenField hfCalculatedNumberHours = ResourceRowItem.FindControl("hfCalculatedNumberHours") as HiddenField;
            Label lblHoursModified = ResourceRowItem.FindControl("lblHoursModified") as Label;

            return "VerifyHourModification('" + txtNumberHours.ClientID + "','" + hfCalculatedNumberHours.ClientID + "','" + lblHoursModified.ClientID + "');";
        }

        private string GenerateHoursRateModifiedScript()
        {
            TextBox txtRate = ResourceRowItem.FindControl("txtRate") as TextBox;
            HiddenField hfCalculatedRate = ResourceRowItem.FindControl("hfCalculatedRate") as HiddenField;
            Label lblRateModified = ResourceRowItem.FindControl("lblRateModified") as Label;

            return "VerifyRateModification('" + txtRate.ClientID + "','" + hfCalculatedRate.ClientID + "','" + lblRateModified.ClientID + "');";
        }

        private string GeneratePermitScript()
        {
            TextBox txtPermitNumber = ResourceRowItem.FindControl("txtPermitNumber") as TextBox;
            HiddenField hfPermitRate = ResourceRowItem.FindControl("hfPermitRate") as HiddenField;
            Label lblPermitRate = ResourceRowItem.FindControl("lblPermitRate") as Label;

            return "CalculatePermitRate('" + txtPermitNumber.ClientID + "','" + hfPermitRate.ClientID + "','" + lblPermitRate.ClientID + "');";
        }

        private string GenerateMealsScript()
        {
            DropDownList drpMeals = ResourceRowItem.FindControl("drpMeals") as DropDownList;
            HiddenField hfMealsRate = ResourceRowItem.FindControl("hfMealsRate") as HiddenField;
            Label lblMealsRate = ResourceRowItem.FindControl("lblMealsRate") as Label;

            return "CalculateMealsRate('" + drpMeals.ClientID + "','" + hfMealsRate.ClientID + "','" + lblMealsRate.ClientID + "');";
        }

        private string GenerateHotelRateScript()
        {
            CheckBox chkHotel = ResourceRowItem.FindControl("chkHotel") as CheckBox;
            HiddenField hfHotelRate = ResourceRowItem.FindControl("hfHotelRate") as HiddenField;
            TextBox txtHotelRate = ResourceRowItem.FindControl("txtHotelRate") as TextBox;

            return "SetHotelRate('" + chkHotel.ClientID + "','" + txtHotelRate.ClientID + "','" + hfHotelRate.ClientID + "');";
        }

        private string GenerateFormatCurrencyStringScript()
        {
            return "this.value = formatCurrency(this.value);";
        }

        private string GenerateFormatDecimalStringScript()
        {
            return "this.value = formatDecimal(this.value);";
        }

        private string GenerateFormatIntegerStringScript()
        {
            return "this.value = formatInteger(this.value);";
        }

        private string GenerateBolinha(string total)
        {
            return "CalculateResourceLumpSumTotal2('" + total + "');";
        }

        public void SetDefaultHoursValues()
        {
            StringBuilder script = new StringBuilder();

            HiddenField hfCalculatedNumberHours = ResourceRowItem.FindControl("hfCalculatedNumberHours") as HiddenField;
            TextBox txtNumberHours = ResourceRowItem.FindControl("txtNumberHours") as TextBox;
            CheckBox chkContinuing = ResourceRowItem.FindControl("chkContinuing") as CheckBox;

            script.Append("   var calculatedNumberHoursValue =  document.getElementById('" + hfCalculatedNumberHours.ClientID + "').value;");
            script.Append("   document.getElementById('" + txtNumberHours.ClientID + "').value = formatDecimal(calculatedNumberHoursValue);");
            script.Append("   document.getElementById('" + chkContinuing.ClientID + "').checked = false;");
            script.Append(GenerateHoursScript());
            script.Append(GenerateHoursTotalScript());
            script.Append(GenerateResourceRevenueScript());
            script.Append(GenerateHoursModifiedScript());
            script.Append(GenerateDiscountScript());
            script.Append("   return false;");

            LinkButton lnkResetHours = ResourceRowItem.FindControl("lnkResetHours") as LinkButton;

            lnkResetHours.Attributes.Add("onclick", script.ToString());
        }

        private string GenerateDiscountScript()
        {
            if (!pnlSpecialPricing.Visible)
                return string.Empty;
            else
                return GenerateOverallDiscountScript() + GenerateLumpSumDiscountScript() + GenerateManualSpecialPricingScript() + GenerateCalculateResourceRevenueTotalScript();
        }

        public void SetDefaultRateValues()
        {
            StringBuilder script = new StringBuilder();

            HiddenField hfCalculatedRate = ResourceRowItem.FindControl("hfCalculatedRate") as HiddenField;
            TextBox txtRate = ResourceRowItem.FindControl("txtRate") as TextBox;

            script.Append("   var calculatedRate =  document.getElementById('" + hfCalculatedRate.ClientID + "').value;");
            script.Append("   document.getElementById('" + txtRate.ClientID + "').value = formatCurrency(calculatedRate);");
            script.Append(GenerateHoursScript());
            script.Append(GenerateHoursTotalScript());
            script.Append(GenerateDiscountScript());
            script.Append(GenerateResourceRevenueScript());
            script.Append(GenerateHoursRateModifiedScript());
            script.Append("   return false;");

            LinkButton lnkRateReset = ResourceRowItem.FindControl("lnkRateReset") as LinkButton;

            lnkRateReset.Attributes.Add("onclick", script.ToString());
        }

        public void SetOverallDiscountOnblur()
        {
            StringBuilder script = new StringBuilder();

            script.Append(GenerateFormatDecimalStringScript());
            script.Append(GenerateAllOverallDiscountScript());
            script.Append(GenerateCalculateResourceRevenueTotalScript());

            txtOverallDiscount.Attributes.Add("onblur", script.ToString());
        }

        public void SetLumpSumDiscountOnblur()
        {
            StringBuilder scriptLumpSumValue = new StringBuilder();
            scriptLumpSumValue.Append(GenerateFormatCurrencyStringScript());
            scriptLumpSumValue.Append(GenerateLumpSumPerDurationScript());
            scriptLumpSumValue.Append(GenerateAllLumpSumDiscountScript());
            scriptLumpSumValue.Append(GenerateCalculateResourceRevenueTotalScript());


            StringBuilder scriptLumpSumDuration = new StringBuilder();
            scriptLumpSumDuration.Append(GenerateFormatIntegerStringScript());
            scriptLumpSumDuration.Append(GenerateLumpSumPerDurationScript());
            scriptLumpSumDuration.Append(GenerateAllLumpSumDiscountScript());
            scriptLumpSumDuration.Append(GenerateCalculateResourceRevenueTotalScript());

            StringBuilder scriptLumpSumPerDay = new StringBuilder();
            scriptLumpSumPerDay.Append(GenerateFormatCurrencyStringScript());
            scriptLumpSumPerDay.Append(GenerateAllLumpSumDiscountScript());
            scriptLumpSumPerDay.Append(GenerateCalculateResourceRevenueTotalScript());

            txtLumpSumValue.Attributes.Add("onblur", scriptLumpSumValue.ToString());
            txtLumpSumDuration.Attributes.Add("onblur", scriptLumpSumDuration.ToString());
            txtLumpSumValuePerDay.Attributes.Add("onblur", scriptLumpSumPerDay.ToString());
        }

        public void SetSpecialPricingTypeScript()
        {
            StringBuilder script = new StringBuilder();
            script.Append(GenerateEnableSpecialPricingTypeStringScript());
            script.Append(GenerateNoDiscountScript());

            rbNoSpecialPricing.Attributes.Add("onclick", script.ToString());
            rbOverallSpecialPricing.Attributes.Add("onclick", script.ToString());
            rbLumpSum.Attributes.Add("onclick", script.ToString());
            rbManualCalculation.Attributes.Add("onclick", script.ToString());
        }

        /// <summary>
        /// Sets Special Pricing Validations Acording to it´s type
        /// </summary>
        private void SetSpecialPricingValidations()
        {
            if (rbNoSpecialPricing.Checked)
            {
                rfvOverallDiscount.Enabled = false;
                rfvLumpSumValue.Enabled = false;
                rfvLumpSumDuration.Enabled = false;
                rfvLumpSumValuePerDay.Enabled = false;
                txtOverallDiscount.Enabled = false;
                txtLumpSumValue.Enabled = false;
                txtLumpSumDuration.Enabled = false;
                txtLumpSumValuePerDay.Enabled = false;
            }
            else if (rbOverallSpecialPricing.Checked)
            {
                rfvOverallDiscount.Enabled = true;
                rfvLumpSumValue.Enabled = false;
                rfvLumpSumDuration.Enabled = false;
                rfvLumpSumValuePerDay.Enabled = false;
                txtOverallDiscount.Enabled = true;
                txtLumpSumValue.Enabled = false;
                txtLumpSumDuration.Enabled = false;
                txtLumpSumValuePerDay.Enabled = false;
            }
            else if (rbLumpSum.Checked)
            {
                rfvOverallDiscount.Enabled = false;
                rfvLumpSumValue.Enabled = true;
                rfvLumpSumDuration.Enabled = true;
                rfvLumpSumValuePerDay.Enabled = true;
                txtOverallDiscount.Enabled = false;
                txtLumpSumValue.Enabled = true;
                txtLumpSumDuration.Enabled = true;
                txtLumpSumValuePerDay.Enabled = true;
            }
            else if (rbManualCalculation.Checked)
            {
                rfvOverallDiscount.Enabled = false;
                rfvLumpSumValue.Enabled = false;
                rfvLumpSumDuration.Enabled = false;
                rfvLumpSumValuePerDay.Enabled = false;
                txtOverallDiscount.Enabled = false;
                txtLumpSumValue.Enabled = false;
                txtLumpSumDuration.Enabled = false;
                txtLumpSumValuePerDay.Enabled = false;
            }
        }

        #endregion

        #region [ Properties ]

        #region [ Job Header Fields ]

        public int JobID
        {
            get;
            set;
        }

        public int DPIId
        {
            get
            {
                if (string.IsNullOrEmpty(Request.QueryString["DpiID"]))
                    return 0;

                return int.Parse(Request.QueryString["DpiID"]);
            }
            set { throw new NotImplementedException(); }
        }

        public string JobNumber
        {
            get { throw new NotImplementedException(); }
            set { lblJobNumber.Text = value; }
        }

        public string PrimaryDivisionNumber
        {
            get { throw new NotImplementedException(); }
            set { lblPrimaryDivision.Text = value; }
        }

        public string CustomerName
        {
            get { throw new NotImplementedException(); }
            set { lblCustomer.Text = value; }
        }

        public string Location
        {
            get { throw new NotImplementedException(); }
            set { lblLocation.Text = value; }
        }

        public string JobAction
        {
            get { throw new NotImplementedException(); }
            set { lblJobAction.Text = value; }
        }

        public string JobCategory
        {
            get { throw new NotImplementedException(); }
            set { lblJobCategory.Text = value; }
        }

        public string JobType
        {
            get { throw new NotImplementedException(); }
            set { lblJobType.Text = value; }
        }

        public int? NumberOfEngines
        {
            get
            { throw new NotImplementedException(); }
            set
            {
                if (value.HasValue)
                    lblNumberEngines.Text = value.Value.ToString();
                else
                    lblNumberEngines.Text = "0";
            }
        }

        public int? NumerOfLoads
        {
            get { throw new NotImplementedException(); }
            set
            {
                if (value.HasValue)
                    lblNumberLoads.Text = value.Value.ToString();
                else
                    lblNumberLoads.Text = "0";
            }
        }

        public int? NumberOfEmpties
        {
            get { throw new NotImplementedException(); }
            set
            {
                if (value.HasValue)
                    lblNumberEmpties.Text = value.Value.ToString();
                else
                    lblNumberEmpties.Text = "0";
            }
        }

        #endregion

        #region [ Resource List ]

        public string Disclaimer
        {
            get { throw new NotImplementedException(); }
            set
            {
                lblDisclaimer.Text = value;
            }
        }

        public IList<CS_DPIResource> ResourceDataSource { get; set; }

        public IList<CS_DPIRate> RateTable { get; set; }

        #region [ Division ]

        public IList<KeyValuePair<int, string>> DivisionRowDataSource
        {
            get { throw new NotImplementedException(); }
            set
            {
                ShowHideControls(value.Count.Equals(0));
                rptPublishedRate.DataSource = value;
                rptPublishedRate.DataBind();
            }
        }

        public void ShowHideControls(bool show)
        {
            if (show)
            {
                divControls.Style.Add("display", "none");
                pnlNoRows.Visible = true;
                rptPublishedRate.Visible = false;
            }
            else
            {
                divControls.Style.Add("display", "block");
                pnlNoRows.Visible = false;
                rptPublishedRate.Visible = true;
            }
        }

        public KeyValuePair<int, string> DivisionRowDataItem { get; set; }

        public RepeaterItem DivisionRowItem { get; set; }

        public string DivisionRowDivisionName
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblDivision = DivisionRowItem.FindControl("lblDivision") as Label;
                if (null != lblDivision) lblDivision.Text = value;
            }
        }

        public int DivisionRowDivisionID
        {
            get { throw new NotImplementedException(); }
            set
            {
                HiddenField hfDivisionID = DivisionRowItem.FindControl("hfDivisionID") as HiddenField;
                hfDivisionID.Value = value.ToString();



                Label lblDivisionRevenue = DivisionRowItem.FindControl("lblDivisionRevenue") as Label;
                if (null != lblDivisionRevenue) lblDivisionRevenue.CssClass = value.ToString() + " DivisionRevenue";
            }
        }

        #endregion

        #region [ Resource ]

        public IList<CS_DPIResource> ResourceRowDataSource
        {
            get { throw new NotImplementedException(); }
            set
            {
                Repeater rptPublishedRateResources = DivisionRowItem.FindControl("rptPublishedRateResources") as Repeater;
                if (null != rptPublishedRateResources)
                {
                    rptPublishedRateResources.DataSource = value;
                    rptPublishedRateResources.DataBind();
                }
            }
        }

        public CS_DPIResource ResourceRowDataItem { get; set; }

        public RepeaterItem ResourceRowItem { get; set; }

        public int ResourceRowID
        {
            get { throw new NotImplementedException(); }
            set
            {
                HiddenField hfResourceID = ResourceRowItem.FindControl("hfResourceID") as HiddenField;
                if (null != hfResourceID) hfResourceID.Value = value.ToString();
            }
        }

        public bool ResourceRowIsEmployee
        {
            get { throw new NotImplementedException(); }
            set
            {
                TextBox txtPermitNumber = ResourceRowItem.FindControl("txtPermitNumber") as TextBox;
                DropDownList drpMeals = ResourceRowItem.FindControl("drpMeals") as DropDownList;
                CheckBox chkHotel = ResourceRowItem.FindControl("chkHotel") as CheckBox;
                TextBox txtHotelRate = ResourceRowItem.FindControl("txtHotelRate") as TextBox;

                if (null != txtPermitNumber && null != drpMeals && null != chkHotel && null != txtHotelRate)
                {
                    txtPermitNumber.Enabled = !value;
                    drpMeals.Enabled = value;
                    chkHotel.Enabled = value;
                    txtHotelRate.Enabled = value;
                }
            }
        }

        public string ResourceRowResourceID
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblResourceID = ResourceRowItem.FindControl("lblResourceID") as Label;
                if (null != lblResourceID) lblResourceID.Text = value;
            }
        }

        public string ResourceRowResourceName
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblResourceName = ResourceRowItem.FindControl("lblResourceName") as Label;
                if (null != lblResourceName) lblResourceName.Text = value;
            }
        }

        public decimal ResourceRowCalculatedHours
        {
            get { throw new NotImplementedException(); }
            set
            {
                HiddenField hfCalculatedNumberHours = ResourceRowItem.FindControl("hfCalculatedNumberHours") as HiddenField;
                if (null != hfCalculatedNumberHours) hfCalculatedNumberHours.Value = value.ToString();
            }
        }

        public decimal? ResourceRowModifiedHours
        {
            get { throw new NotImplementedException(); }
            set
            {
                HiddenField hfCalculatedNumberHours = ResourceRowItem.FindControl("hfCalculatedNumberHours") as HiddenField;
                TextBox txtNumberHours = ResourceRowItem.FindControl("txtNumberHours") as TextBox;
                Label lblHoursModified = ResourceRowItem.FindControl("lblHoursModified") as Label;

                if (null != hfCalculatedNumberHours && null != txtNumberHours && null != lblHoursModified)
                {
                    if (value.HasValue)
                    {
                        txtNumberHours.Text = string.Format("{0:N2}", value);
                        lblHoursModified.Text = "M";
                    }
                    else
                    {
                        txtNumberHours.Text = string.Format("{0:N2}", Convert.ToDecimal(hfCalculatedNumberHours.Value));
                        lblHoursModified.Text = string.Empty;
                    }
                }
            }
        }

        public short ResourceRowCalculationStatus
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblStatusHours = ResourceRowItem.FindControl("lblStatusHours") as Label;
                if (null != lblStatusHours)
                {
                    switch ((Globals.DPI.CalculationStatus)value)
                    {
                        case Globals.DPI.CalculationStatus.INSF:
                            lblStatusHours.Text = "INSF";
                            lblStatusHours.ForeColor = System.Drawing.Color.Red;
                            break;
                        case Globals.DPI.CalculationStatus.Done:
                            lblStatusHours.Text = "Done";
                            lblStatusHours.ForeColor = System.Drawing.Color.Black;
                            break;
                    }
                }
            }
        }

        public bool ResourceRowIsContinuing
        {
            get { throw new NotImplementedException(); }
            set
            {
                CheckBox chkContinuing = ResourceRowItem.FindControl("chkContinuing") as CheckBox;
                if (null != chkContinuing) chkContinuing.Checked = value;
            }
        }

        public decimal? ResourceRowContinuingHours
        {
            get { throw new NotImplementedException(); }
            set
            {
                if (value.HasValue)
                {
                    HiddenField hfContinuingHours = ResourceRowItem.FindControl("hfContinuingHours") as HiddenField;
                    if (null != hfContinuingHours) hfContinuingHours.Value = value.Value.ToString();
                }
            }
        }

        public decimal? ResourceRowRate
        {
            get { throw new NotImplementedException(); }
            set
            {
                if (value.HasValue)
                {
                    HiddenField hfCalculatedRate = ResourceRowItem.FindControl("hfCalculatedRate") as HiddenField;
                    if (null != hfCalculatedRate) hfCalculatedRate.Value = value.Value.ToString();
                }
            }
        }

        public decimal? ResourceRowModifiedRate
        {
            get { throw new NotImplementedException(); }
            set
            {
                HiddenField hfCalculatedRate = ResourceRowItem.FindControl("hfCalculatedRate") as HiddenField;
                TextBox txtRate = ResourceRowItem.FindControl("txtRate") as TextBox;
                Label lblRateModified = ResourceRowItem.FindControl("lblRateModified") as Label;

                if (null != hfCalculatedRate && null != txtRate && null != lblRateModified)
                {
                    if (value.HasValue)
                    {
                        txtRate.Text = string.Format("{0:C}", value.Value);
                        lblRateModified.Text = "M";
                    }
                    else
                    {
                        txtRate.Text = string.Format("{0:C}", Convert.ToDecimal(hfCalculatedRate.Value));
                        lblRateModified.Text = string.Empty;
                    }
                }
            }
        }

        public int? ResourceRowPermitQuantity
        {
            get { throw new NotImplementedException(); }
            set
            {
                if (value.HasValue)
                {
                    TextBox txtPermitNumber = ResourceRowItem.FindControl("txtPermitNumber") as TextBox;
                    if (null != txtPermitNumber) txtPermitNumber.Text = value.Value.ToString();
                }
            }
        }

        public decimal? ResourceRowPermitRate
        {
            get { throw new NotImplementedException(); }
            set
            {
                HiddenField hfPermitRate = ResourceRowItem.FindControl("hfPermitRate") as HiddenField;
                if (null != hfPermitRate)
                {
                    if (value.HasValue)
                    {
                        hfPermitRate.Value = value.Value.ToString();
                    }
                    else
                    {
                        hfPermitRate.Value = RateTable.FirstOrDefault(e => e.ID == (int)Globals.DPI.RateTable.PermitRate && e.Active).Value.ToString();
                    }
                }
            }
        }

        public int? ResourceRowMealQuantity
        {
            get { throw new NotImplementedException(); }
            set
            {
                if (value.HasValue)
                {
                    DropDownList drpMeals = ResourceRowItem.FindControl("drpMeals") as DropDownList;
                    if (null != drpMeals) drpMeals.SelectedValue = value.Value.ToString();
                }
            }
        }

        public decimal? ResourceRowMealRate
        {
            get { throw new NotImplementedException(); }
            set
            {
                HiddenField hfMealsRate = ResourceRowItem.FindControl("hfMealsRate") as HiddenField;
                if (null != hfMealsRate)
                {
                    if (value.HasValue)
                    {
                        hfMealsRate.Value = value.Value.ToString();
                    }
                    else
                    {
                        hfMealsRate.Value = RateTable.FirstOrDefault(e => e.ID == (int)Globals.DPI.RateTable.MealRate && e.Active).Value.ToString();
                    }
                }
            }
        }

        public bool ResourceRowHasHotel
        {
            get { throw new NotImplementedException(); }
            set
            {
                CheckBox chkHotel = ResourceRowItem.FindControl("chkHotel") as CheckBox;
                if (null != chkHotel) chkHotel.Checked = value;
            }
        }

        public decimal? ResourceRowHotelRate
        {
            get { throw new NotImplementedException(); }
            set
            {
                HiddenField hfHotelRate = ResourceRowItem.FindControl("hfHotelRate") as HiddenField;
                if (null != hfHotelRate)
                {
                    if (value.HasValue)
                    {
                        hfHotelRate.Value = value.Value.ToString();
                    }
                    else
                    {
                        hfHotelRate.Value = RateTable.FirstOrDefault(e => e.ID == (int)Globals.DPI.RateTable.HotelRate && e.Active).Value.ToString();
                    }
                }
            }
        }

        public decimal? ResourceRowModifiedHotelRate
        {
            get { throw new NotImplementedException(); }
            set
            {
                HiddenField hfHotelRate = ResourceRowItem.FindControl("hfHotelRate") as HiddenField;
                TextBox txtHotelRate = ResourceRowItem.FindControl("txtHotelRate") as TextBox;

                if (null != hfHotelRate && null != txtHotelRate)
                {
                    if (value.HasValue)
                    {
                        txtHotelRate.Text = string.Format("{0:C}", value.Value);
                    }
                    else if (ResourceRowDataItem.HasHotel)
                    {
                        txtHotelRate.Text = string.Format("{0:C}", Convert.ToDecimal(hfHotelRate.Value));
                    }
                }
            }
        }

        public int ResourceRowDivisionID
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblRevenue = ResourceRowItem.FindControl("lblRevenue") as Label;
                if (null != lblRevenue) lblRevenue.CssClass = value.ToString() + " Revenue";
            }
        }

        #endregion

        #endregion

        #region [ Total ]

        public decimal PreviousTotal
        {
            get
            {
                return Convert.ToDecimal(lblPreviousTotal.Text.Replace("$", "").Replace(",", ""));
            }
            set
            {
                lblPreviousTotal.Text = string.Format("{0:C2}", value);
            }
        }

        public decimal NewRevenue
        {
            get
            {
                if (!string.IsNullOrEmpty(lblNewRevenue.Text))
                    return Convert.ToDecimal(lblNewRevenue.Text.Replace("$", "").Replace(",", ""));

                return 0;
            }
            set
            {
                lblNewRevenue.Text = string.Format("{0:C2}", value);
            }
        }

        public decimal CurrentTotal
        {
            get
            {
                return Convert.ToDecimal(lblCurrentTotal.Text.Replace("$", "").Replace(",", ""));
            }
            set
            {
                lblCurrentTotal.Text = string.Format("{0:C2}", value);
            }
        }

        public DateTime DPIDate { get; set; }

        #endregion

        #region [ Special Pricing ]

        public bool ViewSpecialPricing
        {
            get { return pnlSpecialPricing.Visible; }
            set { pnlSpecialPricing.Visible = value; }
        }

        public Globals.DPI.SpecialPriceType SpecialPriceType
        {
            get
            {
                if (rbOverallSpecialPricing.Checked)
                    return Globals.DPI.SpecialPriceType.OverallJobDiscount;
                else if (rbLumpSum.Checked)
                    return Globals.DPI.SpecialPriceType.TotalProjectLumpSum;
                else if (rbManualCalculation.Checked)
                    return Globals.DPI.SpecialPriceType.ManualSpecialPricingCalculation;
                else
                    return Globals.DPI.SpecialPriceType.NoSpecialPricing;
            }
            set
            {
                switch (value)
                {
                    case Globals.DPI.SpecialPriceType.OverallJobDiscount:
                        rbOverallSpecialPricing.Checked = true;
                        txtOverallDiscount.Enabled = true;
                        rfvOverallDiscount.Enabled = true;
                        hfLastChecked.Value = rbOverallSpecialPricing.ClientID;
                        break;
                    case Globals.DPI.SpecialPriceType.TotalProjectLumpSum:
                        rbLumpSum.Checked = true;
                        txtLumpSumValue.Enabled = true;
                        txtLumpSumDuration.Enabled = true;
                        txtLumpSumValuePerDay.Enabled = true;
                        rfvLumpSumValue.Enabled = true;
                        rfvLumpSumDuration.Enabled = true;
                        rfvLumpSumValuePerDay.Enabled = true;
                        hfLastChecked.Value = rbLumpSum.ClientID;
                        break;
                    case Globals.DPI.SpecialPriceType.ManualSpecialPricingCalculation:
                        rbManualCalculation.Checked = true;
                        pnlManualSpecialPricing.Style[HtmlTextWriterStyle.Display] = "";
                        hfLastChecked.Value = rbManualCalculation.ClientID;
                        break;
                    default:
                        rbNoSpecialPricing.Checked = true;
                        hfLastChecked.Value = rbNoSpecialPricing.ClientID;
                        break;
                }
            }
        }

        public decimal? OverallJobDiscount
        {
            get
            {
                if (!string.IsNullOrEmpty(txtOverallDiscount.Text))
                    return Convert.ToDecimal(txtOverallDiscount.Text.Replace("$", "").Replace(",", ""));
                else
                    return null;
            }
            set
            {
                if (value.HasValue)
                    txtOverallDiscount.Text = string.Format("{0:C2}", value.Value);
                else
                    txtOverallDiscount.Text = string.Empty;
            }
        }

        public decimal? LumpSumValue
        {
            get
            {
                if (!string.IsNullOrEmpty(txtLumpSumValue.Text))
                    return Convert.ToDecimal(txtLumpSumValue.Text.Replace("$", "").Replace(",", ""));
                else
                    return null;
            }
            set
            {
                if (value.HasValue)
                    txtLumpSumValue.Text = string.Format("{0:C2}", value.Value);
                else
                    txtLumpSumValue.Text = string.Empty;
            }
        }

        public int? LumpSumDuration
        {
            get
            {
                if (!string.IsNullOrEmpty(txtLumpSumDuration.Text))
                    return Convert.ToInt32(txtLumpSumDuration.Text);
                else
                    return null;
            }
            set
            {
                if (value.HasValue)
                    txtLumpSumDuration.Text = value.Value.ToString();
                else
                    txtLumpSumDuration.Text = string.Empty;
            }
        }

        public decimal? LumpSumValuePerDay
        {
            get
            {
                if (!string.IsNullOrEmpty(txtLumpSumValuePerDay.Text))
                    return Convert.ToDecimal(txtLumpSumValuePerDay.Text.Replace("$", "").Replace(",", ""));
                else
                    return null;
            }
            set
            {
                if (value.HasValue)
                    txtLumpSumValuePerDay.Text = string.Format("{0:C2}", value.Value);
                else
                    txtLumpSumValuePerDay.Text = string.Empty;
            }
        }

        public IList<CS_DPIManualSpecialPricing> ManualSpecialPricingTable
        {
            get
            {
                if (SpecialPriceType == Globals.DPI.SpecialPriceType.ManualSpecialPricingCalculation &&
                    !string.IsNullOrEmpty(hidManualSpecialPricing.Value))
                {
                    IList<CS_DPIManualSpecialPricing> returnList = new List<CS_DPIManualSpecialPricing>();
                    string[] lines = hidManualSpecialPricing.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < lines.Length; i++)
                    {
                        string[] cells = lines[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                        CS_DPIManualSpecialPricing item = new CS_DPIManualSpecialPricing();

                        item.Description = cells[0];
                        item.QtdHrs = Convert.ToDecimal(cells[1].Replace(",", ""));
                        item.Rate = Convert.ToDecimal(cells[2].Replace("$", "").Replace(",", ""));


                        if (cells.Length > 3 && !string.IsNullOrEmpty(cells[3]))
                            item.ID = Convert.ToInt32(cells[3]);

                        if (cells.Length > 4 && !string.IsNullOrEmpty(cells[4]))
                            item.CreationDate = Convert.ToDateTime(cells[4]);

                        if (cells.Length > 5 && !string.IsNullOrEmpty(cells[5]))
                            item.CreatedBy = cells[5];

                        returnList.Add(item);
                    }
                    return returnList;
                }
                else
                    return null;
            }
            set
            {
                StringBuilder hiddenValue = new StringBuilder();

                if (value.Count > 0)
                {
                    for (int i = 0; i < value.Count; i++)
                    {
                        CS_DPIManualSpecialPricing item = value[i];

                        hiddenValue.AppendLine(string.Format("|{0},{1},{2},{3},{4},{5}", item.Description, item.QtdHrs, item.Rate, item.ID, item.CreationDate.ToString(), item.CreatedBy));
                    }

                    hidManualSpecialPricing.Value = hiddenValue.ToString().Substring(1);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Load", "loadManualSpecialPricingTable();", true);
                }
            }
        }

        public string SpecialPricingNotes
        {
            get { return txtSpecialPricingNotes.Text; }
            set { txtSpecialPricingNotes.Text = value; }
        }

        #endregion

        #region [ Save ]

        public string Username
        {
            get { return Master.Username; }
            set { throw new NotImplementedException(); }
        }

        public int? LoggedEmployee
        {
            get { return Master.LoggedEmployee; }
            set { throw new NotImplementedException(); }
        }

        public Globals.DPI.DpiStatus DPIStatus { get; set; }

        public IList<CS_DPIResource> DPIResources
        {
            get
            {
                List<CS_DPIResource> returnList = new List<CS_DPIResource>();
                for (int i = 0; i < rptPublishedRate.Items.Count; i++)
                {
                    Repeater rptPublishedRateResources = rptPublishedRate.Items[i].FindControl("rptPublishedRateResources") as Repeater;
                    if (null != rptPublishedRateResources)
                    {
                        for (int j = 0; j < rptPublishedRateResources.Items.Count; j++)
                        {
                            decimal total = 0;
                            decimal calculatedHours = 0;
                            decimal modifiedHours = 0;
                            decimal calculatedRate = 0;
                            decimal modifiedRate = 0;
                            int permitQuantity = 0;
                            decimal permitRate = 0;
                            int mealQuantity = 0;
                            decimal mealRate = 0;
                            decimal calculatedHotelRate = 0;
                            decimal modifiedHotelRate = 0;

                            CS_DPIResource resource = new CS_DPIResource();

                            HiddenField hfResourceID = rptPublishedRateResources.Items[j].FindControl("hfResourceID") as HiddenField;
                            if (null != hfResourceID) resource.ID = Convert.ToInt32(hfResourceID.Value);

                            HiddenField hfCalculatedNumberHours = rptPublishedRateResources.Items[j].FindControl("hfCalculatedNumberHours") as HiddenField;
                            TextBox txtNumberHours = rptPublishedRateResources.Items[j].FindControl("txtNumberHours") as TextBox;
                            if (null != hfCalculatedNumberHours && null != txtNumberHours)
                            {
                                calculatedHours = Convert.ToDecimal(hfCalculatedNumberHours.Value);
                                modifiedHours = Convert.ToDecimal(txtNumberHours.Text.Replace("$", ""));
                                if (calculatedHours != modifiedHours)
                                    resource.ModifiedHours = modifiedHours;
                            }

                            CheckBox chkContinuing = rptPublishedRateResources.Items[j].FindControl("chkContinuing") as CheckBox;
                            if (null != chkContinuing) resource.IsContinuing = chkContinuing.Checked;


                            HiddenField hfCalculatedRate = rptPublishedRateResources.Items[j].FindControl("hfCalculatedRate") as HiddenField;
                            TextBox txtRate = rptPublishedRateResources.Items[j].FindControl("txtRate") as TextBox;
                            if (null != hfCalculatedRate && null != txtRate)
                            {
                                decimal discount;

                                if (!string.IsNullOrEmpty(hfDiscount.Value))
                                    discount = Convert.ToDecimal(hfDiscount.Value);
                                else
                                    discount = 0;

                                calculatedRate = Convert.ToDecimal(hfCalculatedRate.Value);
                                modifiedRate = Convert.ToDecimal(txtRate.Text.Replace("$", ""));
                                if (calculatedRate != modifiedRate)
                                    resource.ModifiedRate = modifiedRate;

                                decimal calculatedTotal = (modifiedHours * modifiedRate);

                                total += calculatedTotal - (calculatedTotal * (discount / 100));
                            }

                            TextBox txtPermitQuantity = rptPublishedRateResources.Items[j].FindControl("txtPermitNumber") as TextBox;
                            if (null != txtPermitQuantity && !string.IsNullOrEmpty(txtPermitQuantity.Text))
                            {
                                permitQuantity = Convert.ToInt32(txtPermitQuantity.Text);
                                resource.PermitQuantity = permitQuantity;
                                HiddenField hfPermitRate = rptPublishedRateResources.Items[j].FindControl("hfPermitRate") as HiddenField;
                                if (null != hfPermitRate)
                                {
                                    permitRate = Convert.ToDecimal(hfPermitRate.Value);
                                    resource.PermitRate = permitRate;

                                    total += (permitQuantity * permitRate);
                                }
                            }

                            DropDownList drpMeals = rptPublishedRateResources.Items[j].FindControl("drpMeals") as DropDownList;
                            if (null != drpMeals && !string.IsNullOrEmpty(drpMeals.SelectedValue))
                            {
                                mealQuantity = Convert.ToInt32(drpMeals.SelectedValue);
                                resource.MealQuantity = mealQuantity;
                                HiddenField hfMealsRate = rptPublishedRateResources.Items[j].FindControl("hfMealsRate") as HiddenField;
                                if (null != hfMealsRate)
                                {
                                    mealRate = Convert.ToDecimal(hfMealsRate.Value);
                                    resource.MealRate = mealRate;

                                    total += (mealQuantity * mealRate);
                                }
                            }

                            CheckBox chkHotel = rptPublishedRateResources.Items[j].FindControl("chkHotel") as CheckBox;
                            if (null != chkHotel) resource.HasHotel = chkHotel.Checked;
                            if (resource.HasHotel)
                            {
                                HiddenField hfHotelRate = rptPublishedRateResources.Items[j].FindControl("hfHotelRate") as HiddenField;
                                TextBox txtHotelRate = rptPublishedRateResources.Items[j].FindControl("txtHotelRate") as TextBox;
                                if (null != hfHotelRate && null != txtHotelRate)
                                {
                                    calculatedHotelRate = Convert.ToDecimal(hfHotelRate.Value);
                                    resource.HotelRate = calculatedHotelRate;
                                    modifiedHotelRate = Convert.ToDecimal(txtHotelRate.Text.Replace("$", ""));
                                    if (calculatedHotelRate != modifiedHotelRate)
                                        resource.ModifiedHotelRate = modifiedHotelRate;

                                    total += modifiedHotelRate;
                                }
                            }

                            resource.Total = total;

                            returnList.Add(resource);
                        }
                    }
                }
                return returnList;
            }
            set { throw new NotImplementedException(); }
        }

        public string ParentFieldId
        {
            get { return Request.QueryString.Get("ParentFieldId"); }
        }

        #endregion

        #endregion
    }
}
