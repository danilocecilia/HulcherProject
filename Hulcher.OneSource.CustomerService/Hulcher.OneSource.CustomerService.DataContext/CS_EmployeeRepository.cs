using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Configuration;
using System.Data.SqlClient;
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_EmployeeRepository
	{
		// Add your own data access methods.
		// This file should not get overridden

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
	}
}