namespace EmpMan.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddcolumnOrderReceivedIDtoestimatetable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Estimates", "OrderReceivedID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Estimates", "OrderReceivedID");
        }
    }
}
