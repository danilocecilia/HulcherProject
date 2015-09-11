using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    public interface IReportMasterPageView : IBaseView
    {
        string GetPDFReportFile();

        string Username { get; }

        string Receipts { get; set; }

        string Body { get; }

        string Subject { get; }

        bool ReceiptsVisible { get; set; }
    }
}
