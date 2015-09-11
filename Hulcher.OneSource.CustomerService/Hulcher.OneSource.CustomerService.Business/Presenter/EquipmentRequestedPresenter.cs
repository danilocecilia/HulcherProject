using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Business.Model;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    public class EquipmentRequestedPresenter
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of the View Interface
        /// </summary>
        private IEquipmentRequestedView _view;

        /// <summary>
        /// Instance of the Equipment Model class
        /// </summary>
        private EquipmentModel _equipmentModel;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="view">Instance of the EquipmentRequested View Interface</param>
        public EquipmentRequestedPresenter(IEquipmentRequestedView view)
        {
            this._view = view;
            this._equipmentModel = new EquipmentModel();
        }

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="view">Instance of the EquipmentRequested View Interface</param>
        /// <param name="equipmentModel">Instance of the Equipment Model class</param>
        public EquipmentRequestedPresenter(IEquipmentRequestedView view, EquipmentModel equipmentModel)
        {
            this._view = view;
            this._equipmentModel = equipmentModel;
        }

        #endregion

        #region [ Methods ]

        //Remove
        ///// <summary>
        ///// Loads the control
        ///// </summary>
        //public void Load()
        //{
        //    try
        //    {
        //        _view.EquipmentTypeList = _equipmentModel.ListAllLocalEquipmentType();
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Write(string.Format("An error has ocurred while trying to load the Equipment Types!\n{0}\n{1}", ex.Message, ex.StackTrace));
        //        _view.DisplayMessage("An error ocurred while trying to load the Equipment Types.", false);
        //    }
        //}

        #endregion
    }
}
