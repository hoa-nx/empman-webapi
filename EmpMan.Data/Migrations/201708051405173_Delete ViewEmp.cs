namespace EmpMan.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteViewEmp : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EmpView", "ID", "dbo.Emps");
            DropIndex("dbo.EmpView", new[] { "ID" });
            DropTable("dbo.EmpView");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EmpView",
                c => new
                    {
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.EmpView", "ID");
            AddForeignKey("dbo.EmpView", "ID", "dbo.Emps", "ID");
        }
    }
}
