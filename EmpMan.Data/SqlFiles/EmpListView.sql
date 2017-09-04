SELECT [emp].[ID]
      ,[emp].[No]
      ,[emp].[FullName]
      ,[emp].[Name]
      ,[emp].[Furigana]
      ,[emp].[Gender]
      ,[emp].[IdentNo]
      ,[emp].[IdentIssueDate]
      ,[emp].[IdentIssuePlace]
      ,[emp].[TaxCode]
      ,[emp].[TaxCodeIssueDate]
      ,[emp].[ExtLinkNo]
      ,[emp].[TrainingProfileNo]
      ,[emp].[BornPlace]
      ,[emp].[Avatar]
      ,[emp].[ShowAvatar]
      ,[emp].[WorkingEmail]
      ,[emp].[PersonalEmail]
      ,[emp].[BirthDay]
      ,[emp].[AccountName]
      ,[emp].[PhoneNumber1]
      ,[emp].[PhoneNumber2]
      ,[emp].[PhoneNumber3]
      ,[emp].[Address1]
      ,[emp].[Address2]
      ,[emp].[CurrentCompanyID]
      ,[emp].[CurrentDeptID]
      ,[emp].[CurrentTeamID]
      ,[emp].[CurrentPositionID]
      ,[emp].[StartIntershipDate]
      ,[emp].[EndIntershipDate]
      ,[emp].[StartWorkingDate]
      ,[emp].[StartLearningDate]
      ,[emp].[EndLearningDate]
      ,[emp].[StartTrialDate]
      ,[emp].[EndTrialDate]
      ,[emp].[ContractDate]
      ,[emp].[ContractTypeMasterID]
      ,[emp].[ContractTypeMasterDetailID]
      ,[emp].[JobLeaveRequestDate]
      ,[emp].[JobLeaveDate]
      ,[emp].[IsJobLeave]
      ,[emp].[JobLeaveReason]
      ,[emp].[GoogleId]
      ,[emp].[MarriedDate]
      ,[emp].[ExperienceBeforeContent]
      ,[emp].[ExperienceBeforeConvert]
      ,[emp].[ExperienceConvert]
      ,[emp].[EmpTypeMasterID]
      ,[emp].[EmpTypeMasterDetailID]
      ,[emp].[IsBSE]
      ,[emp].[CollectMasterID]
      ,[emp].[CollectMasterDetailID]
      ,[emp].[EducationLevelMasterID]
      ,[emp].[EducationLevelMasterDetailID]
      ,[emp].[Temperament]
      ,[emp].[Introductor]
      ,[emp].[BloodGroup]
      ,[emp].[Hobby]
      ,[emp].[Objective]
      ,[emp].[FileID]
      ,[emp].[ProfileAttachmentID]
      ,[emp].[DisplayOrder]
      ,[emp].[AccountData]
      ,[emp].[Note]
      ,[emp].[AccessDataLevel]
      ,[emp].[CreatedDate]
      ,[emp].[CreatedBy]
      ,[emp].[UpdatedDate]
      ,[emp].[UpdatedBy]
      ,[emp].[MetaKeyword]
      ,[emp].[MetaDescription]
      ,[emp].[Status]
      ,[emp].[DataStatus]
      ,[emp].[UserAgent]
      ,[emp].[UserHostAddress]
      ,[emp].[UserHostName]
      ,[emp].[RequestDate]
      ,[emp].[RequestBy]
      ,[emp].[ApprovedDate]
      ,[emp].[ApprovedBy]
      ,[emp].[ApprovedStatus]
      ,[emp].[JapaneseLevelMasterID]
      ,[emp].[JapaneseLevelMasterDetailID]
      ,[emp].[BusinessAllowanceLevelMasterID]
      ,[emp].[BusinessAllowanceLevelMasterDetailID]
      ,[emp].[RoomWithInternetAllowanceLevelMasterID]
      ,[emp].[RoomWithInternetAllowanceLevelMasterDetailID]
      ,[emp].[RoomNoInternetAllowanceLevelMasterID]
      ,[emp].[RoomNoInternetAllowanceLevelMasterDetailID]
      ,[emp].[BseAllowanceLevelMasterID]
      ,[emp].[BseAllowanceLevelMasterDetailID]

	  ,[com].[Name] CompanyName
	  ,[dep].[ShortName] DeptName
	  ,[tea].[ShortName] TeamName
	  ,[pos].[ShortName] PositionName
	  ,[jap].[Name] JapaneseLevelName
	  ,[alo].[Name] BusinessAllowanceLevelName

	  ,[rwi].[Name] RoomWithInternetAllowanceLevelName
	  ,[rni].[Name] RoomNoInternetAllowanceLevelName
	  ,[bse].[Name] BseAllowanceLevelName
	  ,[con].[Name] ContractTypeName
	  ,[etp].[Name] EmpTypeName
	  ,[col].[Name] CollectName
	  ,[edu].[Name] EducationLevelName

	  , ISNULL( DATEDIFF(m,[emp].Startworkingdate , CONVERT(DATE,GETDATE())),0)  KeikenFromStartWorkingMonths
	  , ISNULL(DATEDIFF(m,[emp].ContractDate ,CONVERT(DATE,GETDATE())),0)  KeikenFromContractMonths
	  , DATEDIFF(year, [emp].BirthDay, CONVERT(DATE,GETDATE()))- case when DATEADD(year,DATEDIFF(year, [emp].BirthDay, CONVERT(DATE,GETDATE())), BirthDay)> CONVERT(DATE,GETDATE()) then 1 else 0 end   Age
	  , ISNULL(DATEDIFF(DD,[emp].BirthDay , CONVERT(DATE,GETDATE())),0) /362.25 AgeFull
	  , case when DATEPART(d, [emp].BirthDay) = DATEPART(d, CONVERT(DATE,GETDATE())) AND DATEPART(m, [emp].BirthDay) = DATEPART(m, CONVERT(DATE,GETDATE())) THEN 1 ELSE 0 END IsBirthDay


  FROM [dbo].[Emps] emp 
  left outer join [dbo].[Companys]  com on emp.CurrentCompanyID = com.ID
  left outer join [dbo].[Depts]  dep on emp.CurrentDeptID= dep.ID
  left outer join [dbo].[Teams]  tea on emp.CurrentTeamID= tea.ID
  left outer join [dbo].[Positions]  pos on emp.CurrentPositionID= pos.ID
  left outer join [dbo].[MasterDetails]  jap on emp.JapaneseLevelMasterID= jap.MasterID and  emp.JapaneseLevelMasterDetailID= jap.MasterDetailCode
  left outer join [dbo].[MasterDetails]  alo on emp.BusinessAllowanceLevelMasterID= alo.MasterID and  emp.BusinessAllowanceLevelMasterDetailID= alo.MasterDetailCode
  left outer join [dbo].[MasterDetails]  rwi on emp.RoomWithInternetAllowanceLevelMasterID= rwi.MasterID and  emp.RoomWithInternetAllowanceLevelMasterDetailID= rwi.MasterDetailCode
  left outer join [dbo].[MasterDetails]  rni on emp.RoomNoInternetAllowanceLevelMasterID= rni.MasterID and  emp.RoomNoInternetAllowanceLevelMasterDetailID= rni.MasterDetailCode
  left outer join [dbo].[MasterDetails]  bse on emp.BseAllowanceLevelMasterID= bse.MasterID and  emp.BseAllowanceLevelMasterDetailID= bse.MasterDetailCode
  left outer join [dbo].[MasterDetails]  con on emp.ContractTypeMasterID= con.MasterID and  emp.ContractTypeMasterDetailID= con.MasterDetailCode
  left outer join [dbo].[MasterDetails]  etp on emp.EmpTypeMasterID= etp.MasterID and  emp.EmpTypeMasterDetailID= etp.MasterDetailCode
  left outer join [dbo].[MasterDetails]  col on emp.CollectMasterID= col.MasterID and  emp.CollectMasterDetailID= col.MasterDetailCode
  left outer join [dbo].[MasterDetails]  edu on emp.EducationLevelMasterID= edu.MasterID and  emp.EducationLevelMasterDetailID= edu.MasterDetailCode

