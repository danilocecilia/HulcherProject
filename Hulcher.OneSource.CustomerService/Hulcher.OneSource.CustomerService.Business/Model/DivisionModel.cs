using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Integration;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.DataContext.VO;

namespace Hulcher.OneSource.CustomerService.Business.Model
{
    public class DivisionModel : IDisposable
    {
        #region [ Atributes ]

        /// <summary>
        /// Unit of Work used to access the database/in-memory context
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// Repository class for CS_Settings
        /// </summary>
        private ICachedRepository<CS_Division> _repository;

        /// <summary>
        /// Repository class for CS_Division
        /// </summary>
        private CS_DivisionRepository _divisionRepository;

        /// <summary>
        /// Repository class for CS_PhoneType
        /// </summary>
        private IRepository<CS_PhoneType> _phoneTypeRepository;

        /// <summary>
        /// Repository class for CS_LocalDivision
        /// </summary>
        private IRepository<CS_LocalDivision> _localDivisionRepository;

        /// <summary>
        /// Repository class for CS_DivisionPhoneNumber
        /// </summary>
        private IRepository<CS_DivisionPhoneNumber> _localDivisionPhoneNumberRepository;

        #endregion

        #region [ Constructors ]

        public DivisionModel()
        {
            _unitOfWork = new EFUnitOfWork();
            InstantiateObjects();
        }

        public DivisionModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            InstantiateObjects();
        }

