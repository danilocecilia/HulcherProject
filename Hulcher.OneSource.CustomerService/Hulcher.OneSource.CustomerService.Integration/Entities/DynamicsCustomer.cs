using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulcher.OneSource.CustomerService.Integration.Entities
{
    public class DynamicsCustomer
    {
        public string CustId { get; set; }

        public string Addr1 { get; set; } 
        
        public string Addr2 { get; set; }

        public string BillAddr1 { get; set; }
        
        public string BillAddr2 { get; set; }
        
        public string BillAttn { get; set; }
        
        public string BillCity { get; set; }
        
        public string BillCountry { get; set; }
        
        public string BillFax { get; set; }
        
        public string BillName { get; set; }
        
        public string BillPhone { get; set; }
        
        public string BillSalut { get; set; }
        
        public string BillState { get; set; }
        
        public short? BillThruProject { get; set; }
        
        public string BillZip { get; set; }
        
        public string City { get; set; }
        
        public string Country { get; set; }
        
        public string EMailAddr { get; set; }
        
        public string Fax { get; set; }
        
        public string Name { get; set; }
        
        public string Phone { get; set; }
        
        public string State { get; set; }
        
        public string Zip { get; set; }

        public string Status { get; set; }

        public DateTime Crtd_DateTime { get; set; }
            
        public string Crtd_User { get; set; }

        public DateTime LUpd_DateTime { get; set; }

        public string LUpd_User { get; set; }
    }
}
