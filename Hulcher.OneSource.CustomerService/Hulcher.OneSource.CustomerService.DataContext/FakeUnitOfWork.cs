using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data.Objects.DataClasses;

namespace Hulcher.OneSource.CustomerService.DataContext
{
    public class FakeUnitOfWork : IUnitOfWork
    {
        public System.Data.Objects.ObjectContext Context
        {
            get
            {
                return null;
            }
            set
            {
                
            }
        }

        public void Save()
        {
            
        }

        private bool _lazyLoadingEnabled;
        public bool LazyLoadingEnabled
        {
            get
            {
                return _lazyLoadingEnabled;
            }
            set
            {
                _lazyLoadingEnabled = value;
            }
        }

        private bool _proxyCreationEnabled;
        public bool ProxyCreationEnabled
        {
            get
            {
                return _proxyCreationEnabled;
            }
            set
            {
                _proxyCreationEnabled = value;
            }
        }

        private string _connectionString;
        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }

        private FakeObjectSet<CS_CallLog> CreateFakeCallLog()
        {
            FakeObjectSet<CS_CallLog> returnItem = new FakeObjectSet<CS_CallLog>();
            returnItem.AddObject(new CS_CallLog() { ID = 1, Active = true, JobID = 1, CallTypeID = 1, CS_CallType = new CS_CallType() { ID = 1, CallCriteria = true } });
            returnItem.AddObject(new CS_CallLog() { ID = 2, Active = true, JobID = 1, CallTypeID = 2, CS_CallType = new CS_CallType() { ID = 2, CallCriteria = false } });
            return returnItem;
        }

        private FakeObjectSet<CS_CallType> CreateFakeCallType()
        {
            FakeObjectSet<CS_CallType> returnItem = new FakeObjectSet<CS_CallType>();
            returnItem.AddObject(new CS_CallType() { ID = 1, Active = true, Description = "CallType1", CS_PrimaryCallType_CallType = new EntityCollection<CS_PrimaryCallType_CallType>() { new CS_PrimaryCallType_CallType() { ID = 1, CallTypeID = 1, PrimaryCallTypeID = 1 } } });
            returnItem.AddObject(new CS_CallType() { ID = 2, Active = true, Description = "CallType1", CS_PrimaryCallType_CallType = new EntityCollection<CS_PrimaryCallType_CallType>() { new CS_PrimaryCallType_CallType() { ID = 2, CallTypeID = 2, PrimaryCallTypeID = 1 } } });
            returnItem.AddObject(new CS_CallType() { ID = 3, Active = false, Description = "CallType1", CS_PrimaryCallType_CallType = new EntityCollection<CS_PrimaryCallType_CallType>() { new CS_PrimaryCallType_CallType() { ID = 3, CallTypeID = 3, PrimaryCallTypeID = 1 } } });
            returnItem.AddObject(new CS_CallType() { ID = 4, Active = true, Description = "CallType1", IsAutomaticProcess = false, CS_PrimaryCallType_CallType = new EntityCollection<CS_PrimaryCallType_CallType>() { new CS_PrimaryCallType_CallType() { ID = 4, CallTypeID = 4, PrimaryCallTypeID = 2 } } });
            returnItem.AddObject(new CS_CallType() { ID = 5, Active = true, Description = "CallType1", IsAutomaticProcess = false, CS_PrimaryCallType_CallType = new EntityCollection<CS_PrimaryCallType_CallType>() { new CS_PrimaryCallType_CallType() { ID = 5, CallTypeID = 5, PrimaryCallTypeID = 2 } } });
            returnItem.AddObject(new CS_CallType() { ID = 6, Active = true, Description = "CallType1", IsAutomaticProcess = true, CS_PrimaryCallType_CallType = new EntityCollection<CS_PrimaryCallType_CallType>() { new CS_PrimaryCallType_CallType() { ID = 6, CallTypeID = 6, PrimaryCallTypeID = 2 } } });
            return returnItem;
        }

        private FakeObjectSet<CS_PrimaryCallType> CreateFakePrimaryCallType()
        {
            FakeObjectSet<CS_PrimaryCallType> returnItem = new FakeObjectSet<CS_PrimaryCallType>();
            returnItem.AddObject(new CS_PrimaryCallType() { ID = 1, JobRelated = true, Active = true, Type="teste1" });
            returnItem.AddObject(new CS_PrimaryCallType() { ID = 2, JobRelated = true, Active = true, Type = "teste2" });
            returnItem.AddObject(new CS_PrimaryCallType() { ID = 3, JobRelated = false, Active = true, Type = "teste3" });
            returnItem.AddObject(new CS_PrimaryCallType() { ID = 4, JobRelated = false, Active = true, Type = "teste4", PrimaryCallTypeCategory=2 });
            return returnItem;
        }

        private FakeObjectSet<CS_View_Employee_CallLogInfo> CreateFakeViewEmployeeCallLogInfo()
        {
            FakeObjectSet<CS_View_Employee_CallLogInfo> returnItem = new FakeObjectSet<CS_View_Employee_CallLogInfo>();
            returnItem.AddObject(new CS_View_Employee_CallLogInfo() { EmployeeID = 1, Active = true });
            returnItem.AddObject(new CS_View_Employee_CallLogInfo() { EmployeeID = 2, Active = true });
            returnItem.AddObject(new CS_View_Employee_CallLogInfo() { EmployeeID = 3, Active = false });
            return returnItem;
        }
        
