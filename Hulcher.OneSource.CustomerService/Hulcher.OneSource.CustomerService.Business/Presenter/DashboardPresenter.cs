using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.Business.ViewModel;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Core.Utils;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;

using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    public class DashboardPresenter
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of the Dashboard View Interface
        /// </summary>
        private IDashboardView _view;

        /// <summary>
        /// Instance of the Dashboard View Model
        /// </summary>
        private DashboardViewModel _dashboardViewModel;

        /// <summary>
        /// Instance of the Settings Model
        /// </summary>
        private SettingsModel _settingsModel;

        /// <summary>
        /// Instance of Call Log Model
        /// </summary>
        private CallLogModel _callLogModel;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="view">Instance of the View</param>
        public DashboardPresenter(IDashboardView view)
        {
            this._view = view;
        }

        #endregion

        #region [ Methods ]

        #region [ Common ]

        /// <summary>
        /// Fills the initial values of the screen
        /// </summary>
        public void LoadPage()
        {
            try
            {
                using (_dashboardViewModel = new DashboardViewModel(_view))
                {
                    _dashboardViewModel.LoadPage();
                    _dashboardViewModel.FilterJobSummary();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Load Page Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Load Page Information (Presenter - LoadPage Method).", false);
            }

            try
            {
                using (_settingsModel = new SettingsModel())
                {
                    _view.DashboardRefreshRate = _settingsModel.GetDashboardRefreshRate();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to get the Refresh Rate information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Refresh Rate information.", false);
            }
        }

        /// <summary>
        /// Process the event of a Postback
        /// </summary>
        public void LoadPagePostback()
        {
            try
            {
                using (_dashboardViewModel = new DashboardViewModel(_view))
                {
                    _dashboardViewModel.UpdateReadItems();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Update the Read Items!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Update the Read Items.", false);
            }
        }

        /// <summary>
        /// Executes the search to find
        /// </summary>
        public void Find()
        {
            try
            {
                using (_dashboardViewModel = new DashboardViewModel(_view))
                {
                    switch (_view.DashBoardViewType)
                    {
                        case Globals.Dashboard.ViewType.JobSummaryView:
                            _dashboardViewModel.FilterJobSummary();
                            break;
                        case Globals.Dashboard.ViewType.JobCallLogView:
                            _dashboardViewModel.FilterJobCallLog();
                            _view.CallLogSelectedRowId = string.Empty;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Find information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Find information.", false);
            }
        }
        
        /// <summary>
        /// Executes the Export to CSV process
        /// </summary>
        public void ExportToCSV()
        {
            try
            {
                using (_dashboardViewModel = new DashboardViewModel(_view))
                {
                    switch (_view.DashBoardViewType)
                    {
                        case Globals.Dashboard.ViewType.JobSummaryView:
                            _dashboardViewModel.CreateCSVJobSummary();
                            break;
                        case Globals.Dashboard.ViewType.JobCallLogView:
                            _dashboardViewModel.CreateCSVCallLog();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Export Information to a CSV file!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Export Information to a CSV file.", false);
            }
        }

        #endregion

        #region [ Job Summary ]

        /// <summary>
        /// Sets the default date values to the initial dates (start,end) on the screen
        /// </summary>
        public void DefaultDateJobSummary()
        {
            try
            {
                _dashboardViewModel.DefaultDateJobSummary();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to set the default date values to the initial dates (start,end) on the screen!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to set the default date values to the initial dates (start,end) on the screen.", false);
            }      
        }

        /// <summary>
        /// Fills a Job Summary Row
        /// </summary>
        public void FillJobSummaryRow()
        {
            try
            {
                using (_dashboardViewModel = new DashboardViewModel(_view))
                {
                    _dashboardViewModel.GetJobSummaryResourceList();
                    _dashboardViewModel.SetJobSummaryRowData();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Set Job Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Set Job Information.", false);
            }
        }

        /// <summary>
        /// Fills a Job Summary Resource Row
        /// </summary>
        public void FillJobSummaryResourceRow()
        {
            try
            {
                using (_dashboardViewModel = new DashboardViewModel(_view))
                {
                    _dashboardViewModel.SetJobSummaryResourceRowData();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to set the Resource Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to set the Resource Information.", false);
            }
        }

        /// <summary>
        /// Clear the fields on the jobsummary section when user clicks on the reset button
        /// </summary>
        public void ClearFieldsResetJobSummary()
        {
            try
            {
                _view.ClearFieldsResetJobSummary();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to clear the fields on the job summary section!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to clear the fields on the job summary section.", false);
            }            
        }
        #endregion

        #region [ Job Call Log ]

        /// <summary>
        /// Reset the filter fields on the job calllog
        /// </summary>
        public void ClearFieldsResetCallLog()
        {
            try
            {
                _view.ClearFieldsResetCallLog();
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to reset the filter fields on the job calllog!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to reset the filter fields on the job calllog.", false);
            }  
        }

        /// <summary>
        /// Fills a Job Call Log Division Row
        /// </summary>
        public void FillJobCallLogDivisionRow()
        {
            try
            {
                using (_dashboardViewModel = new DashboardViewModel(_view))
                {
                    _dashboardViewModel.SetJobCallLogDivisionRowData();
                    _dashboardViewModel.GetCallLogViewJobList();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Set Division Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Set Division Information.", false);
            }
        }

        /// <summary>
        /// Fills a Job Call Log Job Row
        /// </summary>
        public void FillJobCallLogJobRow()
        {
            try
            {
                using (_dashboardViewModel = new DashboardViewModel(_view))
                {
                    _dashboardViewModel.SetJobCallLogJobRowData();
                    _dashboardViewModel.GetCallLogViewCallEntryList();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Set Job Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Set Job Information.", false);
            }
        }

        /// <summary>
        /// Fills a Job Call Log Call Entry Row
        /// </summary>
        public void FillJobCallLogCallEntryRow()
        {
            try
            {
                using (_dashboardViewModel = new DashboardViewModel(_view))
                {
                    _dashboardViewModel.SetCallLogViewCallEntryRowReadUnread();
                    _dashboardViewModel.SetCallLogViewCallEntryRowData();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Set Call Entry Information!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Set Call Entry Information.", false);
            }
        }

        /// <summary>
        /// Check Dashboard visibility based on General Log selection
        /// </summary>
        public void CheckDashboardVisibilityCallEntry()
        {
            try
            {
                if (_view.GeneralLogCallLogViewFilter)
                {
                    _view.CallLogRepeaterCallEntryColumnVisibility = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to check Dashboard visibility based on General Log selection!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to check Dashboard visibility based on General Log selection.", false);
            }
        }

        /// <summary>
        /// Check Dashboard visibility based on General Log selection
        /// </summary>
        public void CheckDashboardVisibilityDivisionHeader()
        {
            try
            {
                if (_view.GeneralLogCallLogViewFilter)
                {
                    _view.CallLogRepeaterDivisionHeaderVisibility = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to check Dashboard visibility based on General Log selection!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to check Dashboard visibility based on General Log selection.", false);
            }
        }

        /// <summary>
        /// Check Dashboard visibility based on General Log selection
        /// </summary>
        public void CheckDashboardVisibilityDivision()
        {
            try
            {
                if (_view.GeneralLogCallLogViewFilter)
                {
                    _view.CallLogRepeaterDivisionColumnVisibility = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to check Dashboard visibility based on General Log selection!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to check Dashboard visibility based on General Log selection.", false);
            }
        }

        /// <summary>
        /// Check Dashboard visibility based on General Log selection
        /// </summary>
        public void CheckDashboardVisibilityJob()
        {
            try
            {
                if (_view.GeneralLogCallLogViewFilter)
                {
                    _view.CallLogRepeaterJobColumnVisibility = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to check Dashboard visibility based on General Log selection!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to check Dashboard visibility based on General Log selection.", false);
            }
        }

        public void DeleteCallLog()
        {
            try
            {
                using (_callLogModel = new CallLogModel())
                {
                    CS_CallLog callLogToRemove = CreateDeleteCallLogEntity();
                    IList<CS_CallLogResource> callLogResourceListToRemove = CreateDeleteCallLogResourceEntity();
                    IList<CS_CallLogCallCriteriaEmail> callLogCallCriteriaEmail = CreateDeleteCallLogCallCriteriaEmailEntity();
                    _callLogModel.UpdateCallLogAndReferences(callLogToRemove, callLogResourceListToRemove, callLogCallCriteriaEmail);
                }                
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to delete the Call Log!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to delete the Call Log.", false);
            }
        }

        private CS_CallLog CreateDeleteCallLogEntity()
        {
            CS_CallLog oldCallLog = _callLogModel.GetCallLogById(_view.CallLogIdToDelete);
            CS_CallLog callLogToRemove = new CS_CallLog();
            callLogToRemove.ID = _view.CallLogIdToDelete;
            callLogToRemove.JobID = oldCallLog.JobID;
            callLogToRemove.CallTypeID = oldCallLog.CallTypeID;
            callLogToRemove.PrimaryCallTypeId = oldCallLog.PrimaryCallTypeId;
            callLogToRemove.CallDate = oldCallLog.CallDate;
            callLogToRemove.CalledInByEmployee = oldCallLog.CalledInByEmployee;
            callLogToRemove.CalledInByCustomer = oldCallLog.CalledInByCustomer;
            callLogToRemove.Xml = oldCallLog.Xml;
            callLogToRemove.Note = oldCallLog.Note;
            callLogToRemove.CreatedBy = oldCallLog.CreatedBy;
            callLogToRemove.CreationDate = oldCallLog.CreationDate;
            callLogToRemove.ModifiedBy = _view.UserName;
            callLogToRemove.ModificationDate = DateTime.Now;
            callLogToRemove.Active = false;
            return callLogToRemove;
        }

        private IList<CS_CallLogResource> CreateDeleteCallLogResourceEntity()
        {
            IList<CS_CallLogResource> callLogListToRemove = new List<CS_CallLogResource>();
            IList<CS_CallLogResource> oldCallLogResourceList = _callLogModel.ListCallLogResourcesByCallLog(_view.CallLogIdToDelete);
            for (int i = 0; i < oldCallLogResourceList.Count; i++)
            {
                CS_CallLogResource callLogResource = new CS_CallLogResource();
                callLogResource.ID = oldCallLogResourceList[i].ID;
                callLogResource.CallLogID = oldCallLogResourceList[i].CallLogID;
                callLogResource.EmployeeID = oldCallLogResourceList[i].EmployeeID;
                callLogResource.EquipmentID = oldCallLogResourceList[i].EquipmentID;
                callLogResource.ContactID = oldCallLogResourceList[i].ContactID;
                callLogResource.JobID = oldCallLogResourceList[i].JobID;
                callLogResource.Type = oldCallLogResourceList[i].Type;
                callLogResource.CreatedBy = oldCallLogResourceList[i].CreatedBy;
                callLogResource.CreationDate = oldCallLogResourceList[i].CreationDate;
                callLogResource.ModifiedBy = _view.UserName;
                callLogResource.ModificationDate = DateTime.Now;
                callLogResource.Active = false;
                callLogResource.InPerson = oldCallLogResourceList[i].InPerson;
                callLogResource.Voicemail = oldCallLogResourceList[i].Voicemail;
                callLogListToRemove.Add(callLogResource);
            }
            return callLogListToRemove;
        }

        private IList<CS_CallLogCallCriteriaEmail> CreateDeleteCallLogCallCriteriaEmailEntity()
        {
            IList<CS_CallLogCallCriteriaEmail> callCriteriaEmailToDelete = new List<CS_CallLogCallCriteriaEmail>();
            //Creating int list because the method to list call criteria emails already exists
            List<int> callLogIdList = new List<int>();
            callLogIdList.Add(_view.CallLogIdToDelete);
            IList<CS_CallLogCallCriteriaEmail> oldcallCriteriaEmail = _callLogModel.ListCallCriteriaEmails(callLogIdList);
            for (int i = 0; i < oldcallCriteriaEmail.Count; i++)
            {
                CS_CallLogCallCriteriaEmail callCriteriaEmail = new CS_CallLogCallCriteriaEmail();
                callCriteriaEmail.ID = oldcallCriteriaEmail[i].ID;
                callCriteriaEmail.CallLogID = oldcallCriteriaEmail[i].CallLogID;
                callCriteriaEmail.EmailID = oldcallCriteriaEmail[i].EmailID;
                callCriteriaEmail.Name = oldcallCriteriaEmail[i].Name;
                callCriteriaEmail.Email = oldcallCriteriaEmail[i].Email;
                callCriteriaEmail.Status = oldcallCriteriaEmail[i].Status;
                callCriteriaEmail.StatusDate = oldcallCriteriaEmail[i].StatusDate;
                callCriteriaEmail.CreatedBy = oldcallCriteriaEmail[i].CreatedBy;
                callCriteriaEmail.CreationDate = oldcallCriteriaEmail[i].CreationDate;
                callCriteriaEmail.ModifiedBy = _view.UserName;
                callCriteriaEmail.ModificationDate = DateTime.Now;
                callCriteriaEmail.Active = false;
                callCriteriaEmailToDelete.Add(callCriteriaEmail);
            }
            return callCriteriaEmailToDelete;
        }

        #endregion

        #endregion
    }
}
