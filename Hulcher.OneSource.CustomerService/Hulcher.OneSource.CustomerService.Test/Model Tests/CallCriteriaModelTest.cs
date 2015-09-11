using System;
using System.Text;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.DataContext;

using Moq;
using Hulcher.OneSource.CustomerService.Core;
using Hulcher.OneSource.CustomerService.DataContext.VO;

namespace Hulcher.OneSource.CustomerService.Test.Model_Tests
{
    [TestClass]
    public class CallCriteriaModelTest
    {
        #region [ FilterByCriteriaValue ]

        #region [ JobStatus ]

        #region [ Contacts ]

        [TestMethod]
        public void TestFilterContactByCriteriaValueJobStatus()
        {
            FakeObjectSet<CS_CallCriteriaValue> fakecallcriteriasvalues = new FakeObjectSet<CS_CallCriteriaValue>();

            fakecallcriteriasvalues.AddObject
            (
                new CS_CallCriteriaValue()
                {
                    Active = true,
                    CallCriteriaID = 1,
                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.JobStatus,
                    Value = ((int)Globals.JobRecord.JobStatus.Active).ToString(),
                    CS_CallCriteria = new CS_CallCriteria()
                    {
                        ContactID = 1,
                        ID = 1,
                        Active = true
                    }
                }
            );

            IList<CS_Contact> lstContactBase = new List<CS_Contact> 
            {
                new CS_Contact()
                {
                    ID = 1,
                    CS_CallCriteria = new EntityCollection<CS_CallCriteria>()
                    {
                        new CS_CallCriteria()
                        {
                            ContactID = 1,
                            ID = 1,
                            Active = true,
                            CS_CallCriteriaValue = new EntityCollection<CS_CallCriteriaValue>()
                            {
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Customer,
                                    Value = "1090"
                                },
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Division,
                                    Value = "1"
                                }
                            }
                        }
                    }
                }
            };

            IList<CS_Contact> lstContact = lstContactBase;

