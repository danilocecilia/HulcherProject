using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.DataContext.VO;
using Hulcher.OneSource.CustomerService.Business.ViewModel;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    public class CallCriteriaInfoPresenter
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of the CallCriteriaInfo view interface
        /// </summary>
        private ICallCriteriaInfoView _view;

        /// <summary>
        /// Instance of the DivisionModel
        /// </summary>
        private DivisionModel _divisionModel;

        /// <summary>
        /// Instance of the JobModel
        /// </summary>
        private JobModel _jobModel;

        /// <summary>
        /// Instance of the Location Business Class
        /// </summary>
        private LocationModel _locationModel;

        /// <summary>
        /// Instance of the Call Log Business Class
        /// </summary>
        private CallLogModel _callLogModel;

        /// <summary>
        /// Instance of the Customer Business Class
        /// </summary>
        private CustomerModel _customerModel;

        /// <summary>
        /// Instance of the CallCriteria Info View Model Class
        /// </summary>
        private CallCriteriaInfoViewModel _viewModel;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="view">Instance of the CallCriteriaInfo Interface</param>
        public CallCriteriaInfoPresenter(ICallCriteriaInfoView view)
        {
            _view = view;
            _viewModel = new CallCriteriaInfoViewModel(view);
        }

        #endregion

        #region [ Methods ]

        public void ListCustomers()
        {
            try
            {
                if (_view.FilterList.Count > 0)
                {
                    using (_customerModel = new CustomerModel())
                    {
                        _view.CustomerList = _customerModel.ListCustomerByIds(_view.FilterList);
                    }
                }
                else
                    _view.CustomerList = new List<CS_Customer>();

            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the customer list.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to load the company list.", false);
            }
        }

        public void ListAllDivisions()
        {
            try
            {
                using (_divisionModel = new DivisionModel())
                {
                    _view.DivisionList = _divisionModel.ListAllDivision();
                }

            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the division list.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to load the division list.", false);
            }
        }

        public void ListAllJobStatus()
        {
            try
            {
                using (_jobModel = new JobModel())
                {
                    _view.JobSatusList = _jobModel.ListAllJobStatus();
                }

            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the job status list.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to load the job status list.", false);
            }
        }

        public void ListAllPriceType()
        {
            try
            {
                using (_jobModel = new JobModel())
                {
                    _view.PriceTypeList = _jobModel.ListAllPriceTypes();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the price type list.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to load the price type list.", false);
            }
        }

        public void ListAllJobCategories()
        {
            try
            {
                using (_jobModel = new JobModel())
                {
                    _view.JobCategoryList = _jobModel.ListAllJobCategories();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the job category list.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to load the job category list.", false);
            }
        }

        public void ListAllJobType()
        {
            try
            {
                using (_jobModel = new JobModel())
                {
                    _view.JobTypeList = _jobModel.ListAllJobTypes();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the job type list.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to load the job type list.", false);
            }
        }

        public void ListAllJobActions()
        {
            try
            {
                using (_jobModel = new JobModel())
                {
                    _view.JobActionList = _jobModel.ListAllJobActions();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the job action list.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to load the job action list.", false);
            }
        }

        public void ListAllInterimBilling()
        {
            try
            {
                using (_jobModel = new JobModel())
                {

                    _view.FrequencyList = _jobModel.ListAllFrequencies();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the interim billing list.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to load the interim billing list.", false);
            }
        }

        /// <summary>
        /// List All Countries in the Database
        /// </summary>
        public void ListAllCountries()
        {
            try
            {
                using (_locationModel = new LocationModel())
                {
                    _view.CountryList = _locationModel.ListAllCountries();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the country list list.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to load the country list.", false);
            }
        }

        public void ListStates()
        {
            try
            {
                if (_view.FilterList.Count > 0)
                {
                    using (_locationModel = new LocationModel())
                    {
                        _view.StateList = _locationModel.ListStatesByIds(_view.FilterList);
                    }
                }
                else
                    _view.StateList = new List<CS_State>();

            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the state list.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to load the state list.", false);
            }
        }

        public void ListCities()
        {
            try
            {
                if (_view.FilterList.Count > 0)
                {
                    using (_locationModel = new LocationModel())
                    {
                        _view.CityList = _locationModel.ListCitiesByIds(_view.FilterList);
                    }
                }
                else
                    _view.CityList = new List<CS_City>();

            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the city list.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to load the city list.", false);
            }
        }

        #region [ Job Call Log ]

        /// <summary>
        /// List all call types separated by primary call type
        /// </summary>
        public void ListAllCallTypes()
        {
            try
            {
                using (_callLogModel = new CallLogModel())
                {
                    _view.PrimaryCallTypeList = _callLogModel.ListAllPrimaryCallTypesForCallCriteria();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has occurred while trying to list Call Types.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has occurred while trying to list Call Types.", false);
            }
        }

        /// <summary>
        /// Fills the Primary Call Type Repeater Row
        /// </summary>
        public void FillPrimaryCallTypeRow()
        {
            try
            {
                if (null != _view.PrimaryCallTypeRepeaterDataItem)
                {
                    _view.PrimaryCallTypeRepeaterRowDescription = _view.PrimaryCallTypeRepeaterDataItem.Type;
                    _view.PrimaryCallTypeRepeaterRowCallTypeList = _view.PrimaryCallTypeRepeaterDataItem.CS_PrimaryCallType_CallType.Where(e => e.CS_CallType.Active).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has occurred while trying to fill the Primary Call Type details.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has occurred while trying to fill the Primary Call Type details.", false);
            }
        }

        #endregion

        #region [ Call Criteria Listing ]

        public void BindCallcriteriaListing()
        {
            try
            {
                CallCriteriaModel _model = new CallCriteriaModel();

                if (_view.EmployeeID.HasValue)
                {
                    _view.CallCriteriaList = _model.FindCallCriteriaByEmployee(_view.EmployeeID.Value);
                    _view.CallCriteriaEditable(true);
                }
                else if (_view.ContactID.HasValue)
                {
                    _view.CallCriteriaList = _model.FindCallCriteriaByContact(_view.ContactID.Value);
                    _view.CallCriteriaEditable(true);
                }

                if (_view.CallCriteriaList != null)
                {
                    _view.CallCriteriaRepeaterDataSource = _view.CallCriteriaList;
                    if (_view.CallCriteriaList.Count > 0)
                        _view.ShowHidePanelNowRowsCcListing = false;
                    else
                        _view.ShowHidePanelNowRowsCcListing = true;
                }

            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to get the Call Criteria Listing info.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to get the Call Criteria Listing info.", false);
            }
        }

        public void CallCriteriaListingRowCommand()
        {
            try
            {
                if (_view.CallCriteriaRowCommand == "EditCriteria")
                {
                    _view.ClearFields();
                    _viewModel.CallCriteriaListingRowCommand();
                }
                else if (_view.CallCriteriaRowCommand == "RemoveCriteria")
                {
                    _viewModel.DeleteCallCriteria();
                    BindCallcriteriaListing();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to execute the selected command.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to execute the selected command.", false);
            }
        }

        public void FillCallCriteriaListingRow()
        {
            try
            {
                _viewModel.FillCallCriteriaListingRow();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to fill the Call Criteria Listing row.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to fill the Call Criteria Listing row.", false);
            }
        }

        public void FillJobAttributesRow()
        {
            try
            {
                _viewModel.FillJobAttributesRow();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to fill the Job Attributes row.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to fill the Job Attributes row.", false);
            }
        }

        public void FillJobCallLogConditionsRow()
        {
            try
            {
                _viewModel.FillJobCallLogConditionsRow();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to fill the Job Call Log Conditions row.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to fill the Job Call Log Conditions row.", false);
            }
        }

        #endregion

        public void CallCriteriaShowCustomer()
        {
            _view.PanelCallCriteria = true;
            _view.PanelDivision = true;
            _view.PanelCustomer = false;
        }

        public void CallCriteriaShowDivision()
        {
            _view.PanelCallCriteria = true;
            _view.PanelDivision = false;
            _view.PanelCustomer = true;
        }

        public void CallCriteriaShowWideLevel()
        {
            _view.PanelCallCriteria = true;
            _view.PanelDivision = true;
            _view.PanelCustomer = true;

        }

        #endregion

        public void AddCallCriteria()
        {
            try
            {
                _viewModel.AddCallCriteria();
                _view.ClearFields();
                BindCallcriteriaListing();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to Add CallCriteria to the Call Criteria Listing.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to Add CallCriteria to the Call Criteria Listing.", false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void LoadCallCriteria()
        {
            try
            {
                _viewModel.LoadCallCriteria();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("Error on loading Call Criteria. \n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while loading the Call Criteria", false);
            }
        }

        public void Cancel()
        {
            _viewModel.Cancel();
        }
    }
}
