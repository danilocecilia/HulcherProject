using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    /// <summary>
    /// Quick Reference View Interface
    /// </summary>
    public interface IQuickReferenceView : IBaseView
    {
        /// <summary>
        /// Enumerator for the Equipment Filter
        /// </summary>
        Globals.ResourceAllocation.EquipmentFilters EquipmentFilter { get; }

        /// <summary>
        /// Value of the TextBox Filter for the Equipment Filter
        /// </summary>
        string EquipmentFilterValue { get; }

        IList<CS_View_EquipmentInfo> FirstTierDataSource { get; set; }

        IList<CS_View_EquipmentInfo> SecondTierDataSource { get; set; }

        List<CS_View_EquipmentInfo> EquipmentInfoListData { get; set; }

        CS_View_EquipmentInfo FirstTierDataItem { get; }

        CS_View_EquipmentInfo SecondTierDataItem { get; }

        string FirstTierItemDivisionName { set; }

        string FirstTierItemDivisionState { set; }

        string FirstTierItemComboName  { set; }
        
        string FirstTierItemUnitNumber  { set; }
        
        string FirstTierItemDescriptor { set; }
        
        string FirstTierItemStatus { set; }
        
        string FirstTierItemJobLocation { set; }
        
        string FirstTierItemLastCallEntryText { set; }
        
        int[] FirstTierItemLastCallEntryID { set; }
        
        string FirstTierItemOperationStatus { set; }
        
        string FirstTierItemJobNumberText { set; }
        
        int FirstTierItemJobNumberID { set; }

        int? FirstTierItemComboID { set; }

        string SecondTierItemDivisionName { set; }

        string SecondTierItemDivisionState { set; }

        string SecondTierItemUnitNumber { set; }

        string SecondTierItemDescriptor { set; }

        string SecondTierItemStatus { set; }

        string SecondTierItemJobLocation { set; }

        string SecondTierItemLastCallEntryText { set; }

        int[] SecondTierItemLastCallEntryID { set; }

        string SecondTierItemOperationStatus { set; }

        string SecondTierItemJobNumberText { set; }

        int SecondTierItemJobNumberID { set; }

        string SecondTierItemCssClass { set; }

        Globals.Common.Sort.EquipmentSortColumns SortColumn { get; }

        Globals.Common.Sort.SortDirection SortDirection { get; }
    }
}
