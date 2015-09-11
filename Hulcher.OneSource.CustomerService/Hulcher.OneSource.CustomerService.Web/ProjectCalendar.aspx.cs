using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.Business.ViewModel;

namespace Hulcher.OneSource.CustomerService.Web
{
    public partial class ProjectCalendar : System.Web.UI.Page, IProjectCalendarView
    {
        #region [ Attributes ]

        ProjectCalendarPresenter _presenter;

        #endregion

        #region [ Overrides ]

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _presenter = new ProjectCalendarPresenter(this);
        }

        #endregion

        #region [ Events ]
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                _presenter.DefaultDayProjectCalendar();
        }

        protected void actUnitNumber_TextChanged(object sender, EventArgs e)
        {
            actEquipmentType.ContextKey = actUnitNumber.SelectedValue;
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            _presenter.FindProjectCalendar();
        }

        #endregion

        #region [ Methods ]

        #endregion

        #region [ Properties ]

        #region [ Filter ]

        public List<DateTime> CalendarDateRange
        {
            get
            {
                List<DateTime> rangeList = new List<DateTime>();

                DateTime beginDate = dpBeginDate.Value.Value;
                DateTime endDate = dpEndDate.Value.Value;
                int days = (int)endDate.Subtract(beginDate).TotalDays;

                for (int i = 0; i <= days; i++)
                {
                    rangeList.Add(beginDate.AddDays(i));
                }

                return rangeList;
            }
            set
            {
            }
        }

        public int? DivisionFilter
        {
            get
            {
                if (actDivision.SelectedValue == "0")
                    return null;
                else
                    return int.Parse(actDivision.SelectedValue);
            }
        }

        public int? EquipmentTypeFilter
        {
            get
            {
                if (actEquipmentType.SelectedValue == "0")
                    return null;
                else
                    return int.Parse(actEquipmentType.SelectedValue);
            }
        }

        public int? EquipmentFilter
        {
            get
            {
                if (actUnitNumber.SelectedValue == "0")
                    return null;
                else
                    return int.Parse(actUnitNumber.SelectedValue);
            }
        }

        public int? EmployeeFilter
        {
            get
            {
                if (actEmployeeName.SelectedValue == "0")
                    return null;
                else
                    return int.Parse(actEmployeeName.SelectedValue);
            }
        }

        public int? CustomerFilter
        {
            get
            {
                if (actCustomer.SelectedValue == "0")
                    return null;
                else
                    return int.Parse(actCustomer.SelectedValue);
            }
        }

        public int? JobFilter
        {
            get
            {
                if (actJobNumber.SelectedValue == "0")
                    return null;
                else
                    return int.Parse(actJobNumber.SelectedValue);
            }
        }

        public int? JobActionFilter
        {
            get
            {
                if (actJobAction.SelectedValue == "0")
                    return null;
                else
                    return int.Parse(actJobAction.SelectedValue);
            }
        }

        public DateTime StartDateValue
        {
            get
            {
                if (dpBeginDate.Value.HasValue)
                    return dpBeginDate.Value.Value;
                else
                    return DateTime.MinValue;
            }
            set
            {
                dpBeginDate.Value = value;
            }
        }

        public DateTime EndDateValue
        {
            get
            {
                if (dpEndDate.Value.HasValue)
                    return dpEndDate.Value.Value;
                else
                    return DateTime.MinValue;
            }
            set
            {
                dpEndDate.Value = value;
            }
        }

        #endregion

        #region [ CalendarView ]

        public string CalendarSource
        {
            set { litCalendar.Text = value; }
        }

        #endregion

        #region [ Calendar Table ]
        public void ShowHidePanelNowRow(bool show)
        {
            if (show)
            {
                pnlNoRows.Visible = true;

                ProjectCalendarTable.Style.Add("display", "none");

            }
            else
            {
                pnlNoRows.Visible = false;

                ProjectCalendarTable.Style.Add("display", "block");
            }
        }
        #endregion

        public string SetPrintButtonClientClick
        {
            set
            {
                btnPrint.OnClientClick = value;
            }
        }

        public void DisplayMessage(string message, bool closeWindow)
        {
            Master.DisplayMessage(message, closeWindow);
        }
        #endregion
    }
}