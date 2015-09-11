using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Configuration;
using System.Data.SqlClient;
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_ContactRepository
	{
		// Add your own data access methods.
		// This file should not get overridden
        /// <summary>
        /// Executes the BulkCopy process to load Contact Information from Dynamics to a local Table
        /// </summary>
        /// <param name="allEmployees">IDataReader that contains all Contact loaded from Dynamics</param>
        public void BulkCopyAllContacts(List<IDataReader> allContacts)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("OneSourceConnectionString");
                db.ExecuteNonQuery(CommandType.Text, "truncate table CS_Contact_Load");
            }
            catch (Exception ex)
            {
                foreach (IDataReader dataReader in allContacts)
                {
                    dataReader.Close(); // Close DataReader when an exception occurs
                }
                throw new Exception("An error ocurred while truncating data from Contact Info (load).", ex);
            }
            foreach (IDataReader dataReader in allContacts)
            {
                string connString = ConfigurationManager.ConnectionStrings["OneSourceConnectionString"].ConnectionString;
                using (SqlBulkCopy copy = new SqlBulkCopy(connString))
                {
                    try
                    {
                        copy.DestinationTableName = "CS_Contact_Load";
                        copy.BulkCopyTimeout = 6000;
                        copy.WriteToServer(dataReader);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An error ocurred when importing Contact Info (BulkCopy).", ex);
                    }
                    finally
                    {
                        dataReader.Close();
                    }
                }
            }
        }

        public void UpdateFromIntegration()
        {
            using (var db = new CustomerServiceModelContainer())
            {
                db.CS_SP_UpdateContact();
            }
        }
	}
}