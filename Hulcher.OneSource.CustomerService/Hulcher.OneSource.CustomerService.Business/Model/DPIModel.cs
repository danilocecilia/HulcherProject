using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Core.Utils;

using Microsoft.Practices.EnterpriseLibrary.Logging;
using Hulcher.OneSource.CustomerService.DataContext.VO;

namespace Hulcher.OneSource.CustomerService.Business.Model
{
    public class DPIModel : IDisposable
    {
        #region [ Attributes ]

        static Object locked = new Object();

        /// <summary>
        /// Unit of Work used to call the database/unit tests in-memory database
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// Respository for CS_View_DPIInformation entity
        /// </summary>
        private IRepository<CS_View_DPIInformation> _viewDPIInformationRepository;

        /// <summary>
        /// Repository for CS_Job entity
        /// </summary>
        private IRepository<CS_Job> _jobRepository;

        /// <summary>
        /// Respository for CS_DPI entity
        /// </summary>
        private IRepository<CS_DPI> _dpiRepository;

        /// <summary>
        /// Respository for CS_DPIResource entity
        /// </summary>
        private IRepository<CS_DPIResource> _dpiResourceRepository;

        /// <summary>
        /// Repository for CS_DPISpecialPricing entity
        /// </summary>
        private IRepository<CS_DPISpecialPricing> _dpiSpecialPricingRepository;

        /// <summary>
        /// Repository for CS_DPIManualSpecialPricing
        /// </summary>
        private IRepository<CS_DPIManualSpecialPricing> _dpiManualSpecialPricingRepository;

        /// <summary>
        /// Repository for CS_DPIRate entity
        /// </summary>
        private IRepository<CS_DPIRate> _dpiRateRepository;

        /// <summary>
        /// Repository for CS_CallLogResource entity
        /// </summary>
        private IRepository<CS_CallLogResource> _callLogResourceRepository;

        /// <summary>
        /// Repository for CS_Resource entity
        /// </summary>
        private IRepository<CS_Resource> _resourceRepository;

        /// <summary>
        /// Repository for CS_CallLog
        /// </summary>
        private IRepository<CS_CallLog> _callLogRepository;

        /// <summary>
        /// Repository for CS_SpecialPricing entity
        /// </summary>
        private IRepository<CS_SpecialPricing> _specialPricingRepository;

        /// <summary>
        /// Repository for CS_View_DPIReport entity
        /// </summary>
        private IRepository<CS_View_DPIReport> _dpiReportRepository;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Default Constructor
        /// </summary>
        public DPIModel()
        {
            _unitOfWork = new EFUnitOfWork();
            InstanceObjects();
        }

        /// <summary>
        /// Unit Tests constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of Work for in-memory database</param>
        public DPIModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            InstanceObjects();
        }

        #endregion

        #region [ Methods ]

        #region [ Common ]

        public void InstanceObjects()
        {
            _viewDPIInformationRepository = new EFRepository<CS_View_DPIInformation> { UnitOfWork = _unitOfWork };
            _jobRepository = new EFRepository<CS_Job> { UnitOfWork = _unitOfWork };
            _dpiRepository = new EFRepository<CS_DPI> { UnitOfWork = _unitOfWork };
            _dpiResourceRepository = new EFRepository<CS_DPIResource> { UnitOfWork = _unitOfWork };
            _dpiSpecialPricingRepository = new EFRepository<CS_DPISpecialPricing> { UnitOfWork = _unitOfWork };
            _dpiManualSpecialPricingRepository = new EFRepository<CS_DPIManualSpecialPricing> { UnitOfWork = _unitOfWork };
            _dpiRateRepository = new EFRepository<CS_DPIRate> { UnitOfWork = _unitOfWork };
            _callLogResourceRepository = new EFRepository<CS_CallLogResource> { UnitOfWork = _unitOfWork };
            _resourceRepository = new EFRepository<CS_Resource> { UnitOfWork = _unitOfWork };
            _callLogRepository = new EFRepository<CS_CallLog> { UnitOfWork = _unitOfWork };
            _specialPricingRepository = new EFRepository<CS_SpecialPricing> { UnitOfWork = _unitOfWork };
            _dpiReportRepository = new EFRepository<CS_View_DPIReport> { UnitOfWork = _unitOfWork };
        }

        /// <summary>
        /// Get the entity dpi by dpi id
        /// </summary>
        /// <param name="dpiId">dpi id</param>
        /// <returns>dpi entity</returns>
        public CS_DPI GetDPI(int dpiId)
        {
            CS_DPI dpi = _dpiRepository.Get(w => w.Active && w.ID == dpiId && w.CS_Job.CS_JobDivision.Any(a => a.PrimaryDivision && a.JobID == w.JobID),
                "CS_DPISpecialPricing",
                "CS_Job", "CS_Job.CS_JobDivision", "CS_Job.CS_JobDescription", "CS_Job.CS_CustomerInfo",
                "CS_Job.CS_CustomerInfo.CS_Customer",
                "CS_Job.CS_LocationInfo.CS_State",
                "CS_Job.CS_JobInfo.CS_JobAction", "CS_Job.CS_JobInfo.CS_JobCategory", "CS_Job.CS_JobInfo.CS_JobType");

            return dpi;
        }

        /// <summary>
        /// Gets the Current Total of a Job, excluding the current DPI Date
        /// </summary>
        /// <param name="jobID">Job Identifier</param>
        /// <param name="dpiDate">Current DPI Date</param>
        /// <returns>Previous Total</returns>
        public decimal GetPreviousTotal(int jobID, DateTime dpiDate)
        {
            return _dpiRepository.ListAll(e => e.Active
                && e.JobID == jobID
                && e.Date < dpiDate
                && e.ProcessStatus == (short)Globals.DPI.DpiStatus.Approved).Sum(e => e.Total);
        }

        #endregion

        #region [ Listing ]

