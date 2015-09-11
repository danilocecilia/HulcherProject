using System;
using System.Linq;
using System.Collections.Generic;

namespace Hulcher.OneSource.CustomerService.DataContext
{
    public partial class CS_ReserveRepository
    {
        /// <summary>
        /// List Reserve by Job
        /// </summary>
        /// <param name="jobId">jobid</param>
        /// <returns>list</returns>
        public IList<CS_Reserve> GetListReserveByJobId(int jobId)
        {
            return Repository.ListAll(e => e.JobID == jobId && e.Active);
        }
    }
}