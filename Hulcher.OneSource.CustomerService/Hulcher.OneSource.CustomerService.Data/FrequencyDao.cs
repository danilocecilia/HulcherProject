using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Core.DaoInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Data
{
    public class FrequencyDao : BaseDao<CS_Frequency, int>, IFrequencyDao
    {
        #region [ Attributes ]

        /// <summary>
        /// Singleton attribute
        /// </summary>
        private static IFrequencyDao _singleton;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        private FrequencyDao() { }

        #endregion

        #region [ Singleton ]

        /// <summary>
        /// Singleton Property - Contains an instance of this Class
        /// </summary>
        public static IFrequencyDao Singleton
        {
            get
            {
                if (null == _singleton)
                    _singleton = new FrequencyDao();

                return _singleton;
            }
        }

        #endregion
    }
}
