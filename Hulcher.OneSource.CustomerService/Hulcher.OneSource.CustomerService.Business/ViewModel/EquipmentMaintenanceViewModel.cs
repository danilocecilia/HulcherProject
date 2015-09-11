using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.ViewModel;
using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.DataContext;

using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Hulcher.OneSource.CustomerService.Business.ViewModel
{
    public class EquipmentMaintenanceViewModel
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of View Interface
        /// </summary>
        private IEquipmentMaintenanceView _view;

        /// <summary>
        /// Instance of Equipment Model Class
        /// </summary>
        private EquipmentModel _equipmentModel;

        /// <summary>
        /// Instance of CallLog Model Class
        /// </summary>
        private CallLogModel _callLogModel;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="view">Instance of View Interface</param>
        public EquipmentMaintenanceViewModel(IEquipmentMaintenanceView view)
        {
            _view = view;
            _equipmentModel = new EquipmentModel();
            _callLogModel = new CallLogModel();
        }

        #endregion

        #region [ Methods ]

        #region [ EquipmentDown ]
        /// <summary>
        /// Set values to the entity CS_EquipmentDownHistory to be saved on DB
        /// </summary>
        /// <returns></returns>
        private CS_EquipmentDownHistory SetEquipmentDownHistory()
        {
            CS_EquipmentDownHistory equipmentDownHistory = new CS_EquipmentDownHistory();

            if (_view.DownHistoryStartDate.HasValue)
            {
                equipmentDownHistory.DownHistoryStartDate = _view.DownHistoryStartDate.Value;
            }
            if (_view.EquipmentDownDuration.HasValue)
                equipmentDownHistory.Duration = _view.EquipmentDownDuration.Value;

            equipmentDownHistory.Active = true;
            equipmentDownHistory.CreatedBy = _view.Username;
            equipmentDownHistory.CreationDate = DateTime.Now;
            equipmentDownHistory.EquipmentID = _view.EquipmentID;
            equipmentDownHistory.ModificationDate = DateTime.Now;

            if (_view.DownHistoryEndDate.HasValue)
                equipmentDownHistory.DownHistoryEndDate = _view.DownHistoryEndDate.Value;


            equipmentDownHistory.ModifiedBy = _view.Username;
            //equipmentDownHistory.ModificationID;
            //equipmentDownHistory.CreationID;

            return equipmentDownHistory;
        }

        private IList<CS_EquipmentDownHistory> SetComboEquipmentDownHistory()
        {
            IList<CS_EquipmentDownHistory> comboEquipmentDownHistory = new List<CS_EquipmentDownHistory>();

            CS_View_EquipmentInfo eqInfo = _equipmentModel.GetEquipmentInfoByEquipmentID(_view.EquipmentID);// _equipmentInfoRepository.Get(e => e.EquipmentID == _view.EquipmentID);
            if (eqInfo.IsPrimary == 1 && eqInfo.ComboID != null && eqInfo.ComboID.HasValue)
            {
                IList<CS_Equipment> equips = _equipmentModel.ListEquipmentsFromPrimaryEquipment(eqInfo.EquipmentID, eqInfo.ComboID.Value);
                for (int j = 0; j < equips.Count; j++)
                {
                    if (_view.EquipmentID != equips[j].ID)
                    {
                        CS_EquipmentDownHistory equipmentDownHistory = new CS_EquipmentDownHistory();

                        if (_view.DownHistoryStartDate.HasValue)
                        {
                            equipmentDownHistory.DownHistoryStartDate = _view.DownHistoryStartDate.Value;
                        }
                        if (_view.EquipmentDownDuration.HasValue)
                            equipmentDownHistory.Duration = _view.EquipmentDownDuration.Value;

                        equipmentDownHistory.Active = true;
                        equipmentDownHistory.CreatedBy = _view.Username;
                        equipmentDownHistory.CreationDate = DateTime.Now;
                        equipmentDownHistory.EquipmentID = equips[j].ID;
                        equipmentDownHistory.ModificationDate = DateTime.Now;

                        if (_view.DownHistoryEndDate.HasValue)
                            equipmentDownHistory.DownHistoryEndDate = _view.DownHistoryEndDate.Value;


                        equipmentDownHistory.ModifiedBy = _view.Username;
                        //equipmentDownHistory.ModificationID;
                        //equipmentDownHistory.CreationID;

                        comboEquipmentDownHistory.Add(equipmentDownHistory);
                    }
                }
            }
            return comboEquipmentDownHistory;
        }

        private void LoadDownHistory(List<CS_EquipmentDownHistory> equipmentDownHistories)
        {
            _view.DownHistoryGridDataSource = equipmentDownHistories.Where(w => !w.Active).ToList();

            CS_EquipmentDownHistory eDownHistory = equipmentDownHistories.Where(w => w.Active).FirstOrDefault();
            if (null != eDownHistory)
            {
                _view.EquipmentDownDuration = eDownHistory.Duration;
                _view.DownHistoryStartDate = eDownHistory.DownHistoryStartDate;
            }
            else
            {
                _view.EquipmentDownDuration = null;
            }

        }
        #endregion

        #region [ Equipment ]

        /// <summary>
        /// Get the division actual from the current equipment
        /// </summary>
        private void GetDivisionActual(CS_Division division)
        {
            if (null != division)
            {
                _view.ActualEquipmentDivision = division.Name;
            }
        }

        /// <summary>
        /// Executes the find method to filter equipments
        /// </summary>
        public void ListFilteredEquipment()
        {
            _view.EquipmentList = _equipmentModel.ListFilteredEquipment(_view.FilterType, _view.FilterValue);
        }

        /// <summary>
        /// Binds an equipment row
        /// </summary>
        public void BindEquipmentRow()
        {
            if (null != _view.EquipmentRowDataItem)
            {
                _view.EquipmentRowEquipmentID = _view.EquipmentRowDataItem.EquipmentID;
                _view.EquipmentRowDivisionName = _view.EquipmentRowDataItem.DivisionName;
                _view.EquipmentRowDivisionState = _view.EquipmentRowDataItem.DivisionState;
                _view.EquipmentRowComboName = _view.EquipmentRowDataItem.ComboName;
                _view.EquipmentRowUnitNumber = _view.EquipmentRowDataItem.UnitNumber;
                _view.EquipmentRowDescription = _view.EquipmentRowDataItem.Descriptor;

                _view.EquipmentRowJobLocation = _view.EquipmentRowDataItem.JobLocation;
                _view.EquipmentRowJobNumber = _view.EquipmentRowDataItem.JobNumber;
                _view.EquipmentRowJobID = _view.EquipmentRowDataItem.JobID;
                _view.EquipmentRowStatus = _view.EquipmentRowDataItem.Status;

                _view.EquipmentRowLastCallEntryDescription = _view.EquipmentRowDataItem.Type;

                if (_view.EquipmentRowDataItem.CallLogID > 0)
                {

                    if (!_callLogModel.GetCallTypeByDescription(_view.EquipmentRowDataItem.Type).IsAutomaticProcess)
                    _view.EquipmentRowLastCallEntryID = new int?[] { 
                        _view.EquipmentRowDataItem.CallLogJobID,
                        _view.EquipmentRowDataItem.CallLogID
                    };
                }

                _view.EquipmentRowOperationalStatus = _view.EquipmentRowDataItem.EquipmentStatus;

                if (_view.EquipmentRowDataItem.JobID.HasValue && _view.EquipmentRowDataItem.JobID == Globals.GeneralLog.ID)
                    _view.EnableJobNumberLink = false;
            }
        }

        /// <summary>
        /// Loads an existing equipment in the form
        /// </summary>
        public void LoadEquipment()
        {
            CS_Equipment equipment = _equipmentModel.GetEquipment(_view.EquipmentID);
            if (null != equipment)
            {
                _view.EquipmentName = equipment.Name;
                _view.EquipmentDescription = equipment.Description;
                _view.EquipmentType = equipment.CS_EquipmentType.Name;
                _view.EquipmentLicensePlate = equipment.LicensePlate;
                _view.EquipmentSerialNumber = equipment.SerialNumber;
                if (equipment.Year.HasValue)
                    _view.EquipmentYear = equipment.Year.Value.ToString();
                else
                    _view.EquipmentYear = string.Empty;
                _view.EquipmentNotes = equipment.Notes;
                _view.EquipmentBodyType = equipment.BodyType;
                _view.EquipmentMake = equipment.Make;
                _view.EquipmentModel = equipment.Model;
                _view.EquipmentFunction = equipment.EquipmentFunction;
                _view.EquipmentAssignedTo = equipment.AssignedTo;
                _view.EquipmentRegisteredState = equipment.RegisteredState;
                _view.EquipmentAttachPanelBoss = equipment.AttachPanelBoss;
                _view.EquipmentAttachPileDriver = equipment.AttachPileDriver;
                _view.EquipmentAttachSlipSheet = equipment.AttachSlipSheet;
                _view.EquipmentAttachTieClamp = equipment.AttachTieClamp;
                _view.EquipmentAttachTieInserter = equipment.AttachTieInserter;
                _view.EquipmentAttachTieTamper = equipment.AttachTieTamper;
                _view.EquipmentAttachUnderCutter = equipment.AttachUndercutter;

                _view.EquipmentStatus = equipment.Status;
                _view.IsSeasonal = equipment.Seasonal;
                _view.IsHeavyEquipment = equipment.HeavyEquipment;
                _view.DisplayInResourceAllocation = equipment.DisplayInResourceAllocation;
                // TODO: Coverage

                _view.CoverageStartDate = DateTime.Now;
                _view.DownHistoryStartDate = DateTime.Now;

                VerifyIfResourceIsAssignedToJob(equipment.CS_Resource.FirstOrDefault(e => e.Active));
                LoadEquipmentCoverageFields(equipment.CS_EquipmentCoverage.FirstOrDefault(e => e.Active));
                GetDivisionActual(equipment.CS_Division);
                ShowHideEquipmentStatusDurantionFields(equipment.CS_EquipmentDownHistory.FirstOrDefault(e => e.Active));
                EnableDisableCoverageRequiredFields();
                LoadDownHistory(equipment.CS_EquipmentDownHistory.ToList());
                LoadWhiteLightHistory(equipment.CS_EquipmentWhiteLight.ToList());
                LoadCoverageHistory(equipment.CS_EquipmentCoverage.ToList());
            }


        }

        /// <summary>
        /// Set values get from page to the entity CS_Equipment
        /// </summary>
        /// <returns></returns>
        public CS_Equipment SetEquipment()
        {
            CS_Equipment equipment = new CS_Equipment();

            equipment.ID = _view.EquipmentID;
            equipment.ModificationDate = DateTime.Now;
            equipment.ModifiedBy = _view.Username;
            equipment.Status = _view.EquipmentStatus;

            return equipment;
        }

        public void ShowHideEquipmentStatusDurantionFields()
        {
            CS_EquipmentDownHistory equipmentDownHistory = _equipmentModel.GetEquipmentDownHistory(_view.EquipmentID);
            ShowHideEquipmentStatusDurantionFields(equipmentDownHistory);
        }

        private void ShowHideEquipmentStatusDurantionFields(CS_EquipmentDownHistory equipmentDownHistory)
        {
            if (_view.EquipmentStatus.Equals(Globals.EquipmentMaintenance.Status.Up.ToString()))
            {
                _view.DownHistoryEndDate = DateTime.Now;

                if (null != equipmentDownHistory)
                {
                    _view.EquipmentStatusUpDateFieldsVisibility = true;
                    _view.EquipmentStatusUpDateTimeRequired = true;                    

                    _view.EquipmentStatusDateFieldsVisibility = false;
                    _view.EquipmentStatusDateTimeRequired = false;
                    _view.EquipmentStatusDurationRequired = false;
                }
                else
                {
                    _view.EquipmentStatusUpDateFieldsVisibility = false;
                    _view.EquipmentStatusUpDateTimeRequired = false;

                    _view.EquipmentStatusDateFieldsVisibility = false;
                    _view.EquipmentStatusDateTimeRequired = false;
                    _view.EquipmentStatusDurationRequired = false;
                }
            }
            else
            {
                _view.EquipmentStatusUpDateFieldsVisibility = false;
                _view.EquipmentStatusUpDateTimeRequired = false;

                _view.EquipmentStatusDateFieldsVisibility = true;
                _view.EquipmentStatusDateTimeRequired = true;
                _view.EquipmentStatusDurationRequired = true;
            }
        }

        /// <summary>
        /// Apply coverage enable/disable panel when equipment assigned to a job
        /// </summary>
        private void VerifyIfResourceIsAssignedToJob(CS_Resource resource)
        {
            bool IsAssigned = (resource != null) ? true : false;
        }
        #endregion

        #region [ EquipmentDisplay ]
        #region [ Equipment Type ]
        private IList<CS_EquipmentType> SortEquipmentType(IList<CS_EquipmentType> equipmentTypesList)
        {
            switch (_view.SortColumn)
            {
                case Globals.Common.Sort.EquipmentDisplaySortColumns.UnitType:
                    if (_view.SortDirection == Globals.Common.Sort.SortDirection.Ascending)
                        return equipmentTypesList.OrderBy(e => e.Number).ToList();
                    else
                        return equipmentTypesList.OrderByDescending(e => e.Number).ToList();
                default:
                    return equipmentTypesList.OrderBy(e => e.Number).ToList();
            }
        }

        public void BindFirstTierEquipmentTypeDisplay()
        {
            _view.EquipmentTypeDataSource = SortEquipmentType(_view.EquipmentTypeList);
        }

        public void SetFirstTierEquipmentTypeList()
        {
            _view.EquipmentTypechkEquipmentTypeResAllocation = !_view.EquipmentListDisplay.Any(e => e.EquipmentTypeID == _view.FirstTierDataItem.ID && !e.DisplayInResourceAllocation);
            _view.EquipmentTypechkHeavyEquipment = !_view.EquipmentListDisplay.Any(e => e.EquipmentTypeID == _view.FirstTierDataItem.ID && !e.HeavyEquipment);
            _view.EquipmentTypeRowID = _view.FirstTierDataItem.ID;
            _view.FirstTierItemUnitType = _view.FirstTierDataItem.Number;

        }
        #endregion

        #region [ Division ]
        public void GetSecondTierDivisionList()
        {


            _view.DivisionDataSource =
                SortDivision(_view.EquipmentListDisplay.Where(e => e.EquipmentTypeID == _view.FirstTierDataItem.ID).
                    Distinct(new Globals.EquipmentMaintenance.CS_View_EquipmentInfo_Division_Comparer()).ToList());
        }

        private IList<CS_View_EquipmentInfo> SortDivision(IList<CS_View_EquipmentInfo> divisionList)
        {
            switch (_view.SortColumn)
            {
                case Globals.Common.Sort.EquipmentDisplaySortColumns.DivisionName:
                    if (_view.SortDirection == Globals.Common.Sort.SortDirection.Ascending)
                        return divisionList.OrderBy(e => e.DivisionName).ToList();
                    else
                        return divisionList.OrderByDescending(e => e.DivisionName).ToList();
                default:
                    return divisionList.OrderBy(e => e.DivisionName).ToList();
            }
        }

        public void SetSecondTierDivisionList()
        {
            _view.EquipmentTypeRowHasDivision = true;

            _view.DivisionchkHeavyEquipment = !_view.EquipmentListDisplay.Any(e => e.EquipmentTypeID == _view.FirstTierDataItem.ID && e.DivisionID == _view.SecondTierDataItem.DivisionID && !e.HeavyEquipment);
            _view.DivisionchkDisplayInResource = !_view.EquipmentListDisplay.Any(e => e.EquipmentTypeID == _view.FirstTierDataItem.ID && e.DivisionID == _view.SecondTierDataItem.DivisionID && !e.DisplayInResourceAllocation);
            _view.DivisionRowEquipmentTypeID = _view.FirstTierDataItem.ID;
            _view.DivisionRowDivisionID = _view.SecondTierDataItem.DivisionID;
            _view.SecondTierItemDivisionName = _view.SecondTierDataItem.DivisionName;
        }

        #endregion

        #region [ Equipment ]
        public void GetThirdTierEquipmentList()
        {
            IList<CS_View_EquipmentInfo> teste =
                _view.EquipmentListDisplay.Where(e => e.EquipmentTypeID == _view.SecondTierDataItem.EquipmentTypeID &&
                                                      e.DivisionID == _view.SecondTierDataItem.DivisionID).ToList();

            _view.EquipmentDisplayDataSource =
                SortEquipmentInfo(_view.EquipmentListDisplay.Where(e => e.EquipmentTypeID == _view.SecondTierDataItem.EquipmentTypeID &&
                    e.DivisionID == _view.SecondTierDataItem.DivisionID).ToList());
        }

        private IList<CS_View_EquipmentInfo> SortEquipmentInfo(IList<CS_View_EquipmentInfo> equipmentInfoList)
        {
            switch (_view.SortColumn)
            {
                case Globals.Common.Sort.EquipmentDisplaySortColumns.UnitNumber:
                    if (_view.SortDirection == Globals.Common.Sort.SortDirection.Ascending)
                        return equipmentInfoList.OrderBy(e => e.UnitNumber).ToList();
                    else
                        return equipmentInfoList.OrderByDescending(e => e.UnitNumber).ToList();
                case Globals.Common.Sort.EquipmentDisplaySortColumns.UnitDescription:
                    if (_view.SortDirection == Globals.Common.Sort.SortDirection.Ascending)
                        return equipmentInfoList.OrderBy(e => e.Descriptor).ToList();
                    else
                        return equipmentInfoList.OrderByDescending(e => e.Descriptor).ToList();
                default:
                    return equipmentInfoList.OrderBy(e => e.UnitNumber).ToList();
            }
        }

        public void SetThirdTierEquipmentRowList()
        {
            _view.DivisionRowHasEquipment = true;
            //_view.SelectedCheckBoxesH = _view.ThirdTierDataItem.EquipmentID.ToString();
            _view.EquipmentchkResourceAllocation = _view.ThirdTierDataItem.DisplayInResourceAllocation;
            _view.EquipmentchkHeavyEquipment = _view.ThirdTierDataItem.HeavyEquipment;
            _view.EquipmentRowEquipmentTypeID = _view.FirstTierDataItem.ID;
            _view.EquipmentRowDivisionID = _view.SecondTierDataItem.DivisionID;
            _view.EquipmentDisplayEquipmentID = _view.ThirdTierDataItem.EquipmentID;
            _view.ThirdTierItemUnitNumber = _view.ThirdTierDataItem.UnitNumber;
            _view.ThirdTierItemUnitDescription = _view.ThirdTierDataItem.Descriptor;
        }
        #endregion

        #region [ Save ]
        public void UpdateEquipmentDisplay()
        {
            List<int> lstSelectedHeavyEquipments = _view.SelectedHeavyEquipments;

            List<int> lstSelectedDisplayInResourceAllocation = _view.SelectedDisplayInResourceAllocation;

            IList<CS_View_EquipmentInfo> lstEquipmentInfo = _view.EquipmentListDisplay;

            _equipmentModel.UpdateEquipmentDisplay(lstSelectedHeavyEquipments, lstSelectedDisplayInResourceAllocation, lstEquipmentInfo, _view.Username);
        }
        #endregion

        public void BindManagementEquipmentDashboard()
        {
            if (_view.FilterTypeEquipmentDisplay == Globals.EquipmentMaintenance.FilterType.None || string.IsNullOrEmpty(_view.FilterValueEquipmentDisplay))
            {
                _view.EquipmentTypeList = new List<CS_EquipmentType>();
                _view.EquipmentList = new List<CS_View_EquipmentInfo>();
            }
            else
            {
                _view.EquipmentTypeList = _equipmentModel.ListAllEquipmentType(_view.FilterTypeEquipmentDisplay, _view.FilterValueEquipmentDisplay);
                _view.EquipmentListDisplay = _equipmentModel.ListEquipmentByEquipmentType(_view.EquipmentTypeList.Select(e => e.ID).ToList(), _view.FilterTypeEquipmentDisplay, _view.FilterValueEquipmentDisplay);
            }
        }
        #endregion

        #region [ Equipment Coverage ]

        /// <summary>
        /// Set the values to be saved on entity CS_EquipmentCoverage on DB
        /// </summary>
        /// <returns></returns>
        private CS_EquipmentCoverage SetEquipmentCoverage()
        {
            CS_EquipmentCoverage equipmentCoverage = new CS_EquipmentCoverage();

            equipmentCoverage.Active = true;
            if (_view.CoverageStartDate.HasValue)
                equipmentCoverage.CoverageStartDate = _view.CoverageStartDate.Value;

            if (_view.EquipmentCoverageDuration.HasValue)
                equipmentCoverage.Duration = _view.EquipmentCoverageDuration.Value;

            equipmentCoverage.CreatedBy = _view.Username;
            equipmentCoverage.CreationDate = DateTime.Now;
            equipmentCoverage.DivisionID = _view.DivisionID;
            equipmentCoverage.EquipmentID = _view.EquipmentID;

            equipmentCoverage.ModificationDate = DateTime.Now;

            if (_view.CoverageEndDate.HasValue)
                equipmentCoverage.CoverageEndDate = _view.CoverageEndDate.Value;

            equipmentCoverage.ModifiedBy = _view.Username;
            //equipmentCoverage.CreationID;
            //equipmentCoverage.ModificationID;

            return equipmentCoverage;
        }

        public void EnableDisableCoverageRequiredFields()
        {
            if (_view.IsEquipmentCoverage)
            {
                _view.EquipmentCoverageFieldsRequired = true;
            }
            else
            {
                _view.EquipmentCoverageFieldsRequired = false;
            }
        }

        private void LoadEquipmentCoverageFields(CS_EquipmentCoverage equipmentCoverage)
        {
            if (null != equipmentCoverage && !equipmentCoverage.CoverageEndDate.HasValue)
            {
                _view.IsEquipmentCoverage = true;
                _view.EquipmentCoverageStartFields = true;
                _view.EquipmentCoverageEndFields = false;
                _view.CoverageEndDate = DateTime.Now;
            }
            else
            {
                _view.IsEquipmentCoverage = false;
                _view.EquipmentCoverageStartFields = false;
                _view.EquipmentCoverageEndFields = false;
                _view.CoverageEndDate = null;
                _view.DivisionName = string.Empty;
                _view.EquipmentCoverageDuration = null;
            }
        }

        private void LoadCoverageHistory(List<CS_EquipmentCoverage> equipmentCoverages)
        {
            _view.CoverageHistoryGridDataSource = equipmentCoverages;

            CS_EquipmentCoverage eCoverage = equipmentCoverages.Where(w => w.Active).FirstOrDefault();

            if (eCoverage != null)
            {
                _view.DivisionID = eCoverage.DivisionID;
                _view.DivisionName = eCoverage.CS_Division.Name;
                _view.CoverageStartDate = eCoverage.CoverageStartDate;
                _view.EquipmentCoverageDuration = eCoverage.Duration;
            }
        }
        #endregion

        #region [ Equipment WhiteLight ]
        /// <summary>
        /// List All equipments that are marked as Heavy Weight (for the default load of the page)
        /// </summary>
        public void ListHeavyWeightEquipment()
        {
            _view.EquipmentList = _equipmentModel.ListAllHeavyCombo();
        }

        /// <summary>
        /// Load whitelight grid history
        /// </summary>
        private void LoadWhiteLightHistory(IList<CS_EquipmentWhiteLight> whiteLightHistory)
        {
            _view.WhiteLightHistoryGridDataSource = whiteLightHistory;

            _view.IsWhiteLight = whiteLightHistory.Any(e => e.Active);
            _view.WhiteLightNotes = string.Empty;
        }

        /// <summary>
        /// Set the values to be saved on entity CS_EquipmentWhiteLight on DB
        /// </summary>
        /// <returns></returns>
        private CS_EquipmentWhiteLight SetEquipmentWhiteLight()
        {
            CS_EquipmentWhiteLight equipmentWhiteLight = new CS_EquipmentWhiteLight();

            equipmentWhiteLight.Active = true;
            equipmentWhiteLight.CreatedBy = _view.Username;
            equipmentWhiteLight.CreationDate = DateTime.Now;
            //equipmentWhiteLight.CreationID
            equipmentWhiteLight.EquipmentID = _view.EquipmentID;
            equipmentWhiteLight.ModificationDate = DateTime.Now;
            //equipmentWhiteLight.ModificationID
            equipmentWhiteLight.ModifiedBy = _view.Username;
            equipmentWhiteLight.WhiteLightStartDate = DateTime.Now;
            equipmentWhiteLight.Notes = _view.WhiteLightNotes;

            return equipmentWhiteLight;
        }

        private IList<CS_EquipmentWhiteLight> SetEquipmentComboWhiteLight()
        {
            IList<CS_EquipmentWhiteLight> comboWhiteLightEquipments = new List<CS_EquipmentWhiteLight>();


            CS_View_EquipmentInfo eqInfo = _equipmentModel.GetEquipmentInfoByEquipmentID(_view.EquipmentID);// _equipmentInfoRepository.Get(e => e.EquipmentID == _view.EquipmentID);
            if (eqInfo.IsPrimary == 1 && eqInfo.ComboID != null && eqInfo.ComboID.HasValue)
            {
                IList<CS_Equipment> equips = _equipmentModel.ListEquipmentsFromPrimaryEquipment(eqInfo.EquipmentID, eqInfo.ComboID.Value);
                for (int j = 0; j < equips.Count; j++)
                {
                    if (_view.EquipmentID != equips[j].ID)
                    {
                        CS_EquipmentWhiteLight equipmentWhiteLight = new CS_EquipmentWhiteLight();

                        equipmentWhiteLight.Active = true;
                        equipmentWhiteLight.CreatedBy = _view.Username;
                        equipmentWhiteLight.CreationDate = DateTime.Now;
                        //equipmentWhiteLight.CreationID
                        equipmentWhiteLight.EquipmentID = equips[j].ID;
                        equipmentWhiteLight.ModificationDate = DateTime.Now;
                        //equipmentWhiteLight.ModificationID
                        equipmentWhiteLight.ModifiedBy = _view.Username;
                        equipmentWhiteLight.WhiteLightStartDate = DateTime.Now;
                        equipmentWhiteLight.Notes = _view.WhiteLightNotes;

                        comboWhiteLightEquipments.Add(equipmentWhiteLight);
                    }
                }
            }
            return comboWhiteLightEquipments;
        }
        #endregion

        #region [ SAVE ]

        /// <summary>
        /// Method that update equipment
        /// </summary>
        public void SaveEquipment()
        {
            CS_Equipment equipment = SetEquipment();

            CS_EquipmentDownHistory equipmentDownHistory = SetEquipmentDownHistory();

            IList<CS_EquipmentDownHistory> equipmentComboDownHistory = SetComboEquipmentDownHistory();

            CS_EquipmentWhiteLight equipmentWhiteLight = SetEquipmentWhiteLight();

            IList<CS_EquipmentWhiteLight> comboWhiteLightList = SetEquipmentComboWhiteLight(); 

            CS_EquipmentCoverage equipmentCoverage = SetEquipmentCoverage();

            _equipmentModel.SaveEquipment(equipment, equipmentCoverage, equipmentWhiteLight,comboWhiteLightList, equipmentDownHistory, equipmentComboDownHistory,_view.IsHeavyEquipment, _view.IsSeasonal, _view.DisplayInResourceAllocation,_view.ReplicateChangesToCombo, _view.IsEquipmentCoverage, _view.IsWhiteLight, _view.Username);
        }
        #endregion

        #endregion
    }
}
