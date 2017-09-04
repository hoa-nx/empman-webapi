namespace EmpMan.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdditemtoEmpDetailWorktable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmpDetailWorks", "Company2ID", c => c.Int());
            AddColumn("dbo.EmpDetailWorks", "Dept2ID", c => c.Int());
            AddColumn("dbo.EmpDetailWorks", "Team2ID", c => c.Int());
            AddColumn("dbo.EmpDetailWorks", "Position2ID", c => c.Int());
            AddColumn("dbo.EmpDetailWorks", "WorkEmpTypeMasterID", c => c.Int());
            AddColumn("dbo.EmpDetailWorks", "WorkEmpTypeMasterDetailID", c => c.Int());
            CreateIndex("dbo.EmpDetailWorks", new[] { "WorkEmpTypeMasterID", "WorkEmpTypeMasterDetailID" });
            AddForeignKey("dbo.EmpDetailWorks", new[] { "WorkEmpTypeMasterID", "WorkEmpTypeMasterDetailID" }, "dbo.MasterDetails", new[] { "MasterID", "MasterDetailCode" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmpDetailWorks", new[] { "WorkEmpTypeMasterID", "WorkEmpTypeMasterDetailID" }, "dbo.MasterDetails");
            DropIndex("dbo.EmpDetailWorks", new[] { "WorkEmpTypeMasterID", "WorkEmpTypeMasterDetailID" });
            DropColumn("dbo.EmpDetailWorks", "WorkEmpTypeMasterDetailID");
            DropColumn("dbo.EmpDetailWorks", "WorkEmpTypeMasterID");
            DropColumn("dbo.EmpDetailWorks", "Position2ID");
            DropColumn("dbo.EmpDetailWorks", "Team2ID");
            DropColumn("dbo.EmpDetailWorks", "Dept2ID");
            DropColumn("dbo.EmpDetailWorks", "Company2ID");
        }
    }
}
