using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    public interface IProjectCalendarPrintView: IBaseProjectCalendarView
    {
        string PrintDivisionFilter { get; set; }

        string PrintEquipmentTypeFilter { get; set; }

        string PrintEquipmentFilter { get; set; }

        string PrintEmployeeFilter { get; set; }

        string PrintCustomerFilter { get; set; }

        string PrintJobFilter { get; set; }

        string PrintJobActionFilter { get; set; }

        string PrintStartDateFilter { get; set; }

        string PrintEndDateFilter { get; set; }

        IList<string> CalendarSource { set; }

        void AddCalendarSource(string value);
    }
}
