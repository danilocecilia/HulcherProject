using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Core.DaoInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Data
{
    public class CompetitorDao : BaseDao<CS_Competitor, int>, ICompetitorDao
    {
        #region [ Attributes ]

        /// <summary>
        /// Singleton attribute
        /// </summary>
        private static ICompetitorDao _singleton;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        private CompetitorDao() { }

        #endregion

        #region [ Singleton ]

        /// <summary>
        /// Singleton Property - Contains an instance of this Class
        /// </summary>
        public static ICompetitorDao Singleton
        {
            get
            {
                if (null == _singleton)
                    _singleton = new CompetitorDao();

                return _singleton;
            }
        }

        #endregion
    }
}
