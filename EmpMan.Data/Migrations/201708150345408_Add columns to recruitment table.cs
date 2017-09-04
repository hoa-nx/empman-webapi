namespace EmpMan.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addcolumnstorecruitmenttable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecruitmentInterviews", "FileID", c => c.Int());
            AddColumn("dbo.Recruitments", "FileID", c => c.Int());
            AddColumn("dbo.RecruitmentStaffs", "FileID", c => c.Int());
            AddColumn("dbo.RecruitmentStaffs", "IsRegister", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecruitmentStaffs", "IsRegister");
            DropColumn("dbo.RecruitmentStaffs", "FileID");
            DropColumn("dbo.Recruitments", "FileID");
            DropColumn("dbo.RecruitmentInterviews", "FileID");
        }
    }
}
