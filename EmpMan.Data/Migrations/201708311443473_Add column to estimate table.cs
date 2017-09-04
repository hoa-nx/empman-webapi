namespace EmpMan.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addcolumntoestimatetable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Estimates", "EstimateTypeMasterID", c => c.Int());
            AddColumn("dbo.Estimates", "EstimateTypeMasterDetailID", c => c.Int());
            CreateIndex("dbo.Estimates", new[] { "EstimateTypeMasterID", "EstimateTypeMasterDetailID" });
            AddForeignKey("dbo.Estimates", new[] { "EstimateTypeMasterID", "EstimateTypeMasterDetailID" }, "dbo.MasterDetails", new[] { "MasterID", "MasterDetailCode" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Estimates", new[] { "EstimateTypeMasterID", "EstimateTypeMasterDetailID" }, "dbo.MasterDetails");
            DropIndex("dbo.Estimates", new[] { "EstimateTypeMasterID", "EstimateTypeMasterDetailID" });
            DropColumn("dbo.Estimates", "EstimateTypeMasterDetailID");
            DropColumn("dbo.Estimates", "EstimateTypeMasterID");
        }
    }
}
