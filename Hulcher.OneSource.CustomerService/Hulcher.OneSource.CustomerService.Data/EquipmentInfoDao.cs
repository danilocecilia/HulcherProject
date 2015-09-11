using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Core.Utils;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core.DaoInterfaces;
using System.Data.Objects;
using System.Data.Linq.SqlClient;

namespace Hulcher.OneSource.CustomerService.Data
{
    public class EquipmentInfoDao : IEquipmentInfoDao
    {
        #region [ Attributes ]

        /// <summary>
        /// Static attribute to store the Compiled Query for listing all EquipmentInfo
        /// </summary>
        private static Func<CustomerServiceModelContainer, IQueryable<CS_View_EquipmentInfo>> _listAllQuery;

        private static Func<CustomerServiceModelContainer, IQueryable<CS_View_EquipmentInfo>> _listAllComboQuery;

        private static Func<CustomerServiceModelContainer, int, IQueryable<CS_View_EquipmentInfo>> _listAllEquipmentQuery;

        /// <summary>
        /// Static attribute to store the Compiled Query for getting a specific EquipmentInfo
        /// </summary>
        private static Func<CustomerServiceModelContainer, int, IQueryable<CS_View_EquipmentInfo>> _getQuery;

        /// <summary>
        /// Singleton attribute
        /// </summary>
        private static EquipmentInfoDao _singleton;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        public EquipmentInfoDao() { }

        #endregion

        #region [ Singleton ]

        /// <summary>
        /// Singleton Property - Contains an instance of this Class
        /// </summary>
        public static IEquipmentInfoDao Singleton
        {
            get
            {
                if (null == _singleton)
                    _singleton = new EquipmentInfoDao();

                return _singleton;
            }
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// List all items of an Entity
        /// </summary>
        /// <returns>List of Entities</returns>
        public IList<CS_View_EquipmentInfo> ListAll()
        {
            using (var db = new CustomerServiceModelContainer())
            {
                IList<CS_View_EquipmentInfo> returnList = ListAllQuery.Invoke(db).ToList<CS_View_EquipmentInfo>();
                return returnList;
            }
        }

        /// <summary>
        /// Get a specific Entity in the Database
        /// </summary>
        /// <param name="id">Entity's Identifier (Primary Key)</param>
        /// <returns>Entity loaded with all its information</returns>
        public CS_View_EquipmentInfo Get(int id)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                CS_View_EquipmentInfo returnItem = GetQuery.Invoke(db, id).FirstOrDefault<CS_View_EquipmentInfo>();
                return returnItem;
            }
        }

        /// <summary>
        /// List All Equipments filtered by Division
        /// </summary>
        /// <param name="divisionNames"></param>
        /// <returns></returns>
        public IList<CS_View_EquipmentInfo> ListFilteredByDivision(string[] divisionNames)
        {
            using (var db = new CustomerServiceModelContainer())
            {                         
                return db.CS_View_EquipmentInfo.Where(e => divisionNames.Any(division => e.DivisionName.Contains(division))).OrderBy(e => e.ComboName).ToList();
            }
        }

        /// <summary>
        /// List Filtered items of an Entity
        /// </summary>
        /// <returns>List of Entities</returns>
        public IList<CS_View_EquipmentInfo> ListFilteredByDivisionState(string[] divisionStates)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                return db.CS_View_EquipmentInfo.Where(e => divisionStates.Any(state => e.DivisionState.Contains(state))).OrderBy(e => e.ComboName).ToList();           
            }
        }

        /// <summary>
        /// List Filtered items of an Entity
        /// </summary>
        /// <returns>List of Entities</returns>
        public IList<CS_View_EquipmentInfo> ListFilteredByComboNumber(string[] comboNumbers)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                return db.CS_View_EquipmentInfo.Where(e => comboNumbers.Any(comboNumber => e.ComboName.Contains(comboNumber))).OrderBy(e => e.ComboName).ToList();
            }
        }

        /// <summary>
        /// List Filtered items of an Entity
        /// </summary>
        /// <returns>List of Entities</returns>
        public IList<CS_View_EquipmentInfo> ListFilteredByUnitNumber(string[] unitNumbers)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                return db.CS_View_EquipmentInfo.Where(e => unitNumbers.Any(unitNumber => e.UnitNumber.Contains(unitNumber))).OrderBy(e => e.ComboName).ToList();
            }
        }

        /// <summary>
        /// List Filtered items of an Entity
        /// </summary>
        /// <returns>List of Entities</returns>
        public IList<CS_View_EquipmentInfo> ListFilteredByStatus(string[] statusList)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                return db.CS_View_EquipmentInfo.Where(e => statusList.Any(status => e.Status.Contains(status))).OrderBy(e => e.ComboName).ToList();
            }
        }

        /// <summary>
        /// List Filtered items of an Entity
        /// </summary>
        /// <returns>List of Entities</returns>
        public IList<CS_View_EquipmentInfo> ListFilteredByJobLocation(string[] jobLocations)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                return db.CS_View_EquipmentInfo.Where(e => jobLocations.Any(location => e.JobLocation.Contains(location))).OrderBy(e => e.ComboName).ToList();
            }
        }

        /// <summary>
        /// List Filtered items of an Entity
        /// </summary>
        /// <returns>List of Entities</returns>
        public IList<CS_View_EquipmentInfo> ListFilteredByCallType(string[] callTypes)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                return db.CS_View_EquipmentInfo.Where(e => callTypes.Any(callType => e.Type.Contains(callType))).OrderBy(e => e.ComboName).ToList();
            }
        }

        #endregion

        #region [ Abstract BaseModel Implementation ]

        /// <summary>
        /// Property for a Compiled Query to List All itens of an Entity
        /// </summary>
        public Func<CustomerServiceModelContainer, IQueryable<CS_View_EquipmentInfo>> ListAllQuery
        {
            get
            {
                if (null == _listAllQuery)
                {
                    _listAllQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx) => from e in ctx.CS_View_EquipmentInfo
                                                               where e.Active == true
                                                               select e);

                }
                return _listAllQuery;
            }
        }

        /// <summary>
        /// Property for a Compiled Query to Get a specific Entity through its Identifier
        /// </summary>
        public Func<CustomerServiceModelContainer, int, IQueryable<CS_View_EquipmentInfo>> GetQuery
        {
            get
            {
                if (null == _getQuery)
                {
                    _getQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, int equipmentId) => from e in ctx.CS_View_EquipmentInfo
                                                                                where e.EquipmentID == equipmentId && e.Active == true
                                                                                select e);

                }
                return _getQuery;
            }
        }

        #endregion

    }
}
