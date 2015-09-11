using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulcher.OneSource.CustomerService.DataContext.VO
{
    public class PhoneNumberVO
    {
        public int ID { get; set; }
        public string Number { get; set; }
        public string TypeName { get; set; }
        public int TypeID { get; set; }
    }
}
