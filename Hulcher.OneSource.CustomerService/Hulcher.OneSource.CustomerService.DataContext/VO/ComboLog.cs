using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulcher.OneSource.CustomerService.DataContext.VO
{
    [Serializable]
    public class ComboLog
    {
        public string ComboName { get; set; }
        public string Units { get;  set; }
        public string PrimaryUnit { get; set; }
        public string Division { get; set; }
    }
}
