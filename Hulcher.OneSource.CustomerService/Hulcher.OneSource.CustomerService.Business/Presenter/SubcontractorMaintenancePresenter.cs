using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.Model;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Hulcher.OneSource.CustomerService.Business.ViewModel;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core.Security;
using Hulcher.OneSource.CustomerService.Core;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    public class SubcontractorMaintenancePresenter
    {
        #region [ Attributes ]

        private ISubcontractorMaintenanceView _view;

        private SubContractorMaintenanceViewModel _viewModel;

        private SubcontractorModel _subContractorModel;

        #endregion

        #region [ Constructor ]

        public SubcontractorMaintenancePresenter(ISubcontractorMaintenanceView view)
        {
            _view = view;
            _subContractorModel = new  SubcontractorModel();
            _viewModel = new SubContractorMaintenanceViewModel(view, _subContractorModel);
        }

        #endregion

        #region [ Methods ]

        public void LoadSubcontractors()
        {
            try
            {
                _view.SubcontractorList = _subContractorModel.ListAllSubcontractors();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Load Subcontractor list Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Load Subcontractor list  Information (Presenter - LoadSubcontractors Method).", false);
            }            
        }

        public void EditSubcontractor()
        {
            try
            {
                _viewModel.EditSubcontractor();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Edit Subcontractor!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Edit Subcontractor (Presenter - EditSubcontractor Method).", false);
            }  
        }

        public void SaveOrUpdateSubcontractor()
        {
            try
            {
                if (!_view.SubcontractorID.HasValue)
                {
                    CS_Subcontractor subcontractorToSave = CreateSaveSubcontractorEntity();
                    _subContractorModel.SaveSubcontractor(subcontractorToSave);                    
                }
                else
                {
                    CS_Subcontractor subcontractorToUpdate = CreateUpdateSubcontractorEntity();
                    _subContractorModel.UpdateSubcontractor(subcontractorToUpdate);
                }                
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Save Subcontractor!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Save Subcontractor (Presenter - SaveOrUpdateSubcontractor Method).", false);
            }           
        }

        public void DeleteSubcontractor()
        {
            try
            {
                if (_view.SubcontractorID.HasValue)
                {
                    CS_Subcontractor subContractorToRemove = CreateDeleteSubcontractorEntity();
                    _subContractorModel.UpdateSubcontractor(subContractorToRemove);                    
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Remove Subcontractor!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Remove Subcontractor (Presenter - DeleteSubcontractor Method).", false);
            }
        }

        private CS_Subcontractor CreateDeleteSubcontractorEntity()
        {
            CS_Subcontractor oldSubcontractor = _subContractorModel.GetSubcontractorById(_view.SubcontractorID.Value);
            CS_Subcontractor subcontractorToRemove = new CS_Subcontractor();
            subcontractorToRemove.Id = oldSubcontractor.Id;
            subcontractorToRemove.Name = oldSubcontractor.Name;
            subcontractorToRemove.CreatedBy = oldSubcontractor.CreatedBy;
            subcontractorToRemove.CreationDate = oldSubcontractor.CreationDate;
            subcontractorToRemove.ModifiedBy = oldSubcontractor.ModifiedBy;
            subcontractorToRemove.ModificationDate = oldSubcontractor.ModificationDate;
            subcontractorToRemove.Active = false;
            return subcontractorToRemove;
        }

        private CS_Subcontractor CreateUpdateSubcontractorEntity()
        {
            CS_Subcontractor oldSubcontractor = _subContractorModel.GetSubcontractorById(_view.SubcontractorID.Value);
            CS_Subcontractor subcontractorToUpdate = new CS_Subcontractor();
            subcontractorToUpdate.Id = oldSubcontractor.Id;
            subcontractorToUpdate.Name = _view.Name;
            subcontractorToUpdate.CreatedBy = oldSubcontractor.CreatedBy;
            subcontractorToUpdate.CreationDate = oldSubcontractor.CreationDate;
            subcontractorToUpdate.ModifiedBy = _view.UserName;
            subcontractorToUpdate.ModificationDate = DateTime.Now;
            subcontractorToUpdate.Active = oldSubcontractor.Active;
            return subcontractorToUpdate;
        }

        private CS_Subcontractor CreateSaveSubcontractorEntity()
        {
            CS_Subcontractor subcontractorToSave = new CS_Subcontractor();
            subcontractorToSave.Name = _view.Name;
            subcontractorToSave.CreatedBy = _view.UserName;
            subcontractorToSave.CreationDate = DateTime.Now;
            subcontractorToSave.ModifiedBy = _view.UserName;
            subcontractorToSave.ModificationDate = DateTime.Now;
            subcontractorToSave.Active = true;
            return subcontractorToSave;
        }

        public void ClearFields()
        {
            _view.Name = string.Empty;
            _view.CreationPanelVisible = false;
        }

        public void VerifyAccess()
        {
            try
            {
                AZManager azManager = new AZManager();
                AZOperation[] azOP = azManager.CheckAccessById(_view.UserName, _view.Domain, new Globals.Security.Operations[] { Globals.Security.Operations.Subcontractor });

                if (!azOP[0].Result)
                {                    
                    _view.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to verify Permissions!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error ocurred while trying to verify Permissions.", false);
            }
        }
        #endregion
    }
}
