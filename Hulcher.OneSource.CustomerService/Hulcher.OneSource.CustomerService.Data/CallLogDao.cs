using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core.DaoInterfaces;
using System.Data.Objects;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Core;

namespace Hulcher.OneSource.CustomerService.Data
{
    public class CallLogDao : BaseDao<CS_CallLog, int>, ICallLogDao
    {
        #region [ Attributes ]

        /// <summary>
        /// Static attribute to store the Compiled Query for listing all Call Logs
        /// </summary>
        private static Func<CustomerServiceModelContainer, IQueryable<CS_CallLog>> _listAllQuery;

        /// <summary>
        /// Static attribute to store the Compiled Query for getting a specific
        /// </summary>
        private static Func<CustomerServiceModelContainer, int, IQueryable<CS_CallLog>> _getQuery;

        /// <summary>
        /// Static attribute to store the Compiled Query for getting all Call Logs for a specific Job
        /// </summary>
        private static Func<CustomerServiceModelContainer, long, IQueryable<CS_CallLog>> _getJobCallLogsQuery;

        /// <summary>
        /// Static attribute to store the Compiled Query for getting specfific Call Logs, filtered by Date
        /// </summary>
        private static Func<CustomerServiceModelContainer, long, DateTime, IQueryable<CS_CallLog>> _getJobCallLogsFilteredByDateQuery;

        /// <summary>
        /// Static attribute to store the Compiled Query for getting specfific Call Logs, filtered by Time
        /// </summary>
        private static Func<CustomerServiceModelContainer, long, DateTime, IQueryable<CS_CallLog>> _getJobCallLogsFilteredByTimeQuery;

        /// <summary>
        /// Static attribute to store the Compiled Query for getting specfific Call Logs, filtered by Type
        /// </summary>
        private static Func<CustomerServiceModelContainer, long, string, IQueryable<CS_CallLog>> _getJobCallLogsFilteredByTypeQuery;

        /// <summary>
        /// Static attribute to store the Compiled Query for getting specfific Call Logs, filtered by User
        /// </summary>
        private static Func<CustomerServiceModelContainer, long, string, IQueryable<CS_CallLog>> _getJobCallLogsFilteredByUserQuery;

        /// <summary>
        /// Singleton attribute
        /// </summary>
        private static ICallLogDao _singleton;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        private CallLogDao() { }

        #endregion

        #region [ Singleton ]

        /// <summary>
        /// Singleton Property - Contains a instance of this class
        /// </summary>
        public static ICallLogDao Singleton
        {
            get
            {
                if (null == _singleton)
                    _singleton = new CallLogDao();

                return _singleton;
            }
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Property for a Compiled Query to List All itens of an Entity
        /// </summary>
        public Func<CustomerServiceModelContainer, IQueryable<CS_CallLog>> ListAllQuery
        {
            get 
            {
                if (null == _listAllQuery)
                {
                    _listAllQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx) => from e in ctx.CS_CallLog
                                                               //.Include("CS_CallType")
                                                               //.Include("CS_CallType.CS_PrimaryCallType")
                                                               //where e.Active == true
                                                               //orderby e.CallDate descending
                                                               select e);
                }

                return _listAllQuery;
            }
        }

