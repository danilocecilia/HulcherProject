using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_CustomerContractRepository
	{
		// Add your own data access methods.
		// This file should not get overridden
        /// <summary>
        /// Executes the BulkCopy process to load Contract Information from Dynamics to a local Table
        /// </summary>
        /// <param name="allEmployees">IDataReader that contains all Contract loaded from Dynamics</param>
        public void BulkCopyAllContracts(List<IDataReader> allContracts)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("OneSourceConnectionString");
                db.ExecuteNonQuery(CommandType.Text, "truncate table CS_CustomerContract_Load");
            }
            catch (Exception ex)
            {
                foreach (IDataReader dataReader in allContracts)
                {
                    dataReader.Close(); // Close DataReader when an exception occurs
                }
                throw new Exception("An error ocurred while truncating data from Contract Info (load).", ex);
            }

            foreach (IDataReader dataReader in allContracts)
            {

                string connString = ConfigurationManager.ConnectionStrings["OneSourceConnectionString"].ConnectionString;
                using (SqlBulkCopy copy = new SqlBulkCopy(connString))
                {
                    try
                    {
                        copy.DestinationTableName = "CS_CustomerContract_Load";
                        copy.BulkCopyTimeout = 600;
                        copy.WriteToServer(dataReader);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An error ocurred when importing Contract Info (BulkCopy).", ex);
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
                db.CS_SP_UpdateContracts();
            }
        }
	}
}