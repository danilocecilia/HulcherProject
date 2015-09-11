using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.DataContext.VO;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    public interface IEmployeeMaintenanceView :IBaseView
    {
        #region [ Common ]

        string Username { get; }

        string Domain { get; }

        bool ReadOnly { set; }

        #endregion

        #region [ Employee Info ]
        bool IsKeyPerson { get; set; }
        
        string DayAreaCode { get; set; }

        string DayPhone { get; set; }

        string FaxAreaCode { get; set; }

        string FaxPhone { get; set; }

        int? EmployeeId { get; set; }

        string EmployeeName { get; set; }

        DateTime? HireDate { get; set; }

        string PersonID { get; set; }

        string Address { get; set; }

        string Address2 { get; set; }

        string City { get; set; }

        string State { get; set; }

        string Country { get; set; }

        string PostalCode { get; set; }

        string Position { get; set; }

        string EmployeeDivision { get; set; }

        IList<CS_EmployeeEmergencyContact> EmployeeContacts { get; set; }

        string PassportNumber { get; set; }

        string HomeAreaCode { get; set; }

        string HomePhone { get; set; }

        string MobileAreaCode { get; set; }

        string MobilePhone { get; set; }

        string OtherPhoneAreaCode { get; set; }

        string OtherPhone { get; set; }

        bool IsDentonPersonal { get; set; }

        List<CS_PhoneType> AdditionalContactPhoneTypeSource { set; }

        List<PhoneNumberVO> AdditionalContactPhoneList { get; }

        List<CS_PhoneNumber> AdditionalContactPhoneGridDataSource { set; }

        int? CallCriteriaEmployeeID { get; set; }

        #endregion

        #region [ Off Call ]

        bool IsOffCall { get; set; }

        int? ProxyEmployeeId { get; set; }

        DateTime? OffCallStartDate { get; set; }

        DateTime? OffCallEndDate { get; set; }

        TimeSpan? OffCallReturnTime { get; set; }

        IList<CS_EmployeeOffCallHistory> OffCallHistoryList { get; set; }

        #endregion

        #region [ Coverage ]

        bool RequireCoverageFields { get; set; }

        bool DisplayCoverageStartFields { get; set; }

        bool DisplayCoverageEndFields { get; set; }

        bool IsEmployeeAssignedToJob { get; set; }

        bool IsCoverage { get; set; }

        string ActualEmployeeDivision { get; set; }

        DateTime? CoverageStartDate { get; set; }

        TimeSpan? CoverageStartTime { get; set; }

        DateTime? CoverageEndDate { get; set; }

        TimeSpan? CoverageEndTime { get; set; }

        int? CoverageDuration { get; set; }

        int? CoverageDivisionID { get; set; }

        IList<CS_EmployeeCoverage> CoverageHistoryList { get; set; }

        #endregion

        #region [ Driving Info ]

        string DriversLicenseNumber { get; set; }

        string DriversLicenseClass { get; set; }

        string DriversLicenseState { get; set; }

        DateTime? DriversLicenseExpireDate { get; set; }

        #endregion

        
    }
}
