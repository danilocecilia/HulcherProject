using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulcher.OneSource.CustomerService.DataContext.VO
{
    public class MissedETAVO
    {
        public int ETACallLogId { get; set; }

        public int? EmployeeId { get; set; }

        public int? EquipmentId { get; set; }

        public int MissedETAMinutes { get; set; }

        public CS_Resource ResourceData { get; set; }
    }
}
