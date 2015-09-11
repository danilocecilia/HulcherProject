using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hulcher.OneSource.CustomerService.Business.WebControls.ServerControls
{
    [ToolboxData("<{0}:MultiSelectDropDownList runat=\"server\" SelectionMode=\"Multiple\" CssClass=\"multiselectdropdown\" ClientIDMode=\"Static\"></{0}:MultiSelectDropDownList>")]
    public class MultiSelectDropDownList : ListBox
    {
        #region [ Properties ]

        /// <summary>
        /// List of Selected Values
        /// </summary>
        public List<string> SelectedValues
        {
            get
            {
                List<string> valuesList = new List<string>();

                for (int i = 0; i < this.Items.Count; i++)
                {
                    if (this.Items[i].Selected)
                        valuesList.Add(this.Items[i].Value);
                }

                return valuesList;
            }
            set
            {
                for (int i = 0; i < value.Count; i++)
                    if (this.Items.FindByValue(value[i]) != null)
                        this.Items.FindByValue(value[i]).Selected = true;
            }
        }

        public string OnClientClick
        {
            get
            {
                if (null == ViewState["OnClientClick"])
                    OnClientClick = string.Empty;

                return ViewState["OnClientClick"].ToString();
            }
            set
            {
                ViewState["OnClientClick"] = value;
            }
        }

        public bool IsRequired
        {
            get;
            set;
        }

        public override string ID
        {
            get
            {
                return base.ID;
            }
            set
            {
                base.ID = value;
            }
        }

        #endregion

        #region [ Overrides ]

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            StringBuilder script = new StringBuilder();

            script.Append("$(document).ready(function () {");
            script.AppendFormat("$(\'#{0}\').multiselect();", this.ID);

            if (!string.IsNullOrEmpty(OnClientClick))
            {
                script.AppendFormat("$(\"#{0}_div div.ui-multiselect-menu\").bind('click', function()", this.ID);
                script.Append(" { ");
                script.Append(OnClientClick);
                script.Append(" });");

            }

            script.Append("});");

            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            if (null == scriptManager)
            {
                ClientScriptManager cs = this.Page.ClientScript;
                cs.RegisterClientScriptBlock(GetType(), string.Format("MultiSelectDropDownList_{0}", ID), script.ToString(), true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), string.Format("MultiSelectDropDownList_{0}", ID), script.ToString(), true);
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.WriteLine(string.Format("<div id=\"{0}_div\" >", this.ID));
            //writer.WriteLine(string.Format("<input type='text' id=\"{0}_text\"  style='display: none;' >", this.ID));
            base.Render(writer);
            writer.WriteLine("</div>");
        }
        #endregion
    }
}
