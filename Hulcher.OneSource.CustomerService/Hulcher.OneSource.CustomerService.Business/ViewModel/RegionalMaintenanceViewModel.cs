using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core;
using System.Transactions;

namespace Hulcher.OneSource.CustomerService.Business.ViewModel
{
    public class RegionalMaintenanceViewModel
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of the Process DPI View Interface
        /// </summary>
        private IRegionalMaintenanceView _view;

        /// <summary>
        /// Instance of the RegionModel class
        /// </summary>
        private RegionModel _regionModel;

        /// <summary>
        /// Instance of the EmployeeModel class
        /// </summary>
        private EmployeeModel _employeeModel;

        /// <summary>
        /// Instance of the EquipmentModel class
        /// </summary>
        private EquipmentModel _equipmentModel;

        /// <summary>
        /// Instance of the DivisionModel class
        /// </summary>
        private DivisionModel _divisionModel;

        #endregion

        #region [ Constructor ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="view">Instance of the Process DPI View Interface</param>
        public RegionalMaintenanceViewModel(IRegionalMaintenanceView view)
        {
            _view = view;
            _regionModel = new RegionModel();
            _employeeModel = new EmployeeModel();
            _equipmentModel = new EquipmentModel();
            _divisionModel = new DivisionModel();
        }

        #endregion

        #region [ Methods ]

        #region [ Save ]

        public void SaveRegion()
        {
            _regionModel.SaveRegion(_view.RegionID, _view.SelectedDivisions.ToList(), _view.selectedRVP, _view.Username);
        }

        #endregion

        #region [ DashBoard ]

        /// <summary>
        /// Binds the lists based on filter
        /// </summary>
        public void BindDashboard()
        {
            if (_view.FilterType == Globals.RegionalMaintenance.FilterType.None || string.IsNullOrEmpty(_view.FilterValue))
            {
                _view.RegionList = new List<CS_Region>();
                _view.DivisionList = new List<CS_Division>();
                _view.EmployeeList = new List<CS_Employee>();
                _view.EquipmentList = new List<CS_View_EquipmentInfo>();
            }
            else
            {
                _view.RegionList = _regionModel.ListAllRegions(_view.FilterType, _view.FilterValue);
                _view.RegionDivisionList = _divisionModel.ListDivisionByRegionIDList(_view.RegionList.Select(e => e.ID).ToList(), _view.FilterType, _view.FilterValue);
                _view.EmployeeList = _employeeModel.ListEmployeeByRegionIDList(_view.RegionList.Select(e => e.ID).ToList(), _view.FilterType, _view.FilterValue);
                _view.EquipmentList = _equipmentModel.ListEquipmentByRegionIDList(_view.RegionList.Select(e => e.ID).ToList(), _view.FilterType, _view.FilterValue);
            }
        }

        #region [ Region ]

        /// <summary>
        /// Binds the Region Grid
        /// </summary>
        public void BindRegion()
        {
            _view.RegionDataSource = SortRegion(_view.RegionList);
        }

        public void SetRegionFields()
        {
            _view.RegionRowRegionName = _view.RegionDataItem.RegionAndRVPName;
            _view.RegionRowRegionID = _view.RegionDataItem.ID;
        }

        private IList<CS_Region> SortRegion(IList<CS_Region> regionList)
        {
            switch (_view.SortColumn)
            {
                case Globals.Common.Sort.RegionalMaintenanceSortColumns.Region:
                    if (_view.SortDirection == Globals.Common.Sort.SortDirection.Ascending)
                        return regionList.OrderBy(e => e.Name).ToList();
                    else
                        return regionList.OrderByDescending(e => e.Name).ToList();
                case Globals.Common.Sort.RegionalMaintenanceSortColumns.Division:
                case Globals.Common.Sort.RegionalMaintenanceSortColumns.EmployeeEquipment:
                case Globals.Common.Sort.RegionalMaintenanceSortColumns.None:
                default:
                    return regionList.OrderBy(e => e.Name).ToList();
            }
        }

        #endregion

        #region [ Division ]

        public void BindDivision()
        {
            _view.DivisionDataSource = SortDivision(_view.RegionDivisionList.Where(e => e.CS_Region_Division.Any(f => f.RegionID == _view.RegionDataItem.ID)).ToList());
        }

        public void SetDivisionFields()
        {
            _view.RegionRowHasDivision = true;

            _view.DivisionRowRegionID = _view.RegionDataItem.ID;
            _view.DivisionRowDivisionID = _view.DivisionDataItem.ID;
            _view.DivisionRowDivisionName = _view.DivisionDataItem.Name;
        }

