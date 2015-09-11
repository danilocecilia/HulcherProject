using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Core.DaoInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Data
{
    public class JobDivisionDao : BaseDao<CS_JobDivision, long>, IJobDivisionDao
    {
        #region [ Attributes ]

        /// <summary>
        /// Static attribute to store the Compiled Query for listing all Divisions related to a Job
        /// </summary>
        private static Func<CustomerServiceModelContainer, int, IQueryable<CS_JobDivision>> _listAllByJobIdQuery;

        /// <summary>
        /// Singleton attribute
        /// </summary>
        private static IJobDivisionDao _singleton;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        private JobDivisionDao() { }

        #endregion

        #region [ Singleton ]

        /// <summary>
        /// Singleton Property - Contains an instance of this Class
        /// </summary>
        public static IJobDivisionDao Singleton
        {
            get
            {
                if (null == _singleton)
                    _singleton = new JobDivisionDao();

                return _singleton;
            }
        }

        #endregion

        #region [ IJobDivisionDao Implementation ]

        /// <summary>
        /// Property for a Compiled Query to List All Divisions related to a specific Job
        /// </summary>
        public Func<CustomerServiceModelContainer, int, IQueryable<CS_JobDivision>> ListAllByJobIdQuery
        {
            get
            {
                if (null == _listAllByJobIdQuery)
                {
                    _listAllByJobIdQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, int jobId) => from e in ctx.CS_JobDivision
                                                                          where e.JobID == jobId
                                                                          orderby e.ID
                                                                          select e);

                }
                return _listAllByJobIdQuery;
            }
        }

        /// <summary>
        /// Returns a list of all Divisions related to a Job Identifier
        /// </summary>
        /// <param name="jobId">Job Identifier</param>
        /// <returns>Division List</returns>
        public IList<CS_JobDivision> ListAllByJobId(int jobId)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                IList<CS_JobDivision> returnList = ListAllByJobIdQuery.Invoke(db, jobId).ToList<CS_JobDivision>();
                foreach (CS_JobDivision jobDivision in returnList)
                {
                    string t = jobDivision.CS_Division.Description;
                }
                return returnList;
            }
        }

        #endregion
    }
}
