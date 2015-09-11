using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.DataContext;

using Hulcher.OneSource.CustomerService.Core.Security;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Text;

namespace Hulcher.OneSource.CustomerService.Web.UserControls.JobRecord
{
    public partial class JobDescription : System.Web.UI.UserControl, IJobDescriptionView
    {

        #region [ Attributes ]

        CS_ScopeOfWork _ScopeOfWork;

        /// <summary>
        /// Presenter class for the Job Description User Control
        /// </summary>
        private JobDescriptionPresenter _presenter;

        private int? _jobId;

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Set the Validation Group of the Validators inside the User Control
        /// </summary>
        public string ValidationGroup
        {
            set
            {
                rfvGridValidation.ValidationGroup = value;
            }
        }

        /// <summary>
        /// Get and Set the JobId
        /// </summary>
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

        public int? CloningId
        {
            get;
            set;
        }

        public int NumberEnginesValue
        {
            get
            {
                int numberEngines;
                int.TryParse(txtNumberEngines.Text, out numberEngines);
                return numberEngines;
            }
            set
            {
                txtNumberEngines.Text = value.ToString();
            }
        }

        public int NumberLoadsValue
        {
            get
            {
                int numberLoads;
                int.TryParse(txtNumberLoads.Text, out numberLoads);
                return numberLoads;
            }
            set
            {
                txtNumberLoads.Text = value.ToString();
            }
        }

        public int NumberEmptiesValue
        {
            get
            {
                int numberEmpties;
                int.TryParse(txtNumberEmpties.Text, out numberEmpties);
                return numberEmpties;
            }
            set
            {
                txtNumberEmpties.Text = value.ToString();
            }
        }


        public string LadingValue
        {
            get
            {
                return txtLading.Text;
            }
            set
            {
                txtLading.Text = value;
            }
        }

        public string UnNumber
        {
            get
            {
                return txtUnNumber.Text;
            }
            set
            {
                txtUnNumber.Text = value;
            }
        }

        public string StccInfo
        {
            get
            {
                return txtStccInfo.Text;
            }
            set
            {
                txtStccInfo.Text = value;
            }
        }

        public string Hazmat
        {
            get
            {
                return txtHazmat.Text;
            }
            set
            {
                txtHazmat.Text = value;
            }
        }

        public IList<CS_ScopeOfWork> ScopeOfWorkList
        {
            get
            {
                if (null == ViewState["ScopeOfWork"])
                    ViewState["ScopeOfWork"] = new List<CS_ScopeOfWork>();
                return (IList<CS_ScopeOfWork>)ViewState["ScopeOfWork"];
            }
            set
            {
                ViewState["ScopeOfWork"] = value;
                ListScopeOfWork();
            }
        }

        public CS_ScopeOfWork ScopeOfWorkEntity
        {
            get
            {
                if (_ScopeOfWork == null)
                    _ScopeOfWork = new CS_ScopeOfWork();

                return _ScopeOfWork;
            }
            set
            {
                _ScopeOfWork = value;
            }
        }

        public CS_JobDescription JobDescriptionEntity
        {
            get
            {
                CS_JobDescription jobDescription = new CS_JobDescription();

                jobDescription.NumberEngines = null;
                jobDescription.NumberEmpties = null;
                jobDescription.NumberLoads = null;

                if (string.Empty != txtNumberEngines.Text)
                {
                    jobDescription.NumberEngines = int.Parse(txtNumberEngines.Text);
                }

                if (string.Empty != txtNumberEmpties.Text)
                {
                    jobDescription.NumberEmpties = int.Parse(txtNumberEmpties.Text);
                }

                if (string.Empty != txtNumberLoads.Text)
                {
                    jobDescription.NumberLoads = int.Parse(txtNumberLoads.Text);
                }
                jobDescription.Lading = txtLading.Text;
                jobDescription.UnNumber = txtUnNumber.Text;
                jobDescription.STCCInfo = txtStccInfo.Text;
                jobDescription.Hazmat = txtHazmat.Text;
                jobDescription.Active = true;
                jobDescription.CreatedBy = ((ContentPage)this.Page.Master).Username;
                jobDescription.CreationDate = DateTime.Now;
                jobDescription.ModifiedBy = ((ContentPage)Page.Master).Username;
                jobDescription.ModificationDate = DateTime.Now;

                return jobDescription;
            }
        }

