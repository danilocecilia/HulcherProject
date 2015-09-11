using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext.VO;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    public interface IPhoneListingView : IBaseView
    {
        Globals.Phone.PhoneFilterType PhoneListingFilter { get; }

        string FilterValue { get; }

        string PhoneListingEmployeeNameRow { set; }

        string PhoneListingPhoneTypeRow { set; }

        string PhoneListingPhoneNumber { set; }

        EmployeePhoneVO PhoneListingDataItem { get; set; }

        List<EmployeePhoneVO> ListFilteredEmployeePhoneListing { get; set; }

        bool ShowHideFilter { get; set; }

        bool ShowHidePanelButtons { get; set; }

        void ClearFields();

        #region [ Customer ]

        IList<CustomerPhoneVO> PhoneListingCustomerDataSource { get; set; }

        CustomerPhoneVO PhoneListingCustomerRowDataItem { get; set; }

        string PhoneListingCustomerCustomerName { get; set; }
        string PhoneListingCustomerCustomerNumber { get; set; }
        string PhoneListingCustomerContactName { get; set; }
        string PhoneListingCustomerPhoneType { get; set; }
        string PhoneListingCustomerPhoneNumber { get; set; }
        string PhoneListingCustomerCustomerNotes { get; set; }

        #endregion

        #region [ Division ]

        bool ShowHidePanelDivisionPhoneNumber { get; set; }

        bool ShowDeleteButton { get; set; }

        IList<CS_PhoneType> DivisionPhoneTypeListingDataSource { get; set; }

        void ClearDivisionFields();

        #region [ Grid ]

        IList<DivisionPhoneNumberVO> PhoneListingDivisionDataSource { get; set; }
        DivisionPhoneNumberVO PhoneListingDivisionRowDataItem { get; set; }

        int PhoneListingDivisionID { get; set; }
        string PhoneListingDivisionName { get; set; }
        string PhoneListingDivisionAddress { get; set; }
        string PhoneListingDivisionPhoneType { get; set; }
        string PhoneListingDivisionPhoneNumber { get; set; }

        #endregion

        int? LocalDivisionID { get; set; }

        string LocalDivisionNumber { get; set; }

        string LocalDivisionAddress { get; set; }

        int? LocalDivisionStateID { get; set; }

        string LocalDivisionStateName { get; set; }

        int? LocalDivisionCityID { get; set; }

        string LocalDivisionCityName { get; set; }

        int? LocalDivisionZipCodeID { get; set; }

        string LocalDivisionZipCode { get; set; }

        IList<CS_DivisionPhoneNumber> LocalDivisionPhoneListing { get; set; }

        #endregion

        #region [ Methods ]

        #region [ Common ]

        string Username { get; }

        void AlterVisibilityCustomerGrid(bool value);

        void AlterVisibilityEmployeeGrid(bool value);

        void AlterVisibilityDivisionGrid(bool value);

        #endregion

        #endregion
    }
}
