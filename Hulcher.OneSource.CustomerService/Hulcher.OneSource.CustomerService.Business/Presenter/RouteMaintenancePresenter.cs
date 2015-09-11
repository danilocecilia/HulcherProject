using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.ViewModel;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Hulcher.OneSource.CustomerService.Core.Security;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    public class RouteMaintenancePresenter
    {
        #region [ Attributes ]
        /// <summary>
        /// Instance of the Route Maintenance View Interface
        /// </summary>
        private IRouteMaintenanceView _view;

        /// <summary>
        /// Instance of the Route Maintenance View Model
        /// </summary>
        private RouteMaintenanceViewModel _viewModel;

        #endregion

        #region [ Constructors ]
        public RouteMaintenancePresenter(IRouteMaintenanceView view)
        {
            _view = view;
            _viewModel = new RouteMaintenanceViewModel(_view);
        }
        #endregion

        #region [ Methods ]

        public void LoadPage()
        {
            try
            {
                //_viewModel.BindDashboard();  //_view.RouteDashboardDataSource = new List<CS_Route>();
                _viewModel.ListAllDivisions();
                _view.VisualizationPanelVisible = true;
                _view.CreationPanelVisible = false;
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the list of all divisions.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Error has ocurred while trying to load the list of all divisions. Please try again.", false);
            }

        }

        public void VerifyAccess()
        {
            try
            {
                AZManager azManager = new AZManager();
                AZOperation[] azOP = azManager.CheckAccessById(_view.UserName, _view.Domain, new Globals.Security.Operations[] { Globals.Security.Operations.Route });

                if (!azOP[0].Result)
                    _view.ReadOnly = true;
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to verify access to page.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Error has ocurred while trying to verify access to page. Please try again.", false);
            }
        }

        public void BindDashboard()
        {
            try
            {
                _viewModel.BindDashboard();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to load the dashboard view.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Error has ocurred while trying to load the dashboard view. Please try again.", false);
            }
        }

        public void RouteBindRow()
        {
            try
            {
                _viewModel.RouteBindRow();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to bind routegrid row.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Error has ocurred while trying to bind routegrid row. Please try again.", false);
            }
        }

        public void RouteListRowCommand()
        {
            try
            {
                _viewModel.RouteListRowCommand();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to execute the command!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to execute the command.", false);
            }
        }

        public void SaveUpdateRoute()
        {
            try
            {
                _viewModel.SaveUpdateRoute();
                _view.DisplayMessage("Route saved successfully.", _view.SaveAndClose);
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to save route information.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Error has ocurred while trying to save route information. Please try again.", false);
            }
        }

        public void ClearForm()
        {
            try
            {
                _viewModel.ClearForm();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to clear the form.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Error has ocurred while trying to clear the form. Please try again.", false);
            }
        }

        #endregion


        public void CopyStateCityToCreate()
        {
            _view.CopyStateCityToCreate();
        }
    }
}
