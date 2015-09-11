using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core;

namespace Hulcher.OneSource.CustomerService.Web
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AutoComplete : IAutoCompleteWebServiceView
    {
        #region [ Atributes ]

        private AutoCompleteWebServicePresenter _presenter;

        private IList<CS_Customer> _customerList;
        private IList<CS_Contact> _dynamicsContactList;
        private IList<CS_Contact> _customerServiceContactList;
        private IList<CS_JobStatus> _jobStatusList;
        private IList<CS_Job> _jobList;
        private IList<CS_Employee> _employeeList;
        private IList<CS_Contact> _contactList;
        private IList<CS_Division> _divisionList;
        private IList<CS_PriceType> _priceTypeList;
        private IList<CS_JobAction> _jobActionList;
        private IList<CS_City> _cityList;
        private IList<CS_ZipCode> _zipCodeList;
        private IList<CS_State> _stateList;
        private IList<CS_CallType> _callTypeList;
        private IList<CS_EquipmentType> _equipmentTypeList;
        private IList<CS_LocalEquipmentType> _localEquipmentTypeList;
        private IList<CS_Equipment> _equipmentList;


        private string _prefixText;
        private int _count;
        private string _contextKey;

        #endregion

        #region [ Web Methods ]

        [OperationContract]
        public string[] GetCustomerList(string prefixText, int count)
        {
            if (null == _presenter)
                _presenter = new AutoCompleteWebServicePresenter(this);

            _prefixText = prefixText;
            _count = count;

            _presenter.ListCustomerByName();

            if (_customerList.Count > 0)
            {
                string[] returnList = new string[_customerList.Count];
                for (int i = 0; i < returnList.Length; i++)
                    returnList[i] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(_customerList[i].FullCustomerInformation.Trim(), _customerList[i].ID.ToString());
                return returnList;
            }
            else
            {
                string[] emptyReturnList = new string[1];
                emptyReturnList[0] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Not Found", "0");
                return emptyReturnList;
            }
        }

        [OperationContract]
        public string[] GetDynamicsContactList(string prefixText, int count, string contextKey)
        {
            if (null == _presenter)
                _presenter = new AutoCompleteWebServicePresenter(this);

            _prefixText = prefixText;
            _count = count;
            _contextKey = contextKey;

            _presenter.ListDynamicsContactByName();


            if (_dynamicsContactList.Count > 0)
            {
                string[] returnList = new string[_dynamicsContactList.Count];
                for (int i = 0; i < returnList.Length; i++)
                    returnList[i] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(_dynamicsContactList[i].FullContactInformation.Trim(), _dynamicsContactList[i].ID.ToString());
                return returnList;
            }
            else
            {
                string[] emptyReturnList = new string[1];
                emptyReturnList[0] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Not Found", "0");
                return emptyReturnList;
            }
        }

        [OperationContract]
        public string[] GetCustomerServiceContactList(string prefixText, int count, string contextKey)
        {
            if (null == _presenter)
                _presenter = new AutoCompleteWebServicePresenter(this);

            _prefixText = prefixText;
            _count = count;
            _contextKey = contextKey;
            if (string.IsNullOrEmpty(contextKey))
                _contextKey = "0";

            _presenter.ListCustomerServiceContactByName();

            if (_customerServiceContactList.Count > 0)
            {
                string[] returnList = new string[_customerServiceContactList.Count];
                for (int i = 0; i < returnList.Length; i++)
                {
                    //if (_contextKey.Equals("0"))
                    //{
                        if (_customerServiceContactList[i].CS_Customer_Contact.Count > 0)
                        {
                            returnList[i] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
                                string.Format("{0} ({1})",
                                    _customerServiceContactList[i].FullContactInformation.Trim(),
                                    _customerServiceContactList[i].CS_Customer_Contact.First().CS_Customer.Name.Trim()),
                                _customerServiceContactList[i].ID.ToString());
                        }
                        else
                            returnList[i] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(_customerServiceContactList[i].FullContactInformation.Trim(), _customerServiceContactList[i].ID.ToString());
                    //}
                    //else
                    //    returnList[i] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(_customerServiceContactList[i].FullContactInformation.Trim(), _customerServiceContactList[i].ID.ToString());
                }
                return returnList;
            }
            else
            {
                string[] emptyReturnList = new string[1];
                emptyReturnList[0] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Not Found", "0");
                return emptyReturnList;
            }
        }

        [OperationContract]
        public string[] GetEmployeeList(string prefixText, int count, string contextKey)
        {
            if (null == _presenter)
                _presenter = new AutoCompleteWebServicePresenter(this);

            _prefixText = prefixText;
            _count = count;
            _contextKey = contextKey;

            _presenter.ListEmployeeByName();

            if (_employeeList.Count > 0)
            {
                string[] returnList = new string[_employeeList.Count];
                for (int i = 0; i < returnList.Length; i++)
                    returnList[i] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(_employeeList[i].DivisionAndFullName, _employeeList[i].ID.ToString());
                return returnList;
            }
            else
            {
                string[] emptyReturnList = new string[1];
                emptyReturnList[0] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Not Found", "0");
                return emptyReturnList;
            }
        }

        [OperationContract]
        public string[] GetEmployeeListWithDivisionName(string prefixText, int count, string contextKey)
        {
            if (null == _presenter)
                _presenter = new AutoCompleteWebServicePresenter(this);

            _prefixText = prefixText;
            _count = count;
            _contextKey = contextKey;

            _presenter.ListEmployeeByName();

            if (_employeeList.Count > 0)
            {
                string[] returnList = new string[_employeeList.Count];
                for (int i = 0; i < returnList.Length; i++)
                {
                    if (null == EmployeeList[i].CS_Division)
                        returnList[i] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(_employeeList[i].DivisionAndFullName, _employeeList[i].ID + " | 0");
                    else
                        returnList[i] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(_employeeList[i].DivisionAndFullName, _employeeList[i].ID + " | " + EmployeeList[i].CS_Division.Name);
                }
                return returnList;
            }
            else
            {
                string[] emptyReturnList = new string[1];
                emptyReturnList[0] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Not Found", "0");
                return emptyReturnList;
            }
        }

        [OperationContract]
        public string[] GetProjectManagerList(string prefixText, int count)
        {
            if (null == _presenter)
                _presenter = new AutoCompleteWebServicePresenter(this);

            _prefixText = prefixText;
            _count = count;
         
            _presenter.ListProjectManager();

            if (ProjectManagerList.Count > 0)
            {
                string[] returnList = new string[ProjectManagerList.Count];
                for (int i = 0; i < returnList.Length; i++)
                {
                    if (null == ProjectManagerList[i].CS_Division)
                        returnList[i] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(ProjectManagerList[i].DivisionAndFullName, ProjectManagerList[i].ID.ToString());
                    else
                        returnList[i] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(ProjectManagerList[i].DivisionAndFullName, ProjectManagerList[i].ID.ToString());
                }
                return returnList;
            }
            else
            {
                string[] emptyReturnList = new string[1];
                emptyReturnList[0] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Not Found", "0");
                return emptyReturnList;
            }
        }

        [OperationContract]
        public string[] GetAllContactList(string prefixText, int count, string contextKey)
        {
            if (null == _presenter)
                _presenter = new AutoCompleteWebServicePresenter(this);

            _prefixText = prefixText;
            _count = count;
            _contextKey = contextKey;
            if (string.IsNullOrEmpty(contextKey))
                _contextKey = "0";

            _presenter.ListCustomerServiceContactByName();

            if (_customerServiceContactList.Count > 0)
            {
                string[] returnList = new string[_customerServiceContactList.Count];
                for (int i = 0; i < returnList.Length; i++)
                {
                    if (_contextKey.Equals("0"))
                    {
                        if (_customerServiceContactList[i].CS_Customer_Contact.Count > 0)
                        {
                            returnList[i] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
                                string.Format("{0} ({1})",
                                    _customerServiceContactList[i].Name.Trim(),
                                    _customerServiceContactList[i].CS_Customer_Contact.First().CS_Customer.Name.Trim()),
                                _customerServiceContactList[i].ID.ToString());
                        }
                        else
                            returnList[i] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(_customerServiceContactList[i].Name.Trim(), _customerServiceContactList[i].ID.ToString());
                    }
                    else
                        returnList[i] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(_customerServiceContactList[i].Name.Trim(), _customerServiceContactList[i].ID.ToString());
                }
                return returnList;
            }
            else
            {
                string[] emptyReturnList = new string[1];
                emptyReturnList[0] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Not Found", "0");
                return emptyReturnList;
            }
        }

        [OperationContract]
        public string[] GetDivisionList(string prefixText, int count)
        {
            if (null == _presenter)
                _presenter = new AutoCompleteWebServicePresenter(this);

            _prefixText = prefixText;
            _count = count;

            _presenter.ListDivisionByName();

            if (_divisionList.Count > 0)
            {
                string[] returnList = new string[_divisionList.Count];
                for (int i = 0; i < returnList.Length; i++)
                    returnList[i] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(_divisionList[i].ExtendedDivisionName.Trim(), _divisionList[i].ID.ToString());
                return returnList;
            }
            else
            {
                string[] emptyReturnList = new string[1];
                emptyReturnList[0] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Not Found", "0");
                return emptyReturnList;
            }
        }

        [OperationContract]
        public string[] GetPriceTypeList(string prefixText, int count, string contextKey)
        {
            if (null == _presenter)
                _presenter = new AutoCompleteWebServicePresenter(this);

            _prefixText = prefixText;
            _count = count;
            _contextKey = contextKey;

            _presenter.ListPriceTypeByName();

            if (_priceTypeList.Count > 0)
            {
                string[] returnList = new string[_priceTypeList.Count];
                for (int i = 0; i < returnList.Length; i++)
                    returnList[i] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(_priceTypeList[i].Description.Trim(), _priceTypeList[i].ID.ToString());
                return returnList;
            }
            else
            {
                string[] emptyReturnList = new string[1];
                emptyReturnList[0] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Not Found", "0");
                return emptyReturnList;
            }
        }

        [OperationContract]
        public string[] GetJobActionList(string prefixText, int count, string contextKey)
        {
            if (null == _presenter)
                _presenter = new AutoCompleteWebServicePresenter(this);

            _prefixText = prefixText;
            _count = count;
            _contextKey = contextKey;

            _presenter.ListJobActionByName();

            if (_jobActionList.Count > 0)
            {
                string[] returnList = new string[_jobActionList.Count];
                for (int i = 0; i < returnList.Length; i++)
                    returnList[i] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(_jobActionList[i].Description, _jobActionList[i].ID.ToString());
                return returnList;
            }
            else
            {
                string[] emptyReturnList = new string[1];
                emptyReturnList[0] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Not Found", "0");
                return emptyReturnList;
            }
        }

        [OperationContract]
        public string[] GetJobStatusList(string prefixText, int count, string contextKey)
        {
            if (null == _presenter)
                _presenter = new AutoCompleteWebServicePresenter(this);

            _prefixText = prefixText;
            _count = count;
            _contextKey = contextKey;

            _presenter.ListJobStatusByDescription();

            if (_jobStatusList.Count > 0)
            {
                string[] returnList = new string[_jobStatusList.Count];

                for (int i = 0; i < returnList.Length; i++)
                {
                    returnList[i] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(_jobStatusList[i].Description.Trim(), _jobStatusList[i].ID.ToString());
                }

                return returnList;
            }
            else
            {
                string[] emptyReturnList = new string[1];

                emptyReturnList[0] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Not Found", "0");

                return emptyReturnList;
            }
        }

        [OperationContract]
        public string[] GetJobStatusListForJobRecord(string prefixText, int count, string contextKey)
        {
            if (null == _presenter)
                _presenter = new AutoCompleteWebServicePresenter(this);

            _prefixText = prefixText;
            _count = count;
            _contextKey = contextKey;

            _presenter.ListJobStatusByDescription();

            if (_jobStatusList.Count > 0 && _jobStatusList.Any(w => w.ID == (int)Globals.JobRecord.JobStatus.ClosedHold))
            {
                _jobStatusList.Remove(_jobStatusList.FirstOrDefault());
            }

            if (_jobStatusList.Count > 0)
            {
                string[] returnList = new string[_jobStatusList.Count];

                for (int i = 0; i < returnList.Length; i++)
                {
                    returnList[i] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(_jobStatusList[i].Description.Trim(), _jobStatusList[i].ID.ToString());
                }

                return returnList;
            }
            else
            {
                string[] emptyReturnList = new string[1];

                emptyReturnList[0] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Not Found", "0");

                return emptyReturnList;
            }
        }

        [OperationContract]
        public string[] GetCityList(string prefixText, int count, string contextKey)
        {
            if (null == _presenter)
                _presenter = new AutoCompleteWebServicePresenter(this);

            _prefixText = prefixText;
            _count = count;
            _contextKey = contextKey;

            _presenter.ListCityByNameAndState();

            if (_cityList.Count > 0)
            {
                string[] returnList = new string[_cityList.Count];
                for (int i = 0; i < returnList.Length; i++)
                    returnList[i] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(_cityList[i].CityStateInformation, _cityList[i].ID.ToString());
                return returnList;
            }
            else
            {
                string[] emptyReturnList = new string[1];
                emptyReturnList[0] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Not Found", "0");
                return emptyReturnList;
            }
        }

        [OperationContract]
        public string[] GetZipCodeList(string prefixText, int count, string contextKey)
        {
            if (null == _presenter)
                _presenter = new AutoCompleteWebServicePresenter(this);

            _prefixText = prefixText;
            _count = count;
            _contextKey = contextKey;

            _presenter.ListZipCodesByNameAndCity();

            if (_zipCodeList.Count > 0)
            {
                string[] returnList = new string[_zipCodeList.Count];
                for (int i = 0; i < returnList.Length; i++)
                    returnList[i] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(_zipCodeList[i].ZipCodeNameEdited.Trim(), _zipCodeList[i].ID.ToString());
                return returnList;
            }
            else
            {
                string[] emptyReturnList = new string[1];
                emptyReturnList[0] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Not Found", "0");
                return emptyReturnList;
            }
        }

        [OperationContract]
        public string[] GetStateList(string prefixText, int count, string contextKey)
        {
            if (null == _presenter)
                _presenter = new AutoCompleteWebServicePresenter(this);

            _prefixText = prefixText;
            _count = count;
            _contextKey = contextKey;

            _presenter.ListStatesByAcronym();

            if (_stateList.Count > 0)
            {
                string[] returnList = new string[_stateList.Count];
                for (int i = 0; i < returnList.Length; i++)
                    returnList[i] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
                        _stateList[i].AcronymName, _stateList[i].ID.ToString());
                return returnList;
            }
            else
            {
                string[] emptyReturnList = new string[1];
                emptyReturnList[0] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Not Found", "0");
                return emptyReturnList;
            }
        }

        [OperationContract]
        public string[] GetStateByDivisionList(string prefixText, int count)
        {
            if (null == _presenter)
                _presenter = new AutoCompleteWebServicePresenter(this);

            _prefixText = prefixText;
            _count = count;

            _presenter.ListStatesByAcronymAndDivision();

            if (_stateList.Count > 0)
            {
                string[] returnList = new string[_stateList.Count];
                for (int i = 0; i < returnList.Length; i++)
                    returnList[i] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
                        _stateList[i].CountryAcronymName, _stateList[i].ID.ToString());
                return returnList;
            }
            else
            {
                string[] emptyReturnList = new string[1];
                emptyReturnList[0] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Not Found", "0");
                return emptyReturnList;
            }
        }

        [OperationContract]
        public string[] GetJobNumberList(string prefixText, int count, string contextKey)
        {
            if (null == _presenter)
                _presenter = new AutoCompleteWebServicePresenter(this);

            _prefixText = prefixText;
            _count = count;
            _contextKey = contextKey;

            _presenter.ListJobNumberByDescription();

            if (_jobList.Count > 0)
            {
                string[] returnList = new string[_jobList.Count];

                for (int i = 0; i < returnList.Length; i++)
                {
                    //Returns Job Number in case of a Active job, Internal Tracking otherwise (ID = 1 for a Active Job)
                    returnList[i] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(_jobList[i].PrefixedNumber, _jobList[i].ID.ToString());
                }

                return returnList;
            }
            else
            {
                string[] emptyReturnList = new string[1];

                emptyReturnList[0] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Not Found", "0");

                return emptyReturnList;
            }
        }

        [OperationContract]
        public string[] GetJobNumberListWithGeneral(string prefixText, int count, string contextKey)
        {
            if (null == _presenter)
                _presenter = new AutoCompleteWebServicePresenter(this);

            _prefixText = prefixText;
            _count = count;
            _contextKey = contextKey;

            _presenter.ListJobNumberByDescriptionWithGeneral();

            if (_jobList.Count > 0)
            {
                string[] returnList = new string[_jobList.Count];

                for (int i = 0; i < returnList.Length; i++)
                {
                    //Returns Job Number in case of a Active job, Internal Tracking otherwise (ID = 1 for a Active Job)
                    returnList[i] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(_jobList[i].PrefixedNumber, _jobList[i].ID.ToString());
                }

                return returnList;
            }
            else
            {
                string[] emptyReturnList = new string[1];

                emptyReturnList[0] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Not Found", "0");

                return emptyReturnList;
            }
        }

        [OperationContract]
        public string[] GetBillableJobNumberList(string prefixText, int count, string contextKey)
        {
            if (null == _presenter)
                _presenter = new AutoCompleteWebServicePresenter(this);

            _prefixText = prefixText;
            _count = count;
            _contextKey = contextKey;

            _presenter.ListBillableJobNumberByDescription();

            if (_jobList.Count > 0)
            {
                string[] returnList = new string[_jobList.Count];

                for (int i = 0; i < returnList.Length; i++)
                {
                    //Returns Job Number in case of a Active job, Internal Tracking otherwise (ID = 1 for a Active Job)
                    returnList[i] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(_jobList[i].PrefixedNumber, _jobList[i].ID.ToString());
                }

                return returnList;
            }
            else
            {
                string[] emptyReturnList = new string[1];

                emptyReturnList[0] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Not Found", "0");

                return emptyReturnList;
            }
        }

        [OperationContract]
        public string[] GetCallTypeList(string prefixText, int count, string contextKey)
        {
            if (null == _presenter)
                _presenter = new AutoCompleteWebServicePresenter(this);

            _prefixText = prefixText;
            _count = count;
            _contextKey = contextKey;

            _presenter.ListCallTypes();

            if (_callTypeList.Count > 0)
            {
                string[] returnList = new string[_callTypeList.Count];

                for (int i = 0; i < returnList.Length; i++)
                {
                    returnList[i] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(_callTypeList[i].Description, _callTypeList[i].ID.ToString());
                }

                return returnList;
            }
            else
            {
                string[] emptyReturnList = new string[1];

                emptyReturnList[0] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Not Found", "0");

                return emptyReturnList;
            }
        }

        [OperationContract]
        public string[] GetEquipmentTypeList(string prefixText, int count, string contextKey)
        {
            if (null == _presenter)
                _presenter = new AutoCompleteWebServicePresenter(this);

            _prefixText = prefixText;
            _count = count;
            _contextKey = contextKey;

            _presenter.ListAllEquipmentTypeByName();

            if (_equipmentTypeList.Count > 0)
            {
                string[] returnList = new string[_equipmentTypeList.Count];

                for (int i = 0; i < returnList.Length; i++)
                {
                    returnList[i] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(_equipmentTypeList[i].CompleteName, _equipmentTypeList[i].ID.ToString());
                }

                return returnList;
            }
            else
            {
                string[] emptyReturnList = new string[1];

                emptyReturnList[0] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Not Found", "0");

                return emptyReturnList;
            }
        }

        [OperationContract]
        public string[] GetLocalEquipmentTypeList(string prefixText, int count, string contextKey)
        {
            if (null == _presenter)
                _presenter = new AutoCompleteWebServicePresenter(this);

            _prefixText = prefixText;
            _count = count;
            _contextKey = contextKey;

            _presenter.ListAllLocalEquipmentTypeByName();

            if (_localEquipmentTypeList.Count > 0)
            {
                string[] returnList = new string[_localEquipmentTypeList.Count];

                for (int i = 0; i < returnList.Length; i++)
                {
                    returnList[i] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(_localEquipmentTypeList[i].Name, _localEquipmentTypeList[i].ID.ToString());
                }

                return returnList;
            }
            else
            {
                string[] emptyReturnList = new string[1];

                emptyReturnList[0] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Not Found", "0");

                return emptyReturnList;
            }
        }

        [OperationContract]
        public string[] GetEquipmentList(string prefixText, int count, string contextKey)
        {
            if (null == _presenter)
                _presenter = new AutoCompleteWebServicePresenter(this);

            _prefixText = prefixText;
            _count = count;
            _contextKey = contextKey;

            _presenter.ListAllEquipmentByEqType();

            if (_equipmentList.Count > 0)
            {
                string[] returnList = new string[_equipmentList.Count];

                for (int i = 0; i < returnList.Length; i++)
                {
                    returnList[i] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(_equipmentList[i].CompleteName, _equipmentList[i].ID.ToString());
                }

                return returnList;
            }
            else
            {
                string[] emptyReturnList = new string[1];

                emptyReturnList[0] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Not Found", "0");

                return emptyReturnList;
            }

        }
        #endregion

        #region [ Properties ]

        public IList<CS_Customer> CustomerList
        {
            get { return _customerList; }
            set { _customerList = value; }
        }

        public IList<CS_Contact> DynamicsContactList
        {
            get { return _dynamicsContactList; }
            set { _dynamicsContactList = value; }
        }

        public IList<CS_Contact> CustomerServiceContactList
        {
            get { return _customerServiceContactList; }
            set { _customerServiceContactList = value; }
        }

        public IList<CS_JobStatus> JobStatusList
        {
            get { return _jobStatusList; }
            set { _jobStatusList = value; }
        }

        public IList<CS_Employee> EmployeeList
        {
            get { return _employeeList; }
            set { _employeeList = value; }
        }

        public IList<CS_Contact> ContactList
        {
            get { return _contactList; }
            set { _contactList = value; }
        }

        public IList<CS_Division> DivisionList
        {
            get { return _divisionList; }
            set { _divisionList = value; }
        }

        public IList<CS_PriceType> PriceTypeList
        {
            get { return _priceTypeList; }
            set { _priceTypeList = value; }
        }

        public IList<CS_JobAction> JobActionList
        {
            get
            {
                return _jobActionList;
            }
            set
            {
                _jobActionList = value;
            }
        }

        public IList<CS_Job> JobList
        {
            get
            {
                return _jobList;
            }
            set
            {
                _jobList = value;
            }
        }

        public IList<CS_City> CityList
        {
            get { return _cityList; }
            set { _cityList = value; }
        }

        public IList<CS_ZipCode> ZipCodeList
        {
            get { return _zipCodeList; }
            set { _zipCodeList = value; }
        }

        public IList<CS_State> StateList
        {
            get { return _stateList; }
            set { _stateList = value; }
        }

        public IList<CS_CallType> CallTypeList
        {
            get { return _callTypeList; }
            set { _callTypeList = value; }
        }

        public IList<CS_EquipmentType> EquipmentTypeList
        {
            get
            {
                return _equipmentTypeList;
            }
            set
            {
                _equipmentTypeList = value;
            }
        }

        public IList<CS_LocalEquipmentType> LocalEquipmentTypeList
        {
            get
            {
                return _localEquipmentTypeList;
            }
            set
            {
                _localEquipmentTypeList = value;
            }
        }

        public IList<CS_Equipment> EquipmentType
        {
            get
            {
                return _equipmentList;
            }
            set
            {
                _equipmentList = value;
            }
        }

        public string PrefixText
        {
            get { return _prefixText; }
            set { _prefixText = value; }
        }

        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }

        public string ContextKey
        {
            get { return _contextKey; }
            set { _contextKey = value; }
        }

        public string FilterText
        {
            get;
            set;
        }

        public IList<CS_Employee> ProjectManagerList
        {
            get;
            set;
        }
        #endregion
    }
}
