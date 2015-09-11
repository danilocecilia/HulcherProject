using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core.DaoInterfaces;
using System.Data.Objects;

namespace Hulcher.OneSource.CustomerService.Data
{
    public class CustomerInfoDao : BaseDao<CS_CustomerInfo, long>, ICustomerInfoDao
    {
        #region [ Attributes ]

        /// <summary>
        /// Singleton attribute
        /// </summary>
        private static ICustomerInfoDao _singleton;

        /// <summary>
        /// Singleton attribute for list by jobId query
        /// </summary>
        private static Func<CustomerServiceModelContainer, long, IQueryable<CS_CustomerInfo>> _getByJobIdQuery;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        public CustomerInfoDao() { }

        #endregion

        #region [ Singleton ]

        /// <summary>
        /// Singleton Property - Contains an instance of this Class
        /// </summary>
        public static ICustomerInfoDao Singleton
        {
            get
            {
                if (null == _singleton)
                    _singleton = new CustomerInfoDao();

                return _singleton;
            }
        }

        #endregion

        #region [ Properties ]

        public static Func<CustomerServiceModelContainer, long, IQueryable<CS_CustomerInfo>> GetByJobIdQuery
        {
            get
            {
                if (null == _getByJobIdQuery)
                {
                    _getByJobIdQuery =
                        CompiledQuery.Compile((CustomerServiceModelContainer ctx, long jobId) => from e in ctx.CS_CustomerInfo
                                                                                                 where e.JobId == jobId
                                                                                                 && e.Active
                                                                                                 select e);
                }

                return _getByJobIdQuery;
            }
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Retrieves a CustomerInfo associated to a job
        /// </summary>
        /// <param name="jobId">ID of the Job</param>
        /// <returns>Entity containing Customer Information</returns>
        public CS_CustomerInfo GetByJobId(long jobId)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                return GetByJobIdQuery.Invoke(db, jobId).FirstOrDefault();
            }
        }

        #endregion
    }
}
