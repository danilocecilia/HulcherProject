using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    public interface IBasePageView : IBaseView
    {
        /// <summary>
        /// Property that holds the Employee Id that is currently logged in
        /// </summary>
        int? LoggedEmployee { get; set; }

        string Domain { get; }

        string Username { get; }
    }
}
