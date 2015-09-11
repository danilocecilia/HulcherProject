using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    public interface IPhotoReportView : IBaseView
    {
        /// <summary>
        /// Photo Report List related to the job record
        /// </summary>
        IList<CS_JobPhotoReport> PhotoReportList { get; set; }

        /// <summary>
        /// Stream that contains the file that was uploaded by the user
        /// </summary>
        Stream UploadedFile { get; set; }

        /// <summary>
        /// Photo Report item to add/remove from the list
        /// </summary>
        CS_JobPhotoReport PhotoReportEntity { get; set; }

        /// <summary>
        /// Index of the item that should be removed from the list
        /// </summary>
        int RemoveIndex { get; set; }

        /// <summary>
        /// Gets the path of the folder that will keep the uploaded files
        /// </summary>
        string UploadFolder { get; set; }

        /// <summary>
        /// File name that was generated after saving the uploaded file
        /// </summary>
        string FileName { get; set; }

        /// <summary>
        /// Path of the file that was generated after saving the uploaded file
        /// </summary>
        string FilePath { get; set; }

        /// <summary>
        /// Full phisical Path of the file that was generated after saving the uploaded file
        /// </summary>
        string FullFilePath { get; set; }

        /// <summary>
        /// Load the Photo Report List inside the GridView
        /// </summary>
        void ListPhotoReport();

        /// <summary>
        /// Clear all the form fields for a new entry
        /// </summary>
        void ClearFields();

        /// <summary>
        /// Get and Set the JobId
        /// </summary>
        int? JobId { get; set; }
    }
}
