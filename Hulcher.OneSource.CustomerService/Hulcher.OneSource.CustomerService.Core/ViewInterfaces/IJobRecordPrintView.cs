using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core.Utils;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    /// <summary>
    /// View Interface for the JobRecordPrint
    /// </summary>
    public interface IJobRecordPrintView : IBaseView
    {
        /// <summary>
        /// Property for accessing the Job entity
        /// </summary>
        CS_Job Job { set; get; }

        /// <summary>
        /// Property for storing the Job Id
        /// </summary>
        int? JobId { get; set; }

        /// <summary>
        /// Property for getting the username
        /// </summary>
        string Username { get; }

        /// <summary>
        /// Property for getting the Domain
        /// </summary>
        string Domain { get; }

        /// <summary>
        /// Sets page Title
        /// </summary>
        string PageTitle { set; }

        /// <summary>
        /// Special Pricing Information related to the job record
        /// </summary>
        CS_SpecialPricing SpecialPricingEntity { set; }

        /// <summary>
        /// Preset Information related to the job record
        /// </summary>
        CS_PresetInfo PresetInfoEntity { set; }

        /// <summary>
        /// Lost Job Information related to the job record
        /// </summary>
        CS_LostJobInfo LostJobEntity { set; }

        /// <summary>
        /// Set the JobDivision list to the grid on the webpage
        /// </summary>
        IList<CS_JobDivision> JobDivisionEntityList { set; }

        /// <summary>
        /// set the Customer Contract list to the gridview on the webpage
        /// </summary>
        IList<CS_CustomerContract> CustomerContract { set; }

        /// <summary>
        /// set CustomerSpecificInfo List
        /// </summary>
        IList<CustomerSpecificInfo> CustomerSpecificFields { set; }

        /// <summary>
        /// Pass name by parameter;
        /// </summary>
        string ParamName { get; set; }

        /// <summary>
        /// Contact Identifier
        /// </summary>
        int? Identifier { get; set; }

        /// <summary>
        /// Sets PermitInfoGrid DataSource
        /// </summary>
        IList<CS_JobPermit> PermitInfoGridDataSource { set; }

        /// <summary>
        /// Sets EquipmentRequest DataSource
        /// </summary>
        IList<CS_Job_LocalEquipmentType> EquipmentRequestDataSource { set; }

        /// <summary>
        /// Sets PhotoReport Grid
        /// </summary>
        IList<CS_JobPhotoReport> PhotoReporGridDataSource { set; }


        /// <summary>
        /// Sets JobCallLog Grid
        /// </summary>
        IList<CS_CallLog> JobCallLogGridDataSource { set; }

        /// <summary>
        /// sets JobDescriptionEntity
        /// </summary>
        CS_JobDescription JobDescriptionEntity { set; }

        /// <summary>
        /// Sets ScopeOfWork Grid
        /// </summary>
        IList<CS_ScopeOfWork> ScopeOfWorkGridDataSource { set; }
    }
}
