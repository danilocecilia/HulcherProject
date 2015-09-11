using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Business.ViewModel;
using Hulcher.OneSource.CustomerService.Business.Model;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    public class RegionalMaintenancePresenter
    {
        #region [ Attributes ]

        private IRegionalMaintenanceView _view;

        private RegionalMaintenanceViewModel _regionViewModel;

        private RegionModel _regionModel;

        #endregion

        #region [ Constructor ]

        public RegionalMaintenancePresenter(IRegionalMaintenanceView view)
        {
            _view = view;

            _regionViewModel = new RegionalMaintenanceViewModel(view);
            _regionModel = new RegionModel();
        }

        public RegionalMaintenancePresenter(IRegionalMaintenanceView view, RegionModel model)
        {
            _view = view;

            _regionViewModel = new RegionalMaintenanceViewModel(view);
            _regionModel = model;
        }

        #endregion

        #region [ Methods ]

        public void LoadPage()
        {
            try
            {
                _view.EditMode = false;
                _regionViewModel.BindDashboard(); 
                _regionViewModel.BindRegion();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Load the page!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Load the page (Presenter - LoadPage Method).", false);
            }
        }

        public void Find()
        {
            try
            {
                _regionViewModel.BindDashboard();
                _regionViewModel.BindRegion();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Load Region Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Load Region Information (Presenter - LoadPage Method).", false);
            }
        }

        #region [ Save ]

        public void SaveContinue()
        {
            try
            {
                _regionViewModel.SaveRegion();
                LoadPage();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Save Region Information!\n{0}\n{1}\n{2}", ex.Message, ex.StackTrace, ex.InnerException));
                _view.DisplayMessage("There was an error while trying to Save Region Information (Presenter - SaveContinue Method).", false);
            }
        }

        public void SaveClose()
        {
            try
            {
                _regionViewModel.SaveRegion();
                _view.DisplayMessage("Region saved successfully", true);
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Save Region Information!\n{0}\n{1}\n{2}", ex.Message, ex.StackTrace, ex.InnerException));
                _view.DisplayMessage("There was an error while trying to Save Region Information (Presenter - SaveClose Method).", false);
            }
        }

        #endregion

        #region [ Dashboard ]

        #region [ Region ]

        public void BindRegion()
        {
            try
            {
                _regionViewModel.BindRegion();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Fill the Region List!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Fill the Region List (Presenter - BindRegion Method).", false);
            }
        }

        public void CreateRegionRow()
        {
            try
            {
                _regionViewModel.SetRegionFields();
                BindDivision();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Fill the Region Row!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Fill the Region Row (Presenter - CreateRegionRow Method).", false);
            }
        }

        #endregion

        #region [ Division ]

        public void BindDivision()
        {
            try
            {
                _regionViewModel.BindDivision();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Load Division Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Load Division Information (Presenter - BindDivision Method).", false);
            }
        }

        public void CreateDivisionRow()
        {
            try
            {
                _regionViewModel.SetDivisionFields();
                BindEmployee();
                BindCombo();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to fill Division Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to fill Division Information (Presenter - CreateDivisionRow Method).", false);
            }
        }


        #endregion

        #region [ Employee ]

        public void BindEmployee()
        {
            try
            {
                _regionViewModel.BindEmployee();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Load Employee Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Load Employee Information (Presenter - LoadPage Method).", false);
            }
        }

        public void CreateEmployeeRow()
        {
            try
            {
                _regionViewModel.SetEmployeeFields();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to fill Employee Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to fill Employee Information (Presenter - CreateEmployeeRow Method).", false);
            }
        }

        #endregion

        #region [ Equipment ]

        public void BindCombo()
        {
            try
            {
                _regionViewModel.BindCombo();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Load Equipment Combo Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Load Equipment Combo Information (Presenter - LoadPage Method).", false);
            }
        }

        public void CreateComboRow()
        {
            try
            {
                _regionViewModel.SetComboFields();
                BindEquipment();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to fill Equipment Combo Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to fill Equipment Combo Information (Presenter - CreateEmployeeRow Method).", false);
            }
        }

        public void BindEquipment()
        {
            try
            {
                _regionViewModel.BindEquipment();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Load Equipment Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Load Equipment Information (Presenter - LoadPage Method).", false);
            }
        }

        public void CreateEquipmentRow()
        {
            try
            {
                _regionViewModel.SetEquipmentFields();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to fill Equipment Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to fill Equipment Information (Presenter - CreateEmployeeRow Method).", false);
            }
        }

        #endregion

        #endregion

        #region [ CRUD ]

        public void LoadEditMode()
        {
            _view.EditMode = true;
            BindDivisionList();
            BindRVPList();
        }

        public void BindDivisionList()
        {
            try
            {
                _regionViewModel.BindDivisionList();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Load Division Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Load Division Information (Presenter - BindDivisionList Method).", false);
            }
        }

        public void BindRVPList()
        {
            try
            {
                _regionViewModel.BindRegionalVicePresident();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Load RVP Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Load RVP Information (Presenter - BindRVPList Method).", false);
            }
        }

        #endregion

        #endregion
    }
}
