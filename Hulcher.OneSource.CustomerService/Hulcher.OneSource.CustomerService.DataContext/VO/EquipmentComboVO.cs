using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulcher.OneSource.CustomerService.DataContext.VO
{
    [Serializable]
    public class EquipmentComboVO
    {
        public int ComboId { get; set; }
        public int EquipmentId { get; set; }
        public string DivisionNumber { get; set; }
        public string UnitNumber { get; set; }
        public string Descriptor { get; set; }
        public bool IsPrimary { get; set; }
        public bool Seasonal { get; set; }
    }
}
