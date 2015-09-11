USE [OneSource]
GO

/****** Object:  View [dbo].[CS_View_FirstAlertReportOtherVehicle]    Script Date: 07/28/2011 20:10:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[CS_View_FirstAlertReportOtherVehicle]
AS

SELECT
	dbo.CS_FirstAlertVehicle.Make, 
	dbo.CS_FirstAlertVehicle.Model, 
	dbo.CS_FirstAlertVehicle.ID AS FirstAlertVehicleId, 
	dbo.CS_FirstAlertVehicle.Year, 
	dbo.CS_FirstAlertVehicle.Damage, 
	dbo.CS_FirstAlertPerson.LastName, 
	dbo.CS_FirstAlertPerson.FirstName, 
	dbo.CS_FirstAlertPerson.VehiclePosition, 
	dbo.CS_FirstAlert.ID AS FirstAlertId, 
	dbo.CS_FirstAlertVehicle.Active, 
	dbo.CS_FirstAlertVehicle.IsHulcherVehicle
FROM
	dbo.CS_FirstAlertVehicle 
		INNER JOIN dbo.CS_FirstAlert ON dbo.CS_FirstAlertVehicle.FirstAlertID = dbo.CS_FirstAlert.ID AND dbo.CS_FirstAlert.Active = 1 
		LEFT OUTER JOIN dbo.CS_FirstAlertPerson 
			ON dbo.CS_FirstAlertVehicle.ID = dbo.CS_FirstAlertPerson.FirstAlertVehicleID 
			AND dbo.CS_FirstAlert.ID = dbo.CS_FirstAlertPerson.FirstAlertID 
			AND dbo.CS_FirstAlertPerson.Active = 1


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
         Begin Table = "CS_FirstAlertVehicle"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 114
               Right = 198
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CS_FirstAlert"
            Begin Extent = 
               Top = 6
               Left = 236
               Bottom = 114
               Right = 436
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CS_FirstAlertPerson"
            Begin Extent = 
               Top = 6
               Left = 474
               Bottom = 114
               Right = 673
            End
            DisplayFlags = 280
            TopColumn = 3
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CS_View_FirstAlertReportOtherVehicle'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CS_View_FirstAlertReportOtherVehicle'
GO


