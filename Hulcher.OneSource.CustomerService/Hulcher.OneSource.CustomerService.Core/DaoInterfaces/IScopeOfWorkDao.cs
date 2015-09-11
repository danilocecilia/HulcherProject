using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.DaoInterfaces
{
    public interface IScopeOfWorkDao : IBaseDao<CS_ScopeOfWork, long>
    {
        /// <summary>
        /// Lists all Scope of Work for a specific Job
        /// </summary>
        /// <param name="jobId">ID of the Job</param>
        IList<CS_ScopeOfWork> ListAllByJobId(int jobId);
    }
}
