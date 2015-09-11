using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Base;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Xml
{
    public class CascadeListExtenderXml : Extenders
    {
        string _targetControlName;
        string _sourceControlName;
        string _listExtenderName;

        public CascadeListExtenderXml()
        {

        }

        public CascadeListExtenderXml(string targetControlName, string sourceControlName, string listExtenderName)
        {
            _targetControlName = targetControlName;
            _sourceControlName = sourceControlName;
            _listExtenderName = listExtenderName;
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

        public string SourceControlName
        {
            get
            {
                return _sourceControlName;
            }
            set
            {
                _sourceControlName = value;
            }
        }

        public string ListExtenderName
        {
            get
            {
                return _listExtenderName;
            }
            set
            {
                _listExtenderName = value;
            }
        }
    }
}
