using System;
using System.Configuration;
using System.ServiceProcess;
using System.Threading;

using Hulcher.OneSource.CustomerService.Business.Model;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Hulcher.OneSource.CustomerService.EmailService
{
    public partial class EmailService : ServiceBase
    {
        
        #region [ Attributes ]

        /// <summary>
        /// Thread for the integration process
        /// </summary>
        private Thread emailServiceThread;

        /// <summary>
        /// Thread for the integration process
        /// </summary>
        private Thread DeliveryServiceThread;

        /// <summary>
        /// AutoResetEvent to sleep/stop the integration process
        /// </summary>
        AutoResetEvent emailServiceStop = new AutoResetEvent(false);

        /// <summary>
        /// AutoResetEvent to sleep/stop the integration process
        /// </summary>
        AutoResetEvent deliveryServiceStop = new AutoResetEvent(false);

        #endregion

        #region [ Constructor ]
        /// <summary>
        /// Class constructor
        /// </summary>
        public EmailService()
        {
            InitializeComponent();
        }

        #endregion

        #region [ Events ]

        /// <summary>
        /// Method called when the service starts
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            emailServiceThread = new Thread(SendEmailsDoWork);
            emailServiceThread.Start();

            DeliveryServiceThread = new Thread(DeliveryConfirmationDoWork);
            DeliveryServiceThread.Start();
        }

        /// <summary>
        /// Method called when the service stops
        /// </summary>
        protected override void OnStop()
        {
            emailServiceStop.Set();
            emailServiceThread.Join();
            deliveryServiceStop.Set();
            DeliveryServiceThread.Join();
        }
        #endregion

        #region [ Methods ]

        /// <summary>
        /// Service logic method
        /// </summary>
        /// <param name="arg"></param>
        public void SendEmailsDoWork(object arg)
        {
            int miliSecondsTimeOut = Convert.ToInt32(ConfigurationManager.AppSettings["MilisecondsTimeOut"]);

            for (; ;)
            {
                if(emailServiceStop.WaitOne(miliSecondsTimeOut))
                    return;

                try
                {
                    EmailModel model = new EmailModel();
                    model.EmailServiceWork();
                }
                catch (Exception ex)
                {
                    Logger.Write(string.Format("An error has ocurred while running the Send Email Service!\n{0}\n{1}", ex.Message, ex.StackTrace));
                }
            }
        }

        public void DeliveryConfirmationDoWork(object arg)
        {
            int miliSecondsTimeOut = Convert.ToInt32(ConfigurationManager.AppSettings["MilisecondsTimeOut"]);

            for (; ; )
            {
                if (deliveryServiceStop.WaitOne(miliSecondsTimeOut))
                    return;

                try
                {
                    using (EmailModel model = new EmailModel())
                    {
                        model.DeliveryServiceWork();
                    }
                    using (EmailModel model = new EmailModel())
                    {
                        model.ReadServiceWork();
                    }
                }
                catch (Exception ex)
                {
                    Logger.Write(string.Format("An error has ocurred while running the Delivery Confirmation Service!\n{0}\n{1}", ex.Message, ex.StackTrace));
                }
            }
        }
        #endregion
    }
}
