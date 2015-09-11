using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.DaoInterfaces
{
    public interface ICustomerInfoDao : IBaseDao<CS_CustomerInfo, long>
    {
        /// <summary>
        /// Retrieves a CustomerInfo associated to a job
        /// </summary>
        /// <param name="jobId">ID of the Job</param>
        /// <returns>Entity containing Customer Information</returns>
        CS_CustomerInfo GetByJobId(long jobId);
    }
}
