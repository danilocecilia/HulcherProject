using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;

namespace Hulcher.OneSource.CustomerService.DataContext
{
    /// <summary>
    /// Entity object for Division
    /// </summary>
    public partial class CS_JobDivision : EntityObject
    {
        public bool IsFromCustomerInfo
        {
            get;
            set;
        }
    }

    public partial class CS_Division : EntityObject
    {
        public string ExtendedDivisionName
        {
            get
            {
                string name = Name.Trim();

                string description = Description.Trim();

                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(description))
                    return name + " - " + description;
                else
                    return string.Empty;
            }
        }
    }
}
