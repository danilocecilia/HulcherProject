using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.Core;

namespace Hulcher.OneSource.CustomerService.Business.ViewModel
{
    public class DashboardSearchViewModel : IDisposable
    {
        #region [ Attributes ]

        private IDashboardSearchView _view;
        private JobModel _jobModel;
        private CallLogModel _callLogModel;

        #endregion

        #region [ Constructor ]

        public DashboardSearchViewModel(IDashboardSearchView view)
        {
            _view = view;
            _jobModel = new JobModel();
            _callLogModel = new CallLogModel();
        }

        #endregion

        #region [ Methods ]

        public void LoadFilterPanel()
        {
            StringBuilder innerHtml = new StringBuilder();
            innerHtml.Append("<div>");

            if (!string.IsNullOrEmpty(_view.ContactFilterValue))
            {
                innerHtml.AppendFormat("<div class='box' style='float: left; display: inline-block;'><b>Contact Info - </b>{0}:{1}", _view.ContactFilter, _view.ContactFilterValue);
                innerHtml.Append("      </div>");
            }
            if (!string.IsNullOrEmpty(_view.JobFilterValue))
            {
                innerHtml.AppendFormat("<div class='box' style='float: left; display: inline-block;'><b>Job Info - </b>{0}:{1}", _view.JobFilter, _view.JobFilterValue);
                innerHtml.Append("      </div>");
            }
            if (!string.IsNullOrEmpty(_view.LocationFilterValue))
            {
                innerHtml.AppendFormat("<div class='box' style='float: left; display: inline-block;'><b>Location Info - </b>{0}:{1}", _view.LocationFilter, _view.LocationFilterValue);
                innerHtml.Append("      </div>");
            }

            if (!string.IsNullOrEmpty(_view.JobDescriptionFilterValue))
            {
                innerHtml.AppendFormat("<div class='box' style='float: left; display: inline-block;'><b>Job Description Info - </b>{0}:{1}", _view.JobDescriptionFilter, _view.JobDescriptionFilterValue);
                innerHtml.Append("      </div>");
            }

            if (!string.IsNullOrEmpty(_view.EquipmentTypeFilterValue))
            {
                innerHtml.AppendFormat("<div class='box' style='float: left; display: inline-block;'><b>Equipment Type  Info - </b>{0}:{1}", _view.EquipmentTypeFilter, _view.EquipmentTypeFilterValue);
                innerHtml.Append("      </div>");
            }

            if (!string.IsNullOrEmpty(_view.ResourceFilterValue))
            {
                innerHtml.AppendFormat("<div class='box' style='float: left; display: inline-block;'><b>Resource  Info - </b>{0}:{1}", _view.ResourceFilter, _view.ResourceFilterValue);
                innerHtml.Append("      </div>");
            }

            if (!string.IsNullOrEmpty(_view.DateRangeBeginValue.ToShortDateString()) && !string.IsNullOrEmpty(_view.DateRangeEndValue.ToShortDateString()))
            {
                innerHtml.AppendFormat("<div class='box' style='float: left; display: inline-block;'><b>Date Range  Info - </b>From {0} To {1}", _view.DateRangeBeginValue.ToString("MM/dd/yyyy"), _view.DateRangeEndValue.ToString("MM/dd/yyyy"));
                innerHtml.Append("      </div>");
            }

            innerHtml.Append("</div>");

            _view.FilterPanel = innerHtml.ToString();
        }

        public void LoadFilterData()
        {
            _view.JobSummaryDataSource = _jobModel.FindJobSummary(_view.JobIdList).ToList();

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
        /// Creates the Job Summary CSV File to be exported
        /// </summary>
        public void CreateCSVJobSummary()
        {
            IList<CS_View_JobSummary> jobSummaryList = _view.JobSummaryDataSource;

            StringBuilder csv = new StringBuilder();
            StringBuilder csvLine = new StringBuilder();

            csv.AppendLine("DIV,Job #,Company/Resources,Status,Location,Project Manager,Modified By,Last Mod.,Initial Call Date,Preset Date,Last Call Type,Last Call Date/Time,");

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

                    csvLine.Append(jobSummaryList[i].Customer + ",");

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

        /// <summary>
        /// Sorts Job Summary Data Source
        /// </summary>
        /// <param name="keySelector">Lambda expression to select sort column</param>
        /// <param name="sortDirection">Sort direction</param>
        /// <param name="isResoruce">Filter by IsResource field (true/false)</param>
        /// <param name="jobId">If listing resources, filter only resources of this specific Job Identifier</param>
        /// <returns>Sorted Job Summary List</returns>
        private List<CS_View_JobSummary> SortJobSummaryList<TKey>(Func<CS_View_JobSummary, TKey> keySelector, Globals.Common.Sort.SortDirection sortDirection, bool isResoruce, int? jobId)
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

        #endregion

        #region [ IDisposable Implementation ]

        public void Dispose()
        {
            _view = null;
        }

        #endregion
    }
}
