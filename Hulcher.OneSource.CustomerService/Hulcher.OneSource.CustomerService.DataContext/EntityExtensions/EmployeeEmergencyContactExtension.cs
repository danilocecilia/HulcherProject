using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;

namespace Hulcher.OneSource.CustomerService.DataContext
{
    public partial class CS_EmployeeEmergencyContact:EntityObject
    {
        public string FullName
        {
            get { return this.FirstName + " " + this.LastName; }
        }

        public string HomePhoneExtension
        {
            get { return this.HomeAreaCode + "-" + this.HomePhone; }
        }

        public string MobilePhoneExtension
        {
            get { return this.MobileAreaCode + "-" + this.MobilePhone; }
        }
    }
}
