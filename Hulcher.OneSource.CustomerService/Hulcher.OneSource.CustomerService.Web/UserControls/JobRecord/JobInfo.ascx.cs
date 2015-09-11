using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Core.Utils;
using System.Web;
using System.Collections;

namespace Hulcher.OneSource.CustomerService.Web.UserControls.JobRecord
{
    public partial class JobInfo : System.Web.UI.UserControl, IJobInfoView
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of the Presenter Class
        /// </summary>
        private JobInfoPresenter _presenter;

        private CS_JobDivision _jobDivisionItem;

        private CS_Customer _customerFromExternalSource;

        private int _divisionRemoveIndex;

        private int? _jobId;

        private string _jobNumber;

        private string _validationGroup;

        private IList<CS_Reserve> _GetReserveByDivision;

        private IList<CS_Resource> _GetResourcesByDivision;

        private int _divisionId;        

        #endregion

        #region [ Overrides ]

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _presenter = new JobInfoPresenter(this);

            if (!this.CustomerSpecificInfoXML.Equals(string.Empty))
            {
                _presenter.GetFieldsFromXml();
            }
        }

        #endregion

        #region [ View Interface Implementation ]

        public void DisplayMessage(string message, bool closeWindow)
        {
            ((IJobRecordView)Page).DisplayMessage(message, closeWindow);
        }

        #region [ Dropdowns ]

        public IList<CS_JobStatus> JobStatusList
        {
            set
            {
                IList<CS_JobStatus> jobStatusList = value;

                if (!JobId.HasValue)
                {
                    CS_JobStatus jStatus = jobStatusList.Where(w => w.ID == (int)Globals.JobRecord.JobStatus.ClosedHold).FirstOrDefault();
                    CS_JobStatus jStatus2 = jobStatusList.Where(w => w.ID == (int)Globals.JobRecord.JobStatus.Closed).FirstOrDefault();
                    
                    if (jStatus != null)
                    {
                        //Remove the Job Status - H - Closed-Hold from the list of status
                        jobStatusList.Remove(jStatus);
                    }

                    if (jStatus != null)
                    {
                        //Remove the Job Status - H - Closed-Hold from the list of status
                        jobStatusList.Remove(jStatus2);
                    }
                }

                if (JobId.HasValue && JobNumber.ToUpper().Contains("INT"))
                {
                    CS_JobStatus jStatus = jobStatusList.Where(w => w.ID == (int)Globals.JobRecord.JobStatus.Closed).FirstOrDefault();

                    if (jStatus != null)
                    {
                        //Remove the Job Status - H - Closed-Hold from the list of status
                        jobStatusList.Remove(jStatus);
                    }
                }

                ddlJobStatus.Items.Clear();
                ddlJobStatus.DataSource = jobStatusList;
                ddlJobStatus.DataTextField = "Description";
                ddlJobStatus.DataValueField = "ID";
                ddlJobStatus.DataBind();
                ddlJobStatus.Items.Insert(0, new ListItem("- Select One - ", "0"));
            }
        }

        public IList<CS_PriceType> PriceTypeList
        {
            set
            {
                    IList<CS_PriceType> priceTypeList = value;
                ddlPriceType.DataSource = priceTypeList;
                ddlPriceType.DataTextField = "Description";
                ddlPriceType.DataValueField = "ID";
                ddlPriceType.DataBind();
                ddlPriceType.Items.Insert(0, new ListItem("- Select One - ", "0"));
            }
        }

        public IList<CS_JobCategory> JobCategoryList
        {
            set
            {
                ddlJobCategory.DataSource = value;
                ddlJobCategory.DataTextField = "Description";
                ddlJobCategory.DataValueField = "ID";
                ddlJobCategory.DataBind();
                ddlJobCategory.Items.Insert(0, new ListItem("- Select One - ", "0"));
            }
        }

        public IList<CS_JobType> JobTypeList
        {
            set
            {
                ddlJobType.DataSource = value;
                ddlJobType.DataTextField = "Description";
                ddlJobType.DataValueField = "ID";
                ddlJobType.DataBind();
                ddlJobType.Items.Insert(0, new ListItem("- Select One - ", "0"));
            }
        }

        public IList<CS_LostJobReason> LostJobReasonList
        {
            set
            {
                ddlLostJobReason.DataSource = value;
                ddlLostJobReason.DataTextField = "Description";
                ddlLostJobReason.DataValueField = "ID";
                ddlLostJobReason.DataBind();
                ddlLostJobReason.Items.Insert(0, new ListItem("- Select One - ", "0"));
            }
        }

        public IList<CS_Competitor> CompetitorList
        {
            set
            {
                ddlCompetitor.DataSource = value;
                ddlCompetitor.DataTextField = "Description";
                ddlCompetitor.DataValueField = "ID";
                ddlCompetitor.DataBind();
                ddlCompetitor.Items.Insert(0, new ListItem("- Select One - ", "0"));
            }
        }

        //public IList<CS_Employee> EmployeeList
        //{
        //    set
        //    {
        //        IList<CS_Employee> employeeList = value;

        //        ddlPocFollowUp.DataSource = employeeList;
        //        ddlPocFollowUp.DataTextField = "DivisionAndFullName";
        //        ddlPocFollowUp.DataValueField = "ID";
        //        ddlPocFollowUp.DataBind();
        //        ddlPocFollowUp.Items.Insert(0, new ListItem("- Select One - ", "0"));
        //    }
        //}

        public IList<CS_Frequency> FrequencyList
        {
            set
            {
                ddlFrequency.DataSource = value;
                ddlFrequency.DataTextField = "Description";
                ddlFrequency.DataValueField = "ID";
                ddlFrequency.DataBind();
                ddlFrequency.Items.Insert(0, new ListItem("- Select One - ", "0"));
            }
        }

        public IList<CS_Employee> ApprovingRVPList
        {
            set
            {
                IList<CS_Employee> employeeList = value;

                ddlApprovingRVP.DataSource = employeeList;
                ddlApprovingRVP.DataTextField = "FullName";
                ddlApprovingRVP.DataValueField = "ID";
                ddlApprovingRVP.DataBind();
                ddlApprovingRVP.Items.Insert(0, new ListItem("- Select One - ", "0"));
            }
        }

        public IList<CS_Division> DivisionValueList
        {
            set
            {
                ddlDivision.DataSource = value;
                ddlDivision.DataTextField = "ExtendedDivisionName";
                ddlDivision.DataValueField = "ID";
                ddlDivision.DataBind();
                ddlDivision.Items.Insert(0, new ListItem("- Select One - ", "0"));
            }
        }

        #endregion

        #region [ Properties ]

        public CS_View_GetJobData JobInfoLoad 
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                CS_View_GetJobData jobInfo = value;

                if (null != jobInfo)
                {
                    JobNumber = jobInfo.JobPrefixedNumber;

                    dpDatePicker.Value = jobInfo.InitialCallDate;
                    ddlPriceType.SelectedValue = jobInfo.PriceTypeID.ToString();
                    txtInitialCallTime.Text = new DateTime(jobInfo.InitialCallTime.Ticks).ToString("HH:mm");
                    ddlJobCategory.SelectedValue = jobInfo.JobCategoryID.ToString();
                    CustomerId = jobInfo.CustomerID;

                    _presenter.ListAllJobStatus();
                    if (!CloningId.HasValue && (jobInfo.LastJobStatusId.Value == (int)Globals.JobRecord.JobStatus.Active || jobInfo.LastJobStatusId.Value == (int)Globals.JobRecord.JobStatus.Closed))
                    {
                        ddlJobStatus.Items.Remove(ddlJobStatus.Items.FindByValue(((int)Globals.JobRecord.JobStatus.Potential).ToString()));
                        ddlJobStatus.Items.Remove(ddlJobStatus.Items.FindByValue(((int)Globals.JobRecord.JobStatus.Preset).ToString()));
                        ddlJobStatus.Items.Remove(ddlJobStatus.Items.FindByValue(((int)Globals.JobRecord.JobStatus.PresetPurchase).ToString()));
                    }
                    else if (CloningId.HasValue || jobInfo.LastJobStatusId.Value == (int)Globals.JobRecord.JobStatus.Potential || jobInfo.LastJobStatusId.Value == (int)Globals.JobRecord.JobStatus.Preset)
                    {
                        ddlJobStatus.Items.Remove(ddlJobStatus.Items.FindByValue(((int)Globals.JobRecord.JobStatus.Closed).ToString()));
                    }

                    if (jobInfo.LastJobStatusId.Value != (int)Globals.JobRecord.JobStatus.ClosedHold)
                    {
                        ddlJobStatus.Items.Remove(ddlJobStatus.Items.FindByValue(((int)Globals.JobRecord.JobStatus.ClosedHold).ToString()));
                    }
                    else
                    {
                        ListItem selectItem = ddlJobStatus.Items.FindByValue("0");
                        ListItem activeItem = ddlJobStatus.Items.FindByValue(((int)Globals.JobRecord.JobStatus.Active).ToString());
                        ListItem closedHold = ddlJobStatus.Items.FindByValue(((int)Globals.JobRecord.JobStatus.ClosedHold).ToString());
                        ddlJobStatus.Items.Clear();
                        ddlJobStatus.Items.Add(selectItem);
                        ddlJobStatus.Items.Add(activeItem);
                        ddlJobStatus.Items.Add(closedHold);
                    }

                    if (CloningId.HasValue)
                    {
                        ddlJobStatus.Items.Remove(ddlJobStatus.Items.FindByValue(((int)Globals.JobRecord.JobStatus.ClosedHold).ToString()));
                        ddlJobStatus.SelectedValue = ((int)Globals.JobRecord.JobStatus.Preset).ToString();
                        divPresetInfo.Style["display"] = "inline";
                        hidPresetInfoDisplay.Value = "inline";
                    }
                    else
                        ddlJobStatus.SelectedValue = jobInfo.LastJobStatusId.Value.ToString();

                    ddlJobType.SelectedValue = jobInfo.JobTypeID.ToString();

                    actJobAction.SelectedText = jobInfo.JobActionName.ToString();
                    actJobAction.SetValue = jobInfo.JobActionID.ToString();

                    if (jobInfo.ProjectManager != 0)
                    {
                        acProjectManager.SetValue = jobInfo.ProjectManager.ToString();
                        acProjectManager.SelectedText = jobInfo.ProjectManagerFullName;
                    }

                    ckbInterimBill.Checked = jobInfo.InterimBill;
                    actRequestedBy.Enabled = ckbInterimBill.Checked;
                    ddlFrequency.Enabled = ckbInterimBill.Checked;
                    if (jobInfo.InterimBill)
                    {
                        if (jobInfo.RequestedByEmployeeID.HasValue)
                        {
                            actRequestedBy.SelectedValue = jobInfo.RequestedByEmployeeID.Value.ToString();
                        }
                        if (jobInfo.FrequencyID.HasValue)
                            ddlFrequency.SelectedValue = jobInfo.FrequencyID.Value.ToString();
                    }

                    if (jobInfo.PriceTypeID.Equals((int)Globals.JobRecord.PriceType.SpecialRate) || jobInfo.PriceTypeID.Equals((int)Globals.JobRecord.PriceType.BidRate))
                    {
                        divSpecialPricing.Style["display"] = "inline";

                        rfvApprovingRVP.Enabled = false;
                    }

                    if (jobInfo.LastJobStatusId.Value.Equals((int)Globals.JobRecord.JobStatus.Preset) ||
                        jobInfo.LastJobStatusId.Value.Equals((int)Globals.JobRecord.JobStatus.PresetPurchase))
                    {
                        divPresetInfo.Style["display"] = "inline";
                    }
                    else if (jobInfo.LastJobStatusId.Value.Equals((int)Globals.JobRecord.JobStatus.Lost))
                    {
                        divLostJobInfo.Style["display"] = "inline";
                        rfvLostJobReason.Enabled = true;
                    }

                    if (!string.IsNullOrEmpty(jobInfo.CustomerSpecificInfo) && !CloningId.HasValue)
                    {
                        CustomerSpecificInfoXML = jobInfo.CustomerSpecificInfo;
                        _presenter.GetFieldsFromXml();
                    }
                    else
                    {
                        _presenter.GetCustomerSpecificInfoFromCustomer();
                    }

                    updPanel.Update();
                }
            }
        }

        public CS_SpecialPricing SpecialPricingLoad { get; set; }

        public CS_PresetInfo PresetInfoLoad { get; set; }

        public CS_LostJobInfo LostJobInfoLoad { get; set; }

        public IList<CS_JobDivision> JobDivisionListLoad { get; set; }

        public IList<CS_CustomerContract> CustomerContractListLoad { get; set; }

        public IList<CS_Reserve> GetReserveByDivision
        {
            get
            {
                return _GetReserveByDivision;
            }
            set
            {
                _GetReserveByDivision = value;
            }
        }

        public IList<CS_Resource> GetResourcesByDivision
        {
            get
            {
                return _GetResourcesByDivision;
            }
            set
            {
                _GetResourcesByDivision = value;
            }
        }

        public int DivisionId
        {
            get
            {
                return _divisionId;
            }
            set
            {
                _divisionId = value;
            }
        }

        public int? JobId
        {
            get
            {
                return _jobId;
            }
            set
            {
                _jobId = value;
            }
        }

        public string JobNumber
        {
            get
            {
                return _jobNumber;
            }
            set
            {
                _jobNumber = value;
            }
        }

        public int DivisionRemoveIndex
        {
            get
            {
                return _divisionRemoveIndex;
            }
            set
            {
                _divisionRemoveIndex = value;
            }
        }

        public string txtParentControlClientId
        {
            get
            {
                return txtParentUpdate.ClientID;
            }
        }

        public CS_JobInfo JobInfoEntity
        {
            get
            {
                CS_JobInfo jobInfo = new CS_JobInfo();
                jobInfo.InitialCallDate = InitialCallDate;
                jobInfo.InitialCallTime = InitialCallTime;

                DateTime date = DateTime.Parse(string.Format("{0} {1}", InitialCallDate.ToString("MM/dd/yyyy"), InitialCallTime.ToString()));
                DateTimeOffset dateOffSet = new DateTimeOffset(date, DateTimeOffset.Now.Offset);

                jobInfo.InitialCallDatetimeOffset = dateOffSet;
                jobInfo.InterimBill = InterimBill;
                if (RequestedByValue != 0)
                    jobInfo.EmployeeID = RequestedByValue;
                else
                    jobInfo.EmployeeID = null;
                if (FrequencyValue != 0)
                    jobInfo.FrequencyID = FrequencyValue;
                else
                    jobInfo.FrequencyID = null;
                //jobInfo.JobStatusID = JobStatusValue;
                jobInfo.PriceTypeID = PriceTypeValue;
                jobInfo.JobTypeID = JobTypeValue;
                jobInfo.JobCategoryID = JobCategoryValue;
                jobInfo.CreatedBy = ((ContentPage)Page.Master).Username;
                jobInfo.CreationDate = DateTime.Now;
                jobInfo.ModifiedBy = ((ContentPage)Page.Master).Username;
                jobInfo.ModificationDate = DateTime.Now;
                jobInfo.JobActionID = JobActionValue;

                if (!string.IsNullOrEmpty(acProjectManager.SelectedValue))
                {
                    jobInfo.ProjectManager = Convert.ToInt32(acProjectManager.SelectedValue);
                }

                if (plhCustomerSpecificInfo.Controls.Count > 0)
                {
                    jobInfo.CustomerSpecificInfo = _presenter.GetXmlFromDynamicControls();
                }

                jobInfo.Active = true;

                return jobInfo;
            }
            set
            {
                CS_JobInfo jobInfo = value;

                if (null != jobInfo)
                {
                    dpDatePicker.Value = jobInfo.InitialCallDate;
                    ddlPriceType.SelectedValue = jobInfo.PriceTypeID.ToString();
                    txtInitialCallTime.Text = new DateTime(jobInfo.InitialCallTime.Ticks).ToString("HH:mm");
                    ddlJobCategory.SelectedValue = jobInfo.JobCategoryID.ToString();

                    if (jobInfo.LastJobStatusID == (int)Globals.JobRecord.JobStatus.Active)
                    {
                        ddlJobStatus.Items.Remove(ddlJobStatus.Items.FindByValue(((int)Globals.JobRecord.JobStatus.Potential).ToString()));
                        ddlJobStatus.Items.Remove(ddlJobStatus.Items.FindByValue(((int)Globals.JobRecord.JobStatus.Preset).ToString()));
                        ddlJobStatus.Items.Remove(ddlJobStatus.Items.FindByValue(((int)Globals.JobRecord.JobStatus.PresetPurchase).ToString()));
                    }
                    else if (jobInfo.LastJobStatusID == (int)Globals.JobRecord.JobStatus.Potential)
                    {
                        ddlJobStatus.Items.Remove(ddlJobStatus.Items.FindByValue(((int)Globals.JobRecord.JobStatus.Closed).ToString()));
                    }

                    if (jobInfo.LastJobStatusID != (int)Globals.JobRecord.JobStatus.ClosedHold)
                    {
                        ddlJobStatus.Items.Remove(ddlJobStatus.Items.FindByValue(((int)Globals.JobRecord.JobStatus.ClosedHold).ToString()));
                    }
                    else
                    {
                        ddlJobStatus.Items.Remove(ddlJobStatus.Items.FindByValue(((int)Globals.JobRecord.JobStatus.Closed).ToString()));
                    }

                    if (CloningId.HasValue)
                    {
                        ddlJobStatus.Items.Remove(ddlJobStatus.Items.FindByValue(((int)Globals.JobRecord.JobStatus.ClosedHold).ToString()));
                        ddlJobStatus.SelectedValue = ((int)Globals.JobRecord.JobStatus.Preset).ToString();
                        divPresetInfo.Style["display"] = "inline";
                        hidPresetInfoDisplay.Value = "inline";
                    }
                    else
                        ddlJobStatus.SelectedValue = jobInfo.LastJobStatusID.ToString();

                    ddlJobStatus.SelectedValue = jobInfo.LastJobStatusID.ToString();
                    ddlJobType.SelectedValue = jobInfo.JobTypeID.ToString();
                    
                    actJobAction.SelectedText = jobInfo.CS_JobAction.Description;
                    actJobAction.SetValue = jobInfo.JobActionID.ToString();

                    //if (jobInfo.JobStartDate.HasValue)
                    //    txtJobStartDate.Text = jobInfo.JobStartDate.Value.ToString("MM/dd/yyyy");
                    //if (jobInfo.JobCloseDate.HasValue)
                    //    txtJobCloseDate.Text = jobInfo.JobCloseDate.Value.ToString("MM/dd/yyyy");

                    acProjectManager.SelectedValue = jobInfo.ProjectManager.ToString();

                    ckbInterimBill.Checked = jobInfo.InterimBill;
                    actRequestedBy.Enabled = ckbInterimBill.Checked;
                    ddlFrequency.Enabled = ckbInterimBill.Checked;
                    if (jobInfo.InterimBill)
                    {
                        if (jobInfo.EmployeeID.HasValue)
                        {
                            //ddlRequestedBy.SelectedValue = jobInfo.EmployeeID.Value.ToString();
                            actRequestedBy.SelectedValue = jobInfo.EmployeeID.Value.ToString();
                        }
                        if (jobInfo.FrequencyID.HasValue)
                            ddlFrequency.SelectedValue = jobInfo.FrequencyID.Value.ToString();
                    }

                    if (PriceTypeValue.Equals((int)Globals.JobRecord.PriceType.SpecialRate) || PriceTypeValue.Equals((int)Globals.JobRecord.PriceType.BidRate))
                    {
                        divSpecialPricing.Style["display"] = "inline";

                        //rdbLumpSum.Checked = true;
                        //rdbRate.Checked = false;
                        //rfvLumpSumAmount.Enabled = true;
                        //rfvLumpSumPer.Enabled = true;
                        //rfvRateAmount.Enabled = false;
                        //rfvResourceType.Enabled = false;
                        rfvApprovingRVP.Enabled = false;
                    }

                    if (JobStatusValue.Equals((int)Globals.JobRecord.JobStatus.Preset) ||
                        JobStatusValue.Equals((int)Globals.JobRecord.JobStatus.PresetPurchase))
                    {
                        divPresetInfo.Style["display"] = "inline";
                    }
                    else if (JobStatusValue.Equals((int)Globals.JobRecord.JobStatus.Lost))
                    {
                        divLostJobInfo.Style["display"] = "inline";
                        rfvLostJobReason.Enabled = true;
                    }
                }
            }
        }

        public int? CloningId
        {
            get;
            set;
        }

        public CS_SpecialPricing SpecialPricingEntity
        {
            get
            {
                if (!PriceTypeValue.Equals((int)Globals.JobRecord.PriceType.SpecialRate) && !PriceTypeValue.Equals((int)Globals.JobRecord.PriceType.BidRate))
                {
                    return null;
                }
                else
                {
                    CS_SpecialPricing selectedSpecialPricing = new CS_SpecialPricing();

                    if (rbOverallSpecialPricing.Checked)
                    {
                        selectedSpecialPricing.Type = (int)Globals.JobRecord.SpecialPriceType.OverallJobDiscount;
                        selectedSpecialPricing.OverallJobDiscount = decimal.Parse(txtOverallSpecialPricing.Text);
                    }
                    else if (rbLumpSum.Checked)
                    {
                        selectedSpecialPricing.Type = (int)Globals.JobRecord.SpecialPriceType.LumpSum;
                        selectedSpecialPricing.LumpsumValue = decimal.Parse(txtLumpSumValue.Text.Replace("$", "").Replace(",", ""));
                        selectedSpecialPricing.LumpsumDuration = int.Parse(txtLumpSumDuration.Text);
                    }
                    else if (rbManualCalculation.Checked)
                        selectedSpecialPricing.Type = (int)Globals.JobRecord.SpecialPriceType.ManualSpecialPricing;
                    else
                        selectedSpecialPricing.Type = (int)Globals.JobRecord.SpecialPriceType.NoSpecialPrincing;
                    selectedSpecialPricing.Notes = txtSpecialPricingNotes.Text;
                    selectedSpecialPricing.ApprovingRVPEmployeeID = ApprovingRVPValue;
                    selectedSpecialPricing.CreatedBy = ((ContentPage)Page.Master).Username;
                    selectedSpecialPricing.CreationDate = DateTime.Now;
                    selectedSpecialPricing.ModificationDate = DateTime.Now;
                    selectedSpecialPricing.ModifiedBy = ((ContentPage)Page.Master).Username;
                    selectedSpecialPricing.Active = true;
                    selectedSpecialPricing.BidNumber = txtBidNumber.Text;

                    return selectedSpecialPricing;
                }
            }
            set
            {
                CS_SpecialPricing specialPricing = value;
                if (PriceTypeValue.Equals((int)Globals.JobRecord.PriceType.SpecialRate) || PriceTypeValue.Equals((int)Globals.JobRecord.PriceType.BidRate))
                {
                    if (null != specialPricing)
                    {
                        divSpecialPricing.Style["display"] = "inline";
                        ddlApprovingRVP.Enabled = true;

                        if (specialPricing.Type.Equals((int)Globals.JobRecord.SpecialPriceType.OverallJobDiscount))
                        {
                            rbOverallSpecialPricing.Checked = true;
                            txtOverallSpecialPricing.Text = value.OverallJobDiscount.ToString();
                        }
                        else if ((specialPricing.Type.Equals((int)Globals.JobRecord.SpecialPriceType.LumpSum)))
                        {
                            rbLumpSum.Checked = true;
                            txtLumpSumValue.Text = string.Format("{0:C}", value.LumpsumValue);
                            txtLumpSumDuration.Text = value.LumpsumDuration.ToString();
                        }
                        else if (specialPricing.Type.Equals((int)Globals.JobRecord.SpecialPriceType.ManualSpecialPricing))
                        {
                            rbManualCalculation.Checked = true;
                        }
                        else
                        {
                            rbNoSpecialPricing.Checked = true;
                            ddlApprovingRVP.Enabled = false;
                        }

                        if (!string.IsNullOrEmpty(value.Notes))
                            txtSpecialPricingNotes.Text = value.Notes;

                        if (value.ApprovingRVPEmployeeID.HasValue)
                            ddlApprovingRVP.SelectedValue = value.ApprovingRVPEmployeeID.ToString();

                        if (!string.IsNullOrEmpty(value.BidNumber))
                            txtBidNumber.Text = value.BidNumber;
                    }
                }
                else
                    divSpecialPricing.Style["display"] = "none";
            }
        }

        public CS_PresetInfo PresetInfoEntity
        {
            get
            {
                if (!JobStatusValue.Equals((int)Globals.JobRecord.JobStatus.Preset) &&
                    !JobStatusValue.Equals((int)Globals.JobRecord.JobStatus.PresetPurchase))
                {
                    return null;
                }
                else
                {
                    CS_PresetInfo selectedPresetInfo = new CS_PresetInfo();
                    selectedPresetInfo.Instructions = PresetInstructions;
                    if (PresetDate.HasValue)
                        selectedPresetInfo.Date = PresetDate.Value;

                    if (PresetTime.HasValue)
                        selectedPresetInfo.Time = PresetTime.Value;

                    selectedPresetInfo.CreatedBy = ((ContentPage)Page.Master).Username;
                    selectedPresetInfo.CreationDate = DateTime.Now;
                    selectedPresetInfo.ModificationDate = DateTime.Now;
                    selectedPresetInfo.ModifiedBy = ((ContentPage)Page.Master).Username;
                    selectedPresetInfo.Active = true;
                    return selectedPresetInfo;
                }
            }
            set
            {
                CS_PresetInfo presetInfo = value;
                if (JobStatusValue.Equals((int)Globals.JobRecord.JobStatus.Preset) ||
                    JobStatusValue.Equals((int)Globals.JobRecord.JobStatus.PresetPurchase))
                {
                    divPresetInfo.Style["display"] = "inline";
                    if (null != presetInfo)
                    {
                        txtPresetInstructions.Text = presetInfo.Instructions;
                        dpPresetDate.Value = presetInfo.Date;
                        if (presetInfo.Time.HasValue)
                            txtPresetTime.Text = new DateTime(presetInfo.Time.Value.Ticks).ToString("HH:mm");
                    }
                }
                else
                    divPresetInfo.Style["display"] = "none";
            }
        }

        public CS_LostJobInfo LostJobEntity
        {
            get
            {
                if (!JobStatusValue.Equals((int)Globals.JobRecord.JobStatus.Lost))
                {
                    return null;
                }
                else
                {
                    CS_LostJobInfo selectedLostJob = new CS_LostJobInfo();

                    selectedLostJob.ReasonID = LostJobReasonValue;

                    if (CompetitorValue != 0)
                        selectedLostJob.CompetitorID = CompetitorValue;
                    else
                        selectedLostJob.CompetitorID = null;

                    if (PocFollowUp != 0)
                        selectedLostJob.EmployeeID = PocFollowUp;
                    else
                        selectedLostJob.EmployeeID = null;

                    if (HSIRepValue != 0)
                        selectedLostJob.HSIRepEmployeeID = HSIRepValue;
                    else
                        selectedLostJob.HSIRepEmployeeID = null;

                    selectedLostJob.Instructions = txtLostJobNotes.Text;
                    selectedLostJob.CreatedBy = ((ContentPage)Page.Master).Username;
                    selectedLostJob.CreationDate = DateTime.Now;
                    selectedLostJob.ModificationDate = DateTime.Now;
                    selectedLostJob.ModifiedBy = ((ContentPage)Page.Master).Username;
                    selectedLostJob.Active = true;
                    return selectedLostJob;
                }
            }
            set
            {
                CS_LostJobInfo lostJobInfo = value;
                if (JobStatusValue.Equals((int)Globals.JobRecord.JobStatus.Lost))
                {
                    divLostJobInfo.Style["display"] = "inline";

                    if (null != lostJobInfo)
                    {
                        txtLostJobNotes.Text = lostJobInfo.Instructions;
                        ddlLostJobReason.SelectedValue = lostJobInfo.ReasonID.ToString();
                        if (lostJobInfo.CompetitorID.HasValue)
                            ddlCompetitor.SelectedValue = lostJobInfo.CompetitorID.Value.ToString();
                        if (lostJobInfo.EmployeeID.HasValue)
                        {
                            // ddlPocFollowUp.SelectedValue = lostJobInfo.EmployeeID.Value.ToString();
                            actPocFollowUp.SelectedValue = lostJobInfo.EmployeeID.Value.ToString();
                        }
                        if (lostJobInfo.HSIRepEmployeeID.HasValue)
                            actHsiRep.SelectedValue = lostJobInfo.HSIRepEmployeeID.Value.ToString();

                        rfvLostJobReason.Enabled = true;
                    }
                }
                else
                    divLostJobInfo.Style["display"] = "none";
            }
        }

        public IList<CS_JobDivision> JobDivisionEntityList
        {
            get
            {
                if (null == ViewState["JobDivision"])
                    ViewState["JobDivision"] = new List<CS_JobDivision>();

                IList<CS_JobDivision> returnList = (IList<CS_JobDivision>)ViewState["JobDivision"];
                for (int i = 0; i < returnList.Count; i++)
                {
                    if (!string.IsNullOrEmpty(txtGridValidation2.Text))
                    {
                        if (txtGridValidation2.Text.Equals(i.ToString()))
                            returnList[i].PrimaryDivision = true;
                        else
                            returnList[i].PrimaryDivision = false;
                    }
                }
                return returnList;
            }
            set
            {
                ViewState["JobDivision"] = value;
                for (int i = 0; i < value.Count; i++)
                {
                    if (value[i].PrimaryDivision)
                    {
                        txtGridValidation2.Text = i.ToString();
                        break;
                    }
                }
            }
        }

        public IList<CS_CustomerContract> CustomerContract
        {
            set
            {
                gdvCustomerContract.DataSource = value;
                gdvCustomerContract.DataBind();

                if (gdvCustomerContract.Rows.Count > 0)
                    divContract.Style["display"] = "inline";
                else
                    divContract.Style["display"] = "none";
            }
        }

        public DateTime InitialCallDate
        {
            get
            {
                if (dpDatePicker.Value.HasValue)
                {
                    return dpDatePicker.Value.Value;
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        public TimeSpan InitialCallTime
        {
            get
            {
                DateTime date = DateTime.Parse(txtInitialCallTime.Text);
                return date.TimeOfDay;
            }
        }

        public int JobStatusValue
        {
            get { return int.Parse(ddlJobStatus.SelectedValue); }
            set
            {
                ddlJobStatus.SelectedValue = value.ToString();
                if (value.Equals((int)Globals.JobRecord.JobStatus.Preset) ||
                    value.Equals((int)Globals.JobRecord.JobStatus.PresetPurchase))
                    divPresetInfo.Style["display"] = "inline";
                else if (value.Equals((int)Globals.JobRecord.JobStatus.Lost))
                    divLostJobInfo.Style["display"] = "inline";
            }
        }

        public DateTime? JobStartDate
        {
            get
            {
                if (!txtJobStartDate.Text.Equals(string.Empty))
                {
                    return DateTime.Parse(txtJobStartDate.Text);
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (value.HasValue)
                    txtJobStartDate.Text = value.Value.ToString("MM/dd/yyyy");
                else
                    txtJobStartDate.Text = string.Empty;
            }
        }

        public int PriceTypeValue
        {
            get { return int.Parse(ddlPriceType.SelectedValue); }
        }

        public int JobCategoryValue
        {
            get { return int.Parse(ddlJobCategory.SelectedValue); }
            set { ddlJobCategory.SelectedValue = value.ToString(); }
        }

        public int JobTypeValue
        {
            get { return int.Parse(ddlJobType.SelectedValue); }
            set { ddlJobType.SelectedValue = value.ToString(); }
        }

        public int JobActionValue
        {
            get { return int.Parse(actJobAction.SelectedValue); }
        }

        public string JobActionClientID
        {
            get
            {
                return actJobAction.ClientID;
            }
        }

        public DateTime? JobCloseDate
        {
            get
            {
                if (txtJobCloseDate.Text.Equals(string.Empty))
                    return null;
                else
                    return DateTime.Parse(txtJobCloseDate.Text);
            }
            set
            {
                if (value.HasValue)
                    txtJobCloseDate.Text = value.Value.ToString("MM/dd/yyyy");
                else
                    txtJobCloseDate.Text = string.Empty;
            }
        }

        public string LostJobNotes
        {
            get { return txtLostJobNotes.Text; }
            set { txtLostJobNotes.Text = value; }
        }

        public int LostJobReasonValue
        {
            get { return int.Parse(ddlLostJobReason.SelectedValue); }
            set { ddlLostJobReason.SelectedIndex = value; }
        }

        public int CompetitorValue
        {
            get { return int.Parse(ddlCompetitor.SelectedValue); }
            set { ddlCompetitor.SelectedIndex = value; }
        }

        public int PocFollowUp
        {
            get
            {
                if (actPocFollowUp.SelectedValue != "0")
                {
                    string[] splitValues = actPocFollowUp.SelectedValue.Replace(" | ", "|").Split('|');
                    return int.Parse(splitValues[0]);
                }
                else
                    return 0;
            }
            set { actPocFollowUp.SelectedValue = value.ToString(); }
        }

        public string PresetInstructions
        {
            get { return txtPresetInstructions.Text; }
            set { txtPresetInstructions.Text = value; }
        }

        public DateTime? PresetDate
        {
            get
            {
                if (dpPresetDate.Value.HasValue)
                {
                    return dpPresetDate.Value.Value;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                dpPresetDate.Value = value;
            }
        }

        public TimeSpan? PresetTime
        {
            get
            {
                DateTime date;
                if (DateTime.TryParse(txtPresetTime.Text, out date))
                    return date.TimeOfDay;

                return null;
            }
            set
            {
                if (value.HasValue)
                    txtPresetTime.Text = value.Value.ToString();
                else
                    txtPresetTime.Text = string.Empty;
            }
        }

        public bool InterimBill
        {
            get
            {
                return ckbInterimBill.Checked;
            }
        }

        public int RequestedByValue
        {
            get
            {
                if (actRequestedBy.SelectedValue != "0")
                {
                    string[] splitValues = actRequestedBy.SelectedValue.Replace(" | ", "|").Split('|');
                    return int.Parse(splitValues[0]);
                }
                else
                    return 0;
            }
            set { actRequestedBy.SelectedValue = value.ToString(); }
        }

        public int? ApprovingRVPValue
        {
            get
            {
                if (ddlApprovingRVP.SelectedValue == "0")
                    return null;

                return int.Parse(ddlApprovingRVP.SelectedValue);
            }
            set
            {
                if (value.HasValue)
                    ddlApprovingRVP.SelectedIndex = value.Value;
            }
        }

        public int FrequencyValue
        {
            get { return int.Parse(ddlFrequency.SelectedValue); }
            set { ddlFrequency.SelectedIndex = value; }
        }

        public string UserName
        {
            get { return ((ContentPage)Page.Master).Username; }
        }

        public string Domain
        {
            get { return ((ContentPage)Page.Master).Domain; }
        }

        public CS_JobDivision JobDivisionEntity
        {
            get
            {
                return _jobDivisionItem;
            }
            set
            {
                _jobDivisionItem = value;
            }
        }

        public void ListJobDivision()
        {
            gdvDivision.DataSource = JobDivisionEntityList;
            gdvDivision.DataBind();
        }

        public void CheckJobStatus()
        {
            if (Convert.ToInt32(ddlJobStatus.SelectedValue) == (int)Globals.JobRecord.JobStatus.Active)
            {
                ddlJobStatus.Items.Remove(ddlJobStatus.Items.FindByValue(((int)Globals.JobRecord.JobStatus.Potential).ToString()));
                ddlJobStatus.Items.Remove(ddlJobStatus.Items.FindByValue(((int)Globals.JobRecord.JobStatus.Preset).ToString()));
                ddlJobStatus.Items.Remove(ddlJobStatus.Items.FindByValue(((int)Globals.JobRecord.JobStatus.PresetPurchase).ToString()));
            }
            else if (Convert.ToInt32(ddlJobStatus.SelectedValue) == (int)Globals.JobRecord.JobStatus.PresetPurchase)
            {
                ddlJobStatus.Items.Remove(ddlJobStatus.Items.FindByValue(((int)Globals.JobRecord.JobStatus.Potential).ToString()));
            }
            else if (Convert.ToInt32(ddlJobStatus.SelectedValue) == (int)Globals.JobRecord.JobStatus.Potential)
            {
                ddlJobStatus.Items.Remove(ddlJobStatus.Items.FindByValue(((int)Globals.JobRecord.JobStatus.Closed).ToString()));
            }

            if (Convert.ToInt32(ddlJobStatus.SelectedValue) != (int)Globals.JobRecord.JobStatus.ClosedHold)
            {
                ddlJobStatus.Items.Remove(ddlJobStatus.Items.FindByValue(((int)Globals.JobRecord.JobStatus.ClosedHold).ToString()));
            }
        }

        public CS_Division DivisionValue
        {
            get
            {
                CS_Division selectedDivision = new CS_Division();
                selectedDivision.ID = int.Parse(ddlDivision.SelectedItem.Value);
                string[] divNameDesc = ddlDivision.SelectedItem.Text.Split('-');
                selectedDivision.Name = divNameDesc[0].Trim();
                selectedDivision.Description = divNameDesc[1].Trim();
                return selectedDivision;
            }
        }

        public bool LostJobInfoValidationIsEnabled
        {
            get
            {
                return rfvLostJobReason.Enabled;
            }
            set
            {
                rfvLostJobReason.Enabled = value;
            }
        }

        public bool PresetInfoValidationIsEnabled
        {
            get
            {
                return mskPresetTimeValidator.Enabled;
            }
            set
            {
                mskPresetTimeValidator.Enabled = value;
            }
        }

        public bool RequestedByEnabled
        {
            get
            {
                return actRequestedBy.Enabled;
            }
            set
            {
                actRequestedBy.Enabled = value;
            }
        }

        public bool FrequencyEnabled
        {
            get
            {
                return ddlFrequency.Enabled;
            }
            set
            {
                ddlFrequency.Enabled = value;
            }
        }

        public bool CustomerSpecificInfoVisibility
        {
            get
            {
                return chCustomerSpecificInfo.Visible;
            }
            set
            {
                chCustomerSpecificInfo.Visible = value;
            }
        }

        public string CustomerSpecificInfoXML
        {
            get
            {
                if (!hfCustomerSpecificInfo.Value.Equals(string.Empty))
                    return parseHiddenToXml(hfCustomerSpecificInfo.Value);
                if (null != Request.Form[hfCustomerSpecificInfo.UniqueID])
                    return parseHiddenToXml(Request.Form[hfCustomerSpecificInfo.UniqueID].ToString());

                return string.Empty;
            }
            set
            {
                hfCustomerSpecificInfo.Value = parseXmlToHidden(value);
            }
        }

        private string parseXmlToHidden(string xml)
        {
            string returnValue = xml;

            returnValue = returnValue.Replace("<", "xmlOB");
            returnValue = returnValue.Replace(">", "xmlCB");
            returnValue = returnValue.Replace("?", "xmlQM");
            returnValue = returnValue.Replace("\"", "xmlQUOM");
            returnValue = returnValue.Replace(":", "xmlDD");
            returnValue = returnValue.Replace(@"/", "xmlFS");
            returnValue = returnValue.Replace(".", "xmlDOT");

            return returnValue;
        }

        private string parseHiddenToXml(string xml)
        {
            string returnValue = xml;

            returnValue = returnValue.Replace("xmlOB", "<");
            returnValue = returnValue.Replace("xmlCB", ">");
            returnValue = returnValue.Replace("xmlQM", "?");
            returnValue = returnValue.Replace("xmlQUOM", "\"");
            returnValue = returnValue.Replace("xmlDD", ":");
            returnValue = returnValue.Replace("xmlFS", @"/");
            returnValue = returnValue.Replace("xmlDOT", ".");

            return returnValue;
        }

        public List<Control> CustomerSpecificInfoControls
        {
            get
            {
                List<Control> controlList = new List<Control>();
                foreach (Control c in plhCustomerSpecificInfo.Controls)
                {
                    controlList.Add(c);
                }
                return controlList;
            }
            set
            {
                plhCustomerSpecificInfo.Controls.Clear();

                for (int i = 0; i < value.Count; i++)
                {
                    plhCustomerSpecificInfo.Controls.Add(value[i]);
                }

                plhCustomerSpecificInfo.Visible = true;
            }
        }

        public CS_Division DivisionFromExternalSource
        {
            get
            {
                if (ViewState["ExternalJobDivision"] != null)
                {
                    return (CS_Division)ViewState["ExternalJobDivision"];
                }
                else
                    return null;
            }
            set
            {
                ViewState["ExternalJobDivision"] = value;
                _presenter.AddDivisionFromCustomerInfo(value);
                for (int i = 0; i < JobDivisionEntityList.Count; i++)
                {
                    if (JobDivisionEntityList[i].PrimaryDivision)
                    {
                        txtGridValidation2.Text = i.ToString();
                        break;
                    }
                }
            }
        }

        public CS_Customer CustomerFromExternalSource
        {
            get { return _customerFromExternalSource; }
            set
            {
                _customerFromExternalSource = value;
                plhCustomerSpecificInfo.Controls.Clear();

                if (!_customerFromExternalSource.ID.Equals(0))
                {
                    //_presenter.LoadCustomerSpecificInfoFields();
                    //_presenter.ListAllCustomerContracts();
                }
            }
        }

        //public IList<CS_Employee> ProjectManagerList
        //{
        //    set
        //    {
        //        IList<CS_Employee> employeeList = value;

        //        acProjectManager.DataSource = employeeList;
        //        acProjectManager.DataTextField = "DivisionAndFullName";
        //        acProjectManager.DataValueField = "ID";
        //        acProjectManager.DataBind();
        //        acProjectManager.Items.Insert(0, new ListItem("- Select One - ", "0"));
        //    }
        //}

        /// <summary>
        /// Set the Validation Group of the Validators inside the User Control
        /// </summary>f
        public string ValidationGroup
        {
            set
            {
                ddlPriceType.ValidationGroup = value;
                rfvPriceType.ValidationGroup = value;
                txtInitialCallTime.ValidationGroup = value;
                rfvInitialCallTimeValidator.ValidationGroup = value;
                ddlJobCategory.ValidationGroup = value;
                rfvJobCategory.ValidationGroup = value;
                ddlJobStatus.ValidationGroup = value;
                rfvJobStatus.ValidationGroup = value;
                ddlJobType.ValidationGroup = value;
                rfvJobType.ValidationGroup = value;
                
                actJobAction.ValidationGroup = value;
                dpDatePicker.ValidationGroup = value;
                actRequestedBy.ValidationGroup = value;
                actRequestedBy.ValidationGroup = value;
                ddlFrequency.ValidationGroup = value;
                rfvFrequency.ValidationGroup = value;
                rfvGridValidation.ValidationGroup = value;
                rfvGridValidation2.ValidationGroup = value;
                _validationGroup = value;
            }
        }

        public string PresetValidationGroup
        {
            set
            {
                txtPresetTime.ValidationGroup = value;
                mskPresetTimeValidator.ValidationGroup = value;
            }
        }

        public string CreatedBy
        {
            get
            {
                return ((ContentPage)Page.Master).Username;
            }
        }

        public int HSIRepValue
        {
            get
            {
                return int.Parse(actHsiRep.SelectedValue);
            }
            set
            {
                actHsiRep.SelectedValue = value.ToString();
            }
        }

        public int CustomerId
        {
            get;
            set;
        }

     
        //public string CallDateTextBoxClientID
        //{
        //    get
        //    {
        //        return dpDatePicker.TextBoxClientID;
        //    }
        //}

        //public string CallDateEssentialTextBoxClientID
        //{
        //    set
        //    {
        //        hidCallDateEssentialTextBoxClientID.Value = value;
        //    }
        //}

        //public string CallTimeTextBoxClientID
        //{
        //    get
        //    {
        //        return txtInitialCallTime.ClientID;
        //    }
        //}

        //public string CallTimeEssentialTextBoxClientID
        //{
        //    set
        //    {
        //        hidCallTimeEssentialTextBoxClientID.Value = value;
        //    }
        //}

        public string JobActionUniqueID
        {
            get
            {
                return actJobAction.UniqueID;
            }
        }
        
        #endregion

        #region [ Methods ]

        public void RefreshUpdatePanel()
        {
            updPanel.Update();
        }

        public void ClearDivisions()
        {
            gdvDivision.DataSource = null;
            gdvDivision.DataBind();
        }

        public void ReadyOnlyJobInfo()
        {
            ddlPriceType.Enabled = false;
            txtInitialCallTime.Enabled = false;
            ddlJobCategory.Enabled = false;
            ddlJobStatus.Enabled = false;
            ddlJobType.Enabled = false;
            actJobAction.Enabled = false;
            txtJobStartDate.Enabled = false;
            txtJobCloseDate.Enabled = false;
            ddlDivision.Enabled = false;
            acProjectManager.Enabled = false;
            ckbInterimBill.Enabled = false;
            gdvDivision.Enabled = false;
            rbLumpSum.Enabled = false;
            rbNoSpecialPricing.Enabled = false;
            rbManualCalculation.Enabled = false;
            rbOverallSpecialPricing.Enabled = false;
            txtLumpSumDuration.Enabled = false;
            txtLumpSumValue.Enabled = false;
            txtOverallSpecialPricing.Enabled = false;
            txtSpecialPricingNotes.Enabled = false;
            ddlApprovingRVP.Enabled = false;
            txtPresetInstructions.Enabled = false;
            txtPresetTime.Enabled = false;
            txtLostJobNotes.Enabled = false;
            ddlLostJobReason.Enabled = false;
            ddlCompetitor.Enabled = false;
            actPocFollowUp.Enabled = false;
            actHsiRep.Enabled = false;
            ckbInterimBill.Enabled = false;
            actRequestedBy.Enabled = false;
            ddlFrequency.Enabled = false;
            gdvCustomerContract.Enabled = false;
            dpDatePicker.EnableDatePicker = false;
            dpPresetDate.EnableDatePicker = false;
            btnAddDivision.Enabled = false;
        }

        #endregion

        #endregion

        #region [ Events ]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dpPresetDate.ValueToCompare = DateTime.Today.ToShortDateString();

                if (Session["CustomerSpecificFields"] != null)
                    Session.Remove("CustomerSpecificFields");

                if (string.IsNullOrEmpty(txtInitialCallTime.Text))
                {
                    dpDatePicker.Value = DateTime.Now;
                    txtInitialCallTime.Text = DateTime.Now.ToString("HH:mm");
                }

                _presenter.ListAllPriceTypes();

                if (ddlJobStatus.Items.Count == 0)
                    _presenter.ListAllJobStatus();

                _presenter.ListAllJobCategories();
                _presenter.ListAllJobTypes();
                _presenter.ListAllDivisions();

                _presenter.ListAllFrequencies();

                _presenter.ListAllLostJobReasons();
                _presenter.ListAllCompetitors();
                _presenter.ListAllApprovingRVP();

                // This items are here because they won't work inside ValidationGroup property due to Page Lifecycle issues
                dpPresetDate.ValidationGroup = _validationGroup;
                ddlLostJobReason.ValidationGroup = _validationGroup;
                rfvLostJobReason.ValidationGroup = _validationGroup;

                if (_jobId.HasValue)
                    _presenter.LoadJobInfo();
                if (Request.QueryString["CloningId"] != null)
                {
                    CloningId = int.Parse(Request.QueryString["CloningId"]);
                //    _presenter.LoadJobInfoCloningData();
                //    _presenter.LoadSpecialPricingInfoCloning();
                    _presenter.LoadJobDivisionCloning();
                }
            }
            else
            {
                if (int.Parse(ddlJobStatus.SelectedValue) != (int)Globals.JobRecord.JobStatus.Preset && int.Parse(ddlJobStatus.SelectedValue) != (int)Globals.JobRecord.JobStatus.PresetPurchase)
                {
                    dpPresetDate.EnableCompareValidator = false;
                }
                else
                {
                    dpPresetDate.EnableCompareValidator = true;
                }
            }

            // reactivating control statuses
            foreach (ListItem item in ddlPriceType.Items)
                item.Attributes["value"] = item.Value;
            foreach (ListItem item in ddlJobStatus.Items)
                item.Attributes["value"] = item.Value;

            if (!string.IsNullOrEmpty(hidSpecialPricingDisplay.Value))
                divSpecialPricing.Style["display"] = hidSpecialPricingDisplay.Value;
            if (!string.IsNullOrEmpty(hidPresetInfoDisplay.Value))
                divPresetInfo.Style["display"] = hidPresetInfoDisplay.Value;
            if (!string.IsNullOrEmpty(hidLostJobInfoDisplay.Value))
                divLostJobInfo.Style["display"] = hidLostJobInfoDisplay.Value;
            //txtLumpSumAmount.Enabled = rdbLumpSum.Checked;
            //ddlLumpSumPer.Enabled = rdbLumpSum.Checked;
            //txtRateAmount.Enabled = rdbRate.Checked;
            //ddlResourceType.Enabled = rdbRate.Checked;

            //if (Session["CustomerSpecificFields"] != null)
            //    this.CustomerSpecificFields = Session["CustomerSpecificFields"] as IList<CustomerSpecificInfo>;


        }

        protected void LoadJobInfo(object sender, EventArgs e)
        {
            LoadJobInfo();
        }

        public void LoadJobInfo()
        {
            if (_jobId.HasValue)
                _presenter.LoadJobInfo();
        }

        protected void ckbInterimBill_CheckedChanged(object sender, EventArgs e)
        {
            actRequestedBy.Enabled = ckbInterimBill.Checked;
            ddlFrequency.Enabled = ckbInterimBill.Checked;

            if (!ckbInterimBill.Checked)
            {
                actRequestedBy.ClearSelection();
                ddlFrequency.SelectedIndex = 0;

                ScriptManager.GetCurrent(this.Page).SetFocus(ckbInterimBill);
            }
            else
                ScriptManager.GetCurrent(this.Page).SetFocus(actRequestedBy.ClientID + "_TextBox");
        }

        protected void actJobAction_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(actJobAction.SelectedValue))
            {
                _presenter.SetFieldsFromJobAction();
                ScriptManager.GetCurrent(this.Page).SetFocus(actJobAction.TextControlClientID);
            }
        }

        protected void gdvDivision_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CS_JobDivision jobDivision = e.Row.DataItem as CS_JobDivision;

                DivisionId = jobDivision.DivisionID;

                RadioButton radPrimaryDivision = (RadioButton)e.Row.FindControl("radPrimaryDivision");
                if (null != radPrimaryDivision)
                {
                    string script = "SetUniqueRadioButton('gdvDivision.*PrimaryDivision',this);";
                    script += "document.getElementById('" + txtGridValidation2.ClientID + "').value = '" + e.Row.RowIndex.ToString() + "';";
                    radPrimaryDivision.Attributes.Add("onclick", script);
                    radPrimaryDivision.Checked = jobDivision.PrimaryDivision;
                }
                txtGridValidation.Text = "1";

                LinkButton lnkRemove = e.Row.Cells[3].FindControl("lnkRemove") as LinkButton;
                if (null != lnkRemove)
                {
                    lnkRemove.Attributes.Add("OnClientClick", "ignoreDirty();return confirm('Are you sure you want to remove this item from list?')");

                    if (JobId.HasValue)
                    {
                        _presenter.GetReserveByDivision();
                        _presenter.GetResourcesByDivision();

                        if (GetReserveByDivision.Count > 0)
                        {
                            lnkRemove.Enabled = false;
                            lnkRemove.Attributes.Remove("OnClientClick");
                        }
                        if (GetResourcesByDivision.Count > 0)
                        {
                            lnkRemove.Enabled = false;
                            lnkRemove.Attributes.Remove("OnClientClick");
                        }
                    }

                    lnkRemove.CommandArgument = e.Row.DataItemIndex.ToString();
                }
            }
        }

        protected void gdvDivision_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("remove", StringComparison.OrdinalIgnoreCase))
            {
                _divisionRemoveIndex = Convert.ToInt32(e.CommandArgument);

                if (gdvDivision.Rows.Count == 1)
                {
                    txtGridValidation.Text = "";
                    txtGridValidation2.Text = "";
                }
                else if (txtGridValidation2.Text.Equals(e.CommandArgument.ToString()))
                {
                    txtGridValidation2.Text = string.Empty;
                }
                else if (Convert.ToInt32(txtGridValidation2.Text) > _divisionRemoveIndex)
                {
                    txtGridValidation2.Text = (Convert.ToInt32(txtGridValidation2.Text) - 1).ToString();
                }

                _presenter.RemoveDivision();
            }
        }

        protected void btnAddDivision_Click(object sender, EventArgs e)
        {
            _jobDivisionItem = new CS_JobDivision();
            _jobDivisionItem.DivisionID = int.Parse(ddlDivision.SelectedValue);
            _jobDivisionItem.CreatedBy = this.UserName;
            _jobDivisionItem.Active = true;
            _presenter.AddDivision(null);

            ScriptManager.GetCurrent(this.Page).SetFocus(btnAddDivision);
        }

        protected void gdvCustomerContract_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //CS_CustomerContract contractDetails = e.Row.DataItem as CS_CustomerContract;
                //if (null != contractDetails)
                //{
                //    HyperLink hypView = e.Row.Cells[3].FindControl("hypView") as HyperLink;
                //    if (null != hypView)
                //    {
                //        hypView.NavigateUrl = "javascript: ignoreDirty();";
                //        hypView.Attributes["onclick"] = string.Format("ignoreDirty(); var windowContract = window.open('/ContractDetail.aspx?contractId={0}', '', 'width=600, height=400, scrollbars=yes');", contractDetails.ID);
                //    }
                //}
            }
        }

        protected void cvRequestedByValidator_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            if (ckbInterimBill.Checked && actRequestedBy.SelectedValue == "0")
            {
                e.IsValid = false;
            }
            else
            {
                e.IsValid = true;
            }
        }

        protected void cvFrequencyValidator_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            if (ckbInterimBill.Checked && ddlFrequency.SelectedIndex == 0)
            {
                e.IsValid = false;
            }
            else
            {
                e.IsValid = true;
            }
        }

        #endregion

        #region [ Properties ]

        public string PriceTypeClientID
        {
            get { return ddlPriceType.ClientID; }
        }

        public string JobStatusClientID
        {
            get
            {
                return ddlJobStatus.ClientID;
            }
        }

        #endregion
    }
}
