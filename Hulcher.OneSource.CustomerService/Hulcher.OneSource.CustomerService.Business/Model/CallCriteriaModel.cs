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
    public class CallCriteriaModel : IDisposable
    {
        #region [ Attributes ]

        /// <summary>
        /// Repository Class for CS_CallCriteria table
        /// </summary>
        private IRepository<CS_CallCriteria> _callCriteriaRepository;

        /// <summary>
        /// Repository Class for CS_CallCriteriaValue table
        /// </summary>
        private IRepository<CS_CallCriteriaValue> _callCriteriaValueRepository;

        /// <summary>
        /// Unit of Work used to call the database/unit tests in-memory database
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// Repository class for CS_Job
        /// </summary>
        private IRepository<CS_Job> _jobRepository;

        /// <summary>
        /// Repository class for CS_Employee
        /// </summary>
        private IRepository<CS_Employee> _employeeRepository;

        /// <summary>
        /// Repository class for CS_Contact
        /// </summary>
        private IRepository<CS_Contact> _contactRepository;

        /// <summary>
        /// Repository class for CS_CallLog
        /// </summary>
        private IRepository<CS_CallLog> _callLogRepository;

        /// <summary>
        /// Repository class for CS_EmployeeOffCallHistrory
        /// </summary>
        private IRepository<CS_EmployeeOffCallHistory> _employeeOffCallRepository;

        /// <summary>
        /// Instance of CallLog Model
        /// </summary>
        private CallLogModel _callLogModel;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CallCriteriaModel()
        {
            _unitOfWork = new EFUnitOfWork();

            _callCriteriaRepository = new EFRepository<CS_CallCriteria>() { UnitOfWork = _unitOfWork };
            _callCriteriaValueRepository = new EFRepository<CS_CallCriteriaValue>() { UnitOfWork = _unitOfWork };
            _jobRepository = new EFRepository<CS_Job>();
            _jobRepository.UnitOfWork = _unitOfWork;
            _employeeRepository = new EFRepository<CS_Employee>();
            _employeeRepository.UnitOfWork = _unitOfWork;
            _contactRepository = new EFRepository<CS_Contact>();
            _contactRepository.UnitOfWork = _unitOfWork;
            _callLogRepository = new EFRepository<CS_CallLog>();
            _callLogRepository.UnitOfWork = _unitOfWork;
            _employeeOffCallRepository = new EFRepository<CS_EmployeeOffCallHistory>();
            _employeeOffCallRepository.UnitOfWork = _unitOfWork;
            _callLogModel = new CallLogModel(_unitOfWork);
        }

        /// <summary>
        /// Unit Tests constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of Work for in-memory database</param>
        public CallCriteriaModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            _callCriteriaRepository = new EFRepository<CS_CallCriteria>() { UnitOfWork = _unitOfWork };
            _callCriteriaValueRepository = new EFRepository<CS_CallCriteriaValue>() { UnitOfWork = _unitOfWork };
            _jobRepository = new EFRepository<CS_Job>();
            _jobRepository.UnitOfWork = _unitOfWork;
            _employeeRepository = new EFRepository<CS_Employee>();
            _employeeRepository.UnitOfWork = _unitOfWork;
            _contactRepository = new EFRepository<CS_Contact>();
            _contactRepository.UnitOfWork = _unitOfWork;
            _callLogRepository = new EFRepository<CS_CallLog>();
            _callLogRepository.UnitOfWork = _unitOfWork;
         
            _employeeOffCallRepository = new EFRepository<CS_EmployeeOffCallHistory>();
            _employeeOffCallRepository.UnitOfWork = _unitOfWork;

            _callLogModel = new CallLogModel(_unitOfWork);
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Saves CallCriteria Email for a callLog
        /// </summary>
        /// <param name="callLogEntry"></param>
        public void SendCallLogCriteriaEmails(CS_CallLog callLogEntry)
        {
            if (null != callLogEntry)
            {
                List<EmailVO> lstEmailVO = ListReceiptsByCallLog(callLogEntry.CallTypeID.ToString(), callLogEntry.JobID, callLogEntry).ToList();
                if (null != lstEmailVO && lstEmailVO.Count > 0)
                    _callLogModel.SaveCallLogCallCriteriaEmail(lstEmailVO, callLogEntry);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstEmployee"></param>
        /// <param name="job"></param>
        /// <returns></returns>
        public IList<CS_Employee> FilterEmployeeByCriteriaValue(IList<CS_Employee> lstEmployee, CS_Job job)
        {
            List<int> employeeIdList = new List<int>();

            foreach (CS_Employee employee in lstEmployee)
                employeeIdList.Add(employee.ID);

            string JobStatusID = job.CS_JobInfo.LastJobStatusID.ToString();
            string PriceTypeID = job.CS_JobInfo.PriceTypeID.ToString();
            string JobCategoryID = job.CS_JobInfo.JobCategoryID.ToString();
            string JobTypeID = job.CS_JobInfo.JobTypeID.ToString();
            string JobActionID = job.CS_JobInfo.JobActionID.ToString();
            string FrequencyID = (job.CS_JobInfo.FrequencyID.HasValue) ? job.CS_JobInfo.FrequencyID.ToString() : "";

            string CountryID = null;
            string StateID = null;
            string CityID = null;
            if (null != job.CS_LocationInfo)
            {
                CountryID = job.CS_LocationInfo.CountryID.ToString();
                StateID = job.CS_LocationInfo.StateID.ToString();
                CityID = job.CS_LocationInfo.CityID.ToString();
            }
            int CarCount = 0;
            if (null != job.CS_JobDescription)
                CarCount = (((job.CS_JobDescription.NumberEngines.HasValue) ? job.CS_JobDescription.NumberEngines : 0) + ((job.CS_JobDescription.NumberLoads.HasValue) ? job.CS_JobDescription.NumberLoads : 0) + ((job.CS_JobDescription.NumberEmpties.HasValue) ? job.CS_JobDescription.NumberEmpties : 0)).Value;


            IList<CS_CallCriteriaValue> lstCriterias = _callCriteriaValueRepository.ListAll(
                z =>
                (
                    z.CallCriteriaTypeID != (int)Globals.CallCriteria.CallCriteriaType.Division
                    && z.CallCriteriaTypeID != (int)Globals.CallCriteria.CallCriteriaType.Customer
                    && z.CallCriteriaTypeID != (int)Globals.CallCriteria.CallCriteriaType.CallType
                )
                && z.Active
                &&
                (
                    z.CS_CallCriteria.ID == z.CallCriteriaID
                    && z.CS_CallCriteria.Active
                    && z.CS_CallCriteria.EmployeeID.HasValue
                    && employeeIdList.Contains(z.CS_CallCriteria.EmployeeID.Value)
                )
            , new string[] { "CS_CallCriteria" });

            if (lstCriterias.Count == 0)
                return lstEmployee;
            else
                return lstEmployee.Where
                (
                e =>
                    (
                        lstCriterias.Any
                        (
                        z =>
                            z.CS_CallCriteria.EmployeeID == e.ID
                            &&
                            (
                                z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.JobStatus
                                && z.Value == JobStatusID
                                && z.Active
                            )
                            ||
                            (
                                z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.PriceType
                                && z.Value == PriceTypeID
                                && z.Active
                            )
                            ||
                            (
                                z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.JobCategory
                                && z.Value == JobCategoryID
                                && z.Active
                            )
                            ||
                            (
                                z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.JobType
                                && z.Value == JobTypeID
                                && z.Active
                            )
                            ||
                            (
                                z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.JobAction
                                && z.Value == JobActionID
                                && z.Active
                            )
                            ||
                            (
                                z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.Interimbilling
                                && z.Value == FrequencyID
                                && z.Active
                            )
                            ||
                            (
                                z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.Country
                                && z.Value == CountryID
                                && z.Active
                            )
                            ||
                            (
                                z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.State
                                && z.Value == StateID
                                && z.Active
                            )
                            ||
                            (
                                z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.City
                                && z.Value == CityID
                                && z.Active
                            )
                            ||
                            (
                                z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.CarCount
                                &&
                                (
                                    (
                                        (z.Value.Substring(0, 1) == ">") ?
                                            int.Parse(z.Value.Substring(1)) > CarCount
                                        :
                                        (
                                            (z.Value.Substring(0, 1) == "<") ?
                                                int.Parse(z.Value.Substring(1)) < CarCount
                                            :
                                                int.Parse(z.Value) == CarCount
                                        )
                                    )
                                )
                                && z.Active
                            )
                            ||
                            (
                                z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.Commodities
                                && ((z.Value == "1") && (!string.IsNullOrEmpty(job.CS_JobDescription.Lading)))
                                && z.Active
                            )
                            ||
                            (
                                z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.Chemicals
                                &&
                                (
                                    ((z.Value == "1") && (!string.IsNullOrEmpty(job.CS_JobDescription.UnNumber)))
                                    || ((z.Value == "2") && (!string.IsNullOrEmpty(job.CS_JobDescription.STCCInfo)))
                                    || ((z.Value == "3") && (!string.IsNullOrEmpty(job.CS_JobDescription.Hazmat)))
                                )
                                && z.Active
                            )
                        )
                    )
                ).ToList();
        }

        public IList<CS_CallCriteria> FindCallCriteriaByEmployee(int employeeID)
        {
            return _callCriteriaRepository.ListAll(e => e.Active && e.EmployeeID.Value == employeeID && !e.ContactID.HasValue, "CS_CallCriteriaValue").ToList();
        }

        public IList<CS_CallCriteria> FindCallCriteriaByContact(int contactID)
        {
            return _callCriteriaRepository.ListAll(e => e.Active && e.ContactID.Value == contactID && !e.EmployeeID.HasValue, "CS_CallCriteriaValue").ToList();
        }

        public IList<CallCriteriaItemVO> FindCallCriteriaValueByID(int callCriteriaID)
        {
            IList<CallCriteriaItemVO> returnList = new List<CallCriteriaItemVO>();

            try
            {
                CS_CallCriteriaValueRepository callCriteriaValueRepository = new CS_CallCriteriaValueRepository(_callCriteriaValueRepository, _callCriteriaValueRepository.UnitOfWork);
                IList<CS_SP_GetCallCriteriaValues_Result> result = callCriteriaValueRepository.ListCallCriteriaValueByID(callCriteriaID);

                for (int i = 0; i < result.Count; i++)
                {
                    CallCriteriaItemVO item = new CallCriteriaItemVO();

                    item.Criteria = result[i].Description;
                    item.Data = result[i].Value;

                    returnList.Add(item);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returnList;
        }

        public IList<CallCriteriaItemVO> FindCallCriteriaCallLogValueByID(int callCriteriaID)
        {
            IList<CallCriteriaItemVO> returnList = new List<CallCriteriaItemVO>();

            try
            {
                CS_CallCriteriaValueRepository callCriteriaValueRepository = new CS_CallCriteriaValueRepository(_callCriteriaValueRepository, _callCriteriaValueRepository.UnitOfWork);
                IList<CS_SP_GetCallCriteriaValues_Result> result = callCriteriaValueRepository.ListCallCriteriaCallLogValueByID(callCriteriaID);

                for (int i = 0; i < result.Count; i++)
                {
                    CallCriteriaItemVO item = new CallCriteriaItemVO();

                    item.Criteria = result[i].Description;
                    item.Data = result[i].Value;

                    returnList.Add(item);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returnList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstContacts"></param>
        /// <param name="job"></param>
        /// <returns></returns>
        public IList<CS_Contact> FilterContactsByCriteriaValue(IList<CS_Contact> lstContacts, CS_Job job)
        {
            List<int> contactsIdList = new List<int>();

            foreach (CS_Contact contact in lstContacts)
                contactsIdList.Add(contact.ID);

            string JobStatusID = job.CS_JobInfo.LastJobStatusID.ToString();
            string PriceTypeID = job.CS_JobInfo.PriceTypeID.ToString();
            string JobCategoryID = job.CS_JobInfo.JobCategoryID.ToString();
            string JobTypeID = job.CS_JobInfo.JobTypeID.ToString();
            string JobActionID = job.CS_JobInfo.JobActionID.ToString();
            string FrequencyID = (job.CS_JobInfo.FrequencyID.HasValue) ? job.CS_JobInfo.FrequencyID.ToString() : "";

            string CountryID = null;
            string StateID = null;
            string CityID = null;
            if (null != job.CS_LocationInfo)
            {
                CountryID = job.CS_LocationInfo.CountryID.ToString();
                StateID = job.CS_LocationInfo.StateID.ToString();
                CityID = job.CS_LocationInfo.CityID.ToString();
            }
            int CarCount = 0;
            if (null != job.CS_JobDescription)
                CarCount = (((job.CS_JobDescription.NumberEngines.HasValue) ? job.CS_JobDescription.NumberEngines : 0) + ((job.CS_JobDescription.NumberLoads.HasValue) ? job.CS_JobDescription.NumberLoads : 0) + ((job.CS_JobDescription.NumberEmpties.HasValue) ? job.CS_JobDescription.NumberEmpties : 0)).Value;

            IList<CS_CallCriteriaValue> lstCriterias = _callCriteriaValueRepository.ListAll(
                z =>
                (
                    z.CallCriteriaTypeID != (int)Globals.CallCriteria.CallCriteriaType.Division
                    && z.CallCriteriaTypeID != (int)Globals.CallCriteria.CallCriteriaType.Customer
                    && z.CallCriteriaTypeID != (int)Globals.CallCriteria.CallCriteriaType.CallType
                )
                && z.Active
                &&
                (
                    z.CS_CallCriteria.ID == z.CallCriteriaID
                    && z.CS_CallCriteria.Active
                    && z.CS_CallCriteria.ContactID.HasValue
                    && contactsIdList.Contains(z.CS_CallCriteria.ContactID.Value)
                )
            , new string[] { "CS_CallCriteria" });


            if (lstCriterias.Count == 0)
                return lstContacts;
            else
                return lstContacts.Where
                (
                e =>
                    (
                        lstCriterias.Any
                                (
                                z =>
                                    z.CS_CallCriteria.ContactID == e.ID
                                    &&
                                    (
                                        z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.JobStatus
                                        && z.Value == JobStatusID
                                        && z.Active
                                    )
                                    ||
                                    (
                                        z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.PriceType
                                        && z.Value == PriceTypeID
                                        && z.Active
                                    )
                                    ||
                                    (
                                        z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.JobCategory
                                        && z.Value == JobCategoryID
                                        && z.Active
                                    )
                                    ||
                                    (
                                        z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.JobType
                                        && z.Value == JobTypeID
                                        && z.Active
                                    )
                                    ||
                                    (
                                        z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.JobAction
                                        && z.Value == JobActionID
                                        && z.Active
                                    )
                                    ||
                                    (
                                        z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.Interimbilling
                                        && z.Value == FrequencyID
                                        && z.Active
                                    )
                                    ||
                                    (
                                        z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.Country
                                        && z.Value == CountryID
                                        && z.Active
                                    )
                                    ||
                                    (
                                        z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.State
                                        && z.Value == StateID
                                        && z.Active
                                    )
                                    ||
                                    (
                                        z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.City
                                        && z.Value == CityID
                                        && z.Active
                                    )
                                    ||
                                    (
                                        z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.CarCount
                                        &&
                                        (
                                            (
                                                (z.Value.Substring(0, 1) == ">") ?
                                                    int.Parse(z.Value.Substring(1)) > CarCount
                                                :
                                                (
                                                    (z.Value.Substring(0, 1) == "<") ?
                                                        int.Parse(z.Value.Substring(1)) < CarCount
                                                    :
                                                        int.Parse(z.Value) == CarCount
                                                )
                                            )
                                        )
                                        && z.Active
                                    )
                                    ||
                                    (
                                        z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.Commodities
                                        && ((z.Value == "1") && (!string.IsNullOrEmpty(job.CS_JobDescription.Lading)))
                                        && z.Active
                                    )
                                    ||
                                    (
                                        z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.Chemicals
                                        &&
                                        (
                                            ((z.Value == "1") && (!string.IsNullOrEmpty(job.CS_JobDescription.UnNumber)))
                                            || ((z.Value == "2") && (!string.IsNullOrEmpty(job.CS_JobDescription.STCCInfo)))
                                            || ((z.Value == "3") && (!string.IsNullOrEmpty(job.CS_JobDescription.Hazmat)))
                                        )
                                        && z.Active
                                    )
                                )
                    )
                ).ToList();
        }

        /// <summary>
        /// Returns a list of employees that matches division and customer criterias
        /// </summary>
        /// <param name="job">Criteria Job</param>
        /// <returns>List of CS_Employee entities</returns>
        public IList<CS_Employee> ListEmployeeCriteriaByDivisionAndCustomer(CS_Job job)
        {
            if (job.ID.Equals(Globals.GeneralLog.ID))
            {
                return _employeeRepository.ListAll(
                    e => e.Active &&
                         e.CS_CallCriteria.Any(
                            f => f.Active &&
                                 f.EmployeeID.HasValue &&
                                 f.EmployeeID == e.ID &&
                                 f.CS_CallCriteriaValue.Any(
                                    z => z.Active &&
                                         z.CallCriteriaID == f.ID &&
                                         z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.GeneralLog
                                    )
                            )
                    , new string[] { "CS_CallCriteria.CS_CallCriteriaValue" });
            }
            else
            {
                List<string> divisionIdList = new List<string>();
                foreach (CS_JobDivision jobDivision in job.CS_JobDivision)
                    divisionIdList.Add(jobDivision.DivisionID.ToString());
                string customerId = job.CS_CustomerInfo.CustomerId.ToString();

                return _employeeRepository.ListAll
                    (
                    e =>
                        (
                            e.CS_CallCriteria.Any
                            (
                            f =>
                                f.Active
                                && f.EmployeeID.HasValue
                                && f.EmployeeID == e.ID
                                &&
                                (
                                    (

                                        f.CS_CallCriteriaValue.Any
                                        (
                                        z =>
                                            z.Active
                                            && z.CallCriteriaID == f.ID
                                            && z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.Division
                                            && divisionIdList.Contains(z.Value)
                                        )
                                        &&
                                        f.CS_CallCriteriaValue.Any
                                        (
                                        z =>
                                            z.Active
                                            && z.CallCriteriaID == f.ID
                                            && z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.Customer
                                            && z.Value == customerId
                                        )
                                    )
                                    ||
                                    (
                                        f.CS_CallCriteriaValue.Any
                                        (
                                        z =>
                                            z.Active
                                            && z.CallCriteriaID == f.ID
                                            && z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.Division
                                            && divisionIdList.Contains(z.Value)
                                        )
                                        &&
                                        !f.CS_CallCriteriaValue.Any
                                        (
                                        z =>
                                            z.Active
                                            && z.CallCriteriaID == f.ID
                                            && z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.Customer
                                        )
                                    )
                                    ||
                                    (
                                        !f.CS_CallCriteriaValue.Any
                                        (
                                        z =>
                                            z.Active
                                            && z.CallCriteriaID == f.ID
                                            && z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.Division
                                        )
                                        &&
                                        f.CS_CallCriteriaValue.Any
                                        (
                                        z =>
                                            z.Active
                                            && z.CallCriteriaID == f.ID
                                            && z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.Customer
                                            && z.Value == customerId
                                        )
                                    )
                                )
                            )
                        )
                    , new string[] { "CS_CallCriteria.CS_CallCriteriaValue" });
            }
        }

        /// <summary>
        /// Returns a list of contacts that matches division and customer criterias
        /// </summary>
        /// <param name="job">Criteria Job</param>
        /// <returns>List of CS_Contact entities</returns>
        public IList<CS_Contact> ListContactsCriteriaByDivisionAndCustomer(CS_Job job)
        {
            if (job.ID.Equals(Globals.GeneralLog.ID))
            {
                return _contactRepository.ListAll(
                    e => e.Active &&
                         e.CS_CallCriteria.Any(
                            f => f.Active &&
                                 f.EmployeeID.HasValue &&
                                 f.EmployeeID == e.ID &&
                                 f.CS_CallCriteriaValue.Any(
                                    z => z.Active &&
                                         z.CallCriteriaID == f.ID &&
                                         z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.GeneralLog
                                    )
                            )
                    , new string[] { "CS_CallCriteria.CS_CallCriteriaValue" });
            }
            else
            {
                List<string> divisionIdList = new List<string>();
                foreach (CS_JobDivision jobDivision in job.CS_JobDivision)
                    divisionIdList.Add(jobDivision.DivisionID.ToString());
                string customerId = job.CS_CustomerInfo.CustomerId.ToString();

                return _contactRepository.ListAll
                    (
                    e =>
                        (
                            e.CS_CallCriteria.Any
                            (
                            f =>
                                f.Active
                                && f.ContactID.HasValue
                                && f.ContactID == e.ID
                                &&
                                (
                                    (

                                        f.CS_CallCriteriaValue.Any
                                        (
                                        z =>
                                            z.Active
                                            && z.CallCriteriaID == f.ID
                                            && z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.Division
                                            && divisionIdList.Contains(z.Value)
                                        )
                                        &&
                                        f.CS_CallCriteriaValue.Any
                                        (
                                        z =>
                                            z.Active
                                            && z.CallCriteriaID == f.ID
                                            && z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.Customer
                                            && z.Value == customerId
                                        )
                                    )
                                    ||
                                    (
                                        f.CS_CallCriteriaValue.Any
                                        (
                                        z =>
                                            z.Active
                                            && z.CallCriteriaID == f.ID
                                            && z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.Division
                                            && divisionIdList.Contains(z.Value)
                                        )
                                        &&
                                        !f.CS_CallCriteriaValue.Any
                                        (
                                        z =>
                                            z.Active
                                            && z.CallCriteriaID == f.ID
                                            && z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.Customer
                                        )
                                    )
                                    ||
                                    (
                                        !f.CS_CallCriteriaValue.Any
                                        (
                                        z =>
                                            z.Active
                                            && z.CallCriteriaID == f.ID
                                            && z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.Division
                                        )
                                        &&
                                        f.CS_CallCriteriaValue.Any
                                        (
                                        z =>
                                            z.Active
                                            && z.CallCriteriaID == f.ID
                                            && z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.Customer
                                            && z.Value == customerId
                                        )
                                    )
                                )
                            )
                        )
                    , new string[] { "CS_CallCriteria.CS_CallCriteriaValue" });
            }
        }

        #region [ Comented ListEmployeeCriteriaByDivisionAndCustomer & ListContactsCriteriaByDivisionAndCustomer ]

        ///// <summary>
        ///// Returns a list of employees that matches division and customer criterias
        ///// </summary>
        ///// <param name="job">Criteria Job</param>
        ///// <returns>List of CS_Employee entities</returns>
        //public IList<CS_Employee> ListEmployeeCriteriaByDivisionAndCustomer(CS_Job job)
        //{
        //    List<string> divisionIdList = new List<string>();
        //    foreach (CS_JobDivision jobDivision in job.CS_JobDivision)
        //        divisionIdList.Add(jobDivision.DivisionID.ToString());
        //    string customerId = job.CS_CustomerInfo.CustomerId.ToString();

        //    return _employeeRepository.ListAll
        //        (
        //        e =>
        //            (
        //                e.CS_CallCriteria.Any
        //                (
        //                f =>
        //                    (
        //                        f.Active
        //                        && f.EmployeeID.HasValue
        //                        && f.EmployeeID == e.ID
        //                        && f.CS_CallCriteriaValue.Any
        //                        (
        //                        z =>
        //                            z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.Division
        //                            && divisionIdList.Contains(z.Value)
        //                        )
        //                        && f.CS_CallCriteriaValue.Any
        //                        (
        //                        z =>
        //                            z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.Customer
        //                            && z.Value == customerId
        //                        )
        //                    )
        //                )
        //            )
        //        );
        //}

        ///// <summary>
        ///// Returns a list of contacts that matches division and customer criterias
        ///// </summary>
        ///// <param name="job">Criteria Job</param>
        ///// <returns>List of CS_Contact entities</returns>
        //public IList<CS_Contact> ListContactsCriteriaByDivisionAndCustomer(CS_Job job)
        //{
        //    List<string> divisionIdList = new List<string>();
        //    foreach (CS_JobDivision jobDivision in job.CS_JobDivision)
        //        divisionIdList.Add(jobDivision.DivisionID.ToString());
        //    string customerId = job.CS_CustomerInfo.CustomerId.ToString();

        //    return _contactRepository.ListAll
        //        (
        //        e =>
        //            (
        //                e.CS_CallCriteria.Any
        //                (
        //                f =>
        //                    (
        //                        f.Active
        //                        && f.ContactID.HasValue
        //                        && f.ContactID == e.ID
        //                        && f.CS_CallCriteriaValue.Any
        //                        (
        //                        z =>
        //                            z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.Division
        //                            && divisionIdList.Contains(z.Value)
        //                        )
        //                        && f.CS_CallCriteriaValue.Any
        //                        (
        //                        z =>
        //                            z.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.Customer
        //                            && z.Value == customerId
        //                        )
        //                    )
        //                )
        //            )
        //        );
        //}
        #endregion

        public IList<CS_Employee> ListEmployeeByCallLogCriteria(string callLogTypeId, List<CS_Employee> employeeList)
        {
            List<CS_Employee> returnList =
                employeeList.FindAll(
                    e => e.Active
                        && e.CS_CallCriteria.FirstOrDefault(
                            w => w.Active
                                && w.EmployeeID == e.ID
                           ).CS_CallCriteriaValue.Any(
                            h => h.Active
                                && h.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.CallType
                                && h.Value == callLogTypeId));

            return returnList;
        }

        public IList<CS_Employee> VerifyHeavyEquipmentCallCriteria(List<CS_Employee> employeeList, CS_CallLog callLog)
        {
            List<CS_Employee> returnList = new List<CS_Employee>();

            bool hasHeavyEquipment = false;
            bool hasNonHeavyEquipment = false;
            bool hasEquipment = false;
            bool hasWhiteLight = false;

            if (callLog.CS_CallLogResource.Any(e => e.CS_Equipment != null && e.CS_Equipment.HeavyEquipment))
            {
                hasHeavyEquipment = true;
                hasEquipment = true;
            }

            if (callLog.CS_CallLogResource.Any(e => e.CS_Equipment != null && !e.CS_Equipment.HeavyEquipment))
            {
                hasNonHeavyEquipment = true;
                hasEquipment = true;
            }

            if (callLog.CS_CallLogResource.Any(e => e.CS_Equipment != null && e.CS_Equipment.CS_EquipmentWhiteLight.Any(f => f.Active)))
            {
                hasWhiteLight = true;
                hasEquipment = true;
            }

            returnList.AddRange(employeeList.FindAll(e => e.Active
                                    &&
                                    (
                                        (
                                            (e.CS_CallCriteria.FirstOrDefault
                                                (
                                                w => w.Active && w.EmployeeID == e.ID).CS_CallCriteriaValue.Any
                                                (
                                                h => h.Active
                                                    && h.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.HeavyEquipment
                                                    && bool.Parse(h.Value)
                                                    && hasHeavyEquipment))
                                            || (e.CS_CallCriteria.First
                                                (
                                                w => w.Active && w.EmployeeID == e.ID).CS_CallCriteriaValue).Any(
                                                h => h.Active
                                                    && h.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.HeavyEquipment) == false
                                        )
                                        ||
                                        (
                                            (e.CS_CallCriteria.FirstOrDefault
                                                (
                                                w => w.Active && w.EmployeeID == e.ID).CS_CallCriteriaValue.Any
                                                (
                                                h => h.Active
                                                    && h.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.NonHeavyEquipment
                                                    && bool.Parse(h.Value)
                                                    && hasNonHeavyEquipment))
                                            || (e.CS_CallCriteria.First
                                                (
                                                w => w.Active && w.EmployeeID == e.ID).CS_CallCriteriaValue).Any(
                                                h => h.Active
                                                    && h.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.NonHeavyEquipment) == false
                                        )
                                        ||
                                        (
                                            (e.CS_CallCriteria.FirstOrDefault
                                                (
                                                w => w.Active && w.EmployeeID == e.ID).CS_CallCriteriaValue.Any
                                                (
                                                h => h.Active
                                                    && h.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.AllEquipment
                                                    && bool.Parse(h.Value)
                                                    && hasEquipment))
                                            || (e.CS_CallCriteria.First
                                                (
                                                w => w.Active && w.EmployeeID == e.ID).CS_CallCriteriaValue).Any(
                                                h => h.Active
                                                    && h.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.AllEquipment) == false
                                        )
                                        ||
                                        (
                                            (e.CS_CallCriteria.FirstOrDefault
                                                (
                                                w => w.Active && w.EmployeeID == e.ID).CS_CallCriteriaValue.Any
                                                (
                                                h => h.Active
                                                    && h.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.WhiteLight
                                                    && bool.Parse(h.Value)
                                                    && hasWhiteLight))
                                            || (e.CS_CallCriteria.First
                                                (
                                                w => w.Active && w.EmployeeID == e.ID).CS_CallCriteriaValue).Any(
                                                h => h.Active
                                                    && h.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.WhiteLight) == false
                                        )
                                        ||
                                        !hasEquipment
                                    )
                                ));
            return returnList;
        }

        public IList<CS_Contact> ListCustomerContactByCallLogCriteria(string callLogTypeId, List<CS_Contact> contactList)
        {
            List<CS_Contact> returnList = contactList.FindAll(e => e.Active
                                                && e.CS_CallCriteria.FirstOrDefault
                                                    (
                                                    w => w.Active && w.ContactID == e.ID).CS_CallCriteriaValue.Any
                                                        (
                                                        h => h.Active
                                                                && h.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.CallType
                                                                && h.Value == callLogTypeId));

            return returnList;
        }

        public IList<CS_Contact> VerifyHeavyEquipmentCallCriteria(List<CS_Contact> contactList, CS_CallLog callLog)
        {
            List<CS_Contact> returnList = new List<CS_Contact>();
            bool hasHeavyEquipment = false;
            bool hasNonHeavyEquipment = false;
            bool hasEquipment = false;

            if (callLog.CS_CallLogResource.Any(e => e.CS_Equipment != null && e.CS_Equipment.HeavyEquipment))
            {
                hasHeavyEquipment = true;
                hasEquipment = true;
            }

            if (callLog.CS_CallLogResource.Any(e => e.CS_Equipment != null && !e.CS_Equipment.HeavyEquipment))
            {
                hasNonHeavyEquipment = true;
                hasEquipment = true;
            }

            returnList.AddRange(contactList.FindAll(e => e.Active
                                    && ((e.CS_CallCriteria.FirstOrDefault
                                        (
                                        w => w.Active && w.ContactID == e.ID).CS_CallCriteriaValue.Any
                                        (
                                        h => h.Active
                                            && h.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.HeavyEquipment
                                            && bool.Parse(h.Value)
                                            && hasHeavyEquipment))
                                    || (e.CS_CallCriteria.First
                                        (
                                        w => w.Active && w.ContactID == e.ID).CS_CallCriteriaValue).Any(
                                        h => h.Active
                                            && h.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.HeavyEquipment) == false)));

            returnList.AddRange(contactList.FindAll(e => e.Active
                                    && ((e.CS_CallCriteria.FirstOrDefault
                                        (
                                        w => w.Active && w.ContactID == e.ID).CS_CallCriteriaValue.Any
                                        (
                                        h => h.Active
                                            && h.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.NonHeavyEquipment
                                            && bool.Parse(h.Value)
                                            && hasNonHeavyEquipment))
                                    || (e.CS_CallCriteria.First
                                        (
                                        w => w.Active && w.ContactID == e.ID).CS_CallCriteriaValue).Any(
                                        h => h.Active
                                            && h.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.NonHeavyEquipment) == false)));

            returnList.AddRange(contactList.FindAll(e => e.Active
                                    && ((e.CS_CallCriteria.FirstOrDefault
                                        (
                                        w => w.Active && w.ContactID == e.ID).CS_CallCriteriaValue.Any
                                        (
                                        h => h.Active
                                            && h.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.AllEquipment
                                            && bool.Parse(h.Value)
                                            && hasEquipment))
                                    || (e.CS_CallCriteria.First
                                        (
                                        w => w.Active && w.ContactID == e.ID).CS_CallCriteriaValue).Any(
                                        h => h.Active
                                            && h.CallCriteriaTypeID == (int)Globals.CallCriteria.CallCriteriaType.AllEquipment) == false)));

            return returnList;
        }

        public IList<EmailVO> ListValidReceiptsByCallLog(int callLogId, int jobId)
        {
            CS_CallLog callLog = _callLogRepository.Get(e => e.Active && e.ID == callLogId);
            if (null != callLog)
                return ListReceiptsByCallLog(callLog.CallTypeID.ToString(), jobId, callLog);
            else
                return new List<EmailVO>();
        }

        /// <summary>
        /// Recursive Method that gets an active and valid Off Call By EmployeeID
        /// </summary>
        /// <param name="employeeID">Employee Identifier</param>
        /// <returns>an intance of CS_EmployeeOffCallHistory</returns>
        public CS_EmployeeOffCallHistory GetActiveOffCallByEmployeeID(int employeeID)
        {
            CS_EmployeeOffCallHistory offCallProxy = null;

            CS_EmployeeOffCallHistory offCall = _employeeOffCallRepository.Get(a => a.Active && a.EmployeeID == employeeID, "CS_Employee", "CS_Employee_Proxy");

            if (null != offCall)
            {
                offCallProxy = GetActiveOffCallByEmployeeID(offCall.ProxyEmployeeID);

                if (null != offCallProxy)
                    return offCallProxy;
                else
                    return offCall;
            }
            else
                return null;
        }

        public IList<EmailVO> ListReceiptsByCallLog(string callLogTypeId, int jobId, CS_CallLog callLog)
        {
            List<int> callCriteriaIDs = new List<int>();
            return ListReceiptsByCallLog(callLogTypeId, jobId, callLog, out callCriteriaIDs);
        }

        public IList<EmailVO> ListReceiptsByCallLog(string callLogTypeId, int jobId, CS_CallLog callLog, out List<int> callCriteriaIDs)
        {
            CS_Job job = new CS_Job();
            IList<CS_Employee> lstEmployee = new List<CS_Employee>();
            List<CS_Employee> lstEmployeeAux = new List<CS_Employee>();
            IList<CS_Contact> lstContacts = new List<CS_Contact>();
            IList<EmailVO> lstEmail = new List<EmailVO>();

            string emailDomain = string.Empty;

            SettingsModel model = new SettingsModel(_unitOfWork);
            emailDomain = model.GetDomain();

            CS_CallCriteriaRepository _rep = new CS_CallCriteriaRepository(_callCriteriaRepository, _callCriteriaRepository.UnitOfWork);

            int callLogID = 0;

            if (callLog != null)
                callLogID = callLog.ID;

            IList<CS_SP_CheckCallCriteria_Result> callCriteriaResult = _rep.CheckCallCriteria(callLogID, int.Parse(callLogTypeId), jobId);

            IList<int> result = new List<int>();

            for (int i = 0; i < callCriteriaResult.Count; i++)
			{
			    result.Add(callCriteriaResult[i].CallCriteriaID.Value);
			}

            callCriteriaIDs = result.ToList();

            lstEmployee = _employeeRepository.ListAll(e => e.CS_CallCriteria.Any(f => result.Contains(f.ID) && f.Active)).ToList();

            lstContacts = _contactRepository.ListAll(e => e.CS_CallCriteria.Any(f => result.Contains(f.ID) && f.Active)).ToList();

            //OLD METHOD
            //job = _jobRepository.Get(e => e.ID == jobId, new string[] { "CS_JobDivision", "CS_CustomerInfo", "CS_JobInfo", "CS_LocationInfo", "CS_JobDescription" });

            //lstEmployee = ListEmployeeCriteriaByDivisionAndCustomer(job);

            //if (!job.ID.Equals(Globals.GeneralLog.ID))
            //    lstEmployeeAux = FilterEmployeeByCriteriaValue(lstEmployee, job).ToList();

            //if (null != callLog)
            //    lstEmployeeAux.AddRange(VerifyHeavyEquipmentCallCriteria(lstEmployee.ToList(), callLog));

            //lstEmployee = lstEmployeeAux.Distinct().ToList();

            //lstEmployee = ListEmployeeByCallLogCriteria(callLogTypeId, lstEmployee.ToList());
            

            //for (int i = 0; i < lstEmployee.Count; i++)
            //{

            //    CS_EmployeeOffCallHistory offCall = GetActiveOffCallByEmployeeID(lstEmployee[i].ID);

            //    if (null != offCall)
            //        lstEmployee[i] = offCall.CS_Employee_Proxy;
            //}


            //lstContacts = ListContactsCriteriaByDivisionAndCustomer(job);

            //if (!job.ID.Equals(Globals.GeneralLog.ID))
            //    lstContacts = FilterContactsByCriteriaValue(lstContacts, job);

            //lstContacts = ListCustomerContactByCallLogCriteria(callLogTypeId, lstContacts.ToList());

            //if (null != callLog)
            //    lstContacts = VerifyHeavyEquipmentCallCriteria(lstContacts.ToList(), callLog);
           
            for (int i = 0; i < lstEmployee.Count; i++)
            {
                if (!string.IsNullOrEmpty(lstEmployee[i].GetEmployeeEmail(emailDomain)))
                {
                    EmailVO email = new EmailVO();
                    email.PersonID = lstEmployee[i].ID;
                    email.Name = lstEmployee[i].FullName;
                    email.Email = lstEmployee[i].GetEmployeeEmail(emailDomain);
                    email.Type = (int)Globals.CallCriteria.EmailVOType.Employee;

                    if (!lstEmail.Contains(email, new Globals.EmailService.EmailVO_Comparer()))
                        lstEmail.Add(email);
                }
            }

            for (int i = 0; i < lstContacts.Count; i++)
            {
                if (!string.IsNullOrEmpty(lstContacts[i].Email))
                {
                    EmailVO email = new EmailVO();
                    email.PersonID = lstContacts[i].ID;
                    email.Name = lstContacts[i].FullName;
                    email.Email = lstContacts[i].Email;
                    email.Type = (int)Globals.CallCriteria.EmailVOType.Contact;

                    if (!lstEmail.Contains(email, new Globals.EmailService.EmailVO_Comparer()))
                        lstEmail.Add(email);
                }
            }

            return lstEmail;
        }


        /// <summary>
        /// Returns a Call Criteria Entity and its values
        /// </summary>
        /// <param name="customerContactId">ID of the related contact</param>
        /// <returns>Call Criteria Entity</returns>
        public CS_CallCriteria GetCallCriteriaByCustomerContact(int customerContactId)
        {
            return _callCriteriaRepository.Get(e => e.Active && e.ContactID == customerContactId);
        }

        /// <summary>
        /// List call criteria by employee id
        /// </summary>
        /// <param name="contactID">employee id</param>
        /// <returns>list of entity call criteria</returns>
        public IList<CS_CallCriteria> ListCallCriteriaByEmployee(int employeeID, IList<int> callCriteriaIDs)
        {
            return _callCriteriaRepository.ListAll(w => w.Active && w.EmployeeID == employeeID && callCriteriaIDs.Contains(w.ID));
        }

        /// <summary>
        /// List call criteria by contact id
        /// </summary>
        /// <param name="employeeID">contact id</param>
        /// <returns>list of entity call criteria</returns>
        public IList<CS_CallCriteria> ListCallCriteriaByContact(int contactID, IList<int> callCriteriaIDs)
        {
            return _callCriteriaRepository.ListAll(w => w.Active && w.ContactID == contactID && callCriteriaIDs.Contains(w.ID));
        }

        /// <summary>
        /// List all CallCriteria Values by Id
        /// </summary>
        /// <param name="callCriteriaId"></param>
        public IList<CS_CallCriteriaValue> ListAllCallCriteriaValuesListById(int callCriteriaId)
        {
            return _callCriteriaValueRepository.ListAll(e => e.Active && e.CallCriteriaID == callCriteriaId).ToList();
        }

        /// <summary>
        /// Inserts a new Employee Criteria and Values into the DB
        /// </summary>
        /// <param name="criteria">Criteria Entity</param>
        /// <param name="criteriaValuesList">Criteria Values Entity List</param>
        /// <param name="username">Current app Username</param>
        public bool SaveEmployeeCriteria(CS_CallCriteria criteria, IList<CS_CallCriteriaValue> criteriaValuesList, string username)
        {
            return SaveCriteria(criteria, criteriaValuesList, username);
        }

        /// <summary>
        /// Inserts a new Customer Criteria and Values into the DB
        /// </summary>
        /// <param name="criteria">Criteria Entity</param>
        /// <param name="criteriaValuesList">Criteria Values Entity List</param>
        /// <param name="username">Current app Username</param>
        public bool SaveContactCriteria(CS_CallCriteria criteria, IList<CS_CallCriteriaValue> criteriaValuesList, string username)
        {
            bool returnValue = false;
            using (TransactionScope scope = new TransactionScope())
            {
                returnValue = SaveCriteria(criteria, criteriaValuesList, username);

                scope.Complete();
            }
            return returnValue;
        }

        /// <summary>
        /// Inserts a new Criteria and Values into the DB
        /// </summary>
        /// <param name="criteria">Criteria Entity</param>
        /// <param name="criteriaValuesList">Criteria Values Entity List</param>
        /// <param name="username">Current app Username</param>
        public bool SaveCriteria(CS_CallCriteria criteria, IList<CS_CallCriteriaValue> criteriaValuesList, string username)
        {
            if (null == criteria)
            {
                Exception ex = new Exception("The Call Criteria object is null!");
                Logger.Write(string.Format("The Call Criteria object is null.\n{0}\n{1}", ex, ex.InnerException));
                throw ex;
            }

            if (null == criteriaValuesList)
            {
                Exception ex = new Exception("The Call Criteria Value Value object is null!");
                Logger.Write(string.Format("The Call Criteria Value object is null.\n{0}\n{1}", ex, ex.InnerException));
                throw ex;
            }

            try
            {
                criteria.CreatedBy = username;
                criteria.CreationDate = DateTime.Now;
                criteria.ModifiedBy = username;
                criteria.ModificationDate = DateTime.Now;
                criteria.Active = true;

                criteria = _callCriteriaRepository.Add(criteria);
            }

            catch (Exception ex)
            {
                Logger.Write(string.Format("An error while trying to save Criteria information.\n{0}\n{1}", ex, ex.InnerException));
                throw new Exception("Error while trying to save Criteria information!", ex);
            }

            try
            {
                if (null != criteriaValuesList)
                {
                    for (int i = 0; i < criteriaValuesList.Count; i++)
                    {
                        criteriaValuesList[i].CallCriteriaID = criteria.ID;
                        criteriaValuesList[i].CreatedBy = username;
                        criteriaValuesList[i].CreationDate = DateTime.Now;
                        criteriaValuesList[i].ModifiedBy = username;
                        criteriaValuesList[i].ModificationDate = DateTime.Now;
                        criteriaValuesList[i].Active = true;
                    }

                    _callCriteriaValueRepository.AddList(criteriaValuesList);
                }
            }

            catch (Exception ex)
            {
                Logger.Write(string.Format("An error while trying to save Criteria Values information.\n{0}\n{1}", ex, ex.InnerException));
                throw new Exception("Error while trying to save Criteria Values information!", ex);
            }

            return true;
        }

        /// <summary>
        /// Updates an Employee Criteria and Values on the DB
        /// </summary>
        /// <param name="criteria">Criteria Entity</param>
        /// <param name="criteriaValuesList">Criteria Values Entity List</param>
        /// <param name="username">Current app Username</param>
        public void UpdateEmployeeCriteria(CS_CallCriteria criteria, IList<CS_CallCriteriaValue> addedList, IList<CS_CallCriteriaValue> removedList, IList<CS_CallCriteriaValue> updatedList, string username)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                UpdateCriteria(criteria, addedList, removedList, updatedList, username);
                scope.Complete();
            }
        }

        /// <summary>
        /// Updates a Customer Criteria and Values on the DB
        /// </summary>
        /// <param name="criteria">Criteria Entity</param>
        /// <param name="criteriaValuesList">Criteria Values Entity List</param>
        /// <param name="username">Current app Username</param>
        public void UpdateContactCriteria(CS_CallCriteria criteria, IList<CS_CallCriteriaValue> addedList, IList<CS_CallCriteriaValue> removedList, IList<CS_CallCriteriaValue> updatedList, string username)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                UpdateCriteria(criteria, addedList, removedList, updatedList, username);
                scope.Complete();
            }
        }

        /// <summary>
        /// Updates a Criteria and Values on the DB
        /// </summary>
        /// <param name="criteria">Criteria Entity</param>
        /// <param name="criteriaValuesList">Criteria Values Entity List</param>
        /// <param name="username">Current app Username</param>
        public void UpdateCriteria(CS_CallCriteria criteria, IList<CS_CallCriteriaValue> addedList, IList<CS_CallCriteriaValue> removedList, IList<CS_CallCriteriaValue> updatedList, string username)
        {
          

            if (null == criteria)
            {
                Exception ex = new Exception("The Call Criteria object is null!");
                Logger.Write(string.Format("The Call Criteria object is null.\n{0}\n{1}", ex, ex.InnerException));
                throw ex;
            }

            if (null == addedList || null == removedList)
            {
                Exception ex = new Exception("The Call Criteria Value List object is null!");
                Logger.Write(string.Format("The Call Criteria Value List object is null.\n{0}\n{1}", ex, ex.InnerException));
                throw ex;
            }

            try
            {
                criteria.ModifiedBy = username;
                criteria.ModificationDate = DateTime.Now;

                criteria = _callCriteriaRepository.Update(criteria);
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error while trying to save Call Criteria information.\n{0}\n{1}", ex, ex.InnerException));
                throw new Exception("Error while trying to save Call Criteria information!", ex);
            }

            try
            {
                for (int i = 0; i < addedList.Count; i++)
                {
                    addedList[i].CallCriteriaID = criteria.ID;
                    addedList[i].CreatedBy = username;
                    addedList[i].CreationDate = DateTime.Now;
                    addedList[i].ModifiedBy = username;
                    addedList[i].ModificationDate = DateTime.Now;
                    addedList[i].Active = true;
                }

                _callCriteriaValueRepository.AddList(addedList);

                for (int i = 0; i < removedList.Count; i++)
                {
                    removedList[i].ModifiedBy = username;
                    removedList[i].ModificationDate = DateTime.Now;
                    removedList[i].Active = false;
                }

                _callCriteriaValueRepository.UpdateList(removedList);

                for (int i = 0; i < updatedList.Count; i++)
                {
                    updatedList[i].ModifiedBy = username;
                    updatedList[i].ModificationDate = DateTime.Now;
                    updatedList[i].Active = true;
                }

               // removedList.AddRange(updatedList);
                _callCriteriaValueRepository.UpdateList(updatedList);
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error while trying to save Call Criteria Values information.\n{0}\n{1}", ex, ex.InnerException));
                throw new Exception("Error while trying to save Call Criteria Values information!", ex);
            }
        }

        /// <summary>
        /// Generates the Subject for the email that needs to be sent by the Call Criteria Process
        /// </summary>
        /// <param name="job">Job Details</param>
        /// <param name="jobInfo">Job Info Details</param>
        /// <param name="customerInfo">Customer Info Details</param>
        /// <param name="locationInfo">Location Info Details</param>
        /// <param name="callType">Call Type</param>
        /// <returns>Email Subject</returns>
        public string GenerateSubjectForCallCriteria(CS_Job job, CS_JobInfo jobInfo, CS_CustomerInfo customerInfo, CS_LocationInfo locationInfo, string callType)
        {
            string subject = string.Empty;

            if (job.ID == Globals.GeneralLog.ID)
                subject = string.Format("{0} - General Log - {1}", job.Number, callType);
            else
            {
                if (null != job)
                    subject = string.Format("{0}, {1}, {2}, {3} {4}, {5}",
                        job.PrefixedNumber,
                        customerInfo.CS_Customer.Name.Trim(),
                        jobInfo.CS_JobAction.Description,
                        locationInfo.CS_City.Name,
                        locationInfo.CS_State.Acronym,
                        callType);
            }

            return subject;
        }

        /// <summary>
        /// Generates the body for the email
        /// </summary>
        /// <param name="lstCallLog">lst call log</param>
        /// <returns>email body</returns>
        public string GenerateBodyCallLogEmailTable(List<CS_CallLog> lstCallLog)
        {
            string body = string.Empty;

            foreach (CS_CallLog csCallLog in lstCallLog)
            {
                StringBuilder callNote = new StringBuilder();
                callNote.AppendFormat("Call Type:<Text>{0}<BL>", csCallLog.CS_CallType.Description);
                callNote.AppendFormat("Call Date:<Text>{0}<BL>", csCallLog.CallDate.ToString("MM/dd/yyyy"));
                callNote.AppendFormat("Call Time:<Text>{0}:{1}<BL>", csCallLog.CallDate.Hour, csCallLog.CallDate.Minute);

                if (!string.IsNullOrEmpty(csCallLog.Note))
                {
                    callNote.Append(csCallLog.Note);
                }

                body += StringManipulation.TabulateStringTable(callNote.ToString());
            }

            return body;
        }

        /// <summary>
        /// Generates the body for the email
        /// </summary>
        /// <param name="lstCallLog">lst call log</param>
        /// <returns>email body</returns>
        public string GenerateBodyCallLogEmail(List<CS_CallLog> lstCallLog)
        {
            string body = string.Empty;

            foreach (CS_CallLog csCallLog in lstCallLog)
            {
                StringBuilder callNote = new StringBuilder();
                callNote.AppendFormat("Call Type:<Text>{0}<BL>", csCallLog.CS_CallType.Description);
                callNote.AppendFormat("Call Date:<Text>{0}<BL>", csCallLog.CallDate.ToString("MM/dd/yyyy"));
                callNote.AppendFormat("Call Time:<Text>{0}:{1}<BL>", csCallLog.CallDate.Hour, csCallLog.CallDate.Minute);

                if (!string.IsNullOrEmpty(csCallLog.Note))
                {
                    callNote.Append(csCallLog.Note);
                }

                body += StringManipulation.TabulateString(callNote.ToString());
            }

            return body;
        }

        /// <summary>
        /// Gets the Initial Advise Note string based on selected call criteria values
        /// </summary>
        /// <param name="isEmployee">If is Hulcher Employee, then TRUE, else FALSE</param>
        /// <param name="personID">Person Identifier (EmployeeID or ContactID)</param>
        /// <returns>Initial Advise Note</returns>
        public string GetInitalAdviseNote(bool isEmployee, int personID)
        {
            CS_CallCriteriaRepository repository = new CS_CallCriteriaRepository(_callCriteriaRepository, _unitOfWork);
            return repository.GetInitialAdviseNote(isEmployee, personID);
        }

        /// <summary>
        /// Removes a call criteria configuration
        /// </summary>
        /// <param name="callCriteriaId">Call Criteria Identifier</param>
        /// <param name="username">Username that removed the Call Criteria</param>
        public void DeleteCallCriteria(int callCriteriaId, string username)
        {
            CS_CallCriteria callCriteria = _callCriteriaRepository.Get(e => e.ID == callCriteriaId);
            if (null != callCriteria)
            {
                callCriteria.Active = false;
                callCriteria.ModificationDate = DateTime.Now;
                callCriteria.ModifiedBy = username;

                _callCriteriaRepository.Update(callCriteria);
            }
        }

        public void SaveCallCriteria(CS_CallCriteria saveCriteria, IList<CS_CallCriteriaValue> saveCriteriaValueList, string username)
        {
            try
            {
                if (0 == saveCriteria.ID)
                    SaveEmployeeCriteria(saveCriteria, saveCriteriaValueList, username);
                else
                {
                    // Update Call Criteria
                    List<CS_CallCriteriaValue> removedList = null;
                    List<CS_CallCriteriaValue> addedList = null;
                    List<CS_CallCriteriaValue> updatedList = null;

                    IList<CS_CallCriteriaValue> oldList = ListAllCallCriteriaValuesListById(saveCriteria.ID);
                    removedList = oldList.Where(e => !saveCriteriaValueList.Any(f => f.CallCriteriaTypeID == e.CallCriteriaTypeID && f.Value == e.Value)).ToList();
                    addedList = saveCriteriaValueList.Where(e => !oldList.Any(f => f.CallCriteriaTypeID == e.CallCriteriaTypeID && f.Value == e.Value)).ToList();
                    updatedList = saveCriteriaValueList.Where(e => oldList.Any(f => f.CallCriteriaTypeID == e.CallCriteriaTypeID && f.Value == e.Value)).ToList();

                    for (int i = 0; i < updatedList.Count; i++)
                    {
                        CS_CallCriteriaValue ccValue = oldList.FirstOrDefault(f => f.CallCriteriaTypeID == updatedList[i].CallCriteriaTypeID && f.Value == updatedList[i].Value);

                        updatedList[i].CallCriteriaID = ccValue.CallCriteriaID;
                        updatedList[i].ID = ccValue.ID;
                        updatedList[i].CreatedBy = ccValue.CreatedBy;
                        updatedList[i].CreationDate = ccValue.CreationDate;
                        updatedList[i].CreationID = ccValue.CreationID;
                    }

                    UpdateEmployeeCriteria(saveCriteria, addedList, removedList, updatedList, username);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error occured while trying to Update Call Criteria Information.\n{0}\n{1}", ex, ex.InnerException));
                throw new Exception("Error while updating Call Criteria information", ex);
            }
        }
        #endregion

        #region [ IDisposable Implementation ]

        public void Dispose()
        {
            _callLogRepository = null;
            _callCriteriaRepository = null;
            _callCriteriaValueRepository = null;
            _jobRepository = null;
            _employeeRepository = null;
            _contactRepository = null;
            _employeeOffCallRepository = null;

            _callLogModel.Dispose();
            _callLogModel = null;

            _unitOfWork.Dispose();
            _unitOfWork = null;
        }

        #endregion
    }
}
