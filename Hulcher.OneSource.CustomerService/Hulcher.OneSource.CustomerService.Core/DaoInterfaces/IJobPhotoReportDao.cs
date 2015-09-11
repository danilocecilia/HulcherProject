using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.DaoInterfaces
{
    /// <summary>
    /// Interface for the Job Photo Report Class
    /// </summary>
    public interface IJobPhotoReportDao : IBaseDao<CS_JobPhotoReport, int>
    {
        List<CS_JobPhotoReport> GetJobPhotoReportByJob(int jobId);
    }
}
