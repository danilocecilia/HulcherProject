using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;
using System.Configuration;
	
namespace Hulcher.OneSource.CustomerService.DataContext
{   
	public partial class CS_EquipmentRepository
	{
        /// <summary>
        /// Executes the StoredProcedure to update Equipment information from Integration
        /// </summary>
        public void UpdateFromIntegration()
        {
            using (var db = new CustomerServiceModelContainer())
            {
                db.CS_SP_UpdateEquipmentAndType();
            }
        }

        /// <summary>
        /// Executes the BulkCopy process to load Eqiupment Information from Dossier to a local Table
        /// </summary>
        /// <param name="all">IDataReader that contains all Equipments loaded from Dossier</param>
        public void BulkCopyAllEquipments(IDataReader allEquipments)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("OneSourceDatabase");
                db.ExecuteNonQuery(CommandType.Text, "truncate table CS_Equipment_Load");
            }
            catch (Exception ex)
            {
                allEquipments.Close(); // Close DataReader when an exception occurs
                throw new Exception("An error ocurred while truncating data from Equipment Info (load).", ex);
            }

            string connString = ConfigurationManager.ConnectionStrings["OneSourceDatabase"].ConnectionString;
            using (SqlBulkCopy copy = new SqlBulkCopy(connString))
            {
                try
                {
                    copy.DestinationTableName = "CS_Equipment_Load";
                    copy.WriteToServer(allEquipments);
                }
                catch (Exception ex)
                {
                    throw new Exception("An error ocurred when importing Equipment Info (BulkCopy).", ex);
                }
                finally
                {
                    allEquipments.Close();
                }
            }
        }
	}
}