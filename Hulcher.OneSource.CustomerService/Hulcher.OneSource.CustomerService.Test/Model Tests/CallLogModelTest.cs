using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hulcher.OneSource.CustomerService.Business.Model;
using Hulcher.OneSource.CustomerService.DataContext;
using System.Web.UI.WebControls;
using Hulcher.OneSource.CustomerService.Core.ViewInterfaces;
using Moq;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields;
using Hulcher.OneSource.CustomerService.Business.WebControls.ServerControls;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Xml;
using Hulcher.OneSource.CustomerService.Business.WebControls.DynamicFields.Base;
using Hulcher.OneSource.CustomerService.Business.WebControls.Utils;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Data;
using Hulcher.OneSource.CustomerService.DataContext.EntityExtensions;
using Hulcher.OneSource.CustomerService.Core;
using System.Data.Objects.DataClasses;

namespace Hulcher.OneSource.CustomerService.Test.Model_Tests
{
    [TestClass]
    public class CallLogModelTest
    {
        [TestMethod]
        public void VerifyDPICalculateAdded()
        {
            //Arrange
            #region [ Variables ]

            DateTime now = DateTime.Now;
            DateTime startDateTime = new DateTime(2011, 08, 01, 12, 0, 0);
            DateTime startDateTime2 = startDateTime.AddDays(1);

            #endregion

            #region [ CallType Repository ]

            FakeObjectSet<CS_CallType> fakeCallTypeRepository = new FakeObjectSet<CS_CallType>();
            fakeCallTypeRepository.AddObject
                (
                    new CS_CallType()
                    {
                        ID = 27,
                        Description = "Added Resource",
                        Xml = string.Empty,
                        CallCriteria = true,
                        IsAutomaticProcess = true,
                        DpiStatus = 1,
                        Active = true
                    }
                );

            #endregion

            #region [ Resource Repository ]

            FakeObjectSet<CS_Resource> fakeResourceRepository = new FakeObjectSet<CS_Resource>();
            fakeResourceRepository.AddObject(new CS_Resource() { ID = 1, EmployeeID = 1, EquipmentID = null, JobID = 1, Duration = 1, StartDateTime = startDateTime, Active = true });
            fakeResourceRepository.AddObject(new CS_Resource() { ID = 2, EmployeeID = null, EquipmentID = 1, JobID = 1, Duration = 1, StartDateTime = startDateTime, Active = true });
            fakeResourceRepository.AddObject(new CS_Resource() { ID = 3, EmployeeID = null, EquipmentID = 2, JobID = 1, Duration = 1, StartDateTime = startDateTime, Active = true });
            fakeResourceRepository.AddObject(new CS_Resource() { ID = 4, EmployeeID = null, EquipmentID = 3, JobID = 1, Duration = 1, StartDateTime = startDateTime2, Active = true });

            #endregion

            #region [ DPI Repository ]

            FakeObjectSet<CS_DPI> fakeDPIRepository = new FakeObjectSet<CS_DPI>();
            fakeDPIRepository.AddObject(new CS_DPI()
            {
                ID = 1,
                Date = startDateTime.Date,
                Calculate = false,
                ProcessStatus = (int)Globals.DPI.DpiStatus.DraftSaved,
                JobID = 1,
                IsContinuing = true,
                ProcessStatusDate = DateTime.Now,
                CalculationStatus = (short)Globals.DPI.CalculationStatus.INSF,
                Total = 1000,
                CreatedBy = "System",
                //CreationID =,
                CreationDate = DateTime.Now,
                ModifiedBy = "System",
                //ModificationID,
                ModificationDate = DateTime.Now,
                Active = true,
                CS_DPIResource = new EntityCollection<CS_DPIResource>()
                {
                    new CS_DPIResource()
                    {
                        ID = 1,
                        DPIID = 1,
                        EmployeeID = 1,
                        Active = true
                    },
                    new CS_DPIResource()
                    {
                        ID = 1,
                        DPIID = 1,
                        EquipmentID = 1,
                        Active = true
                    },
                    new CS_DPIResource()
                    {
                        ID = 1,
                        DPIID = 1,
                        EquipmentID = 2,
                        Active = true,
                    }
                }
            });

            fakeDPIRepository.AddObject(new CS_DPI()
            {
                ID = 2,
                Date = startDateTime2.Date,
                Calculate = false,
                ProcessStatus = (int)Globals.DPI.DpiStatus.DraftSaved,
                JobID = 1,
                IsContinuing = true,
                ProcessStatusDate = DateTime.Now,
                CalculationStatus = (short)Globals.DPI.CalculationStatus.INSF,
                Total = 1000,
                CreatedBy = "System",
                //CreationID =,
                CreationDate = DateTime.Now,
                ModifiedBy = "System",
                //ModificationID,
                ModificationDate = DateTime.Now,
                Active = true,
                CS_DPIResource = new EntityCollection<CS_DPIResource>()
                {
                    new CS_DPIResource()
                    {
                        ID = 1,
                        DPIID = 1,
                        EmployeeID = 1,
                        Active = true
                    },
                    new CS_DPIResource()
                    {
                        ID = 1,
                        DPIID = 1,
                        EquipmentID = 1,
                        Active = true
                    },
                    new CS_DPIResource()
                    {
                        ID = 1,
                        DPIID = 1,
                        EquipmentID = 2,
                        Active = true
                    },
                    new CS_DPIResource()
                    {
                        ID = 1,
                        DPIID = 1,
                        EquipmentID = 3,
                        Active = true
                    }
                }
            });

            #endregion

            #region [ Parameters ]

            CS_CallLog callLog = new CS_CallLog()
            {
                CallTypeID = (int)Globals.CallEntry.CallType.AddedResource,
                CreationDate = now,
                CallDate = now,
                JobID = 1
            };

            List<int> employeeIDList = new List<int>() { 1 };
            List<int> equipmentIDList = new List<int>() { 1, 2 };

            #endregion

            #region [ Mock ]

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(e => e.CreateObjectSet<CS_CallType>()).Returns(fakeCallTypeRepository);
            mockUnitOfWork.Setup(e => e.CreateObjectSet<CS_Resource>()).Returns(fakeResourceRepository);
            mockUnitOfWork.Setup(e => e.CreateObjectSet<CS_DPI>()).Returns(fakeDPIRepository);
            CallLogModel model = new CallLogModel(mockUnitOfWork.Object);

            #endregion

            //Act
            model.VerifyDPICalculate(callLog, employeeIDList, equipmentIDList);

            //Assert
            CS_DPI dpi = fakeDPIRepository.FirstOrDefault(e => e.ID == 1);
            CS_DPI dpi2 = fakeDPIRepository.FirstOrDefault(e => e.ID == 2);
            Assert.IsTrue(dpi.Calculate, "Fail at Calculate True");
            Assert.IsFalse(dpi2.Calculate, "Fail at Calculate False");
        }

