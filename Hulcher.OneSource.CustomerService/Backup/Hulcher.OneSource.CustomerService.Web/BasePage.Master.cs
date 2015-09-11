using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Hulcher.OneSource.CustomerService.Core.Utils;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Presenter;

namespace Hulcher.OneSource.CustomerService.Web
{
    public partial class BasePage : System.Web.UI.MasterPage, IBasePageView
    {
        private BasePagePresenter _presenter;

        #region [ Public Properties ]

        /// <summary>
        /// Returns the logged Username
        /// </summary>
        public string Username
        {
            get
            {
                string domainUserName = Context.User.Identity.Name;
                string userName = domainUserName;
                if (domainUserName.IndexOf("\\") > -1)
                    userName = domainUserName.Split('\\')[1];
                return Server.HtmlEncode(userName);
            }
        }

        /// <summary>
        /// Returns the logged domain
        /// </summary>
        public string Domain
        {
            get
            {
                string domainUserName = Context.User.Identity.Name;
                if (domainUserName.IndexOf("\\") > -1)
                    return Server.HtmlEncode(domainUserName.Split('\\')[0]);
                else
                    return string.Empty;
            }
        }

        public int? LoggedEmployee
        {
            get
            {
                if (null == Session["LoggedEmployee"])
                    return null;

                return Convert.ToInt32(Session["LoggedEmployee"]);
            }
            set
            {
                Session["LoggedEmployee"] = value;
            }
        }

        #endregion

        #region [ Overrides ]

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            _presenter = new BasePagePresenter(this);
        }

        #endregion

        #region [ Events ]

        protected void Page_Load(object sender, EventArgs e)
        {
            //ScriptManager.RegisterClientScriptInclude(this, this.GetType(), "jquery1.4.4", "/Scripts/jquery-1.6.2.min.js");
            ScriptManager.RegisterClientScriptInclude(this, this.GetType(), "jquery1.4.4", "/Scripts/jquery-1.4.4.js");
            ScriptManager.RegisterClientScriptInclude(this, this.GetType(), "sort", "/Scripts/jquery.tablesorter.js");
            ScriptManager.RegisterClientScriptInclude(this, this.GetType(), "jquery-ui", "/Scripts/jquery-ui-1.8.7.custom.js");
            ScriptManager.RegisterClientScriptInclude(this, this.GetType(), "core", "/Scripts/jquery.ui.core.js");
            ScriptManager.RegisterClientScriptInclude(this, this.GetType(), "widget", "/Scripts/jquery.ui.widget.js");
            ScriptManager.RegisterClientScriptInclude(this, this.GetType(), "ui", "/Scripts/jquery.ui.datepicker.js");
            ScriptManager.RegisterClientScriptInclude(this, this.GetType(), "scroll", "/Scripts/webtoolkit.jscrollable.js");
            ScriptManager.RegisterClientScriptInclude(this, this.GetType(), "scrolltable", "/Scripts/webtoolkit.scrollabletable.js");
            ScriptManager.RegisterClientScriptInclude(this, this.GetType(), "autonumeric", "/Scripts/autoNumeric-1.6.2.js");
            ScriptManager.RegisterClientScriptInclude(this, this.GetType(), "autoloader", "/Scripts/autoLoader.js");
            ScriptManager.RegisterClientScriptInclude(this, this.GetType(), "metadata", "/Scripts/jquery.metadata.js");
            ScriptManager.RegisterClientScriptInclude(this, this.GetType(), "easycounter", "/Scripts/jquery.jqEasyCharCounter.js");
            ScriptManager.RegisterClientScriptInclude(this, this.GetType(), "jquery.jgrowl", "/Scripts/jquery.jgrowl_minimized.js");
            ScriptManager.RegisterClientScriptInclude(this, this.GetType(), "formatCurrency", "/Scripts/jquery.price_format.1.5");

            if (!IsPostBack)
                _presenter.LoadLoggedEmployee();
        }

        #endregion

        #region [ Methods ]

        public void DisplayMessage(string message, bool closeWindow)
        {
            if (false == closeWindow)
            {
                ScriptManager.RegisterClientScriptBlock(
                    Page,
                    this.GetType(),
                    "displayMessage",
                    string.Format("alert('{0}');", message.Replace("\n", "\\n")),
                    true);
            }
            else
            {

                ScriptManager.RegisterClientScriptBlock(
                    Page,
                    this.GetType(),
                    "displayMessage",
                    string.Format("alert('{0}'); window.close();", message.Replace("\n", "\\n")),
                    true);
            }
        }

        #endregion
    }
}