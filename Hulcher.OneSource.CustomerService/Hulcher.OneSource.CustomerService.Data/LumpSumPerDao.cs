using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Core.DaoInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;
using System.Data.Objects;

namespace Hulcher.OneSource.CustomerService.Data
{
    public class LumpSumPerDao : BaseDao<CS_LumpSumPer, int>, ILumpSumPer
    {
        #region [ Attributes ]

        /// <summary>
        /// Singleton attribute
        /// </summary>
        private static ILumpSumPer _singleton;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        private LumpSumPerDao() { }

        #endregion

        #region [ Singleton ]

        /// <summary>
        /// Singleton Property - Contains an instance of this Class
        /// </summary>
        public static ILumpSumPer Singleton
        {
            get
            {
                if (null == _singleton)
                    _singleton = new LumpSumPerDao();

                return _singleton;
            }
        }

        #endregion
    }
}
