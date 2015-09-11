using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.ServerControls
{
    [ToolboxData("<{0}:CountableTextBox runat=server></{0}:CountableTextBox>")]
    public class CountableTextBox : TextBox
    {
        private int _MaxChars;
        public int MaxChars
        {
            get
            {
                return _MaxChars;
            }
            set
            {
                _MaxChars = value;
            }
        }

        private int _MaxCharsWarning;
        public int MaxCharsWarning
        {
            get
            {
                return _MaxCharsWarning;
            }
            set
            {
                _MaxCharsWarning = value;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            StringBuilder script = new StringBuilder();
            script.Append("$(document).ready(function () {\n");
            script.Append("     $('#" + ClientID + "').jqEasyCounter({\n");
            script.Append("         'maxChars': " + _MaxChars.ToString() + ",\n");
            script.Append("         'maxCharsWarning': " +_MaxCharsWarning.ToString() + "\n");
            script.Append("     });\n");
            script.Append("});\n");

            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            if (null == scriptManager)
            {
                ClientScriptManager cs = this.Page.ClientScript;
                cs.RegisterStartupScript(GetType(), string.Format("CountableTextBox_{0}", ID), script.ToString(), true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), string.Format("CountableTextBox_{0}", ID), script.ToString(), true);
            }
        }
    }
}
