using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
   public interface ISubcontractorMaintenanceView: IBaseView
   {
       # region [ Listing ]

       /// <summary>
       /// Subcontractor list to bind the grid
       /// </summary>
       IList<CS_Subcontractor> SubcontractorList { set; }

       #endregion

       #region [ Form ]

       /// <summary>
       /// Indicates if the form is in edit mode
       /// </summary>
       bool EditMode { get; set; }

       /// <summary>
       /// Holds the Subcontractor Identifier
       /// </summary>
       int? SubcontractorID { get; set; }

       /// <summary>
       /// Get the subcontractor name
       /// </summary>
       string Name { get; set; }

       /// <summary>
       /// Get/Set the current user
       /// </summary>
       string UserName { get; }

       bool SavedSuccessfuly { get; set; }

       bool CreationPanelVisible { get; set; }

       bool EnableDisableCreationButton { set; }
       #endregion

       bool ReadOnly { get; set; }

       string Domain { get;  }
   }
}
