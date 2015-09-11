using System;
using System.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core.DaoInterfaces;

namespace Hulcher.OneSource.CustomerService.Data
{
    /// <summary>
    /// Job Model Class
    /// </summary>
    public class JobDao : BaseDao<CS_Job, long>, IJobDao
    {
        #region [ Attributes ]

        /// <summary>
        /// Static attribute to store the Compiled Query for listing all Jobs
        /// </summary>
        private static Func<CustomerServiceModelContainer, IQueryable<CS_Job>> _listAllQuery;

        /// <summary>
        /// Static attribute to store the Compiled Query for listing Jobs by equipment
        /// </summary>
        private static Func<CustomerServiceModelContainer, int, IQueryable<CS_Job>> _listByEquipment;

        /// <summary>
        /// Static attribute to store the Compiled Query for listing all Jobs by equipment and division
        /// </summary>
        private static Func<CustomerServiceModelContainer, int, int, IQueryable<CS_Job>> _listAllJobByEquip;

        /// <summary>
        /// Static attribute to store the Compiled Query for listing all Jobs by division name
        /// </summary>
        private static Func<CustomerServiceModelContainer, string, IQueryable<CS_Job>> _listAllByDivisionNameQuery;

        /// <summary>
        /// Static attribute to store the Compiled Query for listing all Jobs by customer name
        /// </summary>
        private static Func<CustomerServiceModelContainer, string, IQueryable<CS_Job>> _listAllByCustomerNameQuery;

        /// <summary>
        /// Static attribute to store the Compiled Query for listing all Jobs by site name
        /// </summary>
        private static Func<CustomerServiceModelContainer, string, IQueryable<CS_Job>> _listAllByLocationSiteNameQuery;

        /// <summary>
        /// Static attribute to store the Compiled Query for listing all Jobs by Number
        /// </summary>
        private static Func<CustomerServiceModelContainer, string, IQueryable<CS_Job>> _listAllByJobNumberQuery;

        /// <summary>
        /// Static attribute to store the Compiled Query for listing all Jobs by Number
        /// </summary>
        private static Func<CustomerServiceModelContainer, IQueryable<CS_Job>> _listAllForCallEntryQuery;

        /// <summary>
        /// Static attribute to store the Compiled Query for geting JobInfo for call entry 
        /// </summary>
        private static Func<CustomerServiceModelContainer, int, IQueryable<CS_Job>> _getJobInfoForCallEntryQuery;

        /// <summary>
        /// Singleton attribute
        /// </summary>
        private static IJobDao _singleton;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        public JobDao() { }

        #endregion

        #region [ Singleton ]

        /// <summary>
        /// Singleton Property - Contains an instance of this Class
        /// </summary>
        public static IJobDao Singleton
        {
            get
            {
                if (null == _singleton)
                    _singleton = new JobDao();
                
                return _singleton;
            }
        }

        #endregion

        #region [ Abstract BaseModel Implementation ]

