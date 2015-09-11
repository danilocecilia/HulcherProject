using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core.DaoInterfaces;
using System.Data.Objects;

namespace Hulcher.OneSource.CustomerService.Data
{
    public class ResourceDao : BaseDao<CS_Resource, int>, IResourceDao
    {
        #region [ Attributes ]

        private static Func<CustomerServiceModelContainer, int, IQueryable<CS_Resource>> _getResourceQuery;

        private static Func<CustomerServiceModelContainer, int, IQueryable<CS_Resource>> _getResourceByJob;

        private static Func<CustomerServiceModelContainer, int, IQueryable<CS_Resource>> _listAllResourcesInfoByJobQuery;

        private static IResourceDao _singleton;

        #endregion

        #region [ Constructor ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        public ResourceDao() { }

        #endregion

        #region [ Singleton ]

        public static IResourceDao Singleton
        {
            get
            {
                if (null == _singleton)
                    _singleton = new ResourceDao();

                return _singleton;
            }
        }

        #endregion

        #region [ Properties ]

        public Func<CustomerServiceModelContainer, int, IQueryable<CS_Resource>> GetResourceQuery
        {
            get
            {
                if (_getResourceQuery == null)
                {
                    _getResourceQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, int resourceId) => from e in ctx.CS_Resource
                                                                              .Include("CS_Employee")
                                                                              .Include("CS_Equipment")
                                                                              .Include("CS_Job")
                                                                              .Include("CS_Job.CS_CallLog")
                                                                              .Include("CS_Job.CS_CallLog.CS_CallType")
                                                                               where e.ID == resourceId
                                                                               && e.Active
                                                                               select e);
                }
                return _getResourceQuery;
            }
        }

        public Func<CustomerServiceModelContainer, int, IQueryable<CS_Resource>> GetResourcesByJobId
        {
            get
            {
                if(_getResourceByJob == null)
                {
                    _getResourceByJob = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, int jobId) => from e in ctx.CS_Resource
                                                                          where e.JobID == jobId
                                                                          && e.Active == true
                                                                          select e);
                }
                return _getResourceByJob;
            }
        }
        
        /// <summary>
        /// Property for a Compiled Query to List Resources by Job
        /// </summary>
        public Func<CustomerServiceModelContainer, int, IQueryable<CS_Resource>> ListAllResourcesInfoByJobQuery
        {
            get
            {
                if (_listAllResourcesInfoByJobQuery == null)
                {
                    _listAllResourcesInfoByJobQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, int jobId) => from e in ctx.CS_Resource
                                                                          .Include("CS_Employee")
                                                                          .Include("CS_Equipment")
                                                                          .Include("CS_Job")
                                                                          .Include("CS_Job.CS_CallLog")
                                                                          .Include("CS_Job.CS_CallLog.CS_CallType")
                                                                          where e.JobID == jobId
                                                                          && e.Active == true
                                                                          select e);
                }
                return _listAllResourcesInfoByJobQuery;
            }
        }
        #endregion

        #region [ IResourceDao Implementation ]

        /// <summary>
        /// Get the especified resource
        /// </summary>
        /// <param name="resourceId">Resource Identifier</param>
        /// <returns>Resource entity</returns>
        public CS_Resource GetResource(int resourceId)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                return GetResourceQuery.Invoke(db, resourceId).FirstOrDefault();
            }

        }

        /// <summary>
        /// List all resources by job
        /// </summary>
        /// <param name="jobId">jobid</param>
        /// <returns>list</returns>
        public IList<CS_Resource> ListResourcesByJob(int jobId)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                return GetResourcesByJobId.Invoke(db, jobId).ToList();
            }
            
        }

        /// <summary>
        /// List all resources by job for call entry
        /// </summary>
        /// <param name="jobId">jobid</param>
        /// <returns>list</returns>
        public IList<CS_Resource> ListAllResourcesInfoByJob(int jobId)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                return ListAllResourcesInfoByJobQuery.Invoke(db, jobId).ToList();
            }

        }

        /// <summary>
        /// Clear all records (for unit testing)
        /// </summary>
        public void ClearAll()
        {
            using (var db = new CustomerServiceModelContainer())
            {
                ExecuteSql(db, "delete from CS_Resource");
            }
        }

        #endregion
    }
}
