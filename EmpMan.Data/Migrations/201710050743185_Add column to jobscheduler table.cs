namespace EmpMan.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addcolumntojobschedulertable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobSchedulers", "EventDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.JobSchedulers", "EventDate");
        }
    }
}
