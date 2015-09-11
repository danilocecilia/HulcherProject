using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Business.Presenter;
using System.Web.UI.HtmlControls;

namespace Hulcher.OneSource.CustomerService.Web
{
    public partial class SubContractorMaintenance : System.Web.UI.Page, ISubcontractorMaintenanceView
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of Presenter Class
        /// </summary>
        private SubcontractorMaintenancePresenter _presenter;

        #endregion

        #region [ Overrides ]

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _presenter = new SubcontractorMaintenancePresenter(this);
        }

        #endregion

        #region [ Events ]

        #region [ Save SubContractor ]
        public void btnSaveContinue_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                _presenter.SaveOrUpdateSubcontractor();
                _presenter.LoadSubcontractors();
                _presenter.ClearFields();
            }
        }

        public void btnSaveClose_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                _presenter.SaveOrUpdateSubcontractor();
                DisplayMessage("Save successfully.", true);
            }
        }
        #endregion

        #region [ Load ]
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CreationPanelVisible = false;
                _presenter.LoadSubcontractors();
                EnableDisableCreationButton = true;
                _presenter.VerifyAccess();
                btnCreate.Visible = !ReadOnly;
            }
        }
        #endregion


        public void btnCancel_Click(object sender, EventArgs e)
        {
            EnableDisableCreationButton = true;
            CreationPanelVisible = false;
            ClearField();
        }

        public void gvSubContractor_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditSubcontractor")
            {
                SubcontractorID = int.Parse(e.CommandArgument.ToString());
                _presenter.EditSubcontractor();
                _presenter.LoadSubcontractors();
                CreationPanelVisible = true;
                EnableDisableCreationButton = false;
            }
            else if (e.CommandName.Equals("RemoveSubcontractor"))
            {
                SubcontractorID = int.Parse(e.CommandArgument.ToString());
                _presenter.DeleteSubcontractor();
                _presenter.LoadSubcontractors();
                _presenter.ClearFields();
            }
        }

        public void gvSubContractor_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                _presenter.VerifyAccess();
                LinkButton lnkEdit = (LinkButton)e.Row.Cells[6].FindControl("lnkEdit");
                LinkButton lnkRemove = (LinkButton)e.Row.Cells[7].FindControl("lnkRemove");
                if (lnkEdit != null)
                {
                    lnkEdit.Visible = !ReadOnly;
                }
                if (lnkRemove != null)
                {
                    lnkRemove.Visible = !ReadOnly;
                }
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            _presenter.ClearFields();
            SubcontractorID = null;
            CreationPanelVisible = true;
        }
        #endregion

        #region [ Methods ]

        public void ClearField()
        {
            txtSubcontractorName.Text = string.Empty;
        }
        #endregion

        #region [ Properties ]

        #region [ Commom ]
        public string UserName
        {
            get { return ((ContentPage)Page.Master).Username; }
        }

        public void DisplayMessage(string message, bool closeWindow)
        {
            Master.DisplayMessage(message, closeWindow);
        }

        public string Domain
        {
            get
            {
                return Master.Domain;
            }
        }

        #endregion

        #region [ SubContractor ]
        public IList<DataContext.CS_Subcontractor> SubcontractorList
        {
            set
            {
                gvSubContractor.DataSource = value;
                gvSubContractor.DataBind();
            }
        }

        public int? SubcontractorID
        {
            get
            {
                if (null == ViewState["SubcontractorID"])
                    return null;

                return int.Parse(ViewState["SubcontractorID"].ToString());
            }
            set
            {
                ViewState["SubcontractorID"] = value;
            }
        }

        public bool SavedSuccessfuly
        {
            get;
            set;
        }

        public string Name
        {
            get
            {
                return txtSubcontractorName.Text;
            }
            set
            {
                txtSubcontractorName.Text = value;
            }
        }

        public bool CreationPanelVisible
        {
            get { return pnlCreation.Visible; }
            set { pnlCreation.Visible = value; }
        }

        public bool EnableDisableCreationButton
        {
            set
            {
                btnCreate.Enabled = value;
            }
        }

        public bool EditMode
        {
            get
            {
                if (null == ViewState["EditMode"])
                    EditMode = false;

                return bool.Parse(ViewState["EditMode"].ToString());
            }
            set
            {
                ViewState["EditMode"] = value;
            }
        }


        public bool ReadOnly
        {
            get;
            set;
        }
        #endregion

        #endregion

    }
}
