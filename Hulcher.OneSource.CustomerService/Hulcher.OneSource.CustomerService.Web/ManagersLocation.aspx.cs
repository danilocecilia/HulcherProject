using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Web
{
    public partial class ManagersLocation : System.Web.UI.Page, IManagersLocationView
    {
        #region [ Attributes ]

        ManagersLocationPresenter _presenter;

        #endregion

        #region [ Overrides ]

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _presenter = new ManagersLocationPresenter(this);
        }

        #endregion

        #region [ Methods ]

        #endregion

        #region [ Events ]
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                _presenter.PageLoad();
        }

        protected void sgvManagers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ManagersLocationRow = e.Row;

                ManagersLocationDataItem = e.Row.DataItem as CS_View_ManagersLocation;

                _presenter.BindManagersLocation();
            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            _presenter.FilterManagersLocation();
        }
        #endregion

        #region [ Properties ]

        #region [ GridView Properties ]
        public CS_View_ManagersLocation ManagersLocationDataItem { get; set; }

        public GridViewRow ManagersLocationRow { get; set; }
        #endregion

        public string Name
        {
            get
            {
                return txtName.Text;
            }
        }

        public int? CallTypeID
        {
            get
            {
                if (string.IsNullOrEmpty(actCallType.SelectedValue) || actCallType.SelectedValue.Equals("0"))
                    return null;

                return int.Parse(actCallType.SelectedValue);
            }
        }

        public int? JobID
        {
            get
            {
                if (string.IsNullOrEmpty(actJobNumber.SelectedValue) || actJobNumber.SelectedValue.Equals("0"))
                    return null;

                return int.Parse(actJobNumber.SelectedValue);
            }
        }

        public void DisplayMessage(string message, bool closeWindow)
        {
            Master.DisplayMessage(message, closeWindow);
        }

        #region [ List ]

        public List<DataContext.CS_View_ManagersLocation> ListAllManagersLocation
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                sgvManagers.DataSource = value;
                sgvManagers.DataBind();
            }
        }

        #endregion

        #region [ Managers Location Row ]

        public string ManagersLocationRowEmployeeName
        {
            set
            {
                HyperLink hlEmployeeName = ManagersLocationRow.FindControl("hlEmployeeName") as HyperLink;
                if (null != hlEmployeeName) hlEmployeeName.Text = value;
            }
        }

        public int ManagersLocationRowEmployeeId
        {
            set
            {
                HyperLink hlEmployeeName = ManagersLocationRow.FindControl("hlEmployeeName") as HyperLink;
                if (null != hlEmployeeName)
                {
                    hlEmployeeName.NavigateUrl = string.Format(
                        "javascript: var newWindow = window.open('/EmployeeMaintenance.aspx?EmployeeID={0}', '', 'width=1200, height=600, scrollbars=1, resizable=yes');",
                        value);
                }
            }
        }

        public string ManagersLocationRowLastCallType
        {
            set
            {
                HyperLink hlLastCallType = ManagersLocationRow.FindControl("hlLastCallType") as HyperLink;

                hlLastCallType.Text = value;
                if (!string.IsNullOrEmpty(ManagersLocationRowCallEntryId) && !string.IsNullOrEmpty(ManagersLocationRowCallEntryJobId) )
                    hlLastCallType.NavigateUrl = string.Format("javascript: var newWindow = window.open('/CallEntry.aspx?JobId={0}&CallEntryId={1}', '', 'width=800, height=600, scrollbars=1, resizable=yes');", ManagersLocationRowCallEntryJobId, ManagersLocationRowCallEntryId);
            }
        }

        public string ManagersLocationRowCallEntryId { get; set; }

        public string ManagersLocationRowCallEntryJobId { get; set; }

        public string ManagersLocationRowHotelDate
        {
            set
            {
                Label lblHotelDate = ManagersLocationRow.FindControl("lblHotelDate") as Label;

                lblHotelDate.Text = value;
            }
        }

        public string ManagersLocationRowHotelDetails
        {
            set
            {
                Label lblHotelDetails = ManagersLocationRow.FindControl("lblHotelDetails") as Label;
                if (null != lblHotelDetails) lblHotelDetails.Text = value;

                Label lblTool = ManagersLocationRow.FindControl("lblTool") as Label;
                if (null != lblTool) lblTool.Text = value;

                Panel pnlToolTip = ManagersLocationRow.FindControl("pnlToolTip") as Panel;
                if (null != pnlToolTip && !string.IsNullOrEmpty(value))
                {
                    ManagersLocationRow.Cells[3].Attributes.Add("onmouseover", "ShowToolTip('" + pnlToolTip.ClientID + "');");
                    ManagersLocationRow.Cells[3].Attributes.Add("onmouseout", "document.getElementById('" + pnlToolTip.ClientID + "').style.display = 'none';");
                }
            }
        }

        public string ManagersLocationRowJobNumber
        {
            set
            {
                HyperLink hlJobNumber = ManagersLocationRow.FindControl("hlJobNumber") as HyperLink;

                hlJobNumber.Text = value;
                if (!string.IsNullOrEmpty(ManagersLocationRowJobId))
                {
                    hlJobNumber.NavigateUrl = string.Format("javascript: var newWindow = window.open('/JobRecord.aspx?JobId={0}', '', 'width=800, height=600, scrollbars=1, resizable=yes');", ManagersLocationRowJobId);
                }
            }
        }

        public string ManagersLocationRowJobId { get; set; }

        public DateTime? ManagersLocationRowLastCallLogDate
        {
            set
            {
                if (value.HasValue)
                {
                    Label lblLastCallTypeDate = ManagersLocationRow.FindControl("lblLastCallTypeDate") as Label;

                    lblLastCallTypeDate.Text = value.Value.ToString("MM/dd/yyy HH:mm");
                }
            }
        }
        #endregion

        #endregion
    }
}