        /// <summary>
        /// Property for a Compiled Query to Get a specific Entity through its Identifier
        /// </summary>
        public Func<CustomerServiceModelContainer, int, IQueryable<CS_CallLog>> GetQuery
        {
            get
            {
                if (null == _getQuery)
                {
                    _getQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, int callLogId) => from e in ctx.CS_CallLog
                                                                               //.Include("CS_CallType")
                                                                               //.Include("CS_CallType.CS_PrimaryCallType")
                                                                               //where e.ID == callLogId
                                                                               //&& e.Active == true
                                                                               //orderby e.CallDate descending
                                                                               select e);

                }
                return _getQuery;
            }
        }

        /// <summary>
        /// Property for a Compiled Query that returns All Call logs for a specific Job
        /// </summary>
        public Func<CustomerServiceModelContainer, long, IQueryable<CS_CallLog>> GetJobCallLogsQuery
        {
            get
            {
                if (null == _getJobCallLogsQuery)
                {
                    _getJobCallLogsQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, long jobId) => from e in ctx.CS_CallLog
                                                                          // .Include("CS_CallType")
                                                                          // .Include("CS_CallType.CS_PrimaryCallType")
                                                                          //where e.JobID == jobId
                                                                          //&& e.Active == true
                                                                          //orderby e.CallDate descending
                                                                          select e);
                }

                return _getJobCallLogsQuery;
            }
        }

        /// <summary>
        /// Property for a Compiled Query that returns All Call logs for a specific Job, filtered by Date
        /// </summary>
        public Func<CustomerServiceModelContainer, long, DateTime, IQueryable<CS_CallLog>> GetJobCallLogsFilteredByDateQuery
        {
            get
            {
                if (null == _getJobCallLogsFilteredByDateQuery)
                {
                    _getJobCallLogsFilteredByDateQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, long jobId, DateTime date) => from e in ctx.CS_CallLog
                                                                                          //.Include("CS_CallType")
                                                                                          //.Include("CS_CallType.CS_PrimaryCallType")
                                                                                          //where e.JobID == jobId
                                                                                          //&& e.Active == true
                                                                                          //&& e.CallDate.Year == date.Year
                                                                                          //&& e.CallDate.Month == date.Month
                                                                                          //&& e.CallDate.Day == date.Day
                                                                                          //orderby e.CallDate descending
                                                                                          select e);
                }

                return _getJobCallLogsFilteredByDateQuery;
            }
        }

        /// <summary>
        /// Property for a Compiled Query that returns All Call logs for a specific Job, filtered by Time
        /// </summary>
        public Func<CustomerServiceModelContainer, long, DateTime, IQueryable<CS_CallLog>> GetJobCallLogsFilteredByTimeQuery
        {
            get
            {
                if (null == _getJobCallLogsFilteredByTimeQuery)
                {
                    _getJobCallLogsFilteredByTimeQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, long jobId, DateTime time) => from e in ctx.CS_CallLog
                                                                                          //.Include("CS_CallType")
                                                                                          //.Include("CS_CallType.CS_PrimaryCallType")
                                                                                          //where e.JobID == jobId
                                                                                          //&& e.Active == true
                                                                                          //&& e.CallDate.Hour == time.Hour
                                                                                          //&& e.CallDate.Minute == time.Minute
                                                                                          //orderby e.CallDate descending
                                                                                          select e);
                }

                return _getJobCallLogsFilteredByTimeQuery;
            }
        }

        /// <summary>
        /// Property for a Compiled Query that returns All Call logs for a specific Job, filtered by Type
        /// </summary>
        public Func<CustomerServiceModelContainer, long, string, IQueryable<CS_CallLog>> GetJobCallLogsFilteredByTypeQuery
        {
            get
            {
                if (null == _getJobCallLogsFilteredByTypeQuery)
                {
                    _getJobCallLogsFilteredByTypeQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, long jobId, string type) => from e in ctx.CS_CallLog
                                                                                        //.Include("CS_CallType")
                                                                                        //.Include("CS_CallType.CS_PrimaryCallType")
                                                                                        //where e.JobID == jobId
                                                                                        //&& e.Active == true
                                                                                        //&& e.CS_CallType.CS_PrimaryCallType.Type.Contains(type)
                                                                                        //orderby e.CallDate descending
                                                                                        select e);

                }

                return _getJobCallLogsFilteredByTypeQuery;
            }
        }

        /// <summary>
        /// Property for a Compiled Query that returns All Call logs for a specific Job, filtered by Creation User
        /// </summary>
        public Func<CustomerServiceModelContainer, long, string, IQueryable<CS_CallLog>> GetJobCallLogsFilteredByUserQuery
        {
            get
            {
                if (null == _getJobCallLogsFilteredByUserQuery)
                {
                    _getJobCallLogsFilteredByUserQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, long jobId, string user) => from e in ctx.CS_CallLog
                                                                                        //.Include("CS_CallType")
                                                                                        //.Include("CS_CallType.CS_PrimaryCallType")
                                                                                        //where e.JobID == jobId
                                                                                        //&& e.Active == true
                                                                                        //&& e.CreatedBy.Contains(user)
                                                                                        //orderby e.CallDate descending
                                                                                        select e
                                                                                        );
                }

                return _getJobCallLogsFilteredByUserQuery;
            }
        }

        #endregion

        #region [ Methods ]

        public new IList<CS_CallLog> ListAll()
        {
            using (var db = new CustomerServiceModelContainer())
            {
                IList<CS_CallLog> returnItem = ListAllQuery.Invoke(db).ToList<CS_CallLog>();
                return returnItem;
            }
        }

        public new CS_CallLog Get(int id)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                CS_CallLog returnItem = GetQuery.Invoke(db, id).FirstOrDefault<CS_CallLog>();
                return returnItem;
            }
        }

        /// <summary>
        /// Executes a query that returns all Call Logs for a specific Job
        /// </summary>
        /// <param name="jobId">The ID of the selected Job</param>
        /// <returns>A list with all records returned, as CS_CallLog entities</returns>
        public IList<CS_CallLog> ListJobCallLogs(long jobId)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                IList<CS_CallLog> returnItem = GetJobCallLogsQuery.Invoke(db, jobId).ToList<CS_CallLog>();
                return returnItem;
            }
        }

        /// <summary>
        /// Executes a query that returns all Call Logs for a specific Job, according to the specified Filter
        /// </summary>
        /// <param name="type">The Type of the Filter that is going to be used</param>
        /// <param name="value">The value that was input by the user</param>
        /// <param name="jobId">The ID of the selected Job</param>
        /// <returns>A list with all records returned, as CS_CallLog entities</returns>
        public IList<CS_CallLog> ListFilteredJobCallLogs(Globals.JobRecord.FilterType type, string value, long jobId)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                IList<CS_CallLog> returnItem;

                switch (type)
                {
                    case Globals.JobRecord.FilterType.Date:
                        DateTime dt;
                        DateTime.TryParse(value, out dt);
                        returnItem = GetJobCallLogsFilteredByDateQuery.Invoke(db, jobId, dt).ToList<CS_CallLog>();
                        break;
                    case Globals.JobRecord.FilterType.Time:
                        DateTime dt1;
                        DateTime.TryParse(value, out dt1);
                        returnItem = GetJobCallLogsFilteredByTimeQuery.Invoke(db, jobId, dt1).ToList<CS_CallLog>();
                        break;
                    case Globals.JobRecord.FilterType.Type:
                        returnItem = GetJobCallLogsFilteredByTypeQuery.Invoke(db, jobId, value).ToList<CS_CallLog>();
                        break;
                    case Globals.JobRecord.FilterType.User:
                        returnItem = GetJobCallLogsFilteredByUserQuery.Invoke(db, jobId, value).ToList<CS_CallLog>();
                        break;
                    default:
                        returnItem = new List<CS_CallLog>();
                        break;
                }

                return returnItem;
            }
        }

        #endregion
    }
}
