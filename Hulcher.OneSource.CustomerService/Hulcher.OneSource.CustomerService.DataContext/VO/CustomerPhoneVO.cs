using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulcher.OneSource.CustomerService.DataContext.VO
{
    [Serializable]
    public class CustomerPhoneVO
    {
        public string CustomerName { get; set; }

        public string CustomerNumber { get; set; }

        public string PhoneType { get; set; }

        public string PhoneNumber { get; set; }

        public string CustomerNotes { get; set; }

        public string ContactName { get; set; }
    }
}
