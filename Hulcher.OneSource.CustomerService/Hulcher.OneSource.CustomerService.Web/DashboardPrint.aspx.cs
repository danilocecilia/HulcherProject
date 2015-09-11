using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.DataContext;
using System.Web.UI.HtmlControls;

namespace Hulcher.OneSource.CustomerService.Web
{
    public partial class DashboardPrint : System.Web.UI.Page, IDashboardPrintView
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of the Presenter class of this screen
        /// </summary>
        DashboardPrintPresenter _presenter;

        #endregion

        #region [ Events ]

        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter = new DashboardPrintPresenter(this);

            int selectedViewPoint = Int32.Parse(Request.QueryString["ViewPoint"]);

            switch (selectedViewPoint)
            {
                case 1:
                    //LOAD GRID ViewPoint0 (Job CallLog View)
                    _presenter.LoadJobCallLog();

                    //Need to load labels based on the ID passed by the parent page
                    if (null != JobStatusFilterValue)
                        _presenter.LoadJobStatusCallLogLabel();
                    if (null != CallTypeFilterValue)
                        _presenter.LoadCallTypeCallLogLabel();
                    if (null != ModifiedByFilterValue)
                        ModifiedByCallLogLabelText = ModifiedByFilterValue;
                    if (null != DivisionFilterValue)
                        _presenter.LoadDivisionCallLogLabel();
                    if (null != PersonNameFilterValue)
                        _presenter.LoadPersonNameLabel();

                    lblStartDateCallLog.Text += Request.QueryString["StartDateFilter"];
                    lblEndDateCallLog.Text += Request.QueryString["EndDateFilter"];

                    viewPoint0.Visible = true;
                    break;
                case 2:
                    //LOAD GRID ViewPoint1 (Job Summary View)
                    _presenter.LoadJobSummary();

                    //Need to load labels based on the ID passed by the parent page
                    if (null != JobStatusFilterValue)
                        _presenter.LoadJobStatusLabel();
                    if (null != JobNumberFilterValue)
                        _presenter.LoadJobNumberLabel();
                    if (null != DivisionFilterValue)
                        _presenter.LoadDivisionLabel();
                    if (null != CustomerFilterValue)
                        _presenter.LoadCustomerLabel();
                    if (null != PersonNameFilterValue)
                        _presenter.LoadPersonNameLabel();

                    lblDateType.Text += ((Globals.Dashboard.DateFilterType)Int32.Parse(Request.QueryString["DateTypeFilter"])).ToString();
                    lblStartDate.Text += Request.QueryString["StartDateFilter"];
                    lblEndDate.Text += Request.QueryString["EndDateFilter"];

                    viewPoint1.Visible = true;
                    break;
                case 3:
                    // Job Summary View - Search Result
                    _presenter.LoadSearchResults();
                    _presenter.LoadSearchFilterPanel();

                    viewPoint2.Visible = true;
                    break;
                default:
                    break;
            }
        }

        #region [ Job Call Log ]

        public void rptCallLogSummaryDivision_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DivisionRepeaterItem = e.Item;

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                _presenter.SetJobCallLogDivisionRowData();
                _presenter.GetCallLogViewJobList();
            }
        }

        public void rptCallLogSummaryJob_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                JobRepeaterItem = e.Item;
                _presenter.SetJobCallLogJobRowData();
                _presenter.GetCallLogViewCallEntryList();
            }
        }

        public void rptCallLogSummaryCallEntry_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CallLogRepeaterItem = e.Item;

                _presenter.SetCallLogViewCallLogRowData();
            }
        }

        #endregion

        #region [ Job Summary ]

        protected void rptJobSummary_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
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

        #endregion

        #endregion

        #region [ Properties ]

        #region [ Sorting ]

        public Globals.Common.Sort.JobSummarySortColumns JobSummarySortColumn
        {
            get
            {
                if (null == Request.QueryString["OrderBy"])
                    return Globals.Common.Sort.JobSummarySortColumns.None;

                string[] orderBy = Request.QueryString["OrderBy"].Split(' ');
                int orderColumn = int.Parse(orderBy[0]);

                return (Globals.Common.Sort.JobSummarySortColumns)orderColumn;
            }
        }

        public Globals.Common.Sort.JobCallLogSortColumns JobCallLogSortColumn
        {
            get
            {
                if (null == Request.QueryString["OrderBy"])
                    return Globals.Common.Sort.JobCallLogSortColumns.None;

                string[] orderBy = Request.QueryString["OrderBy"].Split(' ');
                int orderColumn = int.Parse(orderBy[0]);

                return (Globals.Common.Sort.JobCallLogSortColumns)orderColumn;
            }
        }

        public Globals.Common.Sort.SortDirection SortDirection
        {
            get
            {
                if (null == Request.QueryString["OrderBy"])
                    return Globals.Common.Sort.SortDirection.Ascending;

                string[] orderBy = Request.QueryString["OrderBy"].Split(' ');
                int orderDirection = int.Parse(orderBy[1]);

                return (Globals.Common.Sort.SortDirection)orderDirection;
            }
        }

        #endregion

        #region [ Common ]

        public int? JobStatusFilterValue
        {
            get
            {
                int value;

                if (Int32.TryParse(Request.QueryString["JobStatusFilter"], out value) && value > 0)
                    return value;
                return null;
            }
        }

        public int? DivisionFilterValue
        {
            get
            {
                int value;

                if (Int32.TryParse(Request.QueryString["DivisionFilter"], out value) && value > 0)
                    return value;
                return null;
            }
        }

        public DateTime StartDateFilterValue
        {
            get
            {
                return DateTime.Parse(Request.QueryString["StartDateFilter"]);
            }
        }

        public DateTime EndDateFilterValue
        {
            get
            {
                //Must format End Date for last hour of the selected day (Default is 0000 Hours)
                return DateTime.Parse(string.Format("{0} 23:59:59", Request.QueryString["EndDateFilter"]));
            }
        }

        public string PersonNameFilterValue
        {
            get
            {
                if (string.IsNullOrEmpty(Request.QueryString["PersonName"]))
                    return string.Empty;

                return Request.QueryString["PersonName"];
            }
        }

        #endregion

        #region [ Job Summary ]

        public int? JobNumberFilterValue
        {
            get
            {
                int value;

                if (Int32.TryParse(Request.QueryString["JobNumberFilter"], out value))
                    return value;
                return null;
            }
        }

        public int? CustomerFilterValue
        {
            get
            {
                int value;

                if (Int32.TryParse(Request.QueryString["CustomerFilter"], out value))
                    return value;
                return null;
            }
        }

        public Core.Globals.Dashboard.DateFilterType DateFilterTypeValue
        {
            get { return (Globals.Dashboard.DateFilterType)Int32.Parse(Request.QueryString["DateTypeFilter"]); }
        }

        #endregion

        #region [ Data Source ]

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
                rptJobSummary.DataSource = value;
                rptJobSummary.DataBind();
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

        private RepeaterItem JobSummaryRepeaterItem { get; set; }

        private RepeaterItem JobSummaryResourceRepeaterItem { get; set; }

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
                Label lblLastCallEntry = JobSummaryRepeaterItem.FindControl("lblLastCallEntry") as Label;
                if (null != lblLastCallEntry) lblLastCallEntry.Text = value;
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
                Label lblLastCallEntry = JobSummaryResourceRepeaterItem.FindControl("lblLastCallEntry") as Label;
                if (null != lblLastCallEntry) lblLastCallEntry.Text = value;
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

        public string JobStatusLabelText
        {
            set { lblJobStatus.Text += value; }
        }

        public string JobNumberLabelText
        {
            set { lblJobNumber.Text += value; }
        }

        public string DivisionLabelText
        {
            set { lblDivision.Text += value; }
        }

        public string CustomerLabelText
        {
            set { lblCustomer.Text += value; }
        }

        public string JobStatusCallLogLabelText
        {
            set { lblJobStatusCallLog.Text += value; }
        }

        public string CallTypeCallLogLabelText
        {
            set { lblCallTypeCallLog.Text += value; }
        }

        public string ModifiedByCallLogLabelText
        {
            set { lblModifiedByCallLog.Text += value; }
        }

        public string DivisionCallLogLabelText
        {
            set { lblDivisionCallLog.Text += value; }
        }

        public string PersonNameLabelText
        {
            set
            {
                lblPerson.Text += value;
                lblPersonCallLog.Text += value;
            }
        }

        public int? CallTypeFilterValue
        {
            get
            {
                int value;

                if (Int32.TryParse(Request.QueryString["CallTypeFilter"], out value) && value > 0)
                    return value;
                return null;
            }
        }

        public string ModifiedByFilterValue
        {
            get
            {
                if (!string.IsNullOrEmpty(Request.QueryString["ModifiedByFilter"]))
                    return Request.QueryString["ModifiedByFilter"].ToString();
                else
                    return null;
            }
        }

        public string OrderBy
        {
            get
            {
                if (!string.IsNullOrEmpty(Request.QueryString["OrderBy"]))
                    return Request.QueryString["OrderBy"].ToString();
                else
                    return null;
            }
        }

        public List<DataContext.CS_View_JobCallLog> CalllogViewDataSource
        {
            get
            {
                if (null == ViewState["CalllogViewDataSource"])
                    ViewState["CalllogViewDataSource"] = new List<CS_View_JobCallLog>();

                return ViewState["CalllogViewDataSource"] as List<CS_View_JobCallLog>;
            }
            set
            {
                ViewState["CalllogViewDataSource"] = value;
            }
        }

        public IList<CS_Division> CallLogViewDivisionList
        {
            set
            {
                if (null == value)
                    rptCallLogSummaryDivision.DataBind();
                else
                {
                    rptCallLogSummaryDivision.DataSource = value;
                    rptCallLogSummaryDivision.DataBind();
                    ViewState["CallLogViewDivisionList"] = value;
                }
            }
        }

        private RepeaterItem DivisionRepeaterItem { get; set; }

        private RepeaterItem JobRepeaterItem { get; set; }

        private RepeaterItem CallLogRepeaterItem { get; set; }

        public List<CS_View_JobCallLog> JobRepeaterDataSource
        {
            get
            {
                return new List<CS_View_JobCallLog>();
            }
            set
            {
                Repeater rep = DivisionRepeaterItem.FindControl("rptCallLogSummaryJob") as Repeater;
                rep.DataSource = value;
                rep.DataBind();
            }
        }

        public List<CS_View_JobCallLog> CallLogRepeaterDataSource
        {
            get
            {
                return new List<CS_View_JobCallLog>();
            }
            set
            {
                Repeater rep = JobRepeaterItem.FindControl("rptCallLogSummaryCallEntry") as Repeater;
                rep.DataSource = value;
                rep.DataBind();
            }
        }

        public CS_Division DivisionRepeaterDataItem
        {
            get
            {
                return DivisionRepeaterItem.DataItem as CS_Division;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public CS_View_JobCallLog JobRepeaterDataItem
        {
            get
            {
                return JobRepeaterItem.DataItem as CS_View_JobCallLog;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public CS_View_JobCallLog CallLogRepeaterDataItem
        {
            get
            {
                return CallLogRepeaterItem.DataItem as CS_View_JobCallLog;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string DivisionRowName
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lbl = DivisionRepeaterItem.FindControl("lblDivision") as Label;
                lbl.Text = value;
            }
        }

        public string JobRowJobNumberCustomer
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lbl = JobRepeaterItem.FindControl("lblJobNumberCustomer") as Label;
                lbl.Text = value;
            }
        }

        public int CallLogRowCallId
        {
            get { throw new NotImplementedException(); }
            set
            {
                HtmlInputHidden hid = CallLogRepeaterItem.FindControl("hidCallId") as HtmlInputHidden;
                if (hid != null)
                    hid.Value = value.ToString();
            }
        }

        public string CallLogRowLastModification
        {
            get { throw new NotImplementedException(); }
            set
            {
                HtmlInputHidden hid = CallLogRepeaterItem.FindControl("hidCallLastModification") as HtmlInputHidden;
                if (hid != null)
                    hid.Value = value.ToString();
            }
        }

        public string CallLogRowCallType
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lbl = CallLogRepeaterItem.FindControl("lblCallType") as Label;
                lbl.Text = value;
            }
        }

        public string CallLogRowCalledInBy
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lbl = CallLogRepeaterItem.FindControl("lblCalledInBy") as Label;
                lbl.Text = value;
            }
        }

        public string CallLogRowCallDate
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lbl = CallLogRepeaterItem.FindControl("lblCallDate") as Label;
                lbl.Text = value;
            }
        }

        public string CallLogRowCallTime
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lbl = CallLogRepeaterItem.FindControl("lblCallTime") as Label;
                lbl.Text = value;
            }
        }

        public string CallLogRowModifiedBy
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lbl = CallLogRepeaterItem.FindControl("lblModifiedBy") as Label;
                lbl.Text = value;
            }
        }

        public string CallLogRowDetails
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lbl = CallLogRepeaterItem.FindControl("lblDetails") as Label;
                lbl.Text = value;
            }
        }

        public bool ShiftTransferLogFilter
        {
            get
            {
                if (!string.IsNullOrEmpty(Request.QueryString["ShiftTransferLogFilter"]))
                    return bool.Parse(Request.QueryString["ShiftTransferLogFilter"].ToString());
                else
                    return false;
            }
        }

        public bool GeneralLogFilter
        {
            get
            {
                if (!string.IsNullOrEmpty(Request.QueryString["GeneralLogFilter"]))
                    return bool.Parse(Request.QueryString["GeneralLogFilter"].ToString());
                else
                    return false;
            }
        }

        #endregion

        #region [ Methods ]

        public void DisplayMessage(string message, bool closeWindow)
        {
            //Won't implement
        }

        #endregion

        private RepeaterItem JobSummarySearchRepeaterItem { get; set; }

        private RepeaterItem JobSummarySearchResourceRepeaterItem { get; set; }

        protected void rptJobSummarySearch_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                JobSummarySearchRepeaterItem = e.Item;
                _presenter.FillJobSummarySearchRow();
            }
        }

        public void rptJobSummarySearchResources_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                JobSummarySearchResourceRepeaterItem = e.Item;
                _presenter.FillJobSummarySearchResourceRow();
            }
        }

        public List<CS_View_JobSummary> JobSummarySearchDataSource
        {
            get
            {
                if (null == ViewState["JobSummarySearchDataSource"])
                    ViewState["JobSummarySearchDataSource"] = new List<CS_View_JobSummary>();
                return ViewState["JobSummarySearchDataSource"] as List<CS_View_JobSummary>;
            }
            set { ViewState["JobSummarySearchDataSource"] = value; }
        }

        public List<CS_View_JobSummary> JobSummarySearchRepeaterDataSource
        {
            get { return new List<CS_View_JobSummary>(); }
            set
            {
                rptJobSummarySearch.DataSource = value;
                rptJobSummarySearch.DataBind();
            }
        }

        public List<CS_View_JobSummary> JobSummarySearchResourceRepeaterDataSource
        {
            get { return new List<CS_View_JobSummary>(); }
            set
            {
                Repeater rptJobSummarySearchResources = JobSummarySearchRepeaterItem.FindControl("rptJobSummarySearchResources") as Repeater;
                if (null != rptJobSummarySearchResources)
                {
                    rptJobSummarySearchResources.DataSource = value;
                    rptJobSummarySearchResources.DataBind();
                }
            }
        }

        public CS_View_JobSummary JobSummarySearchRepeaterDataItem
        {
            get { return JobSummarySearchRepeaterItem.DataItem as CS_View_JobSummary; }
            set { throw new NotImplementedException(); }
        }

        public CS_View_JobSummary JobSummarySearchResourceRepeaterDataItem
        {
            get { return JobSummarySearchResourceRepeaterItem.DataItem as CS_View_JobSummary; }
            set { throw new NotImplementedException(); }
        }

        public string JobSummarySearchRowDivision
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblDivision = JobSummarySearchRepeaterItem.FindControl("lblDivision") as Label;
                if (null != lblDivision) lblDivision.Text = value;
            }
        }

        public string JobSummarySearchRowJobNumber
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblJobNumber = JobSummarySearchRepeaterItem.FindControl("lblJobNumber") as Label;
                if (null != lblJobNumber) lblJobNumber.Text = value;
            }
        }

        public string JobSummarySearchRowCustomer
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblCustomerResource = JobSummarySearchRepeaterItem.FindControl("lblCustomerResource") as Label;
                if (null != lblCustomerResource) lblCustomerResource.Text = value;
            }
        }

        public string JobSummarySearchRowStatus
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblStatus = JobSummarySearchRepeaterItem.FindControl("lblStatus") as Label;
                if (null != lblStatus) lblStatus.Text = value;
            }
        }

        public string JobSummarySearchRowLocation
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblLocation = JobSummarySearchRepeaterItem.FindControl("lblLocation") as Label;
                if (null != lblLocation) lblLocation.Text = value;
            }
        }

        public string JobSummarySearchRowProjectManager
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblProjectManager = JobSummarySearchRepeaterItem.FindControl("lblProjectManager") as Label;
                if (null != lblProjectManager) lblProjectManager.Text = value;
            }
        }

        public string JobSummarySearchRowModifiedBy
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblModifiedBy = JobSummarySearchRepeaterItem.FindControl("lblModifiedBy") as Label;
                if (null != lblModifiedBy) lblModifiedBy.Text = value;
            }
        }

        public DateTime JobSummarySearchRowLastModification
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblLastModification = JobSummarySearchRepeaterItem.FindControl("lblLastModification") as Label;
                if (null != lblLastModification) lblLastModification.Text = value.ToString("MM/dd/yyyy");
            }
        }

        public DateTime JobSummarySearchRowCallDate
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblCallDate = JobSummarySearchRepeaterItem.FindControl("lblCallDate") as Label;
                if (null != lblCallDate) lblCallDate.Text = value.ToString("MM/dd/yyyy");
            }
        }

        public DateTime? JobSummarySearchRowPresetDate
        {
            get { throw new NotImplementedException(); }
            set
            {
                if (value.HasValue)
                {
                    Label lblPreset = JobSummarySearchRepeaterItem.FindControl("lblPreset") as Label;
                    if (null != lblPreset) lblPreset.Text = value.Value.ToString("MM/dd/yyyy");
                }
            }
        }

        public string JobSummarySearchRowLastCallEntry
        {
            get { throw new NotImplementedException(); }
            set
            {
                HyperLink hlLastCallEntry = JobSummarySearchRepeaterItem.FindControl("hlLastCallEntry") as HyperLink;
                if (null != hlLastCallEntry) hlLastCallEntry.Text = value;
            }
        }

        public DateTime? JobSummarySearchRowLastCallDate
        {
            get { throw new NotImplementedException(); }
            set
            {
                if (value.HasValue)
                {
                    Label lblLastCallDate = JobSummarySearchRepeaterItem.FindControl("lblLastCallDate") as Label;
                    if (null != lblLastCallDate) lblLastCallDate.Text = value.Value.ToString("MM/dd/yyyy HH:mm");
                }
            }
        }

        public string JobSummarySearchResourceRowDivision
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblDivision = JobSummarySearchResourceRepeaterItem.FindControl("lblDivision") as Label;
                if (null != lblDivision) lblDivision.Text = value;
            }
        }

        public string JobSummarySearchResourceRowResource
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblCustomerResource = JobSummarySearchResourceRepeaterItem.FindControl("lblCustomerResource") as Label;
                if (null != lblCustomerResource) lblCustomerResource.Text = value;
            }
        }

        public string JobSummarySearchResourceRowLocation
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblLocation = JobSummarySearchResourceRepeaterItem.FindControl("lblLocation") as Label;
                if (null != lblLocation) lblLocation.Text = value;
            }
        }

        public string JobSummarySearchResourceRowModifiedBy
        {
            get { throw new NotImplementedException(); }
            set
            {
                Label lblModifiedBy = JobSummarySearchResourceRepeaterItem.FindControl("lblModifiedBy") as Label;
                if (null != lblModifiedBy) lblModifiedBy.Text = value;
            }
        }

        public DateTime? JobSummarySearchResourceRowLastModification
        {
            get { throw new NotImplementedException(); }
            set
            {
                if (value.HasValue)
                {
                    Label lblLastModification = JobSummarySearchResourceRepeaterItem.FindControl("lblLastModification") as Label;
                    if (null != lblLastModification) lblLastModification.Text = value.Value.ToString("MM/dd/yyyy");
                }
            }
        }

        public DateTime? JobSummarySearchResourceRowCallDate
        {
            get { throw new NotImplementedException(); }
            set
            {
                if (value.HasValue)
                {
                    Label lblCallDate = JobSummarySearchResourceRepeaterItem.FindControl("lblCallDate") as Label;
                    if (null != lblCallDate) lblCallDate.Text = value.Value.ToString("MM/dd/yyyy");
                }
            }
        }

        public string JobSummarySearchResourceRowLastCallEntry
        {
            get { throw new NotImplementedException(); }
            set
            {
                HyperLink hlLastCallEntry = JobSummarySearchResourceRepeaterItem.FindControl("hlLastCallEntry") as HyperLink;
                if (null != hlLastCallEntry) hlLastCallEntry.Text = value;
            }
        }

        public DateTime? JobSummarySearchResourceRowLastCallDate
        {
            get { throw new NotImplementedException(); }
            set
            {
                if (value.HasValue)
                {
                    Label lblLastCallDate = JobSummarySearchResourceRepeaterItem.FindControl("lblLastCallDate") as Label;
                    if (null != lblLastCallDate) lblLastCallDate.Text = value.Value.ToString("MM/dd/yyyy HH:mm");
                }
            }
        }

        public string SearchContactInfoLabel
        {
            set { lblSearchContactInfo.Text = value; }
        }

        public string SearchJobInfoLabel
        {
            set { lblSearchJobInfo.Text = value; }
        }

        public string SearchLocationInfoLabel
        {
            set { lblSearchLocationInfo.Text = value; }
        }

        public string SearchJobDescriptionLabel
        {
            set { lblSearchJobDescription.Text = value; }
        }

        public string SearchEquipmentTypeLabel
        {
            set { lblSearchEquipmentType.Text = value; }
        }

        public string SearchResourceLabel
        {
            set { lblSearchResource.Text = value; }
        }

        public string SearchDateRangeLabel
        {
            set { lblSearchDateRange.Text = value; }
        }

        public string PageGuid
        {
            get
            {
                if (null != Page.Request.QueryString["PageGuid"])
                {
                    ViewState["PageGuid"] = Page.Request.QueryString["PageGuid"].ToString();
                }
                else
                    ViewState["PageGuid"] = string.Empty;

                return ViewState["PageGuid"].ToString();
            }
        }

        public string ContactFilter
        {
            get
            {
                if (null == ViewState["ContactFilter"])
                {
                    if (null != Session[string.Format("ContactFilter_{0}", PageGuid)])
                    {
                        ViewState["ContactFilter"] = Session[string.Format("ContactFilter_{0}", PageGuid)];
                        Session[string.Format("ContactFilter_{0}", PageGuid)] = null;
                    }
                    else
                        ViewState["ContactFilter"] = "None";
                }

                return ViewState["ContactFilter"].ToString();
            }
        }

        public string JobFilter
        {
            get
            {
                if (null == ViewState["JobFilter"])
                {
                    if (null != Session[string.Format("JobFilter_{0}", PageGuid)])
                    {
                        ViewState["JobFilter"] = Session[string.Format("JobFilter_{0}", PageGuid)];
                        Session[string.Format("JobFilter_{0}", PageGuid)] = null;
                    }
                    else
                        ViewState["JobFilter"] = "None";

                }

                return ViewState["JobFilter"].ToString();
            }
        }

        public string LocationFilter
        {
            get
            {
                if (null == ViewState["LocationFilter"])
                {
                    if (null != Session[string.Format("LocationFilter_{0}", PageGuid)])
                    {
                        ViewState["LocationFilter"] = Session[string.Format("LocationFilter_{0}", PageGuid)];
                        Session[string.Format("LocationFilter_{0}", PageGuid)] = null;
                    }
                    else
                        ViewState["LocationFilter"] = "None";

                }

                return ViewState["LocationFilter"].ToString();
            }
        }

        public string JobDescriptionFilter
        {
            get
            {
                if (null == ViewState["JobDescriptionFilter"])
                {
                    if (null != Session[string.Format("JobDescriptionFilter_{0}", PageGuid)])
                    {
                        ViewState["JobDescriptionFilter"] = Session[string.Format("JobDescriptionFilter_{0}", PageGuid)];
                        Session[string.Format("JobDescriptionFilter_{0}", PageGuid)] = null;
                    }
                    else
                        ViewState["JobDescriptionFilter"] = "None";
                }

                return ViewState["JobDescriptionFilter"].ToString();
            }
        }

        public string EquipmentTypeFilter
        {
            get
            {
                if (null == ViewState["EquipmentTypeFilter"])
                {
                    if (null != Session[string.Format("EquipmentTypeFilter_{0}", PageGuid)])
                    {
                        ViewState["EquipmentTypeFilter"] = Session[string.Format("EquipmentTypeFilter_{0}", PageGuid)];
                        Session[string.Format("EquipmentTypeFilter_{0}", PageGuid)] = null;
                    }
                    else
                        ViewState["EquipmentTypeFilter"] = "None";
                }

                return ViewState["EquipmentTypeFilter"].ToString();
            }
        }

        public string ResourceFilter
        {
            get
            {
                if (null == ViewState["ResourceFilter"])
                {
                    if (null != Session[string.Format("ResourceFilter_{0}", PageGuid)])
                    {
                        ViewState["ResourceFilter"] = Session[string.Format("ResourceFilter_{0}", PageGuid)];
                        Session[string.Format("ResourceFilter_{0}", PageGuid)] = null;
                    }
                    else
                        ViewState["ResourceFilter"] = "None";
                }

                return ViewState["ResourceFilter"].ToString();
            }
        }

        public string ContactFilterValue
        {
            get
            {
                if (null == ViewState["ContactFilterValue"])
                {
                    if (null != Session[string.Format("ContactFilterValue_{0}", PageGuid)])
                    {
                        ViewState["ContactFilterValue"] = Session[string.Format("ContactFilterValue_{0}", PageGuid)];
                        Session[string.Format("ContactFilterValue_{0}", PageGuid)] = null;
                    }
                    else
                        ViewState["ContactFilterValue"] = string.Empty;
                }

                return ViewState["ContactFilterValue"].ToString();
            }
        }

        public string JobFilterValue
        {
            get
            {
                if (null == ViewState["JobFilterValue"])
                {
                    if (null != Session[string.Format("JobFilterValue_{0}", PageGuid)])
                    {
                        ViewState["JobFilterValue"] = Session[string.Format("JobFilterValue_{0}", PageGuid)];
                        Session[string.Format("JobFilterValue_{0}", PageGuid)] = null;
                    }
                    else
                        ViewState["JobFilterValue"] = string.Empty;
                }

                return ViewState["JobFilterValue"].ToString();
            }
        }

        public string LocationFilterValue
        {
            get
            {
                if (null == ViewState["LocationFilterValue"])
                {
                    if (null != Session[string.Format("LocationFilterValue_{0}", PageGuid)])
                    {
                        ViewState["LocationFilterValue"] = Session[string.Format("LocationFilterValue_{0}", PageGuid)];
                        Session[string.Format("LocationFilterValue_{0}", PageGuid)] = null;
                    }
                    else
                        ViewState["LocationFilterValue"] = string.Empty;
                }

                return ViewState["LocationFilterValue"].ToString();
            }
        }

        public string JobDescriptionFilterValue
        {
            get
            {
                if (null == ViewState["JobDescriptionFilterValue"])
                {
                    if (null != Session[string.Format("JobDescriptionFilterValue_{0}", PageGuid)])
                    {
                        ViewState["JobDescriptionFilterValue"] = Session[string.Format("JobDescriptionFilterValue_{0}", PageGuid)];
                        Session[string.Format("JobDescriptionFilterValue_{0}", PageGuid)] = null;
                    }
                    else
                        ViewState["JobDescriptionFilterValue"] = string.Empty;
                }

                return ViewState["JobDescriptionFilterValue"].ToString();
            }
        }

        public string EquipmentTypeFilterValue
        {
            get
            {
                if (null == ViewState["EquipmentTypeFilterValue"])
                {
                    if (null != Session[string.Format("EquipmentTypeFilterValue_{0}", PageGuid)])
                    {
                        ViewState["EquipmentTypeFilterValue"] = Session[string.Format("EquipmentTypeFilterValue_{0}", PageGuid)];
                        Session[string.Format("EquipmentTypeFilterValue_{0}", PageGuid)] = null;
                    }
                    else
                        ViewState["EquipmentTypeFilterValue"] = string.Empty;
                }

                return ViewState["EquipmentTypeFilterValue"].ToString();
            }
        }

        public string ResourceFilterValue
        {
            get
            {
                if (null == ViewState["ResourceFilterValue"])
                {
                    if (null != Session[string.Format("ResourceFilterValue_{0}", PageGuid)])
                    {
                        ViewState["ResourceFilterValue"] = Session[string.Format("ResourceFilterValue_{0}", PageGuid)];
                        Session[string.Format("ResourceFilterValue_{0}", PageGuid)] = null;
                    }
                    else
                        ViewState["ResourceFilterValue"] = string.Empty;
                }

                return ViewState["ResourceFilterValue"].ToString();
            }
        }

        public DateTime DateRangeBeginValue
        {
            get
            {
                if (null == ViewState["DateRangeBeginValue"])
                {
                    if (null != Session[string.Format("DateRangeBeginValue_{0}", PageGuid)])
                    {
                        ViewState["DateRangeBeginValue"] = Session[string.Format("DateRangeBeginValue_{0}", PageGuid)];
                        Session[string.Format("DateRangeBeginValue_{0}", PageGuid)] = null;
                    }
                    else
                        ViewState["DateRangeBeginValue"] = DateTime.MinValue;
                }

                return (DateTime)ViewState["DateRangeBeginValue"];
            }
        }

        public DateTime DateRangeEndValue
        {
            get
            {
                if (null == ViewState["DateRangeEndValue"])
                {
                    if (null != Session[string.Format("DateRangeEndValue_{0}", PageGuid)])
                    {
                        ViewState["DateRangeEndValue"] = Session[string.Format("DateRangeEndValue_{0}", PageGuid)];
                        Session[string.Format("DateRangeEndValue_{0}", PageGuid)] = null;
                    }
                    else
                        ViewState["DateRangeEndValue"] = DateTime.MinValue;
                }

                return (DateTime)ViewState["DateRangeEndValue"];
            }
        }

        public IList<int> JobIdList
        {
            get
            {
                if (null == ViewState["JobIdList"])
                {
                    if (null != Session[string.Format("JobIdList_{0}", PageGuid)])
                    {
                        ViewState["JobIdList"] = Session[string.Format("JobIdList_{0}", PageGuid)];
                        Session[string.Format("JobIdList_{0}", PageGuid)] = null;
                    }
                    else
                        ViewState["JobIdList"] = new List<int>();
                }

                return (IList<int>)ViewState["JobIdList"];
            }
            set { throw new NotImplementedException(); }
        }
    }
}