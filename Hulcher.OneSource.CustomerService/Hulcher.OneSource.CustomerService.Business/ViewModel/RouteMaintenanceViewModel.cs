using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.DataContext;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Hulcher.OneSource.CustomerService.Core;

namespace Hulcher.OneSource.CustomerService.Business.ViewModel
{
    public class RouteMaintenanceViewModel
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of the Route Maintenance View Interface
        /// </summary>
        private IRouteMaintenanceView _view;

        /// <summary>
        /// Instance of the Division Model
        /// </summary>
        private DivisionModel _divisionModel;

        /// <summary>
        /// Instance of Location Model
        /// </summary>
        private LocationModel _locationModel;

        #endregion

        #region [ Constructors ]
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="view"></param>
        public RouteMaintenanceViewModel(IRouteMaintenanceView view)
        {
            _view = view;
            _divisionModel = new DivisionModel();
            _locationModel = new LocationModel();
        }

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="view">Instance of the route maintenance view interface</param>
        /// <param name="unitOfWork">Unit of Work class (For unit Testing)</param>
        public RouteMaintenanceViewModel(IRouteMaintenanceView view, IUnitOfWork unitOfWork)
        {
            _view = view;
            _divisionModel = new DivisionModel(unitOfWork);
        }



        #endregion

        #region [ Methods ]

        public void ListAllDivisions()
        {
            _view.ListAllDivisions = _divisionModel.ListAllDivision();
        }

        public void BindDashboard()
        {
            if(_view.StateID.HasValue || _view.CityID.HasValue || _view.ZipCodeID.HasValue)
                _view.RouteDashboardDataSource = _locationModel.ListAllRoutes(_view.StateID, _view.CityID, _view.ZipCodeID);

            _view.VisualizationPanelVisible = true;
            _view.CreationPanelVisible = false;
        }

        public void RouteBindRow()
        {
            _view.RouteRowID = _view.RouteRowDataItem.ID;
            _view.RouteRowLocation = string.Format("{0}, {1}, {2}", _view.RouteRowDataItem.CS_City.CS_State.Acronym, _view.RouteRowDataItem.CS_City.CS_State.Name, _view.RouteRowDataItem.CS_City.Name);
            _view.RouteRowDivision = _view.RouteRowDataItem.CS_Division.Name;

            if (_view.RouteRowDataItem.Miles.HasValue)
                _view.RouteRowMiles = _view.RouteRowDataItem.Miles.Value;

            if (_view.RouteRowDataItem.Hours.HasValue)
                _view.RouteRowHours = Convert.ToDouble(_view.RouteRowDataItem.Hours.Value);

            if (_view.RouteRowDataItem.Fuel.HasValue)
                _view.RouteRowFuel = _view.RouteRowDataItem.Fuel.Value;

           // _view.RouteRowRoute = _view.RouteRowDataItem.Route;
            _view.RouteRowCityPermitOffice = _view.RouteRowDataItem.CityPermitOffice;
            _view.RouteRowCountyPermitOffice = _view.RouteRowDataItem.CountyPermitOffice;
            _view.RouteRowReadOnly = _view.ReadOnly;
        }

        public void RouteListRowCommand()
        {
            if (_view.RouteListRowCommandName == Globals.Common.GridView.GridCommandNames.Edit)
            {
                _view.RouteList = _view.RouteGridInfoList.Where(e => e.ID == _view.RouteListRowCommandArgument).ToList();
                _view.VisualizationPanelVisible = false;
                _view.CreationPanelVisible = true;
                _view.CreationPanelEnabled = false;
            }
            else
            {
                _locationModel.RemoveRoute(_view.RouteListRowCommandArgument, _view.UserName);
                BindDashboard();
            }
        }

        public void SaveUpdateRoute()
        {
            _locationModel.SaveUpdateRoute(_view.RouteList, _view.UserName);
        }

        public void ClearForm()
        {
            _view.ClearForm();
            _view.VisualizationPanelVisible = true;
            _view.CreationPanelVisible = false;
        }

        #endregion
    }
}
