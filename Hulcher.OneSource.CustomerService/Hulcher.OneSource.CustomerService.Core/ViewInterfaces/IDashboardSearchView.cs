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
    public interface IDashboardSearchView : IBaseView
    {
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
        /// Gets the selected Sort Column for Job Summary Repeater
        /// </summary>
        Globals.Common.Sort.JobSummarySortColumns JobSummarySortColumn { get; }

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
        /// Value for the filter panel
        /// </summary>
        string FilterPanel { set; }

        /// <summary>
        /// Job Summary Data Source
        /// </summary>
        List<CS_View_JobSummary> JobSummaryDataSource { get; set; }

        /// <summary>
        /// The CSV File to download
        /// </summary>
        string CSVFile { set; }

        IList<int> JobIdList { get; set; }

        List<CS_View_JobSummary> JobSummaryRepeaterDataSource { get; set; }

        /// <summary>
        /// Gets the selected Sort Direction
        /// </summary>
        Globals.Common.Sort.SortDirection SortDirection { get; }

        /// <summary>
        /// Job Summary Resource Repeater Data Source
        /// </summary>
        List<CS_View_JobSummary> JobSummaryResourceRepeaterDataSource { get; set; }

        /// <summary>
        /// Current Line of the Job Summary Repeater
        /// </summary>
        CS_View_JobSummary JobSummaryRepeaterDataItem { get; set; }

        /// <summary>
        /// Current Line of the Job Summary Resource Repeater
        /// </summary>
        CS_View_JobSummary JobSummaryResourceRepeaterDataItem { get; set; }

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
    }
}
