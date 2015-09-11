using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Base
{    
    [Serializable()]
    public class GridViewExtenderItem
    {
        public string[] ColumnNames { get; set; }

        public string[] RowItem { get; set; }

        public GridViewExtenderItem(string[] columnNames, string[] rowItem)
        {
            this.ColumnNames = columnNames;
            this.RowItem = rowItem;
        }

        public GridViewExtenderItem()
        {
        }
    }
}
