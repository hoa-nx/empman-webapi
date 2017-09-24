namespace EmpMan.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddcolumnsEmpDetailWorktables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmpDetailWorks", "IsChangeCompanyID", c => c.Int());
            AddColumn("dbo.EmpDetailWorks", "IsChangeDeptID", c => c.Int());
            AddColumn("dbo.EmpDetailWorks", "IsChangeTeamID", c => c.Int());
            AddColumn("dbo.EmpDetailWorks", "IsChangePositionID", c => c.Int());
            AddColumn("dbo.EmpDetailWorks", "IsChangeCompany2ID", c => c.Int());
            AddColumn("dbo.EmpDetailWorks", "IsChangeDept2ID", c => c.Int());
            AddColumn("dbo.EmpDetailWorks", "IsChangeTeam2ID", c => c.Int());
            AddColumn("dbo.EmpDetailWorks", "IsChangePosition2ID", c => c.Int());
            AddColumn("dbo.EmpDetailWorks", "IsChangeWorkEmpType", c => c.Int());
            AddColumn("dbo.EmpDetailWorks", "IsChangeEmpType", c => c.Int());
            AddColumn("dbo.EmpDetailWorks", "IsChangeJapaneseLevel", c => c.Int());
            AddColumn("dbo.EmpDetailWorks", "IsChangeBusinessAllowanceLevel", c => c.Int());
            AddColumn("dbo.EmpDetailWorks", "IsChangeRoomWithInternetAllowanceLevel", c => c.Int());
            AddColumn("dbo.EmpDetailWorks", "IsChangeRoomNoInternetAllowanceLevel", c => c.Int());
            AddColumn("dbo.EmpDetailWorks", "IsChangeBseAllowanceLevel", c => c.Int());
            AddColumn("dbo.EmpDetailWorks", "IsChangeCollect", c => c.Int());
            AddColumn("dbo.EmpDetailWorks", "IsChangeEducationLevel", c => c.Int());
            AddColumn("dbo.EmpDetailWorks", "IsChangeContractType", c => c.Int());
            AddColumn("dbo.EmpDetailWorks", "IsChangeOnsiteCustomerID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmpDetailWorks", "IsChangeOnsiteCustomerID");
            DropColumn("dbo.EmpDetailWorks", "IsChangeContractType");
            DropColumn("dbo.EmpDetailWorks", "IsChangeEducationLevel");
            DropColumn("dbo.EmpDetailWorks", "IsChangeCollect");
            DropColumn("dbo.EmpDetailWorks", "IsChangeBseAllowanceLevel");
            DropColumn("dbo.EmpDetailWorks", "IsChangeRoomNoInternetAllowanceLevel");
            DropColumn("dbo.EmpDetailWorks", "IsChangeRoomWithInternetAllowanceLevel");
            DropColumn("dbo.EmpDetailWorks", "IsChangeBusinessAllowanceLevel");
            DropColumn("dbo.EmpDetailWorks", "IsChangeJapaneseLevel");
            DropColumn("dbo.EmpDetailWorks", "IsChangeEmpType");
            DropColumn("dbo.EmpDetailWorks", "IsChangeWorkEmpType");
            DropColumn("dbo.EmpDetailWorks", "IsChangePosition2ID");
            DropColumn("dbo.EmpDetailWorks", "IsChangeTeam2ID");
            DropColumn("dbo.EmpDetailWorks", "IsChangeDept2ID");
            DropColumn("dbo.EmpDetailWorks", "IsChangeCompany2ID");
            DropColumn("dbo.EmpDetailWorks", "IsChangePositionID");
            DropColumn("dbo.EmpDetailWorks", "IsChangeTeamID");
            DropColumn("dbo.EmpDetailWorks", "IsChangeDeptID");
            DropColumn("dbo.EmpDetailWorks", "IsChangeCompanyID");
        }
    }
}
