using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    public interface IProjectCalendarView : IBaseView, IBaseProjectCalendarView
    {
        //#region  [ Filter ]

        //List<DateTime> CalendarDateRange { get; }

        //int? DivisionFilter { get; }

        //int? EquipmentTypeFilter { get; }

        //int? EquipmentFilter { get; }

        //int? EmployeeFilter { get; }

        //int? CustomerFilter { get; }

        //int? JobFilter { get; }

        //int? JobActionFilter { get; }

        //DateTime StartDateValue { get; set; }

        //DateTime EndDateValue { get; set; }

        //#endregion

        //#region [ CalendarView ]

        //string CalendarSource { set; }

        //#endregion
        string SetPrintButtonClientClick { set; }        

        #region [ Calendar Grid ]
        void ShowHidePanelNowRow(bool show);
        #endregion
    }
}
