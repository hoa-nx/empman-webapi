namespace EmpMan.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changemasterdataandempstablecolumns : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Emps", "ExperienceBeforeContent", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Emps", "ExperienceBeforeContent", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
