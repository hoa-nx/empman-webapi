SELECT RES.[ID]
    ,RES.[RecruitmentID]
    ,RES.[RecruitmentStaffID]
    ,RES.[Name]
    ,RES.[ShortName]
    ,RES.[RecruitmentTypeMasterID]
    ,RES.[RecruitmentTypeMasterDetailID]
    ,RES.[RequestInCompanyDate]
    ,RES.[InterviewResult]
    ,RES.[RequestInterviewDate]
    ,RES.[InterViewTime]
    ,RES.[ExamRound1]
    ,RES.[ExamResult]
    ,RES.[CompanyCvNo]
    ,RES.[Pharse]
    ,RES.[FullName]
    ,RES.[BirthDay]
    ,RES.[Gender]
    ,RES.[National]
    ,RES.[IdentNo]
    ,RES.[PhoneNumber]
    ,RES.[Email]
    ,RES.[KiboSalary]
    ,RES.[EducationLevel]
    ,RES.[CollectName]
    ,RES.[ProfessionalKbn]
    ,RES.[EducationType]
    ,RES.[Grade]
    ,RES.[IsCertificated]
    ,RES.[DebtSubjectCount]
    ,RES.[DebtSubjectReason]
    ,RES.[CertificatedDateTime]
    ,RES.[OtherCertificated]
    ,RES.[JapaneseLevel]
    ,RES.[EnglishLevel]
    ,RES.[OtherSkill]
    ,RES.[MarriedStatus]
    ,RES.[Objective]
    ,RES.[CvNote]
    ,RES.[Comment1]
    ,RES.[Comment2]
    ,RES.[CvCreateDate]
    ,RES.[CvUpdateDate]
    ,RES.[CvSendCount]
    ,RES.[CvSendList]
    ,RES.[StartWorkingDate]
    ,RES.[AdddressPlace]
    ,RES.[BornPlace]
    ,RES.[Hobby]
    ,RES.[IsTestRound1ByPass]
    ,RES.[GradeTestRound1]
    ,RES.[EngGradeTestRound1]
    ,RES.[ProfessionalKbnGradeTestRound1]
    ,RES.[GradeTestRound2]
    ,RES.[CvStatus]
    ,RES.[EmpType]
    ,RES.[TrainingClassConditionTalkDate]
    ,RES.[WorkingConditionTalkDate]
    ,RES.[Avatar]
    ,RES.[IsSendSMS]
    ,RES.[SMSCount]
    ,RES.[SMSContent]
    ,RES.[IsTrainingIntroduction]
    ,RES.[DeptReceived]
    ,RES.[TeamReceived]
    ,RES.[TrialStartDate]
    ,RES.[SupportEmpID]
    ,RES.[GhostPC]
    ,RES.[ItMailNotificationDate]
    ,RES.[ResourceDeptMailNotificationDate]
    ,RES.[SystemEmpID]
    ,RES.[RowVersion]
    ,RES.[DisplayOrder]
    ,RES.[AccountData]
    ,RES.[Note]
    ,RES.[AccessDataLevel]
    ,RES.[CreatedDate]
    ,RES.[CreatedBy]
    ,RES.[UpdatedDate]
    ,RES.[UpdatedBy]
    ,RES.[MetaKeyword]
    ,RES.[MetaDescription]
    ,RES.[Status]
    ,RES.[DataStatus]
    ,RES.[UserAgent]
    ,RES.[UserHostAddress]
    ,RES.[UserHostName]
    ,RES.[RequestDate]
    ,RES.[RequestBy]
    ,RES.[ApprovedDate]
    ,RES.[ApprovedBy]
    ,RES.[ApprovedStatus]
    ,RES.[FileID]
    ,RES.[InterviewRoom]
    ,RES.[InterviewDate]
    ,RES.[InterviewComment]
    ,case when REI.[RecruitmentID] IS NOT NULL  THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END IsRegister
    ,RES.[IsFinished]  
    ,REI.Cnt   
    ,REI.ApprovedStatus  ApprovedStatusInterview
	,REC.Name	RecruitmentName
	,REC.CvCount	RecruitmentCvCount
	,REC.IsFinished	RecruitmentIsFinish
	,RET.NAME RecruitmentTypeName
    ,CurrentStatus = CASE  
						--@@chờ phỏng vấn ( còn cho phép đăng ký)
						when  DATEDIFF(MINUTE, REC.AnsRecruitDeptDeadlineDate , GETDATE()) < 0 THEN 0 
						--@@không đăng ký phỏng vấn sau khi đã hết hạn ( trong table interview không có data tương ứng với ứng viên này) 
						when DATEDIFF(MINUTE, REC.AnsRecruitDeptDeadlineDate , GETDATE()) > 0 AND REI.[RecruitmentStaffID] IS NULL THEN 10 
						--@@chưa có lịch phỏng vấn 
						when REC.ID IS NOT NULL AND ( RES.InterviewDate IS NULL) THEN 20 
						--@@có đăng ký phỏng vấn nhưng chưa tiến hành phỏng vấn (~ chờ phỏng vấn 30)
						when REC.ID IS NOT NULL AND ( RES.InterviewDate IS NOT NULL AND DATEDIFF(MINUTE, RES.InterviewDate , GETDATE()) < 0) THEN 30 
						--@@Chờ kết quả phỏng vấn
						when REC.ID IS NOT NULL AND ( RES.InterviewDate IS NOT NULL AND DATEDIFF(MINUTE, RES.InterviewDate , GETDATE()) > 0) AND RTRIM(LTRIM(ISNULL(RES.InterviewResult,''))) ='' THEN 40
						--@@Phỏng vấn NG 
						when REC.ID IS NOT NULL AND ( RES.InterviewDate IS NOT NULL AND DATEDIFF(MINUTE, RES.InterviewDate , GETDATE()) > 0) AND RTRIM(LTRIM(ISNULL(RES.InterviewResult,''))) IN(N'Không đạt',N'Không tới',N'Đã tìm được việc') THEN 41 
						--@@Chờ nói chuyện DKLC ( phỏng vấn xong và kết quả là ok)
						when REC.ID IS NOT NULL AND ( RES.InterviewDate IS NOT NULL AND DATEDIFF(MINUTE, RES.InterviewDate , GETDATE()) > 0) AND RTRIM(LTRIM(ISNULL(RES.InterviewResult,''))) !='' AND (RES.WorkingConditionTalkDate IS NULL OR (RES.WorkingConditionTalkDate IS NOT NULL AND DATEDIFF(MINUTE, RES.WorkingConditionTalkDate, GETDATE()) < 0) )THEN 50 
						--@@Chờ feedback sau khi đã nói chuyện DKLV 
						when REC.ID IS NOT NULL AND ( RES.InterviewDate IS NOT NULL AND DATEDIFF(MINUTE, RES.InterviewDate , GETDATE()) > 0) AND RTRIM(LTRIM(ISNULL(RES.InterviewResult,''))) !='' AND RES.WorkingConditionTalkDate IS NOT NULL AND RES.TrialStartDate IS NULL THEN 60 
						--@@ Đang chờ vào thử việc( co ngay thu viec >= ngay hien tai )
						when REC.ID IS NOT NULL AND ( RES.InterviewDate IS NOT NULL AND DATEDIFF(MINUTE, RES.InterviewDate , GETDATE()) > 0) AND RTRIM(LTRIM(ISNULL(RES.InterviewResult,''))) !='' AND RES.TrialStartDate >  CONVERT(DATE,GETDATE()) THEN 70 
						--@@Đã vào thử việc nhưng chưa đăng ký nhân viên
						when REC.ID IS NOT NULL AND ( RES.InterviewDate IS NOT NULL AND DATEDIFF(MINUTE, RES.InterviewDate , GETDATE()) > 0) AND RTRIM(LTRIM(ISNULL(RES.InterviewResult,''))) !='' AND RES.TrialStartDate <= CONVERT(DATE,GETDATE()) AND EMP.ID IS NULL THEN 80 
						--@@Đã vào thử việc và đã đăng ký nhân viên
						when REC.ID IS NOT NULL AND ( RES.InterviewDate IS NOT NULL AND DATEDIFF(MINUTE, RES.InterviewDate , GETDATE()) > 0) AND RTRIM(LTRIM(ISNULL(RES.InterviewResult,''))) !='' AND RES.TrialStartDate <= CONVERT(DATE,GETDATE()) AND EMP.ID IS NOT NULL THEN 90 
						ELSE
						NULL
	END 

FROM 
[RecruitmentStaffs] RES
LEFT OUTER JOIN [Recruitments] REC ON RES.RecruitmentID = REC.ID
LEFT OUTER JOIN [Emps] EMP ON RES.SystemEmpID = EMP.ID 
LEFT OUTER JOIN (
SELECT DISTINCT
    [RecruitmentID]
    ,[RecruitmentStaffID]
    ,[Name]
    , COUNT(*) OVER ( PaRTITION BY [RecruitmentID] ,[RecruitmentStaffID] ) AS CNT
    , ISNULL(ApprovedStatus,0) ApprovedStatus
	FROM [RecruitmentInterviews] 
	--WHERE ([RegInterviewEmpID] = " + empId + @" OR 0 = " + empId + @")
) REI
ON (RES.[RecruitmentID] = REI.[RecruitmentID] AND RES.[RecruitmentStaffID] = REI.[RecruitmentStaffID])
LEFT OUTER JOIN [dbo].[MasterDetails]  ret on rec.RecruitmentTypeMasterID= ret.MasterID and  rec.RecruitmentTypeMasterDetailID= ret.MasterDetailCode
WHERE 1=1 