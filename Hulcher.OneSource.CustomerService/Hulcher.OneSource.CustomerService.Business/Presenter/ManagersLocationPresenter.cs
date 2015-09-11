using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.Business.ViewModel;


namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    public class ManagersLocationPresenter
    {
        #region [ Attributes ]
        /// <summary>
        /// Instance of the Managers Location
        /// </summary>
        private IManagersLocationView _view;

        /// <summary>
        /// Instance of the Managers Location ViewModel
        /// </summary>
        private ManagersLocationViewModel _viewModel;

        /// <summary>
        /// Instance Of the Employee Model
        /// </summary>
        private EmployeeModel _model;
        #endregion

        #region [ Constructor ]
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="view">Instance of the Managerns Location View</param>
        public ManagersLocationPresenter(IManagersLocationView view)
        {
            _view = view;
            _viewModel = new ManagersLocationViewModel(_view);
            _model = new EmployeeModel();
        }
        #endregion

        #region [ Methods ]
        public void PageLoad()
        {
            try
            {
                _viewModel.ListAllManagersLocation();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Load the Managers Locations Grid!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Load the Managers Locations Grid.", false);
            }
        }

        public void BindManagersLocation()
        {
            try
            {
                _viewModel.BindManagersLocationRow();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Bind the Managers Location Rows", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Bind the Managers Location Rows", false);
            }
        }

        public void FilterManagersLocation()
        {
            try
            {
                _viewModel.FilterManagersLocation();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Filter Managers Location", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Filter Managers Location", false);
            }
        }
        #endregion
    }
}
