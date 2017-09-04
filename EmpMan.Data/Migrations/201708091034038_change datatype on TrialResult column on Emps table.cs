namespace EmpMan.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedatatypeonTrialResultcolumnonEmpstable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Emps", "TrialResult", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Emps", "TrialResult", c => c.DateTime());
        }
    }
}
