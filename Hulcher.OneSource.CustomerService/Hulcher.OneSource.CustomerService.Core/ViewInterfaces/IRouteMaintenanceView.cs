using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    public interface IRouteMaintenanceView : IBaseView
    {
        #region [ Dashborad ]

        /// <summary>
        /// Keep Grid List
        /// </summary>
        IList<CS_Route> RouteGridInfoList { get; set; }

        /// <summary>
        /// Filter type
        /// </summary>
        int? CityID { get; }

        /// <summary>
        /// Filter Type
        /// </summary>
        int? ZipCodeID { get; }

        /// <summary>
        /// fILTER Type
        /// </summary>
        int? StateID { get; }

        /// <summary>
        /// Route list to bind the grid
        /// </summary>
        IList<CS_Route> RouteDashboardDataSource { set; }

        /// <summary>
        /// Route Grid DataItem
        /// </summary>
        CS_Route RouteRowDataItem { get; }

        /// <summary>
        /// RouteGrid ID Key
        /// </summary>
        int RouteRowID { set; }

        /// <summary>
        /// RouteGrid Location Column
        /// </summary>
        string RouteRowLocation { set; }

        /// <summary>
        /// RouteGrid Division Column
        /// </summary>
        string RouteRowDivision { set; }

        /// <summary>
        /// RouteGrid Location Column
        /// </summary>
        int RouteRowMiles { set; }

        /// <summary>
        /// RouteGrid Hours Column
        /// </summary>
        double RouteRowHours { set; }

        /// <summary>
        /// RouteGrid Fuel Column
        /// </summary>
        int RouteRowFuel { set; }

        /// <summary>
        /// RouteGrid Route Column
        /// </summary>
        string RouteRowRoute { set; }

        /// <summary>
        /// RouteGrid CityPermitOffice Column
        /// </summary>
        string RouteRowCityPermitOffice { set; }

        /// <summary>
        /// RouteGrid CountyPermitOffice Column
        /// </summary>
        string RouteRowCountyPermitOffice { set; }

        /// <summary>
        /// RouteGrid LinkButtons Visibility
        /// </summary>
        bool RouteRowReadOnly { set; }

        /// <summary>
        /// RouteGrid Command Name
        /// </summary>
        Globals.Common.GridView.GridCommandNames RouteListRowCommandName { get; set; }

        /// <summary>
        /// RouteGrid Command Argument
        /// </summary>
        int RouteListRowCommandArgument { get; set; }

        #endregion

        #region [ Form ]

        /// <summary>
        /// Indicates if the form is in edit mode
        /// </summary>
        bool EditMode { get; set; }

        /// <summary>
        /// Holds the Route Identifier
        /// </summary>
        int? RouteID { get; set; }

        /// <summary>
        /// Get/Set the current user
        /// </summary>
        string UserName { get; }

        bool SavedSuccessfuly { get; set; }

        /// <summary>
        /// save and close
        /// </summary>
        bool SaveAndClose { get; set; }

        bool CreationPanelVisible { get; set; }

        bool CreationPanelEnabled { set; }

        bool VisualizationPanelVisible { get; set; }

        bool EnableDisableCreationButton { set; }

        bool ReadOnly { get; set; }

        string Domain { get; }

        IList<CS_Division> ListAllDivisions { get; set; }

        /// <summary>
        /// Gets or sets rout list
        /// </summary>
        IList<CS_Route> RouteList { get; set; }

        
        #endregion

        #region [ Methods ]

        void ClearForm();

        void CopyStateCityToCreate();

        #endregion

     
    }
}