        private FakeObjectSet<CS_JobStatus> CreateFakeJobStatus()
        {
            FakeObjectSet<CS_JobStatus> returnItem = new FakeObjectSet<CS_JobStatus>();
            returnItem.AddObject(new CS_JobStatus() { ID = 1, Description = "status1", Active = true });
            returnItem.AddObject(new CS_JobStatus() { ID = 2, Description = "status2", Active = true });
            return returnItem;
        }
        

        private FakeObjectSet<CS_Job> CreateFakeJob()
        {
            FakeObjectSet<CS_Job> returnItem = new FakeObjectSet<CS_Job>();
            returnItem.AddObject(
                new CS_Job()
                {
                    ID = 1,
                    Active = true,
                    Number = "fake",
                    CS_CustomerInfo = new CS_CustomerInfo()
                    {
                        JobId = 1,
                        Active = true,
                        CustomerId = 2,
                        //IsCustomer = true,
                        InitialCustomerContactId = 1,
                        CS_Customer = new CS_Customer() { ID = 2, Active = true, Name = "no name" },
                        CS_Contact3 = new CS_Contact() { ID = 1, Name = "no name", Active = true }
                    },
                    CS_JobInfo = new CS_JobInfo()
                    {
                        JobID = 1,
                        Active = true,
                        InitialCallDate = DateTime.Now,
                        InitialCallTime = TimeSpan.Zero,
                        InterimBill = false,
                        EmployeeID = 1,
                        JobActionID = 1,
                        JobCategoryID = 1,
                        JobTypeID = 1,
                        PriceTypeID = 1,
                        CS_Employee = new CS_Employee() { ID = 1, Name = "no name", Active = true },
                        CS_JobAction = new CS_JobAction() { ID = 1, Description = "no name", Active = true },
                        CS_JobCategory = new CS_JobCategory() { ID = 1, Description = "no name", Active = true },
                        CS_JobType = new CS_JobType() { ID = 1, Description = "no name", Active = true },
                        CS_PriceType = new CS_PriceType() { ID = 1, Description = "no name", Active = true },
                        CS_Job_JobStatus = new EntityCollection<CS_Job_JobStatus>()
                        {
                            new CS_Job_JobStatus() { JobID = 1, JobStatusId = 1, Active = true }
                        }
                    },
                    CS_JobDivision = new EntityCollection<CS_JobDivision>() 
                    { 
                        new CS_JobDivision() { Active = true, JobID = 1, DivisionID = 2, CS_Division = new CS_Division() { ID = 2, Name = "no name" } } 
                    },
                    CS_LocationInfo = new CS_LocationInfo()
                    {
                        JobID = 1,
                        CityID = 2,
                        StateID = 2,
                        CountryID = 2,
                        ZipCodeId = 2,
                        CS_City = new CS_City() { ID = 2, Name = "no name" },
                        CS_State = new CS_State() { ID = 2, Name = "no name" },
                        CS_Country = new CS_Country() { ID = 2, Name = "no name" },
                        CS_ZipCode = new CS_ZipCode() { ID = 2, Name = "no name" }
                    },
                    CS_JobDescription = new CS_JobDescription()
                    {
                        JobId = 1
                    },
                    CS_ScopeOfWork = new EntityCollection<CS_ScopeOfWork>()
                    {
                        new CS_ScopeOfWork() { ID = 1, JobId = 1, ScopeOfWork = "no description", Active = true }
                    }
                });

            returnItem.AddObject(
                new CS_Job() 
                { 
                    ID = 2, Active = true, Number = "no number",
                    CS_CustomerInfo = new CS_CustomerInfo() { JobId = 2, Active = true, CustomerId = 1, CS_Customer = new CS_Customer() { ID = 1, Active = true, Name = "customer" } },
                    CS_JobDivision = new EntityCollection<CS_JobDivision>() 
                    { 
                        new CS_JobDivision() { Active = true, JobID = 2, DivisionID = 2, CS_Division = new CS_Division() { ID = 2, Name = "no name" } } 
                    },
                    CS_LocationInfo = new CS_LocationInfo()
                    {
                        JobID = 2,
                        CityID = 2,
                        StateID = 2,
                        CS_City = new CS_City() { ID = 2, Name = "no name" },
                        CS_State = new CS_State() { ID = 2, Name = "no name" }
                    },
                    CS_JobInfo = new CS_JobInfo()
                    {
                        JobID = 1,
                        Active = true,
                        InitialCallDate = DateTime.Now,
                        InitialCallTime = TimeSpan.Zero,
                        InterimBill = false,
                        EmployeeID = 1,
                        JobActionID = 1,
                        JobCategoryID = 1,
                        JobTypeID = 1,
                        PriceTypeID = 1,
                        CS_Employee = new CS_Employee() { ID = 1, Name = "no name", Active = true },
                        CS_JobAction = new CS_JobAction() { ID = 1, Description = "no name", Active = true },
                        CS_JobCategory = new CS_JobCategory() { ID = 1, Description = "no name", Active = true },
                        CS_JobType = new CS_JobType() { ID = 1, Description = "no name", Active = true },
                        CS_PriceType = new CS_PriceType() { ID = 1, Description = "no name", Active = true },
                        CS_Job_JobStatus = new EntityCollection<CS_Job_JobStatus>()
                        {
                            new CS_Job_JobStatus() { JobID = 2, JobStatusId = 1, Active = true }
                        }
                    }
                });
            returnItem.AddObject(
                new CS_Job() 
                { 
                    ID = 3, Active = true, Number = "no number",
                    CS_CustomerInfo = new CS_CustomerInfo() { JobId = 3, Active = true, CustomerId = 2,  CS_Customer = new CS_Customer() { ID = 2, Active = true, Name = "no name" } },
                    CS_JobDivision = new EntityCollection<CS_JobDivision>() 
                    { 
                        new CS_JobDivision() { Active = true, JobID = 3, DivisionID = 1, CS_Division = new CS_Division() { ID = 1, Name = "division" } } 
                    }, 
                    CS_LocationInfo = new CS_LocationInfo()
                    {
                        JobID = 3,
                        CityID = 2,
                        StateID = 2,
                        CS_City = new CS_City() { ID = 2, Name = "no name" },
                        CS_State = new CS_State() { ID = 2, Name = "no name" }
                    },
                    CS_JobInfo = new CS_JobInfo()
                    {
                        JobID = 3,
                        Active = true,
                        InitialCallDate = DateTime.Now,
                        InitialCallTime = TimeSpan.Zero,
                        InterimBill = false,
                        EmployeeID = 1,
                        JobActionID = 1,
                        JobCategoryID = 1,
                        JobTypeID = 1,
                        PriceTypeID = 1,
                        CS_Employee = new CS_Employee() { ID = 1, Name = "no name", Active = true },
                        CS_JobAction = new CS_JobAction() { ID = 1, Description = "no name", Active = true },
                        CS_JobCategory = new CS_JobCategory() { ID = 1, Description = "no name", Active = true },
                        CS_JobType = new CS_JobType() { ID = 1, Description = "no name", Active = true },
                        CS_PriceType = new CS_PriceType() { ID = 1, Description = "no name", Active = true },
                        CS_Job_JobStatus = new EntityCollection<CS_Job_JobStatus>()
                        {
                            new CS_Job_JobStatus() { JobID = 3, JobStatusId = 1, Active = true }
                        }
                    }
                });
            returnItem.AddObject(
                new CS_Job()
                {
                    ID = 4,
                    Active = true,
                    Number = "no number",
                    CS_CustomerInfo = new CS_CustomerInfo() { JobId = 4, Active = true, CustomerId = 2, CS_Customer = new CS_Customer() { ID = 2, Active = true, Name = "no name" } },
                    CS_JobDivision = new EntityCollection<CS_JobDivision>() 
                    { 
                        new CS_JobDivision() { Active = true, JobID = 4, DivisionID = 2, CS_Division = new CS_Division() { ID = 2, Name = "no name" } } 
                    },
                    CS_LocationInfo = new CS_LocationInfo()
                    {
                        JobID = 4,
                        CityID = 1,
                        StateID = 2,
                        CS_City = new CS_City() { ID = 1, Name = "new york" },
                        CS_State = new CS_State() { ID = 2, Name = "no name" }
                    },
                    CS_JobInfo = new CS_JobInfo()
                    {
                        JobID = 4,
                        Active = true,
                        InitialCallDate = DateTime.Now,
                        InitialCallTime = TimeSpan.Zero,
                        InterimBill = false,
                        EmployeeID = 1,
                        JobActionID = 1,
                        JobCategoryID = 1,
                        JobTypeID = 1,
                        PriceTypeID = 1,
                        CS_Employee = new CS_Employee() { ID = 1, Name = "no name", Active = true },
                        CS_JobAction = new CS_JobAction() { ID = 1, Description = "no name", Active = true },
                        CS_JobCategory = new CS_JobCategory() { ID = 1, Description = "no name", Active = true },
                        CS_JobType = new CS_JobType() { ID = 1, Description = "no name", Active = true },
                        CS_PriceType = new CS_PriceType() { ID = 1, Description = "no name", Active = true },
                        CS_Job_JobStatus = new EntityCollection<CS_Job_JobStatus>()
                        {
                            new CS_Job_JobStatus() { JobID = 4, JobStatusId = 1, Active = true }
                        }
                    }
                });
            returnItem.AddObject(
                new CS_Job()
                {
                    ID=9,
                    Active = true,
                    Internal_Tracking = "12345daniloInternalTracking",
                    CS_CustomerInfo = new CS_CustomerInfo() { JobId = 4, Active = true, CustomerId = 2, CS_Customer = new CS_Customer() { ID = 2, Active = true, Name = "no name" } },
                    CS_JobDivision = new EntityCollection<CS_JobDivision>() 
                    { 
                        new CS_JobDivision() { Active = true, JobID = 2, DivisionID = 2, CS_Division = new CS_Division() { ID = 2, Name = "no name" } } 
                    },
                    CS_LocationInfo = new CS_LocationInfo()
                    {
                        JobID = 9,
                        CityID = 1,
                        StateID = 2,
                        CS_City = new CS_City() { ID = 1, Name = "no name" },
                        CS_State = new CS_State() { ID = 2, Name = "no name" }
                    },
                    CS_JobInfo = new CS_JobInfo()
                    {
                        JobID = 9,
                        Active = true,
                        InitialCallDate = DateTime.Now,
                        InitialCallTime = TimeSpan.Zero,
                        InterimBill = false,
                        EmployeeID = 1,
                        JobActionID = 1,
                        JobCategoryID = 1,
                        JobTypeID = 1,
                        PriceTypeID = 1,
                        CS_Employee = new CS_Employee() { ID = 1, Name = "no name", Active = true },
                        CS_JobAction = new CS_JobAction() { ID = 1, Description = "no name", Active = true },
                        CS_JobCategory = new CS_JobCategory() { ID = 1, Description = "no name", Active = true },
                        CS_JobType = new CS_JobType() { ID = 1, Description = "no name", Active = true },
                        CS_PriceType = new CS_PriceType() { ID = 1, Description = "no name", Active = true },
                        CS_Job_JobStatus = new EntityCollection<CS_Job_JobStatus>()
                        {
                            new CS_Job_JobStatus() { JobID = 9, JobStatusId = 1, Active = true }
                        }
                    }
                });
            returnItem.AddObject(
             new CS_Job()
             {
                 ID = 10,
                 Active = true,
                 Number = "12345daniloJobNumber",
                 CS_CustomerInfo = new CS_CustomerInfo() { JobId = 4, Active = true, CustomerId = 2, CS_Customer = new CS_Customer() { ID = 2, Active = true, Name = "no name" } },
                 CS_JobDivision = new EntityCollection<CS_JobDivision>() 
                    { 
                        new CS_JobDivision() { Active = true, JobID = 2, DivisionID = 2, CS_Division = new CS_Division() { ID = 2, Name = "no name" } } 
                    },
                 CS_LocationInfo = new CS_LocationInfo()
                 {
                     JobID = 10,
                     CityID = 1,
                     StateID = 2,
                     CS_City = new CS_City() { ID = 1, Name = "no name" },
                     CS_State = new CS_State() { ID = 2, Name = "no name" }
                 },
                 CS_JobInfo = new CS_JobInfo()
                 {
                     JobID = 10,
                     Active = true,
                     InitialCallDate = DateTime.Now,
                     InitialCallTime = TimeSpan.Zero,
                     InterimBill = false,
                     EmployeeID = 1,
                     JobActionID = 1,
                     JobCategoryID = 1,
                     JobTypeID = 1,
                     PriceTypeID = 1,
                     CS_Employee = new CS_Employee() { ID = 1, Name = "no name", Active = true },
                     CS_JobAction = new CS_JobAction() { ID = 1, Description = "no name", Active = true },
                     CS_JobCategory = new CS_JobCategory() { ID = 1, Description = "no name", Active = true },
                     CS_JobType = new CS_JobType() { ID = 1, Description = "no name", Active = true },
                     CS_PriceType = new CS_PriceType() { ID = 1, Description = "no name", Active = true },
                     CS_Job_JobStatus = new EntityCollection<CS_Job_JobStatus>()
                        {
                            new CS_Job_JobStatus() { JobID = 10, JobStatusId = 1, Active = true }
                        }
                 }
             });
            return returnItem;
        }