        /// <summary>
        /// Lists DPIs by Date and Resources
        /// </summary>
        /// <param name="actionDate">DPI Date</param>
        /// <param name="employeeIDList">Employee ID List</param>
        /// <param name="equipmentIDList">Equipment ID List</param>
        public IList<CS_DPI> ListDPIByDateAndResources(int jobID, DateTime actionDate)
        {
            DateTime date = actionDate.Date;
            return _dpiRepository.ListAll
                (
                    e =>
                    e.Date == date
                    && e.JobID == jobID
                    && e.ProcessStatus != (short)Globals.DPI.DpiStatus.Approved
                    && e.Active
                    && !e.Calculate
                );
        }

        public IList<CS_View_DPIInformation> ListFilteredDPIs(Globals.DPI.FilterType filterType, DateTime processDate)
        {
            IList<CS_View_DPIInformation> resultList = new List<CS_View_DPIInformation>();

            switch (filterType)
            {
                case Globals.DPI.FilterType.New:
                    resultList = _viewDPIInformationRepository.ListAll
                        (
                            e => !e.DPIIsContinuing
                                && (e.JobStatusID == ((int)Globals.JobRecord.JobStatus.Active) || e.WasActiveToday.Value)
                                && e.DPIDate.Day == processDate.Day
                                && e.DPIDate.Month == processDate.Month
                                && e.DPIDate.Year == processDate.Year
                        );
                    break;
                case Globals.DPI.FilterType.Continuing:
                    resultList = _viewDPIInformationRepository.ListAll
                        (
                            e => e.JobStartDate < processDate
                                && e.DPIIsContinuing
                                && e.JobStatusID == ((int)Globals.JobRecord.JobStatus.Active)
                                && e.DPIDate.Day == processDate.Day
                                && e.DPIDate.Month == processDate.Month
                                && e.DPIDate.Year == processDate.Year
                        );
                    break;
                case Globals.DPI.FilterType.Reprocess:
                    resultList = _viewDPIInformationRepository.ListAll
                        (
                            e => e.JobStartDate.HasValue
                                && e.JobStatusID == ((int)Globals.JobRecord.JobStatus.Active)
                                && e.DPIDate.Day == processDate.Day
                                && e.DPIDate.Month == processDate.Month
                                && e.DPIDate.Year == processDate.Year
                        );
                    break;
                default:
                    resultList = _viewDPIInformationRepository.ListAll();
                    break;
            }

            return resultList.OrderBy(o => o.JobDivision).ThenBy(o => o.JobNumber).ToList();
        }

        /// <summary>
        /// Returns a list of resources related to a DPI
        /// </summary>
        /// <param name="dpiID">DPI Identifier</param>
        /// <returns>DPI Resource List</returns>
        public IList<CS_DPIResource> ListDPIResources(int dpiID)
        {
            return _dpiResourceRepository.ListAll(e => e.DPIID == dpiID && e.Active, "CS_Equipment", "CS_Employee");
        }

        /// <summary>
        /// Returns the DPI Rate Table
        /// </summary>
        /// <returns>Rate Table</returns>
        public IList<CS_DPIRate> ListRateTable()
        {
            return _dpiRateRepository.ListAll(e => e.Active);
        }

        /// <summary>
        /// Gets the last DPI Special Pricing information, excluding the current DPI
        /// </summary>
        /// <param name="jobId">Job ID</param>
        /// <param name="currentDpiId">Current DPI</param>
        /// <returns>Special Pricing info</returns>
        public CS_DPISpecialPricing GetLastDPISpecialPricing(int jobId, int currentDpiId)
        {
            return _dpiSpecialPricingRepository.ListAll(e => e.Active && e.CS_DPI.JobID == jobId && e.DPIID != currentDpiId, f => f.CS_DPI.Date, true).LastOrDefault();
        }

        /// <summary>
        /// List Dpi for the New jobs Report
        /// </summary>
        /// <param name="DpiDate"></param>
        /// <returns></returns>
        public IList<CS_View_DPIReport> ListNewJobsDpiReport(DateTime DpiDate)
        {
            return _dpiReportRepository.ListAll();
        }

        #endregion

        #region [ Saving ]

        #endregion

        #region [ Updating ]

        /// <summary>
        /// Updates DPI and its resources (only for screen, not for processing)
        /// </summary>
        /// <param name="currentDPI">Current DPI</param>
        /// <param name="dpiResources">Resources of the DPI</param>
        /// <param name="specialPricing">Special Pricing Information</param>
        /// <param name="manualSpecialPricingList">Manual Special Pricing Table</param>
        public void UpdateDPI(CS_DPI currentDPI, IList<CS_DPIResource> dpiResources, CS_DPISpecialPricing specialPricing, IList<CS_DPIManualSpecialPricing> manualSpecialPricingList)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                decimal total = UpdateDPI(currentDPI, dpiResources, DateTime.Now);

                if (null != specialPricing)
                {
                    CS_DPISpecialPricing oldSpecialPricing = _dpiSpecialPricingRepository.Get(e => e.DPIID == currentDPI.ID && e.Active);
                    if (null == oldSpecialPricing)
                        _dpiSpecialPricingRepository.Add(specialPricing);
                    else
                        _dpiSpecialPricingRepository.Update(specialPricing);
                }

                if (manualSpecialPricingList.Count > 0)
                {
                    IList<CS_DPIManualSpecialPricing> oldManualList = _dpiManualSpecialPricingRepository.ListAll(e => e.Active && e.DPIId == currentDPI.ID);
                    List<int> currentManualIDs = manualSpecialPricingList.Select(e => e.ID).ToList();

                    List<CS_DPIManualSpecialPricing> addList = manualSpecialPricingList.Where(e => e.ID == 0).ToList();
                    List<CS_DPIManualSpecialPricing> updateList = manualSpecialPricingList.Where(e => e.ID != 0).ToList();
                    List<CS_DPIManualSpecialPricing> removeList = oldManualList.Where(e => !currentManualIDs.Contains(e.ID)).ToList();

                    for (int i = 0; i < removeList.Count; i++)
                    {
                        removeList[i].ModificationDate = currentDPI.ModificationDate;
                        removeList[i].ModifiedBy = currentDPI.ModifiedBy;
                        //removeList[i].ModificationID = currentDPI.ModificationID;
                        removeList[i].Active = false;
                    }

                    _dpiManualSpecialPricingRepository.AddList(addList);
                    _dpiManualSpecialPricingRepository.UpdateList(updateList);
                    _dpiManualSpecialPricingRepository.UpdateList(removeList);
                }

