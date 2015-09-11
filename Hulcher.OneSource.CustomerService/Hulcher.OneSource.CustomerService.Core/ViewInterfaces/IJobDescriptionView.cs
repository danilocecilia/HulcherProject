using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    public interface IJobDescriptionView : IBaseView
    {
        /// <summary>
        /// Retrieves the JobID of the Current Job
        /// </summary>
        int? JobId { get; }

        /// <summary>
        /// Job Id when Cloning a Job record
        /// </summary>
        int? CloningId { get; set; }

        /// <summary>
        /// Sets the Eic Contact List to the DropDown on the WebPage
        /// </summary>
        int NumberEnginesValue { get; set; }

        /// <summary>
        /// Sets the Bill-To Contact List to the DropDown on the WebPage
        /// </summary>
        int NumberLoadsValue { get;  set; }

        /// <summary>
        /// Sets the Additional Contact List to the DropDown on the WebPage
        /// </summary>
        int NumberEmptiesValue { get;  set; }

        /// <summary>
        /// Sets the Lading Text to the textbox on the WebPage
        /// </summary>
        string LadingValue { get; set; }

        /// <summary>
        /// Sets the UN Number Text to the textbox on the WebPage
        /// </summary>
        string UnNumber { get; set; }

        /// <summary>
        /// Sets the STCC Info Text to the textbox on the WebPage
        /// </summary>
        string StccInfo { get; set; }

        /// <summary>
        /// Sets the Hazmat Text to the textbox on the WebPage
        /// </summary>
        string Hazmat { get; set; }

        /// <summary>
        /// List of Scope to use in the DataGrid
        /// </summary>
        IList<CS_ScopeOfWork> ScopeOfWorkList { get; set; }

        CS_ScopeOfWork ScopeOfWorkEntity { get; set; }

        CS_JobDescription JobDescriptionEntity { get; }

        CS_View_GetJobData JobDescriptionLoad { set; }

        /// <summary>
        /// Load the ScopeOfWork Information List inside the GridView
        /// </summary>
        void ListScopeOfWork();

        /// <summary>
        /// Clear all the form fields for a new entry
        /// </summary>
        void ClearFields();
    }
}
