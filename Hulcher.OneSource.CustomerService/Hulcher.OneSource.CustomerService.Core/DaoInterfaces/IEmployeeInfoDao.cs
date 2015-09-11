using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.DataContext;
using System.Data.Objects;
namespace Hulcher.OneSource.CustomerService.Core.DaoInterfaces
{
     /// <summary>
    /// Interface for the EmployeeInfo Dao Class
    /// </summary>
    public interface IEmployeeInfoDao
    {
        IList<CS_View_EmployeeInfo> ListAll();

        CS_View_EmployeeInfo Get(int id);

        IList<CS_View_EmployeeInfo> ListFilteredByDivision(string[] divisionName);

        IList<CS_View_EmployeeInfo> ListFilteredByDivisionState(string[] divisionStates);

        IList<CS_View_EmployeeInfo> ListFilteredByStatus(string[] status);

        IList<CS_View_EmployeeInfo> ListFilteredByJobNumber(string[] jobNumbers);

        IList<CS_View_EmployeeInfo> ListFilteredByPosition(string[] positionName);

        IList<CS_View_EmployeeInfo> ListFilteredByEmployee(string[] employeeName);
    }
}
