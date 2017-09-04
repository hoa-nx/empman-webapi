namespace EmpMan.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddcolumnstoEmpstable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Emps", "InterviewDate", c => c.DateTime());
            AddColumn("dbo.Emps", "InterviewEmp", c => c.String());
            AddColumn("dbo.Emps", "WorkingConditionTalkDate", c => c.DateTime());
            AddColumn("dbo.Emps", "TrialResult", c => c.DateTime());
            AddColumn("dbo.Emps", "BabyBornStartDate", c => c.DateTime());
            AddColumn("dbo.Emps", "BabyBornScheduleEndDate", c => c.DateTime());
            AddColumn("dbo.Emps", "BabyBornActualEndDate", c => c.DateTime());
            AddColumn("dbo.Emps", "BabyBornStartDate2", c => c.DateTime());
            AddColumn("dbo.Emps", "BabyBornScheduleEndDate2", c => c.DateTime());
            AddColumn("dbo.Emps", "BabyBornActualEndDate2", c => c.DateTime());
            AddColumn("dbo.Emps", "IsMarried", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Emps", "IsMarried");
            DropColumn("dbo.Emps", "BabyBornActualEndDate2");
            DropColumn("dbo.Emps", "BabyBornScheduleEndDate2");
            DropColumn("dbo.Emps", "BabyBornStartDate2");
            DropColumn("dbo.Emps", "BabyBornActualEndDate");
            DropColumn("dbo.Emps", "BabyBornScheduleEndDate");
            DropColumn("dbo.Emps", "BabyBornStartDate");
            DropColumn("dbo.Emps", "TrialResult");
            DropColumn("dbo.Emps", "WorkingConditionTalkDate");
            DropColumn("dbo.Emps", "InterviewEmp");
            DropColumn("dbo.Emps", "InterviewDate");
        }
    }
}
