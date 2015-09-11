using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;
using System.Data.SqlClient;
using System.Configuration;

namespace Hulcher.OneSource.CustomerService.DataContext
{
    public class Cache<T> where T : class
    {
        private ICachedRepository<T> _repository;

        private string stringType = typeof(T).ToString();
        private ObjectCache cache = MemoryCache.Default;

        public Cache(ICachedRepository<T> Repository)
        {
            _repository = Repository;
        }

        public void AddObject(IList<T> objectList)
        {
            SqlDependency.Start(ConfigurationManager.ConnectionStrings["OneSourceConnectionString"].ConnectionString);

            CacheItemPolicy policy = new CacheItemPolicy();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["OneSourceConnectionString"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(
                       string.Format("Select ModificationDate From dbo.{0} (NOLOCK)", stringType.Substring(stringType.IndexOf("CS_")))))
                {
                    command.Notification = null;

                    SqlDependency dep = new SqlDependency();

                    dep.AddCommandDependency(command);

                    command.Connection = conn;
                    command.Connection.Open();
                    command.ExecuteNonQuery();

                    SqlChangeMonitor monitor = new SqlChangeMonitor(dep);

                    policy.ChangeMonitors.Add(monitor);
                }
            }

            policy.AbsoluteExpiration = ObjectCache.InfiniteAbsoluteExpiration;
            policy.RemovedCallback += new CacheEntryRemovedCallback(_repository.ReloadCacheItem);

            cache.Add(new CacheItem(stringType, objectList), policy);   
        }
    }
}
