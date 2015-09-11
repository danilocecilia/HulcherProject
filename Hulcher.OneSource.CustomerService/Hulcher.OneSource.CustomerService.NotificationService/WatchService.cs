using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using Hulcher.OneSource.CustomerService.Business.Model;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Hulcher.OneSource.CustomerService.NotificationService
{
    public partial class WatchService : ServiceBase
    {
        #region [ Attributes ]

        /// <summary>
        /// Thread for the integration process
        /// </summary>
        private Thread PresetJobDoWorkServiceThread;

        /// <summary>
        /// Thread for the equipment permits notification process
        /// </summary>
        private Thread EquipmentPermitDoWorkServiceThread;

        /// <summary>
        /// AutoResetEvent to sleep/stop the integration process
        /// </summary>
        AutoResetEvent PresetJobDoWorkServiceStop = new AutoResetEvent(false);

        /// <summary>
        /// AutoResetEvent to sleep/stop the equipment permits notification process
        /// </summary>
        AutoResetEvent EquipmentPermitDoWorkServiceStop = new AutoResetEvent(false);

        #endregion

        #region [ Constructor ]
        /// <summary>
        /// Class constructor
        /// </summary>
        public WatchService()
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
            PresetJobDoWorkServiceThread = new Thread(PresetJobDoWork);
            PresetJobDoWorkServiceThread.Start();

            EquipmentPermitDoWorkServiceThread = new Thread(EquipmentPermitDoWork);
            EquipmentPermitDoWorkServiceThread.Start();
        }

        /// <summary>
        /// Method called when the service stops
        /// </summary>
        protected override void OnStop()
        {
            PresetJobDoWorkServiceStop.Set();
            PresetJobDoWorkServiceThread.Join();

            EquipmentPermitDoWorkServiceStop.Set();
            EquipmentPermitDoWorkServiceThread.Join();
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Service logic method
        /// </summary>
        /// <param name="arg"></param>
        public void PresetJobDoWork(object arg)
        {
            int miliSecondsTimeOut = Convert.ToInt32(ConfigurationManager.AppSettings["MilisecondsTimeOut"]);

            for (; ; )
            {
                if (PresetJobDoWorkServiceStop.WaitOne(miliSecondsTimeOut))
                    return;

                try
                {
                    JobModel jobModel = new JobModel();
                    jobModel.CreateLapsedPresetCallLog();
                }
                catch (Exception ex)
                {
                    Logger.Write(string.Format("An error has ocurred while running the Preset Watch Service!\n{0}\n{1}", ex.Message, ex.StackTrace));
                }
            }
        }

        /// <summary>
        /// Service logic method
        /// </summary>
        /// <param name="arg"></param>
        public void EquipmentPermitDoWork(object arg)
        {
            int miliSecondsTimeOut = Convert.ToInt32(ConfigurationManager.AppSettings["MilisecondsTimeOut"]);

            for (; ; )
            {
                if (EquipmentPermitDoWorkServiceStop.WaitOne(miliSecondsTimeOut))
                    return;

                try
                {
                    EquipmentModel equipmentModel = new EquipmentModel();
                    equipmentModel.CreateLapsedPermitsEmail();
                }
                catch (Exception ex)
                {
                    Logger.Write(string.Format("An error has ocurred while running the Preset Watch Service!\n{0}\n{1}", ex.Message, ex.StackTrace));
                }
            }
        }

        #endregion
    }
}