        [TestMethod]
        public void VerifyDPICalculateParked()
        {
            //Arrange
            #region [ Variables ]

            DateTime now = DateTime.Now;
            DateTime startDateTime = new DateTime(2011, 08, 01, 12, 0, 0);
            DateTime startDateTime2 = startDateTime.AddDays(1);

            #endregion

            #region [ CallType Repository ]

            FakeObjectSet<CS_CallType> fakeCallTypeRepository = new FakeObjectSet<CS_CallType>();
            fakeCallTypeRepository.AddObject
                (
                    new CS_CallType()
                    {
                        ID = (int)Globals.CallEntry.CallType.Parked,
                        Description = "Parked",
                        Xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>  <DynamicFieldsAggregator xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">    <Controls>      <DynamicControls xsi:type=\"DynamicDatePickerXml\">        <Name>dtpDate</Name>        <IsValidEmpty>false</IsValidEmpty>        <EmptyValueMessage>The Parked Date field is required</EmptyValueMessage>        <InvalidValueMessage>Invalid Parked Date format</InvalidValueMessage>        <DateTimeFormat>Default</DateTimeFormat>        <ShowOn>Both</ShowOn>        <ValidationGroup>CallEntry</ValidationGroup>        <Label>          <Text>Parked Date:</Text>          <Css>dynamicLabel</Css>        </Label>      </DynamicControls>      <DynamicControls xsi:type=\"DynamicTimeXml\">        <Name>txtTime</Name>        <IsValidEmpty>false</IsValidEmpty>        <ValidationGroup>CallEntry</ValidationGroup>        <EmptyValueMessage>The Parked Time field is required.</EmptyValueMessage>        <InvalidValueMessage>The Parked Time field is invalid</InvalidValueMessage>        <MaskedType>Time</MaskedType>        <Mask>99:99</Mask>        <Label>          <Text>Parked Time:</Text>          <Css>dynamicLabel</Css>        </Label>      </DynamicControls>      <DynamicControls xsi:type=\"DynamicCountableTextBoxXml\">        <Name>txtNote</Name>        <IsRequired>false</IsRequired>        <MaxChars>255</MaxChars>        <MaxCharsWarning>250</MaxCharsWarning>        <TextMode>MultiLine</TextMode>        <Width>300</Width>        <Height>150</Height>        <Label>          <Text>Note:</Text>          <Css>dynamicLabel</Css>        </Label>      </DynamicControls>    </Controls>  </DynamicFieldsAggregator>",
                        CallCriteria = false,
                        IsAutomaticProcess = false,
                        DpiStatus = 2,
                        Active = true
                    }
                );

            #endregion

            #region [ Resource Repository ]

            FakeObjectSet<CS_Resource> fakeResourceRepository = new FakeObjectSet<CS_Resource>();
            fakeResourceRepository.AddObject(new CS_Resource() { ID = 1, EmployeeID = 1, EquipmentID = null, JobID = 1, Duration = 1, StartDateTime = startDateTime, Active = true });
            fakeResourceRepository.AddObject(new CS_Resource() { ID = 2, EmployeeID = null, EquipmentID = 1, JobID = 1, Duration = 1, StartDateTime = startDateTime, Active = true });
            fakeResourceRepository.AddObject(new CS_Resource() { ID = 3, EmployeeID = null, EquipmentID = 2, JobID = 1, Duration = 1, StartDateTime = startDateTime, Active = true });
            fakeResourceRepository.AddObject(new CS_Resource() { ID = 4, EmployeeID = null, EquipmentID = 3, JobID = 1, Duration = 1, StartDateTime = startDateTime2, Active = true });

            #endregion

            #region [ DPI Repository ]

            FakeObjectSet<CS_DPI> fakeDPIRepository = new FakeObjectSet<CS_DPI>();
            fakeDPIRepository.AddObject(new CS_DPI()
            {
                ID = 1,
                Date = startDateTime.Date,
                Calculate = false,
                ProcessStatus = (int)Globals.DPI.DpiStatus.DraftSaved,
                JobID = 1,
                IsContinuing = true,
                ProcessStatusDate = DateTime.Now,
                CalculationStatus = (short)Globals.DPI.CalculationStatus.INSF,
                Total = 1000,
                CreatedBy = "System",
                //CreationID =,
                CreationDate = DateTime.Now,
                ModifiedBy = "System",
                //ModificationID,
                ModificationDate = DateTime.Now,
                Active = true,
                CS_DPIResource = new EntityCollection<CS_DPIResource>()
                {
                    new CS_DPIResource()
                    {
                        ID = 1,
                        DPIID = 1,
                        EmployeeID = 1,
                        Active = true
                    },
                    new CS_DPIResource()
                    {
                        ID = 1,
                        DPIID = 1,
                        EquipmentID = 1,
                        Active = true
                    },
                    new CS_DPIResource()
                    {
                        ID = 1,
                        DPIID = 1,
                        EquipmentID = 2,
                        Active = true,
                    }
                }
            });

            fakeDPIRepository.AddObject(new CS_DPI()
            {
                ID = 2,
                Date = startDateTime2.Date,
                Calculate = false,
                ProcessStatus = (int)Globals.DPI.DpiStatus.DraftSaved,
                JobID = 1,
                IsContinuing = true,
                ProcessStatusDate = DateTime.Now,
                CalculationStatus = (short)Globals.DPI.CalculationStatus.INSF,
                Total = 1000,
                CreatedBy = "System",
                //CreationID =,
                CreationDate = DateTime.Now,
                ModifiedBy = "System",
                //ModificationID,
                ModificationDate = DateTime.Now,
                Active = true,
                CS_DPIResource = new EntityCollection<CS_DPIResource>()
                {
                    new CS_DPIResource()
                    {
                        ID = 1,
                        DPIID = 1,
                        EmployeeID = 1,
                        Active = true
                    },
                    new CS_DPIResource()
                    {
                        ID = 1,
                        DPIID = 1,
                        EquipmentID = 1,
                        Active = true
                    },
                    new CS_DPIResource()
                    {
                        ID = 1,
                        DPIID = 1,
                        EquipmentID = 2,
                        Active = true
                    },
                    new CS_DPIResource()
                    {
                        ID = 1,
                        DPIID = 1,
                        EquipmentID = 3,
                        Active = true
                    }
                }
            });

            #endregion

            #region [ Parameters ]

            CS_CallLog callLog = new CS_CallLog()
            {
                CallTypeID = (int)Globals.CallEntry.CallType.Parked,
                Xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><DynamicFieldsAggregator xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><Controls><DynamicControls xsi:type=\"DynamicDatePickerXml\"><Name>dtpDate</Name><Label><Text>Release Date:</Text><Css>dynamicLabel</Css><Style /></Label><Css /><Style /><Visible>true</Visible><Text>2011-08-01T00:00:00</Text><IsValidEmpty>false</IsValidEmpty><EmptyValueMessage>The Release Date field is required</EmptyValueMessage><InvalidValueMessage>Invalid Release Date format</InvalidValueMessage><DateTimeFormat>Default</DateTimeFormat><ShowOn>Both</ShowOn><ValidationGroup>CallEntry</ValidationGroup></DynamicControls><DynamicControls xsi:type=\"DynamicTimeXml\"><Name>txtTime</Name><Label><Text>Release Time:</Text><Css>dynamicLabel</Css><Style /></Label><Css /><Style /><Visible>true</Visible><Text>12:00</Text><Mask>99:99</Mask><MaskedType>Time</MaskedType><InputDirection>LeftToRight</InputDirection><IsValidEmpty>false</IsValidEmpty><ValidationGroup>CallEntry</ValidationGroup><InvalidValueMessage>The Release Time field is invalid</InvalidValueMessage><EmptyValueMessage>The Release Time field is required.</EmptyValueMessage></DynamicControls></Controls><Extenders /></DynamicFieldsAggregator>",
                CreationDate = now,
                CallDate = now,
                JobID = 1
            };

            List<int> employeeIDList = new List<int>() { 1 };
            List<int> equipmentIDList = new List<int>() { 1, 2, 3 };

            #endregion

            #region [ Mock ]

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(e => e.CreateObjectSet<CS_CallType>()).Returns(fakeCallTypeRepository);
            mockUnitOfWork.Setup(e => e.CreateObjectSet<CS_Resource>()).Returns(fakeResourceRepository);
            mockUnitOfWork.Setup(e => e.CreateObjectSet<CS_DPI>()).Returns(fakeDPIRepository);
            CallLogModel model = new CallLogModel(mockUnitOfWork.Object);

            #endregion

            //Act
            model.VerifyDPICalculate(callLog, employeeIDList, equipmentIDList);

            //Assert
            CS_DPI dpi = fakeDPIRepository.FirstOrDefault(e => e.ID == 1);
            CS_DPI dpi2 = fakeDPIRepository.FirstOrDefault(e => e.ID == 2);
            Assert.IsTrue(dpi.Calculate, "Fail at Calculate True");
            Assert.IsFalse(dpi2.Calculate, "Fail at Calculate False");
        }

