using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Core.Utils;
using System.Web.UI.HtmlControls;

namespace Hulcher.OneSource.CustomerService.Web.UserControls.JobRecord
{
    public partial class JobCallLog : System.Web.UI.UserControl, IJobCallLogView
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of the presenter class for the User Control
        /// </summary>
        private JobCallLogPresenter _presenter;

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Property for storing the Job ID in the control
        /// </summary>
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

        /// <summary>
        /// Property for ScrollableGridView's DataSource manipulation
        /// </summary>
        public IList<CS_CallLog> DataSource
        {
            set
            {
                sgvCallLog.DataSource = value;
                sgvCallLog.DataBind();
            }
        }

        /// <summary>
        /// Property for accessing the FilterType in the control
        /// </summary>
        public Globals.JobRecord.FilterType FilterType
        {
            get { return (Globals.JobRecord.FilterType)int.Parse(cbbFilter.SelectedValue); }
        }

        /// <summary>
        /// Property for accessing the FilterValue in the control
        /// </summary>
        public string FilterValue
        {
            get { return txtFilterValue.Text; }
        }

        #endregion

        #region [ Overrides ]

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _presenter = new JobCallLogPresenter(this);
        }

        #endregion

        #region [ Events ]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (JobId.HasValue)
                    _presenter.ListFilteredCallLogs();
                else
                    sgvCallLog.DataBind();
            }
        }

        public void btnFind_Click(object sender, EventArgs e)
        {
            if (!Globals.JobRecord.FilterType.None.Equals(FilterType))
                _presenter.ListFilteredCallLogs();
        }

        public void sgvCallLog_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CS_CallLog callLog = e.Row.DataItem as CS_CallLog;
                CallLogGridViewRow = e.Row;
                e.Row.Cells[1].Text = callLog.CallDate.ToString("MM/dd/yyyy");
                e.Row.Cells[2].Text = callLog.CallDate.ToString("HH:mm");

                Label lbl = e.Row.FindControl("lblDetails") as Label;
                Label lblTool = e.Row.FindControl("lblTool") as Label;
                Panel pnl = e.Row.FindControl("pnlToolTip") as Panel;

                HyperLink linkButtonUpdate = e.Row.FindControl("hlUpdate") as HyperLink;

                if (!callLog.CS_CallType.IsAutomaticProcess)
                    linkButtonUpdate.NavigateUrl =
                        string.Format(
                            "javascript: var newWindow = window.open('/CallEntry.aspx?JobId={0}&CallEntryID={1}', '', 'width=800, height=600, scrollbars=1, resizable=yes');",
                            callLog.JobID, callLog.ID);
                else
                    linkButtonUpdate.NavigateUrl = "javascript: alert('This Call Entry is automatic generated and can not be updated.');";

                if (!string.IsNullOrEmpty(callLog.Note))
                {
                    lbl.Text = StringManipulation.TabulateString(callLog.Note);
                    lblTool.Text = lbl.Text;
                }

                if (lbl.Text != string.Empty)
                {
                    e.Row.Cells[5].Attributes.Add("onmouseenter", "ShowToolTip('" + pnl.ClientID + "');");
                    e.Row.Cells[5].Attributes.Add("onmouseleave", "document.getElementById('" + pnl.ClientID + "').style.display = 'none';");
                }

                if (JobId.HasValue)
                {
                    GridViewRow trCallLog = e.Row;
                    if (null != trCallLog)
                    {
                        trCallLog.Attributes.Add("onmouseover", "jobId = '" + JobId.ToString() + "';");
                        trCallLog.Attributes.Add("oncontextmenu", "showDiv(); return false;");
                    }
                }

                _presenter.ValidateCallLogGridLinkButtonVisibility(callLog);
            }
        }

        protected void Update(object sender, EventArgs e)
        {
            _presenter.ListFilteredCallLogs();
        }

        protected void cbbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisableFilters();

            switch (FilterType)
            {
                case Globals.JobRecord.FilterType.Date:
                    meeFilterValueDate.Enabled = true;
                    mevFilterValueDate.Enabled = true;
                    break;
                case Globals.JobRecord.FilterType.Time:
                    meeFilterValueTime.Enabled = true;
                    mevFilterValueTime.Enabled = true;
                    break;
            }
        }

        #endregion

        #region [ Implemented Inherited Methods ]

        public void DisplayMessage(string message, bool closeWindow)
        {
            ((IJobRecordView)Page).DisplayMessage(message, closeWindow);
        }

        #endregion

        #region [ Methods ]

        public void Refresh()
        {
            _presenter.ListFilteredCallLogs();
        }

        private void DisableFilters()
        {
            meeFilterValueDate.Enabled = false;
            mevFilterValueDate.Enabled = false;

            meeFilterValueTime.Enabled = false;
            mevFilterValueTime.Enabled = false;
        }

        public void ReadyOnlyJobCallLog()
        {
            cbbFilter.Enabled = false;
            btnFind.Enabled = false;
            txtFilterValue.Enabled = false;
        }

        #endregion


        public string txtParentControlClientId { get
        {
            return txtParentUpdate.ClientID;
        } }

        public GridViewRow CallLogGridViewRow
        {
            get;
            set;
        }

        public void SetCallLogGridViewHyperLinkVisibility(bool visible, string controlName)
        {
            GridViewRow row = CallLogGridViewRow;
            if (row != null)
            {
                HyperLink lb = (HyperLink)row.FindControl(controlName);
                lb.Visible = visible;
            }
        }

        public bool GetCallLogGridViewHyperLinkVisibility(string controlName)
        {
            GridViewRow row = CallLogGridViewRow;
            HyperLink lb = (HyperLink)row.FindControl(controlName);
            return lb.Visible;
        }
    }
}
