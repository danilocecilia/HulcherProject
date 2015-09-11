using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Base;
using System.Web.UI.WebControls;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Xml;
using Hulcher.OneSource.CustomerService.Business.WebControls.ServerControls;
using System.Web.UI;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Extender
{
    public class ValidationExtender : Extenders
    {
        public WebControl _targetControl;
        public WebControl _callerControl;
        public ValidationExtenderXml extenderXml;

        public ValidationExtender(WebControl targetControl, WebControl callerControl)
        {
            _targetControl = targetControl;
            _callerControl = callerControl;
        }

        public ValidationExtender(ValidationExtenderXml xml, WebControl targetControl, WebControl callerControl)
        {
            _targetControl = targetControl;
            _callerControl = callerControl;
            Name = xml.Name;

            extenderXml = xml;
        }

        public void DoStuff(object sender, EventArgs e)
        {
            if (_callerControl is DynamicTextBox && ((DynamicTextBox)_callerControl).Text != string.Empty)
            {
                if (_targetControl is DynamicTextBox)
                    ((DynamicTextBox)_targetControl).IsRequired = true;
                else if (_targetControl is DynamicCountableTextBox)
                    ((DynamicCountableTextBox)_targetControl).IsRequired = true;
                else if (_targetControl is DynamicDropDownList)
                    ((DynamicDropDownList)_targetControl).IsRequired = true;
            }
            else if (_callerControl is DynamicCountableTextBox && ((DynamicCountableTextBox)_callerControl).Text != string.Empty)
            {
                if (_targetControl is DynamicTextBox)
                    ((DynamicTextBox)_targetControl).IsRequired = true;
                else if (_targetControl is DynamicCountableTextBox)
                    ((DynamicCountableTextBox)_targetControl).IsRequired = true;
                else if (_targetControl is DynamicDropDownList)
                    ((DynamicDropDownList)_targetControl).IsRequired = true;
            }
            else if (_callerControl is DynamicCheckBox && ((DynamicCheckBox)_callerControl).Checked)
            {
                if (_targetControl is DynamicTextBox)
                    ((DynamicTextBox)_targetControl).IsRequired = true;
                else if (_targetControl is DynamicCountableTextBox)
                    ((DynamicCountableTextBox)_targetControl).IsRequired = true;
                else if (_targetControl is DynamicDropDownList)
                    ((DynamicDropDownList)_targetControl).IsRequired = true;
            }
            else
            {
                if (_targetControl is DynamicTextBox)
                    ((DynamicTextBox)_targetControl).IsRequired = false;
                else if (_targetControl is DynamicCountableTextBox)
                    ((DynamicCountableTextBox)_targetControl).IsRequired = false;
                else if (_targetControl is DynamicDropDownList)
                    ((DynamicDropDownList)_targetControl).IsRequired = false;
            }
            ScriptManager.GetCurrent(_callerControl.Page).SetFocus(_callerControl);
        }

        public void RegisterScript(object sender, EventArgs e)
        {
            WebControl control = sender as WebControl;

            

            string script = "if (!ValidationList)";
            script += "var ValidationList = '';";
            script += "ValidationList += '," + control.ClientID + "';";

            ScriptManager.RegisterClientScriptBlock(control, control.GetType(), "RegisterIdToValidationMethod" + control.ID, script, true);

            string teste = "";
            teste += "function ValidationMethod(RequiredClientId) { ";
            teste += "var enabled = false;";
            teste += "var list = ValidationList.substring(1).split(',');";
            teste += "for (i = 0; i < list.length; i++) {";
            teste += "if (document.getElementById(list[i]).value)";
            teste += "enabled = true;";
            teste += "}";
            teste += "ValidatorEnable($get(RequiredClientId), enabled); ";
            teste += "}";

            ScriptManager.RegisterClientScriptBlock(control, control.GetType(), "ValidationMethod", teste, true);
        }

        public override Extenders CreateObjectToSerialize()
        {
            return new ValidationExtenderXml(this._targetControl.ID, this._callerControl.ID);
        }
    }
}
