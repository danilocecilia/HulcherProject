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
    public class EmergencyContactDao : BaseDao<CS_EmployeeEmergencyContact, long>, IEmergencyContactDao
    {
        #region [ Attributes ]

        /// <summary>
        /// Static attribute to store the Compiled Query for getting a group of Employee's
        /// </summary>
        private static Func<CustomerServiceModelContainer, long, IQueryable<CS_EmployeeEmergencyContact>> _listAllEmployeeEmergencyContactsQuery;

        /// <summary>
        /// Singleton attribute
        /// </summary>
        private static IEmergencyContactDao _singleton;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        public EmergencyContactDao() { }

        #endregion

        #region [ Singleton ]

        /// <summary>
        /// Singleton Property - Contains an instance of this Class
        /// </summary>
        public static IEmergencyContactDao Singleton
        {
            get
            {
                if (null == _singleton)
                    _singleton = new EmergencyContactDao();

                return _singleton;
            }
        }

        #endregion

        #region [ IEmergencyContactDao Implementation ]

        /// <summary>
        /// Property for a Compiled Query to List filtered itens of EmployeeEmergencyContacts
        /// </summary>
        public Func<CustomerServiceModelContainer, long, IQueryable<CS_EmployeeEmergencyContact>> ListAllEmployeeEmergencyContactsQuery
        {
            get
            {
                if (null == _listAllEmployeeEmergencyContactsQuery)
                {
                    _listAllEmployeeEmergencyContactsQuery = CompiledQuery.Compile(
                        (CustomerServiceModelContainer ctx, long employeeID) => from e in ctx.CS_EmployeeEmergencyContact
                                                                                where e.EmployeeID == employeeID
                                                                                && e.Active == true
                                                                                select e);

                }
                return _listAllEmployeeEmergencyContactsQuery;
            }
        }

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
        /// Invoke Method of the Filtered Compiled Query
        /// </summary>
        public IList<CS_EmployeeEmergencyContact> ListAllEmployeeEmergencyContacts(long divisionId)
        {
            using (var db = new CustomerServiceModelContainer())
            {
                IList<CS_EmployeeEmergencyContact> returnList = ListAllEmployeeEmergencyContactsQuery.Invoke(db, divisionId).ToList<CS_EmployeeEmergencyContact>();
                return returnList;
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

        #endregion
    }
}
