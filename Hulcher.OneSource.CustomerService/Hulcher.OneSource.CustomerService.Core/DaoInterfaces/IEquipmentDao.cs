using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.DataContext;
using System.Data.Objects;

namespace Hulcher.OneSource.CustomerService.Core.DaoInterfaces
{
    /// <summary>
    /// Interface for the Equipment Dao Class
    /// </summary>
    public interface IEquipmentDao : IBaseDao<CS_Equipment, long>
    {
        void ClearAll();
    }
}