        [TestMethod]
        public void VerifyDPICalculateParkedNoXml()
        {
            //Arrange
            #region [ Variables ]

            DateTime now = DateTime.Now;
            DateTime startDateTime = new DateTime(2011, 08, 01, 12, 0, 0);
            DateTime startDateTime2 = startDateTime.AddDays(1);

            #endregion

            #region [ CallType Repository ]

            FakeObjectSet<CS_CallType> fakeCallTypeRepository = new FakeObjectSet<CS_CallType>();
            fakeCallTypeRepository.AddObject
                (
                    new CS_CallType()
                    {
                        ID = (int)Globals.CallEntry.CallType.Parked,
                        Description = "Parked",
                        Xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>  <DynamicFieldsAggregator xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">    <Controls>      <DynamicControls xsi:type=\"DynamicDatePickerXml\">        <Name>dtpDate</Name>        <IsValidEmpty>false</IsValidEmpty>        <EmptyValueMessage>The Parked Date field is required</EmptyValueMessage>        <InvalidValueMessage>Invalid Parked Date format</InvalidValueMessage>        <DateTimeFormat>Default</DateTimeFormat>        <ShowOn>Both</ShowOn>        <ValidationGroup>CallEntry</ValidationGroup>        <Label>          <Text>Parked Date:</Text>          <Css>dynamicLabel</Css>        </Label>      </DynamicControls>      <DynamicControls xsi:type=\"DynamicTimeXml\">        <Name>txtTime</Name>        <IsValidEmpty>false</IsValidEmpty>        <ValidationGroup>CallEntry</ValidationGroup>        <EmptyValueMessage>The Parked Time field is required.</EmptyValueMessage>        <InvalidValueMessage>The Parked Time field is invalid</InvalidValueMessage>        <MaskedType>Time</MaskedType>        <Mask>99:99</Mask>        <Label>          <Text>Parked Time:</Text>          <Css>dynamicLabel</Css>        </Label>      </DynamicControls>      <DynamicControls xsi:type=\"DynamicCountableTextBoxXml\">        <Name>txtNote</Name>        <IsRequired>false</IsRequired>        <MaxChars>255</MaxChars>        <MaxCharsWarning>250</MaxCharsWarning>        <TextMode>MultiLine</TextMode>        <Width>300</Width>        <Height>150</Height>        <Label>          <Text>Note:</Text>          <Css>dynamicLabel</Css>        </Label>      </DynamicControls>    </Controls>  </DynamicFieldsAggregator>",
                        CallCriteria = false,
                        IsAutomaticProcess = false,
                        DpiStatus = 2,
                        Active = true
                    }
                );

            #endregion

            #region [ Resource Repository ]

            FakeObjectSet<CS_Resource> fakeResourceRepository = new FakeObjectSet<CS_Resource>();
            fakeResourceRepository.AddObject(new CS_Resource() { ID = 1, EmployeeID = 1, EquipmentID = null, JobID = 1, Duration = 1, StartDateTime = startDateTime, Active = true });
            fakeResourceRepository.AddObject(new CS_Resource() { ID = 2, EmployeeID = null, EquipmentID = 1, JobID = 1, Duration = 1, StartDateTime = startDateTime, Active = true });
            fakeResourceRepository.AddObject(new CS_Resource() { ID = 3, EmployeeID = null, EquipmentID = 2, JobID = 1, Duration = 1, StartDateTime = startDateTime, Active = true });
            fakeResourceRepository.AddObject(new CS_Resource() { ID = 4, EmployeeID = null, EquipmentID = 3, JobID = 1, Duration = 1, StartDateTime = startDateTime2, Active = true });

            #endregion

            #region [ DPI Repository ]

            FakeObjectSet<CS_DPI> fakeDPIRepository = new FakeObjectSet<CS_DPI>();
            fakeDPIRepository.AddObject(new CS_DPI()
            {
                ID = 1,
                Date = startDateTime.Date,
                Calculate = false,
                ProcessStatus = (int)Globals.DPI.DpiStatus.DraftSaved,
                JobID = 1,
                IsContinuing = true,
                ProcessStatusDate = DateTime.Now,
                CalculationStatus = (short)Globals.DPI.CalculationStatus.INSF,
                Total = 1000,
                CreatedBy = "System",
                //CreationID =,
                CreationDate = DateTime.Now,
                ModifiedBy = "System",
                //ModificationID,
                ModificationDate = DateTime.Now,
                Active = true,
                CS_DPIResource = new EntityCollection<CS_DPIResource>()
                {
                    new CS_DPIResource()
                    {
                        ID = 1,
                        DPIID = 1,
                        EmployeeID = 1,
                        Active = true
                    },
                    new CS_DPIResource()
                    {
                        ID = 1,
                        DPIID = 1,
                        EquipmentID = 1,
                        Active = true
                    },
                    new CS_DPIResource()
                    {
                        ID = 1,
                        DPIID = 1,
                        EquipmentID = 2,
                        Active = true,
                    }
                }
            });

            fakeDPIRepository.AddObject(new CS_DPI()
            {
                ID = 2,
                Date = startDateTime2.Date,
                Calculate = false,
                ProcessStatus = (int)Globals.DPI.DpiStatus.DraftSaved,
                JobID = 1,
                IsContinuing = true,
                ProcessStatusDate = DateTime.Now,
                CalculationStatus = (short)Globals.DPI.CalculationStatus.INSF,
                Total = 1000,
                CreatedBy = "System",
                //CreationID =,
                CreationDate = DateTime.Now,
                ModifiedBy = "System",
                //ModificationID,
                ModificationDate = DateTime.Now,
                Active = true,
                CS_DPIResource = new EntityCollection<CS_DPIResource>()
                {
                    new CS_DPIResource()
                    {
                        ID = 1,
                        DPIID = 1,
                        EmployeeID = 1,
                        Active = true
                    },
                    new CS_DPIResource()
                    {
                        ID = 1,
                        DPIID = 1,
                        EquipmentID = 1,
                        Active = true
                    },
                    new CS_DPIResource()
                    {
                        ID = 1,
                        DPIID = 1,
                        EquipmentID = 2,
                        Active = true
                    },
                    new CS_DPIResource()
                    {
                        ID = 1,
                        DPIID = 1,
                        EquipmentID = 3,
                        Active = true
                    }
                }
            });

            #endregion

            #region [ Parameters ]

            CS_CallLog callLog = new CS_CallLog()
            {
                CallTypeID = (int)Globals.CallEntry.CallType.Parked,
                CreationDate = startDateTime,
                CallDate = startDateTime,
                JobID = 1
            };

            List<int> employeeIDList = new List<int>() { 1 };
            List<int> equipmentIDList = new List<int>() { 1, 2, 3 };

            #endregion

            #region [ Mock ]

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(e => e.CreateObjectSet<CS_CallType>()).Returns(fakeCallTypeRepository);
            mockUnitOfWork.Setup(e => e.CreateObjectSet<CS_Resource>()).Returns(fakeResourceRepository);
            mockUnitOfWork.Setup(e => e.CreateObjectSet<CS_DPI>()).Returns(fakeDPIRepository);
            CallLogModel model = new CallLogModel(mockUnitOfWork.Object);

            #endregion

            //Act
            model.VerifyDPICalculate(callLog, employeeIDList, equipmentIDList);

            //Assert
            CS_DPI dpi = fakeDPIRepository.FirstOrDefault(e => e.ID == 1);
            CS_DPI dpi2 = fakeDPIRepository.FirstOrDefault(e => e.ID == 2);
            Assert.IsTrue(dpi.Calculate, "Fail at Calculate True");
            Assert.IsFalse(dpi2.Calculate, "Fail at Calculate False");
        }

