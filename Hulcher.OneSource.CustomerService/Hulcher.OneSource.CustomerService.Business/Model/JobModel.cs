using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;
using System.Transactions;
using Hulcher.OneSource.CustomerService.Core.Security;
using Hulcher.OneSource.CustomerService.Core.Utils;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.DataContext.VO;
using Hulcher.OneSource.CustomerService.DataContext.EntityExtensions;
using Hulcher.OneSource.CustomerService.Business.Workflows;
using System.Activities;

namespace Hulcher.OneSource.CustomerService.Business.Model
{
    /// <summary>
    /// Business Class for Job records
    /// </summary>
    public class JobModel : IDisposable
    {
        #region [ Attributes ]

        static Object locked = new Object();

        /// <summary>
        /// Business class for Settings
        /// </summary>
        private SettingsModel _settingsModel;

        /// <summary>
        /// Business Class for Email
        /// </summary>
        private EmailModel _emailModel;

        /// <summary>
        /// Business Class for Call Criteria
        /// </summary>
        private CallCriteriaModel _callCriteriaModel;

        private List<CS_ScopeOfWork> _mailList;

        private List<CS_JobDivision> _divisionList;

        private StringBuilder _mailScope;

        private StringBuilder _mailBillingBuilder;

        private StringBuilder _initialAdvise;

        private bool _generatesInitialAdvise;

        private Dictionary<string, object> _initialAdviseChangeList;

        /// <summary>
        /// Unit of Work used to access the database/in-memory context
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// Repository class for CS_Job
        /// </summary>
        private ICachedRepository<CS_Job> _jobRepository;

        /// <summary>
        /// Repository class for CS_JobStatus
        /// </summary>
        private ICachedRepository<CS_JobStatus> _jobStatusRepository;

        /// <summary>
        /// Repository class for CS_JobDivision
        /// </summary>
        private IRepository<CS_JobDivision> _jobDivisionRepository;

        /// <summary>
        /// Repository class for CS_PriceType
        /// </summary>
        private ICachedRepository<CS_PriceType> _priceTypeRepository;

        /// <summary>
        /// Repository class for CS_JobCategory
        /// </summary>
        private ICachedRepository<CS_JobCategory> _jobCategoryRepository;

        /// <summary>
        /// Repository class for CS_JobType
        /// </summary>
        private ICachedRepository<CS_JobType> _jobTypeRepository;

        /// <summary>
        /// Repository class for CS_JobAction
        /// </summary>
        private ICachedRepository<CS_JobAction> _jobActionRepository;

        /// <summary>
        /// Repository class for CS_LostJobReason
        /// </summary>
        private ICachedRepository<CS_LostJobReason> _lostJobReasonRepository;

        /// <summary>
        /// Repository class for CS_Competitor
        /// </summary>
        private IRepository<CS_Competitor> _competitorRepository;


        /// <summary>
        /// Repository class for CS_Frequency
        /// </summary>
        private ICachedRepository<CS_Frequency> _frequencyRepository;

        /// <summary>
        /// Repository class for CS_CustomerInfo
        /// </summary>
        private IRepository<CS_CustomerInfo> _customerInfoRepository;

        /// <summary>
        /// Repository class for CS_JobDescription
        /// </summary>
        private IRepository<CS_JobDescription> _jobDescriptionRepository;

        /// <summary>
        /// Repository class for CS_ScopeOfWork
        /// </summary>
        private IRepository<CS_ScopeOfWork> _scopeOfWorkRepository;

        /// <summary>
        /// Repository class for CS_LocationInfo
        /// </summary>
        private IRepository<CS_LocationInfo> _locationInfoRepository;

        /// <summary>
        /// Repository class for CS_JobPhotoReport
        /// </summary>
        private IRepository<CS_JobPhotoReport> _jobPhotoReportRepository;

        /// <summary>
        /// Repository class for CS_PresetInfo
        /// </summary>
        private IRepository<CS_PresetInfo> _presetInfoRepository;

        /// <summary>
        /// Repository class for CS_LostJobInfo
        /// </summary>
        private IRepository<CS_LostJobInfo> _lostJobInfoRepository;

        /// <summary>
        /// Repository class for CS_SpecialPricing
        /// </summary>
        private IRepository<CS_SpecialPricing> _specialPricingRepository;

        /// <summary>
        /// Repository class for CS_JobInfo
        /// </summary>
        private IRepository<CS_JobInfo> _jobInfoRepository;

        /// <summary>
        /// Repository class for CS_Job_JobStatus
        /// </summary>
        private IRepository<CS_Job_JobStatus> _jobStatusHistoryRepository;

        /// <summary>
        /// Repository class for CS_JobPermit
        /// </summary>
        private IRepository<CS_JobPermit> _jobPermitRepository;

        /// <summary>
        /// Repository class for CS_Division
        /// </summary>
        private IRepository<CS_Division> _divisionRepository;

        /// <summary>
        /// Repository class for CS_Employee
        /// </summary>
        private IRepository<CS_Employee> _employeeRepository;

        /// <summary>
        /// Repository class for CS_Customer
        /// </summary>
        private IRepository<CS_Customer> _customerRepository;

        /// <summary>
        /// Repository class for CS_City
        /// </summary>
        private IRepository<CS_City> _cityRepository;

        /// <summary>
        /// Repository class for CS_State
        /// </summary>
        private IRepository<CS_State> _stateRepository;

        /// <summary>
        /// Repository class for CS_Country
        /// </summary>
        private IRepository<CS_Country> _countryRepository;

        /// <summary>
        /// Repository class for CS_ZipCode
        /// </summary>
        private IRepository<CS_ZipCode> _zipCodeRepository;

        /// <summary>
        /// Repository class for CS_Contact
        /// </summary>
        private IRepository<CS_Contact> _contactRepository;

        /// <summary>
        /// Repository class for CS_Calllog
        /// </summary>
        private IRepository<CS_CallLog> _callLogRepository;

        /// <summary>
        /// Repository class for CS_JobPermitType
        /// </summary>
        private ICachedRepository<CS_JobPermitType> _jobPermitTypeRepository;

        /// <summary>
        /// Repository class for CS_JobSummaryView
        /// </summary>
        private IRepository<CS_View_JobSummary> _jobSummaryRepository;

        /// <summary>
        /// Repository class for CS_CallLogResource
        /// </summary>
        private IRepository<CS_CallLogResource> _callLogResourceRepository;

        /// <summary>
        /// Repository class for CS_CallLogCallCriteriaEmail
        /// </summary>
        private IRepository<CS_CallLogCallCriteriaEmail> _callLogCallCriteriaEmailRepository;

        /// <summary>
        /// Repository class for CS_View_TurnoverActiveReport
        /// </summary>
        private IRepository<CS_View_TurnoverActiveReport> _turnoverActiveReportRepository;

        /// <summary>
        /// Repository class for CS_View_TurnoverNonActiveReport
        /// </summary>
        private IRepository<CS_View_TurnoverNonActiveReport> _turnoverNonActiveReportRepository;

        /// <summary>
        /// Repository class for CS_View_GetJobData
        /// </summary>
        private IRepository<CS_View_GetJobData> _jobDataRepository;

        /// <summary>
        /// Repository class for CS_Job_LocalEquipmentType
        /// </summary>
        private IRepository<CS_Job_LocalEquipmentType> _jobLocalEquipmentTypeRepository;

        /// <summary>
        /// Repository class for CS_View_ProjectCalendar_Allocation
        /// </summary>
        private IRepository<CS_View_ProjectCalendar_Allocation> _projectCalendarAllocationRepository;

        /// <summary>
        /// Repository class for CS_View_ProjectCalendar_Reserved
        /// </summary>
        private IRepository<CS_View_ProjectCalendar_Reserved> _projectCalendarReservedRepository;

        /// <summary>
        /// Repository class for CS_View_ProjectCalendar_CallLog
        /// </summary>
        private IRepository<CS_View_ProjectCalendar_CallLog> _projectCalendarCallLogRepository;

        #endregion

        #region [ Constructors ]

        public JobModel()
        {
            _unitOfWork = new EFUnitOfWork();

            InstanceObjects();
        }

        public JobModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            InstanceObjects();
        }

        private void InstanceObjects()
        {
            _jobRepository = new CachedRepository<CS_Job>();
            _jobRepository.UnitOfWork = _unitOfWork;

            _jobStatusRepository = new CachedRepository<CS_JobStatus>();
            _jobStatusRepository.UnitOfWork = _unitOfWork;

            _jobDivisionRepository = new EFRepository<CS_JobDivision>();
            _jobDivisionRepository.UnitOfWork = _unitOfWork;

            _priceTypeRepository = new CachedRepository<CS_PriceType>();
            _priceTypeRepository.UnitOfWork = _unitOfWork;

            _jobCategoryRepository = new CachedRepository<CS_JobCategory>();
            _jobCategoryRepository.UnitOfWork = _unitOfWork;

            _jobTypeRepository = new CachedRepository<CS_JobType>();
            _jobTypeRepository.UnitOfWork = _unitOfWork;

            _jobActionRepository = new CachedRepository<CS_JobAction>();
            _jobActionRepository.UnitOfWork = _unitOfWork;

            _lostJobReasonRepository = new CachedRepository<CS_LostJobReason>();
            _lostJobReasonRepository.UnitOfWork = _unitOfWork;

            _competitorRepository = new EFRepository<CS_Competitor>();
            _competitorRepository.UnitOfWork = _unitOfWork;

            _frequencyRepository = new CachedRepository<CS_Frequency>();
            _frequencyRepository.UnitOfWork = _unitOfWork;

            _customerInfoRepository = new EFRepository<CS_CustomerInfo>();
            _customerInfoRepository.UnitOfWork = _unitOfWork;

            _jobDescriptionRepository = new EFRepository<CS_JobDescription>();
            _jobDescriptionRepository.UnitOfWork = _unitOfWork;

            _scopeOfWorkRepository = new EFRepository<CS_ScopeOfWork>();
            _scopeOfWorkRepository.UnitOfWork = _unitOfWork;

            _locationInfoRepository = new EFRepository<CS_LocationInfo>();
            _locationInfoRepository.UnitOfWork = _unitOfWork;

            _jobPhotoReportRepository = new EFRepository<CS_JobPhotoReport>();
            _jobPhotoReportRepository.UnitOfWork = _unitOfWork;

            _presetInfoRepository = new EFRepository<CS_PresetInfo>();
            _presetInfoRepository.UnitOfWork = _unitOfWork;

            _lostJobInfoRepository = new EFRepository<CS_LostJobInfo>();
            _lostJobInfoRepository.UnitOfWork = _unitOfWork;

            _specialPricingRepository = new EFRepository<CS_SpecialPricing>();
            _specialPricingRepository.UnitOfWork = _unitOfWork;

            _jobInfoRepository = new EFRepository<CS_JobInfo>();
            _jobInfoRepository.UnitOfWork = _unitOfWork;

            _jobStatusHistoryRepository = new EFRepository<CS_Job_JobStatus>();
            _jobStatusHistoryRepository.UnitOfWork = _unitOfWork;

            _jobPermitRepository = new EFRepository<CS_JobPermit>();
            _jobPermitRepository.UnitOfWork = _unitOfWork;

            _divisionRepository = new EFRepository<CS_Division>();
            _divisionRepository.UnitOfWork = _unitOfWork;

            _employeeRepository = new EFRepository<CS_Employee>();
            _employeeRepository.UnitOfWork = _unitOfWork;

            _customerRepository = new EFRepository<CS_Customer>();
            _customerRepository.UnitOfWork = _unitOfWork;

            _cityRepository = new EFRepository<CS_City>();
            _cityRepository.UnitOfWork = _unitOfWork;

            _stateRepository = new EFRepository<CS_State>();
            _stateRepository.UnitOfWork = _unitOfWork;

            _countryRepository = new EFRepository<CS_Country>();
            _countryRepository.UnitOfWork = _unitOfWork;

            _zipCodeRepository = new EFRepository<CS_ZipCode>();
            _zipCodeRepository.UnitOfWork = _unitOfWork;

            _contactRepository = new EFRepository<CS_Contact>();
            _contactRepository.UnitOfWork = _unitOfWork;

            _callLogRepository = new EFRepository<CS_CallLog>();
            _callLogRepository.UnitOfWork = _unitOfWork;

            _jobPermitTypeRepository = new CachedRepository<CS_JobPermitType>();
            _jobPermitTypeRepository.UnitOfWork = _unitOfWork;

            _jobSummaryRepository = new EFRepository<CS_View_JobSummary>();
            _jobSummaryRepository.UnitOfWork = _unitOfWork;

            _callLogResourceRepository = new EFRepository<CS_CallLogResource>();
            _callLogResourceRepository.UnitOfWork = _unitOfWork;

            _callLogCallCriteriaEmailRepository = new EFRepository<CS_CallLogCallCriteriaEmail>();
            _callLogCallCriteriaEmailRepository.UnitOfWork = _unitOfWork;

            _turnoverActiveReportRepository = new EFRepository<CS_View_TurnoverActiveReport>();
            _turnoverActiveReportRepository.UnitOfWork = _unitOfWork;

            _turnoverNonActiveReportRepository = new EFRepository<CS_View_TurnoverNonActiveReport>();
            _turnoverNonActiveReportRepository.UnitOfWork = _unitOfWork;

            _jobLocalEquipmentTypeRepository = new EFRepository<CS_Job_LocalEquipmentType>();
            _jobLocalEquipmentTypeRepository.UnitOfWork = _unitOfWork;

            _settingsModel = new SettingsModel(_unitOfWork);
            _emailModel = new EmailModel(_unitOfWork);
            _callCriteriaModel = new CallCriteriaModel(_unitOfWork);

            _mailList = new List<CS_ScopeOfWork>();
            _divisionList = new List<CS_JobDivision>();

            _mailScope = new StringBuilder();
            _mailBillingBuilder = new StringBuilder();

            _jobDataRepository = new EFRepository<CS_View_GetJobData>();
            _jobDataRepository.UnitOfWork = _unitOfWork;

            _jobLocalEquipmentTypeRepository = new EFRepository<CS_Job_LocalEquipmentType>();
            _jobLocalEquipmentTypeRepository.UnitOfWork = _unitOfWork;


            _projectCalendarAllocationRepository = new EFRepository<CS_View_ProjectCalendar_Allocation>();
            _projectCalendarAllocationRepository.UnitOfWork = _unitOfWork;

            _projectCalendarReservedRepository = new EFRepository<CS_View_ProjectCalendar_Reserved>();
            _projectCalendarReservedRepository.UnitOfWork = _unitOfWork;

            _projectCalendarCallLogRepository = new EFRepository<CS_View_ProjectCalendar_CallLog>();
            _projectCalendarCallLogRepository.UnitOfWork = _unitOfWork;
        }

        #endregion

        #region [ Properties ]

        public CS_Job NewJob
        {
            get;
            set;
        }

        public CS_CustomerInfo NewCustomer
        {
            get;
            set;
        }

        public IList<CS_JobDivision> NewJobDivision
        {
            get;
            set;
        }

        public CS_PresetInfo NewPresetInfo
        {
            get;
            set;
        }

        public CS_LostJobInfo NewLostJobInfo
        {
            get;
            set;
        }
        public CS_SpecialPricing NewSpecialPricing
        {
            get;
            set;
        }
        public CS_JobInfo NewJobInfo
        {
            get;
            set;
        }
        public CS_Job_JobStatus NewJobStatusHistory
        {
            get;
            set;
        }
        public CS_LocationInfo NewLocationInfo
        {
            get;
            set;
        }

        public CS_JobDescription NewJobDescription
        {
            get;
            set;
        }

        public IList<CS_ScopeOfWork> NewScopeOfWork
        {
            get;
            set;
        }

        public IList<CS_JobPermit> NewPermit
        {
            get;
            set;
        }

        public IList<CS_JobPhotoReport> NewPhotoReport
        {
            get;
            set;
        }

        public IList<LocalEquipmentTypeVO> NewRequestedEquipment
        {
            get;
            set;
        }

        public int JobStatusID
        {
            get;
            set;
        }

        #endregion

        #region [ Methods ]

        #region [ Listing Methods ]

        /// <summary>
        /// Returns the General Log entity from Database
        /// General Log is a Job with specific attributes and logic behind it
        /// </summary>
        /// <returns>General Log entity</returns>
        public CS_Job GetGeneralJob()
        {
            return _jobRepository.Get(e => e.ID == Globals.GeneralLog.ID);
        }

        public CS_Job GetJobWithFullInclude(int jobID)
        {
            return _jobRepository.Get(e => e.ID == jobID, "CS_CustomerInfo.CS_Customer", "CS_CustomerInfo.CS_Contact", "CS_CustomerInfo.CS_Contact1", "CS_CustomerInfo.CS_Contact2", "CS_CustomerInfo.CS_Contact3", "CS_CustomerInfo.CS_Division", "CS_CustomerInfo.CS_Employee",
                                                          "CS_JobDescription", "CS_ScopeOfWork",
                                                          "CS_JobInfo.CS_Frequency", "CS_JobInfo.CS_JobAction", "CS_JobInfo.CS_JobType", "CS_JobInfo.CS_PriceType", "CS_JobInfo.CS_JobCategory", "CS_JobInfo.CS_Job_JobStatus", "CS_JobInfo.CS_Employee",
                                                          "CS_SpecialPricing", "CS_PresetInfo", "CS_LostJobInfo", "CS_JobDivision",
                                                          "CS_LocationInfo.CS_Country", "CS_LocationInfo.CS_City", "CS_LocationInfo.CS_ZipCode", "CS_LocationInfo.CS_State");
        }

        /// <summary>
        /// Gets job information in order to load the job record page
        /// </summary>
        /// <param name="jobID">Job Identifier</param>
        /// <returns>Job Information</returns>
        public CS_View_GetJobData GetJobData(int jobID)
        {
            return _jobDataRepository.Get(e => e.JobID == jobID);
        }

        /// <summary>
        /// Returns a job filtered by its Number|Internal_Tracking
        /// </summary>
        /// <param name="jobNumber">Identification of the Job</param>
        /// <returns>Entity representing the job</returns>
        public CS_Job GetJobByNumber(string jobNumber)
        {
            return _jobRepository.Get(e => e.Active && (e.Number == jobNumber || e.Internal_Tracking == jobNumber));
        }

