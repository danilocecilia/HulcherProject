using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Presenter;

using Microsoft.Reporting.WebForms;

namespace Hulcher.OneSource.CustomerService.Web
{
    public partial class ReportMasterPage : System.Web.UI.MasterPage, IReportMasterPageView
    {
        #region [ Attributes ]

        private ReportMasterPagePresenter _presenter;

        #endregion

        #region [ Events ]

        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter = new ReportMasterPagePresenter(this);
        }

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            _presenter.SendEmail();
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Get or Sets the ReportViewer Control
        /// </summary>
        public ReportViewer ReportViewer
        {
            get
            {
                return rvwViewer;
            }
            set
            {
                rvwViewer = value;
            }
        }

        public string Username
        {
            get { return Master.Username; }
        }

        public string Receipts
        {
            get { return txtEmailTo.Text; }
            set { txtEmailTo.Text = value; }
        }

        public string Subject
        {
            get { return txtEmailSubject.Text; }
        }

        public string Body
        {
            get { return txtEmailBody.Text; }
        }

        public bool ReceiptsVisible
        {
            get { return divReceipts.Visible; }
            set { divReceipts.Visible = value; }
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Display Message
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="closeWindow">closeWindow</param>
        public void DisplayMessage(string message, bool closeWindow)
        {
            Master.DisplayMessage(message, closeWindow);
        }

        /// <summary>
        /// Generates the PDF file and returns its path
        /// </summary>
        /// <returns></returns>
        public string GetPDFReportFile()
        {
            string pdfReportFile = System.IO.Path.GetTempPath();
            pdfReportFile += "Report" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";

            byte[] pdfBytes = rvwViewer.LocalReport.Render("PDF");
            using (System.IO.FileStream stream = new System.IO.FileStream(pdfReportFile, System.IO.FileMode.Create, System.IO.FileAccess.Write))
            {
                stream.Write(pdfBytes, 0, pdfBytes.Length);
                stream.Close();
            }

            return pdfReportFile;
        }

        #endregion
    }
}