        private FakeObjectSet<CS_Settings> CreateFakeSettings()
        {
            FakeObjectSet<CS_Settings> returnItem = new FakeObjectSet<CS_Settings>();
            //LastUpdateCustomer = 1
            returnItem.AddObject(new CS_Settings { ID = 1, Value = new DateTime(2011, 02, 04).ToString("dd/MM/yyyy HH:mm:ss") });
            //LastUpdateContact = 2
            returnItem.AddObject(new CS_Settings { ID = 2, Value = new DateTime(2011, 02, 04).ToString("dd/MM/yyyy HH:mm:ss") });
            //LastUpdateContract = 3
            returnItem.AddObject(new CS_Settings { ID = 3, Value = new DateTime(2011, 02, 04).ToString("dd/MM/yyyy HH:mm:ss") });
            //ITEmailOnError = 4
            returnItem.AddObject(new CS_Settings { ID = 4, Value = "cburton@hulcher.com" });
            //LastJobNumber = 5
            returnItem.AddObject(new CS_Settings { ID = 5, Value = "1" });
            //LastNonJobNumber = 6
            returnItem.AddObject(new CS_Settings { ID = 6, Value = "1" });
            //LastFirstAlertNumber = 15
            returnItem.AddObject(new CS_Settings { ID = 15, Value = "1" });
            return returnItem;
        }

