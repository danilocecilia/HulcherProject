using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Base
{
    public class ListExtenderItem
    {
        private string _name;
        private string _value;
        private bool _selected;

        public ListExtenderItem()
        {
        }

        public ListExtenderItem(string itemName, string itemValue, bool isSelected)
        {
            _name = itemName;
            _value = itemValue;
            _selected = isSelected;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public bool Selected
        {
            get { return _selected; }
            set { _selected = value; }
        }
    }
}
