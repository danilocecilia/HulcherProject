using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.DaoInterfaces
{
    public interface IResourceDao : IBaseDao<CS_Resource, int>
    {
        /// <summary>
        /// List all resources by job
        /// </summary>
        /// <param name="jobId">jobid</param>
        /// <returns>list</returns>
        IList<CS_Resource> ListResourcesByJob(int jobId);


        /// <summary>
        /// List all resources by job for call entry
        /// </summary>
        /// <param name="jobId">jobid</param>
        /// <returns>list</returns>
        IList<CS_Resource> ListAllResourcesInfoByJob(int jobId);

        /// <summary>
        /// Get the especified resource
        /// </summary>
        /// <param name="resourceId">Resource Identifier</param>
        /// <returns>Resource entity</returns>
        CS_Resource GetResource(int resourceId);

        /// <summary>
        /// Clear all records (for unit testing)
        /// </summary>
        void ClearAll();
    }
}
