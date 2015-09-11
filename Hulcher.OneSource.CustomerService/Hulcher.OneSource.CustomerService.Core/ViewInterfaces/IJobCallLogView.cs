using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;
using System.Web.UI.WebControls;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    /// <summary>
    /// View Interface for the Job Call Log User Control
    /// </summary>
    public interface IJobCallLogView : IBaseView
    {
        /// <summary>
        /// Property for storing the Job ID in the control
        /// </summary>
        int? JobId { get; set; }

        /// <summary>
        /// Property for ScrollableGridView's DataSource manipulation
        /// </summary>
        IList<CS_CallLog> DataSource { set; }

        /// <summary>
        /// Property for accessing the FilterType in the control
        /// </summary>
        Globals.JobRecord.FilterType FilterType { get; }

        /// <summary>
        /// Property for accessing the FilterValue in the control
        /// </summary>
        string FilterValue { get; }

        /// <summary>
        /// Method for setting the HyperLinks Visibility inside CallLog GridView
        /// </summary>
        void SetCallLogGridViewHyperLinkVisibility(bool visible, string controlName);

        /// <summary>
        /// Property for accessing the individual row from Call Log GridView
        /// </summary>
        GridViewRow CallLogGridViewRow { get; set; }

        /// <summary>
        /// Property for accessing the HyperLinks Visibility inside CallLog GridView
        /// </summary>
        bool GetCallLogGridViewHyperLinkVisibility(string controlName);
    }
}
