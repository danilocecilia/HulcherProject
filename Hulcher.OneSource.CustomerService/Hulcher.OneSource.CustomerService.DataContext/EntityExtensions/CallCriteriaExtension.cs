using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;

namespace Hulcher.OneSource.CustomerService.DataContext
{
    public partial class CS_CallCriteria : EntityObject
    {
        public string Description
        {
            get
            {
                if (this.CustomerID.HasValue)
                {
                    return this.CS_Customer.FullCustomerInformation;
                }
                else if (this.DivisionID.HasValue)
                {
                    return this.CS_Division.ExtendedDivisionName;
                }
                else if (null != this.SystemWideLevel)
                {
                    return this.SystemWideLevel;
                }

                return "";
            }
        }
    }
}
