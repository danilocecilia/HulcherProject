using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace Hulcher.OneSource.CustomerService.Integration
{
    public class DossierIntegration
    {
        #region [ Attributes ]

        private static DossierIntegration _singleton;
        private string[] connectionStringNames;
        #endregion

        #region [ Singleton ]

        /// <summary>
        /// Singleton Property - Contains an instance of this Class
        /// </summary>
        public static DossierIntegration Singleton
        {
            get
            {
                if (null == _singleton)
                    _singleton = new DossierIntegration();

                return _singleton;
            }
        }
        #endregion

        #region [ Constructors ]

        private DossierIntegration()
        {
            connectionStringNames = ConfigurationManager.AppSettings["dossierConnectionStrings"].Split(';');
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Returns all Equipments from Dossier
        /// </summary>
        /// <returns>IDataReader that contains All Equipments</returns>
        public IDataReader ListAllEquipments()
        {
            try
            {
                //Using the first (and only) name, since Dossier only works with one Database
                Database db = DatabaseFactory.CreateDatabase(connectionStringNames[0]);
                DbCommand command = db.GetSqlStringCommand("select * from OneSource_Equipment");
                return db.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                throw new Exception("An error has ocurred while trying to get Equipment information from Dossier.", ex);
            }
        }

        /// <summary>
        /// Returns all Equipment Permit from Dossier
        /// </summary>
        /// <returns>IDataReader that contains all Equipment Permit</returns>
        public IDataReader ListAllEquipmentPermit()
        {
            try
            {
                //Using the first (and only) name, since Dossier only works with one Database
                Database db = DatabaseFactory.CreateDatabase(connectionStringNames[0]);
                DbCommand command = db.GetSqlStringCommand("select * from OneSource_EquipmentPermit");
                return db.ExecuteReader(command);
            }
            catch (Exception ex)
            {
                throw new Exception("An error has ocucurred while trying to get Equipment Permit information from Dossier.", ex);
            }
        }

        #endregion
    }
}
