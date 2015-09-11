using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    public interface IPermitInfoView : IBaseView
    {
        /// <summary>
        /// Permit Information List related to the job record
        /// </summary>
        IList<CS_JobPermit> PermitInfoEntityList { get; set; }

        /// <summary>
        /// Stream that contains the file that was uploaded by the user
        /// </summary>
        Stream UploadedFile { get; set; }

        /// <summary>
        /// Permit Info item to add/remove from the list
        /// </summary>
        CS_JobPermit PermitInfoEntity { get; set; }

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
        /// Load the Permit Information List inside the GridView
        /// </summary>
        void ListPermitInfo();

        /// <summary>
        /// Clear all the form fields for a new entry
        /// </summary>
        void ClearFields();

        /// <summary>
        /// Gets/Sets the Job Identifier related to the Permit Info
        /// </summary>
        int? JobId { get; set; }

        /// <summary>
        /// Job Id when cloning a job record
        /// </summary>
        int? CloningId { get; set; }

        /// <summary>
        /// Property for permit types
        /// </summary>
        IList<CS_JobPermitType> PermitTypeList { set; }
    }
}
