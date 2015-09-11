using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Model;

using Microsoft.Practices.EnterpriseLibrary.Logging;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    public class ContractDetailPresenter
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of the Contract Detail Page
        /// </summary>
        private IContractDetailView _view;

        /// <summary>
        /// Instance of the Customer Business Class
        /// </summary>
        private CustomerModel _model;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="view">Instance of the ContractDetail View Interface</param>
        public ContractDetailPresenter(IContractDetailView view)
        {
            this._view = view;
            this._model = new CustomerModel();
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Loads Contract Information
        /// </summary>
        public void LoadContract()
        {
            try
            {
                CS_CustomerContract contractDetails  = _model.GetContractById(_view.ContractId);
                CS_Customer customerDetails = null;
                if (null != contractDetails)
                    customerDetails = _model.GetCustomerById(contractDetails.CustomerID);

                _view.ContractDetails = contractDetails;
                if (null != customerDetails)
                    _view.CustomerName = customerDetails.Name;
            }
            catch (Exception ex)
            {
                _view.DisplayMessage("There was an error loading Contract Information!", false);
                Logger.Write(string.Format("There was an error loading Contract Information.\n{0}", ex.ToString()));
            }
        }

        #endregion
    }
}
