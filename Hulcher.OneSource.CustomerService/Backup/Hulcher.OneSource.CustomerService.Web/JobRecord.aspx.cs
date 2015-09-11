using System;
using System.Linq;
using System.Collections.Generic;
using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.DataContext.VO;
using Hulcher.OneSource.CustomerService.Core;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;
using System.IO;
using Hulcher.OneSource.CustomerService.Core.Utils;
using System.Text;

namespace Hulcher.OneSource.CustomerService.Web
{
    public partial class JobRecord : System.Web.UI.Page, IJobRecordView
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of the Presenter Class
        /// </summary>
        private JobRecordPresenter _presenter;

        private ObjectStateFormatter _formatter = new ObjectStateFormatter();

        #endregion

        #region [ Overrides ]

        /// <summary>
        /// Initializes the Presenter class
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _presenter = new JobRecordPresenter(this);
            uscCustomer.DivisionChanged += new UserControls.JobRecord.CustomerInfo.DivisionChangedHandler(uscCustomer_DivisionChanged);
            uscCustomer.CustomerChanged += new UserControls.JobRecord.CustomerInfo.CustomerChangedHandler(uscCustomer_CustomerChanged);
            uscCustomer.CustomerContactChanged += new UserControls.JobRecord.CustomerInfo.CustomerContactChangedHandler(uscCustomer_CustomerContactChanged);
            //uscCustomer.CustomerEssentialHiddenFieldValueClientID = actCustomerEssential.HiddenFieldValueClientID;
            //uscJobInfo.CallDateEssentialTextBoxClientID = dpCallDateEssential.TextBoxClientID;
            //uscJobInfo.CallTimeEssentialTextBoxClientID = txtCallTimeEssential.ClientID;
        }

        void uscCustomer_CustomerContactChanged(CS_Contact customerContactEntity)
        {
            uscJobInfo.DivisionFromExternalSource = null;
        }

        protected override void SavePageStateToPersistenceMedium(object viewState)
        {
            MemoryStream ms = new MemoryStream();
            _formatter.Serialize(ms, viewState);
            byte[] viewStateArray = ms.ToArray();
            ScriptManager.RegisterHiddenField(this, "__COMPRESSEDVIEWSTATE", Convert.ToBase64String(WebUtil.Compress(viewStateArray)));
        }

        protected override object LoadPageStateFromPersistenceMedium()
        {
            string vsString = Request.Form["__COMPRESSEDVIEWSTATE"];
            if (!string.IsNullOrEmpty(vsString))
            {
                byte[] bytes = Convert.FromBase64String(vsString);
                bytes = WebUtil.Decompress(bytes);
                return _formatter.Deserialize(Convert.ToBase64String(bytes));
            }
            else
                return base.LoadPageStateFromPersistenceMedium();
        }

        #endregion

        #region [ Events ]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString.Get("CreateInitialAdvise")))
                    CreateInitialAdvise = Request.QueryString.Get("CreateInitialAdvise").ToString().ToUpper() == "TRUE";

                if (!string.IsNullOrEmpty(Request.QueryString.Get("JobId")))
                {
                    btnSave.Visible = false;
                    btnSaveAndContinue.Visible = true;
                    btnSaveClose.Visible = true;

                    JobId = Convert.ToInt32(Request.QueryString.Get("JobId"));
                    btnJobCloning.Enabled = true;

                    _presenter.LoadJobEntity();

                    _presenter.SetEmergencyResponse();

                    _presenter.GetJobStartAndCloseDate();
                    btnResourceAllocation.Disabled = false;
                    btnResourceAllocation.Attributes["onclick"] = string.Format("javascript: var newWindow = window.open('/ResourceAllocation.aspx?JobId={0}&ParentControlId={1}', '', 'width=800, height=600, scrollbars=1, resizable=yes'); return false;", JobId, txtParentUpdateClient);

                    btnCallEntry.Disabled = false;
                    btnCallEntry.Attributes["onclick"] = string.Format("javascript: var newWindow = window.open('/CallEntry.aspx?JobId={0}', '', 'width=800, height=600, scrollbars=1, resizable=yes');", JobId);

                    StringBuilder cloningScript = new StringBuilder();

                    cloningScript.AppendLine("javascript:");
                    cloningScript.AppendLine("if (confirm('Are you sure you want to clone this job? The data will be copied over into a new record.'))");
                    cloningScript.AppendFormat("  var newWindow = window.open('/JobRecord.aspx?CloningId={0}', '', 'width=800, height=600, scrollbars=1, resizable=yes');", JobId);
                    cloningScript.AppendLine("else");
                    cloningScript.AppendLine("  return false;");

                    btnJobCloning.Attributes["onclick"] = cloningScript.ToString();

                    _presenter.CheckReadOnlyAccess();
                }
                else if (string.IsNullOrEmpty(Request.QueryString.Get("CloningId")))
                {
                    CreateInitialAdvise = true;
                }
                else
                {
                    CloningId = Convert.ToInt32(Request.QueryString.Get("CloningId"));
                    JobId = Convert.ToInt32(Request.QueryString.Get("CloningId"));
                    _presenter.LoadJobEntity();
                    JobId = null;
                    CreateInitialAdvise = true;

                    btnResourceAllocation.Disabled = true;
                    btnResourceAllocation.Attributes["onclick"] = "";

                    btnCallEntry.Disabled = true;
                    btnCallEntry.Attributes["onclick"] = "";
                }

                _presenter.UpdateTitle();
            }

            Title = PageTitle;
            lblTitle.Text = PageTitle;
        }

        protected void uscCustomer_DivisionChanged(CS_Division divisionEntity)
        {
            uscJobInfo.DivisionFromExternalSource = divisionEntity;
            uscJobInfo.RefreshUpdatePanel();
        }

        protected void uscCustomer_CustomerChanged(CS_Customer customerEntity)
        {
            uscJobInfo.CustomerFromExternalSource = customerEntity;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            CheckValidatorsForCustomerInfo();

            if (IsValid)
            {
                _presenter.SaveJobData();

                if (JobId.HasValue)
                {
                    btnResourceAllocation.Attributes.Remove("disabled");
                    btnResourceAllocation.Attributes["onclick"] = string.Format("javascript: var newWindow = window.open('/ResourceAllocation.aspx?JobId={0}&ParentControlId={1}', '', 'width=800, height=600, scrollbars=1, resizable=yes'); return false;", JobId, txtParentUpdateClient);
                    btnCallEntry.Attributes.Remove("disabled");
                    btnCallEntry.Attributes["onclick"] = string.Format("javascript: var newWindow = window.open('/CallEntry.aspx?JobId={0}', '', 'width=800, height=600, scrollbars=1, resizable=yes');", JobId);

                    btnJobCloning.Enabled = true;
                    StringBuilder cloningScript = new StringBuilder();

                    cloningScript.AppendLine("javascript:");
                    cloningScript.AppendLine("if (confirm('Are you sure you want to clone this job? The data will be copied over into a new record.'))");
                    cloningScript.AppendFormat("  var newWindow = window.open('/JobRecord.aspx?CloningId={0}', '', 'width=800, height=600, scrollbars=1, resizable=yes');", JobId);
                    cloningScript.AppendLine("else");
                    cloningScript.AppendLine("  return false;");

                    btnJobCloning.Attributes["onclick"] = cloningScript.ToString();

                    btnSave.Visible = false;
                    btnSaveAndContinue.Visible = true;
                    btnSaveClose.Visible = true;
                }
            }

            
        }

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            SaveAndClose = true;

            CheckValidatorsForCustomerInfo();

            if (IsValid)
            {
                _presenter.UpdateJobData();

                if (JobId.HasValue)
                {
                    btnResourceAllocation.Attributes.Remove("disabled");
                    btnResourceAllocation.Attributes["onclick"] = string.Format("javascript: var newWindow = window.open('/ResourceAllocation.aspx?JobId={0}&ParentControlId={1}', '', 'width=800, height=600, scrollbars=1, resizable=yes'); return false;", JobId, txtParentUpdateClient);
                    btnCallEntry.Attributes.Remove("disabled");
                    btnCallEntry.Attributes["onclick"] = string.Format("javascript: var newWindow = window.open('/CallEntry.aspx?JobId={0}', '', 'width=800, height=600, scrollbars=1, resizable=yes');", JobId);

                    btnJobCloning.Enabled = true;
                    StringBuilder cloningScript = new StringBuilder();

                    cloningScript.AppendLine("javascript:");
                    cloningScript.AppendLine("if (confirm('Are you sure you want to clone this job? The data will be copied over into a new record.'))");
                    cloningScript.AppendFormat("  var newWindow = window.open('/JobRecord.aspx?CloningId={0}', '', 'width=800, height=600, scrollbars=1, resizable=yes');", JobId);
                    cloningScript.AppendLine("else");
                    cloningScript.AppendLine("  return false;");

                    btnJobCloning.Attributes["onclick"] = cloningScript.ToString();
                }
            }
        }

        protected void btnSaveContinue_Click(object sender, EventArgs e)
        {
            SaveAndClose = false;

            CheckValidatorsForCustomerInfo();

            if (IsValid)
            {
                _presenter.UpdateJobData();

                if (JobId.HasValue)
                {
                    btnResourceAllocation.Attributes.Remove("disabled");
                    btnResourceAllocation.Attributes["onclick"] = string.Format("javascript: var newWindow = window.open('/ResourceAllocation.aspx?JobId={0}&ParentControlId={1}', '', 'width=800, height=600, scrollbars=1, resizable=yes'); return false;", JobId, txtParentUpdateClient);
                    btnCallEntry.Attributes.Remove("disabled");
                    btnCallEntry.Attributes["onclick"] = string.Format("javascript: var newWindow = window.open('/CallEntry.aspx?JobId={0}', '', 'width=800, height=600, scrollbars=1, resizable=yes');", JobId);

                    btnJobCloning.Enabled = true;
                    StringBuilder cloningScript = new StringBuilder();

                    cloningScript.AppendLine("javascript:");
                    cloningScript.AppendLine("if (confirm('Are you sure you want to clone this job? The data will be copied over into a new record.'))");
                    cloningScript.AppendFormat("  var newWindow = window.open('/JobRecord.aspx?CloningId={0}', '', 'width=800, height=600, scrollbars=1, resizable=yes');", JobId);
                    cloningScript.AppendLine("else");
                    cloningScript.AppendLine("  return false;");

                    btnJobCloning.Attributes["onclick"] = cloningScript.ToString();
                }
            }
        }

        protected void cvDivisionValidator_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            if (this.uscJobInfo.JobDivisionEntityList.Count > 0)
                e.IsValid = true;
            else
                e.IsValid = false;
        }
        #endregion

        #region [ View Interface Implementation ]

        public bool CreateInitialAdvise
        {
            get
            {
                return hfCreateInitialAdvise.Value.ToUpper().Equals("TRUE");
            }
            set
            {
                if (value)
                    btnSave.OnClientClick += "SaveInitialEmail();";
                else
                    btnSave.OnClientClick = btnSave.OnClientClick.Replace("SaveInitialEmail();", "");

                hfCreateInitialAdvise.Value = value.ToString();
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

        public void SetCloseEnabled()
        {
            ScriptManager.RegisterClientScriptBlock(
                    Page,
                    this.GetType(),
                    "SetCloseEnabled",
                    string.Format("document.getElementById('{0}').value = 'false';", hfCreateInitialAdvise.ClientID),
                    true);
        }

        public CS_CustomerInfo CustomerInfo
        {
            get { return uscCustomer.CustomerInfoEntity; }
        }

        public IList<CS_JobDivision> DivisionList
        {
            get { return uscJobInfo.JobDivisionEntityList; }
        }

        public CS_PresetInfo PresetInfo
        {
            get { return uscJobInfo.PresetInfoEntity; }
        }

        public CS_LostJobInfo LostJobInfo
        {
            get { return uscJobInfo.LostJobEntity; }
        }

        public CS_SpecialPricing SpecialPricing
        {
            get { return uscJobInfo.SpecialPricingEntity; }
        }

        public CS_JobInfo JobInfo
        {
            get { return uscJobInfo.JobInfoEntity; }
        }

        public DateTime? JobStartDate
        {
            get { return uscJobInfo.JobStartDate; }
            set { uscJobInfo.JobStartDate = value; }
        }

        public DateTime? JobCloseDate
        {
            get { return uscJobInfo.JobCloseDate; }
            set { uscJobInfo.JobCloseDate = value; }
        }

        public CS_LocationInfo LocationInfo
        {
            get { return uscLocation.LocationInfoEntity; }
        }

        public CS_JobDescription JobDescription
        {
            get { return uscJobDescription.JobDescriptionEntity; }
        }

        public IList<CS_ScopeOfWork> ScopeOfWorkList
        {
            get { return uscJobDescription.ScopeOfWorkList; }
        }

        public IList<CS_JobPermit> PermitInfoList
        {
            get { return uscPermitInfo.PermitInfoEntityList; }
        }

        public IList<CS_JobPhotoReport> PhotoReportList
        {
            get { return uscPhotoReport.PhotoReportList; }
        }

        public IList<LocalEquipmentTypeVO> RequestedEquipmentTypeList
        {
            get { return uscEquipmentRequested.SelectedEquipments; }
            set { uscEquipmentRequested.SelectedEquipments = value; }
        }

        public CS_Job Job
        {
            get
            {
                CS_Job csJob = new CS_Job();
                csJob.CreatedBy = Master.Username;
                csJob.CreationDate = DateTime.Now;
                csJob.ModifiedBy = Master.Username;
                csJob.ModificationDate = DateTime.Now;
                csJob.Active = true;
                csJob.EmergencyResponse = IsEmergencyResponse;

                return csJob;
            }
        }

        public CS_View_GetJobData JobLoad
        {
            get
            {
                return ViewState["job"] as CS_View_GetJobData;
            }
            set
            {
                ViewState["job"] = value;

                uscCustomer.CustomerInfoLoad = value;
                uscJobDescription.JobDescriptionLoad = value;
                uscLocation.LocationInfoLoad = value;
                uscJobInfo.JobInfoLoad = value;
                uscJobCallLog.Refresh();
            }
        }

        public bool IsEmergencyResponse
        {
            get
            {
                return Convert.ToBoolean(rblEmergencyResponse.SelectedValue);
            }
            set
            {

                rblEmergencyResponse.SelectedValue = value.ToString();
            }
        }

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

                if (value.HasValue)
                {
                    uscCustomer.JobId = value;
                    uscJobInfo.JobId = value;
                    uscLocation.JobId = value;
                    uscJobDescription.JobId = value;
                    uscEquipmentRequested.JobId = value;
                    uscPermitInfo.JobId = value;
                    uscPhotoReport.JobId = value;

                    chJobCallLog.Collapsed = false;
                }
                else
                {
                    uscCustomer.JobId = value;
                    uscJobInfo.JobId = value;
                    uscLocation.JobId = value;
                    uscJobDescription.JobId = value;
                    uscEquipmentRequested.JobId = value;
                    uscPermitInfo.JobId = value;
                    uscPhotoReport.JobId = value;

                    chJobCallLog.Collapsed = false;

                    chCustomerInfo.Collapsed = false;
                    chJobInfo.Collapsed = false;
                    chLocationInfo.Collapsed = false;
                    chJobDescription.Collapsed = false;
                    chEquipmentRequested.Collapsed = false;
                }

                if (!CloningId.HasValue)
                    uscJobCallLog.JobId = value;
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
            get
            {
                if (null != ViewState["PageTitle"])
                    return ViewState["PageTitle"].ToString();
                return string.Empty;
            }
            set 
            { 
                ViewState["PageTitle"] = value;
                Title = value;
                lblTitle.Text = value;
            }
        }

        public string txtParentUpdateClient
        {
            get { return uscJobCallLog.txtParentControlClientId; }
        }

        public int? CloningId
        {
            get
            {
                if (!String.IsNullOrEmpty(Request.QueryString["CloningId"]))
                    return Int32.Parse(Request.QueryString["CloningId"]);
                return null;
            }
            set
            {
                uscCustomer.CloningId = value;
                uscJobDescription.CloningId = value;
                uscLocation.CloningId = value;
                uscJobInfo.CloningId = value;
            }
        }

        public int JobStatusId
        {
            get { return uscJobInfo.JobStatusValue; }
        }

        public bool ResourceConversion
        {
            set
            {
                if (value)
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ResourceConversion", string.Format("var newWindow = window.open('/ResourceAllocation.aspx?JobId={0}&ParentControlId={1}&ResourceConversion=True', '', 'width=800, height=600, scrollbars=1, resizable=yes');", JobId, txtParentUpdateClient), true);
                else
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NoResourceConversion", string.Format("var newWindow = window.open('/ResourceAllocation.aspx?JobId={0}&ParentControlId={1}', '', 'width=800, height=600, scrollbars=1, resizable=yes');", JobId, txtParentUpdateClient), true);
            }
        }

        public bool HasResources
        {
            set
            {
                if (value)
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HasResources", string.Format("var newWindow = window.open('/CallEntry.aspx?JobId={0}&CallTypeID={1}', '', 'width=800, height=600, scrollbars=1, resizable=yes');", JobId, (int)Globals.CallEntry.CallType.Parked), true);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HasResources", string.Format("var newWindow = window.open('/CallEntry.aspx?JobId={0}', '', 'width=800, height=600, scrollbars=1, resizable=yes');", JobId), true);

                }
            }
        }

        public bool SaveInitialAdviseEmail
        {
            set
            {
                hidSaveInitialEmail.Value = value.ToString().ToLower();
            }
            get
            {
                return (string.IsNullOrEmpty(hidSaveInitialEmail.Value)) ? false : Convert.ToBoolean(hidSaveInitialEmail.Value);
            }
        }

        public bool OpenEmailInitialAdvise
        {
            set
            {
                StringBuilder script = new StringBuilder();

                if (value)
                {
                    script.AppendFormat("RedirectEmailPage({0}); ", JobId);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SendEmailPage", script.ToString(), true);
                }
            }
        }


        #endregion

        #region [ Methods ]

        public bool ReadyOnlyJobRecords
        {
            set
            {
                if (value)
                {
                    btnSave.Enabled = false;
                    rblEmergencyResponse.Enabled = false;

                    uscJobInfo.ReadyOnlyJobInfo();
                    uscCustomer.ReadyOnlyCustomerInfo();
                    uscJobCallLog.ReadyOnlyJobCallLog();
                    uscLocation.ReadyOnlyLocationInfo();
                    uscPermitInfo.ReadyOnlyPermitInfo();
                    uscJobDescription.ReadyOnlyJobDescription();
                    uscEquipmentRequested.ReadOnlyEquipmentRequested();
                    uscPhotoReport.ReadyOnlyPhotoReports();
                }
            }
        }

        public void CheckValidatorsForCustomerInfo()
        {
            uscCustomer.RequiredFieldValidatorActCustomer();
        }

        //public void SetEmailPage()
        //{
        //    if (this.JobId.HasValue)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow",
        //        "window.open('Email.aspx?JobId=" + this.JobId.Value + "','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=0,resizable=0,width=600,');", true);
        //    }
        //}

        #endregion

        #region [ Propperties ] 
        public bool SaveAndClose { get; set; }
        #endregion
    }
}
