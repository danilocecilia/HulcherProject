using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.DataContext;
using Hulcher.OneSource.CustomerService.Core;

using Moq;
using Hulcher.OneSource.CustomerService.DataContext.VO;

namespace Hulcher.OneSource.CustomerService.Test.Model_Tests
{
    [TestClass]
    public class DPIModelTest
    {
        [TestMethod]
        public void TestIfListDPICalculationJobsIsReturningCorrectJobs()
        {
            // Arrange
            DateTime calculationDate = new DateTime(2011, 8, 3);

            FakeObjectSet<CS_Job> fakeJobList = new FakeObjectSet<CS_Job>();
            fakeJobList.AddObject(
                new CS_Job()
                {
                    Active = true,
                    CS_JobInfo = new CS_JobInfo()
                    {
                        CS_Job_JobStatus = new EntityCollection<CS_Job_JobStatus>()
                        {
                            new CS_Job_JobStatus()
                            {
                                Active = true,
                                JobStatusId = (int)Globals.JobRecord.JobStatus.Active,
                                JobStartDate = calculationDate
                            }
                        }
                    },
                    CS_DPI = null
                }
            );
            fakeJobList.AddObject(
                new CS_Job()
                {
                    Active = true,
                    CS_JobInfo = new CS_JobInfo()
                    {
                        CS_Job_JobStatus = new EntityCollection<CS_Job_JobStatus>()
                        {
                            new CS_Job_JobStatus()
                            {
                                Active = true,
                                JobStatusId = (int)Globals.JobRecord.JobStatus.Active,
                                JobStartDate = calculationDate
                            }
                        }
                    },
                    CS_DPI = new EntityCollection<CS_DPI>()
                    {
                        new CS_DPI()
                        {
                            Active = true,
                            ProcessStatus = (int)Globals.DPI.DpiStatus.DraftSaved,
                            Date = calculationDate
                        }
                    }
                }
            );
            fakeJobList.AddObject(
                new CS_Job()
                {
                    Active = true,
                    CS_JobInfo = new CS_JobInfo()
                    {
                        CS_Job_JobStatus = new EntityCollection<CS_Job_JobStatus>()
                        {
                            new CS_Job_JobStatus()
                            {
                                Active = true,
                                JobStatusId = (int)Globals.JobRecord.JobStatus.Active,
                                JobStartDate = calculationDate.AddDays(-1)
                            }
                        }
                    },
                    CS_DPI = new EntityCollection<CS_DPI>()
                    {
                        new CS_DPI()
                        {
                            Active = true,
                            ProcessStatus = (int)Globals.DPI.DpiStatus.DraftSaved,
                            Date = calculationDate
                        }
                    }
                }
            );

            fakeJobList.AddObject(
                new CS_Job()
                {
                    Active = true,
                    CS_JobInfo = new CS_JobInfo()
                    {
                        CS_Job_JobStatus = new EntityCollection<CS_Job_JobStatus>()
                        {
                            new CS_Job_JobStatus()
                            {
                                Active = true,
                                JobStatusId = (int)Globals.JobRecord.JobStatus.Closed
                            }
                        }
                    },
                    CS_DPI = null
                }
            );

            fakeJobList.AddObject(
                new CS_Job()
                {
                    Active = true,
                    CS_JobInfo = new CS_JobInfo()
                    {
                        CS_Job_JobStatus = new EntityCollection<CS_Job_JobStatus>()
                        {
                            new CS_Job_JobStatus()
                            {
                                Active = true,
                                JobStatusId = (int)Globals.JobRecord.JobStatus.Active,
                                JobStartDate = calculationDate
                            }
                        }
                    },
                    CS_DPI = new EntityCollection<CS_DPI>()
                    {
                        new CS_DPI()
                        {
                            Active = true,
                            ProcessStatus = (int)Globals.DPI.DpiStatus.Approved,
                            Date = calculationDate
                        }
                    }
                }
            );

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_Job>()).Returns(fakeJobList);

            DPIModel model = new DPIModel(mockUnitOfWork.Object);

            // Act
            IList<CS_Job> returnList = model.ListDPICalculationJobs(calculationDate);

            // Assert
            Assert.AreEqual(3, returnList.Count);
        }

