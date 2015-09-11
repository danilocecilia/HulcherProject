using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Business.ViewModel;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext.VO;
using Hulcher.OneSource.CustomerService.Business.Model;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    public class ProjectCalendarPrintPresenter
    {
        #region [ Attributes ]
        private IProjectCalendarPrintView _view;

        ProjectCalendarPrintViewModel _viewModel;

        JobModel _model;

        DivisionModel _divisionModel;

        EquipmentModel _equipmentModel;

        EmployeeModel _employeeModel;

        CustomerModel _customerModel;
        #endregion

        #region [ Constructor ]
        public ProjectCalendarPrintPresenter(IProjectCalendarPrintView view)
        {
            _view = view;
            _viewModel = new ProjectCalendarPrintViewModel(view);
            _model = new JobModel();
            _divisionModel = new DivisionModel();
            _equipmentModel = new EquipmentModel();
            _employeeModel = new EmployeeModel();
            _customerModel = new CustomerModel();
        }
        #endregion

        #region [ Methods ]
        
        public void CreateCalendar()
        {
            List<List<DateTime>> listDates = BreakIntoChunks<DateTime>(_view.CalendarDateRange, 7);

            foreach (var item in listDates)
            {
                IList<ProjectCalendarVO> listProjectCalendar = _model.ListAllProjectCalendar(item, _view.DivisionFilter, _view.EquipmentTypeFilter,
                                                                              _view.EquipmentFilter, _view.EmployeeFilter, _view.CustomerFilter, _view.JobFilter,
                                                                              _view.JobActionFilter);

                _viewModel.CreateDynamicCalendar(listProjectCalendar,item);
            }                                                                   
        }

        private  List<List<T>> BreakIntoChunks<T>(List<T> list, int chunkSize)
        {
            if (chunkSize <= 0)
            {
                throw new ArgumentException("chunkSize must be greater than 0.");
            }

            List<List<T>> retVal = new List<List<T>>();

            while (list.Count > 0)
            {
                int count = list.Count > chunkSize ? chunkSize : list.Count;
                retVal.Add(list.GetRange(0, count));
                list.RemoveRange(0, count);
            }

            return retVal;
        }

        public void LoadFilters()
        {
            if (_view.DivisionFilter.HasValue)
                _view.PrintDivisionFilter = _divisionModel.GetDivision(_view.DivisionFilter.Value).Name;
            if (_view.EquipmentTypeFilter.HasValue)
                _view.PrintEquipmentTypeFilter = _equipmentModel.GetEquipmentType(_view.EquipmentTypeFilter.Value).CompleteName;
            if (_view.EquipmentFilter.HasValue)
                _view.PrintEquipmentFilter = _equipmentModel.GetEquipment(_view.EquipmentFilter.Value).CompleteName;
            if (_view.EmployeeFilter.HasValue)
                _view.PrintEmployeeFilter = _employeeModel.GetEmployee(_view.EmployeeFilter.Value).FullName;
            if (_view.CustomerFilter.HasValue)
                _view.PrintCustomerFilter = _customerModel.GetCustomerById(_view.CustomerFilter.Value).Name;
            if (_view.JobFilter.HasValue)
                _view.PrintJobFilter = _model.GetJobById(_view.JobFilter.Value).PrefixedNumber;
            if (_view.JobActionFilter.HasValue)
                _view.PrintJobActionFilter = _model.GetJobAction(_view.JobActionFilter.Value).Description;
            _view.PrintStartDateFilter = _view.StartDateValue.ToShortDateString();
            _view.PrintEndDateFilter = _view.EndDateValue.ToShortDateString();
        }

        #endregion
    }
}
