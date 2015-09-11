using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Core.DaoInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;
using System.Data.Objects;

namespace Hulcher.OneSource.CustomerService.Data
{
    public class ResourceTypeDao : BaseDao<CS_ResourceType, int>, IResourceTypeDao
    {
        #region [ Attributes ]

        /// <summary>
        /// Singleton attribute
        /// </summary>
        private static IResourceTypeDao _singleton;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        private ResourceTypeDao() { }

        #endregion

        #region [ Singleton ]

        /// <summary>
        /// Singleton Property - Contains an instance of this Class
        /// </summary>
        public static IResourceTypeDao Singleton
        {
            get
            {
                if (null == _singleton)
                    _singleton = new ResourceTypeDao();

                return _singleton;
            }
        }

        #endregion
    }
}
