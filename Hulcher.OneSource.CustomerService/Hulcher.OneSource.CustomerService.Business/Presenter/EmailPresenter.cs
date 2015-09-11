using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Hulcher.OneSource.CustomerService.Business.ViewModel;
using Hulcher.OneSource.CustomerService.Core;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    public class EmailPresenter
    {
        #region [ Attributes ]
        /// <summary>
        /// Instance of the Contract Detail Page
        /// </summary>
        private IEmailView _view;

        /// <summary>
        /// Instance of the Email View Model
        /// </summary>
        private EmailViewModel _emailViewModel;

        #endregion

        #region [ Constructor ]

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="view">Instance of the Email View Interface</param>
        public EmailPresenter(IEmailView view)
        {
            this._view = view;
            _emailViewModel = new EmailViewModel(view);

        }

        #endregion

        #region [ Methods ]

        public void LoadPage()
        {
            try
            {
                using (_emailViewModel = new EmailViewModel(_view))
                {
                    _emailViewModel.FormatCallLogHistory();

                    if (!_view.JobID.HasValue)
                        _emailViewModel.ListReceiptsByCallLog();
                    else
                        JobRecordFillingEmail();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to Load the Information.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to load the Information. Please try again.", false);
            }
        }

        public void JobRecordFillingEmail()
        {
            try
            {
            _emailViewModel.JobRecordEmailFilling();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to Load the Information.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to load the Information. Please try again.", false);
            }
        }

        public void SendMail()
        {
            try
            {
                using (_emailViewModel = new EmailViewModel(_view))
                {
                    _emailViewModel.SendMail();
                }

                _view.DisplayMessage("The Email was sent.", true);
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An Error has ocurred while trying to save the Email.\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An Internal Error has ocurred while trying to save the Email. Please try again.", false);
            }
        }

        #endregion
    }
}
