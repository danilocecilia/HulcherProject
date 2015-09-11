using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Model;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.DataContext.VO;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    public class JSONServicePresenter
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of the Sample Page View Interface
        /// </summary>
        private IJSONService _view;

        /// <summary>
        /// Instance of the Contact Business Class
        /// </summary>
        private DivisionModel _divisionModel;

        /// <summary>
        /// Instance of the Customer Businesse Class
        /// </summary>
        private CustomerModel _customerModel;

        /// <summary>
        /// Instance of the Location Businesse Class
        /// </summary>
        private LocationModel _locationModel;

        /// <summary>
        /// Instance of the Job Businesse Class
        /// </summary>
        private JobModel _jobModel;

        /// <summary>
        /// Instance of the Employee Businesse Class
        /// </summary>
        private EmployeeModel _employeeModel;

        /// <summary>
        /// Instance of the CallLog Businesse Class
        /// </summary>
        private CallLogModel _callLogModel;

        /// <summary>
        /// Instance of the Equipment Businesse Class
        /// </summary>
        private EquipmentModel _equipmentModel;

        /// <summary>
        /// Instance of the Resource Allocation Model class
        /// </summary>
        private ResourceAllocationModel _resourceAllocationModel;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="view">Instance of the Permit Info View Interface</param>
        public JSONServicePresenter(IJSONService view)
        {
            this._view = view;
        }

        #endregion

        #region [ Methods ]

        public void GetEmailDataByCallLogId()
        {
            try
            {
                using (_callLogModel = new CallLogModel())
                {
                    List<int> lstCallLogIds = new List<int>();
                    lstCallLogIds.Add(_view.CallLogId);
                    _view.CallLogCallCriteriaEmail = _callLogModel.ListCallCriteriaEmails(lstCallLogIds);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to get EmailData by CallLogID.\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        /// <summary>
        /// List of all project manager
        /// </summary>
        public void ListProjectManager()
        {
            try
            {
                using (_employeeModel = new EmployeeModel())
                {
                    _view.ProjectManagerList = _employeeModel.ListAllEmployeeProjectManagerByName(_view.PrefixText);
                }
            }
            catch (Exception ex)
            {

                Logger.Write(string.Format("There was an error loading the Project Managers\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        public void GetDivisionByEmployee()
        {
            try
            {
                using (_divisionModel = new DivisionModel())
                {
                    CS_Division division = _divisionModel.GetDivisionByEmployee(_view.EmployeeId);
                    if (null != division)
                    {
                        _view.ReturnDataObject = new Globals.SampleDataObject();
                        _view.ReturnDataObject.Id = division.ID;
                        _view.ReturnDataObject.Name = division.Name;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to get Division by Employee.\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        public void GetCustomerByContact()
        {
            try
            {
                using (_customerModel = new CustomerModel())
                {
                    CS_Customer customer = _customerModel.GetCustomerByContact(_view.ContactId);
                    if (null != customer)
                    {
                        _view.ReturnDataObject = new Globals.SampleDataObject();
                        _view.ReturnDataObject.Id = customer.ID;
                        _view.ReturnDataObject.Name = customer.Name;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to get Customer by Contact.\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        public void GetFirstZipCodeByCity()
        {
            try
            {
                using (_locationModel = new LocationModel())
                {
                    CS_ZipCode zipCode = _locationModel.GetZipCodeByCityId(_view.CityId).FirstOrDefault();

                    if (null != zipCode)
                    {
                        _view.ReturnDataObject = new Globals.SampleDataObject();
                        _view.ReturnDataObject.Id = zipCode.ID;
                        _view.ReturnDataObject.Name = zipCode.ZipCodeNameEdited;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to get ZipCode by City.\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        public void GetStateAndCountryByCity()
        {
            try
            {
                using (_locationModel = new LocationModel())
                {
                    CS_State state = _locationModel.GetStateByCityId(_view.CityId);

                    if (null != state)
                    {
                        _view.StateAndCountryDataObject = new Globals.StateAndCountryDataObject();
                        _view.StateAndCountryDataObject.StateId = state.ID;
                        _view.StateAndCountryDataObject.StateName = state.AcronymName;
                        _view.StateAndCountryDataObject.CountryId = state.CountryID;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to get ZipCode by City.\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        public void ListAllCitiesByName()
        {
            try
            {
                using (_locationModel = new LocationModel())
                {
                    _view.CityList = _locationModel.ListCityByNameAndState(0, _view.PrefixText);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the City Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        public void GetPresetNotificationList()
        {
            try
            {
                using (_jobModel = new JobModel())
                {
                    _view.PresetNotificationList = _jobModel.ListPresetNotification(DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                if (null != ex.InnerException)
                    Logger.Write(string.Format("An error has occurred while trying to get the Preset Notification LIst\n{0}\n{1}\n{2}\n{3}", ex.Message, ex.StackTrace, ex.InnerException.Message, ex.InnerException.StackTrace));
                else
                    Logger.Write(string.Format("An error has occurred while trying to get the Preset Notification LIst\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        public void GetEquipmentComboNotificationList()
        {
            try
            {
                using (_equipmentModel = new EquipmentModel())
                {
                    _view.EquipmentComboNotificationList = _equipmentModel.ListEquipmentComboNotification();
                }
            }
            catch (Exception ex)
            {
                if (null != ex.InnerException)
                    Logger.Write(string.Format("An error has occurred while trying to get the EquipmentCombo Notification LIst\n{0}\n{1}\n{2}\n{3}", ex.Message, ex.StackTrace, ex.InnerException.Message, ex.InnerException.StackTrace));
                else
                    Logger.Write(string.Format("An error has occurred while trying to get the EquipmentCombo Notification LIst\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        public void GetEquipmentPermitNotificationList()
        {
            try
            {
                using (_equipmentModel = new EquipmentModel())
                {
                    _view.EquipmentPermitNotificationList = _equipmentModel.ListEquipmentPermitNotification(DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                if (null != ex.InnerException)
                    Logger.Write(string.Format("An error has occurred while trying to get the EquipmentPermit Notification LIst\n{0}\n{1}\n{2}\n{3}", ex.Message, ex.StackTrace, ex.InnerException.Message, ex.InnerException.StackTrace));
                else
                    Logger.Write(string.Format("An error has occurred while trying to get the EquipmentPermit Notification LIst\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        public void GetOffCallNotificationList()
        {
            try
            {
                using (_employeeModel = new EmployeeModel())
                {
                    _view.OffCallNotificationList = _employeeModel.ListEmployeeOffCallHistoryNotification(DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                if (null != ex.InnerException)
                    Logger.Write(string.Format("An error has occurred while trying to get the Off Call Notification LIst\n{0}\n{1}\n{2}\n{3}", ex.Message, ex.StackTrace, ex.InnerException.Message, ex.InnerException.StackTrace));
                else
                    Logger.Write(string.Format("An error has occurred while trying to get the Off Call Notification LIst\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        public void GetMapPlottingObjects()
        {
            try
            {
                List<Globals.MapPlotDataObject> returnList = new List<Globals.MapPlotDataObject>();
                using (_jobModel = new JobModel())
                {
                    returnList.AddRange(_jobModel.FilterMapPlotting(_view.MapPlottingFilter));
                }
                using (_resourceAllocationModel = new ResourceAllocationModel())
                {
                    returnList.AddRange(_resourceAllocationModel.FilterMapPlotting(_view.MapPlottingFilter));
                }
                _view.MapPlottingObjectsList = returnList;
            }
            catch (Exception ex)
            {
                if (null != ex.InnerException)
                    Logger.Write(string.Format("An error has occurred while trying to get the Map Plot List\n{0}\n{1}\n{2}\n{3}", ex.Message, ex.StackTrace, ex.InnerException.Message, ex.InnerException.StackTrace));
                else
                    Logger.Write(string.Format("An error has occurred while trying to get the Map Plot List\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        public void GetJobStatus()
        {
            try
            {
                _view.JobStatusList = new List<CS_JobStatus>();
                using (_jobModel = new JobModel())
                {
                    CS_Job job = _jobModel.GetJobById(_view.JobId);
                    if (job != null)
                        _view.JobStatusList.Add(job.CS_JobInfo.LastJobStatus);
                }
            }
            catch (Exception ex)
            {
                if (null != ex.InnerException)
                    Logger.Write(string.Format("An error has occurred while trying to get the Job Status\n{0}\n{1}\n{2}\n{3}", ex.Message, ex.StackTrace, ex.InnerException.Message, ex.InnerException.StackTrace));
                else
                    Logger.Write(string.Format("An error has occurred while trying to get the Job Status\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        public void GetEquipmentTypeByEquipment()
        {
            try
            {
                using (_equipmentModel = new EquipmentModel())
                {
                    CS_EquipmentType eqType = _equipmentModel.GetEquipmentTypeByEquipmentId(_view.EquipmentId);

                    _view.ReturnDataObject = new Globals.SampleDataObject()
                    {
                        Id = eqType.ID,
                        Name = eqType.Name
                    };
                }
            }
            catch (Exception ex)
            {
                if (null != ex.InnerException)
                    Logger.Write(string.Format("An error has occurred while trying to get the Equipment Type information\n{0}\n{1}\n{2}\n{3}", ex.Message, ex.StackTrace, ex.InnerException.Message, ex.InnerException.StackTrace));
                else
                    Logger.Write(string.Format("An error has occurred while trying to get the Equipment Type information\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        public void CheckExistingCustomer()
        {
            try
            {
                using (_customerModel = new CustomerModel())
                {

                    _view.IsNeWCompany = _customerModel.GetCustomerByName(_view.CustomerName);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to Load customer information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }

        }

        public void CustomerHasOperatorAlert()
        {
            try
            {
                using (_customerModel = new CustomerModel())
                {
                    CS_Customer customer = _customerModel.GetCustomerById(_view.CustomerId);

                    if (null != customer && customer.OperatorAlert.HasValue && customer.OperatorAlert.Value)
                        _view.OperatorAlertMessage = "Operator Alert: " + customer.AlertNotification;
                    else
                        _view.OperatorAlertMessage = "";
                }
            }
            catch (Exception ex)
            {
                if (null != ex.InnerException)
                    Logger.Write(string.Format("An error has occurred while trying to check if the customer is collection\n{0}\n{1}\n{2}\n{3}", ex.Message, ex.StackTrace, ex.InnerException.Message, ex.InnerException.StackTrace));
                else
                    Logger.Write(string.Format("An error has occurred while trying to check if the customer is collection\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        public void CustomerIsCollection()
        {
            try
            {
                using (_customerModel = new CustomerModel())
                {
                    CS_Customer customer = _customerModel.GetCustomerById(_view.CustomerId);

                    if (null != customer)
                        _view.IsCostumerCollection = customer.IsCollection;
                    else
                        _view.IsCostumerCollection = false;
                }
            }
            catch (Exception ex)
            {
                if (null != ex.InnerException)
                    Logger.Write(string.Format("An error has occurred while trying to check if the customer is collection\n{0}\n{1}\n{2}\n{3}", ex.Message, ex.StackTrace, ex.InnerException.Message, ex.InnerException.StackTrace));
                else
                    Logger.Write(string.Format("An error has occurred while trying to check if the customer is collection\n{0}\n{1}", ex.Message, ex.StackTrace));
            }

        }
        #endregion

        #region [ AutoCompleteTextBox Methods ]

        /// List all Customers in the Database and fill the Dropdown
        /// </summary>
        public void ListCustomerByNameJSON()
        {
            try
            {
                using (_customerModel = new CustomerModel())
                {
                    _view.CustomerList = _customerModel.ListCustomerByName(_view.FilterText);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Customer Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        public void ListDynamicsContactByName()
        {
            try
            {
                using (_customerModel = new CustomerModel())
                {
                    _view.DynamicsContactList = _customerModel.ListFilteredContactsByName(long.Parse(_view.FilterValue), true, _view.FilterText);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Contact Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        public void ListCustomerServiceContactByName()
        {
            try
            {
                _customerModel = new CustomerModel();
                _view.CustomerServiceContactList = _customerModel.ListFilteredContactsByName(long.Parse(_view.FilterValue), false, _view.FilterText);
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Contact Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        public void ListJobStatusByDescription()
        {
            try
            {
                using (_jobModel = new JobModel())
                {
                    _view.JobStatusList = _jobModel.ListJobStatusByDescription(_view.FilterText);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Job Status Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        public void ListEmployeeByName()
        {
            try
            {
                long divisionId = 0;

                if (!string.IsNullOrEmpty(_view.FilterValue))
                    divisionId = Convert.ToInt32(_view.FilterValue);

                _employeeModel = new EmployeeModel();
                _view.EmployeeList = _employeeModel.ListFilteredEmployeeByName(divisionId, _view.FilterText).OrderBy(e => e.CS_Division.Name).ThenBy(e => e.FullName).ToList();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Contact Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        public void ListAllCustomerContactByName()
        {
            try
            {
                using (_customerModel = new CustomerModel())
                {
                    _view.ContactList = _customerModel.ListAllFilteredContactsByName(Int32.Parse(_view.FilterValue), _view.FilterText);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Contact Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        /// <summary>
        /// List All divisions filtered by Name
        /// </summary>
        public void ListDivisionByName()
        {
            try
            {
                using (_divisionModel = new DivisionModel())
                {
                    _view.DivisionList = _divisionModel.ListAllFilteredDivisionByName(_view.FilterText);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Division Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        /// <summary>
        /// List All Price Types filtered by Name
        /// </summary>
        public void ListPriceTypeByName()
        {
            try
            {
                using (_jobModel = new JobModel())
                {
                    _view.PriceTypeList = _jobModel.ListAllFilteredPriceTypesByName(_view.FilterText);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Price Type Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        /// <summary>
        /// List all job action filtered by name
        /// </summary>
        public void ListJobActionByName()
        {
            try
            {
                using (_jobModel = new JobModel())
                {
                    _view.JobActionList = _jobModel.ListAllJobActionByName(_view.FilterText);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Job Action information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        public void ListCityByNameAndState()
        {
            try
            {
                using (_locationModel = new LocationModel())
                {
                    _view.CityList = _locationModel.ListCityByNameAndState(int.Parse(_view.FilterValue), _view.FilterText);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the City Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        /// <summary>
        /// List all active ZipCodes filtered by City and Name (Like)
        /// </summary>
        public void ListZipCodesByNameAndCity()
        {
            try
            {
                using (_locationModel = new LocationModel())
                {
                    _view.ZipCodeList = _locationModel.ListZipCodeByNameAndCity(int.Parse(_view.FilterValue), _view.FilterText);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Zip Code Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }


        /// <summary>
        /// List all active States
        /// </summary>
        public void ListStatesByName()
        {
            try
            {
                using (_locationModel = new LocationModel())
                {
                    _view.StateList = _locationModel.ListAllStatesByName(_view.FilterText);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the State Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        /// <summary>
        /// List all active States
        /// </summary>
        public void ListStatesByAcronym()
        {
            try
            {
                using (_locationModel = new LocationModel())
                {
                    _view.StateList = _locationModel.ListAllStatesByAcronym(_view.FilterText, _view.ContextKey);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the State Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        /// <summary>
        /// List all active States
        /// </summary>
        public void ListStatesByAcronymAndDivision()
        {
            try
            {
                using (_locationModel = new LocationModel())
                {
                    _view.StateList = _locationModel.ListAllStatesByAcronymAndDivision(_view.FilterText);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the State Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        /// <summary>
        /// List all JobNumbers and Internal Tracking
        /// </summary>
        public void ListJobNumberByDescription()
        {
            try
            {
                using (_jobModel = new JobModel())
                {
                    _view.JobList = _jobModel.ListAllJobsByNumber(_view.FilterText, _view.ContextKey);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Job Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        /// <summary>
        /// List all JobNumbers and Internal Tracking
        /// </summary>
        public void ListJobNumberByDescriptionWithGeneral()
        {
            try
            {
                using (_jobModel = new JobModel())
                {
                    _view.JobList = _jobModel.ListAllJobsByNumberWithGeneral(_view.FilterText);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Job Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        /// <summary>
        /// List all Billable JobNumbers and Internal Tracking
        /// </summary>
        public void ListBillableJobNumberByDescription()
        {
            try
            {
                using (_jobModel = new JobModel())
                {
                    _view.JobList = _jobModel.ListAllBillableJobsByNumber(_view.FilterText);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Job Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        public void ListCallTypeListByDescription()
        {
            try
            {
                using (_callLogModel = new CallLogModel())
                {
                    _view.CallTypeList = _callLogModel.ListCallTypeFilteredByDescription(_view.FilterText);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Job Status Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        /// <summary>
        /// List All Equipment Types by name
        /// </summary>
        public void ListAllEquipmentTypeByName()
        {
            try
            {
                using (_equipmentModel = new EquipmentModel())
                {
                    _view.EquipmentTypeList = _equipmentModel.ListAllEquipmentTypeByName(_view.FilterText);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Equipment Type Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        /// <summary>
        /// List equipment by equipment type
        /// </summary>
        public void ListAllEquipmentByEqType()
        {
            try
            {
                using (_equipmentModel = new EquipmentModel())
                {
                    _view.EquipmentList = _equipmentModel.ListAllEquipmentByEqType(int.Parse(_view.ContextKey), _view.PrefixText);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Equipment Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        public void GetLocalEquipmentTypelList()
        {
            try
            {
                using (_equipmentModel = new EquipmentModel())
                {
                    _view.LocalEquipmentTypeList = _equipmentModel.ListAllLocalEquipmentTypeByName(_view.PrefixText);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the local equipment type Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }
        #endregion
    }
}
