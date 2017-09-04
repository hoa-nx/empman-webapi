namespace EmpMan.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addcolumnsestimateables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Estimates", "ResultNote", c => c.String());
            AddColumn("dbo.RecruitmentStaffs", "OtherCertificated", c => c.String());
            DropColumn("dbo.Estimates", "Temperament");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Estimates", "Temperament", c => c.String());
            DropColumn("dbo.RecruitmentStaffs", "OtherCertificated");
            DropColumn("dbo.Estimates", "ResultNote");
        }
    }
}
