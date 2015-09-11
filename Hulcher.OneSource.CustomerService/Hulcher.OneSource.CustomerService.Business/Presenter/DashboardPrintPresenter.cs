using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.Business.ViewModel;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Core.Security;
using Hulcher.OneSource.CustomerService.Core.Utils;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;

using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    public class DashboardPrintPresenter
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of the Dashboard View Interface
        /// </summary>
        private IDashboardPrintView _view;

        /// <summary>
        /// Instance of the Job Model
        /// </summary>
        private JobModel _jobModel;

        /// <summary>
        /// Instance of the Division Model
        /// </summary>
        private DivisionModel _divisionModel;

        /// <summary>
        /// Instance of the Customer Model
        /// </summary>
        private CustomerModel _customerModel;

        /// <summary>
        /// Instalce of the Call Log Model
        /// </summary>
        private CallLogModel _callLogModel;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="view">Instance of the View</param>
        public DashboardPrintPresenter(IDashboardPrintView view)
        {
            this._view = view;
            this._jobModel = new JobModel();
            this._divisionModel = new DivisionModel();
            this._customerModel = new CustomerModel();
            this._callLogModel = new CallLogModel();
        }

        #endregion

        #region [ Methods ]

        #region [ Job Call Log - Viewpoint 1 ]

        /// <summary>
        /// Loads Job Call Log Results
        /// </summary>
        public void LoadJobCallLog()
        {
            try
            {
                _view.CalllogViewDataSource =
                    _callLogModel.ListFilteredJobCallLogInfo(
                        _view.JobStatusFilterValue,
                        _view.CallTypeFilterValue,
                        _view.ModifiedByFilterValue,
                        _view.DivisionFilterValue,
                        _view.StartDateFilterValue,
                        _view.EndDateFilterValue,
                        _view.ShiftTransferLogFilter,
                        _view.GeneralLogFilter,
                        _view.PersonNameFilterValue) as List<CS_View_JobCallLog>;

                if (_view.DivisionFilterValue.HasValue)
                {
                    IList<CS_Division> lstResult = new List<CS_Division>();
                    lstResult.Add(_divisionModel.GetDivision(_view.DivisionFilterValue.Value));
                    _view.CallLogViewDivisionList = lstResult;
                }
                else
                {
                    if (_view.GeneralLogFilter)
                    {
                        List<CS_Division> divisionList = new List<CS_Division>();
                        divisionList.Add(_divisionModel.GetGeneralLogDivision());
                        _view.CallLogViewDivisionList = divisionList;
                    }
                    else
                    {
                        IList<CS_Division> divisionList = _divisionModel.ListAllDivision();

                        if (_view.ShiftTransferLogFilter)
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
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Get Job Call Log Division list!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to generate the Print view.", true);
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
                        if (_view.ShiftTransferLogFilter || _view.GeneralLogFilter)
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
                        if (_view.ShiftTransferLogFilter || _view.GeneralLogFilter)
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
            }
        }

        /// <summary>
        /// Fills a row of a call log data
        /// </summary>
        public void SetCallLogViewCallLogRowData()
        {
            try
            {
                if (null != _view.CallLogRepeaterDataItem)
                {
                    _view.CallLogRowCallId = _view.CallLogRepeaterDataItem.CallId;
                    _view.CallLogRowLastModification = _view.CallLogRepeaterDataItem.CallDate.ToString("MM/dd/yyyy HH:mm:ss");
                    _view.CallLogRowCallType = _view.CallLogRepeaterDataItem.CallType;
                    _view.CallLogRowCalledInBy = _view.CallLogRepeaterDataItem.CalledInBy;
                    _view.CallLogRowCallDate = _view.CallLogRepeaterDataItem.CallDate.ToShortDateString();
                    _view.CallLogRowCallTime = _view.CallLogRepeaterDataItem.CallDate.ToShortTimeString();
                    _view.CallLogRowModifiedBy = _view.CallLogRepeaterDataItem.ModifiedBy;
                    if (!string.IsNullOrEmpty(_view.CallLogRepeaterDataItem.Details))
                        _view.CallLogRowDetails = StringManipulation.TabulateString(_view.CallLogRepeaterDataItem.Details);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Set Job Call Log Row Data!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to generate the Print view.", true);
            }
        }

        /// <summary>
        /// Loads the Job Status filter into a label
        /// </summary>
        public void LoadJobStatusCallLogLabel()
        {
            try
            {
                if (_view.JobStatusFilterValue.HasValue)
                    _view.JobStatusCallLogLabelText = _jobModel.GetJobStatus(_view.JobStatusFilterValue.Value).Description;
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to load the Job Status Label information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to generate the Print view.", true);
            }
        }

        /// <summary>
        /// Loads the Call Type filter into a label
        /// </summary>
        public void LoadCallTypeCallLogLabel()
        {
            try
            {
                if (_view.CallTypeFilterValue.HasValue)
                    _view.CallTypeCallLogLabelText = _callLogModel.GetCallType(_view.CallTypeFilterValue.Value).Description;
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to load the Job Status Label information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to generate the Print view.", true);
            }
        }

        /// <summary>
        /// Loads the Division filter into a label
        /// </summary>
        public void LoadDivisionCallLogLabel()
        {
            try
            {
                if (_view.DivisionFilterValue.HasValue)
                    _view.DivisionCallLogLabelText = _divisionModel.GetDivision(_view.DivisionFilterValue.Value).Name;
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to load the Job Status Label information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to generate the Print view.", true);
            }
        }

        #endregion

        #region [ Job Summary - Viewpoint 2 ]

        /// <summary>
        /// Loads Job Summary results
        /// </summary>
        public void LoadJobSummary()
        {
            try
            {
                _view.JobSummaryDataSource =
                    _jobModel.FindJobSummary(
                        _view.JobStatusFilterValue,
                        _view.JobNumberFilterValue,
                        _view.DivisionFilterValue,
                        _view.CustomerFilterValue,
                        _view.DateFilterTypeValue,
                        _view.StartDateFilterValue,
                        _view.EndDateFilterValue,
                        _view.PersonNameFilterValue).ToList();

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
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Find Job Summary information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to generate the Print view.", true);
            }
        }

        /// <summary>
        /// Fills a Job Summary Row
        /// </summary>
        public void FillJobSummaryRow()
        {
            try
            {
                GetJobSummaryResourceList();
                SetJobSummaryRowData();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Set Job Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Set Job Information.", false);
            }
        }

        /// <summary>
        /// Fills a Job Summary Resource Row
        /// </summary>
        public void FillJobSummaryResourceRow()
        {
            try
            {
                SetJobSummaryResourceRowData();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to set the Resource Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to set the Resource Information.", false);
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
                _view.JobSummaryResourceRowLastCallDate = _view.JobSummaryResourceRepeaterDataItem.LastCallDate;
            }
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
        /// Loads Job Status filter into a label
        /// </summary>
        public void LoadJobStatusLabel()
        {
            try
            {
                _view.JobStatusLabelText = _jobModel.GetJobStatus(_view.JobStatusFilterValue.Value).Description;
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to load the Job Status Label information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to generate the Print view.", true);
            }
        }

        /// <summary>
        /// Loads Job Number filter into a label
        /// </summary>
        public void LoadJobNumberLabel()
        {
            try
            {
                CS_Job job = _jobModel.GetJobById(_view.JobNumberFilterValue.Value);
                _view.JobNumberLabelText = job.CS_JobInfo.LastJobStatus.Description == "Active" ? job.Number : job.Internal_Tracking;
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to load the Job Status Label information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to generate the Print view.", true);
            }
        }

        /// <summary>
        /// Loads Division filter into a label
        /// </summary>
        public void LoadDivisionLabel()
        {
            try
            {
                _view.DivisionLabelText = _divisionModel.GetDivision(_view.DivisionFilterValue.Value).Name;
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to load the Job Status Label information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to generate the Print view.", true);
            }
        }

        /// <summary>
        /// Loads Customer filter into a label
        /// </summary>
        public void LoadCustomerLabel()
        {
            try
            {
                _view.CustomerLabelText = _customerModel.GetCustomerById(_view.CustomerFilterValue.Value).Name;
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to load the Job Status Label information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to generate the Print view.", true);
            }
        }

        public void LoadPersonNameLabel()
        {
            try
            {
                _view.PersonNameLabelText = _view.PersonNameFilterValue;
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to load the Job Status Label information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to generate the Print view.", true);
            }
        }

        #endregion

        #region [ Search Results - Viewpoint 3 ]

        /// <summary>
        /// Loads Search Results
        /// </summary>
        public void LoadSearchResults()
        {
            try
            {
                _view.JobSummarySearchDataSource = _jobModel.FindJobSummary(_view.JobIdList).ToList();

                switch (_view.JobSummarySortColumn)
                {
                    case Globals.Common.Sort.JobSummarySortColumns.Division:
                        _view.JobSummarySearchRepeaterDataSource = SortJobSummarySearchList<string>(e => e.Division, _view.SortDirection, false, null);
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.JobNumber:
                        _view.JobSummarySearchRepeaterDataSource = SortJobSummarySearchList<string>(e => e.JobNumber, _view.SortDirection, false, null);
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.CustomerResource:
                        _view.JobSummarySearchRepeaterDataSource = SortJobSummarySearchList<string>(e => e.Customer, _view.SortDirection, false, null);
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.JobStatus:
                        _view.JobSummarySearchRepeaterDataSource = SortJobSummarySearchList<string>(e => e.JobStatus, _view.SortDirection, false, null);
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.Location:
                        _view.JobSummarySearchRepeaterDataSource = SortJobSummarySearchList<string>(e => e.Location, _view.SortDirection, false, null);
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.ProjectManager:
                        _view.JobSummarySearchRepeaterDataSource = SortJobSummarySearchList<string>(e => e.ProjectManager, _view.SortDirection, false, null);
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.ModifiedBy:
                        _view.JobSummarySearchRepeaterDataSource = SortJobSummarySearchList<string>(e => e.ModifiedBy, _view.SortDirection, false, null);
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.LastModification:
                        _view.JobSummarySearchRepeaterDataSource = SortJobSummarySearchList<DateTime>(e => e.LastModification, _view.SortDirection, false, null);
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.InitialCallDate:
                        _view.JobSummarySearchRepeaterDataSource = SortJobSummarySearchList<DateTime>(e => e.CallDate, _view.SortDirection, false, null);
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.PresetDate:
                        _view.JobSummarySearchRepeaterDataSource = SortJobSummarySearchList<DateTime?>(e => e.PresetDate, _view.SortDirection, false, null);
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.LastCallType:
                        _view.JobSummarySearchRepeaterDataSource = SortJobSummarySearchList<string>(e => e.LastCallType, _view.SortDirection, false, null);
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.LastCallDate:
                        _view.JobSummarySearchRepeaterDataSource = SortJobSummarySearchList<DateTime?>(e => e.LastCallDate, _view.SortDirection, false, null);
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.None:
                    default:
                        _view.JobSummarySearchRepeaterDataSource = _view.JobSummarySearchDataSource.FindAll(w => w.IsResource == false);
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Find Job Summary Search Results information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to generate the Print view.", true);
            }
        }

        /// <summary>
        /// Loads the Search Filter Panel with selected filter values
        /// </summary>
        public void LoadSearchFilterPanel()
        {
            try
            {
                string contactFilter = _view.ContactFilter;
                string contactFilterValue = string.Empty;
                if (!string.IsNullOrEmpty(_view.ContactFilterValue))
                    contactFilterValue = string.Format(" - {0}", _view.ContactFilterValue);
                _view.SearchContactInfoLabel = string.Format("Contact Info: {0}{1}", contactFilter, contactFilterValue);

                string jobFilter = _view.JobFilter;
                string jobFilterValue = string.Empty;
                if (!string.IsNullOrEmpty(_view.JobFilterValue))
                    jobFilterValue = string.Format(" - {0}", _view.JobFilterValue);
                _view.SearchJobInfoLabel = string.Format("Job Info: {0}{1}", jobFilter, jobFilterValue);

                string locationFilter = _view.LocationFilter;
                string locationFilterValue = string.Empty;
                if (!string.IsNullOrEmpty(_view.LocationFilterValue))
                    locationFilterValue = string.Format(" - {0}", _view.LocationFilterValue);
                _view.SearchLocationInfoLabel = string.Format("Location Info: {0}{1}", locationFilter, locationFilterValue);

                string jobDescriptionFilter = _view.JobDescriptionFilter;
                string jobDescriptionFilterValue = string.Empty;
                if (!string.IsNullOrEmpty(_view.JobDescriptionFilterValue))
                    jobDescriptionFilterValue = string.Format(" - {0}", _view.JobDescriptionFilterValue);
                _view.SearchJobDescriptionLabel = string.Format("Job Description: {0}{1}", jobDescriptionFilter, jobDescriptionFilterValue);

                string equipmentTypeFilter = _view.EquipmentTypeFilter;
                string equipmentTypeFilterValue = string.Empty;
                if (!string.IsNullOrEmpty(_view.EquipmentTypeFilterValue))
                    equipmentTypeFilterValue = string.Format(" - {0}", _view.EquipmentTypeFilterValue);
                _view.SearchEquipmentTypeLabel = string.Format("Equipment Type: {0}{1}", equipmentTypeFilter, equipmentTypeFilterValue);

                string resourceFilter = _view.ResourceFilter;
                string resourceFilterValue = string.Empty;
                if (!string.IsNullOrEmpty(_view.ResourceFilterValue))
                    resourceFilterValue = string.Format(" - {0}", _view.ResourceFilterValue);
                _view.SearchResourceLabel = string.Format("Resource: {0}{1}", resourceFilter, resourceFilterValue);

                _view.SearchDateRangeLabel = string.Format("Date Range: from {0} to {1}", _view.DateRangeBeginValue.ToString("MM/dd/yyyy"), _view.DateRangeEndValue.ToString("MM/dd/yyyy"));
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Load Search Filter panel information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to generate the Print view.", true);
            }
        }

        /// <summary>
        /// Fills a Job Summary Row
        /// </summary>
        public void FillJobSummarySearchRow()
        {
            try
            {
                GetJobSummarySearchResourceList();
                SetJobSummarySearchRowData();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Set Job Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Set Job Information.", false);
            }
        }

        /// <summary>
        /// Fills a Job Summary Resource Row
        /// </summary>
        public void FillJobSummarySearchResourceRow()
        {
            try
            {
                SetJobSummarySearchResourceRowData();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to set the Resource Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to set the Resource Information.", false);
            }
        }

        /// <summary>
        /// Fills the Resource List of the Current Job 
        /// </summary>
        public void GetJobSummarySearchResourceList()
        {
            if (null != _view.JobSummarySearchRepeaterDataItem)
            {
                switch (_view.JobSummarySortColumn)
                {
                    case Globals.Common.Sort.JobSummarySortColumns.Division:
                        _view.JobSummarySearchResourceRepeaterDataSource = SortJobSummarySearchList<string>(e => e.Division, _view.SortDirection, true, _view.JobSummarySearchRepeaterDataItem.JobID);
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.CustomerResource:
                        switch (_view.SortDirection)
                        {
                            case Globals.Common.Sort.SortDirection.Descending:
                                _view.JobSummarySearchResourceRepeaterDataSource = _view.JobSummarySearchDataSource.FindAll(e => e.JobID == _view.JobSummarySearchRepeaterDataItem.JobID && e.IsResource == true).OrderByDescending(e => e.EquipmentName).ThenByDescending(e => e.EmployeeName).ToList();
                                break;
                            case Globals.Common.Sort.SortDirection.Ascending:
                            default:
                                _view.JobSummarySearchResourceRepeaterDataSource = _view.JobSummarySearchDataSource.FindAll(e => e.JobID == _view.JobSummarySearchRepeaterDataItem.JobID && e.IsResource == true).OrderBy(e => e.EquipmentName).ThenBy(e => e.EmployeeName).ToList();
                                break;
                        }
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.Location:
                        _view.JobSummarySearchResourceRepeaterDataSource = SortJobSummarySearchList<string>(e => e.Location, _view.SortDirection, true, _view.JobSummarySearchRepeaterDataItem.JobID);
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.ModifiedBy:
                        _view.JobSummarySearchResourceRepeaterDataSource = SortJobSummarySearchList<string>(e => e.ModifiedBy, _view.SortDirection, true, _view.JobSummarySearchRepeaterDataItem.JobID);
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.LastModification:
                        _view.JobSummarySearchResourceRepeaterDataSource = SortJobSummarySearchList<DateTime?>(e => e.ResouceLastModification, _view.SortDirection, true, _view.JobSummarySearchRepeaterDataItem.JobID);
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.InitialCallDate:
                        _view.JobSummarySearchResourceRepeaterDataSource = SortJobSummarySearchList<DateTime?>(e => e.ResourCallDate, _view.SortDirection, true, _view.JobSummarySearchRepeaterDataItem.JobID);
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.LastCallType:
                        _view.JobSummarySearchResourceRepeaterDataSource = SortJobSummarySearchList<string>(e => e.LastCallType, _view.SortDirection, true, _view.JobSummarySearchRepeaterDataItem.JobID);
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.LastCallDate:
                        _view.JobSummarySearchResourceRepeaterDataSource = SortJobSummarySearchList<DateTime?>(e => e.LastCallDate, _view.SortDirection, true, _view.JobSummarySearchRepeaterDataItem.JobID);
                        break;
                    case Globals.Common.Sort.JobSummarySortColumns.None:
                    case Globals.Common.Sort.JobSummarySortColumns.JobNumber:
                    case Globals.Common.Sort.JobSummarySortColumns.JobStatus:
                    case Globals.Common.Sort.JobSummarySortColumns.ProjectManager:
                    case Globals.Common.Sort.JobSummarySortColumns.PresetDate:
                    default:
                        _view.JobSummarySearchResourceRepeaterDataSource = _view.JobSummarySearchDataSource.FindAll(e => e.JobID == _view.JobSummarySearchRepeaterDataItem.JobID && e.IsResource == true);
                        break;
                }
            }
        }

        /// <summary>
        /// Sorts Job Summary Data Source
        /// </summary>
        /// <param name="keySelector">Lambda expression to select sort column</param>
        /// <param name="sortDirection">Sort direction</param>
        /// <param name="isResoruce">Filter by IsResource field (true/false)</param>
        /// <param name="jobId">If listing resources, filter only resources of this specific Job Identifier</param>
        /// <returns>Sorted Job Summary List</returns>
        private List<CS_View_JobSummary> SortJobSummarySearchList<TKey>(Func<CS_View_JobSummary, TKey> keySelector, Globals.Common.Sort.SortDirection sortDirection, bool isResoruce, int? jobId)
        {
            switch (sortDirection)
            {
                case Globals.Common.Sort.SortDirection.Descending:
                    if (isResoruce)
                        return _view.JobSummarySearchDataSource.FindAll(e => e.JobID == jobId.Value && e.IsResource == true).OrderByDescending(keySelector).ToList();
                    else
                        return _view.JobSummarySearchDataSource.FindAll(e => e.IsResource == false).OrderByDescending(keySelector).ToList();
                case Globals.Common.Sort.SortDirection.Ascending:
                default:
                    if (isResoruce)
                        return _view.JobSummarySearchDataSource.FindAll(e => e.JobID == jobId.Value && e.IsResource == true).OrderBy(keySelector).ToList();
                    else
                        return _view.JobSummarySearchDataSource.FindAll(e => e.IsResource == false).OrderBy(keySelector).ToList();
            }
        }

        /// <summary>
        /// Fills all fields of a Job Row
        /// </summary>
        public void SetJobSummarySearchRowData()
        {
            if (null != _view.JobSummarySearchRepeaterDataItem)
            {
                _view.JobSummarySearchRowDivision = _view.JobSummarySearchRepeaterDataItem.Division;
                _view.JobSummarySearchRowJobNumber = _view.JobSummarySearchRepeaterDataItem.PrefixedNumber;
                _view.JobSummarySearchRowCustomer = _view.JobSummarySearchRepeaterDataItem.Customer;
                _view.JobSummarySearchRowStatus = _view.JobSummarySearchRepeaterDataItem.JobStatus;
                _view.JobSummarySearchRowLocation = _view.JobSummarySearchRepeaterDataItem.Location;
                _view.JobSummarySearchRowProjectManager = _view.JobSummarySearchRepeaterDataItem.ProjectManager;
                _view.JobSummarySearchRowModifiedBy = _view.JobSummarySearchRepeaterDataItem.ModifiedBy;
                _view.JobSummarySearchRowLastModification = _view.JobSummarySearchRepeaterDataItem.LastModification;
                _view.JobSummarySearchRowCallDate = _view.JobSummarySearchRepeaterDataItem.CallDate;
                _view.JobSummarySearchRowPresetDate = _view.JobSummarySearchRepeaterDataItem.PresetDate;
                _view.JobSummarySearchRowLastCallEntry = _view.JobSummarySearchRepeaterDataItem.LastCallType;
                _view.JobSummarySearchRowLastCallDate = _view.JobSummarySearchRepeaterDataItem.LastCallDate;
            }
        }

        /// <summary>
        /// Fills all fields of a Resource Row
        /// </summary>
        public void SetJobSummarySearchResourceRowData()
        {
            if (null != _view.JobSummarySearchResourceRepeaterDataItem)
            {
                _view.JobSummarySearchResourceRowDivision = _view.JobSummarySearchResourceRepeaterDataItem.Division;
                if (!string.IsNullOrEmpty(_view.JobSummarySearchResourceRepeaterDataItem.EmployeeName))
                    _view.JobSummarySearchResourceRowResource = _view.JobSummarySearchResourceRepeaterDataItem.EmployeeName;
                else
                    _view.JobSummarySearchResourceRowResource = _view.JobSummarySearchResourceRepeaterDataItem.EquipmentName;
                _view.JobSummarySearchResourceRowLocation = _view.JobSummarySearchResourceRepeaterDataItem.Location;
                _view.JobSummarySearchResourceRowModifiedBy = _view.JobSummarySearchResourceRepeaterDataItem.ModifiedBy;
                _view.JobSummarySearchResourceRowLastModification = _view.JobSummarySearchResourceRepeaterDataItem.ResouceLastModification;
                _view.JobSummarySearchResourceRowCallDate = _view.JobSummarySearchResourceRepeaterDataItem.ResourCallDate;
                _view.JobSummarySearchResourceRowLastCallEntry = _view.JobSummarySearchResourceRepeaterDataItem.LastCallType;
                _view.JobSummarySearchResourceRowLastCallDate = _view.JobSummarySearchResourceRepeaterDataItem.LastCallDate;
            }
        }

        #endregion

        #endregion
    }
}
