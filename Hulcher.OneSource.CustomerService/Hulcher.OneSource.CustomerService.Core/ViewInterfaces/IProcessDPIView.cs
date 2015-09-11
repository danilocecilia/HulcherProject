using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.DataContext;

namespace Hulcher.OneSource.CustomerService.Core.ViewInterfaces
{
    public interface IProcessDPIView : IBaseView
    {
        #region [ Job Header ]

        /// <summary>
        /// Get/Set the jobid
        /// </summary>
        int JobID { get; set; }

        /// <summary>
        /// Get or set dpi id
        /// </summary>
        int DPIId { get; set; }

        /// <summary>
        /// Get or set Job Number to the label on the page
        /// </summary>
        string JobNumber { get; set; }

        /// <summary>
        /// Get or set Primary Division Number to the label on the page
        /// </summary>
        string PrimaryDivisionNumber { get; set; }

        /// <summary>
        /// Get or set Customer name to the label on the page
        /// </summary>
        string CustomerName { get; set; }

        /// <summary>
        /// Get or set location to the label on the page
        /// </summary>
        string Location { get; set; }

        /// <summary>
        /// Get or set jobaction to the label on the page
        /// </summary>
        string JobAction { get; set; }

        /// <summary>
        /// Get or set jobcategory to the label on the page
        /// </summary>
        string JobCategory { get; set; }

        /// <summary>
        /// Get or set jobtype to the label on the page
        /// </summary>
        string JobType { get; set; }

        /// <summary>
        /// Get or set number engines to the label on the page
        /// </summary>
        int? NumberOfEngines { get; set; }

        /// <summary>
        /// Get or set number of loads to the label on the page
        /// </summary>
        int? NumerOfLoads { get; set; }

        /// <summary>
        /// Get or set number of empties to the label on the page
        /// </summary>
        int? NumberOfEmpties { get; set; }

        #endregion

        #region [ Resource List ]

        string Disclaimer { get; set; }

        /// <summary>
        /// Data Source to fill the Resources Grid
        /// </summary>
        IList<CS_DPIResource> ResourceDataSource { get; set; }

        /// <summary>
        /// Rate Table
        /// </summary>
        IList<CS_DPIRate> RateTable { get; set; }

        #region [ Division ]

        /// <summary>
        /// Data Source to fill the first-level of the resource grid (Division)
        /// </summary>
        IList<KeyValuePair<int, string>> DivisionRowDataSource { get; set; }

        /// <summary>
        /// Data Row item for the first-level of the resource grid
        /// </summary>
        KeyValuePair<int, string> DivisionRowDataItem { get; set; }

        /// <summary>
        /// Division Name
        /// </summary>
        string DivisionRowDivisionName { get; set; }

        /// <summary>
        /// Division ID
        /// </summary>
        int DivisionRowDivisionID { get; set; }

        #endregion

        #region [ Resource ]

        void SetDefaultRateValues();

        void SetDefaultHoursValues();

        /// <summary>
        /// Method to fill the Resource Revenue Calculation
        /// </summary>
        void SetResourceRevenueCalculation();

        /// <summary>
        /// Data Source to fill the second-level of the resource grid (Resources)
        /// </summary>
        IList<CS_DPIResource> ResourceRowDataSource { get; set; }

        /// <summary>
        /// Data Row item for the second-level of the resource grid
        /// </summary>
        CS_DPIResource ResourceRowDataItem { get; set; }

        /// <summary>
        /// Resource ID (for the DPI Resource Table)
        /// </summary>
        int ResourceRowID { get; set; }

        /// <summary>
        /// Identify if the Resource is an employee or not
        /// </summary>
        bool ResourceRowIsEmployee { get; set; }

        /// <summary>
        /// Resource Identifier
        /// </summary>
        string ResourceRowResourceID { get; set; }

        /// <summary>
        /// Resource Name
        /// </summary>
        string ResourceRowResourceName { get; set; }

        /// <summary>
        /// Resource Calculated Work Hours
        /// </summary>
        decimal ResourceRowCalculatedHours { get; set; }

