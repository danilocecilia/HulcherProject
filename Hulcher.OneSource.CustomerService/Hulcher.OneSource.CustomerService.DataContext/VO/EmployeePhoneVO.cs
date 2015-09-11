using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulcher.OneSource.CustomerService.DataContext.VO
{
    public class EmployeePhoneVO
    {
        public string EmployeeName { get; set; }

        public string PhoneType { get; set; }

        public string PhoneNumber { get; set; }

        public EmployeePhoneVO()
        {
        }

        public EmployeePhoneVO(CS_PhoneNumber phone)
        {
            EmployeeName = phone.CS_Employee_Phone.FullName;
            PhoneType = phone.CS_PhoneType.Name;
            PhoneNumber = phone.Number;
        }
    }
}
