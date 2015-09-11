using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.Business.ViewModel;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;

using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    /// <summary>
    /// Quick Reference Presenter class
    /// </summary>
    public class QuickReferencePresenter
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of the Quick Reference View Interface
        /// </summary>
        private IQuickReferenceView _view;

        /// <summary>
        /// Instance of the Quick Reference ViewModel class
        /// </summary>
        private QuickReferenceViewModel _viewModel = null;

        /// <summary>
        /// Instance of Equipment Model class
        /// </summary>
        private EquipmentModel _equipmentModel = null;

        #endregion

        #region [ Constructor ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="view">Instance of the Quick Reference View Interface</param>
        public QuickReferencePresenter(IQuickReferenceView view)
        {
            _view = view;
            _viewModel = new QuickReferenceViewModel(view);
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Shows an Equipment List based on search parameteres
        /// </summary>
        public void ListFilteredEquipmentAdd()
        {
            try
            {
                using (_equipmentModel = new EquipmentModel())
                {
                    List<CS_View_EquipmentInfo> lstEquipmentInfo = _equipmentModel.ListFilteredHeavyEquipmentInfo(_view.EquipmentFilter, _view.EquipmentFilterValue, "DivisionName") as List<CS_View_EquipmentInfo>;

                    switch (_view.SortColumn)
                    {
                        case Globals.Common.Sort.EquipmentSortColumns.None:
                                _view.EquipmentInfoListData = lstEquipmentInfo.OrderBy(e => e.DivisionName).ThenBy(e => e.ComboName).ThenBy(e => e.UnitNumber).ToList();
                            break;
                        case Globals.Common.Sort.EquipmentSortColumns.DivisionName:
                            if (_view.SortDirection == Globals.Common.Sort.SortDirection.Ascending)
                                _view.EquipmentInfoListData = lstEquipmentInfo.OrderBy(e => e.DivisionName).ToList();
                            else
                                _view.EquipmentInfoListData = lstEquipmentInfo.OrderByDescending(e => e.DivisionName).ToList();
                            break;
                        case Globals.Common.Sort.EquipmentSortColumns.DivisionState:
                            if (_view.SortDirection == Globals.Common.Sort.SortDirection.Ascending)
                                _view.EquipmentInfoListData = lstEquipmentInfo.OrderBy(e => e.DivisionState).ToList();
                            else
                                _view.EquipmentInfoListData = lstEquipmentInfo.OrderByDescending(e => e.DivisionState).ToList();
                            break;
                        case Globals.Common.Sort.EquipmentSortColumns.ComboName:
                            if (_view.SortDirection == Globals.Common.Sort.SortDirection.Ascending)
                                _view.EquipmentInfoListData = lstEquipmentInfo.OrderBy(e => e.ComboName).ToList();
                            else
                                _view.EquipmentInfoListData = lstEquipmentInfo.OrderByDescending(e => e.ComboName).ToList();
                            break;
                        case Globals.Common.Sort.EquipmentSortColumns.UnitNumber:
                            if (_view.SortDirection == Globals.Common.Sort.SortDirection.Ascending)
                                _view.EquipmentInfoListData = lstEquipmentInfo.OrderBy(e => e.UnitNumber).ToList();
                            else
                                _view.EquipmentInfoListData = lstEquipmentInfo.OrderByDescending(e => e.UnitNumber).ToList();
                            break;
                        case Globals.Common.Sort.EquipmentSortColumns.Descriptor:
                            if (_view.SortDirection == Globals.Common.Sort.SortDirection.Ascending)
                                _view.EquipmentInfoListData = lstEquipmentInfo.OrderBy(e => e.Descriptor).ToList();
                            else
                                _view.EquipmentInfoListData = lstEquipmentInfo.OrderByDescending(e => e.Descriptor).ToList();
                            break;
                        case Globals.Common.Sort.EquipmentSortColumns.Status:
                            if (_view.SortDirection == Globals.Common.Sort.SortDirection.Ascending)
                                _view.EquipmentInfoListData = lstEquipmentInfo.OrderBy(e => e.Status).ToList();
                            else
                                _view.EquipmentInfoListData = lstEquipmentInfo.OrderByDescending(e => e.Status).ToList();
                            break;
                        case Globals.Common.Sort.EquipmentSortColumns.JobLocation:
                            if (_view.SortDirection == Globals.Common.Sort.SortDirection.Ascending)
                                _view.EquipmentInfoListData = lstEquipmentInfo.OrderBy(e => e.JobLocation).ToList();
                            else
                                _view.EquipmentInfoListData = lstEquipmentInfo.OrderByDescending(e => e.JobLocation).ToList();
                            break;
                        case Globals.Common.Sort.EquipmentSortColumns.Type:
                            if (_view.SortDirection == Globals.Common.Sort.SortDirection.Ascending)
                                _view.EquipmentInfoListData = lstEquipmentInfo.OrderBy(e => e.Type).ToList();
                            else
                                _view.EquipmentInfoListData = lstEquipmentInfo.OrderByDescending(e => e.Type).ToList();
                            break;
                        case Globals.Common.Sort.EquipmentSortColumns.OperationStatus:
                            if (_view.SortDirection == Globals.Common.Sort.SortDirection.Ascending)
                                _view.EquipmentInfoListData = lstEquipmentInfo.OrderBy(e => e.EquipmentStatus).ToList();
                            else
                                _view.EquipmentInfoListData = lstEquipmentInfo.OrderByDescending(e => e.EquipmentStatus).ToList();
                            break;
                        case Globals.Common.Sort.EquipmentSortColumns.JobNumber:
                            if (_view.SortDirection == Globals.Common.Sort.SortDirection.Ascending)
                                _view.EquipmentInfoListData = lstEquipmentInfo.OrderBy(e => e.JobNumber).ToList();
                            else
                                _view.EquipmentInfoListData = lstEquipmentInfo.OrderByDescending(e => e.JobNumber).ToList();
                            break;
                        default:
                                _view.EquipmentInfoListData = lstEquipmentInfo.OrderBy(e => e.DivisionName).ThenBy(e => e.ComboName).ThenBy(e => e.UnitNumber).ToList();
                            break;
                    }

                    _viewModel.GetFirstTierEquipmentList();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Employee Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to filter the Information. Please try again.", false);
            }
        }

        /// <summary>
        /// Shows an Equipment list of all active Equipments
        /// </summary>
        public void ListAllEquipmentAdd()
        {
            try
            {
                using (_equipmentModel = new EquipmentModel())
                {
                    _view.EquipmentInfoListData = _equipmentModel.ListAllHeavyCombo() as List<CS_View_EquipmentInfo>;
                    _viewModel.GetFirstTierEquipmentList();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Equipment Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Error has ocurred while trying to load the Equipment Information. Please try again.", false);
            }
        }

        public void LoadFirstTierItem()
        {
            try
            {
                _viewModel.SetFirstTierEquipmentList();
                _viewModel.GetSecondTierEquipmentList();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Equipment Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Error has ocurred while trying to load the Equipment Information. Please try again.", false);
            }
        }

        public void loadSecondTierItem()
        {
            try
            {
                _viewModel.SetSecondTierEquipmentList();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Equipment Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Error has ocurred while trying to load the Equipment Information. Please try again.", false);
            }
        }


        #endregion
    }
}
