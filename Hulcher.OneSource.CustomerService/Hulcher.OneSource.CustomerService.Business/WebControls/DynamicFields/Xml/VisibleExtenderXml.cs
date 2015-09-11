using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Base;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Xml
{
    [Serializable()]
    public class VisibleExtenderXml : Extenders
    {
        bool _visible;
        string _callerControlName;
        string _targetControlName;
        string _value;
        string _targetValue;

        public VisibleExtenderXml()
        {

        }

        public VisibleExtenderXml(bool visible, string callerControlName, string targetControlName, string value, string targetValue)
        {
            _visible = visible;
            _callerControlName = callerControlName;
            _targetControlName = targetControlName;
            _value = value;
            _targetValue = targetValue;
        }

        public bool Visible
        {
            get
            {
                return _visible;
            }
            set
            {
                _visible = value;
            }
        }

        public string CallerControlName
        {
            get
            {
                return _callerControlName;
            }
            set
            {
                _callerControlName = value;
            }
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

        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        public string TargetValue
        {
            get
            {
                return _targetValue;
            }
            set
            {
                _targetValue = value;
            }
        }
    }
}
