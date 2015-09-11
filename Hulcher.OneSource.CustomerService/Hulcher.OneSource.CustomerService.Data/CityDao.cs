using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using Hulcher.OneSource.CustomerService.Core.DaoInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Data
{
    public class CityDao : BaseDao<CS_City, int>, ICityDao
    {
        #region [ Attributes ]

        /// <summary>
        /// Static attribute to store the Compiled Query for getting a specific Job
        /// </summary>
        private static Func<CustomerServiceModelContainer, int, IQueryable<CS_City>> _getCityByState;

        /// <summary>
        /// Singleton attribute
        /// </summary>
        private static ICityDao _singleton;

        #endregion

        #region [ Constructors ]
        /// <summary>
        /// Class contructor
        /// </summary>
        public CityDao() { }

        #endregion

        #region [ Singleton ]

        /// <summary>
        /// Singleton Property - Contains an instance of this Class
        /// </summary>
        public static ICityDao Singleton
        {
            get
            {
                if (null == _singleton)
                    _singleton = new CityDao();

                return _singleton;
            }
        }
       
        #endregion

        #region [ Implementation Interface ICityDao ]
        /// <summary>
        /// Property for a Compiled Query to Get a specific Entity through its Identifier
        /// </summary>
        public Func<CustomerServiceModelContainer, int, IQueryable<CS_City>> GetCityByStateId
        {
            get
            {
                if (_getCityByState == null)
                {
                    _getCityByState =
                        CompiledQuery.Compile((CustomerServiceModelContainer ctx, int stateId) => from e in ctx.CS_City
                                                                                                  where e.CS_State.ID == stateId
                                                                                                  orderby e.Name
                                                                                                  select e);

                }
                return _getCityByState;
            }
        }

        /// <summary>
        /// Get list all cities by state id
        /// </summary>
        /// <param name="stateId">state id</param>
        /// <returns>list of cities</returns>
        public IList<CS_City> GetCityByState(int stateId)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                return GetCityByStateId.Invoke(db, stateId).ToList();
            }
        }
        #endregion
    }
}