        /// <summary>
        /// Resource Modified Work Hours
        /// </summary>
        decimal? ResourceRowModifiedHours { get; set; }

        /// <summary>
        /// Resource Calculation Status
        /// </summary>
        short ResourceRowCalculationStatus { get; set; }

        /// <summary>
        /// Identify if a Resource will continue work after 23:59
        /// </summary>
        bool ResourceRowIsContinuing { get; set; }

        /// <summary>
        /// Hold the calculated Hours if the Continuing Flag is checked
        /// </summary>
        decimal? ResourceRowContinuingHours { get; set; }

        /// <summary>
        /// Resource Rate
        /// </summary>
        decimal? ResourceRowRate { get; set; }

        /// <summary>
        /// Resource Modified Rate
        /// </summary>
        decimal? ResourceRowModifiedRate { get; set; }

        /// <summary>
        /// Resource Permit Quantity
        /// </summary>
        int? ResourceRowPermitQuantity { get; set; }

        /// <summary>
        /// Resource Permit Rate
        /// </summary>
        decimal? ResourceRowPermitRate { get; set; }

        /// <summary>
        /// Resource Meal Quantity
        /// </summary>
        int? ResourceRowMealQuantity { get; set; }

        /// <summary>
        /// Resource Meal Rate
        /// </summary>
        decimal? ResourceRowMealRate { get; set; }

        /// <summary>
        /// Indicate if the Resource will stay at a hotel
        /// </summary>
        bool ResourceRowHasHotel { get; set; }

        /// <summary>
        /// Resource Hotel Rate
        /// </summary>
        decimal? ResourceRowHotelRate { get; set; }

        /// <summary>
        /// Resource Modified Hotel Rate
        /// </summary>
        decimal? ResourceRowModifiedHotelRate { get; set; }

        /// <summary>
        /// Resource Division ID
        /// </summary>
        int ResourceRowDivisionID { get; set; }

        #endregion

        #endregion

        #region [ Total ]

        /// <summary>
        /// Previous total of The Job
        /// </summary>
        decimal PreviousTotal { get; set; }

        /// <summary>
        /// Current Revenue
        /// </summary>
        decimal NewRevenue { get; set; }

        /// <summary>
        /// Current Total (Current Revenue + Previous Total)
        /// </summary>
        decimal CurrentTotal { get; set; }

        /// <summary>
        /// DPI Date
        /// </summary>
        DateTime DPIDate { get; set; }

        #endregion

        #region [ Special Pricing ]

        bool ViewSpecialPricing { get;  set; }

        Globals.DPI.SpecialPriceType SpecialPriceType { get; set; }

        decimal? OverallJobDiscount { get; set; }

        decimal? LumpSumValue { get; set; }

        int? LumpSumDuration { get; set; }

        decimal? LumpSumValuePerDay { get; set; }

        IList<CS_DPIManualSpecialPricing> ManualSpecialPricingTable { get; set; }

        string SpecialPricingNotes { get; set; }

        void SetOverallDiscountOnblur();

        void SetLumpSumDiscountOnblur();

        #endregion

        #region [ Save ]

        /// <summary>
        /// Gets the username that is updating the DPI
        /// </summary>
        string Username { get; set; }

        /// <summary>
        /// Gets the Employee Id that is Approving the DPI
        /// </summary>
        int? LoggedEmployee { get; set; }

        /// <summary>
        /// Sets the DPI status before saving it
        /// </summary>
        Globals.DPI.DpiStatus DPIStatus { get; set; }

        /// <summary>
        /// Sets the DPI Resource List to be saved
        /// </summary>
        IList<CS_DPIResource> DPIResources { get; set; }

        /// <summary>
        /// Gets the Parent Field Identifier, to update the Dashboard page
        /// </summary>
        string ParentFieldId { get; }

        /// <summary>
        /// Updates the Dashboard page in order to display the correct value after saving a DPI
        /// </summary>
        void UpdateDashboard();

        #endregion
    }
}