        private FakeObjectSet<CS_Resource> CreateFakeResource()
        {
            FakeObjectSet<CS_Resource> returnItem = new FakeObjectSet<CS_Resource>();
                returnItem.AddObject(new CS_Resource()
                {
                    Active = true, CreatedBy = "Load", CreationDate = DateTime.Now, Description = "abc123", Duration = 1, EmployeeID = 1, EquipmentID=1, ID =1,JobID=5,
                    ModificationDate = DateTime.Now, ModifiedBy = "Load", StartDateTime = DateTime.Now, Type = 1
                });
                returnItem.AddObject(new CS_Resource()
                {
                    Active = true, CreatedBy = "Load", CreationDate = DateTime.Now, Description = "xyz789", Duration = 1, EmployeeID = 1, EquipmentID = 1, ID = 2, JobID = 5,
                    ModificationDate = DateTime.Now, ModifiedBy = "Load", StartDateTime = DateTime.Now, Type = 1
                });
                returnItem.AddObject(new CS_Resource()
                {
                    Active = true, CreatedBy = "Load", CreationDate = DateTime.Now, Description = "def234", Duration = 1, EmployeeID = 1, EquipmentID = 1, ID = 3, JobID = 4,
                    ModificationDate = DateTime.Now, ModifiedBy = "Load", StartDateTime = DateTime.Now, Type = 1
                });
                return returnItem;
        }

