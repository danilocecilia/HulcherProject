USE [OneSource]
GO

/****** Object:  Table [dbo].[CS_FirstAlertPerson]    Script Date: 07/11/2011 18:04:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CS_FirstAlertPerson](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstAlertID] [int] NOT NULL,
	[IsHulcherEmployee] [bit] NOT NULL,
	[FirstAlertVehicleID] [int] NULL,
	[VehiclePosition] [smallint] NULL,
	[EmployeeID] [int] NULL,
	[LastName] [varchar](100) NULL,
	[FirstName] [varchar](100) NULL,
	[CountryID] [int] NULL,
	[StateID] [int] NULL,
	[CityID] [int] NULL,
	[ZipcodeID] [int] NULL,
	[Address] [varchar](100) NULL,
	[InjuryNature] [varchar](100) NULL,
	[InjuryBodyPart] [varchar](100) NULL,
	[MedicalSeverity] [smallint] NULL,
	[Details] [varchar](1000) NULL,
	[DoctorsName] [varchar](100) NULL,
	[DoctorsCountryID] [int] NULL,
	[DoctorsStateID] [int] NULL,
	[DoctorsCityID] [int] NULL,
	[DoctorsZipcodeID] [int] NULL,
	[DoctorsPhoneNumber] [int] NULL,
	[HospitalName] [varchar](100) NULL,
	[HospitalCountryID] [int] NULL,
	[HospitalStateID] [int] NULL,
	[HospitalCityID] [int] NULL,
	[HospitalZipcodeID] [int] NULL,
	[HospitalPhoneNumber] [int] NULL,
	[DriversLicenseNumber] [varchar](15) NULL,
	[DriversLicenseCountryID] [int] NULL,
	[DriversLicenseStateID] [int] NULL,
	[DriversLicenseCityID] [int] NULL,
	[DriversLicenseZipcodeID] [int] NULL,
	[DriversLicenseAddress] [varchar](100) NULL,
	[InsuranceCompany] [varchar](50) NULL,
	[PolicyNumber] [varchar](50) NULL,
	[DrugScreenRequired] [bit] NULL,
	[CreationID] [int] NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModificationID] [int] NULL,
	[ModifiedBy] [varchar](50) NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_CS_FirstAlertPerson] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING ON
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlertPerson_CS_City] FOREIGN KEY([CityID])
REFERENCES [dbo].[CS_City] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson] CHECK CONSTRAINT [FK_CS_FirstAlertPerson_CS_City]
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlertPerson_CS_City_Doctor] FOREIGN KEY([DoctorsCityID])
REFERENCES [dbo].[CS_City] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson] CHECK CONSTRAINT [FK_CS_FirstAlertPerson_CS_City_Doctor]
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlertPerson_CS_City_DriversLicense] FOREIGN KEY([DriversLicenseCityID])
REFERENCES [dbo].[CS_City] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson] CHECK CONSTRAINT [FK_CS_FirstAlertPerson_CS_City_DriversLicense]
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlertPerson_CS_City_Hospital] FOREIGN KEY([HospitalCityID])
REFERENCES [dbo].[CS_City] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson] CHECK CONSTRAINT [FK_CS_FirstAlertPerson_CS_City_Hospital]
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlertPerson_CS_Country] FOREIGN KEY([CountryID])
REFERENCES [dbo].[CS_Country] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson] CHECK CONSTRAINT [FK_CS_FirstAlertPerson_CS_Country]
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlertPerson_CS_Country_Doctor] FOREIGN KEY([DoctorsCountryID])
REFERENCES [dbo].[CS_Country] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson] CHECK CONSTRAINT [FK_CS_FirstAlertPerson_CS_Country_Doctor]
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlertPerson_CS_Country_DriversLicense] FOREIGN KEY([DriversLicenseCountryID])
REFERENCES [dbo].[CS_Country] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson] CHECK CONSTRAINT [FK_CS_FirstAlertPerson_CS_Country_DriversLicense]
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlertPerson_CS_Country_Hospital] FOREIGN KEY([HospitalCountryID])
REFERENCES [dbo].[CS_Country] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson] CHECK CONSTRAINT [FK_CS_FirstAlertPerson_CS_Country_Hospital]
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlertPerson_CS_Employee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson] CHECK CONSTRAINT [FK_CS_FirstAlertPerson_CS_Employee]
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlertPerson_CS_Employee_Creation] FOREIGN KEY([CreationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson] CHECK CONSTRAINT [FK_CS_FirstAlertPerson_CS_Employee_Creation]
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlertPerson_CS_Employee_Modification] FOREIGN KEY([ModificationID])
REFERENCES [dbo].[CS_Employee] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson] CHECK CONSTRAINT [FK_CS_FirstAlertPerson_CS_Employee_Modification]
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlertPerson_CS_FirstAlert] FOREIGN KEY([FirstAlertID])
REFERENCES [dbo].[CS_FirstAlert] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson] CHECK CONSTRAINT [FK_CS_FirstAlertPerson_CS_FirstAlert]
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlertPerson_CS_FirstAlertVehicle] FOREIGN KEY([FirstAlertVehicleID])
REFERENCES [dbo].[CS_FirstAlertVehicle] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson] CHECK CONSTRAINT [FK_CS_FirstAlertPerson_CS_FirstAlertVehicle]
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlertPerson_CS_State] FOREIGN KEY([StateID])
REFERENCES [dbo].[CS_State] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson] CHECK CONSTRAINT [FK_CS_FirstAlertPerson_CS_State]
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlertPerson_CS_State_Doctor] FOREIGN KEY([DoctorsStateID])
REFERENCES [dbo].[CS_State] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson] CHECK CONSTRAINT [FK_CS_FirstAlertPerson_CS_State_Doctor]
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlertPerson_CS_State_DriversLicense] FOREIGN KEY([DriversLicenseStateID])
REFERENCES [dbo].[CS_State] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson] CHECK CONSTRAINT [FK_CS_FirstAlertPerson_CS_State_DriversLicense]
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlertPerson_CS_State_Hospital] FOREIGN KEY([HospitalStateID])
REFERENCES [dbo].[CS_State] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson] CHECK CONSTRAINT [FK_CS_FirstAlertPerson_CS_State_Hospital]
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlertPerson_CS_ZipCode] FOREIGN KEY([ZipcodeID])
REFERENCES [dbo].[CS_ZipCode] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson] CHECK CONSTRAINT [FK_CS_FirstAlertPerson_CS_ZipCode]
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlertPerson_CS_ZipCode_Doctor] FOREIGN KEY([DoctorsZipcodeID])
REFERENCES [dbo].[CS_ZipCode] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson] CHECK CONSTRAINT [FK_CS_FirstAlertPerson_CS_ZipCode_Doctor]
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlertPerson_CS_ZipCode_DriversLicense] FOREIGN KEY([DriversLicenseZipcodeID])
REFERENCES [dbo].[CS_ZipCode] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson] CHECK CONSTRAINT [FK_CS_FirstAlertPerson_CS_ZipCode_DriversLicense]
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson]  WITH CHECK ADD  CONSTRAINT [FK_CS_FirstAlertPerson_CS_ZipCode_Hospital] FOREIGN KEY([HospitalZipcodeID])
REFERENCES [dbo].[CS_ZipCode] ([ID])
GO

ALTER TABLE [dbo].[CS_FirstAlertPerson] CHECK CONSTRAINT [FK_CS_FirstAlertPerson_CS_ZipCode_Hospital]
GO


