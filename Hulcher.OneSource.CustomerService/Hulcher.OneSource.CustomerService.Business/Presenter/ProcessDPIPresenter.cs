using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.Business.ViewModel;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Core;

using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    public class ProcessDPIPresenter
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of the Process DPI View Interface
        /// </summary>
        private IProcessDPIView _view;
        
        /// <summary>
        /// Instance of Process DPI View Model
        /// </summary>
        private ProcessDPIViewModel _viewModel;
        #endregion

        #region [ Constructors ]
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="view">Instance of the Process DPI View Interface</param>
        public ProcessDPIPresenter(IProcessDPIView view)
        {
            _view = view;
            _viewModel = new ProcessDPIViewModel(view);
        }
        #endregion

        #region [ Methods ]

        /// <summary>
        /// Loads the page
        /// </summary>
        public void LoadPage()
        {
            try
            {
                _view.SetOverallDiscountOnblur();
                _view.SetLumpSumDiscountOnblur();
                SetJobHeader();
                BindResources();
                BindSpecialPricing();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to load the page.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error has ocurred while trying to load the page.", false);
            }
        }

        private void BindSpecialPricing()
        {
            try
            {
                _viewModel.LoadSpecialPricing();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to Bind the Special Pricing fields.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error has ocurred while trying to Bind the Special Pricing fields.", false);
            }
        }

        /// <summary>
        /// Sets the Job Header Fields
        /// </summary>
        public void SetJobHeader()
        {
            try
            {
                _viewModel.SetJobHeaderFields();
                _viewModel.FillTotal();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to load the Job Header Information.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error has ocurred while trying to load the Job Header Information.", false);
            }
        }

        /// <summary>
        /// Binds the resource grid
        /// </summary>
        public void BindResources()
        {
            try
            {
                _viewModel.LoadRateTable();
                _viewModel.ListAllResources();
                _viewModel.ListAllDivisions();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to Bind the Resources grid.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error has ocurred while trying to Bind the Resources grid.", false);
            }
        }

        /// <summary>
        /// Sets the information for the Division Rows
        /// </summary>
        public void SetDivisionRowInfo()
        {
            try
            {
                _viewModel.SetDivisionRowInfo();
                _viewModel.ListAllResourcesByDivision();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to Bind the Resources grid (Division Row).\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error has ocurred while trying to Bind the Resources grid (Division Row).", false);
            }
        }

        /// <summary>
        /// Sets the information for the Resource Rows
        /// </summary>
        public void SetResourceRowInfo()
        {
            try
            {
                _viewModel.SetResourceRowInfo();
                _view.SetDefaultHoursValues();
                _view.SetDefaultRateValues();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to Bind the Resources grid (Resource Row).\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error has ocurred while trying to Bind the Resources grid (Resource Row).", false);
            }
        }

        /// <summary>
        /// Saves the DPI as a DRAFT
        /// </summary>
        public void SaveDraft()
        {
            try
            {
                _view.DPIStatus = Globals.DPI.DpiStatus.DraftSaved;
                _viewModel.SaveDPI();
                _view.DisplayMessage("DPI Saved as Draft Successfully", false);
                _view.UpdateDashboard();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to save the DPI as Draft.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error has ocurred while trying to save the DPI as Draft.", false);
            }
        }

        /// <summary>
        /// Saves the DPI as APPROVED
        /// </summary>
        public void Approve()
        {
            try
            {
                _view.DPIStatus = Globals.DPI.DpiStatus.Approved;
                _viewModel.SaveDPI();
                _view.DisplayMessage("DPI Approved Successfully", false);
                _view.UpdateDashboard();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to save the DPI as Approved.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error has ocurred while trying to save the DPI as Approved.", false);
            }
        }

        #endregion
    }
}
