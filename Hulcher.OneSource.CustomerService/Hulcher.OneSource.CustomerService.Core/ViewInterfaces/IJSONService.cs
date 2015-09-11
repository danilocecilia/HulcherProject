using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;

using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.DataContext.VO;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    [ServiceContract]
    public interface IJSONService
    {
        #region [ Properties ]

        #region [ Common Properties - Auto Complete ]

        Globals.SampleDataObject ReturnDataObject { get; set; }

        string FilterText { get; set; }

        string ContextKey { get; set; }

        string FilterValue { get; set; }

        string PrefixText { get; set; }

        #endregion

        #region [ Dashboard Properties ]

        IList<CS_CallLogCallCriteriaEmail> CallLogCallCriteriaEmail { get; set; }

        #endregion

        #region [ Job Properties ]

        int JobId { get; set; }

        IList<CS_JobStatus> JobStatusList { get; set; }

        IList<CS_PriceType> PriceTypeList { get; set; }

        IList<CS_JobAction> JobActionList { get; set; }

        IList<CS_Job> JobList { get; set; }

        IList<PresetNotificationVO> PresetNotificationList { get; set; }

        #endregion

        #region [ Call Log Properties ]

        int CallLogId { get; set; }

        IList<CS_CallType> CallTypeList { get; set; }

        #endregion

        #region [ Division Properties ]

        IList<CS_Division> DivisionList { get; set; }

        #endregion

        #region [ Customer and Contact Properties ]

        string CustomerName { get; set; }

        int ContactId { get; set; }

        IList<CS_Customer> CustomerList { get; set; }

        IList<CS_Contact> DynamicsContactList { get; set; }

        IList<CS_Contact> CustomerServiceContactList { get; set; }

        IList<CS_Contact> ContactList { get; set; }

        int CustomerId { get; set; }

        bool IsCostumerCollection { get; set; }

        string OperatorAlertMessage { get; set; }

        bool IsNeWCompany { get; set; }

        #endregion

        #region [ Employee Properties ]

        int EmployeeId { get; set; }

        IList<CS_Employee> EmployeeList { get; set; }

        IList<CS_Employee> ProjectManagerList { get; set; }

        #endregion

        #region [ Location Properties ]

        int CityId { get; set; }

        Globals.StateAndCountryDataObject StateAndCountryDataObject { get; set; }

        IList<CS_City> CityList { get; set; }

        IList<CS_ZipCode> ZipCodeList { get; set; }

        IList<CS_State> StateList { get; set; }

        #endregion

        #region [ Equipment Properties ]

        IList<CS_View_ConflictedEquipmentCombos> EquipmentComboNotificationList { get; set; }

        IList<CS_EquipmentPermit> EquipmentPermitNotificationList { get; set; }

        IList<CS_EquipmentType> EquipmentTypeList { get; set; }

        IList<CS_Equipment> EquipmentList { get; set; }

        IList<CS_LocalEquipmentType> LocalEquipmentTypeList { get; set; }

        int EquipmentId { get; set; }
        #endregion

        #region [ Off Call Properties ]

        IList<CS_EmployeeOffCallHistory> OffCallNotificationList { get; set; }

        #endregion

        #region [ Map Plotting ]

        IList<Globals.MapPlotDataObject> MapPlottingObjectsList { get; set; }

        Globals.MapPlotRequestDataObject MapPlottingFilter { get; set; }

        #endregion

        #endregion

        #region [ Services ]

        #region [ Dashboard Services ]

        [OperationContract]
        Globals.EmailDataDataObject GetEmailData(int callLogId);

        #endregion

        #region [ Job Services ]

        [OperationContract]
        Globals.SampleDataObject GetJobStatusListJSON(string filterText, string contextKey);

        [OperationContract]
        Globals.SampleDataObject GetJobStatusListForJobRecordJSON(string filterText, string contextKey);

        [OperationContract]
        Globals.SampleDataObject GetPriceTypeListJSON(string filterText, string contextKey);

        [OperationContract]
        Globals.SampleDataObject GetJobActionListJSON(string filterText, string contextKey);

        [OperationContract]
        Globals.SampleDataObject GetJobNumberListJSON(string filterText, string contextKey);

        [OperationContract]
        Globals.SampleDataObject GetBillableJobNumberListJSON(string filterText, string contextKey);

        [OperationContract]
        Globals.SampleDataObject GetJobNumberListWithGeneralJSON(string filterText, string contextKey);

        [OperationContract]
        Globals.SampleDataObject GetJobStatus(int jobId);

        [OperationContract]
        Globals.PresetNotificationDataObject[] GetPresetNotificationList();

        #endregion

        #region [ Call Log Services ]

        [OperationContract]
        Globals.SampleDataObject GetCallTypeListJSON(string filterText, string contextKey);

        #endregion

        #region [ Division Services ]

        [OperationContract]
        Globals.SampleDataObject GetDivision(int employeeId);

        [OperationContract]
        Globals.SampleDataObject GetDivisionListJSON(string filterText, string contextKey);

        #endregion

        #region [ Customer and Contact Services ]

        [OperationContract]
        Globals.SampleDataObject GetCustomer(int contactId);

        [OperationContract]
        Globals.SampleDataObject GetCustomerListJSON(string filterText, string contextKey);

        [OperationContract]
        Globals.SampleDataObject GetDynamicsContactListJSON(string filterText, string contextKey);

        [OperationContract]
        Globals.SampleDataObject GetCustomerServiceContactListJSON(string filterText, string contextKey);

        [OperationContract]
        Globals.SampleDataObject GetAllContactListJSON(string filterText, string contextKey);

        [OperationContract]
        Globals.CollectionCustomer IsCustomerCollection(int customerId);

        [OperationContract]
        Globals.OperatorAlertCustomer CustomerHasOperatorAlert(int customerId);

        [OperationContract]
        Globals.ExistingCompany CheckExistingCustomer(string customerName);
        #endregion

        #region [ Employee Services ]

        [OperationContract]
        Globals.SampleDataObject GetEmployeeListJSON(string filterText, string contextKey);

        [OperationContract]
        Globals.StringDataObject GetEmployeeListWithDivisionNameJSON(string filterText, string contextKey);

        [OperationContract]
        Globals.SampleDataObject GetProjectManagerListJSON(string filterText);
        #endregion

        #region [ Location Services ]

        [OperationContract]
        Globals.SampleDataObject GetZipCodeByCity(int cityId);

        [OperationContract]
        Globals.StateAndCountryDataObject GetStateAndCountryByCity(int cityId);

        [OperationContract]
        Globals.SampleDataObject GetCityListJSON(string filterText, string contextKey);

        [OperationContract]
        Globals.SampleDataObject GetZipCodeListJSON(string filterText, string contextKey);

        [OperationContract]
        Globals.SampleDataObject GetStateListJSON(string filterText, string contextKey);

        [OperationContract]
        Globals.SampleDataObject GetStateByDivisionListJSON(string filterText, string contextKey);

        #endregion

        #region [ Equipment Services ]

        [OperationContract]
        Globals.EquipmentComboNotificationDataObject[] GetEquipmentComboNotificationList();

        [OperationContract]
        Globals.EquipmentPermitNotificationDataObject[] GetEquipmentPermitNotificationList();
        
        [OperationContract]
        Globals.SampleDataObject GetEquipmentTypeListJSON(string filterText, string contextKey);

        [OperationContract]
        Globals.SampleDataObject GetEquipmentListJSON(string filterText, string contextKey);

        [OperationContract]
        Globals.SampleDataObject GetEquipmentType(int equipmentId);

        [OperationContract]
        Globals.LocalEquipmentTypeDataObject[] GetLocalEquipmentTypeListJSON(string prefixText, string contextKey);

        #endregion

        #region [ Off Call Services ]

        [OperationContract]
        Globals.EquipmentOffCallNotificationDataObject[] GetOffCallNotificationList();

        #endregion

        #region [ Map Plotting Services ]

        [OperationContract]
        Globals.MapPlotDataObject[] GetMapPlottingObjects(Globals.MapPlotRequestDataObject mapPlotFilter);

        #endregion 

        #endregion
    }
}

