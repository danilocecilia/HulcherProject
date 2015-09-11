using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.DaoInterfaces
{
    public interface IJobInfoDao : IBaseDao<CS_JobInfo, int>
    {
        /// <summary>
        /// Clears the table (for unit testing)
        /// </summary>
        void ClearAll();
    }
}
