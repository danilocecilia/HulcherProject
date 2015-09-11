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
    public class EquipmentDao : BaseDao<CS_Equipment, long>, IEquipmentDao
    {
        #region [ Attributes ]

        /// <summary>
        /// Singleton attribute
        /// </summary>
        private static IEquipmentDao _singleton;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        private EquipmentDao() { }

        #endregion

        #region [ Singleton ]

        /// <summary>
        /// Singleton Property - Contains an instance of this Class
        /// </summary>
        public static IEquipmentDao Singleton
        {
            get
            {
                if (null == _singleton)
                    _singleton = new EquipmentDao();

                return _singleton;
            }
        }

        #endregion

        #region [ IEquipmentDao Implementation ]

        public void ClearAll()
        {
            using (var db = new CustomerServiceModelContainer())
            {
                ExecuteSql(db, "delete from CS_Equipment");
            }
        }
       
        #endregion
    }
}