        private FakeObjectSet<CS_View_Resource_CallLogInfo> CreateFakeCallLogInfo()
        {
            FakeObjectSet<CS_View_Resource_CallLogInfo> returnItem = new FakeObjectSet<CS_View_Resource_CallLogInfo>();
            returnItem.AddObject(new CS_View_Resource_CallLogInfo() { Active= true, JobID = 5 });
            returnItem.AddObject(new CS_View_Resource_CallLogInfo() { Active = true, JobID = 4 });
            returnItem.AddObject(new CS_View_Resource_CallLogInfo() { Active = true, JobID = 5 });

            return returnItem;
        }

        private FakeObjectSet<CS_Employee> CreateFakeEmployee()
        {
            FakeObjectSet<CS_Employee> returnItem = new FakeObjectSet<CS_Employee>();
            returnItem.AddObject(new CS_Employee() { Active = true, ID = 5 });
            returnItem.AddObject(new CS_Employee() { Active = true, ID = 4 });
            returnItem.AddObject(new CS_Employee() { Active = true, ID = 5 });

            return returnItem;
        }

        private FakeObjectSet<CS_Contact> CreateFakeContact()
        {
            FakeObjectSet<CS_Contact> returnItem = new FakeObjectSet<CS_Contact>();
            returnItem.AddObject(
                new CS_Contact() 
                { 
                    Active = true, ID = 1, DynamicsContact = true,
                    CS_Customer_Contact = new EntityCollection<CS_Customer_Contact>() 
                    { 
                        new CS_Customer_Contact()
                        { 
                            ContactID = 1, CustomerID = 1, Active = true,
                            CS_Customer = new CS_Customer() { ID = 1, Active = true, CS_CustomerInfo = new EntityCollection<CS_CustomerInfo>(){ new CS_CustomerInfo() { JobId = 1}} }
                        } 
                    } 
                });

            returnItem.AddObject(
                new CS_Contact()
                {
                    Active = true, ID = 2, DynamicsContact = true,
                    CS_Customer_Contact = new EntityCollection<CS_Customer_Contact>() 
                    { 
                        new CS_Customer_Contact()
                        { 
                            ContactID = 2, CustomerID = 1, Active = true,
                            CS_Customer = new CS_Customer() { ID = 1, Active = true, CS_CustomerInfo = new EntityCollection<CS_CustomerInfo>(){ new CS_CustomerInfo() { JobId = 1}} }
                        } 
                    }
                });

            return returnItem;
        }

        private FakeObjectSet<CS_Division> CreateFakeDivision()
        {
            FakeObjectSet<CS_Division> returnItem = new FakeObjectSet<CS_Division>();

            returnItem.AddObject(
                new CS_Division()
                {
                    ID = 1, 
                    Name = "001", 
                    Active = true
                });
            returnItem.AddObject(
                new CS_Division()
                {
                    ID = 2,
                    Name = "002",
                    Active = true
                });
            returnItem.AddObject(
                new CS_Division()
                {
                    ID = 3,
                    Name = "003",
                    Active = true
                });
            returnItem.AddObject(
                new CS_Division()
                {
                    ID = 4,
                    Name = "004",
                    Active = true
                });
            returnItem.AddObject(
                new CS_Division()
                {
                    ID = 5,
                    Name = "005",
                    Active = true
                });

            return returnItem;
        }

        public FakeObjectSet<CS_PriceType> CreateFakePriceType()
        {
            FakeObjectSet<CS_PriceType> returnItem = new FakeObjectSet<CS_PriceType>();

            returnItem.AddObject(
                new CS_PriceType()
                {
                    ID = 1,
                    Description = "A Price Type", 
                    Active = true
                });
            returnItem.AddObject(
                new CS_PriceType()
                {
                    ID = 1,
                    Description = "A 2 Price Type",
                    Active = true
                });
            returnItem.AddObject(
                new CS_PriceType()
                {
                    ID = 1,
                    Description = "Price Type B",
                    Active = true
                });
            returnItem.AddObject(
                new CS_PriceType()
                {
                    ID = 1,
                    Description = "Price Type B 2",
                    Active = true
                });
            returnItem.AddObject(
                new CS_PriceType()
                {
                    ID = 1,
                    Description = "Price Type C",
                    Active = true
                });
            returnItem.AddObject(
                new CS_PriceType()
                {
                    ID = 1,
                    Description = "Price Type C 2",
                    Active = true
                });

            return returnItem;
        }

