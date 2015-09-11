using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Hulcher.OneSource.CustomerService.Business.ViewModel;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.DataContext.VO;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    /// <summary>
    /// Permitting Presenter
    /// </summary>
    public class PermittingPresenter
    {
        #region [ Atributes ]

        private IPermittingView _view;
        private PermittingViewModel _viewModel = null;
        private EquipmentModel _equipmentModel;
        #endregion

        #region [ Constructors ]

        public PermittingPresenter(IPermittingView view)
        {
            _view = view;
            _viewModel = new PermittingViewModel(view);
            _equipmentModel = new EquipmentModel();
        }

        /// <summary>
        /// This constructor is used only when testing with Mocks
        /// </summary>
        /// <param name="view"></param>
        /// <param name="model"></param>
        public PermittingPresenter(IPermittingView view, EquipmentModel model)
        {
            _view = view;
            _equipmentModel = model;
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// List all permitting information
        /// </summary>
        public void LoadEquipmentCombo()
        {
            try
            {
                _viewModel.LoadEquipmentCombo();
                _viewModel.GetFirstTierEquipmentList();

                if (_view.QueryStringEquipmentComboId.HasValue)
                {
                    _view.EquipmentComboId = _view.QueryStringEquipmentComboId;
                    LoadCombo();
                }

            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error loading the Equipment Combo.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error loading the Equipment Combo. Please try again.", false);
            }
        }

        /// <summary>
        /// Hides the Panels used for Creation of Combo
        /// Used when the application is first started
        /// </summary>
        public void HideCreationPanels()
        {
            _view.CreationPanelVisible = false;
            _view.LogPanelVisible = false;
        }

        /// <summary>
        /// Shows an Permitting list details
        /// </summary>
        public void LoadDetailedEquipmentCombo()
        {
            try
            {
                _viewModel.GetSecondTierEquipmentList();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error loading the detailed equipment combo.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error loading the detailed equipment combo. Please try again.", false);
            }
        }

        /// <summary>
        /// Shows a list of filtered equipment info
        /// </summary>
        public void ListFilteredEquipmentInfo()
        {
            try
            {
                _viewModel.ListFilteredEquipmentInfo();
                //_viewModel.GetFirstTierEquipmentList();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error loading the filtered equipment combo.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error loading the filtered equipment combo. Please try again.", false);
            }
        }

        public void LoadShoppingCartRow()
        {
            try
            {
                if (_view.EquipmentInfoItem.IsPrimary)
                    _view.IsPrimaryObjectSelected = true;
                _view.DivisionSelected = _view.EquipmentInfoItem.DivisionNumber;
                _view.UnitNumberSelected = _view.EquipmentInfoItem.UnitNumber;
                _view.DescriptorSelected = _view.EquipmentInfoItem.Descriptor;
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error loading the shopping cart row.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error loading the equipments inside the combo. Please try again.", false);
            }
        }
        /// <summary>
        /// Saves or updates the equipment combo in database
        /// </summary>
        public void SaveEquipmentCombo()
        {
            try
            {
                _viewModel.SaveEquipmentCombo();
                _view.SavedSuccessfuly = true;
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to save/update the equipment combo.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to save/update the equipment combo. Please try again.", false);

                _view.SavedSuccessfuly = false;
            }
        }

        /// <summary>
        /// Delete the equipment combo ( inactivates )
        /// </summary>
        public void DeleteEquipmentCombo()
        {
            try
            {
                bool deleted = _equipmentModel.DeleteEquipmentCombo(_view.EquipmentComboId.Value, _view.UserName);

                if (!deleted)
                {
                    _view.DisplayMessage("The combo can not be deleted because their equipments are assigned to a job", false);
                }
                else
                {
                    LoadEquipmentCombo();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while delete the equipment combo.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while delete the equipment combo. Please try again.", false);
            }
        }

        /// <summary>
        /// Loads information of an existing combo for editing
        /// </summary>
        public void LoadCombo()
        {
            try
            {
                if (_view.EquipmentComboId.HasValue)
                {
                    CS_EquipmentCombo combo = _equipmentModel.GetCombo(_view.EquipmentComboId.Value);
                    List<EquipmentComboVO> equipmentList = _equipmentModel.ListEquipmentsOfACombo(_view.EquipmentComboId.Value);
                    if (null != combo)
                    {
                        _view.ComboName = combo.Name;
                        _view.ComboType = combo.ComboType;
                        _view.EquipmentInfoShoppingCartDataSource = equipmentList;

                        _view.CreationPanelVisible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while loading combo data \n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while loading combo data.", false);
            }
        }

        /// <summary>
        /// Remove the Equipment from the ShoppingCart Datasource
        /// </summary>
        public void RemoveEquipmentFromShoppingCart()
        {
            try
            {
                _viewModel.RemoveEquipmentFromShoppingCart();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while Removing the entry \n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while removing the entry.", false);
            }
        }

        public void SetEquipmentComboRowData()
        {
            _viewModel.SetEquipmentComboRowData();

        }

        public void SetDetailedEquipmentComboRowData()
        {
            _viewModel.SetDetailedEquipmentComboRowData();

        }

        public void BindPermittingHistoryLog()
        {
            _viewModel.GetComboLogDataSource();
            _view.LogPanelVisible = true;
        }

        public void FillComboLogList()
        {
            _viewModel.SetComboLogListData();
        }

        public void AddEquipmentsToShopingCart()
        {
            try
            {
                _viewModel.AddEquipmentsToShopingCart();
            }
            catch (ApplicationException ex)
            {
                _view.DisplayMessage(ex.Message, false);
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while Adding the Equipments \n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while Adding the Equipments.", false);
            }
        }
        #endregion
    }
}
