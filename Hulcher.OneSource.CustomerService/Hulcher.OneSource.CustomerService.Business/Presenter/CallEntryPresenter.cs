using System;
using System.Collections.Generic;
using System.Linq;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.DataContext;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Business.ViewModel;
using System.Web.UI;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields;
using Hulcher.OneSource.CustomerService.Business.WebControls.ServerControls;
using System.Reflection.Emit;
using Hulcher.OneSource.CustomerService.DataContext.EntityExtensions;
using Hulcher.OneSource.CustomerService.DataContext.VO;
using System.Transactions;
using Hulcher.OneSource.CustomerService.Core.Utils;
using Hulcher.OneSource.CustomerService.Business.WebControls.Utils;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    public class CallEntryPresenter
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of the CallEntry view interface
        /// </summary>
        private ICallEntryView _view;

        /// <summary>
        /// Instance of the call entry view model
        /// </summary>
        private CallEntryViewModel _viewModel;

        /// <summary>
        /// Instance of the CallLog Model
        /// </summary>
        private CallLogModel _callLogModel;

        /// <summary>
        /// Instance of the Job Model
        /// </summary>
        private JobModel _jobModel;

        /// <summary>
        /// Instance of the resource allocation model
        /// </summary>
        private ResourceAllocationModel _resourceAllocationModel;

        /// <summary>
        /// Instance of the resource allocation model
        /// </summary>
        private EmployeeModel _employeeModel;

        /// <summary>
        /// Instance of the resource customer model
        /// </summary>
        private CustomerModel _customerModel;

        /// <summary>
        /// Instance of the Call Criteria Model
        /// </summary>
        private CallCriteriaModel _callCriteriaModel;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="view">Instance of the CallEntryView Interface</param>
        public CallEntryPresenter(ICallEntryView view)
        {
            _view = view;
            _viewModel = new CallEntryViewModel(_view);
        }

        #endregion

        #region [ Methods ]

        #region [ Common ]

        /// <summary>
        /// Fills the Page Title with the correct information
        /// </summary>
        public void GetPageTitle()
        {
            try
            {
                if (!_view.JobId.HasValue)
                    _view.PageTitle = "New Call Entry";
                else
                {
                    using (_jobModel = new JobModel())
                    {
                        CS_Job job = _jobModel.GetJobById(_view.JobId.Value);
                        if (null != job)
                        {
                            _view.PageTitle = string.Format("{0} - Call Entry", job.PrefixedNumber);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to get the Page Title.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to load the Information. Please try again.", false);
            }
        }

        #endregion

        #region [ Job Info ]

        /// <summary>
        /// Get Job Info into Job Property
        /// </summary>
        public void GetJobInfo()
        {
            try
            {
                using (_jobModel = new JobModel())
                {
                    CS_Job job = _jobModel.GetJobInfoForCallEntry(_view.JobId.Value);
                    _view.Job = job;
                    _view.CustomerId = job.CS_CustomerInfo.CustomerId;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to get the Job Info.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to load the Information. Please try again.", false);
            }

        }

        /// <summary>
        /// Retrieves filtered data for the JobFilter GridView
        /// </summary>
        public void GetJobFilterGridInfo()
        {
            try
            {
                using (_jobModel = new JobModel())
                {
                    _view.JobFilterGridDataSource = _jobModel.CallEntryFilter(_view.JobFilterType, _view.JobFilterValue);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to GetJobFilterGridInfo.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to load the Information. Please try again.", false);
            }

        }

        #endregion

        #region [ Call Log ]

        /// <summary>
        /// List PrimaryCallTypes
        /// </summary>
        public void ListPrimaryCallTypes()
        {
            try
            {
                using (_callLogModel = new CallLogModel())
                {
                    _view.PrimaryCallTypeList = _callLogModel.ListAllPrimaryCallTypesByCallType(_view.CallTypeId, _view.GeneralLog);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to List All PrimaryCallTypes.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to load the Information. Please try again.", false);
            }

        }

        /// <summary>
        /// List all CallTypes by Primarycalltype
        /// </summary>
        public void ListCallTypes()
        {
            try
            {
                using (_callLogModel = new CallLogModel())
                {
                    _view.CallTypeList = _callLogModel.ListAllCallType(_view.GeneralLog);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to List All CallTypes.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to load the Information. Please try again.", false);
            }
        }

        /// <summary>
        /// Gets The Identified PrimaryCallType
        /// </summary>
        public void GetPrimaryCallType()
        {
            try
            {
                using (_callLogModel = new CallLogModel())
                {
                    _view.SelectedPrimaryCallType = _callLogModel.GetPrimaryCallType(_view.PrimaryCallTypeId);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to GetPrimaryCallType.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to load the Information. Please try again.", false);
            }

        }

        /// <summary>
        /// Sets Selected CallType
        /// </summary>
        public void GetCallType()
        {
            try
            {
                using (_callLogModel = new CallLogModel())
                {
                    _view.SelectedCallType = _callLogModel.GetCallType(_view.CallTypeId);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to GetCallType.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to load the Information. Please try again.", false);
            }
        }

        /// <summary>
        /// Creates Dinamic Fields
        /// </summary>
        public void CreateDynamicFields()
        {
            if (_view.SelectedCallType != null)
            {
                if (null != _view.DynamicControlXmlString)
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object>();

                    if (_view.CallDateTime.HasValue)
                        parameters.Add("DateTime", _view.CallDateTime.Value);

                    if (_view.JobId.HasValue)
                        parameters.Add("JobID", _view.JobId.Value);

                    _view.DynamicFieldsControls = DynamicFieldsParser.CreateFieldFromXml(_view.DynamicControlXmlString, parameters);
                }
                else
                    _view.DynamicFieldsControls = new List<Control>();
            }
        }

        /// <summary>
        /// Loads and existing call log
        /// </summary>
        public void LoadCallLog()
        {
            try
            {
                using (_callLogModel = new CallLogModel())
                {
                    if (_view.JobId.HasValue && _view.CallID.HasValue)
                    {
                        _view.CallEntryEntity = _callLogModel.LoadCallLog(_view.JobId.Value, _view.CallID.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load call log.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to load the Information. Please try again.", false);
            }
        }

        /// <summary>
        /// Saves a call log
        /// </summary>
        public void SaveCallLog()
        {
            try
            {
                CS_CallLog callEntry = _view.CallEntryEntity;

                if (_view.DynamicFieldsControls.Count > 0)
                {
                    callEntry.Xml = DynamicFieldsParser.CreateXmlFromControls(_view.DynamicFieldsControls, _view.DynamicControlXmlString);

                    if (string.IsNullOrEmpty(callEntry.Note) && !string.IsNullOrEmpty(callEntry.Xml))
                    {
                        callEntry.Note = "Job #:<Text>";

                        if (_view.JobId != (int)Globals.GeneralLog.ID)
                            callEntry.Note += _view.PrefixedJobNumber + "<BL>";
                        else
                            callEntry.Note += "PA99999 - General Log<BL>";

                        callEntry.Note += DynamicFieldsParser.FormatDynamicFieldsData(DynamicFieldsParser.GetDynamicFieldControlsProperties(callEntry.Xml));
                        callEntry.Note += _viewModel.FormatResourcesOrPersonsData();
                    }
                }

                if (_view.CopyToShiftTransferLog)
                    callEntry.ShiftTransferLog = true;

                if (_view.CopyToGeneralLog)
                    callEntry.HasGeneralLog = true;

                IList<CS_CallLog_LocalEquipmentType> callLogEqTypeList = new List<CS_CallLog_LocalEquipmentType>();
                for (int i = 0; i < _view.SelectedEquipmentTypes.Count; i++)
                {
                    if (!_view.CallID.HasValue)
                        callLogEqTypeList.Add(_viewModel.ParseVOToCallLogEquipmentTypeEntity(callEntry.ID, _view.SelectedEquipmentTypes[i], false));
                    else
                        callLogEqTypeList.Add(_viewModel.ParseVOToCallLogEquipmentTypeEntity(callEntry.ID, _view.SelectedEquipmentTypes[i], true));
                }

                using (_callLogModel = new CallLogModel())
                {
                    _callLogModel.SaveCallLogData(callEntry, _view.SelectedPersons.ToList(), _view.SelectedResources.ToList(), callLogEqTypeList, callEntry.ID != 0, _view.CopyToGeneralLog);


                    IList<int> callLogHistory = _view.CallLogHistoryList;
                    callLogHistory.Add(callEntry.ID);
                    _view.CallLogHistoryList = callLogHistory;



                    //IList<CS_CallLog_LocalEquipmentType> callLogEqTypeList = new List<CS_CallLog_LocalEquipmentType>();
                    //for (int i = 0; i < _view.SelectedEquipmentTypes.Count; i++)
                    //{
                    //    callLogEqTypeList.Add(_viewModel.ParseVOToCallLogEquipmentTypeEntity(callEntry.ID, _view.SelectedEquipmentTypes[i]));
                    //}
                    //if (callLogEqTypeList.Count > 0)
                    //    _callLogModel.SaveCallLogEquipmentTypeList(callLogEqTypeList);


                    if (!_view.SaveAndContinue)
                    {
                        CS_CallType calltype = _callLogModel.GetCallType(callEntry.CallTypeID);

                        _view.OpenEmailPage = calltype.SendEmail;
                    }

                }
                _view.SavedSuccessfuly = true;

            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to save the call log.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to save the Information. Please try again.", false);

                _view.SavedSuccessfuly = false;
            }
        }

        public void LoadCallLogEquipmentType()
        {
            try
            {
                using (_callLogModel = new CallLogModel())
                {
                    if (_view.JobId.HasValue && _view.CallID.HasValue)
                    {
                        IList<CS_CallLog_LocalEquipmentType> callLogEqTypeList = _callLogModel.ListCallLogEquipmentTypesByCallLogID(_view.CallID.Value);
                        IList<LocalEquipmentTypeVO> localEqType = new List<LocalEquipmentTypeVO>();
                        for (int i = 0; i < callLogEqTypeList.Count; i++)
                        {
                            localEqType.Add(_viewModel.ParseCallLogEquipmentTypeEntityToVO(callLogEqTypeList[i]));
                        }
                        _view.SelectedEquipmentTypes = localEqType;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load call log equipment types.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to load call log equipment types. Please try again.", false);
            }
        }

        #endregion

        #region [ Initial Advise ]

        /// <summary>
        /// Load data for call criteria
        /// </summary>
        public void ListAllEmployeeCallLogInfoCallCriteria()
        {
            try
            {
                if (_view.JobId.HasValue && _view.CallID.HasValue)
                {
                    IList<CS_CallLogResource> resources = new List<CS_CallLogResource>();

                    using (_callLogModel = new CallLogModel())
                    {
                        resources = _callLogModel.ListCallLogResourceByCallEntry(_view.CallID.Value);
                    }

                    List<CallCriteriaResourceVO> source = new List<CallCriteriaResourceVO>();

                    for (int i = 0; i < resources.Count; i++)
                    {
                        if (resources[i].Type == (int)Globals.CallCriteria.EmailVOType.Employee)
                        {
                            using (_employeeModel = new EmployeeModel())
                            {
                                source.Add(_employeeModel.GetEmployeeDataForInitialAdvise(resources[i].EmployeeID.Value, _view.CallID.Value));
                            }
                        }
                        else
                        {
                            using (_customerModel = new CustomerModel())
                            {
                                source.Add(_customerModel.GetContactDataForInitialAdvise(resources[i].ContactID.Value, _view.CallID.Value));
                            }
                        }
                    }

                    _view.PersonInitialAdviseGridDataSource = source;
                }
                else
                {
                    IList<EmailVO> resourceList = new List<EmailVO>();
                    List<CallCriteriaResourceVO> source = new List<CallCriteriaResourceVO>();
                    List<int> callCriteriasIDs = new List<int>();

                    using (_callCriteriaModel = new CallCriteriaModel())
                    {
                        resourceList = _callCriteriaModel.ListReceiptsByCallLog(_view.SelectedCallType.ID.ToString(), _view.JobId.Value, null, out callCriteriasIDs);
                        _view.CallCriteriaIDs = callCriteriasIDs;
                    }

                    for (int i = 0; i < resourceList.Count; i++)
                    {
                        if (resourceList[i].Type == (int)Globals.CallCriteria.EmailVOType.Employee)
                        {
                            using (_employeeModel = new EmployeeModel())
                            {
                                source.Add(_employeeModel.GetEmployeeDataForInitialAdvise(resourceList[i].PersonID));
                            }
                        }
                        else
                        {
                            using (_customerModel = new CustomerModel())
                            {
                                source.Add(_customerModel.GetContactDataForInitialAdvise(resourceList[i].PersonID));
                            }
                        }
                    }

                    _view.PersonInitialAdviseGridDataSource = source;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to List .\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to load the Information. Please try again.", false);
            }
        }

        /// <summary>
        /// Sets the visibility of the Initial Advise Panels
        /// </summary>
        public void SetInitialAdvisePanels()
        {
            try
            {
                _view.InitialAdviseVisibility = true;
                _view.ResourceAssignedVisibility = false;
                _view.PersonsAdviseVisibility = false;
                _view.CallLogHistoryPanelVisibility = false;
                _view.CallLogInitialAdviseHistoryVisibility = true;
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to display the Initial Advise Panels.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to display the Initial Advise Panels. Please try again.", false);
            }
        }

        /// <summary>
        /// Fill all fields for the Initial Advise Grid Row
        /// </summary>
        public void FillInitialAdviseControlsFromCallLogResource()
        {
            try
            {
                _viewModel.FillInitialAdvisePersonsGridFields();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to display the Initial Advise information.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to display the Initial Advise information. Please try again.", false);
            }
        }

        #endregion

        #region [ Persons to Advise ]

        /// <summary>
        /// List All added contacts
        /// </summary>
        public void ListAllAddedContacts()
        {
            try
            {
                using (_customerModel = new CustomerModel())
                {
                    IList<CS_Contact> contacts = _customerModel.ListAllAddedContact(_view.CallID.Value, (int)Globals.CallEntry.CallLogResourceType.Contact);

                    for (int i = 0; i < contacts.Count; i++)
                    {
                        CS_Contact contact = contacts[i];
                        _view.SelectedPersons.Add(new CS_CallLogResource() { ContactID = contact.ID, CS_Contact = contact });
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to List all Contacts.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to load the Information. Please try again.", false);
            }
        }

        /// <summary>
        /// List all added resources
        /// </summary>
        public void ListAllAddedEmployess()
        {
            try
            {
                using (_employeeModel = new EmployeeModel())
                {
                    IList<CS_Employee> employees = _employeeModel.ListAllAddedEmployee_CallLogInfo(_view.CallID.Value, (int)Globals.CallEntry.CallLogResourceType.Employee);

                    for (int i = 0; i < employees.Count; i++)
                    {
                        CS_Employee employee = employees[i];
                        _view.SelectedPersons.Add(new CS_CallLogResource() { EmployeeID = employee.ID, CS_Employee = employee });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to List all Employees.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to load the Information. Please try again.", false);
            }
        }

        /// <summary>
        /// Filter the persons to Advise grid based on filter type and value
        /// </summary>
        public void FilterContactOrEmployee()
        {
            try
            {
                List<object> GenericList = new List<object>();

                using (_customerModel = new CustomerModel())
                {
                    using (_employeeModel = new EmployeeModel())
                    {
                        switch (_view.TypeOfPerson)
                        {
                            case Globals.CallEntry.typeOfPerson.Contact:
                                if (_view.CustomerId.HasValue)
                                    GenericList.AddRange(_customerModel.ListAllFilteredContacts(_view.FilteredEmployeeCustomerName, _view.CustomerId.Value));
                                break;
                            case Globals.CallEntry.typeOfPerson.Employee:
                                GenericList.AddRange(_employeeModel.ListAllFilteredEmployee(_view.FilteredEmployeeCustomerName));
                                break;
                            case Globals.CallEntry.typeOfPerson.Both:
                                if (_view.CustomerId.HasValue)
                                    GenericList.AddRange(_customerModel.ListAllFilteredContacts(_view.FilteredEmployeeCustomerName, _view.CustomerId.Value));
                                GenericList.AddRange(_employeeModel.ListAllFilteredEmployee(_view.FilteredEmployeeCustomerName));
                                break;
                        }

                        _view.PersonGridDataSource = GenericList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the contact list.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to load the contact list.", false);
            }
        }

        /// <summary>
        /// Add employee to a selected resources list
        /// </summary>
        public void AddEmployeeToCallEntry()
        {
            try
            {
                using (_employeeModel = new EmployeeModel())
                {
                    CS_Employee employee = _employeeModel.GetEmployee(_view.SelectedEmployeeId);
                    _view.SelectedPersons.Add(new CS_CallLogResource() { EmployeeID = employee.ID, CS_Employee = employee });
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to AddEmployeeToCallEntry.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to load the Information. Please try again.", false);
            }
        }

        /// <summary>
        /// Add Contact to a selected contact list
        /// </summary>
        public void AddContactToCallEntry()
        {
            try
            {
                using (_customerModel = new CustomerModel())
                {
                    CS_Contact contact = _customerModel.GetContactById(_view.SelectedContactId);
                    _view.SelectedPersons.Add(new CS_CallLogResource() { ContactID = contact.ID, CS_Contact = contact });
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to AddContactToCallEntry.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to load the Information. Please try again.", false);
            }
        }

        /// <summary>
        /// Clear data related to persons to advise
        /// </summary>
        public void ClearPersons()
        {
            try
            {
                _view.PersonGridDataSource = new List<object>();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to ClearEmployees.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to load the Information. Please try again.", false);
            }

        }

        /// <summary>
        /// Binds PersonsShopingCart
        /// </summary>
        public void BindPersonsShopingCart()
        {
            List<object> lstPersonsShopingCartDataSource = new List<object>();
            lstPersonsShopingCartDataSource.AddRange(_view.SelectedPersons);
            _view.PersonsShopingCartDataSource = lstPersonsShopingCartDataSource;
        }

        #endregion

        #region [ Resources Assigned ]

        public void ListResourceAssignedFilterOptions()
        {
            try
            {
                using (_resourceAllocationModel = new ResourceAllocationModel())
                {
                    _view.ResourceAssignedFilterValues = _resourceAllocationModel.ListResourcesAssignedFilterValues(_view.SelectedCallType.ShowEmployee.Value, _view.SelectedCallType.ShowEquipment.Value);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to Filter the resources.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to Filter the resources. Please try again.", false);
            }

        }

        /// <summary>
        /// List all Resources related to a specific job record
        /// </summary>
        public void ListAllFilteredResourcesCallLogInfoInfoByJob()
        {
            try
            {
                using (_resourceAllocationModel = new ResourceAllocationModel())
                {
                    _view.ResourceGridDataSource = _resourceAllocationModel.ListFilteredResourcesCallLogInfoByJob(_view.JobId.Value, _view.SelectedCallType.ShowEmployee.Value, _view.SelectedCallType.ShowEquipment.Value, _view.ResourceFilterType, _view.ResourceFilterValue);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to Filter the resources.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to Filter the resources. Please try again.", false);
            }
        }

        /// <summary>
        /// List all resources already assigned to a call log
        /// </summary>
        public void ListAddedResource()
        {
            try
            {
                using (_resourceAllocationModel = new ResourceAllocationModel())
                {
                    _view.SelectedResources = _resourceAllocationModel.ListAddedResource(_view.CallID.Value, (int)Globals.CallEntry.CallLogResourceType.Equipment);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to List Resources.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to load the Information. Please try again.", false);
            }

        }

        /// <summary>
        /// Add resources to a selected resources list
        /// </summary>
        public void AddResourceToCallEntry()
        {
            try
            {
                using (_resourceAllocationModel = new ResourceAllocationModel())
                {
                    _view.SelectedResources.Add(_resourceAllocationModel.GetResource(_view.SelectedResourceId));
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to AddResourceToCallEntry.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to load the Information. Please try again.", false);
            }
        }

        /// <summary>
        /// Clear data related to resources
        /// </summary>
        public void ClearResources()
        {
            try
            {
                _view.ResourceGridDataSource = new List<CS_View_Resource_CallLogInfo>();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to ClearEmployees.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to load the Information. Please try again.", false);
            }

        }

        public void ValidatePrimaryCallType(bool test)
        {
            if (_view.SelectedPrimaryCallType != null)
            {
                if (test)
                    _callLogModel = new CallLogModel(new FakeUnitOfWork());
                else
                    _callLogModel = new CallLogModel();
                _view.ResourceAssignedVisibility = _callLogModel.IsResourceUpdate(_view.SelectedPrimaryCallType);
            }
        }

        #endregion

        #region [ Resources Read-Only ]

        /// <summary>
        /// List all resources related to the current call log on the read-only grid
        /// </summary>
        public void ListResourcesReadOnly()
        {
            try
            {
                if (_view.CallID.HasValue)
                {
                    using (_callLogModel = new CallLogModel())
                    {
                        _view.ResourceReadOnlyDataSource = _callLogModel.ListCallLogResourceByCallEntry(_view.CallID.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the resources related to the Call Log.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to load the resources related to the Call Log. Please try again.", false);
            }
        }

        #endregion

        #region [ Call Log History ]

        /// <summary>
        /// Fills the Call Log History for the Initial Advise Panel
        /// </summary>
        public void ListInitialAdiviseCallLogs()
        {
            try
            {
                using (_callLogModel = new CallLogModel())
                {
                    _view.InitialAdiviseCallLogHistory = _callLogModel.ListInitialAdiviseCallLogsByJob(_view.JobId.Value);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to List Resources.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to load the Information. Please try again.", false);
            }

        }

        /// <summary>
        /// Binds the CallLogHistory Repeater
        /// </summary>
        public void BindCallLogHistory()
        {
            try
            {
                using (_callLogModel = new CallLogModel())
                {
                    _view.CallLogHistory = _callLogModel.ListCallLogByIdList(_view.CallLogHistoryList);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to Bind the Call Log History.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to Bind the Call Log History. Please try again.", false);
            }
        }

        /// <summary>
        /// Gets the Initial advise Note
        /// </summary>
        public void GetInitialAdviseNote()
        {
            try
            {
                using (_callCriteriaModel = new CallCriteriaModel())
                {
                    string initialAdvise = _callCriteriaModel.GetInitalAdviseNote(_view.AdviseNoteIsEmployee, _view.AdviseNoteResourceID);
                    if (!string.IsNullOrEmpty(initialAdvise))
                    {
                        initialAdvise = initialAdvise.Replace(":", ":<Text>").Replace(";", "<BL>");
                        _view.AdviseNote = StringManipulation.TabulateString(initialAdvise);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to get the Resource Initial Advise Note.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to get the Resource Initial Advise Note. Please try again.", false);
            }
        }

        #endregion

        #endregion
    }
}
