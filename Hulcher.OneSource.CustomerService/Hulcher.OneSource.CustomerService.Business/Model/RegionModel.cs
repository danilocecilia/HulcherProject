using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core;
using System.Transactions;

namespace Hulcher.OneSource.CustomerService.Business.Model
{
    public class RegionModel : IDisposable
    {
        #region [ Attributes ]

        /// <summary>
        /// Unit of Work used to access the database/in-memory context
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// Repository class for CS_Settings
        /// </summary>
        private IRepository<CS_Region> _regionRepository;

        /// <summary>
        /// Repository class for CS_Region
        /// </summary>
        private IRepository<CS_Region_Division> _regionDivisionRepository;

        /// <summary>
        /// Repository class for CS_Division
        /// </summary>
        private IRepository<CS_Division> _divisionRepository;

        /// <summary>
        /// Repository class for CS_Employee
        /// </summary>
        private IRepository<CS_Employee> _employeeRepository;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class constructor
        /// </summary>
        public RegionModel()
        {
            _unitOfWork = new EFUnitOfWork();

            InstanceObjects();
        }

        /// <summary>
        /// Class constructor (for unit testing)
        /// </summary>
        public RegionModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            InstanceObjects();
        }

        /// <summary>
        /// Instance repository objects
        /// </summary>
        private void InstanceObjects()
        {
            _regionRepository = new EFRepository<CS_Region>();
            _regionRepository.UnitOfWork = _unitOfWork;

            _regionDivisionRepository = new EFRepository<CS_Region_Division>();
            _regionDivisionRepository.UnitOfWork = _unitOfWork;

            _divisionRepository = new EFRepository<CS_Division>();
            _divisionRepository.UnitOfWork = _unitOfWork;

            _employeeRepository = new EFRepository<CS_Employee>();
            _employeeRepository.UnitOfWork = _unitOfWork;
        }

        #endregion

        #region [ Methods ]

        #region [ Listing ]

        /// <summary>
        /// List all Active Regions
        /// </summary>
        public IList<CS_Region> ListAllRegions(Globals.RegionalMaintenance.FilterType filterType, string filterValue)
        {
            string[] arrValue = filterValue.Split(',');
            for (int i = 0; i < arrValue.Length; i++)
                arrValue[i] = arrValue[i].Trim();

            switch (filterType)
            {
                case Globals.RegionalMaintenance.FilterType.Region:
                    return _regionRepository.ListAll(
                        e => e.Active &&
                             arrValue.Any(g => e.Name.Contains(g)),
                        "CS_Employee_Region");
                case Globals.RegionalMaintenance.FilterType.RVP:
                    return _regionRepository.ListAll(
                        e => e.Active &&
                             arrValue.Any(
                                g => e.CS_Employee_Region.Name.Contains(g) ||
                                     e.CS_Employee_Region.FirstName.Contains(g)),
                        "CS_Employee_Region");
                case Globals.RegionalMaintenance.FilterType.Division:
                    return _regionRepository.ListAll(
                        e => e.Active &&
                             e.CS_Region_Division.Any(
                                f => f.Active && arrValue.Any(g => f.CS_Division.Name.Contains(g))),
                        "CS_Employee_Region");
                case Globals.RegionalMaintenance.FilterType.EmployeeName:
                    return _regionRepository.ListAll(
                        e => e.Active &&
                             e.CS_Region_Division.Any(
                                f => f.Active && f.CS_Division.CS_Employee.Any(
                                    g => g.Active &&  arrValue.Any(
                                        h => g.Name.Contains(h) ||
                                             g.FirstName.Contains(h)))),
                        "CS_Employee_Region");
                case Globals.RegionalMaintenance.FilterType.ComboName:
                    return _regionRepository.ListAll(
                        e => e.Active &&
                             e.CS_Region_Division.Any(
                                f => f.Active && f.CS_Division.CS_Equipment.Any(
                                    g => g.Active && arrValue.Any(
                                        h => g.CS_EquipmentCombo.Name.Contains(h)))),
                        "CS_Employee_Region");
                case Globals.RegionalMaintenance.FilterType.EquipmentUnitNumber:
                    return _regionRepository.ListAll(
                        e => e.Active &&
                             e.CS_Region_Division.Any(
                                f => f.Active && f.CS_Division.CS_Equipment.Any(
                                    g => g.Active && arrValue.Any(
                                        h => g.Name.Contains(h)))),
                        "CS_Employee_Region");
                case Globals.RegionalMaintenance.FilterType.None:
                default:
                    return new List<CS_Region>();
            }
        }

        /// <summary>
        /// List all Active Regions
        /// </summary>
        public IList<CS_Region> ListAllRegions()
        {

            return _regionRepository.ListAll(
                e => e.Active,
                "CS_Employee_Region");
        }

