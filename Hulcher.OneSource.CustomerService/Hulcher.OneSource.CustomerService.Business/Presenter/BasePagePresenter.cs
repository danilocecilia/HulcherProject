using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Core.Security;
using Hulcher.OneSource.CustomerService.Core;

using Microsoft.Practices.EnterpriseLibrary.Logging;
using Hulcher.OneSource.CustomerService.Core.Utils;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.Business.ViewModel;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    public class BasePagePresenter
    {
        /// <summary>
        /// View Interface object
        /// </summary>
        private IBasePageView _view;

        private EmployeeModel _employeeModel;

        #region [ Constructors ]

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="view">View Interface object</param>
        public BasePagePresenter(IBasePageView view)
        {
            _view = view;
        }

        #endregion

        #region [ Methods ]

        public void LoadLoggedEmployee()
        {
            using (_employeeModel = new EmployeeModel())
            {
                CS_Employee employee = _employeeModel.GetEmployeeByLogin(_view.Username, _view.Domain);

                if (null != employee)
                    _view.LoggedEmployee = employee.ID;
            }
        }

        #endregion
    }
}
