using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.DaoInterfaces
{
    /// <summary>
    /// Interface for the Job Permit Class
    /// </summary>
    public interface IJobPermitDao : IBaseDao<CS_JobPermit, int>
    {
        List<CS_JobPermit> GetPermitInfoByJob(int jobId);
    }
}
