using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Core.DaoInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    public interface IEquipmentTypeView : IBaseView
    {
        CS_EquipmentType GetEquipmentType { set; }

        int EquipmentTypeValue { get; set; }

        IList<CS_EquipmentType> ListAllEquipmentTypes { set; }
    }
}
