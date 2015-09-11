using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.DataContext.VO;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    /// <summary>
    /// Call Entry View Interface
    /// </summary>
    public interface ICallEntryView : IBaseView
    {
        #region [ Common ]

        /// <summary>
        /// Sets the Page Title
        /// </summary>
        string PageTitle { set; }

        /// <summary>
        /// Returns the Job Id
        /// </summary>
        int? JobId { get; set; }

        /// <summary>
        /// Return if is General log (99999) or not
        /// </summary>
        bool GeneralLog { get; }

        /// <summary>
        /// Stores the CustomerId to be used within the screen
        /// </summary>
        int? CustomerId { get; set; }

        /// <summary>
        /// Return a boolean to make sure the page will be ready only or not
        /// </summary>
        bool ReadOnlyPage { set; }

        /// <summary>
        /// Sets the OpenMailPage
        /// </summary>
        bool OpenEmailPage { set; }

        /// <summary>
        /// Indicates if after a save the user will stay on this page or not
        /// </summary>
        bool SaveAndContinue { get; set; }

        /// <summary>
        /// Indicates if the action is a history update
        /// </summary>
        bool IsHistoryUpdate { get; }

        /// <summary>
        /// Indicates if the record was saved successfully or not
        /// </summary>
        bool SavedSuccessfuly { get; set; }

        #endregion

        #region [ Job Filter ]

        /// <summary>
        /// Enumerator for the Job Record Filter
        /// </summary>
        Globals.JobRecord.CallEntryFilter JobFilterType { get; }

        /// <summary>
        /// Get the Value for the Job Record filter
        /// </summary>
        string JobFilterValue { get; }

        /// <summary>
        /// Return the List of CS_job used on the jobfilter grid
        /// </summary>
        IList<CS_Job> JobFilterGridDataSource { set; }

        #endregion

        #region [ Job Panel ]

        /// <summary>
        /// Retrieves the CS_Job Entity
        /// </summary>
        CS_Job Job { set; }

        /// <summary>
        /// Gets the selected job number
        /// </summary>
        string PrefixedJobNumber { get; }

        #endregion

        #region [ Call Type ]

        /// <summary>
        /// Return the Call Type id
        /// </summary>
        int CallTypeId { get; }

        /// <summary>
        /// Return the PrimaryCallType Id
        /// </summary>
        int PrimaryCallTypeId { get; }

        /// <summary>
        /// Retrieves the list of PrimaryCallType
        /// </summary>
        IList<CS_PrimaryCallType> PrimaryCallTypeList { set; get; }

        /// <summary>
        /// Retrieves the list of Call Type
        /// </summary>
        IList<CS_CallType> CallTypeList { set; }

        /// <summary>
        /// Sets the Selected Primary CallType on the dropdownlist
        /// </summary>
        CS_PrimaryCallType SelectedPrimaryCallType { get; set; }

        /// <summary>
        /// Sets the selected call type on the call type dropdown
        /// </summary>
        CS_CallType SelectedCallType { get; set; }

        #endregion

        #region [ Call Log Fields ]

        /// <summary>
        /// ID of the Current Call Entry, for updating
        /// </summary>
        int? CallID { get; set; }

        /// <summary>
        /// The Call Entry Entity, for saving or loading
        /// </summary>
        CS_CallLog CallEntryEntity { get; set; }

        /// <summary>
        /// Gets the selected Call Date/time
        /// </summary>
        DateTime? CallDateTime { get; set; }

        /// <summary>
        /// List of Controls for the dynamic fields
        /// </summary>
        List<Control> DynamicFieldsControls { get; set; }

        /// <summary>
        /// String containing the XML of the Dynamic Controls
        /// </summary>
        string DynamicControlXmlString { get; }

        /// <summary>
        /// Gets checked CopyToShiftTransferLog
        /// </summary>
        bool CopyToShiftTransferLog { get; set; }

        /// <summary>
        /// Gets checked CopyToGeneralLog
        /// </summary>
        bool CopyToGeneralLog { get; set; }

        /// <summary>
        /// Get and set the grid's Notes/Comment
        /// </summary>
        string Notes { get; set; }

        #endregion

        #region [ Initial Advise ]

        /// <summary>
        /// Gets or sets Initial Advise Panel Visibilty
        /// </summary>
        bool InitialAdviseVisibility { get; set; }

        /// <summary>
        /// Sets the contacts and customeer to be bind on the grid datasaource
        /// </summary>
        IList<CallCriteriaResourceVO> PersonInitialAdviseGridDataSource { get; set; }

        /// <summary>
        /// Gets the selected Person on Initial Advise Grid 
        /// </summary>
        CallCriteriaResourceVO SelectedPersonInitialAdvise { get; set; }

        /// <summary>
        /// Gets or sets the  Division/Number Label in the Initial Advise Grid
        /// </summary>
        string InitialAdviseGridDivisionNumber { get; set; }

        /// <summary>
        /// Gets or sets the Name Label in the Initial Advise Grid
        /// </summary>
        string InitialAdviseGridName { get; set; }

        /// <summary>
        /// Gets or sets the Note Label in the INitial Advise Grid
        /// </summary>
        string InitialAdviseGridNote { get; set; }

        /// <summary>
        /// Gets or sets the ContactInfo Label in the Initial Advise Grid
        /// </summary>
        string InitialAdviseGridContactInfo { get; set; }

        /// <summary>
        /// Gets or sets the ResourceId Hidden field in the Initial Advise Grid
        /// </summary>
        string InitialAdviseGridResourceId { get; set; }

        /// <summary>
        /// Gets or sets the ResourceType Hidden field in the Initial Advise Grid
        /// </summary>
        string InitialAdviseGridResourceType { get; set; }

        /// <summary>
        /// Gets or sets the In Person Checkbox from the Initial Advise Grid
        /// </summary>
        bool InitialAdviseGridInPerson { get; set; }

        /// <summary>
        /// Gets or sets the Voicemail checkbox from the Initial Advise Grid
        /// </summary>
        bool InitialAdviseGridVoicemail { get; set; }

        #endregion

        #region [ Persons To Advise ]

        /// <summary>
        /// Gets or sets Persons Advise Panel
        /// </summary>
        bool PersonsAdviseVisibility { get; set; }

        /// <summary>
        /// Gest the type of person used to filter the persons grid
        /// </summary>
        Globals.CallEntry.typeOfPerson TypeOfPerson { get; }

        /// <summary>
        /// Get the customer name or employee name to be filter
        /// </summary>
        string FilteredEmployeeCustomerName { get; set; }

        /// <summary>
        /// Sets the list of Contacts and Employees on the persons Grid
        /// </summary>
        IList<object> PersonGridDataSource { set; }

        /// <summary>
        /// Return a list of Employees
        /// </summary>
        IList<CS_CallLogResource> SelectedPersons { get; set; }

        /// <summary>
        /// Retrieve the Employeed Id
        /// </summary>
        int SelectedEmployeeId { get; set; }

        /// <summary>
        /// Stores the Contactid to be used within the screen
        /// </summary>
        int SelectedContactId { get; set; }

        /// <summary>
        /// Sets personsshopingcart source
        /// </summary>
        IList<object> PersonsShopingCartDataSource { set; }

        #endregion

        #region [ Resources Assigned ]

        /// <summary>
        /// Gets or sets the Resources Assigned Panel Visibility
        /// </summary>
        bool ResourceAssignedVisibility { get; set; }

        /// <summary>
        /// Gets the selected filter type for the resource grid
        /// </summary>
        Globals.CallEntry.ResourceFilterType? ResourceFilterType { get; }

        /// <summary>
        /// Gets the value type for filter for the resource grid
        /// </summary>
        string ResourceFilterValue { get; }

        /// <summary>
        /// Returns the list os Reresource info to be used on the Resource GridView
        /// </summary>
        IList<CS_View_Resource_CallLogInfo> ResourceGridDataSource { set; }

        /// <summary>
        /// Return a list of Resources
        /// </summary>
        IList<CS_Resource> SelectedResources { get; set; }

        /// <summary>
        /// Set's the filter values for the Resources Assigned Grid
        /// </summary>
        IList<ListItemVO> ResourceAssignedFilterValues { set; }

        /// <summary>
        /// Get the Resource Id selected on the gridview
        /// </summary>
        int SelectedResourceId { get; set; }

        IList<LocalEquipmentTypeVO> SelectedEquipmentTypes { get; set; }

        #endregion

        #region [ Resources Read-Only ]

        /// <summary>
        /// Sets the datasource of the read only resources grid
        /// </summary>
        IList<CS_CallLogResource> ResourceReadOnlyDataSource { set; }

        #endregion

        #region [ Call Log History ]

        /// <summary>
        /// Gets or sets the Call Log History Panel Visibility
        /// </summary>
        bool CallLogHistoryPanelVisibility { get; set; }

        /// <summary>
        /// Gets or sets the Initial Advise History Panel Visibility
        /// </summary>
        bool CallLogInitialAdviseHistoryVisibility { get; set; }

        /// <summary>
        /// Get or Set the CallLogHistory Id's for the Save & Continue
        /// </summary>
        IList<int> CallLogHistoryList { get; set; }

        /// <summary>
        /// Sets the CallLogHistory Repeater Datasource
        /// </summary>
        IList<CS_CallLog> CallLogHistory { set; }

        /// <summary>
        /// Sets the initial advise calllog history datasaource
        /// </summary>
        IList<CS_CallLog> InitialAdiviseCallLogHistory { set; }

        /// <summary>
        /// Resource Identifier to get Initial advise Notes
        /// </summary>
        int AdviseNoteResourceID { get; set; }

        /// <summary>
        /// Indicates if the resource is an employee to get Initial advise Notes
        /// </summary>
        bool AdviseNoteIsEmployee { get; set; }

        /// <summary>
        /// Resource Advise Note
        /// </summary>
        string AdviseNote { get; set; }

        #endregion

        IList<int> CallCriteriaIDs { get; set; }
    }
}
