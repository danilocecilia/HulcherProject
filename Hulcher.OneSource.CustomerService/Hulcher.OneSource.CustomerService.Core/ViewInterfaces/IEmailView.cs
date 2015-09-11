using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.DataContext.VO;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    public interface IEmailView : IBaseView
    {
        List<int> CallLogListId { get; set; }

        string EmailBody { get; set; }

        string EmailSubject { get; set; }

        IList<CS_CallLogCallCriteriaEmail> Receipts { get; set; }

        string UserName { get; set; }

        int? CreationId { get; set; }

        string ReceiptsString { get; }

        List<int> ReceiptsIds { get; }

        int? JobID { get; set;  }

        IList<EmailVO> EmailVOReceipts { set; }
    }
}