        /// <summary>
        /// Executes the dashboard view search for the Job Summary
        /// </summary>
        /// <param name="jobStatusId"></param>
        /// <param name="jobId"></param>
        /// <param name="divisionId"></param>
        /// <param name="customerId"></param>
        /// <param name="dateFilterType"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public IList<CS_SP_GetJobSummary_Result> FindJobSummary(int? jobStatusId, int? jobId, int? divisionId, int? customerId, Globals.Dashboard.DateFilterType dateFilterType, DateTime beginDate, DateTime endDate, string personName)
        {
            //IList<CS_View_JobSummary> lstJS = new List<CS_View_JobSummary>();

            //switch (dateFilterType)
            //{
            //    case Globals.Dashboard.DateFilterType.InitialCallDate:
            //        lstJS = _jobSummaryRepository.ListAll(w => ((w.JobStatusId == jobStatusId) || (jobStatusId == null)) &&
            //                                            ((w.JobID == jobId) || (jobId == null)) &&
            //                                            ((w.DivisionId == divisionId) || (divisionId == null)) &&
            //                                            ((w.CustomerId == customerId) || (customerId == null)) &&
            //                                            w.CallDate >= beginDate && w.CallDate <= endDate).ToList();
            //        break;
            //    case Globals.Dashboard.DateFilterType.JobStartDate:
            //        lstJS = _jobSummaryRepository.ListAll(w => ((w.JobStatusId == jobStatusId) || (jobStatusId == null)) &&
            //                                            ((w.JobID == jobId) || (jobId == null)) &&
            //                                            ((w.DivisionId == divisionId) || (divisionId == null)) &&
            //                                            ((w.CustomerId == customerId) || (customerId == null)) &&
            //                                            w.StartDate >= beginDate && w.StartDate <= endDate).ToList();
            //        break;
            //    case Globals.Dashboard.DateFilterType.PresetDate:
            //        lstJS = _jobSummaryRepository.ListAll(w => ((w.JobStatusId == jobStatusId) || (jobStatusId == null)) &&
            //                                            ((w.JobID == jobId) || (jobId == null)) &&
            //                                            ((w.DivisionId == divisionId) || (divisionId == null)) &&
            //                                            ((w.CustomerId == customerId) || (customerId == null)) &&
            //                                            w.PresetDate >= beginDate && w.PresetDate <= endDate).ToList();
            //        break;
            //    case Globals.Dashboard.DateFilterType.ModificationDate:
            //        lstJS = _jobSummaryRepository.ListAll(w => ((w.JobStatusId == jobStatusId) || (jobStatusId == null)) &&
            //                                            ((w.JobID == jobId) || (jobId == null)) &&
            //                                            ((w.DivisionId == divisionId) || (divisionId == null)) &&
            //                                            ((w.CustomerId == customerId) || (customerId == null)) &&
            //                                            w.LastModification >= beginDate && w.LastModification <= endDate).ToList();
            //        break;
            //}

            try
            {
                CS_View_JobSummaryRepository jobSummaryRepository = new CS_View_JobSummaryRepository(_jobSummaryRepository, _jobSummaryRepository.UnitOfWork);

                return jobSummaryRepository.GetJobSummary(jobStatusId, jobId, divisionId, customerId, (int)dateFilterType, beginDate, endDate, personName).OrderByDescending(e => e.LastModification).ThenBy(e => e.JobID).ThenBy(e => e.JobNumber).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Executes the dashboard view search for the Job Summary based on a list of ID's (Search Criteria)
        /// </summary>
        /// <param name="jobIdList"></param>
        /// <returns></returns>
        public IList<CS_View_JobSummary> FindJobSummary(IList<int> jobIdList)
        {
            return _jobSummaryRepository.ListAll(e => jobIdList.Contains(e.JobID));
        }

        /// <summary>
        /// Executes the dashboard view search for the Job Summary based on the Job Status
        /// </summary>
        /// <param name="jobIdList"></param>
        /// <returns></returns>
        public IList<CS_View_JobSummary> FindJobSummary(int StatusID)
        {
            return _jobSummaryRepository.ListAll(e => e.JobStatusId == StatusID).OrderByDescending(e => e.LastModification).ThenBy(e => e.JobID).ThenBy(e => e.JobNumber).ToList();
        }

        /// <summary>
        /// Executes Preset/Potential report search
        /// </summary>
        /// <param name="StatusID"></param>
        /// <returns></returns>
        public IList<CS_View_TurnoverNonActiveReport> FindTurnoverNonActive(int StatusID)
        {
            IList<CS_View_TurnoverNonActiveReport> list = _turnoverNonActiveReportRepository.ListAll(e => e.JobStatusId == StatusID && (e.EquipmentName != "" || (e.EquipmentName == "" && e.IsKeyPerson.Value))).OrderByDescending(e => e.LastModification).ThenBy(e => e.JobID).ThenBy(e => e.JobNumber).ToList();
            return list;
        }

        /// <summary>
        /// Get jobAction by id
        /// </summary>
        /// <param name="jobActionId">job action ID</param>
        /// <returns>job action</returns>
        public CS_JobAction GetJobAction(int jobActionId)
        {
            return _jobActionRepository.Get(w => w.Active && w.ID == jobActionId);
        }

        /// <summary>
        /// Gets job Information via Job identifier
        /// </summary>
        /// <param name="jobId">Job identifier</param>
        /// <returns>job Information</returns>
        public CS_Job GetJobById(int jobId)
        {
            return _jobRepository.Get(e => e.Active && e.ID == jobId, new string[] { "CS_JobInfo", "CS_JobInfo.CS_Job_JobStatus", "CS_JobInfo.CS_Job_JobStatus.CS_JobStatus", "CS_LocationInfo", "CS_CustomerInfo" });
        }

        /// <summary>
        /// List all items of an Entity in the Database
        /// </summary>
        /// <returns>List of Entities</returns>
        public IList<CS_Job> ListAllJobs()
        {
            IList<CS_Job> returnList = _jobRepository.ListAll(e => e.Active && e.ID != Globals.GeneralLog.ID,
                new string[] { "CS_JobInfo", "CS_CustomerInfo", "CS_CustomerInfo.CS_Customer", "CS_LocationInfo", "CS_LocationInfo", "CS_LocationInfo.CS_Country", "CS_LocationInfo.CS_State", "CS_LocationInfo.CS_City" });

            return returnList.OrderByDescending(e => e.NumberOrInternalTracking).ToList();
        }

        /// <summary>
        /// List all billable jobs in the database
        /// </summary>
        /// <returns>List of Entities</returns>
        public IList<CS_Job> ListAllBillableJobs()
        {
            int[] jobStatus = new int[]{
                (int)Globals.JobRecord.JobStatus.Active,
                (int)Globals.JobRecord.JobStatus.Lost,
                (int)Globals.JobRecord.JobStatus.Cancelled
            };
            int closedStatus = (int)Globals.JobRecord.JobStatus.Closed;
            return _jobRepository.ListAll(e => e.Active && e.ID != Globals.GeneralLog.ID &&
                ((jobStatus.Contains(e.CS_JobInfo.CS_Job_JobStatus.FirstOrDefault(f => f.Active).JobStatusId) && e.IsBilling) ||
                 e.CS_JobInfo.CS_Job_JobStatus.FirstOrDefault(f => f.Active).JobStatusId == closedStatus),
                new string[] { "CS_JobInfo", "CS_CustomerInfo", "CS_CustomerInfo.CS_Customer" });
        }

        /// <summary>
        /// List all items of an Entity in the Database
        /// </summary>
        /// <returns>List of Entities</returns>
        public IList<CS_Job> ListAllJobsByNumberWithGeneral(string number)
        {
            return _jobRepository.ListAll(e => e.Active && (e.Number.Contains(number) || e.Internal_Tracking.Contains(number)),
                new string[] { "CS_JobInfo", "CS_JobInfo.CS_PriceType", "CS_JobInfo.CS_JobType", "CS_JobInfo.CS_Job_JobStatus" });
        }

        /// <summary>
        /// List all items of an Entity in the Database
        /// </summary>
        /// <returns>List of Entities</returns>
        public IList<CS_Job> ListAllJobsByNumber(string number, string statusId)
        {
            int status = 0;

            if (string.IsNullOrEmpty(statusId))
                status = 0;
            else
                status = int.Parse(statusId);

            return _jobRepository.ListAll(e => e.Active && e.ID != Globals.GeneralLog.ID && (e.Number.Contains(number) || e.Internal_Tracking.Contains(number))
                && (status == 0 || e.CS_JobInfo.CS_Job_JobStatus.Any(f => f.Active && f.JobStatusId == status)),
                new string[] { "CS_JobInfo", "CS_JobInfo.CS_PriceType", "CS_JobInfo.CS_JobType", "CS_JobInfo.CS_Job_JobStatus" });
        }

        /// <summary>
        /// List all items of an Entity in the Database
        /// </summary>
        /// <returns>List of Entities</returns>
        public IList<CS_Job> ListAllBillableJobsByNumber(string number)
        {
            int[] jobStatus = new int[]{
                (int)Globals.JobRecord.JobStatus.Active,
                (int)Globals.JobRecord.JobStatus.Lost,
                (int)Globals.JobRecord.JobStatus.Cancelled
            };
            int closedStatus = (int)Globals.JobRecord.JobStatus.Closed;
            return _jobRepository.ListAll(e => e.Active && e.ID != Globals.GeneralLog.ID &&
                (e.Number.Contains(number) || e.Internal_Tracking.Contains(number)) &&
                ((jobStatus.Contains(e.CS_JobInfo.CS_Job_JobStatus.FirstOrDefault(f => f.Active).JobStatusId) && e.IsBilling) ||
                 e.CS_JobInfo.CS_Job_JobStatus.FirstOrDefault(f => f.Active).JobStatusId == closedStatus),
                new string[] { "CS_JobInfo", "CS_JobInfo.CS_PriceType", "CS_JobInfo.CS_JobType", "CS_JobInfo.CS_Job_JobStatus" });
        }

        /// <summary>
        /// Deletes a Job Record (Inactivates it)
        /// </summary>
        /// <param name="jobId">Job Identifier</param>
        /// <param name="username">Username that requested the operation</param>
        public void DeleteJob(int jobId, string username)
        {
            CS_Job job = _jobRepository.Get(e => e.ID == jobId);
            if (null != job)
            {
                job.ModificationDate = DateTime.Now;
                job.ModifiedBy = username;
                job.Active = false;

                _jobRepository.Update(job);
            }
        }

        /// <summary>
        /// List all items of an Entity in the Database
        /// </summary>
        /// <returns>List of Entities</returns>
        public virtual IList<CS_JobStatus> ListAllJobStatus()
        {
            return _jobStatusRepository.ListAll(e => e.Active, order => order.Description, true);
        }

        /// <summary>
        /// List all Job Status based description
        /// </summary>
        /// <param name="description">description</param>
        /// <returns>Job Status list</returns>
        public virtual IList<CS_JobStatus> ListJobStatusByDescription(string description)
        {
            return _jobStatusRepository.ListAll(e => e.Active && e.Description.ToLower().StartsWith(description.ToLower()), order => order.Description, true);
        }

        /// <summary>
        /// List all items of an Entity in the Database
        /// </summary>
        /// <returns>List of Entities</returns>
        public virtual IList<CS_JobDivision> ListAllJobDivisions()
        {
            return _jobDivisionRepository.ListAll(e => e.Active);
        }

        /// <summary>
        /// List all divisions by job
        /// </summary>
        /// <param name="jobId">job id</param>
        /// <returns>list</returns>
        public IList<CS_JobDivision> ListAllDivisionsByJob(int jobId)
        {
            return _jobDivisionRepository.ListAll(e => e.Active && e.JobID == jobId, "CS_Division");
        }

        /// <summary>
        /// List all items of an Entity in the Database
        /// </summary>
        /// <returns>List of Entities</returns>
        public virtual IList<CS_PriceType> ListAllPriceTypes()
        {
            return _priceTypeRepository.ListAll(e => e.Active, order => order.Description, true);
        }

        /// <summary>
        /// List all active Price Types filtered by Name
        /// </summary>
        /// <param name="prefixFilter">Filter Name (Starts With)</param>
        /// <returns>List of Price Types</returns>
        public IList<CS_PriceType> ListAllFilteredPriceTypesByName(string prefixFilter)
        {
            return _priceTypeRepository.ListAll(e => e.Active && e.Description.ToLower().StartsWith(prefixFilter.ToLower()), order => order.Description, true);
        }

        /// <summary>
        /// List all active Job Actions filtered by name
        /// </summary>
        /// <param name="prefixFilter">filter name (like)</param>
        /// <returns>list of job actions</returns>
        public IList<CS_JobAction> ListAllJobActionByName(string prefixFilter)
        {
            return _jobActionRepository.ListAll(e => e.Active && e.Description.ToLower().Contains(prefixFilter.ToLower()), order => order.Description, true);
        }

        /// <summary>
        /// List all items of an Entity in the Database
        /// </summary>
        /// <returns>List of Entities</returns>
        public virtual IList<CS_JobPermitType> ListAllPermitTypes()
        {
            return _jobPermitTypeRepository.ListAll(e => e.Active, order => order.Description, true);
        }

        /// <summary>
        /// List all items of an Entity in the Database
        /// </summary>
        /// <returns>List of Entities</returns>
        public virtual IList<CS_JobCategory> ListAllJobCategories()
        {
            return _jobCategoryRepository.ListAll(e => e.Active, order => order.Description, true);
        }

        /// <summary>
        /// Return a Job Category based on a Job Action
        /// </summary>
        /// <param name="jobActionId">Job Action Identifier</param>
        /// <returns>Job Category data</returns>
        public virtual CS_JobCategory GetJobCategoryByJobAction(int jobActionId)
        {
            return _jobCategoryRepository.Get(e => e.CS_JobType.Any(f => f.CS_JobAction.Any(g => g.ID == jobActionId)));
        }

        /// <summary>
        /// List all items of an Entity in the Database
        /// </summary>
        /// <returns>List of Entities</returns>
        public virtual IList<CS_JobType> ListAllJobTypes()
        {
            return _jobTypeRepository.ListAll(e => e.Active, order => order.Description, true);
        }

        /// <summary>
        /// Return a Job Type based on a Job Action
        /// </summary>
        /// <param name="jobActionId">Job Action Identifier</param>
        /// <returns>Job Type data</returns>
        public virtual CS_JobType GetJobTypeByJobAction(int jobActionId)
        {
            return _jobTypeRepository.Get(e => e.CS_JobAction.Any(f => f.ID == jobActionId));
        }

        /// <summary>
        /// List all items of an Entity in the Database
        /// </summary>
        /// <returns>List of Entities</returns>
        public virtual IList<CS_JobAction> ListAllJobActions()
        {
            return _jobActionRepository.ListAll(e => e.Active, order => order.Description, true);
        }

        /// <summary>
        /// List all items of an Entity in the Database
        /// </summary>
        /// <returns>List of Entities</returns>
        public virtual IList<CS_LostJobReason> ListAllLostJobReasons()
        {
            return _lostJobReasonRepository.ListAll(e => e.Active, order => order.Description, true);
        }

        /// <summary>
        /// List all items of an Entity in the Database
        /// </summary>
        /// <returns>List of Entities</returns>
        public virtual IList<CS_Competitor> ListAllCompetitors()
        {
            return _competitorRepository.ListAll(e => e.Active, order => order.Description, true);
        }

        /// <summary>
        /// List all items of an Entity in the Database
        /// </summary>
        /// <returns>List of Entities</returns>
        public virtual IList<CS_Frequency> ListAllFrequencies()
        {
            return _frequencyRepository.ListAll(e => e.Active, order => order.Description, true);
        }

        public virtual CS_JobStatus GetJobStatus(int id)
        {
            return _jobStatusRepository.Get(e => e.Active && e.ID == id);
        }

        public virtual CS_PriceType ListPriceTypeById(int id)
        {
            return _priceTypeRepository.Get(e => e.Active && e.ID == id);
        }

        /// <summary>
        /// Get the CustomerInfo Entity related to a Specific Job
        /// </summary>
        /// <returns>A CS_CustomerInfo Entity</returns>
        public virtual CS_CustomerInfo GetCustomerInfoByJobId(int jobId)
        {
            return _customerInfoRepository.Get(e => e.Active && e.JobId == jobId);
        }

        public virtual CS_JobDescription GetJobDescriptionByJobId(int jobId)
        {
            return _jobDescriptionRepository.Get(e => e.Active && e.JobId == jobId);
        }

        public IList<CS_ScopeOfWork> GetScopeOfWorkByJobId(int jobId)
        {
            return _scopeOfWorkRepository.ListAll(e => e.Active && e.JobId == jobId);
        }

        /// <summary>
        /// Get the LocationInfo Entity related to a Specific Job
        /// </summary>
        /// <returns>A CS_LocationInfo Entity</returns>
        public virtual CS_LocationInfo GetLocationInfoByJobId(int jobId)
        {
            return _locationInfoRepository.Get(e => e.Active && e.JobID == jobId);
        }

        /// <summary>
        /// Method that retrieves all jobpermit relatted to a specific job
        /// </summary>
        /// <param name="jobId">job id</param>
        /// <returns>list of jobpermit</returns>
        public IList<CS_JobPhotoReport> GetPhotoReportByJobId(int jobId)
        {
            return _jobPhotoReportRepository.ListAll(e => e.Active && e.JobID == jobId);
        }

        /// <summary>
        /// Get the LocationInfo Entity related to a Specific Job
        /// </summary>
        /// <returns>A CS_LocationInfo Entity</returns>
        public virtual IList<CS_Job_LocalEquipmentType> GetEquipmentRequestedByJob(int jobId)
        {
            return _jobLocalEquipmentTypeRepository.ListAll(e => e.Active && e.JobID == jobId, new string[] { "CS_LocalEquipmentType" });

        }

        /// <summary>
        /// Get the LocationInfo Entity related to a Specific Job
        /// </summary>
        /// <returns>A CS_LocationInfo Entity</returns>
        public virtual IList<LocalEquipmentTypeVO> GetEquipmentRequestedVOByJob(int jobId)
        {
            IList<CS_Job_LocalEquipmentType> list = GetEquipmentRequestedByJob(jobId);
            IList<LocalEquipmentTypeVO> returnList = new List<LocalEquipmentTypeVO>();

            for (int i = 0; i < list.Count; i++)
            {
                LocalEquipmentTypeVO item = new LocalEquipmentTypeVO();
                item.ID = list[i].ID;
                item.LocalEquipmentTypeID = list[i].LocalEquipmentTypeID;
                item.Name = list[i].CS_LocalEquipmentType.Name + ((!string.IsNullOrEmpty(list[i].SpecificEquipment))? " - " + list[i].SpecificEquipment: "");
                item.Quantity = list[i].Quantity;
                item.CreatedBy = list[i].CreatedBy;
                item.CreationDate = list[i].CreationDate;

                returnList.Add(item);
            }

            return returnList;
        }

        /// <summary>
        /// Method that retrieves all equipmentrequested relatted to a specific job
        /// </summary>
        /// <param name="jobId">job id</param>
        /// <returns>list of equipmentrequested</returns>
        public virtual IList<CS_JobPermit> GetPermitInfoByJob(int jobId)
        {
            return _jobPermitRepository.ListAll(e => e.Active && e.JobID == jobId, new string[] { "CS_JobPermitType" });
        }

        /// <summary>
        /// Gets job information related to a specific Job Identifier
        /// </summary>
        /// <param name="jobId">Job Identifier</param>
        /// <returns>Job Info</returns>
        public virtual CS_JobInfo GetJobInfo(int jobId)
        {
            return _jobInfoRepository.Get(e => e.Active && e.JobID == jobId);
        }

        public virtual CS_JobInfo GetJobInfoWithAppend(int jobId)
        {
            return _jobInfoRepository.Get(e => e.Active && e.JobID == jobId, System.Data.Objects.MergeOption.AppendOnly);
        }

        /// <summary>
        /// Gets job special pricing information related to a specific Job Identifier
        /// </summary>
        /// <param name="jobId">Job Identifier</param>
        /// <returns>Job special pricing information</returns>
        public virtual CS_SpecialPricing GetSpecialPricing(int jobId)
        {
            return _specialPricingRepository.Get(e => e.Active && e.JobId == jobId);
        }

        /// <summary>
        /// Gets job preset information related to a specific Job Identifier
        /// </summary>
        /// <param name="jobId">Job Identifier</param>
        /// <returns>Job preset information</returns>
        public virtual CS_PresetInfo GetPresetInfo(int jobId)
        {
            return _presetInfoRepository.Get(e => e.Active && e.JobId == jobId);
        }

        /// <summary>
        /// Gets lost job information related to a specific Job Identifier
        /// </summary>
        /// <param name="jobId">Job Identifier</param>
        /// <returns>lost job information</returns>
        public CS_LostJobInfo GetLostJob(int jobId)
        {
            return _lostJobInfoRepository.Get(e => e.Active && e.JobId == jobId);
        }

        /// <summary>
        /// Gets a list of divisions related to a specific Job Identifier
        /// </summary>
        /// <param name="jobId">Job Identifier</param>
        /// <returns>Division list</returns>
        public virtual IList<CS_JobDivision> ListJobDivision(int jobId)
        {
            return _jobDivisionRepository.ListAll(e => e.Active && e.JobID == jobId);
        }

        /// <summary>
        /// Get a list of Jobs filtered
        /// </summary>
        /// <param name="filter">filter</param>
        /// <param name="value">name</param>
        /// <returns>list</returns>
        public IList<CS_Job> CallEntryFilter(Globals.JobRecord.CallEntryFilter filter, string value)
        {
            string[] includeList = new string[] { "CS_CustomerInfo", "CS_CustomerInfo.CS_Customer", "CS_JobDivision", "CS_JobDivision.CS_Division", "CS_LocationInfo", "CS_LocationInfo.CS_City", "CS_LocationInfo.CS_State", "CS_JobInfo", "CS_JobInfo.CS_Job_JobStatus", "CS_JobInfo.CS_PriceType", "CS_JobInfo.CS_JobType" };

            switch (filter)
            {
                case Globals.JobRecord.CallEntryFilter.Customer:
                    return _jobRepository.ListAll(e => e.Active && e.CS_CustomerInfo.CS_Customer.Name.Contains(value) && e.ID != Globals.GeneralLog.ID,
                        includeList);
                case Globals.JobRecord.CallEntryFilter.Division:
                    return _jobRepository.ListAll(e => e.Active && e.CS_JobDivision.Any(f => f.Active && f.CS_Division.Name.Contains(value)) && e.ID != Globals.GeneralLog.ID,
                        includeList);
                case Globals.JobRecord.CallEntryFilter.JobNumber:
                    return _jobRepository.ListAll(e => e.Active && (e.Number != null && e.Number.Contains(value)) || (e.Number == null && e.Internal_Tracking != null && e.Internal_Tracking.Contains(value)) && e.ID != Globals.GeneralLog.ID,
                        includeList);
                case Globals.JobRecord.CallEntryFilter.Location:
                    return _jobRepository.ListAll(e => e.Active && (e.CS_LocationInfo.CS_City.Name.Contains(value) || e.CS_LocationInfo.CS_State.Name.Contains(value)) && e.ID != Globals.GeneralLog.ID,
                        includeList);
            }

            return null;
        }

        /// <summary>
        /// Get a list of jobs by location name 
        /// </summary>
        /// <param name="locationName">location name</param>
        /// <returns>list</returns>
        public IList<CS_Job> ListAllByLocationSiteName(string locationName)
        {
            return _jobRepository.ListAll(e => e.Active && e.CS_LocationInfo.SiteName.Contains(locationName) && e.ID != Globals.GeneralLog.ID);
        }

        /// <summary>
        /// Invoke Method for the Compiled Query
        /// </summary>
        /// <param name="jobId">job identifier</param>
        /// <returns>List of jobs</returns>
        public CS_Job GetJobInfoForCallEntry(int jobId)
        {
            return _jobRepository.Get(e => e.Active && e.ID == jobId,
                new string[] { "CS_CustomerInfo", "CS_CustomerInfo.CS_Customer", "CS_JobDivision", "CS_JobDivision.CS_Division", "CS_LocationInfo", "CS_LocationInfo.CS_City", "CS_LocationInfo.CS_State" });
        }

        /// <summary>
        /// Return a List of Jobs based on the informed parameters
        /// </summary>
        /// <param name="equipmentTypeId">Equipment Type ID</param>
        /// <param name="divisionId">Division ID</param>
        /// <returns>List of Jobs</returns>
        public IList<CS_Job> ListAllJobsByEquipmentTypeAndDivision(int equipmentTypeId, int divisionId)
        {
            return _jobRepository.ListAll(e => e.Active
                                        && e.CS_Resource.Any(f => f.JobID == e.ID
                                                            && f.CS_Equipment.ID == f.EquipmentID
                                                            && f.CS_Equipment.EquipmentTypeID == equipmentTypeId
                                                            && f.CS_Equipment.DivisionID == divisionId));
        }

        /// <summary>
        /// Return a List of jobs based on the start date and the status (when a preset is overdue)
        /// </summary>
        /// <returns>List of Jobs</returns>
        public IList<CS_Job> ListAllExpiredPresetJob()
        {
            DateTime date = DateTime.Now.Date;
            TimeSpan time = DateTime.Now.TimeOfDay;

            IList<CS_Job> returnList = _jobRepository.ListAll(e => e.Active
                                        && (e.CS_PresetInfo.Active && e.CS_PresetInfo.Date <= date && e.CS_PresetInfo.Time < time)
                                        && (e.CS_JobInfo.CS_Job_JobStatus.FirstOrDefault(j => j.Active).JobStatusId == (int)Globals.JobRecord.JobStatus.Preset
                                        || e.CS_JobInfo.CS_Job_JobStatus.FirstOrDefault(h => h.Active).JobStatusId == (int)Globals.JobRecord.JobStatus.PresetPurchase));


            return returnList.Where(e => !e.CS_CallLog.Any(j => j.Active
                                                            && j.CallTypeID == (int)Globals.CallEntry.CallType.LapsedPreset
                                                            && j.CallDate.Date >= e.CS_PresetInfo.Date
                                                            && j.CallDate.TimeOfDay > e.CS_PresetInfo.Time)).ToList();

        }

        /// <summary>
        /// Returns a list of jobs that had preset date overdue (based on current date)
        /// </summary>
        /// <param name="baseDate">Base date used to create the filter</param>
        /// <returns>Preset Notification VO list</returns>
        public IList<PresetNotificationVO> ListPresetNotification(DateTime baseDate)
        {
            IList<PresetNotificationVO> returnList = new List<PresetNotificationVO>();
            DateTime date = baseDate.Date;
            TimeSpan time = baseDate.TimeOfDay;

            IList<CS_Job> presetList = _jobRepository.ListAll(e => e.Active
                                    && (e.CS_PresetInfo.Active && e.CS_PresetInfo.Date <= date && e.CS_PresetInfo.Time < time)
                                    && (e.CS_JobInfo.CS_Job_JobStatus.FirstOrDefault(j => j.Active).JobStatusId == (int)Globals.JobRecord.JobStatus.Preset
                                    || e.CS_JobInfo.CS_Job_JobStatus.FirstOrDefault(h => h.Active).JobStatusId == (int)Globals.JobRecord.JobStatus.PresetPurchase));

            foreach (CS_Job job in presetList)
            {
                returnList.Add(
                    new PresetNotificationVO()
                    {
                        JobId = job.ID,
                        JobNumber = job.PrefixedNumber,
                        PresetDate = job.CS_PresetInfo.Date.Value,
                        PresetTime = (job.CS_PresetInfo.Time.HasValue ? job.CS_PresetInfo.Time.Value : new TimeSpan?())
                    });
            }

            return returnList;
        }


        /// <summary>
        /// Method to retrieve job start date and close date
        /// </summary>
        /// <param name="jobId">job id</param>
        /// <returns>entity job_jobstatus</returns>
        public CS_Job_JobStatus GetJobStartAndCloseDate(int jobId)
        {
            return _jobStatusHistoryRepository.Get(w => w.Active && w.JobID == jobId);
        }

        /// <summary>
        /// Lists all jobs that matches the SearchCriteria
        /// </summary>
        public IList<int?> ListJobBySearchCriteria(SearchCriteriaVO searchVO)
        {
            CS_JobRepository jobRepository = new CS_JobRepository(_jobRepository, _unitOfWork);

            return jobRepository.ListJobsBySearchCriteria(searchVO);
        }

        /// <summary>
        /// List all active Jobs for the Turnover Report
        /// </summary>
        /// <returns></returns>
        public IList<CS_View_TurnoverActiveReport> ListActiveTurnoverReport()
        {
            return _turnoverActiveReportRepository.ListAll();
        }

        #endregion

        #region [ Email ]
        /// <summary>
        /// Method to save notification of incoiving team or estimating email on the database
        /// </summary>
        /// <param name="receipts">email of receipts</param>
        /// <param name="subject">email subject</param>
        /// <param name="body">body subject</param>
        public void SaveEmailNotification(string receipts, string subject, string body)
        {
            if (!string.IsNullOrEmpty(receipts))
            {
                string[] lstReceipts = receipts.Split(';');

                for (int i = 0; i < lstReceipts.Count(); i++)
                {
                    _emailModel.SaveEmailList(lstReceipts[i], subject, body, "System", Globals.Security.SystemEmployeeID);
                }
            }
        }
        #endregion

        #region [ Save Job Record ]

        public void SaveJobData(bool saveInitialAdvise, int? jobCloningId, bool saveInitialEmailAdvise)
        {
            if (NewJob != null)
            {
                lock (locked)
                {
                    _initialAdviseChangeList = new Dictionary<string, object>();

                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted }))
                    {
                        try
                        {
                            NewJob = _jobRepository.Add(NewJob);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("There was an error saving the Job data. Please verify the content of the fields and try again.", ex);
                        }

                        if (NewJob != null)
                        {
                            SaveCustomerInfoData();
                            SaveDivisionData();
                            SavePresetInfoData();
                            SaveLostJobInfo();
                            SaveSpecialPricingInfo();
                            SaveJobInfo();
                            SaveJobStatusHistory();
                            SaveLocationInfo();
                            SaveJobDescription();
                            SaveScopeOfWorkData();
                            SavePermitInfo();
                            SavePhotoReport();
                            SaveEquipmentRequested();
                            SaveJobNumber();

                            if (saveInitialAdvise)
                                SaveInitialAdvise(saveInitialEmailAdvise);
                            if (jobCloningId.HasValue)
                                SaveCloningCallLog(jobCloningId.Value, NewJob.ID);

                            BuildScopeOfWorkMail();
                            BuildInterimBillMail();

                            scope.Complete();

                        }
                    }
                }
            }
        }

        private void SaveCloningCallLog(int cloningId, int newJobId)
        {
            CallLogModel model = new CallLogModel(_unitOfWork);

            CS_CallType callType = model.GetCallTypeByDescription("Job Cloning");
            CS_Job oldJob = _jobRepository.Get(e => e.ID == cloningId);

            CS_CallLog oldJobLog = new CS_CallLog()
            {
                JobID = cloningId,
                CallTypeID = callType.ID,
                PrimaryCallTypeId = callType.CS_PrimaryCallType_CallType.First(e => e.CallTypeID == callType.ID).PrimaryCallTypeID,
                CallDate = NewJob.CreationDate,
                Note = string.Format("Job # {0} has been cloned to Job # {1}", oldJob.NumberOrInternalTracking, NewJob.NumberOrInternalTracking),
                CreatedBy = NewJob.CreatedBy,
                CreationDate = NewJob.CreationDate,
                ModifiedBy = NewJob.ModifiedBy,
                ModificationDate = NewJob.ModificationDate,
                Active = true,
                ShiftTransferLog = false
            };

            _callLogRepository.Add(oldJobLog);

            CS_CallLog newJobLog = new CS_CallLog()
            {
                JobID = newJobId,
                CallTypeID = oldJobLog.CallTypeID,
                PrimaryCallTypeId = oldJobLog.PrimaryCallTypeId,
                CallDate = oldJobLog.CreationDate,
                Note = oldJobLog.Note,
                CreatedBy = oldJobLog.CreatedBy,
                CreationDate = oldJobLog.CreationDate,
                ModifiedBy = oldJobLog.ModifiedBy,
                ModificationDate = oldJobLog.ModificationDate,
                Active = true,
                ShiftTransferLog = false
            };

            _callLogRepository.Add(newJobLog);
        }

        public void SaveCustomerInfoData()
        {
            if (NewCustomer != null && NewJob != null)
            {
                try
                {
                    NewCustomer.JobId = NewJob.ID;
                    _customerInfoRepository.Add(NewCustomer);
                }
                catch (Exception ex)
                {
                    throw new Exception("There was an error saving the Customer Info data. Please verify the content of the fields and try again.", ex);
                }
            }
        }

        public void SaveDivisionData()
        {
            if (NewJob != null && NewJobDivision != null && NewJobDivision.Count > 0)
            {
                try
                {
                    foreach (CS_JobDivision jobDivision in NewJobDivision)
                    {
                        jobDivision.JobID = NewJob.ID;
                        jobDivision.CS_Division = null;
                        jobDivision.CS_Job = null;
                        _jobDivisionRepository.Add(jobDivision);

                        if (jobDivision.PrimaryDivision)
                            _divisionList.Insert(0, jobDivision);
                        else
                            _divisionList.Add(jobDivision);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("There was an error saving the Job Division data. Please verify the content of the fields and try again.", ex);
                }
            }
        }

        public void SavePresetInfoData()
        {
            if (NewJob != null && NewPresetInfo != null)
            {
                try
                {
                    NewPresetInfo.JobId = NewJob.ID;
                    _presetInfoRepository.Add(NewPresetInfo);
                }
                catch (Exception ex)
                {
                    throw new Exception("There was an error saving the Preset Info data. Please verify the content of the fields and try again.", ex);
                }
            }
        }

        public void SaveLostJobInfo()
        {
            if (NewJob != null && NewLostJobInfo != null)
            {
                try
                {
                    NewLostJobInfo.JobId = NewJob.ID;
                    _lostJobInfoRepository.Add(NewLostJobInfo);
                }
                catch (Exception ex)
                {
                    throw new Exception("There was an error saving the Lost Job Info data. Please verify the content of the fields and try again.", ex);
                }
            }
        }

        public void SaveSpecialPricingInfo()
        {
            if (NewSpecialPricing != null)
            {
                try
                {
                    NewSpecialPricing.JobId = NewJob.ID;
                    _specialPricingRepository.Add(NewSpecialPricing);
                }
                catch (Exception ex)
                {
                    throw new Exception("There was an error saving the data. Please verify the content of the fields and try again.", ex);
                }
            }
        }

        public void SaveJobInfo()
        {
            if (NewJob != null && NewJobInfo != null)
            {
                try
                {
                    NewJobInfo.JobID = NewJob.ID;
                    _jobInfoRepository.Add(NewJobInfo);
                }
                catch (Exception ex)
                {
                    throw new Exception("There was an error saving the Job Info data. Please verify the content of the fields and try again.", ex);
                }
            }
        }

        public void SaveJobStatusHistory()
        {
            if (NewJob != null && NewJobInfo != null)
            {
                try
                {
                    if (NewJobStatusHistory != null)
                    {
                        NewJobStatusHistory.JobID = NewJob.ID;
                        _jobStatusHistoryRepository.Add(NewJobStatusHistory);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("There was an error saving the Job Status data. Please verify the content of the fields and try again.", ex);
                }
            }
        }

        public void SaveLocationInfo()
        {
            if (NewJob != null && NewLocationInfo != null)
            {
                try
                {
                    NewLocationInfo.JobID = NewJob.ID;
                    _locationInfoRepository.Add(NewLocationInfo);
                }
                catch (Exception ex)
                {
                    throw new Exception("There was an error saving the Location Info data. Please verify the content of the fields and try again.", ex);
                }
            }
        }

        public void SaveJobDescription()
        {
            try
            {
                if (null != NewJob && null != NewJobDescription)
                {
                    NewJobDescription.JobId = NewJob.ID;
                    _jobDescriptionRepository.Add(NewJobDescription);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error saving the Job Description data. Please verify the content of the fields and try again.", ex);
            }
        }

        public void SaveScopeOfWorkData()
        {
            if (NewJob != null && NewScopeOfWork != null && NewScopeOfWork.Count > 0)
            {
                try
                {
                    foreach (CS_ScopeOfWork scopeOfWork in NewScopeOfWork)
                    {
                        scopeOfWork.JobId = NewJob.ID;
                        _scopeOfWorkRepository.Add(scopeOfWork);

                        if (scopeOfWork.ScopeChange)
                            _mailList.Add(scopeOfWork);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("There was an error saving the Scope of Work data. Please verify the content of the fields and try again.", ex);
                }
            }
        }

        public void SavePermitInfo()
        {
            if (NewJob != null && NewPermit != null && NewPermit.Count > 0)
            {
                try
                {
                    foreach (CS_JobPermit jobPermit in NewPermit)
                    {
                        CS_JobPermit jp = new CS_JobPermit();
                        jp.Active = jobPermit.Active;
                        jp.AgencyOperator = jobPermit.AgencyOperator;
                        jp.AgentOperatorName = jobPermit.AgentOperatorName;
                        jp.CreatedBy = jobPermit.CreatedBy;
                        jp.CreationDate = jobPermit.CreationDate;
                        jp.CreationID = jobPermit.CreationID;
                        jp.FileName = jobPermit.FileName;
                        jp.ID = jobPermit.ID;
                        jp.JobID = NewJob.ID;
                        jp.Location = jobPermit.Location;
                        jp.ModificationDate = jobPermit.ModificationDate;
                        jp.ModificationID = jobPermit.ModificationID;
                        jp.ModifiedBy = jobPermit.ModifiedBy;
                        jp.Number = jobPermit.Number;
                        jp.Path = jobPermit.Path;
                        jp.PermitDate = jobPermit.PermitDate;
                        jp.Type = jobPermit.Type;

                       // jobPermit.JobID = NewJob.ID;
                        _jobPermitRepository.Add(jp);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("There was an error saving the Job Permit data. Please verify the content of the fields and try again.", ex);
                }
            }
        }

        public void SavePhotoReport()
        {
            if (NewJob != null && NewPhotoReport != null && NewPhotoReport.Count > 0)
            {
                try
                {
                    foreach (CS_JobPhotoReport jobPhoto in NewPhotoReport)
                    {
                        jobPhoto.JobID = NewJob.ID;
                        _jobPhotoReportRepository.Add(jobPhoto);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("There was an error saving the Photo Report data. Please verify the content of the fields and try again.", ex);
                }
            }
        }

        public void SaveEquipmentRequested()
        {
            try
            {
                if (null != NewJob && null != NewRequestedEquipment)
                {
                    for (int i = 0; i < NewRequestedEquipment.Count; i++)
                    {
                        if (NewRequestedEquipment[i].ID.Equals(0))
                        {
                            _jobLocalEquipmentTypeRepository.Add(new CS_Job_LocalEquipmentType()
                            {
                                Active = true,
                                CreatedBy = NewJob.ModifiedBy,
                                CreationDate = NewRequestedEquipment[i].CreationDate,
                                Quantity = NewRequestedEquipment[i].Quantity,
                                SpecificEquipment = (((NewRequestedEquipment[i].LocalEquipmentTypeID == 24 || NewRequestedEquipment[i].LocalEquipmentTypeID == 29) && NewRequestedEquipment[i].Name.IndexOf('-') > -1) ? NewRequestedEquipment[i].Name.Substring(NewRequestedEquipment[i].Name.IndexOf('-') + 1).Trim() : null),
                                JobID = NewJob.ID,
                                LocalEquipmentTypeID = NewRequestedEquipment[i].LocalEquipmentTypeID,
                                ModificationDate = NewJob.ModificationDate,
                                ModifiedBy = NewJob.ModifiedBy
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error saving the Requested Equipment data. Please verify the content of the fields and try again.", ex);
            }
        }

        public void SaveJobNumber()
        {
            if (NewJob != null && NewJobInfo != null)
            {
                try
                {
                    // Generate Job Number if status is active or Preset Purchase
                    if (JobStatusID.Equals((int)Globals.JobRecord.JobStatus.Active) ||
                        JobStatusID.Equals((int)Globals.JobRecord.JobStatus.PresetPurchase))
                    {
                        NewJob.Number = GenerateJobNumber();
                        _jobRepository.Update(NewJob);
                    }
                    // Generate Non Job Number otherwise
                    else
                    {
                        NewJob.Internal_Tracking = GenerateNonJobNumber();
                        _jobRepository.Update(NewJob);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("There was an error generating a Job Number/Internal Tracking. Please verify the content of the fields and try again.", ex);
                }
            }
        }

        #endregion

        #region [ Update Job Record ]

        public IDictionary<string, object> UpdateJobData(bool saveInitialAdvise, bool saveInitialAdviseEmail)
        {
            List<CS_ScopeOfWork> mailList = new List<CS_ScopeOfWork>();
            List<CS_JobDivision> divisionList = new List<CS_JobDivision>();

            _initialAdviseChangeList = new Dictionary<string, object>();

            lock (locked)
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted }))
                {
                    try
                    {
                        CS_Job oldJob = _jobRepository.Get(e => e.Active && e.ID == NewJob.ID);

                        if (null != oldJob)
                        {
                            NewJob.Internal_Tracking = oldJob.Internal_Tracking;
                            NewJob.Number = oldJob.Number;

                            NewJob.CS_JobInfo = NewJobInfo;

                            NewJob.IsBilling = oldJob.IsBilling;

                            //NewJob.CS_JobInfo.CS_Job_JobStatus.Add(NewJobStatusHistory);
                        }

                        JobLifecycle jobLifeCycle = new JobLifecycle();

                        IDictionary<string, object> inputs = new Dictionary<string, object>();

                        inputs.Add("oldJob", oldJob);
                        inputs.Add("oldStatusId", oldJob.CS_JobInfo.LastJobStatusID);
                        inputs.Add("updateStatusId", JobStatusID);

                        NewJob.CS_CustomerInfo = NewCustomer;

                        inputs.Add("updateJob", NewJob);
                        inputs.Add("Username", NewJob.ModifiedBy);
                        inputs.Add("customerId", NewJob.CS_CustomerInfo.CustomerId);

                        IDictionary<string, object> outputs = WorkflowInvoker.Invoke(jobLifeCycle, inputs);

                        IDictionary<string, object> returnDictionary = new Dictionary<string, object>();

                        switch (outputs["Flow"].ToString())
                        {
                            case "Active":
                                NewJob.IsBilling = true;

                                UpdateJob(saveInitialAdvise, true, saveInitialAdviseEmail);

                                if ((bool)outputs["ResourceConversion"])
                                    returnDictionary.Add(new KeyValuePair<string, object>("ResourceConversion", true));
                                else
                                    returnDictionary.Add(new KeyValuePair<string, object>("ResourceConversion", false));

                                break;
                            case "Closed":
                                if (!(bool)outputs["HasResources"])
                                {
                                    if ((bool)outputs["HasTemporaryCustomer"])
                                    {
                                        NewJobStatusHistory.JobStatusId = (int)Globals.JobRecord.JobStatus.ClosedHold;
                                        returnDictionary.Add(new KeyValuePair<string, object>("HasTemporaryCustomer", true));
                                        returnDictionary.Add(new KeyValuePair<string, object>("Message", outputs["Message"].ToString()));
                                    }
                                    UpdateJob(saveInitialAdvise, true, saveInitialAdviseEmail);
                                }
                                else
                                {
                                    returnDictionary.Add(new KeyValuePair<string, object>("HasResources", true));
                                    returnDictionary.Add(new KeyValuePair<string, object>("Message", outputs["Message"].ToString()));
                                }
                                break;
                            case "Cancelled/Lost":
                                if (!(bool)outputs["HasResources"])
                                    UpdateJob(saveInitialAdvise, true, saveInitialAdviseEmail);
                                else
                                {
                                    returnDictionary.Add(new KeyValuePair<string, object>("HasResources", true));
                                    returnDictionary.Add(new KeyValuePair<string, object>("Message", outputs["Message"].ToString()));
                                }

                                break;
                            case "None":
                                UpdateJob(saveInitialAdvise, false, saveInitialAdviseEmail);
                                break;
                            case "Not Allowed":
                                CS_JobStatus JobStatus = GetJobStatus(JobStatusID);

                                returnDictionary.Add(new KeyValuePair<string, object>("OperationNotAllowed", true));
                                returnDictionary.Add(new KeyValuePair<string, object>("Message", string.Format(outputs["Message"].ToString(), JobStatus.Description)));
                                break;
                            default:
                                if (!outputs.ContainsKey("HasAssignedResources"))
                                    UpdateJob(saveInitialAdvise, true, saveInitialAdviseEmail);
                                else if (!(bool)outputs["HasAssignedResources"])
                                    UpdateJob(saveInitialAdvise, true, saveInitialAdviseEmail);
                                else
                                {
                                    returnDictionary.Add(new KeyValuePair<string, object>("HasAssignedResources", true));
                                    returnDictionary.Add(new KeyValuePair<string, object>("Message", outputs["Message"].ToString()));
                                }
                                break;
                        }

                        scope.Complete();

                        return returnDictionary;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("There was an error saving the Job data. Please verify the content of the fields and try again.", ex);
                    }
                }
            }
        }

        public void UpdateJobStatus(int jobId)
        {
            IList<CS_Job_JobStatus> jobStatusHistory = GetJobStatusHistoryByJobId(jobId);
            CS_Job_JobStatus lastJobStatus = jobStatusHistory.Where(e => e.Active).FirstOrDefault();

            if (lastJobStatus != null && lastJobStatus.JobStatusId != NewJobStatusHistory.JobStatusId)
            {
                foreach (var jobStatus in jobStatusHistory)
                {
                    jobStatus.Active = false;
                }
                _jobStatusHistoryRepository.UpdateList(jobStatusHistory);

                // Fix Start/Close date and time
                if (NewJobStatusHistory.JobStatusId == (int)Globals.JobRecord.JobStatus.Active)
                    NewJobStatusHistory.JobStartDate = DateTime.Now;
                else if (NewJobStatusHistory.JobStatusId == (int)Globals.JobRecord.JobStatus.Closed)
                    NewJobStatusHistory.JobCloseDate = DateTime.Now;

                _jobStatusHistoryRepository.Add(NewJobStatusHistory);
                _generatesInitialAdvise = true;
                _initialAdviseChangeList.Add("JobStatus", true);
            }
            else if (lastJobStatus.JobStatusId == NewJobStatusHistory.JobStatusId)
            {
                lastJobStatus.Active = NewJobStatusHistory.Active;
                lastJobStatus.ModificationDate = NewJobStatusHistory.ModificationDate;
                lastJobStatus.ModifiedBy = NewJobStatusHistory.ModifiedBy;

                _jobStatusHistoryRepository.Update(lastJobStatus);
            }
        }

        public IList<CS_Job_JobStatus> GetJobStatusHistoryByJobId(int jobId)
        {
            IList<CS_Job_JobStatus> jobStatusHistory = _jobStatusHistoryRepository.ListAll(e => e.JobID == jobId);
            return jobStatusHistory;
        }

        private void UpdateJob(bool saveInitialAdvise, bool jobStatusChanged, bool saveInitialEmailAdvise)
        {
            _jobRepository.Update(NewJob);

            if (jobStatusChanged)
                UpdateJobNumber();

            UpdateCustomerInfoData();
            UpdateJobDivision();
            UpdatePresetInfo();
            UpdateLostJobInfo();
            UpdateSpecialPricingInfo();
            UpdateJobInfo();

            if (jobStatusChanged)
                UpdateJobStatus(NewJob.ID);

            UpdateLocationInfo();
            UpdateJobDescription();
            UpdateScopeOfWork();
            UpdateJobPermit();
            UpdatePhotoReport();
            UpdateRequestedEquipment();

            if (saveInitialAdvise)
                SaveInitialAdvise(saveInitialEmailAdvise);
            else
                UpdateInitialAdvise();

            BuildScopeOfWorkMailWhenUpdate();
        }

        public void UpdateCustomerInfoData()
        {
            try
            {
                if (null != NewJob && null != NewCustomer)
                {
                    CS_CustomerInfo oldCustomerInfo = _customerInfoRepository.Get(e => e.Active && e.JobId == NewCustomer.JobId);
                    if (oldCustomerInfo.CustomerId != NewCustomer.CustomerId)
                    {
                        _generatesInitialAdvise = true;
                        _initialAdviseChangeList.Add("Customer", true);
                    }
                    if (oldCustomerInfo.DivisionId != NewCustomer.DivisionId)
                    {
                        _generatesInitialAdvise = true;
                        _initialAdviseChangeList.Add("Division", true);
                    }
                    if (oldCustomerInfo.PocEmployeeId != NewCustomer.PocEmployeeId)
                    {
                        _generatesInitialAdvise = true;
                        _initialAdviseChangeList.Add("PocEmployeeId", true);
                    }
                    if (oldCustomerInfo.InitialCustomerContactId != NewCustomer.InitialCustomerContactId)
                    {
                        _generatesInitialAdvise = true;
                        _initialAdviseChangeList.Add("InitialCustomerContactId", true);
                    }

                    _customerInfoRepository.Update(NewCustomer);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error saving the Customer Info data. Please verify the content of the fields and try again.", ex);
            }
        }

        public void UpdateJobDivision()
        {
            try
            {
                if (null != NewJob)
                {
                    IList<CS_JobDivision> oldJobDivisionList = _jobDivisionRepository.ListAll(e => e.Active && e.JobID == NewJob.ID);
                    IList<CS_JobDivision> removeJobDivisionList = new List<CS_JobDivision>();

                    foreach (CS_JobDivision oldJobDivision in oldJobDivisionList)
                    {
                        bool exists = false;
                        foreach (CS_JobDivision jobDivision in NewJobDivision)
                        {
                            if (oldJobDivision.ID.Equals(jobDivision.ID))
                            {
                                exists = true;
                                break;
                            }
                        }
                        if (!exists)
                        {
                            removeJobDivisionList.Add(
                                new CS_JobDivision()
                                {
                                    Active = false,
                                    CreatedBy = oldJobDivision.CreatedBy,
                                    CreationDate = oldJobDivision.CreationDate,
                                    ID = oldJobDivision.ID,
                                    JobID = oldJobDivision.JobID,
                                    ModificationDate = NewJob.ModificationDate,
                                    ModifiedBy = NewJob.ModifiedBy,
                                    DivisionID = oldJobDivision.DivisionID,
                                    PrimaryDivision = false
                                });
                        }
                    }
                    _jobDivisionRepository.UpdateList(removeJobDivisionList);

                    foreach (CS_JobDivision jobDivision in NewJobDivision)
                    {
                        if (jobDivision.ID.Equals(0))
                        {
                            _jobDivisionRepository.Add(jobDivision);
                            _generatesInitialAdvise = true;
                            if (!_initialAdviseChangeList.ContainsKey("Division"))
                                _initialAdviseChangeList.Add("Division", true);
                        }
                        else
                        {
                            CS_JobDivision oldJobDivision = _jobDivisionRepository.Get(e => e.Active && e.ID == jobDivision.ID);
                            if (null != oldJobDivision)
                            {
                                if (oldJobDivision.PrimaryDivision != jobDivision.PrimaryDivision)
                                {
                                    _generatesInitialAdvise = true;
                                    if (!_initialAdviseChangeList.ContainsKey("Division"))
                                        _initialAdviseChangeList.Add("Division", true);
                                }

                                oldJobDivision.ModificationDate = jobDivision.ModificationDate;
                                oldJobDivision.ModifiedBy = jobDivision.ModifiedBy;
                                oldJobDivision.PrimaryDivision = jobDivision.PrimaryDivision;
                                oldJobDivision.DivisionID = jobDivision.DivisionID;

                                _jobDivisionRepository.Update(oldJobDivision);
                            }
                        }

                        if (jobDivision.PrimaryDivision)
                            _divisionList.Insert(0, jobDivision);
                        else
                            _divisionList.Add(jobDivision);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error saving the Job Division data. Please verify the content of the fields and try again.", ex);
            }
        }

        public void UpdatePresetInfo()
        {
            try
            {
                if (null != NewJob && null != NewPresetInfo)
                {
                    CS_PresetInfo oldPresetInfo = _presetInfoRepository.Get(e => e.Active && e.JobId == NewPresetInfo.JobId);
                    if (null == oldPresetInfo)
                    {
                        if (NewPresetInfo.Date.HasValue)
                        {
                            _generatesInitialAdvise = true;
                            _initialAdviseChangeList.Add("PresetDate", true);
                        }

                        _presetInfoRepository.Add(NewPresetInfo);
                    }
                    else
                    {
                        if (oldPresetInfo.Date != NewPresetInfo.Date)
                        {
                            _generatesInitialAdvise = true;
                            _initialAdviseChangeList.Add("PresetDate", true);
                        }
                        if (oldPresetInfo.Instructions != NewPresetInfo.Instructions)
                        {
                            _generatesInitialAdvise = true;
                            _initialAdviseChangeList.Add("PresetInstructions", true);
                        }

                        _presetInfoRepository.Update(NewPresetInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error saving the Preset Info data. Please verify the content of the fields and try again.", ex);
            }
        }

        public void UpdateLostJobInfo()
        {
            try
            {
                if (null != NewLostJobInfo)
                {
                    CS_LostJobInfo oldLostJobInfo = _lostJobInfoRepository.Get(e => e.Active && e.JobId == NewLostJobInfo.JobId);
                    if (null == oldLostJobInfo)
                        _lostJobInfoRepository.Add(NewLostJobInfo);
                    else
                        _lostJobInfoRepository.Update(NewLostJobInfo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error saving the Lost Job Info data. Please verify the content of the fields and try again.", ex);
            }
        }

        public void UpdateSpecialPricingInfo()
        {
            try
            {
                if (null != NewJob && null != NewSpecialPricing)
                {
                    CS_SpecialPricing oldSpecialPricing = _specialPricingRepository.Get(e => e.Active && e.JobId == NewSpecialPricing.JobId);
                    if (null == oldSpecialPricing)
                        _specialPricingRepository.Add(NewSpecialPricing);
                    else
                        _specialPricingRepository.Update(NewSpecialPricing);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error saving the Special Pricing data. Please verify the content of the fields and try again.", ex);
            }
        }

        public void UpdateJobInfo()
        {
            try
            {
                if (null != NewJob && null != NewJobInfo)
                {
                    CS_JobInfo oldJobInfo = _jobInfoRepository.Get(e => e.Active && e.JobID == NewJob.ID);
                    if (null != oldJobInfo)
                    {
                        if (oldJobInfo.JobCategoryID != NewJobInfo.JobCategoryID)
                        {
                            _generatesInitialAdvise = true;
                            _initialAdviseChangeList.Add("JobCategoryID", true);
                        }
                        if (oldJobInfo.JobTypeID != NewJobInfo.JobTypeID)
                        {
                            _generatesInitialAdvise = true;
                            _initialAdviseChangeList.Add("JobTypeID", true);
                        }
                        if (oldJobInfo.JobActionID != NewJobInfo.JobActionID)
                        {
                            _generatesInitialAdvise = true;
                            _initialAdviseChangeList.Add("JobActionID", true);
                        }
                    }

                    _jobInfoRepository.Update(NewJobInfo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error saving the Job Info data. Please verify the content of the fields and try again.", ex);
            }
        }

        public void UpdateLocationInfo()
        {
            try
            {
                if (null != NewJob && null != NewLocationInfo)
                {
                    CS_LocationInfo oldLocationInfo = _locationInfoRepository.Get(e => e.Active && e.JobID == NewLocationInfo.JobID);
                    if (oldLocationInfo.CountryID != NewLocationInfo.CountryID)
                    {
                        _generatesInitialAdvise = true;
                        _initialAdviseChangeList.Add("CountryID", true);
                    }
                    if (oldLocationInfo.StateID != NewLocationInfo.StateID)
                    {
                        _generatesInitialAdvise = true;
                        _initialAdviseChangeList.Add("State", true);
                    }
                    if (oldLocationInfo.CityID != NewLocationInfo.CityID)
                    {
                        _generatesInitialAdvise = true;
                        _initialAdviseChangeList.Add("City", true);
                    }
                    if (oldLocationInfo.ZipCodeId != NewLocationInfo.ZipCodeId)
                    {
                        _generatesInitialAdvise = true;
                        _initialAdviseChangeList.Add("ZipCodeId", true);
                    }

                    _locationInfoRepository.Update(NewLocationInfo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error saving the Location Info data. Please verify the content of the fields and try again.", ex);
            }
        }

        public void UpdateJobDescription()
        {
            try
            {
                if (null != NewJob && null != NewJobDescription)
                {
                    CS_JobDescription oldJobDescription = _jobDescriptionRepository.Get(e => e.Active && e.JobId == NewJobDescription.JobId);
                    if (null != oldJobDescription)
                    {
                        if (oldJobDescription.NumberEngines != NewJobDescription.NumberEngines)
                        {
                            _generatesInitialAdvise = true;
                            _initialAdviseChangeList.Add("NumberEngines", true);
                        }
                        if (oldJobDescription.NumberLoads != NewJobDescription.NumberLoads)
                        {
                            _generatesInitialAdvise = true;
                            _initialAdviseChangeList.Add("NumberLoads", true);
                        }
                        if (oldJobDescription.NumberEmpties != NewJobDescription.NumberEmpties)
                        {
                            _generatesInitialAdvise = true;
                            _initialAdviseChangeList.Add("NumberEmpties", true);
                        }
                        if (oldJobDescription.Hazmat != NewJobDescription.Hazmat)
                        {
                            _generatesInitialAdvise = true;
                            _initialAdviseChangeList.Add("Hazmat", true);
                        }
                        if (oldJobDescription.Lading != NewJobDescription.Lading)
                        {
                            _generatesInitialAdvise = true;
                            _initialAdviseChangeList.Add("Lading", true);
                        }
                        if (oldJobDescription.UnNumber != NewJobDescription.UnNumber)
                        {
                            _generatesInitialAdvise = true;
                            _initialAdviseChangeList.Add("UnNumber", true);
                        }
                        if (oldJobDescription.STCCInfo != NewJobDescription.STCCInfo)
                        {
                            _generatesInitialAdvise = true;
                            _initialAdviseChangeList.Add("STCCInfo", true);
                        }
                    }

                    _jobDescriptionRepository.Update(NewJobDescription);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error saving the Job Description data. Please verify the content of the fields and try again.", ex);
            }
        }

        public void UpdateScopeOfWork()
        {
            try
            {
                if (null != NewJob && null != NewScopeOfWork)
                {
                    foreach (CS_ScopeOfWork scopeOfWork in NewScopeOfWork)
                    {
                        if (scopeOfWork.ID.Equals(0))
                        {
                            _scopeOfWorkRepository.Add(scopeOfWork);

                            _generatesInitialAdvise = true;
                            if (_initialAdviseChangeList.Any(e => e.Key == "ScopeOfWork"))
                                _initialAdviseChangeList.Add("ScopeOfWork", true);

                            if (scopeOfWork.ScopeChange)
                                _mailList.Add(scopeOfWork);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error saving the Scope of Work data. Please verify the content of the fields and try again.", ex);
            }
        }

        public void UpdateJobPermit()
        {
            try
            {
                if (null != NewJob)
                {
                    IList<CS_JobPermit> oldPermitList = _jobPermitRepository.ListAll(e => e.Active && e.JobID == NewJob.ID);
                    IList<CS_JobPermit> updatePermitList = new List<CS_JobPermit>();

                    foreach (CS_JobPermit oldJobPermit in oldPermitList)
                    {
                        bool exists = false;
                        foreach (CS_JobPermit jobPermit in NewPermit)
                        {
                            if (oldJobPermit.ID.Equals(jobPermit.ID))
                            {
                                exists = true;
                                break;
                            }
                        }
                        if (!exists)
                        {
                            updatePermitList.Add(
                                new CS_JobPermit()
                                {
                                    Active = false,
                                    CreatedBy = oldJobPermit.CreatedBy,
                                    CreationDate = oldJobPermit.CreationDate,
                                    ID = oldJobPermit.ID,
                                    JobID = oldJobPermit.JobID,
                                    Location = oldJobPermit.Location,
                                    FileName = oldJobPermit.FileName,
                                    ModificationDate = NewJob.ModificationDate,
                                    ModifiedBy = NewJob.ModifiedBy,
                                    Number = oldJobPermit.Number,
                                    Path = oldJobPermit.Path,
                                    Type = oldJobPermit.Type
                                });
                        }
                    }

                    foreach (CS_JobPermit jobPermit in NewPermit)
                    {
                        if (jobPermit.ID.Equals(0))
                        {
                            jobPermit.CS_JobPermitType = null;
                            _jobPermitRepository.Add(jobPermit);
                        }
                    }

                    foreach (CS_JobPermit jobPermit in updatePermitList)
                        _jobPermitRepository.Update(jobPermit);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error saving the Job Permit data. Please verify the content of the fields and try again.", ex);
            }
        }

        public void UpdatePhotoReport()
        {
            try
            {
                if (null != NewJob)
                {
                    IList<CS_JobPhotoReport> oldPhotoReportList = _jobPhotoReportRepository.ListAll(e => e.Active && e.JobID == NewJob.ID);
                    IList<CS_JobPhotoReport> updateReportList = new List<CS_JobPhotoReport>();

                    foreach (CS_JobPhotoReport oldPhotoReport in oldPhotoReportList)
                    {
                        bool exists = false;
                        foreach (CS_JobPhotoReport jobPhotoReport in NewPhotoReport)
                        {
                            if (oldPhotoReport.ID.Equals(jobPhotoReport.ID))
                            {
                                exists = true;
                                break;
                            }
                        }
                        if (!exists)
                        {
                            updateReportList.Add(
                                new CS_JobPhotoReport()
                                {
                                    Active = false,
                                    CreatedBy = oldPhotoReport.CreatedBy,
                                    CreationDate = oldPhotoReport.CreationDate,
                                    Description = oldPhotoReport.Description,
                                    ID = oldPhotoReport.ID,
                                    JobID = oldPhotoReport.JobID,
                                    FileName = oldPhotoReport.FileName,
                                    ModificationDate = NewJob.ModificationDate,
                                    ModifiedBy = NewJob.ModifiedBy,
                                    Path = oldPhotoReport.Path
                                });
                        }
                    }

                    foreach (CS_JobPhotoReport jobPhoto in NewPhotoReport)
                    {
                        if (jobPhoto.ID.Equals(0))
                            _jobPhotoReportRepository.Add(jobPhoto);
                    }
                    foreach (CS_JobPhotoReport jobPhoto in updateReportList)
                        _jobPhotoReportRepository.Update(jobPhoto);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error saving the Photo Report data. Please verify the content of the fields and try again.", ex);
            }
        }

        public void UpdateRequestedEquipment()
        {
            try
            {
                if (null != NewJob)
                {
                    bool hasChanges = false;
                    bool hasRemoved = false;
                    List<int> changedIndexes = new List<int>();
                    List<string> removedList = new List<string>();
                    IList<CS_Job_LocalEquipmentType> oldRequestedEquipmentList = _jobLocalEquipmentTypeRepository.ListAll(e => e.Active && e.JobID == NewJob.ID, "CS_LocalEquipmentType");
                    IList<CS_Job_LocalEquipmentType> removeRequestedEquipmentList = new List<CS_Job_LocalEquipmentType>();
                    IList<CS_Job_LocalEquipmentType> updateRequestedEquipmentList = new List<CS_Job_LocalEquipmentType>();

                    for (int i = 0; i < oldRequestedEquipmentList.Count; i++)
                    {
                        bool exists = false;

                        int j = 0;
                        for (j = 0; j < NewRequestedEquipment.Count; j++)
                        {
                            if (oldRequestedEquipmentList[i].ID.Equals(NewRequestedEquipment[j].ID))
                            {
                                exists = true;
                                break;
                            }
                        }

                        if (!exists)
                        {
                            hasRemoved = true;
                            removedList.Add(string.Format("{0} - {1} (Removed)", oldRequestedEquipmentList[i].CS_LocalEquipmentType.Name, oldRequestedEquipmentList[i].Quantity));
                            removeRequestedEquipmentList.Add(
                                new CS_Job_LocalEquipmentType()
                                {
                                    Active = false,
                                    CreatedBy = oldRequestedEquipmentList[i].CreatedBy,
                                    CreationDate = oldRequestedEquipmentList[i].CreationDate,
                                    SpecificEquipment = (((NewRequestedEquipment[i].LocalEquipmentTypeID == 24 || NewRequestedEquipment[i].LocalEquipmentTypeID == 29) && NewRequestedEquipment[i].Name.IndexOf('-') > -1) ? NewRequestedEquipment[i].Name.Substring(NewRequestedEquipment[i].Name.IndexOf('-') + 1).Trim() : null),
                                    Quantity = oldRequestedEquipmentList[i].Quantity,
                                    ID = oldRequestedEquipmentList[i].ID,
                                    JobID = oldRequestedEquipmentList[i].JobID,
                                    LocalEquipmentTypeID = oldRequestedEquipmentList[i].LocalEquipmentTypeID,
                                    ModificationDate = NewJob.ModificationDate,
                                    ModifiedBy = NewJob.ModifiedBy,
                                });
                        }
                        else
                        {
                            if (NewRequestedEquipment[j].Quantity != oldRequestedEquipmentList[i].Quantity)
                            {
                                hasChanges = true;
                                changedIndexes.Add(j);
                            }

                            updateRequestedEquipmentList.Add(
                                new CS_Job_LocalEquipmentType()
                                {
                                    Active = true,
                                    CreatedBy = NewRequestedEquipment[j].CreatedBy,
                                    CreationDate = NewRequestedEquipment[j].CreationDate,
                                    SpecificEquipment = (((NewRequestedEquipment[i].LocalEquipmentTypeID == 24 || NewRequestedEquipment[i].LocalEquipmentTypeID == 29) && NewRequestedEquipment[i].Name.IndexOf('-') > -1) ? NewRequestedEquipment[i].Name.Substring(NewRequestedEquipment[i].Name.IndexOf('-') + 1).Trim() : null),
                                    Quantity = NewRequestedEquipment[j].Quantity,
                                    ID = NewRequestedEquipment[j].ID,
                                    JobID = NewJob.ID,
                                    LocalEquipmentTypeID = NewRequestedEquipment[j].LocalEquipmentTypeID,
                                    ModificationDate = NewJob.ModificationDate,
                                    ModifiedBy = NewJob.ModifiedBy,
                                });
                        }
                    }

                    for (int i = 0; i < NewRequestedEquipment.Count; i++)
                    {
                        if (NewRequestedEquipment[i].ID.Equals(0))
                        {
                            hasChanges = true;
                            changedIndexes.Add(i);
                            _jobLocalEquipmentTypeRepository.Add(new CS_Job_LocalEquipmentType()
                            {
                                Active = true,
                                CreatedBy = NewJob.ModifiedBy,
                                CreationDate = NewRequestedEquipment[i].CreationDate,
                                SpecificEquipment = (((NewRequestedEquipment[i].LocalEquipmentTypeID == 24 || NewRequestedEquipment[i].LocalEquipmentTypeID == 29) && NewRequestedEquipment[i].Name.IndexOf('-') > -1) ? NewRequestedEquipment[i].Name.Substring(NewRequestedEquipment[i].Name.IndexOf('-') + 1).Trim() : null),
                                Quantity = NewRequestedEquipment[i].Quantity,
                                JobID = NewJob.ID,
                                LocalEquipmentTypeID = NewRequestedEquipment[i].LocalEquipmentTypeID,
                                ModificationDate = NewJob.ModificationDate,
                                ModifiedBy = NewJob.ModifiedBy
                            });
                        }
                    }

                    if (removeRequestedEquipmentList.Count > 0)
                        _jobLocalEquipmentTypeRepository.UpdateList(removeRequestedEquipmentList);
                    if (updateRequestedEquipmentList.Count > 0)
                        _jobLocalEquipmentTypeRepository.UpdateList(updateRequestedEquipmentList);

                    if (hasChanges)
                    {
                        _generatesInitialAdvise = true;
                        _initialAdviseChangeList.Add("EquipmentRequested", changedIndexes);
                    }
                    if (hasRemoved)
                    {
                        _generatesInitialAdvise = true;
                        _initialAdviseChangeList.Add("EquipmentRequestedRemoved", removedList);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error saving the Requested Equipment data. Please verify the content of the fields and try again.", ex);
            }
        }

        public void UpdateJobNumber()
        {
            try
            {
                // Generate Job Number if status changed to active
                CS_JobInfo oldJobInfo = _jobInfoRepository.Get(e => e.Active && e.JobID == NewJobInfo.JobID);
                if ((JobStatusID.Equals((int)Globals.JobRecord.JobStatus.Active) ||
                     JobStatusID.Equals((int)Globals.JobRecord.JobStatus.PresetPurchase)) &&
                    oldJobInfo.LastJobStatusID != JobStatusID &&
                    string.IsNullOrEmpty(NewJob.Number))
                {
                    NewJob.Number = GenerateJobNumber();
                    _jobRepository.Update(NewJob);
                }
                // 06/23/2011 - ACCORDING TO CYNTHIA, THIS WON'T HAPPEN ANYMORE. ONCE ACTIVE, IT WILL KEEP JOB NUMBER
                // Generate Non Job Number if status changed from active to another one
                //else if ((oldJobInfo.LastJobStatusID.Equals((int)Globals.JobRecord.JobStatus.Active) ||
                //    oldJobInfo.LastJobStatusID.Equals((int)Globals.JobRecord.JobStatus.PresetPurchase)) &&
                //    JobStatusID != oldJobInfo.LastJobStatusID &&
                //    string.IsNullOrEmpty(NewJob.Internal_Tracking))
                //{
                //    NewJob.Internal_Tracking = GenerateNonJobNumber();
                //    _jobRepository.Update(NewJob);
                //}
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error generating a Job Number/Internal Tracking. Please verify the content of the fields and try again.", ex);
            }
        }

        #endregion

        #region [ Scope of Work Email ]

        public void BuildScopeOfWorkMail()
        {
            _mailScope = new StringBuilder();
            try
            {
                for (int i = 0; i < _mailList.Count; i++)
                {
                    _mailScope.AppendLine("Job #: " + NewJob.ID);
                    _mailScope.AppendLine("Proposal #: ");
                    _mailScope.AppendLine("Company: " + _customerRepository.Get(e => e.Active && e.ID == NewCustomer.CustomerId).Name);
                    AppendDivisionScopeOfWorkMail();
                    _mailScope.AppendLine("Resources: ");
                    _mailScope.AppendLine("Job start date: " + NewJobStatusHistory.JobStartDate.ToString());
                    _mailScope.AppendLine("Job type: " + _jobTypeRepository.Get(e => e.Active && e.ID == NewJobInfo.JobTypeID).Description);
                    _mailScope.AppendLine("Scope of work: " + _mailList[i].ScopeOfWork);
                }

                // TODO: E-mail needs to be sent-out when scope of work changes and is a bid record (change that when the BID user story goes on)
                //if (_mailScope.Length > 0)
                //    MailUtility.SendMail("cburton@hulcher.com", "rbrandao@hulcher.com", "ccarvalho@hulcher.com", _mailScope.ToString(), "BID Job - Scope Of Work Change", false, null);
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error creating the Scope Changed e-mail.", ex);
            }
        }

        public void BuildScopeOfWorkMailWhenUpdate()
        {
            try
            {
                if (_generatesInitialAdvise && _mailList.Count.Equals(0))
                {
                    _mailScope.AppendLine("Job #: " + NewJob.ID);
                    _mailScope.AppendLine("Proposal #: ");
                    _mailScope.AppendLine("Company: " + _customerRepository.Get(e => e.Active && e.ID == NewCustomer.CustomerId).Name);
                    for (int j = 0; j < _divisionList.Count; j++)
                    {
                        int divisionId = _divisionList[j].DivisionID;
                        if (j == 0)
                        {

                            _mailScope.AppendLine("Division: " + _divisionRepository.Get(e => e.Active && e.ID == divisionId).Name);
                        }
                        else
                        {
                            _mailScope.AppendLine("          " + _divisionRepository.Get(e => e.Active && e.ID == divisionId).Name);
                        }
                    }
                    _mailScope.AppendLine("Resources: ");
                    _mailScope.AppendLine("Job start date: " + NewJobStatusHistory.JobStartDate);
                    _mailScope.AppendLine("Job type: " + _jobTypeRepository.Get(e => e.Active && e.ID == NewJobInfo.JobTypeID).Description);
                    _mailScope.AppendLine("Scope of work: ");
                    if (null != NewLostJobInfo)
                    {
                        if (NewLostJobInfo.HSIRepEmployeeID.HasValue)
                        {
                            CS_Employee emp = _employeeRepository.Get(e => e.Active && e.ID == NewLostJobInfo.HSIRepEmployeeID.Value);
                            _mailScope.AppendLine("HSI REP: " + emp.Name + "," + emp.FirstName);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < _mailList.Count; i++)
                    {
                        _mailScope.AppendLine("Job #: " + NewJob.ID);
                        _mailScope.AppendLine("Proposal #: ");
                        _mailScope.AppendLine("Company: " + _customerRepository.Get(e => e.Active && e.ID == NewCustomer.CustomerId).Name);
                        for (int j = 0; j < _divisionList.Count; j++)
                        {
                            int divisionId = _divisionList[j].DivisionID;
                            if (j == 0)
                            {
                                _mailScope.AppendLine("Division: " + _divisionRepository.Get(e => e.Active && e.ID == divisionId).Name);
                            }
                            else
                            {
                                _mailScope.AppendLine("          " + _divisionRepository.Get(e => e.Active && e.ID == divisionId).Name);
                            }
                        }
                        _mailScope.AppendLine("Resources: ");
                        _mailScope.AppendLine("Job start date: " + NewJobStatusHistory.JobStartDate.ToString());
                        _mailScope.AppendLine("Job type: " + _jobTypeRepository.Get(e => e.Active && e.ID == NewJobInfo.JobTypeID).Description);
                        _mailScope.AppendLine("Scope of work: " + _mailList[i].ScopeOfWork);
                        CS_LostJobInfo lostJobInfo = _lostJobInfoRepository.Get(e => e.Active && e.JobId == NewJob.ID);
                        if (null != lostJobInfo)
                        {
                            if (lostJobInfo.HSIRepEmployeeID.HasValue)
                            {
                                CS_Employee emp = _employeeRepository.Get(e => e.Active && e.ID == lostJobInfo.HSIRepEmployeeID.Value);
                                if (null != emp)
                                    _mailScope.AppendLine("HSI REP: " + emp.Name + "," + emp.FirstName);
                            }
                        }
                    }
                }

                // TODO: E-mail needs to be sent-out when scope of work changes and is a bid record (change that when the BID user story goes on)
                //if (_mailScope.Length > 0)
                //    MailUtility.SendMail("cburton@hulcher.com", "rbrandao@hulcher.com", "ccarvalho@hulcher.com", _mailScope.ToString(), "BID Job - Scope Of Work Change", false, null);
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error creating the Scope Changed e-mail.", ex);
            }
        }

        private void AppendDivisionScopeOfWorkMail()
        {
            for (int j = 0; j < _divisionList.Count; j++)
            {
                int divisionId = _divisionList[j].DivisionID;
                if (j == 0)
                {
                    _mailScope.AppendLine("Division: " + _divisionRepository.Get(e => e.Active && e.ID == divisionId).Name);
                }
                else
                {
                    _mailScope.AppendLine("          " + _divisionRepository.Get(e => e.Active && e.ID == divisionId).Name);
                }
            }
        }

        private void AppendHsiRepInfoInScopeOfWorkMail()
        {
            CS_LostJobInfo jobInfo = _lostJobInfoRepository.Get(e => e.Active && e.JobId == NewJob.ID);

            int? hsiEmployeeId = jobInfo.HSIRepEmployeeID;
            if (hsiEmployeeId.HasValue)
            {
                CS_Employee emp = _employeeRepository.Get(e => e.Active && e.ID == hsiEmployeeId.Value);
                _mailScope.AppendLine("HSI REP: " + emp.Name + "," + emp.FirstName);
            }
        }

        #endregion

        #region [ Interim Bill Email ]

        public void BuildInterimBillMail()
        {
            _mailBillingBuilder = new StringBuilder();
            try
            {
                if (NewJobInfo != null)
                {
                    if (NewJobInfo.InterimBill)
                    {
                        if (NewJobInfo.FrequencyID.HasValue)
                            _mailBillingBuilder.AppendLine("Frequency: " + _frequencyRepository.Get(e => e.Active && e.ID == NewJobInfo.FrequencyID.Value).Description);
                        if (NewJobStatusHistory.JobStartDate.HasValue)
                            _mailBillingBuilder.AppendLine("Job Start Date: " + NewJobStatusHistory.JobStartDate.ToString());
                        AppendDivisionInfoInterimBillEmail();
                        _mailBillingBuilder.AppendLine("Job #: " + NewJob.ID);
                        if (NewCustomer != null)
                            _mailBillingBuilder.AppendLine("Company: " + _customerRepository.Get(e => e.Active && e.ID == NewCustomer.CustomerId).Name);
                        if (NewLocationInfo != null)
                            _mailBillingBuilder.AppendLine("Location: " + _cityRepository.Get(e => e.Active && e.ID == NewLocationInfo.CityID).Name + " " + _stateRepository.Get(e => e.Active && e.ID == NewLocationInfo.StateID).Name + " " + _countryRepository.Get(e => e.Active && e.ID == NewLocationInfo.CountryID).Name);
                        if (NewJobDescription != null)
                            _mailBillingBuilder.AppendLine("# of engines: " + NewJobDescription.NumberEngines.ToString());
                        _mailBillingBuilder.AppendLine("Job Type: " + _jobTypeRepository.Get(e => e.Active && e.ID == NewJobInfo.JobTypeID).Description);
                        AppendScopeOfWorkInfoInterimBill();

                        if (_mailBillingBuilder.Length > 0)
                        {
                            string emailList = _settingsModel.GetInterimBillEmails();
                            if (!string.IsNullOrEmpty(emailList))
                                _emailModel.SaveEmailList(emailList, "Interim Billing Request", _mailBillingBuilder.ToString(), "System", Globals.Security.SystemEmployeeID);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error creating the Interim Bill e-mail.", ex);
            }
        }

        private void AppendDivisionInfoInterimBillEmail()
        {
            if (NewJobDivision != null)
            {
                foreach (var jobDivision in NewJobDivision)
                {
                    if (jobDivision.PrimaryDivision)
                    {
                        _mailBillingBuilder.AppendLine("Division (primary): " + _divisionRepository.Get(e => e.Active && e.ID == jobDivision.DivisionID).Name);
                    }
                    else
                    {
                        _mailBillingBuilder.AppendLine("                    " + _divisionRepository.Get(e => e.Active && e.ID == jobDivision.DivisionID).Name);
                    }
                }
            }
        }

        private void AppendScopeOfWorkInfoInterimBill()
        {
            if (NewScopeOfWork != null)
            {
                _mailBillingBuilder.AppendLine("Scope of Work: ");
                foreach (var scopeOfWork in NewScopeOfWork)
                {
                    _mailBillingBuilder.AppendLine(scopeOfWork.ScopeOfWork);
                }
            }
        }

        #endregion

        #region [ Estimation Team Email]
        /// <summary>
        /// Method that generates the email subject for the notification for estimation team
        /// </summary>
        /// <param name="jobId">job id</param>
        /// <returns>email subject</returns>
        public string GenerateEmailSubjectEstimationTeam(int jobId)
        {
            CS_Job job = _jobRepository.Get(w => w.ID == jobId && w.Active && w.CS_CustomerInfo.Active && w.CS_CustomerInfo.CS_Customer.Active, "CS_CustomerInfo.CS_Customer");

            if (null != job)
            {
                //TODO:The proposal number is going to be created in another task and will be implemented here
                return job.PrefixedNumber + ", " + "Proposal Number, " + job.CS_CustomerInfo.CS_Customer.Name;
            }

            return string.Empty;
        }

        /// <summary>
        /// Sends the email notification for the estimation team
        /// </summary>
        /// <param name="jobId">job id</param>
        public void SendNotificationForEstimationTeam(int jobId)
        {
            try
            {
                //Body
                string body = GenerateEmailBodyForEstimationTeam(jobId);

                //List receipts
                string receipts = _settingsModel.GetEstimationTeamEmails();

                //Subject
                string subject = "Won bid " + GenerateEmailSubjectEstimationTeam(jobId);

                if (!string.IsNullOrEmpty(receipts))
                {
                    string[] lstReceipts = receipts.Split(';');

                    for (int i = 0; i < lstReceipts.Count(); i++)
                    {
                        _emailModel.SaveEmailList(lstReceipts[i], subject, body, "System", Globals.Security.SystemEmployeeID);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error sending the email notification for the Estimation Team.", ex);
            }
        }

        /// <summary>
        /// Generates a body text to be send for notification estimation team
        /// </summary>
        /// <param name="jobId">jobid</param>
        /// <returns>text body to send email</returns>
        public string GenerateEmailBodyForEstimationTeam(int jobId)
        {
            StringBuilder _jobDataToEstimatingEmail = new StringBuilder();

            CS_Job job = _jobRepository.Get(w => w.ID == jobId
                                            && w.Active
                                            && w.CS_CustomerInfo.Active
                                            && w.CS_CustomerInfo.CS_Customer.Active
                                            && w.CS_CustomerInfo.CS_Division.Active
                                            && w.CS_JobInfo.Active
                                            && w.CS_JobDescription.Active
                                            && w.CS_JobInfo.CS_Job_JobStatus.Any(e => e.Active),
                                            "CS_CustomerInfo",
                                            "CS_CustomerInfo.CS_Customer",
                                            "CS_CustomerInfo.CS_Division",
                                            "CS_JobDescription",
                                            "CS_JobInfo",
                                            "CS_JobInfo.CS_JobAction",
                                            "CS_JobInfo.CS_JobType",
                                            "CS_JobInfo.CS_Job_JobStatus");

            //TODO:The proposal number is going to be created in another task and will be implemented here
            _jobDataToEstimatingEmail.Append("Proposal#:<Text> " + "##");

            _jobDataToEstimatingEmail.Append("<BL>" + "Job#:<Text> " + job.PrefixedNumber);

            _jobDataToEstimatingEmail.Append("<BL>" + "Company:<Text> " + job.CS_CustomerInfo.CS_Customer.Name.Trim());

            if (job.CS_JobDivision.Count > 0)
            {
                IList<CS_JobDivision> divisionList = job.CS_JobDivision.Where(w => w.Active && w.CS_Division.Active).ToList();

                for (int i = 0; i < divisionList.Count; i++)
                {
                    if (i == 0)
                    {
                        _jobDataToEstimatingEmail.Append("<BL>" + "Division:<Text> " + divisionList[i].CS_Division.Name);
                    }
                    else
                    {
                        _jobDataToEstimatingEmail.Append("<BL>" + " <Text>" + divisionList[i].CS_Division.Name);
                    }
                }
            }

            _jobDataToEstimatingEmail.Append("<BL>" + "JobType:<Text> " + job.CS_JobInfo.CS_JobType.Description);

            _jobDataToEstimatingEmail.Append("<BL>" + "JobAction:<Text> " + job.CS_JobInfo.CS_JobAction.Description);

            if (job.CS_ScopeOfWork.Count > 0)
            {
                _jobDataToEstimatingEmail.Append("<BL>" + "Scope Of Work:<Text>" + job.CS_ScopeOfWork.Where(w => w.Active).OrderBy(e => e.ModificationDate).LastOrDefault().ScopeOfWork);
            }

            CS_Job_JobStatus job_JobStatus = job.CS_JobInfo.CS_Job_JobStatus.FirstOrDefault(w => w.Active);

            if (null != job_JobStatus && job_JobStatus.JobStartDate.HasValue)
                _jobDataToEstimatingEmail.Append("<BL>" + "Job start date:<Text>" + job_JobStatus.JobStartDate.Value.ToString("MM/dd/yyyy HH:mm:ss"));

            if (job.CS_Reserve.Where(w => w.Active).Count() > 0)
            {
                IList<CS_Reserve> reserveList = job.CS_Reserve.Where(w => w.Active).ToList();

                for (int i = 0; i < reserveList.Count; i++)
                {
                    if (i == 0)
                    {
                        if (reserveList[i].Type == (int)Globals.ResourceAllocation.ResourceType.Employee)
                        {
                            _jobDataToEstimatingEmail.Append("<BL>" + "Employee:<Text> " + reserveList[i].CS_Employee.FullName);
                        }
                        else
                        {
                            _jobDataToEstimatingEmail.Append("<BL>" + "Equipment:<Text> " + reserveList[i].CS_EquipmentType.Name);
                        }
                    }
                    else
                    {
                        if (reserveList[i].Type == (int)Globals.ResourceAllocation.ResourceType.Employee)
                        {
                            _jobDataToEstimatingEmail.Append("<BL>" + " <Text>" + reserveList[i].CS_Employee.FullName);
                        }
                        else
                        {
                            _jobDataToEstimatingEmail.Append("<BL>" + " <Text>" + reserveList[i].CS_EquipmentType.Name);
                        }
                    }
                }
            }

            if (job.CS_JobDescription.NumberEmpties.HasValue)
                _jobDataToEstimatingEmail.Append("<BL>" + "Number Engines:<Text> " + job.CS_JobDescription.NumberEmpties);

            if (job.CS_JobDescription.NumberLoads.HasValue)
                _jobDataToEstimatingEmail.Append("<BL>" + "Number Loads:<Text> " + job.CS_JobDescription.NumberLoads);

            if (job.CS_JobDescription.NumberEmpties.HasValue)
                _jobDataToEstimatingEmail.Append("<BL>" + "Number Empties:<Text> " + job.CS_JobDescription.NumberEmpties);

            if (!string.IsNullOrEmpty(job.CS_JobDescription.Lading))
                _jobDataToEstimatingEmail.Append("<BL>" + "Lading:<Text> " + job.CS_JobDescription.Lading);

            if (!string.IsNullOrEmpty(job.CS_JobDescription.UnNumber))
                _jobDataToEstimatingEmail.Append("<BL>" + "Un Number:<Text> " + job.CS_JobDescription.UnNumber);

            if (!string.IsNullOrEmpty(job.CS_JobDescription.STCCInfo))
                _jobDataToEstimatingEmail.Append("<BL>" + "STCC Info:<Text> " + job.CS_JobDescription.STCCInfo);

            if (!string.IsNullOrEmpty(job.CS_JobDescription.Hazmat))
                _jobDataToEstimatingEmail.Append("<BL>" + "Hazmat:<Text> " + job.CS_JobDescription.Hazmat);

            if (!string.IsNullOrEmpty(job.CS_JobDescription.STCCInfo))
                _jobDataToEstimatingEmail.Append("<BL>" + "STCC Info:<Text> " + job.CS_JobDescription.STCCInfo);

            return StringManipulation.TabulateString(_jobDataToEstimatingEmail.ToString());
        }

        #endregion

        #region [ Invoicing Team Email ]
        /// <summary>
        /// Sends the email notification for the invoicing team
        /// </summary>
        /// <param name="jobId"></param>
        public void SendNotificationForInvoicingTeam(int jobId)
        {
            try
            {
                //Body
                string body = GenerateEmailBodyForInvoicingTeam(jobId);

                //List receipts
                string receipts = _settingsModel.GetInvoicingTeamEmails();

                //Subject
                string subject = "Reopened Job - " + GenerateEmailSubjectsInvoicingTeam(jobId);

                //Save
                SaveEmailNotification(receipts, subject, body);
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error sending the email notification for the Invoicing Team.", ex);
            }
        }

        /// <summary>
        /// Methods that generates the email subject for the notification for invoicing team
        /// </summary>
        /// <param name="jobId">job id</param>
        /// <returns>email subject</returns>
        public string GenerateEmailSubjectsInvoicingTeam(int jobId)
        {
            CS_Job job = _jobRepository.Get(w => w.ID == jobId && w.Active && w.CS_CustomerInfo.Active && w.CS_CustomerInfo.CS_Customer.Active, "CS_CustomerInfo.CS_Customer");

            if (null != job)
                return job.PrefixedJobNumber + ", " + job.CS_CustomerInfo.CS_Customer.Name.Trim();

            return string.Empty;
        }

        /// <summary>
        /// Generate email body for the invoicing team
        /// </summary>
        /// <param name="jobId">job id</param>
        /// <returns>email body</returns>
        public string GenerateEmailBodyForInvoicingTeam(int jobId)
        {
            StringBuilder _jobDataForInvoicingTeam = new StringBuilder();

            CS_Job job = _jobRepository.Get(w => w.ID == jobId
                                           && w.Active
                                           && w.CS_CustomerInfo.Active
                                           && w.CS_JobInfo.Active
                                           && w.CS_JobDescription.Active
                                           && w.CS_LocationInfo.Active
                                           && w.CS_JobInfo.CS_Job_JobStatus.Any(e => e.Active),
                                           "CS_CustomerInfo",
                                           "CS_CustomerInfo.CS_Customer",
                                           "CS_CustomerInfo.CS_Division",
                                           "CS_CustomerInfo.CS_Contact3",
                                           "CS_CustomerInfo.CS_Contact1",
                                           "CS_JobInfo",
                                           "CS_JobInfo.CS_JobAction",
                                           "CS_JobInfo.CS_JobType",
                                           "CS_JobInfo.CS_PriceType",
                                           "CS_JobInfo.CS_JobCategory",
                                           "CS_JobInfo.CS_Employee",
                                           "CS_JobDescription",
                                           "CS_LocationInfo",
                                           "CS_LocationInfo.CS_Country",
                                           "CS_LocationInfo.CS_State",
                                           "CS_LocationInfo.CS_City",
                                           "CS_JobInfo.CS_Job_JobStatus");

            _jobDataForInvoicingTeam.Append("Job#:<Text> " + job.PrefixedJobNumber);

            _jobDataForInvoicingTeam.Append("<BL>" + "Company:<Text> " + job.CS_CustomerInfo.CS_Customer.Name.Trim());

            if (job.CS_CustomerInfo.InitialCustomerContactId.HasValue)
                _jobDataForInvoicingTeam.Append("<BL>" + "Initial Company Contact:<Text> " + job.CS_CustomerInfo.CS_Contact3.Name);

            if (job.CS_CustomerInfo.BillToContactId.HasValue)
                _jobDataForInvoicingTeam.Append("<BL>" + "Bill to:<Text> " + job.CS_CustomerInfo.CS_Contact1.FullName);

            _jobDataForInvoicingTeam.Append("<BL>" + "Initial Call date:<Text> " + job.CS_JobInfo.InitialCallDate.ToString("MM/dd/yyyy"));

            _jobDataForInvoicingTeam.Append("<BL>" + "Initial Call time:<Text> " + job.CS_JobInfo.InitialCallTime);

            _jobDataForInvoicingTeam.Append("<BL>" + "Price Type:<Text> " + job.CS_JobInfo.CS_PriceType.Description);

            _jobDataForInvoicingTeam.Append("<BL>" + "Job Action:<Text> " + job.CS_JobInfo.CS_JobAction.Description);

            _jobDataForInvoicingTeam.Append("<BL>" + "Job Category:<Text> " + job.CS_JobInfo.CS_JobCategory.Description);

            _jobDataForInvoicingTeam.Append("<BL>" + "Job Type:<Text> " + job.CS_JobInfo.CS_JobType.Description);

            if (job.CS_JobDivision.Count > 0)
            {
                IList<CS_JobDivision> divisionList = job.CS_JobDivision.Where(w => w.Active && w.CS_Division.Active).ToList();

                for (int i = 0; i < divisionList.Count; i++)
                {
                    if (i == 0)
                    {
                        _jobDataForInvoicingTeam.Append("<BL>" + "Division:<Text> " + divisionList[i].CS_Division.Name);
                    }
                    else
                    {
                        _jobDataForInvoicingTeam.Append("<BL>" + " <Text>" + divisionList[i].CS_Division.Name);
                    }
                }
            }

            if (job.CS_JobInfo.InterimBill)
                _jobDataForInvoicingTeam.Append("<BL>" + "Interim Bill:<Text> " + "Yes");
            else
                _jobDataForInvoicingTeam.Append("<BL>" + "Interim Bill:<Text> " + "No");

            if (job.CS_JobInfo.EmployeeID.HasValue)
                _jobDataForInvoicingTeam.Append("<BL>" + "Requested By:<Text> " + job.CS_JobInfo.CS_Employee.FullName);

            if (job.CS_JobInfo.FrequencyID.HasValue)
                _jobDataForInvoicingTeam.Append("<BL>" + "Frequency:<Text> " + job.CS_JobInfo.CS_Frequency.Description);


            _jobDataForInvoicingTeam.Append("<BL>" + "Country:<Text> " + job.CS_LocationInfo.CS_Country.Name);

            _jobDataForInvoicingTeam.Append("<BL>" + "State:<Text> " + job.CS_LocationInfo.CS_State.Name);

            _jobDataForInvoicingTeam.Append("<BL>" + "City:<Text> " + job.CS_LocationInfo.CS_City.Name);

            if (job.CS_JobDescription.NumberEngines.HasValue)
                _jobDataForInvoicingTeam.Append("<BL>" + "Number Engines:<Text> " + job.CS_JobDescription.NumberEngines);

            if (job.CS_JobDescription.NumberLoads.HasValue)
                _jobDataForInvoicingTeam.Append("<BL>" + "Number Loads:<Text> " + job.CS_JobDescription.NumberLoads);

            if (job.CS_JobDescription.NumberEmpties.HasValue)
                _jobDataForInvoicingTeam.Append("<BL>" + "Number Empties:<Text> " + job.CS_JobDescription.NumberEmpties);

            if (!string.IsNullOrEmpty(job.CS_JobDescription.Lading))
                _jobDataForInvoicingTeam.Append("<BL>" + "Lading:<Text> " + job.CS_JobDescription.Lading);

            if (!string.IsNullOrEmpty(job.CS_JobDescription.UnNumber))
                _jobDataForInvoicingTeam.Append("<BL>" + "Un Number:<Text> " + job.CS_JobDescription.UnNumber);

            if (!string.IsNullOrEmpty(job.CS_JobDescription.STCCInfo))
                _jobDataForInvoicingTeam.Append("<BL>" + "STCC Info:<Text> " + job.CS_JobDescription.STCCInfo);

            if (!string.IsNullOrEmpty(job.CS_JobDescription.Hazmat))
                _jobDataForInvoicingTeam.Append("<BL>" + "Hazmat:<Text> " + job.CS_JobDescription.Hazmat);

            if (!string.IsNullOrEmpty(job.CS_JobDescription.STCCInfo))
                _jobDataForInvoicingTeam.Append("<BL>" + "STCC Info:<Text> " + job.CS_JobDescription.STCCInfo);

            if (job.CS_ScopeOfWork.Count > 0)
            {
                _jobDataForInvoicingTeam.Append("<BL>" + "Scope Of Work:<Text>" + job.CS_ScopeOfWork.Where(w => w.Active).OrderBy(e => e.ModificationDate).LastOrDefault().ScopeOfWork);
            }

            CS_Job_JobStatus job_JobStatus = job.CS_JobInfo.CS_Job_JobStatus.FirstOrDefault(w => w.Active);

            if (null != job_JobStatus && job_JobStatus.JobStartDate.HasValue)
                _jobDataForInvoicingTeam.Append("<BL>" + "Job start date:<Text> " + job_JobStatus.JobStartDate.Value.ToString("MM/dd/yyyy HH:mm:ss"));

            if (null != job_JobStatus && job_JobStatus.JobCloseDate.HasValue)
                _jobDataForInvoicingTeam.Append("<BL>" + "Job end date:<Text> " + job_JobStatus.JobCloseDate.Value.ToString("MM/dd/yyyy HH:mm:ss"));

            return StringManipulation.TabulateString(_jobDataForInvoicingTeam.ToString());
        }
        #endregion

        #region [ Initial Advise ]

        public void BuildInitialAdviseNote(bool checkForChanges)
        {
            _initialAdvise = new StringBuilder();

            _initialAdvise.Append("Scope of Work: ");
            for (int i = 0; i < NewScopeOfWork.Count; i++)
            {
                _initialAdvise.Append(" <Text>");
                if (checkForChanges && i == NewScopeOfWork.Count - 1 && _initialAdviseChangeList.ContainsKey("ScopeOfWork"))
                    _initialAdvise.Append("<RED>");
                _initialAdvise.AppendFormat(" {0}<BL>", NewScopeOfWork[i].ScopeOfWork);
            }

            if (!string.IsNullOrEmpty(NewJob.NumberOrInternalTracking))
                _initialAdvise.AppendFormat("Job #:<Text> {0}<BL>", NewJob.PrefixedNumber);

            CS_Customer customerInfo = _customerRepository.Get(e => e.Active && e.ID == NewCustomer.CustomerId);
            if (null != customerInfo)
            {
                _initialAdvise.Append("Company:<Text>");
                if (checkForChanges && _initialAdviseChangeList.ContainsKey("Customer"))
                    _initialAdvise.Append("<RED>");
                _initialAdvise.AppendFormat(" {0}<BL>", customerInfo.Name);
            }

            if (NewCustomer.InitialCustomerContactId.HasValue)
            {
                CS_Contact contactInfo = _contactRepository.Get(e => e.Active && e.ID == NewCustomer.InitialCustomerContactId.Value);
                if (null != contactInfo)
                {
                    _initialAdvise.Append("Initial Company Contact:<Text>");
                    if (checkForChanges && _initialAdviseChangeList.ContainsKey("InitialCustomerContactId"))
                        _initialAdvise.Append("<RED>");
                    _initialAdvise.AppendFormat(" {0}<BL>", contactInfo.FullName);
                }
            }

            if (NewCustomer.PocEmployeeId.HasValue)
            {
                CS_Employee hulcherPOC = _employeeRepository.Get(e => e.Active && e.ID == NewCustomer.PocEmployeeId.Value);
                if (null != hulcherPOC)
                {
                    _initialAdvise.Append("Hulcher P.O.C:<Text>");
                    if (checkForChanges && _initialAdviseChangeList.ContainsKey("PocEmployeeId"))
                        _initialAdvise.Append("<RED>");
                    _initialAdvise.AppendFormat(" {0}<BL>", hulcherPOC.FullName);
                }
            }

            for (int j = 0; j < _divisionList.Count; j++)
            {
                int divisionId = _divisionList[j].DivisionID;
                CS_Division divisionInfo = _divisionRepository.Get(e => e.Active && e.ID == divisionId);
                if (null != divisionInfo)
                {
                    if (j == 0)
                        _initialAdvise.Append("Division:");
                    _initialAdvise.Append(" <Text>");
                    if (checkForChanges && _initialAdviseChangeList.ContainsKey("Division"))
                        _initialAdvise.Append("<RED>");
                    _initialAdvise.AppendFormat(" {0}<BL>", divisionInfo.Name);
                }
            }

            CS_JobStatus jobStatusInfo = _jobStatusRepository.Get(e => e.Active && e.ID == JobStatusID);
            if (null != jobStatusInfo)
            {
                _initialAdvise.Append("Job Status:<Text>");
                if (checkForChanges && _initialAdviseChangeList.ContainsKey("JobStatus"))
                    _initialAdvise.Append("<RED>");
                _initialAdvise.AppendFormat(" {0}<BL>", jobStatusInfo.Description);
            }

            _initialAdvise.AppendFormat("Job start date:<Text> {0}<BL>", NewJobStatusHistory.JobStartDate);

            CS_PriceType priceTypeInfo = _priceTypeRepository.Get(e => e.Active && e.ID == NewJobInfo.PriceTypeID);
            if (null != priceTypeInfo)
                _initialAdvise.AppendFormat("Price type:<Text> {0}<BL>", priceTypeInfo.Description);

            CS_JobCategory jobCategoryInfo = _jobCategoryRepository.Get(e => e.Active && e.ID == NewJobInfo.JobCategoryID);
            if (null != jobCategoryInfo)
            {
                _initialAdvise.Append("Job Category:<Text>");
                if (checkForChanges && _initialAdviseChangeList.ContainsKey("JobCategoryID"))
                    _initialAdvise.Append("<RED>");
                _initialAdvise.AppendFormat(" {0}<BL>", jobCategoryInfo.Description);
            }

            CS_JobType jobTypeInfo = _jobTypeRepository.Get(e => e.Active && e.ID == NewJobInfo.JobTypeID);
            if (null != jobTypeInfo)
            {
                _initialAdvise.Append("Job Type:<Text>");
                if (checkForChanges && _initialAdviseChangeList.ContainsKey("JobTypeID"))
                    _initialAdvise.Append("<RED>");
                _initialAdvise.AppendFormat(" {0}<BL>", jobTypeInfo.Description);
            }

            CS_JobAction jobActionInfo = _jobActionRepository.Get(e => e.Active && e.ID == NewJobInfo.JobActionID);
            if (null != jobActionInfo)
            {
                _initialAdvise.Append("Job Action:<Text>");
                if (checkForChanges && _initialAdviseChangeList.ContainsKey("JobActionID"))
                    _initialAdvise.Append("<RED>");
                _initialAdvise.AppendFormat(" {0}<BL>", jobActionInfo.Description);
            }

            if (null != NewPresetInfo)
            {
                _initialAdvise.Append("Preset Instructions:<Text>");
                if (checkForChanges && _initialAdviseChangeList.ContainsKey("PresetInstructions"))
                    _initialAdvise.Append("<RED>");
                _initialAdvise.AppendFormat(" {0}<BL>", NewPresetInfo.Instructions);

                if (NewPresetInfo.Date.HasValue)
                {
                    _initialAdvise.Append("Preset Date:<Text>");
                    if (checkForChanges && _initialAdviseChangeList.ContainsKey("PresetDate"))
                        _initialAdvise.Append("<RED>");
                    _initialAdvise.AppendFormat(" {0}<BL>", NewPresetInfo.Date.Value.ToString("MM/dd/yyyy"));
                }
            }

            _initialAdvise.Append("Location:<Text>");
            if (checkForChanges && (_initialAdviseChangeList.ContainsKey("CountryID") || _initialAdviseChangeList.ContainsKey("State") || _initialAdviseChangeList.ContainsKey("City") || _initialAdviseChangeList.ContainsKey("ZipCodeId")))
                _initialAdvise.Append("<RED>");

            CS_Country countryInfo = _countryRepository.Get(e => e.Active && e.ID == NewLocationInfo.CountryID);
            if (null != countryInfo)
                _initialAdvise.AppendFormat(" {0}", countryInfo.Name);

            CS_State stateInfo = _stateRepository.Get(e => e.Active && e.ID == NewLocationInfo.StateID);
            if (null != stateInfo)
                _initialAdvise.AppendFormat(", {0}", stateInfo.Name);

            CS_City cityInfo = _cityRepository.Get(e => e.Active && e.ID == NewLocationInfo.CityID);
            if (null != cityInfo)
                _initialAdvise.AppendFormat(", {0}", cityInfo.Name);

            CS_ZipCode zipCodeInfo = _zipCodeRepository.Get(e => e.Active && e.ID == NewLocationInfo.ZipCodeId);
            if (null != zipCodeInfo)
                _initialAdvise.AppendFormat(", {0}", zipCodeInfo.Name);

            _initialAdvise.Append("<BL>");

            if (NewJobDescription.NumberEngines.HasValue)
            {
                _initialAdvise.Append("# of engines:<Text>");
                if (checkForChanges && _initialAdviseChangeList.ContainsKey("NumberEngines"))
                    _initialAdvise.Append("<RED>");
                _initialAdvise.AppendFormat(" {0}<BL>", NewJobDescription.NumberEngines.Value);
            }

            if (NewJobDescription.NumberLoads.HasValue)
            {
                _initialAdvise.Append("# of loads:<Text>");
                if (checkForChanges && _initialAdviseChangeList.ContainsKey("NumberLoads"))
                    _initialAdvise.Append("<RED>");
                _initialAdvise.AppendFormat(" {0}<BL>", NewJobDescription.NumberLoads.Value);
            }

            if (NewJobDescription.NumberEmpties.HasValue)
            {
                _initialAdvise.Append("# of empties:<Text>");
                if (checkForChanges && _initialAdviseChangeList.ContainsKey("NumberEmpties"))
                    _initialAdvise.Append("<RED>");
                _initialAdvise.AppendFormat(" {0}<BL>", NewJobDescription.NumberEmpties.Value);
            }

            _initialAdvise.Append("Lading:<Text>");
            if (checkForChanges && _initialAdviseChangeList.ContainsKey("Lading"))
                _initialAdvise.Append("<RED>");
            _initialAdvise.AppendFormat(" {0}<BL>", NewJobDescription.Lading);

            _initialAdvise.Append("UN#:<Text>");
            if (checkForChanges && _initialAdviseChangeList.ContainsKey("UnNumber"))
                _initialAdvise.Append("<RED>");
            _initialAdvise.AppendFormat(" {0}<BL>", NewJobDescription.UnNumber);

            _initialAdvise.Append("STCC Info:<Text>");
            if (checkForChanges && _initialAdviseChangeList.ContainsKey("STCCInfo"))
                _initialAdvise.Append("<RED>");
            _initialAdvise.AppendFormat(" {0}<BL>", NewJobDescription.STCCInfo);

            _initialAdvise.Append("Hazmat:<Text>");
            if (checkForChanges && _initialAdviseChangeList.ContainsKey("Hazmat"))
                _initialAdvise.Append("<RED>");
            _initialAdvise.AppendFormat(" {0}<BL>", NewJobDescription.Hazmat);

            _initialAdvise.Append("Equip. Requested:");
            for (int i = 0; i < NewRequestedEquipment.Count; i++)
            {
                _initialAdvise.Append("<Text>");
                if (checkForChanges && _initialAdviseChangeList.ContainsKey("EquipmentRequested"))
                {
                    if (((List<int>)_initialAdviseChangeList["EquipmentRequested"]).Contains(i))
                        _initialAdvise.Append("<RED>");
                }
                _initialAdvise.AppendFormat(" {0} - {1}<BL>", NewRequestedEquipment[i].Name, NewRequestedEquipment[i].Quantity);
            }
            if (checkForChanges && _initialAdviseChangeList.ContainsKey("EquipmentRequestedRemoved"))
            {
                List<string> removedItems = _initialAdviseChangeList["EquipmentRequestedRemoved"] as List<string>;
                for (int i = 0; i < removedItems.Count; i++)
                    _initialAdvise.AppendFormat("<Text><RED>{0}<BL>", removedItems[i]);
            }

        }

        public void SaveInitialAdvise(bool saveInitialEmailAdvise)
        {
            try
            {
                BuildInitialAdviseNote(false);
                CS_CallLog newCallEntry = new CS_CallLog();
                newCallEntry.JobID = NewJob.ID;
                newCallEntry.CallTypeID = (int)Globals.CallEntry.CallType.InitialLog;
                newCallEntry.PrimaryCallTypeId = (int)Globals.CallEntry.PrimaryCallType.ResourceUpdateAddedResources;
                newCallEntry.CallDate = NewJobInfo.InitialCallDate + NewJobInfo.InitialCallTime;
                DateTimeOffset dateTimeOffset = DateTimeOffset.Now;
                newCallEntry.Xml = null;
                newCallEntry.Note = _initialAdvise.ToString();
                newCallEntry.CreatedBy = NewJob.CreatedBy;
                newCallEntry.CreationDate = DateTime.Now;
                newCallEntry.ModifiedBy = NewJob.CreatedBy;
                newCallEntry.ModificationDate = DateTime.Now;
                newCallEntry.Active = true;
                newCallEntry.UserCall = true;

                _callLogRepository.Add(newCallEntry);

                if (saveInitialEmailAdvise)
                    SendCallCriteriaInitialAdvise(newCallEntry.ID);

                SaveCallCriteriaInitialAdviseResources(newCallEntry, saveInitialEmailAdvise);
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error while trying to save a new call entry.", ex);
            }
        }

        /// <summary>
        /// Generates records for a Initial Advise Call Criteria Resources
        /// </summary>
        /// <param name="initialAdvise">Generated Initial Advise Call Log</param>
        private void SaveCallCriteriaInitialAdviseResources(CS_CallLog initialAdvise, bool saveInitialEmailAdvise)
        {
            try
            {
                IList<CS_CallLogResource> saveList = new List<CS_CallLogResource>();
                IList<CS_CallLogCallCriteriaEmail> emailSaveList = new List<CS_CallLogCallCriteriaEmail>();

                IList<EmailVO> resourceList = _callCriteriaModel.ListReceiptsByCallLog(initialAdvise.CallTypeID.ToString(), initialAdvise.JobID, initialAdvise);

                for (int i = 0; i < resourceList.Count; i++)
                {
                    // Because of the Type, we need to separate the PersonID in two different variables
                    int? employeeId = null;
                    int? contactId = null;
                    if (resourceList[i].Type == (int)Globals.CallCriteria.EmailVOType.Employee)
                        employeeId = resourceList[i].PersonID;
                    else
                        contactId = resourceList[i].PersonID;

                    CS_CallLogResource resource = new CS_CallLogResource()
                    {
                        CallLogID = initialAdvise.ID,
                        EmployeeID = employeeId,
                        ContactID = contactId,
                        JobID = initialAdvise.JobID,
                        Type = resourceList[i].Type,
                        CreatedBy = initialAdvise.CreatedBy,
                        CreationDate = DateTime.Now,
                        ModifiedBy = initialAdvise.ModifiedBy,
                        ModificationDate = DateTime.Now,
                        Active = true
                    };

                    saveList.Add(resource);

                    if (saveInitialEmailAdvise)
                    {
                        CS_CallLogCallCriteriaEmail emailResource = new CS_CallLogCallCriteriaEmail()
                        {
                            CallLogID = initialAdvise.ID,
                            Name = resourceList[i].Name,
                            Email = resourceList[i].Email,
                            Status = (int)Globals.CallCriteria.CallCriteriaEmailStatus.Pending,
                            StatusDate = DateTime.Now,
                            //CreationID = , 
                            CreatedBy = initialAdvise.CreatedBy,
                            CreationDate = DateTime.Now,
                            //ModificationID,
                            ModifiedBy = initialAdvise.ModifiedBy,
                            ModificationDate = DateTime.Now,
                            Active = true
                        };

                        emailSaveList.Add(emailResource);
                    }
                }

                _callLogResourceRepository.AddList(saveList);

                if (saveInitialEmailAdvise)
                    _callLogCallCriteriaEmailRepository.AddList(emailSaveList);
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error while trying to save the Initial Advise Resources.", ex);
            }
        }

        public void SaveCallCriteriaInitialAdviseCallCriteriaEmailResources(CS_CallLog initialAdvise)
        {
            try
            {
                IList<CS_CallLogCallCriteriaEmail> emailSaveList = new List<CS_CallLogCallCriteriaEmail>();

                IList<EmailVO> resourceList = _callCriteriaModel.ListReceiptsByCallLog(initialAdvise.CallTypeID.ToString(), initialAdvise.JobID, initialAdvise);

                for (int i = 0; i < resourceList.Count; i++)
                {
                    // Because of the Type, we need to separate the PersonID in two different variables
                    int? employeeId = null;
                    int? contactId = null;
                    if (resourceList[i].Type == (int)Globals.CallCriteria.EmailVOType.Employee)
                        employeeId = resourceList[i].PersonID;
                    else
                        contactId = resourceList[i].PersonID;


                    CS_CallLogCallCriteriaEmail emailResource = new CS_CallLogCallCriteriaEmail()
                    {
                        CallLogID = initialAdvise.ID,
                        Name = resourceList[i].Name,
                        Email = resourceList[i].Email,
                        Status = (int)Globals.CallCriteria.CallCriteriaEmailStatus.Pending,
                        StatusDate = DateTime.Now,
                        //CreationID = , 
                        CreatedBy = initialAdvise.CreatedBy,
                        CreationDate = DateTime.Now,
                        //ModificationID,
                        ModifiedBy = initialAdvise.ModifiedBy,
                        ModificationDate = DateTime.Now,
                        Active = true
                    };

                    emailSaveList.Add(emailResource);
                }

                _callLogCallCriteriaEmailRepository.AddList(emailSaveList);
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error while trying to save the Initial Advise Resources.", ex);
            }
        }

        public void UpdateInitialAdvise()
        {
            try
            {
                // Verify if one of these fields changed, to generate a new initial advise
                // Customer
                // Division
                // Preset Date
                // State
                // City
                // # Of Engines
                // # Of Loads
                // # of Empties
                // Hazmat
                // Scope of Work
                if (_generatesInitialAdvise)
                {
                    BuildInitialAdviseNote(true);
                    CS_CallLog newCallEntry = new CS_CallLog();
                    newCallEntry.JobID = NewJob.ID;
                    newCallEntry.CallTypeID = (int)Globals.CallEntry.CallType.InitialAdviseUpdate;
                    newCallEntry.PrimaryCallTypeId = (int)Globals.CallEntry.PrimaryCallType.JobUpdateNotification;
                    newCallEntry.CallDate = DateTime.Now;
                    DateTimeOffset dateTimeOffset = DateTimeOffset.Now;
                    newCallEntry.Xml = null;
                    newCallEntry.Note = _initialAdvise.ToString();
                    newCallEntry.CreatedBy = NewJob.ModifiedBy;
                    newCallEntry.CreationDate = NewJob.ModificationDate;
                    newCallEntry.ModifiedBy = NewJob.ModifiedBy;
                    newCallEntry.ModificationDate = NewJob.ModificationDate;
                    newCallEntry.Active = true;
                    newCallEntry.UserCall = true;

                    _callLogRepository.Add(newCallEntry);

                    SendCallCriteriaInitialAdvise(newCallEntry.ID);
                    SaveCallCriteriaInitialAdviseResources(newCallEntry, true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error while trying to save a new call entry.", ex);
            }
        }

        /// <summary>
        /// Sends the auto-generated email when an Initial Advise is created, based on Call Criteria
        /// </summary>
        public void SendCallCriteriaInitialAdvise(int callLogId)
        {
            try
            {
                IList<EmailVO> receipts = _callCriteriaModel.ListValidReceiptsByCallLog(callLogId, NewJob.ID);
                string emailList = string.Empty;

                foreach (EmailVO emailVO in receipts)
                    emailList += ";" + emailVO.Email;

                if (!string.IsNullOrEmpty(emailList))
                {
                    emailList = emailList.Substring(1);
                    string subject = _callCriteriaModel.GenerateSubjectForCallCriteria(NewJob, NewJobInfo, NewCustomer, NewLocationInfo, "Initial Advise");
                    string body = StringManipulation.TabulateStringTable(_initialAdvise.ToString());
                    int modificationID = Globals.Security.SystemEmployeeID;
                    if (NewJob.ModificationID.HasValue)
                        modificationID = NewJob.ModificationID.Value;
                    _emailModel.SaveEmailList(emailList, subject, body, NewJob.ModifiedBy, modificationID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error sending the Initial Advise email.", ex);
            }
        }

        public string BodyEmailJobRecord()
        {
            BuildInitialAdviseNote(false);
            return StringManipulation.TabulateString(_initialAdvise.ToString());
        }

        public string BodyEmailJobRecordTable()
        {
            BuildInitialAdviseNote(false);
            return StringManipulation.TabulateStringTable(_initialAdvise.ToString());
        }


        #endregion

        #region [ Job Number ]

        /// <summary>
        /// Generates a Job Number
        /// </summary>
        /// <returns>New Job Number</returns>
        /// <remarks>This method MUST be called inside a Transaction, because it updates the Last Job Number value over the settings table</remarks>
        public string GenerateJobNumber()
        {
            // Gets the latest Job Number generated
            int lastJobNumber = _settingsModel.GetLastJobNumber();

            // Increments value
            lastJobNumber++;

            // Updates Last Job Number
            _settingsModel.UpdateLastJobNumber(lastJobNumber);

            return FormatJobNumber(lastJobNumber);
        }

        /// <summary>
        /// Generates a NonJob Number
        /// </summary>
        /// <returns>New Non Job Number</returns>
        /// <remarks>This method MUST be called inside a Transaction, because it updates the Last Job Number value over the settings table</remarks>
        public string GenerateNonJobNumber()
        {
            // Gets the latest Job Number generated
            int lastNonJobNumber = _settingsModel.GetLastNonJobNumber();

            // Increments value
            lastNonJobNumber++;

            // Updates Last Job Number
            _settingsModel.UpdateLastNonJobNumber(lastNonJobNumber);

            return FormatNonJobNumber(lastNonJobNumber);
        }

        /// <summary>
        /// Format the Non Job Number
        /// </summary>
        /// <param name="nonJobNumber">Non Job Number</param>
        /// <returns>Non Job following the right format</returns>
        public string FormatNonJobNumber(int nonJobNumber)
        {
            string nonJobNumberFormatted;
            if (nonJobNumber > 999999)
                nonJobNumberFormatted = nonJobNumber + "INT";
            else
                nonJobNumberFormatted = (nonJobNumber.ToString().PadLeft(6, '0')) + "INT";

            return nonJobNumberFormatted;
        }

        /// <summary>
        /// Format the Job Number
        /// </summary>
        /// <param name="nonJobNumber">Job Number</param>
        /// <returns>Job Number following the right format</returns>
        public string FormatJobNumber(int jobNumber)
        {
            string jobNumberFormatted;
            if (jobNumber > 999999)
                jobNumberFormatted = jobNumber.ToString();
            else
                jobNumberFormatted = (jobNumber.ToString().PadLeft(6, '0'));

            return jobNumberFormatted;
        }

        #endregion

        #region [ Resources ]

        /// <summary>
        /// Checks if a Job has Reserved Resources
        /// </summary>
        public bool HasReservedResources(int jobId)
        {
            IList<CS_Job> lst = _jobRepository.ListAll(e => e.ID == jobId && e.CS_Reserve.Any(j => j.Active));

            if (lst.Count > 0)
                return true;

            return false;
        }



        /// <summary>
        /// Checks if a Job has Assigned Resources
        /// </summary>
        public bool HasAssignedResources(int jobId)
        {
            IList<CS_Job> lst = _jobRepository.ListAll(e => e.ID == jobId && e.CS_Resource.Any(j => j.Active));

            if (lst.Count > 0)
                return true;

            return false;
        }

        #endregion

        #region [ Preset Watch Service ]

        /// <summary>
        /// Main method of the Watch Service
        /// </summary>
        public void CreateLapsedPresetCallLog()
        {
            IList<CS_Job> jobList = ListAllExpiredPresetJob();

            for (int i = 0; i < jobList.Count; i++)
            {
                SavelapsedPreset(jobList[i]);
            }
        }

        /// <summary>
        /// Build the Note for the Lapsed Preset based on Job
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public string BuildlapsedPresetNote(CS_Job job)
        {
            StringBuilder lapsedPreset = new StringBuilder();

            lapsedPreset.Append("Scope of Work: ");
            foreach (CS_ScopeOfWork scopeOfWork in job.CS_ScopeOfWork)
                lapsedPreset.AppendLine(" <Text>" + scopeOfWork.ScopeOfWork + "<BL>");

            if (!string.IsNullOrEmpty(job.NumberOrInternalTracking))
                lapsedPreset.Append("Non-Job #:<Text>" + job.PrefixedNumber + "<BL>");

            lapsedPreset.Append("Company:<Text>" + job.CS_CustomerInfo.CS_Customer.FullCustomerInformation + "<BL>");
            lapsedPreset.Append("Job type:<Text> " + job.CS_JobInfo.CS_JobType.Description + "<BL>");
            lapsedPreset.Append("Job Action:<Text> " + job.CS_JobInfo.CS_JobAction.Description + "<BL>");
            lapsedPreset.Append("Price type:<Text> " + job.CS_JobInfo.CS_PriceType.Description + "<BL>");
            if (null != job.CS_PresetInfo)
            {
                lapsedPreset.Append("Preset Instructions:<Text> " + job.CS_PresetInfo.Instructions + "<BL>");
                if (job.CS_PresetInfo.Date.HasValue)
                    lapsedPreset.Append("Preset Date:<Text> " + job.CS_PresetInfo.Date.Value.ToString("MM/dd/yyyy") + "<BL>");
                if (job.CS_PresetInfo.Time.HasValue)
                    lapsedPreset.Append("Preset Time:<Text> " + job.CS_PresetInfo.Time.Value.ToString() + "<BL>");
            }
            lapsedPreset.Append("Location:<Text> " + job.CS_LocationInfo.CS_Country.Name + ", " + job.CS_LocationInfo.CS_State.Name + ", " + job.CS_LocationInfo.CS_City.Name + ", " + job.CS_LocationInfo.CS_ZipCode.ZipCodeNameEdited + "<BL>");

            return lapsedPreset.ToString();
        }

        /// <summary>
        /// Create the Lapsed Preset Call Entry
        /// </summary>
        /// <param name="job"></param>
        public void SavelapsedPreset(CS_Job job)
        {
            try
            {
                CS_CallLog newCallEntry = new CS_CallLog();
                newCallEntry.JobID = job.ID;
                newCallEntry.CallTypeID = (int)Globals.CallEntry.CallType.LapsedPreset;
                newCallEntry.PrimaryCallTypeId = (int)Globals.CallEntry.PrimaryCallType.JobUpdateNotification;
                newCallEntry.CallDate = DateTime.Now;
                DateTimeOffset dateTimeOffset = DateTimeOffset.Now;
                newCallEntry.Xml = null;
                newCallEntry.Note = BuildlapsedPresetNote(job);
                newCallEntry.CreatedBy = "System";
                newCallEntry.CreationDate = DateTime.Now;
                newCallEntry.ModifiedBy = "System";
                newCallEntry.ModificationDate = DateTime.Now;
                newCallEntry.Active = true;
                newCallEntry.UserCall = true;

                _callLogRepository.Add(newCallEntry);
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error while trying to save a new call entry of type Lapsed Preset.", ex);
            }
        }

        #endregion

        #region [ Billing Status ]

        public bool WasPreviouslyBillable(int jobId)
        {
            List<CS_Job_JobStatus> previousActiveStatus = _jobStatusHistoryRepository.ListAll(
                e => e.Active == false
                    && e.JobStatusId == (int)Globals.JobRecord.JobStatus.Active
                    && e.JobID == jobId).ToList();

            if (previousActiveStatus.Count > 0)
            {
                int firstActiveId = previousActiveStatus[0].ID;
                int closedStatusCount = _jobStatusHistoryRepository.ListAll(
                    e => e.JobStatusId == (int)Globals.JobRecord.JobStatus.Closed
                        && e.JobID == jobId
                        && e.ID > firstActiveId).Count;

                if (closedStatusCount > 0)
                    return true;
            }

            return false;
        }

        #endregion

        #region [ Job Division ]

        public void AddDivisionToJob(int divisionId, int jobId, string userName)
        {
            IList<CS_JobDivision> lstjobDiv = _jobDivisionRepository.ListAll(w => w.JobID == jobId);

            bool notAssigned = !lstjobDiv.Any(a => a.DivisionID == divisionId && a.Active);

            if (notAssigned)
            {
                CS_JobDivision jobDivision = new CS_JobDivision
                {
                    Active = true,
                    CreatedBy = userName,
                    CreationDate = DateTime.Now,
                    PrimaryDivision = false,
                    ModifiedBy = userName,
                    ModificationDate = DateTime.Now,
                    JobID = jobId,
                    DivisionID = divisionId
                };

                _jobDivisionRepository.Add(jobDivision);
            }
        }

        #endregion

        #region [ Map Plotting ]

        /// <summary>
        /// Returns a list of map plotting objects based on filters
        /// </summary>
        /// <param name="parameters">Object that contains all the parameters for the request</param>
        /// <returns>List of map plotting objects</returns>
        public IList<Globals.MapPlotDataObject> FilterMapPlotting(Globals.MapPlotRequestDataObject parameters)
        {
            int activeJobStatus = (int)Globals.JobRecord.JobStatus.Active;
            int generalLogJobId = Globals.GeneralLog.ID;
            bool hasDivisions = parameters.DivisionList != null;
            bool hasJobAction = parameters.JobActionList != null;
            bool hasJobCategory = parameters.JobCategoryList != null;
            bool hasPriceType = parameters.PriceTypeList != null;

            if (parameters.DivisionList == null)
                parameters.DivisionList = new int[0];
            if (parameters.JobActionList == null)
                parameters.JobActionList = new int[0];
            if (parameters.JobCategoryList == null)
                parameters.JobCategoryList = new int[0];
            if (parameters.PriceTypeList == null)
                parameters.PriceTypeList = new int[0];

            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MaxValue;
            if (parameters.CreationDate.HasValue)
            {
                startDate = parameters.CreationDate.Value.Date;
                endDate = parameters.CreationDate.Value.Date.AddDays(1).AddSeconds(-1);
            }

            IList<CS_Job> jobList = _jobRepository.ListAll(
                e => (!parameters.JobNumberID.HasValue || e.ID == parameters.JobNumberID.Value) && 
                     (!parameters.CustomerID.HasValue || e.CS_CustomerInfo.CustomerId == parameters.CustomerID.Value) &&
                     (!parameters.StateID.HasValue || e.CS_LocationInfo.StateID == parameters.StateID.Value) &&
                     (hasDivisions ? e.CS_JobDivision.Any(f => parameters.DivisionList.Contains(f.DivisionID) && f.Active) : true) &&
                     (hasJobAction ? parameters.JobActionList.Any(f => e.CS_JobInfo.JobActionID == f) : true) &&
                     (hasJobCategory ? parameters.JobCategoryList.Contains(e.CS_JobInfo.JobCategoryID) : true) &&
                     (hasPriceType ? parameters.PriceTypeList.Contains(e.CS_JobInfo.PriceTypeID) : true) &&
                     (!parameters.CreationDate.HasValue || (e.CreationDate >= startDate && e.CreationDate <= endDate)) &&
                     e.ID != generalLogJobId &&
                     e.CS_JobInfo.CS_Job_JobStatus.Any(f => f.Active && f.JobStatusId == activeJobStatus)
                , "CS_JobInfo", "CS_JobInfo.CS_Job_JobStatus", "CS_JobInfo.CS_PriceType", "CS_JobInfo.CS_JobType",
                  "CS_CustomerInfo", "CS_CustomerInfo.CS_Customer", "CS_LocationInfo",
                  "CS_LocationInfo.CS_Country", "CS_LocationInfo.CS_State", "CS_LocationInfo.CS_City", "CS_LocationInfo.CS_Zipcode");

            IList<Globals.MapPlotDataObject> returnList = new List<Globals.MapPlotDataObject>();
            for (int i = 0; i < jobList.Count; i++)
            {
                CS_Job currentJob = jobList[i];

                Globals.MapPlotDataObject mapObject = new Globals.MapPlotDataObject();
                if (currentJob.CS_LocationInfo.CS_ZipCode.Latitude.HasValue)
                    mapObject.Latitude = currentJob.CS_LocationInfo.CS_ZipCode.Latitude.Value;
                if (currentJob.CS_LocationInfo.CS_ZipCode.Longitude.HasValue)
                    mapObject.Longitude = currentJob.CS_LocationInfo.CS_ZipCode.Longitude.Value;
                mapObject.Type = "J";
                mapObject.Name = string.Format("Job #: {0}", currentJob.PrefixedNumber);
                mapObject.Description = string.Format("Company: {0}<br/>", currentJob.CS_CustomerInfo.CS_Customer.Name);
                mapObject.Description += string.Format("Location: {0}, {1}, {2}, {3}",
                    currentJob.CS_LocationInfo.CS_City.Name,
                    currentJob.CS_LocationInfo.CS_State.Acronym,
                    currentJob.CS_LocationInfo.CS_Country.Name,
                    currentJob.CS_LocationInfo.CS_ZipCode.ZipCodeNameEdited);

                returnList.Add(mapObject);
            }

            // Fix position for jobs at the same point
            for (int i = 0; i < returnList.Count; i++)
            {
                IList<Globals.MapPlotDataObject> samePointList = returnList.Where(e => e.Latitude == returnList[i].Latitude && e.Longitude == returnList[i].Longitude).ToList();
                double newLongitude = returnList[i].Longitude;
                double newLatitude = returnList[i].Latitude;
                for (int j = 0; j < samePointList.Count; j++)
                {
                    newLongitude += 0.005;
                    newLatitude += 0.005;
                    samePointList[j].Longitude = newLongitude;
                    samePointList[j].Latitude = newLatitude;
                }
            }

            return returnList;
        }

        #endregion

        #region [ Project Calendar ]

        public IList<ProjectCalendarVO> ListAllProjectCalendar(List<DateTime> dateRange, int? divisionFilter, int? equipmentTypeFilter, int? equipmentFilter, int? employeeFilter, int? customerFilter, int? jobFilter, int? jobActionFilter)
        {
            #region [ Raw Data Load ]

            DateTime beginDate = dateRange[0];
            DateTime endDate = dateRange[dateRange.Count - 1].AddDays(1).AddSeconds(-1);
            List<ProjectCalendarVO> calendarList = new List<ProjectCalendarVO>();


            List<CS_View_ProjectCalendar_Allocation> allocationList = _projectCalendarAllocationRepository.ListAll(e =>
                ((jobFilter.HasValue && e.JobID == jobFilter.Value) || (!jobFilter.HasValue)) &&
                ((jobActionFilter.HasValue && e.JobActionID == jobActionFilter.Value) || (!jobActionFilter.HasValue)) &&
                ((divisionFilter.HasValue && e.JobDivisionID == divisionFilter.Value) || (!divisionFilter.HasValue)) &&
                ((equipmentTypeFilter.HasValue && e.EquipmentTypeID.HasValue && e.EquipmentTypeID.Value == equipmentTypeFilter.Value) || (!equipmentTypeFilter.HasValue)) &&
                ((equipmentFilter.HasValue && e.EquipmentID.HasValue && e.EquipmentID.Value == equipmentFilter.Value) || (!equipmentFilter.HasValue)) &&
                ((employeeFilter.HasValue && e.EmployeeID.HasValue && e.EmployeeID.Value == employeeFilter.Value) || (!employeeFilter.HasValue)) &&
                ((customerFilter.HasValue && e.CustomerID == customerFilter.Value) || (!customerFilter.HasValue)) &&

                ((beginDate <= e.LastModification && endDate >= e.LastModification) || (e.PresetInfoDate.HasValue && beginDate <= e.PresetInfoDate && endDate >= e.PresetInfoDate))
                ).ToList();

            List<CS_View_ProjectCalendar_Reserved> reservedList = _projectCalendarReservedRepository.ListAll(e =>
                ((jobFilter.HasValue && e.JobID == jobFilter.Value) || (!jobFilter.HasValue)) &&
                ((jobActionFilter.HasValue && e.JobActionID == jobActionFilter.Value) || (!jobActionFilter.HasValue)) &&
                ((divisionFilter.HasValue && e.JobDivisionID == divisionFilter.Value) || (!divisionFilter.HasValue)) &&
                ((equipmentTypeFilter.HasValue && e.EquipmentTypeID.HasValue && e.EquipmentTypeID.Value == equipmentTypeFilter.Value) || (!equipmentTypeFilter.HasValue)) &&
                ((employeeFilter.HasValue && e.EmployeeID.HasValue && e.EmployeeID.Value == employeeFilter.Value) || (!employeeFilter.HasValue)) &&
                ((customerFilter.HasValue && e.CustomerID == customerFilter.Value) || (!customerFilter.HasValue)) &&

                ((beginDate <= e.LastModification && endDate >= e.LastModification) || (e.PresetInfoDate.HasValue && beginDate <= e.PresetInfoDate && endDate >= e.PresetInfoDate))
                ).ToList();

            List<CS_View_ProjectCalendar_CallLog> callLogList = _projectCalendarCallLogRepository.ListAll(e =>
                ((jobFilter.HasValue && e.JobID == jobFilter.Value) || (!jobFilter.HasValue)) &&

                ((jobActionFilter.HasValue && e.JobActionID == jobActionFilter.Value) || (!jobActionFilter.HasValue)) &&
                ((divisionFilter.HasValue && e.JobDivisionID == divisionFilter.Value) || (!divisionFilter.HasValue)) &&
                ((equipmentTypeFilter.HasValue && e.EquipmentTypeID.HasValue && e.EquipmentTypeID.Value == equipmentTypeFilter.Value) || (!equipmentTypeFilter.HasValue)) &&
                ((equipmentFilter.HasValue && e.EquipmentID.HasValue && e.EquipmentID.Value == equipmentFilter.Value) || (!equipmentFilter.HasValue)) &&
                ((employeeFilter.HasValue && e.EmployeeID.HasValue && e.EmployeeID.Value == employeeFilter.Value) || (!employeeFilter.HasValue)) &&
                ((customerFilter.HasValue && e.CustomerID == customerFilter.Value) || (!customerFilter.HasValue)) &&

                ((beginDate <= e.LastModification && endDate >= e.LastModification) || (e.PresetInfoDate.HasValue && beginDate <= e.PresetInfoDate && endDate >= e.PresetInfoDate))
                ).OrderBy(e => e.JobDivisionID).ThenBy(e => e.JobID).ThenBy(e => e.CallLogID).ToList();

            List<int> jobIDList = allocationList.Select(e => e.JobID).Distinct().ToList();
            jobIDList.AddRange(reservedList.Select(e => e.JobID).Distinct().ToList());
            jobIDList.AddRange(callLogList.Select(e => e.JobID).Distinct().ToList());
            jobIDList = jobIDList.Distinct().ToList();

            //IList<CS_Job_JobStatus> jobStatusHitory = _jobStatusHistoryRepository.ListAll(e => (e.JobStartDate.HasValue || e.JobCloseDate.HasValue) && jobIDList.Contains(e.JobID));
            IList<CS_Job_JobStatus> jobStatusHitory = _jobStatusHistoryRepository.ListAll(e => jobIDList.Contains(e.JobID));

            List<int> removeJobIDList = new List<int>();

            for (int i = 0; i < jobIDList.Count; i++)
            {
                int jobID = jobIDList[i];

                IList<CS_Job_JobStatus> jobStatusHitoryByJob = jobStatusHitory.Where(e => e.JobID == jobID).ToList();

                if (null == jobStatusHitoryByJob || jobStatusHitoryByJob.Count == 0 || !JobHasAnyDateInTheRange(jobStatusHitoryByJob, dateRange))
                {
                    allocationList.RemoveAll(w => w.JobID == jobID);
                    reservedList.RemoveAll(w => w.JobID == jobID);
                    callLogList.RemoveAll(w => w.JobID == jobID);
                }

            }

            List<int> divisionIDList = allocationList.Select(e => e.JobDivisionID).Distinct().ToList();
            divisionIDList.AddRange(reservedList.Select(e => e.JobDivisionID).Distinct().ToList());
            divisionIDList.AddRange(callLogList.Select(e => e.JobDivisionID).Distinct().ToList());
            divisionIDList = divisionIDList.Distinct().ToList();

            IList<CS_Division> divs = _divisionRepository.ListAll();

            #endregion

            #region [ Date Range Loop ]

            for (int dateIndex = 0; dateIndex < dateRange.Count; dateIndex++)
            {
                DateTime date = dateRange[dateIndex];

                #region [ Division Loop ]

                for (int divisionIndex = 0; divisionIndex < divisionIDList.Count; divisionIndex++)
                {
                    int divisionID = divisionIDList[divisionIndex];
                    IList<CS_View_ProjectCalendar_Allocation> jobListAllocation = allocationList.Where(e => e.JobDivisionID == divisionID).ToList();
                    IList<CS_View_ProjectCalendar_Reserved> jobListReserved = reservedList.Where(e => e.JobDivisionID == divisionID).ToList();
                    IList<CS_View_ProjectCalendar_CallLog> jobListCallLog = callLogList.Where(e => e.JobDivisionID == divisionID).ToList();
                    List<int> jobCallLogIDList = jobListCallLog.Select(e => e.JobID).Distinct().ToList();
                    CS_Division div = divs.FirstOrDefault(e => e.ID == divisionID);
                    ProjectCalendarVO currentDivision = calendarList.FirstOrDefault(e => e.DivisionID == divisionID && e.CalendarDate == date);
                    string divName = string.Empty;

                    if (null != div)
                        divName = div.Name;

                    if (null == currentDivision)
                    {
                        currentDivision = new ProjectCalendarVO()
                        {
                            CalendarDate = date,
                            DivisionName = divName,
                            DivisionID = divisionID
                        };

                        calendarList.Add(currentDivision);
                    }

                    #region [ Job Allocation Loop ]

                    for (int jobIndex = 0; jobIndex < jobListAllocation.Count; jobIndex++)
                    {
                        CS_View_ProjectCalendar_Allocation job = jobListAllocation[jobIndex];
                        IList<CS_Job_JobStatus> jobStatusHitoryByJobID = jobStatusHitory.Where(e => e.JobID == job.JobID).ToList();
                        JobCalendarVO currentJob = currentDivision.JobCalendarList.FirstOrDefault(e => e.JobID == job.JobID);
                        bool activeAtDate = JobWasActiveAtDate(jobStatusHitoryByJobID, date);

                        if (null == currentJob)
                        {
                            currentJob = new JobCalendarVO()
                                {
                                    Job = job.PrefixedNumber,
                                    PaintDate = activeAtDate,
                                    JobID = job.JobID,
                                    CustomerName = job.Customer,
                                    DivisionName = job.JobDivisionName,
                                    CityName = job.JobCityName,
                                    StateName = job.JobStateName,
                                    StatusName = job.JobStatusName,
                                    ActionName = job.JobActionName
                                };

                            currentDivision.JobCalendarList.Add(currentJob);
                        }

                        #region [ Resource Adding ]

                        if (job.ResourceEndDateTime.HasValue && job.ResourceStartDateTime.HasValue)
                        {
                            if (date.Date >= job.ResourceStartDateTime.Value.Date && date.Date <= job.ResourceEndDateTime.Value.Date)
                            {
                                if (job.EmployeeID.HasValue)
                                {
                                    ResourceVO resource = currentJob.ResourceList.FirstOrDefault(e => e.EmployeeID == job.EmployeeID);

                                    if (null == resource)
                                    {
                                        resource = new ResourceVO()
                                        {
                                            EmployeeID = job.EmployeeID,
                                            EmployeeName = job.EmployeeName,
                                            ResourceColor = (int)Globals.ProjectCalendar.ResourceType.AddEmployee
                                        };

                                        currentJob.ResourceList.Add(resource);
                                    }

                                    resource.EstimatedWork = true;
                                }
                                else if (job.EquipmentID.HasValue)
                                {
                                    ResourceVO resource = currentJob.ResourceList.FirstOrDefault(e => e.EquipmentID == job.EquipmentID);

                                    if (null == resource)
                                    {
                                        resource = new ResourceVO()
                                        {
                                            EquipmentID = job.EquipmentID,
                                            EquipmentName = job.EquipmentName,
                                            ResourceColor = (int)Globals.ProjectCalendar.ResourceType.AddEquipment
                                        };

                                        currentJob.ResourceList.Add(resource);
                                    }

                                    resource.EstimatedWork = true;
                                }
                            }
                            else if ((beginDate <= job.ResourceStartDateTime.Value.Date && endDate >= job.ResourceStartDateTime.Value.Date) || (beginDate <= job.ResourceEndDateTime.Value.Date && endDate >= job.ResourceEndDateTime.Value.Date))
                            {
                                if (job.EmployeeID.HasValue)
                                {
                                    ResourceVO resource = currentJob.ResourceList.FirstOrDefault(e => e.EmployeeID == job.EmployeeID);

                                    if (null == resource)
                                    {
                                        resource = new ResourceVO()
                                        {
                                            EmployeeID = job.EmployeeID,
                                            EmployeeName = job.EmployeeName,
                                            ResourceColor = (int)Globals.ProjectCalendar.ResourceType.AddEmployee
                                        };

                                        currentJob.ResourceList.Add(resource);
                                    }

                                    resource.EstimatedWork = false;
                                }
                                else if (job.EquipmentID.HasValue)
                                {
                                    ResourceVO resource = currentJob.ResourceList.FirstOrDefault(e => e.EquipmentID == job.EquipmentID);

                                    if (null == resource)
                                    {
                                        resource = new ResourceVO()
                                        {
                                            EquipmentID = job.EquipmentID,
                                            EquipmentName = job.EquipmentName,
                                            ResourceColor = (int)Globals.ProjectCalendar.ResourceType.AddEquipment
                                        };

                                        currentJob.ResourceList.Add(resource);
                                    }

                                    resource.EstimatedWork = false;
                                }
                            }
                        }

                        #endregion
                    }

                    #endregion

                    #region [ Job Reserved Loop ]

                    for (int jobIndex = 0; jobIndex < jobListReserved.Count; jobIndex++)
                    {
                        CS_View_ProjectCalendar_Reserved job = jobListReserved[jobIndex];
                        IList<CS_Job_JobStatus> jobStatusHitoryByJobID = jobStatusHitory.Where(e => e.JobID == job.JobID).ToList();
                        JobCalendarVO currentJob = currentDivision.JobCalendarList.FirstOrDefault(e => e.JobID == job.JobID);
                        bool activeAtDate = JobWasActiveAtDate(jobStatusHitoryByJobID, date);

                        if (null == currentJob)
                        {
                            currentJob = new JobCalendarVO()
                            {
                                Job = job.PrefixedNumber,
                                PaintDate = activeAtDate,
                                JobID = job.JobID,
                                CustomerName = job.Customer,
                                DivisionName = job.JobDivisionName,
                                CityName = job.JobCityName,
                                StateName = job.JobStateName,
                                StatusName = job.JobStatusName,
                                ActionName = job.JobActionName
                            };

                            currentDivision.JobCalendarList.Add(currentJob);
                        }

                        #region [ Resource Adding ]

                        if (job.ResourceEndDateTime.HasValue && job.ResourceStartDateTime.HasValue)
                        {
                            if (date.Date >= job.ResourceStartDateTime.Value.Date && date.Date <= job.ResourceEndDateTime.Value.Date)
                            {
                                if (job.EmployeeID.HasValue)
                                {
                                    ResourceVO resource = currentJob.ResourceList.FirstOrDefault(e => e.EmployeeID == job.EmployeeID);

                                    if (null == resource)
                                    {
                                        resource = new ResourceVO()
                                        {
                                            EmployeeID = job.EmployeeID,
                                            EmployeeName = job.EmployeeName,
                                            ResourceColor = (int)Globals.ProjectCalendar.ResourceType.ReservedEmployee
                                        };

                                        currentJob.ResourceList.Add(resource);
                                    }

                                    resource.Reserved = true;
                                }
                                else if (job.EquipmentTypeID.HasValue)
                                {
                                    ResourceVO resource = currentJob.ResourceList.FirstOrDefault(e => e.EquipmentTypeID == job.EquipmentTypeID);

                                    if (null == resource)
                                    {
                                        resource = new ResourceVO()
                                        {
                                            EquipmentTypeID = job.EquipmentTypeID,
                                            EquipmentTypeName = job.EquipmentTypeName,
                                            ResourceColor = (int)Globals.ProjectCalendar.ResourceType.ReservedEquipment
                                        };

                                        currentJob.ResourceList.Add(resource);
                                    }

                                    resource.Reserved = true;
                                }
                            }
                            else if ((beginDate <= job.ResourceStartDateTime.Value.Date && endDate >= job.ResourceStartDateTime.Value.Date) || (beginDate <= job.ResourceEndDateTime.Value.Date && endDate >= job.ResourceEndDateTime.Value.Date))
                            {
                                if (job.EmployeeID.HasValue)
                                {
                                    ResourceVO resource = currentJob.ResourceList.FirstOrDefault(e => e.EmployeeID == job.EmployeeID);

                                    if (null == resource)
                                    {
                                        resource = new ResourceVO()
                                        {
                                            EmployeeID = job.EmployeeID,
                                            EmployeeName = job.EmployeeName,
                                            ResourceColor = (int)Globals.ProjectCalendar.ResourceType.ReservedEmployee
                                        };

                                        currentJob.ResourceList.Add(resource);
                                    }

                                    resource.Reserved = false;
                                }
                                else if (job.EquipmentTypeID.HasValue)
                                {
                                    ResourceVO resource = currentJob.ResourceList.FirstOrDefault(e => e.EquipmentTypeID == job.EquipmentTypeID);

                                    if (null == resource)
                                    {
                                        resource = new ResourceVO()
                                        {
                                            EquipmentTypeID = job.EquipmentTypeID,
                                            EquipmentTypeName = job.EquipmentTypeName,
                                            ResourceColor = (int)Globals.ProjectCalendar.ResourceType.ReservedEquipment
                                        };

                                        currentJob.ResourceList.Add(resource);
                                    }

                                    resource.Reserved = false;
                                }
                            }
                        }

                        #endregion
                    }

                    #endregion

                    #region [ Job Call Log Loop ]

                    for (int jobIndex = 0; jobIndex < jobCallLogIDList.Count; jobIndex++)
                    {
                        int jobID = jobCallLogIDList[jobIndex];
                        JobCalendarVO currentJob = currentDivision.JobCalendarList.FirstOrDefault(e => e.JobID == jobID);

                        if (null == currentJob)
                        {
                            IList<CS_Job_JobStatus> jobStatusHitoryByJobID = jobStatusHitory.Where(e => e.JobID == jobID).ToList();
                            bool activeAtDate = JobWasActiveAtDate(jobStatusHitoryByJobID, date);
                            CS_View_ProjectCalendar_CallLog job = jobListCallLog.FirstOrDefault(e => e.JobID == jobID);

                            currentJob = new JobCalendarVO()
                            {
                                Job = job.PrefixedNumber,
                                PaintDate = activeAtDate,
                                JobID = job.JobID,
                                CustomerName = job.Customer,
                                DivisionName = job.JobDivisionName,
                                CityName = job.JobCityName,
                                StateName = job.JobStateName,
                                StatusName = job.JobStatusName,
                                ActionName = job.JobActionName
                            };

                            calendarList[calendarList.Count - 1].JobCalendarList.Add(currentJob);
                        }

                        IList<CS_View_ProjectCalendar_CallLog> employeeCallLogHitoryByJobID = jobListCallLog.Where(e => e.EmployeeID.HasValue && e.JobID == jobID).ToList();
                        IList<CS_View_ProjectCalendar_CallLog> equipmentCallLogHitoryByJobID = jobListCallLog.Where(e => e.EquipmentID.HasValue && e.JobID == jobID).ToList();
                        List<int> employeeIDList = employeeCallLogHitoryByJobID.Select(e => e.EmployeeID.Value).Distinct().ToList();
                        List<int> equipmentIDList = equipmentCallLogHitoryByJobID.Select(e => e.EquipmentID.Value).Distinct().ToList();

                        #region [ Employee Adding ]

                        for (int employeeIndex = 0; employeeIndex < employeeIDList.Count; employeeIndex++)
                        {
                            int employeeID = employeeIDList[employeeIndex];
                            IList<CS_View_ProjectCalendar_CallLog> employeeCallLogHitoryByEmployee = employeeCallLogHitoryByJobID.Where(e => e.EmployeeID.Value == employeeID).ToList();

                            bool activeAtDateResource = ResourceWasWorkingAtDate(employeeCallLogHitoryByEmployee, date);

                            ResourceVO resource = currentJob.ResourceList.FirstOrDefault(e => e.EmployeeID == employeeID);

                            if (null == resource)
                            {
                                CS_View_ProjectCalendar_CallLog currentRes = employeeCallLogHitoryByEmployee.FirstOrDefault();
                                resource = new ResourceVO()
                                {
                                    EmployeeID = currentRes.EmployeeID,
                                    EmployeeName = currentRes.EmployeeName,
                                    ResourceColor = (int)Globals.ProjectCalendar.ResourceType.AddEmployee
                                };

                                if ((beginDate <= currentRes.ResourceStartDateTime.Value.Date && endDate >= currentRes.ResourceStartDateTime.Value.Date) || (beginDate <= currentRes.ResourceEndDateTime.Value.Date && endDate >= currentRes.ResourceEndDateTime.Value.Date))
                                    currentJob.ResourceList.Add(resource);
                            }

                            resource.Worked = activeAtDateResource;

                        }

                        #endregion

                        #region [ Equipment Adding ]

                        for (int equipmentIndex = 0; equipmentIndex < equipmentIDList.Count; equipmentIndex++)
                        {
                            int equipmentID = equipmentIDList[equipmentIndex];
                            IList<CS_View_ProjectCalendar_CallLog> equipmentCallLogHitoryByEquipment = equipmentCallLogHitoryByJobID.Where(e => e.EquipmentID.Value == equipmentID).ToList();
                            bool activeAtDateResource = ResourceWasWorkingAtDate(equipmentCallLogHitoryByEquipment, date);

                            ResourceVO resource = currentJob.ResourceList.FirstOrDefault(e => e.EquipmentID == equipmentID);

                            if (null == resource)
                            {
                                CS_View_ProjectCalendar_CallLog currentRes = equipmentCallLogHitoryByEquipment.FirstOrDefault();
                                resource = new ResourceVO()
                                {
                                    EquipmentID = currentRes.EquipmentID,
                                    EquipmentName = currentRes.EquipmentName,
                                    ResourceColor = (int)Globals.ProjectCalendar.ResourceType.AddEquipment
                                };

                                if (currentRes.ResourceStartDateTime.HasValue)
                                {
                                    if ((beginDate <= currentRes.ResourceStartDateTime.Value.Date && endDate >= currentRes.ResourceStartDateTime.Value.Date) || (beginDate <= currentRes.ResourceEndDateTime.Value.Date && endDate >= currentRes.ResourceEndDateTime.Value.Date))
                                        currentJob.ResourceList.Add(resource);
                                }
                            }

                            resource.Worked = activeAtDateResource;

                        }

                        #endregion
                    }

                    #endregion
                }

                #endregion
            }

            #endregion

            return calendarList;
        }

        private bool JobHasAnyDateInTheRange(IList<CS_Job_JobStatus> jobStatusHitory, List<DateTime> dateRange)
        {
            for (int i = 0; i < dateRange.Count; i++)
            {
                if (JobWasActiveAtDate(jobStatusHitory, dateRange[i]))
                    return true;
            }

            return false;
        }

        private bool ResourceWasWorkingAtDate(IList<CS_View_ProjectCalendar_CallLog> employeeCallLogHitory, DateTime date)
        {
            bool activeAtDate = false;
            bool startLowerThan = false;
            bool endGreaterThan = false;

            for (int histIndex = 0; histIndex < employeeCallLogHitory.Count; histIndex++)
            {
                CS_View_ProjectCalendar_CallLog callLogHistory = employeeCallLogHitory[histIndex];

                if (callLogHistory.CallLogTypeID.HasValue)
                {
                    if (callLogHistory.CallLogCallDate.HasValue)
                    {
                        if (callLogHistory.CallLogTypeID.Value == (int)Globals.CallEntry.CallType.AddedResource)
                        {
                            startLowerThan = callLogHistory.CallLogCallDate.Value.Date <= date.Date;

                            if (startLowerThan && employeeCallLogHitory.Count - 1 == histIndex)
                                endGreaterThan = true;
                        }

                        if (callLogHistory.CallLogTypeID.Value == (int)Globals.CallEntry.CallType.Parked)
                        {
                            endGreaterThan = callLogHistory.CallLogParkedDate.Value.Date >= date.Date;
                        }
                    }
                }

                activeAtDate = startLowerThan && endGreaterThan;

                if (activeAtDate)
                    break;
            }

            return activeAtDate;
        }

        private bool JobWasActiveAtDate(IList<CS_Job_JobStatus> jobStatusHitory, DateTime date)
        {
            bool activeAtDate = false;
            bool startLowerThan = false;
            bool endGreaterThan = false;

            for (int histIndex = 0; histIndex < jobStatusHitory.Count; histIndex++)
            {
                CS_Job_JobStatus statusHistory = jobStatusHitory[histIndex];

                if (statusHistory.JobStartDate.HasValue)
                {
                    startLowerThan = statusHistory.JobStartDate.Value.Date <= date.Date;

                    if (startLowerThan && jobStatusHitory.Count - 1 == histIndex)
                        endGreaterThan = true;
                }
                else if (statusHistory.JobStatusId == (int)Globals.JobRecord.JobStatus.Preset || statusHistory.JobStatusId == (int)Globals.JobRecord.JobStatus.PresetPurchase)
                {
                    CS_PresetInfo presetInfo = _presetInfoRepository.Get(w => w.Active && w.JobId == statusHistory.JobID);

                    if (presetInfo != null)
                    {
                        if (presetInfo.Date.HasValue)
                            startLowerThan = presetInfo.Date.Value <= date.Date;
                        else
                            startLowerThan = statusHistory.ModificationDate <= date.Date;
                    }
                }


                if (statusHistory.JobStatusId == (int)Globals.JobRecord.JobStatus.Cancelled || statusHistory.JobStatusId == (int)Globals.JobRecord.JobStatus.ClosedHold ||
                    statusHistory.JobStatusId == (int)Globals.JobRecord.JobStatus.Lost)
                {
                    endGreaterThan = statusHistory.ModificationDate.Date >= date.Date;
                }
                else if (statusHistory.JobStatusId == (int)Globals.JobRecord.JobStatus.Preset || statusHistory.JobStatusId == (int)Globals.JobRecord.JobStatus.PresetPurchase)
                {
                    endGreaterThan = true;
                }

                if (statusHistory.JobCloseDate.HasValue)
                    endGreaterThan = statusHistory.JobCloseDate.Value.Date >= date.Date;

                activeAtDate = startLowerThan && endGreaterThan;

                if (activeAtDate)
                    break;
            }

            return activeAtDate;
        }

        #endregion

        #endregion

        #region [ IDisposable Implementation ]

        public void Dispose()
        {
            _jobRepository = null;
            _jobStatusRepository = null;
            _jobDivisionRepository = null;
            _priceTypeRepository = null;
            _callLogRepository = null;
            _cityRepository = null;
            _competitorRepository = null;
            _contactRepository = null;
            _countryRepository = null;
            _customerInfoRepository = null;
            _contactRepository = null;
            _divisionRepository = null;
            _employeeRepository = null;
            _frequencyRepository = null;
            _jobActionRepository = null;
            _jobCategoryRepository = null;
            _jobDescriptionRepository = null;
            _jobInfoRepository = null;
            _jobPermitRepository = null;
            _jobPhotoReportRepository = null;
            _jobTypeRepository = null;
            _locationInfoRepository = null;
            _lostJobInfoRepository = null;
            _lostJobReasonRepository = null;
            _presetInfoRepository = null;
            _jobDataRepository = null;
            _jobLocalEquipmentTypeRepository = null;
            _projectCalendarAllocationRepository = null;
            _projectCalendarReservedRepository = null;
            _projectCalendarCallLogRepository = null;
            _turnoverNonActiveReportRepository = null;
            _turnoverActiveReportRepository = null;

            _scopeOfWorkRepository = null;
            _specialPricingRepository = null;
            _stateRepository = null;
            _zipCodeRepository = null;
            _callLogResourceRepository = null;
            _callLogCallCriteriaEmailRepository = null;

            _unitOfWork.Dispose();
            _unitOfWork = null;
        }

        #endregion
    }
}
