using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.DataContext;
using System.Data.Objects;

namespace Hulcher.OneSource.CustomerService.Core.DaoInterfaces
{
    /// <summary>
    /// Interface for the Employee Dao Class
    /// </summary>
    public interface IEmployeeDao : IBaseDao<CS_Employee, long>
    {
        /// <summary>
        /// List all Employees filtered by Division
        /// </summary>
        /// <param name="divisionId"></param>
        /// <returns></returns>
        IList<CS_Employee> ListAllFIltered(long divisionId);

        /// <summary>
        /// Invoke Method of the Filtered Compiled Query
        /// </summary>
        /// /// <returns>Employee List</returns>
        IList<CS_Employee> ListAllEmployeeInfo();

        /// <summary>
        /// Invoke Method of the Filtered Compiled Query
        /// </summary>
        /// <param name="jobId">Job Identifier</param>
        /// <returns>Employee List</returns>
        IList<CS_Employee> ListAllEmployeeInfoByJob(int jobId);

        /// <summary>
        /// List all Employees filtered by Division
        /// </summary>
        /// <param name="divisionId"></param>
        /// <returns></returns>
        IList<CS_Employee> ListAllFilteredByName(string name);

        /// <summary>
        /// List all Employees filtered by Division
        /// </summary>
        /// <param name="divisionId"></param>
        /// <returns></returns>
        IList<CS_Employee> ListFilteredByName(long divisionId, string name);

        /// <summary>
        /// Imports the Employee data from Dynamics to Customer Service
        /// </summary>
        /// <param name="allEmployees">List of Employees to be imported</param>
        void BulkCopyAllEmployees(IDataReader allEmployees);

        /// <summary>
        /// Update Employee integration data
        /// </summary>
        void UpdateFromIntegration();

        /// <summary>
        /// List All RVP Employees
        /// </summary>
        /// <returns>List of Employees Entity</returns>
        IList<CS_Employee> ListAllRVP();

        /// <summary>
        /// Clears the table (for unit testing)
        /// </summary>
        void ClearAll();
    }
}
