using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Base;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Xml
{
    [Serializable()]
    public class GridViewExtenderXml : Extenders
    {
        #region [ Attributes ]

        string _sourceControlName;
        string _targetControlName;
        string _label;

        #endregion

        #region [ Properties ]

        public string SourceControlName
        {
            get { return _sourceControlName; }
            set { _sourceControlName = value; }
        }

        public string TargetControlName
        {
            get { return _targetControlName; }
            set { _targetControlName = value; }
        }

        public string Label
        {
            get { return _label; }
            set { _label = value; }
        }

        #endregion

        #region [ Constructors ]

        public GridViewExtenderXml()
        {
        }

        public GridViewExtenderXml(string sourceControlName, string targetControlName, string label)
        {
            _sourceControlName = sourceControlName;
            _targetControlName = targetControlName;
            _label = label;
        }

        #endregion
    }
}
