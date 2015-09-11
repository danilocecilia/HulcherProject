using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.DaoInterfaces
{
    public interface IEquipmentInfoDao
    {
        IList<CS_View_EquipmentInfo> ListAll();

        CS_View_EquipmentInfo Get(int id);

        IList<CS_View_EquipmentInfo> ListFilteredByDivision(string[] divisionNames);

        IList<CS_View_EquipmentInfo> ListFilteredByDivisionState(string[] divisionStates);

        IList<CS_View_EquipmentInfo> ListFilteredByComboNumber(string[] comboNumbers);

        IList<CS_View_EquipmentInfo> ListFilteredByUnitNumber(string[] unitNumbers);

        IList<CS_View_EquipmentInfo> ListFilteredByStatus(string[] statusList);

        IList<CS_View_EquipmentInfo> ListFilteredByJobLocation(string[] jobLocations);

        IList<CS_View_EquipmentInfo> ListFilteredByCallType(string[] callTypes);
    }
}
