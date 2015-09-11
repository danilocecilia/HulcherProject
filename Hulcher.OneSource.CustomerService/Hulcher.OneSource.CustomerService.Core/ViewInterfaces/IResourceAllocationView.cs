using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    public interface IResourceAllocationView : IBaseView
    {
        #region [ Common ]

        /// <summary>
        /// Property created to get the current username
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// Gets or Sets the page title to display the Job #
        /// </summary>
        string PageTitle { get; set; }

        /// <summary>
        /// Property that maps the JobId used in the current context
        /// </summary>
        int JobID { get; }

        /// <summary>
        /// Property that holds the name of the Control to update in the parent Page
        /// </summary>
        string ParentControlId { get; }

        /// <summary>
        /// The value of the Display Property for the Add Collapsible Holder
        /// </summary>
        bool DisplayAddResource { get; set; }

        /// <summary>
        /// Indicates if the saving function occurred correctly
        /// </summary>
        bool SavedSuccessfully { get; set; }

        #endregion

        #region [ Resource Conversion ]

        /// <summary>
        /// Property that flags the resource allocation screen to open on the conversion mode
        /// </summary>
        bool ResourceConversion { get; }

        /// <summary>
        /// Gets or Sets the ReserveList Datasource
        /// </summary>
        IList<CS_View_ReserveList> ReserveListDatasource { get; set; }

        /// <summary>
        /// Gets or Sets checkbox for search
        /// </summary>
        bool ReservedEquipmentsOnly { get; set; }

        /// <summary>
        /// Method that will show the reserve list in case of conversion
        /// </summary>
        void SetPageResourceConversion();

        #endregion

        #region [ Add Resource - Equipment ]

        #region [ Grid and Filters ]

        /// <summary>
        /// Enumerator for the Equipment Filter
        /// </summary>
        Globals.ResourceAllocation.EquipmentFilters? EquipmentFilterAdd { get; set; }

        /// <summary>
        /// Value of the TextBox Filter for the Equipment Filter
        /// </summary>
        string EquipmentFilterValueAdd { get; set; }

        /// <summary>
        /// Retrieves the DataTable that is used as a DataSource for the GridView
        /// </summary>
        IList<CS_View_EquipmentInfo> EquipmentsAddGridDataSource { set; }

        /// <summary>
        /// DataTable responsible for maintaining the Equipment Information updated on Add Tab
        /// </summary>
        IList<CS_View_EquipmentInfo> EquipmentList { get; set; }

        /// <summary>
        /// A list of Equipments that are Selected
        /// Used for updating the Available Buttons
        /// </summary>
        Dictionary<string, bool> SelectedEquipmentAddList { get; set; }

        /// <summary>
        /// Gets the list of selected equipments to add them in shopping cart
        /// </summary>
        IList<string> SelectedEquipmentsAdd { get; }

        bool EquipmentsAddWhiteLight { set; }
        #endregion

        #region [ Equipment / Combo Row ]
        bool EquipmentsComboWhiteLight { set; }

        CS_View_EquipmentInfo EquipmentRowDataItem { get; set; }

        string EquipmentsAddDivision { get; set; }

        string EquipmentsAddDivisionState { get; set; }

        string EquipmentsAddComboName { get; set; }

        string EquipmentsAddUnitNumber { get; set; }

        string EquipmentsAddDescriptor { get; set; }

        string EquipmentsAddStatus { get; set; }

        string EquipmentsAddJobLocation { get; set; }

        string EquipmentsAddOperationStatus { get; set; }

        string EquipmentsAddEquipmentId { get; set; }

        string EquipmentsAddIsCombo { get; set; }

        string EquipmentsAddIsComboUnit { get; set; }

        string EquipmentsAddJobNumber { get; set; }

        string EquipmentsAddType { get; set; }

        int? EquipmentsAddComboID { get; set; }

        string EquipmentsAddJobNumberNavigateUrl { get; set; }

        string EquipmentsAddTypeNavigateUrl { get; set; }

        bool EquipmentsAddIsDivConf { get; set; }

        bool EquipmentsAddPermitExpired { get; set; }

        bool EquipmentsAddchkEquipmentAdd { get; set; }

        List<CS_View_EquipmentInfo> EquipmentsComboGridDataSource { get; set; }

        #endregion

        #region [ Equipment Inside Combo Row ]

        CS_View_EquipmentInfo EquipmentComboDataItem { get; set; }

        string EquipmentsComboDivision { get; set; }

        string EquipmentsComboDivisionState { get; set; }

        string EquipmentsComboComboName { get; set; }

        string EquipmentsComboUnitNumber { get; set; }

        string EquipmentsComboDescriptor { get; set; }

        string EquipmentsComboStatus { get; set; }

        string EquipmentsComboJobLocation { get; set; }

        string EquipmentsComboOperationStatus { get; set; }

        string EquipmentsComboEquipmentId { get; set; }

        string EquipmentsComboIsCombo { get; set; }

        string EquipmentsComboIsComboUnit { get; set; }

        string EquipmentsComboJobNumber { get; set; }

        string EquipmentsComboType { get; set; }

        int? EquipmentsComboComboID { get; set; }

        string EquipmentsComboJobNumberNavigateUrl { get; set; }

        string EquipmentsComboTypeNavigateUrl { get; set; }

        bool EquipmentsComboIsDivConf { get; set; }

        bool EquipmentsComboPermitExpired { get; set; }

        bool EquipmentsCombochkEquipmentAdd { get; set; }

        #endregion

        #region [ Sorting ]

        /// <summary>
        /// Gets the column that is being used to sort equipment data
        /// </summary>
        Globals.Common.Sort.EquipmentSortColumns SortColumn { get; set; }

        /// <summary>
        /// Gets the direction that is being used to sort equipment data
        /// </summary>
        Globals.Common.Sort.SortDirection SortDirection { get; set; }

        #endregion

        #endregion

        #region [ Add Resource - Employee ]

        #region [ Grid and Filters ]

        /// <summary>
        /// Enumerator for the Employee Filter
        /// </summary>
        Globals.ResourceAllocation.EmployeeFilters? EmployeeFilterAdd { get; set; }

        /// <summary>
        /// Value of the TextBox Filter for the Employee Filter
        /// </summary>
        string EmployeeFilterValueAdd { get; set; }

        /// <summary>
        /// Retrieves the DataTable that is used as a DataSource for the GridView
        /// </summary>
        DataTable EmployeeListAdd { set; }

        /// <summary>
        /// DataTable responsible for maintaining the Employee Information updated on Add Tab
        /// </summary>
        DataTable EmployeeDataTable { get; set; }

        /// <summary>
        /// A list of Employees that are Selected
        /// Used for updating the Available Buttons
        /// </summary>
        Dictionary<string, bool> SelectedEmployeeAddList { get; set; }

        /// <summary>
        /// Gets the list of selected employees to add them in shopping cart
        /// </summary>
        IList<int> SelectedEmployeesAdd { get; }

        #endregion

        #endregion

        #region [ Reserve Resource - Equipment ]

        #region [ Grid and Filters ]

        /// <summary>
        /// Retrieves the List that is used as a DataSource for the EquipmentType Filter
        /// </summary>
        IList<CS_EquipmentType> EquipmentTypeFilterSource { set; }

        /// <summary>
        /// Value of the Filter for EquipmentId
        /// </summary>
        int? EquipmentTypeId { get; }

        /// <summary>
        /// Value of the Filter for StateId
        /// </summary>
        int? StateId { get; }

        /// <summary>
        /// Value of the Filter for DivisionId
        /// </summary>
        int? DivisionId { get; }

        /// <summary>
        /// Retrieves the List that is used as a DataSource for the GridView
        /// </summary>
        IList<CS_View_EmployeeInfo> ReserveEmployeeDataSource { set; }

        /// <summary>
        /// Gets the list of selected equipments to reserve them in shopping cart
        /// </summary>
        IList<int[]> SelectedEquipmentsReserve { get; }

        /// <summary>
        /// Executes the DataBind method for the Reserve Equipment Grid
        /// </summary>
        void BindReserveEquipmentGrid();

        #endregion

        #region [ Equipment Type Row ]

        /// <summary>
        /// The ID of the Type of equipment
        /// </summary>
        int SelectedEquipmentType { get; set; }

        /// <summary>
        /// The ID of the Selected Division
        /// </summary>
        int SelectedDivision { get; set; }

        /// <summary>
        /// List of jobs associated to a specific Equipment Type
        /// </summary>
        IList<CS_Job> JobsRelatedToEquipmentType { set; }

        #endregion

        #endregion

        #region [ Reserve Resource - Employee ]

        #region [ Grid and Filters ]

        /// <summary>
        /// Enumerator for the Employee Filter
        /// </summary>
        Globals.ResourceAllocation.EmployeeFilters? EmployeeFilterReserve { get; }

        /// <summary>
        /// Value of the TextBox Filter for the Employee Filter
        /// </summary>
        string EmployeeFilterValueReserve { get; }

        /// <summary>
        /// Retrieves the List that is used as a DataSource for the GridView
        /// </summary>
        IList<CS_View_ReserveEquipment> ReserveEquipmentDataSource { get; set; }

        /// <summary>
        /// The dictionary list to store Reserve Count for each equipment type/division
        /// </summary>
        Dictionary<string, int> ReserveEquipmentLocalCount { get; set; }

        /// <summary>
        /// Gets the list of selected employees to reserve them in shopping cart
        /// </summary>
        IList<int> SelectedEmployeesReserve { get; }

        #endregion

        #endregion

        #region [ Transfer Shopping Cart ]

        /// <summary>
        /// Retrieves the DataTable that is used as a DataSource for the GridView
        /// </summary>
        DataTable TransferShoppingCart { get; set; }

        /// <summary>
        /// Gets a list of selected rows inside the shopping cart that are marked to be removed
        /// </summary>
        IList<int> SelectedRowsToTransfer { get; }

        /// <summary>
        /// Sets the list of the ResourceID Selected and pass it to the new page
        /// </summary>
        IList<int> ResourceIdToTransfer { set; }

        #endregion

        #region [ Shopping Cart ]

        /// <summary>
        /// Retrieves the DataTable that is used as a DataSource for the GridView
        /// </summary>
        DataTable ShoppingCart { get; set; }

        /// <summary>
        /// Gets a list of selected rows inside the shopping cart that are marked to be removed
        /// </summary>
        IList<int> SelectedRowsToRemove { get; }

        /// <summary>
        /// Notes field for the Resource Allocation
        /// </summary>
        string Notes { get; set; }

        /// <summary>
        /// Call Date Field for the Added Resources Call log
        /// </summary>
        DateTime CallDate { get; set; }

        #endregion

        #region [ SubContractor ]
        bool IsSubContractor { get; set; }
        
        string SubContractorInfo { get; set; }

        string FieldPO { get; set; }
        #endregion

        
    }
}
