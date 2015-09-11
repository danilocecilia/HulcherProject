using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    /// <summary>
    /// View Interface for the CustomerInfo Section of the Job Record
    /// </summary>
    public interface ICustomerInfoView : IBaseView
    {
        /// <summary>
        /// Sets the Division List to the DropDown on the WebPage
        /// </summary>
        IList<CS_Division> DivisionList { set; }

        /// <summary>
        /// Sets the Customer Contact List to the DropDown on the WebPage
        /// </summary>
        long CustomerValue { get; set; }

        /// <summary>
        /// Sets the Customer Contact List to the DropDown on the WebPage
        /// </summary>
        long DivisionValue { get; set; }

        /// <summary>
        /// Sets the Validation Group of all the validators inside the page
        /// </summary>
        string ValidationGroup { set; }

        /// <summary>
        /// Get the CustomerInfo Entity object populated from the screen
        /// </summary>
        CS_CustomerInfo CustomerInfoEntity { get; }

        /// <summary>
        /// Get and Set the JobId
        /// </summary>
        int? JobId { get; set; }

        /// <summary>
        /// Job Id when cloning a job record
        /// </summary>
        int? CloningId { get; set; }

        /// <summary>
        /// Indicates if the user has only read access to this page
        /// </summary>
        bool ReadOnlyAccess { get; set; }

        /// <summary>
        /// Get employeeId
        /// </summary>
        int EmployeeId { get; }

        /// <summary>
        /// Validates required fields for customer info section
        /// </summary>
        void RequiredFieldValidatorActCustomer();

        /// <summary>
        /// Set the values to the CS_View_GetJobData
        /// </summary>
        CS_View_GetJobData CustomerInfoLoad { get; set; }
    }
}
