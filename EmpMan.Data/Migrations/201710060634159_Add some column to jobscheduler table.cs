namespace EmpMan.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addsomecolumntojobschedulertable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobSchedulers", "IsChanged", c => c.Boolean());
            AddColumn("dbo.JobSchedulers", "SMSNotifyRemider", c => c.Int());
            AddColumn("dbo.JobSchedulers", "EmailNotifyRemider", c => c.Int());
            AddColumn("dbo.JobSchedulers", "SMSNotifyCount", c => c.Int());
            AddColumn("dbo.JobSchedulers", "EmailNotifyCount", c => c.Int());
            AddColumn("dbo.JobSchedulers", "TemplateText", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.JobSchedulers", "TemplateText");
            DropColumn("dbo.JobSchedulers", "EmailNotifyCount");
            DropColumn("dbo.JobSchedulers", "SMSNotifyCount");
            DropColumn("dbo.JobSchedulers", "EmailNotifyRemider");
            DropColumn("dbo.JobSchedulers", "SMSNotifyRemider");
            DropColumn("dbo.JobSchedulers", "IsChanged");
        }
    }
}
