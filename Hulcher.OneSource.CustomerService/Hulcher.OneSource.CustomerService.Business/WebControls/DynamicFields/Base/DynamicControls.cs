using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Base
{
    public class DynamicControls
    {
        public string Name { get; set; }
        public DynamicLabel Label { get; set; }
        public string Css { get; set; }
        public string Style { get; set; }
        public bool Visible { get; set; }
        public int CustomerSpecificInfoTypeID { get; set; }

        public DynamicControls()
        {
            Name = string.Empty;
            Label = new DynamicLabel();
            Css = string.Empty;
            Style = string.Empty;
            Visible = true;
            CustomerSpecificInfoTypeID = 0;
        }
    }
}
