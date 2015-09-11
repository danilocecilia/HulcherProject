using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace Hulcher.OneSource.CustomerService.Integration
{
    public class IVantageIntegration
    {
        #region [ Attributes ]

        private static IVantageIntegration _singleton;
        private string[] connectionStringNames;

        #endregion

        #region [ Singleton ]

        /// <summary>
        /// Singleton Property - Contains an instance of this Class
        /// </summary>
        public static IVantageIntegration Singleton
        {
            get
            {
                if (null == _singleton)
                    _singleton = new IVantageIntegration();

                return _singleton;
            }
        }

        #endregion

        #region [ Constructors ]

        private IVantageIntegration()
        {
            ///For consistency between projects, we're using the same structure, although IVantageIntegration
            ///only works with one Database
            connectionStringNames = ConfigurationManager.AppSettings["ivantageConnectionStrings"].Split(';');
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Returns all Divisions from IVantage
        /// </summary>
        /// <returns>IDataReader that contains All Divisions</returns>
        public IDataReader ListAllDivisions()
        {
            try
            {
                //Using the first (and only) name, since IVantage only works with one Database
                Database db = DatabaseFactory.CreateDatabase(connectionStringNames[0]);
                DbCommand command = db.GetSqlStringCommand("select * from OneSource_Divisions");
                return db.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                throw new Exception("An error has ocurred while trying to get Division information from IVantage.", ex);
            }
        }

        /// <summary>
        /// Returns all Employees from IVantage
        /// </summary>
        /// <returns>IDataReader that contains All Employees</returns>
        public IDataReader ListAllEmployees()
        {
            try
            {
                //Using the first (and only) name, since IVantage only works with one Database
                Database db = DatabaseFactory.CreateDatabase(connectionStringNames[0]);
                DbCommand command = db.GetSqlStringCommand("select * from OneSource_Employees");
                command.CommandTimeout = 20000;
                return db.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                throw new Exception("An error has ocurred while trying to get Employee information from IVantage.", ex);
            }
        }

        /// <summary>
        /// Returns all Emergency Contacts from IVantage
        /// </summary>
        /// <returns>IDataReader that contains All Emergency Contacts</returns>
        public IDataReader ListAllEmergencyContacts()
        {
            try
            {
                //Using the first (and only) name, since IVantage only works with one Database
                Database db = DatabaseFactory.CreateDatabase(connectionStringNames[0]);
                DbCommand command = db.GetSqlStringCommand("select * from OneSource_EmergencyContacts");
                return db.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                throw new Exception("An error has ocurred while trying to get Emergency Contact information from IVantage.", ex);
            }
        }

        #endregion
    }
}
