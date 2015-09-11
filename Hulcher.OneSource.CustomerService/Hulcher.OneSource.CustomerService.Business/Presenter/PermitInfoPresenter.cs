using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.Core.Utils;

using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Hulcher.OneSource.CustomerService.Business.Presenter
{
    /// <summary>
    /// Permit Info Presenter (Block inside Job Record Page)
    /// </summary>
    public class PermitInfoPresenter
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of Permit Info View interface
        /// </summary>
        private IPermitInfoView _view;

        private JobModel _jobModel;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="view">Instance of the Permit Info View Interface</param>
        public PermitInfoPresenter(IPermitInfoView view)
        {
            this._view = view;
            _jobModel = new JobModel();
        }

        public PermitInfoPresenter(IPermitInfoView view, JobModel jobModel)
        {
            this._view = view;
            this._jobModel = jobModel;
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Saves an uploaded file to the server folder
        /// </summary>
        public void SaveFile()
        {
            try
            {
                string dateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                string databaseFileName = string.Format("{0}_{1}", dateTime, _view.FileName);
                string fullFilePath = string.Format("{0}{1}", _view.UploadFolder, databaseFileName);
                string relativeFilePath = string.Format("{0}{1}", Globals.Configuration.UploadPermitPath, databaseFileName);

                FileUtility.SaveFile(_view.UploadedFile, fullFilePath);

                _view.FullFilePath = fullFilePath;
                _view.FilePath = relativeFilePath;
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to upload the file!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error has ocurred while trying to upload the file. Please try again.", false);
            }
        }

        public void AddPermit()
        {
            try
            {
                if (null != _view.PermitInfoEntity)
                {
                    _view.PermitInfoEntity.Active = true;
                    _view.PermitInfoEntity.CreationDate = DateTime.Now;

                    _view.PermitInfoEntityList.Add(_view.PermitInfoEntity);

                    _view.ListPermitInfo();
                    _view.ClearFields();
                }
                else
                    throw new Exception("The object containing the Permit Information is null!");
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to add an item on the list!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error has ocurred while trying to add an item on the list.", false);
            }
        }

        public void RemovePermit()
        {
            try
            {
                if (_view.PermitInfoEntityList.Count >= _view.RemoveIndex)
                {
                    _view.PermitInfoEntityList.RemoveAt(_view.RemoveIndex);
                    _view.ListPermitInfo();
                }
                else
                    throw new Exception("The Item Index informed does not exist on the List");
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to remove an item from the list!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error has ocurred while trying to remove an item from the list.", false);
            }
        }

        /// <summary>
        /// Return list os permit info by jobid
        /// </summary>
        public void GetPermitInfoByJobId()
        {
            try
            {
                if (_view.JobId.HasValue)
                    _view.PermitInfoEntityList = _jobModel.GetPermitInfoByJob(_view.JobId.Value);
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to retrieve the list of permit info!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error has ocurred while trying to retrieve the list of permit info.", false);
            }
        }

        /// <summary>
        /// List All Permit Types
        /// </summary>
        public void ListPermitTypes()
        {
            try
            {
                _view.PermitTypeList = _jobModel.ListAllPermitTypes();


            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Load Job Permit Types", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Load Job Permit Types", false);
            }
        }

        /// <summary>
        /// Fill Permit Info when cloning a job record
        /// </summary>
        public void ListPermitInfoCloning()
        {
            try
            {
                if (_view.CloningId.HasValue)
                {
                    _view.PermitInfoEntityList = FilteredPermitInfoForCloning( _jobModel.GetPermitInfoByJob(_view.CloningId.Value));                    
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("There was an error while trying to Load Job Permit Info", ex.Message, ex.StackTrace));
                _view.DisplayMessage("There was an error while trying to Load Job Permit Info", false);
            }
        }

        public IList<CS_JobPermit> FilteredPermitInfoForCloning(IList<CS_JobPermit> jobPermitList)
        {
            IList<CS_JobPermit> filteredList = new List<CS_JobPermit>();
            foreach (var item in jobPermitList)
            {
                if (!item.CS_JobPermitType.Description.ToLower().Equals("single-trip"))
                {
                    filteredList.Add(item);
                }
            }
            return filteredList;
        }

        #endregion
    }
}
