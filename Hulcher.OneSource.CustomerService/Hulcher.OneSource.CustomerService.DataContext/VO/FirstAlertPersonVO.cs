using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulcher.OneSource.CustomerService.DataContext.VO
{
    [Serializable]
    public class FirstAlertPersonVO
    {
        public int ID { get; set; }
        public int FirstAlertID { get; set; }
        public bool IsHulcherEmployee { get; set; }
        
        public int? FirstAlertVehicleID { get; set; }
        public int? FirstAlertVehicleIndex { get; set; }
        public short? VehiclePosition { get; set; }

        public int? EmployeeID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public int? CountryID { get; set; }
        public string CountryName { get; set; }
        public int? StateID { get; set; }
        public string StateName { get; set; }
        public int? CityID { get; set; }
        public string CityName { get; set; }
        public int? ZipCodeID { get; set; }
        public string ZipCodeName { get; set; }
        public string Address { get; set; }

        public string InjuryNature { get; set; }
        public string InjuryBodyPart { get; set; }
        public short? MedicalSeverity { get; set; }
        public string Details { get; set; }

        public string DoctorsName { get; set; }
        public int? DoctorsCountryID { get; set; }
        public string DoctorsCountryName { get; set; }
        public int? DoctorsStateID { get; set; }
        public string DoctorsStateName { get; set; }
        public int? DoctorsCityID { get; set; }
        public string DoctorsCityName { get; set; }
        public int? DoctorsZipCodeID { get; set; }
        public string DoctorsZipCodeName { get; set; }
        public string DoctorsPhoneNumber { get; set; }

        public string HospitalName { get; set; }
        public int? HospitalCountryID { get; set; }
        public string HospitalCountryName { get; set; }
        public int? HospitalStateID { get; set; }
        public string HospitalStateName { get; set; }
        public int? HospitalCityID { get; set; }
        public string HospitalCityName { get; set; }
        public int? HospitalZipCodeID { get; set; }
        public string HospitalZipCodeName { get; set; }
        public string HospitalPhoneNumber { get; set; }

        public string DriversLicenseNumber { get; set; }
        public int? DriversLicenseCountryID { get; set; }
        public string DriversLicenseCountryName { get; set; }
        public int? DriversLicenseStateID { get; set; }
        public string DriversLicenseStateName { get; set; }
        public int? DriversLicenseCityID { get; set; }
        public string DriversLicenseCityName { get; set; }
        public int? DriversLicenseZipCodeID { get; set; }
        public string DriversLicenseZipCodeName { get; set; }
        public string DriversLicenseAddress { get; set; }

        public string InsuranceCompany { get; set; }
        public string PolicyNumber { get; set; }

        public bool? DrugScreenRequired { get; set; }

        public int? CreationID { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public int? ModificationID { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModificationDate { get; set; }
        public bool Active { get; set; }
    }
}
