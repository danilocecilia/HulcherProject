using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Core;
using System.Transactions;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields;
using Hulcher.OneSource.CustomerService.Business.WebControls.Utils;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Base;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Xml;
using Hulcher.OneSource.CustomerService.DataContext.VO;

namespace Hulcher.OneSource.CustomerService.Business.Model
{
    public class CallLogModel : IDisposable
    {
        #region [ Attributes ]

        /// <summary>
        /// Unit of Work used to access the database/in-memory context
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// Repository class for CS_CallLog
        /// </summary>
        private IRepository<CS_CallLog> _callLogRepository;

        /// <summary>
        /// Repository class for CS_CallLogResource
        /// </summary>
        private IRepository<CS_CallLogResource> _callLogResourceRepository;

        /// <summary>
        /// Repository class for CS_Resource
        /// </summary>
        private IRepository<CS_Resource> _resourceRepository;

        /// <summary>
        /// Repository class for CS_CallType
        /// </summary>
        private ICachedRepository<CS_CallType> _callTypeRepository;

        /// <summary>
        /// Repository class for CS_PrimaryCallType
        /// </summary>
        private IRepository<CS_PrimaryCallType> _primaryCallTypeRepository;

        /// <summary>
        /// Repository class for CS_Employee
        /// </summary>
        private IRepository<CS_Employee> _employeeRepository;

        /// <summary>
        /// Repository class for CS_Job
        /// </summary>
        private IRepository<CS_Job> _jobRepository;

        /// <summary>
        /// Repository class for CS_Hotel
        /// </summary>
        private IRepository<CS_Hotel> _hotelRepository;

        /// <summary>
        /// Repository class for CS_Subcontractor
        /// </summary>
        private IRepository<CS_Subcontractor> _subcontractorRepository;

        /// <summary>
        /// Repository class for CS_CallLogCallCriteriaEmail
        /// </summary>
        private IRepository<CS_CallLogCallCriteriaEmail> _callLogCallCriteriaEmailRepository;

        /// <summary>
        /// Repository class for CS_View_JobCallLog
        /// </summary>
        private IRepository<CS_View_JobCallLog> _jobCallLogRepository;

        /// <summary>
        /// Repository class for CS_Equipment
        /// </summary>
        private IRepository<CS_Equipment> _equipmentRepository;

        /// <summary>
        /// Repository class for CS_Division
        /// </summary>
        private IRepository<CS_Division> _divisionRepository;

        /// <summary>
        /// Instance of the Equipment Model class
        /// </summary>
        private EquipmentModel _equipmentModel;

        /// <summary>
        /// Instance of the DPI Model class
        /// </summary>
        private DPIModel _dpiModel;

        /// <summary>
        /// Repository class for CS_CallLog_LocalEquipmentType
        /// </summary>
        private IRepository<CS_CallLog_LocalEquipmentType> _callLogEquipmentTypeRepository;

        #endregion

        #region [ Constructors ]

        public CallLogModel()
        {
            _unitOfWork = new EFUnitOfWork();
            InstanceRepositories();
        }

        public CallLogModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            InstanceRepositories();
        }

