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
    /// Job Photo Report Class
    /// </summary>
    public class JobPhotoReportDao : BaseDao<CS_JobPhotoReport, int>, IJobPhotoReportDao
    {
        #region [ Attributes ]

        /// <summary>
        /// Static attribute to store the Compiled Query for getting a list of Photo Report by Job ID
        /// </summary>
        private static Func<CustomerServiceModelContainer, int, IQueryable<CS_JobPhotoReport>> _getPhotoReportByJobIdQuery;

        /// <summary>
        /// Singleton attribute
        /// </summary>
        private static IJobPhotoReportDao _singleton;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        public JobPhotoReportDao() { }

        #endregion

        #region [ Singleton ]

        /// <summary>
        /// Singleton Property - Contains an instance of this Class
        /// </summary>
        public static IJobPhotoReportDao Singleton
        {
            get
            {
                if (null == _singleton)
                    _singleton = new JobPhotoReportDao();
                
                return _singleton;
            }
        }

        #endregion

        #region [ IJobPhotoReportDao Implementation ]

        /// <summary>
        /// Property for a Compiled Query to Get a specific Entity through its Identifier
        /// </summary>
        public Func<CustomerServiceModelContainer, int, IQueryable<CS_JobPhotoReport>> GetPhotoReportByJobIdQuery
        {
            get
            {
                if (_getPhotoReportByJobIdQuery == null)
                {
                    _getPhotoReportByJobIdQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, int jobId) => from e in ctx.CS_JobPhotoReport
                                                                          where e.JobID == jobId
                                                                          && e.Active == true
                                                                          select e);
                }
                return _getPhotoReportByJobIdQuery;
            }
        }

        /// <summary>
        /// Get a list of Photo Reports by Job Id
        /// </summary>
        /// <param name="jobId">Job ID</param>
        /// <returns>List of Photo Reports Entity</returns>
        public List<CS_JobPhotoReport> GetJobPhotoReportByJob(int jobId)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                return GetPhotoReportByJobIdQuery.Invoke(db, jobId).ToList();
            }
        }

        #endregion
    }
}
