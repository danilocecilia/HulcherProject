using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Base;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Xml
{
    public class ValidationExtenderXml : Extenders
    {
        string _targetControlName;
        string _callerControlName;

        public ValidationExtenderXml()
        {

        }

        public ValidationExtenderXml(string targetControlName, string callerControlName)
        {
            _targetControlName = targetControlName;
            _callerControlName = callerControlName;
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
    }
}