        [TestMethod]
        public void TestIfListDPIResourcesIsReturningCorrectResources()
        {
            // Arrange
            int dpiID = 1;

            FakeObjectSet<CS_DPIResource> fakeDpiResourceList = new FakeObjectSet<CS_DPIResource>();
            fakeDpiResourceList.AddObject(
                new CS_DPIResource()
                {
                    DPIID = dpiID,
                    Active = true
                }
            );
            fakeDpiResourceList.AddObject(
                new CS_DPIResource()
                {
                    DPIID = dpiID,
                    Active = true
                }
            );
            fakeDpiResourceList.AddObject(
                new CS_DPIResource()
                {
                    DPIID = dpiID,
                    Active = true
                }
            );
            fakeDpiResourceList.AddObject(
                new CS_DPIResource()
                {
                    DPIID = dpiID,
                    Active = false
                }
            );

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_DPIResource>()).Returns(fakeDpiResourceList);

            DPIModel model = new DPIModel(mockUnitOfWork.Object);

            // Act
            IList<CS_DPIResource> returnList = model.ListDPIResources(dpiID);

            // Assert
            Assert.AreEqual(3, returnList.Count);
        }
        [TestMethod]
        public void MustInsertNewDPIIfNewResourcesAreCreatedForTheJob()
        {
            //Arrange
            FakeObjectSet<CS_Resource> fakeResources = new FakeObjectSet<CS_Resource>();
            fakeResources.AddObject(new CS_Resource() { ID = 1, JobID = 1, Active = true, EquipmentID = 1298, Type = 1 });
            fakeResources.AddObject(new CS_Resource() { ID = 2, JobID = 1, Active = true, EmployeeID = 324972, Type = 2 });
            fakeResources.AddObject(new CS_Resource() { ID = 3, JobID = 1, Active = true, EquipmentID = 123, Type = 1 });

            FakeObjectSet<CS_DPI> fakeDPI = new FakeObjectSet<CS_DPI>();
            fakeDPI.AddObject(new CS_DPI() { ID = 1, Date = DateTime.Now, JobID = 1, Active = true });

            FakeObjectSet<CS_DPIResource> fakeDPIResources = new FakeObjectSet<CS_DPIResource>();
            fakeDPIResources.AddObject(new CS_DPIResource() { ID = 1, DPIID = 1, EquipmentID = 123, Active = true });

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_Resource>()).Returns(fakeResources);
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_DPIResource>()).Returns(fakeDPIResources);
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_DPI>()).Returns(fakeDPI);

            DPIModel dpiModel = new DPIModel(mockUnitOfWork.Object);
            //Act
            dpiModel.InsertDPIResources(DateTime.Now);
            //Assert
            Assert.AreEqual(3, fakeDPIResources.Count());
        }

        [TestMethod]
        public void MustCalculateTotalWorkedHoursByResourceWhenStartAndEndDatesMatchWithoutEmergencyResponse()
        {
            //Arrange
            CS_DPIResource dpiResources = new CS_DPIResource() { ID = 1 };
            List<DPICallLogVO> startCallLog = new List<DPICallLogVO>();
            List<DPICallLogVO> endCallLog = new List<DPICallLogVO>();
            startCallLog.Add(new DPICallLogVO() { ID = 1, ActionTime = new DateTime(2011, 8, 9, 8, 0, 0) });
            endCallLog.Add(new DPICallLogVO() { ID = 2, ActionTime = new DateTime(2011, 8, 9, 12, 0, 0) });
            startCallLog.Add(new DPICallLogVO() { ID = 3, ActionTime = new DateTime(2011, 8, 9, 13, 0, 0) });
            endCallLog.Add(new DPICallLogVO() { ID = 4, ActionTime = new DateTime(2011, 8, 9, 18, 0, 0) });
            DPIModel model = new DPIModel();
            //Act
            model.CalculateHoursForResouce(dpiResources, false, new DateTime(), startCallLog, endCallLog, new DateTime(2011, 8, 9, 0, 0, 0));
            //Assert
            Assert.AreEqual(9, dpiResources.Hours);
        }

        [TestMethod]
        public void MustCalculateTotalWorkedHoursByResourceWhenStartAndEndDatesMatchWithoutEmergencyResponseWithMultipleValues()
        {
            //Arrange
            CS_DPIResource dpiResources = new CS_DPIResource() { ID = 1 };
            List<DPICallLogVO> startCallLog = new List<DPICallLogVO>();
            List<DPICallLogVO> endCallLog = new List<DPICallLogVO>();
            startCallLog.Add(new DPICallLogVO() { ID = 1, ActionTime = new DateTime(2011, 8, 9, 8, 0, 0) });
            startCallLog.Add(new DPICallLogVO() { ID = 3, ActionTime = new DateTime(2011, 8, 9, 9, 0, 0) });
            endCallLog.Add(new DPICallLogVO() { ID = 2, ActionTime = new DateTime(2011, 8, 9, 12, 0, 0) });
            startCallLog.Add(new DPICallLogVO() { ID = 1, ActionTime = new DateTime(2011, 8, 9, 14, 0, 0) });
            endCallLog.Add(new DPICallLogVO() { ID = 4, ActionTime = new DateTime(2011, 8, 9, 18, 0, 0) });
            DPIModel model = new DPIModel();
            //Act
            model.CalculateHoursForResouce(dpiResources, false, new DateTime(), startCallLog, endCallLog, new DateTime(2011, 8, 9, 0, 0, 0));
            //Assert
            Assert.AreEqual(8, dpiResources.Hours);
        }

        [TestMethod]
        public void MustCalculateTotalWorkedHoursByResourceWhenMissesEndDateWithoutEmergencyResponse()
        {
            //Arrange
            CS_DPIResource dpiResources = new CS_DPIResource() { ID = 1 };
            List<DPICallLogVO> startCallLog = new List<DPICallLogVO>();
            List<DPICallLogVO> endCallLog = new List<DPICallLogVO>();
            startCallLog.Add(
                new DPICallLogVO()
                {
                    ID = 1,
                    ActionTime = DateTime.Now.AddHours(-6)
                });
            endCallLog.Add(new DPICallLogVO()
            {
                ID = 2,
                ActionTime = DateTime.Now.AddHours(-4)
            });
            startCallLog.Add(new DPICallLogVO()
            {
                ID = 3,
                ActionTime = DateTime.Now.AddHours(-2)
            });
            DPIModel model = new DPIModel();
            //Act
            model.CalculateHoursForResouce(dpiResources, false, new DateTime(), startCallLog, endCallLog, DateTime.Now.Date);
            //Assert
            Assert.AreEqual(Math.Round(Convert.ToDecimal(2 + DateTime.Now.Subtract(startCallLog[1].ActionTime).TotalHours), 2), Math.Round(dpiResources.Hours, 2));
        }

        [TestMethod]
        public void MustCalculateTotalWorkedHoursByResourceWhenMissesEndDatesWithoutEmergencyResponseWithMultipleValues()
        {
            //Arrange
            CS_DPIResource dpiResources = new CS_DPIResource() { ID = 1 };
            List<DPICallLogVO> startCallLog = new List<DPICallLogVO>();
            List<DPICallLogVO> endCallLog = new List<DPICallLogVO>();
            startCallLog.Add(
                new DPICallLogVO()
                {
                    ID = 1,
                    ActionTime = DateTime.Now.AddHours(-10)
                });
            startCallLog.Add(
                new DPICallLogVO()
                {
                    ID = 2,
                    ActionTime = DateTime.Now.AddHours(-8)
                });
            endCallLog.Add(new DPICallLogVO()
            {
                ID = 3,
                ActionTime = DateTime.Now.AddHours(-6)
            });
            endCallLog.Add(new DPICallLogVO()
            {
                ID = 4,
                ActionTime = DateTime.Now.AddHours(-4)
            });
            startCallLog.Add(new DPICallLogVO()
            {
                ID = 4,
                ActionTime = DateTime.Now.AddHours(-2)
            });
            DPIModel model = new DPIModel();
            //Act
            model.CalculateHoursForResouce(dpiResources, false, new DateTime(), startCallLog, endCallLog, DateTime.Now.Date);
            //Assert
            Assert.AreEqual(Convert.ToInt32(6 + DateTime.Now.Subtract(startCallLog[2].ActionTime).TotalHours), Convert.ToInt32(dpiResources.Hours));
        }

        [TestMethod]
        public void MustCalculateTotalWorkedHoursByResourceWhenStartAndEndDatesMatchWithEmergencyResponse()
        {
            //Arrange
            CS_DPIResource dpiResources = new CS_DPIResource() { ID = 1 };
            List<DPICallLogVO> startCallLog = new List<DPICallLogVO>();
            List<DPICallLogVO> endCallLog = new List<DPICallLogVO>();
            startCallLog.Add(new DPICallLogVO() { ID = 1, ActionTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 9, 0, 0) });
            endCallLog.Add(new DPICallLogVO() { ID = 2, ActionTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 13, 0, 0) });
            endCallLog.Add(new DPICallLogVO() { ID = 3, ActionTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 15, 0, 0) });
            DPIModel model = new DPIModel();
            //Act
            model.CalculateHoursForResouce(dpiResources, true, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 11, 0, 0), startCallLog, endCallLog, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0));
            //Assert
            Assert.AreEqual(4, dpiResources.Hours);
        }

        [TestMethod]
        public void MustCalculateTotalWorkedHoursByResourceWhenMissesEndDateWithEmergencyResponse()
        {
            //Arrange
            DateTime currentDate = DateTime.Now.Date.AddHours(3);
            CS_DPIResource dpiResources = new CS_DPIResource() { ID = 1 };
            List<DPICallLogVO> startCallLog = new List<DPICallLogVO>();
            List<DPICallLogVO> endCallLog = new List<DPICallLogVO>();
            startCallLog.Add(
                new DPICallLogVO()
                {
                    ID = 1,
                    ActionTime = currentDate.AddHours(-10)
                });
            endCallLog.Add(new DPICallLogVO()
            {
                ID = 2,
                ActionTime = currentDate.AddHours(-8)
            });
            endCallLog.Add(new DPICallLogVO()
            {
                ID = 3,
                ActionTime = currentDate.AddHours(-6)
            });
            startCallLog.Add(new DPICallLogVO()
            {
                ID = 4,
                ActionTime = currentDate.AddHours(-4)
            });
            
            DPIModel model = new DPIModel();
            
            //Act
            model.CalculateHoursForResouce(
                dpiResources, true,
                currentDate.AddHours(-10), 
                startCallLog, 
                endCallLog, 
                currentDate.Date.AddDays(-1));
            
            //Assert
            Assert.AreEqual(5, Math.Round(dpiResources.Hours, 2)
            );
        }

        [TestMethod]
        public void MustCalculateTotalWorkedHoursByResourceWhenMissesEndDateWithEmergencyResponseWithMultipleValues()
        {
            //Arrange
            DateTime currentDate = DateTime.Now.Date.AddHours(3);
            CS_DPIResource dpiResources = new CS_DPIResource() { ID = 1 };
            List<DPICallLogVO> startCallLog = new List<DPICallLogVO>();
            List<DPICallLogVO> endCallLog = new List<DPICallLogVO>();
            startCallLog.Add(
                new DPICallLogVO()
                {
                    ID = 1,
                    ActionTime = currentDate.AddHours(-12)
                });
            endCallLog.Add(new DPICallLogVO()
            {
                ID = 2,
                ActionTime = currentDate.AddHours(-8)
            });
            endCallLog.Add(new DPICallLogVO()
            {
                ID = 3,
                ActionTime = currentDate.AddHours(-6)
            });
            startCallLog.Add(new DPICallLogVO()
            {
                ID = 4,
                ActionTime = currentDate.AddHours(-4)
            });
            
            DPIModel model = new DPIModel();
            
            //Act
            model.CalculateHoursForResouce(
                dpiResources, 
                true,
                currentDate.AddHours(-8), 
                startCallLog, 
                endCallLog,
                currentDate.Date.AddDays(-1));
            
            //Assert
            Assert.AreEqual(3, Math.Round(dpiResources.Hours, 2));
        }

        [TestMethod]
        public void TestIfTotalIsBeingCalculated()
        {
            // Arrange
            DateTime calculationDate = DateTime.Today;
            CS_DPI dpiEntry = new CS_DPI() { ID = 1, Active = true, ProcessStatus = (short)Globals.DPI.DpiStatus.Pending };
            CS_Job jobEntry = new CS_Job()
            {
                ID = 1,
                Active = true,
                EmergencyResponse = false,
                CS_JobInfo = new CS_JobInfo() { InitialCallDate = calculationDate, InitialCallTime = new TimeSpan(10, 0, 0), Active = true }
            };

            FakeObjectSet<CS_DPIResource> fakeDPIResource = new FakeObjectSet<CS_DPIResource>();
            fakeDPIResource.AddObject(new CS_DPIResource()
            {
                DPIID = dpiEntry.ID,
                CS_DPI = dpiEntry,
                EquipmentID = 1
            });
            fakeDPIResource.AddObject(new CS_DPIResource()
            {
                DPIID = dpiEntry.ID,
                CS_DPI = dpiEntry,
                EmployeeID = 1
            });

            FakeObjectSet<CS_CallLogResource> fakeCallLogResource = new FakeObjectSet<CS_CallLogResource>();
            fakeCallLogResource.AddObject(new CS_CallLogResource()
            {
                CS_CallLog = new CS_CallLog
                {
                    CS_CallType = new CS_CallType()
                    {
                        ID = 13,
                        IsAutomaticProcess = false,
                        DpiStatus = (int)Globals.DPI.CallTypeDpiStatus.Start,
                        Active = true
                    },
                    Xml = string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?><DynamicFieldsAggregator xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><Controls><DynamicControls xsi:type=\"DynamicDatePickerXml\"><Name>dtpDate</Name><Label><Text>Work Date Continuing:</Text><Css>dynamicLabel</Css><Style /></Label><Css /><Style /><Visible>true</Visible><Text>{0}</Text><IsValidEmpty>false</IsValidEmpty><EmptyValueMessage>The Work Date Continuing field is required</EmptyValueMessage><InvalidValueMessage>Invalid Work Date Continuing format</InvalidValueMessage><DateTimeFormat>Default</DateTimeFormat><ShowOn>Both</ShowOn><ValidationGroup>CallEntry</ValidationGroup></DynamicControls><DynamicControls xsi:type=\"DynamicTimeXml\"><Name>txtTime</Name><Label><Text>Work Time Continuing:</Text><Css>dynamicLabel</Css><Style /></Label><Css>input</Css><Style /><Visible>true</Visible><Text>{1}</Text><Mask>99:99</Mask><MaskedType>Time</MaskedType><InputDirection>LeftToRight</InputDirection><IsValidEmpty>false</IsValidEmpty><ValidationGroup>CallEntry</ValidationGroup><InvalidValueMessage>The Work Time Continuing field is invalid</InvalidValueMessage><EmptyValueMessage>The Work Time Continuing field is required.</EmptyValueMessage></DynamicControls><DynamicControls xsi:type=\"DynamicCountableTextBoxXml\"><Name>txtNote</Name><Label><Text>Note:</Text><Css>dynamicLabel</Css><Style /></Label><Css>input</Css><Style /><Visible>true</Visible><MaxChars>255</MaxChars><MaxCharsWarning>250</MaxCharsWarning><Text>They're BACK!</Text><IsRequired>false</IsRequired><ErrorMessage /><ValidationGroup /><TextMode>MultiLine</TextMode><Width>300</Width><Height>150</Height></DynamicControls></Controls><Extenders /></DynamicFieldsAggregator>",
                        calculationDate.ToString("yyyy-MM-dd"), "10:00"),
                    CreationDate = new DateTime(calculationDate.Year, calculationDate.Month, calculationDate.Day, 10, 0, 0)
                },
                JobID = 1,
                CallLogID = 1,
                EquipmentID = 1,
                CreationDate = new DateTime(calculationDate.Year, calculationDate.Month, calculationDate.Day, 10, 0, 0)
            });
            fakeCallLogResource.AddObject(new CS_CallLogResource()
            {
                CS_CallLog = new CS_CallLog
                {
                    CS_CallType = new CS_CallType()
                    {
                        ID = 18,
                        IsAutomaticProcess = true,
                        DpiStatus = (int)Globals.DPI.CallTypeDpiStatus.End,
                        Active = true
                    },
                    Xml = string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?><DynamicFieldsAggregator xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><Controls><DynamicControls xsi:type=\"DynamicDatePickerXml\"><Name>dtpDate</Name><Label><Text>Work Date Released:</Text><Css>dynamicLabel</Css><Style /></Label><Css /><Style /><Visible>true</Visible><Text>{0}</Text><IsValidEmpty>false</IsValidEmpty><EmptyValueMessage>The Work Date Released field is required</EmptyValueMessage><InvalidValueMessage>Invalid Work Date format</InvalidValueMessage><DateTimeFormat>Default</DateTimeFormat><ShowOn>Both</ShowOn><ValidationGroup>CallEntry</ValidationGroup></DynamicControls><DynamicControls xsi:type=\"DynamicTimeXml\"><Name>txtTime</Name><Label><Text>Work Time Released:</Text><Css>dynamicLabel</Css><Style /></Label><Css /><Style /><Visible>true</Visible><Text>{1}</Text><Mask>99:99</Mask><MaskedType>Time</MaskedType><InputDirection>LeftToRight</InputDirection><IsValidEmpty>false</IsValidEmpty><ValidationGroup>CallEntry</ValidationGroup><InvalidValueMessage>The Work Time Released Time field is invalid</InvalidValueMessage><EmptyValueMessage>The Work Time Released field is required.</EmptyValueMessage></DynamicControls><DynamicControls xsi:type=\"DynamicCountableTextBoxXml\"><Name>txtNote</Name><Label><Text>Note:</Text><Css>dynamicLabel</Css><Style /></Label><Css /><Style /><Visible>true</Visible><MaxChars>255</MaxChars><MaxCharsWarning>250</MaxCharsWarning><Text>They stopped working!</Text><IsRequired>false</IsRequired><ErrorMessage /><ValidationGroup /><TextMode>MultiLine</TextMode><Width>300</Width><Height>150</Height></DynamicControls></Controls><Extenders /></DynamicFieldsAggregator>",
                        calculationDate.ToString("yyyy-MM-dd"), "15:00"),
                    CreationDate = new DateTime(calculationDate.Year, calculationDate.Month, calculationDate.Day, 15, 0, 0)
                },
                JobID = 1,
                CallLogID = 2,
                EquipmentID = 1,
                CreationDate = new DateTime(calculationDate.Year, calculationDate.Month, calculationDate.Day, 15, 0, 0)
            });

            fakeCallLogResource.AddObject(new CS_CallLogResource()
            {
                CS_CallLog = new CS_CallLog
                {
                    CS_CallType = new CS_CallType()
                    {
                        ID = 13,
                        IsAutomaticProcess = false,
                        DpiStatus = (int)Globals.DPI.CallTypeDpiStatus.Start,
                        Active = true
                    },
                    Xml = string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?><DynamicFieldsAggregator xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><Controls><DynamicControls xsi:type=\"DynamicDatePickerXml\"><Name>dtpDate</Name><Label><Text>Work Date Continuing:</Text><Css>dynamicLabel</Css><Style /></Label><Css /><Style /><Visible>true</Visible><Text>{0}</Text><IsValidEmpty>false</IsValidEmpty><EmptyValueMessage>The Work Date Continuing field is required</EmptyValueMessage><InvalidValueMessage>Invalid Work Date Continuing format</InvalidValueMessage><DateTimeFormat>Default</DateTimeFormat><ShowOn>Both</ShowOn><ValidationGroup>CallEntry</ValidationGroup></DynamicControls><DynamicControls xsi:type=\"DynamicTimeXml\"><Name>txtTime</Name><Label><Text>Work Time Continuing:</Text><Css>dynamicLabel</Css><Style /></Label><Css>input</Css><Style /><Visible>true</Visible><Text>{1}</Text><Mask>99:99</Mask><MaskedType>Time</MaskedType><InputDirection>LeftToRight</InputDirection><IsValidEmpty>false</IsValidEmpty><ValidationGroup>CallEntry</ValidationGroup><InvalidValueMessage>The Work Time Continuing field is invalid</InvalidValueMessage><EmptyValueMessage>The Work Time Continuing field is required.</EmptyValueMessage></DynamicControls><DynamicControls xsi:type=\"DynamicCountableTextBoxXml\"><Name>txtNote</Name><Label><Text>Note:</Text><Css>dynamicLabel</Css><Style /></Label><Css>input</Css><Style /><Visible>true</Visible><MaxChars>255</MaxChars><MaxCharsWarning>250</MaxCharsWarning><Text>They're BACK!</Text><IsRequired>false</IsRequired><ErrorMessage /><ValidationGroup /><TextMode>MultiLine</TextMode><Width>300</Width><Height>150</Height></DynamicControls></Controls><Extenders /></DynamicFieldsAggregator>",
                        calculationDate.ToString("yyyy-MM-dd"), "10:00"),
                    CreationDate = new DateTime(calculationDate.Year, calculationDate.Month, calculationDate.Day, 10, 0, 0)
                },
                JobID = 1,
                CallLogID = 3,
                EmployeeID = 1,
                CreationDate = new DateTime(calculationDate.Year, calculationDate.Month, calculationDate.Day, 10, 0, 0)
            });
            fakeCallLogResource.AddObject(new CS_CallLogResource()
            {
                CS_CallLog = new CS_CallLog
                {
                    CS_CallType = new CS_CallType()
                    {
                        ID = 18,
                        IsAutomaticProcess = true,
                        DpiStatus = (int)Globals.DPI.CallTypeDpiStatus.End,
                        Active = true
                    },
                    Xml = string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?><DynamicFieldsAggregator xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><Controls><DynamicControls xsi:type=\"DynamicDatePickerXml\"><Name>dtpDate</Name><Label><Text>Work Date Released:</Text><Css>dynamicLabel</Css><Style /></Label><Css /><Style /><Visible>true</Visible><Text>{0}</Text><IsValidEmpty>false</IsValidEmpty><EmptyValueMessage>The Work Date Released field is required</EmptyValueMessage><InvalidValueMessage>Invalid Work Date format</InvalidValueMessage><DateTimeFormat>Default</DateTimeFormat><ShowOn>Both</ShowOn><ValidationGroup>CallEntry</ValidationGroup></DynamicControls><DynamicControls xsi:type=\"DynamicTimeXml\"><Name>txtTime</Name><Label><Text>Work Time Released:</Text><Css>dynamicLabel</Css><Style /></Label><Css /><Style /><Visible>true</Visible><Text>{1}</Text><Mask>99:99</Mask><MaskedType>Time</MaskedType><InputDirection>LeftToRight</InputDirection><IsValidEmpty>false</IsValidEmpty><ValidationGroup>CallEntry</ValidationGroup><InvalidValueMessage>The Work Time Released Time field is invalid</InvalidValueMessage><EmptyValueMessage>The Work Time Released field is required.</EmptyValueMessage></DynamicControls><DynamicControls xsi:type=\"DynamicCountableTextBoxXml\"><Name>txtNote</Name><Label><Text>Note:</Text><Css>dynamicLabel</Css><Style /></Label><Css /><Style /><Visible>true</Visible><MaxChars>255</MaxChars><MaxCharsWarning>250</MaxCharsWarning><Text>They stopped working!</Text><IsRequired>false</IsRequired><ErrorMessage /><ValidationGroup /><TextMode>MultiLine</TextMode><Width>300</Width><Height>150</Height></DynamicControls></Controls><Extenders /></DynamicFieldsAggregator>",
                        calculationDate.ToString("yyyy-MM-dd"), "16:00"),
                    CreationDate = new DateTime(calculationDate.Year, calculationDate.Month, calculationDate.Day, 16, 0, 0)
                },
                JobID = 1,
                CallLogID = 4,
                EmployeeID = 1,
                CreationDate = new DateTime(calculationDate.Year, calculationDate.Month, calculationDate.Day, 16, 0, 0)
            });

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(e => e.CreateObjectSet<CS_DPIResource>()).Returns(fakeDPIResource);
            mockUnitOfWork.Setup(e => e.CreateObjectSet<CS_CallLogResource>()).Returns(fakeCallLogResource);

            // Act
            DPIModel model = new DPIModel(mockUnitOfWork.Object);
            IList<CS_DPIResource> returnList = model.ProcessDPIResources(dpiEntry, jobEntry, calculationDate);

            // Assert
            CS_DPIResource equipment = returnList.FirstOrDefault(e => e.EquipmentID == 1);
            CS_DPIResource employee = returnList.FirstOrDefault(e => e.EmployeeID == 1);
            Assert.AreEqual(equipment.Hours * equipment.Rate, equipment.Total);
            Assert.AreEqual(employee.Hours * employee.Rate, employee.Total);
        }

        [TestMethod]
        public void TestIfIsReturningDPIsToBeCalculated()
        {
            // Arrange
            DateTime calculationDate = DateTime.Today;

            FakeObjectSet<CS_DPI> fakeDpi = new FakeObjectSet<CS_DPI>();
            fakeDpi.AddObject(new CS_DPI()
            {
                Date = calculationDate,
                Active = true,
                JobID = 1,
                Calculate = false
            });
            fakeDpi.AddObject(new CS_DPI()
            {
                Date = calculationDate,
                Active = true,
                JobID = 2,
                Calculate = false
            });

            fakeDpi.AddObject(new CS_DPI()
            {
                Date = calculationDate,
                Active = true,
                JobID = 3,
                Calculate = false
            });

            fakeDpi.AddObject(new CS_DPI()
            {
                Date = calculationDate.AddDays(-1),
                Active = true,
                JobID = 4,
                Calculate = true
            });

            fakeDpi.AddObject(new CS_DPI()
            {
                Date = calculationDate.AddDays(-1),
                Active = true,
                JobID = 1,
                Calculate = false
            });

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_DPI>()).Returns(fakeDpi);

            // Act
            DPIModel model = new DPIModel(mockUnitOfWork.Object);
            IList<CS_DPI> returnList = model.ListDPIsToBeCalculated(calculationDate);

            // Assert
            Assert.AreEqual(4, returnList.Count);
        }

        [TestMethod]
        public void TestIfIsInsertingDPIResources()
        {
            // Arrange
            DateTime calculationDate = DateTime.Today;
            FakeObjectSet<CS_DPI> fakeDPI = new FakeObjectSet<CS_DPI>();
            fakeDPI.AddObject(new CS_DPI()
            {
                ID = 1,
                JobID = 1,
                Date = calculationDate,
                Active = true
            });

            FakeObjectSet<CS_Resource> fakeResource = new FakeObjectSet<CS_Resource>();
            fakeResource.AddObject(new CS_Resource()
            {
                ID = 1,
                JobID = 1,
                EquipmentID = 1
            });
            fakeResource.AddObject(new CS_Resource()
            {
                ID = 1,
                JobID = 1,
                EmployeeID = 1
            });

            FakeObjectSet<CS_DPIResource> fakeDPIResource = new FakeObjectSet<CS_DPIResource>();

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_DPI>()).Returns(fakeDPI);
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_Resource>()).Returns(fakeResource);
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_DPIResource>()).Returns(fakeDPIResource);

            // Act
            DPIModel model = new DPIModel(mockUnitOfWork.Object);
            model.InsertDPIResources(calculationDate);

            // Assert
            Assert.AreEqual(2, fakeDPIResource.ToList().Count);
        }

        [TestMethod]
        public void MustReturnCorrectMTDCountOnDPI()
        {
            //Arrange
            FakeObjectSet<CS_DPI> fakeDPI = new FakeObjectSet<CS_DPI>();
            fakeDPI.AddObject(new CS_DPI()
            {
                ID = 1,
                JobID = 1,
                Date = new DateTime(2011,07,25),
                Active = true,
                ProcessStatus = (int)Globals.DPI.DpiStatus.Approved
            });
            fakeDPI.AddObject(new CS_DPI()
            {
                ID = 1,
                JobID = 1,
                Date = new DateTime(2011, 07, 30),
                Active = true,
                ProcessStatus = (int)Globals.DPI.DpiStatus.Approved
            });
            fakeDPI.AddObject(new CS_DPI()
            {
                ID = 1,
                JobID = 1,
                Date = new DateTime(2011, 08, 10),
                Active = true,
                ProcessStatus = (int)Globals.DPI.DpiStatus.Approved
            });
            fakeDPI.AddObject(new CS_DPI()
            {
                ID = 1,
                JobID = 1,
                Date = new DateTime(2011, 08, 13),
                Active = true,
                ProcessStatus = (int)Globals.DPI.DpiStatus.Approved
            });
            fakeDPI.AddObject(new CS_DPI()
            {
                ID = 1,
                JobID = 2,
                Date = new DateTime(2011, 08, 20),
                Active = true,
                ProcessStatus = (int)Globals.DPI.DpiStatus.Approved
            });
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(w => w.CreateObjectSet<CS_DPI>()).Returns(fakeDPI);
            DateTime currentDate = new DateTime(2011, 08, 15);
            DPIModel model = new DPIModel(mockUnitOfWork.Object);
            //Act
            int count = model.GetDPIReportMTDCount(currentDate);
            //Assert
            Assert.AreEqual(1, count);
        }
    }
}
