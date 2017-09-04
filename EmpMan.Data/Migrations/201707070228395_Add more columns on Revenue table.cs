namespace EmpMan.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddmorecolumnsonRevenuetable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Revenues", "InMonthOnsiteMM", c => c.Decimal(precision: 24, scale: 10));
            AddColumn("dbo.Revenues", "InMonthSumIncludeOnsiteMM", c => c.Decimal(precision: 24, scale: 10));
            AddColumn("dbo.Revenues", "InMonthDevSumExcludeTransMM", c => c.Decimal(precision: 24, scale: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Revenues", "InMonthDevSumExcludeTransMM");
            DropColumn("dbo.Revenues", "InMonthSumIncludeOnsiteMM");
            DropColumn("dbo.Revenues", "InMonthOnsiteMM");
        }
    }
}
