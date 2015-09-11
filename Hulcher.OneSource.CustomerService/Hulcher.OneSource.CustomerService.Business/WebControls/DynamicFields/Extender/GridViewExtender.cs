using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Base;
using System.Web.UI.WebControls;
using Hulcher.OneSource.CustomerService.Business.WebControls.ServerControls;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Xml;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Extender
{
    public class GridViewExtender : Extenders
    {
        public WebControl _targetControl;
        public DynamicGridView _sourceControl;
        public string _label;

        public GridViewExtender(WebControl sourceControl, WebControl targetControl, string label)
        {
            _sourceControl = (DynamicGridView)sourceControl;
            _targetControl = targetControl;
            _label = label;
        }

        public override Extenders CreateObjectToSerialize()
        {
            return new GridViewExtenderXml(this._sourceControl.ID, this._targetControl.ID, this._label);
        }

        public WebControl TargetCntrol
        {
            get { return _targetControl; }
            set { _targetControl = value; }
        }
    }
}
