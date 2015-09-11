using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Core.Utils;

namespace Hulcher.OneSource.CustomerService.Web
{
    public partial class JobRecordPrint : System.Web.UI.Page, IJobRecordPrintView
    {
        #region [ Attributes ]

        JobRecordPrintPresenter _presenter;
        String _paramName;
        int? _contactId;

        #endregion

        #region [ Overrides ]

        /// <summary>
        /// Initializes the Presenter class
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _presenter = new JobRecordPrintPresenter(this);
        }

        #endregion

        #region [ Contructor ]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString.Get("JobId")))
            {
                JobId = Convert.ToInt32(Request.QueryString.Get("JobId"));
                _presenter.GetJob();
            }

            _presenter.UpdateTitle();
        }

        #endregion

        #region [ Properties ]

        public int? JobId
        {
            get
            {
                if (null != ViewState["JobId"])
                    return Convert.ToInt32(ViewState["JobId"]);
                else
                    return null;
            }
            set
            {
                ViewState["JobId"] = value;
            }
        }

        public CS_Job Job
        {
            get
            {
                if (null != ViewState["Job"])
                    return (CS_Job)ViewState["Job"];
                else
                    return null;
            }
            set
            {
                ViewState["Job"] = value;
                FillCustomerInfo();
                FillJobInfo();
                FillJobLocationInfo();
                _presenter.BindEquipmentRequestedGrid();
                _presenter.BindPermitInfoGrid();
                _presenter.BindPhotoReportGrid();
                _presenter.BindJobCallLog();
                _presenter.BindScopeOfWork();
            }
        }

        public string Username
        {
            get { return Master.Username; }
        }

        public string Domain
        {
            get { return Master.Domain; }
        }

        public string PageTitle
        {
            set
            {
                Title = value;
                lblTitle.Text = value;
            }
        }

        public CS_SpecialPricing SpecialPricingEntity
        {
            set
            {
                CS_SpecialPricing specialPricing = value;
                if ((Job.CS_JobInfo.PriceTypeID.Equals((int)Globals.JobRecord.PriceType.SpecialRate) || Job.CS_JobInfo.PriceTypeID.Equals((int)Globals.JobRecord.PriceType.BidRate))
                    && null != specialPricing)
                {
                    divSpecialPricing.Style["display"] = "inline";

                    if (specialPricing.Type == (int)Globals.JobRecord.SpecialPriceType.OverallJobDiscount)
                    {
                        rbOverallSpecialPricing.Checked = true;
                        lblOverallSpecialPricing.Text = specialPricing.OverallJobDiscount.Value.ToString("f2");
                    }
                    else if (specialPricing.Type == (int)Globals.JobRecord.SpecialPriceType.LumpSum)
                    {
                        rbLumpSum.Checked = true;
                        lblLumpSum.Text = string.Format("{0:C} During {1} Days", specialPricing.LumpsumValue.Value, specialPricing.LumpsumDuration.Value);
                    }
                    else if (specialPricing.Type == (int)Globals.JobRecord.SpecialPriceType.ManualSpecialPricing)
                    {
                        rbManualCalculation.Checked = true;
                    }
                    else
                    {
                        rbNoSpecialPricing.Checked = true;
                    }

                    if (specialPricing.ApprovingRVPEmployeeID.HasValue)
                        lblApprovingRVPDescription.Text = specialPricing.CS_Employee_ApprovingRVP.FullName;

                    if (!string.IsNullOrEmpty(specialPricing.Notes))
                        txtSpecialPricingNotes.Text = specialPricing.Notes;
                }
            }
        }

        public CS_PresetInfo PresetInfoEntity
        {
            set
            {
                CS_PresetInfo presetInfo = value;
                if (Job.CS_JobInfo.LastJobStatusID.Equals((int)Globals.JobRecord.JobStatus.Preset) && null != presetInfo)
                {
                    divPresetInfo.Style["display"] = "inline";

                    lblPresetInstructionsDescription.Text = presetInfo.Instructions;
                    if (presetInfo.Date.HasValue)
                        lblPresetDateDescription.Text = presetInfo.Date.Value.ToString("MM/dd/yyyy");
                    if (presetInfo.Time.HasValue)
                        lblPresetTimeDescription.Text = new DateTime(presetInfo.Time.Value.Ticks).ToString("HH:mm");
                }
            }
        }

        public CS_LostJobInfo LostJobEntity
        {
            set
            {
                CS_LostJobInfo lostJobInfo = value;
                if (Job.CS_JobInfo.LastJobStatusID.Equals((int)Globals.JobRecord.JobStatus.Lost) && null != lostJobInfo)
                {
                    divLostJobInfo.Style["display"] = "inline";

                    lblLostJobNotesDescription.Text = lostJobInfo.Instructions;
                    lblLostJobReasonDescription.Text = lostJobInfo.CS_LostJobReason.Description;
                    if (lostJobInfo.CompetitorID.HasValue)
                        lblCompetitorDescription.Text = lostJobInfo.CS_Competitor.Description;
                    if (lostJobInfo.EmployeeID.HasValue)
                    {
                        Identifier = lostJobInfo.EmployeeID;
                        _presenter.GetEmployeeName();
                        lblPocFollowUpDescription.Text = ParamName;
                    }
                    if (lostJobInfo.HSIRepEmployeeID.HasValue)
                    {
                        Identifier = lostJobInfo.HSIRepEmployeeID;
                        _presenter.GetEmployeeName();
                        lblHsirepDescription.Text = ParamName;
                    }
                }
            }
        }

        public IList<CS_JobDivision> JobDivisionEntityList
        {
            set
            {
                gdvDivision.DataSource = value;
                gdvDivision.DataBind();
            }
        }

        public IList<CS_CustomerContract> CustomerContract
        {
            set
            {
                gdvCustomerContract.DataSource = value;
                gdvCustomerContract.DataBind();

                if (gdvCustomerContract.Rows.Count > 0)
                {
                    ContractHeader.Style["display"] = "inline-block";
                    ContractContent.Style["display"] = "inline-block";
                }
                else
                {
                    ContractHeader.Style["display"] = "none";
                    ContractContent.Style["display"] = "none";
                }
            }
        }

        public IList<Core.Utils.CustomerSpecificInfo> CustomerSpecificFields
        {
            set
            {
                pnlCustomerSpecifcInfo.Visible = true;
                plhCustomerSpecificInfo.Controls.Clear();
                plhCustomerSpecificInfo.Controls.Add(WebUtil.ListCustomerSpecificInfoFields(value));
                Session.Add("CustomerSpecificFields", value);
            }
        }

        public string ParamName
        {
            get
            {
                return _paramName;
            }
            set
            {
                _paramName = value;
            }
        }

        public int? Identifier
        {
            get
            {
                return _contactId;
            }
            set
            {
                _contactId = value;
            }
        }

        public IList<CS_Job_LocalEquipmentType> EquipmentRequestDataSource
        {
            set
            {
                grdEquipmentRequest.DataSource = value;
                grdEquipmentRequest.DataBind();
            }
        }

        public IList<CS_JobPermit> PermitInfoGridDataSource
        {
            set
            {
                grdPermitInfo.DataSource = value;
                grdPermitInfo.DataBind();
            }
        }

        public IList<CS_JobPhotoReport> PhotoReporGridDataSource
        {
            set
            {
                grdPhotoReport.DataSource = value;
                grdPhotoReport.DataBind();
            }
        }

        public IList<CS_CallLog> JobCallLogGridDataSource
        {
            set
            {
                sgvCallLog.DataSource = value;
                sgvCallLog.DataBind();
            }
        }

        public IList<CS_ScopeOfWork> ScopeOfWorkGridDataSource
        {
            set
            {
                sgvScopeOfWork.DataSource = value;
                sgvScopeOfWork.DataBind();
            }
        }

        public CS_JobDescription JobDescriptionEntity
        {
            set
            {
                CS_JobDescription jobDescription = value;
                if (null != jobDescription)
                {
                    if (jobDescription.NumberEngines.HasValue)
                        lblNumberEnginesDescription.Text = jobDescription.NumberEngines.Value.ToString();
                    else
                        lblNumberEnginesDescription.Text = "--";

                    if (jobDescription.NumberEmpties.HasValue)
                        lblNumberEmptiesDescription.Text = jobDescription.NumberEmpties.Value.ToString();
                    else
                        lblNumberEmptiesDescription.Text = "--";

                    if (jobDescription.NumberLoads.HasValue)
                        lblNumberLoadsDescription.Text = jobDescription.NumberLoads.Value.ToString();
                    else
                        lblNumberLoadsDescription.Text = "--";

                    if (!string.IsNullOrEmpty(jobDescription.Lading))
                        lblLadingDescription.Text = jobDescription.Lading;
                    else
                        lblLadingDescription.Text = "--";

                    if (!string.IsNullOrEmpty(jobDescription.UnNumber))
                        lblUnNumberDescription.Text = jobDescription.UnNumber;
                    else
                        lblUnNumberDescription.Text = "--";

                    if (!string.IsNullOrEmpty(jobDescription.STCCInfo))
                        lblStccInfoDescription.Text = jobDescription.STCCInfo;
                    else
                        lblStccInfoDescription.Text = "--";

                    if (!string.IsNullOrEmpty(jobDescription.Hazmat))
                        lblHazmatDescription.Text = jobDescription.Hazmat;
                    else
                        lblHazmatDescription.Text = "--";
                }
            }
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Fill CustomerInfo
        /// </summary>
        public void FillCustomerInfo()
        {
            CS_CustomerInfo customerInfo = Job.CS_CustomerInfo;
            if (null != customerInfo)
            {
                lblCustomerDescription.Text = customerInfo.CS_Customer.Name;

                Identifier = customerInfo.EicContactId;
                _presenter.GetContactName();
                lblEICDescription.Text = ParamName;
                Identifier = customerInfo.AdditionalContactId;
                _presenter.GetContactName();
                lblAdditionalContactDescription.Text = ParamName;
                Identifier = customerInfo.BillToContactId;
                _presenter.GetContactName();
                lblBillToContactDescription.Text = ParamName;


                if (customerInfo.InitialCustomerContactId.HasValue)
                {
                    Identifier = customerInfo.InitialCustomerContactId;
                    _presenter.GetContactName();
                    lblCustomerContactDescription.Text = ParamName;
                }
                else
                {
                    lblCustomerContactDescription.Text = "--";
                }

                if (customerInfo.DivisionId.HasValue)
                {
                    Identifier = customerInfo.DivisionId;
                    _presenter.GetDivisionName();
                    lblCIDivisionDescription.Text = ParamName;
                }
                else
                {
                    lblCIDivisionDescription.Text = "--";
                    lblPOCDescription.Text = "--";
                }

                Identifier = customerInfo.PocEmployeeId;
                _presenter.GetEmployeeName();
                lblPOCDescription.Text = ParamName;
            }
        }

        /// <summary>
        /// Fill JobInfo
        /// </summary>
        public void FillJobInfo()
        {
            CS_JobInfo jobInfo = Job.CS_JobInfo;
            if (null != jobInfo)
            {
                lblCallDateDescription.Text = jobInfo.InitialCallDate.ToString("MM/dd/yyyy");
                lblPriceTypeDescription.Text = jobInfo.CS_PriceType.Description;
                lblInitialCallTimeDescription.Text = new DateTime(jobInfo.InitialCallTime.Ticks).ToString("HH:mm");
                lblJobCategoryDescription.Text = jobInfo.CS_JobCategory.Description;
                lblJobStatusDescription.Text = jobInfo.LastJobStatus.Description;
                lblJobTypeDescription.Text = jobInfo.CS_JobType.Description;
                lblJobActionDescription.Text = jobInfo.CS_JobAction.Description;

                CS_Job_JobStatus job_JobStatus = jobInfo.CS_Job_JobStatus.FirstOrDefault(e => e.Active);

                if (null != job_JobStatus && job_JobStatus.JobStartDate.HasValue)
                    lblJobStartDateDescription.Text = job_JobStatus.JobStartDate.Value.ToString("MM/dd/yyyy");
                else
                    lblJobStartDateDescription.Text = "--";

                if (null != job_JobStatus && job_JobStatus.JobCloseDate.HasValue)
                    lblJobCloseDateDescription.Text = job_JobStatus.JobCloseDate.Value.ToString("MM/dd/yyyy");
                else
                    lblJobCloseDateDescription.Text = "--";

                if (!jobInfo.ProjectManager.Equals(0))
                    lblProjectManagerDescription.Text = jobInfo.ProjectManager.ToString();
                else
                    lblProjectManagerDescription.Text = "--";

                ckbInterimBill.Checked = jobInfo.InterimBill;
                if (jobInfo.InterimBill)
                {
                    if (jobInfo.EmployeeID.HasValue)
                        lblRequestedByDescription.Text = jobInfo.CS_Employee.FullName;
                    else
                        lblRequestedByDescription.Text = "--";

                    if (jobInfo.FrequencyID.HasValue)
                        lblFrequencyDescription.Text = jobInfo.CS_Frequency.Description;
                    else
                        lblFrequencyDescription.Text = "--";
                }
                else
                {
                    lblRequestedByDescription.Text = "--";
                    lblFrequencyDescription.Text = "--";
                }
            }
        }

        public void FillJobLocationInfo()
        {
            CS_LocationInfo locationInfo = Job.CS_LocationInfo;
            if (null != locationInfo)
            {
                lblCountryDescription.Text = (string.IsNullOrEmpty(locationInfo.CS_Country.Name)) ? "--" : locationInfo.CS_Country.Name;
                lblStateDescription.Text = (string.IsNullOrEmpty(locationInfo.CS_State.Name)) ? "--" : locationInfo.CS_State.Name;
                lblCityDescription.Text = (string.IsNullOrEmpty(locationInfo.CS_City.Name)) ? "--" : locationInfo.CS_City.Name;
                lblZipCodeDescription.Text = (string.IsNullOrEmpty(locationInfo.CS_ZipCode.ZipCodeNameEdited)) ? "--" : locationInfo.CS_ZipCode.Name;
                lblSiteNameDescription.Text = (string.IsNullOrEmpty(locationInfo.SiteName)) ? "--" : locationInfo.SiteName;
                lblAlternateLocationDescription.Text = (string.IsNullOrEmpty(locationInfo.AlternateName)) ? "--" : locationInfo.AlternateName;
                lblDirectionsDescription.Text = (string.IsNullOrEmpty(locationInfo.Directions)) ? "--" : locationInfo.Directions;
            }
        }

        /// <summary>
        /// Display a message to the user
        /// </summary>
        /// <param name="message"></param>
        public void DisplayMessage(string message, bool closeWindow)
        {
            Master.DisplayMessage(message, closeWindow);
        }

        #endregion

        #region [ Events ]

        protected void sgvScopeOfWork_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CS_ScopeOfWork scopeOfWork = e.Row.DataItem as CS_ScopeOfWork;

                if (null != scopeOfWork)
                {
                    e.Row.Cells[2].Text = scopeOfWork.CreationDate.ToString("MM/dd/yyyy");
                    e.Row.Cells[3].Text = scopeOfWork.CreationDate.ToString("hh:mm:ss");
                }
            }
        }

        protected void grdPermitInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CS_JobPermit permitInfo = e.Row.DataItem as CS_JobPermit;

                if (null != permitInfo)
                {
                    HyperLink hypAttachment = e.Row.Cells[5].FindControl("hypAttachment") as HyperLink;
                    if (null != hypAttachment)
                    {
                        if (!string.IsNullOrEmpty(permitInfo.Path))
                        {
                            hypAttachment.Text = permitInfo.FileName;
                            hypAttachment.NavigateUrl = string.Format("javascript: var downloadWindow = window.open('/download.aspx?fileName={0}&path={1}', 'download');", permitInfo.FileName, permitInfo.Path);
                        }
                    }
                }
            }
        }

        protected void grdPhotoReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CS_JobPhotoReport photoReport = e.Row.DataItem as CS_JobPhotoReport;

                if (null != photoReport)
                {
                    e.Row.Cells[3].Text = photoReport.CreationDate.ToString("MM/dd/yyyy");
                    e.Row.Cells[4].Text = photoReport.CreationDate.ToString("HH:mm");

                    HyperLink hypAttachment = e.Row.Cells[5].FindControl("hypAttachment") as HyperLink;
                    if (null != hypAttachment)
                    {
                        if (!string.IsNullOrEmpty(photoReport.Path))
                        {
                            hypAttachment.NavigateUrl = "#";
                            hypAttachment.Attributes["onclick"] = string.Format("javascript: var downloadWindow = window.open('/download.aspx?fileName={0}&path={1}', 'download'); return false;", photoReport.FileName, photoReport.Path);
                        }
                    }
                }
            }
        }

        #endregion
    }
}