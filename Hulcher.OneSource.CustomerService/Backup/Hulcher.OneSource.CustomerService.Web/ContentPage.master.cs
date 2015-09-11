using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hulcher.OneSource.CustomerService.Web
{
    public partial class ContentPage : System.Web.UI.MasterPage
    {
        #region [ Public Properties ]

        public string Username
        {
            get { return Master.Username; }
        }

        public string Domain
        {
            get { return Master.Domain; }
        }

        public int? LoggedEmployee
        {
            get
            {
                return Master.LoggedEmployee;
            }
        }

        #endregion

        #region [ Events ]

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #endregion

        #region [ Methods ]

        public void DisplayMessage(string message, bool closeWindow)
        {
            Master.DisplayMessage(message, closeWindow);
        }

        #endregion
    }
}