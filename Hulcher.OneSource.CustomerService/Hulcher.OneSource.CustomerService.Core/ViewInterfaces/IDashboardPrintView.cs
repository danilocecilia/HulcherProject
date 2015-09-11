using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    /// <summary>
    /// Dashboard Print View Interface
    /// </summary>
    public interface IDashboardPrintView : IBaseView
    {
        #region [ Common ]

        #region [ Sorting ]

        Globals.Common.Sort.JobSummarySortColumns JobSummarySortColumn { get; }

        Globals.Common.Sort.JobCallLogSortColumns JobCallLogSortColumn { get; }

        Globals.Common.Sort.SortDirection SortDirection { get; }

        #endregion

        #endregion

        #region [ Job Summary - Viewpoint 2 ]

        /// <summary>
        /// Job Summary Data Source
        /// </summary>
        List<CS_SP_GetJobSummary_Result> JobSummaryDataSource { get; set; }

        /// <summary>
        /// Job Summary Repeater Data Source
        /// </summary>
        List<CS_SP_GetJobSummary_Result> JobSummaryRepeaterDataSource { set; }

        CS_SP_GetJobSummary_Result JobSummaryRepeaterDataItem { get; set; }

        CS_SP_GetJobSummary_Result JobSummaryResourceRepeaterDataItem { get; set; }

        List<CS_SP_GetJobSummary_Result> JobSummaryResourceRepeaterDataSource { get; set; }

        #region [ Row Attributes - Job ]

        string JobSummaryRowDivision { get; set; }

        int JobSummaryRowJobId { get; set; }

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

        DateTime? JobSummaryResourceRowLastCallDate { get; set; }

        #endregion

        /// <summary>
        /// Job Status Label Text, Which will appear at the Print page
        /// </summary>
        string JobStatusLabelText { set; }

        /// <summary>
        /// Job Number Label Text, Which will appear at the Print page
        /// </summary>
        string JobNumberLabelText { set; }

        /// <summary>
        /// Division Label Text, Which will appear at the Print page
        /// </summary>
        string DivisionLabelText { set; }

        /// <summary>
        /// Customer Label Text, Which will appear at the Print page
        /// </summary>
        string CustomerLabelText { set; }

        /// <summary>
        /// Job Status Dropdown Selected Value
        /// </summary>
        int? JobStatusFilterValue { get; }

        /// <summary>
        /// Region Dropdown Selected Value
        /// </summary>
        int? JobNumberFilterValue { get; }

        /// <summary>
        /// Division Dropdown Selected Value
        /// </summary>
        int? DivisionFilterValue { get; }

        /// <summary>
        /// Customer Dropdown Selected Value
        /// </summary>
        int? CustomerFilterValue { get; }

        /// <summary>
        /// Date Type Dropdown Selected Value
        /// </summary>
        Globals.Dashboard.DateFilterType DateFilterTypeValue { get; }

        /// <summary>
        /// Start Date Selected Value
        /// </summary>
        DateTime StartDateFilterValue { get; }

        /// <summary>
        /// End Date Selected Value
        /// </summary>
        DateTime EndDateFilterValue { get; }

        #endregion

        #region [ Job Call Log - Viewpoint 1 ]

        /// <summary>
        /// Call Type Selected Value
        /// </summary>
        int? CallTypeFilterValue { get; }

        /// <summary>
        /// Modified By Selected Value
        /// </summary>
        string ModifiedByFilterValue { get; }

        /// <summary>
        /// Order By listing
        /// </summary>
        string OrderBy { get; }

        /// <summary>
        /// Job Call Log Data Source
        /// </summary>
        List<CS_View_JobCallLog> CalllogViewDataSource { get; set; }

        /// <summary>
        /// Job Call Log Division Data Source
        /// </summary>
        IList<CS_Division> CallLogViewDivisionList { set; }

        /// <summary>
        /// Job Repeater DataSource
        /// </summary>
        List<CS_View_JobCallLog> JobRepeaterDataSource { get; set; }

        /// <summary>
        /// CallLog Repeater DataSource
        /// </summary>
        List<CS_View_JobCallLog> CallLogRepeaterDataSource { get; set; }

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

        /// <summary>
        /// DivisionRow Name column
        /// </summary>
        string DivisionRowName { get; set; }

        /// <summary>
        /// Job Number and Customer column
        /// </summary>
        string JobRowJobNumberCustomer { get; set; }

        /// <summary>
        /// Call Id column
        /// </summary>
        int CallLogRowCallId { get; set; }

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
        /// Details column
        /// </summary>
        string CallLogRowDetails { get; set; }

        /// <summary>
        /// Person Name label text
        /// </summary>
        string PersonNameLabelText { set; }

        /// <summary>
        /// Job Status Label Text for Call Log
        /// </summary>
        string JobStatusCallLogLabelText { set; }

        /// <summary>
        /// Call Type Label Text for Call Log
        /// </summary>
        string CallTypeCallLogLabelText { set; }

        /// <summary>
        /// Modified By Label Text for Call Log
        /// </summary>
        string ModifiedByCallLogLabelText { set; }

        /// <summary>
        /// Division Lable Text for Call Log
        /// </summary>
        string DivisionCallLogLabelText { set; }

        /// <summary>
        /// ShiftTransferLog Filter from Call Log View
        /// </summary>
        bool ShiftTransferLogFilter { get; }

        /// <summary>
        /// GeneralLog Filter from Call Log View
        /// </summary>
        bool GeneralLogFilter { get; }

        /// <summary>
        /// Person Name Text
        /// </summary>
        string PersonNameFilterValue { get; }

        #endregion

        #region [ Job Search - Viewpoint 3 ]

        List<CS_View_JobSummary> JobSummarySearchDataSource { get; set; }

        /// <summary>
        /// Repeater Data Source
        /// </summary>
        List<CS_View_JobSummary> JobSummarySearchRepeaterDataSource { get; set; }

        /// <summary>
        /// Resource Repeater Data Source
        /// </summary>
        List<CS_View_JobSummary> JobSummarySearchResourceRepeaterDataSource { get; set; }

        CS_View_JobSummary JobSummarySearchRepeaterDataItem { get; set; }

        CS_View_JobSummary JobSummarySearchResourceRepeaterDataItem { get; set; }

        /// <summary>
        /// Filter selection of contact filter
        /// </summary>
        string ContactFilter { get; }

        /// <summary>
        /// Filter selection of job filter
        /// </summary>
        string JobFilter { get; }

        /// <summary>
        /// Filter selection of location filter
        /// </summary>
        string LocationFilter { get; }

        /// <summary>
        /// Filter selection of job description filter
        /// </summary>
        string JobDescriptionFilter { get; }

        /// <summary>
        /// Filter selection of equipment type filter
        /// </summary>
        string EquipmentTypeFilter { get; }

        /// <summary>
        /// Filter selection of resource filter
        /// </summary>
        string ResourceFilter { get; }

        /// <summary>
        /// Filter selection of contact value
        /// </summary>
        string ContactFilterValue { get; }

        /// <summary>
        /// Filter selection of job value
        /// </summary>
        string JobFilterValue { get; }

        /// <summary>
        /// Filter selection of location value
        /// </summary>
        string LocationFilterValue { get; }

        /// <summary>
        /// Filter selection of job description value
        /// </summary>
        string JobDescriptionFilterValue { get; }

        /// <summary>
        /// Filter selection of equipment type value
        /// </summary>
        string EquipmentTypeFilterValue { get; }

        /// <summary>
        /// Filter selection of Resource value
        /// </summary>
        string ResourceFilterValue { get; }

        /// <summary>
        /// Filter selection of date range from value
        /// </summary>
        DateTime DateRangeBeginValue { get; }

        /// <summary>
        /// Filter selection of date range to value
        /// </summary>
        DateTime DateRangeEndValue { get; }

        /// <summary>
        /// Value for the Search Contact Info Label
        /// </summary>
        string SearchContactInfoLabel { set; }

        /// <summary>
        /// Value for the Search Job Info Label
        /// </summary>
        string SearchJobInfoLabel { set; }

        /// <summary>
        /// Value for the Search Location Info Label
        /// </summary>
        string SearchLocationInfoLabel { set; }

        /// <summary>
        /// Value for the Search Job Description Label
        /// </summary>
        string SearchJobDescriptionLabel { set; }

        /// <summary>
        /// Value for the Search Equipment Type Label
        /// </summary>
        string SearchEquipmentTypeLabel { set; }

        /// <summary>
        /// Value for the Search Resource Label
        /// </summary>
        string SearchResourceLabel { set; }

        /// <summary>
        /// Value for the Search Date Range Label
        /// </summary>
        string SearchDateRangeLabel { set; }

        string JobSummarySearchRowDivision { get; set; }

        string JobSummarySearchRowJobNumber { get; set; }

        string JobSummarySearchRowCustomer { get; set; }

        string JobSummarySearchRowStatus { get; set; }

        string JobSummarySearchRowLocation { get; set; }

        string JobSummarySearchRowProjectManager { get; set; }

        string JobSummarySearchRowModifiedBy { get; set; }

        DateTime JobSummarySearchRowLastModification { get; set; }

        DateTime JobSummarySearchRowCallDate { get; set; }

        DateTime? JobSummarySearchRowPresetDate { get; set; }

        string JobSummarySearchRowLastCallEntry { get; set; }

        DateTime? JobSummarySearchRowLastCallDate { get; set; }

        string JobSummarySearchResourceRowDivision { get; set; }

        string JobSummarySearchResourceRowResource { get; set; }

        string JobSummarySearchResourceRowLocation { get; set; }

        string JobSummarySearchResourceRowModifiedBy { get; set; }

        DateTime? JobSummarySearchResourceRowLastModification { get; set; }

        DateTime? JobSummarySearchResourceRowCallDate { get; set; }

        string JobSummarySearchResourceRowLastCallEntry { get; set; }

        DateTime? JobSummarySearchResourceRowLastCallDate { get; set; }

        /// <summary>
        /// Gets the list of Job ID's returned from the Procedure
        /// </summary>
        IList<int> JobIdList { get; set; }

        #endregion
    }
}