            CS_Job job = new CS_Job()
            {
                ID = 1,
                CS_JobInfo = new CS_JobInfo() { JobID = 1, CS_Job_JobStatus = new EntityCollection<CS_Job_JobStatus>()  { new CS_Job_JobStatus() { ID=1,JobID=1,JobStatusId = (int)Globals.JobRecord.JobStatus.Active, Active = true }}},
                CS_JobDivision = new EntityCollection<CS_JobDivision>() { new CS_JobDivision() { JobID = 1, DivisionID = 1 } },
                CS_CustomerInfo = new CS_CustomerInfo() { JobId = 1, CustomerId = 1090 },
                CS_LocationInfo = new CS_LocationInfo(),
                CS_JobDescription = new CS_JobDescription()
            };

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_CallCriteriaValue>()).Returns(fakecallcriteriasvalues);
            CallCriteriaModel model = new CallCriteriaModel(mockUnitOfWork.Object);

            lstContact = model.FilterContactsByCriteriaValue(lstContactBase, job);

            Assert.AreEqual(1, lstContact.Count);
        }

        [TestMethod]
        public void TestFilterContactByCriteriaValueJobStatusCriteriaMissMatch()
        {
            FakeObjectSet<CS_CallCriteriaValue> fakecallcriteriasvalues = new FakeObjectSet<CS_CallCriteriaValue>();

            fakecallcriteriasvalues.AddObject
                    (
                        new CS_CallCriteriaValue()
                        {
                            Active = true,
                            CallCriteriaID = 1,
                            CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.JobStatus,
                            Value = ((int)Globals.JobRecord.JobStatus.Cancelled).ToString(),
                            CS_CallCriteria = new CS_CallCriteria()
                            {
                                ContactID = 1,
                                ID = 1,
                                Active = true
                            }
                        }
                    );

            IList<CS_Contact> lstContactBase = new List<CS_Contact> 
            {
                new CS_Contact()
                {
                    ID = 1,
                    CS_CallCriteria = new EntityCollection<CS_CallCriteria>()
                    {
                        new CS_CallCriteria()
                        {
                            ContactID = 1,
                            ID = 1,
                            Active = true,
                            CS_CallCriteriaValue = new EntityCollection<CS_CallCriteriaValue>()
                            {
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Customer,
                                    Value = "1090"
                                },
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Division,
                                    Value = "1"
                                }
                            }
                        }
                    }
                }
            };

            IList<CS_Contact> lstContact = lstContactBase;

            CS_Job job = new CS_Job()
            {
                ID = 1,
                CS_JobInfo = new CS_JobInfo() { JobID = 1, CS_Job_JobStatus = new EntityCollection<CS_Job_JobStatus>() { new CS_Job_JobStatus(){ ID=1,JobID=1,JobStatusId = (int)Globals.JobRecord.JobStatus.Active, Active = true }}},                    
                CS_JobDivision = new EntityCollection<CS_JobDivision>() { new CS_JobDivision() { JobID = 1, DivisionID = 1 } },
                CS_CustomerInfo = new CS_CustomerInfo() { JobId = 1, CustomerId = 1090 },
                CS_LocationInfo = new CS_LocationInfo(),
                CS_JobDescription = new CS_JobDescription()
            };

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_CallCriteriaValue>()).Returns(fakecallcriteriasvalues);
            CallCriteriaModel model = new CallCriteriaModel(mockUnitOfWork.Object);

            lstContact = model.FilterContactsByCriteriaValue(lstContactBase, job);

            Assert.AreEqual(0, lstContact.Count);

        }

        [TestMethod]
        public void TestFilterContactByCriteriaValueJobStatusNoCriteria()
        {
            FakeObjectSet<CS_CallCriteriaValue> fakecallcriteriavalues = new FakeObjectSet<CS_CallCriteriaValue>();

            fakecallcriteriavalues.AddObject
                    (
                        new CS_CallCriteriaValue()
                        {
                            Active = true,
                            CallCriteriaID = 1,
                            CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.JobStatus,
                            Value = ((int)Globals.JobRecord.JobStatus.Active).ToString(),
                            CS_CallCriteria = new CS_CallCriteria()
                            {
                                ContactID = 2,
                                ID = 1,
                                Active = true
                            }
                        }
                    );

            IList<CS_Contact> lstContactBase = new List<CS_Contact> 
            {
                new CS_Contact()
                {
                    ID = 1,
                    CS_CallCriteria = new EntityCollection<CS_CallCriteria>()
                    {
                        new CS_CallCriteria()
                        {
                            ContactID = 1,
                            ID = 1,
                            Active = true,
                            CS_CallCriteriaValue = new EntityCollection<CS_CallCriteriaValue>()
                            {
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Customer,
                                    Value = "1090"
                                },
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Division,
                                    Value = "1"
                                }
                            }
                        }
                    }
                }
            };

            IList<CS_Contact> lstContact = lstContactBase;

            CS_Job job = new CS_Job()
            {
                ID = 1,
                CS_JobInfo = new CS_JobInfo() { JobID = 1, CS_Job_JobStatus = new EntityCollection<CS_Job_JobStatus>() { new CS_Job_JobStatus() { ID =1, JobID =1, JobStatusId = (int)Globals.JobRecord.JobStatus.Active, Active = true}}},                     
                CS_JobDivision = new EntityCollection<CS_JobDivision>() { new CS_JobDivision() { JobID = 1, DivisionID = 1 } },
                CS_CustomerInfo = new CS_CustomerInfo() { JobId = 1, CustomerId = 1090 },
                CS_LocationInfo = new CS_LocationInfo(),
                CS_JobDescription = new CS_JobDescription()
            };

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_CallCriteriaValue>()).Returns(fakecallcriteriavalues);
            CallCriteriaModel model = new CallCriteriaModel(mockUnitOfWork.Object);

            lstContact = model.FilterContactsByCriteriaValue(lstContactBase, job);

            Assert.AreEqual(1, lstContact.Count);
        }

        #endregion

        #region [ Employee ]

        [TestMethod]
        public void TestFilterEmployeeByCriteriaValueJobStatus()
        {
            FakeObjectSet<CS_CallCriteriaValue> fakecallcriteriavalues = new FakeObjectSet<CS_CallCriteriaValue>();

            fakecallcriteriavalues.AddObject
                    (
                        new CS_CallCriteriaValue()
                        {
                            Active = true,
                            CallCriteriaID = 1,
                            CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.JobStatus,
                            Value = ((int)Globals.JobRecord.JobStatus.Active).ToString(),
                            CS_CallCriteria = new CS_CallCriteria()
                            {
                                EmployeeID = 1,
                                ID = 1,
                                Active = true
                            }
                        }
                    );

            IList<CS_Employee> lstEmployeeBase = new List<CS_Employee> 
            {
                new CS_Employee()
                {
                    ID = 1,
                    CS_CallCriteria = new EntityCollection<CS_CallCriteria>()
                    {
                        new CS_CallCriteria()
                        {
                            EmployeeID = 1,
                            ID = 1,
                            Active = true,
                            CS_CallCriteriaValue = new EntityCollection<CS_CallCriteriaValue>()
                            {
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Customer,
                                    Value = "1090"
                                },
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Division,
                                    Value = "1"
                                }
                            }
                        }
                    }
                }
            };

            IList<CS_Employee> lstEmployee = lstEmployeeBase;

            CS_Job job = new CS_Job()
            {
                ID = 1,
                CS_JobInfo = new CS_JobInfo() { JobID = 1, CS_Job_JobStatus = new EntityCollection<CS_Job_JobStatus>() {  new CS_Job_JobStatus() { ID=1,JobID =1, JobStatusId = (int)Globals.JobRecord.JobStatus.Active, Active = true }}},                     
                CS_JobDivision = new EntityCollection<CS_JobDivision>() { new CS_JobDivision() { JobID = 1, DivisionID = 1 } },
                CS_CustomerInfo = new CS_CustomerInfo() { JobId = 1, CustomerId = 1090 },
                CS_LocationInfo = new CS_LocationInfo(),
                CS_JobDescription = new CS_JobDescription()
            };

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_CallCriteriaValue>()).Returns(fakecallcriteriavalues);
            CallCriteriaModel model = new CallCriteriaModel(mockUnitOfWork.Object);

            lstEmployee = model.FilterEmployeeByCriteriaValue(lstEmployeeBase, job);

            Assert.AreEqual(1, lstEmployee.Count);
        }

        [TestMethod]
        public void TestFilterEmployeeByCriteriaValueJobStatusCriteriaMissMatch()
        {
            FakeObjectSet<CS_CallCriteriaValue> fakecallcriteriavalues = new FakeObjectSet<CS_CallCriteriaValue>();

            fakecallcriteriavalues.AddObject
                    (
                        new CS_CallCriteriaValue()
                        {
                            Active = true,
                            CallCriteriaID = 1,
                            CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.JobStatus,
                            Value = ((int)Globals.JobRecord.JobStatus.Cancelled).ToString(),
                            CS_CallCriteria = new CS_CallCriteria()
                            {
                                EmployeeID = 1,
                                ID = 1,
                                Active = true
                            }
                        }
                    );

            IList<CS_Employee> lstEmployeeBase = new List<CS_Employee> 
            {
                new CS_Employee()
                {
                    ID = 1,
                    CS_CallCriteria = new EntityCollection<CS_CallCriteria>()
                    {
                        new CS_CallCriteria()
                        {
                            EmployeeID = 1,
                            ID = 1,
                            Active = true,
                            CS_CallCriteriaValue = new EntityCollection<CS_CallCriteriaValue>()
                            {
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Customer,
                                    Value = "1090"
                                },
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Division,
                                    Value = "1"
                                }
                            }
                        }
                    }
                }
            };

            IList<CS_Employee> lstEmployee = lstEmployeeBase;

            CS_Job job = new CS_Job()
            {
                ID = 1,
                CS_JobInfo = new CS_JobInfo() { JobID = 1, CS_Job_JobStatus = new EntityCollection<CS_Job_JobStatus>() {  new CS_Job_JobStatus() { ID=1,JobID=1,JobStatusId=(int)Globals.JobRecord.JobStatus.Active, Active = true }}},                    
                CS_JobDivision = new EntityCollection<CS_JobDivision>() { new CS_JobDivision() { JobID = 1, DivisionID = 1 } },
                CS_CustomerInfo = new CS_CustomerInfo() { JobId = 1, CustomerId = 1090 },
                CS_LocationInfo = new CS_LocationInfo(),
                CS_JobDescription = new CS_JobDescription()
            };

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_CallCriteriaValue>()).Returns(fakecallcriteriavalues);
            CallCriteriaModel model = new CallCriteriaModel(mockUnitOfWork.Object);

            lstEmployee = model.FilterEmployeeByCriteriaValue(lstEmployeeBase, job);

            Assert.AreEqual(0, lstEmployee.Count);

        }

        [TestMethod]
        public void TestFilterEmployeeByCriteriaValueJobStatusNoCriteria()
        {
            FakeObjectSet<CS_CallCriteriaValue> fakecallcriteriavalues = new FakeObjectSet<CS_CallCriteriaValue>();

            fakecallcriteriavalues.AddObject
                    (
                        new CS_CallCriteriaValue()
                        {
                            Active = true,
                            CallCriteriaID = 1,
                            CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.JobStatus,
                            Value = ((int)Globals.JobRecord.JobStatus.Active).ToString(),
                            CS_CallCriteria = new CS_CallCriteria()
                            {
                                EmployeeID = 2,
                                ID = 1,
                                Active = true
                            }
                        }
                    );

            IList<CS_Employee> lstEmployeeBase = new List<CS_Employee> 
            {
                new CS_Employee()
                {
                    ID = 1,
                    CS_CallCriteria = new EntityCollection<CS_CallCriteria>()
                    {
                        new CS_CallCriteria()
                        {
                            EmployeeID = 1,
                            ID = 1,
                            Active = true,
                            CS_CallCriteriaValue = new EntityCollection<CS_CallCriteriaValue>()
                            {
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Customer,
                                    Value = "1090"
                                },
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Division,
                                    Value = "1"
                                }
                            }
                        }
                    }
                }
            };

            IList<CS_Employee> lstEmployee = lstEmployeeBase;

            CS_Job job = new CS_Job()
            {
                ID = 1,
                CS_JobInfo = new CS_JobInfo() { JobID = 1, CS_Job_JobStatus = new EntityCollection<CS_Job_JobStatus>() {  new CS_Job_JobStatus() { ID=1,JobID = 1,JobStatusId=(int)Globals.JobRecord.JobStatus.Active, Active = true}}},                     
                CS_JobDivision = new EntityCollection<CS_JobDivision>() { new CS_JobDivision() { JobID = 1, DivisionID = 1 } },
                CS_CustomerInfo = new CS_CustomerInfo() { JobId = 1, CustomerId = 1090 },
                CS_LocationInfo = new CS_LocationInfo(),
                CS_JobDescription = new CS_JobDescription()
            };

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_CallCriteriaValue>()).Returns(fakecallcriteriavalues);
            CallCriteriaModel model = new CallCriteriaModel(mockUnitOfWork.Object);

            lstEmployee = model.FilterEmployeeByCriteriaValue(lstEmployeeBase, job);

            Assert.AreEqual(1, lstEmployee.Count);

        }

        #endregion

        #endregion

        #region [ PriceType ]

        #region [ Contact ]

        [TestMethod]
        public void TestFilterContactByCriteriaValuePriceType()
        {
            FakeObjectSet<CS_CallCriteriaValue> fakecallcriteriavalues = new FakeObjectSet<CS_CallCriteriaValue>();

            fakecallcriteriavalues.AddObject
                    (
                        new CS_CallCriteriaValue()
                        {
                            Active = true,
                            CallCriteriaID = 1,
                            CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.PriceType,
                            Value = ((int)Globals.JobRecord.PriceType.PublishedRates).ToString(),
                            CS_CallCriteria = new CS_CallCriteria()
                            {
                                ContactID = 1,
                                ID = 1,
                                Active = true
                            }
                        }
                    );

            IList<CS_Contact> lstContactBase = new List<CS_Contact> 
            {
                new CS_Contact()
                {
                    ID = 1,
                    CS_CallCriteria = new EntityCollection<CS_CallCriteria>()
                    {
                        new CS_CallCriteria()
                        {
                            ContactID = 1,
                            ID = 1,
                            Active = true,
                            CS_CallCriteriaValue = new EntityCollection<CS_CallCriteriaValue>()
                            {
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Customer,
                                    Value = "1090"
                                },
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Division,
                                    Value = "1"
                                }
                            }
                        }
                    }
                }
            };

            IList<CS_Contact> lstContact = lstContactBase;

            CS_Job job = new CS_Job()
            {
                ID = 1,
                CS_JobInfo = new CS_JobInfo() { JobID = 1, PriceTypeID = (int)Globals.JobRecord.PriceType.PublishedRates },
                CS_JobDivision = new EntityCollection<CS_JobDivision>() { new CS_JobDivision() { JobID = 1, DivisionID = 1 } },
                CS_CustomerInfo = new CS_CustomerInfo() { JobId = 1, CustomerId = 1090 },
                CS_LocationInfo = new CS_LocationInfo(),
                CS_JobDescription = new CS_JobDescription()
            };

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_CallCriteriaValue>()).Returns(fakecallcriteriavalues);
            CallCriteriaModel model = new CallCriteriaModel(mockUnitOfWork.Object);

            lstContact = model.FilterContactsByCriteriaValue(lstContactBase, job);

            Assert.AreEqual(1, lstContact.Count);
        }

        [TestMethod]
        public void TestFilterContactByCriteriaValuePriceTypeCriteriaMissMatch()
        {
            FakeObjectSet<CS_CallCriteriaValue> fakecallcriteriavalues = new FakeObjectSet<CS_CallCriteriaValue>();

            fakecallcriteriavalues.AddObject
                    (
                        new CS_CallCriteriaValue()
                        {
                            Active = true,
                            CallCriteriaID = 1,
                            CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.PriceType,
                            Value = ((int)Globals.JobRecord.PriceType.SpecialRate).ToString(),
                            CS_CallCriteria = new CS_CallCriteria()
                            {
                                ContactID = 1,
                                ID = 1,
                                Active = true
                            }
                        }
                    );

            IList<CS_Contact> lstContactBase = new List<CS_Contact> 
            {
                new CS_Contact()
                {
                    ID = 1,
                    CS_CallCriteria = new EntityCollection<CS_CallCriteria>()
                    {
                        new CS_CallCriteria()
                        {
                            ContactID = 1,
                            ID = 1,
                            Active = true,
                            CS_CallCriteriaValue = new EntityCollection<CS_CallCriteriaValue>()
                            {
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Customer,
                                    Value = "1090"
                                },
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Division,
                                    Value = "1"
                                }
                            }
                        }
                    }
                }
            };

            IList<CS_Contact> lstContact = lstContactBase;

            CS_Job job = new CS_Job()
            {
                ID = 1,
                CS_JobInfo = new CS_JobInfo() { JobID = 1, PriceTypeID = (int)Globals.JobRecord.PriceType.PublishedRates },
                CS_JobDivision = new EntityCollection<CS_JobDivision>() { new CS_JobDivision() { JobID = 1, DivisionID = 1 } },
                CS_CustomerInfo = new CS_CustomerInfo() { JobId = 1, CustomerId = 1090 },
                CS_LocationInfo = new CS_LocationInfo(),
                CS_JobDescription = new CS_JobDescription()
            };

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_CallCriteriaValue>()).Returns(fakecallcriteriavalues);
            CallCriteriaModel model = new CallCriteriaModel(mockUnitOfWork.Object);

            lstContact = model.FilterContactsByCriteriaValue(lstContactBase, job);

            Assert.AreEqual(0, lstContact.Count);

        }

        [TestMethod]
        public void TestFilterContactByCriteriaValuePriceTypeNoCriteria()
        {
            FakeObjectSet<CS_CallCriteriaValue> fakecallcriteriavalues = new FakeObjectSet<CS_CallCriteriaValue>();

            fakecallcriteriavalues.AddObject
                    (
                        new CS_CallCriteriaValue()
                        {
                            Active = true,
                            CallCriteriaID = 1,
                            CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.PriceType,
                            Value = ((int)Globals.JobRecord.PriceType.PublishedRates).ToString(),
                            CS_CallCriteria = new CS_CallCriteria()
                            {
                                ContactID = 2,
                                ID = 1,
                                Active = true
                            }
                        }
                    );

            IList<CS_Contact> lstContactBase = new List<CS_Contact> 
            {
                new CS_Contact()
                {
                    ID = 1,
                    CS_CallCriteria = new EntityCollection<CS_CallCriteria>()
                    {
                        new CS_CallCriteria()
                        {
                            ContactID = 1,
                            ID = 1,
                            Active = true,
                            CS_CallCriteriaValue = new EntityCollection<CS_CallCriteriaValue>()
                            {
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Customer,
                                    Value = "1090"
                                },
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Division,
                                    Value = "1"
                                }
                            }
                        }
                    }
                }
            };

            IList<CS_Contact> lstContact = lstContactBase;

            CS_Job job = new CS_Job()
            {
                ID = 1,
                CS_JobInfo = new CS_JobInfo() { JobID = 1, PriceTypeID = (int)Globals.JobRecord.PriceType.PublishedRates },
                CS_JobDivision = new EntityCollection<CS_JobDivision>() { new CS_JobDivision() { JobID = 1, DivisionID = 1 } },
                CS_CustomerInfo = new CS_CustomerInfo() { JobId = 1, CustomerId = 1090 },
                CS_LocationInfo = new CS_LocationInfo(),
                CS_JobDescription = new CS_JobDescription()
            };

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_CallCriteriaValue>()).Returns(fakecallcriteriavalues);
            CallCriteriaModel model = new CallCriteriaModel(mockUnitOfWork.Object);

            lstContact = model.FilterContactsByCriteriaValue(lstContactBase, job);

            Assert.AreEqual(1, lstContact.Count);
        }

        #endregion

        #region [ Employee ]

        [TestMethod]
        public void TestFilterEmployeeByCriteriaValuePriceType()
        {
            FakeObjectSet<CS_CallCriteriaValue> fakecallcriteriavalues = new FakeObjectSet<CS_CallCriteriaValue>();

            fakecallcriteriavalues.AddObject
                    (
                        new CS_CallCriteriaValue()
                        {
                            Active = true,
                            CallCriteriaID = 1,
                            CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.PriceType,
                            Value = ((int)Globals.JobRecord.PriceType.PublishedRates).ToString(),
                            CS_CallCriteria = new CS_CallCriteria()
                            {
                                EmployeeID = 1,
                                ID = 1,
                                Active = true
                            }
                        }
                    );

            IList<CS_Employee> lstEmployeeBase = new List<CS_Employee> 
            {
                new CS_Employee()
                {
                    ID = 1,
                    CS_CallCriteria = new EntityCollection<CS_CallCriteria>()
                    {
                        new CS_CallCriteria()
                        {
                            EmployeeID = 1,
                            ID = 1,
                            Active = true,
                            CS_CallCriteriaValue = new EntityCollection<CS_CallCriteriaValue>()
                            {
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Customer,
                                    Value = "1090"
                                },
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Division,
                                    Value = "1"
                                }
                            }
                        }
                    }
                }
            };

            IList<CS_Employee> lstEmployee = lstEmployeeBase;

            CS_Job job = new CS_Job()
            {
                ID = 1,
                CS_JobInfo = new CS_JobInfo() { JobID = 1, PriceTypeID = (int)Globals.JobRecord.PriceType.PublishedRates },
                CS_JobDivision = new EntityCollection<CS_JobDivision>() { new CS_JobDivision() { JobID = 1, DivisionID = 1 } },
                CS_CustomerInfo = new CS_CustomerInfo() { JobId = 1, CustomerId = 1090 },
                CS_LocationInfo = new CS_LocationInfo(),
                CS_JobDescription = new CS_JobDescription()
            };

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_CallCriteriaValue>()).Returns(fakecallcriteriavalues);
            CallCriteriaModel model = new CallCriteriaModel(mockUnitOfWork.Object);

            lstEmployee = model.FilterEmployeeByCriteriaValue(lstEmployeeBase, job);

            Assert.AreEqual(1, lstEmployee.Count);
        }

        [TestMethod]
        public void TestFilterEmployeeByCriteriaValuePriceTypeCriteriaMissMatch()
        {
            FakeObjectSet<CS_CallCriteriaValue> fakecallcriteriavalues = new FakeObjectSet<CS_CallCriteriaValue>();

            fakecallcriteriavalues.AddObject
                    (
                        new CS_CallCriteriaValue()
                        {
                            Active = true,
                            CallCriteriaID = 1,
                            CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.PriceType,
                            Value = ((int)Globals.JobRecord.PriceType.SpecialRate).ToString(),
                            CS_CallCriteria = new CS_CallCriteria()
                            {
                                EmployeeID = 1,
                                ID = 1,
                                Active = true
                            }
                        }
                    );

            IList<CS_Employee> lstEmployeeBase = new List<CS_Employee> 
            {
                new CS_Employee()
                {
                    ID = 1,
                    CS_CallCriteria = new EntityCollection<CS_CallCriteria>()
                    {
                        new CS_CallCriteria()
                        {
                            EmployeeID = 1,
                            ID = 1,
                            Active = true,
                            CS_CallCriteriaValue = new EntityCollection<CS_CallCriteriaValue>()
                            {
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Customer,
                                    Value = "1090"
                                },
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Division,
                                    Value = "1"
                                }
                            }
                        }
                    }
                }
            };

            IList<CS_Employee> lstEmployee = lstEmployeeBase;

            CS_Job job = new CS_Job()
            {
                ID = 1,
                CS_JobInfo = new CS_JobInfo() { JobID = 1, PriceTypeID = (int)Globals.JobRecord.PriceType.PublishedRates },
                CS_JobDivision = new EntityCollection<CS_JobDivision>() { new CS_JobDivision() { JobID = 1, DivisionID = 1 } },
                CS_CustomerInfo = new CS_CustomerInfo() { JobId = 1, CustomerId = 1090 },
                CS_LocationInfo = new CS_LocationInfo(),
                CS_JobDescription = new CS_JobDescription()
            };

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_CallCriteriaValue>()).Returns(fakecallcriteriavalues);
            CallCriteriaModel model = new CallCriteriaModel(mockUnitOfWork.Object);

            lstEmployee = model.FilterEmployeeByCriteriaValue(lstEmployeeBase, job);

            Assert.AreEqual(0, lstEmployee.Count);

        }

        [TestMethod]
        public void TestFilterEmployeeByCriteriaValuePriceTypeNoCriteria()
        {
            FakeObjectSet<CS_CallCriteriaValue> fakecallcriteriavalues = new FakeObjectSet<CS_CallCriteriaValue>();

            fakecallcriteriavalues.AddObject
                    (
                        new CS_CallCriteriaValue()
                        {
                            Active = true,
                            CallCriteriaID = 1,
                            CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.PriceType,
                            Value = ((int)Globals.JobRecord.PriceType.PublishedRates).ToString(),
                            CS_CallCriteria = new CS_CallCriteria()
                            {
                                EmployeeID = 2,
                                ID = 1,
                                Active = true
                            }
                        }
                    );

            IList<CS_Employee> lstEmployeeBase = new List<CS_Employee> 
            {
                new CS_Employee()
                {
                    ID = 1,
                    CS_CallCriteria = new EntityCollection<CS_CallCriteria>()
                    {
                        new CS_CallCriteria()
                        {
                            EmployeeID = 1,
                            ID = 1,
                            Active = true,
                            CS_CallCriteriaValue = new EntityCollection<CS_CallCriteriaValue>()
                            {
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Customer,
                                    Value = "1090"
                                },
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Division,
                                    Value = "1"
                                }
                            }
                        }
                    }
                }
            };

            IList<CS_Employee> lstEmployee = lstEmployeeBase;

            CS_Job job = new CS_Job()
            {
                ID = 1,
                CS_JobInfo = new CS_JobInfo() { JobID = 1, PriceTypeID = (int)Globals.JobRecord.PriceType.PublishedRates },
                CS_JobDivision = new EntityCollection<CS_JobDivision>() { new CS_JobDivision() { JobID = 1, DivisionID = 1 } },
                CS_CustomerInfo = new CS_CustomerInfo() { JobId = 1, CustomerId = 1090 },
                CS_LocationInfo = new CS_LocationInfo(),
                CS_JobDescription = new CS_JobDescription()
            };

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_CallCriteriaValue>()).Returns(fakecallcriteriavalues);
            CallCriteriaModel model = new CallCriteriaModel(mockUnitOfWork.Object);

            lstEmployee = model.FilterEmployeeByCriteriaValue(lstEmployeeBase, job);

            Assert.AreEqual(1, lstEmployee.Count);

        }

        #endregion

        #endregion

        #region [ JobCategory ]

        #region [ Contact ]

        [TestMethod]
        public void TestFilterContactByCriteriaValueJobCategory()
        {
            FakeObjectSet<CS_CallCriteriaValue> fakecallcriteriavalues = new FakeObjectSet<CS_CallCriteriaValue>();

            fakecallcriteriavalues.AddObject
                    (
                        new CS_CallCriteriaValue()
                        {
                            Active = true,
                            CallCriteriaID = 1,
                            CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.JobCategory,
                            Value = (4).ToString(),
                            CS_CallCriteria = new CS_CallCriteria()
                            {
                                ContactID = 1,
                                ID = 1,
                                Active = true
                            }
                        }
                    );

            IList<CS_Contact> lstContactBase = new List<CS_Contact> 
            {
                new CS_Contact()
                {
                    ID = 1,
                    CS_CallCriteria = new EntityCollection<CS_CallCriteria>()
                    {
                        new CS_CallCriteria()
                        {
                            ContactID = 1,
                            ID = 1,
                            Active = true,
                            CS_CallCriteriaValue = new EntityCollection<CS_CallCriteriaValue>()
                            {
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Customer,
                                    Value = "1090"
                                },
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Division,
                                    Value = "1"
                                }
                            }
                        }
                    }
                }
            };

            IList<CS_Contact> lstContact = lstContactBase;

            CS_Job job = new CS_Job()
            {
                ID = 1,
                CS_JobInfo = new CS_JobInfo() { JobID = 1, JobCategoryID = 4 },
                CS_JobDivision = new EntityCollection<CS_JobDivision>() { new CS_JobDivision() { JobID = 1, DivisionID = 1 } },
                CS_CustomerInfo = new CS_CustomerInfo() { JobId = 1, CustomerId = 1090 },
                CS_LocationInfo = new CS_LocationInfo(),
                CS_JobDescription = new CS_JobDescription()
            };

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_CallCriteriaValue>()).Returns(fakecallcriteriavalues);
            CallCriteriaModel model = new CallCriteriaModel(mockUnitOfWork.Object);

            lstContact = model.FilterContactsByCriteriaValue(lstContactBase, job);

            Assert.AreEqual(1, lstContact.Count);
        }

        [TestMethod]
        public void TestFilterContactByCriteriaValueJobCategoryCriteriaMissMatch()
        {
            FakeObjectSet<CS_CallCriteriaValue> fakecallcriteriavalues = new FakeObjectSet<CS_CallCriteriaValue>();

            fakecallcriteriavalues.AddObject
                    (
                        new CS_CallCriteriaValue()
                        {
                            Active = true,
                            CallCriteriaID = 1,
                            CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.JobCategory,
                            Value = (5).ToString(),
                            CS_CallCriteria = new CS_CallCriteria()
                            {
                                ContactID = 1,
                                ID = 1,
                                Active = true
                            }
                        }
                    );

            IList<CS_Contact> lstContactBase = new List<CS_Contact> 
            {
                new CS_Contact()
                {
                    ID = 1,
                    CS_CallCriteria = new EntityCollection<CS_CallCriteria>()
                    {
                        new CS_CallCriteria()
                        {
                            ContactID = 1,
                            ID = 1,
                            Active = true,
                            CS_CallCriteriaValue = new EntityCollection<CS_CallCriteriaValue>()
                            {
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Customer,
                                    Value = "1090"
                                },
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Division,
                                    Value = "1"
                                }
                            }
                        }
                    }
                }
            };

            IList<CS_Contact> lstContact = lstContactBase;

            CS_Job job = new CS_Job()
            {
                ID = 1,
                CS_JobInfo = new CS_JobInfo() { JobID = 1, JobCategoryID = 4 },
                CS_JobDivision = new EntityCollection<CS_JobDivision>() { new CS_JobDivision() { JobID = 1, DivisionID = 1 } },
                CS_CustomerInfo = new CS_CustomerInfo() { JobId = 1, CustomerId = 1090 },
                CS_LocationInfo = new CS_LocationInfo(),
                CS_JobDescription = new CS_JobDescription()
            };

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_CallCriteriaValue>()).Returns(fakecallcriteriavalues);
            CallCriteriaModel model = new CallCriteriaModel(mockUnitOfWork.Object);

            lstContact = model.FilterContactsByCriteriaValue(lstContactBase, job);

            Assert.AreEqual(0, lstContact.Count);

        }

        [TestMethod]
        public void TestFilterContactByCriteriaValueJobCategoryNoCriteria()
        {
            FakeObjectSet<CS_CallCriteriaValue> fakecallcriteriavalues = new FakeObjectSet<CS_CallCriteriaValue>();

            fakecallcriteriavalues.AddObject
                    (
                        new CS_CallCriteriaValue()
                        {
                            Active = true,
                            CallCriteriaID = 1,
                            CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.JobCategory,
                            Value = (4).ToString(),
                            CS_CallCriteria = new CS_CallCriteria()
                            {
                                ContactID = 2,
                                ID = 1,
                                Active = true
                            }
                        }
                    );

            IList<CS_Contact> lstContactBase = new List<CS_Contact> 
            {
                new CS_Contact()
                {
                    ID = 1,
                    CS_CallCriteria = new EntityCollection<CS_CallCriteria>()
                    {
                        new CS_CallCriteria()
                        {
                            ContactID = 1,
                            ID = 1,
                            Active = true,
                            CS_CallCriteriaValue = new EntityCollection<CS_CallCriteriaValue>()
                            {
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Customer,
                                    Value = "1090"
                                },
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Division,
                                    Value = "1"
                                }
                            }
                        }
                    }
                }
            };

            IList<CS_Contact> lstContact = lstContactBase;

            CS_Job job = new CS_Job()
            {
                ID = 1,
                CS_JobInfo = new CS_JobInfo() { JobID = 1, JobCategoryID = 4 },
                CS_JobDivision = new EntityCollection<CS_JobDivision>() { new CS_JobDivision() { JobID = 1, DivisionID = 1 } },
                CS_CustomerInfo = new CS_CustomerInfo() { JobId = 1, CustomerId = 1090 },
                CS_LocationInfo = new CS_LocationInfo(),
                CS_JobDescription = new CS_JobDescription()
            };

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_CallCriteriaValue>()).Returns(fakecallcriteriavalues);
            CallCriteriaModel model = new CallCriteriaModel(mockUnitOfWork.Object);

            lstContact = model.FilterContactsByCriteriaValue(lstContactBase, job);

            Assert.AreEqual(1, lstContact.Count);
        }

        #endregion

        #region [ Employee ]

        [TestMethod]
        public void TestFilterEmployeeByCriteriaValueJobCategory()
        {
            FakeObjectSet<CS_CallCriteriaValue> fakecallcriteriavalues = new FakeObjectSet<CS_CallCriteriaValue>();

            fakecallcriteriavalues.AddObject
                    (
                        new CS_CallCriteriaValue()
                        {
                            Active = true,
                            CallCriteriaID = 1,
                            CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.JobCategory,
                            Value = (4).ToString(),
                            CS_CallCriteria = new CS_CallCriteria()
                            {
                                EmployeeID = 1,
                                ID = 1,
                                Active = true
                            }
                        }
                    );

            IList<CS_Employee> lstEmployeeBase = new List<CS_Employee> 
            {
                new CS_Employee()
                {
                    ID = 1,
                    CS_CallCriteria = new EntityCollection<CS_CallCriteria>()
                    {
                        new CS_CallCriteria()
                        {
                            EmployeeID = 1,
                            ID = 1,
                            Active = true,
                            CS_CallCriteriaValue = new EntityCollection<CS_CallCriteriaValue>()
                            {
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Customer,
                                    Value = "1090"
                                },
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Division,
                                    Value = "1"
                                }
                            }
                        }
                    }
                }
            };

            IList<CS_Employee> lstEmployee = lstEmployeeBase;

            CS_Job job = new CS_Job()
            {
                ID = 1,
                CS_JobInfo = new CS_JobInfo() { JobID = 1, JobCategoryID = 4 },
                CS_JobDivision = new EntityCollection<CS_JobDivision>() { new CS_JobDivision() { JobID = 1, DivisionID = 1 } },
                CS_CustomerInfo = new CS_CustomerInfo() { JobId = 1, CustomerId = 1090 },
                CS_LocationInfo = new CS_LocationInfo(),
                CS_JobDescription = new CS_JobDescription()
            };

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_CallCriteriaValue>()).Returns(fakecallcriteriavalues);
            CallCriteriaModel model = new CallCriteriaModel(mockUnitOfWork.Object);

            lstEmployee = model.FilterEmployeeByCriteriaValue(lstEmployeeBase, job);

            Assert.AreEqual(1, lstEmployee.Count);
        }

        [TestMethod]
        public void TestFilterEmployeeByCriteriaValueJobCategoryCriteriaMissMatch()
        {
            FakeObjectSet<CS_CallCriteriaValue> fakecallcriteriavalues = new FakeObjectSet<CS_CallCriteriaValue>();

            fakecallcriteriavalues.AddObject
                    (
                        new CS_CallCriteriaValue()
                        {
                            Active = true,
                            CallCriteriaID = 1,
                            CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.JobCategory,
                            Value = (5).ToString(),
                            CS_CallCriteria = new CS_CallCriteria()
                            {
                                EmployeeID = 1,
                                ID = 1,
                                Active = true
                            }
                        }
                    );

            IList<CS_Employee> lstEmployeeBase = new List<CS_Employee> 
            {
                new CS_Employee()
                {
                    ID = 1,
                    CS_CallCriteria = new EntityCollection<CS_CallCriteria>()
                    {
                        new CS_CallCriteria()
                        {
                            EmployeeID = 1,
                            ID = 1,
                            Active = true,
                            CS_CallCriteriaValue = new EntityCollection<CS_CallCriteriaValue>()
                            {
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Customer,
                                    Value = "1090"
                                },
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Division,
                                    Value = "1"
                                }
                            }
                        }
                    }
                }
            };

            IList<CS_Employee> lstEmployee = lstEmployeeBase;

            CS_Job job = new CS_Job()
            {
                ID = 1,
                CS_JobInfo = new CS_JobInfo() { JobID = 1, JobCategoryID = 4 },
                CS_JobDivision = new EntityCollection<CS_JobDivision>() { new CS_JobDivision() { JobID = 1, DivisionID = 1 } },
                CS_CustomerInfo = new CS_CustomerInfo() { JobId = 1, CustomerId = 1090 },
                CS_LocationInfo = new CS_LocationInfo(),
                CS_JobDescription = new CS_JobDescription()
            };

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_CallCriteriaValue>()).Returns(fakecallcriteriavalues);
            CallCriteriaModel model = new CallCriteriaModel(mockUnitOfWork.Object);

            lstEmployee = model.FilterEmployeeByCriteriaValue(lstEmployeeBase, job);

            Assert.AreEqual(0, lstEmployee.Count);

        }

        [TestMethod]
        public void TestFilterEmployeeByCriteriaValueJobCategoryNoCriteria()
        {
            FakeObjectSet<CS_CallCriteriaValue> fakecallcriteriavalues = new FakeObjectSet<CS_CallCriteriaValue>();

            fakecallcriteriavalues.AddObject
                    (
                        new CS_CallCriteriaValue()
                        {
                            Active = true,
                            CallCriteriaID = 1,
                            CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.JobCategory,
                            Value = (4).ToString(),
                            CS_CallCriteria = new CS_CallCriteria()
                            {
                                EmployeeID = 2,
                                ID = 1,
                                Active = true
                            }
                        }
                    );

            IList<CS_Employee> lstEmployeeBase = new List<CS_Employee> 
            {
                new CS_Employee()
                {
                    ID = 1,
                    CS_CallCriteria = new EntityCollection<CS_CallCriteria>()
                    {
                        new CS_CallCriteria()
                        {
                            EmployeeID = 1,
                            ID = 1,
                            Active = true,
                            CS_CallCriteriaValue = new EntityCollection<CS_CallCriteriaValue>()
                            {
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Customer,
                                    Value = "1090"
                                },
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Division,
                                    Value = "1"
                                }
                            }
                        }
                    }
                }
            };

            IList<CS_Employee> lstEmployee = lstEmployeeBase;

            CS_Job job = new CS_Job()
            {
                ID = 1,
                CS_JobInfo = new CS_JobInfo() { JobID = 1, JobCategoryID = 4 },
                CS_JobDivision = new EntityCollection<CS_JobDivision>() { new CS_JobDivision() { JobID = 1, DivisionID = 1 } },
                CS_CustomerInfo = new CS_CustomerInfo() { JobId = 1, CustomerId = 1090 },
                CS_LocationInfo = new CS_LocationInfo(),
                CS_JobDescription = new CS_JobDescription()
            };

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_CallCriteriaValue>()).Returns(fakecallcriteriavalues);
            CallCriteriaModel model = new CallCriteriaModel(mockUnitOfWork.Object);

            lstEmployee = model.FilterEmployeeByCriteriaValue(lstEmployeeBase, job);

            Assert.AreEqual(1, lstEmployee.Count);

        }

        #endregion

        #endregion

        #region [ JobType ]

        #region [ Contact ]

        [TestMethod]
        public void TestFilterContactByCriteriaValueJobType()
        {
            FakeObjectSet<CS_CallCriteriaValue> fakecallcriteriavalues = new FakeObjectSet<CS_CallCriteriaValue>();

            fakecallcriteriavalues.AddObject
                    (
                        new CS_CallCriteriaValue()
                        {
                            Active = true,
                            CallCriteriaID = 1,
                            CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.JobType,
                            Value = ((int)Globals.JobRecord.JobType.JobType1).ToString(),
                            CS_CallCriteria = new CS_CallCriteria()
                            {
                                ContactID = 1,
                                ID = 1,
                                Active = true
                            }
                        }
                    );

            IList<CS_Contact> lstContactBase = new List<CS_Contact> 
            {
                new CS_Contact()
                {
                    ID = 1,
                    CS_CallCriteria = new EntityCollection<CS_CallCriteria>()
                    {
                        new CS_CallCriteria()
                        {
                            ContactID = 1,
                            ID = 1,
                            Active = true,
                            CS_CallCriteriaValue = new EntityCollection<CS_CallCriteriaValue>()
                            {
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Customer,
                                    Value = "1090"
                                },
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Division,
                                    Value = "1"
                                }
                            }
                        }
                    }
                }
            };

            IList<CS_Contact> lstContact = lstContactBase;

            CS_Job job = new CS_Job()
            {
                ID = 1,
                CS_JobInfo = new CS_JobInfo() { JobID = 1, JobTypeID = (int)Globals.JobRecord.JobType.JobType1 },
                CS_JobDivision = new EntityCollection<CS_JobDivision>() { new CS_JobDivision() { JobID = 1, DivisionID = 1 } },
                CS_CustomerInfo = new CS_CustomerInfo() { JobId = 1, CustomerId = 1090 },
                CS_LocationInfo = new CS_LocationInfo(),
                CS_JobDescription = new CS_JobDescription()
            };

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_CallCriteriaValue>()).Returns(fakecallcriteriavalues);
            CallCriteriaModel model = new CallCriteriaModel(mockUnitOfWork.Object);

            lstContact = model.FilterContactsByCriteriaValue(lstContactBase, job);

            Assert.AreEqual(1, lstContact.Count);
        }

        [TestMethod]
        public void TestFilterContactByCriteriaValueJobTypeCriteriaMissMatch()
        {
            FakeObjectSet<CS_CallCriteriaValue> fakecallcriteriavalues = new FakeObjectSet<CS_CallCriteriaValue>();

            fakecallcriteriavalues.AddObject
                    (
                        new CS_CallCriteriaValue()
                        {
                            Active = true,
                            CallCriteriaID = 1,
                            CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.JobType,
                            Value = ((int)Globals.JobRecord.JobType.JobType2).ToString(),
                            CS_CallCriteria = new CS_CallCriteria()
                            {
                                ContactID = 1,
                                ID = 1,
                                Active = true
                            }
                        }
                    );

            IList<CS_Contact> lstContactBase = new List<CS_Contact> 
            {
                new CS_Contact()
                {
                    ID = 1,
                    CS_CallCriteria = new EntityCollection<CS_CallCriteria>()
                    {
                        new CS_CallCriteria()
                        {
                            ContactID = 1,
                            ID = 1,
                            Active = true,
                            CS_CallCriteriaValue = new EntityCollection<CS_CallCriteriaValue>()
                            {
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Customer,
                                    Value = "1090"
                                },
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Division,
                                    Value = "1"
                                }
                            }
                        }
                    }
                }
            };

            IList<CS_Contact> lstContact = lstContactBase;

            CS_Job job = new CS_Job()
            {
                ID = 1,
                CS_JobInfo = new CS_JobInfo() { JobID = 1, JobTypeID = (int)Globals.JobRecord.JobType.JobType1 },
                CS_JobDivision = new EntityCollection<CS_JobDivision>() { new CS_JobDivision() { JobID = 1, DivisionID = 1 } },
                CS_CustomerInfo = new CS_CustomerInfo() { JobId = 1, CustomerId = 1090 },
                CS_LocationInfo = new CS_LocationInfo(),
                CS_JobDescription = new CS_JobDescription()
            };

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_CallCriteriaValue>()).Returns(fakecallcriteriavalues);
            CallCriteriaModel model = new CallCriteriaModel(mockUnitOfWork.Object);

            lstContact = model.FilterContactsByCriteriaValue(lstContactBase, job);

            Assert.AreEqual(0, lstContact.Count);

        }

        [TestMethod]
        public void TestFilterContactByCriteriaValueJobTypeNoCriteria()
        {
            FakeObjectSet<CS_CallCriteriaValue> fakecallcriteriavalues = new FakeObjectSet<CS_CallCriteriaValue>();

            fakecallcriteriavalues.AddObject
                    (
                        new CS_CallCriteriaValue()
                        {
                            Active = true,
                            CallCriteriaID = 1,
                            CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.JobType,
                            Value = ((int)Globals.JobRecord.JobType.JobType1).ToString(),
                            CS_CallCriteria = new CS_CallCriteria()
                            {
                                ContactID = 2,
                                ID = 1,
                                Active = true
                            }
                        }
                    );

            IList<CS_Contact> lstContactBase = new List<CS_Contact> 
            {
                new CS_Contact()
                {
                    ID = 1,
                    CS_CallCriteria = new EntityCollection<CS_CallCriteria>()
                    {
                        new CS_CallCriteria()
                        {
                            ContactID = 1,
                            ID = 1,
                            Active = true,
                            CS_CallCriteriaValue = new EntityCollection<CS_CallCriteriaValue>()
                            {
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Customer,
                                    Value = "1090"
                                },
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Division,
                                    Value = "1"
                                }
                            }
                        }
                    }
                }
            };

            IList<CS_Contact> lstContact = lstContactBase;

            CS_Job job = new CS_Job()
            {
                ID = 1,
                CS_JobInfo = new CS_JobInfo() { JobID = 1, JobTypeID = (int)Globals.JobRecord.JobType.JobType1 },
                CS_JobDivision = new EntityCollection<CS_JobDivision>() { new CS_JobDivision() { JobID = 1, DivisionID = 1 } },
                CS_CustomerInfo = new CS_CustomerInfo() { JobId = 1, CustomerId = 1090 },
                CS_LocationInfo = new CS_LocationInfo(),
                CS_JobDescription = new CS_JobDescription()
            };

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_CallCriteriaValue>()).Returns(fakecallcriteriavalues);
            CallCriteriaModel model = new CallCriteriaModel(mockUnitOfWork.Object);

            lstContact = model.FilterContactsByCriteriaValue(lstContactBase, job);

            Assert.AreEqual(1, lstContact.Count);
        }

        #endregion

        #region [ Employee ]

        [TestMethod]
        public void TestFilterEmployeeByCriteriaValueJobType()
        {
            FakeObjectSet<CS_CallCriteriaValue> fakecallcriteriavalues = new FakeObjectSet<CS_CallCriteriaValue>();

            fakecallcriteriavalues.AddObject
                    (
                        new CS_CallCriteriaValue()
                        {
                            Active = true,
                            CallCriteriaID = 1,
                            CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.JobType,
                            Value = ((int)Globals.JobRecord.JobType.JobType1).ToString(),
                            CS_CallCriteria = new CS_CallCriteria()
                            {
                                EmployeeID = 1,
                                ID = 1,
                                Active = true
                            }
                        }
                    );

            IList<CS_Employee> lstEmployeeBase = new List<CS_Employee> 
            {
                new CS_Employee()
                {
                    ID = 1,
                    CS_CallCriteria = new EntityCollection<CS_CallCriteria>()
                    {
                        new CS_CallCriteria()
                        {
                            EmployeeID = 1,
                            ID = 1,
                            Active = true,
                            CS_CallCriteriaValue = new EntityCollection<CS_CallCriteriaValue>()
                            {
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Customer,
                                    Value = "1090"
                                },
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Division,
                                    Value = "1"
                                }
                            }
                        }
                    }
                }
            };

            IList<CS_Employee> lstEmployee = lstEmployeeBase;

            CS_Job job = new CS_Job()
            {
                ID = 1,
                CS_JobInfo = new CS_JobInfo() { JobID = 1, JobTypeID = (int)Globals.JobRecord.JobType.JobType1 },
                CS_JobDivision = new EntityCollection<CS_JobDivision>() { new CS_JobDivision() { JobID = 1, DivisionID = 1 } },
                CS_CustomerInfo = new CS_CustomerInfo() { JobId = 1, CustomerId = 1090 },
                CS_LocationInfo = new CS_LocationInfo(),
                CS_JobDescription = new CS_JobDescription()
            };

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_CallCriteriaValue>()).Returns(fakecallcriteriavalues);
            CallCriteriaModel model = new CallCriteriaModel(mockUnitOfWork.Object);

            lstEmployee = model.FilterEmployeeByCriteriaValue(lstEmployeeBase, job);

            Assert.AreEqual(1, lstEmployee.Count);
        }

        [TestMethod]
        public void TestFilterEmployeeByCriteriaValueJobTypeCriteriaMissMatch()
        {
            FakeObjectSet<CS_CallCriteriaValue> fakecallcriteriavalues = new FakeObjectSet<CS_CallCriteriaValue>();

            fakecallcriteriavalues.AddObject
                    (
                        new CS_CallCriteriaValue()
                        {
                            Active = true,
                            CallCriteriaID = 1,
                            CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.JobType,
                            Value = ((int)Globals.JobRecord.JobType.JobType2).ToString(),
                            CS_CallCriteria = new CS_CallCriteria()
                            {
                                EmployeeID = 1,
                                ID = 1,
                                Active = true
                            }
                        }
                    );

            IList<CS_Employee> lstEmployeeBase = new List<CS_Employee> 
            {
                new CS_Employee()
                {
                    ID = 1,
                    CS_CallCriteria = new EntityCollection<CS_CallCriteria>()
                    {
                        new CS_CallCriteria()
                        {
                            EmployeeID = 1,
                            ID = 1,
                            Active = true,
                            CS_CallCriteriaValue = new EntityCollection<CS_CallCriteriaValue>()
                            {
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Customer,
                                    Value = "1090"
                                },
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Division,
                                    Value = "1"
                                }
                            }
                        }
                    }
                }
            };

            IList<CS_Employee> lstEmployee = lstEmployeeBase;

            CS_Job job = new CS_Job()
            {
                ID = 1,
                CS_JobInfo = new CS_JobInfo() { JobID = 1, JobTypeID = (int)Globals.JobRecord.JobType.JobType1 },
                CS_JobDivision = new EntityCollection<CS_JobDivision>() { new CS_JobDivision() { JobID = 1, DivisionID = 1 } },
                CS_CustomerInfo = new CS_CustomerInfo() { JobId = 1, CustomerId = 1090 },
                CS_LocationInfo = new CS_LocationInfo(),
                CS_JobDescription = new CS_JobDescription()
            };

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_CallCriteriaValue>()).Returns(fakecallcriteriavalues);
            CallCriteriaModel model = new CallCriteriaModel(mockUnitOfWork.Object);

            lstEmployee = model.FilterEmployeeByCriteriaValue(lstEmployeeBase, job);

            Assert.AreEqual(0, lstEmployee.Count);

        }

        [TestMethod]
        public void TestFilterEmployeeByCriteriaValueJobTypeNoCriteria()
        {
            FakeObjectSet<CS_CallCriteriaValue> fakecallcriteriavalues = new FakeObjectSet<CS_CallCriteriaValue>();

            fakecallcriteriavalues.AddObject
                    (
                        new CS_CallCriteriaValue()
                        {
                            Active = true,
                            CallCriteriaID = 1,
                            CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.JobType,
                            Value = ((int)Globals.JobRecord.JobType.JobType1).ToString(),
                            CS_CallCriteria = new CS_CallCriteria()
                            {
                                EmployeeID = 2,
                                ID = 1,
                                Active = true
                            }
                        }
                    );

            IList<CS_Employee> lstEmployeeBase = new List<CS_Employee> 
            {
                new CS_Employee()
                {
                    ID = 1,
                    CS_CallCriteria = new EntityCollection<CS_CallCriteria>()
                    {
                        new CS_CallCriteria()
                        {
                            EmployeeID = 1,
                            ID = 1,
                            Active = true,
                            CS_CallCriteriaValue = new EntityCollection<CS_CallCriteriaValue>()
                            {
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Customer,
                                    Value = "1090"
                                },
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = (int)Globals.CallCriteria.CallCriteriaType.Division,
                                    Value = "1"
                                }
                            }
                        }
                    }
                }
            };

            IList<CS_Employee> lstEmployee = lstEmployeeBase;

            CS_Job job = new CS_Job()
            {
                ID = 1,
                CS_JobInfo = new CS_JobInfo() { JobID = 1, JobTypeID = (int)Globals.JobRecord.JobType.JobType1 },
                CS_JobDivision = new EntityCollection<CS_JobDivision>() { new CS_JobDivision() { JobID = 1, DivisionID = 1 } },
                CS_CustomerInfo = new CS_CustomerInfo() { JobId = 1, CustomerId = 1090 },
                CS_LocationInfo = new CS_LocationInfo(),
                CS_JobDescription = new CS_JobDescription()
            };

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_CallCriteriaValue>()).Returns(fakecallcriteriavalues);
            CallCriteriaModel model = new CallCriteriaModel(mockUnitOfWork.Object);

            lstEmployee = model.FilterEmployeeByCriteriaValue(lstEmployeeBase, job);

            Assert.AreEqual(1, lstEmployee.Count);

        }

        #endregion

        #endregion

        #endregion

        #region [ ListCriteriaByDivisionAndCustomer ]

        [TestMethod]
        public void TestListEmployeeCriteriaByDivisionAndCustomer()
        {
            FakeObjectSet<CS_Employee> fakeemployee = new FakeObjectSet<CS_Employee>();
            fakeemployee.AddObject(
                new CS_Employee()
                {
                    ID = 1,
                    CS_CallCriteria = new EntityCollection<CS_CallCriteria>()
                    {
                        new CS_CallCriteria()
                        {
                            EmployeeID = 1,
                            ID = 1,
                            Active = true,
                            CS_CallCriteriaValue = new EntityCollection<CS_CallCriteriaValue>()
                            {
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = 1,
                                    Value = "1090"
                                },
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = 2,
                                    Value = "1"
                                }
                            }
                        }
                    }
                });


            CS_Job job = new CS_Job()
            {
                ID = 3,
                CS_JobDivision = new EntityCollection<CS_JobDivision>() { new CS_JobDivision() { JobID = 3, DivisionID = 1 } },
                CS_CustomerInfo = new CS_CustomerInfo() { JobId = 3, CustomerId = 1090 }
            };

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_Employee>()).Returns(fakeemployee);
            CallCriteriaModel model = new CallCriteriaModel(mockUnitOfWork.Object);

            IList<CS_Employee> lstEmployee = model.ListEmployeeCriteriaByDivisionAndCustomer(job);

            Assert.AreEqual(1, lstEmployee.Count);
        }

        [TestMethod]
        public void TestListContactsCriteriaByDivisionAndCustomer()
        {
            FakeObjectSet<CS_Contact> fakecontact = new FakeObjectSet<CS_Contact>();
            fakecontact.AddObject(
                new CS_Contact()
                {
                    ID = 1,
                    CS_CallCriteria = new EntityCollection<CS_CallCriteria>()
                    {
                        new CS_CallCriteria()
                        {
                            ContactID = 1,
                            ID = 1,
                            Active = true,
                            CS_CallCriteriaValue = new EntityCollection<CS_CallCriteriaValue>()
                            {
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = 1,
                                    Value = "1090"
                                },
                                new CS_CallCriteriaValue() 
                                {
                                    Active = true,
                                    CallCriteriaID = 1,
                                    CallCriteriaTypeID = 2,
                                    Value = "1"
                                }
                            }
                        }
                    }
                });


            CS_Job job = new CS_Job()
            {
                ID = 3,
                CS_JobDivision = new EntityCollection<CS_JobDivision>() { new CS_JobDivision() { JobID = 3, DivisionID = 1 } },
                CS_CustomerInfo = new CS_CustomerInfo() { JobId = 3, CustomerId = 1090 }
            };

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_Contact>()).Returns(fakecontact);
            CallCriteriaModel model = new CallCriteriaModel(mockUnitOfWork.Object);

            IList<CS_Contact> lstContact = model.ListContactsCriteriaByDivisionAndCustomer(job);

            Assert.AreEqual(1, lstContact.Count);
        }

        #endregion

        [TestMethod]
        public void TestSaveEmployeeCriteria()
        {
            CallCriteriaModel model = new CallCriteriaModel(new FakeUnitOfWork());

            CS_CallCriteria newCriteria = new CS_CallCriteria()
            {
                EmployeeID = 1
            };

            FakeObjectSet<CS_CallCriteriaValue> newCriteriaValueList = new FakeObjectSet<CS_CallCriteriaValue>();
            newCriteriaValueList.AddObject(new CS_CallCriteriaValue()
            {
                CallCriteriaTypeID = 1,
                Value = "Test"
            });

            newCriteriaValueList.AddObject(new CS_CallCriteriaValue()
            {
                CallCriteriaTypeID = 2,
                Value = "Test2"
            });

            Assert.IsTrue(model.SaveCriteria(newCriteria, newCriteriaValueList.ToList(), "Testing"), "Test finalized with no Exceptions but, there may be errors in the Data inside");
        }

        [TestMethod]
        public void TestUpdateEmployeeCriteria()
        {
            //CallCriteriaModel model = new CallCriteriaModel(new FakeUnitOfWork());

            //CS_CallCriteria newCriteria = new CS_CallCriteria()
            //{
            //    EmployeeID = 1
            //};

            //FakeObjectSet<CS_CallCriteriaValue> newCriteriaValueList = new FakeObjectSet<CS_CallCriteriaValue>();
            //newCriteriaValueList.AddObject(new CS_CallCriteriaValue()
            //{
            //    CallCriteriaTypeID = 1,
            //    Value = "Test"
            //});

            //newCriteriaValueList.AddObject(new CS_CallCriteriaValue()
            //{
            //    CallCriteriaTypeID = 2,
            //    Value = "Test2"
            //});

            //Assert.IsTrue(model.UpdateCriteria(newCriteria, newCriteriaValueList.ToList(), "Testing"), "Test finalized with no Exceptions but, there may be errors in the Data inside");
        }

        [TestMethod]
        public void TestGenerateBodyCallLogEmail()
        {
            List<CS_CallLog> lstCallLog = new List<CS_CallLog>();

            CS_CallType callType = new CS_CallType { ID = 5, Active = true, Description = "Call Type 1" };

            DateTime dt = new DateTime(2011, 05, 10, 1, 11, 22);

            CS_CallLog callLog = new CS_CallLog
                                     {
                                         ID = 1,
                                         Active = true,
                                         CS_CallType = callType,
                                         CallDate = dt,
                                         Note = "Here is the field note."
                                     };

            lstCallLog.Add(callLog);

            //Arrange
            FakeObjectSet<CS_CallLog> fakeCallLog = new FakeObjectSet<CS_CallLog>();
            fakeCallLog.AddObject(callLog);

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_CallLog>()).Returns(fakeCallLog);
            CallCriteriaModel model = new CallCriteriaModel(mockUnitOfWork.Object);

            //Act
            string body = model.GenerateBodyCallLogEmail(lstCallLog);

            StringBuilder sb = new StringBuilder();
            sb.Append("<div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Call Type:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append("Call Type 1");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Call Date:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append("05/10/2011");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Call Time:");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append("1:11");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='width: 100%; display: inline-block;'>");
            sb.Append("<div style='text-align: right;width:30%; height:100% ;display: inline-block;float:left'><b>");
            sb.Append("Here is the field note.");
            sb.Append("</b></div>");
            sb.Append("<div style='text-align: left;width:68%; height:100% ;display: inline-block;float:right'>");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("</div>");


            //Assert
            Assert.AreEqual(sb.ToString(), body);
        }

        [TestMethod]
        public void TestIfSubjectIsCorrect()
        {
            // Arrange
            CS_JobInfo jobInfo = new CS_JobInfo()
            {
                JobID = 2,
                CS_Job_JobStatus = new EntityCollection<CS_Job_JobStatus>() { new CS_Job_JobStatus() { ID = 1, JobID = 2, JobStatusId = 1, Active = true } },
                CS_PriceType = new CS_PriceType() { ID = 1, Acronym = "P", Active = true },
                CS_JobType = new CS_JobType() { ID = 1, Description = "C", Active = true },
                CS_JobAction = new CS_JobAction() { ID = 1, Description = "Action", Active = true }
            };
            CS_Job job = new CS_Job()
            {
                ID = 2, Number = "000001", Active = true,
                CS_JobInfo = jobInfo
            };
            CS_CustomerInfo customerInfo = new CS_CustomerInfo()
            {
                JobId = 2,
                CS_Customer = new CS_Customer() { ID = 1, Name = "Customer Name", Active = true }
            };
            CS_LocationInfo locationInfo = new CS_LocationInfo()
            {
                JobID = 2,
                CS_City = new CS_City() { ID = 1, Name = "City", Active = true },
                CS_State = new CS_State() { ID = 1, Acronym = "ST", Active = true }
            };
            string callType = "Call Type Description";

            // Act
            CallCriteriaModel model = new CallCriteriaModel();
            string result = model.GenerateSubjectForCallCriteria(job, jobInfo, customerInfo, locationInfo, callType);

            // Assert
            string expectedResult = "PC000001, Customer Name, Action, City ST, Call Type Description";
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void TestIfSubjectIsCorrectForGeneralLog()
        {
            // Arrange
            CS_Job job = new CS_Job()
            {
                ID = 1,
                Number = "99999",
                Active = true
            };
            string callType = "Call Type Description";

            // Act
            CallCriteriaModel model = new CallCriteriaModel();
            string result = model.GenerateSubjectForCallCriteria(job, null, null, null, callType);

            // Assert
            string expectedResult = "99999 - General Log - Call Type Description";
            Assert.AreEqual(expectedResult, result);
        }
    }
}
