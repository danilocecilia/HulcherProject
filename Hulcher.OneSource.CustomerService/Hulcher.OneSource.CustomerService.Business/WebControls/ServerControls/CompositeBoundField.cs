using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.ServerControls
{
    public class CompositeBoundField:BoundField
    {
        protected override object GetValue(System.Web.UI.Control controlContainer)
        {
            object item = DataBinder.GetDataItem(controlContainer);
            return DataBinder.Eval(item, this.DataField);
        }
    }
}
