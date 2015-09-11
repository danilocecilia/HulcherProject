using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    public interface IDPIReportView : IBaseView
    {
        /// <summary>
        /// Report View Request Parameter
        /// </summary>
        Globals.DPIReport.ReportView? ReportView { get; }

        /// <summary>
        /// Report Date Requet Parameter
        /// </summary>
        DateTime? ReportDate { get; }

        /// <summary>
        /// New Jobs DataSource
        /// </summary>
        IList<CS_View_DPIReport> NewJobsDataSource { set; }

        /// <summary>
        /// Continuing Jobs DataSource
        /// </summary>
        IList<CS_View_DPIReport> ContinuingJobsDataSource { set; }

        /// <summary>
        /// Report Parameters
        /// </summary>
        IDictionary<string, string> ReportParameters { set; }
    }
}