        /// <summary>
        /// List all Region_Divisions Associated with a Region
        /// </summary>
        public IList<CS_Region_Division> ListRegionDivisionByRegionID(int regionID)
        {
            return _regionDivisionRepository.ListAll(e => e.Active && e.RegionID == regionID, o => o.CS_Division.Name, true, "CS_Division");
        }

        /// <summary>
        /// List all Divisions that are associated to a Region
        /// </summary>
        public IList<CS_Division> ListDivisionByRegion(int regionId)
        {
            return _divisionRepository.ListAll(w => w.Active && w.CS_Region_Division.Any(e => e.Active && e.DivisionID == w.ID && e.RegionID == regionId));
        }


        public CS_Employee GetRVPByRegion(int regionId)
        {
            return _employeeRepository.Get(e => e.Active == true && (e.JobCode.ToUpper().Contains("OPSRVP") || e.JobCode.ToUpper().Contains("REGVPS")) && e.CS_Region_RVP.Any(w => w.Active && w.ID == regionId));
        }

        #endregion

        #region [ Save ]

        /// <summary>
        /// Saves the Region and associated Divisions
        /// </summary>
        /// <param name="regionId">ID of the selected Region</param>
        /// <param name="newRegionDivisionIdList">List of Division ID's</param>
        /// <param name="vpId">ID of the selected RVP</param>
        /// <param name="username">Name of the User commiting changes</param>
        public void SaveRegion(int regionId, List<int> newRegionDivisionIdList, int? vpId, string username)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                DateTime now = DateTime.Now;
                CS_Region region = _regionRepository.Get(e => e.ID == regionId && e.Active);

                if (null != region)
                {
                    List<CS_Region_Division> oldRegionDivisionIdList = _regionDivisionRepository.ListAll(e => e.RegionID == regionId && e.Active).ToList();

                    List<int> divisionsAddIdList = newRegionDivisionIdList.FindAll(e => !oldRegionDivisionIdList.Any(f => f.ID == e));
                    List<CS_Region_Division> divisionsRemoveList = oldRegionDivisionIdList.FindAll(e => !newRegionDivisionIdList.Contains(e.ID));

                    List<CS_Region_Division> divisionsToAdd = new List<CS_Region_Division>();
                    List<CS_Region_Division> divisionsToDelete = new List<CS_Region_Division>();

                    for (int i = 0; i < divisionsAddIdList.Count; i++)
                    {
                        CS_Region_Division newRegionDivision = new CS_Region_Division()
                        {
                            RegionID = regionId,
                            DivisionID = newRegionDivisionIdList[i],
                            CreatedBy = username,
                            CreationDate = now,
                            //CreationID = 
                            ModifiedBy = username,
                            ModificationDate = now,
                            //ModificationID = 
                            Active = true
                        };

                        divisionsToAdd.Add(newRegionDivision);
                    }

                    for (int i = 0; i < divisionsRemoveList.Count; i++)
                    {
                        CS_Region_Division newRegionDivision = new CS_Region_Division()
                        {
                            ID = divisionsRemoveList[i].ID,
                            RegionID = divisionsRemoveList[i].RegionID,
                            DivisionID = divisionsRemoveList[i].DivisionID,
                            CreatedBy = divisionsRemoveList[i].CreatedBy,
                            CreationDate = divisionsRemoveList[i].CreationDate,
                            CreationID = divisionsRemoveList[i].CreationID,
                            ModifiedBy = username,
                            ModificationDate = now,
                            //ModificationID = 
                            Active = false
                        };

                        divisionsToDelete.Add(newRegionDivision);
                    }

                    if (vpId.HasValue)
                    {
                        CS_Region updateRegion = new CS_Region()
                        {
                            ID = region.ID,
                            RegionalVpID = vpId.Value,
                            Name = region.Name,
                            CreatedBy = region.CreatedBy,
                            CreationDate = region.CreationDate,
                            CreationID = region.CreationID,
                            ModifiedBy = region.ModifiedBy,
                            ModificationDate = region.ModificationDate,
                            //ModificationID = region.ModificationID
                            Active = true
                        };

                        _regionRepository.Update(updateRegion);
                    }

                    _regionDivisionRepository.AddList(divisionsToAdd);
                    _regionDivisionRepository.UpdateList(divisionsToDelete);

                    oldRegionDivisionIdList = null;
                    newRegionDivisionIdList = null;
                    divisionsRemoveList = null;
                    divisionsToAdd = null;
                    divisionsToDelete = null;
                }

                scope.Complete();
            }
        }

        #endregion

        #endregion

        #region [ IDisposable Implementation ]

        public void Dispose()
        {

            _unitOfWork.Dispose();
            _unitOfWork = null;
        }

        #endregion
    }
}
