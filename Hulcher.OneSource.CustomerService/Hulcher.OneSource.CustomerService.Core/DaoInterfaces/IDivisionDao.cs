using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.DataContext;
using System.Data;

namespace Hulcher.OneSource.CustomerService.Core.DaoInterfaces
{
    /// <summary>
    /// Interface for the Division Dao Class
    /// </summary>
    public interface IDivisionDao : IBaseDao<CS_Division, long>
    {
        void UpdateFromIntegration();

        void BulkCopyAllDivisions(IDataReader allDivisions);

        void ClearAll();
    }
}
