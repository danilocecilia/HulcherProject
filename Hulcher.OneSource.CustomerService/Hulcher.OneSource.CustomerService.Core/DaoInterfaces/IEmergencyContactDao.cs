using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;
using System.Data;

namespace Hulcher.OneSource.CustomerService.Core.DaoInterfaces
{
    public interface IEmergencyContactDao : IBaseDao<CS_EmployeeEmergencyContact, long>
    {
        void BulkCopyAllEmergencyContacts(IDataReader emergencyContactsReader);

        IList<CS_EmployeeEmergencyContact> ListAllEmployeeEmergencyContacts(long employeeID);

        void UpdateFromIntegration();
    }
}
