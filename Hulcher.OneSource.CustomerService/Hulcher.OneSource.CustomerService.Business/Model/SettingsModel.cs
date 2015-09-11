using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Business.Model
{
    public class SettingsModel : IDisposable
    {
        #region [ Attributes ]

        /// <summary>
        /// Unit of Work used to access the database/in-memory context
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// Repository class for CS_Settings
        /// </summary>
        private IRepository<CS_Settings> _settingsRepository;

        #endregion

        #region [ Constructors ]

        public SettingsModel()
        {
            _unitOfWork = new EFUnitOfWork();

            _settingsRepository = new EFRepository<CS_Settings>();
            _settingsRepository.UnitOfWork = _unitOfWork;
        }

        public SettingsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            _settingsRepository = new EFRepository<CS_Settings>();
            _settingsRepository.UnitOfWork = _unitOfWork;
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Gets the last date that the update customer process ran
        /// </summary>
        /// <returns></returns>
        public DateTime? GetLastUpdateCustomer()
        {
            DateTime? returnValue = null;

            //CS_Settings settings = SettingsDao.Singleton.Get((int)Globals.Configuration.Settings.LastUpdateCustomer);
            CS_Settings settings = _settingsRepository.Get(e => e.ID == (int)Globals.Configuration.Settings.LastUpdateCustomer);
            if (null != settings)
                if (!string.IsNullOrEmpty(settings.Value))
                    returnValue = Convert.ToDateTime(settings.Value, new System.Globalization.CultureInfo("en-US"));
            
            return returnValue;
        }

        /// <summary>
        /// Gets the last date that the update contact process ran
        /// </summary>
        /// <returns></returns>
        public DateTime? GetLastUpdateContact()
        {
            DateTime? returnValue = null;

            //CS_Settings settings = SettingsDao.Singleton.Get((int)Globals.Configuration.Settings.LastUpdateContact);
            CS_Settings settings = _settingsRepository.Get(e => e.ID == (int)Globals.Configuration.Settings.LastUpdateContact);
            if (null != settings)
                if (!string.IsNullOrEmpty(settings.Value))
                    returnValue = Convert.ToDateTime(settings.Value, new System.Globalization.CultureInfo("en-US"));

            return returnValue;
        }

        /// <summary>
        /// Gets the last date that the update contract process ran
        /// </summary>
        /// <returns></returns>
        public DateTime? GetLastUpdateContract()
        {
            DateTime? returnValue = null;

            //CS_Settings settings = SettingsDao.Singleton.Get((int)Globals.Configuration.Settings.LastUpdateContract);
            CS_Settings settings = _settingsRepository.Get(e => e.ID == (int)Globals.Configuration.Settings.LastUpdateContract);
            if (null != settings)
                if (!string.IsNullOrEmpty(settings.Value))
                    returnValue = Convert.ToDateTime(settings.Value, new System.Globalization.CultureInfo("en-US"));

            return returnValue;
        }

        /// <summary>
        /// Gets the Email of the IT members to send the E-Mail
        /// </summary>
        /// <returns></returns>
        public string GetITEmailOnError()
        {
            string returnValue = "";

            //CS_Settings settings = SettingsDao.Singleton.Get((int)Globals.Configuration.Settings.ITEmailOnError);
            CS_Settings settings = _settingsRepository.Get(e => e.ID == (int)Globals.Configuration.Settings.ITEmailOnError);
            if (null != settings)
                returnValue = settings.Value.ToString();

            return returnValue;
        }

        /// <summary>
        /// Gets the latest generated Job number
        /// </summary>
        /// <returns></returns>
        public int GetLastJobNumber()
        {
            int returnValue = 0;

            //CS_Settings settings = SettingsDao.Singleton.Get((int)Globals.Configuration.Settings.LastJobNumber);
            CS_Settings settings = _settingsRepository.Get(e => e.ID == (int)Globals.Configuration.Settings.LastJobNumber);
            if (null != settings)
                returnValue = Convert.ToInt32(settings.Value);

            return returnValue;
        }

        /// <summary>
        /// Gets the latest generated First Alert Number
        /// </summary>
        /// <returns></returns>
        public int GetLastFirstAlertNumber()
        {
            int returnValue = 0;

            //CS_Settings settings = SettingsDao.Singleton.Get((int)Globals.Configuration.Settings.LastJobNumber);
            CS_Settings settings = _settingsRepository.Get(e => e.ID == (int)Globals.Configuration.Settings.LastFirstAlertNumber);
            if (null != settings)
                returnValue = Convert.ToInt32(settings.Value);

            return returnValue;
        }

        /// <summary>
        /// Gets the latest generated Non Job number
        /// </summary>
        /// <returns></returns>
        public int GetLastNonJobNumber()
        {
            int returnValue = 0;

            //CS_Settings settings = SettingsDao.Singleton.Get((int)Globals.Configuration.Settings.LastNonJobNumber);

            CS_Settings settings = _settingsRepository.Get(e => e.ID == (int)Globals.Configuration.Settings.LastNonJobNumber);
            if (null != settings)
                returnValue = Convert.ToInt32(settings.Value);

            return returnValue;
        }

        /// <summary>
        /// Gets the Dashboard Refresh Rate
        /// </summary>
        public int GetDashboardRefreshRate()
        {
            int returnValue = 0;

            CS_Settings settings = _settingsRepository.Get(e => e.ID == (int)Globals.Configuration.Settings.DashboardRefreshRate);
            if (null != settings)
                returnValue = Convert.ToInt32(settings.Value);

            return returnValue;
        }

        /// <summary>
        /// Gets the email list of the first alert recepits
        /// </summary>
        /// <returns>email list</returns>
        public string GetFirstAlertEmailList()
        {
            string returnValue = string.Empty;

            CS_Settings settings = _settingsRepository.Get(e => e.ID == (int)Globals.Configuration.Settings.FirstAlertReceiptList);
            if (null != settings)
                returnValue = settings.Value;

            return returnValue;
        }

        /// <summary>
        /// Updates settings for the last date that the customer process ran
        /// </summary>
        /// <param name="lastDate"></param>
        public void UpdateLastUpdateCustomer(DateTime lastDate)
        {
            //CS_Settings settings = SettingsDao.Singleton.Get((int)Globals.Configuration.Settings.LastUpdateCustomer);
            CS_Settings settings = _settingsRepository.Get(e => e.ID == (int)Globals.Configuration.Settings.LastUpdateCustomer);
            if (null != settings)
            {
                settings.Value = lastDate.ToString("MM/dd/yyyy HH:mm:ss");
                //SettingsDao.Singleton.Update(settings);
                _settingsRepository.Update(settings);
            }
        }

        /// <summary>
        /// Updates settings for the last date that the contact process ran
        /// </summary>
        /// <param name="lastDate"></param>
        public void UpdateLastUpdateContact(DateTime lastDate)
        {
            //CS_Settings settings = SettingsDao.Singleton.Get((int)Globals.Configuration.Settings.LastUpdateContact);
            CS_Settings settings = _settingsRepository.Get(e => e.ID == (int)Globals.Configuration.Settings.LastUpdateContact);
            if (null != settings)
            {
                settings.Value = lastDate.ToString("MM/dd/yyyy HH:mm:ss");
                //SettingsDao.Singleton.Update(settings);
                _settingsRepository.Update(settings);
            }
        }

        /// <summary>
        /// Updates settings for the last date that the contract process ran
        /// </summary>
        /// <param name="lastDate"></param>
        public void UpdateLastUpdateContract(DateTime lastDate)
        {
            //CS_Settings settings = SettingsDao.Singleton.Get((int)Globals.Configuration.Settings.LastUpdateContract);
            CS_Settings settings = _settingsRepository.Get(e => e.ID == (int)Globals.Configuration.Settings.LastUpdateContract);
            if (null != settings)
            {
                settings.Value = lastDate.ToString("MM/dd/yyyy HH:mm:ss");
                //SettingsDao.Singleton.Update(settings);
                _settingsRepository.Update(settings);
            }
        }

        /// <summary>
        /// Updates settings for the Last generated Job number
        /// </summary>
        /// <param name="jobNumber"></param>
        public void UpdateLastJobNumber(int jobNumber)
        {
            //CS_Settings settings = SettingsDao.Singleton.Get((int)Globals.Configuration.Settings.LastJobNumber);
            CS_Settings settings = _settingsRepository.Get(e => e.ID == (int)Globals.Configuration.Settings.LastJobNumber);
            if (null != settings)
            {
                settings.Value = jobNumber.ToString();
                //SettingsDao.Singleton.Update(settings);
                _settingsRepository.Update(settings);
            }
        }

        /// <summary>
        /// Updates settings for the Last generated First Alert Number
        /// </summary>
        /// <param name="firstAlertNumber"></param>
        public void UpdateLastFirstAlertNumber(int firstAlertNumber)
        {
            CS_Settings settings = _settingsRepository.Get(e => e.ID == (int)Globals.Configuration.Settings.LastFirstAlertNumber);
            if (null != settings)
            {
                settings.Value = firstAlertNumber.ToString();
                _settingsRepository.Update(settings);
            }
        }

        /// <summary>
        /// Updates settings for the Last generated Job number
        /// </summary>
        /// <param name="jobNumber"></param>
        public void UpdateLastNonJobNumber(int nonJobNumber)
        {
            //CS_Settings settings = SettingsDao.Singleton.Get((int)Globals.Configuration.Settings.LastNonJobNumber);
            CS_Settings settings = _settingsRepository.Get(e => e.ID == (int)Globals.Configuration.Settings.LastNonJobNumber);
            if (null != settings)
            {
                settings.Value = nonJobNumber.ToString();
                //SettingsDao.Singleton.Update(settings);
                _settingsRepository.Update(settings);
            }
        }

        /// <summary>
        /// Update Settings for the DashBoard Refresh Rate
        /// </summary>
        /// <param name="newRate"></param>
        public void UpdateDashBoardRefreshRate(int newRate)
        {
            CS_Settings settings = _settingsRepository.Get(e => e.ID == (int)Globals.Configuration.Settings.DashboardRefreshRate);
            if (null != settings)
            {
                settings.Value = newRate.ToString();

                _settingsRepository.Update(settings);
            }
        }

        /// <summary>
        /// Gets the emails of the people that needs to be advised for interim bill porpouses
        /// </summary>
        public string GetInterimBillEmails()
        {
            CS_Settings settings = _settingsRepository.Get(e => e.ID == (int)Globals.Configuration.Settings.InterimBillEmails);
            if (null != settings)
                return settings.Value;
            else
                return string.Empty;
        }

        public string GetEstimationTeamEmails()
        {
            CS_Settings settings = _settingsRepository.Get(w => w.ID == (int) Globals.Configuration.Settings.EstimationTeam);

            if (null != settings)
                return settings.Value;

            return string.Empty;
        }

        public string GetInvoicingTeamEmails()
        {
            CS_Settings settings = _settingsRepository.Get(w => w.ID == (int)Globals.Configuration.Settings.InvoicingTeam);

            if (null != settings)
                return settings.Value;

            return string.Empty;
        }

        public string GetPermitTeamEmails()
        {
            CS_Settings settings = _settingsRepository.Get(w => w.ID == (int)Globals.Configuration.Settings.PermitTeam);

            if (null != settings)
                return settings.Value;

            return string.Empty;
        }

        public string GetTransportationTeamEmails()
        {
            CS_Settings settings = _settingsRepository.Get(e => e.ID == (int)Globals.Configuration.Settings.TransportationTeam);

            if (null != settings)
                return settings.Value;

            return string.Empty;
        }

        public string GetAddressChangeNotificationEmails()
        {
            CS_Settings settings = _settingsRepository.Get(e => e.ID == (int)Globals.Configuration.Settings.AddressChangeNotification);

            if (null != settings)
                return settings.Value;

            return string.Empty;
        }

        public string GetContactChangeNotificationEmails()
        {
            CS_Settings settings = _settingsRepository.Get(e => e.ID == (int)Globals.Configuration.Settings.ContactChangeNotification);

            if (null != settings)
                return settings.Value;

            return string.Empty;
        }

        /// <summary>
        /// Gets the domain of the application (dev, uat or hulcher.com)
        /// </summary>
        /// <returns>domain</returns>
        public string GetDomain()
        {
            CS_Settings settings = _settingsRepository.Get(e => e.ID == (int)Globals.Configuration.Settings.ApplicationDomain);
            if (null != settings)
                return settings.Value;
            else
                return string.Empty;
        }

        /// <summary>
        /// Gets the email configuration
        /// </summary>
        /// <returns>[0] - Account, [1] - Password, [2] - Domain</returns>
        public string[] GetEmailConfiguration()
        {
            CS_Settings settings = _settingsRepository.Get(e => e.ID == (int)Globals.Configuration.Settings.EmailConfiguration);
            if (null != settings)
                return settings.Value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            else
                return new string[3];
        }

        /// <summary>
        /// Gets the email listing of people/groups that need to be advised when a customer request is made
        /// </summary>
        /// <returns>email listing (splitted by ';')</returns>
        public string GetCustomerRequestEmails()
        {
            CS_Settings settings = _settingsRepository.Get(e => e.ID == (int)Globals.Configuration.Settings.CustomerRequestEmails);
            if (null != settings)
                return settings.Value;
            else
                return string.Empty;
        }

        /// <summary>
        /// Gets the email listing of people/groups that need to be advised when a contact request is made
        /// </summary>
        /// <returns>email listing (splitted by ';')</returns>
        public string GetContactRequestEmails()
        {
            CS_Settings settings = _settingsRepository.Get(e => e.ID == (int)Globals.Configuration.Settings.ContactRequestEmails);
            if (null != settings)
                return settings.Value;
            else
                return string.Empty;
        }

        #endregion

        #region [ IDisposable Implementation ]

        public void Dispose()
        {
            _settingsRepository = null;

            _unitOfWork.Dispose();
            _unitOfWork = null;
        }

        #endregion
    }
}
