using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    public interface IRegionalMaintenanceView : IBaseView
    {
        #region [ Common ]

        string Username { get; set; }

        int RegionID { get; set; }

        bool EditMode { get; set; }

        Globals.RegionalMaintenance.FilterType FilterType { get; }

        string FilterValue { get; }

        bool ShowHideRegionalDisplay { set; }

        #endregion

        #region [ Dashboard ]

        #region [ Sort ]

        string[] OrderBy { get; }

        Globals.Common.Sort.RegionalMaintenanceSortColumns SortColumn { get; }

        Globals.Common.Sort.SortDirection SortDirection { get; }

        #endregion

        #region [ Region ]

        #region [ Fields ]

        bool RegionRowHasDivision { get; set; }

        string RegionRowRegionName { get; set; }

        int RegionRowRegionID { get; set; }

        #endregion

        IList<CS_Region> RegionDataSource { get; set; }

        IList<CS_Region> RegionList { get; set; }

        CS_Region RegionDataItem { get; set; }

        #endregion

        #region [ Division ]

        #region [ Fields ]

        bool DivisionRowHasEmployeeOrEquipment { get; set; }

        string DivisionRowDivisionName { get; set; }

        int DivisionRowRegionID { get; set; }

        int DivisionRowDivisionID { get; set; }

        #endregion

        IList<CS_Division> RegionDivisionList { get; set; }

        IList<CS_Division> DivisionDataSource { get; set; }

        CS_Division DivisionDataItem { get; set; }

        #endregion

        #region [ Employee ]

        IList<CS_Employee> EmployeeList { get; set; }

        IList<CS_Employee> EmployeeDataSource { get; set; }

        CS_Employee EmployeeDataItem { get; set; }

        #region [ Fields ]

        int EmployeeRowRegionID { get; set; }

        int EmployeeRowDivisionID { get; set; }

        string EmployeeRowEmployeeName { get; set; }

        #endregion

        #endregion

        #endregion

        #region [ Equipment ]

        IList<CS_View_EquipmentInfo> EquipmentList { get; set; }

        IList<CS_View_EquipmentInfo> ComboDataSource { get; set; }

        IList<CS_View_EquipmentInfo> EquipmentDataSource { get; set; }

        CS_View_EquipmentInfo ComboDataItem { get; set; }

        CS_View_EquipmentInfo EquipmentDataItem { get; set; }

        #region [ Fields ]

        int? ComboRowRegionID { get; set; }

        int ComboRowDivisionID { get; set; }

        string ComboRowName { get; set; }

        int? ComboRowID { get; set; }

        bool ComboRowHasEquipments { get; set; }

        int? EquipmentRowRegionID { get; set; }

        int EquipmentRowDivisionID { get; set; }

        int? EquipmentRowComboID { get; set; }

        string EquipmentRowName { get; set; }

        #endregion

        #endregion

        #region [ CRUD ]

        IList<CS_Division> DivisionList { get; set; }

        IList<int> SelectedDivisions { get; set; }

        int? selectedRVP { get; set; }

        IList<CS_Employee> RVPList { get; set; }

        #endregion
    }
}
