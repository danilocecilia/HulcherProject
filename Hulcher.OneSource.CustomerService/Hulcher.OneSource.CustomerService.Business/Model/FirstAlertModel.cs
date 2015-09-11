using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.DataContext;
using System.Transactions;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.DataContext.VO;


namespace Hulcher.OneSource.CustomerService.Business.Model
{
    /// <summary>
    /// First Alert Class
    /// </summary>
    public class FirstAlertModel
    {
        #region [ Attributes ]

        static Object locked = new Object();

        /// <summary>
        /// Repository class for CS_FirstAlert
        /// </summary>
        private IRepository<CS_FirstAlert> _firstAlertRepository;

        /// <summary>
        /// Repository class for CS_CallLog
        /// </summary>
        private IRepository<CS_CallLog> _callLogRepository;

        /// <summary>
        /// Repository class for CS_FirstAlertPerson
        /// </summary>
        private IRepository<CS_FirstAlertPerson> _firstAlertPersonRepository;

        /// <summary>
        /// Repository class for CS_FirstAlertVehicle
        /// </summary>
        private IRepository<CS_FirstAlertVehicle> _firstAlertVehicleRepository;

        /// <summary>
        /// Repository class for CS_FirstAlertDivision
        /// </summary>
        private IRepository<CS_FirstAlertDivision> _firstAlertDivisionRepository;

        /// <summary>
        /// Repository class for CS_CallLogResource
        /// </summary>
        IRepository<CS_CallLogResource> _callLogResourceRepository;

        /// <summary>
        /// Repository class for CS_CallLogCallCriteriaEmail
        /// </summary>
        IRepository<CS_CallLogCallCriteriaEmail> _callLogCallCriteriaEmailRepository;

        /// <summary>
        /// Repository class for CS_FirstAlertType
        /// </summary>
        IRepository<CS_FirstAlertType> _firstAlertTypeRepository;

        /// <summary>
        /// Repository class for CS_FirstAlertFirstAlertType
        /// </summary>
        IRepository<CS_FirstAlertFirstAlertType> _firstAlertFirstAlertTypeRepository;

        /// <summary>
        /// Repository Class for CS_FirstAlertContactPersonal
        /// </summary>
        IRepository<CS_FirstAlertContactPersonal> _firstAlertContactPersonalRepository;

        /// <summary>
        /// Repository class for CS_View_FirstAlertReport
        /// </summary>
        IRepository<CS_View_FirstAlertReport> _firstAlertReportRepository;

        /// <summary>
        /// Repository class for CS_View_FirstAlertReportHulcherVehicles
        /// </summary>
        IRepository<CS_View_FirstAlertReportHulcherVehicles> _firstAlertReportHulcherVehicleRepository;

        /// <summary>
        /// Repository class for CS_View_FirstAlertReportHulcherVehicles
        /// </summary>
        IRepository<CS_View_FirstAlertReportOtherVehicle> _firstAlertReportOtherVehicleRepository;

        /// <summary>
        /// Repository Class for CS_View_FirstAlertReportContactPersonal
        /// </summary>
        IRepository<CS_View_FirstAlertReportContactPersonal> _firstAlertReportContactPersonalViewRepository;

        /// <summary>
        /// Instance of settings model
        /// </summary>
        SettingsModel _settingsModel;

        /// <summary>
        /// Instance of CallCriteria model
        /// </summary>
        CallCriteriaModel _callCriteriaModel;

        /// <summary>
        /// Unit of Work used to access the database/in-memory context
        /// </summary>
        private IUnitOfWork _unitOfWork;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class constructor
        /// </summary>
        public FirstAlertModel()
        {
            _unitOfWork = new EFUnitOfWork();
            InstanceObjects();
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of Work instance (used for unit tests)</param>
        public FirstAlertModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            InstanceObjects();
        }

        #endregion

        #region [ Methods ]

        #region [ Common ]

        /// <summary>
        /// Metod that intances all attribute objects
        /// </summary>
        private void InstanceObjects()
        {
            _firstAlertRepository = new EFRepository<CS_FirstAlert> { UnitOfWork = _unitOfWork };
            _firstAlertPersonRepository = new EFRepository<CS_FirstAlertPerson> { UnitOfWork = _unitOfWork };
            _firstAlertVehicleRepository = new EFRepository<CS_FirstAlertVehicle> { UnitOfWork = _unitOfWork };
            _firstAlertDivisionRepository = new EFRepository<CS_FirstAlertDivision> { UnitOfWork = _unitOfWork };
            _callLogRepository = new EFRepository<CS_CallLog> { UnitOfWork = _unitOfWork };
            _settingsModel = new SettingsModel(_unitOfWork);
            _callCriteriaModel = new CallCriteriaModel(_unitOfWork);
            _callLogResourceRepository = new EFRepository<CS_CallLogResource> { UnitOfWork = _unitOfWork };
            _callLogCallCriteriaEmailRepository = new EFRepository<CS_CallLogCallCriteriaEmail> { UnitOfWork = _unitOfWork };
            _firstAlertTypeRepository = new EFRepository<CS_FirstAlertType> { UnitOfWork = _unitOfWork };
            _firstAlertFirstAlertTypeRepository = new EFRepository<CS_FirstAlertFirstAlertType> { UnitOfWork = _unitOfWork };
            _firstAlertContactPersonalRepository = new EFRepository<CS_FirstAlertContactPersonal> { UnitOfWork = _unitOfWork };
            _firstAlertReportRepository = new EFRepository<CS_View_FirstAlertReport> { UnitOfWork = _unitOfWork };
            _firstAlertReportHulcherVehicleRepository = new EFRepository<CS_View_FirstAlertReportHulcherVehicles> { UnitOfWork = _unitOfWork };
            _firstAlertReportOtherVehicleRepository = new EFRepository<CS_View_FirstAlertReportOtherVehicle> { UnitOfWork = _unitOfWork };
            _firstAlertReportContactPersonalViewRepository = new EFRepository<CS_View_FirstAlertReportContactPersonal> { UnitOfWork = _unitOfWork };
        }

        #endregion

        #region [ Listing ]

        /// <summary>
        /// List all first alert types
        /// </summary>
        /// <returns></returns>
        public IList<CS_FirstAlertType> ListFirstAlertType()
        {
            return _firstAlertTypeRepository.ListAll(e => e.Active);
        }

        /// <summary>
        /// Return list of first alert
        /// </summary>"
        /// <returns>list first alert</returns> 
        public IList<CS_FirstAlert> ListAllFirstAlert()
        {
            IList<CS_FirstAlert> lstFirstAlert = _firstAlertRepository.ListAll(w => w.Active, 
                "CS_FirstAlertFirstAlertType", "CS_FirstAlertFirstAlertType.CS_FirstAlertType",
                "CS_Job", 
                "CS_FirstAlertDivision", "CS_FirstAlertDivision.CS_Division",
                "CS_Country", "CS_State", "CS_City",
                "CS_Customer");

            if (null != lstFirstAlert)
            {
                if (lstFirstAlert.Count > 0)
                {
                    return lstFirstAlert;
                }
            }

            return null;
        }

