using System;
using System.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core.DaoInterfaces;

namespace Hulcher.OneSource.CustomerService.Data
{
    public class JobInfoDao : BaseDao<CS_JobInfo, int>, IJobInfoDao
    {
        #region [ Attributes ]

        /// <summary>
        /// Singleton attribute
        /// </summary>
        private static IJobInfoDao _singleton;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        public JobInfoDao() { }

        #endregion

        #region [ Singleton ]

        /// <summary>
        /// Singleton Property - Contains an instance of this Class
        /// </summary>
        public static IJobInfoDao Singleton
        {
            get
            {
                if (null == _singleton)
                    _singleton = new JobInfoDao();

                return _singleton;
            }
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Clears the table (for unit testing)
        /// </summary>
        public void ClearAll()
        {
            using (var db = new CustomerServiceModelContainer())
            {
                ExecuteSql(db, "delete from cs_jobinfo");
            }
        }

        #endregion
    }
}
