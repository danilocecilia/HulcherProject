using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Base;
using Hulcher.OneSource.CustomerService.Core;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Xml
{
    public class AutoFillExtenderXml : Extenders
    {
        string _targetControlName;
        string _conditionalControlName;
        Globals.CallEntry.AutoFillType _type;
        string _filterId;

        public AutoFillExtenderXml()
        {

        }

        public AutoFillExtenderXml(string targetControlName, Globals.CallEntry.AutoFillType type, string filterId)
        {
            _targetControlName = targetControlName;
            _type = type;
            _filterId = filterId;
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

        public string ConditionalControlName
        {
            get
            {
                return _conditionalControlName;
            }
            set
            {
                _conditionalControlName = value;
            }
        }

        public Globals.CallEntry.AutoFillType Type
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

        public string FilterId
        {
            get
            {
                return _filterId;
            }
            set
            {
                _filterId = value;
            }
        }
    }
}
