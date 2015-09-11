using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core.DaoInterfaces;
using System.Data.Objects;

namespace Hulcher.OneSource.CustomerService.Data
{
    public class ReserveDao : BaseDao<CS_Reserve, int>, IReserveDao
    {
        #region [ Attributes ]

        private static Func<CustomerServiceModelContainer, int, IQueryable<CS_Reserve>> _getReserveByJob;

        private static IReserveDao _singleton;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class constructor
        /// </summary>
        public ReserveDao() { }

        #endregion

        #region [ Singleton ]

        /// <summary>
        /// Singleton Property - Contains an instance of this Class
        /// </summary>
        public static IReserveDao Singleton
        {
            get
            {
                if (null == _singleton)
                    _singleton = new ReserveDao();

                return _singleton;
            }
        }

        #endregion

        #region [ IReserveDao Implementation ]

        public Func<CustomerServiceModelContainer, int, IQueryable<CS_Reserve>> GetListReserveByJob
        {
            get
            {
                if(_getReserveByJob == null)
                {
                    _getReserveByJob = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, int jobId) => from e in ctx.CS_Reserve
                                                                          where e.JobID == jobId
                                                                          && e.Active == true
                                                                          select e);
                }
                return _getReserveByJob;
            }
        }


        /// <summary>
        /// Clear all records (for unit testing)
        /// </summary>
        public void ClearAll()
        {
            using (var db = new CustomerServiceModelContainer())
            {
                ExecuteSql(db, "delete from CS_Reserve");
            }
        }

        /// <summary>
        /// List Reserve by Job
        /// </summary>
        /// <param name="jobId">jobid</param>
        /// <returns>list</returns>
        public IList<CS_Reserve> GetListReserveByJobId(int jobId)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                return GetListReserveByJob.Invoke(db, jobId).ToList();
            }
        }

        #endregion
    }
}
