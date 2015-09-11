using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects.DataClasses;
using Hulcher.OneSource.CustomerService.DataContext.VO;

namespace Hulcher.OneSource.CustomerService.DataContext
{
    public partial class CS_FirstAlertPerson : EntityObject
    {
        public FirstAlertPersonVO FirstAlertPersonVO
        {
            get
            {
                FirstAlertPersonVO VO = new FirstAlertPersonVO();

                VO.ID = this.ID;
                VO.FirstAlertID = this.FirstAlertID;
                VO.IsHulcherEmployee = this.IsHulcherEmployee;
                VO.FirstAlertVehicleID = this.FirstAlertVehicleID;
                VO.VehiclePosition = this.VehiclePosition;
                VO.EmployeeID = this.EmployeeID;
                VO.LastName = this.LastName;
                VO.FirstName = this.FirstName;
                VO.CountryID = this.CountryID;

                VO.StateID = this.StateID;
                if (this.StateID.HasValue)
                    VO.StateName = this.CS_State.AcronymName;

                VO.CityID = this.CityID;
                if (this.CityID.HasValue)
                    VO.CityName = this.CS_City.CityStateInformation;

                VO.ZipCodeID = this.ZipcodeID;
                if (this.ZipcodeID.HasValue)
                    VO.ZipCodeName = this.CS_ZipCode.ZipCodeNameEdited;

                VO.Address = this.Address;
                VO.InjuryNature = this.InjuryNature;
                VO.InjuryBodyPart = this.InjuryBodyPart;
                VO.MedicalSeverity = this.MedicalSeverity;
                VO.Details = this.Details;
                VO.DoctorsName = this.DoctorsName;
                VO.DoctorsCountryID = this.DoctorsCountryID;

                VO.DoctorsStateID = this.DoctorsStateID;
                if (this.DoctorsStateID.HasValue)
                    VO.DoctorsStateName = this.CS_State_Doctor.AcronymName;

                VO.DoctorsCityID = this.DoctorsCityID;
                if (this.DoctorsCityID.HasValue)
                    VO.DoctorsCityName = this.CS_City_Doctor.CityStateInformation;

                VO.DoctorsZipCodeID = this.DoctorsZipcodeID;
                if (this.DoctorsZipcodeID.HasValue)
                    VO.DoctorsZipCodeName = this.CS_ZipCode_Doctor.Name;

                VO.DoctorsPhoneNumber = this.DoctorsPhoneNumber;
                VO.HospitalName = this.HospitalName;
                VO.HospitalCountryID = this.HospitalCountryID;

                VO.HospitalStateID = this.HospitalStateID;
                if (this.HospitalStateID.HasValue)
                    VO.HospitalStateName = this.CS_State_Hospital.AcronymName;

                VO.HospitalCityID = this.HospitalCityID;
                if (this.HospitalCityID.HasValue)
                    VO.HospitalCityName = this.CS_City_Hospital.CityStateInformation;

                VO.HospitalZipCodeID = this.HospitalZipcodeID;
                if (this.HospitalZipcodeID.HasValue)
                    VO.HospitalZipCodeName = this.CS_ZipCode_Hospital.Name;

                VO.HospitalPhoneNumber = this.HospitalPhoneNumber;
                VO.DriversLicenseNumber = this.DriversLicenseNumber;
                VO.DriversLicenseCountryID = this.DriversLicenseCountryID;

                VO.DriversLicenseStateID = this.DriversLicenseStateID;
                if (this.DriversLicenseStateID.HasValue)
                    VO.DriversLicenseStateName = this.CS_State_DriversLicense.AcronymName;

                VO.DriversLicenseCityID = this.DriversLicenseCityID;
                if (this.DriversLicenseCityID.HasValue)
                    VO.DriversLicenseCityName = this.CS_City_DriversLicense.CityStateInformation;

                VO.DriversLicenseZipCodeID = this.DriversLicenseZipcodeID;
                if (this.DriversLicenseZipcodeID.HasValue)
                    VO.DriversLicenseZipCodeName = this.CS_ZipCode_DriversLicense.Name;

                VO.DriversLicenseAddress = this.DriversLicenseAddress;
                VO.InsuranceCompany = this.InsuranceCompany;
                VO.PolicyNumber = this.PolicyNumber;
                VO.DrugScreenRequired = this.DrugScreenRequired;
                VO.CreationID = this.CreationID;
                VO.CreatedBy = this.CreatedBy;
                VO.CreationDate = this.CreationDate;
                VO.ModificationID = this.ModificationID;
                VO.ModifiedBy = this.ModifiedBy;
                VO.ModificationDate = this.ModificationDate;
                VO.Active = this.Active;

                return VO;
            }
            set
            {
                this.ID = value.ID;
                this.FirstAlertID = value.FirstAlertID;
                this.IsHulcherEmployee = value.IsHulcherEmployee;
                this.FirstAlertVehicleID = value.FirstAlertVehicleID;
                this.VehicleIndex = value.FirstAlertVehicleIndex;
                this.VehiclePosition = value.VehiclePosition;
                this.EmployeeID = value.EmployeeID;
                this.LastName = value.LastName;
                this.FirstName = value.FirstName;
                this.CountryID = value.CountryID;
                this.StateID = value.StateID;
                this.CityID = value.CityID;
                this.ZipcodeID = value.ZipCodeID;
                this.Address = value.Address;
                this.InjuryNature = value.InjuryNature;
                this.InjuryBodyPart = value.InjuryBodyPart;
                this.MedicalSeverity = value.MedicalSeverity;
                this.Details = value.Details;
                this.DoctorsName = value.DoctorsName;
                this.DoctorsCountryID = value.DoctorsCountryID;
                this.DoctorsStateID = value.DoctorsStateID;
                this.DoctorsCityID = value.DoctorsCityID;
                this.DoctorsZipcodeID = value.DoctorsZipCodeID;
                this.DoctorsPhoneNumber = value.DoctorsPhoneNumber;
                this.HospitalName = value.HospitalName;
                this.HospitalCountryID = value.HospitalCountryID;
                this.HospitalStateID = value.HospitalStateID;
                this.HospitalCityID = value.HospitalCityID;
                this.HospitalZipcodeID = value.HospitalZipCodeID;
                this.HospitalPhoneNumber = value.HospitalPhoneNumber;
                this.DriversLicenseNumber = value.DriversLicenseNumber;
                this.DriversLicenseCountryID = value.DriversLicenseCountryID;
                this.DriversLicenseStateID = value.DriversLicenseStateID;
                this.DriversLicenseCityID = value.DriversLicenseCityID;
                this.DriversLicenseZipcodeID = value.DriversLicenseZipCodeID;
                this.DriversLicenseAddress = value.DriversLicenseAddress;
                this.InsuranceCompany = value.InsuranceCompany;
                this.PolicyNumber = value.PolicyNumber;
                this.DrugScreenRequired = value.DrugScreenRequired;
                this.Active = true;
            }
        }

