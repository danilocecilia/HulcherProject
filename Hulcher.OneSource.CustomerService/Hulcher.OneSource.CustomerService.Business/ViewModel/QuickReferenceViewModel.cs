using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core;

namespace Hulcher.OneSource.CustomerService.Business.ViewModel
{
    /// <summary>
    /// Quick Reference ViewModel class
    /// </summary>
    public class QuickReferenceViewModel
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of the Quick Reference View Interface
        /// </summary>
        private IQuickReferenceView _view;

        private CallLogModel _callLogModel;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="view">Instance of the Quick Reference View Interface</param>
        public QuickReferenceViewModel(IQuickReferenceView view)
        {
            _view = view;
            _callLogModel = new CallLogModel();
        }

        #endregion

        #region [ Methods ]


        private void DoComboSort(Func<CS_View_EquipmentInfo, string> keySelector, Globals.Common.Sort.SortDirection sortDirection, out List<CS_View_EquipmentInfo> comboList)
        {
            comboList = new List<CS_View_EquipmentInfo>();

            comboList.AddRange(_view.EquipmentInfoListData.FindAll(e => e.ComboID.HasValue && e.IsPrimary == 1).Distinct(new Globals.QuickReference.CS_View_EquipmentInfo_Combo_Comparer()));
            comboList.AddRange(_view.EquipmentInfoListData.FindAll(e => !e.ComboID.HasValue));

            if (sortDirection == Globals.Common.Sort.SortDirection.Ascending)
            {
                comboList = comboList.OrderBy(keySelector).ToList();
            }
            else
            {
                comboList = comboList.OrderByDescending(keySelector).ToList();
            }
        }

        private void DoEquipmentSort(Func<CS_View_EquipmentInfo, string> keySelector, Globals.Common.Sort.SortDirection sortDirection, out List<CS_View_EquipmentInfo> equipmentList)
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


        public void GetFirstTierEquipmentList()
        {
            List<CS_View_EquipmentInfo> comboList;

            switch (_view.SortColumn)
            {
                case Globals.Common.Sort.EquipmentSortColumns.DivisionName:
                    DoComboSort(e => e.DivisionName, _view.SortDirection, out comboList);
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.DivisionState:
                    DoComboSort(e => e.DivisionState, _view.SortDirection, out comboList);
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.ComboName:
                    DoComboSort(e => e.ComboName, _view.SortDirection, out comboList);
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.UnitNumber:
                    DoComboSort(e => e.UnitNumber, _view.SortDirection, out comboList);
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.Descriptor:
                    DoComboSort(e => e.Descriptor, _view.SortDirection, out comboList);
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.Status:
                    DoComboSort(e => e.Status, _view.SortDirection, out comboList);
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.JobLocation:
                    DoComboSort(e => e.JobLocation, _view.SortDirection, out comboList);
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.Type:
                    DoComboSort(e => e.Type, _view.SortDirection, out comboList);
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.OperationStatus:
                    DoComboSort(e => e.EquipmentStatus, _view.SortDirection, out comboList);
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.JobNumber:
                    DoComboSort(e => e.JobNumber, _view.SortDirection, out comboList);
                    break;
                case Globals.Common.Sort.EquipmentSortColumns.None:
                    comboList = new List<CS_View_EquipmentInfo>();
                    comboList.AddRange(_view.EquipmentInfoListData.FindAll(e => e.ComboID.HasValue && e.IsPrimary == 1).Distinct(new Globals.QuickReference.CS_View_EquipmentInfo_Combo_Comparer()));
                    comboList.AddRange(_view.EquipmentInfoListData.FindAll(e => !e.ComboID.HasValue));
                    break;
                default:
                    comboList = new List<CS_View_EquipmentInfo>();
                    break;

            }

            _view.FirstTierDataSource = comboList;
        }

        public void SetFirstTierEquipmentList()
        {
            _view.FirstTierItemDivisionName = _view.FirstTierDataItem.DivisionName;
            _view.FirstTierItemDivisionState = _view.FirstTierDataItem.DivisionState;
            _view.FirstTierItemComboName = _view.FirstTierDataItem.ComboName;
            _view.FirstTierItemComboID = _view.FirstTierDataItem.ComboID;
            _view.FirstTierItemUnitNumber = _view.FirstTierDataItem.UnitNumber;
            _view.FirstTierItemDescriptor = _view.FirstTierDataItem.Descriptor;
            _view.FirstTierItemStatus = _view.FirstTierDataItem.Status;
            _view.FirstTierItemJobLocation = _view.FirstTierDataItem.JobLocation;
            _view.FirstTierItemLastCallEntryText = _view.FirstTierDataItem.Type;
            _view.FirstTierItemOperationStatus = _view.FirstTierDataItem.EquipmentStatus;
            _view.FirstTierItemJobNumberText = _view.FirstTierDataItem.PrefixedNumber;

            if (_view.FirstTierDataItem.JobID.HasValue)
            {
                _view.FirstTierItemJobNumberID = _view.FirstTierDataItem.JobID.Value;
            }

            if (0 != _view.FirstTierDataItem.CallLogJobID && 0 != _view.FirstTierDataItem.CallLogID)
            {
                if (!_callLogModel.GetCallTypeByDescription(_view.FirstTierDataItem.Type).IsAutomaticProcess)
                _view.FirstTierItemLastCallEntryID = new int[] {
                    _view.FirstTierDataItem.CallLogJobID,
                    _view.FirstTierDataItem.CallLogID
                };
            }
        }

