namespace EmpMan.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addrecruitmenttables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RecruitmentInterviews",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RecruitmentID = c.String(maxLength: 128),
                        RecruitmentStaffID = c.String(maxLength: 128),
                        Name = c.String(maxLength: 256),
                        ShortName = c.String(maxLength: 256),
                        RegInterviewEmpID = c.Int(),
                        IsInterviewed = c.Boolean(),
                        IsStaffCancel = c.Boolean(),
                        ScheduleInterviewDate = c.DateTime(),
                        ActualInterviewDate = c.DateTime(),
                        ScheduleInterviewRoom = c.String(),
                        ActualInterviewRoom = c.String(),
                        InterviewContent = c.String(),
                        InterviewComment = c.String(),
                        InterviewResult = c.String(),
                        IsFinished = c.Boolean(),
                        ReportDate = c.DateTime(),
                        IsTrainingIntroduction = c.Boolean(),
                        IsSendSMS = c.Boolean(),
                        SMSCount = c.Int(),
                        SMSContent = c.String(),
                        RowVersion = c.Binary(),
                        DisplayOrder = c.Int(),
                        AccountData = c.String(maxLength: 256),
                        Note = c.String(),
                        AccessDataLevel = c.Int(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                        DataStatus = c.Int(),
                        UserAgent = c.String(),
                        UserHostAddress = c.String(),
                        UserHostName = c.String(),
                        RequestDate = c.DateTime(),
                        RequestBy = c.String(),
                        ApprovedDate = c.DateTime(),
                        ApprovedBy = c.String(),
                        ApprovedStatus = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Recruitments",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        No = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        ShortName = c.String(maxLength: 256),
                        RecruitmentTypeMasterID = c.Int(),
                        RecruitmentTypeMasterDetailID = c.Int(),
                        CvCompanyFolderPath = c.String(),
                        CvDeptFolderPath = c.String(),
                        CvCount = c.Int(),
                        SendMailFromEmpID = c.Int(),
                        SendMailToEmpID = c.Int(),
                        AnsRecruitDeptDeadlineDate = c.DateTime(),
                        AnsLocalDeadlineDate = c.DateTime(),
                        IsNotification = c.Boolean(),
                        ExpireDate = c.DateTime(),
                        Content = c.String(),
                        IsFinished = c.Boolean(),
                        RowVersion = c.Binary(),
                        DisplayOrder = c.Int(),
                        AccountData = c.String(maxLength: 256),
                        Note = c.String(),
                        AccessDataLevel = c.Int(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                        DataStatus = c.Int(),
                        UserAgent = c.String(),
                        UserHostAddress = c.String(),
                        UserHostName = c.String(),
                        RequestDate = c.DateTime(),
                        RequestBy = c.String(),
                        ApprovedDate = c.DateTime(),
                        ApprovedBy = c.String(),
                        ApprovedStatus = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RecruitmentStaffs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RecruitmentID = c.String(maxLength: 128),
                        RecruitmentStaffID = c.String(maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        ShortName = c.String(maxLength: 256),
                        RecruitmentTypeMasterID = c.Int(),
                        RecruitmentTypeMasterDetailID = c.Int(),
                        RequestInCompanyDate = c.DateTime(),
                        InterviewResult = c.String(),
                        RequestInterviewDate = c.DateTime(),
                        InterViewTime = c.DateTime(),
                        ExamRound1 = c.String(),
                        ExamResult = c.String(),
                        CompanyCvNo = c.Int(),
                        Pharse = c.Int(),
                        FullName = c.String(),
                        BirthDay = c.DateTime(),
                        Gender = c.Boolean(),
                        National = c.String(),
                        IdentNo = c.String(),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                        KiboSalary = c.Decimal(precision: 18, scale: 2),
                        EducationLevel = c.String(),
                        CollectName = c.String(),
                        ProfessionalKbn = c.String(),
                        EducationType = c.String(),
                        Grade = c.String(),
                        IsCertificated = c.Boolean(),
                        DebtSubjectCount = c.Int(),
                        DebtSubjectReason = c.String(),
                        CertificatedDateTime = c.String(),
                        JapaneseLevel = c.String(),
                        EnglishLevel = c.String(),
                        OtherSkill = c.String(),
                        MarriedStatus = c.String(),
                        Objective = c.String(),
                        CvNote = c.String(),
                        Comment1 = c.String(),
                        Comment2 = c.String(),
                        CvCreateDate = c.DateTime(),
                        CvUpdateDate = c.DateTime(),
                        CvSendCount = c.Int(),
                        CvSendList = c.String(),
                        StartWorkingDate = c.DateTime(),
                        AdddressPlace = c.String(),
                        BornPlace = c.String(),
                        Hobby = c.String(),
                        IsTestRound1ByPass = c.String(),
                        GradeTestRound1 = c.Decimal(precision: 18, scale: 2),
                        EngGradeTestRound1 = c.Decimal(precision: 18, scale: 2),
                        ProfessionalKbnGradeTestRound1 = c.Decimal(precision: 18, scale: 2),
                        GradeTestRound2 = c.Decimal(precision: 18, scale: 2),
                        CvStatus = c.String(),
                        EmpType = c.String(),
                        TrainingClassConditionTalkDate = c.DateTime(),
                        WorkingConditionTalkDate = c.DateTime(),
                        Avatar = c.String(),
                        IsSendSMS = c.Boolean(),
                        SMSCount = c.Int(),
                        SMSContent = c.String(),
                        IsTrainingIntroduction = c.Boolean(),
                        DeptReceived = c.Int(),
                        TeamReceived = c.Int(),
                        TrialStartDate = c.DateTime(),
                        SupportEmpID = c.Int(),
                        GhostPC = c.String(maxLength: 256),
                        ItMailNotificationDate = c.DateTime(),
                        ResourceDeptMailNotificationDate = c.DateTime(),
                        SystemEmpID = c.Int(),
                        RowVersion = c.Binary(),
                        DisplayOrder = c.Int(),
                        AccountData = c.String(maxLength: 256),
                        Note = c.String(),
                        AccessDataLevel = c.Int(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                        DataStatus = c.Int(),
                        UserAgent = c.String(),
                        UserHostAddress = c.String(),
                        UserHostName = c.String(),
                        RequestDate = c.DateTime(),
                        RequestBy = c.String(),
                        ApprovedDate = c.DateTime(),
                        ApprovedBy = c.String(),
                        ApprovedStatus = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RecruitmentStaffs");
            DropTable("dbo.Recruitments");
            DropTable("dbo.RecruitmentInterviews");
        }
    }
}