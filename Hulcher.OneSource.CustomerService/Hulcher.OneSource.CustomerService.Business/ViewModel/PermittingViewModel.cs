using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.DataContext.VO;
using Hulcher.OneSource.CustomerService.Core.Utils;

namespace Hulcher.OneSource.CustomerService.Business.ViewModel
{
    /// <summary>
    /// PermittingViewModel class
    /// </summary>
    public class PermittingViewModel
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of the Page View. Contains access to Page shared properties.
        /// </summary>
        IPermittingView _view;

        /// <summary>
        /// Access to Equipment related DB Objects
        /// </summary>
        EquipmentModel _equipmentModel;

        #endregion

        #region [ Constructor ]

        public PermittingViewModel(IPermittingView view)
        {
            _view = view;

            _equipmentModel = new EquipmentModel(new EFUnitOfWork());
        }

        public PermittingViewModel(IPermittingView view, EquipmentModel equipmentModel)
        {
            _view = view;

            _equipmentModel = equipmentModel;
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Method for DataBinding the First Repeater
        /// </summary>
        public void LoadEquipmentCombo()
        {
            List<CS_View_EquipmentInfo> equipmentInfoList = _equipmentModel.ListAllCombo().ToList();
            _view.EquipmentInfoListData = equipmentInfoList;
        }

        private void DoComboSort(Func<CS_View_EquipmentInfo, object> keySelector, Globals.Common.Sort.SortDirection sortDirection, out List<CS_View_EquipmentInfo> comboList)
        {
            comboList = new List<CS_View_EquipmentInfo>();

            comboList.AddRange(_view.EquipmentInfoListData.FindAll(e => e.ComboID.HasValue && e.IsPrimary == 1).Distinct(new Globals.QuickReference.CS_View_EquipmentInfo_Combo_Comparer()));


            if (sortDirection == Globals.Common.Sort.SortDirection.Ascending)
            {
                comboList = comboList.OrderBy(keySelector).ToList();
            }
            else
            {
                comboList = comboList.OrderByDescending(keySelector).ToList();
            }
        }

        public void GetFirstTierEquipmentList()
        {
            List<CS_View_EquipmentInfo> comboList;

            switch (_view.SortColumn)
            {
                case Globals.Permitting.PermittingSortColumns.DivisionName:
                    DoComboSort(e => e.DivisionName, _view.SortDirection, out comboList);
                    break;
                case Globals.Permitting.PermittingSortColumns.DivisionState:
                    DoComboSort(e => e.DivisionState, _view.SortDirection, out comboList);
                    break;
                case Globals.Permitting.PermittingSortColumns.ComboUnit:
                    DoComboSort(e => e.ComboName, _view.SortDirection, out comboList);
                    break;
                case Globals.Permitting.PermittingSortColumns.JobNumber:
                    DoComboSort(e => e.JobNumber, _view.SortDirection, out comboList);
                    break;
                case Globals.Permitting.PermittingSortColumns.CreateDate:
                    DoComboSort(e => e.CreateDate, _view.SortDirection, out comboList);
                    break;
                case Globals.Permitting.PermittingSortColumns.Descriptor_Type:
                case Globals.Permitting.PermittingSortColumns.None:
                default:
                    comboList = new List<CS_View_EquipmentInfo>();
                    comboList.AddRange(_view.EquipmentInfoListData.FindAll(e => e.ComboID.HasValue && e.IsPrimary == 1).Distinct(new Globals.QuickReference.CS_View_EquipmentInfo_Combo_Comparer()));
                    comboList = comboList.OrderBy(e => e.DivisionName).ThenBy(e => e.ComboName).ToList();
                    break;
            }
            _view.FirstTierDataSource = comboList;
        }

        private void DoEquipmentSort(Func<CS_View_EquipmentInfo, object> keySelector, Globals.Common.Sort.SortDirection sortDirection, out List<CS_View_EquipmentInfo> equipmentList)
        {
            equipmentList = new List<CS_View_EquipmentInfo>();

            equipmentList.AddRange(_view.EquipmentInfoListData.FindAll(e => e.ComboID.HasValue && e.ComboID == _view.FirstTierDataItem.ComboID && e.Active == true).ToList());

            if (sortDirection == Globals.Common.Sort.SortDirection.Ascending)
            {
                equipmentList = equipmentList.OrderBy(keySelector).ToList();
            }
            else
            {
                equipmentList = equipmentList.OrderByDescending(keySelector).ToList();
            }
        }

        public void GetSecondTierEquipmentList()
        {
            if (null != _view.FirstTierDataItem)
            {
                List<CS_View_EquipmentInfo> equipmentList;

                switch (_view.SortColumn)
                {
                    case Globals.Permitting.PermittingSortColumns.DivisionName:
                        DoEquipmentSort(e => e.DivisionName, _view.SortDirection, out equipmentList);
                        break;
                    case Globals.Permitting.PermittingSortColumns.DivisionState:
                        DoEquipmentSort(e => e.DivisionState, _view.SortDirection, out equipmentList);
                        break;
                    case Globals.Permitting.PermittingSortColumns.ComboUnit:
                        DoEquipmentSort(e => e.UnitNumber, _view.SortDirection, out equipmentList);
                        break;
                    case Globals.Permitting.PermittingSortColumns.Descriptor_Type:
                        DoEquipmentSort(e => e.Descriptor, _view.SortDirection, out equipmentList);
                        break;
                    case Globals.Permitting.PermittingSortColumns.JobNumber:
                        DoEquipmentSort(e => e.JobNumber, _view.SortDirection, out equipmentList);
                        break;
                    case Globals.Permitting.PermittingSortColumns.None:
                    default:
                        equipmentList = new List<CS_View_EquipmentInfo>();
                        equipmentList.AddRange(_view.EquipmentInfoListData.FindAll(e => e.ComboID.HasValue && e.ComboID == _view.FirstTierDataItem.ComboID && e.Active).ToList());
                        break;
                }

                _view.SecondTierDataSource = equipmentList;
            }
        }

        /// <summary>
        /// Method for filling the First Repeater Items values
        /// </summary>
        public void SetEquipmentComboRowData()
        {
            if (null != _view.FirstTierDataItem)
            {
                if (_view.FirstTierDataItem.ComboID.HasValue)
                    _view.FirstTierComboId = _view.FirstTierDataItem.ComboID.Value;

                if (_view.FirstTierDataItem.JobID.HasValue)
                    _view.FirstTierJobId = _view.FirstTierDataItem.JobID.Value;

                _view.FirstTierComboName = _view.FirstTierDataItem.ComboName;
                _view.FirstTierUnitNumber = _view.FirstTierDataItem.UnitNumber;

                if (_view.FirstTierDataItem.CreateDate.HasValue)
                    _view.FirstTierCreateDate = _view.FirstTierDataItem.CreateDate.Value;

                _view.FirstTierDivisionNumber = _view.FirstTierDataItem.DivisionName;
                _view.FirstTierTypeDescriptor = _view.FirstTierDataItem.Descriptor;
                _view.FirstTierJobNumber = _view.FirstTierDataItem.JobNumber;
                _view.FirstTierDivisionState = _view.FirstTierDataItem.DivisionState;
            }
        }

        /// <summary>
        /// Method for filling the Second Repeater (Equipments) Items values.
        /// </summary>
        public void SetDetailedEquipmentComboRowData()
        {
            if (null != _view.FirstTierDataItem && null != _view.SecondTierDataItem)
            {
                if (_view.SecondTierDataItem.ComboID.HasValue)
                    _view.SecondTierItemCssClass = _view.SecondTierDataItem.ComboID.Value.ToString();

                if (_view.SecondTierDataItem.JobID.HasValue)
                    _view.SecondTierJobId = _view.SecondTierDataItem.JobID.Value;

                if (_view.SecondTierDataItem.IsPrimary == 1)
                    _view.SecondTierPrimaryUnit = true;
                else
                    _view.SecondTierPrimaryUnit = false;

                _view.SecondTierUnitNumber = _view.SecondTierDataItem.UnitNumber;

                _view.SecondTierJobNumber = _view.SecondTierDataItem.JobNumber;
                _view.SecondTierDivisionNumber = _view.SecondTierDataItem.DivisionName;
                _view.SecondTierDivisionState = _view.SecondTierDataItem.DivisionState;
                _view.SecondTierEquipmentTypeDescriptor = _view.SecondTierDataItem.Descriptor;
            }
        }

        /// <summary>
        /// Method that lists filtered equipment
        /// </summary>
        public void ListFilteredEquipmentInfo()
        {
            _view.ListFilteredEquipmentInfo =
                _equipmentModel.ListFilteredEquipmentInfo(_view.EquipmentFilters, _view.FilterValue, "DivisionName")
                    .OrderBy(e => e.DivisionName)
                    .ThenBy(e => e.UnitNumber)
                    .ToList();
        }

        /// <summary>
        /// Remove a list of Equípments
        /// </summary>
        public void RemoveEquipmentFromShoppingCart()
        {
            List<EquipmentComboVO> shopCartDatasource = _view.EquipmentInfoShoppingCartDataSource.ToList();

            List<EquipmentComboVO> removeList = shopCartDatasource.FindAll(e => _view.RemovedEquipments.Contains(e.EquipmentId));

            for (int i = 0; i < removeList.Count; i++)
            {
                shopCartDatasource.Remove(removeList[i]);
            }

            _view.EquipmentInfoShoppingCartDataSource = shopCartDatasource;
        }

        /// <summary>
        /// Set Combo Repeater DataSource
        /// </summary>
        public void GetComboLogDataSource()
        {
            string comboName = _view.EquipmentCombo.Name;
            string primaryUnit = "", primaryDivision = "";
            StringBuilder unitNames = new StringBuilder();

            for (int i = 0; i < _view.EquipmentInfoShoppingCartDataSource.Count; i++)
            {
                unitNames.Append(string.Format("{0} {1} <br />", _view.EquipmentInfoShoppingCartDataSource[i].UnitNumber, _view.EquipmentInfoShoppingCartDataSource[i].Descriptor));
                if (_view.EquipmentInfoShoppingCartDataSource[i].IsPrimary)
                {
                    primaryUnit = _view.EquipmentInfoShoppingCartDataSource[i].UnitNumber;
                    primaryDivision = _view.EquipmentInfoShoppingCartDataSource[i].DivisionNumber;
                }
            }

            IList<ComboLog> list = _view.ComboHistoryLogDataSource;
            list.Add(new ComboLog
            {
                ComboName = comboName,
                Units = unitNames.ToString(),
                PrimaryUnit = primaryUnit,
                Division = primaryDivision
            }
            );

            _view.ComboHistoryLogDataSource = list;
        }

        /// <summary>
        /// Set Combo Repeater Text Items
        /// </summary>
        public void SetComboLogListData()
        {
            _view.ComboHistoryRowName = _view.ComboHistoryRepeaterDataItem.ComboName;
            _view.ComboHistoryRowUnits = _view.ComboHistoryRepeaterDataItem.Units;
            _view.ComboHistoryRowPrimary = _view.ComboHistoryRepeaterDataItem.PrimaryUnit;
            _view.ComboHistoryRowDivision = _view.ComboHistoryRepeaterDataItem.Division;
        }

        /// <summary>
        /// Add Selected Equipments To the Shoping Cart
        /// </summary>
        public void AddEquipmentsToShopingCart()
        {
            IList<EquipmentComboVO> lst = _view.EquipmentInfoShoppingCartDataSource;

            foreach (int equipmentId in _view.SelectedEquipmentsAdd)
            {
                if (!lst.Any(e => e.EquipmentId == equipmentId))
                {
                    EquipmentComboVO equipment = _equipmentModel.GetEquipmentOfACombo(equipmentId);
                    if (null != equipment)
                    {
                        if (lst.Count > 0 && !lst.Any(e => e.DivisionNumber == equipment.DivisionNumber || equipment.Seasonal))
                            throw new ApplicationException("Only equipments from the same division can be grouped in a Combo. Please select only equipments from the same division.");
                        if (_view.EquipmentComboId.HasValue)
                            equipment.ComboId = _view.EquipmentComboId.Value;
                        lst.Add(equipment);
                    }
                }
            }

            _view.EquipmentInfoShoppingCartDataSource = lst;
        }

        /// <summary>
        /// Saves or Updates a Combo
        /// </summary>
        public void SaveEquipmentCombo()
        {
            if (_view.EquipmentComboId.HasValue)
            {
                CS_EquipmentCombo combo = _equipmentModel.GetCombo(_view.EquipmentComboId.Value);
                if (null != combo)
                {
                    combo.Name = _view.ComboName;
                    combo.ComboType = _view.ComboType;
                    combo.PrimaryEquipmentID = _view.PrimaryEquipmentId;
                    _equipmentModel.UpdateCombo(combo, _view.EquipmentInfoShoppingCartDataSource, _view.UserName);

                    _view.EquipmentInfoShoppingCartDataSource = _equipmentModel.ListEquipmentsOfACombo(combo.ID);
                }
            }
            else
            {
                CS_EquipmentCombo combo = new CS_EquipmentCombo();
                combo.Name = _view.ComboName;
                combo.ComboType = _view.ComboType;
                combo.PrimaryEquipmentID = _view.PrimaryEquipmentId;
                _equipmentModel.SaveCombo(combo, _view.EquipmentInfoShoppingCartDataSource, _view.UserName);

                _view.EquipmentComboId = combo.ID;

                _view.EquipmentInfoShoppingCartDataSource = _equipmentModel.ListEquipmentsOfACombo(combo.ID);
            }
        }

        #endregion
    }
}
