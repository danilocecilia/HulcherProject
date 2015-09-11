using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;

namespace Hulcher.OneSource.CustomerService.DataContext
{
    public partial class CS_ZipCode : EntityObject
    {
        public string ZipCodeNameEdited
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Name))
                {
                    string ExtendedName = this.Name;

                    if (ExtendedName.Length == 4 || ExtendedName.Length == 3)
                        return ExtendedName.PadLeft(5, '0');
                    else
                        return this.Name;
                }
                return string.Empty;
            }
        }
    }
}
