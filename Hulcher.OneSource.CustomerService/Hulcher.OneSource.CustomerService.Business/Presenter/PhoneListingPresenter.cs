using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.ViewModel;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    public class PhoneListingPresenter
    {
        #region [ Attributes ]
        private IPhoneListingView _view;

        private PhoneListingViewModel _viewModel;
        #endregion

        #region [ Constructor ]
        public PhoneListingPresenter(IPhoneListingView view)
        {
            _view = view;
            _viewModel = new PhoneListingViewModel(_view);
        }
        #endregion

        #region [ Methods ]
        public void BindPhoneListingEmployee()
        {
            try
            {
                _viewModel.BindPhoneListingEmployeeRow();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Bind the Phone Listing Employee Grid!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Bind the Phone Listing Employee Grid", false);
            }
        }

        #region [ Customer ]

        public void BindPhoneListingCustomerRow()
        {
            try
            {
                _viewModel.BindPhoneListingCustomerRow();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Bind the Customer Phone Grid!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Bind the Company Phone Grid!", false);
            }
        }

        #endregion

        #region [ Division ]

        public void BindPhoneTypeListing()
        {
            try
            {
                _viewModel.BindDivisionPhoneTypeListing();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to get the Phone Type Listing!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to get the Phone Type Listing!", false);
            }
        }

        public void BindPhoneListingDivisionRow()
        {
            try
            {
                _viewModel.BindPhoneListingDivisionRow();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Bind the Division Phone Grid!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Bind the Division Phone Grid!", false);
            }
        }

        /// <summary>
        /// Loads the local division data
        /// </summary>
        public void LoadLocalDivision()
        {
            try
            {
                _viewModel.LoadLocalDivision();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to get the Division data!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to get the Division data!", false);
            }
        }

        /// <summary>
        /// Saves the local division data
        /// </summary>
        public void SaveLocalDivision()
        {
            try
            {
                _viewModel.SaveLocalDivision();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to save Division data!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying save Division data!", false);
            }
        }

        /// <summary>
        /// Delete the local division data
        /// </summary>
        public void DeleteLocalDivision()
        {
            try
            {
                _viewModel.DeleteLocalDivision();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to delete Division data!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying delete Division data!", false);
            }
        }

        public void ClearDivisionFields()
        {
            try
            {
                _view.ClearDivisionFields();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to clear the Division Form!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to clear the Division Form!", false);
            }
        }

        #endregion

        public void ListFilteredPhoneListing()
        {
            try
            {
                _viewModel.ListFilteredPhoneListing();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Bind the Phone Listing Employee Grid!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Bind the Phone Listing Employee Grid", false);
            }
        }

        public void ClearFields()
        {
            try
            {
                _view.ClearFields();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to clear the filter fields\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to clear the filter fields", false);
            }
            
        }
        #endregion
    }
}
