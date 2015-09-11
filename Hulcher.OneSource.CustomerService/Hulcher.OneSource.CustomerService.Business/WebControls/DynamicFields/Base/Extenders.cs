using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Base
{
    public class Extenders
    {
        public string Name { get; set; }

        public virtual Extenders CreateObjectToSerialize()
        {
            return this;
        }
    }
}