        /// <summary>
        /// Property for a Compiled Query to List All itens of an Entity
        /// </summary>
        public Func<CustomerServiceModelContainer, IQueryable<CS_Job>> ListAllQuery
        {
            get
            {
                if (null == _listAllQuery)
                {
                    _listAllQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx) => from e in ctx.CS_Job
                                                               .Include("CS_CustomerInfo").Include("CS_CustomerInfo.CS_Customer")
                                                               .Include("CS_JobInfo")
                                                               where e.Active == true
                                                               select e);

                }
                return _listAllQuery;
            }
        }

        #endregion

        #region [ IJobDao Implementation ]

        /// <summary>
        /// Property for a Compiled Query to List All Jobs
        /// </summary>
        public new IList<CS_Job> ListAll()
        {
            using (var db = new CustomerServiceModelContainer())
            {
                IList<CS_Job> returnList = ListAllQuery.Invoke(db).ToList<CS_Job>();
                return returnList;
            }
        }

        /// <summary>
        /// Property for a Compiled Query to List Jobs by equipment
        /// </summary>
        public IList<CS_Job> ListJobsByEquipment(int equipmentId)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                IList<CS_Job> returnList = ListByEquipmentQuery.Invoke(db, equipmentId).ToList<CS_Job>();
                return returnList;
            }
        }

        /// <summary>
        /// Property for a Compiled Query to get a Contact by number
        /// </summary>
        public Func<CustomerServiceModelContainer, int, IQueryable<CS_Job>> ListByEquipmentQuery
        {
            get
            {
                if (null == _listByEquipment)
                {
                    _listByEquipment = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, int equipmentId) => from e in ctx.CS_Job
                                                                                from a in ctx.CS_Resource     
                                                                                     where e.ID == a.JobID 
                                                                                        && a.EquipmentID.Value == equipmentId
                                                                                        && e.Active == a.Active
                                                                                        && e.Active == true
                                                                                     select e);
                }
                return _listByEquipment;
            }
        }

        /// <summary>
        /// Property for a Compiled Query to List Jobs by equipment and division
        /// </summary>
        public Func<CustomerServiceModelContainer, int, int, IQueryable<CS_Job>> ListAllJobByEquip
        {
            get
            { 
                if(_listAllJobByEquip == null)
                {
                    _listAllJobByEquip = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, int equipmentTypeId, int divisionId) => (from e in ctx.CS_Job
                                                                                                        .Include("CS_Job.CS_JobInfo")
                                                                                                    from j in ctx.CS_Resource
                                                                                                    from k in ctx.CS_Equipment
                                                                                                    from l in ctx.CS_Division
                                                                                                    where
                                                                                                        j.JobID == e.ID
                                                                                                        && j.EquipmentID == k.ID
                                                                                                        && k.DivisionID == l.ID
                                                                                                        && k.EquipmentTypeID == equipmentTypeId
                                                                                                        && k.DivisionID == divisionId
                                                                                                        && e.Active
                                                                                                    select e).Distinct());
                }
                return _listAllJobByEquip;
            }
        }

        /// <summary>
        /// Property for a Compiled Query to List Jobs by division name
        /// </summary>
        public Func<CustomerServiceModelContainer, string, IQueryable<CS_Job>> ListAllByDivisionNameQuery
        {
            get
            {
                if (_listAllByDivisionNameQuery == null)
                {
                    _listAllByDivisionNameQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, string divisionName) => (from e in ctx.CS_Job
                                                                                          .Include("CS_CustomerInfo")
                                                                                          .Include("CS_CustomerInfo.CS_Customer")
                                                                                          .Include("CS_JobDivision")
                                                                                          .Include("CS_JobDivision.CS_Division")
                                                                                          .Include("CS_LocationInfo")
                                                                                          .Include("CS_LocationInfo.CS_City")
                                                                                          .Include("CS_LocationInfo.CS_State")
                                                                                     from a in ctx.CS_JobDivision
                                                                                     from b in ctx.CS_Division
                                                                                     where
                                                                                         a.JobID == e.ID
                                                                                         && a.DivisionID == b.ID
                                                                                         && b.Name == divisionName
                                                                                         && e.Active
                                                                                     select e).Distinct());
                }
                return _listAllByDivisionNameQuery;
            }
        }

        /// <summary>
        /// Property for a Compiled Query to List Jobs by customer name
        /// </summary>
        public Func<CustomerServiceModelContainer, string, IQueryable<CS_Job>> ListAllByCustomerNameQuery
        {
            get
            {
                if (_listAllByCustomerNameQuery == null)
                {
                    _listAllByCustomerNameQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, string customerName) => (from e in ctx.CS_Job
                                                                                          .Include("CS_CustomerInfo")
                                                                                          .Include("CS_CustomerInfo.CS_Customer")
                                                                                          .Include("CS_JobDivision")
                                                                                          .Include("CS_JobDivision.CS_Division")
                                                                                          .Include("CS_LocationInfo")
                                                                                          .Include("CS_LocationInfo.CS_City")
                                                                                          .Include("CS_LocationInfo.CS_State")
                                                                                     from a in ctx.CS_CustomerInfo
                                                                                     from b in ctx.CS_Customer
                                                                                     where
                                                                                         a.JobId == e.ID
                                                                                         && a.CustomerId == b.ID
                                                                                         && b.Name == customerName
                                                                                         && e.Active
                                                                                     select e).Distinct());
                }
                
                return _listAllByCustomerNameQuery;
            }
        }

        /// <summary>
        /// Property for a Compiled Query to List Jobs by site name
        /// </summary>
        public Func<CustomerServiceModelContainer, string, IQueryable<CS_Job>> ListAllByLocationSiteNameQuery
        {
            get
            {
                if (_listAllByLocationSiteNameQuery == null)
                {
                    _listAllByLocationSiteNameQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, string locationSiteName) => (from e in ctx.CS_Job
                                                                                          .Include("CS_CustomerInfo")
                                                                                          .Include("CS_CustomerInfo.CS_Customer")
                                                                                          .Include("CS_JobDivision")
                                                                                          .Include("CS_JobDivision.CS_Division")
                                                                                          .Include("CS_LocationInfo")
                                                                                          .Include("CS_LocationInfo.CS_City")
                                                                                          .Include("CS_LocationInfo.CS_State")
                                                                                         from a in ctx.CS_LocationInfo
                                                                                         where
                                                                                             a.JobID == e.ID
                                                                                             && a.SiteName == locationSiteName
                                                                                             && e.Active
                                                                                         select e).Distinct());
                }
                return _listAllByLocationSiteNameQuery;
            }
        }

        /// <summary>
        /// Property for a Compiled Query to List Jobs by site name
        /// </summary>
        public Func<CustomerServiceModelContainer, string, IQueryable<CS_Job>> ListAllByJobNumberQuery
        {
            get
            {
                if (_listAllByJobNumberQuery == null)
                {
                    _listAllByJobNumberQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, string jobNumber) => (from e in ctx.CS_Job
                                                                                          .Include("CS_CustomerInfo")
                                                                                          .Include("CS_CustomerInfo.CS_Customer")
                                                                                          .Include("CS_JobDivision")
                                                                                          .Include("CS_JobDivision.CS_Division")
                                                                                          .Include("CS_LocationInfo")
                                                                                          .Include("CS_LocationInfo.CS_City")
                                                                                          .Include("CS_LocationInfo.CS_State")
                                                                                         from a in ctx.CS_LocationInfo
                                                                                         where
                                                                                             a.JobID == e.ID
                                                                                             && e.Number == jobNumber
                                                                                             && e.Active
                                                                                         select e).Distinct());
                }
                return _listAllByJobNumberQuery;
            }
        }

        /// <summary>
        /// Property for a Compiled Query to List Jobs by site name
        /// </summary>
        public Func<CustomerServiceModelContainer, IQueryable<CS_Job>> ListAllForCallEntryQuery
        {
            get
            {
                if (_listAllForCallEntryQuery == null)
                {
                    _listAllForCallEntryQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx) => (from e in ctx.CS_Job
                                                                .Include("CS_CustomerInfo")
                                                                .Include("CS_CustomerInfo.CS_Customer")
                                                                .Include("CS_JobDivision")
                                                                .Include("CS_JobDivision.CS_Division")
                                                                .Include("CS_LocationInfo")
                                                                .Include("CS_LocationInfo.CS_City")
                                                                .Include("CS_LocationInfo.CS_State")
                                                                from a in ctx.CS_LocationInfo
                                                                where
                                                                    a.JobID == e.ID
                                                                    && e.Active
                                                                select e).Distinct());
                }
                return _listAllForCallEntryQuery;
            }
        }

        /// <summary>
        /// Property for a Compiled Query to get JobInfo by jobid
        /// </summary>
        public Func<CustomerServiceModelContainer, int, IQueryable<CS_Job>> GetJobInfoForCallEntryQuery
        {
            get
            {
                if (_getJobInfoForCallEntryQuery == null)
                {
                    _getJobInfoForCallEntryQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, int jobId) => from e in ctx.CS_Job
                                                                          .Include("CS_CustomerInfo")
                                                                          .Include("CS_CustomerInfo.CS_Customer")
                                                                          .Include("CS_JobDivision")
                                                                          .Include("CS_JobDivision.CS_Division")
                                                                          .Include("CS_LocationInfo")
                                                                          .Include("CS_LocationInfo.CS_City")
                                                                          .Include("CS_LocationInfo.CS_State")
                                                                          where e.ID == jobId
                                                                          && e.Active
                                                                          select e);
                }

                return _getJobInfoForCallEntryQuery;
            }
        }

        /// <summary>
        /// Invoke Method for the Compiled Query
        /// </summary>
        /// <param name="jobId">job identifier</param>
        /// <returns>List of jobs</returns>
        public CS_Job GetJobInfoForCallEntry(int jobId)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                return GetJobInfoForCallEntryQuery.Invoke(db, jobId).FirstOrDefault();
            }
        }

        /// <summary>
        /// Invoke Method for the Compiled Query
        /// </summary>
        /// <param name="equipmentTypeId">Equipment Identifier</param>
        /// <param name="divisionId">Division Identifier</param>
        /// <returns>List of jobs</returns>
        public IList<CS_Job> ListAllJobsByResource(int equipmentTypeId, int divisionId)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                return ListAllJobByEquip.Invoke(db, equipmentTypeId, divisionId).ToList();
            }
        }
        
        /// <summary>
        /// Invoke Method for the Compiled Query
        /// </summary>
        /// <param name="divisionName">Division Name</param>
        /// <returns>List of jobs</returns>
        public IList<CS_Job> ListAllByDivisionName(string divisionName)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                return ListAllByDivisionNameQuery.Invoke(db, divisionName).ToList();
            }
        }

        /// <summary>
        /// Invoke Method for the Compiled Query
        /// </summary>
        /// <param name="customerName">Customer Name</param>
        /// <returns>List of jobs</returns>
        public IList<CS_Job> ListAllByCustomerName(string customerName)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                return ListAllByCustomerNameQuery.Invoke(db, customerName).ToList();
            }
        }

        /// <summary>
        /// Invoke Method for the Compiled Query
        /// </summary>
        /// <param name="locationSiteName">Location Site Name</param>
        /// <returns>List of jobs</returns>
        public IList<CS_Job> ListAllByLocationSiteName(string locationSiteName)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                return ListAllByLocationSiteNameQuery.Invoke(db, locationSiteName).ToList();
            }
        }

        /// <summary>
        /// Invoke Method for the Compiled Query
        /// </summary>
        /// <param name="jobNumber">Number of the Job</param>
        /// <returns>List of corresponding jobs (Should be only one record)</returns>
        public IList<CS_Job> ListAllByJobNumber(string jobNumber)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                return ListAllByJobNumberQuery.Invoke(db, jobNumber).ToList();
            }
        }

        /// <summary>
        /// Invoke Method for the Compiled Query
        /// </summary>
        /// <param name="jobNumber">Number of the Job</param>
        /// <returns>List of corresponding jobs (Should be only one record)</returns>
        public IList<CS_Job> ListAllForCallEntry()
        {
            using (var db = new CustomerServiceModelContainer())
            {
                return ListAllForCallEntryQuery.Invoke(db).ToList();
            }
        }

        /// <summary>
        /// Delete all records from CS_Job
        /// </summary>
        public void ClearAll()
        {
            using (var db = new CustomerServiceModelContainer())
            {
                ExecuteSql(db, "delete from CS_Job");
            }
        }

        #endregion
    }
}
