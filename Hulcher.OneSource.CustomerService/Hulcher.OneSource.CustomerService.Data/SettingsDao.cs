using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core.DaoInterfaces;

namespace Hulcher.OneSource.CustomerService.Data
{
    public class SettingsDao : BaseDao<CS_Settings, int>, ISettingsDao
    {
        #region [ Attributes ]

        /// <summary>
        /// Singleton attribute
        /// </summary>
        private static ISettingsDao _singleton;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        public SettingsDao() { }

        #endregion

        #region [ Singleton ]

        /// <summary>
        /// Singleton Property - Contains an instance of this Class
        /// </summary>
        public static ISettingsDao Singleton
        {
            get
            {
                if (null == _singleton)
                    _singleton = new SettingsDao();

                return _singleton;
            }
        }

        #endregion
    }
}
