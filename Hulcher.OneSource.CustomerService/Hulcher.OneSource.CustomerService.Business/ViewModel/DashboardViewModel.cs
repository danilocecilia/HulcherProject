using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Core.Security;
using Hulcher.OneSource.CustomerService.Core.Utils;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;

using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Hulcher.OneSource.CustomerService.Business.ViewModel
{
    public class DashboardViewModel : IDisposable
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of the Dashboard View Interface
        /// </summary>
        private IDashboardView _view;

        /// <summary>
        /// Instance of the Job Model
        /// </summary>
        private JobModel _jobModel;

        /// <summary>
        /// Instance of the CallLog Model
        /// </summary>
        private CallLogModel _callLogModel;

        /// <summary>
        /// Instance of the Division Model
        /// </summary>
        private DivisionModel _divisionModel;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="view">Instance of the Essential Job Data View Interface</param>
        public DashboardViewModel(IDashboardView view)
        {
            _view = view;
            _jobModel = new JobModel();
            _callLogModel = new CallLogModel();
            _divisionModel = new DivisionModel();
        }

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="view">Instance of the Essential Job Data View Interface</param>
        /// <param name="unitOfWork">Unit of Work class (For unit Testing)</param>
        public DashboardViewModel(IDashboardView view, IUnitOfWork unitOfWork)
        {
            _view = view;
            _jobModel = new JobModel(unitOfWork);
            _callLogModel = new CallLogModel(unitOfWork);
            _divisionModel = new DivisionModel(unitOfWork);
        }

        #endregion

        #region [ Methods ]

        #region [ Common ]

        /// <summary>
        /// Loads the page based on the requirements
        /// </summary>
        public void LoadPage()
        {
            // Set the default begin and end date based on requirements
            _view.BeginDateCallLogViewFilter = DateTime.Now.AddDays(-4);
            _view.EndDateCallLogViewFilter = DateTime.Now;
            _view.BeginDateJobSummaryValue = DateTime.Now.AddDays(-4);
            _view.EndDateJobSummaryValue = DateTime.Now;
            _view.JobStatusCallLogViewFilter = 1;
            _view.JobStatusFilterValue = 1;

            // Set the default view to be Job Call Log View
            _view.DashBoardViewType = Core.Globals.Dashboard.ViewType.JobSummaryView;
        }

        #endregion

        #region [ Job Summary ]

        public void DefaultDateJobSummary()
        {
            _view.JobStatusFilterValue = 1;
            _view.BeginDateJobSummaryValue = DateTime.Now.AddDays(-4);
            _view.EndDateJobSummaryValue = DateTime.Now;
        }

        /// <summary>
        /// Sorts Job Summary Data Source
        /// </summary>
        /// <param name="keySelector">Lambda expression to select sort column</param>
        /// <param name="sortDirection">Sort direction</param>
        /// <param name="isResoruce">Filter by IsResource field (true/false)</param>
        /// <param name="jobId">If listing resources, filter only resources of this specific Job Identifier</param>
        /// <returns>Sorted Job Summary List</returns>
        private List<CS_SP_GetJobSummary_Result> SortJobSummaryList<TKey>(Func<CS_SP_GetJobSummary_Result, TKey> keySelector, Globals.Common.Sort.SortDirection sortDirection, bool isResoruce, int? jobId)
        {
            switch (sortDirection)
            {
                case Globals.Common.Sort.SortDirection.Descending:
                    if (isResoruce)
                        return _view.JobSummaryDataSource.FindAll(e => e.JobID == jobId.Value && e.IsResource == true).OrderByDescending(keySelector).ToList();
                    else
                        return _view.JobSummaryDataSource.FindAll(e => e.IsResource == false).OrderByDescending(keySelector).ToList();
                case Globals.Common.Sort.SortDirection.Ascending:
                default:
                    if (isResoruce)
                        return _view.JobSummaryDataSource.FindAll(e => e.JobID == jobId.Value && e.IsResource == true).OrderBy(keySelector).ToList();
                    else
                        return _view.JobSummaryDataSource.FindAll(e => e.IsResource == false).OrderBy(keySelector).ToList();
            }
        }

        /// <summary>
        /// Fills the Job Summary Listing, based on view Filters
        /// </summary>
        public void FilterJobSummary()
        {
            _view.JobSummaryDataSource =
                _jobModel.FindJobSummary(
                    _view.JobStatusFilterValue,
                    _view.JobNumberFilterValue,
                    _view.DivisionFilterValue,
                    _view.CustomerFilterValue,
                    _view.DateFilterTypeValue,
                    _view.BeginDateJobSummaryValue,
                    _view.EndDateJobSummaryValue,
                    _view.PersonNameValueJobSummary).ToList();

            switch (_view.JobSummarySortColumn)
            {
                case Globals.Common.Sort.JobSummarySortColumns.Division:
                    _view.JobSummaryRepeaterDataSource = SortJobSummaryList<string>(e => e.Division, _view.SortDirection, false, null);
                    break;
                case Globals.Common.Sort.JobSummarySortColumns.JobNumber:
                    _view.JobSummaryRepeaterDataSource = SortJobSummaryList<string>(e => e.JobNumber, _view.SortDirection, false, null);
                    break;
                case Globals.Common.Sort.JobSummarySortColumns.CustomerResource:
                    _view.JobSummaryRepeaterDataSource = SortJobSummaryList<string>(e => e.Customer, _view.SortDirection, false, null);
                    break;
                case Globals.Common.Sort.JobSummarySortColumns.JobStatus:
                    _view.JobSummaryRepeaterDataSource = SortJobSummaryList<string>(e => e.JobStatus, _view.SortDirection, false, null);
                    break;
                case Globals.Common.Sort.JobSummarySortColumns.Location:
                    _view.JobSummaryRepeaterDataSource = SortJobSummaryList<string>(e => e.Location, _view.SortDirection, false, null);
                    break;
                case Globals.Common.Sort.JobSummarySortColumns.ProjectManager:
                    _view.JobSummaryRepeaterDataSource = SortJobSummaryList<string>(e => e.ProjectManager, _view.SortDirection, false, null);
                    break;
                case Globals.Common.Sort.JobSummarySortColumns.ModifiedBy:
                    _view.JobSummaryRepeaterDataSource = SortJobSummaryList<string>(e => e.ModifiedBy, _view.SortDirection, false, null);
                    break;
                case Globals.Common.Sort.JobSummarySortColumns.LastModification:
                    _view.JobSummaryRepeaterDataSource = SortJobSummaryList<DateTime>(e => e.LastModification, _view.SortDirection, false, null);
                    break;
                case Globals.Common.Sort.JobSummarySortColumns.InitialCallDate:
                    _view.JobSummaryRepeaterDataSource = SortJobSummaryList<DateTime>(e => e.CallDate, _view.SortDirection, false, null);
                    break;
                case Globals.Common.Sort.JobSummarySortColumns.PresetDate:
                    _view.JobSummaryRepeaterDataSource = SortJobSummaryList<DateTime?>(e => e.PresetDate, _view.SortDirection, false, null);
                    break;
                case Globals.Common.Sort.JobSummarySortColumns.LastCallType:
                    _view.JobSummaryRepeaterDataSource = SortJobSummaryList<string>(e => e.LastCallType, _view.SortDirection, false, null);
                    break;
                case Globals.Common.Sort.JobSummarySortColumns.LastCallDate:
                    _view.JobSummaryRepeaterDataSource = SortJobSummaryList<DateTime?>(e => e.LastCallDate, _view.SortDirection, false, null);
                    break;
                case Globals.Common.Sort.JobSummarySortColumns.None:
                default:
                    _view.JobSummaryRepeaterDataSource = _view.JobSummaryDataSource.FindAll(w => w.IsResource == false);
                    break;
            }
        }

        /// <summary>
        /// Fills the Resource List of the Current Job 
        /// </summary>
        public void GetJobSummaryResourceList()
        {
            if (null != _view.JobSummaryRepeaterDataItem)
            {
                switch (_view.JobSummarySortColumn)
                {
                    case Globals.Common.Sort.JobSummarySortColumns.Division:
                        _view.JobSummaryResourceRepeaterDataSource = SortJobSummaryList<string>(e => e.Division, _view.SortDirection, true, _view.JobSummaryRepeaterDataItem.JobID);
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.CustomerResource:
                        switch (_view.SortDirection)
                        {
                            case Globals.Common.Sort.SortDirection.Descending:
                                _view.JobSummaryResourceRepeaterDataSource = _view.JobSummaryDataSource.FindAll(e => e.JobID == _view.JobSummaryRepeaterDataItem.JobID && e.IsResource == true).OrderByDescending(e => e.EquipmentName).ThenByDescending(e => e.EmployeeName).ToList();
                                break;
                            case Globals.Common.Sort.SortDirection.Ascending:
                            default:
                                _view.JobSummaryResourceRepeaterDataSource = _view.JobSummaryDataSource.FindAll(e => e.JobID == _view.JobSummaryRepeaterDataItem.JobID && e.IsResource == true).OrderBy(e => e.EquipmentName).ThenBy(e => e.EmployeeName).ToList();
                                break;
                        }
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.Location:
                        _view.JobSummaryResourceRepeaterDataSource = SortJobSummaryList<string>(e => e.Location, _view.SortDirection, true, _view.JobSummaryRepeaterDataItem.JobID);
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.ModifiedBy:
                        _view.JobSummaryResourceRepeaterDataSource = SortJobSummaryList<string>(e => e.ModifiedBy, _view.SortDirection, true, _view.JobSummaryRepeaterDataItem.JobID);
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.LastModification:
                        _view.JobSummaryResourceRepeaterDataSource = SortJobSummaryList<DateTime?>(e => e.ResouceLastModification, _view.SortDirection, true, _view.JobSummaryRepeaterDataItem.JobID);
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.InitialCallDate:
                        _view.JobSummaryResourceRepeaterDataSource = SortJobSummaryList<DateTime?>(e => e.ResourCallDate, _view.SortDirection, true, _view.JobSummaryRepeaterDataItem.JobID);
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.LastCallType:
                        _view.JobSummaryResourceRepeaterDataSource = SortJobSummaryList<string>(e => e.LastCallType, _view.SortDirection, true, _view.JobSummaryRepeaterDataItem.JobID);
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.LastCallDate:
                        _view.JobSummaryResourceRepeaterDataSource = SortJobSummaryList<DateTime?>(e => e.LastCallDate, _view.SortDirection, true, _view.JobSummaryRepeaterDataItem.JobID);
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.None:
                    case Globals.Common.Sort.JobSummarySortColumns.JobNumber:
                    case Globals.Common.Sort.JobSummarySortColumns.JobStatus:
                    case Globals.Common.Sort.JobSummarySortColumns.ProjectManager:
                    case Globals.Common.Sort.JobSummarySortColumns.PresetDate:
                    default:
                        _view.JobSummaryResourceRepeaterDataSource = _view.JobSummaryDataSource.FindAll(e => e.JobID == _view.JobSummaryRepeaterDataItem.JobID && e.IsResource == true);
                        break;
                }
            }
        }

        /// <summary>
        /// Fills all fields of a Job Row
        /// </summary>
        public void SetJobSummaryRowData()
        {
            if (null != _view.JobSummaryRepeaterDataItem)
            {
                _view.JobSummaryRowDivision = _view.JobSummaryRepeaterDataItem.Division;
                _view.JobSummaryRowJobId = _view.JobSummaryRepeaterDataItem.JobID;
                _view.JobSummaryRowHasResources = _view.JobSummaryRepeaterDataItem.HasResources;
                _view.JobSummaryRowJobNumber = _view.JobSummaryRepeaterDataItem.PrefixedNumber;
                _view.JobSummaryRowCustomer = _view.JobSummaryRepeaterDataItem.Customer;
                _view.JobSummaryRowStatus = _view.JobSummaryRepeaterDataItem.JobStatus;
                _view.JobSummaryRowLocation = _view.JobSummaryRepeaterDataItem.Location;
                _view.JobSummaryRowProjectManager = _view.JobSummaryRepeaterDataItem.ProjectManager;
                _view.JobSummaryRowModifiedBy = _view.JobSummaryRepeaterDataItem.ModifiedBy;
                _view.JobSummaryRowLastModification = _view.JobSummaryRepeaterDataItem.LastModification;
                _view.JobSummaryRowCallDate = _view.JobSummaryRepeaterDataItem.CallDate;
                _view.JobSummaryRowPresetDate = _view.JobSummaryRepeaterDataItem.PresetDate;
                _view.JobSummaryRowLastCallEntry = _view.JobSummaryRepeaterDataItem.LastCallType;
                _view.JobSummaryRowLastCallEntryId = _view.JobSummaryRepeaterDataItem.CallLogId;

                if (null == _callLogModel)
                    _callLogModel = new CallLogModel();
            
                _view.JobSummaryRowLastCallEntryIsAutomaticProcess = _callLogModel.GetCallTypeByDescription(_view.JobSummaryRepeaterDataItem.LastCallType).IsAutomaticProcess;

                _view.JobSummaryRowLastCallDate = _view.JobSummaryRepeaterDataItem.LastCallDate;
            }
        }

        /// <summary>
        /// Fills all fields of a Resource Row
        /// </summary>
        public void SetJobSummaryResourceRowData()
        {
            if (null != _view.JobSummaryResourceRepeaterDataItem)
            {
                _view.JobSummaryResourceRowDivision = _view.JobSummaryResourceRepeaterDataItem.Division;
                _view.JobSummaryResourceRowJobId = _view.JobSummaryResourceRepeaterDataItem.JobID;
                if (!string.IsNullOrEmpty(_view.JobSummaryResourceRepeaterDataItem.EmployeeName))
                    _view.JobSummaryResourceRowResource = _view.JobSummaryResourceRepeaterDataItem.EmployeeName;
                else
                    _view.JobSummaryResourceRowResource = _view.JobSummaryResourceRepeaterDataItem.EquipmentName;
                _view.JobSummaryResourceRowLocation = _view.JobSummaryResourceRepeaterDataItem.Location;
                _view.JobSummaryResourceRowModifiedBy = _view.JobSummaryResourceRepeaterDataItem.ModifiedBy;
                _view.JobSummaryResourceRowLastModification = _view.JobSummaryResourceRepeaterDataItem.ResouceLastModification;
                _view.JobSummaryResourceRowCallDate = _view.JobSummaryResourceRepeaterDataItem.ResourCallDate;
                _view.JobSummaryResourceRowLastCallEntry = _view.JobSummaryResourceRepeaterDataItem.LastCallType;
                _view.JobSummaryResourceRowLastCallEntryId = _view.JobSummaryResourceRepeaterDataItem.CallLogId;
                
                if (null == _callLogModel)
                    _callLogModel = new CallLogModel();

                    _view.JobSummaryResourceRowLastCallEntryIsAutomaticProcess = _callLogModel.GetCallTypeByDescription(_view.JobSummaryResourceRepeaterDataItem.LastCallType).IsAutomaticProcess;
                
                _view.JobSummaryResourceRowLastCallDate = _view.JobSummaryResourceRepeaterDataItem.LastCallDate;
            }
        }

        /// <summary>
        /// Creates the Job Summary CSV File to be exported
        /// </summary>
        public void CreateCSVJobSummary()
        {
            IList<CS_SP_GetJobSummary_Result> jobSummaryList = _view.JobSummaryDataSource;

            StringBuilder csv = new StringBuilder();
            StringBuilder csvLine = new StringBuilder();

            csv.AppendLine("DIV,Job #,Customer/Resources,Status,Location,Project Manager,Modified By,Last Mod.,Initial Call Date,Preset Date,Last Call Type,Last Call Date/Time,");

            for (int i = 0; i < jobSummaryList.Count; i++)
            {
                if (jobSummaryList[i].IsResource.Value)
                {
                    csvLine.Append(jobSummaryList[i].Division + ",");
                    csvLine.Append(",");

                    if (string.IsNullOrEmpty(jobSummaryList[i].EmployeeName))
                        csvLine.Append(jobSummaryList[i].EquipmentName + ",");
                    else
                        csvLine.Append(jobSummaryList[i].EmployeeName.Replace(",", "") + ",");

                    csvLine.Append(",");
                    csvLine.Append(jobSummaryList[i].Location.Replace(",", "-") + ",");
                    csvLine.Append(",");
                    csvLine.Append(jobSummaryList[i].ModifiedBy + ",");

                    if (jobSummaryList[i].ResouceLastModification.HasValue)
                        csvLine.Append(jobSummaryList[i].ResouceLastModification.Value.ToShortDateString() + ",");
                    else
                        csvLine.Append(",");

                    if (jobSummaryList[i].ResourCallDate.HasValue)
                        csvLine.Append(jobSummaryList[i].ResourCallDate.Value.ToShortDateString() + ",");
                    else
                        csvLine.Append(",");

                    csvLine.Append(",");

                    csvLine.Append(jobSummaryList[i].LastCallType + ",");

                    if (jobSummaryList[i].LastCallDate.HasValue)
                        csvLine.Append(jobSummaryList[i].LastCallDate.Value.ToString("MM/dd/yyyy hh:mm:ss") + ",");
                    else
                        csvLine.Append(",");
                }
                else
                {
                    csvLine.Append(jobSummaryList[i].Division + ",");
                    csvLine.Append(jobSummaryList[i].PrefixedNumber + ",");

                    csvLine.Append(jobSummaryList[i].Customer.Replace(",", "-") + ",");

                    csvLine.Append(jobSummaryList[i].JobStatus.Replace("–", "-") + ",");
                    csvLine.Append(jobSummaryList[i].Location.Replace(",", "-") + ",");
                    if (string.IsNullOrEmpty(jobSummaryList[i].ProjectManager))
                        csvLine.Append(",");
                    else
                        csvLine.Append(jobSummaryList[i].ProjectManager.Replace(",", "-") + ",");
                    csvLine.Append(jobSummaryList[i].ModifiedBy.Replace(",", "-") + ",");
                    csvLine.Append(jobSummaryList[i].LastModification.ToShortDateString() + ",");
                    csvLine.Append(jobSummaryList[i].CallDate.ToShortDateString() + ",");

                    if (jobSummaryList[i].PresetDate.HasValue)
                        csvLine.Append(jobSummaryList[i].PresetDate.Value.ToShortDateString() + ",");
                    else
                        csvLine.Append(",");

                    csvLine.Append(jobSummaryList[i].LastCallType + ",");

                    if (jobSummaryList[i].LastCallDate.HasValue)
                        csvLine.Append(jobSummaryList[i].LastCallDate.Value.ToString("MM/dd/yyyy hh:mm:ss") + ",");
                    else
                        csvLine.Append(",");
                }

                csv.AppendLine(csvLine.ToString());

                csvLine = new StringBuilder();
            }

            _view.CSVFile = csv.ToString();
        }

        #endregion

        #region [ Job Call Log ]

        /// <summary>
        /// Fills the Job Call Log Listing, based on view Filters
        /// </summary>
        public virtual void FilterJobCallLog()
        {
            _view.CalllogViewDataSource =
                _callLogModel.ListFilteredJobCallLogInfo(
                    _view.JobStatusCallLogViewFilter,
                    _view.CallTypeCallLogViewFilter,
                    _view.ModifiedByCallLogViewFilter,
                    _view.DivisionValueCallLogViewFilter,
                    _view.BeginDateCallLogViewFilter,
                    _view.EndDateCallLogViewFilter,
                    _view.ShiftTransferLogCallLogViewFilter,
                    _view.GeneralLogCallLogViewFilter,
                    _view.PersonNameCallLog) as List<CS_View_JobCallLog>;

            if (!_view.UserHasDeleteCallLogPermission.HasValue)
            {
                _view.UserHasDeleteCallLogPermission = false;

                AZManager manager = new AZManager();
                AZOperation[] operation = manager.CheckAccessById(_view.UserName, _view.Domain, new Globals.Security.Operations[] { Globals.Security.Operations.CallLogDelete });

                if (operation.Count() > 0)
                    _view.UserHasDeleteCallLogPermission = operation[0].Result;
            }

            if (_view.DivisionValueCallLogViewFilter.HasValue)
            {
                IList<CS_Division> lstResult = new List<CS_Division>();
                lstResult.Add(_divisionModel.GetDivision(_view.DivisionValueCallLogViewFilter.Value));
                _view.CallLogViewDivisionList = lstResult;
            }
            else
            {
                if (_view.GeneralLogCallLogViewFilter)
                {
                    List<CS_Division> divisionList = new List<CS_Division>();
                    divisionList.Add(_divisionModel.GetGeneralLogDivision());
                    _view.CallLogViewDivisionList = divisionList;
                }
                else
                {
                    IList<CS_Division> divisionList = _divisionModel.ListAllDivision();

                    if (_view.ShiftTransferLogCallLogViewFilter)
                        divisionList.Add(_divisionModel.GetGeneralLogDivision());

                    if (_view.JobCallLogSortColumn == Globals.Common.Sort.JobCallLogSortColumns.Division && _view.SortDirection == Globals.Common.Sort.SortDirection.Ascending)
                        _view.CallLogViewDivisionList = divisionList.OrderBy(e => e.Name).ToList();
                    else if (_view.JobCallLogSortColumn == Globals.Common.Sort.JobCallLogSortColumns.Division && _view.SortDirection == Globals.Common.Sort.SortDirection.Descending)
                        _view.CallLogViewDivisionList = divisionList.OrderByDescending(e => e.Name).ToList();
                    else
                        // Default Load
                        _view.CallLogViewDivisionList = divisionList.OrderBy(e => e.Name).ToList();
                }
            }
        }

        private List<CS_View_JobCallLog> SortJobCallLogJobList<TKey>(Func<CS_View_JobCallLog, TKey> keySelector, Globals.Common.Sort.SortDirection sortDirection, int divisionId)
        {
            switch (sortDirection)
            {
                case Globals.Common.Sort.SortDirection.Descending:
                    return _view.CalllogViewDataSource
                        .FindAll(e => e.DivisionId == divisionId)
                        .OrderByDescending(keySelector)
                        .Distinct(new Globals.Dashboard.CS_View_JobCallLog_Comparer())
                        .ToList();
                case Globals.Common.Sort.SortDirection.Ascending:
                default:
                    return _view.CalllogViewDataSource
                        .FindAll(e => e.DivisionId == divisionId)
                        .OrderBy(keySelector)
                        .Distinct(new Globals.Dashboard.CS_View_JobCallLog_Comparer())
                        .ToList();
            }
        }

        private List<CS_View_JobCallLog> SortJobCallLogCallEntryList<TKey>(Func<CS_View_JobCallLog, TKey> keySelector, Globals.Common.Sort.SortDirection sortDirection, int jobId)
        {
            switch (sortDirection)
            {
                case Globals.Common.Sort.SortDirection.Descending:
                    return _view.CalllogViewDataSource.FindAll(e => e.JobId == jobId).OrderByDescending(keySelector).ToList();
                case Globals.Common.Sort.SortDirection.Ascending:
                default:
                    return _view.CalllogViewDataSource.FindAll(e => e.JobId == jobId).OrderBy(keySelector).ToList();
            }
        }

        /// <summary>
        /// Fills the Job List of the Current Division
        /// </summary>
        public void GetCallLogViewJobList()
        {
            if (null != _view.DivisionRepeaterDataItem)
            {
                switch (_view.JobCallLogSortColumn)
                {
                    case Globals.Common.Sort.JobCallLogSortColumns.JobNumber:
                        _view.JobRepeaterDataSource = SortJobCallLogJobList(e => e.JobNumber, _view.SortDirection, _view.DivisionRepeaterDataItem.ID);
                        break;
                    case Globals.Common.Sort.JobCallLogSortColumns.Customer:
                        _view.JobRepeaterDataSource = SortJobCallLogJobList(e => e.Customer, _view.SortDirection, _view.DivisionRepeaterDataItem.ID);
                        break;
                    case Globals.Common.Sort.JobCallLogSortColumns.None:
                    case Globals.Common.Sort.JobCallLogSortColumns.Division:
                    case Globals.Common.Sort.JobCallLogSortColumns.CallType:
                    case Globals.Common.Sort.JobCallLogSortColumns.CalledInBy:
                    case Globals.Common.Sort.JobCallLogSortColumns.CallDate:
                    case Globals.Common.Sort.JobCallLogSortColumns.CallTime:
                    case Globals.Common.Sort.JobCallLogSortColumns.ModifiedBy:
                    case Globals.Common.Sort.JobCallLogSortColumns.Details:
                    default:
                        // Default Load
                        if (_view.ShiftTransferLogCallLogViewFilter || _view.GeneralLogCallLogViewFilter)
                            _view.JobRepeaterDataSource = _view.CalllogViewDataSource
                            .FindAll(e => e.DivisionId == _view.DivisionRepeaterDataItem.ID)
                            .OrderByDescending(e => e.JobCreationDate)
                            .Distinct(new Globals.Dashboard.CS_View_JobCallLog_Comparer())
                            .ToList();
                        else
                            _view.JobRepeaterDataSource = _view.CalllogViewDataSource
                                .FindAll(e => e.DivisionId == _view.DivisionRepeaterDataItem.ID)
                                .OrderBy(e => e.JobCreationDate)
                                .Distinct(new Globals.Dashboard.CS_View_JobCallLog_Comparer())
                                .ToList();
                        break;
                }
            }
        }

        /// <summary>
        /// Fills the Call Entry List of the Current Job
        /// </summary>
        public void GetCallLogViewCallEntryList()
        {
            if (null != _view.JobRepeaterDataItem)
            {
                switch (_view.JobCallLogSortColumn)
                {
                    case Globals.Common.Sort.JobCallLogSortColumns.CallType:
                        _view.CallLogRepeaterDataSource = SortJobCallLogCallEntryList<string>(e => e.CallType, _view.SortDirection, _view.JobRepeaterDataItem.JobId);
                        break;
                    case Globals.Common.Sort.JobCallLogSortColumns.CalledInBy:
                        _view.CallLogRepeaterDataSource = SortJobCallLogCallEntryList<string>(e => e.CalledInBy, _view.SortDirection, _view.JobRepeaterDataItem.JobId);
                        break;
                    case Globals.Common.Sort.JobCallLogSortColumns.CallDate:
                    case Globals.Common.Sort.JobCallLogSortColumns.CallTime:
                        _view.CallLogRepeaterDataSource = SortJobCallLogCallEntryList<DateTime>(e => e.CallDate, _view.SortDirection, _view.JobRepeaterDataItem.JobId);
                        break;
                    case Globals.Common.Sort.JobCallLogSortColumns.ModifiedBy:
                        _view.CallLogRepeaterDataSource = SortJobCallLogCallEntryList<string>(e => e.ModifiedBy, _view.SortDirection, _view.JobRepeaterDataItem.JobId);
                        break;
                    case Globals.Common.Sort.JobCallLogSortColumns.Details:
                        _view.CallLogRepeaterDataSource = SortJobCallLogCallEntryList<string>(e => e.Details, _view.SortDirection, _view.JobRepeaterDataItem.JobId);
                        break;
                    case Globals.Common.Sort.JobCallLogSortColumns.None:
                    case Globals.Common.Sort.JobCallLogSortColumns.Division:
                    case Globals.Common.Sort.JobCallLogSortColumns.JobNumber:
                    case Globals.Common.Sort.JobCallLogSortColumns.Customer:
                    default:
                        // Default Load
                        if (_view.ShiftTransferLogCallLogViewFilter || _view.GeneralLogCallLogViewFilter)
                            _view.CallLogRepeaterDataSource = _view.CalllogViewDataSource.FindAll(e => e.JobId == _view.JobRepeaterDataItem.JobId).OrderByDescending(e => e.CallDate).ToList();
                        else
                            _view.CallLogRepeaterDataSource = _view.CalllogViewDataSource.FindAll(e => e.JobId == _view.JobRepeaterDataItem.JobId).OrderBy(e => e.CallDate).ToList();
                        break;
                }
            }
        }

        /// <summary>
        /// Fills all fields of a Division Row
        /// </summary>
        public void SetJobCallLogDivisionRowData()
        {
            if (null != _view.DivisionRepeaterDataItem)
            {
                _view.DivisionRowName = _view.DivisionRepeaterDataItem.Name;
                _view.DivisionCount++;
            }
        }

        /// <summary>
        /// Fills all fields of a Job Row
        /// </summary>
        public void SetJobCallLogJobRowData()
        {
            if (null != _view.JobRepeaterDataItem)
            {
                _view.JobRowJobNumberCustomer = _view.JobRepeaterDataItem.PrefixedNumber + " - " + _view.JobRepeaterDataItem.Customer;

                _view.JobCount++;
            }
        }

        /// <summary>
        /// Fills all fields of a Call Entry Row
        /// </summary>
        public void SetCallLogViewCallEntryRowData()
        {
            if (null != _view.CallLogRepeaterDataItem)
            {
                _view.CallLogRowCallId = _view.CallLogRepeaterDataItem.CallId;
                _view.CallLogRowJobId = _view.CallLogRepeaterDataItem.JobId;
                _view.CallLogRowLastModification = _view.CallLogRepeaterDataItem.CallDate.ToString("MM/dd/yyyy HH:mm:ss");
                _view.CallLogRowCallType = _view.CallLogRepeaterDataItem.CallType;
                _view.CallLogRowCalledInBy = _view.CallLogRepeaterDataItem.CalledInBy;
                _view.CallLogRowCallDate = _view.CallLogRepeaterDataItem.CallDate.ToString("MM/dd/yyyy");
                _view.CallLogRowCallTime = _view.CallLogRepeaterDataItem.CallDate.ToString("HH:mm");
                _view.CallLogRowModifiedBy = _view.CallLogRepeaterDataItem.ModifiedBy;

                if (_view.CallLogRepeaterDataItem.IsAutomaticProcess.HasValue)
                    _view.CallLogRowAutomaticProcess = _view.CallLogRepeaterDataItem.IsAutomaticProcess.Value;
                else
                    _view.CallLogRowAutomaticProcess = false;

                if (!string.IsNullOrEmpty(_view.CallLogRepeaterDataItem.Details))
                    _view.CallLogRowDetails = StringManipulation.TabulateString(_view.CallLogRepeaterDataItem.Details);

                if (_view.UserHasDeleteCallLogPermission.HasValue)
                    _view.EnableDeleteLink = _view.UserHasDeleteCallLogPermission.Value;

                _view.CallLogCount++;
            }
        }

        /// <summary>
        /// Sets the Call Entry Row as Read/Unread based on the Read List and Last Modification Date, and updates the Read List if necessary
        /// </summary>
        public void SetCallLogViewCallEntryRowReadUnread()
        {
            if (null != _view.CallLogRepeaterDataItem)
            {
                DateTime lastModification;
                if (_view.CallLogRepeaterDataItem.ModifiedBy != _view.UserName)
                {
                    if (_view.ReadItems.TryGetValue(_view.CallLogRepeaterDataItem.CallId, out lastModification))
                    {
                        DateTime callDate = new DateTime(
                            _view.CallLogRepeaterDataItem.CallDate.Year,
                            _view.CallLogRepeaterDataItem.CallDate.Month,
                            _view.CallLogRepeaterDataItem.CallDate.Day,
                            _view.CallLogRepeaterDataItem.CallDate.Hour,
                            _view.CallLogRepeaterDataItem.CallDate.Minute,
                            _view.CallLogRepeaterDataItem.CallDate.Second);
                        if (callDate > lastModification)
                        {
                            _view.ReadItems.Remove(_view.CallLogRepeaterDataItem.CallId);
                            _view.CallLogRowStyle = "Unread";
                        }
                        else
                            _view.CallLogRowStyle = "Read";
                    }
                    else
                        _view.CallLogRowStyle = "Unread";
                }
                else
                    _view.CallLogRowStyle = "Read";
            }
        }

        /// <summary>
        /// Uses the NewlyReadItems string to Update ReadItems List
        /// </summary>
        public void UpdateReadItems()
        {
            if (!string.IsNullOrEmpty(_view.NewlyReadItems))
            {
                string[] splitNewlyReadItems = _view.NewlyReadItems.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < splitNewlyReadItems.Length; i++)
                {
                    string[] splitIdAndDate = splitNewlyReadItems[i].Split(',');
                    DateTime tempDateTime;
                    if ((splitIdAndDate[0] != "Delete" && splitIdAndDate[1] != "On") && !_view.ReadItems.TryGetValue(Convert.ToInt32(splitIdAndDate[0]), out tempDateTime))
                    {
                        _view.ReadItems.Add(
                            Convert.ToInt32(splitIdAndDate[0]),
                            Convert.ToDateTime(splitIdAndDate[1], new System.Globalization.CultureInfo("en-US")));
                    }
                }
            }

            _view.NewlyReadItems = string.Empty;
        }

        /// <summary>
        /// Creates the Job Call Log CSV File to be exported
        /// </summary>
        public void CreateCSVCallLog()
        {
            IList<CS_SP_GetJobSummary_Result> jobSummaryList = _view.JobSummaryDataSource;
            List<CS_Division> divisionList = _view.CallLogViewDivisionList as List<CS_Division>;
            List<CS_View_JobCallLog> callLogList = _view.CalllogViewDataSource;

            StringBuilder csv = new StringBuilder();
            StringBuilder csvLine = new StringBuilder();

            csv.AppendLine("DIV,Job #,Company,Call Type,Called In By,Call Date,Call Time,Modified By,Details,");

            for (int i = 0; i < divisionList.Count; i++)
            {
                csvLine = new StringBuilder();

                csvLine.Append(divisionList[i].Name.Trim() + ",");
                csvLine.Append(",,,,,,,,");

                csv.AppendLine(csvLine.ToString());

                List<CS_View_JobCallLog> lstJobs = new List<CS_View_JobCallLog>();

                switch (_view.JobCallLogSortColumn)
                {
                    case Globals.Common.Sort.JobCallLogSortColumns.JobNumber:
                        lstJobs.AddRange(SortJobCallLogJobList(e => e.JobNumber, _view.SortDirection, divisionList[i].ID));
                        break;
                    case Globals.Common.Sort.JobCallLogSortColumns.Customer:
                        lstJobs.AddRange(SortJobCallLogJobList(e => e.Customer, _view.SortDirection, divisionList[i].ID));
                        break;
                    case Globals.Common.Sort.JobCallLogSortColumns.None:
                    case Globals.Common.Sort.JobCallLogSortColumns.Division:
                    case Globals.Common.Sort.JobCallLogSortColumns.CallType:
                    case Globals.Common.Sort.JobCallLogSortColumns.CalledInBy:
                    case Globals.Common.Sort.JobCallLogSortColumns.CallDate:
                    case Globals.Common.Sort.JobCallLogSortColumns.CallTime:
                    case Globals.Common.Sort.JobCallLogSortColumns.ModifiedBy:
                    case Globals.Common.Sort.JobCallLogSortColumns.Details:
                    default:
                        // Default Load
                        lstJobs.AddRange(callLogList.FindAll(e => e.DivisionId == divisionList[i].ID).Distinct(new Globals.Dashboard.CS_View_JobCallLog_Comparer()));
                        break;
                }

                for (int j = 0; j < lstJobs.Count; j++)
                {
                    csvLine = new StringBuilder();

                    csvLine.Append("," + lstJobs[j].PrefixedNumber.Trim() + ",");
                    csvLine.Append(lstJobs[j].Customer.Trim() + ",");
                    csvLine.Append(",,,,,,");

                    csv.AppendLine(csvLine.ToString());

                    List<CS_View_JobCallLog> lstCallLog = new List<CS_View_JobCallLog>();

                    switch (_view.JobCallLogSortColumn)
                    {
                        case Globals.Common.Sort.JobCallLogSortColumns.CallType:
                            lstCallLog.AddRange(SortJobCallLogCallEntryList<string>(e => e.CallType, _view.SortDirection, lstJobs[j].JobId));
                            break;
                        case Globals.Common.Sort.JobCallLogSortColumns.CalledInBy:
                            lstCallLog.AddRange(SortJobCallLogCallEntryList<string>(e => e.CalledInBy, _view.SortDirection, lstJobs[j].JobId));
                            break;
                        case Globals.Common.Sort.JobCallLogSortColumns.CallDate:
                        case Globals.Common.Sort.JobCallLogSortColumns.CallTime:
                            lstCallLog.AddRange(SortJobCallLogCallEntryList<DateTime>(e => e.CallDate, _view.SortDirection, lstJobs[j].JobId));
                            break;
                        case Globals.Common.Sort.JobCallLogSortColumns.ModifiedBy:
                            lstCallLog.AddRange(SortJobCallLogCallEntryList<string>(e => e.ModifiedBy, _view.SortDirection, lstJobs[j].JobId));
                            break;
                        case Globals.Common.Sort.JobCallLogSortColumns.Details:
                            lstCallLog.AddRange(SortJobCallLogCallEntryList<string>(e => e.Details, _view.SortDirection, lstJobs[j].JobId));
                            break;
                        case Globals.Common.Sort.JobCallLogSortColumns.None:
                        case Globals.Common.Sort.JobCallLogSortColumns.Division:
                        case Globals.Common.Sort.JobCallLogSortColumns.JobNumber:
                        case Globals.Common.Sort.JobCallLogSortColumns.Customer:
                        default:
                            // Default Load
                            lstCallLog.AddRange(callLogList.FindAll(e => e.JobId == lstJobs[j].JobId));
                            break;
                    }

                    for (int k = 0; k < lstCallLog.Count; k++)
                    {
                        csvLine = new StringBuilder();

                        csvLine.Append(",,,");
                        csvLine.Append(lstCallLog[k].CallType.Trim() + ",");
                        csvLine.Append(lstCallLog[k].CalledInBy + ",");
                        csvLine.Append(lstCallLog[k].CallDate.ToShortDateString() + ",");
                        csvLine.Append(lstCallLog[k].CallDate.ToShortTimeString() + ",");
                        csvLine.Append(lstCallLog[k].ModifiedBy.Trim() + ",");
                        if (!string.IsNullOrEmpty(lstCallLog[k].Details))
                            csvLine.Append(lstCallLog[k].Details.Replace("\r\n", "").Replace("<BL>", " ").Replace("<Text>", " ").Replace("–", "-").Replace("<RED>", "").Replace(",", "").Replace("\"", ""));
                        else
                            csvLine.Append(",");

                        csv.AppendLine(csvLine.ToString());
                    }
                }
            }

            _view.CSVFile = csv.ToString();
        }

        #endregion

        #endregion

        #region [ IDisposable Implementation ]

        public void Dispose()
        {
            if (null != _jobModel)
                _jobModel.Dispose();

            if (null != _callLogModel)
                _callLogModel.Dispose();

            if (null != _divisionModel)
                _divisionModel.Dispose();

            _jobModel = null;
            _callLogModel = null;
            _divisionModel = null;
        }

        #endregion
    }
}
