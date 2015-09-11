using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext.EntityExtensions;

namespace Hulcher.OneSource.CustomerService.DataContext
{
    public partial class CS_View_ManagersLocation : EntityObject
    {
        public string EmployeeFullName
        {
            get
            {
                StringBuilder fullNameBuilder = new StringBuilder();
                if (EmployeeName != null)
                {
                    fullNameBuilder.Append(EmployeeName.Trim());
                    fullNameBuilder.Append(", ");
                }
                if (EmployeeFirstName != null)
                {
                    fullNameBuilder.Append(EmployeeFirstName.Trim());
                }
                if (!string.IsNullOrEmpty(EmployeeNickName))
                {
                    fullNameBuilder.AppendFormat(" \"{0}\"", EmployeeNickName);
                }
                return fullNameBuilder.ToString();
            }
        }
    }
}
