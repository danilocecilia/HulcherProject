using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.Business.ViewModel;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Core.Security;
using Hulcher.OneSource.CustomerService.Core.Utils;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;

using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    public class DashboardSearchPresenter
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of the Dashboard View Interface
        /// </summary>
        private IDashboardSearchView _view;

        /// <summary>
        /// Instance of the Dashboard View Model
        /// </summary>
        private DashboardSearchViewModel _viewModel;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="view">Instance of the View</param>
        public DashboardSearchPresenter(IDashboardSearchView view)
        {
            this._view = view;
            _viewModel = new DashboardSearchViewModel(view);
        }

        #endregion

        #region [ Methods ]

        #region [ Common ]

        /// <summary>
        /// Fills the initial values of the screen
        /// </summary>
        public void LoadPage()
        {
            try
            {
                _viewModel.LoadFilterPanel();
                LoadData();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Load Page.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Load Page.", false);
            }
        }

        /// <summary>
        /// Load Data Grid With Filtered Data
        /// </summary>
        public void LoadData()
        {
            _viewModel.LoadFilterData();
        }

        /// <summary>
        /// Executes the Export to CSV process
        /// </summary>
        public void ExportToCSV()
        {
            try
            {
                _viewModel.CreateCSVJobSummary();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Export Information to a CSV file!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Export Information to a CSV file.", false);
            }
        }

        #endregion

        #region [ Job Summary ]

        /// <summary>
        /// Fills a Job Summary Row
        /// </summary>
        public void FillJobSummaryRow()
        {
            try
            {

                _viewModel.GetJobSummaryResourceList();
                _viewModel.SetJobSummaryRowData();

            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Set Job Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Set Job Information.", false);
            }
        }

        /// <summary>
        /// Fills a Job Summary Resource Row
        /// </summary>
        public void FillJobSummaryResourceRow()
        {
            try
            {

                _viewModel.SetJobSummaryResourceRowData();

            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to set the Resource Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to set the Resource Information.", false);
            }
        }

        #endregion

        #endregion


    }
}
