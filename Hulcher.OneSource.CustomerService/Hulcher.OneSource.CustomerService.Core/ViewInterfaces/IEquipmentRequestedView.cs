using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.DataContext.VO;
using System.Web.UI;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    public interface IEquipmentRequestedView : IBaseView
    {
        /// <summary>
        /// Retrieves the JobID of the Current Job
        /// </summary>
        int? JobId { get; set; }

        /// <summary>
        /// Retrieves the CallLogID of the Current Call Log
        /// </summary>
        int? CallLogId { get; set; }

        //Remove
        ///// <summary>
        ///// Sets the equipment type listing
        ///// </summary>
        //IList<CS_LocalEquipmentType> EquipmentTypeList { set; }

        /// <summary>
        /// Gets or sets the selected equipment types with quantity
        /// </summary>
        IList<LocalEquipmentTypeVO> SelectedEquipments { get; set; }
    }
}
