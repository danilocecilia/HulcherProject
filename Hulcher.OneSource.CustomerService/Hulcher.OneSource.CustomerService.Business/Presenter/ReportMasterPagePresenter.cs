using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Model;

using Microsoft.Practices.EnterpriseLibrary.Logging;
using Hulcher.OneSource.CustomerService.Core.Utils;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    public class ReportMasterPagePresenter
    {
        #region [ Attributes ]

        private IReportMasterPageView _view;

        private SettingsModel _settingsModel;

        #endregion

        #region [ Constructors ]

        public ReportMasterPagePresenter(IReportMasterPageView view)
        {
            _view = view;

            _settingsModel = new SettingsModel();
        }

        #endregion

        #region [ Methods ]

        public void SendEmail()
        {
            try
            {
                string pdfPath = _view.GetPDFReportFile();
                string[] emailConfiguration = _settingsModel.GetEmailConfiguration();
                string domain = _settingsModel.GetDomain();

                string fromEmail = _view.Username + "@" + domain;

                using (MailUtility mailUtil = new MailUtility(emailConfiguration[0], emailConfiguration[1], emailConfiguration[2]))
                {
                    mailUtil.SendUntrackMail(fromEmail,
                        _view.Receipts.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries),
                        null,
                        _view.Body,
                        _view.Subject,
                        new string[] { pdfPath });
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to send the Email!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to send the Email.", false);
            }
        }

        #endregion
    }
}
