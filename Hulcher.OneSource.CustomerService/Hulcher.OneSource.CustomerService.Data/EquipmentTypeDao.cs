using System;
using System.Data.Objects;
using System.Linq;
using Hulcher.OneSource.CustomerService.Core.DaoInterfaces;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Data
{
    public class EquipmentTypeDao : BaseDao<CS_EquipmentType, long>, IEquipmentTypeDao
    {
        #region [ Attributes ]

        /// <summary>
        /// Singleton attribute
        /// </summary>
        private static IEquipmentTypeDao _singleton;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        public EquipmentTypeDao() { }

        #endregion

        #region [ Singleton ]

        /// <summary>
        /// Singleton Property - Contains an instance of this Class
        /// </summary>
        public static IEquipmentTypeDao Singleton
        {
            get { return _singleton ?? (_singleton = new EquipmentTypeDao()); }
        }

        #endregion

        #region [ IEquipmentTypeDao Implementation ]

        /// <summary>
        /// Clear table (for Unit Testing)
        /// </summary>
        public void ClearAll()
        {
            using (var db = new CustomerServiceModelContainer())
            {
                ExecuteSql(db, "delete from CS_EquipmentType");
            }
        }

        #endregion
    }
}
