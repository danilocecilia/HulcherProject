using System;
using System.Collections.Generic;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core.Security;
using System.Web;
using System.Web.UI.WebControls;
using Hulcher.OneSource.CustomerService.Core;

namespace Hulcher.OneSource.CustomerService.Web
{
    public partial class SamplePage : System.Web.UI.Page, ISamplePageView
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of the Presenter Class
        /// </summary>
        private SamplePagePresenter _presenter;

        /// <summary>
        /// Stores the Job Identifier in order to delete a record
        /// </summary>
        private int _jobId;

        #endregion

        #region [ Overrides ]

        /// <summary>
        /// Initializes the Presenter class
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _presenter = new SamplePagePresenter(this);
        }

        #endregion

        #region [ Events ]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                _presenter.ListAllJobs();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            _presenter.ListAllJobs();
        }

        protected void grdJobs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CS_Job job = e.Row.DataItem as CS_Job;
                int jobStatus = 0;
                if (null != job.CS_JobInfo)
                    jobStatus = job.CS_JobInfo.LastJobStatusID;
                if (!string.IsNullOrEmpty(job.Number) && jobStatus.Equals((int)Globals.JobRecord.JobStatus.Active))
                    e.Row.Cells[0].Text = job.Number;
                else
                    e.Row.Cells[0].Text = job.Internal_Tracking;

                if (null != job.CS_CustomerInfo)
                    if (null != job.CS_CustomerInfo.CS_Customer)
                        e.Row.Cells[1].Text = job.CS_CustomerInfo.CS_Customer.Name;

                HyperLink hypOpen = e.Row.Cells[4].FindControl("hypOpen") as HyperLink;
                if (null != hypOpen)
                    hypOpen.NavigateUrl = string.Format("javascript: var newWindow = window.open('/JobRecord.aspx?JobId={0}', '', 'width=870, height=600, scrollbars=1, resizable=yes');", job.ID);

                LinkButton lnkDelete = e.Row.Cells[5].FindControl("lnkDelete") as LinkButton;
                if (jobStatus.Equals((int)Globals.JobRecord.JobStatus.Active))
                    lnkDelete.Visible = false;
                else
                    lnkDelete.Visible = true;
            }
        }

        protected void grdJobs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("deleteitem", StringComparison.OrdinalIgnoreCase))
            {
                _jobId = Convert.ToInt32(e.CommandArgument);
                _presenter.DeleteJob();
            }
        }

        #endregion

        #region [ View Interface Implementation ]

        /// <summary>
        /// Sets the Job List to the GridView on the WebPage
        /// </summary>
        public IList<CS_Job> JobList
        {
            set
            {
                grdJobs.DataSource = value;
                grdJobs.DataBind();

                AZManager azManager = new AZManager();
                AZOperation[] azOP = azManager.CheckAccessById(Master.Username, Master.Domain, new Globals.Security.Operations[] { Globals.Security.Operations.AlterJob });
                grdJobs.Columns[5].Visible = azOP[0].Result;
            }
        }

        /// <summary>
        /// Gets the selected job Identifier to delete
        /// </summary>
        public int JobId
        {
            get { return _jobId; }
        }

        /// <summary>
        /// Gets the Username that requested an operation
        /// </summary>
        public string Username
        {
            get { return Master.Username; }
        }

        /// <summary>
        /// Display a Message to the user
        /// </summary>
        /// <param name="message">Message to be displayed</param>
        public void DisplayMessage(string message, bool closeWindow)
        {
            Master.DisplayMessage(message, closeWindow);
        }

        #endregion
    }
}