        public FakeObjectSet<CS_City> CreateFakeCity()
        {
            FakeObjectSet<CS_City> returnItem = new FakeObjectSet<CS_City>();

            returnItem.AddObject(
                new CS_City()
                {
                    ID = 1,
                    Name = "City 1",
                    StateID = 1,
                    Active = true
                });

            returnItem.AddObject(
                new CS_City()
                {
                    ID = 1,
                    Name = "City 1",
                    StateID = 2,
                    Active = true
                });

            returnItem.AddObject(
                new CS_City()
                {
                    ID = 1,
                    Name = "City 2",
                    StateID = 1,
                    Active = true
                });

            returnItem.AddObject(
                new CS_City()
                {
                    ID = 1,
                    Name = "City 3",
                    StateID = 1,
                    Active = true
                });

            returnItem.AddObject(
                new CS_City()
                {
                    ID = 1,
                    Name = "City 4",
                    StateID = 1,
                    Active = true
                });

            return returnItem;
        }

        private FakeObjectSet<CS_JobAction> CreateFakeJobAction()
        {
            FakeObjectSet<CS_JobAction> returnItem = new FakeObjectSet<CS_JobAction>();

            returnItem.AddObject(new CS_JobAction()
                                     {
                                         ID = 1,
                                         Description = "Transfer, Diesel",
                                         CreatedBy = string.Empty,
                                         Active = true,
                                         CreationDate = DateTime.Now,
                                         ModifiedBy = string.Empty,
                                         ModificationDate = DateTime.Now
                                     });
            return returnItem;
        }

        private FakeObjectSet<CS_ZipCode> CreateFakeZipCode()
        {
            FakeObjectSet<CS_ZipCode> returnItem = new FakeObjectSet<CS_ZipCode>();

            returnItem.AddObject(
                new CS_ZipCode()
                {
                    ID = 1,
                    Name = "001",
                    CityId = 1,
                    Active = true
                });

            returnItem.AddObject(
                new CS_ZipCode()
                {
                    ID = 2,
                    Name = "001",
                    CityId = 2,
                    Active = true
                });

            returnItem.AddObject(
                new CS_ZipCode()
                {
                    ID = 3,
                    Name = "002",
                    CityId = 1,
                    Active = true
                });

            returnItem.AddObject(
                new CS_ZipCode()
                {
                    ID = 4,
                    Name = "002",
                    CityId = 2,
                    Active = true
                });

            returnItem.AddObject(
                new CS_ZipCode()
                {
                    ID = 5,
                    Name = "003",
                    CityId = 1,
                    Active = true
                });

            return returnItem;
        }

        private FakeObjectSet<CS_JobCategory> CreateFakeJobCategory()
        {
            FakeObjectSet<CS_JobCategory> returnItem = new FakeObjectSet<CS_JobCategory>();

            returnItem.AddObject(
                new CS_JobCategory()
                {
                    ID = 1,
                    Description = "no name",
                    Active = true,
                    CS_JobType = new EntityCollection<CS_JobType>()
                    {
                        new CS_JobType()
                        {
                            ID = 1,
                            Description = "no name",
                            Active = true,
                            CS_JobAction = new EntityCollection<CS_JobAction>()
                            {
                                new CS_JobAction()
                                {
                                    ID = 1,
                                    Description = "no name",
                                    Active = true
                                },
                                new CS_JobAction()
                                {
                                    ID = 2,
                                    Description = "no name",
                                    Active = true
                                }
                            }
                        },
                        new CS_JobType()
                        {
                            ID = 2,
                            Description = "no name",
                            Active = true,
                            CS_JobAction = new EntityCollection<CS_JobAction>()
                            {
                                new CS_JobAction()
                                {
                                    ID = 3,
                                    Description = "no name",
                                    Active = true
                                },
                                new CS_JobAction()
                                {
                                    ID = 4,
                                    Description = "no name",
                                    Active = true
                                }
                            }
                        }
                    }
                });

            return returnItem;
        }

        private FakeObjectSet<CS_JobType> CreateFakeJobType()
        {
            FakeObjectSet<CS_JobType> returnItem = new FakeObjectSet<CS_JobType>();

            returnItem.AddObject(
                new CS_JobType()
                {
                    ID = 1,
                    Description = "no name",
                    Active = true,
                    CS_JobAction = new EntityCollection<CS_JobAction>()
                    {
                        new CS_JobAction()
                        {
                            ID = 1,
                            Description = "no name",
                            Active = true
                        },
                        new CS_JobAction()
                        {
                            ID = 2,
                            Description = "no name",
                            Active = true
                        }
                    }
                });
            returnItem.AddObject(
                new CS_JobType()
                {
                    ID = 2,
                    Description = "no name",
                    Active = true,
                    CS_JobAction = new EntityCollection<CS_JobAction>()
                    {
                        new CS_JobAction()
                        {
                            ID = 3,
                            Description = "no name",
                            Active = true
                        },
                        new CS_JobAction()
                        {
                            ID = 4,
                            Description = "no name",
                            Active = true
                        }
                    }
                });

            return returnItem;
        }


        private FakeObjectSet<CS_State> CreateFakeStates()
        {
            FakeObjectSet<CS_State> returnItem = new FakeObjectSet<CS_State>();
            returnItem.AddObject(
                new CS_State() { Active = true, Acronym = "TX", Name = "Texas", ID = 1 }                
            );
            returnItem.AddObject(
                new CS_State() { Active = true, Acronym = "UT", Name = "Utah", ID = 2 }             
            );
            returnItem.AddObject(
                new CS_State() { Active = true, Acronym = "OH", Name = "Ohio", ID = 3 }                
            );

            return returnItem;    
        }

