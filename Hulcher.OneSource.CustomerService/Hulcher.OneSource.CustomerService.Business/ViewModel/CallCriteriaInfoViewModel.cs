using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.DataContext.VO;
using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Business.ViewModel
{
    public class CallCriteriaInfoViewModel : IDisposable
    {
        #region [ Attributes ]
        /// <summary>
        /// Instance of the CallCriteriaInfo View Interface
        /// </summary>
        private ICallCriteriaInfoView _view;

        /// <summary>
        /// Instance of the CallCriteria model class
        /// </summary>
        private CallCriteriaModel _callCriteriaModel;

        #endregion

        #region [ Constructors ]

        public CallCriteriaInfoViewModel(ICallCriteriaInfoView view)
        {
            _view = view;

            _callCriteriaModel = new CallCriteriaModel();
        }

        #endregion

        #region [ Methods ]

        #region [ Save CallCriteria ]

        public void AddCallCriteria()
        {
            CS_CallCriteria saveCriteria = _view.CallCriteriaEntity;
            IList<CS_CallCriteriaValue> saveCriteriaValueList = _view.CallCriteriaValueEntityList;

            //Save Information
            _callCriteriaModel.SaveCallCriteria(saveCriteria, saveCriteriaValueList, _view.Username);
            SetCallCriteriaPanels(false, false, false);
            _view.CallCriteriaGroup(true);
        }
        #endregion

        #region [ Loading Call Criteria ]
        public void LoadCallCriteria()
        {
            CS_CallCriteria selectedCallCriteria = null;
            bool toSearch = _view.CallCriteriaList != null;


            if (_view.EditingDivisionID == 0 &&
                _view.EditingCustomerID == 0 &&
                string.IsNullOrEmpty(_view.EditingWideName))
            {
                _view.ClearFields();
                _view.AddCallCriteriaEnabled = false;
                SetCallCriteriaPanels(false, false, false);
                _view.CallCriteriaGroup(true);
                return;
            }

            _view.AddCallCriteriaEnabled = true;

            switch (_view.SelectedLevel)
            {
                case Hulcher.OneSource.CustomerService.Core.Globals.CallCriteria.CallCriteriaLevel.Division:
                    if (toSearch)
                        selectedCallCriteria = _view.CallCriteriaList.FirstOrDefault(e => e.DivisionID == _view.EditingDivisionID);
                    SetCallCriteriaPanels(false, true, true);
                    _view.CallCriteriaGroup(false);
                    break;
                case Hulcher.OneSource.CustomerService.Core.Globals.CallCriteria.CallCriteriaLevel.Customer:
                    if (toSearch)
                        selectedCallCriteria = _view.CallCriteriaList.FirstOrDefault(e => e.CustomerID == _view.EditingCustomerID);
                    SetCallCriteriaPanels(true, false, true);
                    _view.CallCriteriaGroup(false);
                    break;
                case Hulcher.OneSource.CustomerService.Core.Globals.CallCriteria.CallCriteriaLevel.Wide:
                    if (toSearch)
                        selectedCallCriteria = _view.CallCriteriaList.FirstOrDefault(e => e.SystemWideLevel.ToLower().Trim() == _view.EditingWideName.ToString().ToLower().Trim());
                    SetCallCriteriaPanels(true, true, true);
                    _view.CallCriteriaGroup(false);
                    break;
            }

            if (selectedCallCriteria != null)
            {
                _view.ClearFields();
                _view.CallCriteriaEntity = selectedCallCriteria;
                _view.CallCriteriaValueEntityList = selectedCallCriteria.CS_CallCriteriaValue.Where(e => e.Active).ToList();
            }
        }

        public void SetCallCriteriaPanels(bool division, bool customer, bool systemWide)
        {
            _view.PanelDivision = division;
            _view.PanelCustomer = customer;
            _view.PanelCallCriteria = systemWide;
        }
        #endregion

        #region [ Call Criteria Listing ]

        public void FillCallCriteriaListingRow()
        {
            if (_view.CallCriteriaRepeaterDataItem != null)
            {
                _view.CallCriteriaRowId = _view.CallCriteriaRepeaterDataItem.ID;
                _view.CallCriteriaRowDescription = _view.CallCriteriaRepeaterDataItem.Description;
                _view.CallCriteriaRowAdviseNotes = _view.CallCriteriaRepeaterDataItem.Notes;

                // TODO: Bind Job Attributes Repeater
                _view.JobAttributesRepeaterDataSource = _callCriteriaModel.FindCallCriteriaValueByID(_view.CallCriteriaRepeaterDataItem.ID);

                // TODO: Bind Job Call Log Conditions Repeater
                _view.JobCallLogConditionsRepeaterDataSource = _callCriteriaModel.FindCallCriteriaCallLogValueByID(_view.CallCriteriaRepeaterDataItem.ID);
            }
        }

        public void FillJobAttributesRow()
        {
            if (null != _view.JobAttributesRepeaterDataItem)
            {
                _view.JobAttributesRowCriteira = _view.JobAttributesRepeaterDataItem.Criteria;
                _view.JobAttributesRowSelected = _view.JobAttributesRepeaterDataItem.Data;
            }
        }

        public void FillJobCallLogConditionsRow()
        {
            if (null != _view.JobCallLogConditionsRepeaterDataItem)
            {
                _view.JobCallLogConditionsRowCriteira = _view.JobCallLogConditionsRepeaterDataItem.Criteria;
                _view.JobCallLogConditionsRowSelected = _view.JobCallLogConditionsRepeaterDataItem.Data;
            }
        }

        public void DeleteCallCriteria()
        {
            _callCriteriaModel.DeleteCallCriteria(_view.CallCriteiraRowCommandArgument, _view.Username);
        }

        public void Cancel()
        {
            _view.ClearFields();
            _view.AddCallCriteriaEnabled = false;
            SetCallCriteriaPanels(false, false, false);
            _view.CallCriteriaGroup(true);
        }

        public void CallCriteriaListingRowCommand()
        {
            CS_CallCriteria callCriteria = _view.CallCriteriaList.FirstOrDefault(e => e.ID == _view.CallCriteiraRowCommandArgument);
            _view.CallCriteriaEntity = callCriteria;

            _view.CallCriteriaValueEntityList = callCriteria.CS_CallCriteriaValue.Where(e => e.Active).ToList();
            SetCallCriteriaPanels(!callCriteria.DivisionID.HasValue, !callCriteria.CustomerID.HasValue, true);
            _view.AddCallCriteriaEnabled = true;
            _view.CallCriteriaGroup(false);
        }

        #endregion

        #endregion

        #region [ IDisposable Implementation ]

        public void Dispose()
        {
            if (_callCriteriaModel != null)
                _callCriteriaModel.Dispose();

            _callCriteriaModel = null;
        }
        #endregion
    }
}
