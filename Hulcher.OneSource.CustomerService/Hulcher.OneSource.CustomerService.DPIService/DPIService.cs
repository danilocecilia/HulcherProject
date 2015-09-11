using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;

using Hulcher.OneSource.CustomerService.Business.Model;

using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Hulcher.OneSource.CustomerService.DPIService
{
    public partial class DPIService : ServiceBase
    {
        #region [ Attributes ]

        /// <summary>
        /// Thread for the integration process
        /// </summary>
        private Thread DPIDoWorkServiceThread;

        /// <summary>
        /// AutoResetEvent to sleep/stop the integration process
        /// </summary>
        AutoResetEvent DPIDoWorkServiceStop = new AutoResetEvent(false);

        #endregion

        #region [ Constructor ]
        
        /// <summary>
        /// Class constructor
        /// </summary>
        public DPIService()
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
            DPIDoWorkServiceThread = new Thread(DPIDoWork);
            DPIDoWorkServiceThread.Start();
        }

        /// <summary>
        /// Method called when the service stops
        /// </summary>
        protected override void OnStop()
        {
            DPIDoWorkServiceStop.Set();
            DPIDoWorkServiceThread.Join();
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Service logic method
        /// </summary>
        /// <param name="arg"></param>
        public void DPIDoWork(object arg)
        {
            int miliSecondsTimeOut = Convert.ToInt32(ConfigurationManager.AppSettings["MilisecondsTimeOut"]);
            //int miliSecondsTimeOut = 10000;

            for (; ; )
            {
                if (DPIDoWorkServiceStop.WaitOne(miliSecondsTimeOut))
                    return;

                try
                {
                    DPIModel dpiModel = new DPIModel();
                    dpiModel.CalculateDPI();
                }
                catch (Exception ex)
                {
                    Logger.Write(string.Format("An error has ocurred while running the DPI Service!\n{0}\n{1}", ex.Message, ex.StackTrace));
                }
            }
        }

        #endregion
    }
}
