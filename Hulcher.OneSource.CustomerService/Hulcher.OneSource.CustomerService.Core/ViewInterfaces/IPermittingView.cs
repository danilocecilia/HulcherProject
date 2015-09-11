using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.DataContext.VO;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    public interface IPermittingView : IBaseView
    {
        #region [ Listing ]

        /// <summary>
        /// String used to order by permitting records
        /// </summary>
        string[] OrderBy { get; }

        /// <summary>
        /// Gets the selected Sort Column
        /// </summary>
        Globals.Permitting.PermittingSortColumns SortColumn { get; }

        /// <summary>
        /// Gets the selected Sort Direction
        /// </summary>
        Globals.Common.Sort.SortDirection SortDirection { get; }

        /// <summary>
        /// The First loaded list of Combos and Equipments
        /// </summary>
        List<CS_View_EquipmentInfo> EquipmentInfoListData { get; set; }

        /// <summary>
        /// The filtered Combos, based on the first list
        /// </summary>
        IList<CS_View_EquipmentInfo> FirstTierDataSource { get; set; }

        /// <summary>
        /// The Row representing the current selected item
        /// </summary>
        CS_View_EquipmentInfo FirstTierDataItem { get; set; }

        /// <summary>
        /// Gets or Sets the ComboId of the first repeater
        /// </summary>
        int FirstTierComboId { get; set; }

        /// <summary>
        /// Gets or Sets the JobId of the first repeater
        /// </summary>
        int FirstTierJobId { get; set; }

        /// <summary>
        /// Gets or Sets the ComboName of the first repeater
        /// </summary>
        string FirstTierComboName { get; set; }

        /// <summary>
        /// Gets or Sets the UnitNumber of the first repeater
        /// </summary>
        string FirstTierUnitNumber { get; set; }
        
        /// <summary>
        /// Gets or Sets the CreateDate of the first repeater
        /// </summary>
        DateTime FirstTierCreateDate { get; set; }

        /// <summary>
        /// Gets or Sets the DivisionNumber of the first repeater
        /// </summary>
        string FirstTierDivisionNumber { get; set; }

        /// <summary>
        /// Gets or Sets the DivisionState of the first repeater
        /// </summary>
        string FirstTierDivisionState { get; set; }

        /// <summary>
        /// Gets or Sets the Type/Descriptor of the first repeater
        /// </summary>
        string FirstTierTypeDescriptor { get; set; }

        /// <summary>
        /// Gets or Sets the JobNumer of the first repeater
        /// </summary>
        string FirstTierJobNumber { get; set; }

        /// <summary>
        /// The filtered Equipments, based on the selected Combo and first list
        /// </summary>
        IList<CS_View_EquipmentInfo> SecondTierDataSource { get; set; }

        /// <summary>
        /// The Row representing the current selected item
        /// </summary>
        CS_View_EquipmentInfo SecondTierDataItem { get; set; }

        /// <summary>
        /// Gets or Sets the JobId of the second repeater
        /// </summary>
        int SecondTierJobId { get; set; }

        /// <summary>
        /// Gets or Sets the UnitName of the second repeater
        /// </summary>
        string SecondTierUnitNumber { get; set; }

        /// <summary>
        /// Gets or Sets if the item is a Primary Unit
        /// </summary>
        bool SecondTierPrimaryUnit { get; set; }

        /// <summary>
        /// Gets or Sets the DivisionNumber of the second repeater
        /// </summary>
        string SecondTierDivisionNumber { get; set; }

        /// <summary>
        /// Gets or Sets the DivisionState of the second repeater
        /// </summary>
        string SecondTierDivisionState { get; set; }

        /// <summary>
        /// Gets or Sets the EquipmentType/Descriptor of the second repeater
        /// </summary>
        string SecondTierEquipmentTypeDescriptor { get; set; }

        /// <summary>
        /// Gets or Sets the JobNumber of the second repeater
        /// </summary>
        string SecondTierJobNumber { get; set; }

        /// <summary>
        /// Gets or Sets the CssClass of the second repeater
        /// </summary>
        string SecondTierItemCssClass { get; set; }

        /// <summary>
        /// Indicates if the Creation panel should be visible or not
        /// </summary>
        bool CreationPanelVisible { get; set; }

        /// <summary>
        /// Indicates if the Log panel should be visible or not
        /// </summary>
        bool LogPanelVisible { get; set; }

        #endregion

        #region [ History ]

        /// <summary>
        /// Set ComboHistory repeater DataSource
        /// </summary>
        IList<ComboLog> ComboHistoryLogDataSource { get; set; } 

        /// <summary>
        /// ComboHistoryRepeater DataItem
        /// </summary>
        ComboLog ComboHistoryRepeaterDataItem { get; set; }

        /// <summary>
        /// ComboHistory Name Label
        /// </summary>
        string ComboHistoryRowName { get; set; }

        /// <summary>
        /// ComboHistory Unit Numbers Label
        /// </summary>
        string ComboHistoryRowUnits { get; set; }

        /// <summary>
        /// ComboHistory Primary Number Label
        /// </summary>
        string ComboHistoryRowPrimary { get; set; }

        /// <summary>
        /// ComboHistory Primary Division Label
        /// </summary>
        string ComboHistoryRowDivision { get; set; }

        #endregion

        #region [ Insert/Update/ Delete ]

        #region [ Equipment Listing ]

        /// <summary>
        /// The enumerator the be used on the method that return wich list have to be retrieved
        /// </summary>
        Globals.ResourceAllocation.EquipmentFilters EquipmentFilters { get; set; }

        /// <summary>
        /// Get the value typed on  the filter
        /// </summary>
        string FilterValue { get; set; }

        /// <summary>
        /// Equipment List to fill the Equipment Grid
        /// </summary>
        IList<CS_View_EquipmentInfo> ListFilteredEquipmentInfo { get; set; }

        #endregion

        #region [ Combo ]

        /// <summary>
        /// Return if equipment is assigned to a job or not
        /// </summary>
        bool IsAssignedToJob { get; set; }

        /// <summary>
        /// Get or set the equipment combo entity
        /// </summary>
        CS_EquipmentCombo EquipmentCombo { get; set; }

        /// <summary>
        /// Get or sets the equipmentcombo id
        /// </summary>
        int? EquipmentComboId { get; set; }

        /// <summary>
        /// Get the equipmentcombo id from query string
        /// </summary>
        int? QueryStringEquipmentComboId { get; }

        /// <summary>
        /// Gets os sets the Combo Name field
        /// </summary>
        string ComboName { get; set; }

        /// <summary>
        /// Gets or sets the Combo Type field
        /// </summary>
        string ComboType { get; set; }

        /// <summary>
        /// Gets the Identifier of the selected Primary Equipment of a Combo
        /// </summary>
        int PrimaryEquipmentId { get; }

        /// <summary>
        /// Get the current username
        /// </summary>
        string UserName { get; set; }

        #endregion

        #region [ Combo Equipments - Shopping Cart ]

        /// <summary>
        /// Equipment Combo Children
        /// </summary>
        IList<EquipmentComboVO> EquipmentComboChildren { get; set; }

        /// <summary>
        /// Equipment Info List to fill shopping cart
        /// </summary>
        IList<EquipmentComboVO> EquipmentInfoShoppingCartDataSource { get; set; }

        /// <summary>
        /// Equipment Info unit
        /// </summary>
        EquipmentComboVO EquipmentInfoItem { get; set; }

        /// <summary>
        /// Is Primary field inside listing
        /// </summary>
        bool IsPrimaryObjectSelected { get; set; }

        /// <summary>
        /// Division Name field inside listing
        /// </summary>
        string DivisionSelected { get; set; }

        /// <summary>
        /// Unit number field inside listing
        /// </summary>
        string UnitNumberSelected { get; set; }

        /// <summary>
        /// Descriptor field inside listing
        /// </summary>
        string DescriptorSelected { get; set; }

        /// <summary>
        /// Get the items that was selected on the equipment grid
        /// </summary>
        IList<int> SelectedEquipmentsAdd { get; set; }

        /// <summary>
        /// Gets the Selected Equipment Id's to be removed
        /// </summary>
        IList<int> RemovedEquipments { get; set; }

        /// <summary>
        /// Indicates if the Save function worked as expected or not
        /// </summary>
        bool SavedSuccessfuly { get; set; }

        #endregion

        #endregion
    }
}
