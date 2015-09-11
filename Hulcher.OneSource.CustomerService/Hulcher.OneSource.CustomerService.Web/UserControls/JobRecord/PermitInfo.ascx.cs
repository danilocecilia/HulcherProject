using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core;

using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Hulcher.OneSource.CustomerService.Web.UserControls.JobRecord
{
    public partial class PermitInfo : System.Web.UI.UserControl, IPermitInfoView
    {
        #region [ Attributes ]
        
        /// <summary>
        /// Presenter class for the Permit Info User Control
        /// </summary>
        private PermitInfoPresenter _presenter;

        /// <summary>
        /// Stream that contains the file that was uploaded by the user
        /// </summary>
        private Stream _uploadedFile;

        /// <summary>
        /// Permit Info item to add to the Permit List
        /// </summary>
        private CS_JobPermit _permitInfoItem;

        /// <summary>
        /// Index of the item that should be removed from the list
        /// </summary>
        private int _removeIndex;

        private int? _jobId;

        #endregion

        #region [ Overrides ]

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _presenter = new PermitInfoPresenter(this);
        }

        #endregion

        #region [ Events ]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (_jobId.HasValue)
                    _presenter.GetPermitInfoByJobId();
                if (Request.QueryString["CloningId"] != null)
                {
                    CloningId = int.Parse(Request.QueryString["CloningId"]);
                    _presenter.ListPermitInfoCloning();
                }
                _presenter.ListPermitTypes();
                ListPermitInfo();                
            }
        }

        protected void afuAttach_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {
            if (afuAttach.HasFile)
            {
                _uploadedFile = afuAttach.PostedFile.InputStream;
                FileName = afuAttach.FileName;
                _presenter.SaveFile();
            }
        }

        protected void afuAttach_UploadedFileError(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {
            DisplayMessage("An error has ocurred while trying to upload the file. Please try again.", false);
            Logger.Write(string.Concat("An error has ocurred while trying to upload the file: !\n", e.statusMessage));
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                _permitInfoItem = new CS_JobPermit();
                _permitInfoItem.Type = int.Parse(cbPermitType.SelectedValue);
                _permitInfoItem.CS_JobPermitType = new CS_JobPermitType() { Description = cbPermitType.SelectedItem.Text };

                _permitInfoItem.Number = txtPermitNumber.Text;
                
                _permitInfoItem.Location = txtPermitLocation.Text;

                if (!string.IsNullOrEmpty(txtAgencyOperator.Text.Trim()))
                    _permitInfoItem.AgencyOperator = txtAgencyOperator.Text.Trim();
                else
                    _permitInfoItem.AgencyOperator = null;

                if (!string.IsNullOrEmpty(txtAgentOperator.Text.Trim()))
                    _permitInfoItem.AgentOperatorName = txtAgentOperator.Text.Trim();
                else
                    _permitInfoItem.AgentOperatorName = null;

                if (dpPermitDate.Value.HasValue)
                    _permitInfoItem.PermitDate = dpPermitDate.Value;
                else
                    _permitInfoItem.PermitDate = null;

                if (null != FileName)
                {
                    _permitInfoItem.FileName = FileName;
                    _permitInfoItem.Path = FilePath;
                }
                _permitInfoItem.CreatedBy = ((ContentPage)Page.Master).Username;
                _permitInfoItem.CreationDate = DateTime.Now;
                _permitInfoItem.ModifiedBy = ((ContentPage)Page.Master).Username;
                _permitInfoItem.ModificationDate = DateTime.Now;
                _presenter.AddPermit();
            }

            ScriptManager.GetCurrent(this.Page).SetFocus(btnAdd);
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
                    
                    LinkButton lnkRemove = e.Row.Cells[6].FindControl("lnkRemove") as LinkButton;
                    if (null != lnkRemove)
                        lnkRemove.CommandArgument = e.Row.DataItemIndex.ToString();
                }
            }
        }

        protected void grdPermitInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("remove", StringComparison.OrdinalIgnoreCase))
            {
                _removeIndex = Convert.ToInt32(e.CommandArgument);
                _presenter.RemovePermit();
            }
        }

        #endregion

        #region [ View Interface Implementation ]

        public void DisplayMessage(string message, bool closeWindow)
        {
            ((IJobRecordView)Page).DisplayMessage(message, closeWindow);
        }

        public IList<CS_JobPermit> PermitInfoEntityList
        {
            get
            {
                if (null == ViewState["PermitInfo"])
                    ViewState["PermitInfo"] = new List<CS_JobPermit>();
                return (IList<CS_JobPermit>)ViewState["PermitInfo"];
            }
            set
            {
                ViewState["PermitInfo"] = value;
            }
        }

        public Stream UploadedFile
        {
            get { return _uploadedFile; }
            set { _uploadedFile = value; }
        }

        public CS_JobPermit PermitInfoEntity
        {
            get { return _permitInfoItem; }
            set { _permitInfoItem = value; }
        }

        public int RemoveIndex
        {
            get { return _removeIndex; }
            set { _removeIndex = value; }
        }

        public string UploadFolder
        {
            get { return Server.MapPath(Globals.Configuration.UploadPermitPath); }
            set { } // do nothing
        }

        public string FileName
        {
            get
            {
                if (null == Session["FileName"])
                    return null;
                return Session["FileName"].ToString();
            }
            set { Session["FileName"] = value; }
        }

        public string FilePath
        {
            get
            {
                if (null == Session["FilePath"])
                    return null;
                return Session["FilePath"].ToString();
            }
            set { Session["FilePath"] = value; }
        }

        public string FullFilePath
        {
            get
            {
                if (null == Session["FullFilePath"])
                    return null;
                return Session["FullFilePath"].ToString();
            }
            set { Session["FullFilePath"] = value; }
        }

        public void ListPermitInfo()
        {
            grdPermitInfo.DataSource = PermitInfoEntityList;
            grdPermitInfo.DataBind();
        }

        public void ClearFields()
        {
            cbPermitType.SelectedValue = "1";
            txtPermitNumber.Text = string.Empty;
            txtPermitLocation.Text = string.Empty;
            txtAgencyOperator.Text = string.Empty;
            txtAgentOperator.Text = string.Empty;
            dpPermitDate.Value = null;
            FileName = null;
            FilePath = null;
            FullFilePath = null;
            afuAttach.ClearAllFilesFromPersistedStore();
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

        public IList<CS_JobPermitType> PermitTypeList
        {
            set
            {
                cbPermitType.DataSource = value;
                cbPermitType.DataTextField = "Description";
                cbPermitType.DataValueField = "ID";
                cbPermitType.DataBind();
                cbPermitType.Items.Insert(0, new ListItem("- Select One -", ""));
                cbPermitType.SelectedValue = ((int)Globals.Permitting.PermitType.Annual).ToString();
                
            }
        }

        #endregion

        #region [ Methods ]

        public void ReadyOnlyPermitInfo()
        {
            cbPermitType.Enabled = false;
            txtPermitNumber.Enabled = false;
            txtPermitLocation.Enabled = false;
            txtAgencyOperator.Enabled = false;
            txtAgentOperator.Enabled = false;
            dpPermitDate.EnableDatePicker = false;
            afuAttach.Enabled = false;
            btnAdd.Enabled = false;
        }
        #endregion
    }
}