        private IObjectSet<CS_View_JobSummary> CreateFakeViewJobSummary()
        {
            FakeObjectSet<CS_View_JobSummary> returnItem = new FakeObjectSet<CS_View_JobSummary>();

            returnItem.AddObject(
                new CS_View_JobSummary()
                {
                    CallDate = new DateTime(2011,1,1),
                    CallLogId = 1,
                    ClosedDate = null,
                    Customer = "no data",
                    CustomerId = 1,
                    Division = "no data",
                    DivisionId = 1,
                    Duration = null,
                    EmployeeName = null,
                    EquipmentName = null,
                    IsResource = false,
                    JobID = 1,
                    JobNumber = "000001",
                    JobStatus = "A - Active",
                    JobStatusId = 1,
                    LastCallDate = new DateTime(2011,1,1),
                    LastCallType = "Initial Advise",
                    LastModification = new DateTime(2011,1,1),
                    Location = "TX - Denton",
                    ModifiedBy = "no data",
                    PresetDate = null,
                    ProjectManager = null,
                    StartDate = new DateTime(2011,1,1)
                });
            returnItem.AddObject(
                new CS_View_JobSummary()
                {
                    CallDate = new DateTime(2011, 2, 1),
                    CallLogId = 2,
                    ClosedDate = null,
                    Customer = "no data",
                    CustomerId = 2,
                    Division = "no data",
                    DivisionId = 2,
                    Duration = null,
                    EmployeeName = null,
                    EquipmentName = null,
                    IsResource = false,
                    JobID = 2,
                    JobNumber = "000002",
                    JobStatus = "P - Preset",
                    JobStatusId = 2,
                    LastCallDate = new DateTime(2011, 2, 1),
                    LastCallType = "Initial Advise",
                    LastModification = new DateTime(2011, 2, 1),
                    Location = "TX - Denton",
                    ModifiedBy = "no data",
                    PresetDate = new DateTime(2011, 2, 5),
                    ProjectManager = null,
                    StartDate = null
                });
            returnItem.AddObject(
                new CS_View_JobSummary()
                {
                    CallDate = new DateTime(2011, 1, 5),
                    CallLogId = 3,
                    ClosedDate = null,
                    Customer = null,
                    CustomerId = 1,
                    Division = "no data",
                    DivisionId = 1,
                    Duration = "7",
                    EmployeeName = null,
                    EquipmentName = "07-123",
                    IsResource = true,
                    JobID = 1,
                    JobNumber = "000001",
                    JobStatus = "A - Active",
                    JobStatusId = 1,
                    LastCallDate = new DateTime(2011, 1, 5),
                    LastCallType = "ETA",
                    LastModification = new DateTime(2011, 1, 5),
                    Location = "TX - Denton",
                    ModifiedBy = "no data",
                    PresetDate = null,
                    ProjectManager = null,
                    StartDate = null
                });
            returnItem.AddObject(
                new CS_View_JobSummary()
                {
                    CallDate = new DateTime(2011, 1, 6),
                    CallLogId = 4,
                    ClosedDate = null,
                    Customer = null,
                    CustomerId = 1,
                    Division = "no data",
                    DivisionId = 1,
                    Duration = "7",
                    EmployeeName = null,
                    EquipmentName = "07-123",
                    IsResource = true,
                    JobID = 1,
                    JobNumber = "000001",
                    JobStatus = "A - Active",
                    JobStatusId = 1,
                    LastCallDate = new DateTime(2011, 1, 6),
                    LastCallType = "ATA",
                    LastModification = new DateTime(2011, 1, 6),
                    Location = "TX - Denton",
                    ModifiedBy = "no data",
                    PresetDate = null,
                    ProjectManager = null,
                    StartDate = null
                });

            return returnItem;
        }

