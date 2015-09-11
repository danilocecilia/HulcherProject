using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core;
using System.Drawing;
using System.Web.UI.HtmlControls;
using Hulcher.OneSource.CustomerService.DataContext.VO;
using System.Text;

namespace Hulcher.OneSource.CustomerService.Web
{
    public partial class AutomatedDPI : System.Web.UI.Page, IAutomatedDPIView
    {
        #region [ Attributes ]

        AutomatedDPIPresenter _presenter;

        private int _totalJobs;
        private double _runningTotal;
        #endregion

        #region [ Overrides ]

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            _presenter = new AutomatedDPIPresenter(this);
        }

        #endregion

        #region [ Events ]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                _presenter.LoadPage();

            _presenter.UpdateGenerationDate();
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            _presenter.ResetTotalCounters();
            _presenter.BindDashboard();
        }

        protected void btnGenerateReport_Click(object sender, EventArgs e)
        {
            StringBuilder script = new StringBuilder();
            string url = " var newWindow = window.open('/DPIReport.aspx?ReportView={0}&ReportDate={1}', '', 'width=1020, height=600, scrollbars=yes, resizable=yes'); ";
            DateTime dayBefore = new DateTime();
            dayBefore = DateTime.Now.AddDays(-1);

            if (FilterValue == Globals.DPI.FilterType.New)
            {
                if (NewJobDateFilter == Globals.DPI.NewJobFilterType.DayBefore.ToString())
                    script.AppendFormat(url, (int)Globals.DPIReport.ReportView.NewJobs, dayBefore.ToShortDateString());
                else
                    script.AppendFormat(url, (int)Globals.DPIReport.ReportView.NewJobs, ProcessDate.Date.ToShortDateString());
            }
            else if (FilterValue == Globals.DPI.FilterType.Continuing)
            {
                script.AppendFormat(url, (int)Globals.DPIReport.ReportView.ContinuingJobs, dayBefore.Date.ToShortDateString());
            }
            else if (FilterValue == Globals.DPI.FilterType.Reprocess)
            {
                script.AppendFormat(url, (int)Globals.DPIReport.ReportView.NewJobs, ProcessDate.Date.ToShortDateString());
            }

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ReportPage", script.ToString(), true);
        }
        

        protected void rptDPIDashboard_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                JobRow = e.Item;

                _presenter.SetJobRowInfo();
                _presenter.GetResourcesRow();
            }
        }

        protected void rptResources_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ResourcesRow = e.Item;

                _presenter.SetResourcesRowInfo();
            }
        }

        #endregion

        #region [ View Implementation ]

        #region [ Common ]

        public void DisplayMessage(string message, bool closeWindow)
        {
            Master.DisplayMessage(message, closeWindow);
        }

        public string GenerationDate
        {
            set
            {
                lblGenerationDateValue.Text = value;
            }
        }

        #endregion

        #region [ Filter ]

        public Globals.DPI.FilterType FilterValue
        {
            get
            {
                return (Globals.DPI.FilterType)int.Parse(cbbProcessingType.SelectedValue);
            }
            set
            {
                cbbProcessingType.SelectedValue = ((int)value).ToString();
            }
        }

        public DateTime ProcessDate
        {
            get
            {
                if (dpReprocessDate.Value.HasValue)
                    return dpReprocessDate.Value.Value;

                return DateTime.Now;
            }
            set
            {
                dpReprocessDate.Value = value;
            }
        }

        public string NewJobDateFilter
        {
            get
            {
                return rbNewJobDataFilter.SelectedValue;
            }
        }

        #endregion

        #region [ DashBoard ]

        #region [ Common ]

        public IList<CS_View_DPIInformation> DashboardDataSource
        {
            get;
            set;
        }

        #endregion

        #region [ Job ]

        public IList<CS_View_DPIInformation> JobRowDatasource
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                if (value.Count != 0)
                {
                    showGrid.Style.Add("display", "block");
                    pnlNoRows.Visible = false;
                    rptDPIDashboard.DataSource = value;
                    rptDPIDashboard.DataBind();
                }
                else
                {
                    pnlNoRows.Visible = true;
                    showGrid.Style.Add("display", "none");
                    rptDPIDashboard.DataSource = value;
                    rptDPIDashboard.DataBind();
                }
            }
        }

        public CS_View_DPIInformation JobRowDataItem
        {
            get
            {
                return JobRow.DataItem as CS_View_DPIInformation;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string JobRowJobNumber
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label label = JobRow.FindControl("lblJobNumber") as Label;
                label.Text = value;
            }
        }

        public string JobRowDivision
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label label = JobRow.FindControl("lblDivision") as Label;
                label.Text = value;
            }
        }

        public string JobRowCustomer
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label label = JobRow.FindControl("lblCustomerResource") as Label;
                label.Text = value;
            }
        }

        public string JobRowLocation
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label label = JobRow.FindControl("lblLocationDescription") as Label;
                label.Text = value;
            }
        }

        public string JobRowJobAction
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label label = JobRow.FindControl("lblJobAction") as Label;
                label.Text = value;
            }
        }

        public int? JobRowCarCountEngines
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label label = JobRow.FindControl("lblCarCountEngines") as Label;

                if (value.HasValue)
                    label.Text = value.Value.ToString();
                else
                    label.Text = "0";
            }
        }

        public int? JobRowCarCountEmpties
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label label = JobRow.FindControl("lblCarCountEmpties") as Label;

                if (value.HasValue)
                    label.Text = value.Value.ToString();
                else
                    label.Text = "0";
            }
        }

        public int? JobRowCarCountLoads
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label label = JobRow.FindControl("lblCarCountLoads") as Label;

                if (value.HasValue)
                    label.Text = value.Value.ToString();
                else
                    label.Text = "0";
            }
        }

        public int JobRowDpiID
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                LinkButton lnkButton = JobRow.FindControl("lnkProcess") as LinkButton;
                lnkButton.OnClientClick = string.Format("window.open('/ProcessDPI.aspx?DpiID={0}&ParentFieldId={1}', '', 'width=1100, height=600, scrollbars=1, resizable=yes');return false", value, btnFilter.UniqueID);
            }
        }

        public Globals.DPI.DpiStatus JobRowDpiStatus
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label label = JobRow.FindControl("lblDPIStatus") as Label;
                switch (value)
                {
                    case Globals.DPI.DpiStatus.DraftSaved:
                        label.Text = string.Format("Draft Saved {0}", JobRowDataItem.DPIProcessStatusDate.ToString("HH:mm"));
                        break;
                    case Globals.DPI.DpiStatus.Approved:
                        if (JobRowDataItem.DPIApprovedById.HasValue)
                            label.Text = string.Format("Approved {0} by {1}", JobRowDataItem.DPIProcessStatusDate.ToString("HH:mm"), JobRowDataItem.DPIApprovedByName);
                        else
                            label.Text = string.Format("Approved {0}", JobRowDataItem.DPIProcessStatusDate.ToString("HH:mm"));
                        break;
                }
            }
        }

        public Globals.DPI.CalculationStatus JobRowStatus
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label label = JobRow.FindControl("lblCalculationStatus") as Label;

                switch (value)
                {
                    case Globals.DPI.CalculationStatus.INSF:
                        label.Text = "INSF";
                        label.ForeColor = Color.Red;
                        label.Font.Bold = true;
                        break;
                    case Globals.DPI.CalculationStatus.Done:
                        label.Text = "Closed";
                        label.Font.Bold = true;
                        break;
                }
            }
        }

        public double JobRowRevenue
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label label = JobRow.FindControl("lblRevenue") as Label;
                label.Text = String.Format("{0:C}", value);
                label.CssClass = " Revenue";
            }
        }

        public bool JobRowHasResources
        {
            get { throw new NotImplementedException(); }
            set
            {
                HtmlGenericControl divExpand = JobRow.FindControl("divExpand") as HtmlGenericControl;
                if (null != divExpand)
                {
                    if (value)
                    {
                        divExpand.Attributes.Add("onclick", "CollapseExpand('" + divExpand.ClientID + "','" + JobRowDataItem.JobID.ToString() + "');");
                        divExpand.Visible = true;
                    }
                    else
                        divExpand.Visible = false;
                }
            }
        }

        #endregion

        #region [ Resource ]

        public IList<CS_View_DPIInformation> ResourcesRowDatasource
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Repeater rptResources = JobRow.FindControl("rptResources") as Repeater;
                rptResources.DataSource = value;
                rptResources.DataBind();
            }
        }

        public CS_View_DPIInformation ResourcesRowDataItem
        {
            get
            {
                return ResourcesRow.DataItem as CS_View_DPIInformation;
            }
            set
            {
                throw new NotImplementedException();
            }

        }

        public int ResourcesRowJobID
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                HtmlTableRow trResource = ResourcesRow.FindControl("trResource") as HtmlTableRow;
                if (null != trResource)
                {
                    trResource.Attributes["class"] = "Child " + value.ToString();
                    trResource.Style.Add(HtmlTextWriterStyle.Display, "none");

                    HtmlGenericControl divExpand = ResourcesRow.FindControl("divExpand") as HtmlGenericControl;
                    if (null != divExpand) divExpand.Visible = false;
                }
            }
        }

        public string ResourcesRowDivision
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label label = ResourcesRow.FindControl("lblDivision") as Label;
                label.Text = value;
            }
        }

        public string ResourcesRowResource
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label label = ResourcesRow.FindControl("lblCustomerResource") as Label;
                label.Text = value;
            }
        }

        public string ResourcesRowDescription
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label label = ResourcesRow.FindControl("lblLocationDescription") as Label;
                label.Text = value;
            }
        }

        public Globals.DPI.CalculationStatus ResourcesRowCalculationStatus
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label label = ResourcesRow.FindControl("lblCalculationStatus") as Label;

                switch (value)
                {
                    case Globals.DPI.CalculationStatus.INSF:
                        label.Text = "INSF";
                        label.ForeColor = Color.Red;
                        label.Font.Bold = true;
                        break;
                    case Globals.DPI.CalculationStatus.Done:
                        label.Text = "Done";
                        label.Font.Bold = true;
                        break;
                }
            }
        }

        public double ResourcesRowRevenue
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                Label label = ResourcesRow.FindControl("lblRevenue") as Label;
                label.Text = String.Format("{0:C}", value);
                label.CssClass = " Revenue";
            }
        }

        #endregion

        #endregion

        #region [ Footer Fields ]

        public double RunningTotal
        {
            get { return _runningTotal; }
            set
            {
                _runningTotal = value;
                lblRunningTotalValue.Text = string.Format("{0:C}", _runningTotal);
            }
        }

        public int TotalJobs
        {
            get
            {
                return _totalJobs;
            }
            set
            {
                _totalJobs = value;
                lblTotalJobsValue.Text = _totalJobs.ToString();
            }
        }

        #endregion

        #endregion

        #region [ Page Properties ]

        #region [ Dashboard ]

        #region [ Job ]

        private RepeaterItem JobRow
        {
            get;
            set;
        }

        #endregion

        #region [ Resource ]

        private RepeaterItem ResourcesRow
        {
            get;
            set;
        }

        #endregion

        #endregion

        #endregion


    }
}
