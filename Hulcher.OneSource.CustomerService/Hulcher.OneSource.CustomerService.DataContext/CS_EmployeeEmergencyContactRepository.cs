using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_EmployeeEmergencyContactRepository
	{
		// Add your own data access methods.
		// This file should not get overridden

        /// <summary>
        /// Executes the BulkCopy process to load Emergency Contact Information from IVantage to a local Table
        /// </summary>
        /// <param name="emergencyContactsReader">IDataReader that contains all Emergency Contacts loaded from IVantage</param>
        public void BulkCopyAllEmergencyContacts(IDataReader emergencyContactsReader)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("OneSourceConnectionString");
                db.ExecuteNonQuery(CommandType.Text, "truncate table CS_EmployeeEmergencyContact_Load");
            }
            catch (Exception ex)
            {
                emergencyContactsReader.Close(); // Close DataReader when an exception occurs
                throw new Exception("An error ocurred while truncating data from Emergency Contact Info (load).", ex);
            }

            string connString = ConfigurationManager.ConnectionStrings["OneSourceConnectionString"].ConnectionString;
            using (SqlBulkCopy copy = new SqlBulkCopy(connString))
            {
                try
                {
                    copy.DestinationTableName = "CS_EmployeeEmergencyContact_Load";
                    copy.BulkCopyTimeout = 600;
                    copy.WriteToServer(emergencyContactsReader);
                }
                catch (Exception ex)
                {
                    throw new Exception("An error ocurred when importing Emergency Contacts Info (BulkCopy).", ex);
                }
                finally
                {
                    emergencyContactsReader.Close();
                }
            }
        }

        /// <summary>
        /// Executes the StoredProcedure to update Employee Emergency Contact information from Integration
        /// </summary>
        public void UpdateFromIntegration()
        {
            using (var db = new CustomerServiceModelContainer())
            {
                db.CS_SP_UpdateEmployeeEmergencyContact();
            }
        }
	}
}