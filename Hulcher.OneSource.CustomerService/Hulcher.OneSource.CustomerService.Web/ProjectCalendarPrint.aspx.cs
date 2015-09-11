using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Presenter;

namespace Hulcher.OneSource.CustomerService.Web
{
    public partial class ProjectCalendarPrint : System.Web.UI.Page, IProjectCalendarPrintView
    {
        #region [ Attributes ]
        ProjectCalendarPrintPresenter _presenter;

        
        #endregion

        #region [ Events ]
        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter = new ProjectCalendarPrintPresenter(this);
            _presenter.LoadFilters();
            _presenter.CreateCalendar();
           // ScriptManager.RegisterStartupScript(this, this.GetType(), "print", "window.print();", true);
        }
        #endregion

        #region [ Properties ]
        public List<DateTime> CalendarDateRange
        {
            get
            {

                List<DateTime> rangeList = new List<DateTime>();

                DateTime beginDate = StartDateValue;
                DateTime endDate = EndDateValue;
                int days = (int)endDate.Subtract(beginDate).TotalDays;

                for (int i = 0; i <= days; i++)
                {
                    rangeList.Add(beginDate.AddDays(i));
                }

                return rangeList;
            }
        }

        public int? DivisionFilter
        {
            get
            {
                if (Request.QueryString["Division"] != null)
                    return int.Parse(Request.QueryString["Division"]);
                else
                    return null;
            }
        }

        public int? EquipmentTypeFilter
        {
            get
            {
                if (Request.QueryString["EquipmentType"] != null)
                    return int.Parse(Request.QueryString["EquipmentType"]);
                else
                    return null;
            }
        }

        public int? EquipmentFilter
        {
            get
            {
                if (Request.QueryString["Equipment"] != null)
                    return int.Parse(Request.QueryString["Equipment"]);
                else
                    return null;
            }
        }

        public int? EmployeeFilter
        {
            get
            {
                if (Request.QueryString["Employee"] != null)
                    return int.Parse(Request.QueryString["Employee"]);
                else
                    return null;
            }
        }

        public int? CustomerFilter
        {
            get
            {
                if (Request.QueryString["Customer"] != null)
                    return int.Parse(Request.QueryString["Customer"]);
                else
                    return null;
            }
        }

        public int? JobFilter
        {
            get
            {
                if (Request.QueryString["Job"] != null)
                    return int.Parse(Request.QueryString["Job"]);
                else
                    return null;
            }
        }

        public int? JobActionFilter
        {
            get
            {
                if (Request.QueryString["JobAction"] != null)
                    return int.Parse(Request.QueryString["JobAction"]);
                else
                    return null;
            }
        }

        public DateTime StartDateValue
        {
            get
            {
                return DateTime.Parse(Request.QueryString["StartDate"]);
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public DateTime EndDateValue
        {
            get
            {
                return DateTime.Parse(Request.QueryString["EndDate"]);
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string CalendarSource
        {
            set
            {
            }
            //set { litCalendar.Text = value; }
        }

        public string PrintDivisionFilter
        {
            get
            {
                return lblDivisionFilter.Text;
            }
            set
            {
                lblDivisionFilter.Text = value;
            }
        }

        public string PrintEquipmentTypeFilter
        {
            get
            {
                return lblEquipmentTypeFilter.Text;
            }
            set
            {
                lblEquipmentTypeFilter.Text = value;
            }
        }

        public string PrintEquipmentFilter
        {
            get
            {
                return lblEquipmentFilter.Text;
            }
            set
            {
                lblEquipmentFilter.Text = value;
            }
        }

        public string PrintEmployeeFilter
        {
            get
            {
                return lblEmployeeFilter.Text;
            }
            set
            {
                lblEmployeeFilter.Text = value;
            }
        }

        public string PrintCustomerFilter
        {
            get
            {
                return lblCustomerFilter.Text;
            }
            set
            {
                lblCustomerFilter.Text = value;
            }
        }

        public string PrintJobFilter
        {
            get
            {
                return lblJobFilter.Text;
            }
            set
            {
                lblJobFilter.Text = value;
            }
        }

        public string PrintJobActionFilter
        {
            get
            {
                return lblJobActionFilter.Text;
            }
            set
            {
                lblJobActionFilter.Text = value;
            }
        }

        public string PrintStartDateFilter
        {
            get
            {
                return lblStartDateValue.Text;
            }
            set
            {
                lblStartDateValue.Text = value;
            }
        }

        public string PrintEndDateFilter
        {
            get
            {
                return lblEndDateValue.Text;
            }
            set
            {
                lblEndDateValue.Text = value;
            }
        }
        #endregion


        IList<string> IProjectCalendarPrintView.CalendarSource
        {
            set { throw new NotImplementedException(); }
        }

        public void AddCalendarSource(string value)
        {
            Literal lit = new Literal();
            lit.Text = value;
            divTeste.Controls.Add(lit);
        }
    }
}