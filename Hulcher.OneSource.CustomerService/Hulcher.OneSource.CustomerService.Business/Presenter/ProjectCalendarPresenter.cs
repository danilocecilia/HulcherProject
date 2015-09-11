using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Business.ViewModel;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Model;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Hulcher.OneSource.CustomerService.DataContext.VO;
using Hulcher.OneSource.CustomerService.Core.Utils;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    public class ProjectCalendarPresenter
    {
        #region [ Attributes ]
        /// <summary>
        /// Instance of the Project Calendar ViewModel
        /// </summary>
        private ProjectCalendarViewModel _viewModel;

        /// <summary>
        /// Instance of the Interface View
        /// </summary>
        private IProjectCalendarView _view;

        JobModel _model;

        #endregion

        #region [ Constructors ]
        /// <summary>
        /// Class Constructor
        /// </summary>
        public ProjectCalendarPresenter(IProjectCalendarView view)
        {
            _view = view;
            _viewModel = new ProjectCalendarViewModel(_view);
            _model = new JobModel();
        }
        #endregion

        #region [ Methods ]

        /// <summary>
        /// Filter Project Calendar Data and Fills the view
        /// </summary>
        public void FindProjectCalendar()
        {
            try
            {
                IList<ProjectCalendarVO> listProjectCalendar = _model.ListAllProjectCalendar(_view.CalendarDateRange, _view.DivisionFilter, _view.EquipmentTypeFilter, _view.EquipmentFilter, _view.EmployeeFilter, _view.CustomerFilter, _view.JobFilter, _view.JobActionFilter);
                _view.ShowHidePanelNowRow(listProjectCalendar.Count.Equals(0));
                _viewModel.CreateDynamicCalendar(listProjectCalendar);
                string queryString = BuildQueryString();
                WebUtil util = new WebUtil();
                string script = util.BuildNewWindowClientScript("/ProjectCalendarPrint.aspx", BuildQueryString(), "", 1100, 600, true, true, true);
                _view.SetPrintButtonClientClick = script;
                //lnkButton.OnClientClick = string.Format("window.open('/ProcessDPI.aspx?DpiID={0}&ParentFieldId={1}', '', 'width=1100, height=600, scrollbars=1, resizable=yes');return false", value, btnFilter.UniqueID);
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to filter Project Calendar!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to filter Project Calendar.", false);
            }
        }

        private string BuildQueryString()
        {                         
            StringBuilder queryStringBuilder = new StringBuilder();
            queryStringBuilder.Append("?StartDate=");
            queryStringBuilder.Append(_view.StartDateValue.ToShortDateString());
             queryStringBuilder.Append("&EndDate=");
            queryStringBuilder.Append(_view.EndDateValue.ToShortDateString());
            if (_view.DivisionFilter.HasValue)
            {
                queryStringBuilder.Append("&Division=");
                queryStringBuilder.Append(_view.DivisionFilter.Value.ToString());
            }
            if (_view.EquipmentTypeFilter.HasValue)
            {
                queryStringBuilder.Append("&EquipmentType=");
                queryStringBuilder.Append(_view.EquipmentTypeFilter.Value.ToString());
            }
             if (_view.EquipmentFilter.HasValue)
            {
                queryStringBuilder.Append("&Equipment=");
                queryStringBuilder.Append(_view.EquipmentFilter.Value.ToString());
            }
            if (_view.EmployeeFilter.HasValue)
            {
                queryStringBuilder.Append("&Employee=");
                queryStringBuilder.Append(_view.EmployeeFilter.Value.ToString());
            }
            if (_view.CustomerFilter.HasValue)
            {
                queryStringBuilder.Append("&Customer=");
                queryStringBuilder.Append(_view.CustomerFilter.Value.ToString());
            }
            if (_view.JobFilter.HasValue)
            {
                queryStringBuilder.Append("&Job=");
                queryStringBuilder.Append(_view.JobFilter.Value.ToString());
            }
            if (_view.JobActionFilter.HasValue)
            {
                queryStringBuilder.Append("&JobAction=");
                queryStringBuilder.Append(_view.JobActionFilter.Value.ToString());
            }
            return queryStringBuilder.ToString();
        }

        /// <summary>
        /// Set default values to the start and end date filter fields
        /// </summary>
        public void DefaultDayProjectCalendar()
        {
            try
            {
                _viewModel.DefaultDayProjectCalendar();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to set the defult values of date to the filters!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error has ocurred while trying to set the defult values of date to the filters.", false);
            }
        }
        #endregion


    }
}
