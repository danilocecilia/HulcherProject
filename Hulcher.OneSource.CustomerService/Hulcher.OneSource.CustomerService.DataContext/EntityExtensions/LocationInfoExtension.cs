using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;

namespace Hulcher.OneSource.CustomerService.DataContext
{
    public partial class CS_LocationInfo : EntityObject
    {
        public string FullLocation
        {
            get
            {
                if (this.CS_State != null && this.CS_City != null && this.CS_Country != null)
                {
                    return string.Format("{0} - {1} - {2}", this.CS_City.ExtendedName.Trim(), this.CS_State.Name.Trim(), this.CS_Country.Name);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string ZipCodeNameEdited
        {
            get
            {
                if (this.CS_ZipCode != null)
                {
                    string ExtendedName = this.CS_ZipCode.Name;

                    if (!string.IsNullOrEmpty(ExtendedName))
                    {
                        if (ExtendedName.Length == 3 || ExtendedName.Length == 4)
                            return ExtendedName.PadLeft(5, '0');
                        else
                            return this.CS_ZipCode.Name;
                    }
                }
                return string.Empty;
            }
        }
    }
}
