namespace EmpMan.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddcolumnstoSeminarandEmpDetailWorktables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmpDetailWorks", "OnsiteCustomerID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmpDetailWorks", "OnsiteCustomerID");
        }
    }
}
