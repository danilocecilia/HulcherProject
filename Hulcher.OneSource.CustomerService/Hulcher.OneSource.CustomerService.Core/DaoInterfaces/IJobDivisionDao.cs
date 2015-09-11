using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.DaoInterfaces
{
    public interface IJobDivisionDao : IBaseDao<CS_JobDivision, long>
    {
        IList<CS_JobDivision> ListAllByJobId(int jobId);
    }
}
