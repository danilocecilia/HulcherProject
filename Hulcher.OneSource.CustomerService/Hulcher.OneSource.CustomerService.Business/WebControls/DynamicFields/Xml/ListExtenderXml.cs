using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Base;
using Hulcher.OneSource.CustomerService.Core;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Xml
{
    public class ListExtenderXml : Extenders
    {
        string _targetControlName;
        Globals.CallEntry.ListType _type;
        List<ListExtenderItem> _items;
        bool _filteredLoadOnly;

        public ListExtenderXml()
        {

        }

        public ListExtenderXml(string targetControlName, Globals.CallEntry.ListType type, List<ListExtenderItem> itemList)
        {
            _targetControlName = targetControlName;
            _type = type;
            _items = itemList;
        }

        public string TargetControlName
        {
            get
            {
                return _targetControlName;
            }
            set
            {
                _targetControlName = value;
            }
        }

        public Globals.CallEntry.ListType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        public List<ListExtenderItem> Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
            }

        }

        public bool FilteredLoadOnly
        {
            get
            {
                return _filteredLoadOnly;
            }
            set
            {
                _filteredLoadOnly = value;
            }
        }
    }
}
