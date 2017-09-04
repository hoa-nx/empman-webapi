namespace EmpMan.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addcolumnsfortable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecruitmentStaffs", "InterviewRoom", c => c.String());
            AddColumn("dbo.RecruitmentStaffs", "InterviewDate", c => c.DateTime());
            AddColumn("dbo.RecruitmentStaffs", "InterviewComment", c => c.String());
            AddColumn("dbo.RecruitmentStaffs", "IsFinished", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecruitmentStaffs", "IsFinished");
            DropColumn("dbo.RecruitmentStaffs", "InterviewComment");
            DropColumn("dbo.RecruitmentStaffs", "InterviewDate");
            DropColumn("dbo.RecruitmentStaffs", "InterviewRoom");
        }
    }
}
