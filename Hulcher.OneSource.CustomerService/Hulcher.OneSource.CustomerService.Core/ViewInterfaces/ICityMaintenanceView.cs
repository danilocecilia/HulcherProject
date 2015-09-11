using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    public interface ICityMaintenanceView : IBaseView
    {
        #region [ Grid ]

        /// <summary>
        /// CityList Row Data Item
        /// </summary>
        CS_City CityRowDataItem { get; }

        /// <summary>
        /// CityList RemoveButton Visibility
        /// </summary>
        bool CityRowCSRecord { get; set; }

        /// <summary>
        /// City List Data Source
        /// </summary>
        IList<CS_City> CityListDataSource { set; }

        /// <summary>
        /// CityList ID
        /// </summary>
        int CityRowCityID { get; set; }

        /// <summary>
        /// CityList State
        /// </summary>
        string CityRowStateName { get; set; }

        /// <summary>
        /// CityList Country
        /// </summary>
        string CityRowCountry { get; set; }

        /// <summary>
        /// CityList RowCommandName
        /// </summary>
        Globals.Common.GridView.GridCommandNames CityListRowCommandName { set; get; }

        /// <summary>
        /// CityList RowCommandArgument
        /// </summary>
        int CityListRowCommandArgument { set; get; }

        #endregion

        #region [ Filter ]

        /// <summary>
        /// Selected State Name
        /// </summary>
        string StateName { get; }

        #endregion

        #region [ Common ]

        /// <summary>
        /// City Identifyer
        /// </summary>
        int? CityID { get; set; }

        /// <summary>
        /// User Name
        /// </summary>
        string Username { get; }

        /// <summary>
        /// City Entity
        /// </summary>
        CS_City CityEntity { get; set; }

        /// <summary>
        /// Zip Code List
        /// </summary>
        IList<CS_ZipCode> ZipCodeList { get; }

        /// <summary>
        /// Enables or Disables the Visualization Panel
        /// </summary>
        bool EnableVisualizationPanel { get; set; }

        /// <summary>
        /// Enables or Disables the Creation Panel
        /// </summary>
        bool EnableCreationPanel { get; set; }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Clears all form fields
        /// </summary>
        void ClearForm();

        #endregion
    }
}
