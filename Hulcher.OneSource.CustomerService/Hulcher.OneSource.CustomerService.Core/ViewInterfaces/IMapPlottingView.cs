using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    public interface IMapPlottingView : IBaseView
    {
        #region [ Filter ]

        #region [ Multiselects ]

        /// <summary>
        /// Division Multiselect List
        /// </summary>
        IList<CS_Division> DivisionDataSource { set; get; }

        /// <summary>
        /// JobAction Multiselect List
        /// </summary>
        IList<CS_JobAction> JobActionDataSource { set; get; }

        /// <summary>
        /// JobCategory Multiselect List
        /// </summary>
        IList<CS_JobCategory> JobCategoryDataSource { set; get; }

        /// <summary>
        /// PriceType Multiselect List
        /// </summary>
        IList<CS_PriceType> PriceTypeDataSource { set; get; }

        /// <summary>
        /// EquipmentType Multiselect List
        /// </summary>
        IList<CS_EquipmentType> EquipmentTypeDataSource { set; get; }

        /// <summary>
        /// Region Multiselect List
        /// </summary>
        IList<CS_Region> RegionDataSource { set; get; }

        #endregion

        #endregion
    }
}