        public virtual CS_FirstAlert GetFirstAlertById(int id)
        {
            CS_FirstAlert selectedFirstAlert = _firstAlertRepository.Get(a => a.Active && a.ID == id, 
                "CS_FirstAlertVehicle", "CS_FirstAlertVehicle.CS_Equipment", 
                "CS_FirstAlertPerson", 
                "CS_FirstAlertPerson.CS_State", "CS_FirstAlertPerson.CS_City", "CS_FirstAlertPerson.CS_ZipCode",
                "CS_FirstAlertPerson.CS_State_Doctor", "CS_FirstAlertPerson.CS_City_Doctor", "CS_FirstAlertPerson.CS_ZipCode_Doctor",
                "CS_FirstAlertPerson.CS_State_Hospital", "CS_FirstAlertPerson.CS_City_Hospital", "CS_FirstAlertPerson.CS_ZipCode_Hospital",
                "CS_FirstAlertPerson.CS_State_DriversLicense", "CS_FirstAlertPerson.CS_City_DriversLicense", "CS_FirstAlertPerson.CS_ZipCode_DriversLicense",
                "CS_FirstAlertDivision", 
                "CS_FirstAlertFirstAlertType",
                "CS_FirstAlertContactPersonal");
            return selectedFirstAlert;
        }

        public virtual CS_FirstAlert GetFirstAlertByIdForReport(int id)
        {
            CS_FirstAlert selectedFirstAlert = _firstAlertRepository.Get(a => a.Active && a.ID == id,
                "CS_FirstAlertDivision", "CS_FirstAlertDivision.CS_Division",
                "CS_FirstAlertPerson", "CS_FirstAlertPerson.CS_Employee",
                "CS_FirstAlertVehicle", "CS_FirstAlertVehicle.CS_Equipment",
                "CS_FirstAlertFirstAlertType", "CS_FirstAlertFirstAlertType.CS_FirstAlertType",
                "CS_Job", "CS_Job.CS_LocationInfo", "CS_Job.CS_LocationInfo.CS_State", "CS_Job.CS_LocationInfo.CS_City");
            return selectedFirstAlert;
        }

        /// <summary>
        /// Returns a First Alert Entity based on a CallLogID
        /// </summary>
        /// <param name="callLogId"></param>
        /// <returns></returns>
        public CS_FirstAlert GetFirstAlertByCallLogId(int callLogId)
        {
            return _firstAlertRepository.Get(e => e.CallLogID == callLogId);
        }

        /// <summary>
        /// Return list of person fo the given first alert
        /// </summary>
        /// <param name="firstAlertID">First alert Identifier</param>
        /// <returns></returns>
        public IList<CS_FirstAlertPerson> ListFirstAlertPersonByFirstAlertID(int firstAlertID)
        {
            return _firstAlertPersonRepository.ListAll(w => w.Active && w.FirstAlertID == firstAlertID);
        }

        /// <summary>
        /// Return list of person fo the given first alert
        /// </summary>
        /// <param name="firstAlertID">First alert Identifier</param>
        /// <returns></returns>
        public IList<CS_FirstAlertPerson> ListFirstAlertEmployeeByFirstAlertID(int firstAlertID)
        {
            return _firstAlertPersonRepository.ListAll(w => w.Active && w.FirstAlertID == firstAlertID && w.IsHulcherEmployee);
        }

        /// <summary>
        /// Returns a list of vehicles associated with the given first alert
        /// </summary>
        /// <param name="firstAlertID">DB ID of the First Alert</param>
        /// <returns>List of FirstAlertVehicle Entity</returns>
        public IList<CS_FirstAlertVehicle> ListFirstAlertVehiclesByFirstAlertID(int firstAlertID)
        {
            return _firstAlertVehicleRepository.ListAll(e => e.Active && e.FirstAlertID == firstAlertID);
        }

        /// <summary>
        /// Returns a list of Hulcher vehicles associated with the given first alert
        /// </summary>
        /// <param name="firstAlertID">DB ID of the First Alert</param>
        /// <returns>List of FirstAlertVehicle Entity</returns>
        public IList<CS_View_FirstAlertReportHulcherVehicles> ListFirstAlertHulcherVehiclesByFirstAlertIDForReport(int firstAlertID)
        {
            //return _firstAlertReportRepository.ListAll(e => e.Active && e.FirstAlertId == firstAlertID && e.IsHulcherVehicle);
            return _firstAlertReportHulcherVehicleRepository.ListAll(e => e.Active && e.FirstAlertId == firstAlertID);
        }

        /// <summary>
        /// Returns a list of Other vehicles associated with the given first alert
        /// </summary>
        /// <param name="firstAlertID">DB ID of the First Alert</param>
        /// <returns>List of FirstAlertVehicle Entity</returns>
        public IList<CS_View_FirstAlertReportOtherVehicle> ListFirstAlertOtherVehiclesByFirstAlertIDForReport(int firstAlertID)
        {
            return _firstAlertReportOtherVehicleRepository.ListAll(e => e.Active && e.FirstAlertId == firstAlertID && !e.IsHulcherVehicle); 
            //return _firstAlertVehicleRepository.ListAll(e => e.Active && e.FirstAlertID == firstAlertID && !e.IsHulcherVehicle);
        }

        /// <summary>
        /// Returns a list of Contact Personal related to the First Alert Identifier
        /// </summary>
        /// <param name="firstAlertID">First Alert Identifier</param>
        /// <returns>Contact Personal List</returns>
        public IList<CS_View_FirstAlertReportContactPersonal> ListFirstAlertContactPersonalForReport(int firstAlertID)
        {
            return _firstAlertReportContactPersonalViewRepository.ListAll(e => e.FirstAlertID == firstAlertID);
        }

