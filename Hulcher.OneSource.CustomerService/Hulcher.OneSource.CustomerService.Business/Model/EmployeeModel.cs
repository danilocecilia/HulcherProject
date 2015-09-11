using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;

using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Core.Utils;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.DataContext.VO;
using Hulcher.OneSource.CustomerService.Integration;
using Hulcher.OneSource.CustomerService.Integration.Entities;

using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Hulcher.OneSource.CustomerService.Business.Model
{
    public class EmployeeModel : IDisposable
    {
        #region [ Atributes ]

        /// <summary>
        /// Unit of Work used to access the database/in-memory context
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// Repository class for CS_Employee
        /// </summary>
        private ICachedRepository<CS_Employee> _employeeRepository;

        /// <summary>
        /// Repository class for CS_EmployeeOffCallHistrory
        /// </summary>
        private IRepository<CS_EmployeeOffCallHistory> _employeeOffCallRepository;

        /// <summary>
        /// Repository class for CS_EmployeeCoverage
        /// </summary>
        private IRepository<CS_EmployeeCoverage> _employeeCoverageRepository;

        /// <summary>
        /// Repository class for CS_View_EmployeeInfo
        /// </summary>
        private IRepository<CS_View_EmployeeInfo> _employeeInfoRepository;

        /// <summary>
        /// Repository class for CS_View_Employee_CallLogInfo
        /// </summary>
        private IRepository<CS_View_Employee_CallLogInfo> _employeeCallLogInfoRepository;

        /// <summary>
        /// Repository class for CS_Division
        /// </summary>
        private IRepository<CS_Division> _divisionRepository;

        /// <summary>
        /// Repository class for CS_EmployeeEmergencyContact
        /// </summary>
        private IRepository<CS_EmployeeEmergencyContact> _employeeEmergencyContactRepository;

        /// <summary>
        /// Repository class for CS_CustomerInfo
        /// </summary>
        private IRepository<CS_CustomerInfo> _customerInfoRepository;

        /// <summary>
        /// Repository class for CS_Contact
        /// </summary>
        private IRepository<CS_Contact> _contactRepository;

        /// <summary>
        /// Repository class for CS_CallCriteria
        /// </summary>
        private IRepository<CS_CallCriteria> _callCriteriaRepository;

        /// <summary>
        /// Repsository class for CS_CallCriteriaValue
        /// </summary>
        private IRepository<CS_CallCriteriaValue> _callCriteriaValueRepository;

        /// <summary>
        /// Repsository class for CS_CallLog
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
        /// Repository class for CS_PhoneType
        /// </summary>
        private IRepository<CS_PhoneType> _phoneTypeRepository;

        /// <summary>
        /// Repository class for CS_PhoneNumber
        /// </summary>
        private IRepository<CS_PhoneNumber> _phoneNumberRepository;

        /// <summary>
        /// Repository class for CS_View_ManagersLocation
        /// </summary>
        private IRepository<CS_View_ManagersLocation> _managersLocationRepository;

        /// <summary>
        /// Model class for Call Criteria
        /// </summary>
        private CallCriteriaModel _callCriteriaModel;

        /// <summary>
        /// Model class for Call Log
        /// </summary>
        private CallLogModel _callLogModel;

        /// <summary>
        /// Model class for Settings
        /// </summary>
        private SettingsModel _settingsModel;

        /// <summary>
        /// Model class for Email
        /// </summary>
        private EmailModel _emailModel;
        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        public EmployeeModel()
        {
            _unitOfWork = new EFUnitOfWork();
            InstanceObjects();
        }

        /// <summary>
        /// Class Constructor
        /// </summary>
        public EmployeeModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            InstanceObjects();
        }

        /// <summary>
        /// Instance objects
        /// </summary>
        private void InstanceObjects()
        {
            _employeeRepository = new CachedRepository<CS_Employee>();
            _employeeOffCallRepository = new EFRepository<CS_EmployeeOffCallHistory>();
            _employeeCoverageRepository = new EFRepository<CS_EmployeeCoverage>();
            _employeeInfoRepository = new EFRepository<CS_View_EmployeeInfo>();
            _employeeCallLogInfoRepository = new EFRepository<CS_View_Employee_CallLogInfo>();
            _divisionRepository = new EFRepository<CS_Division>();
            _employeeEmergencyContactRepository = new EFRepository<CS_EmployeeEmergencyContact>();
            _customerInfoRepository = new EFRepository<CS_CustomerInfo>();
            _contactRepository = new EFRepository<CS_Contact>();
            _callCriteriaRepository = new EFRepository<CS_CallCriteria>();
            _callCriteriaValueRepository = new EFRepository<CS_CallCriteriaValue>();
            _callLogRepository = new EFRepository<CS_CallLog>();
            _callLogResourceRepository = new EFRepository<CS_CallLogResource>();
            _resourceRepository = new EFRepository<CS_Resource>();
            _phoneTypeRepository = new EFRepository<CS_PhoneType>();
            _phoneNumberRepository = new EFRepository<CS_PhoneNumber>();
            _managersLocationRepository = new EFRepository<CS_View_ManagersLocation>();

            _employeeRepository.UnitOfWork = _unitOfWork;
            _employeeOffCallRepository.UnitOfWork = _unitOfWork;
            _employeeCoverageRepository.UnitOfWork = _unitOfWork;
            _employeeInfoRepository.UnitOfWork = _unitOfWork;
            _employeeCallLogInfoRepository.UnitOfWork = _unitOfWork;
            _divisionRepository.UnitOfWork = _unitOfWork;
            _employeeEmergencyContactRepository.UnitOfWork = _unitOfWork;
            _customerInfoRepository.UnitOfWork = _unitOfWork;
            _contactRepository.UnitOfWork = _unitOfWork;
            _callCriteriaRepository.UnitOfWork = _unitOfWork;
            _callCriteriaValueRepository.UnitOfWork = _unitOfWork;
            _callLogRepository.UnitOfWork = _unitOfWork;
            _callLogResourceRepository.UnitOfWork = _unitOfWork;
            _resourceRepository.UnitOfWork = _unitOfWork;
            _phoneTypeRepository.UnitOfWork = _unitOfWork;
            _phoneNumberRepository.UnitOfWork = _unitOfWork;
            _managersLocationRepository.UnitOfWork = _unitOfWork;

            _callCriteriaModel = new CallCriteriaModel(_unitOfWork);
            _settingsModel = new SettingsModel(_unitOfWork);
            _emailModel = new EmailModel(_unitOfWork);
            _callLogModel = new CallLogModel(_unitOfWork);
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Return a list of all employees filtered by name or last name
        /// </summary>
        /// <param name="filteredEmployee">filteredEmployee</param>
        /// <returns>List filtered employees</returns>
        public IList<CS_Employee> ListAllFilteredEmployee(string filteredEmployee)
        {
            string[] arrValue = filteredEmployee.Split(',');
            for (int i = 0; i < arrValue.Length; i++)
                arrValue[i] = arrValue[i].Trim();

            return _employeeRepository.ListAll(w => arrValue.Any(f => w.Active && ((w.FirstName + " " + w.Name).Contains(f) || (w.FirstName + " " + w.Name + " \"" + w.Nickname + "\"").Contains(f))), new string[] { "CS_Division" }).ToList();
        }

        /// <summary>
        /// List all items of an Entity in the Databas
        /// </summary>
        /// <returns>List of Entities</returns>
        public IList<CS_Employee> ListAllEmployee()
        {
            return _employeeRepository.ListAll(e => e.Active, new string[] { "CS_Division" }).OrderBy(e => e.FirstName).ThenBy(e => e.Name).ToList();
            //return EmployeeDao.Singleton.ListAll();
        }

        /// <summary>
        /// List all Employee filtered by a divisionID
        /// </summary>
        /// <returns>List of Employee</returns>
        public IList<CS_Employee> ListAllFilteredEmployee(long divisionId)
        {
            return _employeeRepository.ListAll(e => e.DivisionID == divisionId && e.Active == true).OrderBy(e => e.FirstName).ToList();
        }

        /// <summary>
        /// List All employee Project Managers (ADMT, ADM, DM, SDM, GM, RVP, BDM, PM, SPM, SSMS)
        /// </summary>
        /// <returns>List of Employee</returns>
        public IList<CS_Employee> ListAllEmployeeProjectManager()
        {
            string[] titles = new string[] { "assistant division manager trainee - non union", 
                                             "assistant division manager trainee - union", 
                                             "assistant division manager", 
                                             "division manager", 
                                             "senior division manager",
                                             "general manager – operations",
                                             "regional vice president - sales",
                                             "regional vice president - operations",
                                             "business development manager",
                                             "project manager",
                                             "senior project manager",
                                             "senior sales and marketing specialist"};

            return _employeeRepository.ListAll(
                e => titles.Contains(e.BusinessCardTitle.ToLower()) && e.Active == true,
                new string[] { "CS_Division", "CS_EmployeeCoverage" }).OrderBy(e => e.CS_Division.Name != null ? e.CS_Division.Name : e.FullName).ThenBy(e => e.FullName).ToList();
        }

        public IList<CS_Employee> ListAllEmployeeProjectManagerByName(string name)
        {
            string[] titles = new string[] { "assistant division manager trainee - non union", 
                                             "assistant division manager trainee - union", 
                                             "assistant division manager", 
                                             "division manager", 
                                             "senior division manager",
                                             "general manager – operations",
                                             "regional vice president - sales",
                                             "regional vice president - operations",
                                             "business development manager",
                                             "project manager",
                                             "senior project manager",
                                             "senior sales and marketing specialist"};

            return _employeeRepository.ListAll(e => titles.Contains(e.BusinessCardTitle.ToLower()) && e.Active == true && (e.FirstName.Contains(name) || e.Name.Contains(name) || e.Nickname.Contains(name) || (e.FirstName + " " + e.Name).Contains(name) || (e.FirstName + " " + e.Name + " \"" + e.Nickname + "\"").Contains(name) || (e.CS_Division.Name + " - " + e.Name + ", " + e.FirstName).Contains(name) || (e.CS_Division.Name + " - " + e.Name + ", " + e.FirstName + " \"" + e.Nickname + "\"").Contains(name)),
                new string[] { "CS_Division", "CS_EmployeeCoverage" }).OrderBy(e => e.CS_Division.Name != null ? e.CS_Division.Name : e.FullName).ThenBy(e => e.FullName).ToList();
        }

        /// <summary>
        /// List all items of an Entity in the Database
        /// </summary>
        /// <returns>List of Entities</returns>
        public IList<CS_Employee> ListAllEmployeeRVP()
        {
            return _employeeRepository.ListAll(e => e.Active == true & (e.JobCode.ToUpper().Contains("OPSRVP") || e.JobCode.ToUpper().Contains("REGVPS")));
            //return EmployeeDao.Singleton.ListAllRVP();
        }

        /// <summary>
        /// List all RVP's that arent assigned to a Region
        /// </summary>
        public IList<CS_Employee> ListNotAssignedEmployeeRVP()
        {
            return _employeeRepository.ListAll(e => e.Active == true && (e.JobCode.ToUpper().Contains("OPSRVP") || e.JobCode.ToUpper().Contains("REGVPS")) && !e.CS_Region_RVP.Any(w => w.Active));
        }

        /// <summary>
        /// List employee's associated with a group of Regions
        /// </summary>
        public IList<CS_Employee> ListEmployeeByRegionIDList(IList<int> regionID, Globals.RegionalMaintenance.FilterType filterType, string filterValue)
        {
            string[] arrValue = filterValue.Split(',');
            for (int i = 0; i < arrValue.Length; i++)
                arrValue[i] = arrValue[i].Trim();

            switch (filterType)
            {
                case Globals.RegionalMaintenance.FilterType.ComboName:
                case Globals.RegionalMaintenance.FilterType.EquipmentUnitNumber:
                    return new List<CS_Employee>();
                case Globals.RegionalMaintenance.FilterType.EmployeeName:
                    return _employeeRepository.ListAll(
                        e => e.Active &&
                             e.CS_Division.CS_Region_Division.Any(
                                f => f.Active &&
                                     regionID.Contains(f.RegionID)) &&
                            arrValue.Any(
                                like => e.Name.Contains(like) ||
                                        e.FirstName.Contains(like) ||
                                        e.Nickname.Contains(like)),
                        "CS_Division");
                case Globals.RegionalMaintenance.FilterType.None:
                case Globals.RegionalMaintenance.FilterType.Region:
                case Globals.RegionalMaintenance.FilterType.RVP:
                case Globals.RegionalMaintenance.FilterType.Division:
                default:
                    return _employeeRepository.ListAll(
                        e => e.Active &&
                             e.CS_Division.CS_Region_Division.Any(
                                f => f.Active &&
                                     regionID.Contains(f.RegionID)),
                        "CS_Division");
            }
        }

        /// <summary>
        /// List all EmployeeInfo from the view CS_View_EmployeeInfo
        /// </summary>
        /// <returns>List of Entities</returns>
        public IList<CS_View_EmployeeInfo> ListAllEmployeeInfo()
        {
            return _employeeInfoRepository.ListAll(e => e.Active).OrderBy(e => e.DivisionName != null ? e.DivisionName : e.EmployeeName).ThenBy(e => e.EmployeeName).ToList();
        }

        /// <summary>
        /// Return list all employee info by division
        /// </summary>
        /// <param name="lstDivisionId">lst division id</param>
        /// <returns>list</returns>
        public IList<CS_View_EmployeeInfo> ListAllEmployeeInfoByDivision(List<int> lstDivisionId)
        {
            return _employeeInfoRepository.ListAll(e => e.Active && lstDivisionId.Contains(e.DivisionId)).OrderBy(e => e.DivisionName != null ? e.DivisionName : e.EmployeeName).ThenBy(e => e.EmployeeName).ToList();
        }

        /// <summary>
        /// Return list all employee info by division and reserved
        /// </summary>
        /// <param name="lstDivisionId">lst division id</param>
        /// <returns>list</returns>
        public IList<CS_View_EmployeeInfo> ListAllEmployeeInfoByDivisionAndEmployee(List<int> lstDivisionId, List<int> lstEmployeeId)
        {
            return _employeeInfoRepository.ListAll(e => e.Active && lstDivisionId.Contains(e.DivisionId) && lstEmployeeId.Contains(e.EmployeeId)).OrderBy(e => e.DivisionName != null ? e.DivisionName : e.EmployeeName).ThenBy(e => e.EmployeeName).ToList();
        }

        /// <summary>
        /// List Filtered EmployeeInfo from the view CS_View_EmployeeInfo
        /// </summary>
        /// <returns>List of Entities</returns>
        public IList<CS_View_EmployeeInfo> ListFilteredEmployeeInfo(Globals.ResourceAllocation.EmployeeFilters filter, string value)
        {
            string[] arrValue = value.Split(',');
            for (int i = 0; i < arrValue.Length; i++)
                arrValue[i] = arrValue[i].Trim();

            switch (filter)
            {
                case Globals.ResourceAllocation.EmployeeFilters.Division:
                    return _employeeInfoRepository.ListAll(e => arrValue.Any(f => e.DivisionName.Contains(f))).OrderBy(e => e.DivisionName != null ? e.DivisionName : e.EmployeeName).ThenBy(e => e.EmployeeName).ToList();
                //return EmployeeInfoDao.Singleton.ListFilteredByDivision(arrValue);
                case Globals.ResourceAllocation.EmployeeFilters.DivisionState:
                    return _employeeInfoRepository.ListAll(e => arrValue.Any(f => e.StateName.Contains(f))).OrderBy(e => e.DivisionName != null ? e.DivisionName : e.EmployeeName).ThenBy(e => e.EmployeeName).ToList();
                case Globals.ResourceAllocation.EmployeeFilters.DivisionStateAcronym:
                    return _employeeInfoRepository.ListAll(e => arrValue.Any(f => e.State.Contains(f))).OrderBy(e => e.DivisionName != null ? e.DivisionName : e.EmployeeName).ThenBy(e => e.EmployeeName).ToList();
                //return EmployeeInfoDao.Singleton.ListFilteredByDivisionState(arrValue);
                case Globals.ResourceAllocation.EmployeeFilters.Status:
                    return _employeeInfoRepository.ListAll(e => arrValue.Any(f => e.Assigned.Contains(f))).OrderBy(e => e.DivisionName != null ? e.DivisionName : e.EmployeeName).ThenBy(e => e.EmployeeName).ToList();
                //return EmployeeInfoDao.Singleton.ListFilteredByStatus(arrValue);
                case Globals.ResourceAllocation.EmployeeFilters.JobNumber:
                    return _employeeInfoRepository.ListAll(e => arrValue.Any(f => e.JobNumber.Contains(f))).OrderBy(e => e.DivisionName != null ? e.DivisionName : e.EmployeeName).ThenBy(e => e.EmployeeName).ToList();
                //return EmployeeInfoDao.Singleton.ListFilteredByJobNumber(arrValue);
                case Globals.ResourceAllocation.EmployeeFilters.Position:
                    return _employeeInfoRepository.ListAll(e => arrValue.Any(f => e.Position.Contains(f))).OrderBy(e => e.DivisionName != null ? e.DivisionName : e.EmployeeName).ThenBy(e => e.EmployeeName).ToList();
                //return EmployeeInfoDao.Singleton.ListFilteredByPosition(arrValue);
                case Globals.ResourceAllocation.EmployeeFilters.Employee:
                    return _employeeInfoRepository.ListAll(e => arrValue.Any(f => e.EmployeeName.Contains(f))).OrderBy(e => e.DivisionName != null ? e.DivisionName : e.EmployeeName).ThenBy(e => e.EmployeeName).ToList();
                //return EmployeeInfoDao.Singleton.ListFilteredByEmployee(arrValue);
                default:
                    return new List<CS_View_EmployeeInfo>();
            }
        }

        /// <summary>
        /// List Filtered Employee
        /// </summary>
        public IList<CS_Employee> ListFilteredEmployee(Globals.FirstAlert.EmployeeFilters filter, string value, int? JobID)
        {
            string[] arrValue = value.Split(',');
            for (int i = 0; i < arrValue.Length; i++)
                arrValue[i] = arrValue[i].Trim();

            switch (filter)
            {
                case Globals.FirstAlert.EmployeeFilters.Division:
                    return _employeeRepository.ListAll(e => e.Active && ((JobID.HasValue && e.CS_Resource.Any(h => h.JobID == JobID.Value && h.Active)) || !JobID.HasValue) && arrValue.Any(f => e.CS_Division.Name.Contains(f))).OrderBy(e => e.CS_Division != null ? e.CS_Division.Name : (e.Name + ", " + e.FirstName)).ThenBy(e => (e.Name + ", " + e.FirstName)).ToList();
                case Globals.FirstAlert.EmployeeFilters.FirstName:
                    return _employeeRepository.ListAll(e => e.Active && ((JobID.HasValue && e.CS_Resource.Any(h => h.JobID == JobID.Value && h.Active)) || !JobID.HasValue) && arrValue.Any(f => e.FirstName.Contains(f))).OrderBy(e => e.CS_Division != null ? e.CS_Division.Name : (e.Name + ", " + e.FirstName)).ThenBy(e => (e.Name + ", " + e.FirstName)).ToList();
                case Globals.FirstAlert.EmployeeFilters.LastName:
                    return _employeeRepository.ListAll(e => e.Active && ((JobID.HasValue && e.CS_Resource.Any(h => h.JobID == JobID.Value && h.Active)) || !JobID.HasValue) && arrValue.Any(f => e.Name.Contains(f))).OrderBy(e => e.CS_Division != null ? e.CS_Division.Name : (e.Name + ", " + e.FirstName)).ThenBy(e => (e.Name + ", " + e.FirstName)).ToList();
                case Globals.FirstAlert.EmployeeFilters.Location:
                    return _employeeRepository.ListAll(e => e.Active && ((JobID.HasValue && e.CS_Resource.Any(h => h.JobID == JobID.Value && h.Active)) || !JobID.HasValue) && arrValue.Any(f => (e.CS_Division.CS_State.Acronym + ", " + e.CS_Division.CS_State.Name).Contains(f))).OrderBy(e => e.CS_Division != null ? e.CS_Division.Name : (e.Name + ", " + e.FirstName)).ThenBy(e => (e.Name + ", " + e.FirstName)).ToList();
                case Globals.FirstAlert.EmployeeFilters.None:
                default:
                    return new List<CS_Employee>();
            }
        }

        public IList<CS_Employee> ListFilteredEmployeeByName(long divisionId, string name)
        {
            if (divisionId == 0)
                return _employeeRepository.ListAll(e => (e.FirstName.Contains(name) || e.Name.Contains(name) || e.Nickname.Contains(name) || (e.FirstName + " " + e.Name).Contains(name) || (e.FirstName + " " + e.Name + " \"" + e.Nickname + "\"").Contains(name) || (e.CS_Division.Name + " - " + e.Name + ", " + e.FirstName).Contains(name) || (e.CS_Division.Name + " - " + e.Name + ", " + e.FirstName + " \"" + e.Nickname + "\"").Contains(name)) && e.Active == true, new string[] { "CS_Division", "CS_EmployeeCoverage" }).OrderBy(e => e.FirstName).ThenBy(e => e.Name).ToList();
            //return EmployeeDao.Singleton.ListAllFilteredByName(name);
            else
                return _employeeRepository.ListAll(e => e.DivisionID == divisionId && (e.FirstName.Contains(name) || e.Name.Contains(name) || e.Nickname.Contains(name) || (e.FirstName + " " + e.Name).Contains(name) || (e.FirstName + " " + e.Name + " \"" + e.Nickname + "\"").Contains(name) || (e.CS_Division.Name + " - " + e.Name + ", " + e.FirstName).Contains(name) || (e.CS_Division.Name + " - " + e.Name + ", " + e.FirstName + " \"" + e.Nickname + "\"").Contains(name)) && e.Active == true, new string[] { "CS_Division", "CS_EmployeeCoverage" }).OrderBy(e => e.FirstName).ThenBy(e => e.Name).ToList();
            //return EmployeeDao.Singleton.ListFilteredByName(divisionId, name);
        }

        public virtual CS_Employee GetEmployee(int employeeId)
        {
            return _employeeRepository.Get(e => e.ID == employeeId && e.Active == true, "CS_Division", "CS_Division.CS_State");
            //return EmployeeDao.Singleton.Get(employeeId);
        }

        public virtual CS_Employee GetEmployeeForMaintenance(int employeeId)
        {
            return _employeeRepository.Get(e => e.ID == employeeId && e.Active == true, "CS_Resource", "CS_EmployeeOffCallHistory", "CS_EmployeeCoverage", "CS_PhoneNumber", "CS_PhoneNumber.CS_PhoneType", "CS_CallCriteria", "CS_CallCriteria.CS_CallCriteriaValue", "CS_Division", "CS_Division.CS_State");
            //return EmployeeDao.Singleton.Get(employeeId);
        }

        public CS_Employee GetEmployeeByLogin(string username, string domain)
        {
            string login = domain + "\\" + username;
            return _employeeRepository.Get(e => e.UserLogin == login && e.Active);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callLogId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public IList<CS_Employee> ListAllAddedEmployee_CallLogInfo(int callLogId, int type)
        {
            return
                _employeeRepository.ListAll(
                    w =>
                    w.CS_CallLogResource.Any(
                        e => e.EmployeeID == w.ID && w.Active && e.Active && e.CallLogID == callLogId && e.Type == type));
        }

        /// <summary>
        /// Searches for Employee and Criteria info and returns in a related fashion
        /// </summary>
        /// <param name="id">ID of the Employee</param>
        /// <returns>Entity containing Employee Info</returns>
        public CallCriteriaResourceVO GetEmployeeDataForInitialAdvise(int id)
        {
            return BuildEmployeeDataForInitialAdvise(id);
        }

        /// <summary>
        /// Searches for Employee, Criteria and Resource info and returns in a related fashion
        /// </summary>
        /// <param name="id">ID of the Employee</param>
        /// <param name="callLogId">ID of the CallLog</param>
        /// <returns>Entity containing Employee Info</returns>
        public CallCriteriaResourceVO GetEmployeeDataForInitialAdvise(int employeeId, int callLogId)
        {
            CallCriteriaResourceVO returnItem = BuildEmployeeDataForInitialAdvise(employeeId);

            if (null != returnItem)
            {
                CS_CallLogResource resourceData = _callLogResourceRepository.Get(e => e.EmployeeID == employeeId && e.CallLogID == callLogId);

                if (resourceData.InPerson.HasValue)
                    returnItem.AdviseInPerson = resourceData.InPerson.Value;
                
                if (resourceData.Voicemail.HasValue)
                    returnItem.AdviseByVoicemail = resourceData.Voicemail.Value;
                
                if (!string.IsNullOrEmpty(resourceData.Notes))    
                    returnItem.Notes = resourceData.Notes;
            }

            return returnItem;
        }

        private CallCriteriaResourceVO BuildEmployeeDataForInitialAdvise(int employeeId)
        {
            CallCriteriaResourceVO returnItem = new CallCriteriaResourceVO();

            CS_Employee employee = _employeeRepository.Get(e => e.Active && e.ID == employeeId);
            IList<CS_CallCriteriaValue> criterias = _callCriteriaValueRepository.ListAll(e => e.Active && e.CS_CallCriteria.EmployeeID == employeeId);
            

            if (employee != null)
            {
                returnItem.ResourceID = employeeId;
                returnItem.Type = (int)Globals.CallCriteria.EmailVOType.Employee;
                returnItem.Name = employee.FullName;
                returnItem.Division = employee.FullDivisionName;
                returnItem.ContactInfo = employee.ContactInfo;
                returnItem.AdviseInPerson = false;
                returnItem.AdviseByVoicemail = false;
            }

            if (criterias.Count > 0)
            {
                StringBuilder initialAdviseInfo = new StringBuilder();

                initialAdviseInfo.Append(criterias[0].Value);
                for (int i = 1; i < criterias.Count; i++)
                {
                    initialAdviseInfo.Append(string.Format(", {0}", criterias[i].Value));
                }

                returnItem.InitialAdviseInformation = initialAdviseInfo.ToString();
            }

            return returnItem;
        }

        /// <summary>
        /// Returns "" if value parameter is null
        /// </summary>
        /// <param name="value">value parameter</param>
        /// <returns></returns>
        public string IsNull(string value)
        {
            return (null == value) ? "" : value;
        }

        /// <summary>
        /// Saves the Employee Profile Maintenance
        /// </summary>
        /// <param name="employee">Employee Info</param>
        /// <param name="saveCriteria">Call Criteria Info</param>
        /// <param name="saveCriteriaValueList">New Call Criteria Value Info</param>
        /// <param name="offCall">Off Call Info</param>
        /// <param name="coverage">Coverage Info</param>
        /// <param name="username">Username</param>
        /// <param name="isCoverage">Indicates if the employee is in Coverage</param>
        /// <param name="isOffCall">Indicates if the employee is in Off Call</param>
        public void SaveEmployee(CS_Employee employee, CS_EmployeeOffCallHistory offCall, CS_EmployeeCoverage coverage, string username, bool isCoverage, bool isOffCall, List<PhoneNumberVO> additionalPhoneNumbersList)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                DateTime saveDate = DateTime.Now;
                CS_Employee oldEmployee = _employeeRepository.Get(e => e.ID == employee.ID);

                try
                {
                    // Update Employee Table
                    bool hasAddressChanges = false;
                    bool hasPhoneChanges = false;
                    if (null != oldEmployee)
                    {
                        // Saving Employee Info
                        if (IsNull(oldEmployee.Address) != IsNull(employee.Address))
                        {
                            oldEmployee.Address = employee.Address;
                            hasAddressChanges = true;
                        }
                        if (IsNull(oldEmployee.City) != IsNull(employee.City))
                        {
                            oldEmployee.City = employee.City;
                            hasAddressChanges = true;
                        }
                        if (IsNull(oldEmployee.StateProvinceCode) != IsNull(employee.StateProvinceCode))
                        {
                            oldEmployee.StateProvinceCode = employee.StateProvinceCode;
                            hasAddressChanges = true;
                        }
                        if (IsNull(oldEmployee.CountryCode) != IsNull(employee.CountryCode))
                        {
                            oldEmployee.CountryCode = employee.CountryCode;
                            hasAddressChanges = true;
                        }
                        if (IsNull(oldEmployee.Address2) != IsNull(employee.Address2))
                        {
                            oldEmployee.Address2 = employee.Address2;
                            hasAddressChanges = true;
                        }
                        if (IsNull(oldEmployee.PostalCode) != IsNull(employee.PostalCode))
                        {
                            oldEmployee.PostalCode = employee.PostalCode;
                            hasAddressChanges = true;
                        }
                        if (IsNull(oldEmployee.DayAreaCode) != IsNull(employee.DayAreaCode))
                        {
                            oldEmployee.DayAreaCode = employee.DayAreaCode;
                            hasPhoneChanges = true;
                        }
                        if (IsNull(oldEmployee.DayPhone) != IsNull(employee.DayPhone))
                        {
                            oldEmployee.DayPhone = employee.DayPhone;
                            hasPhoneChanges = true;
                        }
                        if (IsNull(oldEmployee.HomeAreaCode) != IsNull(employee.HomeAreaCode))
                        {
                            oldEmployee.HomeAreaCode = employee.HomeAreaCode;
                            hasPhoneChanges = true;
                        }
                        if (IsNull(oldEmployee.HomePhone) != IsNull(employee.HomePhone))
                        {
                            oldEmployee.HomePhone = employee.HomePhone;
                            hasPhoneChanges = true;
                        }
                        if (IsNull(oldEmployee.FaxAreaCode) != IsNull(employee.FaxAreaCode))
                        {
                            oldEmployee.FaxAreaCode = employee.FaxAreaCode;
                            hasPhoneChanges = true;
                        }
                        if (IsNull(oldEmployee.FaxPhone) != IsNull(employee.FaxPhone))
                        {
                            oldEmployee.FaxPhone = employee.FaxPhone;
                            hasPhoneChanges = true;
                        }
                        if (IsNull(oldEmployee.MobileAreaCode) != IsNull(employee.MobileAreaCode))
                        {
                            oldEmployee.MobileAreaCode = employee.MobileAreaCode;
                            hasPhoneChanges = true;
                        }
                        if (IsNull(oldEmployee.MobilePhone) != IsNull(employee.MobilePhone))
                        {
                            oldEmployee.MobilePhone = employee.MobilePhone;
                            hasPhoneChanges = true;
                        }
                        if (IsNull(oldEmployee.OtherPhoneAreaCode) != IsNull(employee.OtherPhoneAreaCode))
                        {
                            oldEmployee.OtherPhoneAreaCode = employee.OtherPhoneAreaCode;
                            hasPhoneChanges = true;
                        }
                        if (IsNull(oldEmployee.OtherPhone) != IsNull(employee.OtherPhone))
                        {
                            oldEmployee.OtherPhone = employee.OtherPhone;
                            hasPhoneChanges = true;
                        }

                        if (hasAddressChanges)
                            oldEmployee.HasAddressChanges = hasAddressChanges;

                        if (hasPhoneChanges)
                            oldEmployee.HasPhoneChanges = hasPhoneChanges;

                        oldEmployee.ModifiedBy = username;
                        oldEmployee.ModificationDate = saveDate;
                        //employee.ModificationID = null;
                        oldEmployee.IsKeyPerson = employee.IsKeyPerson;
                        oldEmployee.IsDentonPersonal = employee.IsDentonPersonal;
                        CS_Employee employeeToUpdate = new CS_Employee();
                        employeeToUpdate = oldEmployee;

                        _employeeRepository.Update(employeeToUpdate);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Write(string.Format("An error occured while trying to updating Employee information.\n{0}\n{1}", ex, ex.InnerException));
                    throw new Exception("Error while updating Employee information", ex);
                }

                try
                {
                    // Send Email to Pam And Curry
                    if (null != oldEmployee)
                        if (oldEmployee.HasAddressChanges)
                            SendNotificationForAddressChange(oldEmployee);
                }
                catch (Exception ex)
                {
                    Logger.Write(string.Format("An error occured while trying to Send the email update for Address Change.\n{0}\n{1}", ex, ex.InnerException));
                    throw new Exception("Error while notifying Address changes", ex);
                }

                try
                {
                    //Send Email to Pam
                    if (null != oldEmployee)
                        if (oldEmployee.HasPhoneChanges)
                            SendNotificationForContactPhoneChange(oldEmployee);
                }
                catch (Exception ex)
                {
                    Logger.Write(string.Format("An error occured while trying to Send the email update for Phone Change.\n{0}\n{1}", ex, ex.InnerException));
                    throw new Exception("Error while notifying Phone changes", ex);
                }

                try
                {
                    // Update Off Call
                    if (offCall != null)
                    {
                        CS_EmployeeOffCallHistory oldOffCall = _employeeOffCallRepository.Get(e => e.EmployeeID == offCall.EmployeeID && e.Active);
                        if (oldOffCall == null)
                        {
                            offCall.Active = true;
                            offCall.CreatedBy = username;
                            offCall.CreationDate = saveDate;
                            //offCall.CreationID = null;
                            offCall.ModifiedBy = username;
                            offCall.ModificationDate = saveDate;
                            //offCall.ModificationID = null;

                            if (isOffCall)
                            {
                                _employeeOffCallRepository.Add(offCall);
                                _callLogModel.GenerateOffCalCallLog(offCall);
                            }
                        }
                        else
                        {
                            oldOffCall.EmployeeID = offCall.EmployeeID;
                            oldOffCall.ProxyEmployeeID = offCall.ProxyEmployeeID;
                            oldOffCall.OffCallStartDate = offCall.OffCallStartDate;
                            oldOffCall.OffCallEndDate = offCall.OffCallEndDate;
                            oldOffCall.OffCallReturnTime = offCall.OffCallReturnTime;
                            oldOffCall.Active = isOffCall;
                            oldOffCall.ModifiedBy = username;
                            oldOffCall.ModificationDate = saveDate;
                            //oldOffCall.ModificationID = null;

                            _employeeOffCallRepository.Update(oldOffCall);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Write(string.Format("An error occured while trying to Update Off Call Information.\n{0}\n{1}", ex, ex.InnerException));
                    throw new Exception("Error while updating Off Call information", ex);
                }

                try
                {
                    // Update Coverage
                    if (coverage != null)
                    {


                        CS_EmployeeCoverage oldCoverage = _employeeCoverageRepository.Get(e => e.EmployeeID == coverage.EmployeeID && e.Active);
                        if (oldCoverage == null)
                        {
                            coverage.Active = true;
                            coverage.CreatedBy = username;
                            coverage.CreationDate = saveDate;
                            //coverage.CreationID = null;
                            coverage.ModifiedBy = username;
                            coverage.ModificationDate = saveDate;
                            //coverage.ModificationID = null;

                            if (isCoverage)
                            {
                                int? jobId = this.GetJobIdFromResource(coverage.EmployeeID);

                                if (jobId.HasValue)
                                {
                                    JobModel jobModel = new JobModel(_unitOfWork);
                                    jobModel.AddDivisionToJob(coverage.DivisionID, jobId.Value, username);
                                }

                                _employeeCoverageRepository.Add(coverage);
                                _callLogModel.GenerateEmployeeCoverageCalCallLog(coverage);
                            }
                        }
                        else
                        {
                            oldCoverage.EmployeeID = coverage.EmployeeID;
                            oldCoverage.DivisionID = coverage.DivisionID;
                            oldCoverage.Duration = coverage.Duration;
                            oldCoverage.CoverageStartDate = coverage.CoverageStartDate;
                            oldCoverage.CoverageEndDate = coverage.CoverageEndDate;
                            oldCoverage.Active = isCoverage;
                            oldCoverage.ModifiedBy = username;
                            offCall.ModificationDate = saveDate;
                            //oldCoverage.ModificationID = null;

                            _employeeCoverageRepository.Update(oldCoverage);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Write(string.Format("An error occured while trying to Update Off Call Information.\n{0}\n{1}", ex, ex.InnerException));
                    throw new Exception("Error while updating Off Call information", ex);
                }

                try
                {
                    if (null != oldEmployee)
                    {
                        List<CS_PhoneNumber> oldPhoneList = ListAllPhonesByEmployee(oldEmployee.ID);
                        List<CS_PhoneNumber> newPhoneList = GetListFromPhoneVO(additionalPhoneNumbersList);

                        List<CS_PhoneNumber> removedPhoneList = oldPhoneList.Where(e => !newPhoneList.Any(f => f.ID == e.ID)).ToList();
                        List<CS_PhoneNumber> addedPhoneList = newPhoneList.Where(e => e.ID == 0).ToList();

                        for (int i = 0; i < removedPhoneList.Count; i++)
                        {
                            removedPhoneList[i].ModifiedBy = username;
                            removedPhoneList[i].ModificationDate = saveDate;
                            removedPhoneList[i].Active = false;
                        }

                        for (int i = 0; i < addedPhoneList.Count; i++)
                        {
                            addedPhoneList[i].EmployeeID = oldEmployee.ID;
                            addedPhoneList[i].CreatedBy = username;
                            addedPhoneList[i].CreationDate = saveDate;
                            //addedPhoneList[i].CreationID = 
                            addedPhoneList[i].ModifiedBy = username;
                            addedPhoneList[i].ModificationDate = saveDate;
                            //addedPhoneList[i].ModificationID = 
                            addedPhoneList[i].Active = true;
                        }

                        _phoneNumberRepository.UpdateList(removedPhoneList);
                        _phoneNumberRepository.AddList(addedPhoneList);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Write(string.Format("An error occured while trying to Update Additional Contact Information.\n{0}\n{1}", ex, ex.InnerException));
                    throw new Exception("Error while updating Additional Contact Information", ex);
                }

                scope.Complete();
            }
        }

        /// <summary>
        /// Create a stub CS_PhoneNumber from a PhoneNumberVO, for saving
        /// </summary>
        /// <param name="additionalPhoneNumbersList">List of PhoneNumberVO</param>
        /// <returns>List of CS_PhoneNumber</returns>
        private List<CS_PhoneNumber> GetListFromPhoneVO(List<PhoneNumberVO> additionalPhoneNumbersList)
        {
            List<CS_PhoneNumber> returnList = new List<CS_PhoneNumber>();

            if (additionalPhoneNumbersList != null)
            {
                for (int i = 0; i < additionalPhoneNumbersList.Count; i++)
                {
                    PhoneNumberVO vo = additionalPhoneNumbersList[i];

                    CS_PhoneNumber item = new CS_PhoneNumber()
                    {
                        ID = vo.ID,
                        Number = vo.Number,
                        PhoneTypeID = vo.TypeID
                    };

                    returnList.Add(item);
                }
            }

            return returnList;
        }

        /// <summary>
        /// Retrieves a list of the Employee Additional Phone Numbers
        /// </summary>
        /// <param name="employeeId">ID of the Employee</param>
        /// <returns>List with CS_PhoneNumber entities representing the data</returns>
        public List<CS_PhoneNumber> ListAllPhonesByEmployee(int employeeId)
        {
            return _phoneNumberRepository.ListAll(e => e.EmployeeID == employeeId && e.Active, "CS_PhoneType").ToList();
        }

        /// <summary>
        /// Retrieves a list of the Employee Additional Phone Numbers
        /// </summary>
        /// <param name="employeeId">ID of the Employee</param>
        /// <returns>List with CS_PhoneNumber entities representing the data</returns>
        public List<EmployeePhoneListingVO> ListFilteredEmployeePhoneListingVO(Globals.Phone.PhoneFilterType filterType, string value)
        {
            string[] values = value.Split(',');
            IList<CS_Employee> employeeList = new List<CS_Employee>();
            List<EmployeePhoneListingVO> returnList = new List<EmployeePhoneListingVO>();

            switch (filterType)
            {
                case Globals.Phone.PhoneFilterType.Division:
                    employeeList = _employeeRepository.ListAll(e => values.Any(f => e.CS_Division.Name.Contains(f)) && e.Active, "CS_PhoneNumber", "CS_PhoneNumber.CS_PhoneType").ToList();
                    break;
                case Globals.Phone.PhoneFilterType.Customer:
                    employeeList = _employeeRepository.ListAll(e => values.Any(f => e.CS_Customer.Any(c =>
                        (
                            c.Name.Trim()
                            + (string.IsNullOrEmpty(c.Country) ? "" : " " + c.Country.Trim())
                            + (string.IsNullOrEmpty(c.Attn) ? "" : " " + c.Attn.Trim())
                            + (string.IsNullOrEmpty(c.CustomerNumber) ? "" : " " + c.CustomerNumber.Trim())
                        ).Contains(f))) && e.Active, "CS_PhoneNumber", "CS_PhoneNumber.CS_PhoneType").ToList();
                    break;
                case Globals.Phone.PhoneFilterType.Employee:
                    employeeList = _employeeRepository.ListAll(e => values.Any(f =>
                            (
                                (string.IsNullOrEmpty(e.Name) ? "" : e.Name.Trim())
                                + (string.IsNullOrEmpty(e.FirstName) ? "" : " " + e.FirstName.Trim())
                                + (string.IsNullOrEmpty(e.Nickname) ? "" : " " + e.Nickname.Trim())
                            ).Contains(f)) && e.Active, "CS_PhoneNumber", "CS_PhoneNumber.CS_PhoneType").ToList();
                    break;
                default:
                    employeeList = _employeeRepository.ListAll(e => e.Active, "CS_PhoneNumber", "CS_PhoneNumber.CS_PhoneType").ToList();
                    break;
            }

            if (employeeList.Count > 0)
            {
                for (int i = 0; i < employeeList.Count; i++)
                {
                    CS_Employee employee = employeeList[i];
                    returnList.Add(new EmployeePhoneListingVO(employee));
                }
            }

            return returnList;
        }

        private string ReplaceAddtionalChars(string str)
        {
            if (str == null)
                return string.Empty;

            return str.Replace("-", "").Replace(" ", "");
        }

        /// <summary>
        /// Retrieves a list of the Employee Additional Phone Numbers
        /// </summary>
        /// <param name="employeeId">ID of the Employee</param>
        /// <returns>List with CS_PhoneNumber entities representing the data</returns>
        public List<EmployeePhoneVO> ListFilteredEmployeePhoneVO(Globals.Phone.PhoneFilterType filterType, string value)
        {
            string[] values = value.Split(',');
            IList<CS_Employee> employeeList = new List<CS_Employee>();
            List<EmployeePhoneVO> returnList = new List<EmployeePhoneVO>();

            switch (filterType)
            {
                case Globals.Phone.PhoneFilterType.Division:
                    employeeList = _employeeRepository.ListAll(e => values.Any(f => e.CS_Division.Name.Contains(f)) && e.Active, "CS_PhoneNumber", "CS_PhoneNumber.CS_PhoneType").ToList();
                    break;
                case Globals.Phone.PhoneFilterType.Customer:
                    employeeList = _employeeRepository.ListAll(e => values.Any(f => e.CS_Customer.Any(c =>
                        (
                            c.Name.Trim()
                            + (string.IsNullOrEmpty(c.Country) ? "" : " " + c.Country.Trim())
                            + (string.IsNullOrEmpty(c.Attn) ? "" : " " + c.Attn.Trim())
                            + (string.IsNullOrEmpty(c.CustomerNumber) ? "" : " " + c.CustomerNumber.Trim())
                        ).Contains(f))) && e.Active, "CS_PhoneNumber", "CS_PhoneNumber.CS_PhoneType").ToList();
                    break;
                case Globals.Phone.PhoneFilterType.Employee:
                    employeeList = _employeeRepository.ListAll(e => values.Any(f =>
                            (
                                (string.IsNullOrEmpty(e.Name) ? "" : e.Name.Trim())
                                + (string.IsNullOrEmpty(e.FirstName) ? "" : " " + e.FirstName.Trim())
                                + (string.IsNullOrEmpty(e.Nickname) ? "" : " " + e.Nickname.Trim())
                            ).Contains(f)) && e.Active, "CS_PhoneNumber", "CS_PhoneNumber.CS_PhoneType").ToList();
                    break;
                default:
                    employeeList = _employeeRepository.ListAll(e => e.Active, "CS_PhoneNumber", "CS_PhoneNumber.CS_PhoneType").ToList();
                    break;
            }

            if (employeeList.Count > 0)
            {
                for (int i = 0; i < employeeList.Count; i++)
                {
                    CS_Employee employee = employeeList[i];
                    List<CS_PhoneNumber> phones = employee.CS_PhoneNumber2.Where(w => w.Active).ToList();

                    if (!string.IsNullOrEmpty(employee.DayAreaCode) && !string.IsNullOrEmpty(employee.DayPhone))
                    {
                        returnList.Add(new EmployeePhoneVO() { EmployeeName = employee.FullName, PhoneNumber = string.Format("({0}) {1}", employee.DayAreaCode, string.Format("{0}-{1}", ReplaceAddtionalChars(employee.DayPhone).Substring(0, 3), ReplaceAddtionalChars(employee.DayPhone).Substring(3))), PhoneType = "Day" });
                    }

                    if (!string.IsNullOrEmpty(employee.FaxAreaCode) && !string.IsNullOrEmpty(employee.FaxPhone))
                    {
                        returnList.Add(new EmployeePhoneVO() { EmployeeName = employee.FullName, PhoneNumber = string.Format("({0}) {1}", employee.FaxAreaCode, string.Format("{0}-{1}", ReplaceAddtionalChars(employee.FaxPhone).Substring(0, 3), ReplaceAddtionalChars(employee.FaxPhone).Substring(3))), PhoneType = "Fax" });
                    }

                    if (!string.IsNullOrEmpty(employee.HomeAreaCode) && !string.IsNullOrEmpty(employee.HomePhone))
                    {
                        returnList.Add(new EmployeePhoneVO() { EmployeeName = employee.FullName, PhoneNumber = string.Format("({0}) {1}", employee.HomeAreaCode, string.Format("{0}-{1}", ReplaceAddtionalChars(employee.HomePhone).Substring(0, 3), ReplaceAddtionalChars(employee.HomePhone).Substring(3))), PhoneType = "Home" });
                    }

                    if (!string.IsNullOrEmpty(employee.MobileAreaCode) && !string.IsNullOrEmpty(employee.MobilePhone))
                    {
                        returnList.Add(new EmployeePhoneVO() { EmployeeName = employee.FullName, PhoneNumber = string.Format("({0}) {1}", employee.MobileAreaCode, string.Format("{0}-{1}", ReplaceAddtionalChars(employee.MobilePhone).Substring(0, 3), ReplaceAddtionalChars(employee.MobilePhone).Substring(3))), PhoneType = "Cell" });
                    }

                    if (!string.IsNullOrEmpty(employee.OtherPhoneAreaCode) && !string.IsNullOrEmpty(employee.OtherPhone))
                    {
                        returnList.Add(new EmployeePhoneVO() { EmployeeName = employee.FullName, PhoneNumber = string.Format("({0}) {1}", employee.OtherPhoneAreaCode, string.Format("{0}-{1}", ReplaceAddtionalChars(employee.OtherPhone).Substring(0, 3), ReplaceAddtionalChars(employee.OtherPhone).Substring(3))), PhoneType = "Other" });
                    }

                    for (int j = 0; j < phones.Count; j++)
                    {
                        CS_PhoneNumber phone = phones[j];
                        returnList.Add(new EmployeePhoneVO(phone));
                    }
                }
            }

            return returnList.OrderBy(e => e.EmployeeName).ThenBy(e => e.PhoneType).ThenBy(e => e.PhoneNumber).ToList();
        }




        public virtual CS_EmployeeOffCallHistory GetActiveOffCallEmployeeById(int employeeId)
        {
            return _employeeOffCallRepository.Get(e => e.Active && e.EmployeeID == employeeId);
        }

        public void SendNotificationForContactPhoneChange(CS_Employee employee)
        {
            if (null != employee)
            {
                IList<CS_Email> emails = new List<CS_Email>();

                //Body
                string body = GenerateEmailBodyForContactPhoneChange(employee);

                //List receipts
                string receipts = _settingsModel.GetContactChangeNotificationEmails();

                //Subject
                string subject = GenerateEmailSubjectForContactPhoneChange(employee);

                if (!string.IsNullOrEmpty(receipts))
                    emails = _emailModel.SaveEmailList(receipts, subject, body, "System", Globals.Security.SystemEmployeeID);
            }
        }

        public virtual IList<CS_EmployeeOffCallHistory> ListEmployeeOffCallHistory(int employeeId)
        {
            return _employeeOffCallRepository.ListAll(e => !e.Active && e.EmployeeID == employeeId, "CS_Employee_Proxy");
        }

        public virtual IList<CS_EmployeeOffCallHistory> ListEmployeeOffCallHistoryNotification(DateTime currentDate)
        {
            DateTime cDate = currentDate.Date;
            TimeSpan cTime = currentDate.TimeOfDay;
            return _employeeOffCallRepository.ListAll(e => e.Active && ((e.OffCallEndDate < cDate) ? true : ((e.OffCallEndDate == cDate) ? e.OffCallReturnTime <= cTime : false)), "CS_Employee");
        }

        public virtual CS_EmployeeCoverage GetEmployeeCoverageById(int employeeId)
        {
            return _employeeCoverageRepository.Get(e => e.Active && e.EmployeeID == employeeId);
        }

        public virtual IList<CS_EmployeeCoverage> ListEmployeeCoverageHistory(int employeeId)
        {
            return _employeeCoverageRepository.ListAll(e => !e.Active && e.EmployeeID == employeeId);
        }

        /// <summary>
        /// Verify if resource is assigned to a job or not
        /// </summary>
        /// <param name="employeeId">Employee id</param>
        /// <returns>bool</returns>
        public bool VerifyIfResourceIsAssignedToJob(int employeeId)
        {
            CS_Resource resource = _resourceRepository.Get(w => w.Active && w.EmployeeID == employeeId);

            if (resource != null)
                return true;

            return false;
        }

        /// <summary>
        /// Get the job id if a resource is assigned to a job
        /// </summary>
        public int? GetJobIdFromResource(int employeeId)
        {
            CS_Resource resource = _resourceRepository.Get(w => w.Active && w.EmployeeID == employeeId);

            if (resource != null)
                return resource.JobID;

            return null;
        }

        /// <summary>
        /// Returns a list of CS_PhoneType entities that are Active in the DB
        /// </summary>
        public List<CS_PhoneType> LoadPhoneTypes()
        {
            return _phoneTypeRepository.ListAll(e => e.Active).ToList();
        }

        /// <summary>
        /// List all Managers Location
        /// </summary>
        /// <returns>List<CS_View_ManagersLocation></returns>
        public List<CS_View_ManagersLocation> ListAllManagersLocation()
        {
            return _managersLocationRepository.ListAll().ToList();
        }

        /// <summary>
        /// List all filtered managers location
        /// </summary>
        /// <param name="name">employee</param>
        /// <param name="callType">calltype</param>
        /// <param name="jobId">jobid</param>
        /// <returns>List<CS_View_ManagersLocation></returns>
        public List<CS_View_ManagersLocation> ListFilteredManagersLocation(string name, int? callType, int? jobId)
        {
            return _managersLocationRepository.ListAll(w =>
                                        ((name == "") || (w.EmployeeName + ", " + w.EmployeeFirstName + (" \"" + w.EmployeeNickName != null ? w.EmployeeNickName : string.Empty + "\"")).Contains(name)) &&
                                        ((!callType.HasValue) || w.LastCallTypeID == callType) &&
                                        ((!jobId.HasValue) || w.JobID == jobId)).ToList();

        }

        #region [ Email Notification ]
        public string GenerateEmailBodyForContactPhoneChange(CS_Employee employee)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Message from OneSource,");
            sb.AppendLine(string.Format("Employee {0}, has an address change.", employee.FullName));
            sb.AppendLine(string.Format("The new Day Phone number is {0} - {1}", employee.DayAreaCode, employee.DayPhone));
            sb.AppendLine(string.Format("The new Fax Phone number is {0} - {1}", employee.FaxAreaCode, employee.FaxPhone));
            sb.AppendLine(string.Format("The new Home Phone number is {0} - {1}", employee.HomeAreaCode, employee.HomePhone));
            sb.AppendLine(string.Format("The new Mobile Phone number is {0} - {1}", employee.MobileAreaCode, employee.MobilePhone));
            sb.AppendLine(string.Format("The new Other Phone number is {0} - {1}", employee.OtherPhoneAreaCode, employee.OtherPhone));
            sb.AppendLine("Please update your records in Ivantage.");
            sb.AppendLine("Thank you");

            return sb.ToString();
        }

        public void SendNotificationForAddressChange(CS_Employee employeeChanged)
        {
            if (null != employeeChanged)
            {
                IList<CS_Email> emails = new List<CS_Email>();

                //Body
                string body = GenerateEmailBodyForAddressChange(employeeChanged);

                //List receipts
                string receipts = _settingsModel.GetAddressChangeNotificationEmails();

                //Subject
                string subject = GenerateEmailSubjectForAddressChange(employeeChanged);

                if (!string.IsNullOrEmpty(receipts))
                    emails = _emailModel.SaveEmailList(receipts, subject, body, "System", Globals.Security.SystemEmployeeID);
            }
        }

        private string GenerateEmailBodyForAddressChange(CS_Employee employee)
        {
            StringBuilder _addressChangeEmail = new StringBuilder();

            _addressChangeEmail.AppendLine("Message from OneSource,");
            _addressChangeEmail.AppendLine(string.Format("Employee {0}, has an address change.", employee.FullName));
            _addressChangeEmail.AppendLine(string.Format("The new address information is {0}, {1}, {2}, {3}.", employee.FullAddress, employee.City, employee.StateProvinceCode, employee.PostalCode));
            _addressChangeEmail.AppendLine("Please update your records in Ivantage and/or Fidelity.");
            _addressChangeEmail.AppendLine("Thank  you.");

            return _addressChangeEmail.ToString();
        }

        private string GenerateEmailSubjectForContactPhoneChange(CS_Employee employee)
        {
            return string.Format("Employee contact number change for {0}", employee.FullName);
        }

        private string GenerateEmailSubjectForAddressChange(CS_Employee employee)
        {
            return string.Format("Employee address change, for {0}", employee.FullName);
        }

        #endregion

        #endregion

        #region [ Service Methods ]

        /// <summary>
        /// Service Method that will start the import process of IVantage data
        /// </summary>
        /// <returns>True if process ran OK, False if there was an error</returns>
        public bool ServiceWork()
        {
            SettingsModel settings = new SettingsModel();
            EmailModel emailModel = new EmailModel();

            try
            {
                UpdateDivisions();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to import the Division information!\n{0}\n{1}", ex.InnerException, ex.StackTrace));

                StringBuilder mailError = new StringBuilder();
                mailError.AppendLine(string.Format("An Error Ocurred when importing the Division Information!\n{0}\n{1}", ex.InnerException, ex.StackTrace));
                //MailUtility.SendMail(settings.GetITEmailOnError(), "", "", mailError.ToString(), "IVantage Import Service - Error occured on Division Information", false, null);
                emailModel.SaveEmailList(settings.GetITEmailOnError(), "IVantage Import Service - Error occured on Division Information", mailError.ToString(), "System", (int)Globals.Security.SystemEmployeeID);

                return false;
            }

            try
            {
                UpdateEmployees();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to import the Employee information!\n{0}\n{1}", ex.InnerException, ex.StackTrace));

                StringBuilder mailError = new StringBuilder();
                mailError.AppendLine(string.Format("An Error Ocurred when importing the Employee Information!\n{0}\n{1}", ex.InnerException, ex.StackTrace));
                //MailUtility.SendMail(settings.GetITEmailOnError(), "", "", mailError.ToString(), "IVantage Import Service - Error occured on Employee Information", false, null);
                emailModel.SaveEmailList(settings.GetITEmailOnError(), "IVantage Import Service - Error occured on Employee Information", mailError.ToString(), "System", (int)Globals.Security.SystemEmployeeID);

                return false;
            }

            try
            {
                UpdateEmergencyContacts();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to import the Emergency Contact information!\n{0}\n{1}", ex.InnerException, ex.StackTrace));

                StringBuilder mailError = new StringBuilder();
                mailError.AppendLine(string.Format("An Error Ocurred when importing the Emergency Contact Information!\n{0}\n{1}", ex.InnerException, ex.StackTrace));
                //MailUtility.SendMail(settings.GetITEmailOnError(), "", "", mailError.ToString(), "IVantage Import Service - Error occured on Emergency Contact Information", false, null);
                emailModel.SaveEmailList(settings.GetITEmailOnError(), "IVantage Import Service - Error occured on Emergency Contact Information", mailError.ToString(), "System", (int)Globals.Security.SystemEmployeeID);

                return false;
            }

            return true;
        }

        /// <summary>
        /// Executes Update process for Division Information
        /// </summary>
        /// <returns>True if process ran OK, False if there was an error</returns>
        private bool UpdateDivisions()
        {
            try
            {
                CS_DivisionRepository divisionRepository = new CS_DivisionRepository(_divisionRepository, _divisionRepository.UnitOfWork);
                // Bulk Copying Divisions from IVantage
                divisionRepository.BulkCopyAllDivisions(IVantageIntegration.Singleton.ListAllDivisions());
                //DivisionDao.Singleton.BulkCopyAllDivisions(IVantageIntegration.Singleton.ListAllDivisions());

                // Run import tool
                divisionRepository.UpdateFromIntegration();
                //DivisionDao.Singleton.UpdateFromIntegration();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Executes Update process for Employee Information
        /// </summary>
        /// <returns>True if process ran OK, False if there was an error</returns>
        private bool UpdateEmployees()
        {
            try
            {
                CS_EmployeeRepository employeeRepository = new CS_EmployeeRepository(_employeeRepository, _employeeRepository.UnitOfWork);
                // Bulk Copying Employees from IVantage
                employeeRepository.BulkCopyAllEmployees(IVantageIntegration.Singleton.ListAllEmployees());
                //EmployeeDao.Singleton.BulkCopyAllEmployees(IVantageIntegration.Singleton.ListAllEmployees());

                // Run Import tool
                employeeRepository.UpdateFromIntegration();
                //EmployeeDao.Singleton.UpdateFromIntegration();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Executes Update process for Emergency Contact Information
        /// </summary>
        /// <returns>True if process ran OK, False if there was an error</returns>
        private bool UpdateEmergencyContacts()
        {
            try
            {
                CS_EmployeeEmergencyContactRepository employeeEmergencyContactRepository = new CS_EmployeeEmergencyContactRepository(_employeeEmergencyContactRepository, _employeeEmergencyContactRepository.UnitOfWork);
                // Bulk Copying Employees from IVantage
                employeeEmergencyContactRepository.BulkCopyAllEmergencyContacts(IVantageIntegration.Singleton.ListAllEmergencyContacts());
                //EmergencyContactDao.Singleton.BulkCopyAllEmergencyContacts(IVantageIntegration.Singleton.ListAllEmergencyContacts());

                // Run Import tool
                employeeEmergencyContactRepository.UpdateFromIntegration();
                //EmergencyContactDao.Singleton.UpdateFromIntegration();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region [ IDisposable Implementation ]

        public void Dispose()
        {
            _employeeRepository = null;
            _employeeOffCallRepository = null;
            _employeeCoverageRepository = null;
            _employeeInfoRepository = null;
            _employeeCallLogInfoRepository = null;
            _divisionRepository = null;
            _employeeEmergencyContactRepository = null;
            _customerInfoRepository = null;
            _contactRepository = null;
            _callCriteriaRepository = null;
            _callCriteriaValueRepository = null;
            _resourceRepository = null;
            _phoneTypeRepository = null;
            _phoneNumberRepository = null;

            _callCriteriaModel.Dispose();
            _settingsModel.Dispose();
            _emailModel.Dispose();
            _callLogModel.Dispose();

            _unitOfWork.Dispose();
            _unitOfWork = null;
        }

        #endregion
    }
}
