using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Pex.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using System.Web.UI.WebControls;

namespace Hulcher.OneSource.CustomerService.Test
{
    [TestClass, PexClass]
    public partial class JobCallLogViewFake : IJobCallLogView
    {
        GridViewRow row;

        public int? JobId
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IList<DataContext.CS_CallLog> DataSource
        {
            set { throw new NotImplementedException(); }
        }

        public Core.Globals.JobRecord.FilterType FilterType
        {
            get { throw new NotImplementedException(); }
        }

        public string FilterValue
        {
            get { throw new NotImplementedException(); }
        }

        public void SetCallLogGridViewHyperLinkVisibility(bool visible, string controlName)
        {            
            if (row != null)
            {
                HyperLink lb = (HyperLink)row.FindControl(controlName);
                lb.Visible = visible;
            }
        }

        public System.Web.UI.WebControls.GridViewRow CallLogGridViewRow
        {
            get
            {
                row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
                TableCell cell = new TableCell();
                cell.Controls.Add(new HyperLink() { ID = "hlUpdate", Visible = true });
                cell.Controls.Add(new HyperLink() { ID = "hlDelete", Visible = true });
                row.Controls.Add(cell);
                return row;
            }
            set
            {
                row = value;
            }
        }

        public void DisplayMessage(string message, bool closeWindow)
        {
            throw new NotImplementedException();
        }


        public bool GetCallLogGridViewHyperLinkVisibility(string controlName)
        {            
            HyperLink lb = (HyperLink)row.FindControl(controlName);
            return lb.Visible;
        }
    }
}
