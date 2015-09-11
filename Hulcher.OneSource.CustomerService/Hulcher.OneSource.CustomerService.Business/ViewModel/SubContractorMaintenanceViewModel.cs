using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.DataContext.VO;

namespace Hulcher.OneSource.CustomerService.Business.ViewModel
{
    /// <summary>
    /// SubContractorViewModel Class
    /// </summary>
    public class SubContractorMaintenanceViewModel
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of the page view. Constains access to Page shared properties
        /// </summary>
        private ISubcontractorMaintenanceView _view;

        /// <summary>
        /// Access to first alert related DB objects
        /// </summary>
        private SubcontractorModel _subContractorModel;

        #endregion

        #region [ Constructor ]

        public SubContractorMaintenanceViewModel(ISubcontractorMaintenanceView view)
        {
            _view = view;
            _subContractorModel = new SubcontractorModel();
        }

        public SubContractorMaintenanceViewModel(ISubcontractorMaintenanceView view, SubcontractorModel SubContractorModel)
        {
            _view = view;
            _subContractorModel = SubContractorModel;
        }

        #endregion

        #region [ Methods ]

        public void EditSubcontractor()
        {
            CS_Subcontractor subContractor = _subContractorModel.GetSubcontractorById(_view.SubcontractorID.Value);

            if (null != subContractor)
            {
                _view.Name = subContractor.Name;
            }
        }
       
        #endregion
    }
}