        public int? VehicleIndex
        {
            get;
            set;
        }

        public string DoctorAddress
        {
            get
            {
                string address = string.Empty;

                if (null != CS_ZipCode_Doctor)
                    address += string.Format("Zip Code: {0}", CS_ZipCode_Doctor.Name);

                if (null != CS_City_Doctor)
                    address += ((!string.IsNullOrEmpty(address)) ? " - " : "") + CS_City_Doctor.ExtendedName;

                if (null != CS_State_Doctor)
                    address += ((!string.IsNullOrEmpty(address)) ? " - " : "") + CS_State_Doctor.Acronym;

                if (null != CS_Country_Doctor)
                    address += ((!string.IsNullOrEmpty(address)) ? " - " : "") + CS_Country_Doctor.Name;

                return address;
            }
        }

        public string HospitalAddress
        {
            get
            {
                string address = string.Empty;

                if (null != CS_ZipCode_Hospital)
                    address += string.Format("Zip Code: {0}", CS_ZipCode_Hospital.Name);

                if (null != CS_City_Hospital)
                    address += ((!string.IsNullOrEmpty(address)) ? " - " : "") + CS_City_Hospital.ExtendedName;

                if (null != CS_State_Hospital)
                    address += ((!string.IsNullOrEmpty(address)) ? " - " : "") + CS_State_Hospital.Acronym;

                if (null != CS_Country_Hospital)
                    address += ((!string.IsNullOrEmpty(address)) ? " - " : "") + CS_Country_Hospital.Name;

                return address;
            }
        }

        public string MedicalSeverityDescription
        {
            get;
            set;
        }
    }
}
