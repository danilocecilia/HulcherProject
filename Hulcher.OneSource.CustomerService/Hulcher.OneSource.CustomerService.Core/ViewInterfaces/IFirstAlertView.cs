using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Core.Security;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.DataContext.VO;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    /// <summary>
    /// First Alert View Interface
    /// </summary>
    public interface IFirstAlertView : IBaseView
    {
        #region [ Security ]

        /// <summary>
        /// Get the Username from MasterPage
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// Gets the Domain of the current user
        /// </summary>
        string Domain { get; }

        /// <summary>
        /// Sets the page to be read-only (when the user has no permission)
        /// </summary>
        bool ReadOnly { get; set; }

        /// <summary>
        /// Sets the page to disable the delete link for First Alert
        /// </summary>
        bool DeletePermission { get; set; }

        #endregion

        #region [ Common ]

        /// <summary>
        /// Indicates if a save function was made successfully or not
        /// </summary>
        bool SavedSuccessfully { get; set; }

        #endregion

        #region [ First Alert Listing ]

        /// <summary>
        /// Get item filtered on combo on the page
        /// </summary>
        Globals.FirstAlert.FirstAlertFilters? FirstAlertFilter { get; }

        /// <summary>
        /// Get the filter value
        /// </summary>
        string FilterValue { get; }

        /// <summary>
        /// List all first alert details
        /// </summary>
        IList<CS_FirstAlert> FirstAlertList { get; set; }

        #region [ Grid Row ]

        /// <summary>
        /// Gets or Sets the RowDataItem for the first alert gridview
        /// </summary>
        CS_FirstAlert FirstAlertRowDataItem { get; set; }

        /// <summary>
        /// Set first alert id for gridview
        /// </summary>
        string FirstAlertRowAlertId { get; set; }

        /// <summary>
        /// Set alert number for gridview
        /// </summary>
        string FirstAlertRowAlertNumber { get; set; }

        /// <summary>
        /// Set Date And Time for gridview
        /// </summary>
        string FirstAlertRowAlertDateAndTime { get; set; }

        /// <summary>
        /// Set JobNumber for gridview
        /// </summary>
        string FirstAlertRowJobNumber { get; set; }

        /// <summary>
        /// Set Dvision for gridview
        /// </summary>
        string FirstAlertRowDivision { get; set; }

        /// <summary>
        /// Set Customer for gridview
        /// </summary>
        string FirstAlertRowCustomer { get; set; }

        /// <summary>
        /// Set Location for gridview
        /// </summary>
        string FirsAlertRowLocation { get; set; }

        /// <summary>
        /// Set the type of first alert
        /// </summary>
        string FirstAlertRowFirstAlertType { get; set; }

        #endregion

        #endregion

        #region [ First Alert Form ]

        /// <summary>
        /// Indicates if the First Alert page is in Edit Mode (Form) or not (Listing)
        /// </summary>
        bool EditMode { get; set; }

        /// <summary>
        /// Gets the Job ID that was passed via querystring
        /// </summary>
        int? JobIDFromExternalSource { get; }

        /// <summary>
        /// FirstAlert Identifier
        /// </summary>
        int FirstAlertID { get; set; }

        /// <summary>
        /// Selected Job Identifier
        /// </summary>
        int JobID { get; }

        /// <summary>
        /// Sets job related information when a job is selected
        /// </summary>
        CS_Job JobRelatedInformation { set; }

        /// <summary>
        /// FirstAlertEntity for save or update
        /// </summary>
        CS_FirstAlert FirstAlertEntity { get; set; }

        /// <summary>
        /// Firt alert types
        /// </summary>
        IList<CS_FirstAlertType> FirstAlertType { set; }

        /// <summary>
        /// FirstAlertFirstAlert Type List
        /// </summary>
        IList<CS_FirstAlertFirstAlertType> FirstAlertFirstAlertTypeList { get; set; }

        /// <summary>
        /// FirstAlertDivisionList for save or update
        /// </summary>
        IList<CS_FirstAlertDivision> FirstAlertDivisionList { get; set; }

        /// <summary>
        /// ID of the CallLog associated with the FirstAlert, for loading
        /// </summary>
        int? CallLogID { get; }

        /// <summary>
        /// Sets the Division list to fill the Dropdown
        /// </summary>
        IList<CS_Division> DivisionList { set; }

        /// <summary>
        /// Set the text property of Division textbox multiselect alternative solution
        /// </summary>
        string DivisionValue { set; }

        /// <summary>
        /// Set the text property of First Alert Type textbox multiselect alternative solution
        /// </summary>
        string FirstAlertTypeValue { set; }

        #endregion

        #region [ Vehicle ]

        /// <summary>
        /// FirstAlertVehicleList for save or update
        /// </summary>
        IList<CS_FirstAlertVehicle> FirstAlertVehicleList { get; set; }

        /// <summary>
        /// FirstAlertVehicleList Data Item
        /// </summary>
        CS_FirstAlertVehicle FirstAlertVehicleListDataItem { get; set; }

        /// <summary>
        /// Binds the Make Column
        /// </summary>
        string FirstAlertVehicleListUnitNumber { set; }

        /// <summary>
        /// Binds the Make Column
        /// </summary>
        string FirstAlertVehicleListMake { set; }

        /// <summary>
        /// Binds the Model Column
        /// </summary>
        string FirstAlertVehicleListModel { set; }

        /// <summary>
        /// Binds the Year Column
        /// </summary>
        string FirstAlertVehicleListYear { set; }

        /// <summary>
        /// Binds the Damage Column
        /// </summary>
        string FirstAlertVehicleListDamage { set; }

        /// <summary>
        /// Binds the Hulcher Column
        /// </summary>
        string FirstAlertVehicleListHulcher { set; }

        /// <summary>
        /// Hulcher Equipment Filter
        /// </summary>
        Globals.FirstAlert.EquipmentFilters? EquipmentFilter { get; set; }

        /// <summary>
        /// Hulcher Equipment Filter Value
        /// </summary>
        string EquipmentFilterValue { get; set; }

        /// <summary>
        /// Gets or sets IsHucherVehicle radio value
        /// </summary>
        bool IsHulcherVehicle { get; set; }

        /// <summary>
        /// Gets Selected Vehicle List
        /// </summary>
        IList<CS_FirstAlertVehicle> SelectedVehicles { get; }

        /// <summary>
        /// Editing EquipmentID for a vehicle
        /// </summary>
        int CurrentFirstAlertVehicleEquipmentID { get; set; }

        /// <summary>
        /// Editing equipment index
        /// </summary>
        int CurrentFirstAlertVehicleIndex { set; get; }

        /// <summary>
        /// Editing equipment id
        /// </summary>
        int CurrentFirstAlertVehicleID { set; }

        /// <summary>
        /// Filter vehicles by job Id
        /// </summary>
        bool FilterVehiclesByJobID { get; set; }

        /// <summary>
        /// Gets os Sets Other Vehicle Info
        /// </summary>
        CS_FirstAlertVehicle OtherVehicle { get; set; }

        /// <summary>
        /// Row EquipmentID for gvFilteredEquipments RowDataBound
        /// </summary>
        int FilteredEquipmentsEquipmentID { get; set; }

        /// <summary>
        /// Damage From gvFilteredEquipments
        /// </summary>
        string FilteredEquipmentsDamage { get; set; }

        /// <summary>
        /// EstCost From gvFilteredEquipments
        /// </summary>
        string FilteredEquipmentsEstCost { get; set; }

        /// <summary>
        /// CheckBox From gvFilteredEquipments
        /// </summary>
        bool FilteredEquipmentsSelect { get; set; }

        bool VehiclesFormVisible { set; }

        bool VehiclesListVisible { set; }

        /// <summary>
        /// HulcherVehicles Form Visibility
        /// </summary>
        bool HulcherVehiclesVisible { set; }

        /// <summary>
        /// OtherVehicles Form Visibility
        /// </summary>
        bool OtherVehiclesVisible { set; }

        /// <summary>
        /// HulcherVehicleType Enbled 
        /// </summary>
        bool HulcherVehicleHeaderEnabled { set; }

        /// <summary>
        /// Verify if it is an editing action  
        /// </summary>
        bool IsVehicleListEdit { get; }

        /// <summary>
        /// VehiclesList CommandName
        /// </summary>
        string VehiclesListCommandName { get; }

        /// <summary>
        /// VehiclesList CommandArgument
        /// </summary>
        int VehiclesListCommandArgument { get; }

        /// <summary>
        /// Generates a new first alert vehicle temp id
        /// </summary>
        int NewVehicleTempID();

        /// <summary>
        /// Sets Filtered Equipments DataSource
        /// </summary>
        IList<CS_View_EquipmentInfo> FilteredEquipmentsDataSource { set; }

        

        #endregion

        #region [ Person ]

        /// <summary>
        /// FirstAlertPersonList for save or update
        /// </summary>
        IList<FirstAlertPersonVO> FirstAlertPersonList { get; set; }

        /// <summary>
        /// Get or Set the ID of the Edited Person (populate the fields as well)
        /// </summary>
        int CurrentFirstAlertPersonIndex { get; set; }

        /// <summary>
        /// Get the FirstAlertPersonEntity from the screen
        /// </summary>
        FirstAlertPersonVO NewFirstAlertPerson { get; set; }

        /// <summary>
        /// Filter employee by job Id
        /// </summary>
        bool FilterEmployeeByJobID { get; set; }

        /// <summary>
        /// Get the selected Employee Filter
        /// </summary>
        Globals.FirstAlert.EmployeeFilters? EmployeeFilter { get; set; }

        /// <summary>
        /// Get the selected Employee Filter Value
        /// </summary>
        string EmployeeFilterValue { get; set; }

        /// <summary>
        /// Sets the Employee Grid DataSource
        /// </summary>
        IList<CS_Employee> EmployeeDataSource { set; }

        string EmployeeListRowID { set; }

        string EmployeeListRowDivision { set; }

        string EmployeeListRowLastName { set; }

        string EmployeeListRowFirstName { set; }

        string EmployeeListRowLocation { set; }

        CS_Employee EmployeeListRowDataItem { get; set; }

        int? EditEmployeeID { get; }

        string PeopleListRowLastName { set; }

        string PeopleListRowFirstName { set; }

        string PeopleListRowHulcherEmployee { set; }

        string PeopleListRowLocation { set; }

        FirstAlertPersonVO PeopleListRowDataItem { get; }

        bool PeopleFormVisible { set; }

        bool PeopleListVisible { set; }

        IList<CS_FirstAlertVehicle> PersonVehicleList { set; }
        
        void BlockPeopleFilterForEdit();
        
        void UnblockPeopleFilterForEdit();

        #endregion

        #region [ Contact Personal ]

        /// <summary>
        /// FirstAlertContactPersonal list
        /// </summary>
        IList<CS_FirstAlertContactPersonal> FirstAlertContactPersonalList { get; set; }

        #region [ Grid Row ]

        CS_FirstAlertContactPersonal ContactPersonalRowDataItem { get; set; }

        int ContactPersonalRowID { get; set; }

        string ContactPersonalRowName { get; set; }

        DateTime? ContactPersonalRowEmailAdvisedDate { get; set; }

        DateTime? ContactPersonalRowVMXAdvisedDate { get; set; }

        DateTime? ContactPersonalRowInPersonAdvisedDate { get; set; }

        #endregion

        #endregion

        #region [ Methods ]

        #region [ Report ]

        /// <summary>
        /// Opens the First Alert Report
        /// </summary>
        /// <param name="firstAlertId">First Alert Identifier</param>
        void OpenReport(int firstAlertId);

        #endregion

        #endregion
    }
}
