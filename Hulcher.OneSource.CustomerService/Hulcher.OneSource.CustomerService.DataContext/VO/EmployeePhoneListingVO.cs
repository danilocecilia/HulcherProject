using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulcher.OneSource.CustomerService.DataContext.VO
{
    public class EmployeePhoneListingVO
    {
        IList<PhoneNumberVO> _phoneNumberList;

        public EmployeePhoneListingVO()
        {
            _phoneNumberList = new List<PhoneNumberVO>();
        }

        public EmployeePhoneListingVO(CS_Employee employee)
        {
            IList<CS_PhoneNumber> phoneNumbers = employee.CS_PhoneNumber.ToList();
            _phoneNumberList = new List<PhoneNumberVO>();
            Employee = employee;

            if (!string.IsNullOrEmpty(employee.DayAreaCode) && !string.IsNullOrEmpty(employee.DayPhone))
            {
                _phoneNumberList.Add(new PhoneNumberVO() { Number = string.Format("{0}-{1}", employee.DayAreaCode, employee.DayPhone), TypeID = 8, TypeName = "Day" });
            }

            if (!string.IsNullOrEmpty(employee.FaxAreaCode) && !string.IsNullOrEmpty(employee.FaxPhone))
            {
                _phoneNumberList.Add(new PhoneNumberVO() { Number = string.Format("{0}-{1}", employee.FaxAreaCode, employee.FaxPhone), TypeID = 3, TypeName = "Fax" });
            }

            if (!string.IsNullOrEmpty(employee.HomeAreaCode) && !string.IsNullOrEmpty(employee.HomePhone))
            {
                _phoneNumberList.Add(new PhoneNumberVO() { Number = string.Format("{0}-{1}", employee.HomeAreaCode, employee.HomePhone), TypeID = 1, TypeName = "Home" });
            }

            if (!string.IsNullOrEmpty(employee.MobileAreaCode) && !string.IsNullOrEmpty(employee.MobilePhone))
            {
                _phoneNumberList.Add(new PhoneNumberVO() { Number = string.Format("{0}-{1}", employee.MobileAreaCode, employee.MobilePhone), TypeID = 2, TypeName = "Cell" });
            }

            if (!string.IsNullOrEmpty(employee.OtherPhoneAreaCode) && !string.IsNullOrEmpty(employee.OtherPhone))
            {
                _phoneNumberList.Add(new PhoneNumberVO() { Number = string.Format("{0}-{1}", employee.OtherPhoneAreaCode, employee.OtherPhone), TypeID = 9, TypeName = "Other" });
            }


            for (int i = 0; i < phoneNumbers.Count; i++)
            {
                CS_PhoneNumber phoneNumber = phoneNumbers[i];
                _phoneNumberList.Add(new PhoneNumberVO() { ID = phoneNumber.ID, Number = phoneNumber.Number, TypeID = phoneNumber.PhoneTypeID, TypeName = phoneNumber.CS_PhoneType.Name });
            }
        }

        public CS_Employee Employee { get; set; }

        public IList<PhoneNumberVO> PhoneNumberList { get { return _phoneNumberList; } set { _phoneNumberList = value; } }
    }
}
