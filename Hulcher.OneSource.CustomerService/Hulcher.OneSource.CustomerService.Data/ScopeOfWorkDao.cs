using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core.DaoInterfaces;
using System.Data.Objects;

namespace Hulcher.OneSource.CustomerService.Data
{
    public class ScopeOfWorkDao : BaseDao<CS_ScopeOfWork, long>, IScopeOfWorkDao
    {
        #region [ Attributes ]

        /// <summary>
        /// Static attribute to store the Compiled Query for getting a List of Scopes for a specific Job
        /// </summary>
        private static Func<CustomerServiceModelContainer, long, IQueryable<CS_ScopeOfWork>> _listAllByJobIdQuery;

        /// <summary>
        /// Singleton attribute
        /// </summary>
        private static IScopeOfWorkDao _singleton;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        public ScopeOfWorkDao() { }

        #endregion

        #region [ Singleton ]

        /// <summary>
        /// Singleton Property - Contains an instance of this Class
        /// </summary>
        public static IScopeOfWorkDao Singleton
        {
            get
            {
                if (null == _singleton)
                    _singleton = new ScopeOfWorkDao();

                return _singleton;
            }
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Property for a Compiled Query to Get a list of Entities through its Identifier
        /// </summary>
        public Func<CustomerServiceModelContainer, long, IQueryable<CS_ScopeOfWork>> ListAllByJobIdQuery
        {
            get
            {
                if (null == _listAllByJobIdQuery)
                {
                    _listAllByJobIdQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, long jobId) => from e in ctx.CS_ScopeOfWork
                                                                           where e.JobId == jobId
                                                                           orderby e.ID descending
                                                                           select e
                                                                           );

                }
                return _listAllByJobIdQuery;
            }
        }

        #endregion

        #region [ Methods ]

        public IList<CS_ScopeOfWork> ListAllByJobId(int jobId)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                IList<CS_ScopeOfWork> returnItem = ListAllByJobIdQuery.Invoke(db, jobId).ToList<CS_ScopeOfWork>();
                return returnItem;
            }
        }

        #endregion
    }
}
