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

using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Hulcher.OneSource.CustomerService.Business.Model
{
    /// <summary>
    /// Equipment Model class
    /// </summary>
    public class EquipmentModel : IDisposable
    {
        #region [ Attributes ]

        /// <summary>
        /// Unit of Work used to access the database/in-memory context
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// Repository class for CS_EquipmentType
        /// </summary>
        private IRepository<CS_EquipmentType> _equipmentTypeRepository;

        /// <summary>
        /// Repository class for CS_View_ReserveEquipment
        /// </summary>
        private IRepository<CS_View_ReserveEquipment> _viewReserveEquipmentRepository;

        /// <summary>
        /// Repository class for CS_View_EquipmentInfo
        /// </summary>
        private IRepository<CS_View_EquipmentInfo> _equipmentInfoRepository;

        /// <summary>
        /// Repository class for CS_CallLog
        /// </summary>
        private IRepository<CS_CallLog> _callLogRepository;

        /// <summary>
        /// Repository class for CS_CallLogResource
        /// </summary>
        private IRepository<CS_CallLogResource> _callLogResourceRepository;

        /// <summary>
        /// Repository class for CS_View_EquipmentInfo
        /// </summary>
        private IRepository<CS_View_SecondaryEquipmentInfo> _secondaryEquipmentInfoRepository;

        /// <summary>
        /// Repository class for CS_Equipment
        /// </summary>
        private IRepository<CS_Equipment> _equipmentRepository;

        /// <summary>
        /// Repository class for CS_EquipmentCombo
        /// </summary>
        private IRepository<CS_EquipmentCombo> _equipmentComboRepository;

        /// <summary>
        /// Repository class for CS_EquipmentPermit
        /// </summary>
        private IRepository<CS_EquipmentPermit> _equipmentPermitRepository;

        /// <summary>
        /// Reporitory class for CS_EquipmentDownHistory
        /// </summary>
        private IRepository<CS_EquipmentDownHistory> _equipmentDownHistoryRepository;

        /// <summary>
        /// Repository class for CS_EquipmentCoverage
        /// </summary>
        private IRepository<CS_EquipmentCoverage> _equipmentCoverageRepository;

        /// <summary>
        /// Repository class for cs_equipmentwhitelight
        /// </summary>
        private IRepository<CS_EquipmentWhiteLight> _equipmentWhiteLightRepository;

        /// <summary>
        /// Repository class for CS_View_ConflictedEquipmentCombos
        /// </summary>
        private IRepository<CS_View_ConflictedEquipmentCombos> _conflictedCombosRepository;

        /// <summary>
        /// Repository class for CS_EquipmentPermitEmail
        /// </summary>
        private IRepository<CS_EquipmentPermitEmail> _equipmentPermitEmailRepository;

        /// <summary>
        /// Repository class for CS_Resource
        /// </summary>
        private IRepository<CS_Resource> _resourceRepository;

        /// <summary>
        /// Repository class for CS_LocalEquipmentType
        /// </summary>
        private ICachedRepository<CS_LocalEquipmentType> _localEquipmentTypeRepository;

        /// <summary>
        /// Instance for settings model
        /// </summary>
        private SettingsModel _settingsModel;

        /// <summary>
        /// Instance for location model
        /// </summary>
        private LocationModel _locationModel;

        /// <summary>
        /// Instance for email model
        /// </summary>
        private EmailModel _emailModel;

        /// <summary>
        /// Instance of the CallLogModel class
        /// </summary>
        private CallLogModel _callLogModel;

        /// <summary>
        /// Instance of the Call Criteria class
        /// </summary>
        private CallCriteriaModel _callCriteriaModel;

        /// <summary>
        /// Repository Class for CS_Job
        /// </summary>
        private IRepository<CS_Job> _jobRepository;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class constructor
        /// </summary>
        public EquipmentModel()
        {
            _unitOfWork = new EFUnitOfWork();

            _equipmentTypeRepository = new EFRepository<CS_EquipmentType> { UnitOfWork = _unitOfWork };
            _viewReserveEquipmentRepository = new EFRepository<CS_View_ReserveEquipment> { UnitOfWork = _unitOfWork };
            _equipmentInfoRepository = new EFRepository<CS_View_EquipmentInfo> { UnitOfWork = _unitOfWork };
            _callLogRepository = new EFRepository<CS_CallLog> { UnitOfWork = _unitOfWork };
            _callLogResourceRepository = new EFRepository<CS_CallLogResource> { UnitOfWork = _unitOfWork };
            _equipmentRepository = new EFRepository<CS_Equipment> { UnitOfWork = _unitOfWork };
            _secondaryEquipmentInfoRepository = new EFRepository<CS_View_SecondaryEquipmentInfo> { UnitOfWork = _unitOfWork };
            _equipmentComboRepository = new EFRepository<CS_EquipmentCombo> { UnitOfWork = _unitOfWork };
            _equipmentPermitRepository = new EFRepository<CS_EquipmentPermit> { UnitOfWork = _unitOfWork };
            _equipmentDownHistoryRepository = new EFRepository<CS_EquipmentDownHistory> { UnitOfWork = _unitOfWork };
            _equipmentCoverageRepository = new EFRepository<CS_EquipmentCoverage> { UnitOfWork = _unitOfWork };
            _conflictedCombosRepository = new EFRepository<CS_View_ConflictedEquipmentCombos> { UnitOfWork = _unitOfWork };
            _equipmentWhiteLightRepository = new EFRepository<CS_EquipmentWhiteLight> { UnitOfWork = _unitOfWork };
            _equipmentPermitEmailRepository = new EFRepository<CS_EquipmentPermitEmail> { UnitOfWork = _unitOfWork };
            _resourceRepository = new EFRepository<CS_Resource> { UnitOfWork = _unitOfWork };
            _localEquipmentTypeRepository = new CachedRepository<CS_LocalEquipmentType> { UnitOfWork = _unitOfWork };
            _jobRepository = new EFRepository<CS_Job> { UnitOfWork = _unitOfWork };
            _callCriteriaModel = new CallCriteriaModel(_unitOfWork);
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of Work instance (used for unit tests)</param>
        public EquipmentModel(IUnitOfWork unitOfWork)
        {
            _equipmentTypeRepository = new EFRepository<CS_EquipmentType> { UnitOfWork = unitOfWork };
            _viewReserveEquipmentRepository = new EFRepository<CS_View_ReserveEquipment> { UnitOfWork = unitOfWork };
            _equipmentInfoRepository = new EFRepository<CS_View_EquipmentInfo> { UnitOfWork = unitOfWork };
            _equipmentRepository = new EFRepository<CS_Equipment> { UnitOfWork = unitOfWork };
            _callLogRepository = new EFRepository<CS_CallLog> { UnitOfWork = unitOfWork };
            _callLogResourceRepository = new EFRepository<CS_CallLogResource> { UnitOfWork = unitOfWork };
            _equipmentComboRepository = new EFRepository<CS_EquipmentCombo> { UnitOfWork = unitOfWork };
            _equipmentPermitRepository = new EFRepository<CS_EquipmentPermit> { UnitOfWork = unitOfWork };
            _equipmentDownHistoryRepository = new EFRepository<CS_EquipmentDownHistory> { UnitOfWork = unitOfWork };
            _equipmentCoverageRepository = new EFRepository<CS_EquipmentCoverage> { UnitOfWork = unitOfWork };
            _conflictedCombosRepository = new EFRepository<CS_View_ConflictedEquipmentCombos> { UnitOfWork = unitOfWork };
            _equipmentWhiteLightRepository = new EFRepository<CS_EquipmentWhiteLight> { UnitOfWork = unitOfWork };
            _equipmentPermitEmailRepository = new EFRepository<CS_EquipmentPermitEmail> { UnitOfWork = unitOfWork };
            _resourceRepository = new EFRepository<CS_Resource> { UnitOfWork = unitOfWork };
            _localEquipmentTypeRepository = new CachedRepository<CS_LocalEquipmentType> { UnitOfWork = unitOfWork };
            _jobRepository = new EFRepository<CS_Job> { UnitOfWork = _unitOfWork };
            _callCriteriaModel = new CallCriteriaModel(_unitOfWork);
        }


        #endregion

        #region [ Methods ]

        /// <summary>
        /// Method that check if all equipment inside a combo is assigned to a job
        /// </summary>
        /// <param name="lstEquipment">lst equipment</param>
        /// <returns>lst</returns>
        public bool CheckEquipmentIsAssignedJob(List<int> lstEquipment)
        {
            List<CS_Resource> resource = _resourceRepository.ListAll(w => lstEquipment.Contains(w.EquipmentID.Value)).ToList();

            if (resource.Count > 0)
                return true;

            return false;
        }

        /// <summary>
        /// List all reserve equipment by division
        /// </summary>
        /// <param name="lstDivisionId"></param>
        /// <returns></returns>
        public IList<CS_View_ReserveEquipment> ListAllEquipmentsByDivision(List<int> lstDivisionId)
        {
            return _viewReserveEquipmentRepository.ListAll(w => lstDivisionId.Contains(w.DivisionID));
        }

        /// <summary>
        /// List all items of an Entity in the Databas
        /// </summary>
        /// <returns>List of Entities</returns>
        public IList<CS_EquipmentType> ListAllEquipmentType()
        {
            return _equipmentTypeRepository.ListAll(w => w.Active);
        }

        public IList<CS_EquipmentType> ListAllEquipmentType(Globals.EquipmentMaintenance.FilterType filterType, string filterValue)
        {
            string[] arrValue = filterValue.Split(',');
            for (int i = 0; i < arrValue.Length; i++)
                arrValue[i] = arrValue[i].Trim();

            switch (filterType)
            {
                case Globals.EquipmentMaintenance.FilterType.UnitType:
                    return _equipmentTypeRepository.ListAll(
                        e => e.Active &&
                             arrValue.Any(g => e.Number.Contains(g)));
                case Globals.EquipmentMaintenance.FilterType.Division:
                    return _equipmentTypeRepository.ListAll(
                        e => e.Active &&
                             e.CS_Equipment.Any(
                                f => f.Active && arrValue.Any(g => f.CS_Division.Name.Contains(g))));
                case Globals.EquipmentMaintenance.FilterType.UnitNumber:
                    return _equipmentTypeRepository.ListAll(
                         e => e.Active &&
                              e.CS_Equipment.Any(
                                     g => g.Active && arrValue.Any(
                                         h => g.Name.Contains(h))));
                default:
                    return new List<CS_EquipmentType>();
            }

        }

        /// <summary>
        /// List filterd items for the Reserve Equipment View
        /// </summary>
        /// <param name="equipmentTypeId">Equipment Type filter</param>
        /// <param name="stateId">State filter</param>
        /// <param name="divisionId">Division filter</param>
        /// <returns>List of Equipments</returns>
        public IList<CS_View_ReserveEquipment> ListFilteredEquipmentType(int? equipmentTypeId, int? locationId, IList<int> divisionList)
        {
            return _viewReserveEquipmentRepository.ListAll(w => (!equipmentTypeId.HasValue || w.EquipmentTypeID == equipmentTypeId.Value) &&
                    (!locationId.HasValue || w.StateID == locationId.Value) &&
                    (divisionList.Count == 0 || divisionList.Contains(w.DivisionID))).
                    OrderBy(e => e.Division).ThenBy(e => e.EquipmentType).ToList();
        }

        /// <summary>
        /// List Filtered EmployeeInfo from the view CS_View_EmployeeInfo
        /// </summary>
        /// <returns>List of Entities</returns>
        public virtual IList<CS_View_EquipmentInfo> ListFilteredEquipmentInfo(Globals.FirstAlert.EquipmentFilters filter, string value, int? jobID)
        {
            string[] arrValue = value.Split(',');
            for (int i = 0; i < arrValue.Length; i++)
                arrValue[i] = arrValue[i].Trim();

            IList<CS_View_EquipmentInfo> lstEquipInfo = new List<CS_View_EquipmentInfo>();

            switch (filter)
            {
                case Globals.FirstAlert.EquipmentFilters.DivisionName:
                    lstEquipInfo =
                        _equipmentInfoRepository.ListAll(
                            e => e.Active
                                && arrValue.Any(g => e.DivisionName.Contains(g))
                                && ((jobID.HasValue && e.JobID == jobID.Value) || (!jobID.HasValue)),
                            w => w.ComboName,
                            true);
                    break;
                case Globals.FirstAlert.EquipmentFilters.UnitNumber:
                    lstEquipInfo =
                        _equipmentInfoRepository.ListAll(
                            e => e.Active
                                && arrValue.Any(f => e.UnitNumber.Contains(f))
                                && ((jobID.HasValue && e.JobID == jobID.Value) || (!jobID.HasValue)),
                            w => w.ComboName,
                            true);
                    break;
                case Globals.FirstAlert.EquipmentFilters.Make:
                    lstEquipInfo =
                        _equipmentInfoRepository.ListAll(
                            e => e.Active
                                && arrValue.Any(f => e.Make.Contains(f))
                                && ((jobID.HasValue && e.JobID == jobID.Value) || (!jobID.HasValue)),
                            w => w.ComboName,
                            true);
                    break;
                default:
                    lstEquipInfo = _equipmentInfoRepository.ListAll(w => w.Active, w => w.ComboName, true);
                    break;
            }
            return lstEquipInfo;
        }

        /// <summary>
        /// List All Division-Comflicted EquipmentCombos
        /// </summary>
        /// <returns>List of Entities</returns>
        public virtual IList<CS_View_ConflictedEquipmentCombos> ListEquipmentComboNotification()
        {
            return _conflictedCombosRepository.ListAll();
        }

        /// <summary>
        /// List Filtered EmployeeInfo from the view CS_View_EmployeeInfo
        /// </summary>
        /// <returns>List of Entities</returns>
        public virtual IList<CS_View_EquipmentInfo> ListFilteredEquipmentInfo(Globals.ResourceAllocation.EquipmentFilters filter, string value, string orderBy)
        {
            string[] values = value.Split(',');
            for (int i = 0; i < values.Length; i++)
                values[i] = values[i].Trim();

            IList<CS_View_EquipmentInfo> lstEquipInfo = new List<CS_View_EquipmentInfo>();

            switch (filter)
            {
                case Globals.ResourceAllocation.EquipmentFilters.Division:
                    lstEquipInfo =
                        _equipmentInfoRepository.ListAll(
                            e => e.Active
                                && values.Any(v => e.DivisionName.Contains(v)),
                            w => w.ComboName,
                            true);
                    break;
                case Globals.ResourceAllocation.EquipmentFilters.DivisionState:
                    lstEquipInfo =
                        _equipmentInfoRepository.ListAll(
                            e => e.Active
                                && (values.Any(v => e.DivisionStateName.Contains(v))
                                || values.Any(v => e.CoverageDivisionStateName.Contains(v))),
                            w => w.ComboName,
                            true);
                    break;
                case Globals.ResourceAllocation.EquipmentFilters.DivisionStateAcronym:
                    lstEquipInfo =
                        _equipmentInfoRepository.ListAll(
                            e => e.Active
                                && (values.Any(v => e.DivisionState.Contains(v))
                                || values.Any(v => e.CoverageDivisionState.Contains(v))),
                            w => w.ComboName,
                            true);
                    break;
                case Globals.ResourceAllocation.EquipmentFilters.ComboNumber:
                    lstEquipInfo =
                        _equipmentInfoRepository.ListAll(
                            e => e.Active
                                && values.Any(v => e.ComboName.Contains(v)),
                            w => w.ComboName,
                            true);
                    break;
                case Globals.ResourceAllocation.EquipmentFilters.UnitNumber:
                    lstEquipInfo =
                        _equipmentInfoRepository.ListAll(
                            e => e.Active
                                && values.Any(v => e.UnitNumber.Contains(v)),
                            w => w.ComboName,
                            true);
                    break;
                case Globals.ResourceAllocation.EquipmentFilters.Status:
                    lstEquipInfo =
                        _equipmentInfoRepository.ListAll(
                            e => e.Active
                                && values.Any(v => e.Status.Contains(v)),
                            w => w.ComboName,
                            true);
                    break;
                case Globals.ResourceAllocation.EquipmentFilters.JobLocation:
                    lstEquipInfo =
                        _equipmentInfoRepository.ListAll(
                            e => e.Active
                                && values.Any(v => e.JobLocation.Contains(v)),
                            w => w.ComboName,
                            true);
                    break;
                case Globals.ResourceAllocation.EquipmentFilters.CallType:
                    lstEquipInfo =
                        _equipmentInfoRepository.ListAll(
                            e => e.Active
                                && values.Any(v => e.Type.Contains(v)),
                            w => w.ComboName,
                            true);
                    break;
                case Globals.ResourceAllocation.EquipmentFilters.JobNumber:
                    lstEquipInfo =
                        _equipmentInfoRepository.ListAll(
                            e => e.Active
                                && values.Any(v => e.PrefixedNumber.Contains(v)),
                            w => w.ComboName,
                            true);
                    break;
                default:
                    lstEquipInfo = new List<CS_View_EquipmentInfo>();
                    break;
            }

            List<int?> combos = lstEquipInfo.Select(e => e.ComboID).Distinct().ToList();

            List<CS_View_EquipmentInfo> returnList = new List<CS_View_EquipmentInfo>();
            returnList.AddRange(_equipmentInfoRepository.ListAll(e => e.ComboID.HasValue && combos.Contains(e.ComboID)));
            returnList.AddRange(lstEquipInfo.Where(e => !e.ComboID.HasValue));

            return returnList.OrderBy(orderBy).ToList();

        }

        /// <summary>
        /// List Filtered EmployeeInfo from the view CS_View_EmployeeInfo
        /// </summary>
        /// <returns>List of Entities</returns>
        public virtual IList<CS_View_EquipmentInfo> ListFilteredHeavyEquipmentInfo(Globals.ResourceAllocation.EquipmentFilters filter, string value, string orderBy)
        {
            string[] values = value.Split(',');
            for (int i = 0; i < values.Length; i++)
                values[i] = values[i].Trim();

            IList<CS_View_EquipmentInfo> lstEquipInfo = new List<CS_View_EquipmentInfo>();

            switch (filter)
            {
                case Globals.ResourceAllocation.EquipmentFilters.Division:
                    lstEquipInfo =
                        _equipmentInfoRepository.ListAll(
                            e => e.Active
                                && e.HeavyEquipment
                                && values.Any(v => e.DivisionName.Contains(v)),
                            w => w.ComboName,
                            true);
                    break;
                case Globals.ResourceAllocation.EquipmentFilters.DivisionState:
                    lstEquipInfo =
                        _equipmentInfoRepository.ListAll(
                            e => e.Active
                                && e.HeavyEquipment
                                && (values.Any(v => e.DivisionState.Contains(v))
                                || values.Any(v => e.CoverageDivisionState.Contains(v))),
                            w => w.ComboName,
                            true);
                    break;
                case Globals.ResourceAllocation.EquipmentFilters.DivisionStateAcronym:
                    lstEquipInfo =
                        _equipmentInfoRepository.ListAll(
                            e => e.Active
                                && e.HeavyEquipment
                                && (values.Any(v => e.DivisionState.Contains(v))
                                || values.Any(v => e.CoverageDivisionState.Contains(v))),
                            w => w.ComboName,
                            true);
                    break;
                case Globals.ResourceAllocation.EquipmentFilters.ComboNumber:
                    lstEquipInfo =
                        _equipmentInfoRepository.ListAll(
                            e => e.Active
                                && e.HeavyEquipment
                                && values.Any(v => e.ComboName.Contains(v)),
                            w => w.ComboName,
                            true);
                    break;
                case Globals.ResourceAllocation.EquipmentFilters.UnitNumber:
                    lstEquipInfo =
                        _equipmentInfoRepository.ListAll(
                            e => e.Active
                                && e.HeavyEquipment
                                && values.Any(v => e.UnitNumber.Contains(v)),
                            w => w.ComboName,
                            true);
                    break;
                case Globals.ResourceAllocation.EquipmentFilters.Status:
                    lstEquipInfo =
                        _equipmentInfoRepository.ListAll(
                            e => e.Active
                                && e.HeavyEquipment
                                && values.Any(v => e.Status.Contains(v)),
                            w => w.ComboName,
                            true);
                    break;
                case Globals.ResourceAllocation.EquipmentFilters.JobLocation:
                    lstEquipInfo =
                        _equipmentInfoRepository.ListAll(
                            e => e.Active
                                && e.HeavyEquipment
                                && values.Any(v => e.JobLocation.Contains(v)),
                            w => w.ComboName,
                            true);
                    break;
                case Globals.ResourceAllocation.EquipmentFilters.CallType:
                    lstEquipInfo =
                        _equipmentInfoRepository.ListAll(
                            e => e.Active
                                && e.HeavyEquipment
                                && values.Any(v => e.Type.Contains(v)),
                            w => w.ComboName,
                            true);
                    break;
                case Globals.ResourceAllocation.EquipmentFilters.JobNumber:
                    lstEquipInfo =
                        _equipmentInfoRepository.ListAll(
                            e => e.Active
                                && e.HeavyEquipment
                                && values.Any(v => e.PrefixedNumber.Contains(v)),
                            w => w.ComboName,
                            true);
                    break;
                default:
                    lstEquipInfo = new List<CS_View_EquipmentInfo>();
                    break;
            }



            List<int?> combos = lstEquipInfo.Select(e => e.ComboID).Distinct().ToList();

            List<CS_View_EquipmentInfo> returnList = new List<CS_View_EquipmentInfo>();
            returnList.AddRange(_equipmentInfoRepository.ListAll(e => e.ComboID.HasValue && combos.Contains(e.ComboID)));
            returnList.AddRange(lstEquipInfo.Where(e => !e.ComboID.HasValue));

            return returnList.OrderBy(orderBy).ToList();
        }

        /// <summary>
        /// List Filtered EmployeeInfo from the view CS_View_EmployeeInfo
        /// </summary>
        /// <returns>List of Entities</returns>
        public virtual IList<CS_View_EquipmentInfo> ListFilteredEquipmentInfo(Globals.ResourceAllocation.EquipmentFilters filter, string value, List<int> lstEquipmentTypeId, string orderBy)
        {
            string[] values = value.Split(',');
            for (int i = 0; i < values.Length; i++)
                values[i] = values[i].Trim();

            IList<CS_View_EquipmentInfo> lstEquipInfo = new List<CS_View_EquipmentInfo>();

            switch (filter)
            {
                case Globals.ResourceAllocation.EquipmentFilters.Division:
                    lstEquipInfo =
                        _equipmentInfoRepository.ListAll(
                            e => e.Active
                                && values.Any(v => e.DivisionName.Contains(v))
                                && lstEquipmentTypeId.Contains(e.EquipmentTypeID),
                            w => w.ComboName,
                            true);
                    break;
                case Globals.ResourceAllocation.EquipmentFilters.DivisionState:
                    lstEquipInfo =
                        _equipmentInfoRepository.ListAll(
                            e => e.Active
                                && (values.Any(v => e.DivisionState.Contains(v))
                                || values.Any(v => e.CoverageDivisionState.Contains(v)))
                                && lstEquipmentTypeId.Contains(e.EquipmentTypeID),
                            w => w.ComboName,
                            true);
                    break;
                case Globals.ResourceAllocation.EquipmentFilters.ComboNumber:
                    lstEquipInfo =
                        _equipmentInfoRepository.ListAll(
                            e => e.Active
                                && values.Any(v => e.ComboName.Contains(v))
                                && lstEquipmentTypeId.Contains(e.EquipmentTypeID),
                            w => w.ComboName,
                            true);
                    break;
                case Globals.ResourceAllocation.EquipmentFilters.UnitNumber:
                    lstEquipInfo =
                        _equipmentInfoRepository.ListAll(
                            e => e.Active
                                && values.Any(v => e.UnitNumber.Contains(v))
                                && lstEquipmentTypeId.Contains(e.EquipmentTypeID),
                            w => w.ComboName,
                            true);
                    break;
                case Globals.ResourceAllocation.EquipmentFilters.Status:
                    lstEquipInfo =
                        _equipmentInfoRepository.ListAll(
                            e => e.Active
                                && values.Any(v => e.Status.Contains(v))
                                && lstEquipmentTypeId.Contains(e.EquipmentTypeID),
                            w => w.ComboName,
                            true);
                    break;
                case Globals.ResourceAllocation.EquipmentFilters.JobLocation:
                    lstEquipInfo =
                        _equipmentInfoRepository.ListAll(
                            e => e.Active
                                && values.Any(v => e.JobLocation.Contains(v))
                                && lstEquipmentTypeId.Contains(e.EquipmentTypeID),
                            w => w.ComboName,
                            true);
                    break;
                case Globals.ResourceAllocation.EquipmentFilters.CallType:
                    lstEquipInfo =
                        _equipmentInfoRepository.ListAll(
                            e => e.Active
                                && values.Any(v => e.Type.Contains(v))
                                && lstEquipmentTypeId.Contains(e.EquipmentTypeID),
                            w => w.ComboName,
                            true);
                    break;
                default:
                    lstEquipInfo = _equipmentInfoRepository.ListAll(w => w.Active && lstEquipmentTypeId.Contains(w.EquipmentTypeID));
                    break;
            }
            return lstEquipInfo.OrderBy(orderBy).ToList();

        }

        /// <summary>
        ///List all reserved equipments
        /// </summary>
        /// <returns>IList<CS_View_ReserveEquipment></returns>
        public IList<CS_View_ReserveEquipment> ListAllEquipments()
        {
            return _viewReserveEquipmentRepository.ListAll();
        }

        /// <summary>
        /// List all equipments
        /// </summary>
        /// <returns></returns>
        public IList<CS_Equipment> ListAllEquipment()
        {
            return _equipmentRepository.ListAll(w => w.Active);
        }

        /// <summary>
        ///List all equipments from the EquipmentInfoView
        /// </summary>
        /// <returns>IList<CS_View_EquipmentInfo></returns>
        public IList<CS_View_EquipmentInfo> ListAllHeavyCombo()
        {
            List<CS_View_EquipmentInfo> lstEquipInfo = _equipmentInfoRepository.ListAll(w => w.Active && w.HeavyEquipment, w => w.ComboName, true).ToList();

            List<int?> combos = lstEquipInfo.Select(e => e.ComboID).Distinct().ToList();

            List<CS_View_EquipmentInfo> returnList = new List<CS_View_EquipmentInfo>();
            returnList.AddRange(_equipmentInfoRepository.ListAll(e => e.ComboID.HasValue && combos.Contains(e.ComboID)));
            returnList.AddRange(lstEquipInfo.Where(e => !e.ComboID.HasValue));

            return returnList.OrderBy(w => w.DivisionName).ThenBy(w => w.ComboName).ThenBy(w => w.UnitNumber).ToList();
        }

        /// <summary>
        ///List all equipments from the EquipmentInfoView
        /// </summary>
        /// <returns>IList<CS_View_EquipmentInfo></returns>
        public IList<CS_View_EquipmentInfo> ListAllHeavyCombo(string orderBy)
        {
            return _equipmentInfoRepository.ListAll(w => w.Active && w.HeavyEquipment, w => w.ComboName, true).OrderBy(orderBy).ToList();
        }

        /// <summary>
        ///List all equipments from the EquipmentInfoView
        /// </summary>
        /// <returns>IList<CS_View_EquipmentInfo></returns>
        public IList<CS_View_EquipmentInfo> ListAllHeavyComboOrdered()
        {
            return _equipmentInfoRepository.ListAll(w => w.Active && w.HeavyEquipment, w => w.ComboName, true).OrderBy(e => e.ComboName == null).ThenBy(e => e.ComboName).ThenBy(e => e.DivisionID).ThenBy(e => e.UnitNumber).ThenBy(e => e.Descriptor).Take(4000).ToList();
        }

        /// <summary>
        ///List all equipments from the EquipmentInfoView
        /// </summary>
        /// <returns>IList<CS_View_EquipmentInfo></returns>
        public IList<CS_View_EquipmentInfo> ListAllHeavyComboByEquipmentTypeList(List<int> lstEquipmentId)
        {
            return _equipmentInfoRepository.ListAll(w => w.Active && w.HeavyEquipment && lstEquipmentId.Contains(w.EquipmentTypeID), w => w.ComboName, true).ToList();
        }

        /// <summary>
        /// List all Equipments with Combo Information
        /// </summary>
        /// <returns></returns>
        public virtual IList<CS_View_EquipmentInfo> ListAllCombo()
        {
            return _equipmentInfoRepository.ListAll(e => e.Active);
        }

        /// <summary>
        /// List all Equipments with Combo Information By Job
        /// </summary>
        /// <returns></returns>
        public virtual IList<CS_View_EquipmentInfo> ListAllComboByJob(int jobID)
        {
            return _equipmentInfoRepository.ListAll(e => e.Active && e.JobID == jobID);
        }

        /// <summary>
        /// List all Equipments with Combo information, filtered by division
        /// </summary>
        /// <param name="lstDivisionId"></param>
        /// <returns></returns>
        public IList<CS_View_EquipmentInfo> ListAllComboByDivisionList(List<int> lstDivisionId)
        {
            return _equipmentInfoRepository.ListAll(w => w.Active && (lstDivisionId.Contains(w.DivisionID) || (w.CoverageDivisionID.HasValue && lstDivisionId.Contains(w.CoverageDivisionID.Value))));
        }

        /// <summary>
        /// List all Equipments with Combo information, filtered by division and EquipmentType
        /// </summary>
        /// <param name="lstDivisionId"></param>
        /// <returns></returns>
        public IList<CS_View_EquipmentInfo> ListAllComboByDivisionListAndEquipmentTypeList(List<int> lstDivisionId, List<int> lstEquipmentTypeId)
        {
            return _equipmentInfoRepository.ListAll(w => w.Active && (lstDivisionId.Contains(w.DivisionID) || (w.CoverageDivisionID.HasValue && lstDivisionId.Contains(w.CoverageDivisionID.Value))) && lstEquipmentTypeId.Contains(w.EquipmentTypeID));
        }

        /// <summary>
        ///List all equipments from the EquipmentInfoView
        /// </summary>
        /// <returns>Equipment Info List</returns>
        public virtual IList<CS_View_SecondaryEquipmentInfo> ListAllDetailedEquipmentInfo(int comboId)
        {
            return _secondaryEquipmentInfoRepository.ListAll(e => e.Active && e.ComboID == comboId);
        }

        /// <summary>
        /// Gets Equipment Info via Id
        /// </summary>
        /// <param name="equipmentId">Equipment Identifier</param>
        /// <returns>Equipment Information</returns>
        public CS_Equipment GetEquipment(int equipmentId)
        {
            return _equipmentRepository.Get(w => w.ID == equipmentId, "CS_Division", "CS_Division.CS_State");
        }

        /// <summary>
        /// Gets Equipment Info via Id
        /// </summary>
        /// <param name="equipmentId">Equipment Identifier</param>
        /// <returns>Equipment Information</returns>
        public CS_Equipment GetEquipmentForMaintenance(int equipmentId)
        {
            return _equipmentRepository.Get(w => w.ID == equipmentId, "CS_Division", "CS_Division.CS_State", "CS_Resource", "CS_EquipmentCoverage", "CS_EquipmentDownHistory", "CS_EquipmentWhiteLight");
        }

        /// <summary>
        /// Gets Equipment Info via Id
        /// </summary>
        /// <param name="equipmentId">Equipment Identifier</param>
        /// <returns>Equipment Information</returns>
        public CS_Equipment GetEquipmentDetailed(int equipmentId)
        {
            return _equipmentRepository.Get(w => w.ID == equipmentId, new string[] { "CS_Division" });
        }

        /// <summary>
        /// Gets Local Equipment Type info via Id
        /// </summary>
        /// <param name="equipmentTypeId">Equipment Type Identifier</param>
        /// <returns>Local Equipment Type information</returns>
        public CS_LocalEquipmentType GetLocalEquipmentTypeByID(int localEquipmentTypeId)
        {
            return _localEquipmentTypeRepository.Get(w => w.ID == localEquipmentTypeId && w.Active);
        }

        /// <summary>
        /// Gets Equipment Type info via Id
        /// </summary>
        /// <param name="equipmentTypeId">Equipment Type Identifier</param>
        /// <returns>Equipment Type information</returns>
        public CS_EquipmentType GetEquipmentType(int equipmentTypeId)
        {
            return _equipmentTypeRepository.Get(w => w.ID == equipmentTypeId && w.Active);
        }

        /// <summary>
        /// Get the equipment by equipmetn type
        /// </summary>
        /// <param name="equipmentTypeId"></param>
        /// <returns></returns>
        public IList<CS_Equipment> GetEquipmentByEqType(int equipmentTypeId)
        {
            return _equipmentRepository.ListAll(w => w.Active && w.EquipmentTypeID == equipmentTypeId).ToList();
        }

        /// <summary>
        /// Get a Specific Combo or Lone Unit from the Primary Equipments View
        /// </summary>
        /// <param name="equipmentId">ID of the equipment</param>
        /// <returns>Entity with equipment info</returns>
        public CS_View_EquipmentInfo GetSpecificEquipmentFromView(int equipmentId)
        {
            return _equipmentInfoRepository.Get(e => e.EquipmentID == equipmentId && e.Active);
        }

        /// <summary>
        /// Get a Specific Combo Unit from the Secondary Equipments View
        /// </summary>
        /// <param name="equipmentId">ID of the equipment</param>
        /// <returns>Entity with equipment info</returns>
        public CS_View_SecondaryEquipmentInfo GetSpecificSecondaryEquipmentFromView(int equipmentId)
        {
            return _secondaryEquipmentInfoRepository.Get(e => e.EquipmentID == equipmentId && e.Active);
        }

        /// <summary>
        /// Gets a Specific Combo
        /// </summary>
        /// <param name="equipmentComboId">Combo Identifier</param>
        /// <returns>Combo Information</returns>
        public virtual CS_EquipmentCombo GetCombo(int equipmentComboId)
        {
            return _equipmentComboRepository.Get(e => e.ID == equipmentComboId);
        }

        /// <summary>
        /// Saves a Equipment Combo Entity in the Database
        /// </summary>
        /// <param name="equipmentCombo">New Equipment Combo Entity</param>
        /// <param name="equipmentList">Equipments inside Combo</param>
        /// <param name="username">Username that is executing the method</param>
        public void SaveCombo(CS_EquipmentCombo equipmentCombo, IList<EquipmentComboVO> equipmentList, string username)
        {
            if (null == equipmentCombo)
                throw new Exception("The Equipment Combo object is null!");

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    equipmentCombo.CreatedBy = username;
                    equipmentCombo.CreationDate = DateTime.Now;
                    equipmentCombo.ModifiedBy = username;
                    equipmentCombo.ModificationDate = DateTime.Now;
                    equipmentCombo.Active = true;

                    equipmentCombo = _equipmentComboRepository.Add(equipmentCombo);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error while trying to save Combo information!", ex);
                }

                try
                {
                    if (null != equipmentList)
                    {
                        for (int i = 0; i < equipmentList.Count; i++)
                        {
                            equipmentList[i].ComboId = equipmentCombo.ID;
                            UpdateEquipmentCombo(equipmentList[i].EquipmentId, equipmentCombo.ID, username);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error while trying to save Equipment Combo information!", ex);
                }

                scope.Complete();
            }
        }

        /// <summary>
        /// Updates a Equipment Combo Entity in the Database
        /// </summary>
        /// <param name="equipmentCombo">Equipment Combo Entity</param>
        /// <param name="equipmentList">Equipments inside Combo</param>
        /// <param name="username">Username that is executing the method</param>
        public void UpdateCombo(CS_EquipmentCombo equipmentCombo, IList<EquipmentComboVO> equipmentList, string username)
        {
            if (null == equipmentCombo)
                throw new Exception("The Equipment Combo object is null!");

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    equipmentCombo.ModifiedBy = username;
                    equipmentCombo.ModificationDate = DateTime.Now;
                    equipmentCombo.Active = true;

                    equipmentCombo = _equipmentComboRepository.Update(equipmentCombo);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error while trying to save Combo information!", ex);
                }

                try
                {
                    if (null != equipmentList)
                    {
                        List<EquipmentComboVO> oldList = ListEquipmentsOfACombo(equipmentCombo.ID);
                        List<EquipmentComboVO> removedList = oldList.Where(e => !equipmentList.Any(f => f.EquipmentId == e.EquipmentId)).ToList();
                        List<EquipmentComboVO> addedList = equipmentList.Where(e => !oldList.Any(f => f.EquipmentId == e.EquipmentId)).ToList();

                        for (int i = 0; i < addedList.Count; i++)
                            UpdateEquipmentCombo(addedList[i].EquipmentId, equipmentCombo.ID, username);

                        for (int i = 0; i < removedList.Count; i++)
                            UpdateEquipmentCombo(removedList[i].EquipmentId, null, username);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error while trying to save Equipment Combo information!", ex);
                }

                scope.Complete();
            }
        }

        /// <summary>
        /// Updates information of an equipment inside a combo
        /// </summary>
        /// <param name="equipmentId">Equipment Identifier</param>
        /// <param name="comboId">Combo Identifier (Null if the equipment is being removed from a combo)</param>
        /// <param name="username">Username that executed the function</param>
        private void UpdateEquipmentCombo(int equipmentId, int? comboId, string username)
        {
            CS_Equipment equipmentToUpdate = _equipmentRepository.Get(e => e.ID == equipmentId);
            if (null != equipmentToUpdate)
            {
                equipmentToUpdate.ComboID = comboId;
                equipmentToUpdate.ModificationDate = DateTime.Now;
                equipmentToUpdate.ModifiedBy = username;

                _equipmentRepository.Update(equipmentToUpdate);
            }
        }

        /// <summary>
        /// Method that list all equipments of a combo
        /// </summary>
        /// <param name="equipmentComboId">comboid</param>
        /// <returns>list</returns>
        public List<CS_Equipment> ListEquipmentsOfAComboForDelete(int equipmentComboId)
        {
            CS_EquipmentCombo combo = _equipmentComboRepository.Get(e => e.ID == equipmentComboId);

            if (null != combo)
            {
                return _equipmentRepository.ListAll(e => e.ComboID == equipmentComboId && e.Active).ToList();
            }

            return null;
        }

        /// <summary>
        /// Returns a list of all equipments inside a combo
        /// </summary>
        /// <param name="equipmentComboId">EquipmentCombo Identifier</param>
        /// <returns>Equipment List</returns>
        public virtual List<EquipmentComboVO> ListEquipmentsOfACombo(int equipmentComboId)
        {
            List<EquipmentComboVO> returnList = new List<EquipmentComboVO>();

            CS_EquipmentCombo combo = _equipmentComboRepository.Get(e => e.ID == equipmentComboId);
            if (null != combo)
            {
                IList<CS_Equipment> equipmentsInsideACombo = _equipmentRepository.ListAll(e => e.ComboID == equipmentComboId && e.Active);
                foreach (CS_Equipment equipment in equipmentsInsideACombo)
                {
                    returnList.Add(
                        new EquipmentComboVO()
                        {
                            ComboId = equipmentComboId,
                            EquipmentId = equipment.ID,
                            DivisionNumber = equipment.DivisionName,
                            UnitNumber = equipment.Name,
                            Descriptor = equipment.Description,
                            IsPrimary = (combo.PrimaryEquipmentID == equipment.ID),
                            Seasonal = equipment.Seasonal
                        });
                }
            }
            return returnList;
        }

        /// <summary>
        /// Returns a equipment
        /// </summary>
        /// <param name="equipmentId">Equipment Identifier</param>
        /// <returns>Equipment List</returns>
        public virtual EquipmentComboVO GetEquipmentOfACombo(int equipmentId)
        {
            EquipmentComboVO returnEntity = null;

            CS_Equipment equipment = _equipmentRepository.Get(e => e.ID == equipmentId);
            if (null != equipment)
            {
                returnEntity = new EquipmentComboVO()
                {
                    EquipmentId = equipment.ID,
                    DivisionNumber = equipment.DivisionName,
                    UnitNumber = equipment.Name,
                    Descriptor = equipment.Description,
                    IsPrimary = false,
                    Seasonal = equipment.Seasonal
                };
            }

            return returnEntity;
        }

        /// <summary>
        /// Deletes the equipmentcombo ( inactivates )
        /// </summary>
        /// <param name="equipmentComboId">equipmentComboId</param>
        /// <param name="userName">userName</param>
        public bool DeleteEquipmentCombo(int equipmentComboId, string userName)
        {
            if (equipmentComboId != 0)
            {
                if (!CheckEquipmentIsAssignedJob(equipmentComboId))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        CS_EquipmentCombo equipmentCombo = _equipmentComboRepository.Get(w => w.ID == equipmentComboId);

                        if (null != equipmentCombo)
                        {
                            equipmentCombo.ModificationDate = DateTime.Now;
                            equipmentCombo.ModifiedBy = userName;
                            equipmentCombo.Active = false;

                            _equipmentComboRepository.Update(equipmentCombo);

                            List<EquipmentComboVO> equipmentList = ListEquipmentsOfACombo(equipmentCombo.ID);
                            for (int i = 0; i < equipmentList.Count; i++)
                                UpdateEquipmentCombo(equipmentList[i].EquipmentId, null, userName);
                        }

                        scope.Complete();
                    }

                    return true;
                }
            }

            return false;
        }

        public bool CheckEquipmentIsAssignedJob(int equipmentComboId)
        {
            if (equipmentComboId > 0)
            {
                List<CS_Equipment> equipmentList = ListEquipmentsOfAComboForDelete(equipmentComboId);

                List<int> EqIds = new List<int>();

                for (int i = 0; i < equipmentList.Count; i++)
                {
                    EqIds.Add(equipmentList[i].ID);
                }

                return CheckEquipmentIsAssignedJob(EqIds);
            }

            return false;
        }

        /// <summary>
        /// Update the maintenance equipment
        /// </summary>
        /// <param name="equipment">entity CS_Equipment</param>
        public void UpdateMaintenanceEquipment(CS_Equipment equipment)
        {
            if (null == equipment)
                throw new Exception("The Equipment object is null!");

            try
            {
                _equipmentRepository.Update(equipment);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while trying to update the maintenance equipment.", ex);
            }
        }

        /// <summary>
        /// Save the EquipmentCoverage
        /// </summary>
        /// <param name="equipmentCoverage">entity equipmentcoverage</param>
        public void SaveEquipmentCoverage(CS_EquipmentCoverage equipmentCoverage)
        {
            if (null == equipmentCoverage)
                throw new Exception("The Equipment Coverage object is null.");

            try
            {
                _equipmentCoverageRepository.Add(equipmentCoverage);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while trying to save the equipment coverage object", ex);
            }
        }

        /// <summary>
        /// Save the equipmentdownhistory
        /// </summary>
        /// <param name="equipmentDownHistory">entity equipmentdownhistory</param>
        public void SaveEquipmentDownHistory(CS_EquipmentDownHistory equipmentDownHistory)
        {
            if (null == equipmentDownHistory)
                throw new Exception("The EquipmentDownHistory object is null.");

            try
            {
                _equipmentDownHistoryRepository.Add(equipmentDownHistory);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while trying to save the equipment down history object.", ex);
            }
        }

        /// <summary>
        /// Returns a list of equipments based on the filters
        /// </summary>
        /// <param name="filterType">Filter Type</param>
        /// <param name="filterValue">Filter Value</param>
        /// <returns>List of equipments</returns>
        public IList<CS_View_EquipmentInfo> ListFilteredEquipment(Globals.EquipmentMaintenance.FilterType? filterType, string filterValue)
        {
            if (filterType.HasValue && !string.IsNullOrEmpty(filterValue))
            {
                string[] filterValueSplit = filterValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < filterValueSplit.Length; i++)
                    filterValueSplit[i] = filterValueSplit[i].Trim();

                switch (filterType.Value)
                {
                    case Globals.EquipmentMaintenance.FilterType.Division:
                        return _equipmentInfoRepository.ListAll(
                            e => e.Active &&
                                 filterValueSplit.Any(v => e.DivisionName.Contains(v)));
                    case Globals.EquipmentMaintenance.FilterType.DivisionState:
                        return _equipmentInfoRepository.ListAll(
                            e => e.Active &&
                                 (filterValueSplit.Any(v => e.DivisionState.Contains(v))
                                 || filterValueSplit.Any(v => e.CoverageDivisionState.Contains(v))));
                    case Globals.EquipmentMaintenance.FilterType.ComboName:
                        return _equipmentInfoRepository.ListAll(
                            e => e.Active &&
                                 filterValueSplit.Any(v => e.ComboName.Contains(v)));
                    case Globals.EquipmentMaintenance.FilterType.UnitNumber:
                        return _equipmentInfoRepository.ListAll(
                            e => e.Active &&
                                 filterValueSplit.Any(v => e.UnitNumber.Contains(v)));
                    case Globals.EquipmentMaintenance.FilterType.Status:
                        return _equipmentInfoRepository.ListAll(
                            e => e.Active &&
                                 filterValueSplit.Any(v => e.Status.Contains(v)));
                    case Globals.EquipmentMaintenance.FilterType.JobLocation:
                        return _equipmentInfoRepository.ListAll(
                            e => e.Active &&
                                 filterValueSplit.Any(v => e.JobLocation.Contains(v)));
                    case Globals.EquipmentMaintenance.FilterType.CallType:
                        return _equipmentInfoRepository.ListAll(
                            e => e.Active &&
                                 filterValueSplit.Any(v => e.Type.Contains(v)));
                    case Globals.EquipmentMaintenance.FilterType.JobNumber:
                        return _equipmentInfoRepository.ListAll(
                            e => e.Active &&
                                 filterValueSplit.Any(v => e.JobNumber.Contains(v)));
                    default:
                        return new List<CS_View_EquipmentInfo>();
                }
            }
            else
                return new List<CS_View_EquipmentInfo>();
        }

        /// <summary>
        /// Get the equipmentdownhistory by equipemntid
        /// </summary>
        /// <param name="equipmentID">equipment id</param>
        /// <returns></returns>
        public CS_EquipmentDownHistory GetEquipmentDownHistory(int equipmentID)
        {
            CS_EquipmentDownHistory equipmentDownHistory = _equipmentDownHistoryRepository.Get(w => w.Active && w.EquipmentID == equipmentID);

            return equipmentDownHistory;
        }

        /// <summary>
        /// Update the equipmentdownhistory 
        /// </summary>
        /// <param name="equipmentDownHistory">equipmentdownhistory entity</param>
        public void UpdateEquipmentDownHistory(CS_EquipmentDownHistory equipmentDownHistory)
        {
            if (null == equipmentDownHistory)
                throw new Exception("The EquipmentDownHistory object is null.");

            try
            {
                _equipmentDownHistoryRepository.Update(equipmentDownHistory);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while trying to save the equipment down history object.", ex);
            }
        }

        public void UpdateEquipmentDisplay(List<int> lstHeavyEquipment, List<int> lstDisplayInResource, IList<CS_View_EquipmentInfo> lstEquipmentInfo, string userName)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //Return list of equipmentid in a list of int
                List<int> lstEquipmentIds = lstEquipmentInfo.Select(e => e.EquipmentID).ToList();

                //Return list of equipment
                List<CS_Equipment> lstEquipment = _equipmentRepository.ListAll(e => e.Active && lstEquipmentIds.Contains(e.ID)).ToList();

                // Identify items that were changed
                List<CS_Equipment> changedEquipments = lstEquipment.Where(e =>
                    (e.HeavyEquipment && !lstHeavyEquipment.Contains(e.ID)) ||
                    (!e.HeavyEquipment && lstHeavyEquipment.Contains(e.ID)) ||
                    (e.DisplayInResourceAllocation && !lstDisplayInResource.Contains(e.ID)) ||
                    (!e.DisplayInResourceAllocation && lstDisplayInResource.Contains(e.ID))).ToList();

                // Change attributes of the changed equipments
                for (int i = 0; i < changedEquipments.Count; i++)
                {
                    if (lstHeavyEquipment.Contains(changedEquipments[i].ID))
                        changedEquipments[i].HeavyEquipment = true;
                    else
                        changedEquipments[i].HeavyEquipment = false;

                    if (lstDisplayInResource.Contains(changedEquipments[i].ID))
                        changedEquipments[i].DisplayInResourceAllocation = true;
                    else
                        changedEquipments[i].DisplayInResourceAllocation = false;

                    changedEquipments[i].ModificationDate = DateTime.Now;
                    changedEquipments[i].ModifiedBy = userName;
                }

                // Updates all changed equipments
                _equipmentRepository.UpdateList(changedEquipments);

                scope.Complete();
            }
        }

        public void SaveEquipment(CS_Equipment equipment, CS_EquipmentDownHistory equipmentDownHistory, bool? isHeavyEquipment)
        {
            CS_Equipment equip = GetEquipment(equipment.ID);

            equip.ModificationDate = equipment.ModificationDate;
            equip.ModifiedBy = equipment.ModifiedBy;

            if (isHeavyEquipment.HasValue)
                equip.HeavyEquipment = isHeavyEquipment.Value;

            if (equipment.Status == Globals.EquipmentMaintenance.Status.Down.ToString())
            {
                equip.Status = Globals.EquipmentMaintenance.Status.Down.ToString();
            }
            else
            {
                equip.Status = Globals.EquipmentMaintenance.Status.Up.ToString();
            }

            //Update the maintenance equipment
            UpdateMaintenanceEquipment(equip);

            //Update the equipmentdown
            UpdateEquipmentDown(equipmentDownHistory, equip);
        }

        public void SaveEquipment(CS_Equipment equipment, CS_EquipmentDownHistory equipmentDownHistory, IList<CS_EquipmentDownHistory> equipmentComboDownHistory, bool? isHeavyEquipment, bool? isSeasonal, bool? displayInResourceAllocation, bool? replicateToCombo)
        {
            CS_Equipment equip = GetEquipment(equipment.ID);

            equip.ModificationDate = equipment.ModificationDate;
            equip.ModifiedBy = equipment.ModifiedBy;

            if (isSeasonal.HasValue)
                equip.Seasonal = isSeasonal.Value;

            if (isHeavyEquipment.HasValue)
                equip.HeavyEquipment = isHeavyEquipment.Value;

            if (displayInResourceAllocation.HasValue)
                equip.DisplayInResourceAllocation = displayInResourceAllocation.Value;

            if (equipment.Status == Globals.EquipmentMaintenance.Status.Down.ToString())
            {
                equip.Status = Globals.EquipmentMaintenance.Status.Down.ToString();
            }
            else
            {
                equip.Status = Globals.EquipmentMaintenance.Status.Up.ToString();
            }

            //Update the maintenance equipment
            UpdateMaintenanceEquipment(equip);

            //Update the equipmentdown
            UpdateEquipmentDown(equipmentDownHistory, equip);

            if (replicateToCombo.HasValue && replicateToCombo.Value)
            {
                CS_View_EquipmentInfo eqInfo = _equipmentInfoRepository.Get(e => e.EquipmentID == equipment.ID);
                if (eqInfo.IsPrimary == 1 && eqInfo.ComboID.HasValue)
                {
                    IList<CS_Equipment> equips = ListEquipmentsFromPrimaryEquipment(eqInfo.EquipmentID, eqInfo.ComboID.Value);
                    for (int j = 0; j < equips.Count; j++)
                    {
                        if (equipment.ID != equips[j].ID)
                        {
                            equips[j].ModificationDate = equipment.ModificationDate;
                            equips[j].ModifiedBy = equipment.ModifiedBy;

                            if (isHeavyEquipment.HasValue)
                                equips[j].HeavyEquipment = isHeavyEquipment.Value;

                            if (displayInResourceAllocation.HasValue)
                                equips[j].DisplayInResourceAllocation = displayInResourceAllocation.Value;

                            if (equipment.Status == Globals.EquipmentMaintenance.Status.Down.ToString())
                            {
                                equips[j].Status = Globals.EquipmentMaintenance.Status.Down.ToString();
                            }
                            else
                            {
                                equips[j].Status = Globals.EquipmentMaintenance.Status.Up.ToString();
                            }

                            //Update the maintenance equipment
                            UpdateMaintenanceEquipment(equips[j]);

                            CS_EquipmentDownHistory hist = equipmentComboDownHistory.Where(e => e.EquipmentID == equips[j].ID).First();

                            //Update the equipmentdown
                            UpdateEquipmentDown(hist, equips[j]);
                        }
                    }
                }
            }
        }

        public void UpdateEquipmentDown(CS_EquipmentDownHistory equipmentDownHistory, CS_Equipment equipment)
        {
            CS_EquipmentDownHistory eDownHistory = GetEquipmentDownHistory(equipment.ID);

            if (equipment.Status == Globals.EquipmentMaintenance.Status.Down.ToString())
            {
                if (null != equipmentDownHistory)
                {
                    if (null != eDownHistory)
                    {
                        eDownHistory.DownHistoryStartDate = equipmentDownHistory.DownHistoryStartDate;
                        eDownHistory.Duration = equipmentDownHistory.Duration;
                        eDownHistory.ModificationDate = DateTime.Now;
                        eDownHistory.ModifiedBy = equipmentDownHistory.ModifiedBy;

                        //Update the equipmentdownhistory
                        UpdateEquipmentDownHistory(eDownHistory);
                    }
                    else
                    {
                        //Save the equipmentdownhistory
                        equipmentDownHistory.Active = true;
                        equipmentDownHistory.CreationDate = DateTime.Now;
                        equipmentDownHistory.CreatedBy = equipmentDownHistory.ModifiedBy;
                        equipmentDownHistory.DownHistoryEndDate = null;

                        SaveEquipmentDownHistory(equipmentDownHistory);
                    }
                }
            }
            else
            {
                if (null != eDownHistory)
                {
                    eDownHistory.Active = false;
                    eDownHistory.DownHistoryEndDate = equipmentDownHistory.DownHistoryEndDate;
                    eDownHistory.ModifiedBy = equipment.ModifiedBy;

                    //Update the maintenance equipmentdownhistory
                    UpdateEquipmentDownHistory(eDownHistory);
                }
            }
        }

        public CS_View_EquipmentInfo GetEquipmentInfoByEquipmentID(int equipmentID)
        {
            CS_View_EquipmentInfo eqInfo = _equipmentInfoRepository.Get(e => e.EquipmentID == equipmentID);

            return eqInfo;
        }

        public void SaveEquipment(CS_Equipment equipment, CS_EquipmentCoverage equipmentCoverage, CS_EquipmentWhiteLight equipmentWhiteLight, IList<CS_EquipmentWhiteLight> comboEquipmentsWhiteLight, CS_EquipmentDownHistory equipmentDownHistory, IList<CS_EquipmentDownHistory> equipmentComboDownHistory, bool isHeavyEquipment, bool isSeasonal, bool displayInResourceAllocation, bool replicateToCombo, bool IsEquipmentCoverage, bool IsEquipmentWhiteLight, string username)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                #region [ Equipment/EquipmentDown ]
                SaveEquipment(equipment, equipmentDownHistory, equipmentComboDownHistory, isHeavyEquipment, isSeasonal, displayInResourceAllocation, replicateToCombo);
                #endregion

                #region [ EquipmentCoverage ]

                CS_EquipmentCoverage eCoverage = GetEquipmentCoverage(equipment.ID);

                if (IsEquipmentCoverage)
                {
                    if (null != eCoverage)
                    {
                        eCoverage.CoverageStartDate = equipmentCoverage.CoverageStartDate;
                        eCoverage.DivisionID = equipmentCoverage.DivisionID;
                        eCoverage.Duration = equipmentCoverage.Duration;
                        eCoverage.ModifiedBy = equipmentCoverage.ModifiedBy;
                        eCoverage.ModificationDate = equipmentCoverage.ModificationDate;

                        //Update equipmentcoverage
                        UpdateEquipmentCoverage(eCoverage);
                    }
                    else
                    {
                        //Save equipmentcoverage
                        SaveEquipmentCoverage(equipmentCoverage);

                        using (_callLogModel = new CallLogModel())
                        {
                            _callLogModel.CreateCoverageCallLogs(equipment.ID, equipmentCoverage, username);
                        }
                    }
                }
                else
                {
                    if (null != eCoverage)
                    {
                        eCoverage.Active = false;
                        eCoverage.CoverageStartDate = equipmentCoverage.CoverageStartDate;
                        eCoverage.DivisionID = equipmentCoverage.DivisionID;
                        eCoverage.Duration = equipmentCoverage.Duration;
                        if (equipmentCoverage.CoverageEndDate.HasValue)
                        {
                            eCoverage.CoverageEndDate = equipmentCoverage.CoverageEndDate.Value;
                            eCoverage.ModificationDate = equipmentCoverage.CoverageEndDate.Value;
                        }
                        eCoverage.ModifiedBy = equipmentCoverage.ModifiedBy;

                        //Update equipmentcoverage when user uncheck the coverage checkbox
                        UpdateEquipmentCoverage(eCoverage);
                    }
                }

                #endregion

                #region [ WhiteLight ]

                CS_EquipmentWhiteLight eWhiteLight = GetEquipmentWhiteLight(equipment.ID);

                if (null != eWhiteLight)
                {
                    if (!IsEquipmentWhiteLight)
                    {
                        equipmentWhiteLight.ID = eWhiteLight.ID;
                        equipmentWhiteLight.WhiteLightEndDate = DateTime.Now;
                        equipmentWhiteLight.Active = false;
                        equipmentWhiteLight.ModificationDate = DateTime.Now;
                        equipmentWhiteLight.ModifiedBy = equipment.ModifiedBy;
                        if (!string.IsNullOrEmpty(equipmentWhiteLight.Notes))
                            equipmentWhiteLight.Notes = eWhiteLight.Notes + "\n" + equipmentWhiteLight.Notes;

                        UpdateEquipmentWhiteLight(equipmentWhiteLight);
                        if (replicateToCombo)
                        {
                            for (int i = 0; i < comboEquipmentsWhiteLight.Count; i++)
                            {
                                CS_EquipmentWhiteLight eWhiteLightCombo = GetEquipmentWhiteLight(comboEquipmentsWhiteLight[i].EquipmentID);
                                comboEquipmentsWhiteLight[i].ID = eWhiteLightCombo.ID;
                                comboEquipmentsWhiteLight[i].WhiteLightEndDate = DateTime.Now;
                                comboEquipmentsWhiteLight[i].Active = false;
                                comboEquipmentsWhiteLight[i].ModificationDate = DateTime.Now;
                                comboEquipmentsWhiteLight[i].ModifiedBy = equipment.ModifiedBy;
                                if (!string.IsNullOrEmpty(comboEquipmentsWhiteLight[i].Notes))
                                    comboEquipmentsWhiteLight[i].Notes = eWhiteLight.Notes + "\r\n" + comboEquipmentsWhiteLight[i].Notes;
                                UpdateEquipmentWhiteLight(comboEquipmentsWhiteLight[i]);
                            }
                        }
                    }
                }
                else
                {
                    if (IsEquipmentWhiteLight)
                    {
                        SaveEquipmentWhiteLight(equipmentWhiteLight);
                        if (replicateToCombo)
                        {
                            for (int i = 0; i < comboEquipmentsWhiteLight.Count; i++)
                            {
                                SaveEquipmentWhiteLight(comboEquipmentsWhiteLight[i]);
                            }
                        }
                    }
                }
                #endregion

                scope.Complete();
            }

        }

        /// <summary>
        /// Save the new object CS_EquipmentWhiteLight on the DB
        /// </summary>
        /// <param name="equipmentWhiteLight"></param>
        public void UpdateEquipmentWhiteLight(CS_EquipmentWhiteLight equipmentWhiteLight)
        {
            if (null == equipmentWhiteLight)
                throw new Exception("The equipmentWhiteLight object is null.");

            try
            {
                _equipmentWhiteLightRepository.Update(equipmentWhiteLight);

                int? jobId = this.GetJobIdAssignedToEquipment(equipmentWhiteLight.EquipmentID);

                if (jobId.HasValue)
                {
                    SaveWhiteLightCallLog(equipmentWhiteLight, jobId.Value, false);
                }
                else
                {
                    SaveWhiteLightCallLog(equipmentWhiteLight, Globals.GeneralLog.ID, false);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while trying to save the equipmentwhitelight object.", ex);
            }
        }

        /// <summary>
        /// Update the equipmentWhiteLight entity on de DB
        /// </summary>
        /// <param name="equipmentWhiteLight"></param>
        public void SaveEquipmentWhiteLight(CS_EquipmentWhiteLight equipmentWhiteLight)
        {
            if (null == equipmentWhiteLight)
                throw new Exception("The equipmentWhiteLight object is null.");

            try
            {
                _equipmentWhiteLightRepository.Add(equipmentWhiteLight);

                int? jobId = this.GetJobIdAssignedToEquipment(equipmentWhiteLight.EquipmentID);

                if (jobId.HasValue)
                {
                    SaveWhiteLightCallLog(equipmentWhiteLight, jobId.Value, true);
                }
                else
                {
                    SaveWhiteLightCallLog(equipmentWhiteLight, Globals.GeneralLog.ID, true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while trying to save the equipmentwhitelight object.", ex);
            }
        }

        public void SaveWhiteLightCallLog(CS_EquipmentWhiteLight equipmentWhiteLight, int jobID, bool add)
        {
            try
            {

                string notes = BuildWhiteLogNote(equipmentWhiteLight, add);
                CS_CallLog newCallEntry = new CS_CallLog();
                newCallEntry.JobID = jobID;
                newCallEntry.CallTypeID = (int)Globals.CallEntry.CallType.WhiteLight;

                if (jobID == Globals.GeneralLog.ID)
                    newCallEntry.PrimaryCallTypeId = (int)Globals.CallEntry.PrimaryCallType.ResourceUpdate;
                else
                    newCallEntry.PrimaryCallTypeId = (int)Globals.CallEntry.PrimaryCallType.ResourceUpdateEventStatus;

                newCallEntry.CallDate = equipmentWhiteLight.CreationDate;
                DateTimeOffset dateTimeOffset = DateTimeOffset.Now;
                newCallEntry.Xml = null;
                newCallEntry.Note = notes;
                newCallEntry.CreatedBy = equipmentWhiteLight.CreatedBy;
                newCallEntry.CreationDate = DateTime.Now;
                newCallEntry.ModifiedBy = equipmentWhiteLight.CreatedBy;
                newCallEntry.ModificationDate = DateTime.Now;
                newCallEntry.Active = true;
                newCallEntry.UserCall = true;

                newCallEntry = _callLogRepository.Add(newCallEntry);

                SaveWhiteLightCallLogResources(newCallEntry, equipmentWhiteLight);
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error while trying to save a new call entry.", ex);
            }
        }

        public void SaveWhiteLightCallLogResources(CS_CallLog newCallLog, CS_EquipmentWhiteLight equipmentWhiteLight)
        {
            try
            {
                IList<CS_CallLogResource> saveList = new List<CS_CallLogResource>();
                IList<CS_CallLogCallCriteriaEmail> emailSaveList = new List<CS_CallLogCallCriteriaEmail>();

                int? employeeId = null;
                int? contactId = null;

                CS_CallLogResource resource = new CS_CallLogResource()
                {
                    CallLogID = newCallLog.ID,
                    EmployeeID = employeeId,
                    ContactID = contactId,
                    EquipmentID = equipmentWhiteLight.EquipmentID,
                    JobID = newCallLog.JobID,
                    Type = 3,
                    CreatedBy = newCallLog.CreatedBy,
                    CreationDate = DateTime.Now,
                    ModifiedBy = newCallLog.ModifiedBy,
                    ModificationDate = DateTime.Now,
                    Active = true
                };

                resource = _callLogResourceRepository.Add(resource);

                SendNotificationWhiteLight(newCallLog, resource);
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error while trying to save the White Light Resources.", ex);
            }
        }

        private string BuildWhiteLogNote(CS_EquipmentWhiteLight equipmentWhiteLight, bool add)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (add)
            {
                stringBuilder.Append("Start Date: ");
                stringBuilder.Append(" <Text>");

                stringBuilder.AppendFormat(" {0}<BL>", equipmentWhiteLight.WhiteLightStartDate.ToString("MM/dd/yyyy HH:mm"));
            }
            else
            {
                stringBuilder.Append("End Date: ");
                stringBuilder.Append(" <Text>");

                stringBuilder.AppendFormat(" {0}<BL>", equipmentWhiteLight.WhiteLightStartDate.ToString("MM/dd/yyyy HH:mm"));

                TimeSpan duration = equipmentWhiteLight.WhiteLightEndDate.Value.Subtract(equipmentWhiteLight.WhiteLightStartDate);

                stringBuilder.Append("Duration: ");
                stringBuilder.Append(" <Text>");

                stringBuilder.AppendFormat(" {0} days<BL>", ((int)duration.TotalDays).ToString());
            }

            if (!string.IsNullOrEmpty(equipmentWhiteLight.Notes))
            {
                stringBuilder.Append("Notes: ");
                stringBuilder.Append(" <Text>");

                stringBuilder.AppendFormat(" {0}<BL>", equipmentWhiteLight.Notes);
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Get the equipmentcoverage by equipmentid
        /// </summary>
        /// <param name="equipmentID">equipment id</param>
        /// <returns>entity equipmentcoverage</returns>
        public CS_EquipmentCoverage GetEquipmentCoverage(int equipmentID)
        {
            CS_EquipmentCoverage equipmentCoverage = _equipmentCoverageRepository.Get(w => w.Active && w.EquipmentID == equipmentID);

            return equipmentCoverage;
        }

        /// <summary>
        /// Update the equipmentcoverage entity
        /// </summary>
        /// <param name="equipmentCoverage"></param>
        public void UpdateEquipmentCoverage(CS_EquipmentCoverage equipmentCoverage)
        {
            if (null == equipmentCoverage)
                throw new Exception("The EquipmentCoverage object is null.");

            try
            {
                _equipmentCoverageRepository.Update(equipmentCoverage);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while trying to save the equipmentcoverage object.", ex);
            }

        }

        /// <summary>
        /// Get the CS_EquipmentWhiteLight by equipmentid
        /// </summary>
        /// <param name="equipmentID">equipment id</param>
        /// <returns>CS_EquipmentWhiteLight</returns>
        public CS_EquipmentWhiteLight GetEquipmentWhiteLight(int equipmentID)
        {
            CS_EquipmentWhiteLight equipmentWhiteLight = _equipmentWhiteLightRepository.Get(w => w.Active && w.EquipmentID == equipmentID);

            return equipmentWhiteLight;
        }

        /// <summary>
        /// Returns the Status History List for a given Equipment
        /// </summary>
        /// <param name="equipmentId">DB Identifier for the Equipment</param>
        /// <returns>List of Entities</returns>
        public List<CS_EquipmentDownHistory> ListEquipmentDownHistory(int equipmentId)
        {
            return _equipmentDownHistoryRepository.ListAll(e => e.EquipmentID == equipmentId).OrderByDescending(e => e.CreationDate).ToList();
        }

        /// <summary>
        /// Returns the Coverage History List for a given Equipment
        /// </summary>
        /// <param name="equipmentId">DB Identifier for the Equipment</param>
        /// <returns>List of Entities</returns>
        public List<CS_EquipmentCoverage> ListEquipmentCoverageHistory(int equipmentId)
        {
            return _equipmentCoverageRepository.ListAll(e => e.EquipmentID == equipmentId, "CS_Division").OrderByDescending(e => e.CreationDate).ToList();
        }

        /// <summary>
        /// Returns the White Light History List for a given Equipment
        /// </summary>
        /// <param name="equipmentId">DB Identifier for the Equipment</param>
        /// <returns>List of Entities</returns>
        public List<CS_EquipmentWhiteLight> ListWhiteLightHistoryForEquipment(int equipmentId)
        {
            return _equipmentWhiteLightRepository.ListAll(e => e.EquipmentID == equipmentId).OrderByDescending(e => e.CreationDate).ToList();
        }

        /// <summary>
        /// Verify if resource is assigned to a job or not
        /// </summary>
        /// <param name="equipmentId">equipment id</param>
        /// <returns>bool</returns>
        public bool VerifyIfResourceIsAssignedToJob(int equipmentId)
        {
            CS_Resource resource = _resourceRepository.Get(w => w.Active && w.EquipmentID == equipmentId);

            if (resource != null)
                return true;

            return false;
        }

        public int? GetJobIdAssignedToEquipment(int equipmentId)
        {
            CS_Resource resource = _resourceRepository.Get(w => w.Active && w.EquipmentID == equipmentId);
            if (resource != null)
                return resource.JobID;
            else
                return null;
        }

        public IList<CS_EquipmentPermit> ListExpiredPermitByResourceList(IList<CS_Resource> resourceList)
        {
            IList<int?> equipmentIdList = resourceList.Select(e => e.EquipmentID).ToList();
            //DateTime date = DateTime.Now;

            return _equipmentPermitRepository.ListAll(e => e.Active && e.ExpirationDate <= DateTime.Now && equipmentIdList.Contains(e.EquipmentId));
        }

        public void SendNotificationForTransportationTeam(IList<CS_EquipmentPermit> permitList)
        {
            _settingsModel = new SettingsModel();
            _emailModel = new EmailModel();

            for (int i = 0; i < permitList.Count; i++)
            {
                CS_EquipmentPermit permit = permitList[i];
                IList<CS_Email> emails = new List<CS_Email>();

                //Body
                string body = GenerateEmailBodyForTransportationTeam(permit);

                //List receipts
                string receipts = _settingsModel.GetTransportationTeamEmails();

                //Subject
                string subject = GenerateEmailSubjectTransportationTeam(permit);

                if (!string.IsNullOrEmpty(receipts))
                {
                    emails = _emailModel.SaveEmailList(receipts, subject, body, "System", Globals.Security.SystemEmployeeID);
                }
            }
        }

        /// <summary>
        /// Generates body for the email for the Transportation team
        /// </summary>
        /// <param name="license"></param>
        /// <param name="expiredPermits"></param>
        /// <param name="states"></param>
        /// <returns></returns>
        private string GenerateEmailBodyForTransportationTeam(CS_EquipmentPermit permit)
        {
            StringBuilder _permitEmail = new StringBuilder();

            CS_State state = new CS_State();
            using (_locationModel = new LocationModel())
            {
                state = _locationModel.ListAllStatesByAcronym(permit.Code).FirstOrDefault();
            }

            _permitEmail.AppendFormat("{0} was issued a permit on {1} which expired on {2} in {3} for the {4} permit. Please update the permit information in Dossier.", permit.CS_Equipment.Name, permit.IssueDate.ToString("MM/dd/yyyy HH:mm"), permit.ExpirationDate.ToString("MM/dd/yyyy HH:mm"), ((null != state) ? state.AcronymName : string.Empty), permit.Type);

            return StringManipulation.TabulateString(_permitEmail.ToString());
        }

        /// <summary>
        /// Generates the subject for the Transportation team
        /// </summary>
        /// <param name="license"></param>
        /// <param name="states"></param>
        /// <returns></returns>
        public string GenerateEmailSubjectTransportationTeam(CS_EquipmentPermit license)
        {
            CS_State state = new CS_State();
            using (_locationModel = new LocationModel())
            {
                state = _locationModel.ListAllStatesByAcronym(license.Code).FirstOrDefault();
            }

            if (null != license)
                return string.Format("Expired permit(s) identified for {0}, from {1}, in {2}, for {3}", license.LicenseNumber, license.CS_Equipment.DivisionName, ((null != state) ? state.AcronymName : string.Empty), license.Type);

            return string.Empty;
        }

        public IList<CS_View_EquipmentInfo> ListEquipmentByEquipmentType(List<int> equipmentTypeId, Globals.EquipmentMaintenance.FilterType filterType, string filterValue)
        {
            string[] arrValue = filterValue.Split(',');
            for (int i = 0; i < arrValue.Length; i++)
                arrValue[i] = arrValue[i].Trim();

            switch (filterType)
            {
                case Globals.EquipmentMaintenance.FilterType.UnitType:
                    return _equipmentInfoRepository.ListAll(
                        e => e.Active && equipmentTypeId.Contains(e.EquipmentTypeID));

                case Globals.EquipmentMaintenance.FilterType.Division:
                    return _equipmentInfoRepository.ListAll(e => e.Active &&
                                                                 equipmentTypeId.Contains(e.EquipmentTypeID) &&
                                                                 arrValue.Any(
                                                                     like => e.DivisionName.Contains(like)));

                case Globals.EquipmentMaintenance.FilterType.UnitNumber:
                    return _equipmentInfoRepository.ListAll(e => e.Active &&
                                                                 equipmentTypeId.Contains(e.EquipmentTypeID) &&
                                                                 arrValue.Any(like => e.UnitNumber.Contains(like)));

                default:
                    return _equipmentInfoRepository.ListAll(
                        e => e.Active && equipmentTypeId.Contains(e.EquipmentTypeID));

            }
        }

        /// <summary>
        /// List equipmentss associated with a group of Regions
        /// </summary>
        public IList<CS_View_EquipmentInfo> ListEquipmentByRegionIDList(List<int> regionID, Globals.RegionalMaintenance.FilterType filterType, string filterValue)
        {
            string[] arrValue = filterValue.Split(',');
            for (int i = 0; i < arrValue.Length; i++)
                arrValue[i] = arrValue[i].Trim();

            switch (filterType)
            {
                case Globals.RegionalMaintenance.FilterType.ComboName:
                    return _equipmentInfoRepository.ListAll(
                                e => e.Active &&
                                     e.RegionID.HasValue &&
                                     regionID.Contains(e.RegionID.Value) &&
                                     arrValue.Any(
                                        like => e.ComboName.Contains(like)));
                case Globals.RegionalMaintenance.FilterType.EquipmentUnitNumber:
                    return _equipmentInfoRepository.ListAll(
                                e => e.Active &&
                                     e.RegionID.HasValue &&
                                     regionID.Contains(e.RegionID.Value) &&
                                     arrValue.Any(
                                        like => e.UnitNumber.Contains(like)));
                case Globals.RegionalMaintenance.FilterType.EmployeeName:
                    return new List<CS_View_EquipmentInfo>();
                case Globals.RegionalMaintenance.FilterType.None:
                case Globals.RegionalMaintenance.FilterType.Region:
                case Globals.RegionalMaintenance.FilterType.RVP:
                case Globals.RegionalMaintenance.FilterType.Division:
                default:
                    return _equipmentInfoRepository.ListAll(
                                e => e.Active &&
                                     e.RegionID.HasValue &&
                                     regionID.Contains(e.RegionID.Value));
            }
        }

        public IList<CS_Equipment> ListEquipmentsFromPrimaryEquipment(int primaryEquipmentId, int comboId)
        {
            return _equipmentRepository.ListAll(e => e.Active && e.ID != primaryEquipmentId && e.ComboID == comboId);
        }

        public bool EquipmentIsPrimary(int equipmentId)
        {
            CS_View_EquipmentInfo eqInfo = GetEquipmentInfoByEquipmentID(equipmentId);
            if (eqInfo.IsPrimary == 1)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Returns a list of all Active LocalEquiment Types
        /// </summary>
        /// <returns>list of all Active LocalEquiment Types</returns>
        public IList<CS_LocalEquipmentType> ListAllLocalEquipmentType()
        {
            return _localEquipmentTypeRepository.ListAll(e => e.Active);
        }

        /// <summary>
        /// Returns a list of all Active LocalEquiment Types
        /// </summary>
        /// <returns>list of all Active LocalEquiment Types</returns>
        public IList<CS_LocalEquipmentType> ListAllLocalEquipmentTypeByName(string name)
        {
            return _localEquipmentTypeRepository.ListAll(e => e.Active && e.Name.ToLower().Contains(name.ToLower()));
        }

        /// <summary>
        /// List Equipment Type filtered by Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IList<CS_EquipmentType> ListAllEquipmentTypeByName(string name)
        {
            return _equipmentTypeRepository.ListAll(w => w.Active && (w.Name.ToLower().Contains(name.ToLower()) || (w.Number + " - " + w.Name.ToLower()).Contains(name.ToLower())));
        }

        /// <summary>
        /// Get equipment type by equipment id
        /// </summary>
        /// <param name="equipmentId">equipment id</param>
        /// <returns>entity equipment type</returns>
        public CS_EquipmentType GetEquipmentTypeByEquipmentId(int equipmentId)
        {
            return _equipmentTypeRepository.Get(w => w.Active && w.CS_Equipment.Any(a => a.ID == equipmentId));
        }

        /// <summary>
        /// List Equipment by equipment type
        /// </summary>
        /// <param name="equimentTypeId"></param>
        /// <param name="unitNumber"></param>
        /// <returns></returns>
        public IList<CS_Equipment> ListAllEquipmentByEqType(int equimentTypeId, string unitNumber)
        {
            return _equipmentRepository.ListAll(w => w.Active && (w.EquipmentTypeID == equimentTypeId || equimentTypeId == 0) && w.Name.ToLower().Contains(unitNumber.ToLower()), orderby => orderby.Name, true, "CS_EquipmentCombo");
        }

        public void SendNotificationWhiteLight(CS_CallLog callentry, CS_CallLogResource resource)
        {

            _emailModel = new EmailModel();


            IList<CS_Email> emails = new List<CS_Email>();
            string receipts = string.Empty;

            //List receipts
            IList<EmailVO> receiptsEmail = _callCriteriaModel.ListReceiptsByCallLog(callentry.CallTypeID.ToString(), callentry.JobID, callentry);

            if (receiptsEmail.Count > 0)
            {
                for (int i = 0; i < receiptsEmail.Count; i++)
                    receipts += (i == 0) ? receiptsEmail[i].Email : string.Format(";{0}", receiptsEmail[i].Email);

                //Body
                string body = GenerateEmailBodyForWhiteLight(resource);

                //Subject 
                string subject = GenerateEmailSubjectForWhiteLight(callentry, resource);


                emails = _emailModel.SaveEmailList(receipts, subject, body, "System", Globals.Security.SystemEmployeeID);

                _callCriteriaModel.SendCallLogCriteriaEmails(callentry);
            }


        }

        private string GenerateEmailSubjectForWhiteLight(CS_CallLog callLog, CS_CallLogResource resource)
        {

            StringBuilder subject = new StringBuilder();
            int jobID = callLog.JobID;
            CS_Job job = _jobRepository.Get(e => e.ID == jobID, "CS_CustomerInfo", "CS_CustomerInfo.CS_Customer", "CS_JobInfo", "CS_JobInfo.CS_JobAction", "CS_LocationInfo", "CS_LocationInfo.CS_City", "CS_LocationInfo.CS_State");

            subject.Append(job.PrefixedNumber);
            subject.Append(", ");

            if (callLog.CS_Job.ID != (int)Globals.GeneralLog.ID)
            {
                subject.Append(job.CS_CustomerInfo.CS_Customer.Name.Trim());
                subject.Append(", ");
                subject.Append(job.CS_JobInfo.CS_JobAction.Description);
                subject.Append(", ");
                subject.Append(job.CS_LocationInfo.CS_City.Name);
                subject.Append(" ");
                subject.Append(job.CS_LocationInfo.CS_State.Acronym);
            }

            else
            {
                subject.Append(resource.CS_Equipment.CS_Division.Name);
                subject.Append(", ");
                subject.Append(resource.CS_Equipment.CS_Division.CS_State.Name);


            }

            subject.Append(", White Light");

            return subject.ToString();
        }

        private string GenerateEmailBodyForWhiteLight(CS_CallLogResource resources)
        {
            StringBuilder body = new StringBuilder();

            body.Append("<div>");

            body.Append("<div style='width: 100%; display: inline-block;'>");
            body.Append("<div style='text-align: right; width: 30%; height: 100%; display: inline-block; float: left'>");
            body.Append("<b>Call Log Details</b>");
            body.Append("</div><div style='text-align: left; width: 68%; height: 100%; display: inline-block; float: right'></div>");
            body.Append("</div>");


            string description = resources.CS_Equipment.Name;
            string date = resources.CS_Equipment.CS_EquipmentWhiteLight.FirstOrDefault(w => w.Active).WhiteLightStartDate.ToString("MM/dd/yyyy");

            //Description
            body.Append("<div style='width: 100%; display: inline-block;'>");
            body.Append("<div style='text-align: right; width: 30%; height: 100%; display: inline-block; float: left'>");
            body.Append("<b>White Light: </b>");
            body.Append("</div><div style='text-align: left; width: 68%; height: 100%; display: inline-block; float: right'>");
            body.Append(description);
            body.Append("</div>");
            body.Append("</div>");


            //Date
            body.Append("<div style='width: 100%; display: inline-block;'>");
            body.Append("<div style='text-align: right; width: 30%; height: 100%; display: inline-block; float: left'>");
            body.Append("<b>Start Date: </b>");
            body.Append("</div><div style='text-align: left; width: 68%; height: 100%; display: inline-block; float: right'>");
            body.Append(date);
            body.Append("</div>");
            body.Append("</div>");
            body.Append("</div>");

            return body.ToString();
        }
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
                UpdateEquipments();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to import the Equipment information!\n{0}\n{1}", ex.InnerException, ex.StackTrace));

                StringBuilder mailError = new StringBuilder();
                mailError.AppendLine(string.Format("An Error Ocurred when importing the Equipment Information!\n{0}\n{1}", ex.InnerException, ex.StackTrace));
                //MailUtility.SendMail(settings.GetITEmailOnError(), "", "", mailError.ToString(), "Dossier Import Service - Error occured on Equipment Information", false, null);
                emailModel.SaveEmailList(settings.GetITEmailOnError(), "Dossier Import Service - Error occured on Equipment Information", mailError.ToString(), "System", (int)Globals.Security.SystemEmployeeID);

                return false;
            }

            try
            {
                UpdateEquipmentPermit();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to import the Equipment Permit information!\n{0}\n{1}", ex.InnerException, ex.StackTrace));

                StringBuilder mailError = new StringBuilder();
                mailError.AppendLine(string.Format("An Error Ocurred when importing the Equipment Permit Information!\n{0}\n{1}", ex.InnerException, ex.StackTrace));
                //MailUtility.SendMail(settings.GetITEmailOnError(), "", "", mailError.ToString(), "Dossier Import Service - Error occured on Equipment Permit Information", false, null);
                emailModel.SaveEmailList(settings.GetITEmailOnError(), "Dossier Import Service - Error occured on Equipment Permit Information", mailError.ToString(), "System", (int)Globals.Security.SystemEmployeeID);

                return false;
            }

            return true;
        }

        /// <summary>
        /// Executes Update process for Equipment Information
        /// </summary>
        /// <returns>True if process ran OK, False if there was an error</returns>
        private bool UpdateEquipments()
        {
            try
            {
                CS_EquipmentRepository equipmentRepository = new CS_EquipmentRepository(_equipmentRepository, _equipmentRepository.UnitOfWork);
                // Bulk Copying Equipments from Dossier
                equipmentRepository.BulkCopyAllEquipments(DossierIntegration.Singleton.ListAllEquipments());

                // Run import tool
                equipmentRepository.UpdateFromIntegration();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Executes Update process for Equipment Permit Information
        /// </summary>
        /// <returns>>True if process ran OK, False if there was an error</returns>
        private bool UpdateEquipmentPermit()
        {
            try
            {
                CS_EquipmentPermitRepository equipmentPermitRepository = new CS_EquipmentPermitRepository(_equipmentPermitRepository, _equipmentPermitRepository.UnitOfWork);

                // Bulk Copying Equipment Permit from Dossier
                equipmentPermitRepository.BulkCopyAllEquipmentPermit(DossierIntegration.Singleton.ListAllEquipmentPermit());

                // Run Import Tool
                equipmentPermitRepository.UpdateFromIntegration();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Creates Enmails for Lapsed Equipment permits
        /// </summary>
        public void CreateLapsedPermitsEmail()
        {
            try
            {

                IList<CS_EquipmentPermit> expiredPermits = _equipmentPermitRepository.ListAll(
                                                                e => e.Active
                                                                && !e.CS_EquipmentPermitEmail.Any(f => f.Active && f.EquipmentPermitID == e.Id && e.ExpirationDate <= f.CreationDate)
                                                                && e.ExpirationDate <= DateTime.Now
                                                                && e.CS_Equipment.HeavyEquipment
                                                                && e.CS_Equipment.Active
                                                                , "CS_Equipment"
                                                                , "CS_Equipment.CS_Division");

                IList<CS_EquipmentPermitEmail> expiredPermitEmails = new List<CS_EquipmentPermitEmail>();

                if (expiredPermits.Count > 0)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        expiredPermitEmails = SendNotificationForPermitTeam(expiredPermits);
                        _equipmentPermitEmailRepository.AddList(expiredPermitEmails);
                        scope.Complete();
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Sends the email notification for the permit team
        /// </summary>
        /// <param name="equipmentPermitId">job id</param>
        public IList<CS_EquipmentPermitEmail> SendNotificationForPermitTeam(IList<CS_EquipmentPermit> expiredPermits)
        {
            _settingsModel = new SettingsModel();

            try
            {
                IList<CS_EquipmentPermit> expiredLicenses = expiredPermits.Distinct(new Globals.EmailService.CS_EquipmentPermit_Comparer()).ToList();

                IList<CS_State> states = new List<CS_State>();
                using (_locationModel = new LocationModel())
                {
                    states = _locationModel.ListAllStates();
                }

                IList<CS_EquipmentPermitEmail> returnList = new List<CS_EquipmentPermitEmail>();

                for (int i = 0; i < expiredLicenses.Count; i++)
                {
                    CS_EquipmentPermit license = expiredLicenses[i];
                    IList<CS_EquipmentPermit> expiredEquipments = expiredPermits.Where(e => e.LicenseNumber == license.LicenseNumber && e.Code == license.Code).ToList();
                    IList<CS_Email> emails = new List<CS_Email>();

                    //Body
                    string body = GenerateEmailBodyForPermitTeam(license, expiredEquipments, states);

                    //List receipts
                    string receipts = _settingsModel.GetPermitTeamEmails();

                    //Subject
                    string subject = GenerateEmailSubjectEstimationTeam(license, states);

                    if (!string.IsNullOrEmpty(receipts))
                    {
                        using (_emailModel = new EmailModel())
                        {
                            emails = _emailModel.SaveEmailList(receipts, subject, body, "System", Globals.Security.SystemEmployeeID);
                        }
                    }

                    for (int j = 0; j < expiredEquipments.Count; j++)
                    {
                        CS_EquipmentPermit equipmentPermit = expiredEquipments[j];
                        for (int h = 0; h < emails.Count; h++)
                        {
                            CS_Email email = emails[h];
                            returnList.Add
                                (
                                    new CS_EquipmentPermitEmail()
                                    {
                                        EmailID = email.ID,
                                        EquipmentPermitID = equipmentPermit.Id,
                                        CreatedBy = "System",
                                        CreationDate = email.CreationDate,
                                        CreationID = Globals.Security.SystemEmployeeID,
                                        ModifiedBy = "System",
                                        ModificationDate = email.CreationDate,
                                        ModificationID = Globals.Security.SystemEmployeeID,
                                        Active = true
                                    }
                                );
                        }
                    }
                }

                return returnList;
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error sending the email notification for the Permit Team.", ex);
            }
        }

        /// <summary>
        /// Generates body for the email for the permit team
        /// </summary>
        /// <param name="license"></param>
        /// <param name="expiredPermits"></param>
        /// <param name="states"></param>
        /// <returns></returns>
        private string GenerateEmailBodyForPermitTeam(CS_EquipmentPermit license, IList<CS_EquipmentPermit> expiredPermits, IList<CS_State> states)
        {
            StringBuilder _permitEmail = new StringBuilder();
            CS_State state = states.FirstOrDefault(e => e.Acronym == license.Code);
            expiredPermits.OrderBy(e => e.ExpirationDate);

            _permitEmail.Append("Permit#:<Text> " + license.LicenseNumber);

            if (null == state)
                _permitEmail.Append("<BL>" + "Permit State:<Text> " + license.Code);
            else
                _permitEmail.Append("<BL>" + "Permit State:<Text> " + state.AcronymName);

            _permitEmail.Append("<BL>" + "Permit Type:<Text> " + license.Type);

            _permitEmail.Append("<BL>" + "Permit Equipments<Text> ");

            if (expiredPermits.Count > 0)
            {
                for (int i = 0; i < expiredPermits.Count; i++)
                {
                    CS_EquipmentPermit permit = expiredPermits[i];

                    _permitEmail.Append("<BL>" + "  Unit#:<Text> " + permit.CS_Equipment.Name);
                    _permitEmail.Append("<BL>" + "  Descriptor:<Text> " + permit.CS_Equipment.Description);
                    _permitEmail.Append("<BL>" + "  Division#:<Text> " + permit.CS_Equipment.CS_Division.Name);
                    _permitEmail.Append("<BL>" + "  Expiration Date:<Text> " + permit.ExpirationDate + "<BL>");
                }
            }

            return StringManipulation.TabulateString(_permitEmail.ToString());
        }

        /// <summary>
        /// Generates the subject for the permit team
        /// </summary>
        /// <param name="license"></param>
        /// <param name="states"></param>
        /// <returns></returns>
        public string GenerateEmailSubjectEstimationTeam(CS_EquipmentPermit license, IList<CS_State> states)
        {
            CS_State state = states.FirstOrDefault(e => e.Acronym == license.Code);

            if (null != license)
                return string.Format("Expired Permit Number {0}, {1}", license.LicenseNumber, ((null != state) ? state.AcronymName : string.Empty));

            return string.Empty;
        }

        /// <summary>
        /// List expired permits for the notification pop up
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public IList<CS_EquipmentPermit> ListEquipmentPermitNotification(DateTime date)
        {
            return _equipmentPermitRepository.ListAll(
                                                    e => e.Active
                                                    && e.ExpirationDate <= date
                                                    && e.CS_Equipment.HeavyEquipment
                                                    && e.CS_Equipment.Active
                                                    , "CS_Equipment"
                                                    , "CS_Equipment.CS_Division").OrderBy(e => e.LicenseNumber).ThenBy(e => e.Code).ToList();
        }

        #endregion

        #region [ IDisposable Implementation ]

        /// <summary>
        /// Dispose all objects that are no longer needed
        /// </summary>
        public void Dispose()
        {
            _equipmentInfoRepository = null;
            _equipmentRepository = null;
            _equipmentTypeRepository = null;
            _viewReserveEquipmentRepository = null;
            _conflictedCombosRepository = null;
            _equipmentPermitEmailRepository = null;
            _resourceRepository = null;

            if (null != _settingsModel)
            {
                _settingsModel.Dispose();
                _settingsModel = null;
            }

            if (null != _locationModel)
            {
                _locationModel.Dispose();
                _locationModel = null;
            }

            if (null != _emailModel)
            {
                _emailModel.Dispose();
                _emailModel = null;
            }

            if (null != _callLogModel)
            {
                _callLogModel.Dispose();
                _callLogModel = null;
            }

            _unitOfWork.Dispose();
            _unitOfWork = null;
        }

        #endregion


    }
}