        [TestMethod]
        public void VerifyDPICalculateAutomatedProcess()
        {
            //Arrange
            #region [ Variables ]

            DateTime now = DateTime.Now;
            DateTime startDateTime = new DateTime(2011, 08, 01, 12, 0, 0);
            DateTime startDateTime2 = startDateTime.AddDays(1);

            #endregion

            #region [ CallType Repository ]

            FakeObjectSet<CS_CallType> fakeCallTypeRepository = new FakeObjectSet<CS_CallType>();
            fakeCallTypeRepository.AddObject
                (
                    new CS_CallType()
                    {
                        ID = (int)Globals.CallEntry.CallType.TransferResource,
                        Description = "Transfer Resource",
                        Xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>  <DynamicFieldsAggregator xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">    <Controls>      <DynamicControls xsi:type=\"DynamicDatePickerXml\">        <Name>dtpDate</Name>        <IsValidEmpty>false</IsValidEmpty>        <EmptyValueMessage>The Parked Date field is required</EmptyValueMessage>        <InvalidValueMessage>Invalid Parked Date format</InvalidValueMessage>        <DateTimeFormat>Default</DateTimeFormat>        <ShowOn>Both</ShowOn>        <ValidationGroup>CallEntry</ValidationGroup>        <Label>          <Text>Parked Date:</Text>          <Css>dynamicLabel</Css>        </Label>      </DynamicControls>      <DynamicControls xsi:type=\"DynamicTimeXml\">        <Name>txtTime</Name>        <IsValidEmpty>false</IsValidEmpty>        <ValidationGroup>CallEntry</ValidationGroup>        <EmptyValueMessage>The Parked Time field is required.</EmptyValueMessage>        <InvalidValueMessage>The Parked Time field is invalid</InvalidValueMessage>        <MaskedType>Time</MaskedType>        <Mask>99:99</Mask>        <Label>          <Text>Parked Time:</Text>          <Css>dynamicLabel</Css>        </Label>      </DynamicControls>      <DynamicControls xsi:type=\"DynamicCountableTextBoxXml\">        <Name>txtNote</Name>        <IsRequired>false</IsRequired>        <MaxChars>255</MaxChars>        <MaxCharsWarning>250</MaxCharsWarning>        <TextMode>MultiLine</TextMode>        <Width>300</Width>        <Height>150</Height>        <Label>          <Text>Note:</Text>          <Css>dynamicLabel</Css>        </Label>      </DynamicControls>    </Controls>  </DynamicFieldsAggregator>",
                        CallCriteria = false,
                        IsAutomaticProcess = true,
                        DpiStatus = 1,
                        Active = true
                    }
                );

            #endregion

            #region [ Resource Repository ]

            FakeObjectSet<CS_Resource> fakeResourceRepository = new FakeObjectSet<CS_Resource>();
            fakeResourceRepository.AddObject(new CS_Resource() { ID = 1, EmployeeID = 1, EquipmentID = null, JobID = 1, Duration = 1, StartDateTime = startDateTime, Active = true });
            fakeResourceRepository.AddObject(new CS_Resource() { ID = 2, EmployeeID = null, EquipmentID = 1, JobID = 1, Duration = 1, StartDateTime = startDateTime, Active = true });
            fakeResourceRepository.AddObject(new CS_Resource() { ID = 3, EmployeeID = null, EquipmentID = 2, JobID = 1, Duration = 1, StartDateTime = startDateTime, Active = true });
            fakeResourceRepository.AddObject(new CS_Resource() { ID = 4, EmployeeID = null, EquipmentID = 3, JobID = 1, Duration = 1, StartDateTime = startDateTime2, Active = true });

            #endregion

            #region [ DPI Repository ]

            FakeObjectSet<CS_DPI> fakeDPIRepository = new FakeObjectSet<CS_DPI>();
            fakeDPIRepository.AddObject(new CS_DPI()
            {
                ID = 1,
                Date = startDateTime.Date,
                Calculate = false,
                ProcessStatus = (int)Globals.DPI.DpiStatus.DraftSaved,
                JobID = 1,
                IsContinuing = true,
                ProcessStatusDate = DateTime.Now,
                CalculationStatus = (short)Globals.DPI.CalculationStatus.INSF,
                Total = 1000,
                CreatedBy = "System",
                //CreationID =,
                CreationDate = DateTime.Now,
                ModifiedBy = "System",
                //ModificationID,
                ModificationDate = DateTime.Now,
                Active = true,
                CS_DPIResource = new EntityCollection<CS_DPIResource>()
                {
                    new CS_DPIResource()
                    {
                        ID = 1,
                        DPIID = 1,
                        EmployeeID = 1,
                        Active = true
                    },
                    new CS_DPIResource()
                    {
                        ID = 1,
                        DPIID = 1,
                        EquipmentID = 1,
                        Active = true
                    },
                    new CS_DPIResource()
                    {
                        ID = 1,
                        DPIID = 1,
                        EquipmentID = 2,
                        Active = true,
                    }
                }
            });

            fakeDPIRepository.AddObject(new CS_DPI()
            {
                ID = 2,
                Date = startDateTime2.Date,
                Calculate = false,
                ProcessStatus = (int)Globals.DPI.DpiStatus.DraftSaved,
                JobID = 1,
                IsContinuing = true,
                ProcessStatusDate = DateTime.Now,
                CalculationStatus = (short)Globals.DPI.CalculationStatus.INSF,
                Total = 1000,
                CreatedBy = "System",
                //CreationID =,
                CreationDate = DateTime.Now,
                ModifiedBy = "System",
                //ModificationID,
                ModificationDate = DateTime.Now,
                Active = true,
                CS_DPIResource = new EntityCollection<CS_DPIResource>()
                {
                    new CS_DPIResource()
                    {
                        ID = 1,
                        DPIID = 1,
                        EmployeeID = 1,
                        Active = true
                    },
                    new CS_DPIResource()
                    {
                        ID = 1,
                        DPIID = 1,
                        EquipmentID = 1,
                        Active = true
                    },
                    new CS_DPIResource()
                    {
                        ID = 1,
                        DPIID = 1,
                        EquipmentID = 2,
                        Active = true
                    },
                    new CS_DPIResource()
                    {
                        ID = 1,
                        DPIID = 1,
                        EquipmentID = 3,
                        Active = true
                    }
                }
            });

            #endregion

            #region [ Parameters ]

            CS_CallLog callLog = new CS_CallLog()
            {
                CallTypeID = (int)Globals.CallEntry.CallType.TransferResource,
                CreationDate = startDateTime,
                CallDate = startDateTime,
                JobID = 1
            };

            List<int> employeeIDList = new List<int>() { 1 };
            List<int> equipmentIDList = new List<int>() { 1, 2, 3 };

            #endregion

            #region [ Mock ]

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(e => e.CreateObjectSet<CS_CallType>()).Returns(fakeCallTypeRepository);
            mockUnitOfWork.Setup(e => e.CreateObjectSet<CS_Resource>()).Returns(fakeResourceRepository);
            mockUnitOfWork.Setup(e => e.CreateObjectSet<CS_DPI>()).Returns(fakeDPIRepository);
            CallLogModel model = new CallLogModel(mockUnitOfWork.Object);

            #endregion

            //Act
            model.VerifyDPICalculate(callLog, employeeIDList, equipmentIDList);

            //Assert
            CS_DPI dpi = fakeDPIRepository.FirstOrDefault(e => e.ID == 1);
            CS_DPI dpi2 = fakeDPIRepository.FirstOrDefault(e => e.ID == 2);
            Assert.IsTrue(dpi.Calculate, "Fail at Calculate True");
            Assert.IsFalse(dpi2.Calculate, "Fail at Calculate False");
        }

