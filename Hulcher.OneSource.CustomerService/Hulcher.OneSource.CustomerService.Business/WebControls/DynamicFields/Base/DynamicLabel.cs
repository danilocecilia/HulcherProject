using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Base
{
    public class DynamicLabel
    {
        public string Text { get; set; }
        public string Css { get; set; }
        public string Style { get; set; }

        public DynamicLabel()
        {
            Text = string.Empty;
            Css = string.Empty;
            Style = string.Empty;
        }
    }
}
