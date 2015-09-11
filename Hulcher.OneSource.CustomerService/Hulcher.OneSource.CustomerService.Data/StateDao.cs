using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using Hulcher.OneSource.CustomerService.Core.DaoInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Data
{
    public class StateDao : BaseDao<CS_State, int>, IStateDao
    {
        #region [ Attributes ]

        /// <summary>
        /// Static attribute to store the Compiled Query for getting a list of states of a specific Country
        /// </summary>
        private static Func<CustomerServiceModelContainer, int, IQueryable<CS_State>> _getStateByCountryId;

        /// <summary>
        /// Static attribute to store the Compiled Query for getting all States ordered by country
        /// </summary>
        private static Func<CustomerServiceModelContainer, IQueryable<CS_State>> _listAllOrderedByCountry;

        /// <summary>
        /// Singleton attribute
        /// </summary>
        private static IStateDao _singleton;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        private StateDao() { }

        #endregion

        #region [ Singleton ]

        /// <summary>
        /// Singleton Property - Contains an instance of this Class
        /// </summary>
        public static IStateDao Singleton
        {
            get
            {
                if (null == _singleton)
                    _singleton = new StateDao();

                return _singleton;
            }
        }

        #endregion
        
        #region [ IState Implementation ]
        
        /// <summary>
        /// Property for a Compiled Query to Get a specific Entity through its Identifier
        /// </summary>
        public Func<CustomerServiceModelContainer, int, IQueryable<CS_State>> GetStateByCountryId
        {
            get
            {
                if (null == _getStateByCountryId)
                {
                    _getStateByCountryId = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, int countryId) => from e in ctx.CS_State
                                                                              where e.CountryID == countryId && e.Active == true
                                                                              orderby e.Name
                                                                              select e);
                }
                return _getStateByCountryId;
            }
        }

        /// <summary>
        /// Get a state entity by country id
        /// </summary>
        /// <param name="countryId">country id</param>
        /// <returns>state entity</returns>
        public IList<CS_State> GetStateByCountry(int countryId)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                return GetStateByCountryId.Invoke(db, countryId).ToList();
            }
        }

        /// <summary>
        /// Property for a Compiled Query to Get a specific Entity through its Identifier
        /// </summary>
        public Func<CustomerServiceModelContainer, IQueryable<CS_State>> ListAllOrderedByCountry
        {
            get
            {
                if (null == _listAllOrderedByCountry)
                {
                    _listAllOrderedByCountry = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx) => from e in ctx.CS_State
                                                                where e.Active == true
                                                                orderby e.CountryID ascending
                                                                select e);
                }
                return _listAllOrderedByCountry;
            }
        }

        /// <summary>
        /// Get a state entity by country id
        /// </summary>
        /// <param name="countryId">country id</param>
        /// <returns>state entity</returns>
        public IList<CS_State> ListAllByCountry()
        {
            using (var db = new CustomerServiceModelContainer())
            {
                return ListAllOrderedByCountry.Invoke(db).ToList();
            }
        }
        #endregion
    }
}

