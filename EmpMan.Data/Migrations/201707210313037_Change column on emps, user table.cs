namespace EmpMan.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changecolumnonempsusertable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppUsers", "TeamID", c => c.Int());
            AddColumn("dbo.AppUsers", "DeptID", c => c.Int());
            AddColumn("dbo.AppUsers", "CompanyID", c => c.Int());
            AddColumn("dbo.Emps", "CurrentCompanyID", c => c.Int());
            DropColumn("dbo.AppUsers", "DeptCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AppUsers", "DeptCode", c => c.String(maxLength: 10));
            DropColumn("dbo.Emps", "CurrentCompanyID");
            DropColumn("dbo.AppUsers", "CompanyID");
            DropColumn("dbo.AppUsers", "DeptID");
            DropColumn("dbo.AppUsers", "TeamID");
        }
    }
}
