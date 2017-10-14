namespace EmpMan.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEventUsercolumntojobschedulertable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobSchedulers", "EventUser", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.JobSchedulers", "EventUser");
        }
    }
}
