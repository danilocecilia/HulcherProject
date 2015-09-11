using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulcher.OneSource.CustomerService.DataContext.VO
{
    [Serializable]
    public class EmailVO
    {
        public int PersonID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public int Type { get; set; }

        public int Status { get; set; }

        public object[] ID { get; set; }
    }
}
