using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    public interface IEquipmentMaintenanceView : IBaseView
    {
        #region [ Listing ]

        /// <summary>
        /// Equipment list to bind the grid
        /// </summary>
        IList<CS_View_EquipmentInfo> EquipmentList { set; }

        /// <summary>
        /// Filter Type chosen by the user to filter grid information
        /// </summary>
        Globals.EquipmentMaintenance.FilterType? FilterType { get; }

        /// <summary>
        /// Value chosen by the user to filter grid information
        /// </summary>
        string FilterValue { get; }

        /// <summary>
        /// 
        /// </summary>
        IList<CS_EquipmentType> EquipmentTypeList { get; set; }

        /// <summary>
        /// Returns and sets the Enable property of Job Number Hyperlink inside listing grid
        /// </summary>
        bool? EnableJobNumberLink { get; set; }
        #region [ Equipment Row ]

        /// <summary>
        /// An item of the equipment list, used to filter a row
        /// </summary>
        CS_View_EquipmentInfo EquipmentRowDataItem { get; set; }

        int EquipmentRowEquipmentID { get; set; }

        string EquipmentRowDivisionName { get; set; }

        string EquipmentRowDivisionState { get; set; }

        string EquipmentRowComboName { get; set; }

        string EquipmentRowUnitNumber { get; set; }

        string EquipmentRowDescription { get; set; }

        string EquipmentRowStatus { get; set; }

        string EquipmentRowJobLocation { get; set; }

        string EquipmentRowLastCallEntryDescription { get; set; }

        int?[] EquipmentRowLastCallEntryID { get; set; }

        string EquipmentRowJobNumber { get; set; }

        int? EquipmentRowJobID { get; set; }

        string EquipmentRowOperationalStatus { get; set; }

        #endregion

        /// <summary>
        /// Set the enable coverage panel to true/false
        /// </summary>
        bool EnableDisablePanelCoverage { set; }

        #endregion

        #region [ Form ]

        /// <summary>
        /// Indicates if the form is in edit mode
        /// </summary>
        bool EditMode { get; set; }

        /// <summary>
        /// Holds the Equipment Identifier
        /// </summary>
        int EquipmentID { get; set; }

        /// <summary>
        /// Get the username value
        /// </summary>
        string Username { get; set; }

        #region [ Equipment Fields - Read Only ]

        string EquipmentName { get; set; }

        string EquipmentDescription { get; set; }

        string EquipmentType { get; set; }

        string EquipmentLicensePlate { get; set; }

        string EquipmentSerialNumber { get; set; }

        string EquipmentYear { get; set; }

        string EquipmentNotes { get; set; }

        string EquipmentBodyType { get; set; }

        string EquipmentMake { get; set; }

        string EquipmentModel { get; set; }

        string EquipmentFunction { get; set; }

        string EquipmentAssignedTo { get; set; }

        string EquipmentRegisteredState { get; set; }

        bool? EquipmentAttachPanelBoss { get; set; }

        bool? EquipmentAttachPileDriver { get; set; }

        bool? EquipmentAttachSlipSheet { get; set; }

        bool? EquipmentAttachTieClamp { get; set; }

        bool? EquipmentAttachTieInserter { get; set; }

        bool? EquipmentAttachTieTamper { get; set; }

        bool? EquipmentAttachUnderCutter { get; set; }

        #endregion

        #region [ Equipment Update ]

        #region [ White Light ]

        /// <summary>
        /// Indicates if the Equipment is currently in White Light
        /// </summary>
        bool IsWhiteLight { get; set; }

        /// <summary>
        /// Notes for the last White Light data
        /// </summary>
        string WhiteLightNotes { get; set; }

        /// <summary>
        /// Sets the White Light history table
        /// </summary>
        IList<CS_EquipmentWhiteLight> WhiteLightHistoryGridDataSource { set; }

        #endregion

        /// <summary>
        /// Binds the Coverage History DataGrid
        /// </summary>
        List<CS_EquipmentCoverage> CoverageHistoryGridDataSource { set; }

        /// <summary>
        /// Binds the Status History DataGrid
        /// </summary>
        List<CS_EquipmentDownHistory> DownHistoryGridDataSource { set; }

        /// <summary>
        /// Get the HistoryEndDate when the equipment status is down
        /// </summary>
        DateTime? DownHistoryEndDate { get; set; }

        /// <summary>
        /// Get the value to be used if equipmentstatus is down
        /// </summary>
        string EquipmentStatus { get; set; }

        /// <summary>
        /// Sets the visibility of the Date fields related to the Equipment Status UP
        /// </summary>
        bool EquipmentStatusUpDateFieldsVisibility { set; }

        /// <summary>
        /// Sets the visibility of the Date fields related to the Equipment Status DOWN
        /// </summary>
        bool EquipmentStatusDateFieldsVisibility { set; }

        /// <summary>
        /// Turns the validation of the Equipment Status Date and Time controls on/off
        /// </summary>
        bool EquipmentStatusDateTimeRequired { set; }

        /// <summary>
        /// Turns the validation of the Equipment Status Duration on/off
        /// </summary>
        bool EquipmentStatusDurationRequired { set; }

        /// <summary>
        /// Turns the validation of the Equipment Coverage Date and Time controls on/off
        /// </summary>
        bool EquipmentCoverageFieldsRequired { set; }

        /// <summary>
        /// Get the value that indicate if that equipment is heavy or no
        /// </summary>
        bool IsHeavyEquipment { get; set; }

        bool IsSeasonal { get; set; }

        bool DisplayInResourceAllocation { get; set; }

        bool ReplicateChangesToCombo { get; set; }

        bool ReplicateChangesToComboVisibility { get; set; }

        bool EquipmentStatusUpDateTimeRequired { set; }

        DateTime? CoverageEndDate { get; set; }

        DateTime? CoverageStartDate { get; set; }

        int DivisionID { get; set; }

        bool IsEquipmentCoverage { get; set; }

        DateTime? DownHistoryStartDate { get; set; }

        int? EquipmentCoverageDuration { get; set; }

        int? EquipmentDownDuration { get; set; }

        bool EquipmentCoverageStartFields { set; }

        bool EquipmentCoverageEndFields { set; }

        void ClearEquipmentFields();

        string DivisionName { set; }

        string ActualEquipmentDivision { set; }
        #endregion        

        int JobID { get; set; }

        bool IsEquipmentAssignedToJob { get; set; }        

        #endregion

        #region [ Equipment Display ]

        string FirstTierItemUnitType { set; }

        CS_EquipmentType FirstTierDataItem { get; }

        Globals.Common.Sort.EquipmentDisplaySortColumns SortColumn { get; }

        Globals.Common.Sort.SortDirection SortDirection { get; }

        IList<CS_View_EquipmentInfo> EquipmentListDisplay { get; set; }

        IList<CS_View_EquipmentInfo> DivisionDataSource { get; set; }

        IList<CS_EquipmentType> EquipmentTypeDataSource { set; }

        Globals.EquipmentMaintenance.FilterType FilterTypeEquipmentDisplay { get; }

        string FilterValueEquipmentDisplay { get; }

        CS_View_EquipmentInfo SecondTierDataItem { get; }

        string SecondTierItemDivisionName { get; set; }

        CS_View_EquipmentInfo ThirdTierDataItem { get; }

        string ThirdTierItemUnitNumber { get; set; }

        string ThirdTierItemUnitDescription { get; set; }

        IList<CS_View_EquipmentInfo> EquipmentDisplayDataSource { set; }

        int DivisionRowEquipmentTypeID { get; set; }

        int DivisionRowDivisionID { get; set; }

        bool EquipmentTypeRowHasDivision { get; set; }

        int EquipmentTypeRowID { get; set; }

        int EquipmentRowEquipmentTypeID { get; set; }

        bool DivisionRowHasEquipment { get; set; }

        int EquipmentRowDivisionID { get; set; }

        bool EquipmentTypechkHeavyEquipment { get; set; }

        bool DivisionchkHeavyEquipment { get; set; }

        bool EquipmentchkHeavyEquipment { set; }

        List<int> SelectedHeavyEquipments { get; }

        List<int> SelectedDisplayInResourceAllocation { get;}

        int EquipmentDisplayEquipmentID { get; set; }

        bool DivisionchkDisplayInResource { get; set; }

        bool EquipmentchkResourceAllocation { set; }

        bool EquipmentTypechkEquipmentTypeResAllocation { get; set; }
        #endregion
    }
}
