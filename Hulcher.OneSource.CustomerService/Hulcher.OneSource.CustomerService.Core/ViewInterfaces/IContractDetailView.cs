using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    /// <summary>
    /// View interface for the Contract Details Page
    /// </summary>
    public interface IContractDetailView : IBaseView
    {
        /// <summary>
        /// Gets the Contract ID
        /// </summary>
        int ContractId { get; }

        /// <summary>
        /// Sets details of a contract
        /// </summary>
        CS_CustomerContract ContractDetails { set; }

        /// <summary>
        /// Sets Customer Name
        /// </summary>
        string CustomerName { set; }
    }
}
