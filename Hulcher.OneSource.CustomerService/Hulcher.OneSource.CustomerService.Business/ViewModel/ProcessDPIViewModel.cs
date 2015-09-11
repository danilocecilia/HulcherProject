using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Hulcher.OneSource.CustomerService.Core;

namespace Hulcher.OneSource.CustomerService.Business.ViewModel
{
    public class ProcessDPIViewModel
    {
        #region [ Attributes ]

        /// <summary>
        /// Instance of the Process DPI View Interface
        /// </summary>
        private IProcessDPIView _view;

        /// <summary>
        /// Instance of the DPI Model class
        /// </summary>
        private DPIModel _dpiModel;

        /// <summary>
        /// Instance of the Job Model class
        /// </summary>
        private JobModel _jobModel;

        #endregion

        #region [ Constructor ]

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="view">Instance of the Process DPI View Interface</param>
        public ProcessDPIViewModel(IProcessDPIView view)
        {
            _view = view;
            _dpiModel = new DPIModel();
            _jobModel = new JobModel();
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Get the information about Job Header and set the values to the controls
        /// </summary>
        public void SetJobHeaderFields()
        {
            CS_DPI dpi = _dpiModel.GetDPI(_view.DPIId);

            if (null != dpi)
            {
                _view.JobID = dpi.JobID;

                _view.DPIDate = dpi.Date;

                _view.NewRevenue = dpi.Total;

                _view.JobNumber = dpi.CS_Job.PrefixedNumber;

                _view.PrimaryDivisionNumber = dpi.CS_Job.CS_JobDivision.FirstOrDefault().CS_Division.Name;

                _view.CustomerName = dpi.CS_Job.CS_CustomerInfo.CS_Customer.Name;

                _view.Location = string.Format("{0}, {1}",
                    dpi.CS_Job.CS_LocationInfo.CS_State.AcronymName,
                    dpi.CS_Job.CS_LocationInfo.CS_City.Name);

                _view.JobAction = dpi.CS_Job.CS_JobInfo.CS_JobAction.Description;

                _view.JobCategory = dpi.CS_Job.CS_JobInfo.CS_JobCategory.Description;

                _view.JobType = dpi.CS_Job.CS_JobInfo.CS_JobType.Description;

                if (dpi.CS_Job.CS_JobDescription.NumberEngines.HasValue)
                    _view.NumberOfEngines = dpi.CS_Job.CS_JobDescription.NumberEngines;
                else
                    _view.NumberOfEngines = 0;

                if (dpi.CS_Job.CS_JobDescription.NumberLoads.HasValue)
                    _view.NumerOfLoads = dpi.CS_Job.CS_JobDescription.NumberLoads;
                else
                    _view.NumerOfLoads = 0;

                if (dpi.CS_Job.CS_JobDescription.NumberEmpties.HasValue)
                    _view.NumberOfEmpties = dpi.CS_Job.CS_JobDescription.NumberEmpties;
                else
                    _view.NumberOfEmpties = 0;

                _view.ViewSpecialPricing = dpi.CS_Job.CS_JobInfo.PriceTypeID != (int)Globals.JobRecord.PriceType.PublishedRates;
                _view.ViewSpecialPricing = true;

                _view.Disclaimer = "All records listed as INSF have assumed end times of  " + dpi.ModificationDate.ToString("MM/dd/yyyy HH:mm") + " for calculation purposes";
            }
        }

        /// <summary>
        /// Fills the total values
        /// </summary>
        public void FillTotal()
        {
            _view.PreviousTotal = _dpiModel.GetPreviousTotal(_view.JobID, _view.DPIDate);
            _view.CurrentTotal = _view.PreviousTotal + _view.NewRevenue;
        }

        /// <summary>
        /// Fills the Rate table with the values in the Rate Table database
        /// </summary>
        public void LoadRateTable()
        {
            _view.RateTable = _dpiModel.ListRateTable();
        }

        /// <summary>
        /// Fills the Resource list based on a DPI Identifier
        /// </summary>
        public void ListAllResources()
        {
            _view.ResourceDataSource = _dpiModel.ListDPIResources(_view.DPIId);
        }

        /// <summary>
        /// Fills the Division list based on resource list
        /// </summary>
        public void ListAllDivisions()
        {
            List<KeyValuePair<int, string>> divisionList = new List<KeyValuePair<int, string>>();

            List<CS_DPIResource> tempList = new List<CS_DPIResource>();
            tempList.AddRange(_view.ResourceDataSource.Distinct(new Globals.DPI.CS_DPIResource_Comparer()));

            foreach (CS_DPIResource divisionItem in tempList)
            {
                if (divisionItem.EquipmentID.HasValue)
                    divisionList.Add(new KeyValuePair<int, string>(divisionItem.CS_Equipment.DivisionID, divisionItem.CS_Equipment.CS_Division.Name));
                else if (divisionItem.CS_Employee.DivisionID.HasValue)
                    divisionList.Add(new KeyValuePair<int, string>(divisionItem.CS_Employee.DivisionID.Value, divisionItem.CS_Employee.CS_Division.Name));
                else
                    divisionList.Add(new KeyValuePair<int, string>(0, "No Division Related"));
            }

            _view.DivisionRowDataSource = divisionList;
        }

        /// <summary>
        /// Fills the resource list per division
        /// </summary>
        public void ListAllResourcesByDivision()
        {
            List<CS_DPIResource> resourceList = new List<CS_DPIResource>();

            resourceList.AddRange(_view.ResourceDataSource.Where(e => e.EquipmentID.HasValue && e.CS_Equipment.DivisionID == _view.DivisionRowDataItem.Key));
            resourceList.AddRange(_view.ResourceDataSource.Where(e => e.EmployeeID.HasValue && e.CS_Employee.DivisionID.Value == _view.DivisionRowDataItem.Key));

            _view.ResourceRowDataSource = resourceList;
        }

        /// <summary>
        /// Sets the fields for the first-level of the resources grid (Division)
        /// </summary>
        public void SetDivisionRowInfo()
        {
            _view.DivisionRowDivisionName = _view.DivisionRowDataItem.Value;
            _view.DivisionRowDivisionID = _view.DivisionRowDataItem.Key;
        }

        /// <summary>
        /// Sets the fields for the second-level of the resources grid (Resource) 
        /// </summary>
        public void SetResourceRowInfo()
        {
            if (null != _view.ResourceRowDataItem)
            {
                _view.ResourceRowID = _view.ResourceRowDataItem.ID;

                if (_view.ResourceRowDataItem.EmployeeID.HasValue)
                {
                    _view.ResourceRowIsEmployee = true;

                    _view.ResourceRowResourceID = _view.ResourceRowDataItem.CS_Employee.PersonID;
                    _view.ResourceRowResourceName = _view.ResourceRowDataItem.CS_Employee.FullName;

                    _view.ResourceRowMealQuantity = _view.ResourceRowDataItem.MealQuantity;
                    _view.ResourceRowMealRate = _view.ResourceRowDataItem.MealRate;

                    _view.ResourceRowHasHotel = _view.ResourceRowDataItem.HasHotel;
                    _view.ResourceRowHotelRate = _view.ResourceRowDataItem.HotelRate;
                    _view.ResourceRowModifiedHotelRate = _view.ResourceRowDataItem.ModifiedHotelRate;
                }
                else
                {
                    _view.ResourceRowIsEmployee = false;

                    _view.ResourceRowResourceID = _view.ResourceRowDataItem.CS_Equipment.Name;
                    _view.ResourceRowResourceName = _view.ResourceRowDataItem.CS_Equipment.Description;

                    _view.ResourceRowPermitQuantity = _view.ResourceRowDataItem.PermitQuantity;
                    _view.ResourceRowPermitRate = _view.ResourceRowDataItem.PermitRate;
                }

                _view.ResourceRowCalculatedHours = _view.ResourceRowDataItem.Hours;
                _view.ResourceRowModifiedHours = _view.ResourceRowDataItem.ModifiedHours;
                _view.ResourceRowCalculationStatus = _view.ResourceRowDataItem.CalculationStatus;
                _view.ResourceRowIsContinuing = _view.ResourceRowDataItem.IsContinuing;
                _view.ResourceRowContinuingHours = _view.ResourceRowDataItem.ContinuingHours;
                _view.ResourceRowRate = _view.ResourceRowDataItem.Rate;
                _view.ResourceRowModifiedRate = _view.ResourceRowDataItem.ModifiedRate;
                _view.ResourceRowDivisionID = _view.DivisionRowDataItem.Key;

                _view.SetResourceRevenueCalculation();
            }
        }

        /// <summary>
        /// Saves the DPI
        /// </summary>
        public void SaveDPI()
        {
            CS_DPI dpi = _dpiModel.GetDPI(_view.DPIId);
            if (null != dpi)
            {
                DateTime modificationDate = DateTime.Now;

                dpi.ProcessStatus = (short)_view.DPIStatus;
                dpi.ProcessStatusDate = modificationDate;

                if (dpi.ProcessStatus == (short)Globals.DPI.DpiStatus.Approved)
                    dpi.ApprovedBy = _view.LoggedEmployee;

                dpi.ModifiedBy = _view.Username;
                dpi.ModificationDate = modificationDate;
                dpi.Total = _view.DPIResources.Where(e => e.Active).Sum(e => e.Total);

                IList<CS_DPIResource> oldResourceList = _dpiModel.ListDPIResources(_view.DPIId);
                IList<CS_DPIResource> newResourceList = _view.DPIResources;

                foreach (CS_DPIResource newResource in newResourceList)
                {
                    CS_DPIResource oldResource = oldResourceList.FirstOrDefault(e => e.ID == newResource.ID);
                    if (null != oldResource)
                    {
                        newResource.DPIID = oldResource.DPIID;
                        newResource.EquipmentID = oldResource.EquipmentID;
                        newResource.EmployeeID = oldResource.EmployeeID;
                        newResource.CalculationStatus = oldResource.CalculationStatus;
                        newResource.Hours = oldResource.Hours;
                        newResource.Rate = oldResource.Rate;

                        newResource.CreatedBy = oldResource.CreatedBy;
                        //newResource.CreationID = oldResource.CreationID;
                        newResource.CreationDate = oldResource.CreationDate;

                        newResource.ModifiedBy = _view.Username;
                        //newResource.ModificationID = null;
                        newResource.ModificationDate = modificationDate;

                        newResource.Active = oldResource.Active;
                    }
                }

                CS_DPISpecialPricing newSpecialPricing = null;
                IList<CS_DPIManualSpecialPricing> newManualSpecialPricingList = new List<CS_DPIManualSpecialPricing>();
                
                if (_view.ViewSpecialPricing)
                {
                    newSpecialPricing = new CS_DPISpecialPricing();
                    newSpecialPricing.DPIID = _view.DPIId;
                    newSpecialPricing.Type = (int)_view.SpecialPriceType;
                    newSpecialPricing.OverallJobDiscount = _view.OverallJobDiscount;
                    newSpecialPricing.LumpSumValue = _view.LumpSumValue;
                    newSpecialPricing.LumpSumDuration = _view.LumpSumDuration;
                    newSpecialPricing.LumpSumValuePerDay = _view.LumpSumValuePerDay;
                    newSpecialPricing.Notes = _view.SpecialPricingNotes;

                    if (null != dpi.CS_DPISpecialPricing)
                    {
                        newSpecialPricing.CreationDate = dpi.CS_DPISpecialPricing.CreationDate;
                        newSpecialPricing.CreatedBy = dpi.CS_DPISpecialPricing.CreatedBy;
                        newSpecialPricing.CreationID = dpi.CS_DPISpecialPricing.CreationID;
                    }
                    else
                    {
                        newSpecialPricing.CreationDate = modificationDate;
                        newSpecialPricing.CreatedBy = _view.Username;
                        //newSpecialPricing.CreationID = null;
                    }

                    newSpecialPricing.ModificationDate = modificationDate;
                    newSpecialPricing.ModifiedBy = _view.Username;
                    //newSpecialPricing.ModificationID = null;

                    newSpecialPricing.Active = true;

                    if (null != _view.ManualSpecialPricingTable)
                    {
                        for (int i = 0; i < _view.ManualSpecialPricingTable.Count; i++)
                        {
                            CS_DPIManualSpecialPricing oldItem = _view.ManualSpecialPricingTable[i];
                            CS_DPIManualSpecialPricing newItem = new CS_DPIManualSpecialPricing();

                            newItem.ID = oldItem.ID;
                            newItem.DPIId = _view.DPIId;
                            newItem.Description = oldItem.Description;
                            newItem.QtdHrs = oldItem.QtdHrs;
                            newItem.Rate = oldItem.Rate;

                            if (oldItem.ID.Equals(0))
                            {
                                newItem.CreationDate = modificationDate;
                                newItem.CreatedBy = _view.Username;
                                //newItem.CreationID = null;
                            }
                            else
                            {
                                newItem.CreationDate = oldItem.CreationDate;
                                newItem.CreatedBy = oldItem.CreatedBy;
                            }
                            newItem.ModificationDate = modificationDate;
                            newItem.ModifiedBy = _view.Username;
                            //newItem.ModificationID = null;
                            newItem.Active = true;

                            newManualSpecialPricingList.Add(newItem);
                        }
                    }
                }

                _dpiModel.UpdateDPI(dpi, newResourceList, newSpecialPricing, newManualSpecialPricingList);
            }
        }

        public void LoadSpecialPricing()
        {
            CS_DPISpecialPricing dpiSpecialPricing = _dpiModel.GetDPISpecialPricing(_view.DPIId);
            Globals.DPI.SpecialPriceType pricingType = Globals.DPI.SpecialPriceType.NoSpecialPricing;
            decimal overallDiscount = 0, lumpSum = 0, lumpSumPerDay = 0;
            int  lumpSumDuration = 0;
            string notes = "";

            if (null != dpiSpecialPricing && (Globals.DPI.SpecialPriceType)dpiSpecialPricing.Type != Globals.DPI.SpecialPriceType.NoSpecialPricing)
            {
                pricingType = (Globals.DPI.SpecialPriceType)dpiSpecialPricing.Type;
                
                if (dpiSpecialPricing.OverallJobDiscount.HasValue)
                    overallDiscount = dpiSpecialPricing.OverallJobDiscount.Value;

                if (dpiSpecialPricing.LumpSumValue.HasValue)
                    lumpSum = dpiSpecialPricing.LumpSumValue.Value;

                if (dpiSpecialPricing.LumpSumValuePerDay.HasValue)
                    lumpSumPerDay = dpiSpecialPricing.LumpSumValuePerDay.Value;

                if (dpiSpecialPricing.LumpSumDuration.HasValue)
                    lumpSumDuration = dpiSpecialPricing.LumpSumDuration.Value;

                notes = dpiSpecialPricing.Notes;
            }
            else
            {
                dpiSpecialPricing = _dpiModel.GetLastDPISpecialPricing(_view.JobID, _view.DPIId);
                if (null != dpiSpecialPricing && (Globals.DPI.SpecialPriceType)dpiSpecialPricing.Type != Globals.DPI.SpecialPriceType.NoSpecialPricing)
                {
                    pricingType = (Globals.DPI.SpecialPriceType)dpiSpecialPricing.Type;

                    if (dpiSpecialPricing.OverallJobDiscount.HasValue)
                        overallDiscount = dpiSpecialPricing.OverallJobDiscount.Value;

                    if (dpiSpecialPricing.LumpSumValue.HasValue)
                        lumpSum = dpiSpecialPricing.LumpSumValue.Value;

                    if (dpiSpecialPricing.LumpSumValuePerDay.HasValue)
                        lumpSumPerDay = dpiSpecialPricing.LumpSumValuePerDay.Value;

                    if (dpiSpecialPricing.LumpSumDuration.HasValue)
                        lumpSumDuration = dpiSpecialPricing.LumpSumDuration.Value;

                    notes = dpiSpecialPricing.Notes;
                }
                else
                {
                    CS_SpecialPricing jobSpecialPricing = _dpiModel.GetJobSpecialPricing(_view.JobID);

                    if (null != jobSpecialPricing && (Globals.JobRecord.SpecialPriceType)jobSpecialPricing.Type != Globals.JobRecord.SpecialPriceType.NoSpecialPrincing)
                    {
                        switch ((Globals.JobRecord.SpecialPriceType)jobSpecialPricing.Type)
                        {
                            case Globals.JobRecord.SpecialPriceType.NoSpecialPrincing:
                                pricingType = Globals.DPI.SpecialPriceType.NoSpecialPricing;
                                break;
                            case Globals.JobRecord.SpecialPriceType.OverallJobDiscount:
                                pricingType = Globals.DPI.SpecialPriceType.OverallJobDiscount;
                                break;
                            case Globals.JobRecord.SpecialPriceType.LumpSum:
                                pricingType = Globals.DPI.SpecialPriceType.TotalProjectLumpSum;
                                break;
                            case Globals.JobRecord.SpecialPriceType.ManualSpecialPricing:
                                pricingType = Globals.DPI.SpecialPriceType.ManualSpecialPricingCalculation;
                                break;
                            default:
                                break;
                        }

                        if (jobSpecialPricing.OverallJobDiscount.HasValue)
                            overallDiscount = jobSpecialPricing.OverallJobDiscount.Value;

                        if (jobSpecialPricing.LumpsumValue.HasValue)
                            lumpSum = jobSpecialPricing.LumpsumValue.Value;

                        if (jobSpecialPricing.LumpsumValuePerDay.HasValue)
                            lumpSumPerDay = jobSpecialPricing.LumpsumValuePerDay.Value;

                        if (jobSpecialPricing.LumpsumDuration.HasValue)
                            lumpSumDuration = jobSpecialPricing.LumpsumDuration.Value;

                        notes = jobSpecialPricing.Notes;
                    }
                }
            }

            if (pricingType == Globals.DPI.SpecialPriceType.OverallJobDiscount)
            {
                _view.OverallJobDiscount = overallDiscount; 
            }
            else if (pricingType == Globals.DPI.SpecialPriceType.TotalProjectLumpSum)
            {
                _view.LumpSumValue = lumpSum;
                _view.LumpSumValuePerDay = lumpSumPerDay;
                _view.LumpSumDuration = lumpSumDuration;

                // Verify if the job is closed
                CS_Job_JobStatus status = _jobModel.GetJobStartAndCloseDate(_view.JobID);
                if (status.JobStatusId.Equals((int)Globals.JobRecord.JobStatus.Closed))
                {
                    // Calculates the remaining value of the lump sum amount
                    decimal totalRevenue = _dpiModel.GetPreviousTotal(_view.JobID, _view.DPIDate);
                    decimal remainingRevenue = lumpSum - totalRevenue;
                    if (remainingRevenue < 0)
                        remainingRevenue = 0;
                    _view.LumpSumValuePerDay = remainingRevenue;
                }
                
            }
            else if (pricingType == Globals.DPI.SpecialPriceType.ManualSpecialPricingCalculation)
            {
                _view.ManualSpecialPricingTable = _dpiModel.ListDPIManualSpecialPricing(_view.DPIId);
            }

            _view.SpecialPriceType = pricingType;
            _view.SpecialPricingNotes = notes;
        }

        #endregion
    }
}