        [TestMethod]
        public void TestFilterByPrimaryCallType()
        {
            //Arrange
            CallLogModel model = new CallLogModel(new FakeUnitOfWork());
            //Act
            IList<CS_CallType> resultList1 = model.FilterByPrimaryCallType(1);
            //Assert
            Assert.AreEqual(2, resultList1.Count);
        }

        [TestMethod]
        public void TestGetCallTypeByPrimaryCallTypeAutomatic()
        {
            //Arrange
            CallLogModel model = new CallLogModel(new FakeUnitOfWork());
            //Act
            IList<CS_CallType> resultList1 = model.GetCallTypeByPrimaryCallTypeAutomatic(2);
            //Assert
            Assert.AreEqual(2, resultList1.Count);
        }

        [TestMethod]
        public void ShouldReturnCallLogObjectByJobIdAndCallLogId()
        {
            //Arrange
            FakeObjectSet<CS_CallLog> fakeCallLogList = new FakeObjectSet<CS_CallLog>();
            fakeCallLogList.AddObject(new CS_CallLog() { ID = 1, JobID = 1, Active = true });
            fakeCallLogList.AddObject(new CS_CallLog() { ID = 2, JobID = 2, Active = true });
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(e => e.CreateObjectSet<CS_CallLog>()).Returns(fakeCallLogList);
            CallLogModel model = new CallLogModel(mockUnitOfWork.Object);

            //Act
            CS_CallLog callLog = model.LoadCallLog(1, 1);

            //Assert
            Assert.AreEqual(1, callLog.ID);
            Assert.AreEqual(1, callLog.JobID);
        }

