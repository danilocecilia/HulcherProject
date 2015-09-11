USE [ff6110]
GO

/****** Object:  View [dbo].[OneSource_Employees]    Script Date: 04/01/2011 11:53:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[OneSource_Employees]
AS

SELECT
	 a.PersonGUID
	,REPLICATE('0', 5 - LEN(a.PersonID)) + a.PersonID AS PersonID
	,a.LastName
	,a.FirstName
	,a.NickName
	,a.DayAreaCode
	,a.DayPhone
	,a.FaxAreaCode
	,a.FaxPhone
	,a.HomeAreaCode
	,a.HomePhone
	,a.MobileAreaCode
	,a.MobilePhone
	,a.OtherPhoneAreaCode
	,a.OtherPhone
	,a.Address
	,a.Address2
	,a.City
	,a.StateProvinceCode
	,a.CountryCode
	,a.PostalCode
	,a.CanadaAvailableFlag
	,b.StatusCode
	,c.DivisionCode
	,c.LocationCode
	,d.JobCode
	,d.BusinessCardTitle
	,d.PersonJobStartDate
	,e.BirthDate
	,e.DriversLicenseNumber
	,e.DriversLicenseClass
	,e.DriversLicenseStateProvinceCode
	,e.DriversLicenseExpireDate
	,e.PassportNumber
	,e.PassportCountryCode
	,e.PassportIssueDate
	,e.PassportExpireDate
	,f.UserId
	,CHECKSUM(
		 a.PersonGUID
		,REPLICATE('0', 5 - LEN(a.PersonID)) + a.PersonID
		,a.LastName
		,a.FirstName
		,a.NickName
		,a.DayAreaCode
		,a.DayPhone
		,a.FaxAreaCode
		,a.FaxPhone
		,a.HomeAreaCode
		,a.HomePhone
		,a.MobileAreaCode
		,a.MobilePhone
		,a.OtherPhoneAreaCode
		,a.OtherPhone
		,a.Address
		,a.Address2
		,a.City
		,a.StateProvinceCode
		,a.CountryCode
		,a.PostalCode
		,a.CanadaAvailableFlag
		,b.StatusCode
		,c.DivisionCode
		,c.LocationCode
		,d.JobCode
		,d.BusinessCardTitle
		,d.PersonJobStartDate
		,e.BirthDate
		,e.DriversLicenseNumber
		,e.DriversLicenseClass
		,e.DriversLicenseStateProvinceCode
		,e.DriversLicenseExpireDate
		,e.PassportNumber
		,e.PassportCountryCode
		,e.PassportIssueDate
		,e.PassportExpireDate
		,f.UserId
	) AS Checksum
FROM
	dbo.tPerson AS a WITH (NOLOCK) 
		LEFT OUTER JOIN dbo.tPersonStatusHist AS b WITH (NOLOCK) ON a.PersonGUID = b.PersonGUID AND b.PersonStatusCurrentFlag = 1 
		LEFT OUTER JOIN dbo.tPersonLocationHist AS c WITH (NOLOCK) ON a.PersonGUID = c.PersonGUID AND c.PersonLocationCurrentFlag = 1 
		LEFT OUTER JOIN dbo.tPersonJobHist AS d WITH (NOLOCK) ON a.PersonGUID = d.PersonGUID AND d.PersonJobCurrentFlag = 1 
		LEFT OUTER JOIN dbo.tPersonal AS e WITH (NOLOCK) ON a.PersonGUID = e.PersonGUID 
		LEFT OUTER JOIN dbo.USysWebUser AS f WITH (NOLOCK) ON a.PersonGUID = f.PersonGUID

GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "a"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 114
               Right = 238
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "b"
            Begin Extent = 
               Top = 6
               Left = 276
               Bottom = 114
               Right = 477
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 6
               Left = 515
               Bottom = 114
               Right = 725
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "d"
            Begin Extent = 
               Top = 6
               Left = 763
               Bottom = 114
               Right = 990
            End
            DisplayFlags = 280
            TopColumn = 12
         End
         Begin Table = "e"
            Begin Extent = 
               Top = 114
               Left = 38
               Bottom = 222
               Right = 279
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'OneSource_Employees'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'OneSource_Employees'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'OneSource_Employees'
GO