        /// <summary>
        /// Return list of first alert table that was filtered on the page
        /// </summary>
        /// <param name="filter">filter</param>
        /// <param name="value">values</param>
        /// <returns>list cs_firstalert</returns>
        public IList<CS_FirstAlert> ListFilteredFirstAlert(Globals.FirstAlert.FirstAlertFilters filter, string value)
        {
            string[] values = value.Split(',');
            for (int i = 0; i < values.Length; i++)
                values[i] = values[i].Trim();

            IList<CS_FirstAlert> lstFirstAlert = new List<CS_FirstAlert>();

            string[] includeList = new[] { "CS_Job", 
                                           "CS_FirstAlertFirstAlertType", 
                                           "CS_FirstAlertFirstAlertType.CS_FirstAlertType",  
                                           "CS_FirstAlertDivision", 
                                           "CS_FirstAlertDivision.CS_Division", 
                                           "CS_Country", 
                                           "CS_City", 
                                           "CS_State",
                                           "CS_Customer" };

            switch (filter)
            {
                case Globals.FirstAlert.FirstAlertFilters.JobNumber:
                    lstFirstAlert = _firstAlertRepository.ListAll(w => w.Active &&
                                                                    (values.Any(e => w.CS_Job.Number.Contains(e) && w.CS_Job.Number != null)) || (values.Any(a => w.CS_Job.Internal_Tracking.Contains(a) && w.CS_Job.Number == null && w.CS_Job.Internal_Tracking != null)) && w.CS_Job.ID != Globals.GeneralLog.ID,
                                                                    w => w.Number,
                                                                    true, includeList);
                    if (null != lstFirstAlert)
                    {
                        if (lstFirstAlert.Count > 0)
                        {
                            return lstFirstAlert;
                        }
                    }

                    break;
                case Globals.FirstAlert.FirstAlertFilters.Division:
                    lstFirstAlert = _firstAlertRepository.ListAll(w => w.Active &&
                                                                    values.Any(e => w.CS_FirstAlertDivision.Any(f => f.Active && f.CS_Division.Name.Contains(e))),
                                                                    w => w.Number,
                                                                    true, includeList);
                    if (null != lstFirstAlert)
                    {
                        if (lstFirstAlert.Count > 0)
                        {
                            return lstFirstAlert;
                        }
                    }
                    break;
                case Globals.FirstAlert.FirstAlertFilters.Location:
                    lstFirstAlert = _firstAlertRepository.ListAll(w => w.Active && values.Any(e => w.CS_Country.Name.Contains(e) || w.CS_State.Name.Contains(e) || w.CS_City.Name.Contains(e)),
                                                                    w => w.Number,
                                                                    true, includeList);
                    if (null != lstFirstAlert)
                    {
                        if (lstFirstAlert.Count > 0)
                        {
                            return lstFirstAlert;
                        }
                    }
                    break;

                case Globals.FirstAlert.FirstAlertFilters.Date:
                    DateTime dt;
                    if (DateTime.TryParse(value, new System.Globalization.CultureInfo("en-US"), System.Globalization.DateTimeStyles.None, out dt))
                    {
                        lstFirstAlert = _firstAlertRepository.ListAll(e => e.Active && e.Date.Year == dt.Year && e.Date.Month == dt.Month && e.Date.Day == dt.Day,
                                                                    e => e.Number,
                                                                    false, includeList);
                    }

                    if (null != lstFirstAlert)
                    {
                        if (lstFirstAlert.Count > 0)
                        {
                            return lstFirstAlert;
                        }
                    }
                    break;

                case Globals.FirstAlert.FirstAlertFilters.Time:
                    DateTime dt1;
                    if (DateTime.TryParse(value, new System.Globalization.CultureInfo("en-US"), System.Globalization.DateTimeStyles.None, out dt1))
                    {
                        lstFirstAlert = _firstAlertRepository.ListAll(e => e.Active && e.Date.Hour == dt1.Hour && e.Date.Minute == dt1.Minute,
                                                                    e => e.Number,
                                                                    false, includeList);
                    }

                    if (null != lstFirstAlert)
                    {
                        if (lstFirstAlert.Count > 0)
                        {
                            return lstFirstAlert;
                        }
                    }
                    break;

                case Globals.FirstAlert.FirstAlertFilters.IncidentType:
                    lstFirstAlert = _firstAlertRepository.ListAll(w => w.Active && values.Any(e => w.CS_FirstAlertFirstAlertType.Any(f => f.Active && f.CS_FirstAlertType.Description.Contains(e))),
                                                                    w => w.Number,
                                                                    true, includeList);
                    if (null != lstFirstAlert)
                    {
                        if (lstFirstAlert.Count > 0)
                        {
                            return lstFirstAlert;
                        }
                    }
                    break;

                case Globals.FirstAlert.FirstAlertFilters.AlertNumber:

                    lstFirstAlert = _firstAlertRepository.ListAll(w => w.Active && values.Any(f => w.Number.Contains(f)));

                    if (null != lstFirstAlert)
                    {
                        if (lstFirstAlert.Count > 0)
                        {
                            return lstFirstAlert;
                        }
                    }
                    break;
                case Globals.FirstAlert.FirstAlertFilters.Customer:
                    lstFirstAlert = _firstAlertRepository.ListAll(w => w.Active && values.Any(f => w.CS_Customer != null && w.CS_Customer.Name.Contains(f)));

                    if (null != lstFirstAlert)
                    {
                        if (lstFirstAlert.Count > 0)
                        {
                            return lstFirstAlert;
                        }
                    }
                    break;
            }

            return null;
        }

        #endregion

        #region [ Save First Alert ]

        /// <summary>
        /// Method That defines if a First Alert is going to saved or updated
        /// </summary>
        /// <param name="firstAlertEntity"></param>
        public void SaveUpdateFirstAlert(CS_FirstAlert firstAlertEntity, IList<FirstAlertPersonVO> personList, IList<CS_FirstAlertVehicle> vehicleList, IList<CS_FirstAlertDivision> divisionList, IList<CS_FirstAlertFirstAlertType> typeList, IList<CS_FirstAlertContactPersonal> contactPersonalList)
        {
            List<CS_FirstAlertPerson> csPersonList = new List<CS_FirstAlertPerson>();

            for (int i = 0; i < personList.Count; i++)
            {
                csPersonList.Add(new CS_FirstAlertPerson());
                csPersonList[i].FirstAlertPersonVO = personList[i];
            }

            if (firstAlertEntity.ID.Equals(0))
                SaveFirstAlert(firstAlertEntity, csPersonList, vehicleList, divisionList, typeList);
            else
                UpdateFirstAlert(firstAlertEntity, csPersonList, vehicleList, divisionList, typeList, contactPersonalList);
        }

