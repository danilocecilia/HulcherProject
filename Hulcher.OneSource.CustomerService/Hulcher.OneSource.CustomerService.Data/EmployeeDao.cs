using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core.DaoInterfaces;

using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Hulcher.OneSource.CustomerService.Data
{
    public class EmployeeDao : BaseDao<CS_Employee, long>, IEmployeeDao
    {
        #region [ Attributes ]

        /// <summary>
        /// Static attribute to store the Compiled Query for getting a group of Employee's
        /// </summary>
        private static Func<CustomerServiceModelContainer, long, IQueryable<CS_Employee>> _listAllFilteredQuery;

        /// <summary>
        /// Static attribute to store the Compiled Query for getting a group of Employee's for call entry
        /// </summary>
        private static Func<CustomerServiceModelContainer, IQueryable<CS_Employee>> _listAllEmployeeInfoQuery;

        /// <summary>
        /// Static attribute to store the Compiled Query for getting a group of Employee's
        /// </summary>
        private static Func<CustomerServiceModelContainer, string, IQueryable<CS_Employee>> _listAllFilteredByNameQuery;

        /// <summary>
        /// Static attribute to store the Compiled Query for getting a group of Employee's
        /// </summary>
        private static Func<CustomerServiceModelContainer,long, string, IQueryable<CS_Employee>> _listFilteredByNameQuery;

        /// <summary>
        /// Static attribute to store the Compiled Query for getting the Employee RVPs
        /// </summary>
        private static Func<CustomerServiceModelContainer, IQueryable<CS_Employee>> _listAllRVPQuery;
        
        /// <summary>
        /// Static attribute to store the Compiled Query for Listing Employee by Job
        /// </summary>
        private static Func<CustomerServiceModelContainer, int, IQueryable<CS_Employee>> _listAllEmployeeInfoByJobQuery;

        /// <summary>
        /// Singleton attribute
        /// </summary>
        private static IEmployeeDao _singleton;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        private EmployeeDao() { }

        #endregion

        #region [ Singleton ]

        /// <summary>
        /// Singleton Property - Contains an instance of this Class
        /// </summary>
        public static IEmployeeDao Singleton
        {
            get
            {
                if (null == _singleton)
                    _singleton = new EmployeeDao();

                return _singleton;
            }
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Property for a Compiled Query to List All RVP itens of an Entity
        /// </summary>
        public Func<CustomerServiceModelContainer, IQueryable<CS_Employee>> ListAllRVPQuery
        {
            get
            {
                if (null == _listAllRVPQuery)
                {
                    _listAllRVPQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx) => from e in ctx.CS_Employee
                                                               where e.Active == true
                                                               & (e.JobCode.ToUpper().Contains("OPSRVP") || e.JobCode.ToUpper().Contains("REGVPS"))
                                                               select e);
                }
                return _listAllRVPQuery;
                
            }
        }

        /// <summary>
        /// List All RVP Employees
        /// </summary>
        /// <returns>List of Employees Entity</returns>
        public IList<CS_Employee> ListAllRVP()
        {
            using (var db = new CustomerServiceModelContainer())
            {
                IList<CS_Employee> returnList = ListAllRVPQuery.Invoke(db).ToList<CS_Employee>();
                return returnList;
            }
        }
        #endregion

        #region [ IEmployeeDao Implementation ]

        /// <summary>
        /// Property for a Compiled Query to List filtered itens of Employee
        /// </summary>
        public Func<CustomerServiceModelContainer, long, IQueryable<CS_Employee>> ListAllFilteredQuery
        {
            get
            {
                if (null == _listAllFilteredQuery)
                {
                    _listAllFilteredQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, long divisionId) => from e in ctx.CS_Employee
                                                                                where e.DivisionID == divisionId && e.Active == true
                                                                                select e);

                }
                return _listAllFilteredQuery;
            }
        }

        /// <summary>
        /// Property for a Compiled Query to List filtered itens of Employee for call entry
        /// </summary>
        public Func<CustomerServiceModelContainer, IQueryable<CS_Employee>> ListAllEmployeeInfoQuery
        {
            get
            {
                if (null == _listAllEmployeeInfoQuery)
                {
                    _listAllEmployeeInfoQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx) => from e in ctx.CS_Employee
                                                               .Include("CS_Resource")
                                                               .Include("CS_Resource.CS_Job")
                                                               .Include("CS_Resource.CS_Job.CS_CallLog")
                                                               where e.Active
                                                               select e);

                }
                return _listAllEmployeeInfoQuery;
            }
        }

        /// <summary>
        /// Property for a Compiled Query to List filtered itens of Employee by Job for call entry
        /// </summary>
        public Func<CustomerServiceModelContainer, int, IQueryable<CS_Employee>> ListAllEmployeeInfoByJobQuery
        {
            get
            {
                if (null == _listAllEmployeeInfoByJobQuery)
                {
                    _listAllEmployeeInfoByJobQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, int jobId) => from e in ctx.CS_Employee
                                                                          .Include("CS_Resource")
                                                                          .Include("CS_Resource.CS_Job")
                                                                          .Include("CS_Resource.CS_Job.CS_CallLog")
                                                                          from a in ctx.CS_Resource
                                                                          where e.ID == a.EmployeeID
                                                                          && a.JobID == jobId
                                                                          && e.Active
                                                                          select e);

                }
                return _listAllEmployeeInfoByJobQuery;
            }
        }

        /// <summary>
        /// Invoke Method of the Filtered Compiled Query
        /// </summary>
        public IList<CS_Employee> ListAllFIltered(long divisionId)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                IList<CS_Employee> returnList = ListAllFilteredQuery.Invoke(db, divisionId).ToList<CS_Employee>();
                return returnList;
            }
        }


        /// <summary>
        /// Invoke Method of the Filtered Compiled Query
        /// </summary>
        /// /// <returns>Employee List</returns>
        public IList<CS_Employee> ListAllEmployeeInfo()
        {
            using (var db = new CustomerServiceModelContainer())
            {
                IList<CS_Employee> returnList = ListAllEmployeeInfoQuery.Invoke(db).ToList<CS_Employee>();
                return returnList;
            }
        }

        /// <summary>
        /// Invoke Method of the Filtered Compiled Query
        /// </summary>
        /// <param name="jobId">Job Identifier</param>
        /// <returns>Employee List</returns>
        public IList<CS_Employee> ListAllEmployeeInfoByJob(int jobId)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                IList<CS_Employee> returnList = ListAllEmployeeInfoByJobQuery.Invoke(db, jobId).ToList<CS_Employee>();
                return returnList;
            }
        }

        

        /// <summary>
        /// Property for a Compiled Query to List filtered itens of Employee
        /// </summary>
        public Func<CustomerServiceModelContainer, string, IQueryable<CS_Employee>> ListAllFilteredByNameQuery
        {
            get
            {
                if (null == _listAllFilteredByNameQuery)
                {
                    _listAllFilteredByNameQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, string name) => from e in ctx.CS_Employee
                                                                                where e.Name.StartsWith(name) && e.Active == true
                                                                                select e);

                }
                return _listAllFilteredByNameQuery;
            }
        }

        /// <summary>
        /// Invoke Method of the Filtered Compiled Query
        /// </summary>
        public IList<CS_Employee> ListAllFilteredByName(string name)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                IList<CS_Employee> returnList = ListAllFilteredByNameQuery.Invoke(db, name).ToList<CS_Employee>();
                return returnList;
            }
        }

        /// <summary>
        /// Property for a Compiled Query to List filtered itens of Employee
        /// </summary>
        public Func<CustomerServiceModelContainer,long, string, IQueryable<CS_Employee>> ListFilteredByNameQuery
        {
            get
            {
                if (null == _listFilteredByNameQuery)
                {
                    _listFilteredByNameQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, long divisionId,string name) => from e in ctx.CS_Employee
                                                                            where e.DivisionID == divisionId && e.Name.StartsWith(name) && e.Active == true
                                                                            select e);

                }
                return _listFilteredByNameQuery;
            }
        }

        /// <summary>
        /// Invoke Method of the Filtered Compiled Query
        /// </summary>
        public IList<CS_Employee> ListFilteredByName(long divisionId, string name)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                IList<CS_Employee> returnList = ListFilteredByNameQuery.Invoke(db, divisionId, name).ToList<CS_Employee>();
                return returnList;
            }
        }

        /// <summary>
        /// Executes the BulkCopy process to load Employee Information from IVantage to a local Table
        /// </summary>
        /// <param name="allEmployees">IDataReader that contains all Employees loaded from IVantage</param>
        public void BulkCopyAllEmployees(IDataReader allEmployees)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("OneSourceConnectionString");
                db.ExecuteNonQuery(CommandType.Text, "truncate table CS_Employee_Load");
            }
            catch (Exception ex)
            {
                allEmployees.Close(); // Close DataReader when an exception occurs
                throw new Exception("An error ocurred while truncating data from Employee Info (load).", ex);
            }

            string connString = ConfigurationManager.ConnectionStrings["OneSourceConnectionString"].ConnectionString;
            using (SqlBulkCopy copy = new SqlBulkCopy(connString))
            {
                try
                {
                    copy.DestinationTableName = "CS_Employee_Load";
                    copy.BulkCopyTimeout = 600;
                    copy.WriteToServer(allEmployees);
                }
                catch (Exception ex)
                {
                    throw new Exception("An error ocurred when importing Employee Info (BulkCopy).", ex);
                }
                finally
                {
                    allEmployees.Close();
                }
            }
        }

        /// <summary>
        /// Execute the StoreProcedure to update information from ivantage
        /// </summary>
        public void UpdateFromIntegration()
        {
            using (var db = new CustomerServiceModelContainer())
            {
                db.CS_SP_UpdateEmployee();
            }
        }

        /// <summary>
        /// Clears the table (for unit testing)
        /// </summary>
        public void ClearAll()
        {
            using (var db = new CustomerServiceModelContainer())
            {
                ExecuteSql(db, "delete from CS_Employee");
            }
        }

        #endregion
    }
}
