using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;
using System.Configuration;
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_DivisionRepository
	{
        /// <summary>
        /// Executes the StoredProcedure to update Division information from Integration
        /// </summary>
        public void UpdateFromIntegration()
        {
            using (var db = new CustomerServiceModelContainer())
            {
                db.CS_SP_UpdateDivision();
            }
        }

        /// <summary>
        /// Executes the BulkCopy process to load Division Information from IVantage to a local Table
        /// </summary>
        /// <param name="allDivisions">IDataReader that contains all Divisions loaded from IVantage</param>
        public void BulkCopyAllDivisions(IDataReader allDivisions)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("OneSourceConnectionString");
                db.ExecuteNonQuery(CommandType.Text, "truncate table CS_Division_Load");
            }
            catch (Exception ex)
            {
                allDivisions.Close(); // Close DataReader when an exception occurs
                throw new Exception("An error ocurred while truncating data from Division Info (load).", ex);
            }

            string connString = ConfigurationManager.ConnectionStrings["OneSourceConnectionString"].ConnectionString;
            using (SqlBulkCopy copy = new SqlBulkCopy(connString))
            {
                try
                {
                    copy.DestinationTableName = "CS_Division_Load";
                    copy.WriteToServer(allDivisions);
                }
                catch (Exception ex)
                {
                    throw new Exception("An error ocurred when importing Division Info (BulkCopy).", ex);
                }
                finally
                {
                    allDivisions.Close();
                }
            }
        }
	}
}