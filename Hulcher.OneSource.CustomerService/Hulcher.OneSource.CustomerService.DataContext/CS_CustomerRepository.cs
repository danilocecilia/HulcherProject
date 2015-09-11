using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Configuration;
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_CustomerRepository
	{
		// Add your own data access methods.
		// This file should not get overridden
        public void BulkCopyAllCustomers(List<IDataReader> allCustomers)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("OneSourceConnectionString");
                db.ExecuteNonQuery(CommandType.Text, "truncate table CS_Customer_Load");
            }
            catch (Exception ex)
            {
                foreach (IDataReader dataReader in allCustomers)
                {
                    dataReader.Close(); // Close DataReader when an exception occurs
                }

                throw new Exception("An error ocurred while truncating data from Customer Info (load).", ex);
            }

            foreach (IDataReader dataReader in allCustomers)
            {
                string connString = ConfigurationManager.ConnectionStrings["OneSourceConnectionString"].ConnectionString;
                using (SqlBulkCopy copy = new SqlBulkCopy(connString))
                {
                    try
                    {
                        copy.DestinationTableName = "CS_Customer_Load";
                        copy.BulkCopyTimeout = 60000;
                        copy.WriteToServer(dataReader);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An error ocurred when importing Customer Info (BulkCopy).", ex);
                    }
                    finally
                    {
                        dataReader.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Executes a procedure that load the imported records into the respective tables
        /// </summary>
        public void UpdateFromIntegration()
        {
            using (var db = new CustomerServiceModelContainer())
            {
                db.CS_SP_UpdateCustomer();
            }
        }
	}
}