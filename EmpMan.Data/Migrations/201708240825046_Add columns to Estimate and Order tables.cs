namespace EmpMan.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddcolumnstoEstimateandOrdertables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Estimates", "OS", c => c.String());
            AddColumn("dbo.Estimates", "Language", c => c.String());
            AddColumn("dbo.Estimates", "OtherSofts", c => c.String());
            AddColumn("dbo.Estimates", "WarrantyMonths", c => c.Int());
            AddColumn("dbo.Estimates", "WarrantyStartDate", c => c.DateTime());
            AddColumn("dbo.OrderReceiveds", "OS", c => c.String());
            AddColumn("dbo.OrderReceiveds", "Language", c => c.String());
            AddColumn("dbo.OrderReceiveds", "OtherSofts", c => c.String());
            AddColumn("dbo.OrderReceiveds", "WarrantyMonths", c => c.Int());
            AddColumn("dbo.OrderReceiveds", "WarrantyStartDate", c => c.DateTime());
            AddColumn("dbo.OrderReceiveds", "CustomerConfirmDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderReceiveds", "CustomerConfirmDate");
            DropColumn("dbo.OrderReceiveds", "WarrantyStartDate");
            DropColumn("dbo.OrderReceiveds", "WarrantyMonths");
            DropColumn("dbo.OrderReceiveds", "OtherSofts");
            DropColumn("dbo.OrderReceiveds", "Language");
            DropColumn("dbo.OrderReceiveds", "OS");
            DropColumn("dbo.Estimates", "WarrantyStartDate");
            DropColumn("dbo.Estimates", "WarrantyMonths");
            DropColumn("dbo.Estimates", "OtherSofts");
            DropColumn("dbo.Estimates", "Language");
            DropColumn("dbo.Estimates", "OS");
        }
    }
}
