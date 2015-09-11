using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulcher.OneSource.CustomerService.Integration.Entities
{
    public class DynamicsContract
    {
        public string contract { get; set; }
        public string contract_desc { get; set; }
        public string customer { get; set; }
        public DateTime Date_Start_Org { get; set; }
        public DateTime date_end_org { get; set; }
        public string status1 { get; set; }
        public string text_contract1 { get; set; }
        public string text_contract2 { get; set; }
        public DateTime Crtd_DateTime { get; set; }
        public string Crtd_User { get; set; }
        public DateTime LUpd_DateTime { get; set; }
        public string LUpd_User { get; set; }
    }
}
