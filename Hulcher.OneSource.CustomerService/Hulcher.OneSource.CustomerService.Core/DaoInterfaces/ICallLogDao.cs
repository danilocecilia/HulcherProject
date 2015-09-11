using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;

namespace Hulcher.OneSource.CustomerService.Core.DaoInterfaces
{
    /// <summary>
    /// Interface for the Call Log DAO Class
    /// </summary>
    public interface ICallLogDao : IBaseDao<CS_CallLog, int>
    {
        IList<CS_CallLog> ListJobCallLogs(long jobId);

        IList<CS_CallLog> ListFilteredJobCallLogs(Globals.JobRecord.FilterType filterType, string value, long jobId);
    }
}
