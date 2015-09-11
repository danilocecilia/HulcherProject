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
    public partial class PhotoReport : System.Web.UI.UserControl, IPhotoReportView
    {
        #region [ Attributes ]

        /// <summary>
        /// Presenter class for the Photo Report User Control
        /// </summary>
        private PhotoReportPresenter _presenter;

        /// <summary>
        /// Stream that contains the file that was uploaded by the user
        /// </summary>
        private Stream _uploadedFile;

        /// <summary>
        /// Photo Report item to add to the Permit List
        /// </summary>
        private CS_JobPhotoReport _photoReportItem;

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
            _presenter = new PhotoReportPresenter(this);
        }

        #endregion

        #region [ Events ]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (_jobId.HasValue)
                    _presenter.GetPhotoReportByJobId();

                ListPhotoReport();
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
                _photoReportItem = new CS_JobPhotoReport();
                _photoReportItem.Description = txtDescription.Text;
                if (null != FileName)
                {
                    _photoReportItem.FileName = FileName;
                    _photoReportItem.Path = FilePath;
                }
                _photoReportItem.CreatedBy = ((ContentPage)Page.Master).Username;
                _photoReportItem.CreationDate = DateTime.Now;
                _photoReportItem.ModifiedBy = ((ContentPage)Page.Master).Username;
                _photoReportItem.ModificationDate = DateTime.Now;

                _presenter.AddPhotoReport();
            }

            ScriptManager.GetCurrent(this.Page).SetFocus(btnAdd);
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

                    LinkButton lnkRemove = e.Row.Cells[6].FindControl("lnkRemove") as LinkButton;
                    if (null != lnkRemove)
                        lnkRemove.CommandArgument = e.Row.DataItemIndex.ToString();
                }
            }
        }

        protected void grdPhotoReport_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("remove", StringComparison.OrdinalIgnoreCase))
            {
                _removeIndex = Convert.ToInt32(e.CommandArgument);
                _presenter.RemovePhotoReport();
            }
        }

        #endregion

        #region [ View Interface Implementation ]

        public IList<CS_JobPhotoReport> PhotoReportList
        {
            get
            {
                if (null == ViewState["PhotoReportList"])
                    ViewState["PhotoReportList"] = new List<CS_JobPhotoReport>();
                return (IList<CS_JobPhotoReport>)ViewState["PhotoReportList"];
            }
            set
            {
                ViewState["PhotoReportList"] = value;
            }
        }

        public Stream UploadedFile
        {
            get { return _uploadedFile; }
            set { _uploadedFile = value; }
        }

        public CS_JobPhotoReport PhotoReportEntity
        {
            get { return _photoReportItem; }
            set { _photoReportItem = value; }
        }

        public int RemoveIndex
        {
            get { return _removeIndex; }
            set { _removeIndex = value; }
        }

        public string UploadFolder
        {
            get { return Server.MapPath(Globals.Configuration.UploadPhotoReportPath); }
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

        public void ListPhotoReport()
        {
            grdPhotoReport.DataSource = PhotoReportList;
            grdPhotoReport.DataBind();
        }

        public void ClearFields()
        {
            txtAttach.Text = string.Empty;
            txtDescription.Text = string.Empty;
            FileName = null;
            FilePath = null;
            FullFilePath = null;
            afuAttach.ClearAllFilesFromPersistedStore();
        }

        public void DisplayMessage(string message, bool closeWindow)
        {
            ((IJobRecordView)Page).DisplayMessage(message, closeWindow);
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

        #endregion

        #region [ METHODS ]
        public void ReadyOnlyPhotoReports()
        {
            afuAttach.Enabled = false;
            txtAttach.Enabled = false;
            txtDescription.Enabled = false;
            grdPhotoReport.Enabled = false;
        }
        #endregion
    }
}