namespace EmpMan.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addlocationeventcolumntojobschedulertable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobSchedulers", "LocationEvent", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.JobSchedulers", "LocationEvent");
        }
    }
}
