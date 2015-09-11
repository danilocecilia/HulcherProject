using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    public interface ICustomerRequestView : IBaseView
    {
        #region [ Filters ]

        /// <summary>
        /// Filter Type
        /// </summary>
        Globals.CustomerMaintenance.RequestFilterType FilterType { get; set; }

        /// <summary>
        /// Filter Value
        /// </summary>
        string FilterValue { get; set; }

        #endregion

        #region [ Request Listing ]

        /// <summary>
        /// Order by for the grid (column and direction)
        /// </summary>
        string[] OrderBy { get; }

        /// <summary>
        /// Order By Column
        /// </summary>
        Globals.Common.Sort.CustomerRequestSortColumns SortColumn { get; }

        /// <summary>
        /// Order By Direction
        /// </summary>
        Globals.Common.Sort.SortDirection SortDirection { get; }

        /// <summary>
        /// Request Listing
        /// </summary>
        IList<CS_Request> RequestList { get; set; }

        /// <summary>
        /// Current Request Item (during a databind)
        /// </summary>
        CS_Request RequestItem { get; set; }

        #region [ Fields ]

        /// <summary>
        /// Request ID
        /// </summary>
        int RequestItemRequestID { get; set; }

        /// <summary>
        /// Request Date
        /// </summary>
        DateTime RequestItemRequestDate { get; set; }

        /// <summary>
        /// Requested By
        /// </summary>
        string RequestItemRequestedBy { get; set; }

        /// <summary>
        /// Request Type
        /// </summary>
        string RequestItemRequestType { get; set; }

        /// <summary>
        /// Customer / Contact Name
        /// </summary>
        string RequestItemCustomerContactName { get; set; }

        /// <summary>
        /// Request Status
        /// </summary>
        Globals.CustomerMaintenance.RequestStatus RequestItemRequestStatus { get; set; }

        /// <summary>
        /// Notes (email)
        /// </summary>
        string RequestItemRequestNotes { get; set; }

        #endregion

        #endregion

        #region [ Commands ]

        /// <summary>
        /// Selected Request ID
        /// </summary>
        int RequestID { get; set; }

        /// <summary>
        /// Current user
        /// </summary>
        string Username { get; set; }

        #endregion

    }
}
