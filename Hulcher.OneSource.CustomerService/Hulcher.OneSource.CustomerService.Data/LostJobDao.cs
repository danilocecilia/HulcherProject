using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Core.DaoInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Data
{
    public class LostJobDao : BaseDao<CS_LostJobInfo, int>, ILostJobInfoDao
    {
        #region [ Attributes ]

        /// <summary>
        /// Singleton attribute
        /// </summary>
        private static ILostJobInfoDao _singleton;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        private LostJobDao() { }

        #endregion

        #region [ Singleton ]

        /// <summary>
        /// Singleton Property - Contains an instance of this Class
        /// </summary>
        public static ILostJobInfoDao Singleton
        {
            get
            {
                if (null == _singleton)
                    _singleton = new LostJobDao();

                return _singleton;
            }
        }

        #endregion
    }
}
