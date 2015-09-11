using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.DaoInterfaces
{
    public interface ISettingsDao : IBaseDao<CS_Settings, int>
    {
    }
}
