using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core.DaoInterfaces;
using System.Data.Objects;

namespace Hulcher.OneSource.CustomerService.Data
{
    public class SpecialPricingDao : BaseDao<CS_SpecialPricing, int>, ISpecialPricingDao
    {
        #region [ Attributes ]

        /// <summary>
        /// Singleton attribute
        /// </summary>
        private static ISpecialPricingDao _singleton;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        public SpecialPricingDao() { }

        #endregion

        #region [ Singleton ]

        /// <summary>
        /// Singleton Property - Contains an instance of this Class
        /// </summary>
        public static ISpecialPricingDao Singleton
        {
            get
            {
                if (null == _singleton)
                    _singleton = new SpecialPricingDao();

                return _singleton;
            }
        }

        #endregion
    }
}
