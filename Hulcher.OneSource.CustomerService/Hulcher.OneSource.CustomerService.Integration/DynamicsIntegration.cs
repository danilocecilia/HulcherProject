using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Configuration;
using Hulcher.OneSource.CustomerService.Integration.Entities;

namespace Hulcher.OneSource.CustomerService.Integration
{
    public class DynamicsIntegration
    {
        /// <summary>
        /// Singleton attribute
        /// </summary>
        private static DynamicsIntegration _singleton;
        private string[] connectionStringNames;

        #region [ Singleton ]

        /// <summary>
        /// Singleton Property - Contains an instance of this Class
        /// </summary>
        public static DynamicsIntegration Singleton
        {
            get
            {
                if (null == _singleton)
                    _singleton = new DynamicsIntegration();

                return _singleton;
            }
        }

        #endregion

        #region [ Constructors ]

        private DynamicsIntegration()
        {
            connectionStringNames = ConfigurationManager.AppSettings["dynamicsConnectionStrings"].Split(';');
        }

        #endregion

        #region [ Methods ]

        public List<IDataReader> ListAllCustomers(DateTime? lastUpdate)
        {
            List<IDataReader> returnList = new List<IDataReader>();

            foreach (string connectionName in connectionStringNames)
            {
                Database db = DatabaseFactory.CreateDatabase(connectionName);

                DbCommand command = db.GetSqlStringCommand(
                    @"select * from OneSource_Customers
                        where
                         (@lastUpdate is null or LUpd_DateTime > @lastUpdate)
                        order by
                         LUpd_DateTime asc");
                db.AddInParameter(command, "@lastUpdate", DbType.String, lastUpdate.Value.ToString("yyyy-MM-dd hh:mm:ss"));

                returnList.Add(db.ExecuteReader(command));
            }

            return returnList;
        }

        public List<IDataReader> ListAllContracts(DateTime? lastUpdate)
        {
            List<IDataReader> returnList = new List<IDataReader>();

            foreach (string connectionName in connectionStringNames)
            {
                Database db = DatabaseFactory.CreateDatabase(connectionName);

                DbCommand command = db.GetSqlStringCommand(
                @"select * from OneSource_Contract
                 where
	                 (@lastUpdate is null or LUpd_DateTime > @lastUpdate)
                 order by
                         LUpd_DateTime asc");
                db.AddInParameter(command, "@lastUpdate", DbType.DateTime, lastUpdate);

                returnList.Add(db.ExecuteReader(command));
            }

            return returnList;
        }

        public List<IDataReader> ListAllContacts(DateTime? lastUpdate)
        {
            List<IDataReader> returnList = new List<IDataReader>();

            foreach (string connectionName in connectionStringNames)
            {
                Database db = DatabaseFactory.CreateDatabase(connectionName);

                DbCommand command = db.GetSqlStringCommand(
                    @"select * from OneSource_Contact
                       where
                         (@lastUpdate is null or LUpd_DateTime > @lastUpdate)
                       order by
                         LUpd_DateTime asc");
                db.AddInParameter(command, "@lastUpdate", DbType.DateTime, lastUpdate);

                returnList.Add(db.ExecuteReader(command));
            }

            return returnList;
        }

        #endregion

        #region [ Old Methods ]

//        public List<DynamicsCustomer> ListAllCustomers(DateTime? lastUpdate)
//        {
//            List<DynamicsCustomer> returnList = new List<DynamicsCustomer>();

//            foreach (string connectionName in connectionStringNames)
//            {
//                Database db = DatabaseFactory.CreateDatabase(connectionName);

//                DbCommand command = db.GetSqlStringCommand(
//                    @"select
//                         CustId, Addr1, Addr2, BillAddr1, BillAddr2, BillAttn, BillCity, BillCountry, BillFax, BillName, 
//                         BillPhone, BillSalut, BillState, BillThruProject, BillZip, City, Country, EMailAddr, 
//                         Fax, Name, Phone, State, Zip, Status, Crtd_DateTime, Crtd_User, LUpd_DateTime, LUpd_User
//                        from
//                         Customer (nolock)
//                        where
//                         (@lastUpdate is null or LUpd_DateTime > @lastUpdate) and
//                         len(ltrim(rtrim(cast(custId as varchar)))) = 6 and
//                         right(rtrim(cast(custId as varchar)), 2) = '00'
//                        order by
//                         LUpd_DateTime asc");
//                db.AddInParameter(command, "@lastUpdate", DbType.String, lastUpdate.Value.ToString("yyyy-MM-dd hh:mm:ss"));

//                IDataReader reader = db.ExecuteReader(command);
//                while (reader.Read())
//                {
//                    DynamicsCustomer customer = new DynamicsCustomer();
//                    customer.CustId = reader.GetString(reader.GetOrdinal("CustId"));
//                    customer.Addr1 = reader.GetString(reader.GetOrdinal("Addr1"));
//                    customer.Addr2 = reader.GetString(reader.GetOrdinal("Addr2"));
//                    customer.BillAddr1 = reader.GetString(reader.GetOrdinal("BillAddr1"));
//                    customer.BillAddr2 = reader.GetString(reader.GetOrdinal("BillAddr2"));
//                    customer.BillAttn = reader.GetString(reader.GetOrdinal("BillAttn"));
//                    customer.BillCity = reader.GetString(reader.GetOrdinal("BillCity"));
//                    customer.BillCountry = reader.GetString(reader.GetOrdinal("BillCountry"));
//                    customer.BillFax = reader.GetString(reader.GetOrdinal("BillFax"));
//                    customer.BillName = reader.GetString(reader.GetOrdinal("BillName"));
//                    customer.BillPhone = reader.GetString(reader.GetOrdinal("BillPhone"));
//                    customer.BillSalut = reader.GetString(reader.GetOrdinal("BillSalut"));
//                    customer.BillState = reader.GetString(reader.GetOrdinal("BillState"));
//                    customer.BillZip = reader.GetString(reader.GetOrdinal("BillZip"));
//                    customer.BillThruProject = reader.GetInt16(reader.GetOrdinal("BillThruProject"));
//                    customer.City = reader.GetString(reader.GetOrdinal("City"));
//                    customer.Country = reader.GetString(reader.GetOrdinal("Country"));
//                    customer.EMailAddr = reader.GetString(reader.GetOrdinal("EMailAddr"));
//                    customer.Fax = reader.GetString(reader.GetOrdinal("Fax"));
//                    customer.Name = reader.GetString(reader.GetOrdinal("Name"));
//                    customer.Phone = reader.GetString(reader.GetOrdinal("Phone"));
//                    customer.State = reader.GetString(reader.GetOrdinal("State"));
//                    customer.Zip = reader.GetString(reader.GetOrdinal("Zip"));
//                    customer.Status = reader.GetString(reader.GetOrdinal("Status"));
//                    customer.Crtd_DateTime = reader.GetDateTime(reader.GetOrdinal("Crtd_DateTime"));
//                    customer.Crtd_User = reader.GetString(reader.GetOrdinal("Crtd_User"));
//                    customer.LUpd_DateTime = reader.GetDateTime(reader.GetOrdinal("LUpd_DateTime"));
//                    customer.LUpd_User = reader.GetString(reader.GetOrdinal("LUpd_User"));

//                    returnList.Add(customer);
//                }

//            }

//            return returnList;
//        }

//        public List<DynamicsContract> ListAllContracts(DateTime? lastUpdate)
//        {
//            List<DynamicsContract> returnList = new List<DynamicsContract>();

//            foreach (string connectionName in connectionStringNames)
//            {
//                Database db = DatabaseFactory.CreateDatabase(connectionName);

//                DbCommand command = db.GetSqlStringCommand(
//                @"select 
//	                 contract, 
//	                 contract_desc,
//	                 Crtd_DateTime, 
//                     Crtd_User, 
//                     LUpd_DateTime, 
//                     LUpd_User,
//	                 Date_Start_Org,
//                     date_end_org, 
//	                 customer,
//	                 text_contract1,
//	                 text_contract2, 
//	                 status1 
//                 from	
//	                 PJCont (NOLOCK)
//                 where
//	                 (@lastUpdate is null or LUpd_DateTime > @lastUpdate)
//                     AND LTRIM(RTRIM(customer)) <> ''
//                 order by
//                         LUpd_DateTime asc");
//                db.AddInParameter(command, "@lastUpdate", DbType.DateTime, lastUpdate);

//                IDataReader reader = db.ExecuteReader(command);
//                while (reader.Read())
//                {
//                    DynamicsContract contract = new DynamicsContract();
//                    contract.contract = reader.GetString(reader.GetOrdinal("contract"));
//                    contract.contract_desc = reader.GetString(reader.GetOrdinal("contract_desc"));
//                    contract.Crtd_DateTime = reader.GetDateTime(reader.GetOrdinal("Crtd_DateTime"));
//                    contract.Crtd_User = reader.GetString(reader.GetOrdinal("Crtd_User"));
//                    contract.LUpd_DateTime = reader.GetDateTime(reader.GetOrdinal("LUpd_DateTime"));
//                    contract.LUpd_User = reader.GetString(reader.GetOrdinal("LUpd_User"));
//                    contract.Date_Start_Org = reader.GetDateTime(reader.GetOrdinal("Date_Start_Org"));
//                    contract.date_end_org = reader.GetDateTime(reader.GetOrdinal("date_end_org"));
//                    contract.customer = reader.GetString(reader.GetOrdinal("customer"));
//                    contract.text_contract1 = reader.GetString(reader.GetOrdinal("text_contract1"));
//                    contract.text_contract2 = reader.GetString(reader.GetOrdinal("text_contract2"));
//                    contract.status1 = reader.GetString(reader.GetOrdinal("status1"));

//                    returnList.Add(contract);
//                }

//            }

//            return returnList;
//        }

//        public List<DynamicsCustomer> ListAllContacts(DateTime? lastUpdate)
//        {
//            List<DynamicsCustomer> returnList = new List<DynamicsCustomer>();

//            foreach (string connectionName in connectionStringNames)
//            {
//                Database db = DatabaseFactory.CreateDatabase(connectionName);

//                DbCommand command = db.GetSqlStringCommand(
//                    @"select
//                         CustId, Addr1, Addr2, BillAddr1, BillAddr2, BillAttn, BillCity, BillCountry, BillFax, BillName, 
//                         BillPhone, BillSalut, BillState, BillThruProject, BillZip, City, Country, EMailAddr, 
//                         Fax, Name, Phone, State, Zip, Status, Crtd_DateTime, Crtd_User, LUpd_DateTime, LUpd_User
//                       from
//                        customer (NOLOCK)
//                       where 
//                        (@lastUpdate is null or LUpd_DateTime > @lastUpdate)
//                       AND
//                        CASE WHEN len(CustId) = 7 THEN 1 ELSE right(rtrim(cast(custId as varchar)), 2) END 
//                        <> 
//                        CASE WHEN len(CustId) = 7 THEN 0 ELSE '00' END 
//                       order by
//                         LUpd_DateTime asc");
//                db.AddInParameter(command, "@lastUpdate", DbType.DateTime, lastUpdate);

//                IDataReader reader = db.ExecuteReader(command);
//                while (reader.Read())
//                {
//                    DynamicsCustomer customer = new DynamicsCustomer();
//                    customer.CustId = reader.GetString(reader.GetOrdinal("CustId"));
//                    customer.Addr1 = reader.GetString(reader.GetOrdinal("Addr1"));
//                    customer.Addr2 = reader.GetString(reader.GetOrdinal("Addr2"));
//                    customer.BillAddr1 = reader.GetString(reader.GetOrdinal("BillAddr1"));
//                    customer.BillAddr2 = reader.GetString(reader.GetOrdinal("BillAddr2"));
//                    customer.BillAttn = reader.GetString(reader.GetOrdinal("BillAttn"));
//                    customer.BillCity = reader.GetString(reader.GetOrdinal("BillCity"));
//                    customer.BillCountry = reader.GetString(reader.GetOrdinal("BillCountry"));
//                    customer.BillFax = reader.GetString(reader.GetOrdinal("BillFax"));
//                    customer.BillName = reader.GetString(reader.GetOrdinal("BillName"));
//                    customer.BillPhone = reader.GetString(reader.GetOrdinal("BillPhone"));
//                    customer.BillSalut = reader.GetString(reader.GetOrdinal("BillSalut"));
//                    customer.BillState = reader.GetString(reader.GetOrdinal("BillState"));
//                    customer.BillZip = reader.GetString(reader.GetOrdinal("BillZip"));
//                    customer.BillThruProject = reader.GetInt16(reader.GetOrdinal("BillThruProject"));
//                    customer.City = reader.GetString(reader.GetOrdinal("City"));
//                    customer.Country = reader.GetString(reader.GetOrdinal("Country"));
//                    customer.EMailAddr = reader.GetString(reader.GetOrdinal("EMailAddr"));
//                    customer.Fax = reader.GetString(reader.GetOrdinal("Fax"));
//                    customer.Name = reader.GetString(reader.GetOrdinal("Name"));
//                    customer.Phone = reader.GetString(reader.GetOrdinal("Phone"));
//                    customer.State = reader.GetString(reader.GetOrdinal("State"));
//                    customer.Zip = reader.GetString(reader.GetOrdinal("Zip"));
//                    customer.Status = reader.GetString(reader.GetOrdinal("Status"));
//                    customer.Crtd_DateTime = reader.GetDateTime(reader.GetOrdinal("Crtd_DateTime"));
//                    customer.Crtd_User = reader.GetString(reader.GetOrdinal("Crtd_User"));
//                    customer.LUpd_DateTime = reader.GetDateTime(reader.GetOrdinal("LUpd_DateTime"));
//                    customer.LUpd_User = reader.GetString(reader.GetOrdinal("LUpd_User"));

//                    returnList.Add(customer);
//                }
//            }

//            return returnList;
//        }

        #endregion
    }
}
