using System;
using System.Data.Objects;
using System.Linq;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core.DaoInterfaces;

namespace Hulcher.OneSource.CustomerService.Data
{
    /// <summary>
    /// Country Model Class
    /// </summary>
    public class CountryDao : BaseDao<CS_Country, int>, ICountryDao
    {
        #region [ Attributes ]

        /// <summary>
        /// Singleton attribute
        /// </summary>
        private static ICountryDao _singleton;

        #endregion

        #region [ Constructors ]
        /// <summary>
        /// Class contructor
        /// </summary>
        public CountryDao() { }

        #endregion

        #region [ Singleton ]

        /// <summary>
        /// Singleton Property - Contains an instance of this Class
        /// </summary>
        public static ICountryDao Singleton
        {
            get
            {
                if (null == _singleton)
                    _singleton = new CountryDao();

                return _singleton;
            }
        }

       
        #endregion

    }
}