                if (currentDPI.ProcessStatus == (short)Globals.DPI.DpiStatus.Approved)
                {
                    currentDPI.Total = total;
                    _callLogRepository.Add(SetCallLogDPIProcess(currentDPI));
                }

                scope.Complete();
            }
        }

        /// <summary>
        /// Updates DPI and its resources
        /// </summary>
        /// <param name="currentDPI">Current DPI</param>
        /// <param name="dpiResources">Resources of the DPI</param>
        /// <returns>Total value of resources</returns>
        public decimal UpdateDPI(CS_DPI currentDPI, IList<CS_DPIResource> dpiResources, DateTime calculationDate)
        {
            try
            {
                CS_DPI updateDPI = null;
                //IList<CS_DPIResource> updateDPIResouceList = new List<CS_DPIResource>();

                decimal total = 0;
                bool insuficient = false;

                if (null != currentDPI)
                {
                    for (int i = 0; i < dpiResources.Count; i++)
                    {
                        CS_DPIResource dpiResource = dpiResources[i];

                        //CS_DPIResource addResource = new CS_DPIResource()
                        //{
                        //    ID = dpiResource.ID,
                        //    DPIID = currentDPI.ID,
                        //    EquipmentID = dpiResource.EquipmentID,
                        //    EmployeeID = dpiResource.EmployeeID,
                        //    CalculationStatus = dpiResource.CalculationStatus,
                        //    Hours = dpiResource.Hours,
                        //    ModifiedHours = dpiResource.ModifiedHours,
                        //    IsContinuing = dpiResource.IsContinuing,
                        //    ContinuingHours = dpiResource.ContinuingHours,
                        //    Rate = dpiResource.Rate,
                        //    ModifiedRate = dpiResource.ModifiedRate,
                        //    PermitQuantity = dpiResource.PermitQuantity,
                        //    PermitRate = dpiResource.PermitRate,
                        //    MealQuantity = dpiResource.MealQuantity,
                        //    MealRate = dpiResource.MealRate,
                        //    HasHotel = dpiResource.HasHotel,
                        //    HotelRate = dpiResource.HotelRate,
                        //    ModifiedHotelRate = dpiResource.ModifiedHotelRate,
                        //    Total = dpiResource.Total,
                        //    CreatedBy = dpiResource.CreatedBy,
                        //    //CreationID,
                        //    CreationDate = dpiResource.CreationDate,
                        //    ModifiedBy = currentDPI.ModifiedBy,
                        //    //ModificationID,
                        //    ModificationDate = calculationDate,
                        //    Active = true
                        //};
                        dpiResource.ModificationDate = calculationDate;
                        dpiResource.ModifiedBy = currentDPI.ModifiedBy;
                        //dpiResource.ModificationID = null;

                        if (dpiResource.CalculationStatus == (short)Globals.DPI.CalculationStatus.INSF)
                            insuficient = true;

                        //updateDPIResouceList.Add(addResource);

                        total += dpiResource.Total;
                    }

                    updateDPI = new CS_DPI()
                    {
                        ID = currentDPI.ID,
                        Date = currentDPI.Date,
                        JobID = currentDPI.JobID,
                        ProcessStatus = currentDPI.ProcessStatus,
                        IsContinuing = currentDPI.IsContinuing,
                        ProcessStatusDate = currentDPI.ProcessStatusDate,
                        ApprovedBy = currentDPI.ApprovedBy,
                        CalculationStatus = (short)((insuficient) ? Globals.DPI.CalculationStatus.INSF : Globals.DPI.CalculationStatus.Done),
                        Calculate = currentDPI.Calculate,
                        Total = Math.Round(total),
                        FirstETA = currentDPI.FirstETA,
                        FirstATA = currentDPI.FirstATA,
                        CreatedBy = currentDPI.CreatedBy,
                        //CreationID =,
                        CreationDate = currentDPI.CreationDate,
                        ModifiedBy = currentDPI.CreatedBy,
                        //ModificationID,
                        ModificationDate = calculationDate,
                        Active = true
                    };
                    //currentDPI.CalculationStatus = (short)((insuficient) ? Globals.DPI.CalculationStatus.INSF : Globals.DPI.CalculationStatus.Done);
                    //currentDPI.Total = Math.Round(total);
                    //currentDPI.ModifiedBy = currentDPI.CreatedBy;
                    //currentDPI.ModificationDate = calculationDate;
                }

                //_dpiResourceRepository.UpdateList(updateDPIResouceList);
                _dpiResourceRepository.UpdateList(dpiResources);
                _dpiRepository.Update(updateDPI);
                //_dpiRepository.Update(currentDPI);

                return total;
            }
            catch (Exception ex)
            {
                if (null == ex.InnerException)
                    Logger.Write(string.Format("There was an error Updating the DPI  and DPI Resource data.\n{0}\n{1}", ex.Message, ex.StackTrace));
                else
                    Logger.Write(string.Format("There was an error Updating the DPI  and DPI Resource data.\n{0}\n{1}\nInner Exception:\n{2}\n{3}", ex.Message, ex.StackTrace, ex.InnerException.Message, ex.InnerException.StackTrace));
                throw new Exception("There was an error Updating the DPI  and DPI Resource data.", ex);
            }
        }

        public void UpdateDPIToCalculateByResources(int jobID, IList<int> equipmentIDList, IList<int> employeeIDList)
        {
            IList<CS_DPI> dpiList = _dpiRepository.ListAll(e =>
                                                            e.JobID == jobID
                                                            && e.ProcessStatus != (short)Globals.DPI.DpiStatus.Approved
                                                            && e.Active
                                                            && !e.Calculate
                                                            && e.CS_DPIResource.Any(j => ((j.EquipmentID.HasValue && equipmentIDList.Contains(j.EquipmentID.Value)) ||
                                                                                          (j.EmployeeID.HasValue && employeeIDList.Contains(j.EmployeeID.Value)) &&
                                                                                          j.Active))
                                                        );

            IList<CS_DPI> updateList = new List<CS_DPI>();

            for (int i = 0; i < dpiList.Count; i++)
            {
                CS_DPI updateDPI = new CS_DPI()
                {
                    ID = dpiList[i].ID,
                    Date = dpiList[i].Date,
                    JobID = dpiList[i].JobID,
                    ProcessStatus = dpiList[i].ProcessStatus,
                    IsContinuing = dpiList[i].IsContinuing,
                    ProcessStatusDate = dpiList[i].ProcessStatusDate,
                    ApprovedBy = dpiList[i].ApprovedBy,
                    CalculationStatus = dpiList[i].CalculationStatus,
                    Calculate = true,
                    Total = dpiList[i].Total,
                    FirstETA = dpiList[i].FirstETA,
                    FirstATA = dpiList[i].FirstATA,
                    CreatedBy = dpiList[i].CreatedBy,
                    //CreationID =,
                    CreationDate = dpiList[i].CreationDate,
                    ModifiedBy = dpiList[i].ModifiedBy,
                    //ModificationID,
                    ModificationDate = DateTime.Now,
                    Active = true
                };

                updateList.Add(updateDPI);
            }

            _dpiRepository.UpdateList(updateList);
        }

        /// <summary>
        /// Set all values to the CS_CallLog entity
        /// </summary>
        /// <returns>CS_CallLog</returns>
        public CS_CallLog SetCallLogDPIProcess(CS_DPI currentDPI)
        {
            CS_CallLog callLog = new CS_CallLog();

            callLog.Active = true;
            callLog.PrimaryCallTypeId = (int)Globals.CallEntry.PrimaryCallType.JobUpdateNotification;
            callLog.CallDate = DateTime.Now;
            callLog.CalledInByCustomer = null;
            callLog.CalledInByEmployee = null;
            callLog.CallTypeID = (int)Globals.CallEntry.CallType.DPIApproved;
            callLog.CreatedBy = currentDPI.ModifiedBy;
            callLog.CreationDate = currentDPI.ModificationDate;
            //callLog.CreationID = null;
            callLog.HasGeneralLog = null;
            callLog.JobID = currentDPI.JobID;
            callLog.ModificationDate = currentDPI.ModificationDate;
            //callLog.ModificationID
            callLog.ModifiedBy = currentDPI.ModifiedBy;
            callLog.Note = GenerateDPIProcessNote(currentDPI);
            callLog.ShiftTransferLog = null;
            callLog.UserCall = true;
            callLog.Xml = null;

            return callLog;
        }

        /// <summary>
        /// Generates the string to be used on notes field for the call log
        /// </summary>
        /// <returns></returns>
        public string GenerateDPIProcessNote(CS_DPI currentDPI)
        {
            StringBuilder sb = new StringBuilder();

            decimal previousTotal = GetPreviousTotal(currentDPI.JobID, currentDPI.Date);
            decimal currentTotal = previousTotal + currentDPI.Total;

            sb.Append(string.Format("DPI Date:<Text>{0}<BL>", currentDPI.Date.ToString("MM/dd/yyyy")));
            sb.Append(string.Format("Previous Total:<Text>{0}<BL>", string.Format("{0:C2}", previousTotal)));
            sb.Append(string.Format("New Revenue:<Text>{0}<BL>", string.Format("{0:C2}", currentDPI.Total)));
            sb.Append(string.Format("Current Total:<Text>{0}", string.Format("{0:C2}", currentTotal)));

            return sb.ToString();
        }

        #endregion

        #region [ Report ]

        /// <summary>
        /// Returns the count of approved jobs during the month
        /// </summary>
        /// <param name="currentDate">Reference Date</param>
        /// <returns>Count of approved jobs</returns>
        public int GetDPIReportMTDCount(DateTime currentDate)
        {
            DateTime limitDate = new DateTime(currentDate.Year, currentDate.Month, 1).Date;
            return _dpiRepository.ListAll(e =>
                e.ProcessStatus == (int)Globals.DPI.DpiStatus.Approved &&
                e.Active &&
                e.Date >= limitDate &&
                e.Date <= currentDate.Date).Select(f => f.JobID).Distinct().Count();
        }

        /// <summary>
        /// Returns the count of approved jobs during the year
        /// </summary>
        /// <param name="currentDate">Reference Date</param>
        /// <returns>Count of approved jobs</returns>
        public int GetDPIReportYTDCount(DateTime currentDate)
        {
            DateTime limitDate = new DateTime(currentDate.Year, 1, 1).Date;
            return _dpiRepository.ListAll(e =>
                e.ProcessStatus == (int)Globals.DPI.DpiStatus.Approved &&
                e.Active &&
                e.Date >= limitDate &&
                e.Date <= currentDate.Date).Select(f => f.JobID).Distinct().Count();
        }

        /// <summary>
        /// Gets DPI Report Total Revenue
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public decimal GetDPIReportTotalRevenue(DateTime currentDate)
        {
            return _dpiReportRepository.ListAll(w => w.Date.Day == currentDate.Day
                                                        && w.Date.Month == currentDate.Month
                                                        && w.Date.Year == currentDate.Year
                                                ).Sum(e => e.RevenueTotal);
        }

        /// <summary>
        /// Gets the DPI Total Revenue for Continuing Jobs only
        /// </summary>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        public decimal GetDPIReportContinuingTotalRevenue(DateTime currentDate)
        {
            return _dpiReportRepository.ListAll(e => e.IsContinuing
                                                    && e.Date.Day == currentDate.Day
                                                    && e.Date.Month == currentDate.Month
                                                    && e.Date.Year == currentDate.Year
                                               ).Sum(e => e.RevenueTotal);
        }

        /// <summary>
        /// Lists DPI ReportInformation
        /// </summary>
        /// <param name="nullable"></param>
        /// <param name="nullable_2"></param>
        /// <returns></returns>
        public IList<CS_View_DPIReport> ListDPIReportInformation(Globals.DPIReport.ReportView reportView, DateTime currentDate)
        {
            IList<CS_View_DPIReport> lstViewDPIReport = new List<CS_View_DPIReport>();

            if (Globals.DPIReport.ReportView.ContinuingJobs == reportView)
            {
                return lstViewDPIReport = _dpiReportRepository.ListAll(w => w.IsContinuing
                                                                             && w.Date.Day == currentDate.Day
                                                                             && w.Date.Month == currentDate.Month
                                                                             && w.Date.Year == currentDate.Year
                                                                             && w.JobStatusId == (int)Globals.JobRecord.JobStatus.Active
                                                                        );
            }
            else if (currentDate.Date < DateTime.Today.AddDays(-1))
            {
                 return lstViewDPIReport = _dpiReportRepository.ListAll(w => w.Date.Day == currentDate.Day
                                                                            && w.Date.Month == currentDate.Month
                                                                            && w.Date.Year == currentDate.Year
                                                                            && w.JobStatusId == (int)Globals.JobRecord.JobStatus.Active
                                                                        );
            }
            else
            {
                return lstViewDPIReport = _dpiReportRepository.ListAll(w => !w.IsContinuing
                                                                            && w.Date.Day == currentDate.Day
                                                                            && w.Date.Month == currentDate.Month
                                                                            && w.Date.Year == currentDate.Year
                                                                            && (w.JobStatusId == (int)Globals.JobRecord.JobStatus.Active || w.WasActiveToday.Value)
                                                                        );
            }
        }

        public void UpdateDpiTimeArrival(CS_CallLog callLog, Globals.CallEntry.CallType type)
        {
            CallLogModel model;
            DateTime actionDate = new DateTime();

            using (model = new CallLogModel())
            {
                actionDate = model.GetCallLogActionDateTime(callLog.Xml).Value;
            }

            if (!actionDate.Equals(new DateTime()))
            {
                DateTime date = actionDate.Date;
                CS_DPI datedDPI = _dpiRepository.Get(e => e.Date == date && e.JobID == callLog.JobID);
                CS_DPI newDPI;

                if (null != datedDPI)
                {
                    newDPI = new CS_DPI()
                    {
                        ApprovedBy = datedDPI.ApprovedBy,
                        Calculate = datedDPI.Calculate,
                        CalculationStatus = datedDPI.CalculationStatus,
                        CreatedBy = datedDPI.CreatedBy,
                        CreationDate = datedDPI.CreationDate,
                        CreationID = datedDPI.CreationID,
                        Date = datedDPI.Date,
                        FirstATA = datedDPI.FirstATA,
                        FirstETA = datedDPI.FirstETA,
                        ID = datedDPI.ID,
                        IsContinuing = datedDPI.IsContinuing,
                        JobID = datedDPI.JobID,
                        ModificationDate = datedDPI.ModificationDate,
                        ModificationID = datedDPI.ModificationID,
                        ModifiedBy = datedDPI.ModifiedBy,
                        ProcessStatus = datedDPI.ProcessStatus,
                        ProcessStatusDate = datedDPI.ProcessStatusDate,
                        Total = datedDPI.Total
                    };

                    if (type == Globals.CallEntry.CallType.ETA)
                    {
                        if (!newDPI.FirstETA.HasValue || actionDate < newDPI.FirstETA.Value)
                        {
                            newDPI.FirstETA = actionDate;

                            _dpiRepository.Update(newDPI);
                        }
                    }
                    else
                    {
                        if (!newDPI.FirstATA.HasValue || actionDate < newDPI.FirstATA.Value)
                        {
                            newDPI.FirstATA = actionDate;

                            _dpiRepository.Update(newDPI);
                        }
                    }
                }
            }
        }

        #endregion

        #endregion

        #region [ Service Methods ]

        /// <summary>
        /// Executes the DPI Calculation service
        /// </summary>
        public void CalculateDPI()
        {
            DateTime calculationDate = DateTime.Now;

            try
            {
                //Arrange DPI set in database
                InsertDPI(calculationDate);
                InsertDPIResources(calculationDate);
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to Insert new Jobs/Resources in the DPI Table!\n{0}\n{1}", ex.Message, ex.StackTrace));
                throw ex;
            }

            try
            {
                //List DPI that needs to be calculated
                IList<CS_DPI> dpiList = ListDPIsToBeCalculated(calculationDate);

                //Calculate and update DPIs
                for (int i = 0; i < dpiList.Count; i++)
                {
                    CS_DPI dpi = dpiList[i];

                    using (TransactionScope scope = new TransactionScope())
                    {
                        IList<CS_DPIResource> dpiResourceList = ProcessDPIResources(dpi, dpi.CS_Job, calculationDate);
                        dpi.Calculate = false;
                        UpdateDPI(dpi, dpiResourceList, calculationDate);

                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to Calculate and Update Jobs/Resources for the DPI!\n{0}\n{1}", ex.Message, ex.StackTrace));
                throw ex;
            }
        }

        /// <summary>
        /// Inserts DPI for given jobs
        /// </summary>
        /// <param name="jobList">Jobs that are missing DPI</param>
        /// <param name="calculationDate">DPI calculation date</param>
        /// <returns></returns>
        public IList<CS_DPI> InsertDPI(DateTime calculationDate)
        {
            try
            {
                IList<CS_Job> jobList = ListDPICreationJobs(calculationDate);
                IList<CS_DPI> insertingDPIs = new List<CS_DPI>();

                for (int i = 0; i < jobList.Count; i++)
                {
                    bool isContinuing = false;
                    CS_Job job = jobList[i];

                    CS_Job_JobStatus status = job.CS_JobInfo.CS_Job_JobStatus.FirstOrDefault
                                                    (
                                                        e => e.Active
                                                            && e.JobStatusId == (int)Globals.JobRecord.JobStatus.Active
                                                    );

                    if (null != status && status.JobStartDate.HasValue)
                        isContinuing = status.JobStartDate.Value.Date < calculationDate.Date;

                    insertingDPIs.Add
                        (
                            new CS_DPI()
                            {
                                Date = calculationDate,
                                JobID = job.ID,
                                ProcessStatus = (int)Globals.DPI.DpiStatus.Pending,
                                ProcessStatusDate = calculationDate,
                                CalculationStatus = (int)Globals.DPI.CalculationStatus.INSF,
                                IsContinuing = isContinuing,
                                Calculate = true,
                                Total = 0,
                                CreatedBy = "System",
                                //CreationID =,
                                CreationDate = calculationDate,
                                ModifiedBy = "System",
                                //ModificationID,
                                ModificationDate = calculationDate,
                                Active = true
                            }
                        );

                }

                if (insertingDPIs.Count > 0)
                    return _dpiRepository.AddList(insertingDPIs);
                else
                    return new List<CS_DPI>();

            }
            catch (Exception ex)
            {
                throw new Exception("There was an error saving the DPI data.", ex);
            }
        }

        /// <summary>
        /// Inserts DPI Resources for a given DPI List
        /// </summary>
        /// <param name="dpiList">DPI List</param>
        /// <param name="calculationDate">DPI calculation date</param>
        public void InsertDPIResources(DateTime calculationDate)
        {
            try
            {
                List<CS_DPIResource> resourcesToAdd = new List<CS_DPIResource>();

                //INSERT RESOURCES THAT ARE NOT YET IN TODAY'S DPIs
                IList<CS_DPI> dpiList = _dpiRepository.ListAll(e => e.Date.Day == calculationDate.Day && e.Date.Month == calculationDate.Month && e.Date.Year == calculationDate.Year);

                for (int i = 0; i < dpiList.Count; i++)
                {
                    CS_DPI dpi = dpiList[i];
                    IList<CS_Resource> resourceList = _resourceRepository.ListAll(r => r.JobID == dpi.JobID && r.StartDateTime <= calculationDate, "CS_Equipment", "CS_Equipment.CS_EquipmentCombo", "CS_Equipment.CS_EquipmentCombo.CS_Equipment_PrimaryEquipment", "CS_Employee");

                    for (int j = 0; j < resourceList.Count; j++)
                    {
                        CS_Resource resource = resourceList[j];
                        CS_DPIResource dpiResource = null;

                        if (resource.EquipmentID.HasValue)
                        {
                            dpiResource = _dpiResourceRepository.Get(d => d.DPIID == dpi.ID && d.EquipmentID.HasValue && d.EquipmentID.Value.Equals(resource.EquipmentID.Value));
                        }
                        else if (resource.EmployeeID.HasValue)
                        {
                            dpiResource = _dpiResourceRepository.Get(d => d.DPIID == dpi.ID && d.EmployeeID.HasValue && d.EmployeeID.Value.Equals(resource.EmployeeID.Value));
                        }

                        if (null == dpiResource)
                        {
                            CS_DPIResource dpiResourceToAdd = CreateAutomaticDPIObject(dpi, resource, calculationDate);
                            resourcesToAdd.Add(dpiResourceToAdd);
                        }
                    }
                }

                if (resourcesToAdd.Count > 0)
                    _dpiResourceRepository.AddList(resourcesToAdd);
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error saving the DPI Resource data.", ex);
            }
        }

        private CS_DPIResource CreateAutomaticDPIObject(CS_DPI dpi, CS_Resource resource, DateTime calculationDate)
        {
            CS_DPIResource dpiResource = new CS_DPIResource();
            dpiResource.DPIID = dpi.ID;

            if (resource.EquipmentID.HasValue)
            {
                dpiResource.EquipmentID = resource.EquipmentID;

                if (resource.CS_Equipment.Seasonal && resource.CS_Equipment.ComboID.HasValue && resource.CS_Equipment.ID != resource.CS_Equipment.CS_EquipmentCombo.PrimaryEquipmentID && resource.CS_Equipment.DivisionID != resource.CS_Equipment.CS_EquipmentCombo.CS_Equipment_PrimaryEquipment.DivisionID)
                    dpiResource.DivisionID = resource.CS_Equipment.CS_EquipmentCombo.CS_Equipment_PrimaryEquipment.DivisionID;
                else
                    dpiResource.DivisionID = resource.CS_Equipment.DivisionID;
            }

            if (resource.EmployeeID.HasValue)
            {
                dpiResource.EmployeeID = resource.EmployeeID;
                dpiResource.DivisionID = resource.CS_Employee.DivisionID;
            }

            dpiResource.CalculationStatus = dpi.CalculationStatus;
            dpiResource.Hours = 0;
            dpiResource.IsContinuing = false;
            dpiResource.Rate = 0;
            dpiResource.HasHotel = false;
            dpiResource.Total = 0;
            dpiResource.CreatedBy = "System";
            dpiResource.CreationDate = calculationDate;
            dpiResource.ModifiedBy = "System";
            dpiResource.ModificationDate = calculationDate;
            dpiResource.Active = true;
            return dpiResource;
        }

        /// <summary>
        /// Returns a list of all jobs that needs to be calculated
        /// </summary>
        /// <param name="calculationDate">Calculation Date</param>
        /// <returns>Job list</returns>
        public IList<CS_Job> ListDPICalculationJobs(DateTime calculationDate)
        {
            return _jobRepository.ListAll(
                e => e.CS_JobInfo.CS_Job_JobStatus.Any(
                    f => f.Active
                    && f.JobStatusId == (int)Globals.JobRecord.JobStatus.Active
                    && f.JobStartDate <= calculationDate
                )
                && !e.CS_DPI.Any(
                f => f.Active
                    && f.ProcessStatus == (short)Globals.DPI.DpiStatus.Approved
                    && f.Date.Day == calculationDate.Day
                    && f.Date.Month == calculationDate.Month
                    && f.Date.Year == calculationDate.Year
                )
                && e.Active);
        }

        /// <summary>
        /// Returns a list of all jobs that needs to have dpi created
        /// </summary>
        /// <param name="calculationDate">Calculation Date</param>
        /// <returns>Job list</returns>
        public IList<CS_Job> ListDPICreationJobs(DateTime calculationDate)
        {
            return _jobRepository.ListAll(
                e => (e.CS_JobInfo.CS_Job_JobStatus.Any(
                        f => f.Active
                        && (f.JobStatusId == (int)Globals.JobRecord.JobStatus.Active
                        && f.JobStartDate <= calculationDate)
                     )
                    || e.CS_JobInfo.CS_Job_JobStatus.Any(
                            g => g.JobStatusId == (int)Globals.JobRecord.JobStatus.Active
                            && g.JobStartDate <= calculationDate)
                        && e.CS_JobInfo.CS_Job_JobStatus.Any(
                            h => h.Active
                            && h.JobCloseDate.HasValue
                            && (h.JobCloseDate.Value.Day == calculationDate.Day
                            && h.JobCloseDate.Value.Month == calculationDate.Month
                            && h.JobCloseDate.Value.Year == calculationDate.Year)))
                && !e.CS_DPI.Any(
                    f => f.Active
                    && f.Date.Day == calculationDate.Day
                    && f.Date.Month == calculationDate.Month
                    && f.Date.Year == calculationDate.Year
                )
                && e.Active
                , "CS_JobInfo"
                , "CS_JobInfo.CS_Job_JobStatus");
        }

        /// <summary>
        /// Resturns a list of DPIs that need to be calculated
        /// </summary>
        /// <param name="calculationDate">Calculation Date</param>
        /// <param name="jobIDList"></param>
        /// <returns>DPI list</returns>
        public IList<CS_DPI> ListDPIsToBeCalculated(DateTime calculationDate)
        {
            return _dpiRepository.ListAll(e => e.Active &&
                ((e.Date.Day == calculationDate.Day && e.Date.Month == calculationDate.Month && e.Date.Year == calculationDate.Year) || (e.Calculate))
                , "CS_DPIResource");
        }

        public IList<CS_DPIResource> ProcessDPIResources(CS_DPI dpiEntity, CS_Job jobEntity, DateTime calculationDate)
        {
            List<CS_DPIResource> returnList = _dpiResourceRepository.ListAll(e => e.DPIID == dpiEntity.ID && e.Active).ToList();

            DateTime jobCallDate = jobEntity.CS_JobInfo.InitialCallDate + jobEntity.CS_JobInfo.InitialCallTime;

            for (int i = 0; i < returnList.Count; i++)
            {
                List<CS_CallLogResource> callLogs = new List<CS_CallLogResource>();
                int equipId = 0, emplId = 0;

                if (returnList[i].EquipmentID.HasValue)
                {
                    equipId = returnList[i].EquipmentID.Value;
                    callLogs = _callLogResourceRepository.ListAll(e => e.EquipmentID == equipId && e.JobID == jobEntity.ID && e.ModificationDate >= calculationDate.Date, "CS_CallLog", "CS_CallLog.CS_CallType").ToList();
                }
                else
                {
                    emplId = returnList[i].EmployeeID.Value;
                    callLogs = _callLogResourceRepository.ListAll(e => e.EmployeeID == emplId && e.JobID == jobEntity.ID && e.ModificationDate >= calculationDate.Date, "CS_CallLog", "CS_CallLog.CS_CallType").ToList();
                }

                //RETRIEVES LIST OF CALL LOGS THAT HAVE A 'START STATUS', TRANSFORM AND ORDER THEM BY DATE
                List<DPICallLogVO> startCallLogs = GetDPICallLogTimesFromList(callLogs.Where(e => e.CS_CallLog.CS_CallType.DpiStatus == (int)Globals.DPI.CallTypeDpiStatus.Start).ToList(), calculationDate);

                //RETRIEVES LIST OF CALL LOGS THAT HAVE A 'END STATUS', TRANSFORM AND ORDER THEM BY DATE
                List<DPICallLogVO> endCallLogs = GetDPICallLogTimesFromList(callLogs.Where(e => e.CS_CallLog.CS_CallType.DpiStatus == (int)Globals.DPI.CallTypeDpiStatus.End).ToList(), calculationDate);

                callLogs = null;

                returnList[i] = CalculateHoursForResouce(returnList[i], jobEntity.EmergencyResponse, jobCallDate, startCallLogs, endCallLogs, calculationDate);

                if (dpiEntity.ProcessStatus == (int)Globals.DPI.DpiStatus.Pending)
                {
                    returnList[i].Total = Convert.ToDecimal(returnList[i].Rate * returnList[i].Hours);
                }
            }

            return returnList;
        }

        public CS_DPIResource CalculateHoursForResouce(CS_DPIResource currentDPIResource, bool IsEmergencyResponse, DateTime jobStartDate, List<DPICallLogVO> startCallLogs, List<DPICallLogVO> endCallLogs, DateTime calculationDate)
        {
            //VARIABLES NEEDED FOR CALCULATION PURPOSES
            DateTime lastStartTime = new DateTime(), currentStartTime = new DateTime(), currentEndTime = new DateTime(), endOfDay = calculationDate.Date + TimeSpan.Parse("23:59:59");
            double lastTotalHours = 0, totalHours = 0, rate = 0;
            bool lookingForStart = true, insf = false;

            //IF JOB HAS EMERGENCY RESPONSE FLAGGED FOR THE CURRENT DAY, USE IT AS FIRST START DATE
            if (IsEmergencyResponse && (jobStartDate.Date == calculationDate.Date))
            {
                currentStartTime = jobStartDate;
                lookingForStart = false;
            }

            do
            {
                if (lookingForStart)
                {
                    if (startCallLogs.Count > 0)
                    {
                        DPICallLogVO startItem = startCallLogs.FirstOrDefault(e => e.ActionTime > currentEndTime);

                        if (null != startItem) //START DATE FOUND, USE IT FOR CALCULATION AND SEARCH FOR NEXT END DATE
                        {
                            currentStartTime = startItem.ActionTime;
                            lookingForStart = false;
                        }
                        else //NO START DATE THAT MATTERS TO THE LOGIC, END LOOP
                        {
                            break;
                        }
                    }
                    else //NO START DATE, END LOOP
                    {
                        break;
                    }
                }

                if (!lookingForStart)
                {
                    //SEEK HIGHEST END DATE BEFORE THE NEXT START CALL LOG
                    DPICallLogVO endItem = endCallLogs.FirstOrDefault(e => e.ActionTime >= currentStartTime);

                    if (null != endItem) //END DATE FOUND, USE IT FOR CALCULATION AND LOOK FOR NEXT START DATE
                    {
                        DPICallLogVO nextSupposedStartItem = startCallLogs.FirstOrDefault(e => e.ActionTime > endItem.ActionTime);

                        //SEE IF THERE'S ANOTHER END DATE, GREATER THAN THE FIRST
                        if (null != nextSupposedStartItem)
                        {
                            endItem = endCallLogs.LastOrDefault(e => e.ActionTime >= endItem.ActionTime && e.ActionTime < nextSupposedStartItem.ActionTime);
                        }
                        else //IF THERE IS ANY OTHER END DATE AND THERE ARE NO MORE START DATE
                        {
                            endItem = endCallLogs.LastOrDefault(e => e.ActionTime >= endItem.ActionTime);
                        }

                        currentEndTime = endItem.ActionTime;
                        lookingForStart = true;
                    }
                    else //NO END DATE CALL LOG FOUND, CALCULATE USING CURRENT TIME AND END LOOP
                    {
                        if (calculationDate.Date == DateTime.Now.Date && currentStartTime <= DateTime.Now)
                        {
                            currentEndTime = DateTime.Now;
                        }
                        else
                        {
                            currentEndTime = endOfDay;
                        }

                        insf = true;
                    }

                    //SAVE START DATE FOR CALCULATING CONTINUING HOURS
                    lastStartTime = currentStartTime;
                    lastTotalHours = totalHours;
                }

                totalHours += currentEndTime.Subtract(currentStartTime).TotalHours;

            } while (lookingForStart);

            currentDPIResource.Hours = Math.Round(Convert.ToDecimal(totalHours), 2);

            if (!lastStartTime.Equals(new DateTime()))
                currentDPIResource.ContinuingHours = Math.Round(Convert.ToDecimal(lastTotalHours + (endOfDay.Subtract(lastStartTime).TotalHours)), 2);

            if (insf)
                currentDPIResource.CalculationStatus = (int)Globals.DPI.CalculationStatus.INSF;
            else
                currentDPIResource.CalculationStatus = (int)Globals.DPI.CalculationStatus.Done;

            //SIMULATE RATE
            Random rnd = new Random();
            rate = rnd.Next(1, 3) * 100;

            currentDPIResource.Rate = Convert.ToDecimal(rate);

            return currentDPIResource;
        }

        private List<DPICallLogVO> GetDPICallLogTimesFromList(List<CS_CallLogResource> originalCallLogs, DateTime calculationDate)
        {
            List<DPICallLogVO> returnList = new List<DPICallLogVO>();

            CallLogModel model = new CallLogModel();

            for (int i = 0; i < originalCallLogs.Count; i++)
            {
                DPICallLogVO newVO = new DPICallLogVO() { ID = originalCallLogs[i].CallLogID };

                if (originalCallLogs[i].ActionDate.HasValue)
                    newVO.ActionTime = originalCallLogs[i].ActionDate.Value;
                else
                {
                    // Only for old records
                    if (originalCallLogs[i].CS_CallLog.CS_CallType.ID == (int)Globals.CallEntry.CallType.AddedResource)
                    {
                        int jobID = originalCallLogs[i].JobID;
                        int? employeeID = originalCallLogs[i].EmployeeID;
                        int? equipmentID = originalCallLogs[i].EquipmentID;
                        CS_Resource addedResource = _resourceRepository.Get(e => e.Active && e.JobID == jobID &&
                            ((employeeID.HasValue && e.EmployeeID == employeeID.Value) ||
                             (equipmentID.HasValue && e.EquipmentID == equipmentID.Value)));

                        if (null != addedResource)
                            newVO.ActionTime = addedResource.StartDateTime;
                        else
                            newVO.ActionTime = new DateTime();
                    }
                    else if (originalCallLogs[i].CS_CallLog.CS_CallType.IsAutomaticProcess)
                    {
                        newVO.ActionTime = originalCallLogs[i].CS_CallLog.CreationDate;
                    }
                    else
                    {
                        newVO.ActionTime = model.GetCallLogActionDateTime(originalCallLogs[i].CS_CallLog.Xml).Value;
                    }
                }

                if (newVO.ActionTime.Date == calculationDate.Date)
                    returnList.Add(newVO);
            }

            returnList.Sort();

            return returnList;
        }

        public CS_SpecialPricing GetJobSpecialPricing(int jobId)
        {
            return _specialPricingRepository.Get(e => e.Active && e.JobId == jobId);
        }

        public CS_DPISpecialPricing GetDPISpecialPricing(int dpiId)
        {
            return _dpiSpecialPricingRepository.Get(e => e.Active && e.DPIID == dpiId);
        }

        public List<CS_DPIManualSpecialPricing> ListDPIManualSpecialPricing(int dpiId)
        {
            return _dpiManualSpecialPricingRepository.ListAll(e => e.Active && e.DPIId == dpiId).ToList();
        }

        #endregion

        #region [ IDisposable Implementation ]

        public void Dispose()
        {
            _viewDPIInformationRepository = null;
            _jobRepository = null;
            _dpiRepository = null;
            _dpiRateRepository = null;
            _dpiResourceRepository = null;
            _dpiSpecialPricingRepository = null;
            _dpiManualSpecialPricingRepository = null;
            _specialPricingRepository = null;

            _unitOfWork.Dispose();
            _unitOfWork = null;
        }

        #endregion
    }
}
