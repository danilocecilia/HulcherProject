using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Runtime.Serialization;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.DataContext.VO;


namespace Hulcher.OneSource.CustomerService.Core
{
    /// <summary>
    /// Global class for Enums, Constants and Classes
    /// </summary>
    public class Globals
    {
        #region [ Enums and Constants ]

        /// <summary>
        /// Configuration class to get information from the AppSettings section inside the .config file
        /// </summary>
        public class Configuration
        {
            /// <summary>
            /// Path in webserver to upload files for Permit Information
            /// </summary>
            public static string UploadPermitPath
            {
                get
                {
                    string uploadPermitPath = "";
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("UploadPermitPath")))
                        uploadPermitPath = ConfigurationManager.AppSettings.Get("UploadPermitPath");
                    return uploadPermitPath;
                }
            }

            /// <summary>
            /// Path in Webserver to upload files for Photo Report Information
            /// </summary>
            public static string UploadPhotoReportPath
            {
                get
                {
                    string uploadPhotoReportPath = "";
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("UploadPhotoReportPath")))
                        uploadPhotoReportPath = ConfigurationManager.AppSettings.Get("UploadPhotoReportPath");
                    return uploadPhotoReportPath;
                }
            }

            /// <summary>
            /// Path in Webserver where the AzManStore config file is stored
            /// </summary>
            public static string AzManagerStoreConnectionString
            {
                get
                {
                    string azManStoreConnectionString = "";
                    ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["AzManPolicyStoreConnectionString"];
                    if (null != settings)
                    {
                        if (!string.IsNullOrEmpty(settings.ConnectionString))
                        {
                            // TODO: Get physical path through virtual path
                            azManStoreConnectionString = settings.ConnectionString;
                        }
                    }
                    return azManStoreConnectionString;
                }
            }

            /// <summary>
            /// Enum to identify all settings
            /// </summary>
            public enum Settings
            {
                LastUpdateCustomer = 1,
                LastUpdateContact = 2,
                LastUpdateContract = 3,
                ITEmailOnError = 4,
                LastJobNumber = 5,
                LastNonJobNumber = 6,
                DashboardRefreshRate = 7,
                InterimBillEmails = 8,
                ApplicationDomain = 11,
                EmailConfiguration = 12,
                EstimationTeam = 13,
                InvoicingTeam = 14,
                LastFirstAlertNumber = 15,
                FirstAlertReceiptList = 16,
                PermitTeam = 17,
                TransportationTeam = 18,
                AddressChangeNotification = 19,
                ContactChangeNotification = 21,
                CustomerRequestEmails = 22,
                ContactRequestEmails = 23

            }
        }

        public class Common
        {
            public class Sort
            {
                public enum SortDirection
                {
                    Ascending = 1,
                    Descending
                }

                public enum EquipmentDisplaySortColumns
                {
                    None = 0,
                    UnitType,
                    DivisionName,
                    UnitNumber,
                    UnitDescription
                }

                public enum EquipmentSortColumns
                {
                    None = 0,
                    DivisionName,
                    DivisionState,
                    ComboName,
                    UnitNumber,
                    Descriptor,
                    Status,
                    JobLocation,
                    Type,
                    OperationStatus,
                    JobNumber,
                    UnitType
                }

                /// <summary>
                /// Enum that represents the available coluns to sort the Dashboard (Job Summary)
                /// </summary>
                public enum JobSummarySortColumns
                {
                    None = 0,
                    Division = 1,
                    JobNumber = 2,
                    CustomerResource = 3,
                    JobStatus = 4,
                    Location = 5,
                    ProjectManager = 6,
                    ModifiedBy = 7,
                    LastModification = 8,
                    InitialCallDate = 9,
                    PresetDate = 10,
                    LastCallType = 11,
                    LastCallDate = 12
                }

                /// <summary>
                /// Enum that represents the available coluns to sort the Dashboard (Job Call Log)
                /// </summary>
                public enum JobCallLogSortColumns
                {
                    None = 0,
                    Division = 1,
                    JobNumber = 2,
                    Customer = 3,
                    CallType = 4,
                    CalledInBy = 5,
                    CallDate = 6,
                    CallTime = 7,
                    ModifiedBy = 8,
                    Details = 9
                }

                /// <summary>
                /// Enum that represents the available columns to sort the Regional Maintenance grid
                /// </summary>
                public enum RegionalMaintenanceSortColumns
                {
                    None = 0,
                    Region = 1,
                    Division = 2,
                    EmployeeEquipment = 3
                }

                /// <summary>
                /// Enum that represents the available columns to sort the Customer Maintenance grid
                /// </summary>
                public enum CustomerMaintenanceSortColumns
                {
                    None = 0,
                    Customer = 1,
                    Contact = 2,
                    Type = 3,
                    Location = 4
                }

                /// <summary>
                /// Enum that represents the available columns to sort the Customer Request grid
                /// </summary>
                public enum CustomerRequestSortColumns
                {
                    None = 0,
                    Date = 1,
                    RequestedBy = 2,
                    Type = 3,
                    CustomerContactName = 4,
                    Status = 5
                }
            }

            public class GridView
            {
                /// <summary>
                /// Enum that represents the available columns to sort the Customer Request grid
                /// </summary>
                public enum GridCommandNames
                {
                    Edit = 0,
                    Remove = 1
                }
            }
        }

        public class FirstAlert
        {
            public enum FirstAlertFilters
            {
                AlertNumber = 1,
                IncidentType = 2,
                Date = 3,
                Time = 4,
                JobNumber = 5,
                Division = 6,
                Customer = 7,
                Location = 8,
            }

            public enum MedicalSeverity
            {
                MedicalTreatment = 1,
                FirstAid = 2,
                Fatality = 3,
                NoneOfAbove = 4
            }

            public enum VehiclePosition
            {
                Owner = 1,
                Driver = 2,
                Passenger = 3
            }

            public enum EquipmentFilters
            {
                UnitNumber = 1,
                DivisionName = 2,
                Make = 3
            }

            public enum EmployeeFilters
            {
                Division = 1,
                LastName = 2,
                FirstName = 3,
                Location = 4,
                EmployeeID = 5,
                None = 6
            }
        }

        public class Permitting
        {
            public enum PermittingSortColumns
            {
                ComboUnit = 1,
                CreateDate,
                DivisionName,
                DivisionState,
                Descriptor_Type,
                JobNumber,
                None
            }

            public enum PermitType
            {
                Annual = 1
            }
        }

        public class GeneralLog
        {
            public static int ID
            {
                get { return 1; }
            }
        }

        public class JobRecord
        {
            /// <summary>
            /// Enumerates the Options for Filtering Data
            /// </summary>
            public enum FilterType { None = 0, Date = 1, Time = 2, Type = 3, User = 4 }

            /// <summary>
            /// Enumerates the available Job Types
            /// </summary>
            public enum JobType { JobType1 = 1, JobType2 = 2, JobType3 = 3, DamagePrevention = 5 }

            /// <summary>
            /// Enumerates the available Job Status
            /// </summary>
            public enum JobStatus { Active = 1, Preset = 2, Potential = 3, Lost = 4, Cancelled = 5, Closed = 6, Bid = 7, PresetPurchase = 8 , ClosedHold = 9}

            /// <summary>
            /// Enumerates the available Billing Status
            /// </summary>
            public enum BillingStatus { Created = 1, Working = 2, Done = 3 }

            /// <summary>
            /// Enumerates the available Price Types
            /// </summary>
            public enum PriceType { PublishedRates = 1, BidRate = 2, SpecialRate = 3 }

            /// <summary>
            /// Enumerates the available Special Price types
            /// </summary>
            public enum SpecialPriceType { NoSpecialPrincing = 0, OverallJobDiscount = 1, LumpSum = 2, ManualSpecialPricing = 3 }

            /// <summary>
            /// Message that is shown when the file size is too big for upload
            /// </summary>
            public static String UploadFileSizeMessage
            {
                get
                {
                    System.Configuration.Configuration config = WebConfigurationManager.OpenWebConfiguration("~", "", "JobRecord.aspx");
                    HttpRuntimeSection section = config.GetSection("system.web/httpRuntime") as HttpRuntimeSection;
                    double maxFileSize = Math.Round(section.MaxRequestLength / 1024.0, 1);
                    return string.Format("The file size is too big for upload. The size limit is {0:0.#} MB.", maxFileSize);
                }
            }

            /// <summary>
            /// 
            /// </summary>
            public enum CallEntryFilter { JobNumber = 1, Division, Customer, Location }

            #region [ Comparer Classes ]

            public class EmployeeComparer : IComparer<CS_Division>
            {
                public int Compare(CS_Division x, CS_Division y)
                {
                    if (x != null && y != null)
                        return x.Name.CompareTo(y.Name);
                    else if (x == null && y != null)
                        return string.Empty.CompareTo(y.Name);
                    else if (x != null && y == null)
                        return x.Name.CompareTo(string.Empty);
                    else
                        return string.Empty.CompareTo(string.Empty);
                }
            }           

            #endregion
        }

        public class QuickReference
        {
            #region [ Comparer Classes ]

            public class CS_View_EquipmentInfo_Equipment_Comparer : IEqualityComparer<CS_View_EquipmentInfo>
            {
                // Products are equal if their names and product numbers are equal.
                public bool Equals(CS_View_EquipmentInfo x, CS_View_EquipmentInfo y)
                {
                    //Check whether the compared objects reference the same data.
                    return (x.EquipmentID.Equals(y.EquipmentID));
                }

                // If Equals() returns true for a pair of objects 
                // then GetHashCode() must return the same value for these objects.
                public int GetHashCode(CS_View_EquipmentInfo _CS_View_EquipmentInfo)
                {
                    //Check whether the object is null
                    if (Object.ReferenceEquals(_CS_View_EquipmentInfo, null))
                        return 0;

                    //Get hash code for the Name field if it is not null.
                    return _CS_View_EquipmentInfo.EquipmentID.GetHashCode();
                }

            }

            public class CS_View_EquipmentInfo_Combo_Comparer : IEqualityComparer<CS_View_EquipmentInfo>
            {
                // Products are equal if their names and product numbers are equal.
                public bool Equals(CS_View_EquipmentInfo x, CS_View_EquipmentInfo y)
                {
                    //Check whether the compared objects reference the same data.
                    return (x.ComboID.Equals(y.ComboID));
                }

                // If Equals() returns true for a pair of objects 
                // then GetHashCode() must return the same value for these objects.
                public int GetHashCode(CS_View_EquipmentInfo _CS_View_EquipmentInfo)
                {
                    //Check whether the object is null
                    if (Object.ReferenceEquals(_CS_View_EquipmentInfo, null))
                        return 0;

                    //Get hash code for the Name field if it is not null.
                    return _CS_View_EquipmentInfo.ComboID.GetHashCode();
                }

            }
            #endregion
        }

        public class ResourceAllocation
        {
            public enum EmployeeFilters { Division = 1, DivisionState, Status, JobNumber, Position, Employee, DivisionStateAcronym }

            public enum EquipmentFilters { Division = 1, DivisionState, UnitNumber, ComboNumber, Status, JobLocation, CallType, JobNumber, DivisionStateAcronym }

            public enum Type { AddEquipment = 1, AddEmployee = 2, ReserveEquipment = 3, ReserveEmployee = 4 }

            public class CS_Resource_Comparer : IEqualityComparer<CS_Resource>
            {
                // Products are equal if their names and product numbers are equal.
                public bool Equals(CS_Resource x, CS_Resource y)
                {
                    //Check whether the compared objects reference the same data.
                    return (x.StartDateTime.Date.Equals(y.StartDateTime.Date));
                }

                // If Equals() returns true for a pair of objects 
                // then GetHashCode() must return the same value for these objects.
                public int GetHashCode(CS_Resource obj)
                {
                    //Check whether the object is null
                    if (Object.ReferenceEquals(obj, null))
                        return 0;

                    //Get hash code for the Name field if it is not null.
                    return obj.StartDateTime.Date.GetHashCode();
                }
            }
            public enum ResourceType { Equipment = 1, Employee }
        }

        public class RegionalMaintenance
        {
            /// <summary>
            /// Enumerates the Options for Filtering Data
            /// </summary>
            public enum FilterType
            {
                None = 0,
                Region = 1,
                RVP = 2,
                Division = 3,
                EmployeeName = 4,
                ComboName = 5,
                EquipmentUnitNumber = 6
            }

            public class CS_EmployeeByDivision_Comparer : IEqualityComparer<CS_Employee>
            {
                // Products are equal if their names and product numbers are equal.
                public bool Equals(CS_Employee x, CS_Employee y)
                {
                    if (null != x.CS_Division && null != y.CS_Division)
                        //Check whether the compared objects reference the same data.
                        return (x.CS_Division.ID.Equals(y.CS_Division.ID));
                    else
                        return false;
                }

                // If Equals() returns true for a pair of objects 
                // then GetHashCode() must return the same value for these objects.
                public int GetHashCode(CS_Employee obj)
                {
                    //Check whether the object is null
                    if (Object.ReferenceEquals(obj.CS_Division, null))
                        return 0;

                    //Get hash code for the Name field if it is not null.
                    return obj.CS_Division.ID.GetHashCode();
                }
            }
        }

        public class RouteMaintenance
        {
            /// <summary>
            /// Enumerates the Options for Filtering Data
            /// </summary>
            public enum FilterType
            {
                None = 0,
                State = 1,
                City = 2
            }
        }

        public class Dashboard
        {
            /// <summary>
            /// Enum that describes the available filter types for the Data field in the Dashboard
            /// </summary>
            public enum DateFilterType { None = 0, InitialCallDate = 1, PresetDate, JobStartDate, ModificationDate }

            /// <summary>
            /// Enum that represents the current Viewpoint of the Dashboard
            /// </summary>
            public enum ViewType { None = 0, JobCallLogView = 1, JobSummaryView = 2 }

            #region [ Comparer Classes ]

            public class CS_View_JobCallLog_Comparer : IEqualityComparer<CS_View_JobCallLog>
            {
                // Products are equal if their names and product numbers are equal.
                public bool Equals(CS_View_JobCallLog x, CS_View_JobCallLog y)
                {
                    //Check whether the compared objects reference the same data.
                    return (x.JobId.Equals(y.JobId));
                }

                // If Equals() returns true for a pair of objects 
                // then GetHashCode() must return the same value for these objects.

                public int GetHashCode(CS_View_JobCallLog _CS_View_JobCallLog)
                {
                    //Check whether the object is null
                    if (Object.ReferenceEquals(_CS_View_JobCallLog, null)) return 0;

                    //Get hash code for the Name field if it is not null.
                    return _CS_View_JobCallLog.JobId.GetHashCode();
                }

            }

            #endregion
        }

        public class AutoComplete
        {
            public enum AutoCompleteSource
            {
                Customer = 1,
                DynamicsContact,
                CustomerServiceContact,
                Employee,
                Contact,
                EmployeeWithDivision,
                State,
                City,
                JobAction,
                JobStatus,
                JobStatusJobRecord,
                Division,
                PriceType,
                ZipCode,
                JobNumber,
                JobNumberWithGeneral,
                CallType,
                BillableJobNumber,
                StateByDivision,
                JobNumberByStatus,
                EquipmentType,
                Equipment,
                ProjectManager,
                LocalEquipmentType
            }
        }

        public class Security
        {
            /// <summary>
            /// Employee ID for system operations
            /// </summary>
            public const int SystemEmployeeID = 1;

            /// <summary>
            /// Enum that contains all Security Operations used by AZMan
            /// </summary>
            public enum Operations
            {
                AccessMainPage = 1,
                ListBIDFields = 2,
                ShowJobInfoSections = 3,
                AlterJob = 4,
                CreateJobRecord = 5,
                ManageCallCriteria = 6,
                FirstAlert = 7,
                FirstAlertDelete = 8,
                Permiting = 9,
                Subcontractor = 10,
                CallLogDelete = 11,
                Route = 12,
                ManageCollection=13
            }
        }

        public class CallEntry
        {
            public const string ActionDateFieldName = "dtpDate";
            public const string ActionTimeFieldName = "txtTime";
            public static int[] AutomaticCallEntries;

            public class DynamicFields
            {
                public class EquipmentDown
                {
                    public const string ExpectedUpDate = "dtpExpectedUpDate";
                    public const string ExpectedUpTime = "txtExpectedUpTime";
                }
            }

            public enum typeOfPerson
            {
                Employee = 1,
                Contact = 2,
                Both
            }

            public enum CallType : int
            {
                Advise = 1,
                CheckedCall = 6,
                ETA = 10,
                ATA = 11,
                MissedETA = 12,
                AddedResource = 27,
                ReservedResource = 28,
                InitialAdviseUpdate = 33,
                Released = 20,
                Parked = 21,
                EquipmentDown = 22,
                EquipmentUp = 23,
                LapsedPreset = 38,
                TransferResource = 39,
                FirstAlert = 40,
                DPIApproved = 41,
                OffCall = 42,
                Coverage = 43,
                InitialLog = 44,
                WhiteLight = 45
            }

            public enum PrimaryCallType : int
            {
                JobUpdateNotification = 1,
                ResourceUpdateAddedResources = 2,
                ResourceUpdateEventStatus = 5,
                NonJobUpdateNotification = 8,
                ResourceUpdate = 10
            }

            public class Person
            {
                public typeOfPerson Type { get; set; }
                public int ID { get; set; }
                public string Name { get; set; }
                public string Value
                {
                    get { return string.Format("{0}_{1}", Type.ToString(), ID.ToString()); }
                }
            }

            public enum AutoFillType
            {
                JobCity = 1,
                JobState = 2,
                JobCountry = 3,
                PreviousCallType = 4
            }

            public enum ListType
            {
                Custom = 0,
                //Customer, 
                WorkTimeReleased,
                Country,
                State,
                City,
                Hotel,
                Subcontractor
            }

            public enum CallLogResourceType : int
            {
                Contact = 1, Employee, Equipment
            }

            public enum PrimaryCallTypeCategory : int
            {
                JobUpdate = 1,
                ResourceUpdate = 2
            }

            public enum ResourceFilterType : int
            {
                DivisionNumber = 1,
                Name = 2,
                UnitNumber = 3,
                ComboNumber = 4
            }

            public enum MethodOfContactValue
            {
                InPerson = 1,
                Voicemail = 2,
                Email = 3
            }
        }

        public class CallCriteria
        {
            public enum CallCriteriaLevel
            {
                Division = 1,
                Customer,
                Wide
            }

            /// <summary>
            /// Enum to identify CallCriteriaTypes
            /// </summary>
            public enum CallCriteriaType
            {
                None = 0,
                Customer = 1,
                Division = 2,
                JobStatus = 3,
                PriceType = 4,
                JobCategory = 5,
                JobType = 6,
                JobAction = 7,
                Interimbilling = 8,
                GeneralLog = 9,
                Country = 10,
                State = 11,
                City = 12,
                CarCount = 13,
                Commodities = 14,
                Chemicals = 15,
                HeavyEquipment = 16,
                NonHeavyEquipment = 17,
                AllEquipment = 18,
                CallType = 19,
                WhiteLight = 20
            }

            /// <summary>
            /// Enum to identify CallCriteriaTypes
            /// </summary>
            public enum CallCriteriaEmailStatus
            {
                Pending = 1,
                Sent = 2,
                Error = 3,
                ConfirmationReceived = 4,
                ReadConfirmationReceived = 5
            }

            public enum EmailVOType
            {
                Contact = 1,
                Employee = 2
            }

        }

        public class EmailService
        {
            public enum Status
            {
                Pending = 1,
                Sent = 2,
                Error = 3,
                ConfirmationReceived = 4,
                ConfirmedAndRead = 5
            }

            #region [ Comparer Classes ]

            public class CS_CallLogCallCriteriaEmail_Comparer : IEqualityComparer<CS_CallLogCallCriteriaEmail>
            {
                // Products are equal if their names and product numbers are equal.
                public bool Equals(CS_CallLogCallCriteriaEmail x, CS_CallLogCallCriteriaEmail y)
                {
                    //Check whether the compared objects reference the same data.
                    return (x.Email.Equals(y.Email));
                }

                // If Equals() returns true for a pair of objects 
                // then GetHashCode() must return the same value for these objects.
                public int GetHashCode(CS_CallLogCallCriteriaEmail callLogCallCriteriaEmail)
                {
                    //Check whether the object is null
                    if (Object.ReferenceEquals(callLogCallCriteriaEmail, null))
                        return 0;

                    //Get hash code for the Name field if it is not null.
                    return callLogCallCriteriaEmail.Email.GetHashCode();
                }
            }

            public class EmailVO_Comparer : IEqualityComparer<EmailVO>
            {
                // Products are equal if their names and product numbers are equal.
                public bool Equals(EmailVO x, EmailVO y)
                {
                    //Check whether the compared objects reference the same data.
                    return (x.Email == y.Email);
                }

                // If Equals() returns true for a pair of objects 
                // then GetHashCode() must return the same value for these objects.
                public int GetHashCode(EmailVO EmailVO)
                {
                    //Check whether the object is null
                    if (Object.ReferenceEquals(EmailVO, null))
                        return 0;

                    //Get hash code for the Name field if it is not null.
                    return EmailVO.Email.GetHashCode();
                }
            }

            public class CS_EquipmentPermit_Comparer : IEqualityComparer<CS_EquipmentPermit>
            {
                // Products are equal if their names and product numbers are equal.
                public bool Equals(CS_EquipmentPermit x, CS_EquipmentPermit y)
                {
                    //Check whether the compared objects reference the same data.
                    return (x.Code.Equals(y.Code) && x.LicenseNumber.Equals(y.LicenseNumber));
                }

                // If Equals() returns true for a pair of objects 
                // then GetHashCode() must return the same value for these objects.
                public int GetHashCode(CS_EquipmentPermit _CS_EquipmentPermit)
                {
                    //Check whether the object is null
                    if (Object.ReferenceEquals(_CS_EquipmentPermit, null))
                        return 0;

                    //Get hash code for the Name field if it is not null.
                    return (_CS_EquipmentPermit.Code + _CS_EquipmentPermit.LicenseNumber).GetHashCode();
                }

            }

            #endregion
        }

        public class ShiftTurnoverReport
        {
            public enum ReportView
            {
                QuickReference = 1,
                Job = 2
            }
        }

        public class DPIReport
        {
            public enum ReportView
            {
                NewJobs = 1,
                ContinuingJobs = 2
            }
        }

        public class TransferResource
        {
            public enum TransferType
            {
                Job = 1,
                Specific = 2,
                Division = 3
            }
        }

        public class EquipmentMaintenance
        {
            /// <summary>
            /// List of available filters for finding equipments
            /// </summary>
            public enum FilterType
            {
                None = 0,
                Division = 1,
                DivisionState = 2,
                ComboName = 3,
                UnitNumber = 4,
                Status = 5,
                JobLocation = 6,
                CallType = 7,
                JobNumber = 8,
                UnitType = 9
            }

            /// <summary>
            /// List of equipment status
            /// </summary>
            public enum Status
            {
                Up = 1,
                Down = 2
            }

            public const string WhiteLightStatus = "White Light";

            public class CS_View_EquipmentInfo_Division_Comparer : IEqualityComparer<CS_View_EquipmentInfo>
            {
                // Products are equal if their names and product numbers are equal.
                public bool Equals(CS_View_EquipmentInfo x, CS_View_EquipmentInfo y)
                {
                    //Check whether the compared objects reference the same data.
                    return (x.DivisionID.Equals(y.DivisionID));
                }

                // If Equals() returns true for a pair of objects 
                // then GetHashCode() must return the same value for these objects.
                public int GetHashCode(CS_View_EquipmentInfo _CS_View_EquipmentInfo)
                {
                    //Check whether the object is null
                    if (Object.ReferenceEquals(_CS_View_EquipmentInfo, null))
                        return 0;

                    //Get hash code for the Name field if it is not null.
                    return _CS_View_EquipmentInfo.DivisionID.GetHashCode();
                }

            }

            public class CS_View_EquipmentInfo_Equipment_Comparer : IEqualityComparer<CS_View_EquipmentInfo>
            {
                // Products are equal if their names and product numbers are equal.
                public bool Equals(CS_View_EquipmentInfo x, CS_View_EquipmentInfo y)
                {
                    //Check whether the compared objects reference the same data.
                    return (x.EquipmentID.Equals(y.EquipmentID));
                }

                // If Equals() returns true for a pair of objects 
                // then GetHashCode() must return the same value for these objects.
                public int GetHashCode(CS_View_EquipmentInfo _CS_View_EquipmentInfo)
                {
                    //Check whether the object is null
                    if (Object.ReferenceEquals(_CS_View_EquipmentInfo, null))
                        return 0;

                    //Get hash code for the Name field if it is not null.
                    return _CS_View_EquipmentInfo.EquipmentID.GetHashCode();
                }

            }
        }

        public class Phone
        {
            public enum PhoneType
            {
                Home = 1,
                Cell = 2,
                Fax = 3,
                VMX = 4,
                Extension = 5,
                Pager = 6,
                PINNumber = 7,
                Work = 8
            }

            public enum PhoneFilterType
            {
                None = 0,
                Division = 1,
                Customer = 2,
                Employee = 3
            }
        }

        public class DPI
        {
            public enum NewJobFilterType
            {
                CurrentDate = 1,
                DayBefore = 2
            }

            /// <summary>
            /// List of available filters for finding DPIs
            /// </summary>
            public enum FilterType
            {
                New = 1,
                Continuing = 2,
                Reprocess = 3
            }

            public enum DpiStatus
            {
                Pending = 1,
                DraftSaved = 2,
                Approved = 3
            }

            public enum CallTypeDpiStatus
            {
                Start = 1,
                End = 2
            }

            public enum CalculationStatus
            {
                INSF = 1,
                Done = 2
            }

            public enum RateTable
            {
                PermitRate = 1,
                HotelRate = 2,
                MealRate = 3
            }

            public enum SpecialPriceType
            {
                NoSpecialPricing = 1,
                OverallJobDiscount = 2,
                TotalProjectLumpSum = 3,
                ManualSpecialPricingCalculation = 4
            }

            #region [ Comparer Classes ]

            public class CS_View_DPIInformation_Comparer : IEqualityComparer<CS_View_DPIInformation>
            {
                // Products are equal if their names and product numbers are equal.
                public bool Equals(CS_View_DPIInformation x, CS_View_DPIInformation y)
                {
                    //Check whether the compared objects reference the same data.
                    return (x.JobID.Equals(y.JobID));
                }

                // If Equals() returns true for a pair of objects 
                // then GetHashCode() must return the same value for these objects.

                public int GetHashCode(CS_View_DPIInformation _CS_View_DPIInformation)
                {
                    //Check whether the object is null
                    if (Object.ReferenceEquals(_CS_View_DPIInformation, null)) return 0;

                    //Get hash code for the Name field if it is not null.
                    return _CS_View_DPIInformation.JobID.GetHashCode();
                }

            }

            public class CS_DPIResource_Comparer : IEqualityComparer<CS_DPIResource>
            {
                // Products are equal if their names and product numbers are equal.
                public bool Equals(CS_DPIResource x, CS_DPIResource y)
                {
                    //Check whether the compared objects reference the same data.
                    int xDivision = 0;
                    if (x.EmployeeID.HasValue)
                        xDivision = Convert.ToInt32(x.CS_Employee.DivisionID);
                    else
                        xDivision = x.CS_Equipment.DivisionID;

                    int yDivision = 0;
                    if (y.EmployeeID.HasValue)
                        yDivision = Convert.ToInt32(y.CS_Employee.DivisionID);
                    else
                        yDivision = y.CS_Equipment.DivisionID;

                    return (xDivision.Equals(yDivision));
                }

                // If Equals() returns true for a pair of objects 
                // then GetHashCode() must return the same value for these objects.

                public int GetHashCode(CS_DPIResource _CS_DPIResource)
                {
                    //Check whether the object is null
                    if (Object.ReferenceEquals(_CS_DPIResource, null)) return 0;

                    //Get hash code for the Name field if it is not null.
                    if (_CS_DPIResource.EquipmentID.HasValue)
                        return _CS_DPIResource.CS_Equipment.DivisionID.GetHashCode();
                    else
                        return _CS_DPIResource.CS_Employee.DivisionID.GetHashCode();
                }

            }

            #endregion
        }

        public class CustomerMaintenance
        {
            /// <summary>
            /// Enumerates the Options for View Types
            /// </summary>
            public enum ViewType
            {
                Customer,
                Contact,
                Error
            }

            /// <summary>
            /// Enumerates the Options for Filtering Data
            /// </summary>
            public enum FilterType
            {
                None = 0,
                Customer = 1,
                Contact = 2,
                Location = 3
            }

            /// <summary>
            /// Enumerates the Options for filtering Request Data
            /// </summary>
            public enum RequestFilterType
            {
                None = 0,
                CustomerName = 1,
                ContactName = 2,
                Status = 3
            }

            /// <summary>
            /// Enumerates the Options for Request Status
            /// </summary>
            public enum RequestStatus
            {
                Pending = 1,
                Approved = 2,
                Cancelled = 3
            }

            #region [ Comparer Classes ]

            public class CS_Customer_Comparer : IEqualityComparer<CS_Customer>
            {
                // Products are equal if their names and product numbers are equal.
                public bool Equals(CS_Customer x, CS_Customer y)
                {
                    //Check whether the compared objects reference the same data.
                    return
                        (
                            x.Name == y.Name &&
                            x.Attn == y.Attn &&
                            x.Address1 == y.Address1 &&
                            x.Address2 == y.Address2 &&
                            x.State == y.State &&
                            x.City == y.City &&
                            x.Country == y.Country &&
                            x.Zip == y.Zip &&
                            x.HomePhoneCodeArea == y.HomePhoneCodeArea &&
                            x.Phone == y.Phone &&
                            x.FaxCodeArea == y.FaxCodeArea &&
                            x.Fax == y.Fax &&
                            x.BillThruProject == y.BillThruProject &&
                            x.Email == y.Email &&
                            x.Webpage == y.Webpage &&
                            x.IMAddress == y.IMAddress &&
                            x.BillName == y.BillName &&
                            x.BillAddress1 == y.BillAddress1 &&
                            x.BillAddress2 == y.BillAddress2 &&
                            x.BillAttn == y.BillAttn &&
                            x.BillState == y.BillState &&
                            x.BillCity == y.BillCity &&
                            x.BillCountry == y.BillCountry &&
                            x.BillZip == y.BillZip &&
                            x.BillingHomePhoneCodeArea == y.BillingHomePhoneCodeArea &&
                            x.BillPhone == y.BillPhone &&
                            x.BillFaxCodeArea == y.BillFaxCodeArea &&
                            x.BillFax == y.BillFax &&
                            x.BillSalutation == y.BillSalutation
                        );

                }

                // If Equals() returns true for a pair of objects 
                // then GetHashCode() must return the same value for these objects.
                public int GetHashCode(CS_Customer _customer)
                {
                    //Check whether the object is null
                    if (Object.ReferenceEquals(_customer, null))
                        return 0;

                    //Get hash code for the Name field if it is not null.
                    return _customer.GetHashCode();
                }

            }

            public class CS_Contact_Comparer : IEqualityComparer<CS_Contact>
            {
                // Products are equal if their names and product numbers are equal.
                public bool Equals(CS_Contact x, CS_Contact y)
                {
                    //Check whether the compared objects reference the same data.
                    return
                        (
                            x.Name == y.Name &&
                            x.LastName == y.LastName &&
                            x.ContactNumber == y.ContactNumber &&
                            x.Attn == y.Attn &&
                            x.Address1 == y.Address1 &&
                            x.Address2 == y.Address2 &&
                            x.State == y.State &&
                            x.City == y.City &&
                            x.Country == y.Country &&
                            x.Zip == y.Zip &&
                            x.HomePhoneCodeArea == y.HomePhoneCodeArea &&
                            x.Phone == y.Phone &&
                            x.FaxCodeArea == y.FaxCodeArea &&
                            x.Fax == y.Fax &&
                            x.Email == y.Email &&
                            x.Webpage == y.Webpage &&
                            x.IMAddress == y.IMAddress &&
                            x.DynamicsContact == y.DynamicsContact &&
                            x.BillName == y.BillName &&
                            x.BillAddress1 == y.BillAddress1 &&
                            x.BillAddress2 == y.BillAddress2 &&
                            x.BillAttn == y.BillAttn &&
                            x.BillState == y.BillState &&
                            x.BillCity == y.BillCity &&
                            x.BillCountry == y.BillCountry &&
                            x.BillZip == y.BillZip &&
                            x.BillingHomePhoneCodeArea == y.BillingHomePhoneCodeArea &&
                            x.BillPhone == y.BillPhone &&
                            x.BillFaxCodeArea == y.BillFaxCodeArea &&
                            x.BillFax == y.BillFax &&
                            x.BillSalutation == y.BillSalutation &&
                            x.BillThruProject == y.BillThruProject
                        );
                }

                // If Equals() returns true for a pair of objects 
                // then GetHashCode() must return the same value for these objects.
                public int GetHashCode(CS_Contact _contact)
                {
                    //Check whether the object is null
                    if (Object.ReferenceEquals(_contact, null))
                        return 0;

                    //Get hash code for the Name field if it is not null.
                    return _contact.GetHashCode();
                }

            }

            #endregion
        }

        public class ProjectCalendar
        {
            public enum ResourceType { ReservedEquipment = 1, ReservedEmployee = 2, AddEquipment = 3, AddEmployee = 4}

            public class ProjectCalendarComparer : IEqualityComparer<ProjectCalendarVO>
            {
                public bool Equals(ProjectCalendarVO x, ProjectCalendarVO y)
                {
                    return x.DivisionID.Equals(y.DivisionID);
                }

                public int GetHashCode(ProjectCalendarVO obj)
                {
                    return obj.DivisionID.GetHashCode();
                }
            }
        }

        #endregion

        #region [ ITemplate Column - For Auto Complete List All ]

        public class DynamicGridViewButtonTemplate : ITemplate
        {
            string _colName;
            string _idName;
            string _displayName;
            string _returnValueId;
            string _returnTextId;
            string _parentFieldId;
            string _returnHfId;

            DataControlRowType _rowType;

            public DynamicGridViewButtonTemplate(string ColName, DataControlRowType RowType)
            {
                _colName = ColName;
                _rowType = RowType;
            }

            public DynamicGridViewButtonTemplate(string ColName, string idName, DataControlRowType RowType)
            {
                _colName = ColName;
                _idName = idName;
                _rowType = RowType;
            }

            public DynamicGridViewButtonTemplate(string displayName, string idName, string returnValueId, string returnTextId, string returnHfId, string parentFieldId, DataControlRowType RowType)
            {
                _displayName = displayName;
                _idName = idName;
                _rowType = RowType;
                _returnValueId = returnValueId;
                _returnTextId = returnTextId;
                _returnHfId = returnHfId;
                _parentFieldId = parentFieldId;
            }

            public DynamicGridViewButtonTemplate(string ColName, string displayName, string idName, string returnValueId, string returnTextId, string returnHfId, string parentFieldId, DataControlRowType RowType)
            {
                _displayName = displayName;
                _idName = idName;
                _rowType = RowType;
                _returnValueId = returnValueId;
                _returnTextId = returnTextId;
                _returnHfId = returnHfId;
                _parentFieldId = parentFieldId;
            }

            public void InstantiateIn(System.Web.UI.Control container)
            {
                switch (_rowType)
                {
                    case DataControlRowType.Header:
                        Literal lc = new Literal();
                        lc.Text = "<b>" + _colName + "</b>";
                        container.Controls.Add(lc);
                        break;

                    case DataControlRowType.DataRow:

                        HyperLink lnk = new HyperLink();
                        lnk.DataBinding += new EventHandler(this.btnDataBind);
                        lnk.ID = "lnkSelect";
                        container.Controls.Add(lnk);

                        break;

                    default:
                        break;
                }
            }


            private void btnDataBind(object sender, EventArgs e)
            {
                HyperLink lnk = sender as HyperLink;
                GridViewRow row = lnk.NamingContainer as GridViewRow;
                string returnValue = string.Empty;

                if (_idName.Contains(","))
                {
                    returnValue = "{0} | {1}";
                    string[] idNameSplit = _idName.Split(',');
                    returnValue = string.Format(returnValue,
                                                DataBinder.Eval(row.DataItem, idNameSplit[0]).ToString(),
                                                (DataBinder.Eval(row.DataItem, idNameSplit[1]) == null ? "" : DataBinder.Eval(row.DataItem, idNameSplit[1]).ToString())
                        );
                }
                else
                {
                    returnValue = DataBinder.Eval(row.DataItem, _idName).ToString();
                }

                lnk.Text = DataBinder.Eval(row.DataItem, _displayName).ToString();
                lnk.NavigateUrl = string.Format("javascript: updateParentPage('{0}','{1}','{2}','{3}','{4}', '{5}');", _returnValueId, _returnTextId, _returnHfId, returnValue, DataBinder.Eval(row.DataItem, _displayName).ToString(), _parentFieldId);
            }
        }

        #endregion

        #region [ Data Contract Classes - for JSON Service ]

        [DataContract]
        [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
        public class SampleDataObject
        {
            public SampleDataObject()
            {
            }
            public SampleDataObject(int NewId, string NewName)
            {
                Id = NewId;
                Name = NewName;
            }
            [DataMember]
            public int Id { get; set; }

            [DataMember]
            public string Name { get; set; }
        }

        [DataContract]
        [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
        public class CollectionCustomer
        {
            [DataMember]
            public bool Collection { get; set; }
        }

        [DataContract]
        [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
        public class OperatorAlertCustomer
        {
            [DataMember]
            public string AlertMessage { get; set; }
        }

        [DataContract]
        [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
        public class ExistingCompany
        {
            [DataMember]
            public bool AlreadyExistsCompany { get; set; }
        }

        public class AutoCompleteDataObject
        {
            public AutoCompleteDataObject()
            {
            }
            public AutoCompleteDataObject(string NewLabel, string NewValue)
            {
                label = NewLabel;
                value = NewValue;
            }
            [DataMember]
            public string label { get; set; }

            [DataMember]
            public string value { get; set; }

            [DataMember]
            public Dictionary<string, string> Parameters { get; set; }
        }

        [DataContract]
        [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
        public class StateAndCountryDataObject
        {
            public StateAndCountryDataObject()
            {
            }
            public StateAndCountryDataObject(int stateId, string stateName, int countryId)
            {
                StateId = stateId;
                StateName = stateName;
                CountryId = countryId;
            }
            [DataMember]
            public int StateId { get; set; }

            [DataMember]
            public string StateName { get; set; }

            [DataMember]
            public int CountryId { get; set; }
        }

        [DataContract]
        [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
        public class StringDataObject
        {
            public StringDataObject()
            {
            }
            public StringDataObject(string NewId, string NewName)
            {
                Id = NewId;
                Name = NewName;
            }
            [DataMember]
            public string Id { get; set; }

            [DataMember]
            public string Name { get; set; }
        }

        [DataContract]
        [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
        public class EmailDataDataObject
        {
            public EmailDataDataObject(string NewInnerHTML)
            {
                InnerHTML = NewInnerHTML;
            }

            [DataMember]
            public string InnerHTML { get; set; }
        }

        [DataContract]
        [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
        public class MapPlotRequestDataObject
        {
            public MapPlotRequestDataObject()
            {
            }

            public MapPlotRequestDataObject(int? jobNumberID, int? customerId, int? stateId, IList<int> divisionList, IList<int> jobActionList, IList<int> jobCategoryList,
                IList<int> priceTypeList, DateTime? creationDate, IList<int> equipmentTypeList, IList<int> regionList,
                string comboNumber, string unitNumber, string employeeName, string employeeTitle)
            {
                JobNumberID = jobNumberID;
                CustomerID = customerId;
                StateID = stateId;
                DivisionList = divisionList;
                JobActionList = jobActionList;
                JobCategoryList = jobCategoryList;
                PriceTypeList = priceTypeList;
                CreationDate = creationDate;
                EquipmentTypeList = equipmentTypeList;
                RegionList = regionList;
                ComboNumber = comboNumber;
                UnitNumber = unitNumber;
                EmployeeName = employeeName;
                EmployeeTitle = employeeTitle;
            }

            [DataMember]
            public int? JobNumberID { get; set; }

            [DataMember]
            public int? CustomerID { get; set; }

            [DataMember]
            public int? StateID { get; set; }

            [DataMember]
            public IList<int> DivisionList { get; set; }

            [DataMember]
            public IList<int> JobActionList { get; set; }

            [DataMember]
            public IList<int> JobCategoryList { get; set; }

            [DataMember]
            public IList<int> PriceTypeList { get; set; }

            [DataMember]
            public DateTime? CreationDate { get; set; }

            [DataMember]
            public IList<int> EquipmentTypeList { get; set; }

            [DataMember]
            public IList<int> RegionList { get; set; }

            [DataMember]
            public string ComboNumber { get; set; }

            [DataMember]
            public string UnitNumber { get; set; }

            [DataMember]
            public string EmployeeName { get; set; }

            [DataMember]
            public string EmployeeTitle { get; set; }
        }

        [DataContract]
        [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
        public class MapPlotDataObject
        {
            public MapPlotDataObject()
            {

            }

            public MapPlotDataObject(double newLatitude, double newLongitude, string newType, string newDescription, string newName)
            {
                Latitude = newLatitude;
                Longitude = newLongitude;
                Type = newType;
                Description = newDescription;
                Name = newName;
            }

            [DataMember]
            public double Latitude { get; set; }

            [DataMember]
            public double Longitude { get; set; }

            [DataMember]
            public string Type { get; set; }

            [DataMember]
            public string Description { get; set; }

            [DataMember]
            public string Name { get; set; }
        }

        [DataContract]
        [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
        public class PresetNotificationDataObject
        {
            public PresetNotificationDataObject()
            {
            }
            public PresetNotificationDataObject(int jobId, string jobNumber, string presetDate)
            {
                JobId = jobId;
                JobNumber = jobNumber;
            }

            [DataMember]
            public int JobId { get; set; }

            [DataMember]
            public string JobNumber { get; set; }

            [DataMember]
            public string PresetDate { get; set; }
        }

        [DataContract]
        [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
        public class EquipmentComboNotificationDataObject
        {
            public EquipmentComboNotificationDataObject()
            {
            }
            public EquipmentComboNotificationDataObject(int comboId, string comboName)
            {
                ComboId = comboId;
                ComboName = comboName;
            }

            [DataMember]
            public int ComboId { get; set; }

            [DataMember]
            public string ComboName { get; set; }
        }

        [DataContract]
        [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
        public class EquipmentOffCallNotificationDataObject
        {
            public EquipmentOffCallNotificationDataObject()
            {
            }

            public EquipmentOffCallNotificationDataObject(int employeeId, string employeeName)
            {
                EmployeeId = employeeId;
                EmployeeName = employeeName;
            }

            [DataMember]
            public int EmployeeId { get; set; }

            [DataMember]
            public string EmployeeName { get; set; }

            [DataMember]
            public string OffCallDate { get; set; }
        }

        [DataContract]
        [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
        public class EquipmentPermitNotificationDataObject
        {
            public EquipmentPermitNotificationDataObject()
            {
            }
            public EquipmentPermitNotificationDataObject(int permitId, string permitNumber, string equipmentName)
            {
                PermitId = permitId;
                PermitNumber = permitNumber;
                EquipmentName = equipmentName;
            }

            [DataMember]
            public int PermitId { get; set; }

            [DataMember]
            public string PermitNumber { get; set; }

            [DataMember]
            public string EquipmentName { get; set; }

            [DataMember]
            public string ExpirationDate { get; set; }
        }

        [DataContract]
        [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
        public class LocalEquipmentTypeDataObject
        {
            public LocalEquipmentTypeDataObject()
            {
            }
            public LocalEquipmentTypeDataObject(int id, string name)
            {
                Id = id;
                Name = name;
            }

            [DataMember]
            public int Id { get; set; }

            [DataMember]
            public string Name { get; set; }
        }

        #endregion
    }
}
