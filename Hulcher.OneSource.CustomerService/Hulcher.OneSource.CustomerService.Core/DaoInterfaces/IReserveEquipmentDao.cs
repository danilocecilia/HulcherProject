using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.DaoInterfaces
{
    /// <summary>
    /// Interface Dao for the Reserve Equipment View
    /// </summary>
    public interface IReserveEquipmentDao
    {
        /// <summary>
        /// List all Items for the Reserve Equipment View
        /// </summary>
        /// <returns>List of Equipments</returns>
        IList<CS_View_ReserveEquipment> ListAll();

        /// <summary>
        /// List filterd items for the Reserve Equipment View
        /// </summary>
        /// <param name="equipmentTypeId">Equipment Type filter</param>
        /// <param name="stateId">State filter</param>
        /// <param name="divisionId">Division filter</param>
        /// <returns>List of Equipments</returns>
        IList<CS_View_ReserveEquipment> ListAllFiltered(int? equipmentTypeId, int? stateId, int? divisionId);
    }
}
