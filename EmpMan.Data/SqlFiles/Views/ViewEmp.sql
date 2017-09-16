USE [EmpManAPI]
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPaneCount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewEmp'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewEmp'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewEmp'
GO

/****** Object:  View [dbo].[ViewEmp]    Script Date: 2017/09/16 21:00:20 ******/
DROP VIEW [dbo].[ViewEmp]
GO

/****** Object:  View [dbo].[ViewEmp]    Script Date: 2017/09/16 21:00:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[ViewEmp]
AS
SELECT                      emp.ID, emp.No, emp.FullName, emp.Name, emp.Furigana, emp.Gender, emp.IdentNo, emp.IdentIssueDate, 
                                      emp.IdentIssuePlace, emp.TaxCode, emp.TaxCodeIssueDate, emp.ExtLinkNo, emp.TrainingProfileNo, emp.BornPlace, 
                                      emp.Avatar, emp.ShowAvatar, emp.WorkingEmail, emp.PersonalEmail, emp.BirthDay, emp.AccountName, 
                                      emp.PhoneNumber1, emp.PhoneNumber2, emp.PhoneNumber3, emp.Address1, emp.Address2, emp.CurrentCompanyID, 
                                      emp.CurrentDeptID, emp.CurrentTeamID, emp.CurrentPositionID, emp.StartIntershipDate, emp.EndIntershipDate, 
                                      emp.StartWorkingDate, emp.StartLearningDate, emp.EndLearningDate, emp.StartTrialDate, emp.EndTrialDate, 
                                      emp.ContractDate, emp.ContractTypeMasterID, emp.ContractTypeMasterDetailID, emp.JobLeaveRequestDate, 
                                      emp.JobLeaveDate, emp.IsJobLeave, emp.JobLeaveReason, emp.GoogleId, emp.MarriedDate, 
                                      emp.ExperienceBeforeContent, emp.ExperienceBeforeConvert, emp.ExperienceConvert, emp.EmpTypeMasterID, 
                                      emp.EmpTypeMasterDetailID, emp.IsBSE, emp.CollectMasterID, emp.CollectMasterDetailID, 
                                      emp.EducationLevelMasterID, emp.EducationLevelMasterDetailID, emp.Temperament, emp.Introductor, emp.BloodGroup, 
                                      emp.Hobby, emp.Objective, emp.FileID, emp.ProfileAttachmentID, emp.DisplayOrder, emp.AccountData, emp.Note, 
                                      emp.AccessDataLevel, emp.CreatedDate, emp.CreatedBy, emp.UpdatedDate, emp.UpdatedBy, emp.MetaKeyword, 
                                      emp.MetaDescription, emp.Status, emp.DataStatus, emp.UserAgent, emp.UserHostAddress, emp.UserHostName, 
                                      emp.RequestDate, emp.RequestBy, emp.ApprovedDate, emp.ApprovedBy, emp.ApprovedStatus, 
                                      emp.JapaneseLevelMasterID, emp.JapaneseLevelMasterDetailID, emp.BusinessAllowanceLevelMasterID, 
                                      emp.BusinessAllowanceLevelMasterDetailID, emp.RoomWithInternetAllowanceLevelMasterID, 
                                      emp.RoomWithInternetAllowanceLevelMasterDetailID, emp.RoomNoInternetAllowanceLevelMasterID, 
                                      emp.RoomNoInternetAllowanceLevelMasterDetailID, emp.BseAllowanceLevelMasterID, 
                                      emp.BseAllowanceLevelMasterDetailID, com.Name AS CompanyName, dep.ShortName AS DeptName, 
                                      tea.ShortName AS TeamName, pos.ShortName AS PositionName, jap.Name AS JapaneseLevelName, 
                                      alo.Name AS BusinessAllowanceLevelName, rwi.Name AS RoomWithInternetAllowanceLevelName, 
                                      rni.Name AS RoomNoInternetAllowanceLevelName, bse.Name AS BseAllowanceLevelName, 
                                      con.Name AS ContractTypeName, etp.Name AS EmpTypeName, col.Name AS CollectName, 
                                      edu.Name AS EducationLevelName, ISNULL(DATEDIFF(m, emp.StartWorkingDate, CONVERT(DATE, GETDATE())), 0) 
                                      AS KeikenFromStartWorkingMonths, ISNULL(DATEDIFF(m, emp.ContractDate, CONVERT(DATE, GETDATE())), 0) 
                                      AS KeikenFromContractMonths, DATEDIFF(year, emp.BirthDay, CONVERT(DATE, GETDATE())) 
                                      - CASE WHEN DATEADD(year, DATEDIFF(year, [emp].BirthDay, CONVERT(DATE, GETDATE())), BirthDay) 
                                      > CONVERT(DATE, GETDATE()) THEN 1 ELSE 0 END AS Age, ISNULL(DATEDIFF(DD, emp.BirthDay, CONVERT(DATE, 
                                      GETDATE())), 0) / 362.25 AS AgeFull, CASE WHEN DATEPART(d, [emp].BirthDay) = DATEPART(d, CONVERT(DATE, 
                                      GETDATE())) AND DATEPART(m, [emp].BirthDay) = DATEPART(m, CONVERT(DATE, GETDATE())) 
                                      THEN 1 ELSE 0 END AS IsBirthDay
FROM                         dbo.Emps AS emp LEFT OUTER JOIN
                                      dbo.Companys AS com ON emp.CurrentCompanyID = com.ID LEFT OUTER JOIN
                                      dbo.Depts AS dep ON emp.CurrentDeptID = dep.ID LEFT OUTER JOIN
                                      dbo.Teams AS tea ON emp.CurrentTeamID = tea.ID LEFT OUTER JOIN
                                      dbo.Positions AS pos ON emp.CurrentPositionID = pos.ID LEFT OUTER JOIN
                                      dbo.MasterDetails AS jap ON emp.JapaneseLevelMasterID = jap.MasterID AND 
                                      emp.JapaneseLevelMasterDetailID = jap.MasterDetailCode LEFT OUTER JOIN
                                      dbo.MasterDetails AS alo ON emp.BusinessAllowanceLevelMasterID = alo.MasterID AND 
                                      emp.BusinessAllowanceLevelMasterDetailID = alo.MasterDetailCode LEFT OUTER JOIN
                                      dbo.MasterDetails AS rwi ON emp.RoomWithInternetAllowanceLevelMasterID = rwi.MasterID AND 
                                      emp.RoomWithInternetAllowanceLevelMasterDetailID = rwi.MasterDetailCode LEFT OUTER JOIN
                                      dbo.MasterDetails AS rni ON emp.RoomNoInternetAllowanceLevelMasterID = rni.MasterID AND 
                                      emp.RoomNoInternetAllowanceLevelMasterDetailID = rni.MasterDetailCode LEFT OUTER JOIN
                                      dbo.MasterDetails AS bse ON emp.BseAllowanceLevelMasterID = bse.MasterID AND 
                                      emp.BseAllowanceLevelMasterDetailID = bse.MasterDetailCode LEFT OUTER JOIN
                                      dbo.MasterDetails AS con ON emp.ContractTypeMasterID = con.MasterID AND 
                                      emp.ContractTypeMasterDetailID = con.MasterDetailCode LEFT OUTER JOIN
                                      dbo.MasterDetails AS etp ON emp.EmpTypeMasterID = etp.MasterID AND 
                                      emp.EmpTypeMasterDetailID = etp.MasterDetailCode LEFT OUTER JOIN
                                      dbo.MasterDetails AS col ON emp.CollectMasterID = col.MasterID AND 
                                      emp.CollectMasterDetailID = col.MasterDetailCode LEFT OUTER JOIN
                                      dbo.MasterDetails AS edu ON emp.EducationLevelMasterID = edu.MasterID AND 
                                      emp.EducationLevelMasterDetailID = edu.MasterDetailCode
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
         Begin Table = "emp"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 383
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "com"
            Begin Extent = 
               Top = 6
               Left = 421
               Bottom = 136
               Right = 601
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "dep"
            Begin Extent = 
               Top = 6
               Left = 639
               Bottom = 136
               Right = 818
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tea"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 217
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pos"
            Begin Extent = 
               Top = 138
               Left = 255
               Bottom = 268
               Right = 497
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "jap"
            Begin Extent = 
               Top = 138
               Left = 535
               Bottom = 268
               Right = 728
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "alo"
            Begin Extent = 
               Top = 138
               Left = 766
               Bottom = 268
               Right = 959
            End
            DisplayFlags = 280
            TopColumn = 0
 ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewEmp'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'        End
         Begin Table = "rwi"
            Begin Extent = 
               Top = 270
               Left = 38
               Bottom = 400
               Right = 231
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "rni"
            Begin Extent = 
               Top = 270
               Left = 269
               Bottom = 400
               Right = 462
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "bse"
            Begin Extent = 
               Top = 270
               Left = 500
               Bottom = 400
               Right = 693
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "con"
            Begin Extent = 
               Top = 270
               Left = 731
               Bottom = 400
               Right = 924
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "etp"
            Begin Extent = 
               Top = 402
               Left = 38
               Bottom = 532
               Right = 231
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "col"
            Begin Extent = 
               Top = 402
               Left = 269
               Bottom = 532
               Right = 462
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "edu"
            Begin Extent = 
               Top = 402
               Left = 500
               Bottom = 532
               Right = 693
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewEmp'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewEmp'
GO

