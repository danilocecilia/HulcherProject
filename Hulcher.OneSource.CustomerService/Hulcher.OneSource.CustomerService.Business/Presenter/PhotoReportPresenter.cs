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
    /// Photo Report Presenter (Block inside Job Record Page)
    /// </summary>
    public class PhotoReportPresenter
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of Photo Report View interface
        /// </summary>
        private IPhotoReportView _view;

        /// <summary>
        /// Instance of Job Model object
        /// </summary>
        private JobModel _jobModel;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="view">Instance of the Photo Report View Interface</param>
        public PhotoReportPresenter(IPhotoReportView view)
        {
            _jobModel = new JobModel();
            this._view = view;
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Saves an uploaded file to the server folder
        /// </summary>
        public void SaveFile()
        {
            //string fileExtension = _view.FileName.Substring(_view.FileName.LastIndexOf(".") + 1);
            //if (!fileExtension.Equals("png", StringComparison.OrdinalIgnoreCase) &&
            //    !fileExtension.Equals("bmp", StringComparison.OrdinalIgnoreCase) &&
            //    !fileExtension.Equals("jpg", StringComparison.OrdinalIgnoreCase) &&
            //    !fileExtension.Equals("jpeg", StringComparison.OrdinalIgnoreCase) &&
            //    !fileExtension.Equals("tif", StringComparison.OrdinalIgnoreCase) &&
            //    !fileExtension.Equals("tiff", StringComparison.OrdinalIgnoreCase) &&
            //    !fileExtension.Equals("gif", StringComparison.OrdinalIgnoreCase))
            //{
            //    throw new Exception("Only files with extension .png, .bmp, .jpg, .tiff or .gif are allowed");
            //}

            try
            {
                string dateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                string databaseFileName = string.Format("{0}_{1}", dateTime, _view.FileName);
                string fullFilePath = string.Format("{0}{1}", _view.UploadFolder, databaseFileName);
                string relativeFilePath = string.Format("{0}{1}", Globals.Configuration.UploadPhotoReportPath, databaseFileName);

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

        public void AddPhotoReport()
        {
            try
            {
                if (null != _view.PhotoReportEntity)
                {
                    _view.PhotoReportEntity.Active = true;
                    _view.PhotoReportEntity.CreationDate = DateTime.Now;

                    _view.PhotoReportList.Add(_view.PhotoReportEntity);

                    _view.ListPhotoReport();
                    _view.ClearFields();
                }
                else
                    throw new Exception("The object containing the Photo Report item is null!");
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to add an item on the list!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error has ocurred while trying to add an item on the list.", false);
            }
        }

        public void RemovePhotoReport()
        {
            try
            {
                if (_view.PhotoReportList.Count >= _view.RemoveIndex)
                {
                    _view.PhotoReportList.RemoveAt(_view.RemoveIndex);
                    _view.ListPhotoReport();
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
        /// Return list os photo report by jobid
        /// </summary>
        public void GetPhotoReportByJobId()
        {
            try
            {
                if (_view.JobId.HasValue)
                {
                    _view.PhotoReportList = _jobModel.GetPhotoReportByJobId(_view.JobId.Value);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("An error has ocurred while trying to retrieve the list of permit info!\n{0}\n{1}", ex.Message, ex.StackTrace));
                _view.DisplayMessage("An error has ocurred while trying to retrieve the list of permit info.", false);
            }
        }
        #endregion
    }
}