        private IList<CS_Division> SortDivision(IList<CS_Division> divisionList)
        {
            switch (_view.SortColumn)
            {
                case Globals.Common.Sort.RegionalMaintenanceSortColumns.Division:
                    if (_view.SortDirection == Globals.Common.Sort.SortDirection.Ascending)
                        return divisionList.OrderBy(e => e.Name).ToList();
                    else
                        return divisionList.OrderByDescending(e => e.Name).ToList();
                case Globals.Common.Sort.RegionalMaintenanceSortColumns.Region:
                case Globals.Common.Sort.RegionalMaintenanceSortColumns.EmployeeEquipment:
                case Globals.Common.Sort.RegionalMaintenanceSortColumns.None:
                default:
                    return divisionList.OrderBy(e => e.Name).ToList();
            }
        }

        #endregion

        #region [ Employee ]

        public void BindEmployee()
        {
            _view.EmployeeDataSource = SortEmployee(_view.EmployeeList.Where(e => e.CS_Division.ID == _view.DivisionDataItem.ID).ToList());
        }

        public void SetEmployeeFields()
        {
            _view.DivisionRowHasEmployeeOrEquipment = true;

            _view.EmployeeRowRegionID = _view.RegionDataItem.ID;
            _view.EmployeeRowDivisionID = _view.DivisionDataItem.ID;
            _view.EmployeeRowEmployeeName = _view.EmployeeDataItem.FullName;
        }

        private IList<CS_Employee> SortEmployee(IList<CS_Employee> employeeList)
        {
            switch (_view.SortColumn)
            {
                case Globals.Common.Sort.RegionalMaintenanceSortColumns.EmployeeEquipment:
                    if (_view.SortDirection == Globals.Common.Sort.SortDirection.Ascending)
                        return employeeList.OrderBy(e => e.FullName).ToList();
                    else
                        return employeeList.OrderByDescending(e => e.FullName).ToList();
                case Globals.Common.Sort.RegionalMaintenanceSortColumns.Region:
                case Globals.Common.Sort.RegionalMaintenanceSortColumns.Division:
                case Globals.Common.Sort.RegionalMaintenanceSortColumns.None:
                default:
                    return employeeList.OrderBy(e => e.FullName).ToList();
            }
        }

        #endregion

        #region [ Equipment ]

        public void BindCombo()
        {
            _view.ComboDataSource = SortCombo(
                _view.EquipmentList.ToList().FindAll(
                    e => e.ComboID.HasValue &&
                         e.DivisionID == _view.DivisionDataItem.ID &&
                         e.IsPrimary == 1).Distinct(new Globals.QuickReference.CS_View_EquipmentInfo_Combo_Comparer()).ToList(),
                _view.EquipmentList.ToList().FindAll(
                    e => !e.ComboID.HasValue &&
                         e.DivisionID == _view.DivisionDataItem.ID));
        }

        public void SetComboFields()
        {
            _view.DivisionRowHasEmployeeOrEquipment = true;

            _view.ComboRowRegionID = _view.ComboDataItem.RegionID;
            _view.ComboRowDivisionID = _view.ComboDataItem.DivisionID;
            _view.ComboRowID = _view.ComboDataItem.ComboID;

            if (_view.ComboRowID.HasValue)
            {
                _view.ComboRowName = _view.ComboDataItem.ComboName;
            }
            else
            {
                _view.ComboRowName = string.Format("{0} - {1}", _view.ComboDataItem.UnitNumber, _view.ComboDataItem.Descriptor);
            }
        }

        private IList<CS_View_EquipmentInfo> SortCombo(IList<CS_View_EquipmentInfo> comboList, IList<CS_View_EquipmentInfo> equipmentList)
        {
            List<CS_View_EquipmentInfo> returnList = new List<CS_View_EquipmentInfo>();
            switch (_view.SortColumn)
            {
                case Globals.Common.Sort.RegionalMaintenanceSortColumns.EmployeeEquipment:
                    if (_view.SortDirection == Globals.Common.Sort.SortDirection.Ascending)
                    {
                        returnList.AddRange(comboList.OrderBy(e => e.ComboName.ToString()).ToList());
                        returnList.AddRange(equipmentList.OrderBy(e => e.UnitNumber).ThenBy(e => e.Descriptor).ToList());
                    }
                    else
                    {
                        returnList.AddRange(equipmentList.OrderByDescending(e => e.UnitNumber).ThenByDescending(e => e.Descriptor).ToList());
                        returnList.AddRange(comboList.OrderByDescending(e => e.ComboName.ToString()).ToList());
                    }
                    break;
                case Globals.Common.Sort.RegionalMaintenanceSortColumns.None:
                case Globals.Common.Sort.RegionalMaintenanceSortColumns.Region:
                case Globals.Common.Sort.RegionalMaintenanceSortColumns.Division:
                default:
                    returnList.AddRange(comboList.OrderBy(e => e.ComboName.ToString()).ToList());
                    returnList.AddRange(equipmentList.OrderBy(e => e.UnitNumber).ThenBy(e => e.Descriptor).ToList());
                    break;

            }
            return returnList;
        }

