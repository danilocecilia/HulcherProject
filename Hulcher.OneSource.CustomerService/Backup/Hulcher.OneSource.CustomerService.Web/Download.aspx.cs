using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hulcher.OneSource.CustomerService.Web
{
    public partial class Download : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string fileName = Request.QueryString.Get("fileName");
            string path = Request.QueryString.Get("path");

            try
            {
                if (!string.IsNullOrEmpty(fileName) && !string.IsNullOrEmpty(path))
                {
                    Response.Clear();
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", string.Format("attachment; filename={0}", fileName));
                    Response.WriteFile(Server.MapPath(path));
                    Response.End();
                }
                else
                    throw new Exception("The file information for the download was not found!");
            }
            catch
            {                
                // TODO: Redirect to an error page
            }
        }
    }
}