        public CS_View_GetJobData JobDescriptionLoad
        {
            set
            {
                CS_View_GetJobData jobDescription = value;

                if (null != jobDescription)
                {
                    if (jobDescription.NumberEngines.HasValue)
                    {
                        NumberEnginesValue = jobDescription.NumberEngines.Value;
                    }

                    if (jobDescription.NumberEmpties.HasValue)
                    {
                        NumberEmptiesValue = jobDescription.NumberEmpties.Value;
                    }

                    if (jobDescription.NumberLoads.HasValue)
                    {
                        NumberLoadsValue = jobDescription.NumberLoads.Value;
                    }

                    LadingValue = jobDescription.Lading;
                    UnNumber = jobDescription.UnNumber;
                    StccInfo = jobDescription.STCCInfo;
                    Hazmat = jobDescription.Hazmat;
                }
            }
        }

        #endregion

        #region [ Overrides ]

        /// <summary>
        /// Initializes the Presenter class
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _presenter = new JobDescriptionPresenter(this);
        }

        #endregion

        #region [ Methods ]

        public void ClearFields()
        {
            txtScope.Text = string.Empty;
            chkScopeChange.Checked = false;
        }

        public void ListScopeOfWork()
        {
            sgvScopeOfWork.DataSource = ScopeOfWorkList;
            sgvScopeOfWork.DataBind();
        }

        public void ReadyOnlyJobDescription()
        {
            txtUnNumber.Enabled = false;
            txtStccInfo.Enabled = false;
            txtScope.Enabled = false;
            txtNumberLoads.Enabled = false;
            txtNumberEngines.Enabled = false;
            txtNumberEmpties.Enabled = false;
            txtLading.Enabled = false;
            txtHazmat.Enabled = false;
            txtGridValidation.Enabled = false;
            btnAdd.Enabled = false;
            chkScopeChange.Enabled = false;
        }

        #endregion

        #region [ Events ]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (_jobId.HasValue)
                {
                    _presenter.LoadScopeOfWork();
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            AddScopeOfWork(string.Empty);
        }

        public void AddScopeOfWork(string scopeOfWork)
        {
            _ScopeOfWork = new CS_ScopeOfWork();

            if (string.IsNullOrEmpty(scopeOfWork))
                _ScopeOfWork.ScopeOfWork = txtScope.Text;
            else
                _ScopeOfWork.ScopeOfWork = scopeOfWork;

            _ScopeOfWork.ScopeChange = chkScopeChange.Checked;
            _ScopeOfWork.CreatedBy = ((ContentPage)this.Page.Master).Username;
            _ScopeOfWork.CreationDate = DateTime.Now;
            _ScopeOfWork.ModifiedBy = ((ContentPage)Page.Master).Username;
            _ScopeOfWork.ModificationDate = DateTime.Now;

            _presenter.AddScopeOfWork();

            ScriptManager.GetCurrent(this.Page).SetFocus(btnAdd);

            upScopeOfWork.Update();
        }

        protected void sgvScopeOfWork_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CS_ScopeOfWork scopeOfWork = e.Row.DataItem as CS_ScopeOfWork;

                if (null != scopeOfWork)
                {
                    e.Row.Cells[2].Text = scopeOfWork.CreationDate.ToString("MM/dd/yyyy");
                    e.Row.Cells[3].Text = scopeOfWork.CreationDate.ToString("HH:mm:ss");
                }

                txtGridValidation.Text = "1";
            }
        }

        #endregion

        #region [ Abstract Implementation ]

        /// <summary>
        /// Display the error message
        /// </summary>
        public void DisplayMessage(string message, bool closeWindow)
        {
            //((IJobInfoView)Page).DisplayMessage(message, closeWindow);
        }

        #endregion
    }
}