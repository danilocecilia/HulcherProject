using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Presenter;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Web
{
    public partial class ContractDetail : System.Web.UI.Page, IContractDetailView
    {
        #region [ Attributes ]

        /// <summary>
        /// Presenter class for the Contract Detail page
        /// </summary>
        private ContractDetailPresenter _presenter;

        /// <summary>
        /// Stores Contract Identifier received via QueryString
        /// </summary>
        private int _contractId;

        /// <summary>
        /// Stores Contract details loaded via Presenter Class
        /// </summary>
        private CS_CustomerContract _contractDetails;

        /// <summary>
        /// Stores Customer Name related to the Contract
        /// </summary>
        private string _customerName;

        #endregion

        #region [ Overrides ]

        /// <summary>
        /// Overrides OnInit method to load presenter object
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _presenter = new ContractDetailPresenter(this);
        }

        #endregion

        #region [ Events ]

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString.Get("ContractId").Equals(string.Empty))
                {
                    DisplayMessage("No Contract Identifier was sent to the Details Page!", true);
                    return;
                }
                else
                {
                    _contractId = Convert.ToInt32(Request.QueryString.Get("ContractId"));
                    _presenter.LoadContract();

                    if (null == _contractDetails)
                    {
                        DisplayMessage("Contract Information was not found!", true);
                        return;
                    }
                    else
                    {
                        Title = "Contract Details - " + _contractDetails.ContractNumber;

                        lblCustomer.Text = _customerName;
                        lblNumber.Text = _contractDetails.ContractNumber;
                        lblDescription.Text = _contractDetails.Description;
                        lblAdditionalDetails.Text = _contractDetails.AdditionalDetails;
                        lblStartDate.Text = _contractDetails.StartDate.ToString("MM/dd/yyyy");
                        lblEndDate.Text = _contractDetails.EndDate.ToString("MM/dd/yyyy");
                    }
                }
            }
        }

        #endregion

        #region [ IContractDetailView Implementation ]

        /// <summary>
        /// Contract Identifier
        /// </summary>
        public int ContractId
        {
            get { return _contractId; }
        }

        /// <summary>
        /// Contract Details
        /// </summary>
        public CS_CustomerContract ContractDetails
        {
            set { _contractDetails = value; }
        }

        /// <summary>
        /// Customer Name
        /// </summary>
        public string CustomerName
        {
            set { _customerName = value; }
        }

        /// <summary>
        /// Displays a message to the client
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="closeWindow">True closes the window after the message appeares</param>
        public void DisplayMessage(string message, bool closeWindow)
        {
            Master.DisplayMessage(message, closeWindow);
        }

        #endregion
    }
}