using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Core.Utils;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Web
{
    public partial class Dashboard : System.Web.UI.Page, IDashboardView
    {
        #region [ Attributes and Properties ]

        private DashboardPresenter _presenter;

        private RepeaterItem JobSummaryRepeaterItem { get; set; }

        private RepeaterItem JobSummaryResourceRepeaterItem { get; set; }

        private RepeaterItem DivisionRepeaterItem { get; set; }

        private RepeaterItem JobRepeaterItem { get; set; }

        private RepeaterItem CallLogRepeaterItem { get; set; }

        #endregion

        #region [ Overrides ]

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _presenter = new DashboardPresenter(this);
        }

        #endregion

        #region [ Events ]

        #region [ Common ]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _presenter.LoadPage();
                ConfigurePrintButtonScript();
            }
            else
                _presenter.LoadPagePostback();
        }

        protected void rbSummary_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeViews();
            Find();
        }

        protected void tmrUpdate_Tick(object sender, EventArgs e)
        {
            string currentSelection = hfSelectedCallLog.Value;
            Find();
            hfSelectedCallLog.Value = currentSelection;
        }

        protected void btnFakeSort_Click(object sender, EventArgs e)
        {
            Find();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            _presenter.ExportToCSV();
        }

        protected void btnEmailManual_Click(object sender, EventArgs e)
        {
            SendEmail = true;
        }

        public void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn != null && !string.IsNullOrEmpty(btn.CommandArgument))
            {
                CallLogIdToDelete = int.Parse(btn.CommandArgument);
                _presenter.DeleteCallLog();
                _presenter.Find();
            }
        }

        #endregion

        #region [ Job Summary ]

        protected void btnFilterJobSummary_OnClick(object sender, EventArgs e)
        {
            hfOrderBy.Value = string.Empty;
            Find();
        }

        protected void rptJobSummary_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
                ChangeHeaderCssClass(e.Item);
            else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                JobSummaryRepeaterItem = e.Item;
                _presenter.FillJobSummaryRow();
            }
        }

        public void rptJobSummaryResources_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                JobSummaryResourceRepeaterItem = e.Item;
                _presenter.FillJobSummaryResourceRow();
            }
        }

        protected void btnResetFieldsJobSummary_OnClick(object sender, EventArgs e)
        {
            _presenter.DefaultDateJobSummary();
            _presenter.ClearFieldsResetJobSummary();
            ChangeViews();
        }
        #endregion

        #region [ Job Call Log ]

        protected void btnFilterCallLog_Click(object sender, EventArgs e)
        {
            hfOrderBy.Value = string.Empty;
            hfCallId.Value = string.Empty;
            hfJobId.Value = string.Empty;
            Find();
        }

        public void rptCallLogSummaryDivision_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DivisionRepeaterItem = e.Item;
            if (e.Item.ItemType == ListItemType.Header)
            {
                _presenter.CheckDashboardVisibilityDivisionHeader();
                ChangeHeaderCssClass(e.Item);
            }
            else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                _presenter.CheckDashboardVisibilityDivision();
                _presenter.FillJobCallLogDivisionRow();
            }
        }

        public void rptCallLogSummaryJob_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                JobRepeaterItem = e.Item;
                _presenter.CheckDashboardVisibilityJob();
                _presenter.FillJobCallLogJobRow();
            }
        }

        public void rptCallLogSummaryCallEntry_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CallLogRepeaterItem = e.Item;
                _presenter.CheckDashboardVisibilityCallEntry();
                _presenter.FillJobCallLogCallEntryRow();
                MakeCallLogRowClickable();
                GetJobIdCallLogIdCheckBox();
            }
        }

        protected void btnResetFieldsCallLog_Click(object sender, EventArgs e)
        {
            _presenter.ClearFieldsResetCallLog();
            ChangeViews();
        }
        #endregion

        #endregion

        #region [ Methods ]

        public void ClearFieldsResetCallLog()
        {
            actJobStatusCallLog.SelectedValue = ((int)Globals.JobRecord.JobStatus.Active).ToString();
            actCallTypeCallLog.SetValue = "0";
            actCallTypeCallLog.SelectedText = string.Empty;
            txtPlaceHolderModifiedBy.Text = string.Empty;
            chkGeneralLog.Checked = false;
            chkShiftTransferLog.Checked = false;
            actDivisionCallLogView.SelectedText = string.Empty;
            actDivisionCallLogView.SelectedValue = "0";
            PersonNameCallLog = "";

            dpStartDateCallEntry.Value = DateTime.Now.AddDays(-4);
            dpEndDateCallEntry.Value = DateTime.Now;

            hfOrderBy.Value = string.Empty;
            Find();
        }

        public void ClearFieldsResetJobSummary()
        {
            actJobStatusJobSummary.SelectedValue = ((int)Globals.JobRecord.JobStatus.Active).ToString();

            actJobNumberJobSumary.SelectedText = string.Empty;
            actJobNumberJobSumary.SelectedValue = "0";
            actDivisionJobSummary.SelectedText = string.Empty;
            actDivisionJobSummary.SelectedValue = "0";
            actCustomerJobSummary.SelectedText = string.Empty;
            actCustomerJobSummary.SelectedValue = "0";
            cbbDateTypeJobSummary.SelectedValue = "4";
            PersonNameValueJobSummary = "";

            dpBeginDateJobSummary.Value = DateTime.Now.AddDays(-4);
            dpEndDateJobSummary.Value = DateTime.Now;

            hfOrderBy.Value = string.Empty;
            Find();
        }

        public void DisplayMessage(string message, bool closeWindow)
        {
            Master.DisplayMessage(message, closeWindow);
        }

        private void Find()
        {
            // Resets the status of the Manual email button
            btnEmailManual.Enabled = false;

            Page.Validate(vsJobCallLog.ValidationGroup);
            ConfigurePrintButtonScript();
            if (IsValid)
                _presenter.Find();
        }

        private void ChangeViews()
        {
            if (rbSummary.SelectedValue == ((int)Globals.Dashboard.ViewType.JobSummaryView).ToString())
            {
                hfOrderBy.Value = string.Empty;
                pnlJobSummaryView.Visible = true;
                pnlCallLogView.Visible = false;
                pnlGridCallLogSummary.Visible = false;
                divButtonsCallLog.Visible = false;
                pnlGridJobSummary.Visible = true;
            }
            else
            {
                hfOrderBy.Value = string.Empty;
                pnlCallLogView.Visible = true;
                pnlJobSummaryView.Visible = false;
                pnlGridCallLogSummary.Visible = true;
                divButtonsCallLog.Visible = true;
                pnlGridJobSummary.Visible = false;
            }
        }

        public void ConfigurePrintButtonScript()
        {
            try
            {
                WebUtil util = new WebUtil();
                string script = string.Empty;
                if (this.DashBoardViewType == Globals.Dashboard.ViewType.JobCallLogView)
                    script = util.BuildNewWindowClientScript("/DashboardPrint.aspx", util.BuildQueryStringToPrintCallLogViewInDashBoardView(this), string.Empty, 800, 600, true, true, true);
                else if (this.DashBoardViewType == Globals.Dashboard.ViewType.JobSummaryView)
                    script = util.BuildNewWindowClientScript("/DashboardPrint.aspx", util.BuildQueryStringToPrintJobSummaryInDashBoardView(this), string.Empty, 800, 600, true, true, true);
                btnPrint.OnClientClick = script;
            }
            catch (Exception)
            {
                DisplayMessage("Error while generating Print Button Script", false);
            }

        }

        public void ChangeHeaderCssClass(RepeaterItem item)
        {
            string thName = string.Empty;
            if (DashBoardViewType == Globals.Dashboard.ViewType.JobSummaryView)
            {
                switch (JobSummarySortColumn)
                {
                    case Globals.Common.Sort.JobSummarySortColumns.Division:
                        thName = "thDivision";
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.JobNumber:
                        thName = "thJobNumber";
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.CustomerResource:
                        thName = "thCustomerResource";
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.JobStatus:
                        thName = "thJobStatus";
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.Location:
                        thName = "thLocation";
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.ProjectManager:
                        thName = "thProjectManager";
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.ModifiedBy:
                        thName = "thModifiedBy";
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.LastModification:
                        thName = "thLastModification";
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.InitialCallDate:
                        thName = "thInitialCallDate";
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.PresetDate:
                        thName = "thPresetDate";
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.LastCallType:
                        thName = "thLastCallType";
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.LastCallDate:
                        thName = "thLastCallDate";
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.None:
                    default:
                        thName = string.Empty;
                        break;
                }
            }
            else if (DashBoardViewType == Globals.Dashboard.ViewType.JobCallLogView)
            {
                switch (JobCallLogSortColumn)
                {
                    case Globals.Common.Sort.JobCallLogSortColumns.Division:
                        thName = "thDivision";
                        break;
                    case Globals.Common.Sort.JobCallLogSortColumns.JobNumber:
                        thName = "thJobNumber";
                        break;
                    case Globals.Common.Sort.JobCallLogSortColumns.Customer:
                        thName = "thCustomer";
                        break;
                    case Globals.Common.Sort.JobCallLogSortColumns.CallType:
                        thName = "thCallType";
                        break;
                    case Globals.Common.Sort.JobCallLogSortColumns.CalledInBy:
                        thName = "thCalledInBy";
                        break;
                    case Globals.Common.Sort.JobCallLogSortColumns.CallDate:
                        thName = "thCallDate";
                        break;
                    case Globals.Common.Sort.JobCallLogSortColumns.CallTime:
                        thName = "thCallTime";
                        break;
                    case Globals.Common.Sort.JobCallLogSortColumns.ModifiedBy:
                        thName = "thModifiedBy";
                        break;
                    case Globals.Common.Sort.JobCallLogSortColumns.Details:
                        thName = "thDetails";
                        break;
                    case Globals.Common.Sort.JobCallLogSortColumns.None:
                    default:
                        thName = string.Empty;
                        break;
                }
            }

            string[] items = OrderBy;
            if (!string.IsNullOrEmpty(thName))
            {
                HtmlTableCell control = item.FindControl(thName) as HtmlTableCell;
                if (null != control)
                {
                    if (items[1] == "1")
                        control.Attributes.Add("class", control.Attributes["class"] + " Ascending");
                    else
                        control.Attributes.Add("class", control.Attributes["class"] + " Descending");
                }
            }
        }

        public void MakeCallLogRowClickable()
        {
            HtmlTableRow row = CallLogRepeaterItem.FindControl("trCallEntry") as HtmlTableRow;
            if (null != row)
            {
                row.Attributes.Add("onclick", string.Format("javascript:rowClick('{0}');", row.ClientID));
                row.Attributes.Add("onmouseover", row.Attributes["onmouseover"] + "javascript:this.style.cursor = 'hand';");
                row.Attributes.Add("onmouseout", "javascript:this.style.cursor = '';");
            }
        }

        public void GetJobIdCallLogIdCheckBox()
        {
            CheckBox checkBox = CallLogRepeaterItem.FindControl("chkEmail") as CheckBox;
            CS_View_JobCallLog csViewJobCallLog = (CS_View_JobCallLog)CallLogRepeaterItem.DataItem;

            if (null != checkBox && null != csViewJobCallLog)
            {
                checkBox.Attributes.Add("onclick", "onChecked(this,'" + csViewJobCallLog.JobId + "','" + csViewJobCallLog.CallId + "');");
                if (hfCallId.Value != string.Empty)
                {
                    string[] lstCallIds = hfCallId.Value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < lstCallIds.Length; i++)
                    {
                        if (csViewJobCallLog.CallId == Convert.ToInt32(lstCallIds[i]))
                        {
                            btnEmailManual.Enabled = true;
                            checkBox.Checked = true;
                            break;
                        }
                    }
                }
            }
        }

        #endregion

        #region [ Properties ]

        #region [ Common ]

        public string UserName { get { return this.Master.Username; } }

        public string Domain { get { return this.Master.Domain; } }

        public bool? UserHasDeleteCallLogPermission
        {
            get
            {
                if (null == ViewState["UserHasDeleteCallLogPermission"])
                    return null;
                else
                    return (bool?)ViewState["UserHasDeleteCallLogPermission"];
            }
            set
            {
                ViewState["UserHasDeleteCallLogPermission"] = value;
            }
        }

        public Globals.Dashboard.ViewType DashBoardViewType
        {
            get { return (Globals.Dashboard.ViewType)int.Parse(rbSummary.SelectedValue); }
            set
            {
                rbSummary.SelectedValue = Convert.ToInt32(value).ToString();
                //rbSummary_SelectedIndexChanged(rbSummary, new EventArgs());
                ChangeViews();
            }
        }

        public int DashboardRefreshRate
        {
            get { return tmrUpdate.Interval; }
            set { tmrUpdate.Interval = value * 1000; }
        }

        public string[] OrderBy
        {
            get { return hfOrderBy.Value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries); }
            set
            {
                for (int i = 0; i < value.Length; i++)
                {
                    if (i == 0)
                    {
                        hfOrderBy.Value = value[i];
                    }
                    else
                    {
                        hfOrderBy.Value = hfOrderBy.Value + value[i];
                    }
                }
            }
        }

        public Globals.Common.Sort.SortDirection SortDirection
        {
            get
            {
                if (OrderBy.Length == 0)
                    return Globals.Common.Sort.SortDirection.Ascending;

                return (Globals.Common.Sort.SortDirection)Int32.Parse(OrderBy[1]);
            }
        }

        public string CSVFile
        {
            set
            {
                string attachment = "attachment; filename={0}_{1}.csv";
                string currentDate = DateTime.Now.ToString("MMddyyyy");
                if (DashBoardViewType == Globals.Dashboard.ViewType.JobSummaryView)
                    attachment = string.Format(attachment, "JobSummary", currentDate);
                else
                    attachment = string.Format(attachment, "JobCallLog", currentDate);
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.AddHeader("content-disposition", attachment);
                HttpContext.Current.Response.ContentType = "text/csv";
                HttpContext.Current.Response.AddHeader("Pragma", "public");
                HttpContext.Current.Response.Write(value);
                HttpContext.Current.Response.End();
            }
        }

        public bool SendEmail
        {
            set
            {
                if (value)
                {
                    if (hfCallId.Value != string.Empty)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "SendEmailPage", string.Format("window.open('/Email.aspx?CallLogListID={0}', '', 'width=800, height=600, scrollbars=1, resizable=yes'); $('.checkEmail input:checked').attr('checked', false); ", hfCallId.Value), true);
                        hfCallId.Value = "";
                        hfJobId.Value = "";
                    }
                }
            }
        }

        #endregion

        #region [ Job Summary ]

        #region [ Filters ]

        public int? JobStatusFilterValue
        {
            get
            {
                if (actJobStatusJobSummary.SelectedValue != "0")
                    return Convert.ToInt32(actJobStatusJobSummary.SelectedValue);
                return null;
            }
            set
            {
                if (value.HasValue)
                    actJobStatusJobSummary.SelectedValue = value.ToString();
            }
        }

        public int? JobNumberFilterValue
        {
            get
            {
                if (actJobNumberJobSumary.SelectedValue != "0")
                    return Convert.ToInt32(actJobNumberJobSumary.SelectedValue);
                return null;
            }
            set
            {
                if (value.HasValue)
                    actJobNumberJobSumary.SelectedValue = value.ToString();
            }
        }

        public int? DivisionFilterValue
        {
            get
            {
                if (actDivisionJobSummary.SelectedValue != "0")
                    return Convert.ToInt32(actDivisionJobSummary.SelectedValue);
                return null;
            }
            set
            {
                if (value.HasValue)
                    actDivisionJobSummary.SelectedValue = value.ToString();
            }
        }

        public int? CustomerFilterValue
        {
            get
            {
                if (actCustomerJobSummary.SelectedValue != "0")
                    return Convert.ToInt32(actCustomerJobSummary.SelectedValue);
                return null;
            }
            set
            {
                if (value.HasValue)
                    actCustomerJobSummary.SelectedValue = value.ToString();
            }
        }

        public Globals.Dashboard.DateFilterType DateFilterTypeValue
        {
            get { return (Globals.Dashboard.DateFilterType)int.Parse(cbbDateTypeJobSummary.SelectedValue); }
            set
            {
                int typeValue = Convert.ToInt32(value);
                cbbDateTypeJobSummary.SelectedValue = typeValue.ToString();
            }
        }

        public DateTime BeginDateJobSummaryValue
        {
            get
            {
                if (dpBeginDateJobSummary.Value.HasValue)
                    return dpBeginDateJobSummary.Value.Value;
                else
                    return DateTime.MinValue;
            }
            set { dpBeginDateJobSummary.Value = value; }
        }

        public DateTime EndDateJobSummaryValue
        {
            get
            {
                if (dpEndDateJobSummary.Value.HasValue)
                    return dpEndDateJobSummary.Value.Value.AddDays(1).AddSeconds(-1);
                else
                    return DateTime.Now;
            }
            set { dpEndDateJobSummary.Value = value; }
        }

        public string PersonNameValueJobSummary
        {
            get
            {
                return txtJobPerson.Text;
            }
            set
            {
                txtJobPerson.Text = value;
            }
        }

        #endregion

        #region [ Data Sources ]

        public List<CS_SP_GetJobSummary_Result> JobSummaryDataSource
        {
            get
            {
                if (null == ViewState["JobSummaryDataSource"])
                    ViewState["JobSummaryDataSource"] = new List<CS_SP_GetJobSummary_Result>();
                return ViewState["JobSummaryDataSource"] as List<CS_SP_GetJobSummary_Result>;
            }
            set { ViewState["JobSummaryDataSource"] = value; }
        }

        public List<CS_SP_GetJobSummary_Result> JobSummaryRepeaterDataSource
        {
            get { return new List<CS_SP_GetJobSummary_Result>(); }
            set
            {
                ShowHideControls(value.Count.Equals(0));
                rptJobSummary.DataSource = value;
                rptJobSummary.DataBind();
            }
        }

        public void ShowHideControls(bool show)
        {
            if (show)
            {
                pnlNoRows.Visible = true;

                divButtons.Style.Add("display", "none");
                divJobSum.Style.Add("display", "none");

            }
            else
            {
                pnlNoRows.Visible = false;

                divButtons.Style.Add("display", "block");
                divJobSum.Style.Add("display", "block");
            }
        }

        public List<CS_SP_GetJobSummary_Result> JobSummaryResourceRepeaterDataSource
        {
            get { return new List<CS_SP_GetJobSummary_Result>(); }
            set
            {
                Repeater rptJobSummaryResources = JobSummaryRepeaterItem.FindControl("rptJobSummaryResources") as Repeater;
                if (null != rptJobSummaryResources)
                {
                    rptJobSummaryResources.DataSource = value;
                    rptJobSummaryResources.DataBind();
                }
            }
        }

        #endregion

        #region [ Data Items ]

        public CS_SP_GetJobSummary_Result JobSummaryRepeaterDataItem
        {
            get { return JobSummaryRepeaterItem.DataItem as CS_SP_GetJobSummary_Result; }
            set { throw new NotImplementedException(); }
        }

        public CS_SP_GetJobSummary_Result JobSummaryResourceRepeaterDataItem
        {
            get { return JobSummaryResourceRepeaterItem.DataItem as CS_SP_GetJobSummary_Result; }
            set { throw new NotImplementedException(); }
        }

        #endregion

        #region [ Sorting ]

        public Globals.Common.Sort.JobSummarySortColumns JobSummarySortColumn
        {
            get
            {
                if (OrderBy.Length == 0 || DashBoardViewType == Globals.Dashboard.ViewType.JobCallLogView)
                    return Globals.Common.Sort.JobSummarySortColumns.None;

                return (Globals.Common.Sort.JobSummarySortColumns)int.Parse(OrderBy[0]);
            }
        }

        #endregion

        #region [ Row Attributes - Job ]

        public string JobSummaryRowDivision
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblDivision = JobSummaryRepeaterItem.FindControl("lblDivision") as Label;
                if (null != lblDivision) lblDivision.Text = value;
            }
        }

        public int JobSummaryRowJobId
        {
            get { throw new NotImplementedException(); }
            set
            {
                HiddenField hfIdJob = JobSummaryRepeaterItem.FindControl("hfIdJob") as HiddenField;
                if (null != hfIdJob) hfIdJob.Value = value.ToString();

                HyperLink hlOpenJob = JobSummaryRepeaterItem.FindControl("hlOpenJob") as HyperLink;
                if (null != hlOpenJob) hlOpenJob.NavigateUrl = string.Format("javascript: var newWindow = window.open('/JobRecord.aspx?JobId={0}', '', 'width=1040, height=600, scrollbars=1, resizable=yes');", value.ToString());

                HtmlGenericControl divExpand = JobSummaryRepeaterItem.FindControl("divExpand") as HtmlGenericControl;
                if (null != divExpand)
                {
                    string[] expandedJobs = hfExpandedJobs.Value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (expandedJobs.Contains(value.ToString()))
                        divExpand.Attributes["class"] = "Collapse";
                }

                HtmlTableRow trJob = JobSummaryRepeaterItem.FindControl("trJob") as HtmlTableRow;
                if (null != trJob)
                {
                    trJob.Attributes.Add("onmouseover", "jobId = '" + value.ToString() + "';");
                    trJob.Attributes.Add("oncontextmenu", "showDiv(); return false;");
                }
            }
        }

        public int? JobSummaryRowHasResources
        {
            get { throw new NotImplementedException(); }
            set
            {
                HtmlGenericControl divExpand = JobSummaryRepeaterItem.FindControl("divExpand") as HtmlGenericControl;
                if (null != divExpand)
                {
                    if (value.HasValue && value.Value > 0)
                    {
                        divExpand.Attributes.Add("onclick", "CollapseExpand('" + divExpand.ClientID + "','" + JobSummaryRepeaterDataItem.JobID.ToString() + "');");
                        divExpand.Visible = true;
                    }
                    else
                        divExpand.Visible = false;
                }
            }
        }

        public string JobSummaryRowJobNumber
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblJobNumber = JobSummaryRepeaterItem.FindControl("lblJobNumber") as Label;
                if (null != lblJobNumber) lblJobNumber.Text = value;
            }
        }

        public string JobSummaryRowCustomer
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblCustomerResource = JobSummaryRepeaterItem.FindControl("lblCustomerResource") as Label;
                if (null != lblCustomerResource) lblCustomerResource.Text = value;
            }
        }

        public string JobSummaryRowStatus
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblStatus = JobSummaryRepeaterItem.FindControl("lblStatus") as Label;
                if (null != lblStatus) lblStatus.Text = value;
            }
        }

        public string JobSummaryRowLocation
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblLocation = JobSummaryRepeaterItem.FindControl("lblLocation") as Label;
                if (null != lblLocation) lblLocation.Text = value;
            }
        }

        public string JobSummaryRowProjectManager
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblProjectManager = JobSummaryRepeaterItem.FindControl("lblProjectManager") as Label;
                if (null != lblProjectManager) lblProjectManager.Text = value;
            }
        }

        public string JobSummaryRowModifiedBy
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblModifiedBy = JobSummaryRepeaterItem.FindControl("lblModifiedBy") as Label;
                if (null != lblModifiedBy) lblModifiedBy.Text = value;
            }
        }

        public DateTime JobSummaryRowLastModification
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblLastModification = JobSummaryRepeaterItem.FindControl("lblLastModification") as Label;
                if (null != lblLastModification) lblLastModification.Text = value.ToString("MM/dd/yyyy");
            }
        }

        public DateTime JobSummaryRowCallDate
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblCallDate = JobSummaryRepeaterItem.FindControl("lblCallDate") as Label;
                if (null != lblCallDate) lblCallDate.Text = value.ToString("MM/dd/yyyy");
            }
        }

        public DateTime? JobSummaryRowPresetDate
        {
            get { throw new NotImplementedException(); }
            set
            {
                if (value.HasValue)
                {
                    Label lblPreset = JobSummaryRepeaterItem.FindControl("lblPreset") as Label;
                    if (null != lblPreset) lblPreset.Text = value.Value.ToString("MM/dd/yyyy");
                }
            }
        }

        public string JobSummaryRowLastCallEntry
        {
            get { throw new NotImplementedException(); }
            set
            {
                HyperLink hlLastCallEntry = JobSummaryRepeaterItem.FindControl("hlLastCallEntry") as HyperLink;
                if (null != hlLastCallEntry) hlLastCallEntry.Text = value;
            }
        }

        public int? JobSummaryRowLastCallEntryId
        {
            get { throw new NotImplementedException(); }
            set
            {
                if (value.HasValue)
                {
                    HyperLink hlLastCallEntry = JobSummaryRepeaterItem.FindControl("hlLastCallEntry") as HyperLink;
                    if (null != hlLastCallEntry) hlLastCallEntry.NavigateUrl = string.Format("javascript: var newWindow = window.open('/CallEntry.aspx?JobId={0}&CallEntryID={1}', '', 'width=800, height=600, scrollbars=1, resizable=yes');", JobSummaryRepeaterDataItem.JobID.ToString(), value.Value.ToString());
                }
            }
        }

        public bool JobSummaryRowLastCallEntryIsAutomaticProcess
        {
            get { throw new NotImplementedException(); }
            set
            {
                    HyperLink hlLastCallEntry = JobSummaryRepeaterItem.FindControl("hlLastCallEntry") as HyperLink;
                    if (null != hlLastCallEntry && value) hlLastCallEntry.NavigateUrl = "javascript: alert('This Call Entry is automatic generated and can not be updated.');";
            }
        }

        public DateTime? JobSummaryRowLastCallDate
        {
            get { throw new NotImplementedException(); }
            set
            {
                if (value.HasValue)
                {
                    Label lblLastCallDate = JobSummaryRepeaterItem.FindControl("lblLastCallDate") as Label;
                    if (null != lblLastCallDate) lblLastCallDate.Text = value.Value.ToString("MM/dd/yyyy HH:mm");
                }
            }
        }

        #endregion

        #region [ Row Attributes - Resource ]

        public string JobSummaryResourceRowDivision
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblDivision = JobSummaryResourceRepeaterItem.FindControl("lblDivision") as Label;
                if (null != lblDivision) lblDivision.Text = value;
            }
        }

        public int JobSummaryResourceRowJobId
        {
            get { throw new NotImplementedException(); }
            set
            {
                HiddenField hfIdJob = JobSummaryResourceRepeaterItem.FindControl("hfIdJob") as HiddenField;
                if (null != hfIdJob) hfIdJob.Value = value.ToString();

                string[] expandedJobs = hfExpandedJobs.Value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                HtmlTableRow trResource = JobSummaryResourceRepeaterItem.FindControl("trResource") as HtmlTableRow;
                if (null != trResource)
                {
                    if (!JobSummaryResourceRepeaterDataItem.IsReserve.Value)
                        trResource.Attributes["class"] = "Child " + value.ToString();
                    else
                        trResource.Attributes["class"] = "Reserved " + value.ToString();

                    if (!expandedJobs.Contains(value.ToString()))
                        trResource.Style.Add(HtmlTextWriterStyle.Display, "none");
                    trResource.Attributes.Add("oncontextmenu", "return false;");

                    HtmlGenericControl divExpand = JobSummaryResourceRepeaterItem.FindControl("divExpand") as HtmlGenericControl;
                    if (null != divExpand) divExpand.Visible = false;
                }
            }
        }

        public string JobSummaryResourceRowResource
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblCustomerResource = JobSummaryResourceRepeaterItem.FindControl("lblCustomerResource") as Label;
                if (null != lblCustomerResource) lblCustomerResource.Text = value;
            }
        }

        public string JobSummaryResourceRowLocation
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblLocation = JobSummaryResourceRepeaterItem.FindControl("lblLocation") as Label;
                if (null != lblLocation) lblLocation.Text = value;
            }
        }

        public string JobSummaryResourceRowModifiedBy
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblModifiedBy = JobSummaryResourceRepeaterItem.FindControl("lblModifiedBy") as Label;
                if (null != lblModifiedBy) lblModifiedBy.Text = value;
            }
        }

        public DateTime? JobSummaryResourceRowLastModification
        {
            get { throw new NotImplementedException(); }
            set
            {
                if (value.HasValue)
                {
                    Label lblLastModification = JobSummaryResourceRepeaterItem.FindControl("lblLastModification") as Label;
                    if (null != lblLastModification) lblLastModification.Text = value.Value.ToString("MM/dd/yyyy");
                }
            }
        }

        public DateTime? JobSummaryResourceRowCallDate
        {
            get { throw new NotImplementedException(); }
            set
            {
                if (value.HasValue)
                {
                    Label lblCallDate = JobSummaryResourceRepeaterItem.FindControl("lblCallDate") as Label;
                    if (null != lblCallDate) lblCallDate.Text = value.Value.ToString("MM/dd/yyyy");
                }
            }
        }

        public string JobSummaryResourceRowLastCallEntry
        {
            get { throw new NotImplementedException(); }
            set
            {
                HyperLink hlLastCallEntry = JobSummaryResourceRepeaterItem.FindControl("hlLastCallEntry") as HyperLink;
                if (null != hlLastCallEntry) hlLastCallEntry.Text = value;
            }
        }

        public int? JobSummaryResourceRowLastCallEntryId
        {
            get { throw new NotImplementedException(); }
            set
            {
                if (value.HasValue)
                {
                    HyperLink hlLastCallEntry = JobSummaryResourceRepeaterItem.FindControl("hlLastCallEntry") as HyperLink;
                    if (null != hlLastCallEntry) hlLastCallEntry.NavigateUrl = string.Format("javascript: var newWindow = window.open('/CallEntry.aspx?JobId={0}&CallEntryID={1}', '', 'width=800, height=600, scrollbars=1, resizable=yes');", JobSummaryRepeaterDataItem.JobID.ToString(), value.Value.ToString());
                }
            }
        }

        public bool JobSummaryResourceRowLastCallEntryIsAutomaticProcess
        {
            get { throw new NotImplementedException(); }
            set
            {
                    HyperLink hlLastCallEntry = JobSummaryResourceRepeaterItem.FindControl("hlLastCallEntry") as HyperLink;
                    if (null != hlLastCallEntry && value) hlLastCallEntry.NavigateUrl = "javascript: alert('This Call Entry is automatic generated and can not be updated.');";
            }
        }

        public DateTime? JobSummaryResourceRowLastCallDate
        {
            get { throw new NotImplementedException(); }
            set
            {
                if (value.HasValue)
                {
                    Label lblLastCallDate = JobSummaryResourceRepeaterItem.FindControl("lblLastCallDate") as Label;
                    if (null != lblLastCallDate) lblLastCallDate.Text = value.Value.ToString("MM/dd/yyyy HH:mm");
                }
            }
        }

        #endregion

        #endregion

        #region [ Job Call Log ]

        #region [ Filters ]

        public int? JobStatusCallLogViewFilter
        {
            get
            {
                if (actJobStatusCallLog.SelectedValue != "0")
                    return Int32.Parse(actJobStatusCallLog.SelectedValue);
                else
                    return null;
            }
            set { actJobStatusCallLog.SelectedValue = value.ToString(); }
        }

        public int? CallTypeCallLogViewFilter
        {
            get
            {
                if (actCallTypeCallLog.SelectedValue != "0")
                    return Int32.Parse(actCallTypeCallLog.SelectedValue);
                else
                    return null;
            }
            set { actCallTypeCallLog.SelectedValue = value.ToString(); }
        }

        public string ModifiedByCallLogViewFilter
        {
            get { return txtPlaceHolderModifiedBy.Text; }
            set { txtPlaceHolderModifiedBy.Text = value; }
        }

        public bool ShiftTransferLogCallLogViewFilter
        {
            get { return chkShiftTransferLog.Checked; }
            set { chkShiftTransferLog.Checked = value; }
        }

        public bool GeneralLogCallLogViewFilter
        {
            get { return chkGeneralLog.Checked; }
            set { chkGeneralLog.Checked = value; }
        }

        public int? DivisionValueCallLogViewFilter
        {
            get
            {
                if (actDivisionCallLogView.SelectedValue != "0")
                    return Int32.Parse(actDivisionCallLogView.SelectedValue);
                else
                    return null;
            }
            set { actDivisionCallLogView.SelectedValue = value.ToString(); }
        }

        public DateTime BeginDateCallLogViewFilter
        {
            get
            {
                if (dpStartDateCallEntry.Value.HasValue)
                    return dpStartDateCallEntry.Value.Value;
                else
                    return DateTime.MinValue;
            }
            set { dpStartDateCallEntry.Value = value; }
        }

        public DateTime EndDateCallLogViewFilter
        {
            get
            {
                if (dpEndDateCallEntry.Value.HasValue)
                    return dpEndDateCallEntry.Value.Value.AddDays(1).AddSeconds(-1);
                else
                    return DateTime.Now;
            }
            set { dpEndDateCallEntry.Value = value; }
        }

        public string PersonNameCallLog
        {
            get
            {
                return txtCallLogPerson.Text;
            }
            set
            {
                txtCallLogPerson.Text = value;
            }
        }

        #endregion

        #region [ Data Sources ]

        public List<CS_View_JobCallLog> CalllogViewDataSource
        {
            get
            {
                if (null == ViewState["CalllogViewDataSource"])
                    ViewState["CalllogViewDataSource"] = new List<CS_View_JobCallLog>();
                return ViewState["CalllogViewDataSource"] as List<CS_View_JobCallLog>;
            }
            set { ViewState["CalllogViewDataSource"] = value; }
        }

        public IList<CS_Division> CallLogViewDivisionList
        {
            get
            {
                if (null == ViewState["CallLogViewDivisionList"])
                    return new List<CS_Division>();
                return ViewState["CallLogViewDivisionList"] as List<CS_Division>;
            }
            set
            {
                if (null == value)
                    rptCallLogSummaryDivision.DataBind();
                else
                {
                    rptCallLogSummaryDivision.DataSource = value;
                    rptCallLogSummaryDivision.DataBind();
                    ViewState["CallLogViewDivisionList"] = value;

                    hfDivisionCount.Value = DivisionCount.ToString();
                    hfJobCount.Value = JobCount.ToString();
                    hfCallLogCount.Value = CallLogCount.ToString();
                }
            }
        }

        public List<CS_View_JobCallLog> JobRepeaterDataSource
        {
            get { return new List<CS_View_JobCallLog>(); }
            set
            {
                Repeater rep = DivisionRepeaterItem.FindControl("rptCallLogSummaryJob") as Repeater;
                rep.DataSource = value;
                rep.DataBind();
            }
        }

        public List<CS_View_JobCallLog> CallLogRepeaterDataSource
        {
            get { return new List<CS_View_JobCallLog>(); }
            set
            {
                Repeater rep = JobRepeaterItem.FindControl("rptCallLogSummaryCallEntry") as Repeater;
                rep.DataSource = value;
                rep.DataBind();
            }
        }

        #endregion

        #region [ Data Items ]

        public CS_Division DivisionRepeaterDataItem
        {
            get { return DivisionRepeaterItem.DataItem as CS_Division; }
            set { throw new NotImplementedException(); }
        }

        public CS_View_JobCallLog JobRepeaterDataItem
        {
            get { return JobRepeaterItem.DataItem as CS_View_JobCallLog; }
            set { throw new NotImplementedException(); }
        }

        public CS_View_JobCallLog CallLogRepeaterDataItem
        {
            get { return CallLogRepeaterItem.DataItem as CS_View_JobCallLog; }
            set { throw new NotImplementedException(); }
        }

        #endregion

        #region [ Sorting ]

        public Globals.Common.Sort.JobCallLogSortColumns JobCallLogSortColumn
        {
            get
            {
                if (OrderBy.Length == 0 || DashBoardViewType == Globals.Dashboard.ViewType.JobSummaryView)
                    return Globals.Common.Sort.JobCallLogSortColumns.None;

                return (Globals.Common.Sort.JobCallLogSortColumns)int.Parse(OrderBy[0]);
            }
        }

        #endregion

        #region [ Counters ]

        public int DivisionCount { get; set; }

        public int JobCount { get; set; }

        public int CallLogCount { get; set; }

        #endregion

        #region [ Row Attributes - Division ]

        public string DivisionRowName
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lbl = DivisionRepeaterItem.FindControl("lblDivision") as Label;
                lbl.Text = value;
            }
        }

        public bool CallLogRepeaterDivisionColumnVisibility
        {
            set
            {
                ChangeCallLogRepeaterColumnVisibility(this.DivisionRepeaterItem, "divisionColumn", value);
                ChangeRowVisibility(this.DivisionRepeaterItem, "trDivision", value);
            }
        }

        public bool CallLogRepeaterDivisionHeaderVisibility
        {
            set
            {
                ChangeCallLogRepeaterColumnVisibility(this.DivisionRepeaterItem, "thDivision", value);
                ChangeCallLogRepeaterColumnVisibility(this.DivisionRepeaterItem, "thJobNumber", value);
                ChangeCallLogRepeaterColumnVisibility(this.DivisionRepeaterItem, "thCustomer", value);
            }
        }

        #endregion

        #region [ Row Attributes - Job ]

        public string JobRowJobNumberCustomer
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lbl = JobRepeaterItem.FindControl("lblJobNumberCustomer") as Label;
                lbl.Text = value;
            }
        }

        public bool CallLogRepeaterJobColumnVisibility
        {
            set
            {
                ChangeCallLogRepeaterColumnVisibility(this.JobRepeaterItem, "colCustomer", value);
                ChangeCallLogRepeaterColumnVisibility(this.JobRepeaterItem, "colJobNumberCustomer", value);

                ChangeRowVisibility(this.JobRepeaterItem, "trJob", value);
            }
        }

        #endregion

        #region [ Row Attributes - Call Log ]

        public int CallLogRowCallId
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                HtmlInputHidden hidCallId = CallLogRepeaterItem.FindControl("hidCallId") as HtmlInputHidden;
                if (null != hidCallId) hidCallId.Value = value.ToString();

                HyperLink hlUpdate = CallLogRepeaterItem.FindControl("hlUpdate") as HyperLink;
                if (null != hlUpdate && !CallLogRepeaterDataItem.IsAutomaticProcess.Value) hlUpdate.NavigateUrl = string.Format("javascript: var newWindow = window.open('/CallEntry.aspx?JobId={0}&CallEntryId={1}', '', 'width=870, height=600, scrollbars=1, resizable=yes');", CallLogRepeaterDataItem.JobId, value);
            }
        }

        public int CallLogRowJobId
        {
            get { throw new NotImplementedException(); }
            set
            {
                HtmlTableRow trCallEntry = CallLogRepeaterItem.FindControl("trCallEntry") as HtmlTableRow;
                if (null != trCallEntry)
                {
                    trCallEntry.Attributes.Add("onmouseover", "jobId = '" + value.ToString() + "';");
                    trCallEntry.Attributes.Add("oncontextmenu", "showDiv(); return false;");
                }
            }
        }

        public string CallLogRowLastModification
        {
            get { throw new NotImplementedException(); }
            set
            {
                HtmlInputHidden hidCallLastModification = CallLogRepeaterItem.FindControl("hidCallLastModification") as HtmlInputHidden;
                if (null != hidCallLastModification) hidCallLastModification.Value = value.ToString();
            }
        }

        public string CallLogRowCallType
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblCallType = CallLogRepeaterItem.FindControl("lblCallType") as Label;
                if (null != lblCallType) lblCallType.Text = value;
            }
        }

        public string CallLogRowCalledInBy
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblCalledInBy = CallLogRepeaterItem.FindControl("lblCalledInBy") as Label;
                if (null != lblCalledInBy) lblCalledInBy.Text = value;
            }
        }

        public string CallLogRowCallDate
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblCallDate = CallLogRepeaterItem.FindControl("lblCallDate") as Label;
                if (null != lblCallDate) lblCallDate.Text = value;
            }
        }

        public string CallLogRowCallTime
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblCallTime = CallLogRepeaterItem.FindControl("lblCallTime") as Label;
                if (null != lblCallTime) lblCallTime.Text = value;
            }
        }

        public string CallLogRowModifiedBy
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblModifiedBy = CallLogRepeaterItem.FindControl("lblModifiedBy") as Label;
                if (null != lblModifiedBy) lblModifiedBy.Text = value;
            }
        }

        public bool CallLogRowAutomaticProcess
        {
            get { throw new NotImplementedException(); }
            set
            {
                HyperLink hlUpdate = CallLogRepeaterItem.FindControl("hlUpdate") as HyperLink;
                if (null != hlUpdate) hlUpdate.Visible = !value;
            }
        }

        public string CallLogRowDetails
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblDetails = CallLogRepeaterItem.FindControl("lblDetails") as Label;
                if (null != lblDetails) lblDetails.Text = value;

                Label lblTool = CallLogRepeaterItem.FindControl("lblTool") as Label;
                Label lblToolTipEmail = CallLogRepeaterItem.FindControl("lblToolTipEmail") as Label;

                HyperLink hlEmail = CallLogRepeaterItem.FindControl("hlEmail") as HyperLink;

                HtmlTableCell pnlCell = CallLogRepeaterItem.FindControl("pnlCell") as HtmlTableCell;
                Panel pnlToolTip = CallLogRepeaterItem.FindControl("pnlToolTip") as Panel;
                Panel pnlToolTipEmail = CallLogRepeaterItem.FindControl("pnlToolTipEmail") as Panel;
                if (null != pnlCell && null != pnlToolTip)
                {
                    if (null != hlEmail)
                    {
                        hlEmail.NavigateUrl = "#";
                        hlEmail.Attributes.Add("onmouseenter", string.Format("getMousePosition();CallEmailWebService({0}, document.getElementById('" + pnlToolTipEmail.ClientID + "'), document.getElementById('" + lblToolTipEmail.ClientID + "'));", CallLogRepeaterDataItem.CallId));
                        hlEmail.Attributes.Add("onmouseleave", "document.getElementById('" + pnlToolTipEmail.ClientID + "').style.display = 'none'; document.getElementById('" + lblToolTipEmail.ClientID + "').innerHTML = '';");
                    }

                    pnlCell.Attributes.Add("onmouseenter", "ShowToolTip(document.getElementById('" + pnlToolTip.ClientID + "'), document.getElementById('" + lblDetails.ClientID + "'), document.getElementById('" + lblTool.ClientID + "'));");
                    pnlCell.Attributes.Add("onmouseleave", "document.getElementById('" + pnlToolTip.ClientID + "').style.display = 'none'; document.getElementById('" + lblTool.ClientID + "').innerHTML = '';");
                }
            }
        }

        public string CallLogSelectedRowId
        {
            set
            {
                hfSelectedCallLog.Value = value;
            }
        }

        public bool CallLogRepeaterCallEntryColumnVisibility
        {
            set
            {
                ChangeCallLogRepeaterColumnVisibility(this.CallLogRepeaterItem, "colDivision", value);
                ChangeCallLogRepeaterColumnVisibility(this.CallLogRepeaterItem, "colJob", value);
                ChangeCallLogRepeaterColumnVisibility(this.CallLogRepeaterItem, "colCust", value);
            }
        }

        public bool EnableDeleteLink 
        {
            set
            {
                Button btnDelete = CallLogRepeaterItem.FindControl("btnDelete") as Button;
                HtmlInputHidden hid = (HtmlInputHidden)CallLogRepeaterItem.FindControl("hidCallId");
                if (btnDelete != null)
                {
                    btnDelete.Enabled = value;                    
                }
                if (hid != null)
                {
                    btnDelete.CommandArgument = hid.Value;
                }
            }
        }

        public int CallLogIdToDelete
        {
            get;
            set;
        }

        #endregion

        #region [ Read/Unread Functionality ]

        public Dictionary<int, DateTime> ReadItems
        {
            get
            {
                if (null == ViewState["ReadItems"])
                    ViewState["ReadItems"] = new Dictionary<int, DateTime>();
                return (Dictionary<int, DateTime>)ViewState["ReadItems"];
            }
            set { ViewState["ReadItems"] = value; }
        }

        public string NewlyReadItems
        {
            get { return hfReadCallLogs.Value; }
            set { hfReadCallLogs.Value = value; }
        }

        public string CallLogRowStyle
        {
            set
            {
                HtmlTableRow row = CallLogRepeaterItem.FindControl("trCallEntry") as HtmlTableRow;
                if (null != row)
                    row.Attributes["class"] += string.Format(" {0} Division{1} Job{2} CallLogCount{3} jobCallLogTable CallEntry",
                        value, DivisionCount, JobCount, CallLogCount.ToString());
            }
        }

        #endregion

        private void ChangeCallLogRepeaterColumnVisibility(RepeaterItem item, string colName, bool visibility)
        {
            HtmlTableCell cell = (HtmlTableCell)item.FindControl(colName);
            if (null != cell) cell.Visible = visibility;
        }

        private void ChangeRowVisibility(RepeaterItem item, string trName, bool visibility)
        {
            HtmlTableRow row = (HtmlTableRow)item.FindControl(trName);
            if (null != row) row.Visible = visibility;
        }

        #endregion

        #region [ Quick Search ]

        public int QuickSearchJobId
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #endregion
    }
}
