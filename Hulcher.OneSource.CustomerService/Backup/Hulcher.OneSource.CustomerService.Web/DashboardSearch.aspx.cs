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
    public partial class DashboardSearch : System.Web.UI.Page, IDashboardSearchView
    {
        #region [ Attributes and Properties ]

        private DashboardSearchPresenter _presenter;

        private RepeaterItem JobSummaryRepeaterItem { get; set; }

        private RepeaterItem JobSummaryResourceRepeaterItem { get; set; }

        #endregion

        #region [ Overrides ]

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _presenter = new DashboardSearchPresenter(this);
        }

        #endregion

        #region [ Events ]

        #region [ Common ]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _presenter.LoadPage();
            }
        }

        protected void btnFakeSort_Click(object sender, EventArgs e)
        {
            _presenter.LoadData();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            _presenter.ExportToCSV();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            OpenPrintWindow();
        }

        #endregion

        #region [ Job Summary ]

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

        #endregion

        #endregion

        #region [ Methods ]

        public void DisplayMessage(string message, bool closeWindow)
        {
            Master.DisplayMessage(message, closeWindow);
        }

        public void ChangeHeaderCssClass(RepeaterItem item)
        {
            string thName = string.Empty;

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

        public void OpenPrintWindow()
        {
            Session[string.Format("JobIdList_{0}", PageGuid)] = JobIdList;

            Session[string.Format("ContactFilter_{0}", PageGuid)] = ContactFilter;
            Session[string.Format("JobFilter_{0}", PageGuid)] = JobFilter;
            Session[string.Format("LocationFilter_{0}", PageGuid)] = LocationFilter;
            Session[string.Format("JobDescriptionFilter_{0}", PageGuid)] = JobDescriptionFilter;
            Session[string.Format("EquipmentTypeFilter_{0}", PageGuid)] = EquipmentTypeFilter;
            Session[string.Format("ResourceFilter_{0}", PageGuid)] = ResourceFilter;

            Session[string.Format("ContactFilterValue_{0}", PageGuid)] = ContactFilterValue;
            Session[string.Format("JobFilterValue_{0}", PageGuid)] = JobFilterValue;
            Session[string.Format("LocationFilterValue_{0}", PageGuid)] = LocationFilterValue;
            Session[string.Format("JobDescriptionFilterValue_{0}", PageGuid)] = JobDescriptionFilterValue;
            Session[string.Format("EquipmentTypeFilterValue_{0}", PageGuid)] = EquipmentTypeFilterValue;
            Session[string.Format("ResourceFilteValuer_{0}", PageGuid)] = ResourceFilterValue;

            Session[string.Format("DateRangeBeginValue_{0}", PageGuid)] = DateRangeBeginValue;
            Session[string.Format("DateRangeEndValue_{0}", PageGuid)] = DateRangeEndValue;

            string link = string.Format("window.open('/DashboardPrint.aspx?ViewPoint=3&PageGuid={0}", PageGuid);

            if (OrderBy.Length > 0)
            {
                link = string.Format("{0}&OrderBy={1} {2}", link, OrderBy[0], OrderBy[1]);
            }

            link = string.Format("{0}', '', 'width=900, height=800, scrollbars=1, resizable=yes');", link);

            ScriptManager.RegisterClientScriptBlock(
                this,
                this.GetType(),
                "OpenPrintWindow",
                link,
                true);
        }

        #endregion

        #region [ Properties ]

        #region [ Common ]

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
                attachment = string.Format(attachment, "JobSummary", currentDate);
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
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SendEmailPage", string.Format("window.open('/Email.aspx?CallLogListID={0}', '', 'width=800, height=600, scrollbars=1, resizable=yes');", hfCallId.Value), true);
                    }
                }
            }
        }

        #endregion

        #region [ Job Summary ]

        #region [ Filters ]

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

        public string FilterPanel
        {
            set { lblFilterValues.Text = value; }
        }

        #endregion

        #region [ Data Sources ]

        public List<CS_View_JobSummary> JobSummaryDataSource
        {
            get
            {
                if (null == ViewState["JobSummaryDataSource"])
                    ViewState["JobSummaryDataSource"] = new List<CS_View_JobSummary>();
                return ViewState["JobSummaryDataSource"] as List<CS_View_JobSummary>;
            }
            set { ViewState["JobSummaryDataSource"] = value; }
        }

        public List<CS_View_JobSummary> JobSummaryRepeaterDataSource
        {
            get { return new List<CS_View_JobSummary>(); }
            set
            {
                rptJobSummary.DataSource = value;
                rptJobSummary.DataBind();
            }
        }

        public List<CS_View_JobSummary> JobSummaryResourceRepeaterDataSource
        {
            get { return new List<CS_View_JobSummary>(); }
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

        public CS_View_JobSummary JobSummaryRepeaterDataItem
        {
            get { return JobSummaryRepeaterItem.DataItem as CS_View_JobSummary; }
            set { throw new NotImplementedException(); }
        }

        public CS_View_JobSummary JobSummaryResourceRepeaterDataItem
        {
            get { return JobSummaryResourceRepeaterItem.DataItem as CS_View_JobSummary; }
            set { throw new NotImplementedException(); }
        }

        #endregion

        #region [ Sorting ]

        public Globals.Common.Sort.JobSummarySortColumns JobSummarySortColumn
        {
            get
            {
                if (OrderBy.Length == 0)
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
                if (null != hlOpenJob) hlOpenJob.NavigateUrl = string.Format("javascript: var newWindow = window.open('/JobRecord.aspx?JobId={0}', '', 'width=800, height=600, scrollbars=1, resizable=yes');", value.ToString());

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

        #endregion
    }
}
