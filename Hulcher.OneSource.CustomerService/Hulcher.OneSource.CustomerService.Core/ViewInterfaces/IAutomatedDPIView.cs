using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    public interface IAutomatedDPIView : IBaseView
    {
        string GenerationDate { set; }

        #region [ Filter ]

        Globals.DPI.FilterType FilterValue { get; set; }

        DateTime ProcessDate { get; set; }

        string NewJobDateFilter { get; }
        #endregion

        #region [ Dashboard Bind ]

        #region [ Common ]

        IList<CS_View_DPIInformation> DashboardDataSource { get; set; }

        #endregion

        #region [ Job ]

        IList<CS_View_DPIInformation> JobRowDatasource { get; set; }

        CS_View_DPIInformation JobRowDataItem { get; set; }

        string JobRowJobNumber { get; set; }
        string JobRowDivision { get; set; }
        string JobRowCustomer { get; set; }
        string JobRowLocation { get; set; }
        string JobRowJobAction { get; set; }
        int? JobRowCarCountEngines { get; set; }
        int? JobRowCarCountEmpties { get; set; }
        int? JobRowCarCountLoads { get; set; }
        int JobRowDpiID { get; set; }
        Globals.DPI.DpiStatus JobRowDpiStatus { get; set; }
        Globals.DPI.CalculationStatus JobRowStatus { get; set; }
        double JobRowRevenue { get; set; }
        bool JobRowHasResources { get; set; }

        #endregion

        #region [ Resources ]

        IList<CS_View_DPIInformation> ResourcesRowDatasource { get; set; }

        CS_View_DPIInformation ResourcesRowDataItem { get; set; }
        int ResourcesRowJobID { get; set; }
        string ResourcesRowDivision { get; set; }
        string ResourcesRowResource { get; set; }
        string ResourcesRowDescription { get; set; }
        Globals.DPI.CalculationStatus ResourcesRowCalculationStatus { get; set; }
        double ResourcesRowRevenue { get; set; }

        #endregion

        #endregion

        #region [ Footer Fields ]

        double RunningTotal { get; set; }
        int TotalJobs { get; set; }

        #endregion
    }
}