        [TestMethod]
        public void ShouldReturnNullObjectWhenLoadingByJobIdAndCallLogId()
        {
            //Arrange
            FakeObjectSet<CS_CallLog> fakeCallLogList = new FakeObjectSet<CS_CallLog>();
            fakeCallLogList.AddObject(new CS_CallLog() { ID = 1, JobID = 1, Active = true });
            fakeCallLogList.AddObject(new CS_CallLog() { ID = 2, JobID = 2, Active = true });
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(e => e.CreateObjectSet<CS_CallLog>()).Returns(fakeCallLogList);
            CallLogModel model = new CallLogModel(mockUnitOfWork.Object);

            //Act
            CS_CallLog callLog = model.LoadCallLog(1, 2);

            //Assert
            Assert.IsNull(callLog);
        }

        [TestMethod]
        public void ShouldReturnCallLogsRelatedToJob()
        {
            //Arrange
            FakeObjectSet<CS_CallLog> fakeCallLogList = new FakeObjectSet<CS_CallLog>();
            fakeCallLogList.AddObject(new CS_CallLog() { ID = 1, JobID = 1, Active = true });
            fakeCallLogList.AddObject(new CS_CallLog() { ID = 2, JobID = 1, Active = true });
            fakeCallLogList.AddObject(new CS_CallLog() { ID = 3, JobID = 2, Active = true });
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(e => e.CreateObjectSet<CS_CallLog>()).Returns(fakeCallLogList);
            CallLogModel model = new CallLogModel(mockUnitOfWork.Object);

            //Act
            IList<CS_CallLog> callLogList = model.ListAllJobCallLogs(1);

            //Assert
            Assert.AreEqual(2, callLogList.Count);
        }

        [TestMethod]
        public void ShouldReturnEmptyListtIfCallLogRelatedToJobIsNotFound()
        {
            //Arrange
            FakeObjectSet<CS_CallLog> fakeCallLogList = new FakeObjectSet<CS_CallLog>();
            fakeCallLogList.AddObject(new CS_CallLog() { ID = 1, JobID = 1, Active = true });
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(e => e.CreateObjectSet<CS_CallLog>()).Returns(fakeCallLogList);
            CallLogModel model = new CallLogModel(mockUnitOfWork.Object);

            //Act
            IList<CS_CallLog> callLogList = model.ListAllJobCallLogs(2);

            //Assert
            Assert.IsNotNull(callLogList);
            Assert.AreEqual(0, callLogList.Count);
        }

