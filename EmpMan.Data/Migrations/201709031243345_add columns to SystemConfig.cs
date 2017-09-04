namespace EmpMan.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcolumnstoSystemConfig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SystemConfigs", "SystemValue", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SystemConfigs", "SystemValue");
        }
    }
}
