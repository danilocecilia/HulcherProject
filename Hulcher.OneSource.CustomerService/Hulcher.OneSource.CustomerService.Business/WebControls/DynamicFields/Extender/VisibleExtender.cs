using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Base;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Xml;
using System.Web.UI.WebControls;
using Hulcher.OneSource.CustomerService.Business.WebControls.ServerControls;
using System.Web.UI;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Extender
{
    [Serializable()]
    public class VisibleExtender : Extenders
    {
        public bool visible;
        [NonSerialized(), XmlIgnore()]

        public WebControl callerControl;
        [NonSerialized(), XmlIgnore()]
        public WebControl targetControl;
        public string value;
        public string targetValue;

        public VisibleExtender()
        {

        }

        public VisibleExtender(VisibleExtenderXml xml, WebControl _callerControl, WebControl _targetControl)
        {
            visible = xml.Visible;
            value = xml.Value;
            targetValue = xml.TargetValue;

            callerControl = _callerControl;
            targetControl = _targetControl;
        }

        public VisibleExtender(bool _visible, WebControl _caller, WebControl _targetControl, string _targetValue)
        {
            visible = _visible;
            callerControl = _caller;
            targetControl = _targetControl;
            targetValue = _targetValue;
        }

        public void DoStuff(object sender, EventArgs e)
        {
            if (callerControl is DynamicTextBox && targetValue == ((DynamicTextBox)callerControl).Text)
            {
                targetControl.Visible = visible;
            }
            else if (callerControl is DynamicCountableTextBox && targetValue == ((DynamicCountableTextBox)callerControl).Text)
            {
                targetControl.Visible = visible;
            }
            else if (callerControl is DynamicDropDownList && targetValue == ((DynamicDropDownList)callerControl).SelectedValue)
            {
                targetControl.Visible = visible;
            }
            else if(callerControl is DynamicCheckBox && targetValue == ((DynamicCheckBox)callerControl).Checked.ToString().ToLower())
            {
                targetControl.Visible = visible;
            }
            else if (callerControl is DynamicRadioButtonList && targetValue == ((DynamicRadioButtonList)callerControl).SelectedValue.ToString())
            {
                targetControl.Visible = visible;
            }

            if (null != callerControl.Page)
                ScriptManager.GetCurrent(callerControl.Page).SetFocus(callerControl);
        }

        public override Extenders CreateObjectToSerialize()
        {
            return new VisibleExtenderXml(this.visible, this.callerControl.ID, this.targetControl.ID, this.value, this.targetValue);
        }

    }
}