        [TestMethod]
        public void TestGetPrimaryCallType()
        {
            //Arrange
            CallLogModel model = new CallLogModel(new FakeUnitOfWork());
            //Act
            CS_PrimaryCallType result = model.GetPrimaryCallType(1);
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetCallType()
        {
            //Arrange
            CallLogModel model = new CallLogModel(new FakeUnitOfWork());
            //Act
            CS_CallType result = model.GetCallType(1);
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ParseNoteField()
        {
            //Arrange
            string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><DynamicFieldsAggregator xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><Controls><DynamicControls xsi:type=\"DynamicCountableTextBoxXml\"><Name>txtNote</Name><Label><Text>Note:</Text><Css>dynamicLabel</Css><Style /></Label><Css>input</Css><Style /><Visible>true</Visible><MaxChars>255</MaxChars><MaxCharsWarning>250</MaxCharsWarning><Text>testsdavdgvasdvaghcdhacsdhgacsdgd</Text><IsRequired>false</IsRequired><ErrorMessage /><ValidationGroup /><TextMode>MultiLine</TextMode><Width>300</Width><Height>150</Height></DynamicControls></Controls><Extenders /></DynamicFieldsAggregator>";
            CallLogModel model = new CallLogModel(new FakeUnitOfWork());

            //Act
            string note = model.GetDynamicFieldValueByName<DynamicCountableTextBoxXml>(xml, "txtNote", "Text");

            //Assert
            Assert.AreEqual("testsdavdgvasdvaghcdhacsdhgacsdgd", note);
        }

        [TestMethod]
        public void TestListInitialAdiviseCallLogsByJob()
        {
            //Arrange
            CallLogModel model = new CallLogModel(new FakeUnitOfWork());

            //Act
            IList<CS_CallLog> result = model.ListInitialAdiviseCallLogsByJob(1);

            //Assert
            Assert.AreEqual(1, result.Count);
        }

        //[TestMethod]
        //public void ShouldParseFromPersonSourceToCallLogResource()
        //{
        //    CS_Employee emp = new CS_Employee();
        //    emp.ID = 7;
        //    emp.Name = "Ruziska";
        //    emp.FirstName = "Danilo";
        //    emp.DayAreaCode = "area";
        //    emp.CS_Division = new CS_Division() { ID = 1, Name = "Division123" };

        //    CS_Contact contact = new CS_Contact();
        //    contact.ID = 2;
        //    contact.Name = "Contact Name";

        //    IList<IInitialAdvisePerson> list = new List<IInitialAdvisePerson>();

        //    list.Add(emp);
        //    list.Add(contact);

        //    CallLogModel model = new CallLogModel();

        //    IList<CS_CallLogResource> resources = model.ParsePersonToCallLogResource(list);

        //    Assert.AreEqual(list[0].ID, resources[0].EmployeeID);
        //    Assert.IsNull(resources[0].ContactID);

        //    Assert.AreEqual(list[1].ID, resources[1].ContactID);
        //    Assert.IsNull(resources[1].EmployeeID);
        //}

        [TestMethod]
        public void TestListFilteredJobCallLogInfoByStatus()
        {
            // Arrange
            CallLogModel model = new CallLogModel(new FakeUnitOfWork());
            // Act
            IList<CS_View_JobCallLog> result = model.ListFilteredJobCallLogInfo(1, null, "", null, DateTime.MinValue, DateTime.Now, false, false, "");
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void TestListFilteredJobCallLogInfoByCallType()
        {
            // Arrange
            CallLogModel model = new CallLogModel(new FakeUnitOfWork());
            // Act
            IList<CS_View_JobCallLog> result = model.ListFilteredJobCallLogInfo(null, 1, "", null, DateTime.MinValue, DateTime.Now, false, false, "");
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }
        [TestMethod]
        public void TestListFilteredJobCallLogInfoByModifiedBy()
        {
            // Arrange
            CallLogModel model = new CallLogModel(new FakeUnitOfWork());
            // Act
            IList<CS_View_JobCallLog> result = model.ListFilteredJobCallLogInfo(null, null, "User 2", null, DateTime.MinValue, DateTime.Now, false, false, "");
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void TestListFilteredJobCallLogInfoByDivision()
        {
            // Arrange
            CallLogModel model = new CallLogModel(new FakeUnitOfWork());
            // Act
            IList<CS_View_JobCallLog> result = model.ListFilteredJobCallLogInfo(null, null, "", 2, DateTime.MinValue, DateTime.Now, false, false, "");
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void TestListFilteredJobCallLogInfoByModificationDate()
        {
            // Arrange
            CallLogModel model = new CallLogModel(new FakeUnitOfWork());
            // Act
            IList<CS_View_JobCallLog> result = model.ListFilteredJobCallLogInfo(null, null, "", null, new DateTime(2011, 2, 6), DateTime.Now, false, false, "");
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void ShouldReturnCallLogListFilteredByCallDate()
        {
            //Arrange
            FakeObjectSet<CS_CallLog> fakeCallLogList = new FakeObjectSet<CS_CallLog>();
            fakeCallLogList.AddObject(new CS_CallLog() { ID = 1, JobID = 1, Active = true, CallDate = new DateTime(2011, 5, 1, 0, 0, 0) });
            fakeCallLogList.AddObject(new CS_CallLog() { ID = 2, JobID = 1, Active = true, CallDate = new DateTime(2011, 5, 10, 0, 0, 0) });
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(e => e.CreateObjectSet<CS_CallLog>()).Returns(fakeCallLogList);
            CallLogModel model = new CallLogModel(mockUnitOfWork.Object);

            //Act
            IList<CS_CallLog> callLogList = model.ListFilteredCallLogs(Core.Globals.JobRecord.FilterType.Date, "5/10/2011", 1);

            //Assert
            Assert.AreEqual(1, callLogList.Count);
            Assert.AreEqual(2, callLogList[0].ID);
        }

        [TestMethod]
        public void ShouldReturnEmptyListIfFilteringCallLogsByDateFilterAndDontProvideDateValue()
        {
            //Arrange
            FakeObjectSet<CS_CallLog> fakeCallLogList = new FakeObjectSet<CS_CallLog>();
            fakeCallLogList.AddObject(new CS_CallLog() { ID = 1, JobID = 1, Active = true });
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(e => e.CreateObjectSet<CS_CallLog>()).Returns(fakeCallLogList);
            CallLogModel model = new CallLogModel(mockUnitOfWork.Object);

            //Act
            IList<CS_CallLog> callLogList = model.ListFilteredCallLogs(Core.Globals.JobRecord.FilterType.Date, "5/10/2011", 1);

            //Assert
            Assert.AreEqual(0, callLogList.Count);
        }

        [TestMethod]
        public void ShouldReturnNullListIfFilteringCallLogsByDateFilterAndProvideIncorrectDateFormat()
        {
            //Arrange
            FakeObjectSet<CS_CallLog> fakeCallLogList = new FakeObjectSet<CS_CallLog>();
            fakeCallLogList.AddObject(new CS_CallLog() { ID = 1, JobID = 1, Active = true, CallDate = new DateTime(2011, 5, 10) });
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(e => e.CreateObjectSet<CS_CallLog>()).Returns(fakeCallLogList);
            CallLogModel model = new CallLogModel(mockUnitOfWork.Object);

            //Act
            IList<CS_CallLog> callLogList = model.ListFilteredCallLogs(Core.Globals.JobRecord.FilterType.Date, "5/102011", 1);

            //Assert
            Assert.IsNull(callLogList);
        }

        [TestMethod]
        public void ShouldReturnNullListIfFilteringCallLogsByDateFilterAndProvideInvalidDate()
        {
            //Arrange
            FakeObjectSet<CS_CallLog> fakeCallLogList = new FakeObjectSet<CS_CallLog>();
            fakeCallLogList.AddObject(new CS_CallLog() { ID = 1, JobID = 1, Active = true, CallDate = DateTime.Now });
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(e => e.CreateObjectSet<CS_CallLog>()).Returns(fakeCallLogList);
            CallLogModel model = new CallLogModel(mockUnitOfWork.Object);

            //Act
            IList<CS_CallLog> callLogList = model.ListFilteredCallLogs(Core.Globals.JobRecord.FilterType.Date, "2/30/2011", 1);

            //Assert
            Assert.IsNull(callLogList);
        }

        [TestMethod]
        public void ShouldReturnCallLogListFilteredByCallTime()
        {
            //Arrange
            FakeObjectSet<CS_CallLog> fakeCallLogList = new FakeObjectSet<CS_CallLog>();
            fakeCallLogList.AddObject(new CS_CallLog() { ID = 1, JobID = 1, Active = true, CallDate = new DateTime(2011, 05, 1) });
            fakeCallLogList.AddObject(new CS_CallLog() { ID = 2, JobID = 1, Active = true, CallDate = new DateTime(2011, 05, 10, 13, 45, 0) });
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(e => e.CreateObjectSet<CS_CallLog>()).Returns(fakeCallLogList);
            CallLogModel model = new CallLogModel(mockUnitOfWork.Object);

            //Act
            IList<CS_CallLog> callLogList = model.ListFilteredCallLogs(Core.Globals.JobRecord.FilterType.Time, "5/11/2011 13:45", 1);
            //Assert
            Assert.AreEqual(1, callLogList.Count);
            Assert.AreEqual(2, callLogList[0].ID);
        }

        [TestMethod]
        public void ShouldReturnEmptyListIfFilteringCallLogsByTimeFilterAndDontProvideTimeValue()
        {
            //Arrange
            FakeObjectSet<CS_CallLog> fakeCallLogList = new FakeObjectSet<CS_CallLog>();
            fakeCallLogList.AddObject(new CS_CallLog() { ID = 1, JobID = 1, Active = true, CallDate = new DateTime(2011, 05, 1) });
            fakeCallLogList.AddObject(new CS_CallLog() { ID = 2, JobID = 1, Active = true, CallDate = new DateTime(2011, 05, 10) });
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(e => e.CreateObjectSet<CS_CallLog>()).Returns(fakeCallLogList);
            CallLogModel model = new CallLogModel(mockUnitOfWork.Object);

            //Act
            IList<CS_CallLog> callLogList = model.ListFilteredCallLogs(Core.Globals.JobRecord.FilterType.Time, "5/10/2011 13:45", 1);
            //Assert
            Assert.AreEqual(0, callLogList.Count);
        }

        [TestMethod]
        public void ShouldReturnNullListIfFilteringCallLogsByTimeFilterAndProvideIncorrectTimeFormat()
        {
            //Arrange
            FakeObjectSet<CS_CallLog> fakeCallLogList = new FakeObjectSet<CS_CallLog>();
            fakeCallLogList.AddObject(new CS_CallLog() { ID = 1, JobID = 1, Active = true, CallDate = new DateTime(2011, 05, 1) });
            fakeCallLogList.AddObject(new CS_CallLog() { ID = 2, JobID = 1, Active = true, CallDate = new DateTime(2011, 05, 10, 13, 45, 0) });
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(e => e.CreateObjectSet<CS_CallLog>()).Returns(fakeCallLogList);
            CallLogModel model = new CallLogModel(mockUnitOfWork.Object);

            //Act
            IList<CS_CallLog> callLogList = model.ListFilteredCallLogs(Core.Globals.JobRecord.FilterType.Time, "5/10/2011 1345", 1);
            //Assert
            Assert.IsNull(callLogList);
        }

        [TestMethod]
        public void ShouldReturnNullListIfFilteringCallLogsByTimeFilterAndProvideInvalidTime()
        {
            //Arrange
            FakeObjectSet<CS_CallLog> fakeCallLogList = new FakeObjectSet<CS_CallLog>();
            fakeCallLogList.AddObject(new CS_CallLog() { ID = 1, JobID = 1, Active = true, CallDate = new DateTime(2011, 05, 1) });
            fakeCallLogList.AddObject(new CS_CallLog() { ID = 2, JobID = 1, Active = true, CallDate = new DateTime(2011, 05, 10, 13, 45, 0) });
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(e => e.CreateObjectSet<CS_CallLog>()).Returns(fakeCallLogList);
            CallLogModel model = new CallLogModel(mockUnitOfWork.Object);

            //Act
            IList<CS_CallLog> callLogList = model.ListFilteredCallLogs(Core.Globals.JobRecord.FilterType.Time, "5/10/2011 24:61", 1);
            //Assert
            Assert.IsNull(callLogList);
        }

        [TestMethod]
        public void ShouldReturnCallLogListFilteredByCallTypeDescription()
        {
            //Arrange
            FakeObjectSet<CS_CallLog> fakeCallLogList = new FakeObjectSet<CS_CallLog>();
            fakeCallLogList.AddObject(new CS_CallLog()
            {
                ID = 1,
                JobID = 1,
                Active = true,
                CS_CallType = new CS_CallType() { Description = "CallTypeDescriptionTest" }
            });
            fakeCallLogList.AddObject(new CS_CallLog()
            {
                ID = 2,
                JobID = 2,
                Active = true,
                CS_CallType = new CS_CallType() { Description = "CallTypeDescriptionTest2" }
            });
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(e => e.CreateObjectSet<CS_CallLog>()).Returns(fakeCallLogList);
            CallLogModel model = new CallLogModel(mockUnitOfWork.Object);

            //Act
            IList<CS_CallLog> callLogList = model.ListFilteredCallLogs(Core.Globals.JobRecord.FilterType.Type, "CallTypeDescriptionTest2", 2);
            //Assert
            Assert.AreEqual(1, callLogList.Count);
            Assert.AreEqual(2, callLogList[0].ID);
        }

        [TestMethod]
        public void ShouldReturnEmptyListIfFielteringCallLogsByCallTypeDescriptionAndDontProvideDescription()
        {
            //Arrange
            FakeObjectSet<CS_CallLog> fakeCallLogList = new FakeObjectSet<CS_CallLog>();
            fakeCallLogList.AddObject(new CS_CallLog()
            {
                ID = 1,
                JobID = 1,
                Active = true,
                CS_CallType = new CS_CallType() { Description = "CallTypeDescriptionTest" }
            });
            fakeCallLogList.AddObject(new CS_CallLog()
            {
                ID = 2,
                JobID = 2,
                Active = true,
                CS_CallType = new CS_CallType() { Description = "CallTypeDescriptionTest2" }
            });
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(e => e.CreateObjectSet<CS_CallLog>()).Returns(fakeCallLogList);
            CallLogModel model = new CallLogModel(mockUnitOfWork.Object);

            //Act
            IList<CS_CallLog> callLogList = model.ListFilteredCallLogs(Core.Globals.JobRecord.FilterType.Type, "", 2);
            //Assert
            Assert.AreEqual(1, callLogList.Count);
            Assert.AreEqual(2, callLogList[0].ID);
        }

        [TestMethod]
        public void ShouldReturnCallLogListFilteredByUser()
        {
            //Arrange
            FakeObjectSet<CS_CallLog> fakeCallLogList = new FakeObjectSet<CS_CallLog>();
            fakeCallLogList.AddObject(new CS_CallLog()
            {
                ID = 1,
                JobID = 1,
                Active = true,
                CreatedBy = "druziska"
            });
            fakeCallLogList.AddObject(new CS_CallLog()
            {
                ID = 2,
                JobID = 2,
                Active = true,
                CreatedBy = "cburton"
            });

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(e => e.CreateObjectSet<CS_CallLog>()).Returns(fakeCallLogList);
            CallLogModel model = new CallLogModel(mockUnitOfWork.Object);

            //Act
            IList<CS_CallLog> callLogList = model.ListFilteredCallLogs(Core.Globals.JobRecord.FilterType.User, "druziska", 1);
            //Assert
            Assert.AreEqual(1, callLogList.Count);
            Assert.AreEqual(1, callLogList[0].ID);
        }

        [TestMethod]
        public void TestIfListAllPrimaryCallTypesIsReturningJobRelatedRows()
        {
            // Arrange
            FakeObjectSet<CS_PrimaryCallType> fakePrimaryCallTypeList = new FakeObjectSet<CS_PrimaryCallType>();
            fakePrimaryCallTypeList.AddObject(
                new CS_PrimaryCallType()
                {
                    ID = 1,
                    Type = "Job Related Primary Call Type",
                    Active = true,
                    JobRelated = true
                }
            );
            fakePrimaryCallTypeList.AddObject(
                new CS_PrimaryCallType()
                {
                    ID = 2,
                    Type = "Non-Job Related Primary Call Type",
                    Active = true,
                    JobRelated = false
                }
            );
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(e => e.CreateObjectSet<CS_PrimaryCallType>()).Returns(fakePrimaryCallTypeList);
            CallLogModel model = new CallLogModel(mockUnitOfWork.Object);

            // Act
            IList<CS_PrimaryCallType> primaryCallTypeList = model.ListAllPrimaryCallTypes(true);

            // Assert
            Assert.AreEqual(1, primaryCallTypeList.Count);
            Assert.AreEqual(1, primaryCallTypeList[0].ID);
        }
    }
}
