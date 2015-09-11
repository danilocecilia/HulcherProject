using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;

namespace Hulcher.OneSource.CustomerService.DataContext
{
    /// <summary>
    /// Entity object for State
    /// </summary>
    public partial class CS_State : EntityObject
    {
        public string AcronymName
        {
            get
            {
                return string.Format("{0} - {1}", Acronym, Name);
            }
        }

        public string CountryAcronymName
        {
            get
            {
                if (null != CS_Country)
                    return string.Format("{0} - {1} - {2}", CS_Country.Name, Acronym.Trim(), Name.Trim());
                else
                    return AcronymName;
            }
        }
    }
}
