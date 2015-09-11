using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.ViewModel;
using Hulcher.OneSource.CustomerService.Business.Model;

using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    public class EquipmentMaintenancePresenter
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of View Interface
        /// </summary>
        private IEquipmentMaintenanceView _view;

        /// <summary>
        /// Instance of View Model class
        /// </summary>
        private EquipmentMaintenanceViewModel _viewModel;

        private EquipmentModel _equipmentModel;

        private JobModel _jobModel;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="view">Instance of View Interface</param>
        public EquipmentMaintenancePresenter(IEquipmentMaintenanceView view)
        {
            _view = view;
            _viewModel = new EquipmentMaintenanceViewModel(_view);
            _equipmentModel = new EquipmentModel();
            _jobModel = new JobModel();
        }

        #endregion

        #region [ Methods ]

        #region [ Listing ]
        /// <summary>
        /// Loads an existing equipment in the form
        /// </summary>
        public void LoadEquipment()
        {
            try
            {
                _view.EditMode = true;

                _view.ReplicateChangesToComboVisibility = _equipmentModel.EquipmentIsPrimary(_view.EquipmentID);

                _viewModel.LoadEquipment();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to load Equipment Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to load Equipment Information.", false);
            }
        }

        /// <summary>
        /// Verify actual condictions to show or hide some controls on the page for the equipment status
        /// </summary>
        public void ShowHideEquipmentStatusDuration()
        {
            _viewModel.ShowHideEquipmentStatusDurantionFields();
        }

        /// <summary>
        /// Executes the find method to filter equipments
        /// </summary>
        public void ListFilteredEquipment()
        {
            try
            {
                _view.EditMode = false;
                if (_view.FilterType.HasValue && !string.IsNullOrEmpty(_view.FilterValue))
                    _viewModel.ListFilteredEquipment();
                else
                    _viewModel.ListHeavyWeightEquipment();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to list the Equipments!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to list the Equipments.", false);
            }
        }

        /// <summary>
        /// Executes the loading process of the page
        /// </summary>
        public void LoadPage()
        {
            try
            {
                _view.EditMode = false;                
                _viewModel.ListHeavyWeightEquipment();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to load the Page!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to load the Page.", false);
            }
        }
        #endregion

        /// <summary>
        /// Binds an equipment row
        /// </summary>
        public void BindEquipmentRow()
        {
            try
            {
                _viewModel.BindEquipmentRow();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to load Equipment Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to load Equipment Information.", false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ClearEquipmentFields()
        {
            try
            {
                _view.ClearEquipmentFields();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to clear equipment fields!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to clear equipment fields.", false);
            }
        }

        /// <summary>
        /// Save/Update equipment/down/coverage/WhiteLight
        /// </summary>
        public void SaveEquipment()
        {
            try
            {
                _viewModel.SaveEquipment();

                _view.EditMode = false;

                _view.ReplicateChangesToCombo = false;

                if (_view.FilterType.HasValue && !string.IsNullOrEmpty(_view.FilterValue))
                    _viewModel.ListFilteredEquipment();
                else
                    _viewModel.ListHeavyWeightEquipment();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to save Equipment Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to save Equipment Information.", false);
            }
        }

        public void VerifyIfResourceIsAssignedToJob()
        {
            try
            {                
                _view.IsEquipmentAssignedToJob = _equipmentModel.VerifyIfResourceIsAssignedToJob(_view.EquipmentID);
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to verify the resources assigned to job.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to verify the resources assigned to job.", false);
            }
        }

        public void SaveCoverageDivision()
        {
            try
            {
                _jobModel.AddDivisionToJob(_view.DivisionID, _view.JobID, _view.Username);
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to save Coverage Division!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to save Coverage Division.", false);
            }            
        }

        public void SetJobID()
        {
            try
            {
                int? result = _equipmentModel.GetJobIdAssignedToEquipment(_view.EquipmentID);
                if (result.HasValue)
                    _view.JobID = result.Value;
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to set job ID!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to set job ID.", false);
            }
        }

        #region [ Equipment Display ]

        #region [ Equipment Type ]
        public void LoadFirstTierItem()
        {
            try
            {
                _viewModel.SetFirstTierEquipmentTypeList();
                BindSecondTierDivisionList();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to set the Equipment Type fields on the grid!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Error has ocurred while trying to set the Equipment Type fields on the grid. Please try again.", false);
            }
        }

        public void BindEquipmentTypeDisplay()
        {
            try
            {
                _viewModel.BindFirstTierEquipmentTypeDisplay();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Equipment Type Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Error has ocurred while trying to load the Equipment Type Information. Please try again.", false);
            }

        }
        #endregion

        #region [ Division ]
        /// <summary>
        /// Method to bind the second grid ( Division Repeater ) on the screen.
        /// </summary>
        public void BindSecondTierDivisionList()
        {
            try
            {
                _viewModel.GetSecondTierDivisionList();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Division Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Error has ocurred while trying to load the Division Information. Please try again.", false);
            }
        }

        /// <summary>
        /// Method that creates the division row of the division repeater inside equipment display repeater
        /// </summary>
        public void CreateDivisionRow()
        {
            try
            {
                _viewModel.SetSecondTierDivisionList();
                BindThirdTierEquipmentList();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Division Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Error has ocurred while trying to load the Division Information. Please try again.", false);
            }

        }
        #endregion

        #region [ Equipment ]
        /// <summary>
        /// Method that creates the equipment rows
        /// </summary>
        public void CreateEquipmentRow()
        {
            try
            {
                _viewModel.SetThirdTierEquipmentRowList();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to Fill the Equipment Row!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Error has ocurred while trying to fill the Equipment Row. Please try again.", false);
            }
        }

        public void BindThirdTierEquipmentList()
        {
            try
            {
                _viewModel.GetThirdTierEquipmentList();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Equipment Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Error has ocurred while trying to load the Equipment Information. Please try again.", false);
            }
        }
        #endregion

        /// <summary>
        /// Method to retrieve the list of equipment type, division and equipment to be used to bind all repeaters.
        /// </summary>
        public void BindManagementEquipmentDashboard()
        {
            try
            {
                _viewModel.BindManagementEquipmentDashboard();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the Equipment Display Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Error has ocurred while trying to load the Equipment Display Information. Please try again.", false);
            }
        }

        public void UpdateEquipmentDisplay()
        {
            try
            {
                _viewModel.UpdateEquipmentDisplay();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to Update the Equipment information on Equipment Display Grid.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Error has ocurred while trying to Update the Equipment information on Equipment Display Grid. Please try again.", false);
            }
            
        }

        #endregion
     
        #endregion
    }
}
