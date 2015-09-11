using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.Business.ViewModel;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Core.Security;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;

using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    public class CustomerRequestPresenter
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of Customer Request View
        /// </summary>
        private ICustomerRequestView _view;

        /// <summary>
        /// Instance of Customer Model
        /// </summary>
        private CustomerModel _customerModel;

        #endregion

        #region [ Constructor ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="view">Instance of Customer Request View</param>
        public CustomerRequestPresenter(ICustomerRequestView view)
        {
            _view = view;

            _customerModel = new CustomerModel();
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Loads the page
        /// </summary>
        public void LoadPage()
        {
            try
            {
                BindRequest();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to load the page!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to load the page. Please try again.", false);
            }
        }

        /// <summary>
        /// Binds the Request grid
        /// </summary>
        public void BindRequest()
        {
            try
            {
                _view.RequestList = SortList(_customerModel.ListAllFilteredRequests(_view.FilterType, _view.FilterValue));
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to Load the Request information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to Load the Request information. Please try again.", false);
            }
        }

        /// <summary>
        /// Binds the Request Row
        /// </summary>
        public void BindRequestRow()
        {
            try
            {
                if (null != _view.RequestItem)
                {
                    _view.RequestItemRequestID = _view.RequestItem.ID;
                    _view.RequestItemRequestDate = _view.RequestItem.RequestDate;
                    _view.RequestItemRequestedBy = _view.RequestItem.CreatedBy;
                    if (_view.RequestItem.IsCustomer)
                    {
                        _view.RequestItemRequestType = "Customer";
                        _view.RequestItemCustomerContactName = _view.RequestItem.CS_Customer.Name;
                    }
                    else
                    {
                        _view.RequestItemRequestType = "Contact";
                        _view.RequestItemCustomerContactName = _view.RequestItem.CS_Contact.FullName;
                    }
                    _view.RequestItemRequestStatus = (Globals.CustomerMaintenance.RequestStatus)_view.RequestItem.Status;
                    _view.RequestItemRequestNotes = _view.RequestItem.Note;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to Load the Request row information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to Load the Request row information. Please try again.", false);
            }
        }

        /// <summary>
        /// Deletes an existing request
        /// </summary>
        public void DeleteRequest()
        {
            try
            {
                bool canDelete = _customerModel.DeleteRequest(_view.RequestID, _view.Username);
                if (canDelete)
                    _view.DisplayMessage("Request removed successfully!", false);
                else
                    _view.DisplayMessage("Company working on an active job, unable to complete delete request", false);
                BindRequest();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to Delete the Request!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to Delete the Request. Please try again.", false);
            }
        }

        /// <summary>
        /// Resends the data of an existing request
        /// </summary>
        public void ResendRequest()
        {
            try
            {
                _customerModel.ResendRequest(_view.RequestID);
                _view.DisplayMessage("Request resend successfully!", false);
                BindRequest();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to Resend the Request!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to Resend the Request. Please try again.", false);
            }
        }

        /// <summary>
        /// Sorts the list based on parameters
        /// </summary>
        /// <param name="requestList">Request List</param>
        /// <returns>Sorted Request List</returns>
        private IList<CS_Request> SortList(IList<CS_Request> requestList)
        {
            switch (_view.SortColumn)
            {
                case Globals.Common.Sort.CustomerRequestSortColumns.Date:
                    if (_view.SortDirection == Globals.Common.Sort.SortDirection.Ascending)
                        return requestList.OrderBy(e => e.RequestDate).ToList();
                    else
                        return requestList.OrderByDescending(e => e.RequestDate).ToList();
                case Globals.Common.Sort.CustomerRequestSortColumns.RequestedBy:
                    if (_view.SortDirection == Globals.Common.Sort.SortDirection.Ascending)
                        return requestList.OrderBy(e => e.CreatedBy).ToList();
                    else
                        return requestList.OrderByDescending(e => e.CreatedBy).ToList();
                case Globals.Common.Sort.CustomerRequestSortColumns.Type:
                    if (_view.SortDirection == Globals.Common.Sort.SortDirection.Ascending)
                        return requestList.OrderBy(e => e.IsCustomer).ToList();
                    else
                        return requestList.OrderByDescending(e => e.IsCustomer).ToList();
                case Globals.Common.Sort.CustomerRequestSortColumns.CustomerContactName:
                    if (_view.SortDirection == Globals.Common.Sort.SortDirection.Ascending)
                        return requestList
                            .OrderBy(e => (e.CS_Customer != null ? e.CS_Customer.Name : e.CS_Contact.LastName))
                            .ThenBy(e => (e.CS_Customer != null ? e.CS_Customer.Name : e.CS_Contact.Name))
                            .ToList();
                    else
                        return requestList
                            .OrderByDescending(e => (e.CS_Customer != null ? e.CS_Customer.Name : e.CS_Contact.LastName))
                            .ThenByDescending(e => (e.CS_Customer != null ? e.CS_Customer.Name : e.CS_Contact.Name))
                            .ToList();
                case Globals.Common.Sort.CustomerRequestSortColumns.Status:
                    if (_view.SortDirection == Globals.Common.Sort.SortDirection.Ascending)
                        return requestList.OrderBy(e => e.Status).ToList();
                    else
                        return requestList.OrderByDescending(e => e.Status).ToList();
                case Globals.Common.Sort.CustomerRequestSortColumns.None:
                default:
                    return requestList.OrderBy(e => e.RequestDate).ToList();
            }
        }

        #endregion
    }
}
