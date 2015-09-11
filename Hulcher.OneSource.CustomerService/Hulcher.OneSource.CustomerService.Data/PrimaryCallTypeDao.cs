using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core.DaoInterfaces;

namespace Hulcher.OneSource.CustomerService.Data
{
    public class PrimaryCallTypeDao : BaseDao<CS_PrimaryCallType, long>, IPrimaryCallTypeDao
    {

    }
}
