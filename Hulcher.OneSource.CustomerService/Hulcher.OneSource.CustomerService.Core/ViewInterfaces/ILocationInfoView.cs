using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    public interface ILocationInfoView : IBaseView
    {
        /// <summary>
        /// Set the Country list to the dropdownlist on the webpage
        /// </summary>
        IList<CS_Country> CountryList { set; }

        /// <summary>
        /// Set the Country list to the dropdownlist on the page
        /// </summary>
        IList<CS_City> CityList { set; }

        /// <summary>
        /// Set the state list to the dropdownlist on the page
        /// </summary>
        IList<CS_State> StateList { set; }

        /// <summary>
        /// Set the state list by country id on the page
        /// </summary>
        IList<CS_State> StateListByCountryId { set; }

        /// <summary>
        /// Set the city list by state id
        /// </summary>
        IList<CS_City> CityListByStateId { set; }

        /// <summary>
        /// Set the zipcode list by city Id
        /// </summary>
        IList<CS_ZipCode> ZipCodeListByCityId { set; }
        
        /// <summary>
        /// Get the country id
        /// </summary>
        int CountryValue { get; set; }

        /// <summary>
        /// Get the state id
        /// </summary>
        int StateValue { get; }

        /// <summary>
        /// Get the city id
        /// </summary>
        int CityValue { get; }

        /// <summary>
        /// Get the zipcode id
        /// </summary>
        string ZipCodeValue { get; }

        /// <summary>
        /// Get the sitename
        /// </summary>
        string SiteName { get; }

        /// <summary>
        /// Get the alternate location
        /// </summary>
        string AlternateLocation { get; }

        /// <summary>
        /// Get directions
        /// </summary>
        string Directions { get; }

        /// <summary>
        /// Access to the Location Info Entity for saving
        /// </summary>
        CS_LocationInfo LocationInfoEntity { get; }

        /// <summary>
        /// Used to load the page
        /// </summary>
        CS_View_GetJobData LocationInfoLoad { set; }

        /// <summary>
        /// Get and Set the Job Id
        /// </summary>
        int? JobId { get; set; }

        /// <summary>
        /// Job Id value when cloning a job record
        /// </summary>
        int? CloningId { get; set; }
    }
}