        private IObjectSet<CS_View_JobCallLog> CreateFakeViewJobCallLog()
        {
            FakeObjectSet<CS_View_JobCallLog> returnItem = new FakeObjectSet<CS_View_JobCallLog>();

            returnItem.AddObject(
                new CS_View_JobCallLog()
                {
                    DivisionId = 1,
                    JobId = 1,
                    JobNumber = "000001",
                    JobStatusId = 1,
                    CallTypeId = 1,
                    CallType = "Call Type 1",
                    CallDate = new DateTime(2011,1,6),
                    Details = "Details 1",
                    Customer = "Customer 1",
                    ModifiedById = 1,
                    ModifiedBy = "User1",
                    ModificationDate = new DateTime(2011,1,6),
                    Active = true,
                    IsGeneralLog = false,
                    ShiftTransferLog = false
                });
            returnItem.AddObject(
                new CS_View_JobCallLog()
                {
                    DivisionId = 1,
                    JobId = 1,
                    JobNumber = "000001",
                    JobStatusId = 1,
                    CallTypeId = 2,
                    CallType = "Call Type 2",
                    CallDate = new DateTime(2011, 1, 6),
                    Details = "Details 1",
                    Customer = "Customer 1",
                    ModifiedById = 2,
                    ModifiedBy = "User 2",
                    ModificationDate = new DateTime(2011, 1, 6),
                    Active = true,
                    IsGeneralLog = false,
                    ShiftTransferLog = false
                });
            returnItem.AddObject(
                new CS_View_JobCallLog()
                {
                    DivisionId = 2,
                    JobId = 2,
                    JobNumber = "000002",
                    JobStatusId = 2,
                    CallTypeId = 1,
                    CallType = "Call Type 1",
                    CallDate = new DateTime(2011, 1, 6),
                    Details = "Details 1",
                    Customer = "Customer 1",
                    ModifiedById = 1,
                    ModifiedBy = "User1",
                    ModificationDate = new DateTime(2011, 1, 6),
                    Active = true,
                    IsGeneralLog = false,
                    ShiftTransferLog = false
                });
            returnItem.AddObject(
                new CS_View_JobCallLog()
                {
                    DivisionId = 3,
                    JobId = 3,
                    JobNumber = "000003",
                    JobStatusId = 1,
                    CallTypeId = 1,
                    CallType = "Call Type 1",
                    CallDate = new DateTime(2011, 2, 6),
                    Details = "Details 1",
                    Customer = "Customer 1",
                    ModifiedById = 1,
                    ModifiedBy = "User1",
                    ModificationDate = new DateTime(2011, 2, 6),
                    Active = true,
                    IsGeneralLog = false,
                    ShiftTransferLog = false
                });

            return returnItem;
        }

        public System.Data.Objects.IObjectSet<T> CreateObjectSet<T>() where T : class
        {
            if (typeof(T) == typeof(CS_CallLog))
            {
                return (System.Data.Objects.IObjectSet<T>)CreateFakeCallLog();
            }
            else if (typeof(T) == typeof(CS_CallType))
            {
                return (System.Data.Objects.IObjectSet<T>)CreateFakeCallType();
            }
            else if (typeof(T) == typeof(CS_PrimaryCallType))
            {
                return (System.Data.Objects.IObjectSet<T>)CreateFakePrimaryCallType();
            }
            else if (typeof(T) == typeof(CS_View_Employee_CallLogInfo))
            {
                return (System.Data.Objects.IObjectSet<T>)CreateFakeViewEmployeeCallLogInfo();
            }
            else if (typeof(T) == typeof(CS_Job))
            {
                return (System.Data.Objects.IObjectSet<T>)CreateFakeJob();
            }
            else if (typeof(T) == typeof(CS_Settings))
            {
                return (System.Data.Objects.IObjectSet<T>)CreateFakeSettings();
            }
            else if (typeof(T) == typeof(CS_Resource))
            {
                return (System.Data.Objects.IObjectSet<T>)CreateFakeResource();
            }
            else if (typeof(T) == typeof(CS_View_Resource_CallLogInfo))
            {
                return (System.Data.Objects.IObjectSet<T>)CreateFakeCallLogInfo();
            }
            else if (typeof(T) == typeof(CS_Employee))
            {
                return (System.Data.Objects.IObjectSet<T>)CreateFakeEmployee();
            }
            else if (typeof(T) == typeof(CS_Contact))
            {
                return (System.Data.Objects.IObjectSet<T>)CreateFakeContact();
            }
            else if (typeof(T) == typeof(CS_Division))
            {
                return (System.Data.Objects.IObjectSet<T>)CreateFakeDivision();
            }
            else if (typeof(T) == typeof(CS_PriceType))
            {
                return (System.Data.Objects.IObjectSet<T>)CreateFakePriceType();
            }
            else if (typeof(T) == typeof(CS_City))
            {
                return (System.Data.Objects.IObjectSet<T>)CreateFakeCity();
            }
            else if (typeof(T) == typeof(CS_JobStatus))
            {
                return (System.Data.Objects.IObjectSet<T>)CreateFakeJobStatus();
            }
            else if (typeof(T) == typeof(CS_ZipCode))
            {
                return (System.Data.Objects.IObjectSet<T>)CreateFakeZipCode();
            }
            else if (typeof(T) == typeof(CS_JobAction))
            {
                return (System.Data.Objects.IObjectSet<T>)CreateFakeJobAction();
            }
            else if (typeof(T) == typeof(CS_JobCategory))
            {
                return (System.Data.Objects.IObjectSet<T>)CreateFakeJobCategory();
            }
            else if (typeof(T) == typeof(CS_JobType))
            {
                return (System.Data.Objects.IObjectSet<T>)CreateFakeJobType();
            }
            else if (typeof(T) == typeof(CS_State))
            {
                return (System.Data.Objects.IObjectSet<T>)CreateFakeStates();
            }
            else if (typeof(T) == typeof(CS_View_JobSummary))
            {
                return (System.Data.Objects.IObjectSet<T>)CreateFakeViewJobSummary();
            }
            else if (typeof(T) == typeof(CS_View_JobCallLog))
            {
                return (System.Data.Objects.IObjectSet<T>)CreateFakeViewJobCallLog();
            }
            else
            {
                return new FakeObjectSet<T>();
            }
        }

        public bool TryGetObjectStateEntry(object entity, out ObjectStateEntry stateEntry)
        {
            stateEntry = null;
            return false;
        }

        public void ChangeObjectState(object entity, System.Data.EntityState entityState)
        {
        }

        public void Dispose()
        {
        }
    }
}
