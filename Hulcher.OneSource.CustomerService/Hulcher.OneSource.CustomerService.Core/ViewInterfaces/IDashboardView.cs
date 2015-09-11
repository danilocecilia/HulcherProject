using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    /// <summary>
    /// Dashboard View Interface
    /// </summary>
    public interface IDashboardView : IBaseView
    {
        #region [ Common ]

        /// <summary>
        /// Property for returning the current Viewpoint of the Page
        /// </summary>
        Globals.Dashboard.ViewType DashBoardViewType { get; set; }

        /// <summary>
        /// Gets the Dashboard Refresh Rate settings
        /// </summary>
        int DashboardRefreshRate { get; set; }

        /// <summary>
        /// String used to Order dashboard records
        /// </summary>
        string[] OrderBy { get; set; }

        /// <summary>
        /// Gets the selected Sort Direction
        /// </summary>
        Globals.Common.Sort.SortDirection SortDirection { get; }

        /// <summary>
        /// The CSV File to download
        /// </summary>
        string CSVFile { set; }

        /// <summary>
        /// Loged user
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// Domain
        /// </summary>
        string Domain { get; }

        /// <summary>
        /// Indicates if the current user has permission to delete a call log
        /// </summary>
        bool? UserHasDeleteCallLogPermission { get; set; }

        #endregion

        #region [ Job Summary ]

        #region [ Filters ]
        /// <summary>
        /// Method that clear the filter fields to reset to the initial values
        /// </summary>
        void ClearFieldsResetJobSummary();

        /// <summary>
        /// Job Status Dropdown Selected Value
        /// </summary>
        int? JobStatusFilterValue { get; set; }

        /// <summary>
        /// Region Dropdown Selected Value
        /// </summary>
        int? JobNumberFilterValue { get; set; }

        /// <summary>
        /// Division Dropdown Selected Value
        /// </summary>
        int? DivisionFilterValue { get; set; }

        /// <summary>
        /// Customer Dropdown Selected Value
        /// </summary>
        int? CustomerFilterValue { get; set; }

        /// <summary>
        /// Date Type Dropdown Selected Value
        /// </summary>
        Globals.Dashboard.DateFilterType DateFilterTypeValue { get; set; }

        /// <summary>
        /// Start Date Selected Value
        /// </summary>
        DateTime BeginDateJobSummaryValue { get; set; }

        /// <summary>
        /// End Date Selected Value
        /// </summary>
        DateTime EndDateJobSummaryValue { get; set; }

        /// <summary>
        /// Person Name Text
        /// </summary>
        string PersonNameValueJobSummary { get; set; }

        #endregion

        #region [ Data Sources ]

        /// <summary>
        /// Job Summary Data Source
        /// </summary>
        List<CS_SP_GetJobSummary_Result> JobSummaryDataSource { get; set; }

        /// <summary>
        /// Job Summary Repeater Data Source
        /// </summary>
        List<CS_SP_GetJobSummary_Result> JobSummaryRepeaterDataSource { get; set; }

        /// <summary>
        /// Job Summary Resource Repeater Data Source
        /// </summary>
        List<CS_SP_GetJobSummary_Result> JobSummaryResourceRepeaterDataSource { get; set; }

        #endregion

        #region [ Data Items ]

        /// <summary>
        /// Current Line of the Job Summary Repeater
        /// </summary>
        CS_SP_GetJobSummary_Result JobSummaryRepeaterDataItem { get; set; }

        /// <summary>
        /// Current Line of the Job Summary Resource Repeater
        /// </summary>
        CS_SP_GetJobSummary_Result JobSummaryResourceRepeaterDataItem { get; set; }

        #endregion

        #region [ Sorting ]

        /// <summary>
        /// Gets the selected Sort Column for Job Summary Repeater
        /// </summary>
        Globals.Common.Sort.JobSummarySortColumns JobSummarySortColumn { get; }

        #endregion

        #region [ Row Attributes - Job ]

        string JobSummaryRowDivision { get; set; }

        int JobSummaryRowJobId { get; set; }

        int? JobSummaryRowHasResources { get; set; }

        string JobSummaryRowJobNumber { get; set; }

        string JobSummaryRowCustomer { get; set; }

        string JobSummaryRowStatus { get; set; }

        string JobSummaryRowLocation { get; set; }

        string JobSummaryRowProjectManager { get; set; }

        string JobSummaryRowModifiedBy { get; set; }

        DateTime JobSummaryRowLastModification { get; set; }

        DateTime JobSummaryRowCallDate { get; set; }

        DateTime? JobSummaryRowPresetDate { get; set; }

        string JobSummaryRowLastCallEntry { get; set; }

        int? JobSummaryRowLastCallEntryId { get; set; }

        bool JobSummaryRowLastCallEntryIsAutomaticProcess { get; set; }

        DateTime? JobSummaryRowLastCallDate { get; set; }

        #endregion

        #region [ Row Attributes - Resource ]

        string JobSummaryResourceRowDivision { get; set; }

        int JobSummaryResourceRowJobId { get; set; }

        string JobSummaryResourceRowResource { get; set; }

        string JobSummaryResourceRowLocation { get; set; }

        string JobSummaryResourceRowModifiedBy { get; set; }

        DateTime? JobSummaryResourceRowLastModification { get; set; }

        DateTime? JobSummaryResourceRowCallDate { get; set; }

        string JobSummaryResourceRowLastCallEntry { get; set; }

        int? JobSummaryResourceRowLastCallEntryId { get; set; }

        bool JobSummaryResourceRowLastCallEntryIsAutomaticProcess { get; set; }

        DateTime? JobSummaryResourceRowLastCallDate { get; set; }

        #endregion

        #endregion

        #region [ Job Call Log ]

        #region [ Filters ]

        /// <summary>
        /// Job status filter from call log view
        /// </summary>
        int? JobStatusCallLogViewFilter { get; set; }

        /// <summary>
        /// Call type value filter from call log view
        /// </summary>
        int? CallTypeCallLogViewFilter { get; set; }

        /// <summary>
        /// Modified by value filter from call log view
        /// </summary>
        string ModifiedByCallLogViewFilter { get; set; }

        /// <summary>
        /// ShiftTransferLog by value filter from call log view
        /// </summary>
        bool ShiftTransferLogCallLogViewFilter { get; set; }

        /// <summary>
        /// GeneralLog by value filter from call log view
        /// </summary>
        bool GeneralLogCallLogViewFilter { get; set; }

        /// <summary>
        /// Division value filter from call log view
        /// </summary>
        int? DivisionValueCallLogViewFilter { get; set; }

        /// <summary>
        /// Begin date value for call log view
        /// </summary>
        DateTime BeginDateCallLogViewFilter { get; set; }

        /// <summary>
        /// End date value for call log view
        /// </summary>
        DateTime EndDateCallLogViewFilter { get; set; }

        /// <summary>
        /// Person Name Text
        /// </summary>
        string PersonNameCallLog { get; set; }

        /// <summary>
        /// Method to reset filter fields on the job calllog
        /// </summary>
        void ClearFieldsResetCallLog();
        #endregion

        #region [ Data Sources ]

        /// <summary>
        /// CallLog View Data Source
        /// </summary>
        List<CS_View_JobCallLog> CalllogViewDataSource { get; set; }

        /// <summary>
        /// CallLog View DivisionList
        /// </summary>
        IList<CS_Division> CallLogViewDivisionList { get; set; }

        /// <summary>
        /// Job Repeater DataSource
        /// </summary>
        List<CS_View_JobCallLog> JobRepeaterDataSource { get; set; }

        /// <summary>
        /// CallLog Repeater DataSource
        /// </summary>
        List<CS_View_JobCallLog> CallLogRepeaterDataSource { get; set; }

        #endregion

        #region [ Data Items ]

        /// <summary>
        /// DivisionRepeater iteration DataItem
        /// </summary>
        CS_Division DivisionRepeaterDataItem { get; set; }

        /// <summary>
        /// JobRepeater iteration DataItem
        /// </summary>
        CS_View_JobCallLog JobRepeaterDataItem { get; set; }

        /// <summary>
        /// CallLogRepeater iteration DataItem
        /// </summary>
        CS_View_JobCallLog CallLogRepeaterDataItem { get; set; }

        #endregion

        #region [ Sorting ]

        /// <summary>
        /// Gets the selected Sort Column for Job Call Log Repeater
        /// </summary>
        Globals.Common.Sort.JobCallLogSortColumns JobCallLogSortColumn { get; }

        #endregion

        #region [ Counters ]

        /// <summary>
        /// Index of the Division
        /// </summary>
        int DivisionCount { get; set; }

        /// <summary>
        /// Index of the Job
        /// </summary>
        int JobCount { get; set; }

        /// <summary>
        /// Index of the Call Log
        /// </summary>
        int CallLogCount { get; set; }

        #endregion

        #region [ Row Attributes - Division ]

        /// <summary>
        /// DivisionRow Name column
        /// </summary>
        string DivisionRowName { get; set; }

        /// <summary>
        /// Change Division Column Visibility in Call Log Repeater
        /// </summary>
        bool CallLogRepeaterDivisionColumnVisibility { set; }

        /// <summary>
        /// Change Division Header Visibility in Call Log Repeater
        /// </summary>
        bool CallLogRepeaterDivisionHeaderVisibility { set; }

        #endregion

        #region [ Row Attributes - Job ]

        /// <summary>
        /// Job Number and Customer column
        /// </summary>
        string JobRowJobNumberCustomer { get; set; }

        /// <summary>
        /// Change Job Column Visibility in Call Log Repeater
        /// </summary>
        bool CallLogRepeaterJobColumnVisibility { set; }

        #endregion

        #region [ Row Attributes - Call Log ]

        /// <summary>
        /// Call Id column
        /// </summary>
        int CallLogRowCallId { get; set; }

        /// <summary>
        /// JobId for the right click functionality
        /// </summary>
        int CallLogRowJobId { get; set; }

        /// <summary>
        /// Last Modification Column
        /// </summary>
        string CallLogRowLastModification { get; set; }

        /// <summary>
        /// Division Name column
        /// </summary>
        string CallLogRowCallType { get; set; }

        /// <summary>
        /// CalledInBy column
        /// </summary>
        string CallLogRowCalledInBy { get; set; }

        /// <summary>
        /// CallDate column
        /// </summary>
        string CallLogRowCallDate { get; set; }

        /// <summary>
        /// CallTime column
        /// </summary>
        string CallLogRowCallTime { get; set; }

        /// <summary>
        /// ModifiedBy column
        /// </summary>
        string CallLogRowModifiedBy { get; set; }

        /// <summary>
        /// Verify if it is a Automatic Process or not
        /// </summary>
        bool CallLogRowAutomaticProcess { get; set; }

        /// <summary>
        /// Details column
        /// </summary>
        string CallLogRowDetails { get; set; }

        /// <summary>
        /// SelectedRow Hidden Field
        /// </summary>
        string CallLogSelectedRowId { set; }

        /// <summary>
        /// Change Call Entry Column Visibility in Call Log Repeater
        /// </summary>
        bool CallLogRepeaterCallEntryColumnVisibility { set; }

        /// <summary>
        /// Set If the Delete Link must be enabled  
        /// </summary>
        bool EnableDeleteLink { set; }

        /// <summary>
        /// Set the CallLogId to the delete functionality
        /// </summary>
        int CallLogIdToDelete { get; set; }
        #endregion

        #region [ Read/Unread Functionality ]

        /// <summary>
        /// List with the ID's and Last Modification DateTime of the Unread Items
        /// </summary>
        Dictionary<int, DateTime> ReadItems { get; set; }

        /// <summary>
        /// String that contains the Newly read items (ID's), separated by a comma
        /// </summary>
        string NewlyReadItems { get; set; }

        /// <summary>
        /// Set the Style of the Call Entry Row ("Read" or "Unread")
        /// </summary>
        string CallLogRowStyle { set; }

        #endregion
            

        #endregion
    }
}