        /// <summary>
        /// Saves CS_FirstAlert Entity and relations
        /// </summary>
        /// <param name="firstAlertEntity"></param>
        private void SaveFirstAlert(CS_FirstAlert firstAlertEntity, IList<CS_FirstAlertPerson> personList, IList<CS_FirstAlertVehicle> vehicleList, IList<CS_FirstAlertDivision> divisionList, IList<CS_FirstAlertFirstAlertType> typeList)
        {
            if (firstAlertEntity != null)
            {
                lock (locked)
                {
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted }))
                    {
                        DateTime creationDate = DateTime.Now;
                        try
                        {
                            firstAlertEntity.CreatedBy = this.UserName;
                            firstAlertEntity.CreationDate = creationDate;
                            //firstAlertEntity.CreationID;
                            firstAlertEntity.ModifiedBy = this.UserName;
                            firstAlertEntity.ModificationDate = creationDate;
                            //firstAlertEntity.ModificationID;
                            firstAlertEntity = GenerateFirstAlertNumber(firstAlertEntity);
                            firstAlertEntity = _firstAlertRepository.Add(firstAlertEntity);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("There was an error saving the Job data. Please verify the content of the fields and try again.", ex);
                        }

                        if (firstAlertEntity != null)
                        {
                            for (int i = 0; i < personList.Count; i++)
                            {
                                CS_FirstAlertPerson person = personList[i];
                                person.FirstAlertID = firstAlertEntity.ID;
                                person.CreatedBy = this.UserName;
                                person.CreationDate = creationDate;
                                //person.CreationID;
                                person.ModifiedBy = this.UserName;
                                person.ModificationDate = creationDate;
                                //person.ModificationID;
                            }

                            for (int i = 0; i < vehicleList.Count; i++)
                            {
                                CS_FirstAlertVehicle vehicle = vehicleList[i];
                                vehicle.FirstAlertID = firstAlertEntity.ID;
                                vehicle.CreatedBy = this.UserName;
                                vehicle.CreationDate = creationDate;
                                //vehicle.CreationID;
                                vehicle.ModifiedBy = this.UserName;
                                vehicle.ModificationDate = creationDate;
                                //vehicle.ModificationID;
                            }

                            for (int i = 0; i < divisionList.Count; i++)
                            {
                                CS_FirstAlertDivision division = divisionList[i];
                                division.FirstAlertID = firstAlertEntity.ID;
                                division.CreatedBy = this.UserName;
                                division.CreationDate = creationDate;
                                //division.CreationID;
                                division.ModifiedBy = this.UserName;
                                division.ModificationDate = creationDate;
                                //division.ModificationID;
                            }

                            for (int i = 0; i < typeList.Count; i++)
                            {
                                CS_FirstAlertFirstAlertType type = typeList[i];
                                type.FirstAlertID = firstAlertEntity.ID;
                                type.CreatedBy = this.UserName;
                                type.CreationDate = creationDate;
                                //type.CreationID;
                                type.ModifiedBy = this.UserName;
                                type.ModificationDate = creationDate;
                                //type.ModificationID;
                            }

                            vehicleList = SaveFirstAlertVehicle(vehicleList);
                            SaveFirstAlertPerson(LinkVehiclesAndPersons(personList,vehicleList));
                            SaveFirstAlertDivision(divisionList);
                            SaveFirstAlertFirstAlertType(typeList);
                            if (firstAlertEntity.JobID == Globals.GeneralLog.ID)
                                SaveFirstAlertCallLog(firstAlertEntity, true);
                            else
                            {
                                SaveFirstAlertCallLog(firstAlertEntity, false);
                                if (firstAlertEntity.CopyToGeneralLog)
                                    SaveFirstAlertCallLog(firstAlertEntity, true);
                            }
                            scope.Complete();
                        }
                    }
                }
            }
        }

        private IList<CS_FirstAlertPerson> LinkVehiclesAndPersons(IList<CS_FirstAlertPerson> personList, IList<CS_FirstAlertVehicle> vehicleList)
        {
            for (int i = 0; i < vehicleList.Count; i++)
            {
                List<CS_FirstAlertPerson> personVOList = personList.Where(e => e.VehicleIndex == vehicleList[i].TemporaryID).ToList();

                for (int j = 0; j < personVOList.Count; j++)
                {
                    int personVOAux = personList.IndexOf(personVOList[j]);

                    personVOList[j].FirstAlertVehicleID = vehicleList[i].ID;

                    personList[personVOAux] = personVOList[j];
                }
            }

            return personList;
        }

        /// <summary>
        /// Saves a list of CS_FirstAlertPerson
        /// </summary>
        /// <param name="firstAlertPersonList"></param>
        private void SaveFirstAlertPerson(IList<CS_FirstAlertPerson> firstAlertPersonList)
        {
            try
            {
                if (firstAlertPersonList != null && firstAlertPersonList.Count > 0)
                    _firstAlertPersonRepository.AddList(firstAlertPersonList);
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error saving the First Alert Person data. Please verify the content of the fields and try again.", ex);
            }
        }

        /// <summary>
        /// Saves a list of CS_FirstAlertVehicle
        /// </summary>
        /// <param name="_firstAlertVehicleList"></param>
        private IList<CS_FirstAlertVehicle> SaveFirstAlertVehicle(IList<CS_FirstAlertVehicle> _firstAlertVehicleList)
        {
            try
            {
                if (_firstAlertVehicleList != null && _firstAlertVehicleList.Count > 0)
                    _firstAlertVehicleList = _firstAlertVehicleRepository.AddList(_firstAlertVehicleList);

                return _firstAlertVehicleList;
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error saving the First Alert Vehicle data. Please verify the content of the fields and try again.", ex);
            }
        }

        /// <summary>
        /// Saves a list of CS_FirstAlertDivision
        /// </summary>
        /// <param name="_firstAlertDivisionList"></param>
        private void SaveFirstAlertDivision(IList<CS_FirstAlertDivision> _firstAlertDivisionList)
        {
            try
            {
                if (_firstAlertDivisionList != null && _firstAlertDivisionList.Count > 0)
                    _firstAlertDivisionRepository.AddList(_firstAlertDivisionList);
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error saving the First Alert Division data. Please verify the content of the fields and try again.", ex);
            }
        }

        /// <summary>
        /// Saves a list of CS_FirstAlertFirstAlertType
        /// </summary>
        /// <param name="_firstAlertFirstAlertTypeList"></param>
        private void SaveFirstAlertFirstAlertType(IList<CS_FirstAlertFirstAlertType> _firstAlertFirstAlertTypeList)
        {
            try
            {
                if (_firstAlertFirstAlertTypeList != null && _firstAlertFirstAlertTypeList.Count > 0)
                    _firstAlertFirstAlertTypeRepository.AddList(_firstAlertFirstAlertTypeList);
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error saving the First Alert Type data. Please verify the content of the fields and try again.", ex);
            }
        }

        /// <summary>
        /// Generates and set First Alert Number
        /// </summary>
        /// <param name="firstAlertEntity"></param>
        /// <returns></returns>
        public CS_FirstAlert GenerateFirstAlertNumber(CS_FirstAlert firstAlertEntity)
        {
            // Gets the latest First Alert Number generated
            int lastFirstAlertNumber = _settingsModel.GetLastFirstAlertNumber();

            // Increments value
            lastFirstAlertNumber++;

            // Updates Last First Alert Number
            _settingsModel.UpdateLastFirstAlertNumber(lastFirstAlertNumber);

            firstAlertEntity.Number = FormatFirstAlertNumber(lastFirstAlertNumber);

            return firstAlertEntity;
        }

        private string FormatFirstAlertNumber(int lastFirstAlertNumber)
        {
            string formatedAlertNumber;
            if (lastFirstAlertNumber > 9999)
                formatedAlertNumber = lastFirstAlertNumber.ToString();
            else
                formatedAlertNumber = (lastFirstAlertNumber.ToString().PadLeft(4, '0'));

            return formatedAlertNumber;
        }

        #endregion

        #region [ Update First Alert ]

        /// <summary>
        /// Updates CS_FirstAlert Entity and relations
        /// </summary>
        /// <param name="firstAlertEntity"></param>
        private void UpdateFirstAlert(CS_FirstAlert firstAlertEntity, IList<CS_FirstAlertPerson> personList, IList<CS_FirstAlertVehicle> vehicleList, IList<CS_FirstAlertDivision> divisionList, IList<CS_FirstAlertFirstAlertType> typeList, IList<CS_FirstAlertContactPersonal> contactPersonalList)
        {
            if (firstAlertEntity != null)
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    CS_FirstAlert oldfirstAlert = _firstAlertRepository.Get(e => e.ID == firstAlertEntity.ID, "CS_FirstAlertPerson", "CS_FirstAlertVehicle", "CS_FirstAlertDivision", "CS_FirstAlertFirstAlertType");
                    DateTime modificationDate = DateTime.Now;
                        
                    try
                    {

                        firstAlertEntity.Number = oldfirstAlert.Number;
                        firstAlertEntity.CreatedBy = oldfirstAlert.CreatedBy;
                        //firstAlertEntity.CreationID = oldfirstAlert.CreationID;
                        firstAlertEntity.CreationDate = oldfirstAlert.CreationDate;
                        firstAlertEntity.ModifiedBy = this.UserName;
                        firstAlertEntity.ModificationDate = modificationDate;
                        //firstAlertEntity.ModificationID;

                        firstAlertEntity = _firstAlertRepository.Update(firstAlertEntity);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("There was an error updating the First Alert data. Please verify the content of the fields and try again.", ex);
                    }

                    if (firstAlertEntity != null)
                    {
                        vehicleList = UpdateFirstAlertVehicle(vehicleList, oldfirstAlert.CS_FirstAlertVehicle.Where(e => e.Active).ToList(), firstAlertEntity);
                        UpdateFirstAlertPerson(LinkVehiclesAndPersons(personList,vehicleList), oldfirstAlert.CS_FirstAlertPerson.Where(e => e.Active).ToList(), firstAlertEntity);
                        UpdateFirstAlertDivision(divisionList, oldfirstAlert.CS_FirstAlertDivision.Where(e => e.Active).ToList(), firstAlertEntity);
                        UpdateFirstAlertFirstAlertType(typeList, oldfirstAlert.CS_FirstAlertFirstAlertType.Where(e => e.Active).ToList(), firstAlertEntity);
                    }

                    try
                    {
                        if (null != contactPersonalList && contactPersonalList.Count > 0)
                            _firstAlertContactPersonalRepository.UpdateList(contactPersonalList);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("There was an error update the Contact Personal list.", ex);
                    }

                    scope.Complete();
                }
            }
        }

        /// <summary>
        /// Updates a list of CS_FirstAlertPerson
        /// </summary>
        /// <param name="firstAlertPersonList"></param>
        private void UpdateFirstAlertPerson(IList<CS_FirstAlertPerson> firstAlertPersonList, IList<CS_FirstAlertPerson> oldDBList, CS_FirstAlert firstAlert)
        {
            try
            {
                //Finds Who needs to be saved
                List<CS_FirstAlertPerson> saveList = firstAlertPersonList.Where(e => e.ID == 0).ToList();
                //Finds Who needs to be updates
                List<CS_FirstAlertPerson> updateList = firstAlertPersonList.Where(e => oldDBList.Any(a => a.ID == e.ID)).ToList();
                //Finds Who needs to be deleted
                List<CS_FirstAlertPerson> toDeleteList = oldDBList.Where(e => !firstAlertPersonList.Any(a => a.ID == e.ID && a.ID != 0)).ToList();
                List<CS_FirstAlertPerson> deleteList = new List<CS_FirstAlertPerson>();

                //Apply logical delete at deleteList
                for (int i = 0; i < toDeleteList.Count; i++)
                {
                    CS_FirstAlertPerson deleteItem = toDeleteList[i];

                    deleteList.Add
                        (
                            new CS_FirstAlertPerson()
                            {
                                ID = deleteItem.ID,
                                FirstAlertID = deleteItem.FirstAlertID,
                                IsHulcherEmployee = deleteItem.IsHulcherEmployee,
                                FirstAlertVehicleID = deleteItem.FirstAlertVehicleID,
                                VehiclePosition = deleteItem.VehiclePosition,
                                EmployeeID = deleteItem.EmployeeID,
                                LastName = deleteItem.LastName,
                                FirstName = deleteItem.FirstName,
                                CountryID = deleteItem.CountryID,
                                StateID = deleteItem.StateID,
                                CityID = deleteItem.CityID,
                                ZipcodeID = deleteItem.ZipcodeID,
                                Address = deleteItem.Address,
                                InjuryNature = deleteItem.InjuryNature,
                                InjuryBodyPart = deleteItem.InjuryBodyPart,
                                MedicalSeverity = deleteItem.MedicalSeverity,
                                Details = deleteItem.Details,
                                DoctorsName = deleteItem.DoctorsName,
                                DoctorsCountryID = deleteItem.DoctorsCountryID,
                                DoctorsStateID = deleteItem.DoctorsStateID,
                                DoctorsCityID = deleteItem.DoctorsCityID,
                                DoctorsZipcodeID = deleteItem.DoctorsZipcodeID,
                                DoctorsPhoneNumber = deleteItem.DoctorsPhoneNumber,
                                HospitalName = deleteItem.HospitalName,
                                HospitalCountryID = deleteItem.HospitalCountryID,
                                HospitalStateID = deleteItem.HospitalStateID,
                                HospitalCityID = deleteItem.HospitalCityID,
                                HospitalZipcodeID = deleteItem.HospitalZipcodeID,
                                HospitalPhoneNumber = deleteItem.HospitalPhoneNumber,
                                DriversLicenseNumber = deleteItem.DriversLicenseNumber,
                                DriversLicenseCountryID = deleteItem.DriversLicenseCountryID,
                                DriversLicenseStateID = deleteItem.DriversLicenseStateID,
                                DriversLicenseCityID = deleteItem.DriversLicenseCityID,
                                DriversLicenseZipcodeID = deleteItem.DriversLicenseZipcodeID,
                                DriversLicenseAddress = deleteItem.DriversLicenseAddress,
                                InsuranceCompany = deleteItem.InsuranceCompany,
                                PolicyNumber = deleteItem.PolicyNumber,
                                DrugScreenRequired = deleteItem.DrugScreenRequired,
                                CreationID = deleteItem.CreationID,
                                CreatedBy = deleteItem.CreatedBy,
                                CreationDate = deleteItem.CreationDate,
                                ModificationID = firstAlert.ModificationID,
                                ModifiedBy = firstAlert.ModifiedBy,
                                ModificationDate = firstAlert.ModificationDate,
                                Active = false

                            }
                        );
                }

                //Maintain cretedby and creationdate
                for (int i = 0; i < updateList.Count; i++)
                {
                    CS_FirstAlertPerson newReg = updateList[i];
                    CS_FirstAlertPerson oldReg = oldDBList.First(e => e.ID == updateList[i].ID);

                    newReg.CreatedBy = oldReg.CreatedBy;
                    newReg.CreationDate = oldReg.CreationDate;
                    newReg.ModificationID = firstAlert.ModificationID;
                    newReg.ModifiedBy = firstAlert.ModifiedBy;
                    newReg.ModificationDate = firstAlert.ModificationDate;
                }

                //Fill create and modified info
                for (int i = 0; i < saveList.Count; i++)
                {
                    CS_FirstAlertPerson person = saveList[i];
                    person.FirstAlertID = firstAlert.ID;
                    person.CreatedBy = firstAlert.ModifiedBy;
                    person.CreationDate = firstAlert.ModificationDate;
                    //person.CreationID;
                    person.ModifiedBy = firstAlert.ModifiedBy;
                    person.ModificationDate = firstAlert.ModificationDate;
                    //person.ModificationID;
                }

                //Merges updateList and deleteList
                updateList.AddRange(deleteList);

                //Saves new CS_FirstAlertPerson list
                SaveFirstAlertPerson(saveList);

                //Updates CS_FirstAlertPerson list
                if (updateList != null && updateList.Count > 0)
                    _firstAlertPersonRepository.UpdateList(updateList);
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error updating the First Alert Person data. Please verify the content of the fields and try again.", ex);
            }
        }

        /// <summary>
        /// Updates a list of CS_FirstAlertVehicle
        /// </summary>
        /// <param name="_firstAlertVehicleList"></param>
        private IList<CS_FirstAlertVehicle> UpdateFirstAlertVehicle(IList<CS_FirstAlertVehicle> firstAlertVehicleList, IList<CS_FirstAlertVehicle> oldDBList, CS_FirstAlert firstAlert)
        {
            try
            {
                //Finds Who needs to be saved
                List<CS_FirstAlertVehicle> saveList = firstAlertVehicleList.Where(e => e.ID == 0).ToList();
                //Finds Who needs to be updates
                List<CS_FirstAlertVehicle> updateList = firstAlertVehicleList.Where(e => oldDBList.Any(a => a.ID == e.ID)).ToList();
                //Finds Who needs to be deleted
                List<CS_FirstAlertVehicle> toDeleteList = oldDBList.Where(e => !firstAlertVehicleList.Any(a => a.ID == e.ID && a.ID != 0)).ToList();
                List<CS_FirstAlertVehicle> deleteList = new List<CS_FirstAlertVehicle>();

                //Apply logical delete at deleteList (Active = false)
                for (int i = 0; i < toDeleteList.Count; i++)
                {
                    CS_FirstAlertVehicle deleteItem = toDeleteList[i];

                    deleteList.Add
                        (
                            new CS_FirstAlertVehicle()
                            {
                                ID = deleteItem.ID,
                                FirstAlertID = deleteItem.FirstAlertID,
                                IsHulcherVehicle = deleteItem.IsHulcherVehicle,
                                EquipmentID = deleteItem.EquipmentID,
                                Make = deleteItem.Make,
                                Model = deleteItem.Model,
                                Year = deleteItem.Year,
                                Damage = deleteItem.Damage,
                                EstimatedCost = deleteItem.EstimatedCost,
                                CreationID = deleteItem.CreationID,
                                CreatedBy = deleteItem.CreatedBy,
                                CreationDate = deleteItem.CreationDate,
                                ModificationID = firstAlert.ModificationID,
                                ModifiedBy = firstAlert.ModifiedBy,
                                ModificationDate = firstAlert.ModificationDate,
                                Active = false

                            }
                        );
                }

                //Maintain cretedby and creationdate
                for (int i = 0; i < updateList.Count; i++)
                {
                    CS_FirstAlertVehicle newReg = updateList[i];
                    CS_FirstAlertVehicle oldReg = oldDBList.First(e => e.ID == updateList[i].ID);

                    newReg.CreatedBy = oldReg.CreatedBy;
                    newReg.CreationDate = oldReg.CreationDate;
                    newReg.ModificationID = firstAlert.ModificationID;
                    newReg.ModifiedBy = firstAlert.ModifiedBy;
                    newReg.ModificationDate = firstAlert.ModificationDate;
                }

                //Fill create and modified info
                for (int i = 0; i < saveList.Count; i++)
                {
                    CS_FirstAlertVehicle vehicle = saveList[i];
                    vehicle.FirstAlertID = firstAlert.ID;
                    vehicle.CreatedBy = firstAlert.ModifiedBy;
                    vehicle.CreationDate = firstAlert.ModificationDate;
                    //vehicle.CreationID;
                    vehicle.ModifiedBy = firstAlert.ModifiedBy;
                    vehicle.ModificationDate = firstAlert.ModificationDate;
                    //vehicle.ModificationID;
                }

                //Merges updateList and deleteList
                updateList.AddRange(deleteList);
                
                //Saves new CS_FirstAlertVehicle list
                saveList = SaveFirstAlertVehicle(saveList).ToList();

                //Updates CS_FirstAlertVehicle list
                if (updateList != null && updateList.Count > 0)
                    updateList = _firstAlertVehicleRepository.UpdateList(updateList).ToList();

                updateList.AddRange(saveList);

                return updateList.Where(e => e.Active).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error updating the First Alert Vehicle data. Please verify the content of the fields and try again.", ex);
            }
        }

        /// <summary>
        /// Updates a list of CS_FirstAlertDivision
        /// </summary>
        /// <param name="_firstAlertDivisionList"></param>
        private void UpdateFirstAlertDivision(IList<CS_FirstAlertDivision> firstAlertDivisionList, IList<CS_FirstAlertDivision> oldDBList, CS_FirstAlert firstAlert)
        {

            try
            {
                //Finds Who needs to be saved
                List<CS_FirstAlertDivision> saveList = firstAlertDivisionList.Where(e => !oldDBList.Any(a => a.DivisionID == e.DivisionID)).ToList();
                //Finds Who needs to be deleted
                List<CS_FirstAlertDivision> toDeleteList = oldDBList.Where(e => !firstAlertDivisionList.Any(a => a.DivisionID == e.DivisionID)).ToList();
                List<CS_FirstAlertDivision> deleteList = new List<CS_FirstAlertDivision>();

                //Apply logical delete at deleteList (Active = false)
                for (int i = 0; i < toDeleteList.Count; i++)
                {
                    CS_FirstAlertDivision deleteItem = toDeleteList[i];

                    deleteList.Add(
                        new CS_FirstAlertDivision()
                        {
                            ID = deleteItem.ID,
                            DivisionID = deleteItem.DivisionID,
                            FirstAlertID = deleteItem.FirstAlertID,
                            CreationID = deleteItem.CreationID,
                            CreatedBy = deleteItem.CreatedBy,
                            CreationDate = deleteItem.CreationDate,
                            ModificationID = firstAlert.ModificationID,
                            ModifiedBy = firstAlert.ModifiedBy,
                            ModificationDate = firstAlert.ModificationDate,
                            Active = false
                        }
                    );
                }

                //Fill create and modified info
                for (int i = 0; i < saveList.Count; i++)
                {
                    CS_FirstAlertDivision division = saveList[i];
                    division.FirstAlertID = firstAlert.ID;
                    division.CreatedBy = firstAlert.ModifiedBy;
                    division.CreationDate = firstAlert.ModificationDate;
                    //division.CreationID;
                    division.ModifiedBy = firstAlert.ModifiedBy;
                    division.ModificationDate = firstAlert.ModificationDate;
                    //division.ModificationID;
                }

                //Saves new CS_FirstAlertDivision list
                SaveFirstAlertDivision(saveList);

                //Updates CS_FirstAlertDivision list
                if (deleteList != null && deleteList.Count > 0)
                    _firstAlertDivisionRepository.UpdateList(deleteList);
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error updating the First Alert Division data. Please verify the content of the fields and try again.", ex);
            }
        }

        /// <summary>
        /// Updates a list of CS_FirstAlertDivision
        /// </summary>
        /// <param name="_firstAlertDivisionList"></param>
        private void UpdateFirstAlertFirstAlertType(IList<CS_FirstAlertFirstAlertType> firstAlertFirstAlertTypeList, IList<CS_FirstAlertFirstAlertType> oldDBList, CS_FirstAlert firstAlert)
        {

            try
            {
                //Finds Who needs to be saved
                List<CS_FirstAlertFirstAlertType> saveList = firstAlertFirstAlertTypeList.Where(e => !oldDBList.Any(a => a.FirstAlertTypeID == e.FirstAlertTypeID)).ToList();
                //Finds Who needs to be deleted
                List<CS_FirstAlertFirstAlertType> toDeleteList = oldDBList.Where(e => !firstAlertFirstAlertTypeList.Any(a => a.FirstAlertTypeID == e.FirstAlertTypeID)).ToList();
                List<CS_FirstAlertFirstAlertType> deleteList = new List<CS_FirstAlertFirstAlertType>();

                //Apply logical delete at deleteList (Active = false)
                for (int i = 0; i < toDeleteList.Count; i++)
                {
                    CS_FirstAlertFirstAlertType deleteItem = toDeleteList[i];

                    deleteList.Add(
                        new CS_FirstAlertFirstAlertType()
                        {
                            ID = deleteItem.ID,
                            FirstAlertTypeID = deleteItem.FirstAlertTypeID,
                            FirstAlertID = deleteItem.FirstAlertID,
                            CreationID = deleteItem.CreationID,
                            CreatedBy = deleteItem.CreatedBy,
                            CreationDate = deleteItem.CreationDate,
                            ModificationID = firstAlert.ModificationID,
                            ModifiedBy = firstAlert.ModifiedBy,
                            ModificationDate = firstAlert.ModificationDate,
                            Active = false
                        }
                    );
                }

                //Fill create and modified info
                for (int i = 0; i < saveList.Count; i++)
                {
                    CS_FirstAlertFirstAlertType type = saveList[i];
                    type.FirstAlertID = firstAlert.ID;
                    type.CreatedBy = firstAlert.ModifiedBy;
                    type.CreationDate = firstAlert.ModificationDate;
                    //type.CreationID;
                    type.ModifiedBy = firstAlert.ModifiedBy;
                    type.ModificationDate = firstAlert.ModificationDate;
                    //type.ModificationID;
                }

                //Saves new CS_FirstAlertDivision list
                SaveFirstAlertFirstAlertType(saveList);

                //Updates CS_FirstAlertDivision list
                if (deleteList != null && deleteList.Count > 0)
                    _firstAlertFirstAlertTypeRepository.UpdateList(deleteList);
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error updating the First Alert Type data. Please verify the content of the fields and try again.", ex);
            }
        }

        #endregion

        #region [ Delete First Alert ]

        /// <summary>
        /// Deletes First Alert (Active = false)
        /// </summary>
        /// <param name="firstAlertID"></param>
        public void DeleteFirstAlert(int firstAlertID)
        {
            if (!firstAlertID.Equals(0))
            {
                CS_FirstAlert firstAlertEntity = _firstAlertRepository.Get(e => e.ID == firstAlertID);
                firstAlertEntity.Active = false;
                _firstAlertRepository.Update(firstAlertEntity);
            }
        }

        #endregion

        #region [ First Alert Call Log ]

        /// <summary>
        /// Build the Note for the Lapsed Preset based on Job
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public string BuildFirstAlertNote(CS_FirstAlert firstAlert)
        {
            StringBuilder firstAlertNote = new StringBuilder();

            string number = "";

            if (firstAlert.Number.ToString().Length > 4)
                number = firstAlert.Number.ToString().PadLeft(6, '0');
            else
                number = firstAlert.Number.ToString().PadLeft(4, '0');

            firstAlertNote.Append("First Alert ID#:<Text>" + number + "<BL>");
            firstAlertNote.Append("Job #:<Text>" + firstAlert.CS_Job.PrefixedNumber + "<BL>");
            firstAlertNote.Append("Creating User:<Text>" + firstAlert.CreatedBy + "<BL>");
            if (null != firstAlert.CS_Customer)
                firstAlertNote.Append("Company:<Text>" + firstAlert.CS_Customer.FullCustomerInformation + "<BL>");
            firstAlertNote.Append("Location:<Text>");
            if (null != firstAlert.CS_City)
                firstAlertNote.Append(firstAlert.CS_City.ExtendedName);
            if (null != firstAlert.CS_City && null != firstAlert.CS_State)
                firstAlertNote.Append(", ");
            if (null != firstAlert.CS_State)
                firstAlertNote.Append(firstAlert.CS_State.AcronymName);
            firstAlertNote.Append("<BL>");

            firstAlertNote.Append("First Alert Type:");
            if (firstAlert.CS_FirstAlertFirstAlertType.Count > 0)
            {
                foreach (CS_FirstAlertFirstAlertType firstAlertType in firstAlert.CS_FirstAlertFirstAlertType)
                    firstAlertNote.Append("<Text>" + firstAlertType.CS_FirstAlertType.Description + "<BL>");
            }
            else
                firstAlertNote.Append("<BL>");

            if (null != firstAlert.CS_FirstAlertPerson && firstAlert.CS_FirstAlertPerson.Count > 0)
            {
                firstAlertNote.Append("Employee Involved:");
                foreach (CS_FirstAlertPerson person in firstAlert.CS_FirstAlertPerson)
                {
                    if (person.IsHulcherEmployee)
                        firstAlertNote.Append("<Text>" + person.CS_Employee.DivisionAndFullName + "<BL>");
                }
            }

            firstAlertNote.Append("Reported By:<Text>" + firstAlert.ReportedBy + "<BL>");

            if (firstAlert.CompletedByEmployeeID.HasValue)
                firstAlertNote.Append("Completed By:<Text>" + firstAlert.CS_Employee_CompletedBy.DivisionAndFullName + "<BL>");

            if (!string.IsNullOrEmpty(firstAlert.Details))
                firstAlertNote.Append("Details:<Text>" + firstAlert.Details + "<BL>");

            return firstAlertNote.ToString();
        }

        /// <summary>
        /// Create the Lapsed Preset Call Entry
        /// </summary>
        /// <param name="job"></param>
        private void SaveFirstAlertCallLog(CS_FirstAlert firstAlert, bool isGeneralLog)
        {
            try
            {
                CS_CallLog newCallEntry = new CS_CallLog();

                if (isGeneralLog) 
                    newCallEntry.JobID = Globals.GeneralLog.ID;
                else
                    newCallEntry.JobID = firstAlert.JobID;
                newCallEntry.CallTypeID = (int)Globals.CallEntry.CallType.FirstAlert;
                if (isGeneralLog)
                    newCallEntry.PrimaryCallTypeId = (int)Globals.CallEntry.PrimaryCallType.NonJobUpdateNotification;
                else
                    newCallEntry.PrimaryCallTypeId = (int)Globals.CallEntry.PrimaryCallType.JobUpdateNotification;
                newCallEntry.CallDate = DateTime.Now;
                DateTimeOffset dateTimeOffset = DateTimeOffset.Now;
                newCallEntry.Xml = null;
                newCallEntry.Note = BuildFirstAlertNote(firstAlert);
                newCallEntry.CreatedBy = "System";
                newCallEntry.CreationDate = DateTime.Now;
                newCallEntry.ModifiedBy = "System";
                newCallEntry.ModificationDate = DateTime.Now;
                newCallEntry.Active = true;
                newCallEntry.UserCall = true;

                _callLogRepository.Add(newCallEntry);

                IList<EmailVO> callCriteriaList = SaveCallCriteriaFirstAlertResources(newCallEntry);

                if (!isGeneralLog)
                    SaveFirstAlertContactPersonal(firstAlert, callCriteriaList);
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error while trying to save a new call entry of type First Alert.", ex);
            }
        }

        private void SaveFirstAlertContactPersonal(CS_FirstAlert firstAlert, IList<EmailVO> callCriteriaList)
        {
            try
            {
                foreach (EmailVO item in callCriteriaList)
                {
                    CS_FirstAlertContactPersonal contactPersonal = new CS_FirstAlertContactPersonal();
                    contactPersonal.FirstAlertID = firstAlert.ID;
                    if (item.Type == (int)Globals.CallCriteria.EmailVOType.Employee)
                        contactPersonal.EmployeeID = item.PersonID;
                    else if (item.Type == (int)Globals.CallCriteria.EmailVOType.Contact)
                        contactPersonal.ContactID = item.PersonID;

                    contactPersonal.EmailAdviseDate = firstAlert.ModificationDate;
                    contactPersonal.EmailAdviseUser = firstAlert.ModifiedBy;

                    contactPersonal.CreatedBy = firstAlert.ModifiedBy;
                    contactPersonal.CreationDate = firstAlert.ModificationDate;
                    contactPersonal.ModifiedBy = firstAlert.ModifiedBy;
                    contactPersonal.ModificationDate = firstAlert.ModificationDate;
                    contactPersonal.Active = true;

                    _firstAlertContactPersonalRepository.Add(contactPersonal);
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error while trying to save Contact Personal information", ex);
            }
        }

        /// <summary>
        /// Generates records for a FirstAlert Call Criteria Resources
        /// </summary>
        /// <param name="initialAdvise">Generated Initial Advise Call Log</param>
        private IList<EmailVO> SaveCallCriteriaFirstAlertResources(CS_CallLog firstAlert)
        {
            try
            {
                IList<CS_CallLogResource> saveList = new List<CS_CallLogResource>();
                IList<CS_CallLogCallCriteriaEmail> emailSaveList = new List<CS_CallLogCallCriteriaEmail>();

                IList<EmailVO> resourceList = _callCriteriaModel.ListReceiptsByCallLog(firstAlert.CallTypeID.ToString(), firstAlert.JobID, firstAlert);

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
                        CallLogID = firstAlert.ID,
                        EmployeeID = employeeId,
                        ContactID = contactId,
                        JobID = firstAlert.JobID,
                        Type = resourceList[i].Type,
                        CreatedBy = firstAlert.CreatedBy,
                        CreationDate = DateTime.Now,
                        ModifiedBy = firstAlert.ModifiedBy,
                        ModificationDate = DateTime.Now,
                        Active = true
                    };

                    saveList.Add(resource);

                    CS_CallLogCallCriteriaEmail emailResource = new CS_CallLogCallCriteriaEmail()
                    {
                        CallLogID = firstAlert.ID,
                        Name = resourceList[i].Name,
                        Email = resourceList[i].Email,
                        Status = (int)Globals.CallCriteria.CallCriteriaEmailStatus.Pending,
                        StatusDate = DateTime.Now,
                        //CreationID = , 
                        CreatedBy = firstAlert.CreatedBy,
                        CreationDate = DateTime.Now,
                        //ModificationID,
                        ModifiedBy = firstAlert.ModifiedBy,
                        ModificationDate = DateTime.Now,
                        Active = true
                    };

                    emailSaveList.Add(emailResource);
                }

                _callLogResourceRepository.AddList(saveList);

                _callLogCallCriteriaEmailRepository.AddList(emailSaveList);

                return resourceList;
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error while trying to save the First Alert Resources.", ex);
            }
        }

        #endregion

        #endregion

        #region [ IDisposable Implementation ]
        /// <summary>
        /// Dispose all objects that are no longer needed
        /// </summary>
        public void Dispose()
        {
            _firstAlertRepository = null;
            _firstAlertPersonRepository = null;
            _firstAlertVehicleRepository = null;
            _firstAlertDivisionRepository = null;
            _callLogRepository = null;
            _callLogResourceRepository = null;
            _callLogCallCriteriaEmailRepository = null;
            _firstAlertTypeRepository = null;
            _firstAlertFirstAlertTypeRepository = null;

            _settingsModel.Dispose();
            _settingsModel = null;
            _callCriteriaModel.Dispose();
            _callCriteriaModel = null;

            _unitOfWork.Dispose();
            _unitOfWork = null;
        }
        #endregion

        public string UserName { get; set; }
    }
}
