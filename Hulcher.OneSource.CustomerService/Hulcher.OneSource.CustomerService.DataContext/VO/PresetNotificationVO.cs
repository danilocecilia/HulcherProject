using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulcher.OneSource.CustomerService.DataContext.VO
{
    [Serializable]
    public class PresetNotificationVO
    {
        public int JobId { get; set; }

        public string JobNumber { get; set; }

        public DateTime PresetDate { get; set; }

        public TimeSpan? PresetTime { get; set; }
    }
}
