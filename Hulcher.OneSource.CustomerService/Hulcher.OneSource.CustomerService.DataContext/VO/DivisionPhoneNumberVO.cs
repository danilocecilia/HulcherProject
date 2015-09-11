using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulcher.OneSource.CustomerService.DataContext.VO
{
    public class DivisionPhoneNumberVO
    {
        public int DivisionID;
        public string DivisionName;
        public string DivisionAddress;
        public string DivisionStateName;
        public string DivisionCityName;
        public string DivisionZipCode;
        public int PhoneID;
        public string PhoneType;
        public string PhoneNumber;

        public DivisionPhoneNumberVO()
        {
        }

        public DivisionPhoneNumberVO(CS_LocalDivision division, CS_DivisionPhoneNumber phone)
        {
            DivisionID = division.ID;
            DivisionName = division.Name;
            DivisionAddress = division.Address;

            if (division.StateID.HasValue)
                DivisionStateName = division.CS_State.CountryAcronymName;

            if (division.CityID.HasValue)
                DivisionCityName = division.CS_City.ExtendedName;

            if (division.ZipCodeID.HasValue)
                DivisionZipCode = division.CS_ZipCode.ZipCodeNameEdited;

            PhoneID = phone.ID;
            PhoneType = phone.CS_PhoneType.Name;
            if (null != phone.Number)
            {
                string number = phone.Number.Trim();
                PhoneNumber = string.Format("({0}) {1}", number.Substring(0, number.IndexOf("-")), number.Substring(number.IndexOf("-") + 1));
            }
        }

        public DivisionPhoneNumberVO(CS_LocalDivision division)
        {
            DivisionID = division.ID;
            DivisionName = division.Name;
            DivisionAddress = division.Address;
        }

        public string FullAddressInformation
        {
            get
            {
                string returnString = "";

                if (!string.IsNullOrEmpty(DivisionStateName))
                    returnString += DivisionStateName;

                if (!string.IsNullOrEmpty(DivisionCityName))
                {
                    if (!string.IsNullOrEmpty(returnString))
                        returnString += " - ";

                    returnString += DivisionCityName;
                }

                if (!string.IsNullOrEmpty(DivisionZipCode))
                {
                    if (!string.IsNullOrEmpty(returnString))
                        returnString += " - ";

                    returnString += DivisionZipCode;
                }

                if (!string.IsNullOrEmpty(DivisionAddress))
                {
                    if (!string.IsNullOrEmpty(returnString))
                        returnString += " - ";

                    returnString += DivisionAddress;
                }

                return returnString;
            }
        }
    }
}
