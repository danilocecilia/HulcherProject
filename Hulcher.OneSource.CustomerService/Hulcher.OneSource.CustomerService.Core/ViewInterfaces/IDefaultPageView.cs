using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Hulcher.OneSource.CustomerService.DataContext.VO;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    /// <summary>
    /// Interface for the Default Page
    /// </summary>
    public interface IDefaultPageView : IBaseView
    {
        /// <summary>
        /// Gets the logged username
        /// </summary>
        string Username { get; }
        
        /// <summary>
        /// Gets the logged domain
        /// </summary>
        string Domain { get; }

        /// <summary>
        /// Enables/Disables the New Job button
        /// </summary>
        bool EnableNewJobButton { set; }

        /// <summary>
        /// Enables/Disables the First Alert Link
        /// </summary>
        bool EnableFirstAlertLink { set; }

        /// <summary>
        /// Enables/Disables Route Maintenance Button
        /// </summary>
        bool EnableRouteButton { set; }

        //JobId to be used for QuickSearch
        int QuickSearchJobId { get; set; }

        //QuickSearch Textbox Value        
        string QuickSearchJobValue { get; set; }

        //Gets or sets the Quick Search Query String
        string QuickSearchQueryString { get; set; }

        /// <summary>
        /// Enables/Disables the call criteria for the customer contact and employees
        /// </summary>
        bool EnableNewCallCriteria { set; }

        /// <summary>
        /// List of Items in the Resource DropDown
        /// </summary>
        ListItemCollection ResourceDropDownItems { get; }

        /// <summary>
        /// List of Items in the Equipment Type DropDown
        /// </summary>
        ListItemCollection EquipmentTypeDropDownItems { get; }

        /// <summary>
        /// List of Items in the Job Description DropDown
        /// </summary>
        ListItemCollection JobDescriptionDropDownItems { get; }

        /// <summary>
        /// List of Items in the Location Info DropDown
        /// </summary>
        ListItemCollection LocationInfoDropDownItems { get; }

        /// <summary>
        /// List of Items in the Job Info DropDown
        /// </summary>
        ListItemCollection JobInfoDropDownItems { get; }

        /// <summary>
        /// List of Items in the Contact Info DropDown
        /// </summary>
        ListItemCollection ContactInfoDropDownItems { get; }

        /// <summary>
        /// Get or Set the StartDate Datepicker value
        /// </summary>
        DateTime? StartDate { get; set; }

        /// <summary>
        /// Get or Set the EndDate Datepicker value
        /// </summary>
        DateTime? EndDate { get; set; }

        /// <summary>
        /// Returns a Key/Value pair representing the Selected Value
        /// </summary>
        KeyValuePair<string, string> ResourceDropDownSelected { get; }

        /// <summary>
        /// Returns a Key/Value pair representing the Selected Value
        /// </summary>
        KeyValuePair<string, string> EquipmentTypeDropDownSelected { get; }

        /// <summary>
        /// Returns a Key/Value pair representing the Selected Value
        /// </summary>
        KeyValuePair<string, string> JobDescriptionDropDownSelected { get; }

        /// <summary>
        /// Returns a Key/Value pair representing the Selected Value
        /// </summary>
        KeyValuePair<string, string> LocationInfoDropDownSelected { get; }

        /// <summary>
        /// Returns a Key/Value pair representing the Selected Value
        /// </summary>
        KeyValuePair<string, string> JobInfoDropDownSelected { get; }

        /// <summary>
        /// Returns a Key/Value pair representing the Selected Value
        /// </summary>
        KeyValuePair<string, string> ContactInfoDropDownSelected { get; }

        /// <summary>
        /// Returns a Key/Value pair representing the Selected Value
        /// </summary>
        KeyValuePair<string, string> StartDateSelected { get; }

        /// <summary>
        /// Returns a Key/Value pair representing the Selected Value
        /// </summary>
        KeyValuePair<string, string> EndDateSelected { get; }

        /// <summary>
        /// List that holds the Job Id from the Search Criteria
        /// </summary>
        IList<int> SearchJobList { get; set; }

        /// <summary>
        /// Holds all the objects from the Filters
        /// </summary>
        SearchCriteriaVO SearchFiltersVO { get; set; }

        bool IsSearchJobListEmpty { get; set; }

        /// <summary>
        /// Enable Permiting Notification
        /// </summary>
        bool EnablePermitingNotification { set; }
    }
}
