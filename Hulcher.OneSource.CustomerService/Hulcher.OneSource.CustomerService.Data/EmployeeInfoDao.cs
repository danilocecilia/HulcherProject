using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core.DaoInterfaces;
using System.Data.Objects;

namespace Hulcher.OneSource.CustomerService.Data
{
    public class EmployeeInfoDao : IEmployeeInfoDao
    {
        #region [ Attributes ]

        /// <summary>
        /// Static attribute to store the Compiled Query for listing all EmployeeInfo
        /// </summary>
        private static Func<CustomerServiceModelContainer, IQueryable<CS_View_EmployeeInfo>> _listAllQuery;

        /// <summary>
        /// Static attribute to store the Compiled Query for getting a specific EmployeeInfo
        /// </summary>
        private static Func<CustomerServiceModelContainer, int, IQueryable<CS_View_EmployeeInfo>> _getQuery;

        /// <summary>
        /// Singleton attribute
        /// </summary>
        private static IEmployeeInfoDao _singleton;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        public EmployeeInfoDao() { }

        #endregion

        #region [ Singleton ]

        /// <summary>
        /// Singleton Property - Contains an instance of this Class
        /// </summary>
        public static IEmployeeInfoDao Singleton
        {
            get
            {
                if (null == _singleton)
                    _singleton = new EmployeeInfoDao();

                return _singleton;
            }
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// List all items of an Entity
        /// </summary>
        /// <returns>List of Entities</returns>
        public IList<CS_View_EmployeeInfo> ListAll()
        {
            using (var db = new CustomerServiceModelContainer())
            {
                IList<CS_View_EmployeeInfo> returnList = ListAllQuery.Invoke(db).ToList<CS_View_EmployeeInfo>();
                return returnList;
            }
        }

        /// <summary>
        /// List Filtered items of an Entity
        /// </summary>
        /// <returns>List of Entities</returns>
        public IList<CS_View_EmployeeInfo> ListFilteredByDivision(string[] divisionNames)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                IList<CS_View_EmployeeInfo> returnList = db.CS_View_EmployeeInfo.Where(e => divisionNames.Contains(e.DivisionName)).OrderBy(e => e.DivisionName).ToList();
                return returnList;
            }
        }

        /// <summary>
        /// List Filtered items of an Entity
        /// </summary>
        /// <returns>List of Entities</returns>
        public IList<CS_View_EmployeeInfo> ListFilteredByDivisionState(string[] divisionStates)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                IList<CS_View_EmployeeInfo> returnList = db.CS_View_EmployeeInfo.Where(e => divisionStates.Contains(e.State)).OrderBy(e => e.DivisionName).ToList();
                return returnList;
            }
        }

        /// <summary>
        /// List Filtered items of an Entity
        /// </summary>
        /// <returns>List of Entities</returns>
        public IList<CS_View_EmployeeInfo> ListFilteredByStatus(string[] status)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                IList<CS_View_EmployeeInfo> returnList = db.CS_View_EmployeeInfo.Where(e => status.Contains(e.Assigned)).OrderBy(e => e.DivisionName).ToList();
                return returnList;
            }
        }

        /// <summary>
        /// List Filtered items of an Entity
        /// </summary>
        /// <returns>List of Entities</returns>
        public IList<CS_View_EmployeeInfo> ListFilteredByJobNumber(string[] jobNumbers)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                IList<CS_View_EmployeeInfo> returnList = db.CS_View_EmployeeInfo.Where(e => jobNumbers.Contains(e.JobNumber)).OrderBy(e => e.DivisionName).ToList();
                return returnList;
            }
        }

        /// <summary>
        /// List Filtered items of an Entity
        /// </summary>
        /// <returns>List of Entities</returns>
        public IList<CS_View_EmployeeInfo> ListFilteredByPosition(string[] positionNames)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                IList<CS_View_EmployeeInfo> returnList = db.CS_View_EmployeeInfo.Where(e => positionNames.Contains(e.Position)).OrderBy(e => e.Position).ToList();

                return returnList;
            }
        }

        /// <summary>
        /// List Filtered items of an Entity
        /// </summary>
        /// <returns>List of Entities</returns>
        public IList<CS_View_EmployeeInfo> ListFilteredByEmployee(string[] employeeNames)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                IList<CS_View_EmployeeInfo> returnList = db.CS_View_EmployeeInfo.Where(e => employeeNames.Contains(e.EmployeeName)).OrderBy(e => e.EmployeeName).ToList();
                return returnList;
            }
        }

        /// <summary>
        /// Get a specific Entity in the Database
        /// </summary>
        /// <param name="id">Entity's Identifier (Primary Key)</param>
        /// <returns>Entity loaded with all its information</returns>
        public CS_View_EmployeeInfo Get(int id)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                CS_View_EmployeeInfo returnItem = GetQuery.Invoke(db, id).FirstOrDefault<CS_View_EmployeeInfo>();
                return returnItem;
            }
        }

        #endregion

        #region [ Abstract BaseModel Implementation ]

        /// <summary>
        /// Property for a Compiled Query to List All itens of an Entity
        /// </summary>
        public Func<CustomerServiceModelContainer, IQueryable<CS_View_EmployeeInfo>> ListAllQuery
        {
            get
            {
                if (null == _listAllQuery)
                {
                    _listAllQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx) => from e in ctx.CS_View_EmployeeInfo
                                                               where e.Active == true
                                                               select e);

                }
                return _listAllQuery;
            }
        }

        /// <summary>
        /// Property for a Compiled Query to Get a specific Entity through its Identifier
        /// </summary>
        public Func<CustomerServiceModelContainer, int, IQueryable<CS_View_EmployeeInfo>> GetQuery
        {
            get
            {
                if (null == _getQuery)
                {
                    _getQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, int employeeId) => from e in ctx.CS_View_EmployeeInfo
                                                                               where e.EmployeeId == employeeId && e.Active == true
                                                                               select e);

                }
                return _getQuery;
            }
        }

        #endregion
    }
}
