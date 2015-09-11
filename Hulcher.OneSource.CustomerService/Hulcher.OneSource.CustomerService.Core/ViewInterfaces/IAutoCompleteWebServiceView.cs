using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    public interface IAutoCompleteWebServiceView
    {
        IList<CS_Customer> CustomerList { get; set; }

        IList<CS_Contact> DynamicsContactList { get; set; }

        IList<CS_Contact> CustomerServiceContactList { get; set; }

        IList<CS_JobStatus> JobStatusList { get; set; }

        IList<CS_Employee> EmployeeList { get; set; }

        IList<CS_Contact> ContactList { get; set; }

        IList<CS_Division> DivisionList { get; set; }

        IList<CS_PriceType> PriceTypeList { get; set; }

        IList<CS_JobAction> JobActionList { get; set; }

        IList<CS_City> CityList { get; set; }

        IList<CS_ZipCode> ZipCodeList { get; set; }

        IList<CS_State> StateList { get; set; }

        IList<CS_CallType> CallTypeList { get; set; }

        string PrefixText { get; set; }
        int Count { get; set; }
        string ContextKey { get; set; }

        IList<CS_Job> JobList { get; set; }

        string FilterText { get; set; }

        IList<CS_EquipmentType> EquipmentTypeList { get; set; }

        IList<CS_Equipment> EquipmentType { get; set; }

        IList<CS_Employee> ProjectManagerList { get; set; }

        IList<CS_LocalEquipmentType> LocalEquipmentTypeList { get; set; }
    }
}