        public void GetSecondTierEquipmentList()
        {
            if (null != _view.FirstTierDataItem)
            {
                List<CS_View_EquipmentInfo> equipmentList;

                switch (_view.SortColumn)
                {
                    case Globals.Common.Sort.EquipmentSortColumns.DivisionName:
                        DoEquipmentSort(e => e.DivisionName, _view.SortDirection, out equipmentList);
                        break;
                    case Globals.Common.Sort.EquipmentSortColumns.DivisionState:
                        DoEquipmentSort(e => e.DivisionState, _view.SortDirection, out equipmentList);
                        break;
                    case Globals.Common.Sort.EquipmentSortColumns.ComboName:
                        DoEquipmentSort(e => e.ComboName, _view.SortDirection, out equipmentList);
                        break;
                    case Globals.Common.Sort.EquipmentSortColumns.UnitNumber:
                        DoEquipmentSort(e => e.UnitNumber, _view.SortDirection, out equipmentList);
                        break;
                    case Globals.Common.Sort.EquipmentSortColumns.Descriptor:
                        DoEquipmentSort(e => e.Descriptor, _view.SortDirection, out equipmentList);
                        break;
                    case Globals.Common.Sort.EquipmentSortColumns.Status:
                        DoEquipmentSort(e => e.Status, _view.SortDirection, out equipmentList);
                        break;
                    case Globals.Common.Sort.EquipmentSortColumns.JobLocation:
                        DoEquipmentSort(e => e.JobLocation, _view.SortDirection, out equipmentList);
                        break;
                    case Globals.Common.Sort.EquipmentSortColumns.Type:
                        DoEquipmentSort(e => e.Type, _view.SortDirection, out equipmentList);
                        break;
                    case Globals.Common.Sort.EquipmentSortColumns.OperationStatus:
                        DoEquipmentSort(e => e.EquipmentStatus, _view.SortDirection, out equipmentList);
                        break;
                    case Globals.Common.Sort.EquipmentSortColumns.JobNumber:
                        DoEquipmentSort(e => e.JobNumber, _view.SortDirection, out equipmentList);
                        break;
                    case Globals.Common.Sort.EquipmentSortColumns.None:
                        equipmentList = new List<CS_View_EquipmentInfo>();
                        equipmentList.AddRange(_view.EquipmentInfoListData.FindAll(e => e.ComboID.HasValue && e.ComboID == _view.FirstTierDataItem.ComboID && e.Active == true).ToList());
                        break;
                    default:
                        equipmentList = new List<CS_View_EquipmentInfo>();
                        break;
                }

                _view.SecondTierDataSource = equipmentList;
            }
        }

        public void SetSecondTierEquipmentList()
        {
            if (null != _view.SecondTierDataItem)
            {
                _view.SecondTierItemDivisionName = _view.SecondTierDataItem.DivisionName;
                _view.SecondTierItemDivisionState = _view.SecondTierDataItem.DivisionState;
                _view.SecondTierItemUnitNumber = _view.SecondTierDataItem.UnitNumber;
                _view.SecondTierItemDescriptor = _view.SecondTierDataItem.Descriptor;
                _view.SecondTierItemStatus = _view.SecondTierDataItem.Status;
                _view.SecondTierItemJobLocation = _view.SecondTierDataItem.JobLocation;
                _view.SecondTierItemLastCallEntryText = _view.SecondTierDataItem.Type;
                _view.SecondTierItemOperationStatus = _view.SecondTierDataItem.EquipmentStatus;
                _view.SecondTierItemJobNumberText = _view.SecondTierDataItem.PrefixedNumber;

                if (_view.SecondTierDataItem.ComboID.HasValue)
                    _view.SecondTierItemCssClass = _view.SecondTierDataItem.ComboID.Value.ToString();

                if (_view.SecondTierDataItem.JobID.HasValue)
                {
                    _view.SecondTierItemJobNumberID = _view.SecondTierDataItem.JobID.Value;
                };

                if (0 != _view.SecondTierDataItem.CallLogJobID && 0 != _view.SecondTierDataItem.CallLogID)
                {
                    if (!_callLogModel.GetCallTypeByDescription(_view.FirstTierDataItem.Type).IsAutomaticProcess)
                    _view.SecondTierItemLastCallEntryID = new int[] {
                    _view.SecondTierDataItem.CallLogJobID,
                    _view.SecondTierDataItem.CallLogID
                    };
                }


            }
        }

        #endregion
    }
}
