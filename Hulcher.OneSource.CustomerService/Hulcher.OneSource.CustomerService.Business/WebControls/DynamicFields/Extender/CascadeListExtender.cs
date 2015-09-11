using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Base;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Xml;
using Hulcher.OneSource.CustomerService.Business.WebControls.ServerControls;
using System.Web.UI;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Extender
{
    [Serializable]
    public class CascadeListExtender : Extenders
    {
        public WebControl _targetControl;
        public WebControl _sourceControl;
        public CascadeListExtenderXml extenderXml;

        public CascadeListExtender(WebControl targetControl, WebControl sourceControl)
        {
            _targetControl = targetControl;
            _sourceControl = sourceControl;
        }

        public CascadeListExtender(CascadeListExtenderXml xml, WebControl targetControl, WebControl sourceControl)
        {
            _targetControl = targetControl;
            _sourceControl = sourceControl;
            Name = xml.Name;

            extenderXml = xml;
        }

        public void DoStuff(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(((DynamicDropDownList)_sourceControl).SelectedValue))
            {
                if (((DynamicDropDownList)_targetControl).FilterId != Convert.ToInt32(((DynamicDropDownList)_sourceControl).SelectedValue))
                {
                    ((DynamicDropDownList)_targetControl).FilterId = Convert.ToInt32(((DynamicDropDownList)_sourceControl).SelectedValue);
                    if (null != ((DynamicDropDownList)_targetControl).ListExtender)
                        ((DynamicDropDownList)_targetControl).ListExtender.DoStuff(_sourceControl, new EventArgs());
                    if (null != ((DynamicDropDownList)_targetControl)._cascadeExtender)
                        ((DynamicDropDownList)((DynamicDropDownList)_targetControl)._cascadeExtender._targetControl).Clear();

                }
            }
            if (null != _targetControl.Page)
                ScriptManager.GetCurrent(_targetControl.Page).SetFocus(_targetControl);
        }

        public override Extenders CreateObjectToSerialize()
        {
            return new CascadeListExtenderXml(this._targetControl.ID, this._sourceControl.ID, ((DynamicDropDownList)_targetControl).ListExtender.Name);
        }
    }
}
