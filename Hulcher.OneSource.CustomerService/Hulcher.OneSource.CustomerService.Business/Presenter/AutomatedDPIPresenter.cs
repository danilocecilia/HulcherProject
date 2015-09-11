using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.ViewModel;
using Hulcher.OneSource.CustomerService.Business.Model;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Hulcher.OneSource.CustomerService.Core;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    public class AutomatedDPIPresenter
    {
        IAutomatedDPIView _view;
        AutomatedDPIViewModel _viewModel;

        public AutomatedDPIPresenter(IAutomatedDPIView view)
        {
            _view = view;
            _viewModel = new AutomatedDPIViewModel(view);
        }

        public void LoadPage()
        {
            try
            {
                _view.FilterValue = Core.Globals.DPI.FilterType.New;
                _view.ProcessDate = DateTime.Now;

                BindDashboard();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to load the Page!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to load the Page.", false);
            }
        }

        public void UpdateGenerationDate()
        {
            try
            {
                _viewModel.UpdateGenerationDate();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to update Generation Date!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to update Generation Date.", false);
            }
        }

        public void BindDashboard()
        {
            try
            {
                using (DPIModel _model = new DPIModel())
                {
                    _viewModel.ListFilteredDPIs();

                    //_view.DashboardDataSource = _model.ListFilteredDPIs(_view.FilterValue, _view.ProcessDate);
                    //_viewModel.ListAllJobs();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to load the DPI Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to load the DPI Information.", false);
            }
        }

        public void SetJobRowInfo()
        {
            try
            {
                _view.JobRowJobNumber = _view.JobRowDataItem.PrefixedNumber;
                _view.JobRowDivision = _view.JobRowDataItem.JobDivision;
                _view.JobRowCustomer = _view.JobRowDataItem.JobCustomerName;
                _view.JobRowLocation = _view.JobRowDataItem.JobLocation;
                _view.JobRowJobAction = _view.JobRowDataItem.JobAction;
                _view.JobRowCarCountEmpties = _view.JobRowDataItem.NumberEmpties;
                _view.JobRowCarCountEngines = _view.JobRowDataItem.NumberEngines;
                _view.JobRowCarCountLoads = _view.JobRowDataItem.NumberLoads;
                _view.JobRowDpiID = _view.JobRowDataItem.DPIID;
                _view.JobRowDpiStatus = (Globals.DPI.DpiStatus)_view.JobRowDataItem.DPIProcessStatus;
                _view.JobRowStatus = (Globals.DPI.CalculationStatus)_view.JobRowDataItem.DPICalculationStatus;
                _view.JobRowRevenue = double.Parse(_view.JobRowDataItem.DPITotal.ToString());

                _view.RunningTotal += double.Parse(_view.JobRowDataItem.DPITotal.ToString());
                _view.TotalJobs++;
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to load the DPI Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to load the DPI Information.", false);
            }
        }

        public void GetResourcesRow()
        {
            try
            {
                _viewModel.ListAllResourcesByJob();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to load the DPI Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to load the DPI Information.", false);
            }
        }

        public void SetResourcesRowInfo()
        {
            try
            {
                _view.JobRowHasResources = true;
                _view.ResourcesRowJobID = _view.ResourcesRowDataItem.JobID;

                if (_view.ResourcesRowDataItem.EmployeeID.HasValue)
                {
                    _view.ResourcesRowDivision = _view.ResourcesRowDataItem.EmployeeDivision;
                    _view.ResourcesRowResource = _view.ResourcesRowDataItem.EmployeePosition;
                    _view.ResourcesRowDescription = _view.ResourcesRowDataItem.EmployeeName;
                }
                else
                {
                    _view.ResourcesRowDivision = _view.ResourcesRowDataItem.EquipmentDivision;
                    _view.ResourcesRowResource = _view.ResourcesRowDataItem.EquipmentUnitNumber;
                    _view.ResourcesRowDescription = _view.ResourcesRowDataItem.EquipmentName;
                }

                if (_view.ResourcesRowDataItem.DPIResourceTotal.HasValue)
                    _view.ResourcesRowRevenue = double.Parse(_view.ResourcesRowDataItem.DPIResourceTotal.ToString());
                else
                    _view.ResourcesRowRevenue = 0;
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to load the DPI Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to load the DPI Information.", false);
            }
        }

        public void ResetTotalCounters()
        {
            try
            {
                _viewModel.ResetTotalCounters();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to reset the DPI Total Counters!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to reset Total Counter information. Information in the labels might be wrong.", false);
            }
        }
    }
}
