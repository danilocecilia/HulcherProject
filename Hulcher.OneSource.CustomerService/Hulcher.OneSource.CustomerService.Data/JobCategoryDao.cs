using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Core.DaoInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Data
{
    public class JobCategoryDao : BaseDao<CS_JobCategory, int>, IJobCategoryDao
    {
        #region [ Attributes ]

        /// <summary>
        /// Singleton attribute
        /// </summary>
        private static IJobCategoryDao _singleton;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        private JobCategoryDao() { }

        #endregion

        #region [ Singleton ]

        /// <summary>
        /// Singleton Property - Contains an instance of this Class
        /// </summary>
        public static IJobCategoryDao Singleton
        {
            get
            {
                if (null == _singleton)
                    _singleton = new JobCategoryDao();

                return _singleton;
            }
        }

        #endregion
    }
}
