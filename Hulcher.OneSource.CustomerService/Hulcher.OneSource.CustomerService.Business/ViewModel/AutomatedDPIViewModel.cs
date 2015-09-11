using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.ViewModel;
using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.DataContext;

using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Hulcher.OneSource.CustomerService.Business.ViewModel
{
    public class AutomatedDPIViewModel
    {
        #region [ Attributes ]

        private IAutomatedDPIView _view;

        private DPIModel _dpiModel;
        #endregion

        #region [ Constructors ]

        public AutomatedDPIViewModel(IAutomatedDPIView view)
        {
            _view = view;
            _dpiModel = new DPIModel();
        }

        #endregion

        #region [ Methods ]

        #region [ Page Load ]

        public void UpdateGenerationDate()
        {
            _view.GenerationDate = string.Format("{0} at {1}", DateTime.Now.ToString("MM/dd/yyyy"), DateTime.Now.ToString("HH:mm"));
        }

        #endregion

        #region [ Filter ]
        public void ListFilteredDPIs()
        {
            DateTime dayBefore = new DateTime();
            dayBefore = DateTime.Now.AddDays(-1);

            if (_view.FilterValue == Globals.DPI.FilterType.New)
            {
                if (_view.NewJobDateFilter == Globals.DPI.NewJobFilterType.DayBefore.ToString())
                {
                    _view.DashboardDataSource = _dpiModel.ListFilteredDPIs(_view.FilterValue, dayBefore);
                }
                else
                {
                    _view.DashboardDataSource = _dpiModel.ListFilteredDPIs(_view.FilterValue, _view.ProcessDate);
                }
            }
            else if (_view.FilterValue == Globals.DPI.FilterType.Continuing)
            {
                _view.DashboardDataSource = _dpiModel.ListFilteredDPIs(_view.FilterValue, dayBefore);
            }
            else if (_view.FilterValue == Globals.DPI.FilterType.Reprocess)
            {
                _view.DashboardDataSource = _dpiModel.ListFilteredDPIs(_view.FilterValue, _view.ProcessDate);
            }

            ListAllJobs();
        }
        #endregion

        #region [ Dashboard ]

        public void ListAllJobs()
        {
            List<CS_View_DPIInformation> jobList = new List<CS_View_DPIInformation>();

            jobList.AddRange(_view.DashboardDataSource.Distinct(new Globals.DPI.CS_View_DPIInformation_Comparer()));

            _view.JobRowDatasource = jobList;
        }

        public void ListAllResourcesByJob()
        {
            List<CS_View_DPIInformation> resourceList = new List<CS_View_DPIInformation>();

            resourceList.AddRange(_view.DashboardDataSource.Where(e => e.JobID == _view.JobRowDataItem.JobID && ((e.EmployeeID.HasValue && !e.EquipmentID.HasValue) || (!e.EmployeeID.HasValue && e.EquipmentID.HasValue))));

            _view.ResourcesRowDatasource = resourceList;
        }

        #endregion

        #endregion

        public void ResetTotalCounters()
        {
            _view.TotalJobs = 0;
            _view.RunningTotal = 0;
        }
    }
}
