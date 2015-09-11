using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulcher.OneSource.CustomerService.DataContext.VO
{
    public class DPICallLogVO : IComparable<DPICallLogVO>
    {
        public int ID { get; set; }
        public DateTime ActionTime { get; set; }

        public int CompareTo(DPICallLogVO other)
        {
            return this.ActionTime.CompareTo(other.ActionTime);
        }
    }
}
