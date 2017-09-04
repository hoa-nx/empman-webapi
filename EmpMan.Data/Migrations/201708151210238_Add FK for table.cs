namespace EmpMan.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFKfortable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecruitmentInterviews", "RecruitmentStaffNo", c => c.Int());
            CreateIndex("dbo.RecruitmentInterviews", "RegInterviewEmpID");
            CreateIndex("dbo.RecruitmentInterviews", "FileID");
            CreateIndex("dbo.RecruitmentInterviews", "RecruitmentStaffNo");
            CreateIndex("dbo.RecruitmentStaffs", "DeptReceived");
            CreateIndex("dbo.RecruitmentStaffs", "TeamReceived");
            CreateIndex("dbo.RecruitmentStaffs", "SupportEmpID");
            CreateIndex("dbo.RecruitmentStaffs", "SystemEmpID");
            CreateIndex("dbo.RecruitmentStaffs", "FileID");
            AddForeignKey("dbo.RecruitmentInterviews", "FileID", "dbo.FileStorages", "ID");
            AddForeignKey("dbo.RecruitmentStaffs", "DeptReceived", "dbo.Depts", "ID");
            AddForeignKey("dbo.RecruitmentStaffs", "FileID", "dbo.FileStorages", "ID");
            AddForeignKey("dbo.RecruitmentStaffs", "SupportEmpID", "dbo.Emps", "ID");
            AddForeignKey("dbo.RecruitmentStaffs", "SystemEmpID", "dbo.Emps", "ID");
            AddForeignKey("dbo.RecruitmentStaffs", "TeamReceived", "dbo.Teams", "ID");
            AddForeignKey("dbo.RecruitmentInterviews", "RecruitmentStaffNo", "dbo.RecruitmentStaffs", "ID");
            AddForeignKey("dbo.RecruitmentInterviews", "RegInterviewEmpID", "dbo.Emps", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RecruitmentInterviews", "RegInterviewEmpID", "dbo.Emps");
            DropForeignKey("dbo.RecruitmentInterviews", "RecruitmentStaffNo", "dbo.RecruitmentStaffs");
            DropForeignKey("dbo.RecruitmentStaffs", "TeamReceived", "dbo.Teams");
            DropForeignKey("dbo.RecruitmentStaffs", "SystemEmpID", "dbo.Emps");
            DropForeignKey("dbo.RecruitmentStaffs", "SupportEmpID", "dbo.Emps");
            DropForeignKey("dbo.RecruitmentStaffs", "FileID", "dbo.FileStorages");
            DropForeignKey("dbo.RecruitmentStaffs", "DeptReceived", "dbo.Depts");
            DropForeignKey("dbo.RecruitmentInterviews", "FileID", "dbo.FileStorages");
            DropIndex("dbo.RecruitmentStaffs", new[] { "FileID" });
            DropIndex("dbo.RecruitmentStaffs", new[] { "SystemEmpID" });
            DropIndex("dbo.RecruitmentStaffs", new[] { "SupportEmpID" });
            DropIndex("dbo.RecruitmentStaffs", new[] { "TeamReceived" });
            DropIndex("dbo.RecruitmentStaffs", new[] { "DeptReceived" });
            DropIndex("dbo.RecruitmentInterviews", new[] { "RecruitmentStaffNo" });
            DropIndex("dbo.RecruitmentInterviews", new[] { "FileID" });
            DropIndex("dbo.RecruitmentInterviews", new[] { "RegInterviewEmpID" });
            DropColumn("dbo.RecruitmentInterviews", "RecruitmentStaffNo");
        }
    }
}
