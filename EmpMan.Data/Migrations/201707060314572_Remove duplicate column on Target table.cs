namespace EmpMan.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveduplicatecolumnonTargettable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Targets", "ApprovedBy", c => c.String());
            AlterColumn("dbo.Targets", "ApprovedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Targets", "ApprovedDate", c => c.String());
            AlterColumn("dbo.Targets", "ApprovedBy", c => c.String(maxLength: 256));
        }
    }
}
