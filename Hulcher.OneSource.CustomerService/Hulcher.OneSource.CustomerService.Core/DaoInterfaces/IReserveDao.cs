using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.DaoInterfaces
{
    public interface IReserveDao : IBaseDao<CS_Reserve, int>
    {
        /// <summary>
        /// Clear all records (for unit testing)
        /// </summary>
        void ClearAll();

        /// <summary>
        /// List Reserve by Job
        /// </summary>
        /// <param name="jobId">jobid</param>
        /// <returns>list</returns>
        IList<CS_Reserve> GetListReserveByJobId(int jobId);
    }
}
