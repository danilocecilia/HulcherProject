using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    /// <summary>
    /// View Interface for the Sample Page
    /// </summary>
    public interface ISamplePageView : IBaseView
    {
        /// <summary>
        /// Sets the Job List to the GridView on the WebPage
        /// </summary>
        IList<CS_Job> JobList { set; }

        /// <summary>
        /// Gets Job Identifier in order to delete a Job Record
        /// </summary>
        int JobId { get; }
        
        /// <summary>
        /// Gets the Username that requested an operation
        /// </summary>
        string Username { get; }
    }

}
