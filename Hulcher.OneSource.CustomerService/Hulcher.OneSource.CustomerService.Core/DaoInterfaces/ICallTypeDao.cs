using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.DaoInterfaces
{
    /// <summary>
    /// Interface for the CallType Dao
    /// </summary>
    public interface ICallTypeDao : IBaseDao<CS_CallType, int>
    {
        /// <summary>
        /// Filters the CallTypes by PrimaryCallType ID
        /// </summary>
        /// <param name="primaryCallTypeId">ID of the PrimaryCallType</param>
        /// <returns>List of CallTypes related to that ID</returns>
        List<CS_CallType> FilterByPrimaryCallType(int primaryCallTypeId);
    }
}
