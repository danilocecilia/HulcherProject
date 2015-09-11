using System;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    /// <summary>
    /// View Interface for the Essential Job Data
    /// </summary>
    public interface IEssentialJobDataView : IBaseView
    {  
        /// <summary>
        /// Get if is the emergency response 
        /// </summary>
        bool IsEmergencyResponse { get; }

        /// <summary>
        /// Get or set the Primary Contact id
        /// </summary>
        int? PrimaryContactId { get; set; }

        /// <summary>
        /// Get the customer id
        /// </summary>
        int CustomerId { get; }

        /// <summary>
        /// Get or set the Hulcher Contact Id
        /// </summary>
        int? HulcherContactId { get; set; }

        /// <summary>
        /// Get the primary division id
        /// </summary>
        int PrimaryDivisionId { get; }

        /// <summary>
        /// Get the initial call date
        /// </summary>
        DateTime InitialCallDate { get; }

        /// <summary>
        /// Get the initial call time
        /// </summary>
        TimeSpan InitialCallTime { get; }

        /// <summary>
        /// Get the Jobs Status ID
        /// </summary>
        int JobStatusId { get; }

        /// <summary>
        /// Get the price the id
        /// </summary>
        int PriceTypeId { get; }

        /// <summary>
        /// Get the Job acttion id
        /// </summary>
        int JobActionId { get; }

        /// <summary>
        /// Get the state id
        /// </summary>
        int StateId { get; }

        /// <summary>
        /// Get the city id
        /// </summary>
        int CityId { get; }

        /// <summary>
        /// Get the zipcode id
        /// </summary>
        int ZipCode { get; }

        /// <summary>
        /// Get the scope of work
        /// </summary>
        string ScopeOfWork { get; }

        /// <summary>
        /// Get the current username
        /// </summary>
        string Username { get; }

        /// <summary>
        /// Get the JobId
        /// </summary>
        int JobId { get; set; }

        /// <summary>
        /// Indicates if the Job Record was saved successfuly
        /// </summary>
        bool SavedSuccessfuly { set; }
    }
}
