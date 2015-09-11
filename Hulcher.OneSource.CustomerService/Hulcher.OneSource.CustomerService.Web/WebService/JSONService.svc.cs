using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;

using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.DataContext.VO;

namespace Hulcher.OneSource.CustomerService.Web.WebService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class JSONService : IJSONService
    {
        #region [ Attributes ]

        private JSONServicePresenter _presenter;

        #endregion

        #region [ Properties ]

        #region [ Common Properties - Auto Complete ]

        public Globals.SampleDataObject ReturnDataObject { get; set; }

        public string FilterText { get; set; }

        public string ContextKey { get; set; }

        public string FilterValue { get; set; }

        public string PrefixText { get; set; }

        #endregion

        #region [ Dashboard Properties ]

        public IList<CS_CallLogCallCriteriaEmail> CallLogCallCriteriaEmail { get; set; }

        #endregion

        #region [ Job Properties ]

        public int JobId { get; set; }

        public IList<CS_JobStatus> JobStatusList { get; set; }

        public IList<CS_PriceType> PriceTypeList { get; set; }

        public IList<CS_JobAction> JobActionList { get; set; }

        public IList<CS_Job> JobList { get; set; }

        public IList<PresetNotificationVO> PresetNotificationList { get; set; }

        #endregion

        #region [ Call Log Properties ]

        public int CallLogId { get; set; }

        public IList<CS_CallType> CallTypeList { get; set; }

        #endregion

        #region [ Division Properties ]

        public IList<CS_Division> DivisionList { get; set; }

        #endregion

        #region [ Customer and Contact Properties ]

        public int ContactId { get; set; }

        public IList<CS_Customer> CustomerList { get; set; }

        public IList<CS_Contact> DynamicsContactList { get; set; }

        public IList<CS_Contact> CustomerServiceContactList { get; set; }

        public IList<CS_Contact> ContactList { get; set; }

        public int CustomerId { get; set; }

        public bool IsCostumerCollection { get; set; }

        public string OperatorAlertMessage { get; set; }

        public string CustomerName { get; set; }

        public  bool IsNeWCompany {get;set;}
        #endregion

        #region [ Employee Properties ]

        public int EmployeeId { get; set; }

        public IList<CS_Employee> EmployeeList { get; set; }

        public IList<CS_Employee> ProjectManagerList
        {
            get;
            set;
        }
        #endregion

        #region [ Location Properties ]

        public int CityId { get; set; }

        public Globals.StateAndCountryDataObject StateAndCountryDataObject { get; set; }

        public IList<CS_City> CityList { get; set; }

        public IList<CS_ZipCode> ZipCodeList { get; set; }

        public IList<CS_State> StateList { get; set; }

        #endregion

        #region [ Equipment Properties ]

        public IList<CS_View_ConflictedEquipmentCombos> EquipmentComboNotificationList { get; set; }

        public IList<CS_LocalEquipmentType> LocalEquipmentTypeList { get; set; }

        public IList<CS_EquipmentPermit> EquipmentPermitNotificationList { get; set; }

        public IList<CS_EquipmentType> EquipmentTypeList { get; set; }

        public IList<CS_Equipment> EquipmentList { get; set; }

        public int EquipmentId { get; set; }
        #endregion

        #region [ Off Call Properties ]

        public IList<CS_EmployeeOffCallHistory> OffCallNotificationList { get; set; }

        #endregion

        #region [ Map Plotting Properties ]

        public IList<Globals.MapPlotDataObject> MapPlottingObjectsList { get; set; }

        public Globals.MapPlotRequestDataObject MapPlottingFilter { get; set; }

        #endregion

        #endregion

        #region [ Services ]

        #region [ Dashboard Services ]

        public Globals.EmailDataDataObject GetEmailData(int callLogId)
        {
            _presenter = new JSONServicePresenter(this);
            CallLogId = callLogId;
            _presenter.GetEmailDataByCallLogId();

            string innerHtml = "<div>";

            if (CallLogCallCriteriaEmail.Count == 0)
                innerHtml += "<div style='text-align: center;'>No Data Found</div>";
            else
                for (int i = 0; i < CallLogCallCriteriaEmail.Count; i++)
                {
                    CS_CallLogCallCriteriaEmail email = CallLogCallCriteriaEmail[i];

                    innerHtml += "<div style='width: 100%; display: inline-block;'>";
                    innerHtml += "<div style='text-align: right; width: 30%; height: 100%; display: inline-block; float: left'><b>Name:</b></div>";
                    innerHtml += "<div style='text-align: left; width: 68%; height: 100%; display: inline-block; float: right'>";
                    innerHtml += email.Name;
                    innerHtml += "</div>";
                    innerHtml += "<div style='text-align: right; width: 30%; height: 100%; display: inline-block; float: left'><b>Email:</b></div>";
                    innerHtml += "<div style='text-align: left; width: 68%; height: 100%; display: inline-block; float: right'>";
                    innerHtml += email.Email;
                    innerHtml += "</div>";
                    innerHtml += "<div style='text-align: right; width: 30%; height: 100%; display: inline-block; float: left'><b>Status Date:</b></div>";
                    innerHtml += "<div style='text-align: left; width: 68%; height: 100%; display: inline-block; float: right'>";
                    innerHtml += email.StatusDate.ToString();
                    innerHtml += "</div>";
                    innerHtml += "<div style='text-align: right; width: 30%; height: 100%; display: inline-block; float: left'><b>Status:</b></div>";
                    innerHtml += "<div style='text-align: left; width: 68%; height: 100%; display: inline-block; float: right'>";
                    switch ((Globals.CallCriteria.CallCriteriaEmailStatus)email.Status)
                    {
                        case Globals.CallCriteria.CallCriteriaEmailStatus.Pending:
                            innerHtml += "Pending";
                            break;
                        case Globals.CallCriteria.CallCriteriaEmailStatus.Sent:
                            innerHtml += "Sent";
                            break;
                        case Globals.CallCriteria.CallCriteriaEmailStatus.Error:
                            innerHtml += "Error";
                            break;
                        case Globals.CallCriteria.CallCriteriaEmailStatus.ConfirmationReceived:
                            innerHtml += "Sent and Confirmed";
                            break;
                        case Globals.CallCriteria.CallCriteriaEmailStatus.ReadConfirmationReceived:
                            innerHtml += "Sent, Confirmed and Read";
                            break;
                        default:
                            innerHtml += "Pending";
                            break;
                    }
                    innerHtml += "</div>";
                    innerHtml += "</div>";

                    if (i != CallLogCallCriteriaEmail.Count - 1)
                        innerHtml += "<HR>";
                }

            innerHtml += "</div>";

            Globals.EmailDataDataObject retObject = new Globals.EmailDataDataObject(innerHtml);

            return retObject;
        }

        #endregion

        #region [ Job Services ]

        public Globals.SampleDataObject GetJobStatusListJSON(string filterText, string contextKey)
        {
            if (null == _presenter)
                _presenter = new JSONServicePresenter(this);

            FilterText = filterText;
            ContextKey = contextKey;

            _presenter.ListJobStatusByDescription();

            if (null != JobStatusList && JobStatusList.Count == 1)
            {
                return new Globals.SampleDataObject(JobStatusList[0].ID, JobStatusList[0].Description);
            }
            else
            {
                return new Globals.SampleDataObject(0, filterText);
            }
        }

        public Globals.SampleDataObject GetJobStatusListForJobRecordJSON(string filterText, string contextKey)
        {
            if (null == _presenter)
                _presenter = new JSONServicePresenter(this);

            FilterText = filterText;
            FilterValue = contextKey;

            _presenter.ListJobStatusByDescription();

            if (JobStatusList.Count > 0 && JobStatusList.Any(w => w.ID == (int)Globals.JobRecord.JobStatus.ClosedHold))
            {
                JobStatusList.Remove(JobStatusList.FirstOrDefault());
            }

            if (null != JobStatusList && JobStatusList.Count == 1)
            {
                return new Globals.SampleDataObject(JobStatusList[0].ID, JobStatusList[0].Description);
            }
            else
            {
                return new Globals.SampleDataObject(0, filterText);
            }
        }


        public Globals.SampleDataObject GetPriceTypeListJSON(string filterText, string contextKey)
        {
            if (null == _presenter)
                _presenter = new JSONServicePresenter(this);

            FilterText = filterText;
            FilterValue = contextKey;

            _presenter.ListPriceTypeByName();

            if (PriceTypeList.Count == 1)
            {
                return new Globals.SampleDataObject(PriceTypeList[0].ID, PriceTypeList[0].Description.Trim());
            }
            else
            {
                return new Globals.SampleDataObject(0, filterText);
            }
        }

        public Globals.SampleDataObject GetJobActionListJSON(string filterText, string contextKey)
        {
            if (null == _presenter)
                _presenter = new JSONServicePresenter(this);

            FilterText = filterText;

            _presenter.ListJobActionByName();

            if (JobActionList.Count == 1)
            {
                return new Globals.SampleDataObject(JobActionList[0].ID, JobActionList[0].Description);
            }
            else
            {
                return new Globals.SampleDataObject(0, filterText);
            }
        }

        public Globals.SampleDataObject GetJobNumberListJSON(string filterText, string contextKey)
        {
            if (null == _presenter)
                _presenter = new JSONServicePresenter(this);

            FilterText = filterText;
            ContextKey = contextKey;

            _presenter.ListJobNumberByDescription();

            if (JobList.Count == 1)
            {
                return new Globals.SampleDataObject(JobList[0].ID, JobList[0].PrefixedNumber);
            }
            else
            {
                return new Globals.SampleDataObject(0, filterText);
            }
        }

        public Globals.SampleDataObject GetJobNumberListWithGeneralJSON(string filterText, string contextKey)
        {
            if (null == _presenter)
                _presenter = new JSONServicePresenter(this);

            FilterText = filterText;

            _presenter.ListJobNumberByDescriptionWithGeneral();

            if (JobList.Count == 1)
            {
                return new Globals.SampleDataObject(JobList[0].ID, JobList[0].PrefixedNumber);
            }
            else
            {
                return new Globals.SampleDataObject(0, filterText);
            }
        }

        public Globals.SampleDataObject GetBillableJobNumberListJSON(string filterText, string contextKey)
        {
            if (null == _presenter)
                _presenter = new JSONServicePresenter(this);

            FilterText = filterText;

            _presenter.ListBillableJobNumberByDescription();

            if (JobList.Count == 1)
            {
                return new Globals.SampleDataObject(JobList[0].ID, JobList[0].PrefixedNumber);
            }
            else
            {
                return new Globals.SampleDataObject(0, filterText);
            }
        }

        public Globals.SampleDataObject GetJobStatus(int jobId)
        {
            if (null == _presenter)
                _presenter = new JSONServicePresenter(this);

            JobId = jobId;

            _presenter.GetJobStatus();

            if (JobStatusList.Count == 1)
            {
                return new Globals.SampleDataObject(JobStatusList[0].ID, JobStatusList[0].Description);
            }
            else
            {
                return new Globals.SampleDataObject(0, "");
            }
        }

        public Globals.PresetNotificationDataObject[] GetPresetNotificationList()
        {
            _presenter = new JSONServicePresenter(this);
            _presenter.GetPresetNotificationList();

            if (null != PresetNotificationList)
            {
                Globals.PresetNotificationDataObject[] returnList = new Globals.PresetNotificationDataObject[PresetNotificationList.Count];
                for (int i = 0; i < PresetNotificationList.Count; i++)
                {
                    returnList[i] = new Globals.PresetNotificationDataObject()
                    {
                        JobId = PresetNotificationList[i].JobId,
                        JobNumber = PresetNotificationList[i].JobNumber,
                        PresetDate = PresetNotificationList[i].PresetDate.ToString("MM/dd/yyyy")
                    };
                    if (PresetNotificationList[i].PresetTime.HasValue)
                        returnList[i].PresetDate += string.Format(" {0}", PresetNotificationList[i].PresetTime.Value.ToString(@"hh\:mm"));
                }
                return returnList;
            }
            else
                return null;
        }

        #endregion

        #region [ Call Log Services ]

        public Globals.SampleDataObject GetCallTypeListJSON(string filterText, string contextKey)
        {
            if (null == _presenter)
                _presenter = new JSONServicePresenter(this);

            FilterText = filterText;

            _presenter.ListCallTypeListByDescription();

            if (CallTypeList.Count == 1)
            {
                return new Globals.SampleDataObject(CallTypeList[0].ID, CallTypeList[0].Description);
            }
            else
            {
                return new Globals.SampleDataObject(0, filterText);
            }
        }

        #endregion

        #region [ Division Services ]

        public Globals.SampleDataObject GetDivision(int employeeId)
        {
            _presenter = new JSONServicePresenter(this);
            EmployeeId = employeeId;
            _presenter.GetDivisionByEmployee();
            return ReturnDataObject;
        }

        public Globals.SampleDataObject GetDivisionListJSON(string filterText, string contextKey)
        {
            if (null == _presenter)
                _presenter = new JSONServicePresenter(this);

            FilterText = filterText;

            _presenter.ListDivisionByName();

            if (DivisionList.Count == 1)
            {
                return new Globals.SampleDataObject(DivisionList[0].ID, DivisionList[0].ExtendedDivisionName.Trim());
            }
            else
            {
                return new Globals.SampleDataObject(0, filterText);
            }
        }

        #endregion

        #region [ Customer and Contact Services ]

        public Globals.SampleDataObject GetCustomer(int contactId)
        {
            _presenter = new JSONServicePresenter(this);
            ContactId = contactId;
            _presenter.GetCustomerByContact();
            return ReturnDataObject;
        }

        public Globals.SampleDataObject GetCustomerListJSON(string filterText, string contextKey)
        {
            if (null == _presenter)
                _presenter = new JSONServicePresenter(this);

            FilterText = filterText;

            _presenter.ListCustomerByNameJSON();

            if (null != CustomerList && CustomerList.Count == 1)
            {
                return new Globals.SampleDataObject(CustomerList[0].ID, CustomerList[0].FullCustomerInformation);
            }
            else
            {
                return new Globals.SampleDataObject(0, filterText);
            }
        }

        public Globals.SampleDataObject GetDynamicsContactListJSON(string filterText, string contextKey)
        {
            if (null == _presenter)
                _presenter = new JSONServicePresenter(this);

            FilterText = filterText;
            FilterValue = contextKey;

            _presenter.ListDynamicsContactByName();


            if (DynamicsContactList.Count == 1)
            {
                return new Globals.SampleDataObject(DynamicsContactList[0].ID, DynamicsContactList[0].FullContactInformation.Trim());
            }
            else
            {
                return new Globals.SampleDataObject(0, filterText);
            }
        }

        public Globals.SampleDataObject GetCustomerServiceContactListJSON(string filterText, string contextKey)
        {
            if (null == _presenter)
                _presenter = new JSONServicePresenter(this);

            FilterText = filterText;
            FilterValue = contextKey;
            if (string.IsNullOrEmpty(contextKey))
                FilterValue = "0";

            _presenter.ListCustomerServiceContactByName();

            if (CustomerServiceContactList.Count == 1)
            {
                return new Globals.SampleDataObject(CustomerServiceContactList[0].ID, string.Format("{0} ({1})", CustomerServiceContactList[0].FullContactInformation.Trim(), CustomerServiceContactList[0].CS_Customer_Contact.First().CS_Customer.Name.Trim()));
            }
            else
            {
                return new Globals.SampleDataObject(0, filterText);
            }
        }

        public Globals.SampleDataObject GetAllContactListJSON(string filterText, string contextKey)
        {
            if (null == _presenter)
                _presenter = new JSONServicePresenter(this);

            FilterText = filterText;
            FilterValue = contextKey;

            if (string.IsNullOrEmpty(contextKey))
                FilterValue = "0";

            _presenter.ListCustomerServiceContactByName();

            if (CustomerServiceContactList.Count == 1)
            {
                Globals.SampleDataObject objectData;

                if (FilterValue.Equals("0"))
                {

                    if (CustomerServiceContactList[0].CS_Customer_Contact.Count > 0)
                    {
                        objectData = new Globals.SampleDataObject(CustomerServiceContactList[0].ID, string.Format("{0} ({1})", CustomerServiceContactList[0].Name.Trim(), CustomerServiceContactList[0].CS_Customer_Contact.First().CS_Customer.Name.Trim()));
                    }
                    else
                    {
                        objectData = new Globals.SampleDataObject(CustomerServiceContactList[0].ID, CustomerServiceContactList[0].Name.Trim());
                    }
                }
                else
                {
                    objectData = new Globals.SampleDataObject(CustomerServiceContactList[0].ID, CustomerServiceContactList[0].Name.Trim());
                }

                return objectData;
            }
            else
            {
                return new Globals.SampleDataObject(0, filterText);
            }
        }

        public Globals.CollectionCustomer IsCustomerCollection(int customerId)
        {
            _presenter = new JSONServicePresenter(this);
            CustomerId = customerId;
            _presenter.CustomerIsCollection();
            return new Globals.CollectionCustomer() { Collection = IsCostumerCollection };
        }

        public Globals.OperatorAlertCustomer CustomerHasOperatorAlert(int customerId)
        {
            _presenter = new JSONServicePresenter(this);
            CustomerId = customerId;
            _presenter.CustomerHasOperatorAlert();
            return new Globals.OperatorAlertCustomer() { AlertMessage = OperatorAlertMessage };
        }

        public Globals.ExistingCompany CheckExistingCustomer(string customerName)
        {
            _presenter = new JSONServicePresenter(this);
            CustomerName = customerName;
            _presenter.CheckExistingCustomer();
            return new Globals.ExistingCompany() { AlreadyExistsCompany = IsNeWCompany };
        }

        #endregion

        #region [ Employee Services ]

        public Globals.SampleDataObject GetEmployeeListJSON(string filterText, string contextKey)
        {
            if (null == _presenter)
                _presenter = new JSONServicePresenter(this);

            FilterText = IgnoreCoverage(filterText);
            FilterValue = contextKey;

            _presenter.ListEmployeeByName();

            if (EmployeeList.Count == 1)
            {
                return new Globals.SampleDataObject(EmployeeList[0].ID, EmployeeList[0].DivisionAndFullName);
            }
            else
            {
                return new Globals.SampleDataObject(0, filterText);
            }
        }

        public Globals.StringDataObject GetEmployeeListWithDivisionNameJSON(string filterText, string contextKey)
        {
            if (null == _presenter)
                _presenter = new JSONServicePresenter(this);

            FilterText = IgnoreCoverage(filterText);
            FilterValue = contextKey;

            _presenter.ListEmployeeByName();

            if (EmployeeList.Count > 0)
            {
                Globals.StringDataObject dataObject;

                if (null == EmployeeList[0].CS_Division)
                    dataObject = new Globals.StringDataObject(EmployeeList[0].ID.ToString() + "|0", EmployeeList[0].DivisionAndFullName);
                else
                    dataObject = new Globals.StringDataObject(EmployeeList[0].ID.ToString() + "|" + EmployeeList[0].CS_Division.Name, EmployeeList[0].DivisionAndFullName);

                return dataObject;
            }
            else
            {
                return new Globals.StringDataObject("0|0", filterText);
            }
        }

        public Globals.SampleDataObject GetProjectManagerListJSON(string filterText)
        {
            if (null == _presenter)
                _presenter = new JSONServicePresenter(this);

            FilterText = filterText;

            _presenter.ListProjectManager();

            if (ProjectManagerList.Count == 1)
            {
                return new Globals.SampleDataObject(ProjectManagerList[0].ID, ProjectManagerList[0].DivisionAndFullName.Trim().ToString());
            }
            else
            {
                return new Globals.SampleDataObject(0, filterText);
            }
        }

        #endregion

        #region [ Location Services ]

        public Globals.SampleDataObject GetZipCodeByCity(int cityId)
        {
            _presenter = new JSONServicePresenter(this);
            CityId = cityId;
            _presenter.GetFirstZipCodeByCity();
            return ReturnDataObject;
        }

        public Globals.StateAndCountryDataObject GetStateAndCountryByCity(int cityId)
        {
            _presenter = new JSONServicePresenter(this);
            CityId = cityId;
            _presenter.GetStateAndCountryByCity();
            return StateAndCountryDataObject;
        }

        public Globals.SampleDataObject GetCityListJSON(string filterText, string contextKey)
        {
            if (null == _presenter)
                _presenter = new JSONServicePresenter(this);

            FilterText = filterText;
            FilterValue = contextKey;

            _presenter.ListCityByNameAndState();

            if (CityList.Count == 1)
            {
                return new Globals.SampleDataObject(CityList[0].ID, CityList[0].CityStateInformation.Trim());
            }
            else
            {
                return new Globals.SampleDataObject(0, filterText);
            }
        }

        public Globals.SampleDataObject GetZipCodeListJSON(string filterText, string contextKey)
        {
            if (null == _presenter)
                _presenter = new JSONServicePresenter(this);

            FilterText = filterText;
            FilterValue = contextKey;

            _presenter.ListZipCodesByNameAndCity();

            if (ZipCodeList.Count == 1)
            {
                return new Globals.SampleDataObject(ZipCodeList[0].ID, ZipCodeList[0].ZipCodeNameEdited.Trim());
            }
            else
            {
                return new Globals.SampleDataObject(0, filterText);
            }
        }

        public Globals.SampleDataObject GetStateListJSON(string filterText, string contextKey)
        {
            if (null == _presenter)
                _presenter = new JSONServicePresenter(this);

            FilterText = filterText;
            ContextKey = contextKey;

            _presenter.ListStatesByAcronym();

            if (StateList.Count == 1)
            {
                return new Globals.SampleDataObject(StateList[0].ID, string.Format("{0} - {1} - {2}", StateList[0].CS_Country.Name, StateList[0].Acronym.Trim(), StateList[0].Name.Trim()));
            }
            else
            {
                return new Globals.SampleDataObject(0, filterText);
            }
        }

        public Globals.SampleDataObject GetStateByDivisionListJSON(string filterText, string contextKey)
        {
            if (null == _presenter)
                _presenter = new JSONServicePresenter(this);

            FilterText = filterText;

            _presenter.ListStatesByAcronymAndDivision();

            if (StateList.Count == 1)
            {
                return new Globals.SampleDataObject(StateList[0].ID, string.Format("{0} - {1} - {2}", StateList[0].CS_Country.Name, StateList[0].Acronym.Trim(), StateList[0].Name.Trim()));
            }
            else
            {
                return new Globals.SampleDataObject(0, filterText);
            }
        }

        #endregion

        #region [ Equipment Services ]

        public Globals.EquipmentComboNotificationDataObject[] GetEquipmentComboNotificationList()
        {
            _presenter = new JSONServicePresenter(this);
            _presenter.GetEquipmentComboNotificationList();

            if (null != EquipmentComboNotificationList)
            {
                Globals.EquipmentComboNotificationDataObject[] returnList = new Globals.EquipmentComboNotificationDataObject[EquipmentComboNotificationList.Count];
                for (int i = 0; i < EquipmentComboNotificationList.Count; i++)
                {
                    returnList[i] = new Globals.EquipmentComboNotificationDataObject()
                    {
                        ComboId = EquipmentComboNotificationList[i].ComboID,
                        ComboName = EquipmentComboNotificationList[i].ComboName
                    };
                }
                return returnList;
            }
            else
                return null;
        }

        public Globals.EquipmentPermitNotificationDataObject[] GetEquipmentPermitNotificationList()
        {
            _presenter = new JSONServicePresenter(this);
            _presenter.GetEquipmentPermitNotificationList();

            if (null != EquipmentPermitNotificationList)
            {
                Globals.EquipmentPermitNotificationDataObject[] returnList = new Globals.EquipmentPermitNotificationDataObject[EquipmentPermitNotificationList.Count];
                for (int i = 0; i < EquipmentPermitNotificationList.Count; i++)
                {
                    returnList[i] = new Globals.EquipmentPermitNotificationDataObject()
                    {
                        PermitId = EquipmentPermitNotificationList[i].Id,
                        PermitNumber = string.Format("{0}, {1}", EquipmentPermitNotificationList[i].LicenseNumber, EquipmentPermitNotificationList[i].Code),
                        EquipmentName = string.Format("{0}, {1}", EquipmentPermitNotificationList[i].CS_Equipment.Name, EquipmentPermitNotificationList[i].CS_Equipment.Description),
                        ExpirationDate = EquipmentPermitNotificationList[i].ExpirationDate.ToString("MM/dd/yyyy"),
                    };
                }
                return returnList;
            }
            else
                return null;
        }

        public Globals.SampleDataObject GetEquipmentTypeListJSON(string filterText, string contextKey)
        {
            _presenter = new JSONServicePresenter(this);

            FilterText = filterText;

            _presenter.ListAllEquipmentTypeByName();

            if (null != EquipmentTypeList && EquipmentTypeList.Count == 1)
            {
                return new Globals.SampleDataObject(EquipmentTypeList[0].ID, EquipmentTypeList[0].CompleteName);
            }
            else
            {
                return new Globals.SampleDataObject(0, filterText);
            }
        }

        public Globals.LocalEquipmentTypeDataObject[] GetLocalEquipmentTypeListJSON(string prefixText, string contextKey)
        {
            _presenter = new JSONServicePresenter(this);
            _presenter.GetLocalEquipmentTypelList();

            if (null != LocalEquipmentTypeList)
            {
                Globals.LocalEquipmentTypeDataObject[] returnList = new Globals.LocalEquipmentTypeDataObject[LocalEquipmentTypeList.Count];
                for (int i = 0; i < LocalEquipmentTypeList.Count; i++)
                {
                    returnList[i] = new Globals.LocalEquipmentTypeDataObject()
                    {
                        Id = LocalEquipmentTypeList[i].ID,
                        Name = LocalEquipmentTypeList[i].Name
                    };
                }
                return returnList;
            }
            else
                return null;
        }
        public Globals.SampleDataObject GetEquipmentListJSON(string filterText, string contextKey)
        {
            if (null == _presenter)
                _presenter = new JSONServicePresenter(this);

            FilterText = filterText;
            FilterValue = contextKey;

            _presenter.ListAllEquipmentByEqType();

            if (null != EquipmentList && EquipmentList.Count == 1)
            {
                return new Globals.SampleDataObject(EquipmentList[0].ID, EquipmentList[0].CompleteName);
            }
            else
            {
                return new Globals.SampleDataObject(0, filterText);
            }
        }

        public Globals.SampleDataObject GetEquipmentType(int equipmentId)
        {
            _presenter = new JSONServicePresenter(this);
            EquipmentId = equipmentId;
            _presenter.GetEquipmentTypeByEquipment();
            return ReturnDataObject;
        }
        #endregion

        #region [ Off Call Services ]

        public Globals.EquipmentOffCallNotificationDataObject[] GetOffCallNotificationList()
        {
            _presenter = new JSONServicePresenter(this);
            _presenter.GetOffCallNotificationList();

            if (null != OffCallNotificationList)
            {
                Globals.EquipmentOffCallNotificationDataObject[] returnList = new Globals.EquipmentOffCallNotificationDataObject[OffCallNotificationList.Count];
                for (int i = 0; i < OffCallNotificationList.Count; i++)
                {
                    returnList[i] = new Globals.EquipmentOffCallNotificationDataObject()
                    {
                        EmployeeId = OffCallNotificationList[i].EmployeeID,
                        EmployeeName = OffCallNotificationList[i].CS_Employee.FullName,
                        OffCallDate = (OffCallNotificationList[i].OffCallEndDate + OffCallNotificationList[i].OffCallReturnTime).ToString("MM/dd/yyyy HH:mm")
                    };
                }
                return returnList;
            }
            else
                return null;
        }

        #endregion

        #region [ Utils ]

        private string IgnoreCoverage(string filterText)
        {
            string[] text = filterText.Split('/');

            if (text.Length > 1)
            {
                return text[1];
            }

            return text[0];
        }

        #endregion

        #region [ Map Plotting ]

        public Globals.MapPlotDataObject[] GetMapPlottingObjects(Globals.MapPlotRequestDataObject mapPlotFilter)
        {
            _presenter = new JSONServicePresenter(this);

            MapPlottingFilter = mapPlotFilter;

            _presenter.GetMapPlottingObjects();

            if (null != MapPlottingObjectsList)
            {
                Globals.MapPlotDataObject[] returnList = new Globals.MapPlotDataObject[MapPlottingObjectsList.Count];
                for (int i = 0; i < MapPlottingObjectsList.Count; i++)
                {
                    returnList[i] = new Globals.MapPlotDataObject()
                    {
                        Latitude = MapPlottingObjectsList[i].Latitude,
                        Longitude = MapPlottingObjectsList[i].Longitude,
                        Type = MapPlottingObjectsList[i].Type,
                        Description = MapPlottingObjectsList[i].Description,
                        Name = MapPlottingObjectsList[i].Name
                    };
                }

                return returnList;
            }
            else
                return null;
        }

        #endregion

        #endregion




        
    }
}
