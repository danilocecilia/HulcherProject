using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core.DaoInterfaces;
using System.Data.Objects;

namespace Hulcher.OneSource.CustomerService.Data
{
    public class LocationInfoDao : BaseDao<CS_LocationInfo, long>, ILocationInfoDao
    {
        #region [ Attributes ]

        /// <summary>
        /// Singleton attribute
        /// </summary>
        private static ILocationInfoDao _singleton;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        public LocationInfoDao() { }

        #endregion

        #region [ Singleton ]

        /// <summary>
        /// Singleton Property - Contains an instance of this Class
        /// </summary>
        public static ILocationInfoDao Singleton
        {
            get
            {
                if (null == _singleton)
                    _singleton = new LocationInfoDao();

                return _singleton;
            }
        }

        #endregion
    }
}
