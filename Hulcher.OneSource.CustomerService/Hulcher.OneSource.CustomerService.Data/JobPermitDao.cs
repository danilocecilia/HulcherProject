using System;
using System.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core.DaoInterfaces;

namespace Hulcher.OneSource.CustomerService.Data
{
    /// <summary>
    /// Job Permit Class
    /// </summary>
    public class JobPermitDao : BaseDao<CS_JobPermit, int>, IJobPermitDao
    {
        #region [ Attributes ]

        /// <summary>
        /// Static attribute to store the Compiled Query for getting a specific Job by jobid
        /// </summary>
        private static Func<CustomerServiceModelContainer, int, IQueryable<CS_JobPermit>> _getPermitInfoByJobId;

        /// <summary>
        /// Singleton attribute
        /// </summary>
        private static IJobPermitDao _singleton;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        public JobPermitDao() { }

        #endregion

        #region [ Singleton ]

        /// <summary>
        /// Singleton Property - Contains an instance of this Class
        /// </summary>
        public static IJobPermitDao Singleton
        {
            get
            {
                if (null == _singleton)
                    _singleton = new JobPermitDao();

                return _singleton;
            }
        }

        #endregion

        #region [ IJobPermitDao Implementation ]

        /// <summary>
        /// Property for a Compiled Query to Get a specific Entity through its Identifier
        /// </summary>
        public Func<CustomerServiceModelContainer, int, IQueryable<CS_JobPermit>> GetPermitInfoByJobId
        {
            get
            {
                if (_getPermitInfoByJobId == null)
                {
                    _getPermitInfoByJobId = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, int jobId) => from e in ctx.CS_JobPermit
                                                                          where e.JobID == jobId
                                                                          && e.Active == true
                                                                          select e);
                }
                return _getPermitInfoByJobId;
            }
        }

        /// <summary>
        /// Executes a query that retrieve a entity related to a job id
        /// </summary>
        /// <param name="jobId">job id</param>
        /// <returns>entity jobpermit</returns>
        public List<CS_JobPermit> GetPermitInfoByJob(int jobId)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                return GetPermitInfoByJobId.Invoke(db, jobId).ToList();
            }
        }

        #endregion
    }
}
