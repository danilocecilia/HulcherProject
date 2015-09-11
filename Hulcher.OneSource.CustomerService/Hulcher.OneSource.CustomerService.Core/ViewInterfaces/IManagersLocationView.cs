using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    public interface IManagersLocationView : IBaseView
    {
        /// <summary>
        /// Holds the value for the name filter
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Holds the Value for the Call Type filter
        /// </summary>
        int? CallTypeID { get; }

        /// <summary>
        /// Holds the value for the Job Number filter
        /// </summary>
        int? JobID { get; }

        /// <summary>
        /// List of all managers locations ( they were sets keyperson in employee maintenance screen )
        /// </summary>
        List<DataContext.CS_View_ManagersLocation> ListAllManagersLocation { get; set; }

        CS_View_ManagersLocation ManagersLocationDataItem { get; set; }

        string ManagersLocationRowEmployeeName { set; }

        int ManagersLocationRowEmployeeId { set; }

        string ManagersLocationRowLastCallType { set; }

        string ManagersLocationRowHotelDetails { set; }

        string ManagersLocationRowJobNumber { set; }

        DateTime? ManagersLocationRowLastCallLogDate { set; }

        string ManagersLocationRowJobId { get; set; }

        string ManagersLocationRowCallEntryId { get; set; }

        string ManagersLocationRowCallEntryJobId { get; set; }
    }
}