        public void InstantiateObjects()
        {
            _repository = new CachedRepository<CS_Division>();
            _divisionRepository = new CS_DivisionRepository(_repository, _unitOfWork);

            _phoneTypeRepository = new EFRepository<CS_PhoneType>();
            _phoneTypeRepository.UnitOfWork = _unitOfWork;

            _localDivisionRepository = new EFRepository<CS_LocalDivision>();
            _localDivisionRepository.UnitOfWork = _unitOfWork;

            _localDivisionPhoneNumberRepository = new EFRepository<CS_DivisionPhoneNumber>();
            _localDivisionPhoneNumberRepository.UnitOfWork = _unitOfWork;
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Returns a single Division Entity
        /// </summary>
        /// <param name="divisionId">ID of the Division</param>
        /// <returns>Division Entity</returns>
        public CS_Division GetDivision(int divisionId)
        {

            return _divisionRepository.Get(e => e.ID == divisionId && e.IsGeneralLog == false, "CS_State");
        }

        /// <summary>
        /// Returns the General Log fictional Division
        /// </summary>
        /// <returns>Fictional Division Entity</returns>
        public CS_Division GetGeneralLogDivision()
        {
            return _divisionRepository.Get(e => e.IsGeneralLog);
        }

        /// <summary>
        /// List all items of an Entity in the Database
        /// </summary>
        /// <returns>List of Entities</returns>
        public IList<CS_Division> ListAllDivision()
        {
            return _divisionRepository.ListAll(e => e.Active && e.IsGeneralLog == false && e.Name != "-").OrderBy(e => e.Name).ThenBy(e => e.Description).ToList();
        }

        /// <summary>
        /// List Divisions associated with a certain Region
        /// </summary>
        public IList<CS_Division> ListDivisionByRegionIDList(List<int> regionIDList, Globals.RegionalMaintenance.FilterType filterType, string filterValue)
        {
            string[] arrValue = filterValue.Split(',');
            for (int i = 0; i < arrValue.Length; i++)
                arrValue[i] = arrValue[i].Trim();

            switch (filterType)
            {
                case Globals.RegionalMaintenance.FilterType.Division:
                    return _divisionRepository.ListAll(
                        e => e.Active &&
                             e.CS_Region_Division.Any(
                                f => f.Active &&
                                     regionIDList.Contains(f.RegionID) &&
                                     arrValue.Any(
                                        like => f.CS_Division.Name.Contains(like))));
                case Globals.RegionalMaintenance.FilterType.EmployeeName:
                    return _divisionRepository.ListAll(
                        e => e.Active &&
                             e.CS_Region_Division.Any(
                                f => f.Active &&
                                     regionIDList.Contains(f.RegionID) &&
                                     f.CS_Division.CS_Employee.Any(
                                        g => arrValue.Any(
                                            like => g.Name.Contains(like) ||
                                                    g.FirstName.Contains(like)))));
                case Globals.RegionalMaintenance.FilterType.ComboName:
                    return _divisionRepository.ListAll(
                        e => e.Active &&
                             e.CS_Region_Division.Any(
                                f => f.Active &&
                                     regionIDList.Contains(f.RegionID) &&
                                     f.CS_Division.CS_Equipment.Any(
                                        g => arrValue.Any(
                                            like => g.CS_EquipmentCombo.Name.Contains(like)))));
                case Globals.RegionalMaintenance.FilterType.EquipmentUnitNumber:
                    return _divisionRepository.ListAll(
                        e => e.Active &&
                             e.CS_Region_Division.Any(
                                f => f.Active &&
                                     regionIDList.Contains(f.RegionID) &&
                                     f.CS_Division.CS_Equipment.Any(
                                        g => arrValue.Any(
                                            like => g.Name.Contains(like)))));
                case Globals.RegionalMaintenance.FilterType.Region:
                case Globals.RegionalMaintenance.FilterType.RVP:
                case Globals.RegionalMaintenance.FilterType.None:
                default:
                    return _divisionRepository.ListAll(
                        e => e.Active &&
                             e.CS_Region_Division.Any(
                                j => j.Active &&
                                     regionIDList.Contains(j.RegionID)));

            }
        }

        /// <summary>
        /// List all active divisions filtered by name
        /// </summary>
        /// <param name="prefixFilter">Prefix Filter to search with LIKE at the name field</param>
        /// <returns></returns>
        public IList<CS_Division> ListAllFilteredDivisionByName(string prefixFilter)
        {
            return _divisionRepository.ListAll(e => e.Active && e.IsGeneralLog == false && e.Name.ToLower().Contains(prefixFilter.ToLower()), order => order.Name, true);
        }

        /// <summary>
        /// Returns a list of CS_PhoneType entities that are Active in the DB
        /// </summary>
        public List<CS_PhoneType> LoadPhoneTypes()
        {
            return _phoneTypeRepository.ListAll(e => e.Active).ToList();
        }

        /// <summary>
        /// Method import employees using BulkCopy
        /// </summary>
        public void ImportEmployeeInfoFromBulkCopy()
        {
            _divisionRepository.BulkCopyAllDivisions(IVantageIntegration.Singleton.ListAllDivisions());
        }

        /// <summary>
        /// Return division by an employee
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public CS_Division GetDivisionByEmployee(int employeeId)
        {
            return _divisionRepository.Get(w => w.CS_Employee.Any(a => a.ID == employeeId));
        }

        /// <summary>
        /// List all Divisions that arent associated to a Region
        /// </summary>
        public IList<CS_Division> ListNotAssociatedDivisions()
        {
            return _divisionRepository.ListAll(w => w.Active && !w.CS_Region_Division.Any(e => e.Active && e.DivisionID == w.ID));
        }

        public IList<CS_LocalDivision> ListLocalDivisionByName(string divisionName)
        {
            string[] arrValue = divisionName.Split(',');
            for (int i = 0; i < arrValue.Length; i++)
                arrValue[i] = arrValue[i].Trim();

            return _localDivisionRepository.ListAll(e => e.Active && arrValue.Any(g => (e.Name).Contains(g)), "CS_DivisionPhoneNumber" , "CS_State", "CS_City", "CS_ZipCode");
        }

        #region [ Parse Methods ]

        public IList<DivisionPhoneNumberVO> ParseEntityToDivisionPhone(IList<CS_LocalDivision> divisionList)
        {
            IList<DivisionPhoneNumberVO> returnList = new List<DivisionPhoneNumberVO>();

            for (int i = 0; i < divisionList.Count; i++)
            {
                if (divisionList[i].CS_DivisionPhoneNumber.Count > 0)
                {
                    IList<CS_DivisionPhoneNumber> phoneList = divisionList[i].CS_DivisionPhoneNumber.ToList();

                    for (int j = 0; j < phoneList.Count; j++)
                    {
                        returnList.Add(new DivisionPhoneNumberVO(divisionList[i], phoneList[j]));
                    }
                }
                else
                {
                    returnList.Add(new DivisionPhoneNumberVO(divisionList[i]));
                }
            }

            return returnList;
        }

        #endregion

        #endregion

        #region [ IDisposable Implementation ]

        public void Dispose()
        {
            _divisionRepository = null;

            _unitOfWork.Dispose();
            _unitOfWork = null;
        }

        #endregion

        public List<CS_Division> ListAllDivisionsByResources(List<CS_Resource> resourceList)
        {
            List<CS_Division> list = new List<CS_Division>();

            for (int i = 0; i < resourceList.Count; i++)
            {
                CS_Division div = null;

                if (resourceList[i].EmployeeID.HasValue)
                {
                    int id = resourceList[i].EmployeeID.Value;
                    div = _divisionRepository.Get(e => e.ID == e.CS_Employee.FirstOrDefault(f => f.ID == id).CS_Division.ID);
                }
                else
                {
                    int id = resourceList[i].EquipmentID.Value;
                    div = _divisionRepository.Get(e => e.ID == e.CS_Equipment.FirstOrDefault(f => f.ID == id).CS_Division.ID);
                }

                if (null != div)
                    list.Add(div);
            }

            return list;
        }

        /// <summary>
        /// Loads Local Division data based on its Identifier
        /// </summary>
        /// <param name="localDivisionID">Local Division Identifier</param>
        /// <returns>Local Division data</returns>
        public CS_LocalDivision LoadLocalDivision(int localDivisionID)
        {
            return _localDivisionRepository.Get(e => e.ID == localDivisionID);
        }

        /// <summary>
        /// Loads Local Division data and phone numbers based on its Identifier
        /// </summary>
        /// <param name="localDivisionID">Local Division Identifier</param>
        /// <returns>Local Division data</returns>
        public CS_LocalDivision LoadLocalDivisionWithPhones(int localDivisionID)
        {
            return _localDivisionRepository.Get(e => e.ID == localDivisionID, "CS_DivisionPhoneNumber.CS_PhoneType" , "CS_State", "CS_City", "CS_ZipCode");
        }

        /// <summary>
        /// Saves or Updates a local division information and its phone numbers
        /// </summary>
        /// <param name="divisionData">Division data do Save/Update</param>
        /// <param name="phoneNumbers">Phone numbers listing</param>
        public void SaveLocalDivision(CS_LocalDivision divisionData, IList<CS_DivisionPhoneNumber> phoneNumbers)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                IList<CS_DivisionPhoneNumber> addList = phoneNumbers.Where(e => e.ID == 0).ToList();

                if (divisionData.ID > 0)
                {
                    _localDivisionRepository.Update(divisionData);

                    IList<CS_DivisionPhoneNumber> oldList = _localDivisionPhoneNumberRepository.ListAll(e => e.DivisionID == divisionData.ID && e.Active);

                    List<int> updateIds = phoneNumbers.Where(e => e.ID > 0).Select(e => e.ID).ToList();
                    IList<CS_DivisionPhoneNumber> removeList = oldList.Where(e => !updateIds.Contains(e.ID)).ToList();
                    IList<CS_DivisionPhoneNumber> updateList = oldList.Where(e => updateIds.Contains(e.ID)).ToList();

                    foreach (CS_DivisionPhoneNumber item in removeList)
                    {
                        item.ModifiedBy = divisionData.ModifiedBy;
                        item.ModificationDate = divisionData.ModificationDate;
                        item.Active = false;
                    }

                    foreach (CS_DivisionPhoneNumber item in updateList)
                    {
                        CS_DivisionPhoneNumber updatedItem = phoneNumbers.Where(e => e.ID == item.ID).Single();

                        item.PhoneTypeID = updatedItem.PhoneTypeID;
                        item.Number = updatedItem.Number;
                        item.ModifiedBy = divisionData.ModifiedBy;
                        item.ModificationDate = divisionData.ModificationDate;
                    }

                    if (removeList.Count > 0)
                        _localDivisionPhoneNumberRepository.UpdateList(removeList);
                    if (updateList.Count > 0)
                        _localDivisionPhoneNumberRepository.UpdateList(updateList);
                }
                else
                {
                    _localDivisionRepository.Add(divisionData);
                }

                foreach (CS_DivisionPhoneNumber item in addList)
                {
                    item.DivisionID = divisionData.ID;
                    item.CreatedBy = divisionData.ModifiedBy;
                    item.CreationDate = divisionData.ModificationDate;
                    item.ModifiedBy = divisionData.ModifiedBy;
                    item.ModificationDate = divisionData.ModificationDate;
                    item.Active = true;
                }

                if (addList.Count > 0)
                    _localDivisionPhoneNumberRepository.AddList(addList);

                scope.Complete();
            }
        }

        public void DeleteLocalDivision(int divisionLocationID, string user)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                CS_LocalDivision localdivision = _localDivisionRepository.Get(e => e.ID == divisionLocationID && e.Active);

                if (null != localdivision)
                {
                    DateTime actionDate = DateTime.Now;

                    CS_LocalDivision updateLocalDivision = new CS_LocalDivision()
                    {
                        Active = false,
                        Address = localdivision.Address,
                        CityID = localdivision.CityID,
                        CreatedBy = localdivision.CreatedBy,
                        CreationDate = localdivision.CreationDate,
                        CreationID = localdivision.CreationID,
                        ID = localdivision.ID,
                        ModificationDate = actionDate,
                        //ModificationID,
                        ModifiedBy = user,
                        Name = localdivision.Name,
                        StateID = localdivision.StateID,
                        ZipCodeID = localdivision.ZipCodeID
                    };

                    IList<CS_DivisionPhoneNumber> divisionPhones = _localDivisionPhoneNumberRepository.ListAll(e => e.CS_LocalDivision.ID == divisionLocationID && e.Active);
                    IList<CS_DivisionPhoneNumber> updateDivisionPhones = new List<CS_DivisionPhoneNumber>();

                    for (int i = 0; i < divisionPhones.Count; i++)
                    {
                        updateDivisionPhones.Add
                        (
                            new CS_DivisionPhoneNumber()
                            {
                                Active = false,
                                CreatedBy = divisionPhones[i].CreatedBy,
                                CreationDate = divisionPhones[i].CreationDate,
                                CreationID = divisionPhones[i].CreationID,
                                DivisionID = divisionPhones[i].DivisionID,
                                ID = divisionPhones[i].ID,
                                ModificationDate = actionDate,
                                //ModificationID,
                                ModifiedBy = user,
                                Number = divisionPhones[i].Number,
                                PhoneTypeID = divisionPhones[i].PhoneTypeID
                            }
                        );
                    }

                    _localDivisionRepository.Update(updateLocalDivision);

                    if (updateDivisionPhones.Count > 0)
                        _localDivisionPhoneNumberRepository.UpdateList(updateDivisionPhones);
                }

                scope.Complete();
            }
        }
    }
}
