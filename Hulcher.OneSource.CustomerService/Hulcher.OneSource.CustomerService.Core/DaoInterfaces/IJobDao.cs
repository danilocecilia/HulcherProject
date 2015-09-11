using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.DaoInterfaces
{
    /// <summary>
    /// Interface for the Job Dao Class
    /// </summary>
    public interface IJobDao : IBaseDao<CS_Job, long>
    {
        IList<CS_Job> ListJobsByEquipment(int equipmentId);

        IList<CS_Job> ListAllJobsByResource(int equipmentTypeId, int divisionId);

        IList<CS_Job> ListAllByDivisionName(string divisionName);
        
        IList<CS_Job> ListAllByCustomerName(string customerName);
        
        IList<CS_Job> ListAllByLocationSiteName(string locationSiteName);

        IList<CS_Job> ListAllByJobNumber(string jobNumber);

        IList<CS_Job> ListAllForCallEntry();

        CS_Job GetJobInfoForCallEntry(int jobId);

        void ClearAll();
    }
}
