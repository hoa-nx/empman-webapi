namespace EmpMan.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddmorecolumnsonTargettable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Targets", "Koritu", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Targets", "ActKoritu", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Targets", "ChangePercentEmp", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Targets", "ChangeEmp", c => c.Int());
            AddColumn("dbo.Targets", "ManagerEmp", c => c.Int());
            AddColumn("dbo.Targets", "Leader2Emp", c => c.Int());
            AddColumn("dbo.Targets", "Leader1Emp", c => c.Int());
            AddColumn("dbo.Targets", "SubLeader2", c => c.Int());
            AddColumn("dbo.Targets", "SubLeader1", c => c.Int());
            AddColumn("dbo.Targets", "LeaveJobPercentEmp", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Targets", "LeaveJobEmp", c => c.Int());
            AddColumn("dbo.Targets", "ActChangePercentEmp", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Targets", "ActChangeEmp", c => c.Int());
            AddColumn("dbo.Targets", "ActManagerEmp", c => c.Int());
            AddColumn("dbo.Targets", "ActLeader2Emp", c => c.Int());
            AddColumn("dbo.Targets", "ActLeader1Emp", c => c.Int());
            AddColumn("dbo.Targets", "ActSubLeader2", c => c.Int());
            AddColumn("dbo.Targets", "ActSubLeader1", c => c.Int());
            AddColumn("dbo.Targets", "ActLeaveJobPercentEmp", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Targets", "ActLeaveJobEmp", c => c.Int());
            AddColumn("dbo.Targets", "ChangePercentMM", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Targets", "ChangeMM", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Targets", "QuotationMM", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Targets", "OnsiteMM", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Targets", "ActChangePercentMM", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Targets", "ActChangeMM", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Targets", "ActQuotationMM", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Targets", "ActOnsiteMM", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Targets", "ActOnsiteMM");
            DropColumn("dbo.Targets", "ActQuotationMM");
            DropColumn("dbo.Targets", "ActChangeMM");
            DropColumn("dbo.Targets", "ActChangePercentMM");
            DropColumn("dbo.Targets", "OnsiteMM");
            DropColumn("dbo.Targets", "QuotationMM");
            DropColumn("dbo.Targets", "ChangeMM");
            DropColumn("dbo.Targets", "ChangePercentMM");
            DropColumn("dbo.Targets", "ActLeaveJobEmp");
            DropColumn("dbo.Targets", "ActLeaveJobPercentEmp");
            DropColumn("dbo.Targets", "ActSubLeader1");
            DropColumn("dbo.Targets", "ActSubLeader2");
            DropColumn("dbo.Targets", "ActLeader1Emp");
            DropColumn("dbo.Targets", "ActLeader2Emp");
            DropColumn("dbo.Targets", "ActManagerEmp");
            DropColumn("dbo.Targets", "ActChangeEmp");
            DropColumn("dbo.Targets", "ActChangePercentEmp");
            DropColumn("dbo.Targets", "LeaveJobEmp");
            DropColumn("dbo.Targets", "LeaveJobPercentEmp");
            DropColumn("dbo.Targets", "SubLeader1");
            DropColumn("dbo.Targets", "SubLeader2");
            DropColumn("dbo.Targets", "Leader1Emp");
            DropColumn("dbo.Targets", "Leader2Emp");
            DropColumn("dbo.Targets", "ManagerEmp");
            DropColumn("dbo.Targets", "ChangeEmp");
            DropColumn("dbo.Targets", "ChangePercentEmp");
            DropColumn("dbo.Targets", "ActKoritu");
            DropColumn("dbo.Targets", "Koritu");
        }
    }
}
