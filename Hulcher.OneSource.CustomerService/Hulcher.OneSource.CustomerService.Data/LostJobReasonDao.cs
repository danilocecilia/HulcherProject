using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Core.DaoInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;


namespace Hulcher.OneSource.CustomerService.Data
{
    public class LostJobReasonDao : BaseDao<CS_LostJobReason, int>, ILostJobReasonDao
    {
        #region [ Attributes ]

        /// <summary>
        /// Singleton attribute
        /// </summary>
        private static ILostJobReasonDao _singleton;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        private LostJobReasonDao() { }

        #endregion

        #region [ Singleton ]

        /// <summary>
        /// Singleton Property - Contains an instance of this Class
        /// </summary>
        public static ILostJobReasonDao Singleton
        {
            get
            {
                if (null == _singleton)
                    _singleton = new LostJobReasonDao();

                return _singleton;
            }
        }

        #endregion
    }
}
