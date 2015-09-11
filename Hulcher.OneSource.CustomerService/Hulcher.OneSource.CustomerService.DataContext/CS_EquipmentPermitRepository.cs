using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using Microsoft.Practices.EnterpriseLibrary.Data;
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_EquipmentPermitRepository
	{
        /// <summary>
        /// Executes the StoredProcedure to update Equipment Permit information from Integration
        /// </summary>
        public void UpdateFromIntegration()
        {
            using (var db = new CustomerServiceModelContainer())
            {
                db.CS_SP_UpdateEquipmentPermit();
            }
        }

        /// <summary>
        /// Executes the BulkCopy process to load Eqiupment Permit Information from Dossier to a local Table
        /// </summary>
        /// <param name="all">IDataReader that contains all Equipment Permit loaded from Dossier</param>
        public void BulkCopyAllEquipmentPermit(IDataReader allEquipmentPermit)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("OneSourceDatabase");
                db.ExecuteNonQuery(CommandType.Text, "truncate table CS_EquipmentPermit_Load");
            }
            catch (Exception ex)
            {
                allEquipmentPermit.Close(); // Close DataReader when an exception occurs
                throw new Exception("An error ocurred while truncating data from Equipment Permit Info (load).", ex);
            }

            string connString = ConfigurationManager.ConnectionStrings["OneSourceDatabase"].ConnectionString;
            using (SqlBulkCopy copy = new SqlBulkCopy(connString))
            {
                try
                {
                    copy.DestinationTableName = "CS_EquipmentPermit_Load";
                    copy.WriteToServer(allEquipmentPermit);
                }
                catch (Exception ex)
                {
                    throw new Exception("An error ocurred when importing Equipment Info (BulkCopy).", ex);
                }
                finally
                {
                    allEquipmentPermit.Close();
                }
            }
        }
	}
}