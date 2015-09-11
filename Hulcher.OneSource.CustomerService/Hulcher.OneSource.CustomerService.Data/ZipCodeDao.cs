using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core.DaoInterfaces;

namespace Hulcher.OneSource.CustomerService.Data
{
    public class ZipCodeDao : BaseDao<CS_ZipCode, int>, IZipCodeDao
    {
        #region [ Attributes ]

        /// <summary>
        /// Static attribute to store the Compiled Query for getting a specific Job
        /// </summary>
        private static Func<CustomerServiceModelContainer, int, IQueryable<CS_ZipCode>> _getZipCodeByCityId;

        /// <summary>
        /// Singleton attribute
        /// </summary>
        private static IZipCodeDao _singleton;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        private ZipCodeDao() { }

        #endregion

        #region [ Singleton ]

        /// <summary>
        /// Singleton Property - Contains an instance of this Class
        /// </summary>
        public static IZipCodeDao Singleton
        {
            get
            {
                if (null == _singleton)
                    _singleton = new ZipCodeDao();

                return _singleton;
            }
        }

        #endregion

        #region [ IZipCodeDao Implementation ]
        /// <summary>
        /// Property for a Compiled Query to Get a specific Entity through its Identifier
        /// </summary>
        public Func<CustomerServiceModelContainer, int, IQueryable<CS_ZipCode>> GetZipCodeByCityId
        {
            get
            {
                if (null == _getZipCodeByCityId)
                {
                    _getZipCodeByCityId = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, int zipCodeId) => from e in ctx.CS_ZipCode
                                                                              where e.CityId == zipCodeId && e.Active == true
                                                                              orderby e.Name
                                                                              select e);
                }
                return _getZipCodeByCityId;
            }
        }

        /// <summary>
        /// Get zip code by city
        /// </summary>
        /// <param name="zipCodeId">zipcode id</param>
        /// <returns>list of zipcode</returns>
        public IList<CS_ZipCode> GetZipCodeByCity(int zipCodeId)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                return GetZipCodeByCityId.Invoke(db, zipCodeId).ToList();
            }
        }
        #endregion
    }
}
