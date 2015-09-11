using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Hulcher.OneSource.CustomerService.DataContext;
using System.Data.SqlClient;
using System.Configuration;

namespace Hulcher.OneSource.CustomerService.Web
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            SqlDependency.Start(ConfigurationManager.ConnectionStrings["OneSourceConnectionString"].ConnectionString);

            CachedRepository<CS_JobStatus> _jobStatusRepository = new CachedRepository<CS_JobStatus>();
            _jobStatusRepository.LoadCacheItem(j => j.Active);

            CachedRepository<CS_PriceType> _priceTypeRepository = new CachedRepository<CS_PriceType>();
            _priceTypeRepository.LoadCacheItem(j => j.Active);

            CachedRepository<CS_JobCategory> _jobCategoryRepository = new CachedRepository<CS_JobCategory>();
            _jobCategoryRepository.LoadCacheItem(j => j.Active);

            CachedRepository<CS_JobType> _jobTypeRepository = new CachedRepository<CS_JobType>();
            _jobTypeRepository.LoadCacheItem(j => j.Active);

            CachedRepository<CS_JobAction> _jobActionRepository = new CachedRepository<CS_JobAction>();
            _jobActionRepository.LoadCacheItem(j => j.Active);

            CachedRepository<CS_LostJobReason> _lostJobReasonRepository = new CachedRepository<CS_LostJobReason>();
            _lostJobReasonRepository.LoadCacheItem(j => j.Active);

            CachedRepository<CS_Frequency> _frequencyRepository = new CachedRepository<CS_Frequency>();
            _frequencyRepository.LoadCacheItem(j => j.Active);

            CachedRepository<CS_LocalEquipmentType> _localEquipmentTypeRepository = new CachedRepository<CS_LocalEquipmentType>();
            _localEquipmentTypeRepository.LoadCacheItem(j => j.Active);

            //CachedRepository<CS_CallType> _localCallTypeRepository = new CachedRepository<CS_CallType>();
            //_localCallTypeRepository.LoadCacheItem(j => j.Active);
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