        private void InstanceRepositories()
        {
            _callLogRepository = new EFRepository<CS_CallLog>();
            _callLogRepository.UnitOfWork = _unitOfWork;

            _callLogResourceRepository = new EFRepository<CS_CallLogResource>();
            _callLogResourceRepository.UnitOfWork = _unitOfWork;

            _resourceRepository = new EFRepository<CS_Resource>();
            _resourceRepository.UnitOfWork = _unitOfWork;

            _callTypeRepository = new CachedRepository<CS_CallType>();
            _callTypeRepository.UnitOfWork = _unitOfWork;

            _primaryCallTypeRepository = new EFRepository<CS_PrimaryCallType>();
            _primaryCallTypeRepository.UnitOfWork = _unitOfWork;

            _employeeRepository = new EFRepository<CS_Employee>();
            _employeeRepository.UnitOfWork = _unitOfWork;

            _jobRepository = new EFRepository<CS_Job>();
            _jobRepository.UnitOfWork = _unitOfWork;

            _hotelRepository = new EFRepository<CS_Hotel>();
            _hotelRepository.UnitOfWork = _unitOfWork;

            _subcontractorRepository = new EFRepository<CS_Subcontractor>();
            _subcontractorRepository.UnitOfWork = _unitOfWork;

            _jobCallLogRepository = new EFRepository<CS_View_JobCallLog>();
            _jobCallLogRepository.UnitOfWork = _unitOfWork;

            _divisionRepository = new EFRepository<CS_Division>();
            _divisionRepository.UnitOfWork = _unitOfWork;

            _callLogCallCriteriaEmailRepository = new EFRepository<CS_CallLogCallCriteriaEmail>();
            _callLogCallCriteriaEmailRepository.UnitOfWork = _unitOfWork;

            _equipmentRepository = new EFRepository<CS_Equipment>();
            _equipmentRepository.UnitOfWork = _unitOfWork;

            _callLogEquipmentTypeRepository = new EFRepository<CS_CallLog_LocalEquipmentType>();
            _callLogEquipmentTypeRepository.UnitOfWork = _unitOfWork;
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Get the entity call log by job id and calllog id
        /// </summary>
        /// <param name="jobId">job id</param>
        /// <param name="callLogId">call log id</param>
        /// <returns>entity</returns>
        public CS_CallLog LoadCallLog(int jobId, int callLogId)
        {
            return _callLogRepository.Get(w => w.JobID == jobId && w.ID == callLogId);
        }

        /// <summary>
        /// Lists all the Call Logs for a specific Job
        /// </summary>
        /// <param name="jobId">ID of the selected Job</param>
        /// <returns>Entity containing the Call Log Record info</returns>
        public IList<CS_CallLog> ListAllJobCallLogs(long jobId)
        {
            //return CallLogDao.Singleton.ListJobCallLogs(jobId);
            return _callLogRepository.ListAll(
                e => e.JobID == jobId && e.Active == true,
                e => e.CallDate,
                false,
                new string[] { "CS_CallType" });
        }

        /// <summary>
        /// List all the Call Logs for a specific Job, filtered by User input
        /// </summary>
        /// <param name="filterType">Enum that correspond to the User selected filter</param>
        /// <param name="value">Value inputed by the user</param>
        /// <param name="jobId">ID of the selected Job</param>
        /// <returns>Entity containing the Call Log Record info</returns>
        public IList<CS_CallLog> ListFilteredCallLogs(Globals.JobRecord.FilterType filterType, string value, long jobId)
        {
            //return CallLogDao.Singleton.ListFilteredJobCallLogs(filterType, value, jobId);
            IList<CS_CallLog> returnItem = null;

            string[] includeList = new string[] { "CS_CallType" };

            switch (filterType)
            {
                case Globals.JobRecord.FilterType.Date:
                    DateTime dt;
                    if (DateTime.TryParse(value, new System.Globalization.CultureInfo("en-US"), System.Globalization.DateTimeStyles.None, out dt))
                    {
                        returnItem = _callLogRepository.ListAll(
                            e => e.JobID == jobId
                                && e.Active == true
                                && e.CallDate.Year == dt.Year
                                && e.CallDate.Month == dt.Month
                                && e.CallDate.Day == dt.Day,
                            e => e.CallDate, false,
                            includeList);
                    }
                    break;
                case Globals.JobRecord.FilterType.Time:
                    DateTime dt1;
                    if (DateTime.TryParse(value, new System.Globalization.CultureInfo("en-US"), System.Globalization.DateTimeStyles.None, out dt1))
                    {
                        returnItem = _callLogRepository.ListAll(
                            e => e.JobID == jobId
                                && e.Active == true
                                && e.CallDate.Hour == dt1.Hour
                                && e.CallDate.Minute == dt1.Minute,
                            e => e.CallDate, false,
                            includeList);
                    }
                    break;
                case Globals.JobRecord.FilterType.Type:
                    returnItem = _callLogRepository.ListAll(
                        e => e.JobID == jobId
                            && e.Active == true
                            && e.CS_CallType.Description.Contains(value),
                        e => e.CallDate, false,
                            includeList);
                    break;
                case Globals.JobRecord.FilterType.User:
                    returnItem = _callLogRepository.ListAll(
                        e => e.JobID == jobId
                            && e.Active == true
                            && e.CreatedBy.Contains(value),
                        e => e.CallDate, false,
                            includeList);
                    break;
                default:
                    returnItem = new List<CS_CallLog>();
                    break;
            }

            return returnItem;
        }

        /// <summary>
        /// List All CallTypes
        /// </summary>
        /// <returns>Call Type List</returns>
        public IList<CS_CallType> ListAllCallType()
        {
            //return CallTypeDao.Singleton.ListAll();
            return _callTypeRepository.ListAll(e => e.Active == true, e => e.Description, true);
        }

        /// <summary>
        /// List All CallTypes filtered by JobRelated
        /// </summary>
        /// <returns>Call Type List</returns>
        public List<CS_CallType> ListAllCallType(bool isGeneralLog)
        {
            //return CallTypeDao.Singleton.ListAll();
            return _callTypeRepository.ListAll(e => e.Active && e.IsAutomaticProcess == false && e.CS_PrimaryCallType_CallType.Any(f => f.CallTypeID == e.ID && f.CS_PrimaryCallType.JobRelated != isGeneralLog), e => e.Description, true).ToList();
        }

        /// <summary>
        /// List All Primary Call Types for Call Criteria
        /// </summary>
        /// <returns></returns>
        public IList<CS_PrimaryCallType> ListAllPrimaryCallTypesForCallCriteria()
        {
            return _primaryCallTypeRepository.ListAll(e => e.Active && e.JobRelated, "CS_PrimaryCallType_CallType", "CS_PrimaryCallType_CallType.CS_CallType");
        }

        /// <summary>
        /// List All Primary Call Types
        /// </summary>
        /// <param name="jobRelated">TRUE will list only job-related primary call types, FALSE will list only non-job-related.</param>
        /// <returns>Primary Call Type List</returns>
        public IList<CS_PrimaryCallType> ListAllPrimaryCallTypes(bool jobRelated)
        {
            return _primaryCallTypeRepository.ListAll(e => e.Active == true && e.JobRelated == jobRelated);
        }

        /// <summary>
        /// List All PrimaryCallTypes by CallTypeId
        /// </summary>
        /// <param name="callTypeId">ID of the CallType to filter</param>
        /// <returns>PrimaryCallType List</returns>
        public IList<CS_PrimaryCallType> ListAllPrimaryCallTypesByCallType(int callTypeId, bool isGeneralLog)
        {
            return _primaryCallTypeRepository.ListAll(e => e.Active == true && e.JobRelated != isGeneralLog && e.CS_PrimaryCallType_CallType.Any(f => f.PrimaryCallTypeID == e.ID && f.CallTypeID == callTypeId));
        }

        /// <summary>
        /// List CallTypes By Primary CallType
        /// </summary>
        /// <param name="primaryCallTypeId">Primary CallType Identifier</param>
        /// <returns>Call Type List</returns>
        public IList<CS_CallType> FilterByPrimaryCallType(int primaryCallTypeId)
        {
            //return CallTypeDao.Singleton.FilterByPrimaryCallType(primaryCallTypeId);
            return _callTypeRepository.ListAll(
                e => e.CS_PrimaryCallType_CallType.Any(f => f.PrimaryCallTypeID == primaryCallTypeId)
                    && e.Active == true);
        }

        public IList<CS_CallType> GetCallTypeByPrimaryCallTypeAutomatic(int primaryCallTypeId)
        {
            return _callTypeRepository.ListAll(
               e => e.CS_PrimaryCallType_CallType.Any(f => f.PrimaryCallTypeID == primaryCallTypeId)
                   && e.Active == true && e.IsAutomaticProcess == false);
        }

        /// <summary>
        /// Returns a Job Entity, loaded with some related entities
        /// </summary>
        /// <param name="jobId">ID of the Job</param>
        /// <returns>Job Entity</returns>
        public CS_Job GetJobInformationForCallEntry(int jobId)
        {
            //return JobDao.Singleton.GetJobInfoForCallEntry(jobId);
            return _jobRepository.Get(
                e => e.ID == jobId
                    && e.Active == true,
                new string[] { "CS_CustomerInfo", "CS_CustomerInfo.CS_Customer", "CS_JobDivision",
                "CS_JobDivision.CS_Division", "CS_LocationInfo", "CS_LocationInfo.CS_City", "CS_LocationInfo.CS_State" });
        }

        /// <summary>
        /// Gets The Identified PrimaryCallType
        /// </summary>
        /// <param name="primaryCallType">PrimaryCallType Identifier</param>
        /// <returns>CS_PrimaryCallType Entity</returns>
        public CS_PrimaryCallType GetPrimaryCallType(int primaryCallType)
        {
            return _primaryCallTypeRepository.Get(e => e.ID == primaryCallType);
        }

        /// <summary>
        /// Gets The Identified CallType
        /// </summary>
        /// <param name="callType">CallType Identifier</param>
        /// <returns>CS_CallType Entity</returns>
        public CS_CallType GetCallType(int callType)
        {
            return _callTypeRepository.Get(e => e.ID == callType);
        }

        /// <summary>
        /// Gets CallLog by Job Identifier
        /// </summary>
        /// <param name="jobId">Job Identifier</param>
        /// <param name="callType">CallType Description</param>
        /// <returns>CS_CallLog Entity</returns>
        public CS_CallLog GetLastCallEntryByFilter(int jobId, string callType)
        {
            return _callLogRepository.ListAll(e => e.JobID == jobId && (e.CS_CallType.Description == callType || string.IsNullOrEmpty(callType)) && e.Active, o => o.ID, false, new string[0]).FirstOrDefault();
        }

        /// <summary>
        /// List Call Logs by Id
        /// </summary>
        /// <param name="callLogIdList">Call Log Id list</param>
        /// <returns>A list of Call Log</returns>
        public IList<CS_CallLog> ListCallLogByIdList(IList<int> callLogIdList)
        {
            return _callLogRepository.ListAll(e => e.Active == true && callLogIdList.Contains(e.ID), new string[] { "CS_CallType", "CS_Job" }).OrderByDescending(order => order.CreationDate).ToList();
        }

        /// <summary>
        /// Saves CalLog data
        /// </summary>
        public void SaveCallLogData(CS_CallLog callLog, List<CS_CallLogResource> callLogResourcePersons, List<CS_Resource> callLogResourceResource,IList<CS_CallLog_LocalEquipmentType> localEquipmentTypeList,  bool update, bool copyToGeneralLog)
        {
            if (callLog != null)
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted }))
                {
                    CS_CallLog newCallLogForGeneralLog = null;
                    List<CS_CallLogResource> contactsList = callLogResourcePersons.Where(e => e.CS_Contact != null).ToList();
                    List<CS_CallLogResource> employeesList = callLogResourcePersons.Where(e => e.CS_Employee != null).ToList();
                    DateTime? actionDate = GetCallLogActionDateTime(callLog.Xml);
                    IList<CS_PrimaryCallType> primaryCallType = _primaryCallTypeRepository.ListAll(e => e.Active == true && e.CS_PrimaryCallType_CallType.Any(f => f.PrimaryCallTypeID == e.ID && f.CallTypeID == callLog.CallTypeID));
                    bool isPersonCallType = false;
                    for (int i = 0; i < primaryCallType.Count; i++)
                    {
                        if (primaryCallType[i].PersonsPrimaryCallType)
                        {
                            isPersonCallType = true;
                            break;
                        }
                    }

                    try
                    {
                        if (copyToGeneralLog)
                        {
                            // New Request - Input Job Number that the general log was copied from inside the details of the general log
                            string note = callLog.Note;
                            CS_Job job = _jobRepository.Get(e => e.ID == callLog.JobID, "CS_JobInfo");
                            if (null != job)
                                note += string.Format("Copied from Job: <Text>{0}<BL>", job.NumberOrInternalTracking);

                            newCallLogForGeneralLog = new CS_CallLog();
                            newCallLogForGeneralLog.JobID = Globals.GeneralLog.ID;
                            newCallLogForGeneralLog.CallTypeID = callLog.CallTypeID;
                            newCallLogForGeneralLog.PrimaryCallTypeId = callLog.PrimaryCallTypeId;
                            newCallLogForGeneralLog.CallDate = callLog.CallDate;
                            newCallLogForGeneralLog.CalledInByEmployee = callLog.CalledInByEmployee;
                            newCallLogForGeneralLog.CalledInByCustomer = callLog.CalledInByCustomer;
                            newCallLogForGeneralLog.Xml = callLog.Xml;
                            newCallLogForGeneralLog.Note = note;
                            newCallLogForGeneralLog.CreatedBy = callLog.CreatedBy;
                            newCallLogForGeneralLog.CreationDate = callLog.CreationDate;
                            newCallLogForGeneralLog.ModifiedBy = callLog.ModifiedBy;
                            newCallLogForGeneralLog.ModificationDate = callLog.ModificationDate;
                            newCallLogForGeneralLog.Active = callLog.Active;
                            newCallLogForGeneralLog.ShiftTransferLog = false;
                            newCallLogForGeneralLog.HasGeneralLog = false;
                            newCallLogForGeneralLog.UserCall = callLog.UserCall;
                            //newCallLogForGeneralLog.CreationID;
                            //newCallLogForGeneralLog.ModificationID;
                            newCallLogForGeneralLog.CalledInByExternal = callLog.CalledInByExternal;
                                
                            newCallLogForGeneralLog = _callLogRepository.Add(newCallLogForGeneralLog);

                    
                            callLog.HasGeneralLog = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("There was an error while trying to copy the Call Entry to the General Log. Please verify the content of the fields and try again.", ex);
                    }

                    try
                    {
                        if (string.IsNullOrEmpty(callLog.Note) && !string.IsNullOrEmpty(callLog.Xml))
                            callLog.Note = FormatDynamicFieldsData(GetDynamicFieldControlsProperties(callLog.Xml));

                        if (!update)
                        {
                            callLog = _callLogRepository.Add(callLog);

                            if (localEquipmentTypeList != null)
                            {
                                for (int i = 0; i < localEquipmentTypeList.Count; i++)
                                {
                                    localEquipmentTypeList[i].CallLogID = callLog.ID;
                                }
                                SaveCallLogEquipmentTypeList(localEquipmentTypeList);
                            }
                        }
                        else
                        {
                            callLog = _callLogRepository.Update(callLog);
                            if (localEquipmentTypeList != null)
                            {
                                for (int i = 0; i < localEquipmentTypeList.Count; i++)
                                {
                                    localEquipmentTypeList[i].CallLogID = callLog.ID;
                                }
                                UpdateCallLogEquipmentTypeList(callLog.ID, callLog.ModifiedBy, localEquipmentTypeList);
                            }
                        }
                         
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("There was an error saving the Call Entry data. Please verify the content of the fields and try again.", ex);
                    }

                    if (!update)
                    {
                        try
                        {
                            for (int i = 0; i < contactsList.Count; i++)
                            {
                                CS_CallLogResource item = contactsList[i];

                                SaveCallLogResourceData(callLog, item);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("There was an error saving the Contacts related to the Call Entry data. Please verify the content of the fields and try again.", ex);
                        }

                        try
                        {
                            for (int i = 0; i < employeesList.Count; i++)
                            {
                                CS_CallLogResource item = employeesList[i];

                                SaveCallLogResourceData(callLog, item);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("There was an error saving the Employees related to the Call Entry data. Please verify the content of the fields and try again.", ex);
                        }

                        try
                        {
                            for (int i = 0; i < callLogResourceResource.Count; i++)
                            {
                                CS_Resource item = callLogResourceResource[i];

                                SaveCallLogResourceData(callLog, item);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("There was an error saving the Resources related to the Call Entry data. Please verify the content of the fields and try again.", ex);
                        }

                        try
                        {
                            using (CallCriteriaModel callCriteriaModel = new CallCriteriaModel())
                            {
                                CS_CallLog currentCallLog = _callLogRepository.Get(e => e.ID == callLog.ID);
                                if (null != currentCallLog)
                                {
                                    List<EmailVO> lstEmailVO = callCriteriaModel.ListReceiptsByCallLog(currentCallLog.CallTypeID.ToString(), currentCallLog.JobID, currentCallLog).ToList();
                                    if (null != lstEmailVO && lstEmailVO.Count > 0)
                                        SaveCallLogCallCriteriaEmail(lstEmailVO, callLog);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("There was an error saving the Call Criteria related to the Call Entry data. Please verify the content of the fields and try again.", ex);
                        }

                        //Logicaly Delete All CS_Resource Related in a Parked CallType
                        if (callLog.CallTypeID == (int)Globals.CallEntry.CallType.Parked)
                        {
                            using (ResourceAllocationModel resourceModel = new ResourceAllocationModel())
                            {
                                resourceModel.RemoveResources(callLogResourceResource);
                            }
                        }

                        // Verify Missed ETA rule
                        if (callLog.CallTypeID == (int)Globals.CallEntry.CallType.ATA)
                        {
                            try
                            {
                                int etaCallLogId = (int)Globals.CallEntry.CallType.ETA;
                                int ataCallLogId = (int)Globals.CallEntry.CallType.ATA;
                                List<MissedETAVO> missedETAList = new List<MissedETAVO>();
                                CS_CallLog firstETA = null;
                                CS_CallLog lastATA = null;
                                bool useETA = false;

                                foreach (CS_Resource resource in callLogResourceResource)
                                {
                                    // List all ETA and ATA call types for this resource in this job
                                    IList<CS_CallLog> callLogList = _callLogRepository.ListAll(
                                        e => (e.CallTypeID == etaCallLogId || e.CallTypeID == ataCallLogId)
                                            && e.JobID == callLog.JobID
                                            && e.Active
                                            && e.ID != callLog.ID
                                            && e.CS_CallLogResource.Any(f => f.Active &&
                                                (f.EmployeeID.HasValue && f.EmployeeID.Value == resource.EmployeeID.Value) ||
                                                f.EquipmentID.HasValue && f.EquipmentID.Value == resource.EquipmentID.Value));

                                    if (null != callLogList && callLogList.Count > 0)
                                    {
                                        firstETA = null;
                                        lastATA = null;

                                        // Check if there's an existing ATA for this resource
                                        if (callLogList.Any(e => e.CallTypeID == ataCallLogId))
                                        {
                                            // Find the last existing ATA
                                            foreach (CS_CallLog currentCallLog in callLogList.Where(e => e.CallTypeID == ataCallLogId))
                                            {
                                                if (lastATA == null)
                                                    lastATA = currentCallLog;
                                                else if (GetCallLogActionDateTime(lastATA.Xml).Value < GetCallLogActionDateTime(currentCallLog.Xml).Value)
                                                    lastATA = currentCallLog;
                                            }
                                        }

                                        // Find first ETA after any ATA (if exists)
                                        foreach (CS_CallLog currentCallLog in callLogList.Where(e => e.CallTypeID == etaCallLogId))
                                        {
                                            useETA = true;
                                            if (lastATA != null)
                                            {
                                                // If exists a last ATA, check if the ATA date is lower than the current ETA date
                                                // if not, do not use this ETA
                                                if (GetCallLogActionDateTime(lastATA.Xml).Value > GetCallLogActionDateTime(currentCallLog.Xml).Value)
                                                    useETA = false;
                                            }

                                            if (useETA)
                                            {
                                                if (firstETA == null)
                                                    firstETA = currentCallLog;
                                                else if (GetCallLogActionDateTime(firstETA.Xml).Value > GetCallLogActionDateTime(currentCallLog.Xml).Value)
                                                    firstETA = currentCallLog;
                                            }
                                        }

                                        // If exists an ETA not finished by an ATA before, add to Missed ETA listing
                                        if (null != firstETA)
                                        {
                                            missedETAList.Add(
                                                new MissedETAVO()
                                                {
                                                    ETACallLogId = firstETA.ID,
                                                    EmployeeId = resource.EmployeeID,
                                                    EquipmentId = resource.EquipmentID,
                                                    MissedETAMinutes = GetMissedETAMinutes(firstETA, callLog),
                                                    ResourceData = resource
                                                }
                                            );
                                        }
                                    }
                                }

                                for (int i = 0; i < missedETAList.Count; i++)
                                {
                                    if (missedETAList[i].MissedETAMinutes != 0 && (missedETAList[i].MissedETAMinutes > 15 || missedETAList[i].MissedETAMinutes < -15))
                                    {
                                        // Group resources with the same Missed ETA
                                        List<MissedETAVO> newMissedETAList = new List<MissedETAVO>();
                                        newMissedETAList.Add(
                                            new MissedETAVO()
                                            {
                                                ETACallLogId = missedETAList[i].ETACallLogId,
                                                EmployeeId = missedETAList[i].EmployeeId,
                                                EquipmentId = missedETAList[i].EquipmentId,
                                                MissedETAMinutes = missedETAList[i].MissedETAMinutes,
                                                ResourceData = missedETAList[i].ResourceData
                                            }
                                        );
                                        for (int j = missedETAList.Count - 1; j > i; j--)
                                        {
                                            if (missedETAList[i].ETACallLogId == missedETAList[j].ETACallLogId &&
                                                missedETAList[i].MissedETAMinutes == missedETAList[j].MissedETAMinutes)
                                            {
                                                newMissedETAList.Add(
                                                    new MissedETAVO()
                                                    {
                                                        ETACallLogId = missedETAList[j].ETACallLogId,
                                                        EmployeeId = missedETAList[j].EmployeeId,
                                                        EquipmentId = missedETAList[j].EquipmentId,
                                                        MissedETAMinutes = missedETAList[j].MissedETAMinutes,
                                                        ResourceData = missedETAList[j].ResourceData
                                                    }
                                                );

                                                missedETAList.RemoveAt(j);
                                            }
                                        }

                                        // Insert Missed ETA call log
                                        SaveCallLogDataMissedETA(callLog, newMissedETAList);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("There was an error generating the Missed ETA Call Entry. Please verify the content of the fields and try again.", ex);
                            }
                        }

                        List<int> employees = new List<int>();
                        List<int> equipments = new List<int>();

                        if (null != callLogResourceResource && callLogResourceResource.Count > 0)
                            employees = callLogResourceResource.Where(e => e.EmployeeID.HasValue).Select(e => e.EmployeeID.Value).ToList();

                        if (null != callLogResourcePersons && callLogResourcePersons.Count > 0)
                            employees.AddRange(callLogResourcePersons.Where(e => e.EmployeeID.HasValue).Select(e => e.EmployeeID.Value).ToList());

                        if (null != callLogResourceResource && callLogResourceResource.Count > 0)
                            equipments = callLogResourceResource.Where(e => e.EquipmentID.HasValue).Select(e => e.EquipmentID.Value).ToList();

                        VerifyDPICalculate(callLog, employees, equipments);
                    }
                    else
                    {
                        List<CS_CallLogResource> oldList = _callLogResourceRepository.ListAll(e => e.CallLogID == callLog.ID && e.Active).ToList();

                        if (isPersonCallType)
                        {
                            try
                            {
                                List<CS_CallLogResource> removedList = oldList.Where
                                    (e => 
                                        (!e.EmployeeID.HasValue || !callLogResourcePersons.Any(f => f.EmployeeID.HasValue && f.EmployeeID == e.EmployeeID)) && 
                                        (!e.ContactID.HasValue || !callLogResourcePersons.Any(f => f.ContactID.HasValue && f.ContactID == e.ContactID)) && 
                                        !e.EquipmentID.HasValue
                                    ).ToList();
                                List<CS_CallLogResource> updateList = oldList.Where
                                    (e =>
                                        (!e.EmployeeID.HasValue || callLogResourcePersons.Any(f => f.EmployeeID.HasValue && f.EmployeeID == e.EmployeeID)) &&
                                        (!e.ContactID.HasValue || callLogResourcePersons.Any(f => f.ContactID.HasValue && f.ContactID == e.ContactID)) &&
                                        !e.EquipmentID.HasValue
                                    ).ToList();
                                List<CS_CallLogResource> addedList = callLogResourcePersons.Where
                                    (e =>
                                        (!e.EmployeeID.HasValue || !oldList.Any(f => f.EmployeeID.HasValue && f.EmployeeID == e.EmployeeID)) &&
                                        (!e.ContactID.HasValue || !oldList.Any(f => f.ContactID.HasValue && f.ContactID == e.ContactID)) &&
                                        !e.EquipmentID.HasValue
                                    ).ToList();

                                if (removedList.Count > 0)
                                {
                                    for (int i = 0; i < removedList.Count; i++)
                                    {
                                        removedList[i].ModificationDate = DateTime.Now;
                                        removedList[i].ModifiedBy = callLog.ModifiedBy;
                                        removedList[i].Active = false;
                                    }

                                    _callLogResourceRepository.UpdateList(removedList);
                                }

                                if (updateList.Count > 0)
                                {
                                    for (int i = 0; i < updateList.Count; i++)
                                    {
                                        CS_CallLogResource resource = callLogResourcePersons.FirstOrDefault
                                            (w => w.EmployeeID == updateList[i].EmployeeID && w.ContactID == updateList[i].ContactID);
                                        updateList[i].ActionDate = actionDate;
                                        updateList[i].ModificationDate = DateTime.Now;
                                        updateList[i].ModifiedBy = callLog.ModifiedBy;
                                        updateList[i].Active = true;
                                        updateList[i].InPerson = resource.InPerson;
                                        updateList[i].Voicemail = resource.Voicemail;
                                        updateList[i].Notes = resource.Notes;
                                    }

                                    _callLogResourceRepository.UpdateList(updateList);
                                }

                                if (addedList.Count > 0)
                                {
                                    for (int i = 0; i < addedList.Count; i++)
                                    {
                                        addedList[i].ActionDate = actionDate;
                                        addedList[i].CreatedBy = callLog.ModifiedBy;
                                        addedList[i].CreationDate = DateTime.Now;
                                        addedList[i].ModifiedBy = callLog.ModifiedBy;
                                        addedList[i].ModificationDate = DateTime.Now;
                                        addedList[i].CallLogID = callLog.ID;

                                        _callLogResourceRepository.Add(addedList[i]);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("There was an error updating the Call Log Persons list.", ex);
                            }
                        }

                        try
                        {
                            if (!isPersonCallType)
                            {
                                List<CS_CallLogResource> removedList = oldList.Where(e => e.Active && ((e.EquipmentID.HasValue && !callLogResourceResource.Any(f => f.EquipmentID == e.EquipmentID)) || (e.EmployeeID.HasValue && !callLogResourceResource.Any(f => f.EmployeeID == e.EmployeeID)))).ToList();
                                List<CS_CallLogResource> updateList = oldList.Where(e => e.Active && ((e.EquipmentID.HasValue && callLogResourceResource.Any(f => f.EquipmentID.HasValue && f.EquipmentID == e.EquipmentID)) || (e.EmployeeID.HasValue && callLogResourceResource.Any(f => f.EmployeeID.HasValue && f.EmployeeID == e.EmployeeID)))).ToList();
                                List<CS_Resource> addedList = callLogResourceResource.Where(e => !oldList.Any(f => f.EquipmentID.HasValue && f.EquipmentID == e.EquipmentID) && !oldList.Any(f => f.EmployeeID.HasValue && f.EmployeeID == e.EmployeeID)).ToList();

                                if (removedList.Count > 0)
                                {
                                    for (int i = 0; i < removedList.Count; i++)
                                    {
                                        removedList[i].ModificationDate = DateTime.Now;
                                        removedList[i].ModifiedBy = callLog.ModifiedBy;
                                        removedList[i].Active = false;
                                    }
                                    _callLogResourceRepository.UpdateList(removedList);
                                }

                                if (updateList.Count > 0)
                                {
                                    for (int i = 0; i < updateList.Count; i++)
                                    {
                                        updateList[i].ActionDate = actionDate;
                                        updateList[i].ModificationDate = DateTime.Now;
                                        updateList[i].ModifiedBy = callLog.ModifiedBy;
                                        updateList[i].Active = true;
                                    }
                                    _callLogResourceRepository.UpdateList(updateList);
                                }

                                if (addedList.Count > 0)
                                {
                                    for (int i = 0; i < addedList.Count; i++)
                                    {
                                        CS_Resource item = addedList[i];

                                        SaveCallLogResourceData(callLog, item);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("There was an error updating the Call Log Resources list.", ex);
                        }

                        try
                        {
                            List<int> employees = callLogResourcePersons.Where(e => e.EmployeeID.HasValue).Select(e => e.EmployeeID.Value).ToList();
                            List<int> equipments = callLogResourceResource.Where(e => e.EquipmentID.HasValue).Select(e => e.EquipmentID.Value).ToList();

                            VerifyDPICalculate(callLog, employees, equipments);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("There was an error updating DPI information", ex);
                        }
                    }

                    if (copyToGeneralLog)
                    {
                        try
                        {
                            for (int i = 0; i < contactsList.Count; i++)
                            {
                                CS_CallLogResource item = contactsList[i];

                                SaveCallLogResourceData(newCallLogForGeneralLog, item);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("There was an error saving the Contacts related to the General Log Call Entry data. Please verify the content of the fields and try again.", ex);
                        }

                        try
                        {
                            for (int i = 0; i < employeesList.Count; i++)
                            {
                                CS_CallLogResource item = employeesList[i];

                                SaveCallLogResourceData(newCallLogForGeneralLog, item);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("There was an error saving the Employees related to the General Log Call Entry data. Please verify the content of the fields and try again.", ex);
                        }

                        try
                        {
                            for (int i = 0; i < callLogResourceResource.Count; i++)
                            {
                                CS_Resource item = callLogResourceResource[i];

                                SaveCallLogResourceData(newCallLogForGeneralLog, item);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("There was an error saving the Resources related to the General Log Call Entry data. Please verify the content of the fields and try again.", ex);
                        }
                    }

                    _dpiModel = new DPIModel();

                    if (callLog.CallTypeID == (int)Globals.CallEntry.CallType.ATA)
                    {
                        _dpiModel.UpdateDpiTimeArrival(callLog, Globals.CallEntry.CallType.ATA);
                    }
                    else if (callLog.CallTypeID == (int)Globals.CallEntry.CallType.ETA)
                    {
                        _dpiModel.UpdateDpiTimeArrival(callLog, Globals.CallEntry.CallType.ETA);
                    }

                    scope.Complete();
                }
            }
        }

        public int GetMissedETAMinutes(CS_CallLog etaCallLog, CS_CallLog ataCallLog)
        {
            int minutes = 0;

            if (null != etaCallLog && !string.IsNullOrEmpty(etaCallLog.Xml))
            {
                DynamicFieldsAggregator etaAggregator = DynamicFieldsParser.GetAggregatorFromXml(etaCallLog.Xml);
                DynamicControls controlDateETA = etaAggregator.Controls.Find(delegate(DynamicControls match) { return match.Name == "dtpDate"; });
                DynamicControls controlTimeETA = etaAggregator.Controls.Find(delegate(DynamicControls match) { return match.Name == "txtTime"; });
                string etaDate = string.Format("{0} {1}", ((DynamicDatePickerXml)controlDateETA).Text.Value.ToShortDateString(), ((DynamicTimeXml)controlTimeETA).Text);
                DateTime dtEta = new DateTime();

                DynamicFieldsAggregator ataAggregator = DynamicFieldsParser.GetAggregatorFromXml(ataCallLog.Xml);
                DynamicControls controlDateATA = ataAggregator.Controls.Find(delegate(DynamicControls match) { return match.Name == "dtpDate"; });
                DynamicControls controlTimeATA = ataAggregator.Controls.Find(delegate(DynamicControls match) { return match.Name == "txtTime"; });
                string ataDate = string.Format("{0} {1}", ((DynamicDatePickerXml)controlDateATA).Text.Value.ToShortDateString(), ((DynamicTimeXml)controlTimeATA).Text);
                DateTime dtAta = new DateTime();

                if (DateTime.TryParse(etaDate, out dtEta) && DateTime.TryParse(ataDate, out dtAta))
                {
                    minutes = int.Parse(dtAta.Subtract(dtEta).TotalMinutes.ToString("0"));
                }
            }

            return minutes;
        }

        public void SaveCallLogDataMissedETA(CS_CallLog callLog, List<MissedETAVO> missedETAList)
        {
            List<CS_Resource> resourceList = new List<CS_Resource>();
            int minutes = 0;
            foreach (MissedETAVO missedETAItem in missedETAList)
            {
                minutes = missedETAItem.MissedETAMinutes;
                resourceList.Add(missedETAItem.ResourceData);
            }

            CS_CallLog missedETA = new CS_CallLog();

            missedETA.JobID = callLog.JobID;
            missedETA.PrimaryCallTypeId = (int)Globals.CallEntry.PrimaryCallType.ResourceUpdateAddedResources;
            missedETA.CallTypeID = (int)Globals.CallEntry.CallType.MissedETA;
            missedETA.CallDate = callLog.CallDate;
            missedETA.CalledInByEmployee = callLog.CalledInByEmployee;
            missedETA.CalledInByCustomer = callLog.CalledInByCustomer;
            missedETA.Note = string.Format("{0}Missed Minutes:<Text>{1} Minute(s)", callLog.Note, minutes);
            //missedETA.CreatedBy = callLog.CreatedBy;
            missedETA.CreatedBy = "System";
            //missedETA.ModifiedBy = callLog.ModifiedBy;
            missedETA.ModifiedBy = "System";
            missedETA.CreationDate = callLog.CreationDate;
            missedETA.ModificationDate = callLog.ModificationDate;
            missedETA.Active = callLog.Active;
            missedETA.Xml = string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?><DynamicFieldsAggregator xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><Controls><DynamicControls xsi:type=\"DynamicTextBoxXml\"><Name>txtMinutes</Name><Text>{0}</Text><Label><Text>Missed ETA Minutes:</Text><Css>dynamicLabel</Css></Label><Css>input</Css></DynamicControls></Controls></DynamicFieldsAggregator>", minutes);
            missedETA.UserCall = callLog.UserCall;
            missedETA.ShiftTransferLog = callLog.ShiftTransferLog;
            missedETA.HasGeneralLog = callLog.HasGeneralLog;

            SaveCallLogData(missedETA, new List<CS_CallLogResource>(), resourceList, new List<CS_CallLog_LocalEquipmentType>(), false, false);

        }

        /// <summary>
        /// Save/Update CallLogResource data
        /// </summary>
        private void SaveCallLogResouceData(CS_CallLog callLog, CS_CallLogResource item, bool update)
        {
            if (callLog != null)
            {
                item.ModificationDate = callLog.ModificationDate;
                item.ModifiedBy = callLog.ModifiedBy;

                if (!update)
                {
                    _callLogResourceRepository.Add(item);
                }
                else
                {
                    _callLogResourceRepository.Update(item);
                }
            }
        }

        /// <summary>
        /// Save/Update CallLogResource data
        /// </summary>
        private void SaveCallLogResourceData(CS_CallLog newCallLog, CS_CallLogResource newResource)
        {
            CS_CallLogResource newCallLogResource = new CS_CallLogResource();

            if (newCallLog != null)
            {
                if (newResource.ContactID.HasValue)
                {
                    newCallLogResource.ContactID = newResource.ContactID;
                    newCallLogResource.Type = (int)Globals.CallEntry.CallLogResourceType.Contact;
                }
                else
                {
                    newCallLogResource.EmployeeID = newResource.EmployeeID;
                    newCallLogResource.Type = (int)Globals.CallEntry.CallLogResourceType.Employee;
                }

                newCallLogResource.CreatedBy = newCallLog.CreatedBy;
                newCallLogResource.CreationDate = newCallLog.CreationDate;
                newCallLogResource.JobID = newCallLog.JobID;
                newCallLogResource.ModificationDate = newCallLog.ModificationDate;
                newCallLogResource.ModifiedBy = newCallLog.ModifiedBy;
                newCallLogResource.CallLogID = newCallLog.ID;
                newCallLogResource.Active = true;
                newCallLogResource.InPerson = newResource.InPerson;
                newCallLogResource.Voicemail = newResource.Voicemail;
                newCallLogResource.Notes = newResource.Notes;

                _callLogResourceRepository.Add(newCallLogResource);
            }
        }

        /// <summary>
        /// Save/Update CallLogResourceEquipment data
        /// </summary>
        private void SaveCallLogResourceData(CS_CallLog newCallLog, CS_Resource newEquipment)
        {
            CS_CallLogResource newCallLogResource = new CS_CallLogResource();
            DateTime? actionDate = GetCallLogActionDateTime(newCallLog.Xml);
            if (newCallLog != null)
            {
                if (newEquipment.EquipmentID.HasValue)
                    newCallLogResource.EquipmentID = newEquipment.EquipmentID;
                else
                    newCallLogResource.EmployeeID = newEquipment.EmployeeID;

                newCallLogResource.ActionDate = actionDate;
                newCallLogResource.CreatedBy = newCallLog.CreatedBy;
                newCallLogResource.CreationDate = newCallLog.CreationDate;
                newCallLogResource.JobID = newCallLog.JobID;
                newCallLogResource.ModificationDate = newCallLog.ModificationDate;
                newCallLogResource.ModifiedBy = newCallLog.ModifiedBy;
                newCallLogResource.Type = (int)Globals.CallEntry.CallLogResourceType.Equipment;
                newCallLogResource.CallLogID = newCallLog.ID;
                newCallLogResource.Active = true;
                _callLogResourceRepository.Add(newCallLogResource);

                if (newEquipment.EquipmentID.HasValue)
                {
                    if (newCallLog.CallTypeID.Equals((int)Globals.CallEntry.CallType.EquipmentDown) ||
                        newCallLog.CallTypeID.Equals((int)Globals.CallEntry.CallType.EquipmentUp))
                    {
                        CS_Equipment equipment = new CS_Equipment();
                        equipment.ID = newEquipment.EquipmentID.Value;
                        equipment.ModificationDate = newCallLog.ModificationDate;
                        equipment.ModifiedBy = newCallLog.ModifiedBy;
                        equipment.ModificationID = newCallLog.ModificationID;

                        CS_EquipmentDownHistory equipmentDownHistory = new CS_EquipmentDownHistory();
                        equipmentDownHistory.ModificationDate = newCallLog.ModificationDate;
                        equipmentDownHistory.ModifiedBy = newCallLog.ModifiedBy;
                        equipmentDownHistory.ModificationID = newCallLog.ModificationID;
                        equipmentDownHistory.EquipmentID = newEquipment.EquipmentID.Value;

                        DateTime? actionDateTime = GetCallLogActionDateTime(newCallLog.Xml);

                        if (actionDateTime.HasValue)
                        {
                            if (newCallLog.CallTypeID.Equals((int)Globals.CallEntry.CallType.EquipmentDown))
                            {
                                equipment.Status = Globals.EquipmentMaintenance.Status.Down.ToString();
                                equipmentDownHistory.DownHistoryStartDate = actionDateTime.Value;

                                // Getting Estimated Duration
                                DateTime? durationDateTime = GetDynamicFieldDateTime(newCallLog.Xml, Globals.CallEntry.DynamicFields.EquipmentDown.ExpectedUpDate, Globals.CallEntry.DynamicFields.EquipmentDown.ExpectedUpTime);
                                if (durationDateTime.HasValue && durationDateTime > actionDateTime)
                                    equipmentDownHistory.Duration = Convert.ToInt32(durationDateTime.Value.Subtract(actionDateTime.Value).TotalDays);
                                else
                                    equipmentDownHistory.Duration = 0; // when no date is set up
                            }
                            else if (newCallLog.CallTypeID.Equals((int)Globals.CallEntry.CallType.EquipmentUp))
                            {
                                equipment.Status = Globals.EquipmentMaintenance.Status.Up.ToString();
                                equipmentDownHistory.DownHistoryEndDate = actionDateTime.Value;
                            }

                            _equipmentModel = new EquipmentModel();
                            _equipmentModel.SaveEquipment(equipment, equipmentDownHistory, null);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the Action Date/Time from the Dynamic Fields of a call log
        /// </summary>
        /// <param name="xml">XML with the filelds and values</param>
        /// <returns>Action Date/Time if exists</returns>
        public DateTime? GetCallLogActionDateTime(string xml)
        {
            return GetDynamicFieldDateTime(xml, Globals.CallEntry.ActionDateFieldName, Globals.CallEntry.ActionTimeFieldName);
        }

        public void VerifyDPICalculate(CS_CallLog callLog, List<int> employeeIDList, List<int> equipmentIDList)
        {
            CS_CallType callType = GetCallType(callLog.CallTypeID);
            List<DateTime?> actionDateList = new List<DateTime?>();

            if (null != callType && callType.DpiStatus.HasValue)
            {
                if (callType.ID == (int)Globals.CallEntry.CallType.AddedResource)
                {
                    int jobID = callLog.JobID;
                    IList<CS_Resource> addedResources = _resourceRepository.ListAll(e => e.Active && e.JobID == jobID && ((e.EmployeeID.HasValue && employeeIDList.Contains(e.EmployeeID.Value)) || (e.EquipmentID.HasValue && equipmentIDList.Contains(e.EquipmentID.Value)))).Distinct(new Globals.ResourceAllocation.CS_Resource_Comparer()).ToList();

                    if (addedResources.Count > 0)
                        actionDateList.AddRange(addedResources.Select(e => ((DateTime?)e.StartDateTime)).ToList());
                }
                else
                {
                    if (callType.IsAutomaticProcess)
                    {
                        actionDateList.Add(callLog.CreationDate);
                    }
                    else if (!string.IsNullOrEmpty(callLog.Xml))
                    {
                        actionDateList.Add(GetCallLogActionDateTime(callLog.Xml));
                    }
                    else
                    {
                        actionDateList.Add(callLog.CallDate);
                    }
                }

                _dpiModel = new DPIModel(_unitOfWork);

                for (int i = 0; i < actionDateList.Count; i++)
                {
                    DateTime? actionDate = actionDateList[i];

                    if (actionDate.HasValue)
                    {
                        IList<CS_DPI> dpis = _dpiModel.ListDPIByDateAndResources(callLog.JobID, actionDate.Value);

                        for (int j = 0; j < dpis.Count; j++)
                        {
                            dpis[j].Calculate = true;
                            _dpiModel.UpdateDPI(dpis[j], new List<CS_DPIResource>(), DateTime.Now);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets any Date/Time field from the Dynamic Fields of a call log
        /// </summary>
        /// <param name="xml">XML with the filelds and values</param>
        /// <param name="dateFieldName">Date Field Name</param>
        /// <param name="timeFieldName">Time Field Name</param>
        /// <returns>Date/Time if exists</returns>
        public DateTime? GetDynamicFieldDateTime(string xml, string dateFieldName, string timeFieldName)
        {
            DateTime? actionDateTime = null;
            if (!string.IsNullOrEmpty(xml))
            {
                DynamicFieldsSerialize serializer = new DynamicFieldsSerialize();
                DynamicFieldsAggregator aggregator = serializer.DeserializeObject(xml);
                if (null != aggregator)
                {
                    // Getting Action Date from Dynamic fields
                    DynamicControls actionDate = aggregator.Controls.Find(e => e.Name.Equals(dateFieldName));
                    DynamicControls actionTime = aggregator.Controls.Find(e => e.Name.Equals(timeFieldName));

                    if (null != actionDate && null != actionTime)
                    {
                        if (((DynamicDatePickerXml)actionDate).Text.HasValue &&
                            !string.IsNullOrEmpty(((DynamicTimeXml)actionTime).Text))
                        {
                            actionDateTime = DateTime.Parse(
                                string.Format("{0} {1}",
                                    ((DynamicDatePickerXml)actionDate).Text.Value.ToString("MM/dd/yyyy"),
                                    ((DynamicTimeXml)actionTime).Text),
                                new System.Globalization.CultureInfo("en-US"));
                        }
                    }
                }
            }
            return actionDateTime;
        }

        private DynamicFieldsAggregator GetDynamicFieldAggregator(string xml)
        {
            if (!string.IsNullOrEmpty(xml))
            {
                DynamicFieldsSerialize serializer = new DynamicFieldsSerialize();
                return serializer.DeserializeObject(xml);
            }

            return new DynamicFieldsAggregator();
        }

        private string GetXmlFromAggregator(DynamicFieldsAggregator aggregator)
        {
            DynamicFieldsSerialize serializer = new DynamicFieldsSerialize();
            return serializer.SerializeObject(aggregator);
        }

        public string GetDynamicFieldValueByName<T>(string xml, string name, string value)
            where T : DynamicControls
        {
            DynamicFieldsSerialize serialize = new DynamicFieldsSerialize();
            DynamicFieldsAggregator aggregator = serialize.DeserializeObject(xml);

            T control = (T)aggregator.Controls.Find(a => a.Name.Equals(name));

            string result = control.GetType().GetProperty(value).GetValue(control, null).ToString();

            return result;
        }

        public Dictionary<string, string> GetDynamicFieldControlsProperties(string xml)
        {
            Dictionary<string, string> dicResult = new Dictionary<string, string>();

            DynamicFieldsSerialize serialize = new DynamicFieldsSerialize();
            DynamicFieldsAggregator aggregator = serialize.DeserializeObject(xml);

            foreach (var control in aggregator.Controls)
            {
                dicResult.Add(control.Label.Text, control.GetType().GetProperty("Text").GetValue(control, null).ToString());
            }

            return dicResult;
        }

        public string FormatDynamicFieldsData(Dictionary<string, string> controls)
        {
            StringBuilder formatBuilder = new StringBuilder();
            foreach (var control in controls)
            {
                formatBuilder.Append(control.Key + "<Text><BL>");
                formatBuilder.Append(control.Value + "<BL>");
            }

            return formatBuilder.ToString();
        }

        /// <summary>
        /// Returns a list of all Active Hotels
        /// </summary>
        /// <returns>Hotel list</returns>
        public IList<CS_Hotel> ListAllHotels()
        {
            return _hotelRepository.ListAll(e => e.Active, order => order.Description, true);
        }

        /// <summary>
        /// Returns a list of all Active Subcontractors
        /// </summary>
        /// <returns>Subcontractor list</returns>
        public IList<CS_Subcontractor> ListAllSubcontractors()
        {
            return _subcontractorRepository.ListAll(e => e.Active, order => order.Name, true);
        }

        /// <summary>
        /// List InitialAdivise CallLogs by job identifier
        /// </summary>
        /// <param name="jobId">job identifier</param>
        /// <returns>List of Entities</returns>
        public IList<CS_CallLog> ListInitialAdiviseCallLogsByJob(int jobId)
        {
            CS_CallLogResource res = new CS_CallLogResource();

            return _callLogRepository.ListAll(e => e.Active && e.JobID == jobId && 
                (e.CS_CallType.ID == (int)Globals.CallEntry.CallType.Advise ||
                 e.CS_CallType.ID == (int)Globals.CallEntry.CallType.InitialLog),
                "CS_CallLogResource", "CS_CallLogResource.CS_Employee1", "CS_CallLogResource.CS_Contact").OrderByDescending(order => order.CallDate).ToList();
        }

        public IList<CS_CallLogResource> ListCallLogResourceByCallEntry(int callEntryId)
        {
            return _callLogResourceRepository.ListAll(e => e.Active && e.CallLogID == callEntryId,
                "CS_Employee", "CS_Employee.CS_Division", "CS_Equipment", "CS_Equipment.CS_Division", "CS_Equipment.CS_EquipmentCombo");
        }

        public IList<CS_CallLogResource> ListCallLogResourceByJob(int jobId)
        {
            return _callLogResourceRepository.ListAll(e => e.Active && e.JobID == jobId);
        }

        public void SaveCallLogCallCriteriaEmail(List<EmailVO> lstEmailVO, CS_CallLog callLog)
        {
            if (null != lstEmailVO)
            {
                if (lstEmailVO.Count > 0)
                {
                    foreach (EmailVO emailVo in lstEmailVO)
                    {
                        CS_CallLogCallCriteriaEmail csCallLogCallCriteriaEmail = new CS_CallLogCallCriteriaEmail
                                                                                     {

                                                                                         CallLogID = callLog.ID,
                                                                                         Name = emailVo.Name,
                                                                                         Email = emailVo.Email,
                                                                                         Status = (int)Globals.EmailService.Status.Pending,
                                                                                         StatusDate = DateTime.Now,
                                                                                         CreatedBy = callLog.CreatedBy,
                                                                                         CreationDate = DateTime.Now,
                                                                                         ModifiedBy = callLog.CreatedBy,
                                                                                         ModificationDate = DateTime.Now,
                                                                                         Active = true
                                                                                     };

                        _callLogCallCriteriaEmailRepository.Add(csCallLogCallCriteriaEmail);
                    }
                }
            }
        }

        public bool IsResourceUpdate(CS_PrimaryCallType primaryCallType)
        {
            CS_PrimaryCallType result = _primaryCallTypeRepository.Get(cl => cl.ID == primaryCallType.ID);
            if (result.PrimaryCallTypeCategory.HasValue)
            {
                int categoryValue = Convert.ToInt32(Globals.CallEntry.PrimaryCallTypeCategory.ResourceUpdate);
                if (result.PrimaryCallTypeCategory == categoryValue)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// List filtered information from the CS_View_JobCallLog View
        /// </summary>
        public IList<CS_View_JobCallLog> ListFilteredJobCallLogInfo(int? JobStatusId, int? CallTypeId, string ModifiedBy, int? DivisionId, DateTime StartModificationDate, DateTime EndModificationDate, bool shiftTransferLog, bool generalLog, string personName)
        {
            try
            {
                CS_View_JobCallLogRepository jobCallLogRepository = new CS_View_JobCallLogRepository(_jobCallLogRepository, _jobCallLogRepository.UnitOfWork);

                return jobCallLogRepository.GetJobCallLog(JobStatusId, CallTypeId, DivisionId, ModifiedBy, shiftTransferLog, generalLog, StartModificationDate, EndModificationDate, personName).OrderBy(e => e.ModificationDate).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// List CallTypes filtered by Description (Name)
        /// </summary>
        /// <returns>Filtered List</returns>
        public IList<CS_CallType> ListCallTypeFilteredByDescription(string description)
        {
            return _callTypeRepository.ListAll(e => e.Active && e.Description.Contains(description));
        }

        /// <summary>
        /// Returns a CallLog for the last Initial Advise or Update for the JobId
        /// </summary>
        /// <param name="jobIds">ID of the Job</param>
        /// <returns>CallLog Entity</returns>
        public CS_CallLog GetLastInitialAdviseOfJob(int jobId)
        {
            return _callLogRepository.ListAll(e => e.JobID == jobId && (e.CallTypeID == 1 || e.CallTypeID == 33)).OrderByDescending(e => e.CreationDate).FirstOrDefault();
        }

        /// <summary>
        /// Returns a List of persons that need to receive notification based on call log list
        /// </summary>
        /// <param name="callLogIdList">List containing call log identifiers</param>
        /// <returns>List of persons</returns>
        public IList<CS_CallLogCallCriteriaEmail> ListCallCriteriaEmails(List<int> callLogIdList)
        {
            IList<CS_CallLogCallCriteriaEmail> emailList = _callLogCallCriteriaEmailRepository.ListAll(e => e.Active && callLogIdList.Contains(e.CallLogID)).ToList();

            IList<CS_CallLogCallCriteriaEmail> newEmailList = new List<CS_CallLogCallCriteriaEmail>();
            foreach (CS_CallLogCallCriteriaEmail email in emailList)
            {
                CS_CallLogCallCriteriaEmail existingEmail = null;
                if (newEmailList.Count > 0)
                    existingEmail = newEmailList.FirstOrDefault(e => e.Email == email.Email);
                if (null != existingEmail)
                {
                    if (existingEmail.Status.Equals((int)Globals.CallCriteria.CallCriteriaEmailStatus.Sent) ||
                        existingEmail.Status.Equals((int)Globals.CallCriteria.CallCriteriaEmailStatus.ConfirmationReceived) ||
                        existingEmail.Status.Equals((int)Globals.CallCriteria.CallCriteriaEmailStatus.ReadConfirmationReceived))
                    {
                        if (email.Status.Equals((int)Globals.CallCriteria.CallCriteriaEmailStatus.Pending))
                        {
                            newEmailList.Remove(existingEmail);
                            newEmailList.Add(email);
                        }
                    }
                }
                else
                    newEmailList.Add(email);
            }

            return newEmailList;
        }

        /// <summary>
        /// Attaches an Email Identifier with a call criteria entry
        /// </summary>
        /// <param name="receiptId">Receipt ID</param>
        /// <param name="emailId">Email ID</param>
        public void AttachEmailToCallCriteria(int receiptId, int? emailId)
        {
            CS_CallLogCallCriteriaEmail email = _callLogCallCriteriaEmailRepository.Get(e => e.ID == receiptId);
            if (null != email)
            {
                email.EmailID = emailId;
                email.Status = (short)Globals.CallCriteria.CallCriteriaEmailStatus.Sent;
                email.StatusDate = DateTime.Now;

                _callLogCallCriteriaEmailRepository.Update(email);
            }
        }

        /// <summary>
        /// Updates the status of an email based on Email Identifier
        /// </summary>
        /// <param name="emailId">Email ID</param>
        /// <param name="callCriteriaEmailStatus">New Status</param>
        public void UpdateEmailStatusByEmailId(int emailId, Globals.CallCriteria.CallCriteriaEmailStatus callCriteriaEmailStatus)
        {
            CS_CallLogCallCriteriaEmail email = _callLogCallCriteriaEmailRepository.Get(e => e.EmailID == emailId);
            if (null != email)
            {
                email.Status = (short)callCriteriaEmailStatus;
                email.StatusDate = DateTime.Now;

                _callLogCallCriteriaEmailRepository.Update(email);
            }
        }

        /// <summary>
        ///  Get the call log information
        /// </summary>
        /// <param name="resourceId">resource id</param>
        /// <returns>list of call log</returns>
        public IList<CS_CallLog> GetCallLogByResource(int jobId, int? employeeId, int? equipmentId)
        {
            IList<CS_CallLog> lstCsCallLogs = _callLogRepository.ListAll(
                w => w.Active && (
                    w.CS_CallLogResource.Any(
                        e => (employeeId.HasValue && e.EmployeeID == employeeId) ||
                             (equipmentId.HasValue && e.EquipmentID == equipmentId)
                    ) &&
                    w.JobID == jobId
                )).ToList();

            if (null != lstCsCallLogs)
            {
                return lstCsCallLogs;
            }

            return null;
        }

        public CS_CallType GetCallTypeByDescription(string description)
        {
            CS_CallType callType = new CS_CallType() { IsAutomaticProcess = false };

            if (!string.IsNullOrEmpty(description))
                callType = _callTypeRepository.Get(e => e.Active && e.Description.Contains(description));

            return callType; 
        }

        public void GenerateOffCalCallLog(CS_EmployeeOffCallHistory offCall)
        {
            string note = GenerateOffCallNote(offCall);

            CS_CallLog callLog = new CS_CallLog()
            {
                CallDate = DateTime.Now,
                CallTypeID = (int)Globals.CallEntry.CallType.OffCall,
                PrimaryCallTypeId = (int)Globals.CallEntry.PrimaryCallType.NonJobUpdateNotification,
                HasGeneralLog = false,
                JobID = (int)Globals.GeneralLog.ID,
                Note = note,
                CreatedBy = offCall.CreatedBy,
                CreationDate = DateTime.Now,
                ModificationDate = DateTime.Now,
                ModifiedBy = offCall.ModifiedBy,
                Active = true
            };

            callLog = _callLogRepository.Add(callLog);

            CS_CallLogResource clrItem = new CS_CallLogResource()
            {
                CallLogID = callLog.ID,
                JobID = (int)Globals.GeneralLog.ID,
                CreatedBy = offCall.CreatedBy,
                CreationDate = DateTime.Now,
                ModifiedBy = offCall.ModifiedBy,
                ModificationDate = DateTime.Now,
                EmployeeID = offCall.EmployeeID,
                Type = (int)Globals.CallEntry.CallLogResourceType.Employee,
                Active = true
            };

            _callLogResourceRepository.Add(clrItem);
        }

        public void GenerateEmployeeCoverageCalCallLog(CS_EmployeeCoverage coverage)
        {
            string note = GenerateEmployeeCoverageNote(coverage);
            DateTime processDate = DateTime.Now;
            CS_Resource resource = _resourceRepository.Get(w => w.EmployeeID == coverage.EmployeeID && w.Active);

            CS_CallLog callLog = new CS_CallLog()
            {
                CallDate = processDate,
                CallTypeID = (int)Globals.CallEntry.CallType.Coverage,
                PrimaryCallTypeId = (int)Globals.CallEntry.PrimaryCallType.NonJobUpdateNotification,
                HasGeneralLog = false,
                JobID = (int)Globals.GeneralLog.ID,
                Note = note,
                CreatedBy = coverage.CreatedBy,
                CreationDate = processDate,
                ModificationDate = processDate,
                ModifiedBy = coverage.ModifiedBy,
                Active = true
            };

            callLog = _callLogRepository.Add(callLog);

            if (null != resource)
            {
                CS_CallLog callLogResource = new CS_CallLog()
                {
                    CallDate = processDate,
                    CallTypeID = (int)Globals.CallEntry.CallType.Coverage,
                    PrimaryCallTypeId = (int)Globals.CallEntry.PrimaryCallType.NonJobUpdateNotification,
                    HasGeneralLog = false,
                    JobID = resource.JobID,
                    Note = note,
                    CreatedBy = coverage.CreatedBy,
                    CreationDate = processDate,
                    ModificationDate = processDate,
                    ModifiedBy = coverage.ModifiedBy,
                    Active = true
                };

                callLogResource = _callLogRepository.Add(callLogResource);
            }


            
        }

        public CS_CallLog UpdateCallLogAndReferences(CS_CallLog callLogToUpdate, IList<CS_CallLogResource> callLogResourceList,  IList<CS_CallLogCallCriteriaEmail> callLogCriteriaEmailList)
        {
            CS_CallLog callLogUpdated = null;
            if (callLogToUpdate != null)
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted }))
                {
                    callLogUpdated = _callLogRepository.Update(callLogToUpdate);
                    for (int i = 0; i < callLogResourceList.Count; i++)
                    {
                        _callLogResourceRepository.Update(callLogResourceList[i]);
                    }
                    for (int i = 0; i < callLogCriteriaEmailList.Count; i++)
                    {
                        _callLogCallCriteriaEmailRepository.Update(callLogCriteriaEmailList[i]);
                    }
                    scope.Complete();
                }
            }
            return callLogUpdated;
        }

        public IList<CS_CallLogResource> ListCallLogResourcesByCallLog(int callLogID)
        {
            IList<CS_CallLogResource> callLogResourceList = _callLogResourceRepository.ListAll(e => e.Active && e.CallLogID == callLogID);
            return callLogResourceList;
        }

        public CS_CallLog GetCallLogById(int callLogId)
        {
            CS_CallLog callLog = _callLogRepository.Get(e => e.Active && e.ID == callLogId);
            return callLog;
        }

        #endregion

        #region [ Resource Transfer ]

        public void CreateTransferResourceCallLogs(List<CS_Resource> resourceList, Dictionary<int, Globals.TransferResource.TransferType> transferTypeList, int fromJobId, int toJobId, string username)
        {
            CreateParkedCallLogs(resourceList, fromJobId, username);

            CreateTransferCallLogs(resourceList, transferTypeList, fromJobId, toJobId, username);
        }

        private void CreateParkedCallLogs(List<CS_Resource> resourceList, int fromJobId, string username)
        {
            string note = GenerateParkedNote(resourceList);
            List<int> employees = new List<int>();
            List<int> equipments = new List<int>();

            CS_CallType callType = _callTypeRepository.Get(e => e.ID == (int)Globals.CallEntry.CallType.Parked);

            DynamicFieldsAggregator aggregator = new DynamicFieldsAggregator();

            if (null != callType)
            {
                aggregator = GetDynamicFieldAggregator(callType.Xml);

                DynamicDatePickerXml actionDate = aggregator.Controls.Find(e => e.Name.Equals(Globals.CallEntry.ActionDateFieldName)) as DynamicDatePickerXml;
                DynamicTimeXml actionTime = aggregator.Controls.Find(e => e.Name.Equals(Globals.CallEntry.ActionTimeFieldName)) as DynamicTimeXml;

                if (null != actionDate)
                {
                    actionDate.Text = DateTime.Now;
                }

                if (null != actionTime)
                {
                    actionTime.Text = DateTime.Now.ToString("HH:mm");
                }
            }

            CS_CallLog callLog = new CS_CallLog()
            {
                CallDate = DateTime.Now,
                CallTypeID = (int)Globals.CallEntry.CallType.Parked,
                PrimaryCallTypeId = (int)Globals.CallEntry.PrimaryCallType.ResourceUpdate,
                Xml = GetXmlFromAggregator(aggregator),
                JobID = fromJobId,
                Note = note,
                CreatedBy = username,
                CreationDate = DateTime.Now,
                ModificationDate = DateTime.Now,
                ModifiedBy = username,
                Active = true
            };

            callLog = _callLogRepository.Add(callLog);

            for (int i = 0; i < resourceList.Count; i++)
            {
                CS_CallLogResource clrItem = new CS_CallLogResource()
                {
                    CallLogID = callLog.ID,
                    JobID = fromJobId,
                    CreatedBy = username,
                    CreationDate = DateTime.Now,
                    ModifiedBy = username,
                    ModificationDate = DateTime.Now,
                    Active = true
                };

                if (resourceList[i].EmployeeID.HasValue)
                {
                    clrItem.EmployeeID = resourceList[i].EmployeeID.Value;
                    clrItem.Type = (int)Globals.CallEntry.CallLogResourceType.Employee;
                    employees.Add(resourceList[i].EmployeeID.Value);
                }
                else
                {
                    clrItem.EquipmentID = resourceList[i].EquipmentID.Value;
                    clrItem.Type = (int)Globals.CallEntry.CallLogResourceType.Equipment;
                    equipments.Add(resourceList[i].EquipmentID.Value);
                }

                _callLogResourceRepository.Add(clrItem);

                VerifyDPICalculate(callLog, employees, equipments);
            }
        }

        private void CreateTransferCallLogs(List<CS_Resource> resourceList, Dictionary<int, Globals.TransferResource.TransferType> transferTypeList, int fromJobId, int toJobId, string username)
        {
            string note = GenerateTransferNote(resourceList, transferTypeList, fromJobId, toJobId);
            List<int> employees = new List<int>();
            List<int> equipments = new List<int>();

            CS_CallLog callLogFromJob = new CS_CallLog()
            {
                CallDate = DateTime.Now,
                CallTypeID = (int)Globals.CallEntry.CallType.TransferResource,
                PrimaryCallTypeId = (int)Globals.CallEntry.PrimaryCallType.ResourceUpdate,
                JobID = fromJobId,
                Note = note,
                CreatedBy = username,
                CreationDate = DateTime.Now,
                //CreationID = 
                ModificationDate = DateTime.Now,
                ModifiedBy = username,
                //ModificationID = 
                Active = true
            };

            CS_CallLog callLogToJob = new CS_CallLog()
            {
                CallDate = DateTime.Now,
                CallTypeID = (int)Globals.CallEntry.CallType.TransferResource,
                PrimaryCallTypeId = (int)Globals.CallEntry.PrimaryCallType.ResourceUpdate,
                JobID = toJobId,
                Note = note,
                CreatedBy = username,
                CreationDate = DateTime.Now,
                //CreationID =
                ModificationDate = DateTime.Now,
                ModifiedBy = username,
                //ModificationID =
                Active = true
            };

            _callLogRepository.Add(callLogFromJob);
            _callLogRepository.Add(callLogToJob);

            employees = resourceList.Where(e => e.EmployeeID.HasValue).Select(e => e.EmployeeID.Value).ToList();
            equipments = resourceList.Where(e => e.EquipmentID.HasValue).Select(e => e.EquipmentID.Value).ToList();

            VerifyDPICalculate(callLogFromJob, employees, equipments);
            VerifyDPICalculate(callLogToJob, employees, equipments);
        }

        public void TransferExistingResourceCallLogs(List<CS_Resource> oldResourceList, List<int> oldCallLogIdList, int toJobId, string username)
        {
            List<CS_CallLog> logsToTransfer = ListCallLogByIdList(oldCallLogIdList).ToList();
            List<int> employees = new List<int>();
            List<int> equipments = new List<int>();

            for (int i = 0; i < logsToTransfer.Count; i++)
            {
                //Retrieves a list of ResourceIds associated with that call log
                List<CS_CallLogResource> associatedCallLogResources = ListCallLogResourcesForCallLog(logsToTransfer[i].ID);
                List<int> associatedResourcesIdList = ListResourceIds(associatedCallLogResources);

                #region [ Transfer CallLog ]

                //Make a copy of the Old CallLog to the New Job
                CS_CallLog oldLog = new CS_CallLog()
                {
                    ID = logsToTransfer[i].ID,
                    JobID = logsToTransfer[i].JobID,
                    CallTypeID = logsToTransfer[i].CallTypeID,
                    PrimaryCallTypeId = logsToTransfer[i].PrimaryCallTypeId,
                    CallDate = logsToTransfer[i].CallDate,
                    CalledInByEmployee = logsToTransfer[i].CalledInByEmployee,
                    CalledInByCustomer = logsToTransfer[i].CalledInByCustomer,
                    Xml = logsToTransfer[i].Xml,
                    Note = logsToTransfer[i].Note,
                    ShiftTransferLog = logsToTransfer[i].ShiftTransferLog,
                    HasGeneralLog = logsToTransfer[i].HasGeneralLog,
                    UserCall = logsToTransfer[i].UserCall,
                    CreatedBy = logsToTransfer[i].CreatedBy,
                    CreationDate = logsToTransfer[i].CreationDate,
                    CreationID = logsToTransfer[i].CreationID,
                    ModificationDate = DateTime.Now,
                    ModifiedBy = username,
                    //ModificationID = ,
                    Active = logsToTransfer[i].Active
                };

                CS_CallLog newLog = new CS_CallLog()
                {
                    CallDate = oldLog.CallDate,
                    JobID = toJobId,
                    CalledInByCustomer = oldLog.CalledInByCustomer,
                    CalledInByEmployee = oldLog.CalledInByEmployee,
                    CallTypeID = oldLog.CallTypeID,
                    PrimaryCallTypeId = oldLog.PrimaryCallTypeId,
                    HasGeneralLog = oldLog.HasGeneralLog,
                    ShiftTransferLog = oldLog.ShiftTransferLog,
                    UserCall = oldLog.UserCall,
                    Xml = oldLog.Xml,
                    Note = oldLog.Note,
                    CreatedBy = oldLog.CreatedBy,
                    CreationDate = oldLog.CreationDate,
                    //CreationID = oldLog.CreationID,
                    ModificationDate = DateTime.Now,
                    ModifiedBy = username,
                    //ModificationID = 
                    Active = true
                };

                oldLog.ModifiedBy = username;
                oldLog.ModificationDate = DateTime.Now;
                //oldLog.ModificationID

                //Check if all resources are being transfered, if they are, logically delete the call log on the old job
                if (AreAllResourcesBeingTransfered(associatedResourcesIdList, oldResourceList))
                    oldLog.Active = false;

                oldLog = _callLogRepository.Update(oldLog);
                newLog = _callLogRepository.Add(newLog);

                #endregion

                #region [ Transfer CallLogResource ]

                for (int p = 0; p < associatedCallLogResources.Count; p++)
                {
                    CS_CallLogResource oldCallLogResource = associatedCallLogResources[p];

                    CS_CallLogResource newCallLogResource = new CS_CallLogResource()
                    {
                        CallLogID = newLog.ID,
                        JobID = toJobId,
                        EmployeeID = oldCallLogResource.EmployeeID,
                        EquipmentID = oldCallLogResource.EquipmentID,
                        ContactID = oldCallLogResource.ContactID,
                        Type = oldCallLogResource.Type,
                        CreatedBy = oldCallLogResource.CreatedBy,
                        CreationDate = oldCallLogResource.CreationDate,
                        //CreationID = ,
                        ModifiedBy = username,
                        ModificationDate = DateTime.Now,
                        //ModificationID = ,
                        Active = true
                    };

                    if (oldCallLogResource.EmployeeID.HasValue)
                    {
                        employees.Add(oldCallLogResource.EmployeeID.Value);
                    }
                    else
                    {
                        equipments.Add(oldCallLogResource.EquipmentID.Value);
                    }

                    oldCallLogResource.Active = false;

                    _callLogResourceRepository.Update(oldCallLogResource);
                    _callLogResourceRepository.Add(newCallLogResource);
                }

                #endregion

                VerifyDPICalculate(oldLog, employees, equipments);
                VerifyDPICalculate(newLog, employees, equipments);
            }
        }

        #region [ Auxiliar Methods ]

        private bool AreAllResourcesBeingTransfered(List<int> associatedCallLogResourceIds, List<CS_Resource> resourceList)
        {
            for (int i = 0; i < associatedCallLogResourceIds.Count; i++)
            {
                if (!resourceList.Exists(e => e.ID == associatedCallLogResourceIds[i]))
                    return false;
            }

            return true;
        }

        private List<CS_CallLogResource> ListCallLogResourcesForCallLog(int callLogId)
        {
            return _callLogResourceRepository.ListAll(e => e.CallLogID == callLogId).ToList();
        }

        private List<int> ListResourceIds(List<CS_CallLogResource> callLogResources)
        {
            List<int> ids = new List<int>();

            for (int i = 0; i < callLogResources.Count; i++)
            {
                if (callLogResources[i].EmployeeID.HasValue)
                {
                    int id = callLogResources[i].EmployeeID.Value;
                    ids.Add(_resourceRepository.Get(e => e.EmployeeID == id).ID);
                }
                else if (callLogResources[i].EquipmentID.HasValue)
                {
                    int id = callLogResources[i].EquipmentID.Value;
                    ids.Add(_resourceRepository.Get(e => e.EquipmentID == id).ID);
                }
            }

            return ids;
        }

        private string GenerateTransferNote(List<CS_Resource> resourceList, Dictionary<int, Globals.TransferResource.TransferType> transferTypeList, int fromJobId, int toJobId)
        {
            StringBuilder note = new StringBuilder();

            CS_Job fromJob = _jobRepository.Get(e => e.ID == fromJobId);
            CS_Job toJob = _jobRepository.Get(e => e.ID == toJobId);

            note.Append(string.Format("Resources Transfered:<Text><BL> From Job:<Text>{0}<BL> To Job:<Text>{1}<BL>", fromJob.NumberOrInternalTracking, toJob.NumberOrInternalTracking));
            note.Append(string.Format("Transfer Date:<Text>{0}<BL>", DateTime.Now.ToString("MM/dd/yyyy")));
            note.Append(string.Format("Transfer Time:<Text>{0}<BL>", DateTime.Now.ToString("hh:mm:ss")));
            note.Append("Resources:");

            for (int i = 0; i < resourceList.Count; i++)
            {
                CS_Employee emp = null; CS_Equipment equ = null;
                int id;

                note.Append("<Text>");

                if (resourceList[i].EmployeeID.HasValue)
                {
                    id = resourceList[i].EmployeeID.Value;
                    emp = _employeeRepository.Get(e => e.ID == id);
                }
                else
                {
                    id = resourceList[i].EquipmentID.Value;
                    equ = _equipmentRepository.Get(e => e.ID == id);
                }

                if (null != emp)
                {
                    note.Append(emp.FullName);
                }
                else
                {
                    note.Append(string.Format("{0} - {1}", equ.Name, equ.Description));
                }

                switch (transferTypeList[resourceList[i].ID])
                {
                    case Globals.TransferResource.TransferType.Job:
                        note.Append(string.Format(" ({0})", "From Job Location"));
                        break;
                    case Globals.TransferResource.TransferType.Specific:
                        note.Append(string.Format(" ({0})", "From Specific Location"));
                        break;
                    case Globals.TransferResource.TransferType.Division:
                        note.Append(string.Format(" ({0})", "From Division"));
                        break;
                    default:
                        break;
                }

                note.Append("<BL>");
            }

            return note.ToString();

        }

        private string GenerateParkedNote(List<CS_Resource> resourceList)
        {
            StringBuilder note = new StringBuilder();
            note.Append(string.Format("Parked Date:<Text>{0}<BL>", DateTime.Now.ToString("MM/dd/yyyy")));
            note.Append(string.Format("Parked Time:<Text>{0}<BL>", DateTime.Now.ToString("hh:mm:ss")));
            note.Append("Resources:");

            for (int i = 0; i < resourceList.Count; i++)
            {
                CS_Employee emp = null; CS_Equipment equ = null;
                int id;

                if (resourceList[i].EmployeeID.HasValue)
                {
                    id = resourceList[i].EmployeeID.Value;
                    emp = _employeeRepository.Get(e => e.ID == id);
                }
                else
                {
                    id = resourceList[i].EquipmentID.Value;
                    equ = _equipmentRepository.Get(e => e.ID == id);
                }

                note.Append("<Text>");

                if (null != emp)
                    note.Append(emp.FullName);
                else
                    note.Append(string.Format("{0} - {1}", equ.Name, equ.Description));

                note.Append("<BL>");
            }

            return note.ToString();
        }

        private string GenerateOffCallNote(CS_EmployeeOffCallHistory offCall)
        {
            StringBuilder note = new StringBuilder();
            note.Append(string.Format("The Employee {0} is Off Call.<BL>", offCall.CS_Employee.FullName));
            note.Append(string.Format("Start Date: <Text>{0}<BL>", offCall.OffCallStartDate.ToShortDateString()));
            note.Append(string.Format("End Date: <Text>{0}<BL>", offCall.OffCallEndDate.ToShortDateString()));
            note.Append(string.Format("Return Time: <Text>{0}<BL>", (DateTime.Today + offCall.OffCallReturnTime).ToString("HH:mm")));
            note.Append(string.Format("Proxy: <Text>{0}<BL>", offCall.CS_Employee_Proxy.FullName));

            return note.ToString();
        }

        private string GenerateEmployeeCoverageNote(CS_EmployeeCoverage coverage)
        {
            StringBuilder note = new StringBuilder();
            note.Append(string.Format("The Employee {0} is on Coverage.<BL>", coverage.CS_Employee.FullName));
            note.Append(string.Format("Start Date: <Text>{0} {1}<BL>", coverage.CoverageStartDate.ToShortDateString(), coverage.CoverageStartDate.ToString("HH:mm")));
            note.Append(string.Format("Duration: <Text>{0}<BL>", coverage.Duration.ToString()));
            note.Append(string.Format("Coverage Division: <Text>{0}<BL>", coverage.CS_Division.Name));

            return note.ToString();
        }

        #endregion

        #endregion

        public void CreateCoverageCallLogs(int equipmentId, CS_EquipmentCoverage equipmentCoverage, string username)
        {
            List<CS_CallLog> callLogs = new List<CS_CallLog>();
            DateTime now = DateTime.Now;

            CS_Equipment equipment = _equipmentRepository.Get(e => e.ID == equipmentId);

            if (null != equipment)
            {
                string notes = GenerateCoverageNote(equipment, equipmentCoverage);

                #region [ General Log Coverage ]

                CS_CallLog coverageLogOnGeneral = new CS_CallLog()
                {
                    CallTypeID = (int)Globals.CallEntry.CallType.Coverage,
                    CallDate = now,
                    HasGeneralLog = false,
                    JobID = (int)Globals.GeneralLog.ID,
                    Note = notes,
                    CreationDate = now,
                    CreatedBy = username,
                    //CreationID =
                    ModificationDate = now,
                    ModifiedBy = username,
                    //ModificationID = 
                    Active = true
                };

                callLogs.Add(coverageLogOnGeneral);

                #endregion

                #region [ Job Specific Coverage ]

                CS_Resource resource = _resourceRepository.Get(e => e.EquipmentID == equipment.ID && e.Active);

                if (null != resource)
                {
                    CS_CallLog coverageLogOnJob = new CS_CallLog()
                    {
                        CallTypeID = (int)Globals.CallEntry.CallType.Coverage,
                        CallDate = now,
                        HasGeneralLog = false,
                        JobID = resource.JobID,
                        Note = notes,
                        CreationDate = now,
                        CreatedBy = username,
                        //CreationID =
                        ModificationDate = now,
                        ModifiedBy = username,
                        //ModificationID = 
                        Active = true
                    };

                    callLogs.Add(coverageLogOnJob);
                }

                #endregion

                _callLogRepository.AddList(callLogs);
            }
            else
            {
                throw new Exception("Equipment not found");
            }
        }

        private string GenerateCoverageNote(CS_Equipment equipment, CS_EquipmentCoverage equipmentCoverage)
        {
            StringBuilder notes = new StringBuilder();

            notes.Append(string.Format("Coverage: <Text>The resource {0} - {1} is on Coverage <BL>", equipment.Name, equipment.Description));
            notes.Append(string.Format("Start Date: <Text>{0}<BL>", equipmentCoverage.CoverageStartDate));
            notes.Append(string.Format("Duration: <Text>{0}<BL>", equipmentCoverage.Duration));
            notes.Append(string.Format("Coverage Division: <Text>{0}<BL>", _divisionRepository.Get(e => e.ID == equipmentCoverage.DivisionID).Name));

            return notes.ToString();
        }

        #region [ CallLog / EquipmentType ]

        public IList<CS_CallLog_LocalEquipmentType> ListCallLogEquipmentTypesByCallLogID(int callLogID)
        {
            //InstanceRepositories();
            return _callLogEquipmentTypeRepository.ListAll(e => e.Active && e.CallLogID == callLogID, new string[] { "CS_LocalEquipmentType" });
        }

        public void SaveCallLogEquipmentTypeList(IList<CS_CallLog_LocalEquipmentType> list)
        {
            if(list != null && list.Count > 0)
                _callLogEquipmentTypeRepository.AddList(list);                            
        }

        public void UpdateCallLogEquipmentTypeList(int callLogID, string username, IList<CS_CallLog_LocalEquipmentType> list)
        {
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].ID == 0)
                        _callLogEquipmentTypeRepository.Add(list[i]);
                    else
                        _callLogEquipmentTypeRepository.Update(list[i]);
                }                
            }

            IList<int> existingIDs = list.Where(f => f.ID != 0).Select(e => e.ID).ToList();
            IList<CS_CallLog_LocalEquipmentType> removedList = _callLogEquipmentTypeRepository.ListAll(e => e.CallLogID == callLogID && e.Active && !existingIDs.Contains(e.ID));
            if (null != removedList && removedList.Count > 0)
            {
                for (int i = 0; i < removedList.Count; i++)
                {
                    removedList[i].ModifiedBy = username;
                    removedList[i].ModificationDate = DateTime.Now;
                    //removedList[i].ModificationID;
                    removedList[i].Active = false;
                }

                _callLogEquipmentTypeRepository.UpdateList(removedList);
            }
        }

        #endregion

        #region [ IDisposable Implementation ]

        public void Dispose()
        {
            _callLogRepository = null;
            _callTypeRepository = null;
            _employeeRepository = null;
            _jobRepository = null;
            _callLogResourceRepository = null;
            _callLogCallCriteriaEmailRepository = null;
            _primaryCallTypeRepository = null;
            _hotelRepository = null;
            _subcontractorRepository = null;
            _resourceRepository = null;
            _divisionRepository = null;
            _callLogEquipmentTypeRepository = null;

            if (null != _dpiModel)
            {
                _dpiModel.Dispose();
                _dpiModel = null;
            }

            if (null != _equipmentModel)
            {
                _equipmentModel.Dispose();
                _equipmentModel = null;
            }

            _unitOfWork.Dispose();
            _unitOfWork = null;
        }

        #endregion
    }
}