        public void BindEquipment()
        {
            _view.EquipmentDataSource = SortEquipment(_view.EquipmentList.ToList().FindAll(
                e => e.ComboID.HasValue &&
                     e.ComboID == _view.ComboDataItem.ComboID && 
                     e.DivisionID == _view.DivisionDataItem.ID &&
                     e.Active == true).ToList());
        }

        public void SetEquipmentFields()
        {
            _view.ComboRowHasEquipments = true;

            _view.EquipmentRowRegionID = _view.EquipmentDataItem.RegionID;
            _view.EquipmentRowDivisionID = _view.EquipmentDataItem.DivisionID;
            _view.EquipmentRowComboID = _view.EquipmentDataItem.ComboID;
            _view.EquipmentRowName = string.Format("{0} - {1}", _view.EquipmentDataItem.UnitNumber, _view.EquipmentDataItem.Descriptor);
        }

        private IList<CS_View_EquipmentInfo> SortEquipment(IList<CS_View_EquipmentInfo> equipmentList)
        {
            switch (_view.SortColumn)
            {
                case Globals.Common.Sort.RegionalMaintenanceSortColumns.EmployeeEquipment:
                    if (_view.SortDirection == Globals.Common.Sort.SortDirection.Ascending)
                        return equipmentList.OrderBy(e => e.UnitNumber).ThenBy(e => e.Descriptor).ToList();
                    else
                        return equipmentList.OrderByDescending(e => e.UnitNumber).ThenByDescending(e => e.Descriptor).ToList();
                case Globals.Common.Sort.RegionalMaintenanceSortColumns.None:
                case Globals.Common.Sort.RegionalMaintenanceSortColumns.Region:
                case Globals.Common.Sort.RegionalMaintenanceSortColumns.Division:
                default:
                    return equipmentList.OrderBy(e => e.UnitNumber).ThenBy(e => e.Descriptor).ToList();
            }
        }

        #endregion

        #endregion

        #region [ Division ]

        public void BindDivisionList()
        {
            IList<CS_Division> selectedDivisions = _regionModel.ListDivisionByRegion(_view.RegionID);
            IList<CS_Division> divisionList = _divisionModel.ListNotAssociatedDivisions();

            _view.DivisionList = MergeDivisionList(selectedDivisions, divisionList);

            if (selectedDivisions.Count > 0)
                _view.SelectedDivisions = selectedDivisions.Select(e => e.ID).ToList();
        }

        private IList<CS_Division> MergeDivisionList(IList<CS_Division> selectedDivisions, IList<CS_Division> divisionList)
        {
            List<CS_Division> returnList = new List<CS_Division>();

            returnList.AddRange(divisionList);
            returnList.AddRange(selectedDivisions);

            return returnList.OrderBy(e => e.Name).ToList();
        }

        #endregion

        #region [ RPV ]

        public void BindRegionalVicePresident()
        {
            CS_Employee selectedRVP = _regionModel.GetRVPByRegion(_view.RegionID);
            IList<CS_Employee> RVPList = _employeeModel.ListNotAssignedEmployeeRVP();

            _view.RVPList = MergeRVPList(selectedRVP, RVPList);

            if (null != selectedRVP && selectedRVP.ID != 0)
                _view.selectedRVP = selectedRVP.ID;
            else
                _view.selectedRVP = null;
        }

        private IList<CS_Employee> MergeRVPList(CS_Employee selectedRVP, IList<CS_Employee> RVPList)
        {
            List<CS_Employee> returnList = new List<CS_Employee>();

            if (null != selectedRVP && selectedRVP.ID != 0)
                returnList.Add(selectedRVP);

            returnList.AddRange(RVPList);

            return returnList.OrderBy(e => e.FullName).ToList();
        }

        #endregion

        #endregion
    }
}
