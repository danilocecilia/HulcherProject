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
    public class DivisionDao : BaseDao<CS_Division, long>, IDivisionDao
    {
        #region [ Attributes ]

        /// <summary>
        /// Singleton attribute
        /// </summary>
        private static IDivisionDao _singleton;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        public DivisionDao() { }

        #endregion
            
        #region [ Singleton ]

        /// <summary>
        /// Singleton Property - Contains an instance of this Class
        /// </summary>
        public static IDivisionDao Singleton
        {
            get
            {
                if (null == _singleton)
                    _singleton = new DivisionDao();

                return _singleton;
            }
        }

        #endregion

        #region [ IDivisionDao Members ]

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

        public void ClearAll()
        {
            using (var db = new CustomerServiceModelContainer())
            {
                ExecuteSql(db, "delete from CS_Division");
            }
        }

        #endregion
    }
}
